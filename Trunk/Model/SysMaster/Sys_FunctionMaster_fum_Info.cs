using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.SysMaster
{
    public class Sys_FunctionMaster_fum_Info:IModelObject
    {

        #region 构造函数
        public Sys_FunctionMaster_fum_Info()
        {

            fum_iRecordID = 0;

            fum_cFunctionNumber = string.Empty;

            fum_cFunctionDesc = string.Empty;

            fum_cRemark = string.Empty;

            fum_cAdd = string.Empty;

            fum_cLast = string.Empty;

            this.formMaster = new List<Sys_FormMaster_fom_Info>();

        }
        #endregion

        #region 析构函数
        ~Sys_FunctionMaster_fum_Info()
        {

        }
        #endregion

        #region 属性

        public Int32 fum_iRecordID { set; get; }

        public string fum_cFunctionNumber { set; get; }

        public string fum_cFunctionDesc { set; get; }

        public string fum_cRemark { set; get; }

        public string fum_cAdd { set; get; }

        public DateTime? fum_dAddDate { set; get; }

        public string fum_cLast { set; get; }

        public DateTime? fum_dLastDate { set; get; }

        public List<Sys_FormMaster_fom_Info> formMaster
        {
            set;
            get;
        }

        #endregion

        #region 长度属性

        public int fum_iRecordID_Length { set; get; }

        public int fum_cFunctionNumber_Length { set; get; }

        public int fum_cFunctionDesc_Length { set; get; }

        public int fum_cRemark_Length { set; get; }

        public int fum_cAdd_Length { set; get; }

        public int fum_dAddDate_Length { set; get; }

        public int fum_cLast_Length { set; get; }

        public int fum_dLastDate_Length { set; get; }
        #endregion

        //#region 属性名称

        //public string fum_iRecordID_Name { get { return "fum_iRecordID"; } }

        //public string fum_cFunctionNumber_Name { get { return "fum_cFunctionNumber"; } }

        //public string fum_cFunctionDesc_Name { get { return "fum_cFunctionDesc"; } }

        //public string fum_cRemark_Name { get { return "fum_cRemark"; } }
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