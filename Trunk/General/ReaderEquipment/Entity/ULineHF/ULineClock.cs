using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity.ULineHF
{
    /// <summary>
    /// 读取时钟实体
    /// </summary>
    public class ULineClock : EventArgs
    {
        public ULineClock()
        {

        }
        public ULineClock(string cmd)
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
                    this._minute = cmd.Substring(16, 2);
                    this._second = cmd.Substring(18, 2);
                    this._week = cmd.Substring(20, 2);

                    this._clock = cmd.Substring(8, 14);
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
        /// 年、月、日、时、分、秒、星期
        /// </summary>
        public string _clock
        {
            get;
            set;
        }
        #region MyRegion
        /// <summary>
        /// 年
        /// </summary>
        public string _year
        {
            get;
            set;
        }
        /// <summary>
        /// 月
        /// </summary>
        public string _month
        {
            get;
            set;
        }
        /// <summary>
        /// 日
        /// </summary>
        public string _day
        {
            get;
            set;
        }
        /// <summary>
        /// 时
        /// </summary>
        public string _hour
        {
            get;
            set;
        }
        /// <summary>
        /// 分
        /// </summary>
        public string _minute
        {
            get;
            set;
        }
        /// <summary>
        /// 秒
        /// </summary>
        public string _second
        {
            get;
            set;
        }
        /// <summary>
        /// 星期
        /// </summary>
        public string _week
        {
            get;
            set;
        }
        #endregion
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
        /// <summary>
        /// 机器号
        /// </summary>
        public string _MachineID
        {
            get;
            set;
        }
    }
}
