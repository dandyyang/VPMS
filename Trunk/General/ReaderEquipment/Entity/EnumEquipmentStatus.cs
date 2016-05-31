using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReaderEquipment.Entity
{
    public enum EnumEquipmentStatus
    {
        /// <summary>
        /// 已联接、读数正常
        /// </summary>
        Conneted,
        /// <summary>
        /// 已断开、无数据返回
        /// </summary>
        Disconneted,
        /// <summary>
        /// 批量数据异常
        /// </summary>
        BatchRecordException,
        /// <summary>
        /// 单条数据异常
        /// </summary>
        SigleRecordException,
        /// <summary>
        /// 机器时间异常
        /// </summary>
        TimeException
    }
}
