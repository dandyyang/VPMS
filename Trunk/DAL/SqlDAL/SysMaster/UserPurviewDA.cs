using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.SysMaster;
using model = Model.SysMaster;
using LinqToSQLModel;
using DAL.SqlDAL.LocalLayer;
using System.Data.Linq.SqlClient;
using Model.SysMaster;

namespace DAL.SqlDAL.SysMaster
{
    class UserPurviewDA:IUserPurviewDA
    {
        private Sys_UserPurview_usp_Info FindUser(Sys_UserPurview_usp_Info info)
        {
            Sys_FormMaster_fom_Info formInfo = new Sys_FormMaster_fom_Info();

            List<Sys_FormMaster_fom_Info> formList = new List<Sys_FormMaster_fom_Info>();
            formList = info.formMasterList;
            if (formList!=null)
            {
                formInfo = info.formMasterList[0];

                string sqlString = string.Empty;
                sqlString += "SELECT DISTINCT usm_cUserLoginID,usm_cChaName " + Environment.NewLine;
                sqlString += "FROM Sys_UserMaster_usm " + Environment.NewLine;
                sqlString += "LEFT JOIN  Sys_UserPurview_usp" + Environment.NewLine;
                sqlString += "ON usm_cUserLoginID=usp_cUserLoginID" + Environment.NewLine;
                sqlString += "LEFT JOIN Sys_FormPurview_frp" + Environment.NewLine;
                sqlString += "ON usp_cPurviewCode=frp_cPurviewCode" + Environment.NewLine;
                sqlString += "LEFT JOIN Sys_FormMaster_fom" + Environment.NewLine;
                sqlString += "ON frp_cFormNumber=fom_cFormNumber WHERE fom_cFormNumber='" + formInfo.fom_cFormNumber + "'";

                IEnumerable<Sys_UserMaster_usm_Info> infos = null;
                List<Sys_UserMaster_usm_Info> infoList = null;

                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        infos = db.ExecuteQuery<Sys_UserMaster_usm_Info>(sqlString, new object[] { });

                        if (infos != null)
                        {
                            infoList = infos.ToList<Sys_UserMaster_usm_Info>();
                        }
                        foreach (Sys_UserMaster_usm_Info t in infoList)
                        {
                            info.userMasterList.Add(t);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                for (int i = 0; i < info.userMasterList.Count; i++)
                {
                    sqlString = string.Empty;
                    sqlString += "SELECT DISTINCT fum_cFunctionNumber,fum_cFunctionDesc" + Environment.NewLine;
                    sqlString += "FROM Sys_FunctionMaster_fum" + Environment.NewLine;
                    sqlString += "LEFT JOIN Sys_FormPurview_frp" + Environment.NewLine;
                    sqlString += "ON fum_cFunctionNumber=frp_cFunctionNumber" + Environment.NewLine;

                    sqlString += "LEFT JOIN Sys_FormMaster_fom" + Environment.NewLine;
                    sqlString += "ON fom_cFormNumber=frp_cFormNumber" + Environment.NewLine;

                    sqlString += "LEFT JOIN Sys_UserPurview_usp" + Environment.NewLine;
                    sqlString += "ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;
                    sqlString += "LEFT JOIN Sys_UserMaster_usm" + Environment.NewLine;
                    sqlString += "ON usp_cUserLoginID=usm_cUserLoginID WHERE usm_cUserLoginID='" + info.userMasterList[i].usm_cUserLoginID + "' AND fom_cFormNumber='" + formInfo.fom_cFormNumber + "'";
                    IEnumerable<Sys_FunctionMaster_fum_Info> fums = null;
                    List<Sys_FunctionMaster_fum_Info> fumList = null;
                    try
                    {
                        using (VPMSDBDataContext db = new VPMSDBDataContext())
                        {
                            fums = db.ExecuteQuery<Sys_FunctionMaster_fum_Info>(sqlString, new object[] { });

                            if (fums != null)
                            {
                                fumList = fums.ToList<Sys_FunctionMaster_fum_Info>();
                            }
                            List<Sys_FunctionMaster_fum_Info> fumList1 = new List<Sys_FunctionMaster_fum_Info>();
                            foreach (Sys_FunctionMaster_fum_Info t in fumList)
                            {
                                fumList1.Add(t);
                                //info.userMasterList[i].functionMasterList.Add(t);
                                //info.userMasterList.Add(t);
                            }
                            info.userMasterList[i].functionMasterList = fumList1;
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }

                }


            }
            return info;
        }

        private Sys_UserPurview_usp_Info FindRole(Sys_UserPurview_usp_Info info)
        {
            Sys_FormMaster_fom_Info formInfo = new Sys_FormMaster_fom_Info();
            if (info.formMasterList!=null)
            {
                formInfo = info.formMasterList[0];

                string sqlString = string.Empty;
                sqlString += "SELECT DISTINCT rlm_cRoleID,rlm_cRoleDesc " + Environment.NewLine;
                sqlString += "FROM Sys_RoleMaster_rlm " + Environment.NewLine;
                sqlString += "LEFT JOIN  Sys_UserPurview_usp" + Environment.NewLine;
                sqlString += "ON rlm_cRoleID=usp_cRoleID" + Environment.NewLine;
                sqlString += "LEFT JOIN Sys_FormPurview_frp" + Environment.NewLine;
                sqlString += "ON usp_cPurviewCode=frp_cPurviewCode" + Environment.NewLine;
                sqlString += "LEFT JOIN Sys_FormMaster_fom" + Environment.NewLine;
                sqlString += "ON frp_cFormNumber=fom_cFormNumber WHERE fom_cFormNumber='" + formInfo.fom_cFormNumber + "'";

                IEnumerable<Sys_RoleMaster_rlm_Info> infos = null;
                List<Sys_RoleMaster_rlm_Info> infoList = null;

                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        infos = db.ExecuteQuery<Sys_RoleMaster_rlm_Info>(sqlString, new object[] { });

                        if (infos != null)
                        {
                            infoList = infos.ToList<Sys_RoleMaster_rlm_Info>();
                        }
                        foreach (Sys_RoleMaster_rlm_Info t in infoList)
                        {
                            info.roleMasterList.Add(t);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                for (int i = 0; i < info.roleMasterList.Count; i++)
                {
                    sqlString = string.Empty;
                    sqlString += "SELECT DISTINCT fum_cFunctionNumber,fum_cFunctionDesc" + Environment.NewLine;
                    sqlString += "FROM Sys_FunctionMaster_fum" + Environment.NewLine;
                    sqlString += "LEFT JOIN Sys_FormPurview_frp" + Environment.NewLine;
                    sqlString += "ON fum_cFunctionNumber=frp_cFunctionNumber" + Environment.NewLine;

                    sqlString += "LEFT JOIN Sys_FormMaster_fom" + Environment.NewLine;
                    sqlString += "ON fom_cFormNumber=frp_cFormNumber" + Environment.NewLine;

                    sqlString += "LEFT JOIN Sys_UserPurview_usp" + Environment.NewLine;
                    sqlString += "ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;
                    sqlString += "LEFT JOIN Sys_RoleMaster_rlm" + Environment.NewLine;
                    sqlString += "ON usp_cRoleID=rlm_cRoleID WHERE rlm_cRoleID='" + info.roleMasterList[i].rlm_cRoleID + "' AND fom_cFormNumber='" + formInfo.fom_cFormNumber + "'";
                    IEnumerable<Sys_FunctionMaster_fum_Info> fums = null;
                    List<Sys_FunctionMaster_fum_Info> fumList = null;
                    try
                    {
                        using (VPMSDBDataContext db = new VPMSDBDataContext())
                        {
                            fums = db.ExecuteQuery<Sys_FunctionMaster_fum_Info>(sqlString, new object[] { });

                            if (fums != null)
                            {
                                fumList = fums.ToList<Sys_FunctionMaster_fum_Info>();
                            }
                            List<Sys_FunctionMaster_fum_Info> fumList1 = new List<Sys_FunctionMaster_fum_Info>();
                            foreach (Sys_FunctionMaster_fum_Info t in fumList)
                            {
                                fumList1.Add(t);
                                //info.userMasterList[i].functionMasterList.Add(t);
                                //info.userMasterList.Add(t);
                            }
                            info.roleMasterList[i].functionMasterList = fumList1;
                        }
                    }
                    catch (Exception Ex)
                    {
                        throw Ex;
                    }

                }
            }
            return info;
        }

        private Sys_UserPurview_usp_Info FindFunction(Sys_UserPurview_usp_Info info)
        {
            Sys_FormMaster_fom_Info formInfo = new Sys_FormMaster_fom_Info();
            if (info.formMasterList!=null)
            {
                formInfo = info.formMasterList[0];


                string sqlString = string.Empty;
                sqlString += "SELECT DISTINCT fum_cFunctionNumber,fum_cFunctionDesc " + Environment.NewLine;
                sqlString += "FROM Sys_FunctionMaster_fum " + Environment.NewLine;
                sqlString += "LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
                sqlString += "ON fum_cFunctionNumber=frp_cFunctionNumber" + Environment.NewLine;
                sqlString += "LEFT JOIN Sys_FormMaster_fom" + Environment.NewLine;
                sqlString += "ON frp_cFormNumber=fom_cFormNumber WHERE fom_cFormNumber='" + formInfo.fom_cFormNumber + "'";

                IEnumerable<Sys_FunctionMaster_fum_Info> infos = null;
                List<Sys_FunctionMaster_fum_Info> infoList = null;

                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        infos = db.ExecuteQuery<Sys_FunctionMaster_fum_Info>(sqlString, new object[] { });

                        if (infos != null)
                        {
                            infoList = infos.ToList<Sys_FunctionMaster_fum_Info>();
                        }
                        foreach (Sys_FunctionMaster_fum_Info t in infoList)
                        {
                            info.functionMasterList.Add(t);
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

        private Sys_UserPurview_usp_Info FindFormPurview(Sys_UserPurview_usp_Info info)
        {
            Sys_FormMaster_fom_Info formInfo = new Sys_FormMaster_fom_Info();
            if (info.formMasterList != null)
            {
                formInfo = info.formMasterList[0];


                string sqlString = string.Empty;
                sqlString += "SELECT frp_iRecordID, frp_cPurviewCode,frp_cFunctionNumber " + Environment.NewLine;
                sqlString += "FROM Sys_FormPurview_frp " + Environment.NewLine;
                sqlString += "LEFT JOIN  Sys_FormMaster_fom" + Environment.NewLine;
                sqlString += "ON fom_cFormNumber=frp_cFormNumber WHERE fom_cFormNumber='" + formInfo.fom_cFormNumber + "'";

                IEnumerable<Sys_FormPurview_frp_Info> infos = null;
                List<Sys_FormPurview_frp_Info> infoList = null;

                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        infos = db.ExecuteQuery<Sys_FormPurview_frp_Info>(sqlString, new object[] { });

                        if (infos != null)
                        {
                            infoList = infos.ToList<Sys_FormPurview_frp_Info>();
                        }
                        foreach (Sys_FormPurview_frp_Info t in infoList)
                        {
                            info.formPurviewList.Add(t);
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

        private bool DelUserPurview(Sys_UserPurview_usp_Info info)
        {
            string sqlString = string.Empty;
            sqlString += "SELECT DISTINCT frp_cPurviewCode" + Environment.NewLine;
            sqlString += "FROM Sys_FormPurview_frp" + Environment.NewLine;
            sqlString += "LEFT JOIN Sys_FormMaster_fom" + Environment.NewLine;
            sqlString += "ON frp_cFormNumber=fom_cFormNumber WHERE fom_cFormNumber='" + info.formMasterList[0].fom_cFormNumber + "'" + Environment.NewLine;

            IEnumerable<Sys_FormPurview_frp_Info> frps = null;
            List<Sys_FormPurview_frp_Info> frpList = null;

            IEnumerable<Sys_UserPurview_usp_Info> usps = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    frps = db.ExecuteQuery<Sys_FormPurview_frp_Info>(sqlString, new object[] { });

                    if (frps != null)
                    {
                        frpList = frps.ToList<Sys_FormPurview_frp_Info>();
                    }
                    for (int i = 0; i < frpList.Count; i++)
                    {
                        sqlString = "delete from Sys_UserPurview_usp where usp_cPurviewCode='" + frpList[i].frp_cPurviewCode + "'";

                        usps = db.ExecuteQuery<Sys_UserPurview_usp_Info>(sqlString, new object[] { });
                    }
                }
                return true;
            }
            catch (Exception Ex)
            {
                //throw Ex;
                return false;
            }
        }

        #region IDataBaseCommandDA<Sys_UserPurview_usp_Info> Members

        public Sys_UserPurview_usp_Info GetRecord_First()
        {
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_UserPurview_usp> taQuery =
                        (from ta in db.Sys_UserPurview_usps
                         orderby ta.usp_iRecordID ascending
                         select ta).Take(1);
                    Sys_UserPurview_usp_Info info = new Sys_UserPurview_usp_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_UserPurview_usp, Sys_UserPurview_usp_Info>(t);
                            FindUser(info);
                            FindRole(info);
                            FindFunction(info);
                        }
                    }
                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Sys_UserPurview_usp_Info GetRecord_Last()
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_UserPurview_usp> taQuery =
                        (from ta in db.Sys_UserPurview_usps
                         orderby ta.usp_iRecordID descending
                         select ta).Take(1);
                    Sys_UserPurview_usp_Info info = new Sys_UserPurview_usp_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_UserPurview_usp, Sys_UserPurview_usp_Info>(t);
                            FindUser(info);
                            FindRole(info);
                            FindFunction(info);
                        }
                    }
                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Sys_UserPurview_usp_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_UserPurview_usp_Info info = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    string iRecordId = string.Empty;
                    foreach (var id in commandInfo.KeyInfoList)
                    {
                        iRecordId = id.KeyValue;
                        break;
                    }
                    IQueryable<Sys_UserPurview_usp> taQuery =
                        (from ta in db.Sys_UserPurview_usps
                         where ta.usp_iRecordID < Convert.ToInt32(iRecordId)
                         orderby ta.usp_iRecordID descending
                         select ta).Take(1);

                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_UserPurview_usp, Sys_UserPurview_usp_Info>(t);
                            FindUser(info);
                            FindRole(info);
                            FindFunction(info);
                        }
                    }
                    return info;
                }
            }
            catch (Exception Ex)
            {
                return info;
            }
        }

        public Sys_UserPurview_usp_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_UserPurview_usp_Info info = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    string iRecordId = string.Empty;
                    foreach (var id in commandInfo.KeyInfoList)
                    {
                        iRecordId = id.KeyValue;
                        break;
                    }
                    IQueryable<Sys_UserPurview_usp> taQuery =
                        (from ta in db.Sys_UserPurview_usps
                         where ta.usp_iRecordID > Convert.ToInt32(iRecordId)
                         orderby ta.usp_iRecordID ascending
                         select ta).Take(1);
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_UserPurview_usp, Sys_UserPurview_usp_Info>(t);
                            FindUser(info);
                            FindRole(info);
                            FindFunction(info);
                        }
                    }
                    return info;
                }
            }
            catch (Exception Ex)
            {
                return info;
            }
        }

        #endregion

        #region IExtraDA Members

        public bool IsExistRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public bool IsMyLockedRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject GetTableFieldLenght()
        {
            LocalGeneral general = new LocalGeneral();
            Sys_UserPurview_usp_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<Sys_UserPurview_usp_Info>("Sys_UserPurview_usp");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region IMainDA<Sys_UserPurview_usp_Info> Members

        public bool InsertRecord(Sys_UserPurview_usp_Info infoObject)
        {
            Sys_UserPurview_usp_Info uspInfo = new Sys_UserPurview_usp_Info();
            Sys_FormMaster_fom_Info formInfo = new Sys_FormMaster_fom_Info();
            Sys_UserMaster_usm_Info userInfo = null;
            Sys_RoleMaster_rlm_Info roleInfo = null;
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                try
                {
                    formInfo = infoObject.formMasterList[0];
                    DelUserPurview(infoObject);
                    for (int i = 0; i < infoObject.userMasterList.Count; i++)
                    {
                        for (int j = 0; j < infoObject.userMasterList[i].functionMasterList.Count; j++)
                        {
                            Sys_UserPurview_usp uspUser = new Sys_UserPurview_usp();
                            uspUser = Common.General.CopyObjectValue<Sys_UserPurview_usp_Info, Sys_UserPurview_usp>(infoObject);
                            userInfo = new Sys_UserMaster_usm_Info();
                            userInfo = infoObject.userMasterList[i];
                            uspUser.usp_cUserLoginID = userInfo.usm_cUserLoginID;

                            uspUser.usp_cPurviewCode = infoObject.userMasterList[i].functionMasterList[j].fum_cFunctionNumber + formInfo.fom_cFormNumber;
                            uspUser.usp_cAdd = infoObject.usp_cAdd;
                            uspUser.usp_cLast = infoObject.usp_cLast;
                            uspUser.usp_dAddDate = infoObject.usp_dAddDate.Value;
                            uspUser.usp_dLastDate = infoObject.usp_dLastDate.Value;

                            db.Sys_UserPurview_usps.InsertOnSubmit(uspUser);

                        }
                    }
                    for (int i = 0; i < infoObject.roleMasterList.Count; i++)
                    {
                        for (int j = 0; j < infoObject.roleMasterList[i].functionMasterList.Count; j++)
                        {
                            Sys_UserPurview_usp uspUser = new Sys_UserPurview_usp();
                            uspUser = Common.General.CopyObjectValue<Sys_UserPurview_usp_Info, Sys_UserPurview_usp>(infoObject);
                            roleInfo = new Sys_RoleMaster_rlm_Info();
                            roleInfo = infoObject.roleMasterList[i];
                            uspUser.usp_cRoleID = roleInfo.rlm_cRoleID;

                            uspUser.usp_cPurviewCode = infoObject.roleMasterList[i].functionMasterList[j].fum_cFunctionNumber + formInfo.fom_cFormNumber;
                            uspUser.usp_cAdd = infoObject.usp_cAdd;
                            uspUser.usp_cLast = infoObject.usp_cLast;
                            uspUser.usp_dAddDate = infoObject.usp_dAddDate.Value;
                            uspUser.usp_dLastDate = infoObject.usp_dLastDate.Value;

                            db.Sys_UserPurview_usps.InsertOnSubmit(uspUser);

                        }
                    }
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public bool UpdateRecord(Sys_UserPurview_usp_Info infoObject)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public Sys_UserPurview_usp_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            Sys_UserPurview_usp_Info usp = new Sys_UserPurview_usp_Info();
            usp = KeyObject as Sys_UserPurview_usp_Info;

            FindUser(usp);
            FindRole(usp);
            FindFunction(usp);
            return usp;
        }

        public List<Sys_UserPurview_usp_Info> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            Sys_UserPurview_usp_Info usp = new Sys_UserPurview_usp_Info();
            List<Sys_UserPurview_usp_Info> uspInfo = new List<Sys_UserPurview_usp_Info>();
            usp = searchCondition as Sys_UserPurview_usp_Info;

            FindFunctionByUR(usp);
            uspInfo.Add(usp);
            return uspInfo;
        }

        #endregion

        private Sys_UserPurview_usp_Info FindFunctionByUR(Sys_UserPurview_usp_Info info)
        {
            string temp = string.Empty;
            Sys_FormMaster_fom_Info formInfo = new Sys_FormMaster_fom_Info();
            if (info.formMasterList != null)
            {
                formInfo = info.formMasterList[0];
                if (info.roleMasterList[0].rlm_cRoleID != "")
                    temp = info.roleMasterList[0].rlm_cRoleID.ToString();
                else
                    temp = info.userMasterList[0].usm_cUserLoginID.ToString();

                string sqlString = string.Empty;
                sqlString += "SELECT DISTINCT fum_cFunctionNumber,fum_cFunctionDesc " + Environment.NewLine;
                sqlString += "FROM Sys_FunctionMaster_fum " + Environment.NewLine;
                sqlString += "LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
                sqlString += "ON fum_cFunctionNumber=frp_cFunctionNumber" + Environment.NewLine;

                sqlString += "LEFT JOIN  Sys_UserPurview_usp" + Environment.NewLine;
                sqlString += "ON usp_cPurviewCode=frp_cPurviewCode" + Environment.NewLine;

                sqlString += "LEFT JOIN Sys_FormMaster_fom" + Environment.NewLine;
                if (info.roleMasterList[0].rlm_cRoleID != "")
                    sqlString += "ON frp_cFormNumber=fom_cFormNumber WHERE fom_cFormNumber='" + formInfo.fom_cFormNumber + "' AND rlm_cRoleID='" + temp + "'";
                else
                    sqlString += "ON frp_cFormNumber=fom_cFormNumber WHERE fom_cFormNumber='" + formInfo.fom_cFormNumber + "' AND usp_cUserLoginID='" + temp + "'";

                IEnumerable<Sys_FunctionMaster_fum_Info> infos = null;
                List<Sys_FunctionMaster_fum_Info> infoList = null;

                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        infos = db.ExecuteQuery<Sys_FunctionMaster_fum_Info>(sqlString, new object[] { });

                        if (infos != null)
                        {
                            infoList = infos.ToList<Sys_FunctionMaster_fum_Info>();
                        }
                        foreach (Sys_FunctionMaster_fum_Info t in infoList)
                        {
                            info.functionMasterList.Add(t);
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
    }
}
