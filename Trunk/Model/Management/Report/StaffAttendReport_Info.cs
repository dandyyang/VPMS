using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
  public  class StaffAttendReport_Info:IModel.IModelObject
    {
      public StaffAttendReport_Info() 
      {
          this.qry_CardUserNumber = string.Empty;
          this.qry_ChaName = string.Empty;
          this.qry_EnName = string.Empty;
          this.qry_DepartmentNum = string.Empty;
          this.qry_SchoolNum = string.Empty;
          this.qry_IsNormal = false;
          this.CarUserList = null;
      }

      ///传入使用属性

        #region MyRegion
        /// <summary>
        /// 用户编号
        /// </summary>
        public string qry_CardUserNumber { set; get; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string qry_ChaName { set; get; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string qry_EnName { set; get; }

        /// <summary>
        /// 科室编号
        /// </summary>
        public string qry_DepartmentNum { set; get; }

        /// <summary>
        /// 院系部编号

        /// </summary>
        public string qry_SchoolNum { set; get; }

        /// <summary>
        /// 开始日期

        /// </summary>
        public DateTime? qry_FromDate { set; get; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? qry_ToDate { set; get; }

        /// <summary>
        ///状态是否正常

        /// </summary>
        public bool qry_IsNormal { set; get; } 
        #endregion

      //查询后返回List
        public List<StaffAttendReport_CardUser_Info> CarUserList { get; set; }

        #region IModelObject Members

        public int RecordID
        {
            get;
            set;
        }

        #endregion
    }
}
