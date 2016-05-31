using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
    public class Reader24GMonitorData_Info
    {
        public Reader24GMonitorData_Info()
        {
            this.Monitor = string.Empty;
            this.UnusualRecordCount = 0;
            this.UnusualRecordPresent = 0;
        }

        /// <summary>
        /// 2.4监控地点
        /// </summary>
        public string Monitor { get; set; }

        /// <summary>
        /// 异常数据总数
        /// </summary>
        public int UnusualRecordCount { get; set; }

        /// <summary>
        /// 异常数据百份比
        /// </summary>
        public decimal UnusualRecordPresent { get; set; }
    }
}
