using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
    public class CheckOnAttendanceURreport_Info : Model.IModel.IModelObject
    {
        public CheckOnAttendanceURreport_Info()
        {
            this.Reader24GMonitor = string.Empty;
            this.PassType = string.Empty;
            this.MorningIn = string.Empty;
            this.MorningOut = string.Empty;
            this.NoonIn = string.Empty;
            this.NoonOut = string.Empty;
            this.NightIn = string.Empty;
            this.NightOut = string.Empty;
            this.Reader24GMonitorDataList = new List<Reader24GMonitorData_Info>();
        }

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 2.4G监控地点
        /// </summary>
        public string Reader24GMonitor { get; set; }

        /// <summary>
        /// 进出类型
        /// </summary>
        public string PassType { get; set; }

        /// <summary>
        /// 上午进校时段
        /// </summary>
        public string MorningIn { get; set; }

        /// <summary>
        /// 上午出校时段
        /// </summary>
        public string MorningOut { get; set; }

        /// <summary>
        /// 中午进校时段
        /// </summary>
        public string NoonIn { get; set; }

        /// <summary>
        /// 中午出校时段
        /// </summary>
        public string NoonOut { get; set; }

        /// <summary>
        /// 晚上进校时段
        /// </summary>
        public string NightIn { get; set; }

        /// <summary>
        /// 晚上出校时段
        /// </summary>
        public string NightOut { get; set; }

        /// <summary>
        /// 异常数据列表统计
        /// </summary>
        public List<Reader24GMonitorData_Info> Reader24GMonitorDataList { get; set; }


    }
}
