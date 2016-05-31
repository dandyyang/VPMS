using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Model.Management.Report
{
   public class RptStudentClassAttend_Info:IModel.IModelObject
    {
       public RptStudentClassAttend_Info() 
       {
           this.qry_SchoolNum = string.Empty;
           this.qry_SpecialtyNum = string.Empty;
           this.qry_GraduationPeriod = string.Empty;
           this.StudentClassAttendReport = null;
           this.StudentClassAttendDetailReport = null;
       }

       public DateTime qry_StartDate { get; set; }

       public DateTime qry_EndDate { get; set; }

       public string qry_SchoolNum { get; set; }

       public string qry_SpecialtyNum { get; set; }

       public string qry_GraduationPeriod { get; set; }

       /// <summary>
       /// rpt主报表数据源
       /// </summary>
       public List<StudentClassAttendReport_Info> StudentClassAttendReport { get; set; }

       /// <summary>
       /// rpt子报表数据源
       /// </summary>
       public List<StudentClassAttendDetailReport_Info> StudentClassAttendDetailReport { get; set; }

       #region IModelObject 成员

       public int RecordID
       {
           get;
           set;
       }

       #endregion
    }
}
