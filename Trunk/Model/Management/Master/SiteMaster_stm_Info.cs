using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Management.Master
{
    /// <summary>
    /// 地點主檔
    /// </summary>
    public class SiteMaster_stm_Info:IModelObject
    {
        public SiteMaster_stm_Info()
        {
            this.stm_iRecordID = 0;
            this.stm_cNumber = string.Empty;
            this.stm_cBuildingNumber = string.Empty;
            this.BuildingName = string.Empty;
            this.stm_cName = string.Empty;
            this.stm_cRemark = string.Empty;
            this.stm_cAdd = string.Empty;
            this.stm_cLast = string.Empty;
            this.stm_cName_Length = 0;
            this.stm_cNumber_Length = 0;
            this.stm_cRemark_Length = 0;
        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public int stm_iRecordID
        {
            set;
            get;
        }
        /// <summary>
        /// 編號
        /// </summary>
        public string stm_cNumber
        {
            set;
            get;
        }

        /// <summary>
        /// 建築物編號

        /// </summary>
        public string stm_cBuildingNumber
        {
            set;
            get;
        }

        /// <summary>
        /// 建築物名稱

        /// </summary>
        public string BuildingName
        {
            set;
            get;
        }


        /// <summary>
        /// 名稱
        /// </summary>
        public string stm_cName
        {
            set;
            get;
        }
        /// <summary>
        /// 備注
        /// </summary>
        public string stm_cRemark
        {
            set;
            get;
        }
        /// <summary>
        /// 新增者

        /// </summary>
        public string stm_cAdd
        {
            set;
            get;
        }
        /// <summary>
        /// 新增时间
        /// </summary>
        public System.DateTime? stm_dAddDate
        {
            set;
            get;
        }
        /// <summary>
        /// 修改者

        /// </summary>
        public string stm_cLast
        {
            set;
            get;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime? stm_dLastDate
        {
            set;
            get;
        }


        /// <summary>
        /// 編號長度
        /// </summary>
        public int stm_cNumber_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 名稱長度
        /// </summary>
        public int stm_cName_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 備注長度
        /// </summary>
        public int stm_cRemark_Length
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
