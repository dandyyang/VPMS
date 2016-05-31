using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
  public  class StaffAttendReport_WorkAttend_Info
    {
      public StaffAttendReport_WorkAttend_Info() 
      {
          this.rpt_date = string.Empty;
          this.rpt_isNormal = string.Empty;
          this.rpt_HolidayType = string.Empty;
          this.rpt_TimeInterval1_time1 = string.Empty;
          this.rpt_TimeInterval1_time2 = string.Empty;
          this.rpt_TimeInterval1_misstype = string.Empty;
          this.rpt_TimeInterval2_time1 = string.Empty;
          this.rpt_TimeInterval2_time2 = string.Empty;
          this.rpt_TimeInterval2_misstype = string.Empty;
          this.rpt_TimeInterval3_time1 = string.Empty;
          this.rpt_TimeInterval3_time2 = string.Empty;
          this.rpt_TimeInterval3_misstype = string.Empty;
          this.rpt_AttendTime = 0;
          this.rpt_MissTime = 0;
      }

      /// <summary>
      /// 格式化后的日期

      /// </summary>
      public string rpt_date { get; set; }

      /// <summary>
      /// 考勤是否正常
      /// </summary>
      public string rpt_isNormal { get; set; }

      /// <summary>
      /// 假期类型
      /// </summary>
      public string rpt_HolidayType { get; set; }

      /// <summary>
      /// 时段1打卡时间1
      /// </summary>
      public string rpt_TimeInterval1_time1 { get; set; }

      /// <summary>
      /// 时段1打卡时间2
      /// </summary>
      public string rpt_TimeInterval1_time2 { get; set; }

      /// <summary>
      /// 时段1缺勤类型
      /// </summary>
      public string rpt_TimeInterval1_misstype { get; set; }

      /// <summary>
      /// 时段2打卡时间1
      /// </summary>
      public string rpt_TimeInterval2_time1 { get; set; }

      /// <summary>
      /// 时段2打卡时间2
      /// </summary>
      public string rpt_TimeInterval2_time2 { get; set; }

      /// <summary>
      /// 时段2缺勤类型
      /// </summary>
      public string rpt_TimeInterval2_misstype { get; set; }

      /// <summary>
      /// 时段3打卡时间1
      /// </summary>
      public string rpt_TimeInterval3_time1 { get; set; }

      /// <summary>
      /// 时段3打卡时间2
      /// </summary>
      public string rpt_TimeInterval3_time2 { get; set; }

      /// <summary>
      /// 时段3缺勤类型
      /// </summary>
      public string rpt_TimeInterval3_misstype { get; set; }

      /// <summary>
      /// 出勤时间
      /// </summary>
      public decimal rpt_AttendTime { get; set; }

      /// <summary>
      /// 缺勤时间
      /// </summary>
      public decimal rpt_MissTime { get; set; }

    }
}
