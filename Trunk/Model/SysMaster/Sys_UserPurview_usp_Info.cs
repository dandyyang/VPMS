using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.SysMaster
{
    public class Sys_UserPurview_usp_Info:IModelObject
    {

        #region 构造函数
        public Sys_UserPurview_usp_Info()
        {

            usp_iRecordID = 0;

            usp_cUserLoginID = string.Empty;

            usp_cRoleID = string.Empty;

            usp_cPurviewCode = string.Empty;

            usp_cAdd = string.Empty;

            usp_cLast = string.Empty;

            this.roleMasterList = new List<Sys_RoleMaster_rlm_Info>();

            this.userMasterList = new List<Sys_UserMaster_usm_Info>();

            this.formMasterList = new List<Sys_FormMaster_fom_Info>();

            this.functionMasterList = new List<Sys_FunctionMaster_fum_Info>();

            this.formPurviewList = new List<Sys_FormPurview_frp_Info>();

        }
        #endregion

        #region 析构函数
        ~Sys_UserPurview_usp_Info()
        {

        }
        #endregion

        #region 属性

        public Int32 usp_iRecordID { set; get; }

        public string usp_cUserLoginID { set; get; }

        public string usp_cRoleID { set; get; }

        public string usp_cPurviewCode { set; get; }

        public string usp_cAdd { set; get; }

        public DateTime? usp_dAddDate { set; get; }

        public string usp_cLast { set; get; }

        public DateTime? usp_dLastDate { set; get; }

        public List<Sys_RoleMaster_rlm_Info> roleMasterList
        {
            set;
            get;
        }

        public List<Sys_UserMaster_usm_Info> userMasterList
        {
            set;
            get;
        }

        public List<Sys_FormMaster_fom_Info> formMasterList
        {
            set;
            get;
        }

        public List<Sys_FunctionMaster_fum_Info> functionMasterList
        {
            set;
            get;
        }

        public List<Sys_FormPurview_frp_Info> formPurviewList
        {
            set;
            get;
        }
        #endregion

        #region 长度属性

        public int usp_iRecordID_Length { set; get; }

        public int usp_cUserLoginID_Length { set; get; }

        public int usp_cRoleID_Length { set; get; }

        public int usp_cPurviewCode_Length { set; get; }

        public int usp_cAdd_Length { set; get; }

        public int usp_dAddDate_Length { set; get; }

        public int usp_cLast_Length { set; get; }

        public int usp_dLastDate_Length { set; get; }
        #endregion

        //#region 属性名称

        //public string usp_iRecordID_Name { get { return "usp_iRecordID"; } }

        //public string usp_cUserLoginID_Name { get { return "usp_cUserLoginID"; } }

        //public string usp_cRoleID_Name { get { return "usp_cRoleID"; } }

        //public string usp_cPurviewCode_Name { get { return "usp_cPurviewCode"; } }

        //public string usp_cAdd_Name { get { return "usp_cAdd"; } }

        //public string usp_dAddDate_Name { get { return "usp_dAddDate"; } }

        //public string usp_cLast_Name { get { return "usp_cLast"; } }

        //public string usp_dLastDate_Name { get { return "usp_dLastDate"; } }
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
