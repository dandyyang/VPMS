using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.General;
using LinqToSQLModel;
using System.Data.Linq.SqlClient;
using Model.Management.Master;
using Common;
using Model.General;

namespace DAL.SqlDAL.General
{
    class GeneralDA : IGeneralDA
    {

        #region IGeneralDA Members

        public List<Model.General.ComboboxDataInfo> GetMasterDataInformations(Common.DefineConstantValue.MasterType masterType)
        {
            List<Model.General.ComboboxDataInfo> infoList = null;

            try
            {
                switch (masterType)
                {
                    case DefineConstantValue.MasterType.WebMessage:
                        infoList = GetMessageInfo();
                        break;
                    case Common.DefineConstantValue.MasterType.BuildingMaster:
                        infoList = GetBuildingMaster();
                        break;

                    case DefineConstantValue.MasterType.SchoolMaster:
                        infoList = GetSchoolMaster();
                        break;

                    case DefineConstantValue.MasterType.SpecialtyMaster:
                        infoList = GetSpecialtyMaster();
                        break;

                    case DefineConstantValue.MasterType.DepartmentMaster:
                        infoList = GetDepartmentMaster();
                        break;
                    case DefineConstantValue.MasterType.SiteMaster:
                        infoList = GetSiteMaster();
                        break;
                    case DefineConstantValue.MasterType.BuildingSiteMaster:
                        infoList = GetBuildSiteMaster();
                        break;

                    case DefineConstantValue.MasterType.CourseMaster:
                        infoList = GetCourseMaster();
                        break;

                    case DefineConstantValue.MasterType.SIOT_AMS_INTRX:
                        infoList = GetCodeMaster(DefineConstantValue.CodeMasterDefine.KEY1_SIOT_AMS_INTRX);
                        break;
                    case DefineConstantValue.MasterType.SIOT_AMS_OUTTRX:
                        infoList = GetCodeMaster(DefineConstantValue.CodeMasterDefine.KEY1_SIOT_AMS_OUTTRX);
                        break;
                    case DefineConstantValue.MasterType.WinFormSkin:
                        infoList = GetWinFormSkin();
                        break;
                    case DefineConstantValue.MasterType.CodeMaster_Key1:
                        infoList = GetCodeMaster_Key1();
                        break;

                    case DefineConstantValue.MasterType.SupplierMaster:

                        infoList = GetSupplierMaster();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return infoList;
        }

        private List<ComboboxDataInfo> GetFinancialNetassets()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<ArticleTypeDefine_atd> Query = from t in db.ArticleTypeDefine_atds

                                                               where t.atd_cParentRecordNum == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_NETASSETS && t.atd_lValid == true

                                                               select t;

                    if (Query != null && Query.Count() > 0)
                    {
                        foreach (ArticleTypeDefine_atd item in Query)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = "[" + item.atd_cTypeNum + "] " + item.atd_cDescript.ToString();
                            li.ValueMember = item.atd_cTypeNum.ToString();
                            list.Add(li);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetFinancialLiabilities()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<ArticleTypeDefine_atd> Query = from t in db.ArticleTypeDefine_atds

                                                               where t.atd_cParentRecordNum == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_LIABILITIES && t.atd_lValid == true

                                                               select t;

                    if (Query != null && Query.Count() > 0)
                    {
                        foreach (ArticleTypeDefine_atd item in Query)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = "[" + item.atd_cTypeNum + "] " + item.atd_cDescript.ToString();
                            li.ValueMember = item.atd_cTypeNum.ToString();
                            list.Add(li);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetFinancialProperty()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<ArticleTypeDefine_atd> Query = from t in db.ArticleTypeDefine_atds

                                                               where t.atd_cParentRecordNum == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_PROPERTY && t.atd_lValid == true

                                                               select t;

                    if (Query != null && Query.Count() > 0)
                    {
                        foreach (ArticleTypeDefine_atd item in Query)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = "[" + item.atd_cTypeNum + "] " + item.atd_cDescript.ToString();
                            li.ValueMember = item.atd_cTypeNum.ToString();
                            list.Add(li);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetFinancialRevenueType()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<ArticleTypeDefine_atd> Query = from t in db.ArticleTypeDefine_atds

                                                               where t.atd_cParentRecordNum == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCIALREVENUE && t.atd_lValid == true

                                                               select t;

                    if (Query != null && Query.Count() > 0)
                    {
                        foreach (ArticleTypeDefine_atd item in Query)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.atd_cDescript.ToString();
                            li.ValueMember = item.atd_cTypeNum.ToString();
                            list.Add(li);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetFinancialAffairsPayType()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<ArticleTypeDefine_atd> Query = from t in db.ArticleTypeDefine_atds

                                                               where t.atd_cParentRecordNum == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCEEXPENDITURE && t.atd_lValid == true

                                                               select t;

                    if (Query != null && Query.Count() > 0)
                    {
                        foreach (ArticleTypeDefine_atd item in Query)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.atd_cDescript.ToString();
                            li.ValueMember = item.atd_cTypeNum.ToString();
                            list.Add(li);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetSupplierMaster()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<SupplierMaster_slm> Query = from t in db.SupplierMaster_slms
                                                            orderby t.slm_iRecordID descending
                                                            select t;

                    if (Query != null && Query.Count() > 0)
                    {
                        foreach (SupplierMaster_slm item in Query)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.slm_cChinaName.ToString();
                            li.ValueMember = item.slm_cClientNum.ToString();
                            list.Add(li);
                        }
                    }


                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetDailyPayType()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<ArticleTypeDefine_atd> Query = from t in db.ArticleTypeDefine_atds

                                                               where t.atd_cParentRecordNum == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_ARTICLETYPEDEFINE_FINANCEEXPENDITURE && t.atd_cSysNum == "" && t.atd_lValid == true

                                                               select t;

                    if (Query != null && Query.Count() > 0)
                    {
                        foreach (ArticleTypeDefine_atd item in Query)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.atd_cDescript.ToString();
                            li.ValueMember = item.atd_cTypeNum.ToString();
                            list.Add(li);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetGoToSchoolType()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<CodeMaster_cmt> Query = from t in db.CodeMaster_cmts
                                                        where t.cmt_cKey1 == DefineConstantValue.CodeMasterDefine.KEY1_SIOT_GOTOSCHOOLTYPE
                                                        select t;
                    if (Query != null && Query.Count() > 0)
                    {
                        foreach (CodeMaster_cmt item in Query)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.cmt_cValue.ToString().Trim();
                            li.ValueMember = item.cmt_cKey2.ToString();
                            list.Add(li);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetPaymentType()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    string Str = "STU_DINING_TYPE";
                    string Str2 = "TEA_PAY_COST";
                    IEnumerable<PaymentSetting_pm> Query = from t in db.PaymentSetting_pms
                                                           where t.pms_cSettingName.ToString() == Str && t.pms_iKey <= 4
                                                           orderby t.pms_iKey ascending
                                                           select t;
                    if (Query != null && Query.Count() > 0)
                    {
                        foreach (PaymentSetting_pm item in Query)
                        {
                            PaymentSetting_pm infoPm = db.PaymentSetting_pms.FirstOrDefault(t => t.pms_cSettingName == Str2 && t.pms_iKey == item.pms_iKey);
                            if (infoPm != null)
                            {
                                if (infoPm.pms_iValue == 1)
                                {
                                    ComboboxDataInfo li = new ComboboxDataInfo();
                                    li.DisplayMember = item.pms_cRemark.ToString().Trim();
                                    li.ValueMember = item.pms_iKey.ToString();
                                    list.Add(li);
                                }
                            }


                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetMonitorTimeSetting()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();
            string sqlCmd = string.Empty;

            sqlCmd += "select * from dbo.CheckOnWorkAttendanceSetting_cws right join dbo.UserActivityMonitorItem_uam on uam_cItemNumber=cws_cNumber";

            List<CheckOnWorkAttendanceSetting_cws> returnList = new List<CheckOnWorkAttendanceSetting_cws>();

            IEnumerable<CheckOnWorkAttendanceSetting_cws> infos;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<CheckOnWorkAttendanceSetting_cws>(sqlCmd, new object[] { });

                    if (infos != null)
                    {
                        returnList = infos.ToList<CheckOnWorkAttendanceSetting_cws>();
                    }

                    if (returnList.Count > 0)
                    {

                        foreach (CheckOnWorkAttendanceSetting_cws item in returnList)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.cws_cName;
                            li.ValueMember = item.cws_iRecordID.ToString();
                            list.Add(li);
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }


            return list;
        }

        private List<ComboboxDataInfo> Get24GUnusualCause()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<CodeMaster_cmt> tab = from t in db.CodeMaster_cmts
                                                      where t.cmt_cKey1 == DefineConstantValue.CodeMasterDefine.KEY1_SIOT_24G_UNUSUALRECORD_CAUSE
                                                      select t;
                    if (tab.Count() > 0)
                    {
                        foreach (CodeMaster_cmt item in tab)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.cmt_cValue;
                            li.ValueMember = item.cmt_cKey2.ToString();
                            list.Add(li);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return list;
        }

        private List<ComboboxDataInfo> GetDingLockTime()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<PaymentSetting_pm> tab = from t in db.PaymentSetting_pms
                                                         where t.pms_cSettingName.IndexOf("LockTime") > 0
                                                         select t;
                    if (tab.Count() > 0)
                    {
                        foreach (PaymentSetting_pm item in tab)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.pms_cSettingName;
                            li.ValueMember = item.pms_cValue.ToString();
                            list.Add(li);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return list;
        }

        private List<ComboboxDataInfo> GetPrinterModel()
        {
            List<ComboboxDataInfo> list = new List<ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<CodeMaster_cmt> _codeMaster = from t in db.CodeMaster_cmts
                                                              where t.cmt_cKey1 == DefineConstantValue.CodeMasterDefine.KEY1_SIOT_PINTER_MODEL
                                                              select t;

                    if (_codeMaster != null)
                    {
                        foreach (CodeMaster_cmt item in _codeMaster)
                        {
                            ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.cmt_cRemark;
                            li.ValueMember = item.cmt_cValue;
                            list.Add(li);
                        }

                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return list;
        }

        private List<ComboboxDataInfo> GetSchoolIDCardModel(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SchoolIDCardModelMaster_idm> taQuery = null;
                    if (key == "test")
                    {
                        taQuery =
                    from t in db.SchoolIDCardModelMaster_idm

                    orderby t.idm_iRecordID descending
                    select t;
                    }
                    else
                    {
                        taQuery =
                    from t in db.SchoolIDCardModelMaster_idm
                    where t.idm_cClientKey == key
                    orderby t.idm_iRecordID descending
                    select t;
                    }

                    if (taQuery.Count() > 0)
                    {
                        foreach (SchoolIDCardModelMaster_idm t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.idm_cModelName.ToString();
                            info.ValueMember = t.idm_cFilePath.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<ComboboxDataInfo> GetMessageInfo()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<AutoMessageMaster_amm> taQuery =
                        from t in db.AutoMessageMaster_amms
                        orderby t.amm_dAddTime descending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (AutoMessageMaster_amm t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();

                            Guid tempG = new Guid(t.amm_iRecordID.ToString());

                            Int32 iAll = (from s in db.AutoMessageRecipient_amr
                                          where s.amr_iAMMRecordID == tempG
                                          select s).Count();

                            Int32 iSuccess = (from s in db.AutoMessageRecipient_amr
                                              where s.amr_iAMMRecordID == tempG
                                              && s.amr_lIsSend == true && s.amr_lIsFailure == false
                                              select s).Count();

                            info.DisplayMember = t.amm_cTitle.ToString() + "  " + (iSuccess / iAll).ToString() + "%";
                            info.ValueMember = t.amm_iRecordID.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<ComboboxDataInfo> GetTheme()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<TH24G_ExamineInformation_eif> taQuery =
                        from t in db.TH24G_ExamineInformation_eif
                        orderby t.eif_iId ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (TH24G_ExamineInformation_eif t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.eif_cExamineDesc.ToString();
                            info.ValueMember = t.eif_cExamineNum.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<ComboboxDataInfo> GetSubject()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<TH24G_ExaminationQuestionsWarehouse_eqw> taQuery =
                        from t in db.TH24G_ExaminationQuestionsWarehouse_eqws
                        orderby t.eqw_iId ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (TH24G_ExaminationQuestionsWarehouse_eqw t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.eqw_cEQDescription.ToString();
                            info.ValueMember = t.eqw_iId.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        //**************************

        private List<ComboboxDataInfo> GetSchoolTerm()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SchoolWeekSetting_sws> taQuery =
                        from t in db.SchoolWeekSetting_sws
                        orderby t.sws_iRecordID ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (SchoolWeekSetting_sws t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.sws_cYear + t.sws_iSemesterOfYear.ToString().Replace("1", "第一学期").Replace("2", "第二学期");
                            info.ValueMember = t.sws_iRecordID.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        public List<ComboboxDataInfo> GetMasterDataInformations(DefineConstantValue.MasterType masterType, object keyInfo)
        {
            List<Model.General.ComboboxDataInfo> infoList = null;

            try
            {
                switch (masterType)
                {
                    case DefineConstantValue.MasterType.CourseMaster:
                        infoList = GetCourseMasterWidthKey(keyInfo as string);
                        break;
                    case DefineConstantValue.MasterType.CodeMaster_Key2:
                        infoList = GetCodeMaster_Key2(keyInfo as string);
                        break;
                   
                    case DefineConstantValue.MasterType.RightToQueryForTeacherDetailPaymentInfo:
                        infoList = GetRightToQueryForTeacerDetailPaymentInfo(keyInfo as string);
                        break;

                    case DefineConstantValue.MasterType.RightToQueryForClassDetailPaymentInfo:
                        infoList = GetRightToQueryForClassDetailPaymentInfo(keyInfo as string);
                        break;

                    case DefineConstantValue.MasterType.RightToQueryForTeachervAttenaceInfo:
                        infoList = GetRightToQueryForTeacerAttenaceInfo(keyInfo as string);
                        break;

                    case DefineConstantValue.MasterType.RightToQueryForStuddentAttenaceInfo:
                        infoList = GetRightToQueryForStudentAttenaceInfo(keyInfo as string);
                        break;
                    case DefineConstantValue.MasterType.Department_Teacher:
                        infoList = GetDepartmentTeacher(keyInfo as string);
                        break;

                    case DefineConstantValue.MasterType.SiteMaster:
                        infoList = GetSiteMasterWithKey(keyInfo as string);
                        break;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return infoList;
        }

        private List<ComboboxDataInfo> GetAllStaffInfo()
        {
            List<ComboboxDataInfo> returnList = new List<ComboboxDataInfo>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<CardUserMaster_cus> allStaff = from t in db.CardUserMaster_cus
                                                              where t.cus_cIdentityNum == Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff && t.cus_lValid == true
                                                              orderby t.cus_cChaName ascending
                                                              select t;

                    if (allStaff != null && allStaff.Count() > 0)
                    {
                        foreach (CardUserMaster_cus item in allStaff)
                        {
                            ComboboxDataInfo info = new ComboboxDataInfo();

                            info.DisplayMember = item.cus_cChaName;

                            info.ValueMember = item.cus_cNumber;

                            returnList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw;
            }


            return returnList;
        }

        private List<Model.General.ComboboxDataInfo> GetFinancialAffairsManage_DailyPay_SubType(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();

            if (key != string.Empty && key != "")
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        IEnumerable<ArticleTypeDefine_atd> tab = from t in db.ArticleTypeDefine_atds
                                                                 where t.atd_cParentRecordNum == key && t.atd_lValid == true
                                                                 select t;

                        if (tab != null && tab.Count() > 0)
                        {
                            foreach (ArticleTypeDefine_atd item in tab)
                            {
                                Model.General.ComboboxDataInfo li = new ComboboxDataInfo();
                                li.DisplayMember = item.atd_cDescript.ToString();
                                li.ValueMember = item.atd_cTypeNum.ToString();

                                infoList.Add(li);
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetSiteMasterWithKey(string key)
        {
            if (key != string.Empty && key != "")
            {
                List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<SiteMaster_stm> stm = (from t in db.SiteMaster_stms
                                                       where t.stm_cBuildingNumber == key
                                                       select t).OrderBy(t => t.stm_cNumber);
                    if (stm != null)
                    {
                        foreach (SiteMaster_stm item in stm)
                        {
                            Model.General.ComboboxDataInfo li = new ComboboxDataInfo();
                            li.DisplayMember = item.stm_cName.ToString();
                            li.ValueMember = item.stm_cNumber.ToString();

                            infoList.Add(li);
                        }
                    }
                }



                return infoList;
            }
            else
            {
                return null;
            }
        }

        private List<Model.General.ComboboxDataInfo> GetDepartmentTeacher(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();

            if (key != null && key != "")
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        IEnumerable<CardUserMaster_cus> tab = from t in db.CardUserMaster_cus
                                                              where t.cus_cIdentityNum == Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff && t.cus_lValid == true && t.cus_cDepartmentNum == key
                                                              select t;
                        if (tab != null && tab.Count() > 0)
                        {
                            foreach (CardUserMaster_cus item in tab)
                            {
                                Model.General.ComboboxDataInfo li = new ComboboxDataInfo();
                                li.DisplayMember = item.cus_cChaName.ToString();
                                li.ValueMember = item.cus_cNumber.ToString();

                                infoList.Add(li);
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetRightToQueryForStudentAttenaceInfo(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();

            string sqlString = string.Empty;
            if (IsAdmin_ForClassList(key, DefineConstantValue.SIOT_DataRightsTypeDefine.Attendance) || key == "sa")
            {
                //sqlString = "select '无限制' as DisplayMember,'--isAdmin' as ValueMember";
                sqlString = "select distinct " + Environment.NewLine;
                sqlString += "ISNULL( scm_cName,'') as scm_cName," + Environment.NewLine;
                sqlString += "ISNULL(cus_cGraduationPeriod,'') as cus_cGraduationPeriod," + Environment.NewLine;
                sqlString += "ISNULL(spm_cName,'') as spm_cName," + Environment.NewLine;
                sqlString += "ISNULL(cmt_cValue,'') as cmt_cValue," + Environment.NewLine;
                sqlString += "ISNULL(cus_cClassNum,'') as cus_cClassNum," + Environment.NewLine;
                sqlString += "spm_cName+'('+cus_cGraduationPeriod+')'+cmt_cValue as DisplayMember," + Environment.NewLine;
                sqlString += "cus_cSchoolNum+'_'+cus_cGraduationPeriod+'_'+cus_cSpecialtyNum+'_'+cus_cClassNum as ValueMember" + Environment.NewLine;
                sqlString += "from dbo.PaymentDetailInfo_pdi" + Environment.NewLine;
                sqlString += "left join dbo.RFIDCardManage_rcm" + Environment.NewLine;
                sqlString += "on rcm_cCardHFMacID=pdi_cCardID" + Environment.NewLine;
                sqlString += "left join dbo.RFIDCardIssuance_rci" + Environment.NewLine;
                sqlString += "on rcm_cCardNum=rci_cCardNum" + Environment.NewLine;
                sqlString += "left join dbo.CardUserMaster_cus" + Environment.NewLine;
                sqlString += "on rci_cCardUserNum=cus_cNumber and cus_cIdentityNum='" + DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student + "'" + Environment.NewLine;
                sqlString += "left join dbo.SchoolMaster_scm" + Environment.NewLine;
                sqlString += "on cus_cSchoolNum=scm_cNumber" + Environment.NewLine;
                sqlString += "left join dbo.SpecialtyMaster_spm" + Environment.NewLine;
                sqlString += "on cus_cSpecialtyNum=spm_cNumber" + Environment.NewLine;
                sqlString += "left join dbo.CodeMaster_cmt" + Environment.NewLine;
                sqlString += "on cus_cClassNum=cmt_cKey2 and cmt_cKey1='" + DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserClass + "'" + Environment.NewLine;
                sqlString += "order by cus_cGraduationPeriod,cus_cClassNum" + Environment.NewLine;
            }
            else
            {
                sqlString = "select spm_cName+'('+dtc_cGraduationPeriod+')'+cmt_cValue as DisplayMember," + Environment.NewLine;
                sqlString += "dtc_cSchoolNum+'_'+dtc_cGraduationPeriod+'_'+dtc_cSpecialtyNum+'_'+dtc_cClassNum as ValueMember" + Environment.NewLine;
                sqlString += "from dbo.DataRightsRoleUser_dru" + Environment.NewLine;
                sqlString += "left join dbo.DataRightsRole_ClassList_dtc" + Environment.NewLine;
                sqlString += "on dtc_cRoleNumber= dru_cRoleNumber and dtc_cDataRightType='" + DefineConstantValue.SIOT_DataRightsTypeDefine.Attendance + "' and dtc_iIsAllRights=0" + Environment.NewLine;
                sqlString += "left join dbo.SchoolMaster_scm" + Environment.NewLine;
                sqlString += "on scm_cNumber=dtc_cSchoolNum" + Environment.NewLine;
                sqlString += "left join dbo.SpecialtyMaster_spm" + Environment.NewLine;
                sqlString += "on spm_cNumber=dtc_cSpecialtyNum" + Environment.NewLine;
                sqlString += "left join dbo.CodeMaster_cmt" + Environment.NewLine;
                sqlString += "on cmt_cKey1='" + DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserClass + "' and cmt_cKey2=dtc_cClassNum" + Environment.NewLine;
                sqlString += "where dur_cUserLoginID='" + key + "'" + Environment.NewLine;

            }

            IEnumerable<Model.General.ComboboxDataInfo> infos = null;
            List<string> list = new List<string>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<Model.General.ComboboxDataInfo>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        foreach (Model.General.ComboboxDataInfo t in infos)
                        {
                            if (t.ValueMember != null)
                            {
                                infoList.Add(t);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetRightToQueryForTeacerAttenaceInfo(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<ComboboxDataInfo>();

            string sqlString = string.Empty;
            if (IsAdmin_ForTeacherList(key, DefineConstantValue.SIOT_DataRightsTypeDefine.Attendance) || key == "sa")
            {
                #region 获得管理员相关权限

                sqlString = "select cus_cChaName as DisplayMember," + Environment.NewLine;
                sqlString += "cus_cNumber as ValueMember" + Environment.NewLine;
                sqlString += "from dbo.CardUserMaster_cus" + Environment.NewLine;
                sqlString += "where cus_cIdentityNum='" + DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff + "'" + Environment.NewLine;
                sqlString += "and cus_lValid=1" + Environment.NewLine;
                #endregion
            }
            else
            {
                #region 获取非管理员权限
                sqlString = "select distinct cus_cChaName as DisplayMember,cus_cNumber as ValueMember" + Environment.NewLine;
                sqlString += "from dbo.DataRightsRoleUser_dru" + Environment.NewLine;
                sqlString += "left join dbo.DataRightsRole_drr" + Environment.NewLine;
                sqlString += "on dru_cRoleNumber=drr_cNumber" + Environment.NewLine;
                sqlString += "left join dbo.DataRightsRole_TeacherList_dtl" + Environment.NewLine;
                sqlString += "on drr_cNumber=dtl_cRoleNumber and dtl_cDataRightType='" + DefineConstantValue.SIOT_DataRightsTypeDefine.Attendance + "'" + Environment.NewLine;
                sqlString += "left join dbo.CardUserMaster_cus" + Environment.NewLine;
                sqlString += "on dtl_cTeacherNum=cus_cNumber" + Environment.NewLine;
                sqlString += "where dur_cUserLoginID='" + key + "'" + Environment.NewLine;

                #endregion
            }


            IEnumerable<Model.General.ComboboxDataInfo> infos = null;
            List<string> list = new List<string>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<Model.General.ComboboxDataInfo>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        foreach (Model.General.ComboboxDataInfo t in infos)
                        {
                            if (t.ValueMember != null)
                            {
                                infoList.Add(t);
                            }
                        }
                    }
                    AddSelfRight(infoList, key);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        /// <summary>
        /// 检查是否带有管理员权限_查看老师名单
        /// </summary>
        /// <param name="webAcount"></param>
        /// <param name="DataRightType"></param>
        /// <returns></returns>
        private Boolean IsAdmin_ForTeacherList(string webAcount, string DataRightType)
        {
            Boolean returnTab = false;
            Int32 tab = 0;

            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                try
                {
                    tab = (from t in db.DataRightsRoleUser_drus
                           join s in db.DataRightsRole_drrs
                           on t.dru_cRoleNumber equals s.drr_cNumber
                           join p in db.DataRightsRole_TeacherList_dtls
                           on t.dru_cRoleNumber equals p.dtl_cRoleNumber
                           where t.dur_cUserLoginID == webAcount && p.dtl_iIsAllRights == true && p.dtl_cDataRightType == DataRightType
                           select t).Count();
                    if (tab > 0)
                    {
                        returnTab = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
            return returnTab;
        }

        /// <summary>
        /// 检查是否带有管理员权限_查看班级名单
        /// </summary>
        /// <param name="webAcount"></param>
        /// <param name="DataRightType"></param>
        /// <returns></returns>
        private Boolean IsAdmin_ForClassList(string webAcount, string DataRightType)
        {
            Boolean returnTab = false;
            Int32 tab = 0;
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                try
                {
                    tab = (from t in db.DataRightsRoleUser_drus
                           join s in db.DataRightsRole_drrs
                           on t.dru_cRoleNumber equals s.drr_cNumber
                           join p in db.DataRightsRole_ClassList_dtcs
                           on t.dru_cRoleNumber equals p.dtc_cRoleNumber
                           where t.dur_cUserLoginID == webAcount && p.dtc_iIsAllRights == true && p.dtc_cDataRightType == DataRightType
                           select t).Count();

                    if (tab > 0)
                    {
                        returnTab = true;
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }


            return returnTab;
        }

        private List<Model.General.ComboboxDataInfo> GetRightToQueryForTeacerDetailPaymentInfo(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();

            string sqlString = string.Empty;

            if (IsAdmin_ForTeacherList(key, DefineConstantValue.SIOT_DataRightsTypeDefine.Payment) || key == "sa")
            {
                #region 获得管理员相关权限

                sqlString = "select cus_cChaName as DisplayMember," + Environment.NewLine;
                sqlString += "cus_cNumber as ValueMember" + Environment.NewLine;
                sqlString += "from dbo.CardUserMaster_cus" + Environment.NewLine;
                sqlString += "where cus_cIdentityNum='" + DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff + "'" + Environment.NewLine;
                sqlString += "and cus_lValid=1" + Environment.NewLine;
                #endregion
            }
            else
            {
                #region 获取非管理员权限
                sqlString = "select distinct cus_cChaName as DisplayMember,cus_cNumber as ValueMember" + Environment.NewLine;
                sqlString += "from dbo.DataRightsRoleUser_dru" + Environment.NewLine;
                sqlString += "left join dbo.DataRightsRole_drr" + Environment.NewLine;
                sqlString += "on dru_cRoleNumber=drr_cNumber" + Environment.NewLine;
                sqlString += "left join dbo.DataRightsRole_TeacherList_dtl" + Environment.NewLine;
                sqlString += "on drr_cNumber=dtl_cRoleNumber and dtl_cDataRightType='" + DefineConstantValue.SIOT_DataRightsTypeDefine.Payment + "'" + Environment.NewLine;
                sqlString += "left join dbo.CardUserMaster_cus" + Environment.NewLine;
                sqlString += "on dtl_cTeacherNum=cus_cNumber" + Environment.NewLine;
                sqlString += "where dur_cUserLoginID='" + key + "'" + Environment.NewLine;

                #endregion
            }
            IEnumerable<Model.General.ComboboxDataInfo> infos = null;
            List<string> list = new List<string>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<Model.General.ComboboxDataInfo>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        foreach (Model.General.ComboboxDataInfo t in infos)
                        {
                            if (t.ValueMember != null)
                            {
                                infoList.Add(t);
                            }
                        }
                    }
                    AddSelfRight(infoList, key);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetRightToQueryForClassDetailPaymentInfo(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();

            string sqlString = string.Empty;
            if (IsAdmin_ForClassList(key, DefineConstantValue.SIOT_DataRightsTypeDefine.Payment) || key == "sa")
            {
                //sqlString = "select '无限制' as DisplayMember,'--isAdmin' as ValueMember";

                //sqlString = "select distinct " + Environment.NewLine;
                //sqlString += "ISNULL( scm_cName,'') as scm_cName," + Environment.NewLine;
                //sqlString += "ISNULL(cus_cGraduationPeriod,'') as cus_cGraduationPeriod," + Environment.NewLine;
                //sqlString += "ISNULL(spm_cName,'') as spm_cName," + Environment.NewLine;
                //sqlString += "ISNULL(cmt_cValue,'') as cmt_cValue," + Environment.NewLine;
                //sqlString += "ISNULL(cus_cClassNum,'') as cus_cClassNum," + Environment.NewLine;
                //sqlString += "spm_cName+'('+cus_cGraduationPeriod+')'+cmt_cValue as DisplayMember," + Environment.NewLine;
                //sqlString += "cus_cSchoolNum+'_'+cus_cGraduationPeriod+'_'+cus_cSpecialtyNum+'_'+cus_cClassNum as ValueMember" + Environment.NewLine;
                //sqlString += "from dbo.PaymentDetailInfo_pdi" + Environment.NewLine;
                //sqlString += "left join dbo.RFIDCardManage_rcm" + Environment.NewLine;
                //sqlString += "on rcm_cCardHFMacID=pdi_cCardID" + Environment.NewLine;
                //sqlString += "left join dbo.RFIDCardIssuance_rci" + Environment.NewLine;
                //sqlString += "on rcm_cCardNum=rci_cCardNum" + Environment.NewLine;
                //sqlString += "left join dbo.CardUserMaster_cus" + Environment.NewLine;
                //sqlString += "on rci_cCardUserNum=cus_cNumber and cus_cIdentityNum='" + DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student + "'" + Environment.NewLine;
                //sqlString += "left join dbo.SchoolMaster_scm" + Environment.NewLine;
                //sqlString += "on cus_cSchoolNum=scm_cNumber" + Environment.NewLine;
                //sqlString += "left join dbo.SpecialtyMaster_spm" + Environment.NewLine;
                //sqlString += "on cus_cSpecialtyNum=spm_cNumber" + Environment.NewLine;
                //sqlString += "left join dbo.CodeMaster_cmt" + Environment.NewLine;
                //sqlString += "on cus_cClassNum=cmt_cKey2 and cmt_cKey1='" + DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserClass + "'" + Environment.NewLine;
                //sqlString += "where spm_cName+'('+cus_cGraduationPeriod+')'+cmt_cValue is not NULL order by cus_cGraduationPeriod,cus_cClassNum" + Environment.NewLine;

                sqlString = "select distinct ISNULL( scm_cName,'') as scm_cName," + Environment.NewLine;
                sqlString += "ISNULL(cus_cGraduationPeriod,'') as cus_cGraduationPeriod," + Environment.NewLine;
                sqlString += "ISNULL(spm_cName,'') as spm_cName," + Environment.NewLine;
                sqlString += "ISNULL(cmt_cValue,'') as cmt_cValue," + Environment.NewLine;
                sqlString += "cus_cClassNum," + Environment.NewLine;
                sqlString += "spm_cName+'('+cus_cGraduationPeriod+')'+cmt_cValue as DisplayMember," + Environment.NewLine;
                sqlString += "cus_cSchoolNum+'_'+cus_cGraduationPeriod+'_'+cus_cSpecialtyNum+'_'+cus_cClassNum as ValueMember" + Environment.NewLine;
                sqlString += "from dbo.CardUserMaster_cus" + Environment.NewLine;
                sqlString += "left join dbo.SchoolMaster_scm" + Environment.NewLine;
                sqlString += "on cus_cSchoolNum=scm_cNumber" + Environment.NewLine;
                sqlString += "left join dbo.SpecialtyMaster_spm" + Environment.NewLine;
                sqlString += "on cus_cSpecialtyNum=spm_cNumber" + Environment.NewLine;
                sqlString += "left join dbo.CodeMaster_cmt" + Environment.NewLine;
                sqlString += "on cus_cClassNum=cmt_cKey2 and cmt_cKey1='" + DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserClass + "'" + Environment.NewLine;
                sqlString += "where spm_cName+'('+cus_cGraduationPeriod+')'+cmt_cValue is not NULL" + Environment.NewLine;
                sqlString += "order by cus_cGraduationPeriod asc,spm_cName asc,cus_cClassNum asc" + Environment.NewLine;

            }
            else
            {
                sqlString = "select spm_cName+'('+dtc_cGraduationPeriod+')'+cmt_cValue as DisplayMember," + Environment.NewLine;
                sqlString += "dtc_cSchoolNum+'_'+dtc_cGraduationPeriod+'_'+dtc_cSpecialtyNum+'_'+dtc_cClassNum as ValueMember" + Environment.NewLine;
                sqlString += "from dbo.DataRightsRoleUser_dru" + Environment.NewLine;
                sqlString += "left join dbo.DataRightsRole_ClassList_dtc" + Environment.NewLine;
                sqlString += "on dtc_cRoleNumber= dru_cRoleNumber and dtc_cDataRightType='" + DefineConstantValue.SIOT_DataRightsTypeDefine.Payment + "' and dtc_iIsAllRights=0" + Environment.NewLine;
                sqlString += "left join dbo.SchoolMaster_scm" + Environment.NewLine;
                sqlString += "on scm_cNumber=dtc_cSchoolNum" + Environment.NewLine;
                sqlString += "left join dbo.SpecialtyMaster_spm" + Environment.NewLine;
                sqlString += "on spm_cNumber=dtc_cSpecialtyNum" + Environment.NewLine;
                sqlString += "left join dbo.CodeMaster_cmt" + Environment.NewLine;
                sqlString += "on cmt_cKey1='" + DefineConstantValue.CodeMasterDefine.KEY1_SIOT_CardUserClass + "' and cmt_cKey2=dtc_cClassNum" + Environment.NewLine;
                sqlString += "where dur_cUserLoginID='" + key + "'" + Environment.NewLine;

            }

            IEnumerable<Model.General.ComboboxDataInfo> infos = null;
            List<string> list = new List<string>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<Model.General.ComboboxDataInfo>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        foreach (Model.General.ComboboxDataInfo t in infos)
                        {
                            if (t.ValueMember != null)
                            {
                                infoList.Add(t);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return infoList;
        }

        private List<ComboboxDataInfo> AddSelfRight(List<ComboboxDataInfo> objList, string webUserAccount)
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    WebUserIdentityBind tab = (from t in db.WebUserIdentityBind
                                               where t.wui_WebUser == webUserAccount && t.wui_Valid == true
                                               select t).FirstOrDefault();
                    if (tab != null)
                    {
                        CardUserMaster_cus tab_CardUser = (from t in db.CardUserMaster_cus
                                                           where t.cus_cNumber == tab.wui_CardUserNum && t.cus_lValid == true
                                                           orderby t.cus_dLastDate descending
                                                           select t).FirstOrDefault();
                        if (tab_CardUser != null)
                        {
                            ComboboxDataInfo li = null;
                            li = objList.SingleOrDefault(t => t.ValueMember == tab_CardUser.cus_cNumber);

                            if (li == null)
                            {

                                li = new ComboboxDataInfo();
                                li.DisplayMember = tab_CardUser.cus_cChaName;
                                li.ValueMember = tab_CardUser.cus_cNumber;
                                objList.Add(li);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                //throw Ex;
                return objList;
            }
            return objList;
        }

        private List<Model.General.ComboboxDataInfo> GetTeacherCourseInfoWidthKey(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "select cum.cum_cName as DisplayMember,cum.cum_cNumber as ValueMember from dbo.CourseMaster_cum as cum" + Environment.NewLine;
            sqlString += "left join dbo.CourseMasterTeacher_cut as cut" + Environment.NewLine;
            sqlString += "on cut.cut_cCourseNumber=cum.cum_cNumber" + Environment.NewLine;
            sqlString += "where cut.cut_cTeacherNumber='" + key + "'" + Environment.NewLine;

            IEnumerable<Model.General.ComboboxDataInfo> infos = null;
            List<string> list = new List<string>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<Model.General.ComboboxDataInfo>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        foreach (Model.General.ComboboxDataInfo t in infos)
                        {
                            infoList.Add(t);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return infoList;
        }


        /// <summary>
        /// 2.4控制器监控编号

        /// </summary>
        /// <returns></returns>
        List<ComboboxDataInfo> GetMonitorNumber()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Reader24GMonitor_rgm> taQuery =
                        from t in db.Reader24GMonitor_rgms
                        orderby t.rgm_iRecordID ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (Reader24GMonitor_rgm t in taQuery)
                        {
                            if (t.rgm_cMonitoringLocation.ToString() != "")
                            {
                                Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                                info.DisplayMember = t.rgm_cMonitoringLocation;
                                info.ValueMember = t.rgm_cMonitorNumber.ToString();
                                infoList.Add(info);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        /// <summary>
        /// 地点主档
        /// </summary>
        /// <returns></returns>
        private List<ComboboxDataInfo> GetSiteMaster()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SiteMaster_stm> taQuery =
                        from t in db.SiteMaster_stms
                        orderby t.stm_cNumber ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (SiteMaster_stm t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.stm_cName;
                            info.ValueMember = t.stm_cNumber.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        /// <summary>
        /// 建築物地点主档
        /// 備註：其實是地點主檔，但是在格式是地點前增加建築物名稱，如：教學樓--102室
        /// </summary>
        /// <returns></returns>
        private List<ComboboxDataInfo> GetBuildSiteMaster()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    var taQuery =
                        from t in db.SiteMaster_stms
                        from b in db.BuildingMaster_bdms
                        where t.stm_cBuildingNumber == b.bdm_cNumber
                        orderby t.stm_cNumber ascending
                        select new
                        {
                            stm_cName = t.stm_cName,
                            stm_cNumber = t.stm_cNumber,
                            bdm_cName = b.bdm_cName
                        };
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.bdm_cName + "--" + t.stm_cName;
                            info.ValueMember = t.stm_cNumber.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }
        private List<ComboboxDataInfo> GetAbsenceType()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<AbsenceTypeSetting_at> taQuery =
                        from t in db.AbsenceTypeSetting_ats
                        orderby t.ats_cNumber ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (AbsenceTypeSetting_at t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.ats_cName;
                            info.ValueMember = t.ats_cNumber.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetGraduationPeriod()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    var taQuery =
                        (from t in db.CardUserMaster_cus
                         where t.cus_cIdentityNum == Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student
                         orderby t.cus_cGraduationPeriod
                         select new { GraduationPeriod = t.cus_cGraduationPeriod }).Distinct();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.GraduationPeriod;
                            info.ValueMember = t.GraduationPeriod;
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetCardStatc()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<CodeMaster_cmt> taQuery =
                        from t in db.CodeMaster_cmts
                        where t.cmt_cKey1 == Common.DefineConstantValue.CodeMasterDefine.KEY1_SIOT_RFIDCARDUSESTATUS
                        orderby t.cmt_iRecordID
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (CodeMaster_cmt t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.cmt_cValue;
                            info.ValueMember = t.cmt_cKey2;
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetCodeMaster_Key2(string CodeMaster_Key1)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    var taQuery =
                        (from t in db.CodeMaster_cmts
                         where t.cmt_cKey1 == CodeMaster_Key1
                         select new { name = t.cmt_cKey2 }).Distinct();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.name;
                            info.ValueMember = t.name;
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetCodeMaster_Key1()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    var taQuery =
                         (from t in db.CodeMaster_cmts
                          select new { name = t.cmt_cKey1 }
                          ).Distinct();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.name;
                            info.ValueMember = t.name;
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetWinFormSkin()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<CodeMaster_cmt> taQuery =
                        from t in db.CodeMaster_cmts
                        where t.cmt_cKey1 == DefineConstantValue.CodeMasterDefine.KEY1_WINFORM_SKIN
                        orderby t.cmt_iRecordID ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (CodeMaster_cmt t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.cmt_cKey2;
                            info.ValueMember = t.cmt_cValue.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetDepartmentMaster()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<DepartmentMaster_dpm> taQuery =
                        from t in db.DepartmentMaster_dpms
                        orderby t.dpm_iRecordID ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (DepartmentMaster_dpm t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.dpm_cName;
                            info.ValueMember = t.dpm_cNumber.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetSpecialtyMaster()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SpecialtyMaster_spm> taQuery =
                        from t in db.SpecialtyMaster_spms
                        orderby t.spm_iRecordID ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (SpecialtyMaster_spm t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.spm_cName;
                            info.ValueMember = t.spm_cNumber.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetSchoolMaster()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SchoolMaster_scm> taQuery =
                        from t in db.SchoolMaster_scms
                        orderby t.scm_iRecordID ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (SchoolMaster_scm t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.scm_cName;
                            info.ValueMember = t.scm_cNumber;
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetCodeMaster(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<CodeMaster_cmt> taQuery =
                        from t in db.CodeMaster_cmts
                        where t.cmt_cKey1 == key.ToString()
                        orderby t.cmt_cKey2 ascending
                        select t;
                    if (taQuery.Count() > 0)
                    {
                        foreach (CodeMaster_cmt t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.cmt_cValue;
                            info.ValueMember = t.cmt_cKey2;
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetBuildingMaster()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<BuildingMaster_bdm> taQuery =
                        from bdms in db.BuildingMaster_bdms
                        orderby bdms.bdm_iRecordID ascending
                        select bdms;
                    if (taQuery.Count() > 0)
                    {
                        foreach (BuildingMaster_bdm t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.bdm_cName;
                            info.ValueMember = t.bdm_cNumber.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetCourseMaster()
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<CourseMaster_cum> taQuery =
                        from cums in db.CourseMaster_cums
                        orderby cums.cum_cName ascending
                        select cums;
                    if (taQuery.Count() > 0)
                    {
                        foreach (CourseMaster_cum t in taQuery)
                        {
                            Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                            info.DisplayMember = t.cum_cName;
                            info.ValueMember = t.cum_cNumber.ToString();
                            infoList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetCourseMasterWidthKey(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SpecialtyMasterCourse_smc> taQuery =
                        from t in db.SpecialtyMasterCourse_smcs
                        where t.smc_cSpecialtyNumber == key
                        orderby t.smc_iRecordID ascending
                        select t;

                    IQueryable<CourseMaster_cum> subTable =
                        from t in db.CourseMaster_cums
                        orderby t.cum_iRecordID ascending
                        select t;

                    if (taQuery.Count() > 0)
                    {
                        foreach (SpecialtyMasterCourse_smc t in taQuery)
                        {
                            foreach (CourseMaster_cum s in subTable)
                            {
                                if (t.smc_cCourseNumber == s.cum_cNumber)
                                {
                                    Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                                    info.DisplayMember = s.cum_cName;
                                    info.ValueMember = s.cum_cNumber.ToString();
                                    infoList.Add(info);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        private List<Model.General.ComboboxDataInfo> GetTeacherInfoWidthKey(string key)
        {
            List<Model.General.ComboboxDataInfo> infoList = new List<Model.General.ComboboxDataInfo>();
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                IQueryable<CourseMasterTeacher_cut> taQuery =
                    from t in db.CourseMasterTeacher_cut
                    where t.cut_cCourseNumber == key
                    orderby t.cut_iRecordID ascending
                    select t;

                IQueryable<CardUserMaster_cus> subTable =
                    from t in db.CardUserMaster_cus
                    where t.cus_cIdentityNum == Common.DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff
                    orderby t.cus_iRecordID ascending
                    select t;

                if (taQuery.Count() > 0)
                {
                    foreach (CourseMasterTeacher_cut t in taQuery)
                    {
                        foreach (CardUserMaster_cus s in subTable)
                        {
                            if (t.cut_cTeacherNumber == s.cus_cNumber)
                            {
                                Model.General.ComboboxDataInfo info = new Model.General.ComboboxDataInfo();
                                info.DisplayMember = s.cus_cChaName;
                                info.ValueMember = s.cus_cNumber.ToString();
                                infoList.Add(info);
                            }
                        }
                    }
                }
            }
            return infoList;
        }

        #endregion

        /// <summary>
        /// 获取教师信息
        /// </summary>
        /// <param name="hfID">hf卡ID</param>
        /// <returns></returns>
        public List<CardUserMaster_cus_Info> GetCardUserInfomation(string hfID)
        {
            List<CardUserMaster_cus_Info> listCusInfos = new List<CardUserMaster_cus_Info>();
            IEnumerable<CardUserMaster_cus> infos;
            try
            {
                string sqlString = @"select *, (cus.cus_cSchoolNum+'-'+cus.scm_cName) as cus_cSchoolNum," + Environment.NewLine;
                sqlString += @" (cus.cus_cDepartmentNum+'-'+dpm.dpm_cName) as cus_cDepartmentNum, cus.cus_cNumber, cus.cus_cSexNum, cus.cus_cChaName," + Environment.NewLine;
                sqlString += @" cus.cus_cIdentityNum, cus.cus_cSMSReceivePhone, cus.cus_lValid, cus.cus_imgPhoto" + Environment.NewLine;
                sqlString += @" from ( select * from ( select * from (" + Environment.NewLine;
                sqlString += @"select * from dbo.CardUserMaster_cus as A" + Environment.NewLine;
                sqlString += @" inner join dbo.RFIDCardIssuance_rci as B" + Environment.NewLine;
                sqlString += @" on A.cus_cNumber = B.rci_cCardUserNum" + Environment.NewLine;
                sqlString += @" and A.cus_lValid = 1" + Environment.NewLine;
                sqlString += @" and A.cus_cIdentityNum = 'STAFF') as D" + Environment.NewLine;
                sqlString += @" inner join dbo.RFIDCardManage_rcm as C" + Environment.NewLine;
                sqlString += @" on C.rcm_cCardNum = D.rci_cCardNum" + Environment.NewLine;
                sqlString += @" and C.rcm_lValid = 1" + Environment.NewLine;
                sqlString += @" and C.rcm_cCardHFID = '" + (hfID.Length > 8 ? hfID.Substring(1, 8) : hfID) + "'" + Environment.NewLine;
                sqlString += @" ) as E inner join dbo.SchoolMaster_scm as F on E.cus_cSchoolNum = F.scm_cNumber" + Environment.NewLine;
                sqlString += @" ) as cus inner join dbo.DepartmentMaster_dpm as dpm on cus.cus_cDepartmentNum = dpm.dpm_cNumber" + Environment.NewLine;

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<CardUserMaster_cus>(sqlString, new object[] { });

                    foreach (CardUserMaster_cus info in infos)
                    {
                        CardUserMaster_cus_Info cusInfo = new CardUserMaster_cus_Info();
                        cusInfo.cus_cAdd = info.cus_cAdd;
                        cusInfo.cus_cChaName = info.cus_cChaName;
                        cusInfo.cus_cClassNum = info.cus_cClassNum;
                        cusInfo.cus_cDepartmentNum = info.cus_cDepartmentNum;
                        cusInfo.cus_cEngName = info.cus_cEngName;
                        cusInfo.cus_cGraduationPeriod = info.cus_cGraduationPeriod;
                        cusInfo.cus_cIdentityNum = info.cus_cIdentityNum;
                        cusInfo.cus_cLast = info.cus_cLast;
                        cusInfo.cus_cMailAddress = info.cus_cMailAddress;
                        cusInfo.cus_cNumber = info.cus_cNumber;
                        cusInfo.cus_cRemark = info.cus_cRemark;
                        cusInfo.cus_cSchoolNum = info.cus_cSchoolNum;
                        cusInfo.cus_cSexNum = info.cus_cSexNum;
                        cusInfo.cus_cSMSReceivePhone = info.cus_cSMSReceivePhone;
                        cusInfo.cus_cSpecialtyNum = info.cus_cSpecialtyNum;
                        cusInfo.cus_dAddDate = info.cus_dAddDate;
                        cusInfo.cus_dLastDate = info.cus_dLastDate;
                        cusInfo.cus_imgPhoto = info.cus_imgPhoto;
                        cusInfo.cus_iRecordID = info.cus_iRecordID;
                        cusInfo.cus_lIsSendEmail = info.cus_lIsSendEmail;
                        cusInfo.cus_lIsSendSMS = info.cus_lIsSendSMS;
                        cusInfo.cus_lValid = info.cus_lValid;

                        listCusInfos.Add(cusInfo);
                    }
                }

            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return listCusInfos;
        }

        /// <summary>
        /// 获取卡用户信息
        /// </summary>
        /// <param name="cardMacID">32位卡ID</param>
        /// <returns></returns>
        public CardUserMaster_cus_Info GetCardUserInfo(string cardMacID)
        {
            CardUserMaster_cus_Info studentInfo = null;

            IEnumerable<CardUserMaster_cus_Info> infos;
            try
            {
                string sqlString = @"SELECT " + Environment.NewLine;
                sqlString += @"cus_cNumber," + Environment.NewLine;
                sqlString += @"cus_cGraduationPeriod," + Environment.NewLine;
                sqlString += @" SpecialtyName.spm_cName AS cus_cSpecialty," + Environment.NewLine;
                sqlString += @"ClassName.cmt_cValue AS cus_cClass," + Environment.NewLine;
                sqlString += @"cus_cChaName, " + Environment.NewLine;
                sqlString += @"Department.dpm_cName AS cus_cDepartment, " + Environment.NewLine;
                sqlString += @"cus_cIdentityNum, " + Environment.NewLine;
                sqlString += @"cus_guidPhotoKey," + Environment.NewLine;
                sqlString += @"cus_cStudentId" + Environment.NewLine;
                sqlString += @" FROM " + Environment.NewLine;
                sqlString += @"CardUserMaster_cus " + Environment.NewLine;
                sqlString += @"INNER JOIN RFIDCardIssuance_rci on rci_cCardUserNum=cus_cNumber " + Environment.NewLine;
                sqlString += @"INNER JOIN RFIDCardManage_rcm on rcm_cCardNum=rci_cCardNum " + Environment.NewLine;
                sqlString += @"LEFT JOIN CodeMaster_cmt AS ClassName on ClassName.cmt_cKey1='SIOT_CARDUSERCLASS' and ClassName.cmt_cKey2=cus_cClassNum " + Environment.NewLine;
                sqlString += @"LEFT JOIN SpecialtyMaster_spm AS SpecialtyName ON SpecialtyName.spm_cNumber=cus_cSpecialtyNum " + Environment.NewLine;
                sqlString += @"LEFT JOIN DepartmentMaster_dpm AS Department ON Department.dpm_cNumber=cus_cDepartmentNum ";
                sqlString += @"WHERE rcm_cCardHFMacID='" + cardMacID.Trim() + "'" + Environment.NewLine;

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<CardUserMaster_cus_Info>(sqlString, new object[] { });

                    studentInfo = infos.FirstOrDefault();
                }

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return studentInfo;

        }

        /// <summary>
        /// 获取卡用户信息
        /// </summary>
        /// <param name="cardMacID">8位卡ID</param>
        /// <returns></returns>
        public CardUserMaster_cus_Info GetCardUserInfoByCardID(string cardHFID)
        {
            CardUserMaster_cus_Info studentInfo = null;

            IEnumerable<CardUserMaster_cus_Info> infos;
            try
            {
                string sqlString = @"SELECT " + Environment.NewLine;
                sqlString += @"cus_cNumber," + Environment.NewLine;
                sqlString += @"cus_cGraduationPeriod," + Environment.NewLine;
                sqlString += @" SpecialtyName.spm_cName AS cus_cSpecialty," + Environment.NewLine;
                sqlString += @"ClassName.cmt_cValue AS cus_cClass," + Environment.NewLine;
                sqlString += @"cus_cChaName, " + Environment.NewLine;
                sqlString += @"Department.dpm_cName AS cus_cDepartment, " + Environment.NewLine;
                sqlString += @"cus_cIdentityNum, " + Environment.NewLine;
                sqlString += @"cus_guidPhotoKey," + Environment.NewLine;
                sqlString += @"cus_cStudentId" + Environment.NewLine;
                sqlString += @" FROM " + Environment.NewLine;
                sqlString += @"CardUserMaster_cus " + Environment.NewLine;
                sqlString += @"INNER JOIN RFIDCardIssuance_rci on rci_cCardUserNum=cus_cNumber " + Environment.NewLine;
                sqlString += @"INNER JOIN RFIDCardManage_rcm on rcm_cCardNum=rci_cCardNum " + Environment.NewLine;
                sqlString += @"LEFT JOIN CodeMaster_cmt AS ClassName on ClassName.cmt_cKey1='SIOT_CARDUSERCLASS' and ClassName.cmt_cKey2=cus_cClassNum " + Environment.NewLine;
                sqlString += @"LEFT JOIN SpecialtyMaster_spm AS SpecialtyName ON SpecialtyName.spm_cNumber=cus_cSpecialtyNum " + Environment.NewLine;
                sqlString += @"LEFT JOIN DepartmentMaster_dpm AS Department ON Department.dpm_cNumber=cus_cDepartmentNum ";
                sqlString += @"WHERE rcm_cCardHFID='" + cardHFID.Trim() + "'" + Environment.NewLine;

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<CardUserMaster_cus_Info>(sqlString, new object[] { });

                    studentInfo = infos.FirstOrDefault();
                }

            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return studentInfo;

        }

        /// <summary>
        /// 获取时间设置信息
        /// </summary>
        /// <param name="courseType">时间设置类型，若空则为查询全部</param>
        /// <returns></returns>
        public SendMessageInfo GetSendMessageProperty()
        {
            SendMessageInfo info = new SendMessageInfo();
            CodeMaster_cmt_Info codeMasterInfo = new CodeMaster_cmt_Info();
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                try
                {
                    IQueryable<CodeMaster_cmt> tab = from t in db.CodeMaster_cmts
                                                     where t.cmt_cKey1 == "SENDEMAILSETTING" || t.cmt_cKey1 == "SENDSMSSETTING"
                                                     select t;

                    if (tab.Count() > 0)
                    {
                        foreach (CodeMaster_cmt t in tab)
                        {
                            if (t.cmt_cKey1 == "SENDEMAILSETTING")
                            {
                                switch (t.cmt_cKey2)
                                {
                                    case "SERVICENAME":
                                        info.smi_emailService = t.cmt_cValue;
                                        break;
                                    case "USERID":
                                        info.smi_emailUserID = t.cmt_cValue;
                                        break;
                                    case "PASSWORD":
                                        info.smi_emailUserPassWord = t.cmt_cValue;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            if (t.cmt_cKey1 == "SENDSMSSETTING")
                            {
                                switch (t.cmt_cKey2)
                                {
                                    case "USERID":
                                        info.smi_userID = t.cmt_cValue;
                                        break;
                                    case "PASSWORD":
                                        info.smi_passWord = t.cmt_cValue;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }

            return info;
        }

        /// <summary>
        /// 获取监控组表列表
        /// </summary>
        /// <returns></returns>
        public List<Model.General.ComboboxDataInfo> GetUserActivityMonitorItemSettingGroup()
        {
            List<Model.General.ComboboxDataInfo> infos = new List<Model.General.ComboboxDataInfo>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    var data = db.UserActivityMonitorItemSettingGroup_msgs.Select(d => d).ToList();
                    foreach (UserActivityMonitorItemSettingGroup_msg info in data)
                    {
                        Model.General.ComboboxDataInfo d = new ComboboxDataInfo();
                        d.DisplayMember = info.msg_cName;
                        d.ValueMember = info.msg_cNumber;
                        infos.Add(d);
                    }
                }
                return infos;
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

    }
}
