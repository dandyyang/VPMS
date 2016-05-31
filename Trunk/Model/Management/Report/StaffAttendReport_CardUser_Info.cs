using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
  public  class StaffAttendReport_CardUser_Info:IModel.IModelObject
    {
      public StaffAttendReport_CardUser_Info() 
      {
          this.rpt_date = string.Empty;
          this.rpt_CardUserNum = string.Empty;
          this.rpt_ChaName = string.Empty;
          this.rpt_EnName = string.Empty;
          this.rpt_SchoolName = string.Empty;
          this.rpt_DepartmentName = string.Empty;
          this.rpt_TotalWorkAttenTime = 0;
          this.rpt_TotalWorkMissTime = 0;
          this.rpt_TotalClassAttendTime = 0;
          this.rpt_TotalClassMissTime = 0;

          this.WorkAttendDetailList = null;
          this.ClassAttendDetailLIst = null;
      }

      /// <summary>
      /// 格式化的日期
      /// </summary>
      public string rpt_date { get; set; }

      /// <summary>
      /// 用户编号
      /// </summary>
      public string rpt_CardUserNum { get; set; }

      /// <summary>
      /// 中文名称
      /// </summary>
      public string rpt_ChaName { get; set; }

      /// <summary>
      /// 英文名称
      /// </summary>
      public string rpt_EnName { get; set; }

      /// <summary>
      /// 院系部

      /// </summary>
      public string rpt_SchoolName { get; set; }

      /// <summary>
      /// 科室
      /// </summary>
      public string rpt_DepartmentName { get; set; }

      /// <summary>
      /// 上班出勤总时间

      /// </summary>
      public decimal rpt_TotalWorkAttenTime { get; set; }

      /// <summary>
      ///  上班缺勤总时间

      /// </summary>
      public decimal rpt_TotalWorkMissTime { get; set; }

      /// <summary>
      /// 上课出勤总时间

      /// </summary>
      public decimal rpt_TotalClassAttendTime { get; set; }

      /// <summary>
      /// 上课缺勤总时间

      /// </summary>
      public decimal rpt_TotalClassMissTime { get; set; }

      public List<StaffAttendReport_WorkAttend_Info> WorkAttendDetailList { get; set; }

      public List<StaffAttendReport_ClassAttend_Info> ClassAttendDetailLIst { get; set; }


      #region IModelObject Members

      public int RecordID
      {
          get;
          set;
      }

      #endregion
    }
}
