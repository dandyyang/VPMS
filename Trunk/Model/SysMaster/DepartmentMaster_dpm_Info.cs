using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Model.IModel;

namespace Model.SysMaster
{
    /// <summary>
    /// 科室主檔
    /// </summary>
    [Serializable]
    [DataContract]
    public class DepartmentMaster_dpm_Info:IModelObject
    {
        public DepartmentMaster_dpm_Info()
        {
            this.dpm_iRecordID = 0;
            this.dpm_cNumber = string.Empty;
            this.dpm_cName = string.Empty;
            this.dpm_cRemark = string.Empty;
            this.dpm_cAdd = string.Empty;
            this.dpm_cLast = string.Empty;
            this.dpm_cName_Length = 0;
            this.dpm_cNumber_Length = 0;
            this.dpm_cRemark_Length = 0;
        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public int dpm_iRecordID
        {
            set;
            get;
        }
        /// <summary>
        /// 編號
        /// </summary>
        public string dpm_cNumber
        {
            set;
            get;
        }
        /// <summary>
        /// 名稱
        /// </summary>
        public string dpm_cName
        {
            set;
            get;
        }
        /// <summary>
        /// 備注
        /// </summary>
        public string dpm_cRemark
        {
            set;
            get;
        }
        /// <summary>
        /// 新增者
        /// </summary>
        public string dpm_cAdd
        {
            set;
            get;
        }
        /// <summary>
        /// 新增时间
        /// </summary>
        public System.DateTime? dpm_dAddDate
        {
            set;
            get;
        }
        /// <summary>
        /// 修改者
        /// </summary>
        public string dpm_cLast
        {
            set;
            get;
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public System.DateTime? dpm_dLastDate
        {
            set;
            get;
        }


        /// <summary>
        /// 編號長度
        /// </summary>
        public int dpm_cNumber_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 名稱長度
        /// </summary>
        public int dpm_cName_Length
        {
            set;
            get;
        }
        /// <summary>
        /// 備注長度
        /// </summary>
        public int dpm_cRemark_Length
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
