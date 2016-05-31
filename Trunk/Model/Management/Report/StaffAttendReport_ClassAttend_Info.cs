using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
  public  class StaffAttendReport_ClassAttend_Info
    {
      public StaffAttendReport_ClassAttend_Info() 
      {
          this.rpt_Date = string.Empty;
          this.rpt_TimeSpanName = string.Empty;
          this.rpt_CourseName = string.Empty;
          this.rpt_SchoolName = string.Empty;
          this.rpt_SpecialtyName = string.Empty;
          this.rpt_GraduationPeriod = string.Empty;
          this.rpt_ClassName = string.Empty;
          this.rpt_IsNormal = string.Empty;
          this.rpt_StartTime = string.Empty;
          this.rpt_EndTime = string.Empty;
          this.rpt_AbsenceType = string.Empty;
          this.rpt_AbsenceTime = 0;
          this.rpt_AttendTime = 0;
          this.rpt_iSeq = 0;
      }

      /// <summary>
      /// 日期名称
      /// </summary>
      public string rpt_Date { set; get; }

      /// <summary>
      /// 日期名称
      /// </summary>
      public string rpt_CRDate { set; get; }

      /// <summary>
      /// 时段名称
      /// </summary>
      public string rpt_TimeSpanName { get; set; }

      /// <summary>
      /// 课程名称
      /// </summary>
      public string rpt_CourseName { get; set; }

      /// <summary>
      /// 院系部

      /// </summary>
      public string rpt_SchoolName { get; set; }

      /// <summary>
      /// 专业名称
      /// </summary>
      public string rpt_SpecialtyName { get; set; }

      /// <summary>
      /// 屇别名称
      /// </summary>
      public string rpt_GraduationPeriod { get; set; }

      /// <summary>
      /// 班级名称
      /// </summary>
      public string rpt_ClassName { get; set; }

      /// <summary>
      /// 是否正常
      /// </summary>
      public string rpt_IsNormal { get; set; }

      /// <summary>
      /// 上课时间
      /// </summary>
      public string rpt_StartTime { get; set; }

      /// <summary>
      /// 下课时间
      /// </summary>
      public string rpt_EndTime { get; set; }

      /// <summary>
      /// 出勤时间
      /// </summary>
      public int rpt_AttendTime { get; set; }

      /// <summary>
      /// 缺勤时间
      /// </summary>
      public int rpt_AbsenceTime { get; set; }

      /// <summary>
      /// 缺勤类型
      /// </summary>
      public string rpt_AbsenceType { get; set; }

      /// <summary>
      /// 课程序号
      /// </summary>
      public int rpt_iSeq { get; set; }
    }
}
