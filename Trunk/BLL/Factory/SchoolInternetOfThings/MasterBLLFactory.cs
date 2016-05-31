using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Factory.Management
{
    public class MasterBLLFactory
    {
        /// <summary>
        /// 院系部主檔
        /// </summary>
        public static readonly string SchoolMaster = "BLL.DAL.Management.Master.SchoolMasterBL,BLL";
        public static readonly string BuildingMaster = "BLL.DAL.Management.Master.BuildingMasterBL,BLL";
        public static readonly string SiteMaster = "BLL.DAL.Management.Master.SiteMasterBL,BLL";
        public static readonly string CourseMaster = "BLL.DAL.Management.Master.CourseMasterBL,BLL";
        public static readonly string SpecialtyMaster = "BLL.DAL.Management.Master.SpecialtyMasterBL,BLL";
        public static readonly string CardUserMaster = "BLL.DAL.Management.Master.CardUserMasterBL,BLL";
        public static readonly string CheckOnWorkAttendanceSetting = "BLL.DAL.Management.Setting.CheckOnWorkAttendanceSettingBL,BLL";
        public static readonly string AttendClassSetting = "BLL.DAL.Management.Setting.AttendClassSettingBL,BLL";
        public static readonly string AbsenceTypeSetting = "BLL.DAL.Management.Setting.AbsenceTypeSettingBL,BLL";
        public static readonly string HolidaysSetting = "BLL.DAL.Management.Setting.HolidaysSettingBL,BLL";
        public static readonly string ClassReviseCourse = "BLL.DAL.Management.CheckOnWorkAttendanceAffair.ClassReviseCourseBL,BLL";
        public static readonly string AbsenceRegister = "BLL.DAL.Management.CheckOnWorkAttendanceAffair.AbsenceRegisterBL,BLL";
        public static readonly string SchoolCourseRevise = "BLL.DAL.Management.CheckOnWorkAttendanceAffair.SchoolCourseReviseBL,BLL";
        public static readonly string RFIDCardManage = "BLL.DAL.Management.Master.RFIDCardManageBL,BLL";
        public static readonly string RFIDCardIssuance = "BLL.DAL.Management.Transaction.RFIDCardIssuanceBL,BLL";
        public static readonly string UserCardManage = "BLL.DAL.Management.Transaction.UserCardManageBL,BLL";
        public static readonly string OverTimeRegister = "BLL.DAL.Management.CheckOnWorkAttendanceAffair.OverTimeRegisterBL,BLL";
        public static readonly string StudentAttend_rpt = "BLL.DAL.Management.Report.ReportBL,BLL";
        public static readonly string UserSkin_urs = "BLL.DAL.Management.Master.UserSkinBL,BLL";
        public static readonly string CodeMaster_cmt = "BLL.DAL.Management.Master.CodeMasterBL,BLL";
        public static readonly string DeleteDataLogic_ddl = "BLL.DAL.Management.DeleteData.DeleteDataLogicBL,BLL";
        public static readonly string SchoolWeekSetting = "BLL.DAL.Management.Setting.SchoolWeekSettingBL,BLL";
        public static readonly string General = "BLL.DAL.Management.Service.GeneralBL,BLL";
        public static readonly string MessageManagement = "BLL.DAL.Management.MessageManagement.MessageManagementBL,BLL";
        public static readonly string _24GInOutMessageDemo = "BLL.DAL.Management._24GInOutMessage._24GInOutMessageBL,BLL";
        public static readonly string StaffBasicWages = "BLL.DAL.Management.SalaryManagement.StaffBasicWagesBL,BLL";
        public static readonly string StaffWeekWorkHourWages = "BLL.DAL.Management.SalaryManagement.StaffWeekWorkHourWagesBL,BLL";
        public static readonly string StaffDutyWorkHourWages = "BLL.DAL.Management.SalaryManagement.StaffDutyWorkHourWagesBL,BLL";
        public static readonly string StaffOrganizationsWorkHourWages = "BLL.DAL.Management.SalaryManagement.StaffOrganizationsWorkHourWagesBL,BLL";
        public static readonly string StaffAllowanceWages = "BLL.DAL.Management.SalaryManagement.StaffAllowanceWagesBL,BLL";
        public static readonly string StaffOverTimeWages = "BLL.DAL.Management.SalaryManagement.StaffOverTimeWagesBL,BLL";
        public static readonly string StaffSalary = "BLL.DAL.Management.SalaryManagement.StaffSalaryBL,BLL";
        public static readonly string TeacherAttendCheck = "BLL.DAL.Management.CheckOnWorkAttendanceAffair.TeacherAttendCheckBL,BLL";
        public static readonly string CheckOnTeacherCourseAttendance = "BLL.DAL.Management.CheckOnWorkAttendanceAffair.CheckOnTeacherCourseAttendanceBL,BLL";
        public static readonly string Test = "BLL.DAL.Management.Test.TestBL,BLL";

        /// <summary>
        /// 借书证用户信息—古劳中学
        /// </summary>
        public static readonly string LibraryCardUserInformation_GL = "BLL.DAL.Management.LibraryManagement.LibraryCardUserInformation_GLBL,BLL";

        /// <summary>
        /// 停课设置
        /// </summary>
        public static readonly string SuspendClassSetting = "BLL.DAL.Management.Setting.SuspendClassSettingBL,BLL";

        public static readonly string CheckOnTeacherAttendance = "BLL.DAL.Management.CheckOnWorkAttendanceSearch.CheckOnTeacherAttendanceBL,BLL";

        public static readonly string ClassCourseSettingMSOGroup = "BLL.DAL.Management.Setting.ClassCourseSettingMSOGroupBL,BLL";

        public static readonly string ClassSettingSwitch = "BLL.DAL.Management.Setting.ClassSettingSwitchBL,BLL";

        public static readonly string EntranceguardPeriod = "BLL.DAL.Management.EntranceGuard.EntranceguardPeriodBL,BLL";

        public static readonly string EntranceguardRoles = "BLL.DAL.Management.EntranceGuard.EntranceguardRolesBL,BLL";

        public static readonly string EntranceguardRight = "BLL.DAL.Management.EntranceGuard.EntranceguardRightBL,BLL";

        public static readonly string EntranceguardTask = "BLL.DAL.Management.EntranceGuard.EntranceguardTaskBL,BLL";

        public static readonly string MonitorTimeSetting = "BLL.DAL.Management.EReleasePass.MonitorTimeSettingBL,BLL";

        public static readonly string VisitDetailRegister = "BLL.DAL.Management.VisitorManagement.VisitDetailRegisterBL,BLL";

        public static readonly string VisitorGroupMaster = "BLL.DAL.Management.VisitorManagement.VisitorGroupMasterBL,BLL";

        public static readonly string VisitorMaster = "BLL.DAL.Management.VisitorManagement.VisitorMasterBL,BLL";

        //消费系统
        public static readonly string PaymentRecharge = "BLL.DAL.Management.Payment.RechargeBL,BLL";
        public static readonly string PaymentSettingManagement = "BLL.DAL.Management.Payment.SettingManagementBL,BLL";
        public static readonly string PaymentMachineInfo = "BLL.DAL.Management.Payment.PaymentMachineInfoBL,BLL";
        public static readonly string PaymentCardManegement = "BLL.DAL.Management.Payment.CardManagermentBL,BLL";
        public static readonly string PaymentDiningAdjust = "BLL.DAL.Management.Payment.PaymentDiningAdjustBL,BLL";
        public static readonly string PaymentDetail = "BLL.DAL.Management.Payment.PaymentDetailBL,BLL";
        public static readonly string PaymentDailyAdjustMark = "BLL.DAL.Management.Payment.PaymentDailyAdjustMarkBL,BLL";
        public static readonly string PaymentInfo = "BLL.DAL.Management.Payment.PaymentInfoBL,BLL";
        public static readonly string PaymentDetailInfo = "BLL.DAL.Management.Payment.PaymentDetailInfoBL,BLL";
        public static readonly string PaymentDormitoryWater = "BLL.DAL.Management.Payment.PaymentDormitoryWaterBL,BLL";
        public static readonly string PaymentChangeCardRecord = "BLL.DAL.Management.Payment.PaymentChangeCardRecordBL,BLL";
        public static readonly string PaymentUDGeneralSetting = "BLL.DAL.Management.Payment.PaymentUDGeneralSettingBL,BLL";
        public static readonly string PaymentUDMealState = "BLL.DAL.Management.Payment.PaymentUDMealStateBL,BLL";
        public static readonly string PaymentSetting = "BLL.DAL.Management.Payment.PaymentSettingBL,BLL";
        /// <summary>
        /// 黑名单 解挂\挂失
        /// </summary>
        public static readonly string BlacklistUploadService = "BLL.DAL.Management.Service.BlacklistUploadServiceBL,BLL";

        /// <summary>
        /// 班级课程及考勤设置
        /// </summary>
        public static readonly string ClassCourseSetting = "BLL.DAL.Management.Setting.ClassCourseSettingBL,BLL";


        #region 2011-10-10
        public static readonly string SiteMonitoring = "BLL.DAL.Management.Report.SiteMonitoringBL,BLL";
        #endregion

        #region 2011-10-17
        public static readonly string DataRightsRole = "BLL.DAL.Management.DataRightsManagement.DataRightsRoleBL,BLL";
        #endregion

        #region Administrator

        //考勤设置
        public static readonly string MonitorControllerSetting = "BLL.DAL.Management.Administrator.MonitorControllerSettingBL,BLL";
        public static readonly string ReaderOfControllerSetting = "BLL.DAL.Management.Administrator.ReaderOfControllerSettingBL,BLL";

        /// <summary>
        /// 2.4G设置
        /// </summary>
        public static readonly string Reader24GSetting = "BLL.DAL.Management.Administrator.Reader24GSettingBL,BLL";
        public static readonly string Antenna24GSetting = "BLL.DAL.Management.Administrator.Antenna24GSettingBL,BLL";


        //2.4G监控
        public static readonly string Reader24GMonitor = "BLL.DAL.Management.Administrator.Reader24GMonitorBL,BLL";
        public static readonly string _24GMonitorTeam = "BLL.DAL.Management.Administrator._24GMonitorTeamBL,BLL";
        public static readonly string _24GMonitorTeamDetail = "BLL.DAL.Management.Administrator._24GMonitorTeamDetailBL,BLL";

        /// <summary>
        /// 服务通用数据
        /// </summary>
        public static readonly string ServiceGeneralData = "BLL.DAL.Management.Service.ServiceGeneralBL,BLL";

        /// <summary>
        /// 學生上放學監控
        /// </summary>
        public static readonly string StudentGoToSchoolMonitorService = "BLL.DAL.Management.Service.StudentGoToSchoolMonitorServiceBL,BLL";

        /// <summary>
        /// 课程考勤服务
        /// </summary>
        public static readonly string CourseCOWAService = "BLL.DAL.Management.Service.CourseCOWAServiceBL,BLL";

        /// <summary>
        /// 教职工上下班考勤
        /// </summary>
        public static readonly string TeacherGoToWorkService = "BLL.DAL.Management.Service.StaffCOWAServiceBL,BLL";

        /// <summary>
        /// 考勤源始数据服务
        /// </summary>
        public static readonly string COWASourceDataService = "BLL.DAL.Management.Service.COWASourceDataServiceBL,BLL";

        /// <summary>
        /// 用户活动监控原始数据服务
        /// </summary>
        public static readonly string UserActivityMonitorSourceDataService = "BLL.DAL.Management.Service.UserActivityMonitorSourceDataServiceBL,BLL";

        /// <summary>
        ///  同步服务器控制器时间  
        /// </summary>
        public static readonly string SynchroControllerService = "BLL.DAL.Management.Service.SynchroControllerBL,BLL";

        /// <summary>
        ///  获取考勤管理机服务资料  
        /// </summary>
        public static readonly string EntranceGuardService = "BLL.DAL.Management.Service.EntranceGuardServiceBL,BLL";

        /// <summary>
        /// 用户活动监控事项
        /// </summary>
        public static readonly string UserActivityMonitorItem = "BLL.DAL.Management.Administrator.UserActivityMonitorItemBL,BLL";

        /// <summary>
        /// 用户活动监控事项规则设置组别
        /// </summary>
        public static readonly string UserActivityMonitorItemSettingGroup = "BLL.DAL.Management.Administrator.UserActivityMonitorItemSettingGroupBL,BLL";

        #endregion

        #region CheckOnWorkAttendanceSearch

        public static readonly string StaffCOWATime = "BLL.DAL.Management.CheckOnWorkAttendanceSearch.StaffCOWASingleSearchBL,BLL";

        public static readonly string StudentActivitySearch = "BLL.DAL.Management.CheckOnWorkAttendanceSearch.StudentActivitySearchBL,BLL";

        public static readonly string CheckOnWorkAttendanceSite = "BLL.DAL.Management.Setting.CheckOnWorkAttendanceSiteBL,BLL";

        public static readonly string CheckOnAttendanceUnusualRecord = "BLL.DAL.Management.Service.CheckOnAttendanceUnusualRecordBL,BLL";

        public static readonly string CheckOnWorkAttendanceGroupSetting = "BLL.DAL.Management.Setting.CheckOnWorkAttendanceGroupSettingBL,BLL";
        #endregion

        #region 2.4G會議監控
        public static readonly string MeetingSignIn = "BLL.DAL.Management.MeetingSignIn.MeetingSignInBL,BLL";
        #endregion

        #region 2.4G
        public static readonly string ExamineInformation = "BLL.DAL.Management.interaction.ExamineInformationBL,BLL";

        public static readonly string InteractiveUISettingInfo = "BLL.DAL.Management.interaction.InteractiveUISettingInfoBL,BLL";

        public static readonly string ExaminationQuestionsWarehouse = "BLL.DAL.Management.interaction.ExaminationQuestionsWarehouseBL,BLL";

        public static readonly string InteractiveResult = "BLL.DAL.Management.interaction.InteractiveResultBL,BLL";

        public static readonly string AnalysisAnswer = "BLL.DAL.Management.interaction.AnalysisAnswerBL,BLL";


        ///// <summary>
        ///// 2.4G互動界面設置
        ///// </summary>
        //public static readonly string TH24GInteractiveUISetting = "BLL.DAL.Management.TH24G.TH24GInteractiveUISettingBL,BLL";
        #endregion

        #region 2.4GOffLine

        public static readonly string ExamineInformationOffLine = "BLL.DAL.Management.interactionOffLine.ExamineInformationOffLineBL,BLL";

        public static readonly string InteractiveResultOffLine = "BLL.DAL.Management.interactionOffLine.InteractiveResultOffLineBL,BLL";

        public static readonly string InteractionParticipantListOffLine = "BLL.DAL.Management.interactionOffLine.InteractionParticipantListOffLineBL,BLL";

        public static readonly string QueryHSYZClassInfo = "BLL.DAL.Management.interactionOffLine.QueryHSYZClassInfoBL,BLL";

        public static readonly string QueryHSYZGradeInfo = "BLL.DAL.Management.interactionOffLine.QueryHSYZGradeInfoBL,BLL";

        public static readonly string ExaminationQuestionsSetting = "BLL.DAL.Management.interactionOffLine.ExaminationQuestionsSettingBL,BLL";

        #endregion


        #region 公共设备管理
        public static readonly string AMSUnitMasterBL = "BLL.DAL.Management.DeviceManagement.AMSUnitMasterBL,BLL";
        public static readonly string AMSTypeMasterBL = "BLL.DAL.Management.DeviceManagement.AMSTypeMasterBL,BLL";
        public static readonly string AMSItemMasterBL = "BLL.DAL.Management.DeviceManagement.AMSItemMasterBL,BLL";
        public static readonly string AMSInStockTrxBL = "BLL.DAL.Management.DeviceManagement.AMSInStockTrxBL,BLL";
        public static readonly string AMSOutStockTrxBL = "BLL.DAL.Management.DeviceManagement.AMSOutStockTrxBL,BLL";
        #endregion

        #region 發送信息，郵件相關

        /// <summary>
        ///  系统通知
        /// </summary>
        public static readonly string SysNotice = "BLL.DAL.Management.Service.SysNoticeBL,BLL";


        //------------------------------------------------------------------------------------//
        //add by  justinleung  2011/9/7 17:09:30
        //remark: 後臺發送用戶定制信息

        public static readonly string AutoMessageMasterBL = "BLL.DAL.Management.Master.AutoMessageMasterBL,BLL";
        //------------------------------------------------------------------------------------//

        #endregion

        public static readonly string EReleasePass = "BLL.DAL.Management.EReleasePass.EReleasePassBL,BLL";

        

        #region 财务管理

        /// <summary>
        /// 财务管理--缴费登记
        /// </summary>
        public static readonly string FinanceTableMoneyRecord = "BLL.DAL.Management.FinancialAffairsManage.TableMoneyRecordBL,BLL";

        /// <summary>
        /// 财务支出详细表
        /// </summary>
        public static readonly string FinancialAffairsPayDetail = "BLL.DAL.Management.FinancialAffairsManage.FinancialAffairsPayDetailBL,BLL";

        /// <summary>
        /// 财务收入详细
        /// </summary>
        public static readonly string FinancialAffairsIncome = "BLL.DAL.Management.FinancialAffairsManage.FinancialAffairsIncomeDetailBL,BLL";

        /// <summary>
        /// 财务收入支出月份结余
        /// </summary>
        public static readonly string FinancialAffairsMonthlyBalance = "BLL.DAL.Management.FinancialAffairsManage.FinancialAffairsMonthlyBalanceBL,BLL";

        /// <summary>
        /// 资产负债年初数
        /// </summary>
        public static readonly string AssetsAndLiabilitiesOpeningAmountOfTheYear = "BLL.DAL.Management.FinancialAffairsManage.AssetsAndLiabilitiesOpeningAmountOfTheYearBL,BLL";

        /// <summary>
        /// 资产负债期末数
        /// </summary>
        public static readonly string AssetsAndLiabilitiesClosingAmount = "BLL.DAL.Management.FinancialAffairsManage.AssetsAndLiabilitiesClosingAmountBL,BLL";

        /// <summary>
        /// 资产负债表
        /// </summary>
        public static readonly string AssetsAndLiabilitiesReport = "BLL.DAL.Management.FinancialAffairsManage.AssetsAndLiabilitiesReportBL,BLL";


        #endregion

        /// <summary>
        /// 根據對應的Accessor接口 獲得對應的BLL
        /// </summary>
        /// <typeparam name="IAccessor"></typeparam>
        /// <param name="accessorFullName"></param>
        /// <returns></returns>
        public static IBLL GetBLL<IBLL>(string accessorFullName)
        {
            //動態創建實例類型 
            try
            {
                Type accessorType = Type.GetType(accessorFullName, false);
                return (IBLL)Activator.CreateInstance(accessorType);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
