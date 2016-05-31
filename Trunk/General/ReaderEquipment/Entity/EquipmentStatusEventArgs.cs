using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity
{
    /// <summary>
    /// 设备状态事件参数
    /// </summary>
    public class EquipmentStatusEventArgs : EventArgs
    {
        public EquipmentStatusEventArgs()
        {
            this.EquipmentStatus = EnumEquipmentStatus.Disconneted;
            this.RecordTime = DateTime.MinValue;
        }

        /// <summary>
        /// 设备机号
        /// </summary>
        public int MachineID { set; get; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public EnumEquipmentStatus EquipmentStatus { set; get; }
        /// <summary>
        /// 记录产生的时间
        /// </summary>
        public DateTime RecordTime { get; set; }
    }
}
