using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.Management.Report
{
   public class StudentAttendReport_Info:Model.IModel.IModelObject
    {
       public StudentAttendReport_Info() 
       {
           rpt_AttendDate = string.Empty;
           rpt_CourseTtimeName = string.Empty;
           rpt_CourseName = string.Empty;
           //rpt_TeacherName = string.Empty;
           rpt_StudentName = string.Empty;
           rpt_SchoolName = string.Empty;
           rpt_SpecialtyName = string.Empty;
           rpt_GraduationPeriod = string.Empty;
           rpt_ClassName = string.Empty;
           rpt_isNormalText = string.Empty;
           rpt_isNormal = false;
           rpt_StartTime = string.Empty;
           rpt_EndTime = string.Empty;
           rpt_AttendMin = 0;
           rpt_MissMin = 0;
           rpt_AttendStact = string.Empty;
           rpt_cclID = 0;
           rpt_UserNum = string.Empty;
           qry_iSeq = 0;
           cus_guidPhotoKey = Guid.Empty;

           qry_SchoolNum = string.Empty;
           qry_SpecialtyNum = string.Empty;
           qry_GraduationPeriod = string.Empty;
           qry_ClassNum = string.Empty;
           qry_StudentName = string.Empty;
           qry_isNormal = false;
           qry_CourseName = string.Empty;
           cus_imgPhotoUrl = string.Empty;
       }

       //查询后使用

       #region MyRegion

       /// <summary>
       /// 图片guid
       /// </summary>
       public Guid cus_guidPhotoKey { get; set; }

       /// <summary>
       /// 图片URL
       /// </summary>
       public string cus_imgPhotoUrl { get; set; }

       /// <summary>
       /// 用户表记录ID
       /// </summary>
       public int rpt_UserRecordID { get; set; }

       /// <summary>
       /// 用户编号
       /// </summary>
       public string rpt_UserNum { get; set; }

       /// <summary>
       /// ccl表记录ID
       /// </summary>
       public long rpt_cclID { get; set; }

       /// <summary>
       /// 出勤日期
       /// </summary>
       public string rpt_AttendDate { get; set; }

       /// <summary>
       /// 上课时段
       /// </summary>
       public string rpt_CourseTtimeName { get; set; }

       /// <summary>
       /// 课程名称
       /// </summary>
       public string rpt_CourseName { get; set; }

       /// <summary>
       ///讲师姓名
       /// </summary>
       //public string rpt_TeacherName { get; set; }

       /// <summary>
       ///学生姓名
       /// </summary>
       public string rpt_StudentName { get; set; }

       /// <summary>
       ///院系部

       /// </summary>
       public string rpt_SchoolName { get; set; }

       /// <summary>
       ///专业名称
       /// </summary>
       public string rpt_SpecialtyName { get; set; }

       /// <summary>
       ///届别
       /// </summary>
       public string rpt_GraduationPeriod { get; set; }

       /// <summary>
       ///班级
       /// </summary>
       public string rpt_ClassName { get; set; }

       /// <summary>
       ///考勤是否正常
       /// </summary>
       public bool rpt_isNormal { get; set; }

       /// <summary>
       ///考勤是否正常显示
       /// </summary>
       public string rpt_isNormalText { get; set; }

       /// <summary>
       ///上课时间
       /// </summary>
       public string rpt_StartTime { get; set; }

       /// <summary>
       ///下课时间
       /// </summary>
       public string rpt_EndTime { get; set; }

       /// <summary>
       ///出勤时间
       /// </summary>
       public int rpt_AttendMin { get; set; }

       /// <summary>
       ///缺勤时间
       /// </summary>
       public int rpt_MissMin { get; set; }

       /// <summary>
       ///缺勤类型
       /// </summary>
       public string rpt_AttendStact { get; set; }

       #endregion

       //查询传入属性

       #region MyRegion
       /// <summary>
       /// 院系编号
       /// </summary>
       public string qry_SchoolNum { get; set; }

       /// <summary>
       /// 身份
       /// </summary>
       public string qry_IdentityNum { get; set; }

       /// <summary>
       /// 专业编号
       /// </summary>
       public string qry_SpecialtyNum { get; set; }

       /// <summary>
       /// 届别
       /// </summary>
       public string qry_GraduationPeriod { get; set; }

       /// <summary>
       /// 班级编号
       /// </summary>
       public string qry_ClassNum { get; set; }

       /// <summary>
       /// 学生姓名
       /// </summary>
       public string qry_StudentName { get; set; }

       /// <summary>
       /// 开始日期

       /// </summary>
       public DateTime? qry_FromDate { get; set; }

       /// <summary>
       /// 结束日期
       /// </summary>
       public DateTime? qry_ToDate { get; set; }

       /// <summary>
       /// 考勤是否正常
       /// </summary>
       public bool qry_isNormal { get; set; }

       /// <summary>
       /// 课程名称
       /// </summary>
       public string qry_CourseName { get; set; }

       /// <summary>
       /// 课程序号
       /// </summary>
       public int qry_iSeq { get; set; }

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
