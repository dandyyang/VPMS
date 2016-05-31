using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity.ULineHF
{
    /// <summary>
    /// UlineHF读写器实体
    /// </summary>
    public class ULineHFReaderEntity : EventArgs
    {
        public ULineHFReaderEntity()
        {

        }

        public ULineHFReaderEntity(string cmd)
        {
            if (string.IsNullOrEmpty(cmd))
            {
                return;
            }
            else
            {
                try
                {
                    this._ID = cmd.Substring(8, 8);
                    this._MachineID = Convert.ToInt32(cmd.Substring(16, 2), 16).ToString();
                    this._AutomaticUploadingMode = cmd.Substring(18, 2) == "00" ? false : true;
                    this._ReadCardMode = cmd.Substring(20, 2);
                    this._DesignatedPieceNum = cmd.Substring(22, 2);
                    this._CardType = cmd.Substring(24, 2);
                    this._PeaceLength = cmd.Substring(26, 2);
                    this._RecordSaveFrequency = cmd.Substring(28, 2);
                    this._ReadTimeInterval = cmd.Substring(30, 2);
                    this._HornSwitch = cmd.Substring(32, 2);
                    this._HornSoundInterval = cmd.Substring(34, 2);
                    this.HornSountTimes = cmd.Substring(36, 2);
                    this._ParamsLED = cmd.Substring(38, 8);
                    this._RfPower = cmd.Substring(46, 2);
                    this._SaveCycling = cmd.Substring(48, 2);
                    this._ParamsHold = cmd.Substring(50, 6);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private string _ID;
        /// <summary>
        /// 设备ID，默认为FFFFFFFF
        /// </summary>
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _MachineID;
        /// <summary>
        /// 机器号
        /// </summary>
        public string MachineID
        {
            get { return _MachineID; }
            set { _MachineID = value; }
        }
        private bool _AutomaticUploadingMode = false;
        /// <summary>
        /// 自动上传模式，默认不上传
        /// </summary>
        public bool AutomaticUploadingMode
        {
            get { return _AutomaticUploadingMode; }
            set { _AutomaticUploadingMode = value; }
        }
        private string _ReadCardMode = "FF";
        /// <summary>
        /// 读卡模式
        /// </summary>
        public string ReadCardMode
        {
            get { return _ReadCardMode; }
            set { _ReadCardMode = value; }
        }
        private string _DesignatedPieceNum = "FF";
        /// <summary>
        /// 指定读卡块
        /// </summary>
        public string DesignatedPieceNum
        {
            get { return _DesignatedPieceNum; }
            set { _DesignatedPieceNum = value; }
        }
        private string _CardType = "02";
        /// <summary>
        /// 卡类型，01为A型卡(TI)，02为B型卡
        /// </summary>
        public string CardType
        {
            get { return _CardType; }
            set { _CardType = value; }
        }
        private string _PeaceLength = "04";
        /// <summary>
        /// 块长度
        /// </summary>
        public string PeaceLength
        {
            get { return _PeaceLength; }
            set { _PeaceLength = value; }
        }
        private string _RecordSaveFrequency = "01";
        /// <summary>
        /// 未读取到同一张卡而生成记录的时间间隔
        /// </summary>
        public string RecordSaveFrequency
        {
            get { return _RecordSaveFrequency; }
            set { _RecordSaveFrequency = value; }
        }
        private string _ReadTimeInterval = "0A";
        /// <summary>
        /// 读卡时间间隔，以50ms为单位
        /// </summary>
        public string ReadTimeInterval
        {
            get { return _ReadTimeInterval; }
            set { _ReadTimeInterval = value; }
        }
        private string _HornSwitch = "FF";
        /// <summary>
        /// 喇叭开关
        /// </summary>
        public string HornSwitch
        {
            get { return _HornSwitch; }
            set { _HornSwitch = value; }
        }
        private string _HornSoundInterval = "01";
        /// <summary>
        /// 间隔时间内喇叭鸣叫的次数
        /// </summary>
        public string HornSoundInterval
        {
            get { return _HornSoundInterval; }
            set { _HornSoundInterval = value; }
        }
        private string _HornSountTimes = "02";
        /// <summary>
        /// 鸣叫间隔时间，以50ms为单位
        /// </summary>
        public string HornSountTimes
        {
            get { return _HornSountTimes; }
            set { _HornSountTimes = value; }
        }
        private string _ParamsLED = "00000201";
        /// <summary>
        ///    LED等控制参数
        /// </summary>
        public string ParamsLED
        {
            get { return _ParamsLED; }
            set { _ParamsLED = value; }
        }
        private string _RfPower = "FF";
        /// <summary>
        /// 射频功率
        /// </summary>
        public string RfPower
        {
            get { return _RfPower; }
            set { _RfPower = value; }
        }
        private string _SaveCycling = "00";
        /// <summary>
        /// 循环保存
        /// </summary>
        public string SaveCycling
        {
            get { return _SaveCycling; }
            set { _SaveCycling = value; }
        }
        private string _ParamsHold = "FFFFFF";
        /// <summary>
        /// 保留参数
        /// </summary>
        public string ParamsHold
        {
            get { return _ParamsHold; }
            set { _ParamsHold = value; }
        }
    }
}
