using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.SysMaster
{
    public class Sys_UserRoles_usr_Info:IModelObject
    {

        #region 构造函数
        public Sys_UserRoles_usr_Info()
        {

            usr_iRecordID = 0;

            usr_cUserLoginID = string.Empty;

            usr_cRoleID = string.Empty;

            //usr_cAdd = string.Empty;

            //usr_dAddDate = DateTime.MinValue;

            //usr_cLast = string.Empty;

            //usr_dLastDate = DateTime.MinValue;

        }
        #endregion

        #region 析构函数
        ~Sys_UserRoles_usr_Info()
        {

        }
        #endregion

        #region 属性

        public Int32 usr_iRecordID { set; get; }

        public string usr_cUserLoginID { set; get; }

        public string usr_cRoleID { set; get; }

        //public string usr_cAdd { set; get; }

        //public DateTime? usr_dAddDate { set; get; }

        //public string usr_cLast { set; get; }

        //public DateTime? usr_dLastDate { set; get; }
        #endregion

        #region 长度属性

        public int usr_iRecordID_Length { set; get; }

        public int usr_cUserLoginID_Length { set; get; }

        public int usr_cRoleID_Length { set; get; }

        public int usr_cAdd_Length { set; get; }

        public int usr_dAddDate_Length { set; get; }

        public int usr_cLast_Length { set; get; }

        public int usr_dLastDate_Length { set; get; }
        #endregion

        //#region 属性名称

        //public string usr_iRecordID_Name { get { return "usr_iRecordID"; } }

        //public string usr_cUserLoginID_Name { get { return "usr_cUserLoginID"; } }

        //public string usr_cRoleID_Name { get { return "usr_cRoleID"; } }

        //public string usr_cAdd_Name { get { return "usr_cAdd"; } }

        //public string usr_dAddDate_Name { get { return "usr_dAddDate"; } }

        //public string usr_cLast_Name { get { return "usr_cLast"; } }

        //public string usr_dLastDate_Name { get { return "usr_dLastDate"; } }
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