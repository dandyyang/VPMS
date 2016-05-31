using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Management.Master
{
    public class SpecialtyMasterCourse_smc_Info:IModelObject
    {
        public SpecialtyMasterCourse_smc_Info()
        {
            this.smc_iRecordID = 0;
            this.smc_iSpecialtyID = 0;
            this.smc_cSpecialtyNumber = string.Empty;
            this.smc_cCourseNumber = string.Empty;
            this.cum_cName = string.Empty;
        }

        /// <summary>
        /// 记录ID
        /// </summary>
        public int smc_iRecordID
        {
            set;
            get;
        }
        /// <summary>
        /// 專業记录ID
        /// </summary>
        public int smc_iSpecialtyID
        {
            set;
            get;
        }

        /// <summary>
        /// 專業編號
        /// </summary>
        public string smc_cSpecialtyNumber
        {
            set;
            get;
        }
        /// <summary>
        /// 課程編號
        /// </summary>
        public string smc_cCourseNumber
        {
            set;
            get;
        }
        /// <summary>
        /// 課程名

        /// </summary>
        public string cum_cName
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
