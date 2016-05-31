using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Base
{
    public class UserInformationInfo
    {
        public UserInformationInfo()
        {
            this.UsmCAdd = string.Empty;
            this.UsmCChaName = string.Empty;
            this.UsmCDeptNum = string.Empty;
            this.UsmCEngName = string.Empty;
            this.UsmCLast = string.Empty;
            this.UsmCPasswork = string.Empty;
            this.UsmCRemark = string.Empty;
            this.UsmCUserID = string.Empty;
            this.UsmILock = false;
            this.UsmIRecID = 0;
        }

        public string UsmCAdd
        {
            get;
            set;
        }

        public string UsmCChaName
        {
            get;
            set;
        }

        public string UsmCDeptNum
        {
            get;
            set;
        }

        public string UsmCEngName
        {
            get;
            set;
        }

        public string UsmCLast
        {
            get;
            set;
        }

        public bool UsmILock
        {
            get;
            set;
        }

        public string UsmCPasswork
        {
            get;
            set;
        }

        public string UsmCRemark
        {
            get;
            set;
        }

        public string UsmCUserID
        {
            get;
            set;
        }

        public DateTime? UsmDAddDate
        {
            get;
            set;
        }

        public DateTime? UsmDLastDate
        {
            get;
            set;
        }

        public long UsmIRecID
        {
            get;
            set;
        }
        
    }

    public enum UserInformationInfoEnum
    {
        UsmCAdd,
        UsmCChaName,
        UsmCDeptNum,
        UsmCEngName,
        UsmCLast,
        UsmILock,
        UsmCPasswork,
        UsmCRemark,
        UsmCUserID,
        UsmDAddDate,
        UsmDLastDate,
        UsmIRecID,
        TableName

    }
}
