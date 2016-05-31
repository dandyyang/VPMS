using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Management.Master
{
    /// <summary>
    /// 專業主檔
    /// </summary>
    public class SpecialtyMaster_spm_Info:IModelObject
    {
        public SpecialtyMaster_spm_Info()
        {
            this.spm_iRecordID = 0;
            this.spm_cNumber = string.Empty;
            this.spm_cName = string.Empty;
            this.spm_cDesc = string.Empty;
            //this.stm_cName = string.Empty;
            this.spm_cRemark = string.Empty;
            this.spm_cAdd = string.Empty;
            this.spm_cLast = string.Empty;
            this.spm_cName_Length = 0;
            this.spm_cNumber_Length = 0;
            this.spm_cRemark_Length = 0;
        }
        /// <summary>
        /// 记录ID
        /// </summary>
        public int spm_iRecordID
        {
            set;
            get;
        }
        /// <summary>
        /// 編號
        /// </summary>
        public string spm_cNumber
        {
            set;
            get;
        }
        /// <summary>
        /// 名稱
        /// </summary>
        public string spm_cName
        {
            set;
            get;
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string spm_cDesc
        {
            set;
            get;
        }
        /// <summary>
        /// 備注
        /// </summary>
        public string spm_cRemark
        {
            set;
            get;
        }
        /// <summary>
        /// 新增者


        /// </summary>
        public string spm_cAdd
        {
            set;
            get;
        }
        /// <summary>
        /// 新增时间
        /// </summary>
        public System.DateTime? spm_dAddDate
        {
            set;
            get;
        }
        /// <summary>
        /// 修改者


        /// </summary>
        public string spm_cLast
        {
            set;
            get;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime? spm_dLastDate
        {
            set;
            get;
        }
        /// <summary>
        /// 
        /// </summary>

        /// <summary>
        /// 編號長度
        /// </summary>
        public int spm_cNumber_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 名稱長度
        /// </summary>
        public int spm_cName_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 備注長度
        /// </summary>
        public int spm_cRemark_Length
        {
            set;
            get;
        }

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
