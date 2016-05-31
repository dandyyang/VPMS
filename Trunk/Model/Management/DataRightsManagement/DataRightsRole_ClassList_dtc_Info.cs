using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Management.DataRightsManagement
{
    public class DataRightsRole_ClassList_dtc_Info : IModelObject
    {
        public DataRightsRole_ClassList_dtc_Info()
        {
            dtc_iRecordID = 0;
            dtc_cRoleNumber = string.Empty;
            dtc_cDataRightType = string.Empty;
            dtc_iIsAllRights = false;
            dtc_cSchoolNum = string.Empty;
            dtc_cSpecialtyNum = string.Empty;
            dtc_cGraduationPeriod = string.Empty;
            dtc_cClassNum = string.Empty;
        }

        public int dtc_iRecordID { set; get; }

        public string dtc_cRoleNumber { set; get; }

        public string dtc_cDataRightType { set; get; }

        public bool dtc_iIsAllRights { set; get; }

        public string dtc_cSchoolNum { set; get; }

        public string dtc_cSpecialtyNum { set; get; }

        public string dtc_cGraduationPeriod { set; get; }

        public string dtc_cClassNum { set; get; }

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
