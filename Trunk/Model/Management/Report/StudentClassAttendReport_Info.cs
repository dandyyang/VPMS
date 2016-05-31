using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
   public class StudentClassAttendReport_Info
    {

        public StudentClassAttendReport_Info()
        {
            this.rpt_Date = string.Empty;

            this.rpt_SchoolName = string.Empty;

            this.rpt_SpecialtyName = string.Empty;

            this.rpt_GraduationPeriod = string.Empty;

            this.rpt_ClassName = string.Empty;

            this.rpt_ClassTeacher = string.Empty;

            this.rpt_DayOfWeek = string.Empty;

            this.rpt_Holiday = string.Empty;

            this.rpt_SchoolNum = string.Empty;

            this.rpt_SpecialtyNum = string.Empty;

            this.rpt_ClassNum = string.Empty;


        }

        public string rpt_Date { get; set; }

        public string rpt_SchoolName { get; set; }

        public string rpt_SchoolNum { get; set; }

        public string rpt_SpecialtyName { get; set; }

        public string rpt_SpecialtyNum { get; set; }

        public string rpt_GraduationPeriod { get; set; }

        public string rpt_ClassName { get; set; }

        public string rpt_ClassNum { get; set; }

        public string rpt_ClassTeacher { get; set; }

        public string rpt_DayOfWeek { get; set; }

        public string rpt_Holiday { get; set; }
    }
}
