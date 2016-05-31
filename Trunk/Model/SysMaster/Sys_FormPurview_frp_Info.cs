using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.SysMaster
{
    public class Sys_FormPurview_frp_Info:IModelObject
    {

        #region 构造函数
        public Sys_FormPurview_frp_Info()
        {

            frp_iRecordID = 0;

            frp_cPurviewCode = string.Empty;

            frp_cFunctionNumber = string.Empty;

            frp_cFormNumber = string.Empty;

            //frp_cAdd = string.Empty;

            //frp_dAddDate = DateTime.MinValue;

            //frp_cLast = string.Empty;

            //frp_dLastDate = DateTime.MinValue;

        }
        #endregion

        #region 析构函数
        ~Sys_FormPurview_frp_Info()
        {

        }
        #endregion

        #region 属性

        public Int32 frp_iRecordID { set; get; }

        public string frp_cPurviewCode { set; get; }

        public string frp_cFunctionNumber { set; get; }

        public string frp_cFormNumber { set; get; }

        //public string frp_cAdd { set; get; }

        //public DateTime frp_dAddDate { set; get; }

        //public string frp_cLast { set; get; }

        //public DateTime frp_dLastDate { set; get; }
        #endregion

        #region 长度属性

        public int frp_iRecordID_Length { set; get; }

        public int frp_cPurviewCode_Length { set; get; }

        public int frp_cFunctionNumber_Length { set; get; }

        public int frp_cFormNumber_Length { set; get; }

        public int frp_cAdd_Length { set; get; }

        public int frp_dAddDate_Length { set; get; }

        public int frp_cLast_Length { set; get; }

        public int frp_dLastDate_Length { set; get; }
        #endregion

        //#region 属性名称

        //public string frp_iRecordID_Name { get { return "frp_iRecordID"; } }

        //public string frp_cPurviewCode_Name { get { return "frp_cPurviewCode"; } }

        //public string frp_cFunctionNumber_Name { get { return "frp_cFunctionNumber"; } }

        //public string frp_cFormNumber_Name { get { return "frp_cFormNumber"; } }
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