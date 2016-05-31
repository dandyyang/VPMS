using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReaderEquipment.Entity.TH24G;
using ReaderEquipment.Entity;
using Common;
using System.Configuration;

namespace ReaderEquipment.ReaderImplement
{
    public partial class TH24GTypeAR : AbstractReader // Form
    {
        #region 字段、属性

        private delegate void MachineIDChangedDelegate(object sender, MachineIDChangedEventArgs args);
        private event MachineIDChangedDelegate OnMachineIDChanging;
        public override int MachineID
        {
            get
            {
                return base.MachineID;
            }
            set
            {
                if (OnMachineIDChanging != null)
                {
                    MachineIDChangedEventArgs args = new MachineIDChangedEventArgs();
                    args.OldMacID = base.MachineID;
                    args.NewMacID = value;
                    OnMachineIDChanging(null, args);
                }

                base.MachineID = value;
            }
        }

        private object _LockDictionary;
        private Dictionary<int, ReaderExDetailInfo> _dicReaderExDetails;
        /// <summary>
        /// 机器状态明细
        /// </summary>
        public Dictionary<int, ReaderExDetailInfo> DicReaderExDetails
        {
            get
            {
                return this._dicReaderExDetails;
            }
            set
            {
                if (this._LockDictionary == null)
                {
                    this._LockDictionary = new object();
                }
                lock (this._LockDictionary)
                {
                    this._dicReaderExDetails = value;
                }
            }
        }

        /// <summary>
        /// 当前接收的数据类型
        /// </summary>
        private EnumRecDataMode _currentDataType;

        /// <summary>
        /// 批量读取数据的计时器
        /// </summary>
        System.Timers.Timer _tmrReadData;
        /// <summary>
        /// 虚拟COM口套件
        /// </summary>
        private AxMSCommLib.AxMSComm _MSCOM;
        /// <summary>
        /// 内部转换用标签属性实体
        /// </summary>
        //private List<TagFormat> _listTagFormat;
        private List<TagInformationInfo> _listTagInfos;

        private short _iRecordListMaxLen;
        /// <summary>
        /// 批量返回的数据记录列表最大容量
        /// </summary>
        public short RecordListMaxLen
        {
            set { this._iRecordListMaxLen = value; }
        }

        /// <summary>
        /// 单条记录的最大长度
        /// </summary>
        private short _iRecordMaxLen;

        /// <summary>
        /// 重试最大次数
        /// </summary>
        private short _iRetryMaxCount;

        /// <summary>
        /// 数据返回的最长等待时间(单位：秒)
        /// </summary>
        private int _iMaxRvDataTime;

        /// <summary>
        /// 自检时钟，循环检查是否有数据返回
        /// </summary>
        private System.Timers.Timer _tmrCheckDataRec;

        /// <summary>
        /// 自检机器时间的计时器
        /// </summary>
        private System.Timers.Timer _tmrCheckTime;

        #endregion

        public TH24GTypeAR()
        {
            InitializeComponent();

            this._tmrReadData = new System.Timers.Timer();
            this._tmrReadData.Interval = 800;
            this._tmrReadData.Elapsed += new System.Timers.ElapsedEventHandler(_tmrReadData_Elapsed);

            this._tmrCheckTime = new System.Timers.Timer();
            this._tmrCheckTime.Interval = 60000;
            this._tmrCheckTime.Elapsed += new System.Timers.ElapsedEventHandler(_tmrCheckTime_Elapsed);

            this._tmrCheckDataRec = new System.Timers.Timer();
            this._tmrCheckDataRec.Interval = 1000;
            this._tmrCheckDataRec.Elapsed += new System.Timers.ElapsedEventHandler(_tmrCheckNullDataRec_Elapsed);

            this.OnMachineIDChanging += new MachineIDChangedDelegate(TH24GTypeAR_OnMachineIDChanging);

            this.RecordListMaxLen = 11;
            this._iRecordMaxLen = 62;

            this._iMaxRvDataTime = -3600;
            string strMaxRvDataTime = ConfigurationSettings.AppSettings["ReaderMaxWaitTime"];
            if (!string.IsNullOrEmpty(strMaxRvDataTime))
            {
                int iTmp;
                if (int.TryParse(strMaxRvDataTime, out iTmp))
                {
                    this._iMaxRvDataTime = iTmp * -1;
                }
            }

            this._iRetryMaxCount = 2000;
            string strMaxRetryCount = ConfigurationSettings.AppSettings["ReaderMaxRetryCount"];
            if (!string.IsNullOrEmpty(strMaxRetryCount))
            {
                short iTmp;
                if (short.TryParse(strMaxRetryCount, out iTmp))
                {
                    this._iRetryMaxCount = iTmp;
                }
            }
        }

        /// <summary>
        /// 机号变更
        /// </summary>
        void TH24GTypeAR_OnMachineIDChanging(object sender, MachineIDChangedEventArgs args)
        {
            if (args != null)
            {
                int iMacID = args.NewMacID;

                //添加入自检记录列表
                if (this.DicReaderExDetails == null)
                {
                    this.DicReaderExDetails = new Dictionary<int, ReaderExDetailInfo>();
                }
                if (!this.DicReaderExDetails.Keys.Contains(iMacID))
                {
                    ReaderExDetailInfo readerExDetail = new ReaderExDetailInfo();
                    readerExDetail.Ex_MacID = iMacID;
                    this.DicReaderExDetails.Add(iMacID, readerExDetail);
                }
            }
        }

        protected override void OnTagResult(TagInfoResultEventArgs tagArgs)
        {
            #region 有数据返回，设置读写器状态为连通中

            if (tagArgs != null && tagArgs.TagInformationList != null && tagArgs.TagInformationList.Count > 0)
            {
                int iMacIDRv = Convert.ToInt32(tagArgs.TagInformationList[0].MachineID);
                if (this.DicReaderExDetails.Keys.Contains(iMacIDRv))
                {
                    ReaderExDetailInfo readerChk = this.DicReaderExDetails[iMacIDRv];
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

            #endregion

            base.OnTagResult(tagArgs);
        }

        /// <summary>
        /// 循环自检虚拟COM是否没有有数据返回
        /// </summary>
        void _tmrCheckNullDataRec_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.DicReaderExDetails == null)
            {
                return;
            }

            foreach (KeyValuePair<int, ReaderExDetailInfo> detailPair in this.DicReaderExDetails)
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
        /// 闲时获取机器的内部时间值并作校验
        /// </summary>
        void _tmrCheckTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.DicReaderExDetails == null)
            {
                return;
            }

            DateTime dtStart = DateTime.Now.Date;
            DateTime dtEnd = DateTime.Now.Date.AddHours(1);

            if (DateTime.Now > dtStart && DateTime.Now < dtEnd)// 在自检时段内并且未完成自检，则重复发送时间获取时间的指令
            {
                if (this._currentDataType != EnumRecDataMode.AdjustTimeMode)
                {
                    if (this._tmrReadData.Enabled)
                    {
                        this._tmrReadData.Stop();
                    }
                    this._currentDataType = EnumRecDataMode.AdjustTimeMode;
                }

                OrderSend_GetMacTime(base.MachineID);
            }
            else
            {
                if (this._currentDataType != EnumRecDataMode.CollectRecordMode)
                {
                    foreach (KeyValuePair<int, ReaderExDetailInfo> detailPair in DicReaderExDetails)
                    {
                        if (detailPair.Value.Ex_dtLastTimeChk.Date < DateTime.Now.AddMinutes(-5) || detailPair.Value.Ex_dtLastTimeChk.Date > DateTime.Now.AddMinutes(5))
                        {
                            if (detailPair.Value.Ex_dtLastTimeChk == DateTime.MinValue || detailPair.Value.CurrentStatus == EnumEquipmentStatus.Disconneted)
                            {
                                //读写器未连接或检查时间未被刷新时不作错误提醒
                                continue;
                            }
                            if (!detailPair.Value.Ex_lIsSendTimeChkWarning && detailPair.Value.CurrentStatus != EnumEquipmentStatus.TimeException)
                            {
                                ReaderExceptionEventArgs readerEx = new ReaderExceptionEventArgs();
                                readerEx.EquipmentStatus = EnumEquipmentStatus.TimeException;
                                readerEx.MachineID = detailPair.Value.Ex_MacID;
                                readerEx.ExceptionObject = new Exception("机器时间有误，机号：" + readerEx.MachineID);
                                OnReaderException(readerEx);

                                detailPair.Value.CurrentStatus = EnumEquipmentStatus.TimeException;
                                detailPair.Value.Ex_lIsSendTimeChkWarning = true;
                            }
                        }
                    }

                    OrderSend_ResetTime("255");

                    if (!this._tmrReadData.Enabled)
                    {
                        this._tmrReadData.Start();
                    }
                    this._currentDataType = EnumRecDataMode.CollectRecordMode;
                }
            }
        }

        /// <summary>
        /// 批量收取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _tmrReadData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this._currentDataType == EnumRecDataMode.CollectRecordMode)
            {
                int iMacID = base.MachineID;
                OrderSend_GetBatchData(iMacID);
            }
        }

        #region 继承函数群

        public override ReturnValueInfo Connect()
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();
            try
            {
                DateTime dtStart = DateTime.Now.Date;
                DateTime dtEnd = DateTime.Now.Date.AddHours(1);

                // 在自检时段内并且未完成自检，则重复发送时间获取时间的指令
                if (DateTime.Now > dtStart && DateTime.Now < dtEnd)
                {
                    this._currentDataType = EnumRecDataMode.AdjustTimeMode;
                }
                else
                {
                    this._currentDataType = EnumRecDataMode.CollectRecordMode;
                }

                base.IsReading = false;

                InitComPort();

                rvInfo.IsSuccess = true;
            }
            catch (Exception ex)
            {
                rvInfo.IsSuccess = false;
                rvInfo.MessageText = ex.Message;
            }

            return rvInfo;
        }

        public override bool Disconnect()
        {
            try
            {
                if (this._MSCOM.PortOpen)
                {
                    this._MSCOM.PortOpen = false;
                }
                if (this.DicReaderExDetails != null)
                {
                    foreach (KeyValuePair<int, ReaderExDetailInfo> detailPair in this.DicReaderExDetails)
                    {
                        if (detailPair.Value.CurrentStatus != EnumEquipmentStatus.Disconneted)
                        {
                            EquipmentStatusEventArgs statusArgs = new EquipmentStatusEventArgs();
                            statusArgs.EquipmentStatus = EnumEquipmentStatus.Disconneted;
                            statusArgs.RecordTime = DateTime.Now;
                            statusArgs.MachineID = detailPair.Value.Ex_MacID;
                            OnStatusChange(statusArgs);
                            detailPair.Value.CurrentStatus = EnumEquipmentStatus.Disconneted;
                        }
                    }
                    this.DicReaderExDetails = new Dictionary<int, ReaderExDetailInfo>();
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message + Environment.NewLine + "串口无法关闭！");
            }

            return true;
        }

        public override ReturnValueInfo StartRead()
        {
            ReturnValueInfo returnInfo = new ReturnValueInfo();

            try
            {
                if (!this._MSCOM.PortOpen)
                {
                    this._MSCOM.CommPort = base.CommPort;
                    this._MSCOM.PortOpen = true;
                }

                this._LockDictionary = new object();

                this._tmrReadData.Start();
                this._tmrCheckDataRec.Start();
                this._tmrCheckTime.Start();
                base.IsReading = true;
            }
            catch (Exception Ex)
            {
                throw new Exception("串口无法打开，详细异常消息如下：" + Environment.NewLine + Ex.Message);
            }

            returnInfo.IsSuccess = true;

            return returnInfo;
        }

        public override void StopRead()
        {
            this._tmrReadData.Stop();
            this._tmrCheckDataRec.Stop();
            this._tmrCheckTime.Stop();
            base.IsReading = false;

            if (this._MSCOM != null && this._MSCOM.PortOpen)
            {
                this._MSCOM.PortOpen = false;
            }

            if (this.DicReaderExDetails != null)
            {
                foreach (KeyValuePair<int, ReaderExDetailInfo> detailPair in this.DicReaderExDetails)
                {
                    detailPair.Value.ResetAllSign();
                    if (detailPair.Value.CurrentStatus != EnumEquipmentStatus.Disconneted)
                    {
                        detailPair.Value.CurrentStatus = EnumEquipmentStatus.Disconneted;

                        EquipmentStatusEventArgs statusArgs = new EquipmentStatusEventArgs();
                        statusArgs.EquipmentStatus = EnumEquipmentStatus.Disconneted;
                        statusArgs.RecordTime = DateTime.Now;
                        statusArgs.MachineID = detailPair.Value.Ex_MacID;
                        OnStatusChange(statusArgs);
                    }
                }
            }

            this._listTagInfos = new List<TagInformationInfo>();
        }

        public override List<TagInformationInfo> GetTagList(string strAntennaNum)
        {
            if (this._currentDataType == EnumRecDataMode.CollectRecordMode)
            {
                OrderSend_GetBatchData(base.MachineID);

                if (this._listTagInfos != null)
                {
                    return this._listTagInfos;
                }
            }
            return null;
        }

        public override List<TagInformationInfo> GetTagList(string strAntennaNum, bool lIsDeleteHistory)
        {
            return null;
        }

        public override bool DeleteData(string strMacID)
        {
            int num;
            if (!int.TryParse(strMacID, out num))
            {
                return false;
            }
            int iMachineID = int.Parse(strMacID);

            OrderSend_DeleteRecord(iMachineID);

            return base.DeleteData(strMacID);
        }

        public override bool AdjustClockByPCTime(string strSN, string strIP)
        {
            try
            {
                if (!this._MSCOM.PortOpen)
                {
                    this._MSCOM.CommPort = base.CommPort;
                    this._MSCOM.PortOpen = true;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message + Environment.NewLine + "串口无法打开！");
            }

            OrderSend_ResetTime(strSN);
            return base.AdjustClockByPCTime(strSN, strIP);
        }

        #endregion

        /// <summary>
        /// 初始化Com 口信息
        /// </summary>
        private void InitComPort()
        {
            this._MSCOM = this.axMSComm05;

            #region 初始化虚拟串口

            if (this._MSCOM.PortOpen)
            {
                this._MSCOM.PortOpen = false;//初始化
            }

            this._MSCOM.InputLen = 0;  //清除接收缓冲区

            //_mscomBatch.CommPort = base.CommPort;//设置端口号为COM1

            this._MSCOM.RThreshold = 285;   //每接收一个字符则激发OnComm()事件

            this._MSCOM.Settings = "38400,N,8,1";  //端口设置

            this._MSCOM.DTREnable = true;  //置DTR有效

            //_mscomBatch.DSRTimeout = 100;

            this._MSCOM.RTSEnable = true;  //置RTS有效

            //_mscomBatch.CTSTimeout = 100;

            this._MSCOM.Handshaking = MSCommLib.HandshakeConstants.comNone;

            //_mscomBatch.InputMode = MSCommLib.InputModeConstants.comInputModeText;  //文本

            this._MSCOM.InputMode = MSCommLib.InputModeConstants.comInputModeBinary;   //二进制

            this._MSCOM.NullDiscard = false;

            this._MSCOM.OnComm += new EventHandler(ReadDataFromCOM);  //执行一个OnComm事件

            this._MSCOM.InBufferCount = 0;
            this._MSCOM.OutBufferCount = 0;

            #endregion
        }

        private object _lockRead;

        /// <summary>
        /// 从虚拟COM组件处接收返回的指令数据
        /// </summary>
        private void ReadDataFromCOM(object sender, EventArgs e)
        {
            if (this._lockRead == null)
            {
                this._lockRead = new object();
            }
            lock (this._lockRead)
            {
                try
                {
                    if (this.axMSComm05.CommEvent == (short)(MSCommLib.OnCommConstants.comEvReceive))  //查询CommEvent属性
                    {
                        object objInput = this._MSCOM.Input;

                        byte[] arrInput = (byte[])objInput;

                        if (arrInput.Length == 0)
                        {
                            return;
                        }

                        int iMacBaseID = base.MachineID;

                        //将返回的字节数组转换成字符串处理
                        string strInput = string.Empty;
                        for (int i = 0; i <= (arrInput.Length - 1); i++)
                        {
                            strInput = strInput + arrInput[i].ToString("X2");
                        }

                        if (this._currentDataType == EnumRecDataMode.CollectRecordMode)//收数模式
                        {
                            #region 收数处理

                            this._iRecordMaxLen = 62;

                            string strRecordIndex;
                            List<TagInformationInfo> listTagInfos = DataHandling_FormatBatchTagData(strInput, out strRecordIndex);

                            if (listTagInfos == null)
                            {
                                return;
                            }
                            if (listTagInfos.Count < 1)
                            {
                                return;
                            }

                            iMacBaseID = Convert.ToInt32(listTagInfos[0].MachineID);

                            TagInfoResultEventArgs tagInfo = new TagInfoResultEventArgs();
                            tagInfo.TagInformationList = listTagInfos;

                            //记录来自设备的最原始卡记录字符串
                            tagInfo.TagSourceInformation = strInput;

                            if (!string.IsNullOrEmpty(strRecordIndex))
                            {
                                string[] arrIndex = strRecordIndex.Split('-');
                                if (arrIndex != null && arrIndex.Length > 2)
                                {
                                    tagInfo.ExamineNum = arrIndex[0];
                                }
                            }

                            this.OnTagResult(tagInfo);
                            Console.WriteLine("抛数完毕。" + listTagInfos[0].MachineID);

                            if (!string.IsNullOrEmpty(strRecordIndex))
                            {
                                string[] arrIndex = strRecordIndex.Split('-');
                                if (arrIndex != null && arrIndex.Length > 2)
                                {
                                    string strMacID = arrIndex[0];
                                    int iMacIDIndex = int.Parse(strMacID);
                                    string strIndex = arrIndex[2];
                                    string strSstatus = arrIndex[3];
                                    if (strSstatus == "01")
                                    {
                                        OrderSend_ConfirmBatchData(iMacIDIndex, strIndex);
                                        Console.WriteLine("确认完毕。" + iMacIDIndex.ToString() + " " + DateTime.Now.ToString());
                                    }
                                }
                            }

                            #endregion
                        }
                        else if (this._currentDataType == EnumRecDataMode.AdjustTimeMode)//时间获取模式
                        {
                            #region  时间检查处理

                            this._iRecordMaxLen = 28;

                            int iMacIDChkTime = 0;
                            DateTime? dtGet = DataHandling_FormatMacTimeVal(strInput, ref iMacIDChkTime);
                            if (iMacIDChkTime != 0)
                            {
                                if (!this.DicReaderExDetails.Keys.Contains(iMacIDChkTime))
                                {
                                    return;
                                }

                                ReaderExDetailInfo readerChk = this.DicReaderExDetails[iMacIDChkTime];
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
                            }

                            #endregion
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.axMSComm05 = new AxMSCommLib.AxMSComm();
                    InitComPort();
                    if (!this.axMSComm05.PortOpen)
                    {
                        this.axMSComm05.CommPort = base.CommPort;
                        this.axMSComm05.PortOpen = true;
                    }
                }
            }
        }

        #region 数据处理函数群

        /// <summary>
        /// 实体转换，将TabFormat类型实体数组转换成TagInformationInfo实体数组（弃用）
        /// </summary>
        /// <param name="listTabFormat"></param>
        /// <returns></returns>
        private List<TagInformationInfo> DataHandling_TagDataListExChange(List<TagFormat> listTagFormat)
        {
            List<TagInformationInfo> listTagInfo = new List<TagInformationInfo>();
            if (listTagFormat != null)
            {
                for (int i = 0; i < listTagFormat.Count; i++)
                {
                    TagInformationInfo tagInfo = new TagInformationInfo();

                    int iMacID = Convert.ToInt32(listTagFormat[i].StrMacID, 16);
                    tagInfo.MachineID = iMacID.ToString();
                    tagInfo.IDNum = listTagFormat[i].StrID;
                    tagInfo.ReaderKey = iMacID.ToString();
                    tagInfo.TagExistFirstDatetime = DateTime.Parse(listTagFormat[i].StrFirstReadTime);
                    tagInfo.ReadDatetime = DateTime.Parse(listTagFormat[i].StrLastReadTime);
                    tagInfo.TabNum = listTagFormat[i].StrRecordIndex;
                    tagInfo.ReadTimes = int.Parse(listTagFormat[i].StrReadTimes);
                    tagInfo.CRC = listTagFormat[i].StrCRC;
                    tagInfo.Keyboard = listTagFormat[i].StrTagKey.Substring(0, 2);
                    tagInfo.Power = listTagFormat[i].StrTagKey.Substring(2, 2);

                    #region 电量值处理

                    if (listTagFormat[i].StrPowerValue == "00")
                    {
                        tagInfo.IsPowerLow = false;
                    }
                    else
                    {
                        tagInfo.IsPowerLow = true;
                    }

                    #endregion

                    tagInfo.TagID = listTagFormat[i].StrTagID;
                    tagInfo.Antenna = 1;
                    listTagInfo.Add(tagInfo);
                }
                return listTagInfo;
            }
            return null;
        }

        /// <summary>
        /// 格式化处理批量数据
        /// </summary>
        /// <param name="strSourceData">需要处理的返回指令数据</param>
        /// <param name="strRecordIndex">返回需要被确认的流水号</param>
        /// <returns></returns>
        private List<TagInformationInfo> DataHandling_FormatBatchTagData(string strSourceData, out string strRecordIndex)
        {
            try
            {
                List<TagInformationInfo> listTagInfos = new List<TagInformationInfo>();
                strSourceData = strSourceData.Trim();
                string[] arrStrData = new string[this._iRecordListMaxLen];
                strRecordIndex = string.Empty;

                if (strSourceData.Length >= 14)
                {
                    //处理正常数据
                    List<string> listSourceData = ReaderGeneral.HandleSourceDataToList(strSourceData, this._iRecordMaxLen);
                    List<ReaderExceptionEventArgs> listExArgs = new List<ReaderExceptionEventArgs>();
                    if (listSourceData != null && listSourceData.Count > 0)
                    {
                        #region  处理正常数据返回

                        for (int i = 0; i < listSourceData.Count; i++)
                        {
                            try
                            {
                                TagInformationInfo tagFormat = new TagInformationInfo();
                                tagFormat = tagFormat.GetTagInformationInfoIns(listSourceData[i]);
                                if (tagFormat != null)
                                {
                                    listTagInfos.Add(tagFormat);
                                }

                                //转换
                                if (i == listSourceData.Count - 1)
                                {
                                    strRecordIndex = tagFormat.MachineID + "-" + tagFormat.IDNum + "-" + tagFormat.RecordIndex + "-" + tagFormat.States;
                                }
                            }
                            catch (Exception exFormat)
                            {
                                #region 异常处理

                                ReaderExceptionEventArgs readerEX = new ReaderExceptionEventArgs();
                                readerEX.EquipmentStatus = EnumEquipmentStatus.SigleRecordException;
                                exFormat.Source = " COM:" + base.CommPort.ToString() + " " + listSourceData[i];
                                readerEX.ExceptionObject = exFormat;
                                readerEX.RecordTime = DateTime.Now;
                                listExArgs.Add(readerEX);
                                //OnReaderException(readerEX);

                                if (i == listSourceData.Count - 1)
                                {
                                    strRecordIndex = Convert.ToInt32(listSourceData[i].Substring(0, 2), 16).ToString() + "-" + listSourceData[i].Substring(4, 8) + "-" + listSourceData[i].Substring(12, 4) + "-" + listSourceData[i].Substring(2, 2);
                                }

                                #endregion
                            }

                        }

                        this._listTagInfos = listTagInfos;

                        #endregion

                        List<string> listLog = new List<string>();
                        if (listTagInfos.Count == 0)
                        {
                            ReaderExceptionEventArgs args = new ReaderExceptionEventArgs();
                            args.EquipmentStatus = EnumEquipmentStatus.BatchRecordException;
                            args.ExceptionObject = new Exception("COM:" + base.CommPort.ToString() + "，批量数据异常，请注意最近无数据返回的读写器。");
                            args.RecordTime = DateTime.Now;
                            OnReaderException(args);
                        }
                        else if (listTagInfos.Count > 0)
                        {
                            if (listExArgs.Count > 0 && listExArgs.Count <= listTagInfos.Count)
                            {
                                foreach (ReaderExceptionEventArgs args in listExArgs)
                                {
                                    if (args == null)
                                    {
                                        continue;
                                    }
                                    if (!listLog.Contains(args.ExceptionObject.Message))
                                    {
                                        args.MachineID = int.Parse(listTagInfos[0].MachineID);
                                        OnReaderException(args);
                                    }
                                }
                            }
                        }

                        return listTagInfos;
                    }

                    //处理确认信息数据数据
                    List<string> listEmptyData = ReaderGeneral.HandleSourceDataToList(strSourceData);
                    if (listEmptyData != null && listEmptyData.Count > 0)
                    {
                        #region 处理确认信息数据数据返回

                        int iMacIDFinish = Convert.ToInt32(strSourceData.Substring(2, 2), 16);

                        //当读写器无未确认数据或未采集到数据时，会返回一条为7字节的数据。
                        string strFinished = GetFinishedCollectionInstruction(iMacIDFinish);
                        string strUploaded = GetUploadedCollectionInstruction(iMacIDFinish);

                        if (listEmptyData.Contains(strFinished) || listEmptyData.Contains(strUploaded))
                        {
                            TagInfoResultEventArgs arg = new TagInfoResultEventArgs();
                            arg.TagInformationList = new List<TagInformationInfo>();
                            TagInformationInfo tagInfo = new TagInformationInfo();
                            tagInfo.MachineID = iMacIDFinish.ToString();
                            arg.TagInformationList.Add(tagInfo);

                            this.OnTagResult(arg);
                        }

                        #endregion
                    }
                }

                return listTagInfos;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        /// <summary>
        /// 格式化机器时间值
        /// </summary>
        /// <param name="strInput">需要处理的返回指令数据</param>
        /// <returns></returns>
        private DateTime? DataHandling_FormatMacTimeVal(string strInput, ref int iMacID)
        {
            DateTime? dtMac = null;
            try
            {
                List<string> listSourceData = ReaderGeneral.HandleSourceDataToList(strInput, this._iRecordMaxLen);
                if (listSourceData.Count > 0)
                {
                    foreach (string itemData in listSourceData)
                    {
                        string strCut = DataHandling_CutDataHeadTail(itemData);
                        if (!string.IsNullOrEmpty(strCut))
                        {
                            iMacID = Convert.ToInt32(strCut.Substring(0, 2), 16);
                            int iYear = Convert.ToInt32(strCut.Substring(4, 2), 16) + 2000;
                            int iMonth = Convert.ToInt32(strCut.Substring(6, 2), 16);
                            int iDay = Convert.ToInt32(strCut.Substring(8, 2), 16);
                            int iHour = Convert.ToInt32(strCut.Substring(10, 2), 16);
                            int iMinute = Convert.ToInt32(strCut.Substring(12, 2), 16);
                            int iSecond = Convert.ToInt32(strCut.Substring(14, 2), 16);
                            string strDate = iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString();
                            DateTime dtDate = DateTime.Parse(strDate);
                            //TIPS: 使读写器的时间误差在2分钟内。
                            if (dtDate >= DateTime.Now.AddMinutes(-2) && dtDate <= DateTime.Now.AddMinutes(2))
                            {
                                dtMac = dtDate;
                                break;
                            }
                        }
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
        /// 去掉指令的桢头和桢尾(每楨数据去头2位，尾6位)（弃用）
        /// </summary>
        /// <param name="strData">待处理数据</param>
        /// <returns></returns>
        public static string DataHandling_CutDataHeadTail(string strData)
        {
            string strRecData = string.Empty;
            string strMacID = string.Empty;

            try
            {
                // 修改为只去前2位，增加保留机器号。
                strMacID = strData.Substring(2, 4);
                strRecData = strData.Substring(8, strData.Length - 8);
                strRecData = strMacID + strRecData;
                strRecData = strRecData.Substring(0, strRecData.Length - 6);
            }
            catch (Exception)
            {
                return null;
            }
            return strRecData;
        }

        #endregion

        #region 发送指令函数群

        /// <summary>
        /// 指令发送-批量获取11条记录
        /// </summary>
        /// <param name="iMachineID">机号</param>
        public void OrderSend_GetBatchData(int iMachineID)
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
                this.axMSComm05.Output = arrDataSend;
                Application.DoEvents();
            }
            catch (Exception)
            {

                //throw ex;
            }
        }

        /// <summary>
        /// 指令发送-批量确定数据
        /// </summary>
        /// <param name="iMacID">机号</param>
        /// <param name="strRecordIndex">流水号</param>
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
                this.axMSComm05.Output = arrDataSend;
                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 指令发送-获取机器当前时间
        /// </summary>
        /// <param name="iMachineID">机号</param>
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
                this.axMSComm05.Output = arrDataSend;
                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 指令发送-校对读写器时间
        /// </summary>
        /// <param name="strMacID">机号</param>
        private void OrderSend_ResetTime(string strMacID)
        {
            byte[] data = { 0xC0, 0xFF, 0xA9, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            int imacID = 0;
            if (!string.IsNullOrEmpty(strMacID) && int.TryParse(strMacID, out imacID))
            {
                byte[] bMacID = BitConverter.GetBytes(imacID);
                data[1] = bMacID[0];
            }

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
                this.axMSComm05.Output = data;
                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 抹除读写器数据
        /// </summary>
        /// <param name="machineID"></param>
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
                this.axMSComm05.Output = data;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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

        #endregion
    }

    /// <summary>
    /// 标签数据格式化实体（弃用）
    /// </summary>
    public class TagFormat
    {
        #region 属性

        /// <summary>
        /// 机器号
        /// </summary>
        public string StrMacID { get; set; }
        /// <summary>
        /// ID号
        /// </summary>
        public string StrID { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string StrStutasCode { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string StrRecordIndex { get; set; }
        /// <summary>
        /// 标签序列号(前四位)
        /// </summary>
        public string StrTagKey { get; set; }
        /// <summary>
        /// 标签序列号(后六位)
        /// </summary>
        public string StrTagID { get; set; }
        /// <summary>
        /// 首次读卡片时间
        /// </summary>
        public string StrFirstReadTime { get; set; }
        /// <summary>
        /// 记录是否有效
        /// </summary>
        public string StrValid { get; set; }
        /// <summary>
        /// 单位时间重复读取次数
        /// </summary>
        public string StrReadTimes { get; set; }
        /// <summary>
        /// 最后一次读取卡片时间
        /// </summary>
        public string StrLastReadTime { get; set; }
        /// <summary>
        /// CRC校验码
        /// </summary>
        public string StrCRC { get; set; }
        /// <summary>
        /// 电量值
        /// </summary>
        public string StrPowerValue { get; set; }

        #endregion

        /// <summary>
        /// 标签数据格式化实体构造函数
        /// </summary>
        /// <param name="strTagData">未格式化的源数据</param>
        public TagFormat(string strTagData)
        {
            try
            {
                this.StrMacID = strTagData.Substring(0, 2);
                this.StrStutasCode = strTagData.Substring(2, 2);
                this.StrID = strTagData.Substring(4, 8);
                this.StrRecordIndex = strTagData.Substring(12, 4);
                this.StrTagKey = strTagData.Substring(16, 4);
                this.StrPowerValue = strTagData.Substring(18, 2);
                this.StrTagID = strTagData.Substring(20, 6);
                this.StrFirstReadTime = ReaderGeneral.ChangetoDatebyHexstring(strTagData.Substring(26, 12));
                this.StrValid = strTagData.Substring(38, 2);
                this.StrReadTimes = Convert.ToInt64(strTagData.Substring(40, 2), 16).ToString();
                this.StrLastReadTime = DateTime.Parse(StrFirstReadTime).ToShortDateString() + " " + ReaderGeneral.ChangetoDatebyHexstring(strTagData.Substring(42, 6));
                this.StrCRC = strTagData.Substring(48, 4);
            }
            catch (Exception Ex)
            {
                Exception ex = new Exception("异常信息：" + Ex.Message + Environment.NewLine + "异常源数据" + strTagData);
                throw ex;
            }
        }
    }
}
