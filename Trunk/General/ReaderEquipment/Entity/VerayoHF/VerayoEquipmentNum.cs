using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity.VerayoHF
{
    /// <summary>
    /// 读终端设备号实体
    /// </summary>
    public class VerayoEquipmentNum : EventArgs
    {
        public VerayoEquipmentNum(string cmd)
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

                    this._device = cmd.Substring(8, 8);
                    this._terminal = cmd.Substring(16, 2);

                    this._Num = cmd.Substring(8, 10);
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
        /// 设备ID加机器机号
        /// </summary>
        public string _Num
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
        /// <summary>
        /// 设备ID
        /// </summary>
        public string _device
        { get; set; }
        /// <summary>
        /// 机器机号
        /// </summary>
        public string _terminal
        { get; set; }
    }
}
