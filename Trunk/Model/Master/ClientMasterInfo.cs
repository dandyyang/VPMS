using System;
using Model.IModel;

namespace Model.Master
{
    public class ClientMasterInfo:IModelObject
    {
        public ClientMasterInfo()
        {
            this.RecordID = 0;
            this.ClmIRecID = 0;
            this.ClmCClientNum = string.Empty;
            this.ClmCEnglishName = string.Empty;
            this.ClmCChinaName = string.Empty;
            this.ClmCTaxNumber = string.Empty;
            this.ClmCLinkman = string.Empty;
            this.ClmCAddress = string.Empty;
            this.ClmCPhone = string.Empty;
            this.ClmCFax = string.Empty;
            this.ClmCWebSite = string.Empty;
            this.ClmCRemark = string.Empty;
            this.ClmCAdd = string.Empty;
            this.ClmCLast = string.Empty;

        }

        public int ClmIRecID
        {
            get;
            set;
        }
        
        public string ClmCClientNum
        {
            get;
            set;
        }
       
        public string ClmCEnglishName
        {
            get;
            set;
        }
        
        public string ClmCChinaName
        {
            get;
            set;
        }

        public string ClmCTaxNumber
        {
            get;
            set;
        }

        public string ClmCLinkman
        {
            get;
            set;
        }
        
        public string ClmCAddress
        {
            get;
            set;
        }
        
        public string ClmCPhone
        {
            get;
            set;
        }
        
        public string ClmCFax
        {
            get;
            set;
        }
        
        public string ClmCWebSite
        {
            get;
            set;
        }
        
        public string ClmCRemark
        {
            get;
            set;
        }
        
        public string ClmCAdd
        {
            get;
            set;
        }
        
        public DateTime? ClmDAddDate
        {
            get;
            set;
        }
        
        public string ClmCLast
        {
            get;
            set;
        }
        
        public DateTime? ClmDLastDate
        {
            get;
            set;
        }



        public int ClmCClientNum_Lenght
        {
            get;
            set;
        }

        public int ClmCEnglishName_Lenght
        {
            get;
            set;
        }

        public int ClmCChinaName_Lenght
        {
            get;
            set;
        }

        public int ClmCTaxNumber_Lenght
        {
            get;
            set;
        }

        public int ClmCLinkman_Lenght
        {
            get;
            set;
        }

        public int ClmCAddress_Lenght
        {
            get;
            set;
        }

        public int ClmCPhone_Lenght
        {
            get;
            set;
        }

        public int ClmCFax_Lenght
        {
            get;
            set;
        }

        public int ClmCWebSite_Lenght
        {
            get;
            set;
        }

        public int ClmCRemark_Lenght
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

    public enum ClientMasterInfoEnum
    {
        ClmIRecID,
        ClmCClientNum,
        ClmCEnglishName,
        ClmCChinaName,
        ClmCLinkman,
        ClmCAddress,
        ClmCPhone,
        ClmCFax,
        ClmCWebSite,
        ClmCRemark,
        ClmCAdd,
        ClmDAddDate,
        ClmCLast,
        ClmDLastDate
    }
}
