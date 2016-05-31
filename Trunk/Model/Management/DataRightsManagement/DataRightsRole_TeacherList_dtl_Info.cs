using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Management.DataRightsManagement
{
    public class DataRightsRole_TeacherList_dtl_Info : IModelObject
    {

        public DataRightsRole_TeacherList_dtl_Info()
        {
            dtl_iRecordID = 0;
            dtl_cRoleNumber = string.Empty;
            dtl_cDataRightType = string.Empty;
            dtl_iIsAllRights = false;
            dtl_cTeacherNum = string.Empty;
            dtl_cTeacherName = string.Empty;
        }

        public int dtl_iRecordID { set; get; }

        public string dtl_cRoleNumber { set; get; }

        public string dtl_cDataRightType { set; get; }

        public bool dtl_iIsAllRights { set; get; }

        public string dtl_cTeacherNum { set; get; }

        public string dtl_cTeacherName { set; get; }

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
