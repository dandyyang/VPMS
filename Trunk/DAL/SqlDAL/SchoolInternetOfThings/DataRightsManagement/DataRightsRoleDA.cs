using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.DataRightsManagement;
using DAL.IDAL.Management.DataRightsManagement;
using LinqToSQLModel;
using DAL.SqlDAL.Management.DataRightsManagement;
using System.Data.Linq.SqlClient;
using DAL.SqlDAL.LocalLayer;
using Common;
using Model.SysMaster;
using Model.General;

namespace DAL.SqlDAL.Management.DataRightsManagement
{
    class DataRightsRoleDA : IDataRightsRoleDA
    {
        StringBuilder strSQL;
        #region 内部函数
        DataRightsRole_drr_Info GetTeacherList(DataRightsRole_drr_Info info)
        {
            strSQL = new StringBuilder();
            strSQL.AppendLine("SELECT dpm_cName AS dtl_cRoleNumber,dtl_cDataRightType,dtl_iIsAllRights,dtl_cTeacherNum,cus_cChaName AS dtl_cTeacherName FROM DataRightsRole_TeacherList_dtl");

            strSQL.AppendLine("LEFT JOIN CardUserMaster_cus on dtl_cTeacherNum=cus_cNumber");
            strSQL.AppendLine("LEFT JOIN DepartmentMaster_dpm on cus_cDepartmentNum=dpm_cNumber");

            strSQL.AppendLine("WHERE 1=1");
            strSQL.AppendLine("AND dtl_cRoleNumber='" + info.drr_cNumber + "'");

            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                try
                {
                    info.teacherList = db.ExecuteQuery<DataRightsRole_TeacherList_dtl_Info>(strSQL.ToString(), new object[] { }).ToList();
                }
                catch
                {
                    info.teacherList = new List<DataRightsRole_TeacherList_dtl_Info>();
                }
            }
            return info;
        }

        DataRightsRole_drr_Info GetClassList(DataRightsRole_drr_Info info)
        {
            strSQL = new StringBuilder();
            strSQL.AppendLine("SELECT * FROM DataRightsRole_ClassList_dtc");
            strSQL.AppendLine("WHERE 1=1");
            strSQL.AppendLine("AND dtc_cRoleNumber='" + info.drr_cNumber + "'");

            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                try
                {
                    info.classList = db.ExecuteQuery<DataRightsRole_ClassList_dtc_Info>(strSQL.ToString(), new object[] { }).ToList();
                }
                catch
                {
                    info.classList = new List<DataRightsRole_ClassList_dtc_Info>();
                }
            }
            return info;
        }
        #endregion

        #region IDataBaseCommandDA<DataRightsRole_drr_Info> Members

        public DataRightsRole_drr_Info GetRecord_First()
        {
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<DataRightsRole_drr> taQuery =
                        (from ta in db.DataRightsRole_drrs
                         orderby ta.drr_iRecordID ascending
                         select ta).Take(1);
                    DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DataRightsRole_drr, DataRightsRole_drr_Info>(t);
                            GetTeacherList(info);
                            GetClassList(info);
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

        public DataRightsRole_drr_Info GetRecord_Last()
        {
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<DataRightsRole_drr> taQuery =
                        (from ta in db.DataRightsRole_drrs
                         orderby ta.drr_iRecordID descending
                         select ta).Take(1);
                    DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DataRightsRole_drr, DataRightsRole_drr_Info>(t);
                            GetTeacherList(info);
                            GetClassList(info);
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

        public DataRightsRole_drr_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            DataRightsRole_drr_Info info = null;
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
                    IQueryable<DataRightsRole_drr> taQuery =
                        (from ta in db.DataRightsRole_drrs
                         where ta.drr_iRecordID < Convert.ToInt32(iRecordId)
                         orderby ta.drr_iRecordID descending
                         select ta).Take(1);

                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DataRightsRole_drr, DataRightsRole_drr_Info>(t);
                            GetTeacherList(info);
                            GetClassList(info);
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

        public DataRightsRole_drr_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            DataRightsRole_drr_Info info = null;
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
                    IQueryable<DataRightsRole_drr> taQuery =
                        (from ta in db.DataRightsRole_drrs
                         where ta.drr_iRecordID > Convert.ToInt32(iRecordId)
                         orderby ta.drr_iRecordID ascending
                         select ta).Take(1);
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DataRightsRole_drr, DataRightsRole_drr_Info>(t);
                            GetTeacherList(info);
                            GetClassList(info);
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
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                DataRightsRole_drr drr = new DataRightsRole_drr();
                try
                {
                    drr = Common.General.CopyObjectValue<object, DataRightsRole_drr>(KeyObject);
                    IQueryable<DataRightsRole_drr> taQuery =
                            (from ta in db.DataRightsRole_drrs where ta.drr_cNumber == drr.drr_cNumber select ta);
                    if (taQuery.Count() > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
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
            throw new NotImplementedException();
        }

        #endregion

        #region IMainDA<DataRightsRole_drr_Info> Members

        public bool InsertRecord(DataRightsRole_drr_Info infoObject)
        {
            if (infoObject == null)
            {
                return false;
            }
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                DataRightsRole_drr drr = new DataRightsRole_drr();
                try
                {
                    if (infoObject.teacherList != null && infoObject.teacherList.Count > 0)
                    {
                        for (int i = 0; i < infoObject.teacherList.Count; i++)
                        {

                            DataRightsRole_TeacherList_dtl_Info teacherInfo = infoObject.teacherList[i];

                            DataRightsRole_TeacherList_dtl item = new DataRightsRole_TeacherList_dtl();
                            item.dtl_cRoleNumber = infoObject.drr_cNumber;
                            item.dtl_cDataRightType = teacherInfo.dtl_cDataRightType;
                            item.dtl_iIsAllRights = teacherInfo.dtl_iIsAllRights;
                            item.dtl_cTeacherNum = teacherInfo.dtl_cTeacherNum;
                            //drr.DataRightsRole_TeacherList_dtls.Add(item);
                            db.DataRightsRole_TeacherList_dtls.InsertOnSubmit(item);
                            db.SubmitChanges();
                        }
                    }
                    if (infoObject.classList != null && infoObject.classList.Count > 0)
                    {
                        for (int i = 0; i < infoObject.classList.Count; i++)
                        {

                            DataRightsRole_ClassList_dtc_Info classInfo = infoObject.classList[i];

                            DataRightsRole_ClassList_dtc item = new DataRightsRole_ClassList_dtc();
                            item.dtc_cRoleNumber = infoObject.drr_cNumber;
                            item.dtc_cDataRightType = classInfo.dtc_cDataRightType;
                            item.dtc_iIsAllRights = classInfo.dtc_iIsAllRights;
                            item.dtc_cSchoolNum = classInfo.dtc_cSchoolNum;
                            item.dtc_cSpecialtyNum = classInfo.dtc_cSpecialtyNum;
                            item.dtc_cGraduationPeriod = classInfo.dtc_cGraduationPeriod;
                            item.dtc_cClassNum = classInfo.dtc_cClassNum;
                            //drr.DataRightsRole_ClassList_dtcs.Add(item);
                            db.DataRightsRole_ClassList_dtcs.InsertOnSubmit(item);
                            db.SubmitChanges();
                        }
                    }
                    infoObject.drr_cLast = infoObject.drr_cAdd;
                    drr = Common.General.CopyObjectValue<DataRightsRole_drr_Info, DataRightsRole_drr>(infoObject);
                    db.DataRightsRole_drrs.InsertOnSubmit(drr);
                    db.SubmitChanges();

                    // db.SubmitChanges();
                    db.Transaction.Commit();
                    return true;
                }
                catch (Exception Ex)
                {
                    db.Transaction.Rollback();
                    return false;
                }
            }
        }

        public bool UpdateRecord(DataRightsRole_drr_Info infoObject)
        {
            string sqlString = string.Empty;
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();
                DataRightsRole_drr drrInfo = new DataRightsRole_drr();
                try
                {
                    drrInfo = db.DataRightsRole_drrs.SingleOrDefault(t => t.drr_iRecordID == infoObject.drr_iRecordID);

                    IEnumerable<DataRightsRole_TeacherList_dtl> _teacher =
                        (from ta in db.DataRightsRole_TeacherList_dtls
                         where ta.dtl_cRoleNumber == infoObject.drr_cNumber
                         orderby ta.dtl_iRecordID ascending
                         select ta);

                    db.DataRightsRole_TeacherList_dtls.DeleteAllOnSubmit(_teacher);
                    db.SubmitChanges();
                    //foreach (var item in _teacher)
                    //{
                    //    db.DataRightsRole_TeacherList_dtls.DeleteOnSubmit(item);
                    //    db.SubmitChanges();
                    //}
                    IEnumerable<DataRightsRole_ClassList_dtc> _class = (from ta in db.DataRightsRole_ClassList_dtcs
                                                                        where ta.dtc_cRoleNumber == infoObject.drr_cNumber
                                                                        orderby ta.dtc_iRecordID ascending
                                                                        select ta);
                    db.DataRightsRole_ClassList_dtcs.DeleteAllOnSubmit(_class);
                    db.SubmitChanges();
                    //foreach (var item in _class)
                    //{
                    //    db.DataRightsRole_ClassList_dtcs.DeleteOnSubmit(item);
                    //    db.SubmitChanges();
                    //}


                    info = Common.General.CopyObjectValue<DataRightsRole_drr, DataRightsRole_drr_Info>(drrInfo);

                    IEnumerable<DataRightsRole_drr_Info> IDrr = null;
                    IEnumerable<DataRightsRole_TeacherList_dtl_Info> ITeacher = null;
                    IEnumerable<DataRightsRole_ClassList_dtc_Info> IClass = null;

                    sqlString += " UPDATE DataRightsRole_drr" + Environment.NewLine;
                    sqlString += " SET drr_cNumber='" + infoObject.drr_cNumber + "'," + Environment.NewLine;
                    sqlString += " drr_cName='" + infoObject.drr_cName + "'," + Environment.NewLine;
                    sqlString += " drr_cRemark='" + infoObject.drr_cRemark + "'," + Environment.NewLine;
                    sqlString += " drr_cLast='" + infoObject.drr_cLast + "'," + Environment.NewLine;
                    sqlString += " drr_dLastDate='" + infoObject.drr_dLastDate.Value.ToString(DefineConstantValue.gc_DateTimeFormat) + "'" + Environment.NewLine;
                    sqlString += " WHERE drr_iRecordID='" + infoObject.drr_iRecordID + "'";

                    IDrr = db.ExecuteQuery<DataRightsRole_drr_Info>(sqlString, new object[] { });

                    if (infoObject.teacherList != null && infoObject.teacherList.Count > 0)
                    {
                        for (int i = 0; i < infoObject.teacherList.Count; i++)
                        {
                            sqlString = string.Empty;
                            DataRightsRole_TeacherList_dtl_Info teacherInfo = infoObject.teacherList[i];
                            sqlString += "INSERT INTO DataRightsRole_TeacherList_dtl" + Environment.NewLine;
                            sqlString += "(dtl_cRoleNumber, dtl_cDataRightType,dtl_iIsAllRights,dtl_cTeacherNum)" + Environment.NewLine;
                            sqlString += " VALUES " + Environment.NewLine;
                            sqlString += "('" + infoObject.drr_cNumber + "','" + teacherInfo.dtl_cDataRightType + "','" + teacherInfo.dtl_iIsAllRights + "','" + teacherInfo.dtl_cTeacherNum + "')";

                            ITeacher = db.ExecuteQuery<DataRightsRole_TeacherList_dtl_Info>(sqlString, new object[] { });
                        }
                    }
                    if (infoObject.classList != null && infoObject.classList.Count > 0)
                    {
                        for (int i = 0; i < infoObject.classList.Count; i++)
                        {
                            sqlString = string.Empty;
                            DataRightsRole_ClassList_dtc_Info classInfo = infoObject.classList[i];
                            sqlString += "INSERT INTO DataRightsRole_ClassList_dtc" + Environment.NewLine;
                            sqlString += "(dtc_cRoleNumber, dtc_cDataRightType,dtc_iIsAllRights,dtc_cSchoolNum,dtc_cSpecialtyNum,dtc_cGraduationPeriod,dtc_cClassNum)" + Environment.NewLine;
                            sqlString += " VALUES " + Environment.NewLine;
                            sqlString += "('" + infoObject.drr_cNumber + "','" + classInfo.dtc_cDataRightType + "','" + classInfo.dtc_iIsAllRights + "','" + classInfo.dtc_cSchoolNum + "','" + classInfo.dtc_cSpecialtyNum + "','" + classInfo.dtc_cGraduationPeriod + "','" + classInfo.dtc_cClassNum + "')";

                            IClass = db.ExecuteQuery<DataRightsRole_ClassList_dtc_Info>(sqlString, new object[] { });
                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            DataRightsRole_drr_Info info = null;

            info = KeyObject as DataRightsRole_drr_Info;
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                try
                {
                    db.Connection.Open();
                    db.Transaction = db.Connection.BeginTransaction();
                    DataRightsRole_drr drr = db.DataRightsRole_drrs.Single<DataRightsRole_drr>(i => i.drr_iRecordID == info.drr_iRecordID);

                    IEnumerable<DataRightsRole_TeacherList_dtl> _teacher = from t in db.DataRightsRole_TeacherList_dtls
                                                                           where t.dtl_cRoleNumber == drr.drr_cNumber
                                                                           select t;
                    db.DataRightsRole_TeacherList_dtls.DeleteAllOnSubmit(_teacher);
                    db.SubmitChanges();

                    IEnumerable<DataRightsRole_ClassList_dtc> _class = from t in db.DataRightsRole_ClassList_dtcs
                                                                       where t.dtc_cRoleNumber == drr.drr_cNumber
                                                                       select t;
                    db.DataRightsRole_ClassList_dtcs.DeleteAllOnSubmit(_class);
                    db.SubmitChanges();

                    IEnumerable<DataRightsRoleUser_dru> _roleUser = from t in db.DataRightsRoleUser_drus
                                                                    where t.dru_cRoleNumber == drr.drr_cNumber
                                                                    select t;
                    db.DataRightsRoleUser_drus.DeleteAllOnSubmit(_roleUser);
                    db.SubmitChanges();

                    //for (int i = 0; i < drr.DataRightsRole_TeacherList_dtls.Count; i++)
                    //{
                    //    DataRightsRole_TeacherList_dtl sta = drr.DataRightsRole_TeacherList_dtls[i];
                    //    db.DataRightsRole_TeacherList_dtls.DeleteOnSubmit(sta);
                    //}
                    //for (int i = 0; i < drr.DataRightsRole_ClassList_dtcs.Count; i++)
                    //{
                    //    DataRightsRole_ClassList_dtc sta = drr.DataRightsRole_ClassList_dtcs[i];
                    //    db.DataRightsRole_ClassList_dtcs.DeleteOnSubmit(sta);
                    //}
                    db.DataRightsRole_drrs.DeleteOnSubmit(drr);
                    db.SubmitChanges();
                    db.Transaction.Commit();
                    return true;
                }
                catch (Exception Ex)
                {
                    db.Transaction.Rollback();
                    return false;
                }
            }

        }

        public DataRightsRole_drr_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();
            info = KeyObject as DataRightsRole_drr_Info;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<DataRightsRole_drr> taQuery =
                        (from ta in db.DataRightsRole_drrs
                         where ta.drr_iRecordID == info.drr_iRecordID
                         select ta).Take(1);

                    if (taQuery != null)
                    {
                        foreach (DataRightsRole_drr t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DataRightsRole_drr, DataRightsRole_drr_Info>(t);
                            GetTeacherList(info);
                            GetClassList(info);
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

        public List<DataRightsRole_drr_Info> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            //throw new NotImplementedException();

            strSQL = new StringBuilder();
            strSQL.AppendLine("SELECT * FROM DataRightsRole_drr");

            DataRightsRole_drr_Info info = null;

            info = searchCondition as DataRightsRole_drr_Info;
            if (info != null)
            {
                strSQL.AppendLine("WHERE 1=1");
                if (info.drr_cNumber != string.Empty)
                    strSQL.AppendLine("AND drr_cNumber='" + info.drr_cNumber + "'");
                if (info.drr_cName != string.Empty)
                    strSQL.AppendLine("AND drr_cName='" + info.drr_cName + "'");
            }

            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                try
                {
                    return db.ExecuteQuery<DataRightsRole_drr_Info>(strSQL.ToString(), new object[] { }).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion

        #region IDataRightsRoleDA Members

        public List<DataRightsRole_drr_Info> GetAllRoleList()
        {
            List<DataRightsRole_drr_Info> returnList = new List<DataRightsRole_drr_Info>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<DataRightsRole_drr> tab = from t in db.DataRightsRole_drrs
                                                          orderby t.drr_dAddDate descending
                                                          select t;
                    if (tab != null && tab.Count() > 0)
                    {
                        foreach (DataRightsRole_drr t in tab)
                        {
                            DataRightsRole_drr_Info tempInfo = null;
                            tempInfo = Common.General.CopyObjectValue<DataRightsRole_drr, DataRightsRole_drr_Info>(t);
                            GetRoleUser(tempInfo);
                            returnList.Add(tempInfo);
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return returnList;
        }

        private void GetRoleUser(DataRightsRole_drr_Info Entity)
        {
            if (Entity != null)
            {
                List<Sys_UserMaster_usm_Info> usmList = new List<Sys_UserMaster_usm_Info>();
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        IEnumerable<DataRightsRoleUser_dru> tab = from t in db.DataRightsRoleUser_drus
                                                                  where t.dru_cRoleNumber == Entity.drr_cNumber
                                                                  select t;

                        if (tab != null && tab.Count() > 0)
                        {
                            foreach (DataRightsRoleUser_dru item in tab)
                            {
                                Sys_UserMaster_usm usmTab = db.Sys_UserMaster_usms.SingleOrDefault(t => t.usm_cUserLoginID == item.dur_cUserLoginID);
                                if (usmTab != null)
                                {
                                    Sys_UserMaster_usm_Info tempInfo = null;
                                    tempInfo = Common.General.CopyObjectValue<Sys_UserMaster_usm, Sys_UserMaster_usm_Info>(usmTab);
                                    usmList.Add(tempInfo);
                                }

                            }
                        }
                    }
                    Entity.RoleUserList = usmList;
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }
        }

        public List<Sys_UserMaster_usm_Info> GetRoleUsers(string roleNumber)
        {
            List<Sys_UserMaster_usm_Info> usmList = null;

            if (roleNumber != null && roleNumber.Trim() != "")
            {
                string sqlString = string.Empty;

                sqlString = "SELECT " + Environment.NewLine;
                sqlString += " usm_cUserLoginID," + Environment.NewLine;
                sqlString += " usm_cChaName " + Environment.NewLine;
                sqlString += " FROM DataRightsRoleUser_dru " + Environment.NewLine;
                sqlString += " INNER JOIN Sys_UserMaster_usm " + Environment.NewLine;
                sqlString += " ON dur_cUserLoginID=usm_cUserLoginID " + Environment.NewLine;
                sqlString += " WHERE dru_cRoleNumber='" + roleNumber + "'";

                IEnumerable<Sys_UserMaster_usm_Info> infos = null;

                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        infos = db.ExecuteQuery<Sys_UserMaster_usm_Info>(sqlString);

                        if (infos != null)
                        {
                            usmList = infos.ToList();
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return usmList;
        }

        public List<Sys_UserMaster_usm_Info> GetAllSysUserList()
        {
            List<Sys_UserMaster_usm_Info> returnList = new List<Sys_UserMaster_usm_Info>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<Sys_UserMaster_usm> tab = from t in db.Sys_UserMaster_usms
                                                          where t.usm_iLock == false
                                                          orderby t.usm_dAddDate descending
                                                          select t;
                    if (tab != null)
                    {
                        foreach (Sys_UserMaster_usm item in tab)
                        {
                            Sys_UserMaster_usm_Info tempInfo = null;
                            tempInfo = Common.General.CopyObjectValue<Sys_UserMaster_usm, Sys_UserMaster_usm_Info>(item);
                            returnList.Add(tempInfo);
                        }

                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return returnList;
        }

        public ReturnValueInfo SaveRoleUser(DataRightsRole_drr_Info Entity)
        {
            ReturnValueInfo returnInfo = new ReturnValueInfo();
            returnInfo.boolValue = false;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    //IEnumerable<DataRightsRoleUser_dru> delTab = from t in db.DataRightsRoleUser_drus
                    //                                             where t.dru_cRoleNumber == Entity.drr_cNumber
                    //                                             select t;
                    //db.DataRightsRoleUser_drus.DeleteAllOnSubmit(delTab);

                    foreach (Sys_UserMaster_usm_Info item in Entity.RoleUserList)
                    {
                        DataRightsRoleUser_dru addTab = new DataRightsRoleUser_dru();
                        addTab.dru_cRoleNumber = Entity.drr_cNumber;
                        addTab.dur_cUserLoginID = item.usm_cUserLoginID;

                        db.DataRightsRoleUser_drus.InsertOnSubmit(addTab);
                    }
                    db.SubmitChanges();
                    returnInfo.boolValue = true;
                }
            }
            catch (Exception Ex)
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = Ex.Message;
            }

            return returnInfo;
        }

        public Model.General.ReturnValueInfo DeleteRoleUser(DataRightsRole_drr_Info Entity)
        {
            ReturnValueInfo returnInfo = new ReturnValueInfo();
            returnInfo.boolValue = false;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    foreach (Sys_UserMaster_usm_Info item in Entity.RoleUserList)
                    {
                        DataRightsRoleUser_dru info = db.DataRightsRoleUser_drus.SingleOrDefault<DataRightsRoleUser_dru>(t => t.dru_cRoleNumber == Entity.drr_cNumber && t.dur_cUserLoginID == item.usm_cUserLoginID);

                        db.DataRightsRoleUser_drus.DeleteOnSubmit(info);
                    }
                    db.SubmitChanges();
                    returnInfo.boolValue = true;
                }
            }
            catch (Exception Ex)
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = Ex.Message;
            }

            return returnInfo;
        }

        public ReturnValueInfo SaveUserToRole(List<Sys_UserMaster_usm_Info> userList, List<DataRightsRole_drr_Info> roleList, bool iClearUserRole, bool iClearRoleUser)
        {
            ReturnValueInfo returnInfo = new ReturnValueInfo();
            returnInfo.boolValue = false;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    if (iClearUserRole)
                    {
                        //删除用户之前所有角色

                        foreach (Sys_UserMaster_usm_Info item in userList)
                        {


                            IEnumerable<DataRightsRoleUser_dru> deleteTab = from t in db.DataRightsRoleUser_drus
                                                                            where t.dur_cUserLoginID == item.usm_cUserLoginID
                                                                            select t;

                            if (deleteTab != null && deleteTab.Count() > 0)
                            {
                                db.DataRightsRoleUser_drus.DeleteAllOnSubmit(deleteTab);
                                db.SubmitChanges();
                            }

                        }
                    }

                    if (iClearRoleUser)
                    {
                        foreach (DataRightsRole_drr_Info item in roleList)
                        {
                            IEnumerable<DataRightsRoleUser_dru> deleteTab = from t in db.DataRightsRoleUser_drus
                                                                            where t.dru_cRoleNumber == item.drr_cNumber
                                                                            select t;
                            if (deleteTab != null && deleteTab.Count() > 0)
                            {
                                db.DataRightsRoleUser_drus.DeleteAllOnSubmit(deleteTab);
                                db.SubmitChanges();
                            }
                        }
                    }

                    foreach (DataRightsRole_drr_Info role in roleList)
                    {
                        foreach (Sys_UserMaster_usm_Info User in userList)
                        {
                            DataRightsRoleUser_dru tab = db.DataRightsRoleUser_drus.FirstOrDefault(t => t.dur_cUserLoginID == User.usm_cUserLoginID && t.dru_cRoleNumber == role.drr_cNumber);
                            if (tab == null)
                            {
                                tab = new DataRightsRoleUser_dru();
                                tab.dru_cRoleNumber = role.drr_cNumber;
                                tab.dur_cUserLoginID = User.usm_cUserLoginID;

                                db.DataRightsRoleUser_drus.InsertOnSubmit(tab);
                            }
                        }
                    }
                    db.SubmitChanges();
                    returnInfo.boolValue = true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return returnInfo;
        }

        public List<DataRightsRole_drr_Info> GetUserRoleList(Sys_UserMaster_usm_Info Entity)
        {
            List<DataRightsRole_drr_Info> returnList = new List<DataRightsRole_drr_Info>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<DataRightsRole_drr> tab = from t in db.DataRightsRoleUser_drus
                                                          join d in db.DataRightsRole_drrs
                                                          on t.dru_cRoleNumber equals d.drr_cNumber
                                                          //into data
                                                          where t.dur_cUserLoginID == Entity.usm_cUserLoginID
                                                          select d;
                    if (tab != null && tab.Count() > 0)
                    {
                        foreach (DataRightsRole_drr item in tab)
                        {
                            DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();
                            info = Common.General.CopyObjectValue<DataRightsRole_drr, DataRightsRole_drr_Info>(item);
                            returnList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return returnList;
        }

        public List<DataRightsRole_drr_Info> GetUserRoles(string userLoginID)
        {
            List<DataRightsRole_drr_Info> returnList = new List<DataRightsRole_drr_Info>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<DataRightsRole_drr> tab = from t in db.DataRightsRoleUser_drus
                                                          join d in db.DataRightsRole_drrs
                                                          on t.dru_cRoleNumber equals d.drr_cNumber
                                                          where t.dur_cUserLoginID == userLoginID
                                                          select d;
                    if (tab != null && tab.Count() > 0)
                    {
                        foreach (DataRightsRole_drr item in tab)
                        {
                            DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();
                            info = Common.General.CopyObjectValue<DataRightsRole_drr, DataRightsRole_drr_Info>(item);
                            returnList.Add(info);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return returnList;
        }

        public List<Sys_UserMaster_usm_Info> GetRoleUserList(DataRightsRole_drr_Info Entity)
        {
            List<Sys_UserMaster_usm_Info> returnList = new List<Sys_UserMaster_usm_Info>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<Sys_UserMaster_usm> tab = from b in db.Sys_UserMaster_usms
                                                          join t in db.DataRightsRoleUser_drus
                                                          on b.usm_cUserLoginID equals t.dur_cUserLoginID
                                                          where t.dru_cRoleNumber == Entity.drr_cNumber
                                                          select b;


                    foreach (Sys_UserMaster_usm item in tab)
                    {
                        Sys_UserMaster_usm_Info info = new Sys_UserMaster_usm_Info();
                        info = Common.General.CopyObjectValue<Sys_UserMaster_usm, Sys_UserMaster_usm_Info>(item);
                        returnList.Add(info);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return returnList;
        }

        public ReturnValueInfo SaveUserRole(string userNumber, List<string> roleNumList)
        {
            ReturnValueInfo returnInfo = new ReturnValueInfo();
            returnInfo.boolValue = false;

            if (roleNumList == null || roleNumList.Count == 0)
            {
                returnInfo.boolValue = true;
                return returnInfo;
            }

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    foreach (string roleNum in roleNumList)
                    {
                        DataRightsRoleUser_dru info = new DataRightsRoleUser_dru();
                        info.dru_cRoleNumber = roleNum;
                        info.dur_cUserLoginID = userNumber;

                        db.DataRightsRoleUser_drus.InsertOnSubmit(info);
                    }
                    db.SubmitChanges();
                    returnInfo.boolValue = true;
                }
            }
            catch (Exception Ex)
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = Ex.Message;
            }

            return returnInfo;
        }

        public ReturnValueInfo DeleteUserRole(string userNumber, List<string> roleNumList)
        {
            ReturnValueInfo returnInfo = new ReturnValueInfo();
            returnInfo.boolValue = false;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    foreach (string roleNum in roleNumList)
                    {
                        IEnumerable<DataRightsRoleUser_dru> deleteTab = from t in db.DataRightsRoleUser_drus
                                                                        where t.dru_cRoleNumber == roleNum && t.dur_cUserLoginID == userNumber
                                                                        select t;
                        if (deleteTab != null && deleteTab.Count() > 0)
                        {
                            db.DataRightsRoleUser_drus.DeleteAllOnSubmit(deleteTab);
                        }
                    }

                    db.SubmitChanges();

                    returnInfo.boolValue = true;
                }
            }
            catch (Exception Ex)
            {
                returnInfo.boolValue = false;
                returnInfo.messageText = Ex.Message;
            }

            return returnInfo;
        }

        #endregion
    }
}
