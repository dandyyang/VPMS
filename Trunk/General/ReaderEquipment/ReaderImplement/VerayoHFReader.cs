using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReaderEquipment.Entity;
using ReaderEquipment.Entity.VerayoHF;

namespace ReaderEquipment.ReaderImplement
{
    public partial class VerayoHFReader : AbstractReader
    {
        private int _recordRange = 1;
        #region 字段、属性

        System.Timers.Timer _timerReadData;

        private object _objReadingLock;

        private object _objGetUidLock;

        private AxMSCommLib.AxMSComm _mscom4Reader;

        /// <summary>
        /// 正在使用的指令编号

        /// </summary>
        private string _IsUsingCmd;

        public string IsUsingCmd
        {
            get { return _IsUsingCmd; }
            set { _IsUsingCmd = value; }
        }

        private short _comPort = 5;
        /// <summary>
        /// 连接读写器使用的COM口号
        /// </summary>
        public short ComPort
        {
            get { return _comPort; }
            set { _comPort = value; }
        }


        private bool _IsReading = false;

        #endregion

        #region 注册抛数事件
        /// <summary>
        /// 获取时钟事件

        /// </summary>
        public event EventHandler<VerayoClock> ClockShowing;
        /// <summary>
        /// 获取设备主参数事件

        /// </summary>
        public event EventHandler<VerayoReaderEntity> EquipmentMainParamsShowing;

        /// <summary>
        /// 获取UID事件
        /// </summary>
        public event EventHandler<VerayoUID> TagUidShowing;
        /// <summary>
        /// 获取指定块事件
        /// </summary>
        public event EventHandler<VerayoDesignatedPiece> DesignatedPieceShowing;
        /// <summary>
        /// 获取标签Internal标记事件
        /// </summary>
        public event EventHandler<VerayoTagsInternal> TagsInternalShowing;
        /// <summary>
        /// 获取随机数事件
        /// </summary>
        public event EventHandler<VerayoRandomNum> RandomNumShowing;
        /// <summary>
        /// 获取标签产生的响应事件
        /// </summary>
        public event EventHandler<VerayoGetLable> GetLableShowing;

        /// <summary>
        /// 获取标签PUF数据事件
        /// </summary>
        public event EventHandler<VerayoTagPUF> TagPUFShowing;

        /// <summary>
        /// 获取读写器响应到标签事件
        /// </summary>
        public event EventHandler<VerayoReaderLable> ReaderLableShowing;
        /// <summary>
        /// 获取带认证读指定块事件
        /// </summary>
        public event EventHandler<VerayoTakeAuthenticationReadPiece> TakeAuthenticationReadPieceShowing;
        /// <summary>
        /// 获取未上传的记录事件
        /// </summary>
        public event EventHandler<VerayoRecordsInformation> RecordsInformationShowing;

        /// <summary>
        /// 获取读写器记录总数事件

        /// </summary>
        public event EventHandler<VerayoRecordsAmount> RecordsAmountShowing;
        /// <summary>
        /// 获取终端设备号事件

        /// </summary>
        public event EventHandler<VerayoEquipmentNum> EquipmentNumShowing;
        /// <summary>
        /// 获取所有终端设备号事件

        /// </summary>
        public event EventHandler<VerayoAllEquipmentNum> AllEquipmentNumShowing;

        /// <summary>
        /// 获取终端设备机器类型事件

        /// </summary>
        public event EventHandler<VerayoEquipmentType> EquipmentTypeShowing;

        /// <summary>
        /// 获取程序版本号事件

        /// </summary>
        public event EventHandler<VerayoEquipmentVersion> EquipmentVersionShowing;
        #endregion

        //private bool _check;

        public VerayoHFReader()
        {
            InitializeComponent();

            _objReadingLock = new object();
            _objGetUidLock = new object();

            _timerReadData = new System.Timers.Timer();
            _timerReadData.Interval = 500;
            _timerReadData.Elapsed += new System.Timers.ElapsedEventHandler(_timerReadData_Elapsed);

            //_check = false;
        }

        void _timerReadData_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (_objGetUidLock)
            {
                //if (_check)
                //{
                //    return;
                //}
                //CheckUid(base.MachineID);

                CheckUid(base.MachineID);
                GetTagUid(base.MachineID);
                
                //_check = true;

            }
        }

        /// <summary>
        ///初始化COM口信息
        /// </summary>
        private void InitComPort()
        {
            this._mscom4Reader = this.mscom4Reader;

            if (this._mscom4Reader.PortOpen)
            {
                this._mscom4Reader.PortOpen = false;
            }

            _mscom4Reader.InputLen = 0;  //清除接收缓冲区

            _mscom4Reader.CommPort = base.CommPort;//设置端口号

            _mscom4Reader.RThreshold = 100;   //每接收一个字符则激发OnComm()事件

            _mscom4Reader.Settings = "38400,N,8,1";  //端口设置

            _mscom4Reader.DTREnable = true;  //置DTR有效

            _mscom4Reader.RTSEnable = true;  //置RTS有效

            _mscom4Reader.Handshaking = MSCommLib.HandshakeConstants.comNone;

            _mscom4Reader.InputMode = MSCommLib.InputModeConstants.comInputModeBinary;   //二进制

            _mscom4Reader.NullDiscard = false;

            _mscom4Reader.OnComm += new EventHandler(ReadDataForCOM);  //执行一个OnComm事件

            _mscom4Reader.InBufferCount = 0;

            _mscom4Reader.OutBufferCount = 0;
        }

        /// <summary>
        /// 从COM口处读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadDataForCOM(object sender, EventArgs e)
        {
            if (this._mscom4Reader.CommEvent == (short)(MSCommLib.OnCommConstants.comEvReceive))//查询CommEvent属性
            {
                lock (this._objReadingLock)
                {
                    object inputObjs = this._mscom4Reader.Input;

                    byte[] inputBytes = (byte[])inputObjs;

                    if (inputBytes.Length < 1)
                    {
                        return;
                    }

                    #region MyRegion
                    SourceDataHandling(inputBytes);
                    //string strInput = string.Empty;

                    //for (int i = 0; i < inputBytes.Length - 1; i++)
                    //{
                    //    strInput = strInput + inputBytes[i].ToString("X2");
                    //}
                    //List<string> listUid = ReaderGeneral.HandleSourceDataToList(strInput);

                    //TagInfoResultEventArgs args = new TagInfoResultEventArgs();
                    //args.InformationObject = listUid;
                    //OnTagResult(args); 
                    #endregion
                }
            }
        }
       
        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="inputBytes"></param>
        private void SourceDataHandling(byte[] inputBytes)
        {
            string strInput = string.Empty;

            for (int i = 0; i < inputBytes.Length - 1; i++)
            {
                strInput = strInput + inputBytes[i].ToString("X2");
            }

            List<string> listUid = new List<string>();
            if (_IsReading)
            {
                listUid = ReaderGeneral.HandleSourceDataToList(strInput);

                TagInfoResultEventArgs args = new TagInfoResultEventArgs();
                args.InformationObject = listUid;
                OnTagResult(args);
            }
            else
            {
                listUid = ReaderGeneral.HandleSourceDataToList(strInput);
                if (listUid != null && listUid.Count > 0)
                {
                    switch (this._IsUsingCmd)
                    {
                        case "A1":
                            {
                                VerayoClock entity = new VerayoClock(listUid[0]);
                                OnGettingClock(entity);
                                break;
                            }
                        case "A3":
                            {
                                VerayoReaderEntity entity = new VerayoReaderEntity(listUid[0]);
                                OnGettingEquipMainParams(entity);
                                break;
                            }
                        case "B1":
                            {
                                VerayoUID entity = new VerayoUID(listUid[0]);
                                OnGettingTagUID(entity);
                                break;
                            }
                        case "E1":
                            {
                                VerayoEquipmentNum entity = new VerayoEquipmentNum(listUid[0]);
                                OnGettingEquipmentNum(entity);
                                break;
                            }
                        default:
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// 开始读数
        /// </summary>
        /// <returns></returns>
        public override ReaderEquipment.Entity.ReturnValueInfo StartRead()
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();
            try
            {
                if (!this._mscom4Reader.PortOpen)
                {
                    this._mscom4Reader.CommPort = base.CommPort;
                    this._mscom4Reader.PortOpen = true;
                    //_timerReadData.Start();
                    //int modeID = int.Parse(base.ControllerSN);
                    //ChangeMode(base.MachineID, modeID);

                    //rvInfo.IsSuccess = true;
                }
                #region MyRegion
                _IsReading = true;

                _timerReadData.Start();
                int modeID = int.Parse(base.ControllerSN);
                ChangeMode(base.MachineID, modeID);

                rvInfo.IsSuccess = true;
                #endregion
            }
            catch (Exception Ex)
            {
                rvInfo.IsSuccess = false;
                rvInfo.MessageText = Ex.Message + Environment.NewLine + "串口无法打开！";
            }
            return rvInfo;
        }
        /// <summary>
        /// 停止读数
        /// </summary>
        public override void StopRead()
        {
            _timerReadData.Stop();

            _IsReading = false;
            if (this._mscom4Reader != null && this._mscom4Reader.PortOpen)
            {
                this._mscom4Reader.PortOpen = false;
            }
        }
        /// <summary>
        /// 打开读写器连接
        /// </summary>
        /// <returns></returns>
        public override ReaderEquipment.Entity.ReturnValueInfo Connect()
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();
            try
            {
                InitComPort();

                #region MyRegion

                _mscom4Reader.PortOpen = true;
                //_mscom4Reader.PortOpen = true;

                AdjustMachineTimer(""); 
                #endregion

                rvInfo.IsSuccess = true;
            }
            catch (Exception Ex)
            {
                rvInfo.IsSuccess = false;
                rvInfo.MessageText = Ex.Message;
            }
            return rvInfo;
        }

        public override bool Disconnect()
        
        {
            try
            {
                if (this._mscom4Reader.PortOpen)
                {
                    this._mscom4Reader.PortOpen = false;
                }
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.Message + Environment.NewLine + "COM口无法被关闭。");
            }

            return true;
        }

        public override List<ReaderEquipment.Entity.TagInformationInfo> GetTagList(string antennaNum)
        {
            throw new NotImplementedException();
        }

        public override List<ReaderEquipment.Entity.TagInformationInfo> GetTagList(string antennaNum, bool DeleteHistory)
        {
            throw new NotImplementedException();
        }        
        /// <summary>
        /// 寻卡操作
        /// </summary>
        /// <param name="machineID"></param>
        /// B0      OK
        public void CheckUidPublic(int machineID)
        {
            CheckUid(machineID);
            CheckUid(machineID);
            CheckUid(machineID);
        }

        private void CheckUid(int machineID)
        {
            _mscom4Reader.RThreshold = 10;

            byte[] byMacID = BitConverter.GetBytes(MachineID);

            byte[] dataB0 = { 0xC0, 0x01, 0xB0, 0x00, 0x00, 0x00, 0xC1 };
            dataB0[1] = byMacID[0];
            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(dataB0, out highCRC, out lowCRC);
            dataB0[4] = lowCRC;
            dataB0[5] = highCRC;

            try
            {
                this._mscom4Reader.Output = dataB0;

                this._IsUsingCmd = "B0";

                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 获取标签UID
        /// </summary>
        ///B1       OK
        public void GetTagUidPublic(int machineID)
        {
            for (int i = 0; i < 21; i++)
            {
                GetTagUid(machineID); 
            }
        }

        private void GetTagUid(int machineID)
        {
            _mscom4Reader.RThreshold = 10;

            byte[] byMacID = BitConverter.GetBytes(MachineID);

            byte[] dataB1 = { 0xC0, 0x01, 0xB1, 0x00, 0x00, 0x00, 0xC1 };
            dataB1[1] = byMacID[0];
            byte lowCRC_B1;
            byte highCRC_B1;
            ReaderGeneral.CRC16_CCITT(dataB1, out highCRC_B1, out lowCRC_B1);
            dataB1[4] = lowCRC_B1;
            dataB1[5] = highCRC_B1;

            try
            {
                this._mscom4Reader.Output = dataB1;

                this._IsUsingCmd = "B1";

                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 转换读卡模式
        /// </summary>
        /// <param name="machineID"></param>
        /// <param name="modeID">模式编号</param>
        private void ChangeMode(int machineID, int modeID)
        {
            byte[] byMacID = BitConverter.GetBytes(MachineID);
            byte[] byModeID = BitConverter.GetBytes(modeID);

            byte[] data = { 0xC0, 0x01, 0xA2, 0x08, 0xFF, 0xFF, 0xFF, 0xFF, 0x01, 0x00, 0x01, 0x0A, 0x00, 0x00, 0xC1 };
            data[8] = data[1] = byMacID[0];

            data[10] = byModeID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[12] = lowCRC;
            data[13] = highCRC;

            try
            {
                this._mscom4Reader.Output = data;

                this._IsUsingCmd = "A2";

                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 设置阅读器主参数
        /// </summary>
        /// <param name="machineID"></param>
        /// <param name="entity"></param>
        /// A2      OK
        public void SetReaderMainParams(int machineID, VerayoReaderEntity entity)
        {
            SetReaderMainParamsPrivate(machineID, entity);
            SetReaderMainParamsPrivate(machineID, entity);
            SetReaderMainParamsPrivate(machineID, entity);
        }

        private void SetReaderMainParamsPrivate(int machineID, VerayoReaderEntity entity)
        {
            //FFFFFFFF01 00 00 0A FFFF 0204 03 FF0102 00 60
            /*FFFFFFFF02 00 00 0A FFFF 0204 03 FF0102 00 60 
             * 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
             * 0256*/
            _mscom4Reader.RThreshold = 10;

            byte[] data = { 0xC0, 0x00, 0xA2, 0x30, // 0x12,
                              0xFF, 0xFF,0xFF, 0xFF,0x00, 0x00,0x00, 0x00,0xFF, 0xFF,0x00, 0x00,0x00,0xFF, 0x00,0x00, 0x00,0x00, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,  
                              0x00, 0x00, 0xC1 };
            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(entity.ID) && entity.ID.Length == 8)
            {
                byte[] byIDFirst = BitConverter.GetBytes(Convert.ToInt32(entity.ID.Substring(0, 2), 16));
                data[4] = byIDFirst[0];

                byte[] byIDSecond = BitConverter.GetBytes(Convert.ToInt32(entity.ID.Substring(2, 2), 16));
                data[5] = byIDSecond[0];

                byte[] byIDThirld = BitConverter.GetBytes(Convert.ToInt32(entity.ID.Substring(4, 2), 16));
                data[6] = byIDThirld[0];

                byte[] byIDFourth = BitConverter.GetBytes(Convert.ToInt32(entity.ID.Substring(6, 2), 16));
                data[7] = byIDFourth[0];
            }

            if (!string.IsNullOrEmpty(entity.MachineID))
            {
                byte[] byMacIDNew = BitConverter.GetBytes(Convert.ToInt32(entity.MachineID, 16));
                data[8] = byMacIDNew[0];
            }
            else
            {
                data[8] = byMacID[0];
            }

            if (!string.IsNullOrEmpty(entity.UploadingMode))
            {
                byte[] byUpload = BitConverter.GetBytes(Convert.ToInt32(entity.UploadingMode, 16));
                data[9] = byUpload[0];
            }

            if (!string.IsNullOrEmpty(entity.WorkingMode))
            {
                byte[] byWork = BitConverter.GetBytes(Convert.ToInt32(entity.WorkingMode, 16));
                data[10] = byWork[0];
            }
            if (!string.IsNullOrEmpty(entity.ReadTimeInterval))
            {
                byte[] byReadTime = BitConverter.GetBytes(Convert.ToInt32(entity.ReadTimeInterval, 16));
                data[11] = byReadTime[0];
            }
            if (!string.IsNullOrEmpty(entity.ReadCardParam) && entity.ReadCardParam.Length == 4)
            {
                byte[] byParamFirst = BitConverter.GetBytes(Convert.ToInt32(entity.ReadCardParam.Substring(0, 2), 16));
                data[12] = byParamFirst[0];

                byte[] byParamSecond = BitConverter.GetBytes(Convert.ToInt32(entity.ReadCardParam.Substring(2, 2), 16));
                data[13] = byParamSecond[0];
            }
            if (!string.IsNullOrEmpty(entity.CardType) && entity.CardType.Length == 2)
            {
                byte[] byCardType = BitConverter.GetBytes(Convert.ToInt32(entity.CardType, 16));
                data[14] = byCardType[0];
            }

            if (!string.IsNullOrEmpty(entity.PeaceLength) && entity.PeaceLength.Length == 2)
            {
                byte[] byPeaceLength = BitConverter.GetBytes(Convert.ToInt32(entity.PeaceLength, 16));
                data[15] = byPeaceLength[0];
            }
            if (!string.IsNullOrEmpty(entity.RecordSaveFrequency) && entity.RecordSaveFrequency.Length == 2)
            {
                byte[] byRecordSaveFrequency = BitConverter.GetBytes(Convert.ToInt32(entity.RecordSaveFrequency, 16));
                data[16] = byRecordSaveFrequency[0];
            }
            if (!string.IsNullOrEmpty(entity.HornSwitch) && entity.HornSwitch.Length == 2)
            {
                byte[] byHS = BitConverter.GetBytes(Convert.ToInt32(entity.HornSwitch,16));
                data[17] = byHS[0];
            }
            if (!string.IsNullOrEmpty(entity.HornSoundInterval) && entity.HornSoundInterval.Length == 2)
            {
                byte[] byHSI = BitConverter.GetBytes(Convert.ToInt32(entity.HornSoundInterval,16));
                data[18] = byHSI[0];
            }
            if (!string.IsNullOrEmpty(entity.HornSountTimes) && entity.HornSountTimes.Length == 2)
            {
                byte[] byHST = BitConverter.GetBytes(Convert.ToInt32(entity.HornSountTimes,16));
                data[19] = byHST[0];
            }
            if (!string.IsNullOrEmpty(entity.AlarmMode) && entity.AlarmMode.Length == 2)
            {
                byte[] byAM = BitConverter.GetBytes(Convert.ToInt32(entity.AlarmMode,16));
                data[20] = byAM[0];
            }
            if (!string.IsNullOrEmpty(entity.M1CardKeyType) && entity.M1CardKeyType.Length == 2)
            {
                byte[] byMCK = BitConverter.GetBytes(Convert.ToInt32(entity.M1CardKeyType,16));
                data[21] = byMCK[0];
            }

            //byte lowCRC;
            //byte highCRC;
            //ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            //data[22] = lowCRC;
            //data[23] = highCRC;

            #region MyRegion

            for (int i = 22; i < 52; i++)
            {
                byte[] by00 = BitConverter.GetBytes(Convert.ToInt32("00", 16));
                data[i] = by00[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[52] = lowCRC;
            data[53] = highCRC;
            #endregion

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "A2";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #region MyRegion

        #region 终端出厂设置指令
        /// <summary>
        /// 校对时钟(广播式校对)
        /// </summary>
        /// <param name="machineID">机器号，如果为空，则默认为255</param>
        public ReturnValueInfo AdjustMachineTimer(string machineID)
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();

            byte[] data = { 0xC0, 0xFF, 0xA0, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            if (string.IsNullOrEmpty(machineID))
            {
                machineID = "255";
            }
            else
            {
                int macID;
                if (!int.TryParse(machineID, out macID))
                {
                    rvInfo.IsSuccess = false;
                    rvInfo.MessageText = "传入参数错误，需要整型值。";
                }

                byte[] byMacID = BitConverter.GetBytes(macID);
                data[1] = byMacID[0];
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
                mscom4Reader.Output = data;
                this._IsUsingCmd = "A0";
                Application.DoEvents();
                rvInfo.IsSuccess = true;
            }
            catch (Exception Ex)
            {
                rvInfo.IsSuccess = false;
                rvInfo.MessageText = Ex.Message;
            }

            return rvInfo;
        }

        /// <summary>
        /// 读取时钟（需停止读数后方可使用）
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// A1      OK
        public void GetReaderClock(int machineID)
        {
            GetReaderClockPrivate(machineID);
            GetReaderClockPrivate(machineID);
            GetReaderClockPrivate(machineID);
        }

        private void GetReaderClockPrivate(int machineID)
        {
            this._mscom4Reader.RThreshold = 15;

            byte[] data = { 0xC0, 0x00, 0xA1, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "A1";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 读取阅读器主参数
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// A3      OK
        public void GetReaderMainParams(int machineID)
        {
            //需要两次调用该方法来激活COM口抛出一条完整的指令条。

            GetReaderMainParamsPrivate(machineID);
            GetReaderMainParamsPrivate(machineID);
            GetReaderMainParamsPrivate(machineID);
        }

        private void GetReaderMainParamsPrivate(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xA3, 0x00, 0x00, 0x00, 0xC1 };

            _mscom4Reader.RThreshold = 80;

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "A3";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 恢复默认阅读器主参数
        /// </summary>
        /// <param name="machineID">机器号</param>
        ///A4       OK
        public void SetReaderRecover(int machineID)
        {
            SetReaderRecoverPrivate(machineID);
            SetReaderRecoverPrivate(machineID);
            SetReaderRecoverPrivate(machineID);
        }

        private void SetReaderRecoverPrivate(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xA4, 0x00, 0x00, 0x00, 0xC1 };

            _mscom4Reader.RThreshold = 10;

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "A4";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        #endregion

        #region Verayo标签操作专用指令
        /// <summary>
        /// 标签认证读卡器指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// B2      OK
        public void TagsAuthenticationCommand(int machineID)
        {
            TagsAuthenticationCommandPrivate(machineID);
            TagsAuthenticationCommandPrivate(machineID);
            TagsAuthenticationCommandPrivate(machineID);
        }

        private void TagsAuthenticationCommandPrivate(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xB2, 0x00, 0x00, 0x00, 0xC1 };

            _mscom4Reader.RThreshold = 25;

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "B2";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 256认证读卡器指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void SGSReaderCommand_256(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xB3, 0x00, 0x00, 0x00, 0xC1 };

            _mscom4Reader.RThreshold = 50;

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "B3";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 512认证读卡器指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void SGSReaderCommand_512(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xBC, 0x00, 0x00, 0x00, 0xC1 };

            _mscom4Reader.RThreshold = 50;

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "BC";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 写指定块指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="peaceNum">节块号</param>
        /// <param name="peaceData">节数据</param>
        public void WrittenDesignatedPiece(int machineID, string peaceNum, string peaceData)
        {
            byte[] data = { 0xC0, 0x00, 0xB4, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(peaceNum,16));
            data[4] = byPeaceNum[0];

            if (!string.IsNullOrEmpty(peaceData) && peaceData.Length == 8)
            {
                byte[] byFirst = BitConverter.GetBytes(Convert.ToInt32(peaceData.Substring(0, 2),16));
                data[5] = byFirst[0];
                byte[] bySecond = BitConverter.GetBytes(Convert.ToInt32(peaceData.Substring(2, 2),16));
                data[6] = bySecond[0];
                byte[] byThird = BitConverter.GetBytes(Convert.ToInt32(peaceData.Substring(4, 2),16));
                data[7] = byThird[0];
                byte[] byFour = BitConverter.GetBytes(Convert.ToInt32(peaceData.Substring(6, 2),16));
                data[8] = byFour[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[9] = lowCRC;
            data[10] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "B4";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 读指定块指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="peaceNum">块节号</param>
        public void GetDesignatedPiece(int machineID, int peaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0xB5, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(peaceNum);
            data[4] = byPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "B5";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 读标签Internal标记指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void GetTagsInternalCommand(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xB6, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "B6";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 获取随机数指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void GetRandomNumCommand(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xB7, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "B7";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 写口令到标签指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="randomNum">4字节随机数作为口令</param>
        public void WritePasswordToTheLable(int machineID, string randomNum)
        {
            byte[] data = { 0xC0, 0x00, 0xB8, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(randomNum) && randomNum.Length == 8)
            {
                byte[] byFirst = BitConverter.GetBytes(Convert.ToInt32(randomNum.Substring(0, 2),16));
                data[4] = byFirst[0];
                byte[] bySecond = BitConverter.GetBytes(Convert.ToInt32(randomNum.Substring(2, 2),16));
                data[5] = bySecond[0];
                byte[] byThird = BitConverter.GetBytes(Convert.ToInt32(randomNum.Substring(4, 2),16));
                data[6] = byThird[0];
                byte[] byFour = BitConverter.GetBytes(Convert.ToInt32(randomNum.Substring(6, 2),16));
                data[7] = byFour[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[8] = lowCRC;
            data[9] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "B8";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 读标签产生的响应指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void GetLableCommand(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xB9, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "B9";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 读标签PUF数据指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void GetPUFDataCommand(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xBA, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "BA";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 写读写器响应到标签指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void GetReaderLableCommand(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xBB, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "BB";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 使能标签认证模式
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void MakeCanLableModel(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xBD, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "BD";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 带认证写指定块指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="peaceNum">节块号</param>
        /// <param name="peaceData">节数据</param>
        /// BE
        //待解决,有卡和冇卡情况
        public void WrittenAuthenticationDesignatedPiece(int machineID, string peaceNum, string peaceData)
        {
            for (int i = 0; i < 20; i++)
            {
                WrittenAuthenticationDesignatedPiecePrivate(machineID, peaceNum, peaceData); 
            }
        }

        void WrittenAuthenticationDesignatedPiecePrivate(int machineID, string peaceNum, string peaceData)
        {
            _mscom4Reader.RThreshold = 30;

            byte[] data = { 0xC0, 0x00, 0xBE, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(peaceNum));
            data[4] = byPeaceNum[0];
            if (!string.IsNullOrEmpty(peaceData) && peaceData.Length==8)
            {
                byte[] byFirst = BitConverter.GetBytes(Convert.ToInt32(peaceData.Substring(0, 2),16));
                data[5] = byFirst[0];
                byte[] bySecond = BitConverter.GetBytes(Convert.ToInt32(peaceData.Substring(2, 2),16));
                data[6] = bySecond[0];
                byte[] byThird = BitConverter.GetBytes(Convert.ToInt32(peaceData.Substring(4, 2),16));
                data[7] = byThird[0];
                byte[] byFour = BitConverter.GetBytes(Convert.ToInt32(peaceData.Substring(6, 2),16));
                data[8] = byFour[0]; 
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[9] = lowCRC;
            data[10] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "BE";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 带认证读指定块指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="randomNum">1字节读取数据起始块号</param>
        /// BF      OK

        public void ReadAuthenticationDesignatedPiece(int machineID, string starPeaceNum)
        {
            ReadAuthenticationDesignatedPiecePrivate(machineID, starPeaceNum);
            ReadAuthenticationDesignatedPiecePrivate(machineID, starPeaceNum);
        }

        void ReadAuthenticationDesignatedPiecePrivate(int machineID, string starPeaceNum)
        {
            _mscom4Reader.RThreshold = 30;

            byte[] data = { 0xC0, 0x00, 0xBF, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte[] byStarPeaceNum = BitConverter.GetBytes(Convert.ToInt32(starPeaceNum, 16));
            data[4] = byStarPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "BF";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        #endregion

        #region 终端数据访问专用指令

        /// <summary>
        /// 读取一条未上传记录
        /// </summary>
        /// <param name="machineID">机器号</param>

        public void GetSigleData(int machineID)
        //{
        //    GetSigleDataPrivate(machineID);
        //    GetSigleDataPrivate(machineID);
        //}

        //private void GetSigleDataPrivate(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xC1, 0x00, 0x00, 0x00, 0xC1 };

            //_mscom4ULine.RThreshold = 50;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "C1";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 确认一笔记录

        /// </summary>
        /// <param name="recordIndex">交易流水号</param>
        public void ConfirmMultiData(int machineID, string recordIndex)
        {
            byte[] data = { 0xC0, 0x00, 0xC2, 0x02, 0x00, 0x00, 0x00, 0x00, 0xC1 };
            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            int iRecordIndex = Convert.ToInt32(recordIndex, 16);
            byte[] byRecordIndex = BitConverter.GetBytes(iRecordIndex);
            data[4] = byRecordIndex[1];
            data[5] = byRecordIndex[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[6] = lowCRC;
            data[7] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "C2";
                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 读取指定条数的记录

        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="recordRange">需要往后读取的未上传数据条数</param>
        public void GetDesignatedRecords(int machineID, int recordRange)
        {
            byte[] data = { 0xC0, 0x00, 0xC3, 0x01, 0x00, 0x00, 0x00, 0xC1 };
            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            if (recordRange < 1)
            {
                recordRange = 1;
            }
            else if (recordRange > 255)
            {
                recordRange = 255;
            }

            byte[] byRecordRange = BitConverter.GetBytes(recordRange);
            data[4] = byRecordRange[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "C3";
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            _recordRange = recordRange;
        }

        /// <summary>
        /// 获取当前保存在读写器中的记录总条数

        /// </summary>
        /// <param name="machineID">机器号</param>

        public void GetRecordsAmount(int machineID)
        {
            GetRecordsAmountPrivate(machineID);
            GetRecordsAmountPrivate(machineID);
        }

        private void GetRecordsAmountPrivate(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xC4, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 20;

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "C4";
                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 一次读取11条记录

        /// </summary>
        /// <param name="machineID">机器号</param>

        public void GetMultiData(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xC5, 0x00, 0x00, 0x00, 0xC1 };

            //this._mscom4ULine.RThreshold = 10;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "C5";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 擦除FLASH数据
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void DeleteAllRecords(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xCC, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "CC";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 读取FLASH所有数据

        /// </summary>
        /// <param name="machineID"></param>

        public void GetAllRecords(int machineID)
        {
            GetAllRecordsPrivate(machineID);
            GetAllRecordsPrivate(machineID);
        }

        private void GetAllRecordsPrivate(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xCF, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "CF";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region 终端控制专用指令
        /// <summary>
        /// 启动读卡器自动认证指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void StartReaderCommand(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xD0, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "D0";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 更改读卡间隔
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="readTime">1字节读卡时间间隔，以50ms为单位</param>
        public void ChangeReadInterval(int machineID, int readTime)
        {
            byte[] data = { 0xC0, 0x00, 0xD1, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte[] byReadTime = BitConverter.GetBytes(readTime);
            data[4] = byReadTime[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "D1";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// 更改VERAYO卡密钥
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="key">16字节新密钥</param>
        public void ChangeVerayoKey(int machineID, string key)
        {
            byte[] data = { 0xC0, 0x00, 0xD3, 0x10, 
                              0x00 ,0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                              0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(key) && key.Length==20)
            {
                byte[] byOne = BitConverter.GetBytes(Convert.ToInt32(key.Substring(0, 2),16));
                data[4] = byOne[0];
                byte[] byTwo = BitConverter.GetBytes(Convert.ToInt32(key.Substring(2, 2),16));
                data[5] = byTwo[0];
                byte[] byThree = BitConverter.GetBytes(Convert.ToInt32(key.Substring(4, 2),16));
                data[6] = byThree[0];
                byte[] byFour = BitConverter.GetBytes(Convert.ToInt32(key.Substring(6, 2),16));
                data[7] = byFour[0];
                byte[] byFive = BitConverter.GetBytes(Convert.ToInt32(key.Substring(8, 2),16));
                data[8] = byFive[0];
                byte[] bySix = BitConverter.GetBytes(Convert.ToInt32(key.Substring(10, 2),16));
                data[9] = bySix[0];
                byte[] bySeven = BitConverter.GetBytes(Convert.ToInt32(key.Substring(12, 2),16));
                data[10] = bySeven[0];
                byte[] byEight = BitConverter.GetBytes(Convert.ToInt32(key.Substring(14, 2),16));
                data[11] = byEight[0];
                byte[] byNine = BitConverter.GetBytes(Convert.ToInt32(key.Substring(16, 2),16));
                data[12] = byNine[0];
                byte[] byTen = BitConverter.GetBytes(Convert.ToInt32(key.Substring(18, 2),16));
                data[13] = byTen[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[14] = lowCRC;
            data[15] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "D3";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        #endregion

        #region 终端基础指令（适用于所有终端设备）
        /// <summary>
        /// 设置终端设备号，设置时需要单独连接一台设备。

        /// </summary>
        /// <param name="machineID">机器号，1字节</param>
        /// <param name="ID">ID号，4字节</param>
        /// E0


        public void SetEquipmentNum(string machineID, string ID)
        {
            SetEquipmentNumPrivate(machineID, ID);
            SetEquipmentNumPrivate(machineID, ID);
        }

        void SetEquipmentNumPrivate(string machineID, string ID)
        {
            _mscom4Reader.RThreshold = 15;

            byte[] data = { 0xC0, 0xFF, 0xE0, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            if (!string.IsNullOrEmpty(machineID) && !string.IsNullOrEmpty(ID))
            {
                if (machineID.Length == 2 && ID.Length == 8)
                {
                    for (int i = 0; i < ID.Length / 2; i++)
                    {
                        byte[] byID = BitConverter.GetBytes(Convert.ToInt32(ID.Substring(i * 2, 2), 16));
                        data[4 + i] = byID[0];
                    }

                    byte[] byMacID = BitConverter.GetBytes(Convert.ToInt32(machineID, 16));
                    data[8] = byMacID[0];
                }
            }
            else
            {
                return;
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[9] = lowCRC;
            data[10] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "E0";
                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 读取终端设备号
        /// </summary>
        /// E1      OK

        public void GetEquipmentNum()
        {
            GetEquipmentNumPrivate();
            GetEquipmentNumPrivate();
            GetEquipmentNumPrivate();
        }

        private void GetEquipmentNumPrivate()
        {
            this._mscom4Reader.RThreshold = 20;

            byte[] data = { 0xC0, 0xFF, 0xE1, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "E1";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 修改终端设备机号
        /// </summary>
        /// <param name="machineID">原机器号</param>
        /// <param name="newMachineID">新机器号</param>
        public void ModifyEquipmentNum(int machineID, int newMachineID)
        {
            byte[] data = { 0xC0, 0x00, 0xE2, 0x01, 0x00, 0x00, 0x00, 0xC1 };
            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte[] byRecordIndex = BitConverter.GetBytes(newMachineID);
            data[4] = byRecordIndex[1];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "E2";
                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void GetAllEquipmentNum()
        {
            this._mscom4Reader.RThreshold = 20;

            byte[] data = { 0xC0, 0xFF, 0xE3, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "E3";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 读取终端机器设备类型
        /// </summary>
        /// <param name="MachineID">机器号</param>

        public void GetEquipmentType(int MachineID)
        {
            GetEquipmentTypePrivate(MachineID);
            GetEquipmentTypePrivate(MachineID);
        }

        private void GetEquipmentTypePrivate(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0xE4, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "E4";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 读取程序版本
        /// </summary>
        /// <param name="MachineID">机器号</param>

        public void GetProgramVersion(int MachineID)
        {
            GetProgramVersionPrivate(MachineID);
            GetProgramVersionPrivate(MachineID);
        }

        void GetProgramVersionPrivate(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0xE5, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "E5";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        #endregion        

        #region 暂时未写

        #region ISO14443A操作指令
        /// <summary>
        /// ISO14443A_Request指令(寻卡)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 21
        public void ISO14443A_Request(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x21, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "21";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO14443A_Anticoll指令(Get_UID)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 22
        public void ISO14443A_Anticoll(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x22, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "22";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO14443A_Select(选卡)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="ID">4字节ID</param>
        /// 23        
        public void ISO14443A_Select(int MachineID,string ID)
        {
            byte[] data = { 0xC0, 0x00, 0x23, 0x04,
                              0x00, 0x00, 0x00, 0x00,  
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(ID) && ID.Length == 8)
            {
                byte[] byOne = BitConverter.GetBytes(Convert.ToInt32(ID.Substring(0, 2),16));
                data[4] = byOne[0];
                byte[] byTwo = BitConverter.GetBytes(Convert.ToInt32(ID.Substring(2, 2),16));
                data[5] = byTwo[0];
                byte[] byThree = BitConverter.GetBytes(Convert.ToInt32(ID.Substring(4, 2),16));
                data[6] = byThree[0];
                byte[] byFour = BitConverter.GetBytes(Convert.ToInt32(ID.Substring(6, 2),16));
                data[7] = byFour[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[8] = lowCRC;
            data[9] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "23";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO14443A_Halt指令(挂起)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 24
        public void ISO14443A_Halt(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x24, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "24";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO14443A_Authentication(认证)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="ModelID">1字节认证模式</param>
        /// <param name="PeaceNum">1字节认证绝对块号</param>
        /// <param name="Key">6字节密钥</param>
        /// 25
        public void ISO14443A_Authentication(int MachineID, string ModelID, string PeaceNum, string Key)
        {
            byte[] data = { 0xC0, 0x00, 0x25, 0x08,
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byModelID = BitConverter.GetBytes(Convert.ToInt32(ModelID,16));
            data[4] = byModelID[0];
            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[5] = byPeaceNum[0];
            if (!string.IsNullOrEmpty(Key) && Key.Length == 12)
            {
                byte[] byOne = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(0, 2),16));
                data[6] = byOne[0];
                byte[] byTwo = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(2, 2),16));
                data[7] = byTwo[0];
                byte[] byThree = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(4, 2),16));
                data[8] = byThree[0];
                byte[] byFour = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(6, 2),16));
                data[9] = byFour[0];
                byte[] byFive = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(8, 2),16));
                data[10] = byFive[0];
                byte[] bySix = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(10, 2),16));
                data[11] = bySix[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[12] = lowCRC;
            data[13] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "25";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1_Read(读块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum"></param>
        /// 26
        public void M1_Read(int MachineID, int PeaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0x26, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(PeaceNum);
            data[4] = byPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "26";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1_Write(写块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum">1字节块号</param>
        /// <param name="PeaceData">16字节写入数据</param>
        /// 27
        public void M1_Write(int MachineID, string PeaceNum, string PeaceData)
        {
            byte[] data = { 0xC0, 0x00, 0x27, 0x11,
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[4] = byPeaceNum[0];
            if (!string.IsNullOrEmpty(PeaceData) && PeaceData.Length == 32)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(14, 2),16));
                data[12] = by8[0];
                byte[] by9 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(16, 2),16));
                data[13] = by9[0];
                byte[] by10 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(18, 2),16));
                data[14] = by10[0];
                byte[] by11 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(20, 2),16));
                data[15] = by11[0];
                byte[] by12 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(22, 2),16));
                data[16] = by12[0];
                byte[] by13 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(24, 2),16));
                data[17] = by13[0];
                byte[] by14 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(26, 2),16));
                data[18] = by14[0];
                byte[] by15 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(28, 2),16));
                data[19] = by15[0];
                byte[] by16 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(30, 2),16));
                data[20] = by16[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[21] = lowCRC;
            data[22] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "27";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1_Initvalue(初始化钱包)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum">1字节块号</param>
        /// <param name="PeaceData">16字节写入数据</param>
        /// 28
        public void M1_Initvalue(int MachineID, string PeaceNum, string PeaceData)
        {
            byte[] data = { 0xC0, 0x00, 0x28, 0x11,
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[4] = byPeaceNum[0];
            if (!string.IsNullOrEmpty(PeaceData) && PeaceData.Length == 32)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(14, 2),16));
                data[12] = by8[0];
                byte[] by9 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(16, 2),16));
                data[13] = by9[0];
                byte[] by10 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(18, 2),16));
                data[14] = by10[0];
                byte[] by11 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(20, 2),16));
                data[15] = by11[0];
                byte[] by12 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(22, 2),16));
                data[16] = by12[0];
                byte[] by13 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(24, 2),16));
                data[17] = by13[0];
                byte[] by14 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(26, 2),16));
                data[18] = by14[0];
                byte[] by15 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(28, 2),16));
                data[19] = by15[0];
                byte[] by16 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(30, 2),16));
                data[20] = by16[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[21] = lowCRC;
            data[22] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "28";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1_Readvalue(读钱包余额)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum"></param>
        /// 29
        public void M1_Readvalue(int MachineID, int PeaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0x29, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(PeaceNum);
            data[4] = byPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "29";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1_Decrement(电子钱包减值)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum">1字节块号</param>
        /// <param name="PeaceData">4字节欲减少的值，低位在前</param>
        /// 2A
        public void M1_Decrement(int MachineID, string PeaceNum, string PeaceData)
        {
            byte[] data = { 0xC0, 0x00, 0x2A, 0x05, 
                              0x00, 0x00, 0x00, 0x00, 0x00,
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[4] = byPeaceNum[0];
            if (!string.IsNullOrEmpty(PeaceData) && PeaceData.Length == 8)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(6, 2),16));
                data[8] = by4[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[9] = lowCRC;
            data[10] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "2A";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1_Increment(电子钱包增值)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum">1字节块号</param>
        /// <param name="PeaceData">4字节欲增加的值，低位在前</param>
        /// 2B
        public void M1_Increment(int MachineID, string PeaceNum, string PeaceData)
        {
            byte[] data = { 0xC0, 0x00, 0x2B, 0x05, 
                              0x00, 0x00, 0x00, 0x00, 0x00,
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[4] = byPeaceNum[0];
            if (!string.IsNullOrEmpty(PeaceData) && PeaceData.Length == 8)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(6, 2),16));
                data[8] = by4[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[9] = lowCRC;
            data[10] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "2B";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1_Restore(备份钱包，将块数据调入卡缓冲区)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum"></param>
        /// 2C
        public void M1_Restore(int MachineID, int PeaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0x2C, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(PeaceNum);
            data[4] = byPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "2C";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1_Transfer(备份钱包，将卡缓冲区中数据保存到块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum"></param>
        /// 2D
        public void M1_Transfer(int MachineID, int PeaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0x2D, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(PeaceNum);
            data[4] = byPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "2D";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1带认证读指令
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum"></param>
        /// 2E
        public void M1_SGSRead(int MachineID, int PeaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0x2E, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(PeaceNum);
            data[4] = byPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "2E";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// M1带认证写指令
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceNum"></param>
        /// 2F
        public void M1_SGSWrite(int MachineID, string PeaceNum, string PeaceData)
        {
            byte[] data = { 0xC0, 0x00, 0x2F, 0x11,
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[4] = byPeaceNum[0];
            if (!string.IsNullOrEmpty(PeaceData) && PeaceData.Length == 32)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(14, 2),16));
                data[12] = by8[0];
                byte[] by9 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(16, 2),16));
                data[13] = by9[0];
                byte[] by10 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(18, 2),16));
                data[14] = by10[0];
                byte[] by11 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(20, 2),16));
                data[15] = by11[0];
                byte[] by12 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(22, 2),16));
                data[16] = by12[0];
                byte[] by13 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(24, 2),16));
                data[17] = by13[0];
                byte[] by14 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(26, 2),16));
                data[18] = by14[0];
                byte[] by15 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(28, 2),16));
                data[19] = by15[0];
                byte[] by16 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(30, 2),16));
                data[20] = by16[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[21] = lowCRC;
            data[22] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "2F";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region ISO14443B操作指令
        /// <summary>
        /// ISO14443B_Request(寻卡)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 31
        public void ISO14443B_Request(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x31, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "31";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO14443B_Halt(挂起)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 32
        public void ISO14443B_Halt(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x32, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "32";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO14443B_Deselect(取消选择)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 33
        public void ISO14443B_Deselect(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x33, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "33";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region AT88RF020卡操作指令

        /// <summary>
        /// RF020Check(RF020Check认证)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Key">8字节密钥</param>
        /// 41
        public void RF020Check(int MachineID, string Key)
        {
            byte[] data = { 0xC0, 0x00, 0x41, 0x08,
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(Key) && Key.Length==16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(0,2),16));
                data[4] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(2, 2),16));
                data[5] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(4, 2),16));
                data[6] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(6, 2),16));
                data[7] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(8, 2),16));
                data[8] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(10, 2),16));
                data[9] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(12, 2),16));
                data[10] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(Key.Substring(14, 2),16));
                data[11] = by8[0]; 
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[12] = lowCRC;
            data[13] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "41";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// RF020Read(RF020读指定块数据)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Address">1字节页地址</param>
        /// 42
        public void RF020Read(int MachineID, int Address)
        {
            byte[] data = { 0xC0, 0x00, 0x42, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byAddress = BitConverter.GetBytes(Address);
            data[4] = byAddress[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "42";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// RF020Write(写块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Address">1字节页地址</param>
        /// <param name="AddressData">8字节要写入的数据</param>
        /// 43
        public void RF020Write(int MachineID, string Address, string AddressData)
        {
            byte[] data = { 0xC0, 0x00, 0x43, 0x09, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byAddress = BitConverter.GetBytes(Convert.ToInt32(Address,16));
            data[4] = byAddress[0];
            if (!string.IsNullOrEmpty(AddressData) && AddressData.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(0,2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(14, 2),16));
                data[12] = by8[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[13] = lowCRC;
            data[14] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "43";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// RF020Lock(锁卡)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="WriteData">4字节写入数据</param>
        /// 44
        public void RF020Lock(int MachineID, string WriteData)
        {
            byte[] data = { 0xC0, 0x00, 0x44, 0x04, 
                              0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(WriteData) && WriteData.Length == 8)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(WriteData.Substring(0,2),16));
                data[4] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(WriteData.Substring(2, 2),16));
                data[5] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(WriteData.Substring(4, 2),16));
                data[6] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(WriteData.Substring(6, 2),16));
                data[7] = by4[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[8] = lowCRC;
            data[9] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "44";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// RF020Count(计数操作)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="WriteData">6字节签名信息</param>
        /// 45
        public void RF020Count(int MachineID, string Info)
        {
            byte[] data = { 0xC0, 0x00, 0x45, 0x06, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(Info) && Info.Length == 12)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(Info.Substring(0,2),16));
                data[4] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(Info.Substring(2, 2),16));
                data[5] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(Info.Substring(4, 2),16));
                data[6] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(Info.Substring(6, 2),16));
                data[7] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(Info.Substring(8, 2),16));
                data[8] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(Info.Substring(10, 2),16));
                data[9] = by6[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[10] = lowCRC;
            data[11] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "45";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// RF020Deselect(进入halt状态)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 46
        public void RF020Deselect(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x46, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "46";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region SR卡操作指令
        /// <summary>
        /// SR176_Select(选卡)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 51
        public void SR176_Select(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x51, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "51";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// SR176_Completion
        /// </summary>
        /// <param name="MachineID"></param>
        /// 52
        public void SR176_Completion(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x52, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "52";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// SR176_Read(读块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Address">1字节地址</param>
        /// 53
        public void SR176_Read(int MachineID, int Address)
        {
            byte[] data = { 0xC0, 0x00, 0x53, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byAddress = BitConverter.GetBytes(Address);
            data[4] = byAddress[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "53";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// SR176_Write(写块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Address">1字节地址</param>
        /// <param name="AddressData">2字节数据</param>
        /// 54
        public void SR176_Write(int MachineID, string Address, string AddressData)
        {
            byte[] data = { 0xC0, 0x00, 0x54, 0x03, 
                              0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byAddress = BitConverter.GetBytes(Convert.ToInt32(Address,16));
            data[4] = byAddress[0];
            if (!string.IsNullOrEmpty(AddressData) && AddressData.Length==4)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(0,2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(2, 2),16));
                data[6] = by2[0]; 
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[7] = lowCRC;
            data[8] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "54";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// SR176_Protect(锁定块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Address">1字节地址</param>
        /// 55
        public void SR176_Protect(int MachineID, int Address)
        {
            byte[] data = { 0xC0, 0x00, 0x55, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byAddress = BitConverter.GetBytes(Address);
            data[4] = byAddress[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "55";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// SR4K_Read(读指定块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Address">1字节地址</param>
        /// 56
        public void SR4K_Read(int MachineID, int Address)
        {
            byte[] data = { 0xC0, 0x00, 0x56, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byAddress = BitConverter.GetBytes(Address);
            data[4] = byAddress[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "56";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// SR4K_Write(写指定块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Address">1字节地址</param>
        /// <param name="AddressData">4字节数据</param>
        /// 57
        public void SR4K_Write(int MachineID, string Address, string AddressData)
        {
            byte[] data = { 0xC0, 0x00, 0x57, 0x05, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byAddress = BitConverter.GetBytes(Convert.ToInt32(Address,16));
            data[4] = byAddress[0];
            if (!string.IsNullOrEmpty(AddressData) && AddressData.Length == 8)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(AddressData.Substring(6, 2),16));
                data[8] = by4[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[9] = lowCRC;
            data[10] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "57";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// SR4K_Auth(认证标签)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceData">1字节数据</param>
        /// 58
        public void SR4K_Auth(int MachineID, int PeaceData)
        {
            byte[] data = { 0xC0, 0x00, 0x58, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byPeaceData = BitConverter.GetBytes(PeaceData);
            data[4] = byPeaceData[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "58";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// SR4K_GetUID(读取UID)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 59
        public void SR4K_GetUID(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x59, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "59";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// SR4K_Protect(锁卡)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="PeaceData">4字节数据</param>
        /// 5A
        public void SR4K_Protect(int MachineID, string PeaceData)
        {
            byte[] data = { 0xC0, 0x00, 0x5A, 0x04, 
                              0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(PeaceData) && PeaceData.Length == 8)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(0, 2),16));
                data[4] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(2, 2),16));
                data[5] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(4, 2),16));
                data[6] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(6, 2),16));
                data[7] = by4[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[8] = lowCRC;
            data[9] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "5A";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region ISO15693卡操作指令
        /// <summary>
        /// ISO15693_Inventory16(16时隙寻卡，带防冲突)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 61
        public void ISO15693_Inventory16(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x61, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "61";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Inventory(1时隙寻卡)
        /// </summary>
        /// <param name="MachineID"></param>
        /// 62
        public void ISO15693_Inventory(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0x62, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "62";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Stay_Quiet(挂起指定标签)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="UID">8字节UID</param>
        /// 63
        public void ISO15693_Stay_Quiet(int MachineID, string UID)
        {
            byte[] data = { 0xC0, 0x00, 0x63, 0x08, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0,2),16));
                data[4] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[5] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[6] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[7] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[8] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[9] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[10] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[11] = by8[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[12] = lowCRC;
            data[13] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "63";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Select(选择指定标签)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="UID">8字节UID</param>
        /// 64
        public void ISO15693_Select(int MachineID, string UID)
        {
            byte[] data = { 0xC0, 0x00, 0x64, 0x08, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[4] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[5] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[6] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[7] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[8] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[9] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[10] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[11] = by8[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[12] = lowCRC;
            data[13] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "64";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Reset_To_Ready(复位指定标签)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// 65
        public void ISO15693_Reset_To_Ready(int MachineID, string Flag, string UID)
        {
            byte[] data = { 0xC0, 0x00, 0x65, 0x09, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[13] = lowCRC;
            data[14] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "65";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Read(读指定块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// 66
        public void ISO15693_Read(int MachineID, string Flag, string UID)
        {
            byte[] data = { 0xC0, 0x00, 0x66, 0x09, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[13] = lowCRC;
            data[14] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "66";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Write(写指定块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// <param name="PeaceNum">1字节块号</param>
        /// <param name="PeaceData">4字节数据</param>
        /// 67
        public void ISO15693_Write(int MachineID, string Flag, string UID, string PeaceNum, string PeaceData)
        {
            byte[] data = { 0xC0, 0x00, 0x67, 0x0E, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,0x00, 0x00,  
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }
            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[13] = byPeaceNum[0];
            if (!string.IsNullOrEmpty(PeaceData) && PeaceData.Length == 8)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(0, 2),16));
                data[14] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(2, 2),16));
                data[15] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(4, 2),16));
                data[16] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(PeaceData.Substring(6, 2),16));
                data[17] = by4[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[18] = lowCRC;
            data[19] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "67";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Lock(锁定指定块)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// <param name="PeaceNum">1字节块号</param>
        /// 68
        public void ISO15693_Lock(int MachineID, string Flag, string UID, string PeaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0x68, 0x0A, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }
            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[13] = byPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[14] = lowCRC;
            data[15] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "68";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// ISO15693_Write_AFI(写AFI)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// <param name="PeaceNum">1字节块号</param>
        /// 69
        public void ISO15693_Write_AFI(int MachineID, string Flag, string UID, string PeaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0x69, 0x0A, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }
            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[13] = byPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[14] = lowCRC;
            data[15] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "69";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// ISO15693_Lock_AFI(锁定AFI)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// 6A
        public void ISO15693_Lock_AFI(int MachineID, string Flag, string UID)
        {
            byte[] data = { 0xC0, 0x00, 0x6A, 0x09, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[13] = lowCRC;
            data[14] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "6A";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Write_DSFID(写DSFID)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// <param name="DSFID">1字节DSFID</param>
        /// 6B
        public void ISO15693_Write_DSFID(int MachineID, string Flag, string UID, string DSFID)
        {
            byte[] data = { 0xC0, 0x00, 0x6B, 0x0A, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }
            byte[] byDSFID = BitConverter.GetBytes(Convert.ToInt32(DSFID,16));
            data[13] = byDSFID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[14] = lowCRC;
            data[15] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "6B";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Lock_DSFID(锁定DSFID)
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// 6C
        public void ISO15693_Lock_DSFID(int MachineID, string Flag, string UID)
        {
            byte[] data = { 0xC0, 0x00, 0x6C, 0x09, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[13] = lowCRC;
            data[14] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "6C";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Get_System_Information
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// 6D
        public void ISO15693_Get_System_Information(int MachineID, string Flag, string UID)
        {
            byte[] data = { 0xC0, 0x00, 0x6D, 0x09, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[13] = lowCRC;
            data[14] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "6D";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }
        /// <summary>
        /// ISO15693_Get_Multiple_Block_Security
        /// </summary>
        /// <param name="MachineID"></param>
        /// <param name="Flag">1字节Flag标记</param>
        /// <param name="UID">8字节UID</param>
        /// <param name="PeaceNum">1字节块号</param>
        /// 6E
        public void ISO15693_Get_Multiple_Block_Security(int MachineID, string Flag, string UID, string PeaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0x6E, 0x0A, 
                              0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                              0x00, 0x00, 0xC1 };

            this._mscom4Reader.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte[] byFlag = BitConverter.GetBytes(Convert.ToInt32(Flag,16));
            data[4] = byFlag[0];
            if (!string.IsNullOrEmpty(UID) && UID.Length == 16)
            {
                byte[] by1 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(0, 2),16));
                data[5] = by1[0];
                byte[] by2 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(2, 2),16));
                data[6] = by2[0];
                byte[] by3 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(4, 2),16));
                data[7] = by3[0];
                byte[] by4 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(6, 2),16));
                data[8] = by4[0];
                byte[] by5 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(8, 2),16));
                data[9] = by5[0];
                byte[] by6 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(10, 2),16));
                data[10] = by6[0];
                byte[] by7 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(12, 2),16));
                data[11] = by7[0];
                byte[] by8 = BitConverter.GetBytes(Convert.ToInt32(UID.Substring(14, 2),16));
                data[12] = by8[0];
            }
            byte[] byPeaceNum = BitConverter.GetBytes(Convert.ToInt32(PeaceNum,16));
            data[13] = byPeaceNum[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[14] = lowCRC;
            data[15] = highCRC;

            try
            {
                mscom4Reader.Output = data;
                this._IsUsingCmd = "6E";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #endregion

        #endregion

        #region 事件处理
        /// <summary>
        /// 发布时钟

        /// </summary>
        /// <param name="entity"></param>
        /// A1
        protected void OnGettingClock(VerayoClock entity)
        {
            if (this.ClockShowing != null)
            {
                this.ClockShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布设备主参数

        /// </summary>
        /// <param name="entity"></param>
        /// A3
        protected void OnGettingEquipMainParams(VerayoReaderEntity entity)
        {
            if (this.EquipmentMainParamsShowing != null)
            {
                this.EquipmentMainParamsShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布UID
        /// </summary>
        /// <param name="entity"></param>
        /// B1
        protected void OnGettingTagUID(VerayoUID entity)
        {
            if (this.TagUidShowing != null)
            {
                this.TagUidShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布指定块
        /// </summary>
        /// <param name="entity"></param>
        /// B5
        protected void OnGettingDesignatedPiece(VerayoDesignatedPiece entity)
        {
            if (this.DesignatedPieceShowing != null)
            {
                this.DesignatedPieceShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布标签Internal标记
        /// </summary>
        /// <param name="entity"></param>
        /// B6
        protected void OnGettingTagsInternal(VerayoTagsInternal entity)
        {
            if (this.TagsInternalShowing != null)
            {
                this.TagsInternalShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布随机数
        /// </summary>
        /// <param name="entity"></param>
        /// B7
        protected void OnGettingRandomNum(VerayoRandomNum entity)
        {
            if (this.RandomNumShowing != null)
            {
                this.RandomNumShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布标签产生的响应
        /// </summary>
        /// <param name="entity"></param>
        /// B9
        protected void OnGettingGetLable(VerayoGetLable entity)
        {
            if (this.GetLableShowing != null)
            {
                this.GetLableShowing(this, entity);
            }
        }

        /// <summary>
        /// 发布标签PUF数据
        /// </summary>
        /// <param name="entity"></param>
        /// BA
        protected void OnGettingTagPUF(VerayoTagPUF entity)
        {
            if (this.TagPUFShowing != null)
            {
                this.TagPUFShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布读写器响应到标签
        /// </summary>
        /// <param name="entity"></param>
        /// BB
        protected void OnGettingReaderLable(VerayoReaderLable entity)
        {
            if (this.ReaderLableShowing != null)
            {
                this.ReaderLableShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布带认证读指定块
        /// </summary>
        /// <param name="entity"></param>
        /// BF
        protected void OnGettingTakeAuthenticationReadPiece(VerayoTakeAuthenticationReadPiece entity)
        {
            if (this.TakeAuthenticationReadPieceShowing != null)
            {
                this.TakeAuthenticationReadPieceShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布记录
        /// </summary>
        /// <param name="entity"></param>
        /// C1、C3、C5、CF
        protected void OnGettingRecordsInformation(VerayoRecordsInformation entity)
        {
            if (this.RecordsInformationShowing != null)
            {
                this.RecordsInformationShowing(this, entity);
            }
        }

        /// <summary>
        /// 发布读写器记录总数

        /// </summary>
        /// <param name="entity"></param>
        /// C4
        protected void OnGettingRecordsAmount(VerayoRecordsAmount entity)
        {
            if (this.RecordsAmountShowing != null)
            {
                this.RecordsAmountShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布终端机器号

        /// </summary>
        /// <param name="entity"></param>
        /// E1
        protected void OnGettingEquipmentNum(VerayoEquipmentNum entity)
        {
            if (this.EquipmentNumShowing != null)
            {
                this.EquipmentNumShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布所有终端机器号
        /// </summary>
        /// <param name="entity"></param>
        /// E3
        protected void OnGettingAllEquipmentNum(VerayoAllEquipmentNum entity)
        {
            if (this.AllEquipmentNumShowing != null)
            {
                this.AllEquipmentNumShowing(this, entity);
            }
        }

        /// <summary>
        /// 发布终端机器类型

        /// </summary>
        /// <param name="entity"></param>
        /// E4
        protected void OnGettingEquipmentType(VerayoEquipmentType entity)
        {
            if (this.EquipmentTypeShowing != null)
            {
                this.EquipmentTypeShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布程序版本号

        /// </summary>
        /// <param name="entity"></param>
        /// E5
        protected void OnGettingEquipmentVersion(VerayoEquipmentVersion entity)
        {
            if (this.EquipmentVersionShowing != null)
            {
                this.EquipmentVersionShowing(this, entity);
            }
        }

        #endregion

    }
}
