using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity.ULineHF
{
    /// <summary>
    /// 读取记录总数实体
    /// </summary>
    public class ULineRecordsAmount : EventArgs
    {
        public ULineRecordsAmount(string cmd)
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

                    this._noCollect = cmd.Substring(14, 2);

                    this._count = cmd.Substring(8, 16);
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
        /// 
        /// </summary>
        public string _count
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
        /// 未采集记录条数
        /// </summary>
        public string _noCollect
        { get; set; }
    }
}