using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity.VerayoHF
{
    /// <summary>
    /// 写读写器响应到标签指令
    /// </summary>
    public class VerayoReaderLable:EventArgs
    {
        public VerayoReaderLable(string cmd)
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
                    this._ReaderLable = cmd.Substring(8, 64);
                }
                catch (Exception)
                {
                    this._cmd = cmd.ToString();
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
        /// 32字节上位机产生的响应
        /// </summary>
        public string _ReaderLable
        {
            get;
            set;
        }
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