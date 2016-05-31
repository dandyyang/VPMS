using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.SysMaster
{
    public class Sys_RoleMaster_rlm_Info:IModelObject
    {

        #region 构造函数
        public Sys_RoleMaster_rlm_Info()
        {

            rlm_iRecordID = 0;

            rlm_cRoleID = string.Empty;

            rlm_cRoleDesc = string.Empty;

            rlm_cAdd = string.Empty;

            rlm_cLast = string.Empty;

            this.userMasterList = new List<Sys_UserMaster_usm_Info>();

            this.functionMasterList = new List<Sys_FunctionMaster_fum_Info>();

        }
        #endregion

        #region 析构函数
        ~Sys_RoleMaster_rlm_Info()
        {

        }
        #endregion

        #region 属性

        public Int32 rlm_iRecordID { set; get; }

        public string rlm_cRoleID { set; get; }

        public string rlm_cRoleDesc { set; get; }

        public string rlm_cAdd { set; get; }

        public DateTime? rlm_dAddDate { set; get; }

        public string rlm_cLast { set; get; }

        public DateTime? rlm_dLastDate { set; get; }

        public List<Sys_UserMaster_usm_Info> userMasterList
        {
            set;
            get;
        }
        public List<Sys_FunctionMaster_fum_Info> functionMasterList
        {
            set;
            get;
        }

        #endregion

        #region 长度属性

        public int rlm_iRecordID_Length { set; get; }

        public int rlm_cRoleID_Length { set; get; }

        public int rlm_cRoleDesc_Length { set; get; }

        public int rlm_cAdd_Length { set; get; }

        public int rlm_dAddDate_Length { set; get; }

        public int rlm_cLast_Length { set; get; }

        public int rlm_dLastDate_Length { set; get; }
        #endregion

        //#region 属性名称

        //public string rlm_iRecordID_Name { get { return "rlm_iRecordID"; } }

        //public string rlm_cRoleID_Name { get { return "rlm_cRoleID"; } }

        //public string rlm_cRoleDesc_Name { get { return "rlm_cRoleDesc"; } }

        //public string rlm_cAdd_Name { get { return "rlm_cAdd"; } }

        //public string rlm_dAddDate_Name { get { return "rlm_dAddDate"; } }

        //public string rlm_cLast_Name { get { return "rlm_cLast"; } }

        //public string rlm_dLastDate_Name { get { return "rlm_dLastDate"; } }
        //#endregion

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}