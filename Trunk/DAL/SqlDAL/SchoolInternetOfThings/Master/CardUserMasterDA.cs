using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Management.Master;
using model = Model.Management.Master;
using LinqToSQLModel;
using DAL.SqlDAL.LocalLayer;
using Model.Management.Master;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using Model.General;


namespace DAL.SqlDAL.Management.Master
{
    class CardUserMasterDA : ICardUserMasterDA
    {

        #region IDataBaseCommandDA<CardUserMaster_cus_Info> Members

        public CardUserMaster_cus_Info GetRecord_First()
        {
            Model.Management.Master.CardUserMaster_cus_Info tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CardUserMaster_cus queryTab = db.CardUserMaster_cus.OrderBy(t => t.cus_iRecordID).FirstOrDefault();
                    tab = Common.General.CopyObjectValue<CardUserMaster_cus, CardUserMaster_cus_Info>(queryTab);
                    if (tab != null)
                    {
                        AddCardUserPhoneNum(tab);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return tab;
        }

        public CardUserMaster_cus_Info GetRecord_Last()
        {
            Model.Management.Master.CardUserMaster_cus_Info tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CardUserMaster_cus queryTab = db.CardUserMaster_cus.OrderByDescending(t => t.cus_iRecordID).FirstOrDefault();
                    tab = Common.General.CopyObjectValue<CardUserMaster_cus, CardUserMaster_cus_Info>(queryTab);
                    if (tab != null)
                    {
                        AddCardUserPhoneNum(tab);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return tab;
        }

        public CardUserMaster_cus_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            string RecordID = null;
            Model.Management.Master.CardUserMaster_cus_Info tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    if (commandInfo.KeyInfoList != null)
                    {
                        RecordID = commandInfo.KeyInfoList[0].KeyValue.ToString();
                        CardUserMaster_cus queryTab = db.CardUserMaster_cus.Where(t => t.cus_iRecordID < Convert.ToInt32(RecordID)).OrderByDescending(t => t.cus_iRecordID).FirstOrDefault();
                        tab = Common.General.CopyObjectValue<CardUserMaster_cus, CardUserMaster_cus_Info>(queryTab);
                        if (tab != null)
                        {
                            AddCardUserPhoneNum(tab);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return tab;
        }

        public CardUserMaster_cus_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Model.Management.Master.CardUserMaster_cus_Info tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    string RecordID = null;
                    if (commandInfo.KeyInfoList != null)
                    {
                        RecordID = commandInfo.KeyInfoList[0].KeyValue.ToString();
                        CardUserMaster_cus queryTab = db.CardUserMaster_cus.Where(t => t.cus_iRecordID > Convert.ToInt32(RecordID)).OrderBy(t => t.cus_iRecordID).FirstOrDefault();
                        tab = Common.General.CopyObjectValue<CardUserMaster_cus, CardUserMaster_cus_Info>(queryTab);
                        if (tab != null)
                        {
                            AddCardUserPhoneNum(tab);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return tab;
        }

        #endregion

        #region IMainDA<CardUserMaster_cus_Info> Members

        public bool InsertRecord(CardUserMaster_cus_Info infoObject)
        {
            CardUserMaster_cus tab = null;
            bool isSuccess = false;
            tab = Common.General.CopyObjectValue<CardUserMaster_cus_Info, CardUserMaster_cus>(infoObject);

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {

                    //插入亲情号码
                    CardUserPhoneNumMaster_cup newCup = new CardUserPhoneNumMaster_cup()
                    {
                        cup_CardUserNum = tab.cus_cNumber,
                        cup_phone1 = infoObject.CardUserPhoneNum.cup_Phone1,
                        cup_phone2 = infoObject.CardUserPhoneNum.cup_Phone2,
                        cup_phone3 = infoObject.CardUserPhoneNum.cup_Phone3,
                        cup_phone4 = infoObject.CardUserPhoneNum.cup_Phone4,
                        cup_data1 = "",
                        cup_data2 = ""
                    };

                    tab.CardUserPhoneNumMaster_cups.Add(newCup);
                    db.CardUserMaster_cus.InsertOnSubmit(tab);

                    if (infoObject.cus_cIdentityNum == Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student)
                    {
                        //添加班级信息
                        HandDelClassInfo(infoObject);
                    }


                    db.SubmitChanges();
                    isSuccess = true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public bool UpdateRecord(CardUserMaster_cus_Info infoObject)
        {
            CardUserMaster_cus tab = new CardUserMaster_cus();
            bool isSuccess = false;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    tab = db.CardUserMaster_cus.SingleOrDefault(t => t.cus_cNumber == infoObject.cus_cNumber);
                    if (tab != null)
                    {
                        tab.cus_cChaName = infoObject.cus_cChaName;
                        tab.cus_cEngName = infoObject.cus_cEngName;
                        tab.cus_cSexNum = infoObject.cus_cSexNum;
                        tab.cus_cIdentityNum = infoObject.cus_cIdentityNum;
                        tab.cus_cSchoolNum = infoObject.cus_cSchoolNum;
                        tab.cus_cDepartmentNum = infoObject.cus_cDepartmentNum;
                        tab.cus_cSpecialtyNum = infoObject.cus_cSpecialtyNum;
                        tab.cus_cGraduationPeriod = infoObject.cus_cGraduationPeriod;
                        tab.cus_cClassNum = infoObject.cus_cClassNum;
                        tab.cus_cSMSReceivePhone = infoObject.cus_cSMSReceivePhone;
                        tab.cus_lValid = infoObject.cus_lValid;
                        tab.cus_lIsSendSMS = infoObject.cus_lIsSendSMS;
                        tab.cus_lIsSendEmail = infoObject.cus_lIsSendEmail;
                        tab.cus_cMailAddress = infoObject.cus_cMailAddress;
                        tab.cus_imgPhoto = infoObject.cus_imgPhoto;
                        tab.cus_cDormitorySiteNum = infoObject.cus_cDormitorySiteNum;
                        tab.cus_cGroupNum = infoObject.cus_cGroupNum;
                        tab.cus_cPosition = infoObject.cus_cPosition;
                        tab.cus_cBedNum = infoObject.cus_cBedNum;//add by justinleung 2011/09/05

                        tab.cus_cAppendPhone1 = infoObject.cus_cAppendPhone1;
                        tab.cus_cAppendPhone2 = infoObject.cus_cAppendPhone2;
                        tab.cus_cAppendPhone3 = infoObject.cus_cAppendPhone3;

                        tab.cus_cStudentId = infoObject.cus_cStudentId;
                        tab.cus_cGotoSchoolType = infoObject.cus_cGotoSchoolType;
                        tab.cus_lCashPay = infoObject.cus_lCashPay;

                        CardUserPhoneNumMaster_cup cupTab = db.CardUserPhoneNumMaster_cup.SingleOrDefault(t => t.cup_CardUserNum == infoObject.cus_cNumber);
                        if (cupTab != null)
                        {
                            cupTab.cup_phone1 = infoObject.CardUserPhoneNum.cup_Phone1;
                            cupTab.cup_phone2 = infoObject.CardUserPhoneNum.cup_Phone2;
                            cupTab.cup_phone3 = infoObject.CardUserPhoneNum.cup_Phone3;
                            cupTab.cup_phone4 = infoObject.CardUserPhoneNum.cup_Phone4;
                        }
                        else
                        {


                            CardUserPhoneNumMaster_cup newTab = new CardUserPhoneNumMaster_cup()
                            {
                                cup_CardUserNum = infoObject.cus_cNumber,
                                cup_phone1 = infoObject.CardUserPhoneNum.cup_Phone1,
                                cup_phone2 = infoObject.CardUserPhoneNum.cup_Phone2,
                                cup_phone3 = infoObject.CardUserPhoneNum.cup_Phone3,
                                cup_phone4 = infoObject.CardUserPhoneNum.cup_Phone4,
                                cup_data1 = "",
                                cup_data2 = ""
                            };

                            db.CardUserPhoneNumMaster_cup.InsertOnSubmit(newTab);
                        }


                        db.SubmitChanges();
                        isSuccess = true;
                    }


                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            bool isSuccess = false;
            CardUserMaster_cus tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    tab = db.CardUserMaster_cus.SingleOrDefault(t => t.cus_iRecordID == KeyObject.RecordID);
                    if (tab != null)
                    {
                        IEnumerable<CourseMasterTeacher_cut> delTab =
                        from t in db.CourseMasterTeacher_cut
                        where t.cut_cTeacherNumber == tab.cus_cNumber
                        select t;

                        IEnumerable<CardUserPhoneNumMaster_cup> cupTab =
                            from t in db.CardUserPhoneNumMaster_cup
                            where t.cup_CardUserNum == tab.cus_cNumber
                            select t;

                        if (delTab.Count() > 0)
                        {
                            db.CourseMasterTeacher_cut.DeleteAllOnSubmit(delTab);
                        }
                        if (cupTab.Count() > 0)
                        {
                            db.CardUserPhoneNumMaster_cup.DeleteAllOnSubmit(cupTab);
                        }

                        db.CardUserMaster_cus.DeleteOnSubmit(tab);

                        db.SubmitChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public CardUserMaster_cus_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            model.CardUserMaster_cus_Info info = null;
            CardUserMaster_cus tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    info = new model.CardUserMaster_cus_Info();
                    tab = db.CardUserMaster_cus.SingleOrDefault(t => t.cus_iRecordID == (KeyObject as CardUserMaster_cus_Info).cus_iRecordID);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (tab != null)
            {
                info = Common.General.CopyObjectValue<CardUserMaster_cus, CardUserMaster_cus_Info>(tab);
                AddInfo(info);
                AddCardUserPhoneNum(info);
            }
            return info;
        }

        protected void AddCardUserPhoneNum(CardUserMaster_cus_Info info)
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CardUserPhoneNumMaster_cup cupTab = db.CardUserPhoneNumMaster_cup.SingleOrDefault(t => t.cup_CardUserNum == info.cus_cNumber);
                    if (cupTab != null)
                    {
                        info.CardUserPhoneNum = new CardUserPhoneNumMaster_cup_Info();
                        info.CardUserPhoneNum.cup_Phone1 = cupTab.cup_phone1;
                        info.CardUserPhoneNum.cup_Phone2 = cupTab.cup_phone2;
                        info.CardUserPhoneNum.cup_Phone3 = cupTab.cup_phone3;
                        info.CardUserPhoneNum.cup_Phone4 = cupTab.cup_phone4;
                    }

                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        protected void AddInfo(CardUserMaster_cus_Info info)
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt tab = null;
                    if (info.cus_cSexNum != "")
                    {
                        tab = db.CodeMaster_cmts.SingleOrDefault(t => t.cmt_cKey1 == Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserSex && t.cmt_cKey2 == info.cus_cSexNum);
                        if (tab != null)
                        {
                            info.cus_cSex = tab.cmt_cValue;
                        }
                    }
                    tab = null;
                    if (info.cus_cIdentityNum != "")
                    {
                        tab = db.CodeMaster_cmts.SingleOrDefault(t => t.cmt_cKey1 == Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserIdentity && t.cmt_cKey2 == info.cus_cIdentityNum);
                        if (tab != null)
                        {
                            info.cus_cIdentity = tab.cmt_cValue;
                        }
                    }

                    if (info.cus_cSchoolNum != "")
                    {
                        SchoolMaster_scm tab_school = db.SchoolMaster_scms.SingleOrDefault(t => t.scm_cNumber == info.cus_cSchoolNum);
                        if (tab_school != null)
                        {
                            info.cus_cSchool = tab_school.scm_cName;
                        }
                    }

                    if (info.cus_cDepartmentNum != "")
                    {
                        DepartmentMaster_dpm tab_department = db.DepartmentMaster_dpms.SingleOrDefault(t => t.dpm_cNumber == info.cus_cDepartmentNum);
                        if (tab_department != null)
                        {
                            info.cus_cDepartment = tab_department.dpm_cName;
                        }
                    }

                    if (info.cus_cSpecialtyNum != "")
                    {
                        SpecialtyMaster_spm tab_specialty = db.SpecialtyMaster_spms.SingleOrDefault(t => t.spm_cNumber == info.cus_cSpecialtyNum);
                        if (tab_specialty != null)
                        {
                            info.cus_cSpecialty = tab_specialty.spm_cName;
                        }
                    }
                    tab = null;

                    if (info.cus_cClassNum != "")
                    {
                        tab = db.CodeMaster_cmts.SingleOrDefault(t => t.cmt_cKey1 == Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserClass && t.cmt_cKey2 == info.cus_cClassNum);
                        if (tab != null)
                        {
                            info.cus_cClass = tab.cmt_cValue;
                        }
                    }


                    if (info.cus_cDormitorySiteNum != "")
                    {
                        SiteMaster_stm stm = db.SiteMaster_stms.FirstOrDefault(t => t.stm_cNumber == info.cus_cDormitorySiteNum);
                        if (stm != null)
                        {
                            BuildingMaster_bdm bdm = db.BuildingMaster_bdms.FirstOrDefault(t => t.bdm_cNumber == stm.stm_cBuildingNumber);
                            if (bdm != null)
                            {
                                info.cus_cDormitorySiteName = bdm.bdm_cName + "--" + stm.stm_cName;
                            }
                            else
                            {
                                info.cus_cDormitorySiteName = stm.stm_cName;
                            }

                        }
                    }

                    if (info.cus_cGroupNum != "")
                    {
                        UserActivityMonitorItemSettingGroup_msg msg = db.UserActivityMonitorItemSettingGroup_msgs.FirstOrDefault(t => t.msg_cNumber == info.cus_cGroupNum);
                        if (msg != null)
                        {
                            info.cus_cGroupName = msg.msg_cName;
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void HandDelClassInfo(CardUserMaster_cus_Info info)
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    ClassCourseSetting_ccs query = db.ClassCourseSetting_ccs.FirstOrDefault(t => t.ccs_cSchoolNum == info.cus_cSchoolNum && t.ccs_cGraduationPeriod == info.cus_cGraduationPeriod && t.ccs_cSpecialtyNum == info.cus_cSpecialtyNum && t.ccs_cClassNum == info.cus_cClassNum);

                    if (query == null)
                    {
                        ClassCourseSetting_ccs insertInfo = new ClassCourseSetting_ccs();

                        insertInfo.ccs_cSchoolNum = info.cus_cSchoolNum;

                        insertInfo.ccs_cSpecialtyNum = info.cus_cSpecialtyNum;

                        insertInfo.ccs_cGraduationPeriod = info.cus_cGraduationPeriod;

                        insertInfo.ccs_cClassNum = info.cus_cClassNum;

                        insertInfo.ccs_cSiteNum = "";

                        insertInfo.ccs_cCardUserNum = "";

                        insertInfo.ccs_lIsCourseCOWA = false;

                        insertInfo.ccs_lIsGoToSchoolCOWA = false;

                        insertInfo.ccs_lValid = true;

                        insertInfo.ccs_cRemark = "";

                        insertInfo.ccs_dAddDate = DateTime.Now;

                        insertInfo.ccs_dLastDate = DateTime.Now;

                        insertInfo.ccs_cAdd = "sa";

                        insertInfo.ccs_cLast = "sa";

                        db.ClassCourseSetting_ccs.InsertOnSubmit(insertInfo);

                        db.SubmitChanges();

                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public List<CardUserMaster_cus_Info> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            model.CardUserMaster_cus_Info info = null;
            info = MDobject as model.CardUserMaster_cus_Info;

            string sqlString = string.Empty;
            string whereString = string.Empty;
            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += "cus.cus_iRecordID," + Environment.NewLine;
            sqlString += "cus.cus_cStudentId," + Environment.NewLine;
            sqlString += "cus.cus_cNumber," + Environment.NewLine;
            sqlString += "cus.cus_cChaName," + Environment.NewLine;
            sqlString += "cus.cus_cEngName," + Environment.NewLine;
            sqlString += "cmt1.cmt_cValue AS cus_cSexNum," + Environment.NewLine;
            sqlString += "cmt2.cmt_cValue AS cus_cIdentityNum, " + Environment.NewLine;
            sqlString += "scm.scm_cName AS cus_cSchoolNum," + Environment.NewLine;
            sqlString += "dpm.dpm_cName AS cus_cDepartmentNum," + Environment.NewLine;
            sqlString += "cmt3.cmt_cValue AS cus_cClassNum," + Environment.NewLine;
            sqlString += "spm.spm_cName AS cus_cSpecialtyNum," + Environment.NewLine;
            sqlString += "cus.cus_cGraduationPeriod," + Environment.NewLine;
            sqlString += "cus.cus_cSMSReceivePhone," + Environment.NewLine;
            sqlString += "cus.cus_cMailAddress," + Environment.NewLine;
            sqlString += "cus.cus_lIsSendSMS," + Environment.NewLine;
            sqlString += "cus.cus_lIsSendEmail," + Environment.NewLine;
            sqlString += "cus.cus_lValid," + Environment.NewLine;
            sqlString += "cus.cus_guidPhotoKey," + Environment.NewLine;
            sqlString += "ats.ats_cName AS cus_AbsenceTypeName," + Environment.NewLine;
            sqlString += "Convert(nvarchar(100), arg.arg_iRecordID) AS cus_AbsenceRecordID," + Environment.NewLine;
            sqlString += " CONVERT(binary,'') AS byte_cus_imgPhoto," + Environment.NewLine;
            //justinleung 2011/08/31 增加宿舍地點
            sqlString += " cus.cus_cDormitorySiteNum AS cus_cDormitorySiteNum, " + Environment.NewLine;
            sqlString += " cus.cus_cPosition," + Environment.NewLine;
            sqlString += " bdm.bdm_cName+'--'+stm.stm_cName AS cus_cDormitorySiteName," + Environment.NewLine;
            sqlString += " cus.cus_cBedNum AS cus_cBedNum, " + Environment.NewLine;//add by justinleung 2011/09/05
            sqlString += " ISNULL(mgp.msg_cName,'') AS cus_cGroupNum, " + Environment.NewLine;

            #region Ximon 2011/11/06  导出学生数据使用
            sqlString += "cus.cus_cAppendPhone1," + Environment.NewLine;
            sqlString += "cus.cus_cAppendPhone2," + Environment.NewLine;
            sqlString += "cus.cus_cAppendPhone3," + Environment.NewLine;
            sqlString += "cup.cup_phone1 as cellphone1," + Environment.NewLine;
            sqlString += "cup.cup_phone2 as cellphone2," + Environment.NewLine;
            sqlString += "cup.cup_phone3 as cellphone3," + Environment.NewLine;
            sqlString += "cup.cup_phone4 as cellphone4," + Environment.NewLine;
            sqlString += "ISNULL(cmt4.cmt_cValue,'') as cus_cGotoSchoolType," + Environment.NewLine;
            sqlString += "cus.cus_lCashPay" + Environment.NewLine;
            #endregion

            sqlString += "FROM CardUserMaster_cus AS cus" + Environment.NewLine;
            sqlString += "LEFT JOIN CodeMaster_cmt cmt1" + Environment.NewLine;
            sqlString += "ON cus.cus_cSexNum=cmt1.cmt_cKey2" + Environment.NewLine;
            sqlString += "LEFT JOIN CodeMaster_cmt cmt2" + Environment.NewLine;
            sqlString += "ON cus.cus_cIdentityNum=cmt2.cmt_cKey2 and cmt2.cmt_cKey1='" + Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserIdentity + "'" + Environment.NewLine;
            sqlString += "LEFT JOIN SchoolMaster_scm AS scm" + Environment.NewLine;
            sqlString += "ON cus.cus_cSchoolNum=scm.scm_cNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN DepartmentMaster_dpm AS dpm" + Environment.NewLine;
            sqlString += "ON cus.cus_cDepartmentNum=dpm.dpm_cNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN CodeMaster_cmt AS cmt3" + Environment.NewLine;
            sqlString += "ON cus.cus_cClassNum=cmt3.cmt_cKey2" + Environment.NewLine;
            sqlString += "LEFT JOIN SpecialtyMaster_spm AS spm" + Environment.NewLine;
            sqlString += "ON cus.cus_cSpecialtyNum=spm.spm_cNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN dbo.AbsenceRegister_arg AS arg" + Environment.NewLine;
            sqlString += "ON cus.cus_cNumber=arg.arg_cCardUserNum" + Environment.NewLine;
            sqlString += "AND '" + info.cus_QueryDate + "' BETWEEN Convert(varchar(100), arg.arg_dStartDateTime,111) AND Convert(varchar(100),arg.arg_dEndDateTime,111)" + Environment.NewLine;
            sqlString += "LEFT JOIN dbo.AbsenceTypeSetting_ats AS ats" + Environment.NewLine;
            sqlString += "ON arg.arg_cATSNumber=ats.ats_cNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN dbo.UserActivityMonitorItemSettingGroup_msg AS mgp" + Environment.NewLine;
            sqlString += "ON mgp.msg_cNumber=cus.cus_cGroupNum" + Environment.NewLine;
            sqlString += "LEFT JOIN dbo.SiteMaster_stm AS stm" + Environment.NewLine;
            sqlString += "ON cus.cus_cDormitorySiteNum=stm.stm_cNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN dbo.BuildingMaster_bdm AS bdm" + Environment.NewLine;
            sqlString += "ON bdm.bdm_cNumber=stm.stm_cBuildingNumber" + Environment.NewLine;

            #region Ximon 2011/11/06  导出学生数据使用
            sqlString += "LEFT JOIN dbo.CardUserPhoneNumMaster_cup AS cup" + Environment.NewLine;
            sqlString += "ON cup.cup_CardUserNum=cus.cus_cNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN dbo.CodeMaster_cmt AS cmt4" + Environment.NewLine;
            sqlString += "ON cus.cus_cGotoSchoolType=cmt4.cmt_cKey2 AND cmt4.cmt_cKey1='" + Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_GOTOSCHOOLTYPE + "'" + Environment.NewLine;

            #endregion

            if (info != null)
            {
                whereString = " WHERE 1=1 " + Environment.NewLine;
                if (info.cus_cNumber != null)
                {
                    if (info.cus_cNumber.ToString().Contains("*") || info.cus_cNumber.ToString().Contains("?"))
                    {
                        whereString += "AND (( cus.cus_cNumber LIKE N'" + info.cus_cNumber.ToString().Replace("*", "%").Replace("?", "_") + "' ) or (" + "cus.cus_cStudentId LIKE N'" + info.cus_cNumber.ToString().Replace("*", "%").Replace("?", "_") + "'" + "))" + Environment.NewLine;


                    }
                    else
                    {
                        whereString += "AND ((cus.cus_cNumber LIKE N'%" + info.cus_cNumber.ToString() + "%') or (" + "cus.cus_cStudentId LIKE N'%" + info.cus_cNumber.ToString() + "%'" + ") )" + Environment.NewLine;
                    }
                }

                if (info.cus_cStudentId != null)
                {
                    if (info.cus_cStudentId.ToString().Contains("*") || info.cus_cStudentId.ToString().Contains("?"))
                    {
                        whereString += "AND ( cus.cus_cStudentId LIKE N'" + info.cus_cStudentId.ToString().Replace("*", "%").Replace("?", "_") + "' ) " + Environment.NewLine;


                    }
                    else
                    {
                        whereString += "AND ((cus.cus_cStudentId LIKE N'%" + info.cus_cStudentId.ToString() + "%') )" + Environment.NewLine;
                    }
                }

                if (info.cus_cChaName != null)
                {
                    if (info.cus_cChaName.ToString().Contains("*") || info.cus_cChaName.ToString().Contains("?"))
                    {
                        whereString += "AND cus.cus_cChaName LIKE N'" + info.cus_cChaName.ToString().Replace("*", "%").Replace("?", "_") + "'" + Environment.NewLine;
                    }
                    else
                    {
                        whereString += "AND cus.cus_cChaName LIKE N'%" + info.cus_cChaName.ToString() + "%'" + Environment.NewLine;
                    }
                }

                if (info.cus_cEngName != null)
                {
                    if (info.cus_cEngName.ToString().Contains("*") || info.cus_cEngName.ToString().Contains("?"))
                    {
                        whereString += "AND cus.cus_cEngName LIKE N'" + info.cus_cEngName.ToString().Replace("*", "%").Replace("?", "_") + "'" + Environment.NewLine;
                    }
                    else
                    {
                        whereString += "AND cus.cus_cEngName LIKE N'%" + info.cus_cEngName.ToString() + "%'" + Environment.NewLine;
                    }
                }

                if (info.cus_cGraduationPeriod != null)
                {
                    if (info.cus_cGraduationPeriod.ToString().Contains("*") || info.cus_cGraduationPeriod.ToString().Contains("?"))
                    {
                        whereString += "AND cus.cus_cGraduationPeriod LIKE N'" + info.cus_cGraduationPeriod.ToString().Replace("*", "%").Replace("?", "_") + "'" + Environment.NewLine;
                    }
                    else
                    {
                        whereString += "AND cus.cus_cGraduationPeriod LIKE N'%" + info.cus_cGraduationPeriod.ToString() + "%'" + Environment.NewLine;
                    }
                }

                if (info.cus_cDepartmentNum != null && info.cus_cDepartmentNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cDepartmentNum ='" + info.cus_cDepartmentNum.ToString() + "'" + Environment.NewLine;
                }

                if (info.cus_cSpecialtyNum != null && info.cus_cSpecialtyNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cSpecialtyNum ='" + info.cus_cSpecialtyNum.ToString() + "'" + Environment.NewLine;
                }

                if (info.cus_cClassNum != null && info.cus_cClassNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cClassNum ='" + info.cus_cClassNum.ToString() + "'" + Environment.NewLine;
                }

                if (info.cus_cIdentityNum != null && info.cus_cIdentityNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cIdentityNum ='" + info.cus_cIdentityNum.ToString() + "'" + Environment.NewLine;
                }

                if (info.cus_cSchoolNum != null && info.cus_cSchoolNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cSchoolNum ='" + info.cus_cSchoolNum.ToString() + "'" + Environment.NewLine;
                }

                if (info.cus_cSexNum != null && info.cus_cSexNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cSexNum ='" + info.cus_cSexNum.ToString() + "'" + Environment.NewLine;
                }

                if (info.cus_lValid)
                {
                    whereString += "AND cus.cus_lValid = 1" + Environment.NewLine;
                }
            }

            if (whereString != null)
            {
                sqlString += whereString;
            }
            sqlString += "ORDER BY cus.cus_cStudentId asc";


            List<model.CardUserMaster_cus_Info> infoList = new List<CardUserMaster_cus_Info>();


            IEnumerable<CardUserMaster_cus_Info> cus_infos = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {


                    cus_infos = db.ExecuteQuery<CardUserMaster_cus_Info>(sqlString, new object[] { }).ToList();
                    var courseMasterTeacher_cuts = db.CourseMasterTeacher_cut.Where(d => cus_infos.Select(s => s.cus_cNumber).Contains(d.cut_cTeacherNumber));
                    var data = courseMasterTeacher_cuts.ToList();
                    foreach (CardUserMaster_cus_Info item in cus_infos)
                    {
                        infoList.Add(item);
                    }
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        #endregion

        #region IExtraDA Members

        public bool IsExistRecord(object KeyObject)
        {
            CardUserMaster_cus info = null;
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                info = db.CardUserMaster_cus.SingleOrDefault(t => t.cus_cNumber == KeyObject.ToString().Trim());
                if (info != null)
                {
                    return true;
                }
            }
            return false;
        }

        //public bool IsLockRecord(object KeyObject)
        //{
        //    return true;
        //}

        //public bool IsUnLockRecord(object KeyObject)
        //{
        //    return true;
        //}

        public Model.IModel.IModelObject GetTableFieldLenght()
        {
            LocalGeneral general = new LocalGeneral();
            CardUserMaster_cus_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<CardUserMaster_cus_Info>("CardUserMaster_cus");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region IExtraDA Members


        public Model.General.ReturnValueInfo LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IExtraDA Members


        public bool IsMyLockedRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICardUserMasterDA Members

        public int GetPersonnelsCount(string Type)
        {
            string SQLWhere = string.Empty;

            if (Type == "TEACHER")
            {
                SQLWhere = "STAFF";
            }
            else if (Type == "STUDENT")
            {
                SQLWhere = "STUDENT";
            }


            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    return db.CardUserMaster_cus.Count(t => t.cus_cIdentityNum == SQLWhere);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return 0;
        }

        #endregion


        public CardUserMaster_cus_Info CheckCardNum(CardUserMaster_cus_Info info)
        {
            string sqlString = string.Empty;
            string sqlWhere = string.Empty;
            sqlString += "SELECT rcm_cCardHFID AS cus_cEngName,cus_cNumber," + Environment.NewLine;
            sqlString += " CONVERT(binary,'') AS byte_cus_imgPhoto " + Environment.NewLine;
            sqlString += " FROM CardUserMaster_cus" + Environment.NewLine;
            sqlString += " LEFT JOIN RFIDCardIssuance_rci" + Environment.NewLine;
            sqlString += " ON rci_cCardUserNum=cus_cNumber" + Environment.NewLine;

            sqlString += " LEFT JOIN RFIDCardManage_rcm" + Environment.NewLine;
            sqlString += " ON rci_cCardNum=rcm_cCardNum" + Environment.NewLine;

            if (info != null)
            {
                sqlWhere = " WHERE 1=1 AND " + Environment.NewLine;
                if (info.cus_cNumber != null)
                {
                    sqlWhere += " cus_cNumber='" + info.cus_cNumber.ToString() + "'" + Environment.NewLine;
                }
            }
            sqlString += sqlWhere;

            IEnumerable<model.CardUserMaster_cus_Info> infos = null;
            List<model.CardUserMaster_cus_Info> infoList = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<model.CardUserMaster_cus_Info>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        infoList = infos.ToList<model.CardUserMaster_cus_Info>();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList[0];
        }

        /// <summary>
        /// 更新卡用户亲情号码


        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ReturnValueInfo UpdataCardUserPhoneNum(CardUserMaster_cus_Info info)
        {
            ReturnValueInfo ReturnValue = new ReturnValueInfo();
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                CardUserPhoneNumMaster_cup tab = db.CardUserPhoneNumMaster_cup.SingleOrDefault(t => t.cup_CardUserNum == info.cus_cNumber);
                if (tab != null)
                {
                    tab.cup_phone1 = info.CardUserPhoneNum.cup_Phone1;
                    tab.cup_phone2 = info.CardUserPhoneNum.cup_Phone2;
                    tab.cup_phone3 = info.CardUserPhoneNum.cup_Phone3;
                    tab.cup_phone4 = info.CardUserPhoneNum.cup_Phone4;
                }
                else
                {
                    CardUserPhoneNumMaster_cup NewTab = new CardUserPhoneNumMaster_cup()
                    {
                        cup_CardUserNum = info.cus_cNumber,
                        cup_data1 = "",
                        cup_data2 = "",
                        cup_phone1 = info.CardUserPhoneNum.cup_Phone1,
                        cup_phone2 = info.CardUserPhoneNum.cup_Phone2,
                        cup_phone3 = info.CardUserPhoneNum.cup_Phone3,
                        cup_phone4 = info.CardUserPhoneNum.cup_Phone4
                    };

                    db.CardUserPhoneNumMaster_cup.InsertOnSubmit(NewTab);
                }

                db.SubmitChanges();
            }


            return ReturnValue;
        }

        #region ICardUserMasterDA Members


        public List<CardUserMaster_cus_Info> ClassSearch(CardUserMaster_cus_Info info)
        {
            //throw new NotImplementedException();            

            string sqlString = string.Empty;
            string whereString = string.Empty;
            sqlString = "SELECT DISTINCT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            //sqlString += "cus.cus_iRecordID," + Environment.NewLine;
            ////sqlString += "cus_cChaName," + Environment.NewLine;
            ////sqlString += "rci_cCardNum AS cus_cEngName," + Environment.NewLine;
            //sqlString += "cus.cus_cEngName," + Environment.NewLine;
            //sqlString += "cmt1.cmt_cValue AS cus_cSexNum," + Environment.NewLine;
            //sqlString += "cmt2.cmt_cValue AS cus_cIdentityNum, " + Environment.NewLine;
            sqlString += "scm.scm_cNumber AS cus_cNumber," + Environment.NewLine;
            sqlString += "scm.scm_cName AS cus_cSchoolNum," + Environment.NewLine;

            sqlString += "dpm.dpm_cNumber AS cus_cChaName," + Environment.NewLine;
            sqlString += "dpm.dpm_cName AS cus_cDepartmentNum," + Environment.NewLine;

            sqlString += "cmt3.cmt_cKey2 AS cus_cEngName," + Environment.NewLine;
            sqlString += "cmt3.cmt_cValue AS cus_cClassNum," + Environment.NewLine;

            sqlString += "spm.spm_cNumber AS cus_cIdentityNum," + Environment.NewLine;
            sqlString += "spm.spm_cName AS cus_cSpecialtyNum," + Environment.NewLine;

            sqlString += "cus.cus_cGraduationPeriod," + Environment.NewLine;
            //sqlString += "cus.cus_cSMSReceivePhone," + Environment.NewLine;
            //sqlString += "cus.cus_cMailAddress," + Environment.NewLine;
            //sqlString += "cus.cus_lIsSendSMS," + Environment.NewLine;
            //sqlString += "cus.cus_lIsSendEmail," + Environment.NewLine;
            //sqlString += "ats.ats_cName AS cus_AbsenceTypeName," + Environment.NewLine;
            //sqlString += "Convert(nvarchar(100), arg.arg_iRecordID) AS cus_AbsenceRecordID," + Environment.NewLine;
            sqlString += " CONVERT(binary,'') AS byte_cus_imgPhoto " + Environment.NewLine;
            //cus.cus_imgPhoto
            sqlString += "FROM CardUserMaster_cus AS cus" + Environment.NewLine;


            ////sqlString += "LEFT JOIN RFIDCardIssuance_rci" + Environment.NewLine;
            ////sqlString += "ON rci_cCardUserNum=cus_cNumber" + Environment.NewLine;
            ////sqlString += "LEFT JOIN RFIDCardManage_rcm" + Environment.NewLine;
            ////sqlString += "ON rci_cCardNum=rcm_cCardNum" + Environment.NewLine;


            sqlString += "LEFT JOIN CodeMaster_cmt cmt1" + Environment.NewLine;
            sqlString += "ON cus.cus_cSexNum=cmt1.cmt_cKey2" + Environment.NewLine;
            sqlString += "LEFT JOIN CodeMaster_cmt cmt2" + Environment.NewLine;
            sqlString += "ON cus.cus_cIdentityNum=cmt2.cmt_cKey2" + Environment.NewLine;
            sqlString += "LEFT JOIN SchoolMaster_scm AS scm" + Environment.NewLine;
            sqlString += "ON cus.cus_cSchoolNum=scm.scm_cNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN DepartmentMaster_dpm AS dpm" + Environment.NewLine;
            sqlString += "ON cus.cus_cDepartmentNum=dpm.dpm_cNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN CodeMaster_cmt AS cmt3" + Environment.NewLine;
            sqlString += "ON cus.cus_cClassNum=cmt3.cmt_cKey2" + Environment.NewLine;
            sqlString += "LEFT JOIN SpecialtyMaster_spm AS spm" + Environment.NewLine;
            sqlString += "ON cus.cus_cSpecialtyNum=spm.spm_cNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN dbo.AbsenceRegister_arg AS arg" + Environment.NewLine;
            sqlString += "ON cus.cus_cNumber=arg.arg_cCardUserNum" + Environment.NewLine;
            sqlString += "AND '" + info.cus_QueryDate + "' BETWEEN Convert(varchar(100), arg.arg_dStartDateTime,111) AND Convert(varchar(100),arg.arg_dEndDateTime,111)" + Environment.NewLine;
            sqlString += "LEFT JOIN dbo.AbsenceTypeSetting_ats AS ats" + Environment.NewLine;
            sqlString += "ON arg.arg_cATSNumber=ats.ats_cNumber" + Environment.NewLine;



            if (info != null)
            {
                whereString = " WHERE 1=1 " + Environment.NewLine;

                if (info.cus_cGraduationPeriod != null)
                {
                    if (info.cus_cGraduationPeriod.ToString().Contains("*") || info.cus_cGraduationPeriod.ToString().Contains("?"))
                    {
                        whereString += "AND cus.cus_cGraduationPeriod LIKE N'" + info.cus_cGraduationPeriod.ToString().Replace("*", "%").Replace("?", "_") + "'" + Environment.NewLine;
                    }
                    else
                    {
                        whereString += "AND cus.cus_cGraduationPeriod LIKE N'%" + info.cus_cGraduationPeriod.ToString() + "%'" + Environment.NewLine;
                    }
                }

                if (info.cus_cDepartmentNum != null && info.cus_cDepartmentNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cDepartmentNum ='" + info.cus_cDepartmentNum.ToString() + "'" + Environment.NewLine;
                }

                if (info.cus_cSpecialtyNum != null && info.cus_cSpecialtyNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cSpecialtyNum ='" + info.cus_cSpecialtyNum.ToString() + "'" + Environment.NewLine;
                }

                if (info.cus_cClassNum != null && info.cus_cClassNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cClassNum ='" + info.cus_cClassNum.ToString() + "'" + Environment.NewLine;
                }

                if (info.cus_cSchoolNum != null && info.cus_cSchoolNum.ToString() != "")
                {
                    whereString += "AND cus.cus_cSchoolNum ='" + info.cus_cSchoolNum.ToString() + "'" + Environment.NewLine;
                }
            }

            if (whereString != null)
            {
                sqlString += whereString;
            }
            //sqlString += "ORDER BY cus.cus_iRecordID asc";

            IEnumerable<model.CardUserMaster_cus_Info> infos = null;
            List<model.CardUserMaster_cus_Info> infoList = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<model.CardUserMaster_cus_Info>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        infoList = infos.ToList<model.CardUserMaster_cus_Info>();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }


        public Model.General.ReturnValueInfo UpdateMonitorItemGroup(string groupNum, int cardUserId)
        {
            //throw new NotImplementedException();
            string sqlString = "update CardUserMaster_cus set cus_cGroupNum={0} where cus_iRecordID={1}";
            Model.General.ReturnValueInfo rinfo = new Model.General.ReturnValueInfo();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    db.ExecuteCommand(sqlString, new object[] { groupNum, cardUserId });
                }
                rinfo.boolValue = true;
            }
            catch (Exception ex)
            {
                rinfo.boolValue = false;
                rinfo.messageText = ex.Message;
                // throw Ex;
            }
            return rinfo;
        }


        #endregion

        #region ICardUserMasterDA Members


        public List<CardUserMaster_cus_Info> ExportClassInfo(CardUserMaster_cus_Info info)
        {
            List<CardUserMaster_cus_Info> list = null;
            string sqlString = string.Empty;

            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + " cus_cNumber," + Environment.NewLine;
            sqlString += "cus_cStudentId," + Environment.NewLine;
            sqlString += "cus_cChaName," + Environment.NewLine;
            sqlString += "cus_cEngName," + Environment.NewLine;
            sqlString += "cmt1.cmt_cValue as cus_cSex," + Environment.NewLine;
            sqlString += "scm_cName as cus_cSchool," + Environment.NewLine;
            sqlString += "spm_cName as cus_cSpecialty," + Environment.NewLine;
            sqlString += "cus_cGraduationPeriod," + Environment.NewLine;
            sqlString += "cmt2.cmt_cValue as cus_cClass" + Environment.NewLine;
            sqlString += "from dbo.CardUserMaster_cus" + Environment.NewLine;
            sqlString += "left join dbo.CodeMaster_cmt as cmt1" + Environment.NewLine;
            sqlString += "on cmt1.cmt_cKey1='" + Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserSex + "' and cmt1.cmt_cKey2=cus_cSexNum" + Environment.NewLine;
            sqlString += "left join dbo.SchoolMaster_scm" + Environment.NewLine;
            sqlString += "on cus_cSchoolNum=scm_cNumber" + Environment.NewLine;
            sqlString += "left join dbo.SpecialtyMaster_spm" + Environment.NewLine;
            sqlString += "on cus_cSpecialtyNum=spm_cNumber" + Environment.NewLine;
            sqlString += "left join dbo.CodeMaster_cmt as cmt2" + Environment.NewLine;
            sqlString += "on cmt2.cmt_cKey1='" + Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserClass + "' and cmt2.cmt_cKey2=cus_cClassNum" + Environment.NewLine;
            sqlString += "where cus_cIdentityNum='" + Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student + "'" + Environment.NewLine;

            if (info.cus_cSchoolNum != null && info.cus_cSchoolNum != "")
            {
                sqlString += "and cus_cSchoolNum='" + info.cus_cSchoolNum + "'" + Environment.NewLine;
            }
            if (info.cus_cGraduationPeriod != null && info.cus_cGraduationPeriod != "")
            {
                sqlString += "and cus_cGraduationPeriod='" + info.cus_cGraduationPeriod + "'" + Environment.NewLine;
            }
            if (info.cus_cSpecialtyNum != null && info.cus_cSpecialtyNum != "")
            {
                sqlString += "and cus_cSpecialtyNum='" + info.cus_cSpecialtyNum + "'" + Environment.NewLine;
            }
            if (info.cus_cClassNum != null && info.cus_cClassNum != "")
            {
                sqlString += "and cus_cClassNum='" + info.cus_cClassNum + "'" + Environment.NewLine;
            }



            IEnumerable<CardUserMaster_cus_Info> cus_infos = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {


                    cus_infos = db.ExecuteQuery<CardUserMaster_cus_Info>(sqlString, new object[] { }).ToList();
                    list = new List<CardUserMaster_cus_Info>();
                    foreach (CardUserMaster_cus_Info item in cus_infos)
                    {

                        list.Add(item);
                    }
                }

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return list;
        }

        public bool UpdateCardUserClass(CardUserMaster_cus_Info info)
        {
            bool isSuccess = false;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CardUserMaster_cus tab = db.CardUserMaster_cus.FirstOrDefault(t => t.cus_cNumber == info.cus_cNumber);
                    if (tab != null)
                    {
                        SchoolMaster_scm school = db.SchoolMaster_scms.FirstOrDefault(t => t.scm_cName == info.cus_cSchool);
                        if (school != null)
                        {
                            tab.cus_cSchoolNum = school.scm_cNumber;
                        }

                        SpecialtyMaster_spm specialty = db.SpecialtyMaster_spms.FirstOrDefault(t => t.spm_cName == info.cus_cSpecialty);
                        if (specialty != null)
                        {
                            tab.cus_cSpecialtyNum = specialty.spm_cNumber;
                        }

                        CodeMaster_cmt CardUserclass = db.CodeMaster_cmts.FirstOrDefault(t => t.cmt_cKey1 == Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserClass && t.cmt_cValue == info.cus_cClass);
                        if (CardUserclass != null)
                        {
                            tab.cus_cClassNum = CardUserclass.cmt_cKey2;
                        }
                        tab.cus_cStudentId = info.cus_cStudentId;
                        tab.cus_dLastDate = DateTime.Now;
                        tab.cus_cLast = info.cus_cLast;

                        db.SubmitChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }


            return isSuccess;
        }

        public bool HandelCardBind(List<CardUserMaster_cus_Info> studentList)
        {
            bool isSuccess = false;
            if (studentList != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        foreach (CardUserMaster_cus_Info item in studentList)
                        {
                            CardUserMaster_cus cus = db.CardUserMaster_cus.FirstOrDefault(t => t.cus_cNumber == item.cus_cNumber);
                            if (cus != null)
                            {
                                cus.cus_lValid = false;

                                RFIDCardIssuance_rci rci = db.RFIDCardIssuance_rcis.FirstOrDefault(t => t.rci_cCardUserNum == item.cus_cNumber);
                                if (rci != null)
                                {
                                    RFIDCardManage_rcm rcm = db.RFIDCardManage_rcms.FirstOrDefault(t => t.rcm_cCardNum == rci.rci_cCardNum);
                                    if (rcm != null)
                                    {
                                        if (rcm.rcm_cStatusNum == Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_RFIDCARDUSESTATUS_USING)
                                        {
                                            rcm.rcm_cStatusNum = Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_RFIDCARDUSESTATUS_UNUSED;
                                        }
                                        db.RFIDCardIssuance_rcis.DeleteOnSubmit(rci);
                                    }
                                }

                                db.SubmitChanges();

                            }

                        }
                        isSuccess = true;
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }


            return isSuccess;
        }

        #endregion
    }
}
