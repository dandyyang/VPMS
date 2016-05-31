using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.SysMaster
{
    public class Sys_UserMaster_usm_Info:IModelObject
    {

        #region 构造函数
        public Sys_UserMaster_usm_Info()
        {

            usm_iRecordID = 0;

            usm_cUserLoginID = string.Empty;

            usm_cChaName = string.Empty;

            usm_cPasswork = string.Empty;

            usm_cEMail = string.Empty;

            usm_iLock = false;

            usm_cRemark = string.Empty;

            usm_cAdd = string.Empty;

            usm_cLast = string.Empty;

            this.roleMasterList = new List<Sys_RoleMaster_rlm_Info>();

            this.formMasterList = new List<Sys_FormMaster_fom_Info>();

            this.functionMasterList = new List<Sys_FunctionMaster_fum_Info>();

        }
        #endregion

        #region 析构函数
        ~Sys_UserMaster_usm_Info()
        {

        }
        #endregion

        #region 属性

        public Int32 usm_iRecordID { set; get; }

        public string usm_cUserLoginID { set; get; }

        public string usm_cChaName { set; get; }

        public string usm_cPasswork { set; get; }

        public string usm_cEMail { set; get; }

        public bool usm_iLock { set; get; }

        public string iLock { get; set; }

        public string usm_cRemark { set; get; }

        public string usm_cAdd { set; get; }

        public DateTime? usm_dAddDate { set; get; }

        public string usm_cLast { set; get; }

        public DateTime? usm_dLastDate { set; get; }

        public List<Sys_RoleMaster_rlm_Info> roleMasterList
        {
            set;
            get;
        }
        public List<Sys_FormMaster_fom_Info> formMasterList
        {
            set;
            get;
        }
        public List<Sys_FormMaster_fom_Info> webMasterList
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

        public int usm_iRecordID_Length { set; get; }

        public int usm_cUserLoginID_Length { set; get; }

        public int usm_cChaName_Length { set; get; }

        public int usm_cPasswork_Length { set; get; }

        public int usm_cEMail_Length { set; get; }

        public int usm_iLock_Length { set; get; }

        public int usm_cRemark_Length { set; get; }

        public int usm_cAdd_Length { set; get; }

        public int usm_dAddDate_Length { set; get; }

        public int usm_cLast_Length { set; get; }

        public int usm_dLastDate_Length { set; get; }
        #endregion

        //#region 属性名称

        //public string usm_iRecordID_Name { get { return "usm_iRecordID"; } }

        //public string usm_cUserLoginID_Name { get { return "usm_cUserLoginID"; } }

        //public string usm_cChaName_Name { get { return "usm_cChaName"; } }

        //public string usm_cPasswork_Name { get { return "usm_cPasswork"; } }

        //public string usm_cEMail_Name { get { return "usm_cEMail"; } }

        //public string usm_iLock_Name { get { return "usm_iLock"; } }

        //public string usm_cRemark_Name { get { return "usm_cRemark"; } }

        //public string usm_cAdd_Name { get { return "usm_cAdd"; } }

        //public string usm_dAddDate_Name { get { return "usm_dAddDate"; } }

        //public string usm_cLast_Name { get { return "usm_cLast"; } }

        //public string usm_dLastDate_Name { get { return "usm_dLastDate"; } }
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