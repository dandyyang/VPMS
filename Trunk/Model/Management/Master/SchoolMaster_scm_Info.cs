using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Model.IModel;

namespace Model.Management.Master
{
    /// <summary>
    /// 院系部主檔

    /// </summary>
    public class SchoolMaster_scm_Info:IModelObject
    {
        public SchoolMaster_scm_Info()
        {
            this.scm_iRecordID = 0;
            this.scm_cNumber = string.Empty;
            this.scm_cName = string.Empty;
            this.scm_cRemark = string.Empty;
            this.scm_cAdd = string.Empty;
            this.scm_cLast = string.Empty;
            this.scm_cNumber_Length = 0;
            this.scm_cName_Length = 0;
            this.scm_cRemark_Length = 0;

        }
        /// <summary>
        /// 记录ID
        /// </summary>
        public int scm_iRecordID
        {
            set;
            get;
        }
        /// <summary>
        /// 編號
        /// </summary>
        public string scm_cNumber
        {
            set;
            get;
        }
        /// <summary>
        /// 名稱
        /// </summary>
        public string scm_cName
        {
            set;
            get;
        }
        /// <summary>
        /// 備注
        /// </summary>
        public string scm_cRemark
        {
            set;
            get;
        }
        /// <summary>
        /// 新增者

        /// </summary>
        public string scm_cAdd
        {
            set;
            get;
        }
        /// <summary>
        /// 新增时间
        /// </summary>
        public System.DateTime? scm_dAddDate
        {
            set;
            get;
        }
        /// <summary>
        /// 修改者

        /// </summary>
        public string scm_cLast
        {
            set;
            get;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime? scm_dLastDate
        {
            set;
            get;
        }

        /// <summary>
        /// 編號長度
        /// </summary>
        public int scm_cNumber_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 名稱長度
        /// </summary>
        public int scm_cName_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 備注長度
        /// </summary>
        public int scm_cRemark_Length
        {
            set;
            get;
        }

        #region IModelObject Members

        public int RecordID
        {
            set;
            get;
        }

        #endregion
    }
}
