using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReaderEquipment.Entity;
using ReaderEquipment.Entity.ULineHF;

namespace ReaderEquipment.ReaderImplement
{
    public partial class ULineHFReader : AbstractReader
    {
        #region 字段、属性、事件

        #region MyRegion
        private int _recordRange = 1;
        #endregion


        private AxMSCommLib.AxMSComm _mscom4ULine;

        /// <summary>
        /// 正在使用的指令编号
        /// </summary>
        private string _IsUsingCmd;

        public string IsUsingCmd
        {
            get { return _IsUsingCmd; }
            set { _IsUsingCmd = value; }
        }

        private string _OutCmd;

        public string OutCmd
        {
            get { return _OutCmd; }
            set { _OutCmd = value; }
        }

        private object _objReadingLock;

        private object _objTimerLock;

        private System.Timers.Timer _timerRead;

        /// <summary>
        /// 获取数据事件
        /// </summary>
        public event EventHandler<TagInfoResultEventArgs> TagInformationResultShowing;

        /// <summary>
        /// 获取设备主参数事件
        /// </summary>
        public event EventHandler<ULineHFReaderEntity> EquipmentMainParamsShowing;

        #region MyRegion

        /// <summary>
        /// 获取时钟事件

        /// </summary>
        public event EventHandler<ULineClock> ClockShowing;

        /// <summary>
        /// 获取读写器记录总数事件

        /// </summary>
        public event EventHandler<ULineRecordsAmount> RecordsAmount;

        /// <summary>
        /// 获取标签UID命令事件

        /// </summary>
        public event EventHandler<ULineUID> SetCommandReadUid;

        /// <summary>
        /// 获取读卡命令事件

        /// </summary>
        public event EventHandler<ULineSetCommandCardReading> SetCommandCardRead;
        

         /// <summary>
        /// 获取终端设备号事件

        /// </summary>
        public event EventHandler<ULineEquipmentNum> EquipmentNum;

         /// <summary>
        /// 获取终端设备机器类型事件

        /// </summary>
        public event EventHandler<ULineEquipmentType> EquipmentType;

        /// <summary>
        /// 获取程序版本号事件

        /// </summary>
        public event EventHandler<ULineEquipmentVersion> EquipmentVersion;
        
        #endregion

        private short _BufferLength = 430;
        /// <summary>
        /// COM口缓存字符量
        /// </summary>
        public short BufferLength
        {
            get { return _BufferLength; }
            set { _BufferLength = value; }
        }
        private int _ReadInterval = 500;
        /// <summary>
        /// 读取标签的时间间隔
        /// </summary>
        public int ReadInterval
        {
            get { return _ReadInterval; }
            set { _ReadInterval = value; }
        }
        private bool _IsReading = false;
        /// <summary>
        /// 是否正在读取数据
        /// </summary>
        public bool IsReading
        {
            get { return _IsReading; }
            set { _IsReading = value; }
        }

        /// <summary>
        /// 存储设备主参数
        /// </summary>
        //private ULineHFReaderEntity _ULineHFReaderEntity;

        #endregion

        public ULineHFReader()
        {
            InitializeComponent();

            _mscom4ULine = new AxMSCommLib.AxMSComm();
            _objReadingLock = new object();

            _timerRead = new System.Timers.Timer();
            _timerRead.Interval = this._ReadInterval;
            _timerRead.Elapsed += new System.Timers.ElapsedEventHandler(_timerRead_Elapsed);
        }


        public ULineHFReader(short comPort)
        {


            InitializeComponent();
            this.CommPort = comPort;
            _mscom4ULine = new AxMSCommLib.AxMSComm();
            _objReadingLock = new object();

            _timerRead = new System.Timers.Timer();
            _timerRead.Interval = this._ReadInterval;
            _timerRead.Elapsed += new System.Timers.ElapsedEventHandler(_timerRead_Elapsed);
        }

        public ULineHFReader(short comPort, int machineID)
        {
            InitializeComponent();

            this.CommPort = comPort;
            this.MachineID = machineID;

            _mscom4ULine = new AxMSCommLib.AxMSComm();
            _objReadingLock = new object();
            _objTimerLock = new object();

            _timerRead = new System.Timers.Timer();
            _timerRead.Interval = 800;
            _timerRead.Elapsed += new System.Timers.ElapsedEventHandler(_timerRead_Elapsed);
        }

        void _timerRead_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (this._objTimerLock)
            {
                switch (this._IsUsingCmd)
                {
                    case "C1":
                            GetSigleData(base.MachineID);
                            break;
                    case "C3":
                            GetDesignatedRecords(base.MachineID, _recordRange);
                            break;
                    case "C5":
                            GetMultiData(base.MachineID);
                            break;
                    case "CF":
                        GetAllRecords(base.MachineID);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        ///初始化COM口信息
        /// </summary>
        private void InitComPort()
        {
            this._mscom4ULine = this.mscom4ULine;

            if (this._mscom4ULine.PortOpen)
            {
                this._mscom4ULine.PortOpen = false;
            }

            _mscom4ULine.InputLen = 0;  //清除接收缓冲区

            _mscom4ULine.CommPort = base.CommPort;//设置端口号


            _mscom4ULine.RThreshold = this._BufferLength;   //每接收一个字符则激发OnComm()事件

            //_mscom4ULine.RThreshold =1;   //每接收一个字符则激发OnComm()事件

            _mscom4ULine.Settings = "38400,N,8,1";  //端口设置

            _mscom4ULine.DTREnable = true;  //置DTR有效

            _mscom4ULine.RTSEnable = true;  //置RTS有效

            _mscom4ULine.Handshaking = MSCommLib.HandshakeConstants.comNone;

            _mscom4ULine.InputMode = MSCommLib.InputModeConstants.comInputModeBinary;   //二进制

            _mscom4ULine.NullDiscard = false;

            _mscom4ULine.OnComm += new EventHandler(ReadDataForCOM);  //执行一个OnComm事件

            _mscom4ULine.InBufferCount = 0;

            _mscom4ULine.OutBufferCount = 0;
        }

        /// <summary>
        /// 从COM口处读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadDataForCOM(object sender, EventArgs e)
        {
            if (this._mscom4ULine.CommEvent == (short)(MSCommLib.OnCommConstants.comEvReceive))//查询CommEvent属性
            {
                lock (this._objReadingLock)
                {

                    System.Threading.Thread.Sleep(8);

                    object inputObjs = this._mscom4ULine.Input;

                    byte[] inputBytes = (byte[])inputObjs;

                    if (inputBytes.Length < 1)
                    {
                        return;
                    }

                    SourceDataHandling(inputBytes);
                }
            }
        }

        /// <summary>
        /// 处理原始记录
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

            #region MyRegion
            if (this._IsReading)
            {
                listUid = ReaderGeneral.HandleSourceDataToList(strInput, 78);
                List<TagInformationInfo> listTagInfos = new List<TagInformationInfo>();
                foreach (string strUid in listUid)
                {
                    TagInformationInfo tagInfo = GetEntity(strUid);
                    if (tagInfo != null)
                    {
                        listTagInfos.Add(tagInfo);
                    }
                }
                TagInfoResultEventArgs args = new TagInfoResultEventArgs();

                #region MyRegion

                switch (this._IsUsingCmd)
                {
                    case "C1":
                        {
                            listUid = ReaderGeneral.HandleSourceDataToList(strInput);
                            listTagInfos = new List<TagInformationInfo>();
                            TagInformationInfo tagInfo = GetEntity(listUid[0]);
                            if (tagInfo != null)
                            {
                                listTagInfos.Add(tagInfo);
                            }
                            args.ExamineNum = "C1";
                            #region 读取一条记录后停止读数
                            //if (listTagInfos.Count > 0)
                            //    StopRead(); 
                            #endregion
                            break;
                        }
                    case "C3":
                        {
                            listTagInfos = new List<TagInformationInfo>();
                            for (int i = 0; i < _recordRange; i++)
                            {
                                TagInformationInfo tagInfo = GetEntity(listUid[i]);
                                if (tagInfo != null)
                                {
                                    listTagInfos.Add(tagInfo);
                                }
                            }
                            args.ExamineNum = "C3";
                            #region 读取指定条数后停止读数
                            //StopRead(); 
                            #endregion
                            break;
                        }
                    case "C5":
                        {
                            args.ExamineNum = "C5";
                            break;
                        }
                    case "CF":
                        {
                            args.ExamineNum = "CF";
                            break;
                        }
                    default:
                        break;
                }                
                #endregion

                args.TagInformationList.AddRange(listTagInfos);
                OnTagResult(args);
            }
            else
            {
                listUid = ReaderGeneral.HandleSourceDataToList(strInput);
                if (listUid != null && listUid.Count > 0)
                {
                    switch (this._IsUsingCmd)
                    {
                        case "BF":
                            {
                                ULineHFReaderEntity entity = new ULineHFReaderEntity(listUid[0]);
                                OnGettingEquipMainParams(entity);
                                break;
                            }
                        #region MyRegion
                        case "AF":
                            {
                                ULineClock entity = new ULineClock(listUid[0]);
                                OnGettingClock(entity);
                                break;
                            }
                        case "D6":
                            {
                                ULineUID entity = new ULineUID(listUid[0]);
                                OnSetCommandReadUid(entity);
                                break;
                            }
                        case "C4":
                            {
                                ULineRecordsAmount entity = new ULineRecordsAmount(listUid[0]);
                                OnGettingRecordsAmount(entity);
                                break;
                            }

                        #region 待解决
                        case "DA":
                            {
                                ULineSetCommandCardReading entity = new ULineSetCommandCardReading(listUid[0]);
                                OnSetCommandCardRead(entity);
                                break;
                            }
                        #endregion  
                      
                        case "E1":
                            {
                                ULineEquipmentNum entity = new ULineEquipmentNum(listUid[0]);
                                OnGettingEquipmentNum(entity);
                                break;
                            }
                        case "E4":
                            {
                                ULineEquipmentType entity = new ULineEquipmentType(listUid[0]);
                                OnGettingEquipmentType(entity);
                                break;
                            }
                        case "E5":
                            {
                                ULineEquipmentVersion entity = new ULineEquipmentVersion(listUid[0]);
                                OnGettingEquipmentVersion(entity);
                                break;
                            }
                        #endregion
                        default:
                            break;
                    }
                }
            } 
            #endregion
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
                if (!this._mscom4ULine.PortOpen)
                {
                    this._mscom4ULine.CommPort = base.CommPort;
                    this._mscom4ULine.PortOpen = true;
                }

                _mscom4ULine.RThreshold = this.BufferLength;

                _IsReading = true;

                _timerRead.Start();

                AdjustMachineTimer("");

                #region MyRegion
                if (_OutCmd != string.Empty)
                {
                    _IsUsingCmd = _OutCmd;
                }
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

        public override void StopRead()
        {
            _timerRead.Stop();
            this._IsReading = false;
        }

        /// <summary>
        /// 连接设备
        /// </summary>
        /// <returns></returns>
        public override ReaderEquipment.Entity.ReturnValueInfo Connect()
        {
            ReturnValueInfo rvInfo = new ReturnValueInfo();
            try
            {
                InitComPort();

                _mscom4ULine.PortOpen = true;

                AdjustMachineTimer("");

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
        /// 关闭设备连接
        /// </summary>
        /// <returns></returns>
        public override bool Disconnect()
        {
            try
            {
                if (this._mscom4ULine.PortOpen)
                {
                    this._mscom4ULine.PortOpen = false;
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

        #region 终端基础信息专用指令

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
                mscom4ULine.Output = data;
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

        public void GetReaderClock(int machineID)
        {
            GetReaderClockPrivate(machineID);
            GetReaderClockPrivate(machineID);
        }

        private void GetReaderClockPrivate(int machineID)
        {
            this._mscom4ULine.RThreshold = 15;

            byte[] data = { 0xC0, 0x00, 0xAF, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "AF";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 终端重新复位（需停止读数后方可使用）
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void ResetTerminal(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xB0, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "B0";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 终端进入IAP模式（需停止读数后方可使用）
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void ChangeToModeIAP(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xB1, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "B1";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 读标签指定块安全标记（需停止读数后方可使用）
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="startPeaceNum">1字节起始块号</param>
        /// <param name="peaceAmount">1字节指定块数量</param>
        public void GetSecurityTokensOfDesignatedPiece(int machineID, int startPeaceNum, int peaceAmount)
        {
            byte[] data = { 0xC0, 0x00, 0xB3, 0x02, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte[] byStartPeaceNum = BitConverter.GetBytes(startPeaceNum);
            data[4] = byStartPeaceNum[0];
            byte[] byPeaceAmount = BitConverter.GetBytes(peaceAmount);
            data[5] = byPeaceAmount[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[6] = lowCRC;
            data[7] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "B3";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置阅读器主参数（需停止读数后方可使用）
        /// </summary>
        /// <param name="entity">主参数实体</param>
        public void SetReaderMainParams(int machineID, ULineHFReaderEntity entity)
        {
            if (entity == null)
            {
                return;
            }

            this._mscom4ULine.RThreshold = 20;
            //FFFFFFFF0100FFFF0204030AFF010200000201FF00FFFFFF
            //byte[] data = { 0xC0, 0x00, 0xBA, 0x1A, 
            //                  0xFF, 0xFF,0xFF, 0xFF,0x00, 0x00,0xFF, 0xFF,0x00, 0x00,0x00, 0x00,0x00,0x00, 0x00,0x00, 0x00,0x00, 0x00,0x00, 0x00,0x00, 0x00,0x00,
            //                  0x00, 0x00, 0xC1 };

            byte[] data = { 0xC0, 0x01, 0xBA, 0x1A, 
                              0xFF, 0xFF,0xFF, 0xFF,0x06, 0x00,0xFF, 0xFF,0x02, 0x04,0x03, 0x0A,0xFF,0x01, 0x02,0x00, 0x00,0x02, 0x01,0xFF, 0x00,0xFF, 0xFF,0xFF,
                              0x00, 0x00, 0xC1 };

            //byte[] byMacID = BitConverter.GetBytes(machineID);
            //data[1] = byMacID[0];

            //if (!string.IsNullOrEmpty(entity.ID) && entity.ID.Length == 8)
            //{
            //    byte[] byIDFirst = BitConverter.GetBytes(Convert.ToInt32(entity.ID.Substring(0, 2), 16));
            //    data[4] = byIDFirst[0];

            //    byte[] byIDSecond = BitConverter.GetBytes(Convert.ToInt32(entity.ID.Substring(2, 2), 16));
            //    data[5] = byIDSecond[0];

            //    byte[] byIDThirld = BitConverter.GetBytes(Convert.ToInt32(entity.ID.Substring(4, 2), 16));
            //    data[6] = byIDThirld[0];

            //    byte[] byIDFourth = BitConverter.GetBytes(Convert.ToInt32(entity.ID.Substring(6, 2), 16));
            //    data[7] = byIDFourth[0];
            //}

            //if (!string.IsNullOrEmpty(entity.MachineID))
            //{
            //    byte[] byMacIDNew = BitConverter.GetBytes(int.Parse(entity.MachineID));
            //    data[8] = byMacIDNew[0];
            //}
            //else
            //{
            //    data[8] = byMacID[0];
            //}

            //if (entity.AutomaticUploadingMode)
            //{
            //    data[9] = BitConverter.GetBytes(1)[0];
            //}

            //if (!string.IsNullOrEmpty(entity.ReadCardMode) && entity.ReadCardMode.Length == 2)
            //{
            //    byte[] byReadCardMode = BitConverter.GetBytes(Convert.ToInt32(entity.ReadCardMode, 16));
            //    data[10] = byReadCardMode[0];
            //}

            //if (!string.IsNullOrEmpty(entity.DesignatedPieceNum) && entity.DesignatedPieceNum.Length == 2)
            //{
            //    byte[] byDesignatedPieceNum = BitConverter.GetBytes(Convert.ToInt32(entity.DesignatedPieceNum, 16));
            //    data[11] = byDesignatedPieceNum[0];
            //}

            //if (!string.IsNullOrEmpty(entity.CardType) && entity.CardType.Length == 2)
            //{
            //    byte[] byCardType = BitConverter.GetBytes(Convert.ToInt32(entity.CardType, 16));
            //    data[12] = byCardType[0];
            //}

            //if (!string.IsNullOrEmpty(entity.PeaceLength) && entity.PeaceLength.Length == 2)
            //{
            //    byte[] byPeaceLength = BitConverter.GetBytes(Convert.ToInt32(entity.PeaceLength, 16));
            //    data[13] = byPeaceLength[0];
            //}

            //if (!string.IsNullOrEmpty(entity.RecordSaveFrequency) && entity.RecordSaveFrequency.Length == 2)
            //{
            //    byte[] byRecordSaveFrequency = BitConverter.GetBytes(Convert.ToInt32(entity.RecordSaveFrequency, 16));
            //    data[14] = byRecordSaveFrequency[0];
            //}

            //if (!string.IsNullOrEmpty(entity.ReadTimeInterval) && entity.ReadTimeInterval.Length == 2)
            //{
            //    byte[] byReadTimeInterval = BitConverter.GetBytes(Convert.ToInt32(entity.ReadTimeInterval, 16));
            //    data[15] = byReadTimeInterval[0];
            //}

            //if (!string.IsNullOrEmpty(entity.HornSwitch) && entity.HornSwitch.Length == 2)
            //{
            //    byte[] byHornSwitch = BitConverter.GetBytes(Convert.ToInt32(entity.HornSwitch, 16));
            //    data[16] = byHornSwitch[0];
            //}

            //if (!string.IsNullOrEmpty(entity.HornSoundInterval) && entity.HornSoundInterval.Length == 2)
            //{
            //    byte[] byHornSoundInterval = BitConverter.GetBytes(Convert.ToInt32(entity.HornSoundInterval, 16));
            //    data[17] = byHornSoundInterval[0];
            //}

            //if (!string.IsNullOrEmpty(entity.HornSountTimes) && entity.HornSountTimes.Length == 2)
            //{
            //    byte[] byHornSountTimes = BitConverter.GetBytes(Convert.ToInt32(entity.HornSountTimes, 16));
            //    data[18] = byHornSountTimes[0];
            //}

            //if (!string.IsNullOrEmpty(entity.ParamsLED) && entity.ParamsLED.Length == 8)
            //{
            //    byte[] byParamsLEDFirst = BitConverter.GetBytes(Convert.ToInt32(entity.ParamsLED.Substring(0, 2), 16));
            //    data[19] = byParamsLEDFirst[0];

            //    byte[] byParamsLEDSecond = BitConverter.GetBytes(Convert.ToInt32(entity.ParamsLED.Substring(2, 2), 16));
            //    data[20] = byParamsLEDSecond[0];

            //    byte[] byParamsLEDThird = BitConverter.GetBytes(Convert.ToInt32(entity.ParamsLED.Substring(4, 2), 16));
            //    data[21] = byParamsLEDThird[0];

            //    byte[] byParamsLEDFourth = BitConverter.GetBytes(Convert.ToInt32(entity.ParamsLED.Substring(6, 2), 16));
            //    data[22] = byParamsLEDFourth[0];
            //}

            //if (!string.IsNullOrEmpty(entity.RfPower) && entity.RfPower.Length == 2)
            //{
            //    byte[] byRfPower = BitConverter.GetBytes(Convert.ToInt32(entity.RfPower, 16));
            //    data[23] = byRfPower[0];
            //}

            //if (!string.IsNullOrEmpty(entity.SaveCycling) && entity.SaveCycling.Length == 2)
            //{
            //    byte[] bySaveCycling = BitConverter.GetBytes(Convert.ToInt32(entity.SaveCycling, 16));
            //    data[24] = bySaveCycling[0];
            //}

            //if (!string.IsNullOrEmpty(entity.ParamsHold) && entity.ParamsHold.Length == 8)
            //{
            //    byte[] byParamsHoldFirst = BitConverter.GetBytes(Convert.ToInt32(entity.ParamsHold.Substring(0, 2), 16));
            //    data[25] = byParamsHoldFirst[0];

            //    byte[] byParamsHoldSecond = BitConverter.GetBytes(Convert.ToInt32(entity.ParamsHold.Substring(2, 2), 16));
            //    data[26] = byParamsHoldSecond[0];

            //    byte[] byParamsHoldThird = BitConverter.GetBytes(Convert.ToInt32(entity.ParamsHold.Substring(4, 2), 16));
            //    data[27] = byParamsHoldThird[0];
            //}

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[28] = lowCRC;
            data[29] = highCRC;
        


            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "BA";
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
        public void GetReaderMainParams(int machineID)
        {
            //需要两次调用该方法来激活COM口抛出一条完整的指令条。
            GetReaderMainParamsPrivate(machineID);
            

            //GetReaderMainParamsPrivate(machineID);
        }

        private void GetReaderMainParamsPrivate(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xBF, 0x00, 0x00, 0x00, 0xC1 };

            //_mscom4ULine.RThreshold = 50;

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
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
                mscom4ULine.Output = data;
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
                mscom4ULine.Output = data;
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
                mscom4ULine.Output = data;
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

            this._mscom4ULine.RThreshold = 20;

            byte[] byMacID = BitConverter.GetBytes(machineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
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
                mscom4ULine.Output = data;
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
                mscom4ULine.Output = data;
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
                mscom4ULine.Output = data;
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
        /// 设置上传模式
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="isAutomicUploadind">是否为自动上传模式，true为自动，false为被动</param>
        public void SetUploadingMode(int machineID, bool isAutomicUploadind)
        {
            byte[] data = { 0xC0, 0x00, 0xD0, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            if (isAutomicUploadind)
            {
                data[4] = BitConverter.GetBytes(1)[0];
            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "D0";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置读卡参数
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="cardReadingMode">读卡模式，十六进制数，长度为2</param>
        /// <param name="readPeaceNum">需要读取的块号，十六进制数，长度为2</param>
        public void SetCardReadingParams(int machineID, string cardReadingMode, string readPeaceNum)
        {
            byte[] data = { 0xC0, 0x00, 0xD1, 0x02, 0xFF, 0xFF, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {

                if (!string.IsNullOrEmpty(cardReadingMode) && cardReadingMode.Length == 2)
                {
                    byte[] byCardReadingMode = BitConverter.GetBytes(Convert.ToInt32(cardReadingMode, 16));
                    data[4] = byCardReadingMode[0];
                }

                if (!string.IsNullOrEmpty(readPeaceNum) && readPeaceNum.Length == 2)
                {
                    byte[] byReadPeaceNum = BitConverter.GetBytes(Convert.ToInt32(readPeaceNum, 16));
                    data[5] = byReadPeaceNum[0];
                }

            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[6] = lowCRC;
            data[7] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "D1";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置读取标签类型
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="tagType">标签类型，01表示A类，02表示B类</param>
        /// <param name="tagPeaceLength">标签块长度，04表示每块4字节，08表示每块8字节</param>
        public void SetTagTypeReadingParams(int machineID, string tagType, string tagPeaceLength)
        {
            byte[] data = { 0xC0, 0x00, 0xD2, 0x02, 0x02, 0x04, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {

                if (!string.IsNullOrEmpty(tagType) && tagType.Length == 2)
                {
                    byte[] byCardReadingMode = BitConverter.GetBytes(int.Parse(tagType));
                    data[4] = byCardReadingMode[0];
                }

                if (!string.IsNullOrEmpty(tagPeaceLength) && tagPeaceLength.Length == 2)
                {
                    byte[] byReadPeaceNum = BitConverter.GetBytes(int.Parse(tagPeaceLength));
                    data[5] = byReadPeaceNum[0];
                }

            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[6] = lowCRC;
            data[7] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "D2";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置记录保存时间
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="timeInterval">时间参数，十六进制，取值范围 00-FF</param>
        public void SetRecordSavingInterval(int machineID, string timeInterval)
        {
            byte[] data = { 0xC0, 0x00, 0xD3, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {
                if (!string.IsNullOrEmpty(timeInterval) && timeInterval.Length == 2)
                {
                    byte[] bytimeInterval = BitConverter.GetBytes(Convert.ToInt32(timeInterval, 16));
                    data[4] = bytimeInterval[0];
                }
            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "D3";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置主动读卡状态下两次读卡指令之间的时间间隔 时间间隔单位为50MS，仅当读卡模式为主动读卡有效。
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="timeInterval">时间参数,十六进制，取值范围 00-FF</param>
        public void SetCardReadingSavingInterval(int machineID, string timeInterval)
        {
            byte[] data = { 0xC0, 0x00, 0xD4, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {
                if (!string.IsNullOrEmpty(timeInterval) && timeInterval.Length == 2)
                {
                    byte[] bytimeInterval = BitConverter.GetBytes(Convert.ToInt32(timeInterval, 16));
                    data[4] = bytimeInterval[0];
                }
            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "D4";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置写卡指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="strCardWritringCmd">指令字符串，长度只能为12或者20，其他长度则会被忽略</param>
        public void SetCommmandCardWriting(int machineID, string strCardWritringCmd)
        {
            byte[] data = new byte[12];
            byte[] dataFor6 = { 0xC0, 0x00, 0xD5, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };
            byte[] dataForA = { 0xC0, 0x00, 0xD5, 0x0A, 
                                  0x00, 0x00, 0x00, 0x00,0x00, 0x00,0x00, 0x00,0x00, 0x00,
                                  0x00, 0x00, 0xC1 };

            if (!string.IsNullOrEmpty(strCardWritringCmd) && strCardWritringCmd.Length == 12)
            {
                data = dataFor6;
            }
            else if (!string.IsNullOrEmpty(strCardWritringCmd) && strCardWritringCmd.Length == 20)
            {
                data = dataForA;
            }
            else
            {
                return;
            }

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {
                for (int i = 0; i < strCardWritringCmd.Length / 2; i++)
                {
                    byte[] byComm = BitConverter.GetBytes(Convert.ToInt32(strCardWritringCmd.Substring(i * 2, 2), 16));
                    data[i + 4] = byComm[0];
                }
            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "D5";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 读取标签的UID命令
        /// </summary>
        /// <param name="machineID"></param>

        public void SetCommandReadUidOfTags(int machineID)
        {
            SetCommandReadUidOfTagsPrivate(machineID);
            SetCommandReadUidOfTagsPrivate(machineID);
        }


        private void SetCommandReadUidOfTagsPrivate(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xD6, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4ULine.RThreshold = 10;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "D6";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 清显示屏
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void SetCommandMonitorClearing(int machineID)
        {

            byte[] data = { 0xC0, 0x00, 0xD7, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "D7";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置显示器类型指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="strLEDCmd">显示器类型参数，00表示数码管显示，任何非零值表示为液晶显示器</param>
        public void SetCommandLedTypeWriting(int machineID, string strLEDCmd)
        {
            byte[] data = { 0xC0, 0x00, 0xD8, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {
                if (!string.IsNullOrEmpty(strLEDCmd) && strLEDCmd.Length == 2)
                {
                    byte[] byLEDCmd = BitConverter.GetBytes(Convert.ToInt32(strLEDCmd, 16));
                    data[4] = byLEDCmd[0];
                }
            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "D8";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置读卡指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="strCardReadingCmd">读卡指令参数，参数分别表示要读取的数据块,0XFF表示不读取数据块，读取数据直接上传不保留在FLASH中</param>

        public void SetCommandCardReading(int machineID, string strCardReadingCmd)
        {
            for (int i = 0; i < 16; i++)
            {
                SetCommandCardReadingPrivate(machineID, strCardReadingCmd); 
            }
        }

        private void SetCommandCardReadingPrivate(int machineID, string strCardReadingCmd)
        {
            this._mscom4ULine.RThreshold = 15;

            byte[] data = { 0xC0, 0x00, 0xDA, 0x01, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {
                if (!string.IsNullOrEmpty(strCardReadingCmd) && strCardReadingCmd.Length == 2)
                {
                    byte[] byCardReading = BitConverter.GetBytes(Convert.ToInt32(strCardReadingCmd, 16));
                    data[4] = byCardReading[0];
                }
            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "DA";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置喇叭控制指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="strHornControllingCmd">第一字节鸣叫方式参数。0xFF为默认值：读到卡后鸣叫，0x00：读到卡后不鸣叫，0xAA马上鸣叫；第二字节鸣叫次数参数：每次蜂鸣器鸣叫时重复次数；第三字节鸣叫持续时间参数：蜂鸣器打开的持续时间，以50ms为单位；默认配置参数为：000102；鸣叫方式为AA时参数单次有效不保存进EEPROM主参数中。</param>
        public void SetCommandHornControlling(int machineID, string strHornControllingCmd)
        {
            byte[] data = { 0xC0, 0x00, 0xDB, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {
                if (!string.IsNullOrEmpty(strHornControllingCmd) && strHornControllingCmd.Length == 6)
                {
                    for (int i = 0; i < strHornControllingCmd.Length / 2; i++)
                    {
                        byte[] byHornControlling = BitConverter.GetBytes(Convert.ToInt32(strHornControllingCmd.Substring(i * 2, 2), 16));
                        data[i + 4] = byHornControlling[0];
                    }
                }
            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "DB";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 恢复默认参数设置指令
        /// </summary>
        /// <param name="machineID">机器号</param>
        public void ResetDefaultParamsSetting(int machineID)
        {
            byte[] data = { 0xC0, 0x00, 0xDC, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "DC";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 发送显示内容至LCD
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="strDisplayContext">显示内容</param>
        public void SendDisplayContext(int machineID, string strDisplayContext)
        {
            byte[] data = new byte[12];
            byte[] dataFor6 = { 0xC0, 0x00, 0xDD, 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };
            byte[] dataForA = { 0xC0, 0x00, 0xDD, 0x0A, 
                                  0x00, 0x00, 0x00, 0x00,0x00, 0x00,0x00, 0x00,0x00, 0x00,
                                  0x00, 0x00, 0xC1 };

            if (!string.IsNullOrEmpty(strDisplayContext) && strDisplayContext.Length == 12)
            {
                data = dataFor6;
            }
            else if (!string.IsNullOrEmpty(strDisplayContext) && strDisplayContext.Length == 20)
            {
                data = dataForA;
            }
            else
            {
                return;
            }

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {
                for (int i = 0; i < strDisplayContext.Length / 2; i++)
                {
                    byte[] byComm = BitConverter.GetBytes(Convert.ToInt32(strDisplayContext.Substring(i * 2, 2), 16));
                    data[i + 4] = byComm[0];
                }
            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "DD";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 设置射频功率
        /// </summary>
        /// <param name="machineID">机器号</param>
        /// <param name="strRfRate">射频功率，十六进制，00表示按照默认功率发射，FF表示按照最大功率发射</param>
        public void SetCommandRfRateControlling(int machineID, string strRfRate)
        {
            byte[] data = { 0xC0, 0x00, 0xDF, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            try
            {
                if (!string.IsNullOrEmpty(strRfRate) && strRfRate.Length == 2)
                {
                    byte[] byRfRate = BitConverter.GetBytes(Convert.ToInt32(strRfRate, 16));
                    data[4] = byRfRate[0];
                }
            }
            catch (Exception)
            {

            }

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[5] = lowCRC;
            data[6] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "DF";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region 基础指令

        /// <summary>
        /// 设置终端设备号，设置时需要单独连接一台设备。
        /// </summary>
        /// <param name="machineID">机器号，1字节</param>
        /// <param name="ID">ID号，4字节</param>
        public void SetEquipmentNum(string machineID, string ID)
        {
            byte[] data = { 0xC0, 0xFF, 0xE0, 0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xC1 };

            if (!string.IsNullOrEmpty(machineID) && !string.IsNullOrEmpty(ID))
            {
                if (machineID.Length == 2 && ID.Length == 8)
                {
                    for (int i = 0; i < ID.Length / 2; i++)
                    {
                        byte[] byID = BitConverter.GetBytes(Convert.ToInt32(ID.Substring(i * 2, 16), 16));
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
                mscom4ULine.Output = data;
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

        public void GetEquipmentNum()
        {
            GetEquipmentNumPrivate();
            GetEquipmentNumPrivate();
        }

        private void GetEquipmentNumPrivate()
        {
            this._mscom4ULine.RThreshold = 20;

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
                mscom4ULine.Output = data;
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
                mscom4ULine.Output = data;
                this._IsUsingCmd = "E2";
                Application.DoEvents();
            }
            catch (Exception ex)
            {

                throw ex;
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

            this._mscom4ULine.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
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
            //GetProgramVersionPrivate(MachineID);
        }

        void GetProgramVersionPrivate(int MachineID)
        {
            byte[] data = { 0xC0, 0x00, 0xE5, 0x00, 0x00, 0x00, 0xC1 };

            this._mscom4ULine.RThreshold = 15;

            byte[] byMacID = BitConverter.GetBytes(MachineID);
            data[1] = byMacID[0];

            byte lowCRC;
            byte highCRC;
            ReaderGeneral.CRC16_CCITT(data, out highCRC, out lowCRC);
            data[4] = lowCRC;
            data[5] = highCRC;

            try
            {
                mscom4ULine.Output = data;
                this._IsUsingCmd = "E5";
                Application.DoEvents();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        /// <summary>
        /// 将字符串数据转换成实体
        /// </summary>
        /// <param name="strEntity"></param>
        /// <returns></returns>
        private TagInformationInfo GetEntity(string strEntity)
        {
            TagInformationInfo tagInfo = new TagInformationInfo();

            try
            {
                //机器号
                string strMacID = strEntity.Substring(2, 2);
                tagInfo.MachineID = Convert.ToInt32(strMacID, 16).ToString();
                //状态
                string strState = strEntity.Substring(4, 2);
                if (strState == "00")
                {
                    tagInfo.IsRemark = false;
                }
                else if (strState == "01")
                {
                    tagInfo.IsRemark = true;
                }
                //流水号
                tagInfo.TabNum = strEntity.Substring(8, 4);
                //标签号
                tagInfo.TagID = strEntity.Substring(12, 16);
                try
                {
                    //开始读到时间
                    string strFirstTime = ReaderGeneral.ChangetoDatebyHexstring(strEntity.Substring(48, 12));
                    tagInfo.TagExistFirstDatetime = DateTime.Parse(strFirstTime);
                }
                catch (Exception Ex)
                {

                    return null;
                }

                try
                {
                    //最后读到时间
                    string strLastTime = tagInfo.TagExistFirstDatetime.ToShortDateString() + " " + ReaderGeneral.ChangetoDatebyHexstring(strEntity.Substring(62, 6));
                    tagInfo.ReadDatetime = DateTime.Parse(strLastTime);
                }
                catch (Exception Ex)
                {

                    return null;
                }
            }
            catch (Exception Ex)
            {

                return null;
            }

            return tagInfo;
        }

        /// <summary>
        /// 发布标签读取结果
        /// </summary>
        /// <param name="eventArgs"></param>
        protected void OnTagResult(TagInfoResultEventArgs eventArgs)
        {
            if (this.TagInformationResultShowing != null)
            {
                this.TagInformationResultShowing(this, eventArgs);
            }
        }

        /// <summary>
        /// 发布设备主参数
        /// </summary>
        /// <param name="entity"></param>
        public void OnGettingEquipMainParams(ULineHFReaderEntity entity)
        {
            if (this.EquipmentMainParamsShowing != null)
            {
                this.EquipmentMainParamsShowing(this, entity);
            }
        }
        /// <summary>
        /// 发布时钟

        /// </summary>
        /// <param name="entity"></param>
        public void OnGettingClock(ULineClock entity)
        {
            if (this.ClockShowing != null)
            {
                this.ClockShowing(this, entity);
            }
        }

        /// <summary>
        /// 发布读写器记录总数

        /// </summary>
        /// <param name="entity"></param>
        public void OnGettingRecordsAmount(ULineRecordsAmount entity)
        {
            if (this.RecordsAmount != null)
            {
                this.RecordsAmount(this, entity);
            }
        }
        /// <summary>
        /// 发布UID命令

        /// </summary>
        /// <param name="entity"></param>
        public void OnSetCommandReadUid(ULineUID entity)
        {
            if (this.SetCommandReadUid != null)
            {
                this.SetCommandReadUid(this, entity);
            }
        }

        /// <summary>
        /// 发布读卡命令

        /// </summary>
        /// <param name="entity"></param>
        public void OnSetCommandCardRead(ULineSetCommandCardReading entity)
        {
            if (this.SetCommandCardRead != null)
            {
                this.SetCommandCardRead(this, entity);
            }
        }

        /// <summary>
        /// 发布终端机器号

        /// </summary>
        /// <param name="entity"></param>
        public void OnGettingEquipmentNum(ULineEquipmentNum entity)
        {
            if (this.EquipmentNum != null)
            {
                this.EquipmentNum(this, entity);
            }
        }

        /// <summary>
        /// 发布终端机器类型

        /// </summary>
        /// <param name="entity"></param>
        public void OnGettingEquipmentType(ULineEquipmentType entity)
        {
            if (this.EquipmentType != null)
            {
                this.EquipmentType(this, entity);
            }
        }
        /// <summary>
        /// 发布程序版本号

        /// </summary>
        /// <param name="entity"></param>
        protected void OnGettingEquipmentVersion(ULineEquipmentVersion entity)
        {
            if (this.EquipmentVersion != null)
            {
                this.EquipmentVersion(this, entity);
            }
        }
    }
}
