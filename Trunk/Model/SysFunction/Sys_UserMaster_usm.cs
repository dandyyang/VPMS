using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.SysFunction
{
    public   class Sys_UserMaster_usm:IModelObject 
    {

        public Sys_UserMaster_usm()
        {
            this.usm_cAdd = string.Empty;
            this.usm_cChaName = string.Empty;
            this.usm_cEMail = string.Empty;
            this.usm_cLast = string.Empty;
            this.usm_cPasswork = string.Empty;
            this.usm_cRemark = string.Empty;
            this.usm_cUserLoginID = "Admin";
            this.usm_iLock = false;
          
            
        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public int usm_iRecordID
        { get; set; }

        /// <summary>
        /// 用戶登錄ID
        /// </summary>
        public string usm_cUserLoginID
        { get; set; }

        /// <summary>
        /// 用戶名稱
        /// </summary>
        public string usm_cChaName
        { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>
        public string usm_cPasswork
        { get; set; }

        /// <summary>
        /// 郵件地址
        /// </summary>
        public string usm_cEMail
        { get; set; }

        /// <summary>
        /// 鎖標記
        /// </summary>
        public bool usm_iLock
        { get; set; }

        /// <summary>
        /// 備注
        /// </summary>
        public string usm_cRemark
        { get; set; }

        public string usm_cAdd
        { get; set; }

        public DateTime? usm_dAddDate
        { get; set; }

        public string usm_cLast
        { get; set; }

        public DateTime? usm_dLastDate
        { get; set; }



        #region IModelObject Members

        public int RecordID
        {
            get   ;
            set    ;
        }

        #endregion
    }
}
