using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
   public class StudentClassAttendDetailReport_Info
    {

        public StudentClassAttendDetailReport_Info()
        {
            this.rpt_AttendStact = string.Empty;
            this.rpt_ClassNum = string.Empty;
            this.rpt_CourseName = string.Empty;
            this.rpt_CourseTtimeName = string.Empty;
            this.rpt_Date = string.Empty;
            this.rpt_EndTime = string.Empty;
            this.rpt_GraduationPeriod = string.Empty;
            this.rpt_MissMin = 0;
            this.rpt_SchoolNum = string.Empty;
            this.rpt_SpecialtyNum = string.Empty;
            this.rpt_StartTime = string.Empty;
            this.rpt_StudentName = string.Empty;


        }

        /// <summary>
        /// 出勤日期
        /// </summary>
        public string rpt_Date { get; set; }

        /// <summary>
        /// 上课时段
        /// </summary>
        public string rpt_CourseTtimeName { get; set; }

        /// <summary>
        ///上课时间
        /// </summary>
        public string rpt_StartTime { get; set; }

        /// <summary>
        ///下课时间
        /// </summary>
        public string rpt_EndTime { get; set; }

        /// <summary>
        ///缺勤时间
        /// </summary>
        public int rpt_MissMin { get; set; }

        /// <summary>
        ///缺勤类型
        /// </summary>
        public string rpt_AttendStact { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string rpt_CourseName { get; set; }

        /// <summary>
        ///学生姓名
        /// </summary>
        public string rpt_StudentName { get; set; }

        public string rpt_SchoolNum { get; set; }

        public string rpt_SpecialtyNum { get; set; }

        public string rpt_GraduationPeriod { get; set; }

        public string rpt_ClassNum { get; set; }
    }
}
