using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity.ULineHF
{
    /// <summary>
    /// 读程序版本实体
    /// </summary>
    public class ULineEquipmentVersion : EventArgs
    {
        public ULineEquipmentVersion(string cmd)
        {
            if (string.IsNullOrEmpty(cmd))
            {
                return;
            }
            else
            {
                try
                {
                    this._cmd = cmd.ToString();

                    this._MachineID = cmd.Substring(2, 2);
                    this._status = cmd.Substring(4, 2);
                    this._dataLong = cmd.Substring(6, 2);

                    this._year = cmd.Substring(8, 2);
                    this._month = cmd.Substring(10, 2);
                    this._day = cmd.Substring(12, 2);
                    this._hour = cmd.Substring(14, 2);

                    this._Version = cmd.Substring(8, 8);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        /// <summary>
        /// 完成指令
        /// </summary>
        public string _cmd
        { get; set; }
        /// <summary>
        /// 机器号
        /// </summary>
        public string _MachineID
        {
            get;
            set;
        }
        /// <summary>
        /// 版本号(年、月、日、时)
        /// </summary>
        public string _Version
        {
            get;
            set;
        }
        /// <summary>
        /// 年
        /// </summary>
        public string _year
        { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public string _month
        { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        public string _day
        { get; set; }
        /// <summary>
        /// 时
        /// </summary>
        public string _hour
        { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string _status
        {
            get;
            set;
        }
        /// <summary>
        /// 数据长度
        /// </summary>
        public string _dataLong
        {
            get;
            set;
        }
    }
}
