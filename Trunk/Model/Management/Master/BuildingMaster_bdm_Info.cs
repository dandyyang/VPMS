using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Management.Master
{
    /// <summary>
    /// 建築物主檔


    /// </summary>
  public  class BuildingMaster_bdm_Info:IModel.IModelObject
    {
        public BuildingMaster_bdm_Info()
        {
            this.bdm_iRecordID = 0;
            this.bdm_cNumber = string.Empty;
            this.bdm_cName = string.Empty;
            this.bdm_cRemark = string.Empty;
            this.bdm_cAdd = string.Empty;
            this.bdm_cLast = string.Empty;
            this.bdm_cNumber_Length = 0;
            this.bdm_cName_Length = 0;
            this.bdm_cRemark_Length = 0;


        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public int bdm_iRecordID
        {
            set;
            get;
        }

        /// <summary>
        /// 編號
        /// </summary>
        public string bdm_cNumber
        {
            set;
            get;
        }

        /// <summary>
        /// 名稱
        /// </summary>
        public string bdm_cName
        {
            set;
            get;
        }

        /// <summary>
        /// 備注
        /// </summary>
        public string bdm_cRemark
        {
            set;
            get;
        }

        /// </summary>
        public string bdm_cAdd
        {
            set;
            get;
        }
        /// <summary>
        /// 新增时间
        /// </summary>
        public System.DateTime? bdm_dAddDate
        {
            set;
            get;
        }
        /// <summary>
        /// 修改者


        /// </summary>
        public string bdm_cLast
        {
            set;
            get;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime? bdm_dLastDate
        {
            set;
            get;
        }

        /// <summary>
        /// 編號長度
        /// </summary>
        public int bdm_cNumber_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 名稱長度
        /// </summary>
        public int bdm_cName_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 備注長度
        /// </summary>
        public int bdm_cRemark_Length
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
