using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.IModel;

namespace Model.Management.Master
{
    public class CodeMaster_cmt_Info:IModelObject
    {

        #region 构造函数

        public CodeMaster_cmt_Info()
        {

            cmt_iRecordID = 0;

            cmt_cKey1 = string.Empty;

            cmt_cKey2 = string.Empty;

            cmt_cValue = string.Empty;

            cmt_fNumber = 0;

            cmt_cRemark = string.Empty;

            cmt_cRemark2 = string.Empty;

            cmt_cAdd = string.Empty;

            cmt_dAddDate = DateTime.MinValue;

            cmt_cLast = string.Empty;

            cmt_dLastDate = DateTime.MinValue;

        }
        #endregion

        #region 析构函数
        ~CodeMaster_cmt_Info()
        {

        }
        #endregion

        #region 属性


        public Int32 cmt_iRecordID { set; get; }

        public string cmt_cKey1 { set; get; }

        public string cmt_cKey2 { set; get; }

        public string cmt_cValue { set; get; }

        public decimal cmt_fNumber { set; get; }

        public string cmt_cRemark { set; get; }

        public string cmt_cRemark2 { set; get; }

        public string cmt_cAdd { set; get; }

        public DateTime? cmt_dAddDate { set; get; }

        public string cmt_cLast { set; get; }

        public DateTime? cmt_dLastDate { set; get; }
        #endregion

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    } 
}
