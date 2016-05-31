using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.SysFunction
{
    public class LogDetail_lgd_Info
    {
        public LogDetail_lgd_Info()
        {
            this.lgd_iID = 0;
            this.lgd_cIpAddr = "";
            this.lgd_cSysName = "";
            this.lgd_cClassMethodName = "";
            this.lgd_cLogType = "";
            this.lgd_cLogMessage = "";
            this.lgd_cRemark = "";
            this.lgd_cOperator = "";
            this.lgd_dOperateDateTime = DateTime.MinValue;
        }

        public int lgd_iID { get; set; }
        public string lgd_cIpAddr { get; set; }
        public string lgd_cSysName { get; set; }
        public string lgd_cClassMethodName { get; set; }
        public string lgd_cLogType { get; set; }
        public string lgd_cLogMessage { get; set; }
        public string lgd_cRemark { get; set; }
        public string lgd_cOperator { get; set; }
        public DateTime lgd_dOperateDateTime { get; set; }
    }
}