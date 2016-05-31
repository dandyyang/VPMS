using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity
{
    /// <summary>
    /// 机器故障异常事件参数
    /// </summary>
    public class ReaderExceptionEventArgs : EquipmentStatusEventArgs
    {
        /// <summary>
        /// 异常信息(状态正常时，为NULL值)
        /// </summary>
        public Exception ExceptionObject { set; get; }
    }
}
