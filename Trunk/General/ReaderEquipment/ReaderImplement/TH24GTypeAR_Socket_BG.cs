using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReaderEquipment.Entity.TH24G;
using System.Threading;
using ReaderEquipment.Entity;
using System.Net.Sockets;
using System.Net;

namespace ReaderEquipment.ReaderImplement
{
    /// <summary>
    /// 2.4G读写器API，基于socket，后台模式，适合于不需要界面交互的后台服务使用。
    /// </summary>
    class TH24GTypeAR_Socket_BG : AbstractReaderBackground
    {
        #region 变量定义

        private delegate void MachineIDChangedDelegate(object sender, MachineIDChangedEventArgs args);
        /// <summary>
        /// 机号变更处理
        /// </summary>
        private event MachineIDChangedDelegate OnMachineIDChanging;

        /// <summary>
        /// 设备ID。
        /// </summary>
        public override int MachineID
        {
            get
            {
                return base.MachineID;
            }
            set
            {
                if (OnMachineIDChanging != null && value != 0)
                {
                    MachineIDChangedEventArgs args = new MachineIDChangedEventArgs();
                    args.OldMacID = base.MachineID;
                    args.NewMacID = value;
                    OnMachineIDChanging(null, args);
                }

                base.MachineID = value;
            }
        }

        List<string> _listHistoryIndex;

        /// <summary>
        /// 重试最大次数
        /// </summary>
        private short _iRetryMaxCount;

        /// <summary>
        /// 数据返回的最长等待时间(单位：秒)
        /// </summary>
        private int _iMaxRvDataTime;

        private bool _isConnected;
        /// <summary>
        /// 标识设备是否已连接
        /// </summary>
        public bool IsConnected
        {
            get { return _isConnected; }
        }

        private bool _isReading;

        private Dictionary<int, ReaderExDetailInfo> _dicReaderDetails;
        /// <summary>
        /// 设备状态详细。
        /// </summary>
        public Dictionary<int, ReaderExDetailInfo> DicReaderDetails
        {
            get { return _dicReaderDetails; }
            set { _dicReaderDetails = value; }
        }
        /// <summary>
        /// 网络接口。
        /// </summary>
        private Socket _SendClient;
        /// <summary>
        /// 监听设备端口。
        /// </summary>
        private const short ListenPort = 1024;

        private ManualResetEvent _ConnectDone;

        /// <summary>
        /// 读数时钟。
        /// </summary>
        private System.Timers.Timer _tmrReadBatchData;

        /// <summary>
        /// 检查无数据返回时钟。
        /// </summary>
        private System.Timers.Timer _tmrChkNullData;

        /// <summary>
        /// 检查机器内部时钟。
        /// </summary>
        private System.Timers.Timer _tmrCheckTime;

        private System.Timers.Timer _tmrReceiveData;

        /// <summary>
        /// 当前接收的数据类型
        /// </summary>
        private EnumRecDataMode _currentDataType;

        #endregion

        public TH24GTypeAR_Socket_BG()
        {
            try
            {
                this._isConnected = false;

                this._iMaxRvDataTime = -3600;
                this._iRetryMaxCount = 2000;
                this._isReading = false;
                this.MachineID = 0;

                this._dicReaderDetails = new Dictionary<int, ReaderExDetailInfo>();

                this.OnMachineIDChanging += new MachineIDChangedDelegate(TH24GTypeAR_Socket_OnMachineIDChanging);

                this._ConnectDone = new ManualResetEvent(false);

                this._tmrReadBatchData = new System.Timers.Timer();
                this._tmrReadBatchData.Interval = 1000;
                this._tmrReadBatchData.Elapsed += new System.Timers.ElapsedEventHandler(_tmrReadBatchData_Elapsed);

                this._tmrChkNullData = new System.Timers.Timer();
                this._tmrChkNullData.Interval = 3000;
                this._tmrChkNullData.Elapsed += new System.Timers.ElapsedEventHandler(_tmrChkNullData_Elapsed);

                this._tmrCheckTime = new System.Timers.Timer();
                this._tmrCheckTime.Interval = 5000;
                this._tmrCheckTime.Elapsed += new System.Timers.ElapsedEventHandler(_tmrCheckTime_Elapsed);

                this._tmrReceiveData = new System.Timers.Timer();
                this._tmrReceiveData.Interval = 4000;
                this._tmrReceiveData.Elapsed += new System.Timers.ElapsedEventHandler(_tmrReceiveData_Elapsed);
            }
            catch (Exception ex)
            {
                ReaderGeneral.ConsoleWrite(ex.Message);

                ReaderExceptionEventArgs args = new ReaderExceptionEventArgs();
                args.ExceptionObject = ex;
                args.RecordTime = DateTime.Now;
                OnReaderException(args);
            }
        }

        private object _lockRec;
        private bool _IsReceiving;
        void _tmrReceiveData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //if (this._lockRec == null)
            //{
            //    this._lockRec = new object();
            //}
            if (!_IsReceiving)
            {
                DataReceive();
            }
        }

        /// <summary>
        /// 闲时获取机器的内部时间值并作校验
        /// </summary>
        void _tmrCheckTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CheckMachineTime();
        }

        /// <summary>
        /// 检查机器时间
        /// </summary>
        private void CheckMachineTime()
        {
            DateTime dtStart = DateTime.Now.Date;
            DateTime dtEnd = DateTime.Now.Date.AddHours(1);

            if (DateTime.Now > dtStart && DateTime.Now < dtEnd)// 在自检时段内并且未完成自检，则重复发送时间获取时间的指令
            {
                if (this._currentDataType != EnumRecDataMode.AdjustTimeMode)
                {
                    if (this._tmrReadBatchData.Enabled)
                    {
                        this._tmrReadBatchData.Stop();
                    }
                    this._currentDataType = EnumRecDataMode.AdjustTimeMode;
                }

                OrderSend_GetMacTime(base.MachineID);
            }
            else
            {
                if (this._currentDataType != EnumRecDataMode.CollectRecordMode)
                {
                    foreach (KeyValuePair<int, ReaderExDetailInfo> detailPair in this.DicReaderDetails)
                    {
                        if (detailPair.Value.Ex_dtLastTimeChk == DateTime.MinValue || detailPair.Value.CurrentStatus == EnumEquipmentStatus.Disconneted)
                        {
                            //读写器未连接或检查时间未被刷新时不作错误提醒
                            continue;
                        }
                        if (detailPair.Value.Ex_dtLastTimeChk < detailPair.Value.Ex_dtLastRunTime.AddMinutes(-1) || detailPair.Value.Ex_dtLastTimeChk > detailPair.Value.Ex_dtLastRunTime.AddMinutes(1))
                        {
                            if (!detailPair.Value.Ex_lIsSendTimeChkWarning && detailPair.Value.CurrentStatus != EnumEquipmentStatus.TimeException)
                            {
                                ReaderExceptionEventArgs readerEx = new ReaderExceptionEventArgs();
                                readerEx.EquipmentStatus = EnumEquipmentStatus.TimeException;
                                readerEx.MachineID = detailPair.Value.Ex_MacID;
                                readerEx.ExceptionObject = new Exception("机器时间有误，机号：" + readerEx.MachineID);
                                OnReaderException(readerEx);

                                OrderSend_ResetTime(readerEx.MachineID);

                                detailPair.Value.CurrentStatus = EnumEquipmentStatus.TimeException;
                                detailPair.Value.Ex_lIsSendTimeChkWarning = true;
                            }
                        }
                    }

                    this._currentDataType = EnumRecDataMode.CollectRecordMode;
                    if (!this._tmrReadBatchData.Enabled)
                    {
                        this._tmrReadBatchData.Start();
                    }
                }
            }
        }

        /// <summary>
        /// 检查设备是否无数据返回
        /// </summary>
        void _tmrChkNullData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.DicReaderDetails == null)
            {
                return;
            }

            foreach (KeyValuePair<int, ReaderExDetailInfo> detailPair in this.DicReaderDetails)
            {
                if (detailPair.Value.Ex_dtRvData < DateTime.Now.AddMinutes(-1))
                {
                    //如果超过一分钟未更新接收数据的时间，则开始累计
                    detailPair.Value.Ex_iCounterRvData++;
                }

                if (detailPair.Value.Ex_dtRvData < DateTime.Now.AddSeconds(_iMaxRvDataTime))//最长等待时间内无任何数据返回
                {
                    if (detailPair.Value.Ex_dtRvData == DateTime.MinValue)
                    {
                        continue;
                    }
                    if (detailPair.Value.Ex_iCounterRvData > _iRetryMaxCount)//达到最大重试次数
                    {
                        if (!detailPair.Value.Ex_lIsSendNullDataWarning)
                        {
                            detailPair.Value.Ex_lIsSendNullDataWarning = true;
                            ReaderExceptionEventArgs exArgs = new ReaderExceptionEventArgs();
                            exArgs.EquipmentStatus = EnumEquipmentStatus.Disconneted;
                            exArgs.ExceptionObject = new Exception("连接中断，无数据返回。");
                            exArgs.MachineID = detailPair.Value.Ex_MacID;
                            exArgs.RecordTime = DateTime.Now;

                            if (detailPair.Value.CurrentStatus != EnumEquipmentStatus.Disconneted)
                            {
                                EquipmentStatusEventArgs statusArgs = new EquipmentStatusEventArgs();
                                statusArgs.EquipmentStatus = EnumEquipmentStatus.Disconneted;
                                statusArgs.MachineID = detailPair.Value.Ex_MacID;
                                statusArgs.RecordTime = DateTime.Now;
                                OnStatusChange(statusArgs);

                                detailPair.Value.CurrentStatus = EnumEquipmentStatus.Disconneted;
                            }

                            OnReaderException(exArgs);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 批量读数时钟
        /// </summary>
        void _tmrReadBatchData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int iMacID = base.MachineID;
            try
            {
                //CheckMachineTime();

                if (!this._isReading)
                {
                    this._isReading = true;
                }

                if (this._currentDataType == EnumRecDataMode.CollectRecordMode)
                {
                    OrderSend_GetBatchData(iMacID);
                }
            }
            catch (Exception ex)
            {
                Common.General.WriteLocalLogs(Common.General.GetCurrentFuncName(), "批量读数异常：" + ex.Message, string.Empty, SystemLog.SystemLog.LogType.Error, SystemLog.SystemLog.FileType.LogFile);

                ReaderExceptionEventArgs args = new ReaderExceptionEventArgs();
                args.MachineID = iMacID;
                args.ExceptionObject = ex;
                args.RecordTime = DateTime.Now;
                OnReaderException(args);
            }
        }

        void TH24GTypeAR_Socket_OnMachineIDChanging(object sender, MachineIDChangedEventArgs args)
        {
            if (args != null)
            {
                int iMacID = args.NewMacID;

                //添加入自检记录列表
                if (this.DicReaderDetails == null)
                {
                    this.DicReaderDetails = new Dictionary<int, ReaderExDetailInfo>();
                }
                if (!this.DicReaderDetails.Keys.Contains(iMacID))
                {
                    ReaderExDetailInfo readerExDetail = new ReaderExDetailInfo();
                    readerExDetail.Ex_MacID = iMacID;
                    this.DicReaderDetails.Add(iMacID, readerExDetail);
                }
            }
        }

        public override ReturnValueInfo Connect()
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();

            //检查非空值
            if (string.IsNullOrEmpty(base.ReaderIP))
            {
                rvInfo.IsSuccess = false;
                rvInfo.MessageText = "IP地址不能为空，属性为ReaderIP";
                return rvInfo;
            }

            try
            {
                //检查地址格式
                string strRegx = @"(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])";
                bool lIPChk = Common.General.IsRegEx(strRegx, base.ReaderIP);
                if (!lIPChk)
                {
                    rvInfo.MessageText = "IP地址格式有误";
                    return rvInfo;
                }

                //转换IP格式
                List<byte> listIP = new List<byte>();
                string[] arrIP = base.ReaderIP.Split('.');
                foreach (string strIPItem in arrIP)
                {
                    listIP.Add(Convert.ToByte(strIPItem));
                }

                //打开连接                
                IPAddress ipAddr = new IPAddress(listIP.ToArray());

                ReaderGeneral.ConsoleWrite("Begin Connecting............");

                this._SendClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //this._SendClient.SendTimeout = 3000;
                //this._SendClient.ReceiveTimeout = 3000;
                this._SendClient.BeginConnect(ipAddr, ListenPort, new AsyncCallback(ConnectCallBack), this._SendClient);

                this._listHistoryIndex = new List<string>();

                ReaderGeneral.ConsoleWrite("Into Waiting.............");

                //this._ConnectDone.WaitOne();

                ReaderGeneral.ConsoleWrite("Wait Complete...........");
            }
            catch (Exception ex)
            {
                rvInfo.MessageText = "连接出现异常：" + ex.Message;
                Common.General.WriteLocalLogs(Common.General.GetCurrentFuncName(), "连接出现异常：" + ex.Message, string.Empty, SystemLog.SystemLog.LogType.Error, SystemLog.SystemLog.FileType.LogFile);
            }

            return rvInfo;
        }

        public override bool Disconnect()
        {
            try
            {
                if (!this._isConnected)
                {
                    return true;
                }
                if (this._tmrChkNullData != null)
                {
                    this._tmrChkNullData.Stop();
                }
                if (this._tmrReadBatchData != null)
                {
                    this._tmrReadBatchData.Stop();
                }
                if (this._tmrReceiveData != null)
                {
                    this._tmrReceiveData.Stop();
                }
                if (this._tmrCheckTime != null)
                {
                    this._tmrCheckTime.Stop();
                }

                if (this._dicReaderDetails != null)
                {
                    foreach (KeyValuePair<int, ReaderExDetailInfo> detailPair in this._dicReaderDetails)
                    {
                        if (detailPair.Value.CurrentStatus != EnumEquipmentStatus.Disconneted)
                        {
                            detailPair.Value.CurrentStatus = EnumEquipmentStatus.Disconneted;

                            EquipmentStatusEventArgs statusArgs = new EquipmentStatusEventArgs();
                            statusArgs.EquipmentStatus = EnumEquipmentStatus.Disconneted;
                            statusArgs.RecordTime = DateTime.Now;
                            statusArgs.MachineID = detailPair.Value.Ex_MacID;
                            OnStatusChange(statusArgs);
                        }
                        detailPair.Value.ResetAllSign();
                    }
                }

                this._listHistoryIndex.Clear();

                if (this._SendClient == null)
                {
                    this._SendClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                this._SendClient.Shutdown(SocketShutdown.Both);
                this._SendClient.Disconnect(false);
                this._SendClient.Close();

                ReaderGeneral.ConsoleWrite("Disconnected..........");
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs args = new ReaderExceptionEventArgs();
                args.ExceptionObject = ex;
                args.RecordTime = DateTime.Now;
                OnReaderException(args);
                return false;
            }
            finally
            {
                this._isConnected = false;
            }
            return true;
        }

        private void ConnectCallBack(IAsyncResult res)
        {
            try
            {
                // 从state对象获取socket.  
                Socket client = (Socket)res.AsyncState;
                // 完成连接.  
                client.EndConnect(res);

                ReaderGeneral.ConsoleWrite("Socket connected to " + client.RemoteEndPoint.ToString());
                this._isConnected = true;
            }
            catch (Exception ex)
            {

                ReaderGeneral.ConsoleWrite(ex.Message);
                this._SendClient = null;
                ReaderGeneral.ConsoleWrite("需要重新初始化..............");
            }

            // 连接已完成，主线程继续.  
            //this._ConnectDone.Set();
            ReaderGeneral.ConsoleWrite("Connect OK............");
        }

        public override ReturnValueInfo StartRead()
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();
            try
            {
                if (!this._isConnected)
                {
                    rvInfo.MessageText = "设备未连接。";
                    rvInfo.IsSuccess = false;
                    return rvInfo;
                }

                this._currentDataType = EnumRecDataMode.AdjustTimeMode;

                if (this._tmrReadBatchData != null)
                {
                    this._tmrReadBatchData.Start();
                }
                if (this._tmrChkNullData != null)
                {
                    this._tmrChkNullData.Start();
                }
                if (this._tmrCheckTime != null)
                {
                    this._tmrCheckTime.Start();
                }
                if (this._tmrReceiveData != null)
                {
                    this._tmrReceiveData.Start();
                }

                this._isReading = true;
            }
            catch (Exception ex)
            {
                rvInfo.MessageText = ex.Message;
            }
            rvInfo.IsSuccess = true;
            return rvInfo;
        }

        public override void StopRead()
        {
            if (!_isConnected)
            {
                return;
            }

            if (this._tmrReadBatchData != null)
            {
                this._tmrReadBatchData.Stop();
                this._isReading = false;
            }
            if (this._tmrChkNullData != null)
            {
                this._tmrChkNullData.Stop();
            }
            if (this._tmrReceiveData != null)
            {
                this._tmrReceiveData.Stop();
            }

            if (this._dicReaderDetails != null)
            {
                foreach (KeyValuePair<int, ReaderExDetailInfo> detailPair in this._dicReaderDetails)
                {
                    detailPair.Value.ResetSign();
                }
            }

            this._listHistoryIndex.Clear();

            this._isReading = false;
        }

        public override List<TagInformationInfo> GetTagList(string strMacID)
        {
            this._isReading = true;
            this._currentDataType = EnumRecDataMode.CollectRecordMode;
            OrderSend_GetBatchData(Convert.ToInt32(strMacID));
            //this.Invoke(new DelegateDataReceiving(DataReceive));
            DataReceive();
            return null;
        }

        /// <summary>
        /// 获取批量数据
        /// </summary>
        private void OrderSend_GetBatchData(int iMachineID)
        {
            byte[] arrDataSend = { 0xC0, 0x00, 0x05, 0x00, 0x00, 0x00, 0xC1 };
            byte[] bMacID = BitConverter.GetBytes(iMachineID);
            arrDataSend[1] = bMacID[0];

            byte bLowCRC;
            byte bHighCRC;
            ReaderGeneral.CRC16_CCITT(arrDataSend, out bHighCRC, out bLowCRC);
            arrDataSend[4] = bLowCRC;
            arrDataSend[5] = bHighCRC;

            try
            {
                if (this._SendClient != null)
                {
                    ReaderGeneral.ConsoleWrite("Begin Send...." + iMachineID.ToString());

                    if (this._listHistoryIndex.Count > 1024 * 100)
                    {
                        this._listHistoryIndex.Clear();
                    }

                    this._SendClient.Send(arrDataSend);

                    ReaderGeneral.ConsoleWrite("End Send...." + iMachineID.ToString());
                }
            }
            catch (Exception ex)
            {
                ReaderGeneral.ConsoleWrite(ex.Message);
                //throw ex;
            }
        }

        /// <summary>
        /// 确认批量数据
        /// </summary>
        private void OrderSend_ConfirmBatchData(int iMacID, string strRecordIndex)
        {
            byte[] arrDataSend = { 0xC0, 0x00, 0x06, 0x02, 0x00, 0x00, 0x00, 0x00, 0xC1 };
            byte[] byMacID = BitConverter.GetBytes(iMacID);
            arrDataSend[1] = byMacID[0];

            int iRecordIndex = Convert.ToInt32(strRecordIndex, 16);
            byte[] byRecordIndex = BitConverter.GetBytes(iRecordIndex);
            arrDataSend[4] = byRecordIndex[1];
            arrDataSend[5] = byRecordIndex[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(arrDataSend, out highCRC, out lowCRC);
            arrDataSend[6] = lowCRC;
            arrDataSend[7] = highCRC;

            try
            {
                if (this._SendClient != null)
                {
                    this._SendClient.Send(arrDataSend);
                    //DataReceive();
                }
            }
            catch (Exception ex)
            {
                ReaderExceptionEventArgs args = new ReaderExceptionEventArgs();
                args.MachineID = iMacID;
                args.ExceptionObject = ex;
                args.RecordTime = DateTime.Now;
                OnReaderException(args);
            }
        }

        /// <summary>
        /// 获取机器时间
        /// </summary>
        public void OrderSend_GetMacTime(int iMachineID)
        {
            byte[] arrDataSend = { 0xC0, 0x00, 0xAA, 0x00, 0x00, 0x00, 0xC1 };
            byte[] bMacID = BitConverter.GetBytes(iMachineID);
            arrDataSend[1] = bMacID[0];

            byte bLowCRC;
            byte bHighCRC;
            ReaderGeneral.CRC16_CCITT(arrDataSend, out bHighCRC, out bLowCRC);
            arrDataSend[4] = bLowCRC;
            arrDataSend[5] = bHighCRC;

            try
            {
                if (this._SendClient != null)
                {
                    this._SendClient.Send(arrDataSend);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 重置机器时间
        /// </summary>
        private void OrderSend_ResetTime(int iMachineID)
        {
            byte[] data = { 0xC0, 0xFF, 0xA9, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            byte[] bMacID = BitConverter.GetBytes(iMachineID);
            data[1] = bMacID[0];

            int year = DateTime.Now.Year - 2000;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            data[4] = BitConverter.GetBytes(year)[0];
            data[5] = BitConverter.GetBytes(month)[0];
            data[6] = BitConverter.GetBytes(day)[0];
            data[7] = BitConverter.GetBytes(hour)[0];
            data[8] = BitConverter.GetBytes(minute)[0];
            data[9] = BitConverter.GetBytes(second)[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[11] = lowCRC;
            data[12] = highCRC;

            try
            {
                if (this._SendClient != null)
                {
                    this._SendClient.Send(data);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        ///  抹除所有数据
        /// </summary>
        private void OrderSend_DeleteRecord(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xE9, 0x00, 0x00, 0x00, 0x00, 0xC1 };
            byte[] iMachineID = BitConverter.GetBytes(machineID);
            data[1] = iMachineID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                if (this._SendClient != null)
                {
                    this._SendClient.Send(data);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void DataReceive()
        {
            this._IsReceiving = true;

            try
            {
                if (!this._isReading)
                {
                    this._IsReceiving = false;
                    return;
                }
                ReaderGeneral.ConsoleWrite("开始接收数据：" + DateTime.Now.ToString());

                byte[] arrBuff = new byte[10240];


                int iRecLen = this._SendClient.Receive(arrBuff);

                ReaderGeneral.ConsoleWrite("接收到数据长度为：" + iRecLen);

                List<byte> listBuff = arrBuff.ToList();
                listBuff.RemoveRange(iRecLen, listBuff.Count - iRecLen);

                ReaderGeneral.ConsoleWrite("数据池数据填充完毕：" + DateTime.Now.ToString());

                //转换正常格式数据列
                //string strChange = ReaderGeneral.ChangeHexArrayToString(arrBuff);
                List<string> listSource = ReaderGeneral.HandleSourceDataToList(listBuff.ToArray());
                ReaderGeneral.ConsoleWrite("接收数据量：" + listSource.Count + "条。 " + DateTime.Now.ToString());

                //符合正常监控数据的长度。
                List<string> listRecord = listSource.Where(x => x.Length == 62).ToList();
                if (listRecord != null && listRecord.Count > 0)
                    ReaderGeneral.ConsoleWrite("正常数据数量：" + listRecord.Count);
                //符合确认返回数据的长度。
                List<string> listConfirm = listSource.Where(x => x.Length == 14).ToList();
                if (listConfirm != null && listConfirm.Count > 0)
                    ReaderGeneral.ConsoleWrite("确认数据：" + listConfirm.Count);
                //符合时间数据的长度。
                List<string> listTime = listSource.Where(x => x.Length == 28).ToList();
                if (listTime != null && listTime.Count > 0)
                    ReaderGeneral.ConsoleWrite("时间值数据：" + listTime.Count);

                if (this._currentDataType == EnumRecDataMode.CollectRecordMode)
                {
                    #region  处理正常监控数据

                    Dictionary<string, TagInfoResultEventArgs> dicReceive = new Dictionary<string, TagInfoResultEventArgs>();
                    foreach (string strTag in listRecord)
                    {
                        TagInformationInfo tagInfo = new TagInformationInfo();

                        try
                        {
                            tagInfo = tagInfo.GetTagInformationInfoIns(strTag);
                        }
                        catch (Exception ex)
                        {
                            ReaderGeneral.ConsoleWrite(ex.Message);
                            if (!string.IsNullOrEmpty(ex.Message))
                            {
                                string[] arrString = ex.Message.Split('-');
                                if (arrString.Length > 1)
                                {
                                    TagInfoResultEventArgs args = new TagInfoResultEventArgs();
                                    args.TagInformationList = new List<TagInformationInfo>();
                                    TagInformationInfo tagThrow = new TagInformationInfo();
                                    tagThrow.MachineID = arrString[1];
                                    args.TagInformationList.Add(tagThrow);
                                    OnTagResult(args);
                                }
                            }
                            tagInfo = null;
                        }

                        if (tagInfo != null)
                        {
                            if (dicReceive.Keys.Contains(tagInfo.MachineID))
                            {
                                if (dicReceive[tagInfo.MachineID] == null)
                                {
                                    dicReceive[tagInfo.MachineID] = new TagInfoResultEventArgs();
                                    dicReceive[tagInfo.MachineID].TagInformationList = new List<TagInformationInfo>();
                                }
                                dicReceive[tagInfo.MachineID].TagInformationList.Add(tagInfo);
                            }
                            else
                            {
                                TagInfoResultEventArgs args = new TagInfoResultEventArgs();
                                args.TagInformationList = new List<TagInformationInfo>();
                                args.TagInformationList.Add(tagInfo);
                                dicReceive.Add(tagInfo.MachineID, args);
                            }
                            if (tagInfo.States == "01")
                            {
                                dicReceive[tagInfo.MachineID].ExamineNum = tagInfo.RecordIndex;
                            }
                        }
                    }

                    if (dicReceive != null && dicReceive.Count > 0)
                    {
                        foreach (KeyValuePair<string, TagInfoResultEventArgs> kvPair in dicReceive)
                        {
                            ReaderGeneral.ConsoleWrite("抛出数据量：" + kvPair.Value.TagInformationList.Count);
                            OnTagResult(kvPair.Value);
                        }
                    }

                    #endregion

                    #region 处理确认数据

                    foreach (string confirmItem in listConfirm)
                    {
                        try
                        {
                            int iMacID = Convert.ToInt32(confirmItem.Substring(2, 2), 16);
                            string strFinish = GetFinishedCollectionInstruction(iMacID);
                            string strUpload = GetUploadedCollectionInstruction(iMacID);
                            if (confirmItem == strFinish || confirmItem == strUpload)
                            {
                                TagInfoResultEventArgs arg = new TagInfoResultEventArgs();
                                arg.TagInformationList = new List<TagInformationInfo>();
                                TagInformationInfo tagInfo = new TagInformationInfo();
                                tagInfo.MachineID = iMacID.ToString();
                                arg.TagInformationList.Add(tagInfo);
                                ReaderGeneral.ConsoleWrite("no more data .................................................." + iMacID.ToString());
                                this.OnTagResult(arg);
                            }
                        }
                        catch (Exception ex)
                        {
                            ReaderGeneral.ConsoleWrite("Error: " + ex.Message);
                        }
                    }

                    #endregion
                }
                else if (this._currentDataType == EnumRecDataMode.AdjustTimeMode)
                {
                    #region 处理时间检查

                    if (listTime != null && listTime.Count > 0)
                    {
                        foreach (string timeItem in listTime)
                        {
                            int iMacIDChkTime = 0;
                            DateTime? dtGet = DataHandling_FormatMacTimeVal(timeItem, ref iMacIDChkTime);
                            if (iMacIDChkTime != 0)
                            {
                                if (!this.DicReaderDetails.Keys.Contains(iMacIDChkTime))
                                {
                                    return;
                                }

                                ReaderExDetailInfo readerChk = this.DicReaderDetails[iMacIDChkTime];
                                if (dtGet != null)
                                {
                                    // 获取到有效的机器时间后，将此时间值赋予获取成功标识。
                                    readerChk.Ex_dtLastTimeChk = dtGet.Value;
                                    readerChk.Ex_iCounterTimeChk = 0;
                                    readerChk.Ex_lIsSendTimeChkWarning = false;
                                }
                                else
                                {
                                    readerChk.Ex_iCounterTimeChk++;
                                }
                                readerChk.Ex_dtLastRunTime = DateTime.Now;
                            }
                        }
                    }

                    #endregion
                }

                ReaderGeneral.ConsoleWrite("数据接收完毕。" + DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                ReaderGeneral.ConsoleWrite(ex.Message);
            }

            this._IsReceiving = false;
        }

        protected override void OnTagResult(TagInfoResultEventArgs tagArgs)
        {
            #region 有数据返回，设置读写器状态为连通中

            if (this._isReading)
            {
                if (tagArgs != null && tagArgs.TagInformationList != null && tagArgs.TagInformationList.Count > 0)
                {
                    int iMacIDRv = Convert.ToInt32(tagArgs.TagInformationList[0].MachineID);
                    if (this.DicReaderDetails.Keys.Contains(iMacIDRv))
                    {
                        ReaderExDetailInfo readerChk = this.DicReaderDetails[iMacIDRv];
                        readerChk.Ex_dtRvDataFormatChk = DateTime.Now;
                        readerChk.Ex_dtRvData = DateTime.Now;
                        readerChk.Ex_iCounterRvData = 0;

                        if (readerChk.CurrentStatus != EnumEquipmentStatus.Conneted)
                        {
                            EquipmentStatusEventArgs statusArgs = new EquipmentStatusEventArgs();
                            statusArgs.MachineID = iMacIDRv;
                            statusArgs.EquipmentStatus = EnumEquipmentStatus.Conneted;
                            statusArgs.RecordTime = DateTime.Now;

                            readerChk.CurrentStatus = EnumEquipmentStatus.Conneted;
                            readerChk.Ex_lIsSendInvalidDataWarning = false;

                            OnStatusChange(statusArgs);
                        }
                    }
                }
            }

            #endregion

            try
            {
                if (tagArgs != null)
                {
                    if (tagArgs.TagInformationList != null && tagArgs.TagInformationList.Count > 0 && !string.IsNullOrEmpty(tagArgs.ExamineNum))
                    {
                        if (!this._listHistoryIndex.Contains(tagArgs.ExamineNum))
                        {
                            this._listHistoryIndex.Add(tagArgs.ExamineNum);
                            base.OnTagResult(tagArgs);
                        }

                        OrderSend_ConfirmBatchData(int.Parse(tagArgs.TagInformationList[0].MachineID), tagArgs.ExamineNum);
                        ReaderGeneral.ConsoleWrite("数据确认流水号：【" + tagArgs.ExamineNum + "】，数据来源机：" + tagArgs.TagInformationList[0].MachineID);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.General.WriteLocalLogs(Common.General.GetCurrentFuncName(), "确认数据异常：" + ex.Message, string.Empty, SystemLog.SystemLog.LogType.Error, SystemLog.SystemLog.FileType.LogFile);
            }
        }

        private DateTime? DataHandling_FormatMacTimeVal(string strSourceData, ref int iMacID)
        {
            DateTime? dtMac = null;
            try
            {
                if (!string.IsNullOrEmpty(strSourceData))
                {
                    iMacID = Convert.ToInt32(strSourceData.Substring(2, 2), 16);
                    int iYear = Convert.ToInt32(strSourceData.Substring(8, 2), 16) + 2000;
                    int iMonth = Convert.ToInt32(strSourceData.Substring(10, 2), 16);
                    int iDay = Convert.ToInt32(strSourceData.Substring(12, 2), 16);
                    int iHour = Convert.ToInt32(strSourceData.Substring(14, 2), 16);
                    int iMinute = Convert.ToInt32(strSourceData.Substring(16, 2), 16);
                    int iSecond = Convert.ToInt32(strSourceData.Substring(18, 2), 16);
                    string strDate = iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString();
                    DateTime dtDate = DateTime.Parse(strDate);
                    //TIPS: 使读写器的时间误差在2分钟内。
                    if (dtDate >= DateTime.Now.AddMinutes(-1) && dtDate <= DateTime.Now.AddMinutes(1))
                    {
                        dtMac = dtDate;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return dtMac;
        }

        /// <summary>
        /// 获取数据已完成采集的回发指令
        /// </summary>
        /// <returns></returns>
        private string GetFinishedCollectionInstruction(int iMacID)
        {
            if (iMacID == 0)
            {
                return null;
            }

            string strInst = string.Empty;

            byte[] data = { 0xC2, 0x00, 0x0A, 0x00, 0x00, 0x00, 0xC3 };
            byte[] iMachineID = BitConverter.GetBytes(iMacID);
            data[1] = iMachineID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            string strTmp = BitConverter.ToString(data);
            string[] arrTmp = strTmp.Split('-');
            if (arrTmp.Length != 7)
            {
                return null;
            }
            foreach (string strItem in arrTmp)
            {
                strInst += strItem;
            }

            return strInst;
        }

        /// <summary>
        /// 获取数据已上传完毕的回发指令
        /// </summary>
        /// <returns></returns>
        private string GetUploadedCollectionInstruction(int iMacID)
        {
            if (iMacID == 0)
            {
                return null;
            }

            string strInst = string.Empty;

            byte[] data = { 0xC2, 0x00, 0x09, 0x00, 0x00, 0x00, 0xC3 };
            byte[] iMachineID = BitConverter.GetBytes(iMacID);
            data[1] = iMachineID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            string strTmp = BitConverter.ToString(data);
            string[] arrTmp = strTmp.Split('-');
            if (arrTmp.Length != 7)
            {
                return null;
            }
            foreach (string strItem in arrTmp)
            {
                strInst += strItem;
            }

            return strInst;
        }
    }
}
