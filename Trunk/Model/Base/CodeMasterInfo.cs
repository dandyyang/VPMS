using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Base
{
    public class CodeMasterInfo:IModelObject
    {   
        public CodeMasterInfo() 
        {
            this.RecordID = 0;
            this.CmtCAdd = string.Empty ;
            this.CmtCKey1 = string.Empty ;
            this.CmtCKey2 = string.Empty;
            this.CmtCLast = string.Empty;
            this.CmtCRemark = string.Empty;
            this.CmtCRemark2 = string.Empty;
            this.CmtCValue = string.Empty;
            this.CmtFNumber = 0;
            this.CmtIRecID = 0;
        }

        public long CmtIRecID
        {
            get;
            set;
        }

        public string CmtCKey1
        {
            get;
            set;
        }

        public string CmtCKey2
        {
            get;
            set;
        }

        public string CmtCValue
        {
            get;
            set;
        }

        public decimal CmtFNumber
        {
            get;
            set;
        }

        public string CmtCRemark
        {
            get;
            set;
        }

        public string CmtCRemark2
        {
            get;
            set;
        }

        public string CmtCAdd
        {
            get;
            set;
        }

        public DateTime? CmtDAddDate
        {
            get;
            set;
        }

        public string CmtCLast
        {
            get;
            set;
        }

        public DateTime? CmtDLastDate
        {
            get;
            set;
        }


        public int CmtCKey1_Lenght
        {
            get;
            set;
        }

        public int CmtCKey2_Lenght
        {
            get;
            set;
        }

        public int CmtCValue_Lenght
        {
            get;
            set;
        }

        public int CmtCRemark_Lenght
        {
            get;
            set;
        }

        public int CmtCRemark2_Lenght
        {
            get;
            set;
        }

        public int CmtCAdd_Lenght
        {
            get;
            set;
        }

        public int CmtCLast_Lenght
        {
            get;
            set;
        }


        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }

    public enum CodeMasterInfoEnum
    {
        CmtIRecID,
        CmtCKey1,
        CmtCKey2,
        CmtCValue,
        CmtFNumber,
        CmtCRemark,
        CmtCRemark2,
        CmtCAdd,
        CmtDAddDate,
        CmtCLast,
        CmtDLastDate,
        TableName
    }
}
