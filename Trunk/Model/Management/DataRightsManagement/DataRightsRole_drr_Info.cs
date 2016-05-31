using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;
using Model.Management.DataRightsManagement;
using Model.SysMaster;

namespace Model.Management.DataRightsManagement
{
    public class DataRightsRole_drr_Info : IModelObject
    {
        public DataRightsRole_drr_Info()
        {
            drr_iRecordID = 0;
            drr_cNumber = string.Empty;
            drr_cName = string.Empty;
            drr_cRemark = string.Empty;
            drr_cAdd = string.Empty;
            drr_cLast = string.Empty;
            teacherList = new List<DataRightsRole_TeacherList_dtl_Info>();
            classList = new List<DataRightsRole_ClassList_dtc_Info>();
        }

        public int drr_iRecordID { set; get; }

        public string drr_cNumber { set; get; }

        public string drr_cName { set; get; }

        public string drr_cRemark { set; get; }

        public string drr_cAdd { set; get; }

        public DateTime? drr_dAddDate { set; get; }

        public string drr_cLast { set; get; }

        public DateTime? drr_dLastDate { set; get; }

        public List<DataRightsRole_TeacherList_dtl_Info> teacherList { set; get; }

        public List<DataRightsRole_ClassList_dtc_Info> classList { set; get; }

        /// <summary>
        ///该角色下所有用户

        /// </summary>
        public List<Sys_UserMaster_usm_Info> RoleUserList { set; get; }

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
