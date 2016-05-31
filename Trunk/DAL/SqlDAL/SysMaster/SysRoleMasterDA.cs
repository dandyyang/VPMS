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
using Common;

namespace DAL.SqlDAL.SysMaster
{
    class SysRoleMasterDA:ISysRoleMasterDA
    {

        private Sys_RoleMaster_rlm_Info FindUserRole(Sys_RoleMaster_rlm_Info info)
        {
            string sqlString = string.Empty;
            sqlString += "SELECT usm_cUserLoginID,usm_cChaName " + Environment.NewLine;
            sqlString += "FROM Sys_UserMaster_usm " + Environment.NewLine;
            sqlString += "LEFT JOIN  Sys_UserRoles_usr" + Environment.NewLine;
            sqlString += "ON usm_cUserLoginID=usr_cUserLoginID"+Environment.NewLine;
            sqlString+="LEFT JOIN Sys_RoleMaster_rlm"+Environment.NewLine;
            sqlString += "ON rlm_cRoleID=usr_cRoleID WHERE rlm_cRoleID='" + info.rlm_cRoleID + "'";

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
                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        #region IDataBaseCommandDA<Sys_RoleMaster_rlm_Info> Members

        public Sys_RoleMaster_rlm_Info GetRecord_First()
        {
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_RoleMaster_rlm> taQuery =
                        (from ta in db.Sys_RoleMaster_rlms
                         orderby ta.rlm_iRecordID ascending
                         select ta).Take(1);
                    Sys_RoleMaster_rlm_Info info = new Sys_RoleMaster_rlm_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_RoleMaster_rlm, Sys_RoleMaster_rlm_Info>(t);
                            FindUserRole(info);
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

        public Sys_RoleMaster_rlm_Info GetRecord_Last()
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_RoleMaster_rlm> taQuery =
                        (from ta in db.Sys_RoleMaster_rlms
                         orderby ta.rlm_iRecordID descending
                         select ta).Take(1);
                    Sys_RoleMaster_rlm_Info info = new Sys_RoleMaster_rlm_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_RoleMaster_rlm, Sys_RoleMaster_rlm_Info>(t);
                            FindUserRole(info);
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

        public Sys_RoleMaster_rlm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_RoleMaster_rlm_Info info = null;
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
                    IQueryable<Sys_RoleMaster_rlm> taQuery =
                        (from ta in db.Sys_RoleMaster_rlms
                         where ta.rlm_iRecordID < Convert.ToInt32(iRecordId)
                         orderby ta.rlm_iRecordID descending
                         select ta).Take(1);

                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_RoleMaster_rlm, Sys_RoleMaster_rlm_Info>(t);
                            FindUserRole(info);
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

        public Sys_RoleMaster_rlm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_RoleMaster_rlm_Info info = null;
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
                    IQueryable<Sys_RoleMaster_rlm> taQuery =
                        (from ta in db.Sys_RoleMaster_rlms
                         where ta.rlm_iRecordID > Convert.ToInt32(iRecordId)
                         orderby ta.rlm_iRecordID ascending
                         select ta).Take(1);
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_RoleMaster_rlm, Sys_RoleMaster_rlm_Info>(t);
                            FindUserRole(info);
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
                Sys_RoleMaster_rlm rlm = new Sys_RoleMaster_rlm();
                try
                {
                    rlm = Common.General.CopyObjectValue<object, Sys_RoleMaster_rlm>(KeyObject);

                    IQueryable<Sys_RoleMaster_rlm> taQuery =
                            (from ta in db.Sys_RoleMaster_rlms where ta.rlm_cRoleID == rlm.rlm_cRoleID select ta);
                    if (taQuery.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
            LocalGeneral general = new LocalGeneral();
            Sys_RoleMaster_rlm_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<Sys_RoleMaster_rlm_Info>("Sys_RoleMaster_rlm");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region IMainDA<Sys_RoleMaster_rlm_Info> Members

        public bool InsertRecord(Sys_RoleMaster_rlm_Info infoObject)
        {
            if (infoObject == null)
            {
                return false;
            }

            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                Sys_RoleMaster_rlm rlm = new Sys_RoleMaster_rlm();
                try
                {
                    rlm = Common.General.CopyObjectValue<Sys_RoleMaster_rlm_Info, Sys_RoleMaster_rlm>(infoObject);



                    if (infoObject.userMasterList != null && infoObject.userMasterList.Count > 0)
                    {
                        for (int i = 0; i < infoObject.userMasterList.Count; i++)
                        {

                            Sys_UserMaster_usm_Info usmInfo = infoObject.userMasterList[i];

                            Sys_UserRoles_usr item = new Sys_UserRoles_usr();

                            item.usr_cUserLoginID = usmInfo.usm_cUserLoginID;
                            item.usr_cRoleID = infoObject.rlm_cRoleID;
                                //courseitem.cum_cNumber;
                            rlm.Sys_UserRoles_usrs.Add(item);
                            //db.Sys_UserRoles_usrs.InsertOnSubmit(item);
                        }
                    }


                    db.Sys_RoleMaster_rlms.InsertOnSubmit(rlm);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public bool UpdateRecord(Sys_RoleMaster_rlm_Info infoObject)
        {
            string sqlString = string.Empty;
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                Sys_RoleMaster_rlm rlm = new Sys_RoleMaster_rlm();
                Sys_RoleMaster_rlm_Info info=new Sys_RoleMaster_rlm_Info();
                try
                {

                    rlm = db.Sys_RoleMaster_rlms.SingleOrDefault(t => t.rlm_iRecordID == infoObject.rlm_iRecordID);

                    for (int i = 0; i < rlm.Sys_UserRoles_usrs.Count; i++)
                    {
                        Sys_UserRoles_usr sta = rlm.Sys_UserRoles_usrs[i];
                        db.Sys_UserRoles_usrs.DeleteOnSubmit(sta);
                    }


                    info = Common.General.CopyObjectValue<Sys_RoleMaster_rlm, Sys_RoleMaster_rlm_Info>(rlm);

                    IEnumerable<Sys_RoleMaster_rlm_Info> IErlm = null;
                    IEnumerable<Sys_UserRoles_usr_Info> IEusr = null;

                    sqlString += "UPDATE Sys_RoleMaster_rlm" + Environment.NewLine;
                    sqlString += " SET rlm_cRoleID='" + infoObject.rlm_cRoleID + "'," + Environment.NewLine;
                    sqlString += " rlm_cRoleDesc='" + infoObject.rlm_cRoleDesc + "'," + Environment.NewLine;
                    sqlString += " rlm_cLast='" + infoObject.rlm_cLast + "'," + Environment.NewLine;
                    sqlString += " rlm_dLastDate='" + infoObject.rlm_dLastDate.Value.ToString(DefineConstantValue.gc_DateTimeFormat) + "'" + Environment.NewLine;
                    sqlString += " WHERE rlm_iRecordID='" + infoObject.rlm_iRecordID + "'";

                    IErlm = db.ExecuteQuery<Sys_RoleMaster_rlm_Info>(sqlString, new object[] { });

                    sqlString = string.Empty;
                    sqlString += "DELETE FROM Sys_UserRoles_usr WHERE usr_cRoleID='" + infoObject.rlm_cRoleID + "'";
                    IEusr = db.ExecuteQuery<Sys_UserRoles_usr_Info>(sqlString, new object[] { });


                    if (infoObject.userMasterList != null && infoObject.userMasterList.Count > 0)
                    {                       
                        for (int i = 0; i < infoObject.userMasterList.Count; i++)
                        {
                            sqlString = string.Empty;
                            Sys_UserMaster_usm_Info usmInfo = infoObject.userMasterList[i];

                            sqlString += "INSERT INTO Sys_UserRoles_usr" + Environment.NewLine;
                            sqlString += "(usr_cUserLoginID, usr_cRoleID)" + Environment.NewLine;
                            sqlString += " VALUES " + Environment.NewLine;
                            sqlString += "('" + usmInfo.usm_cUserLoginID + "','" + infoObject.rlm_cRoleID + "')";

                            IEusr = db.ExecuteQuery<Sys_UserRoles_usr_Info>(sqlString, new object[] { });
                        }
                    }

                    //if (rlm != null)
                    //{
                    //    //usm = Common.General.SetDataToLingQEntity(usm, infoObject);//时间溢出
                    //    rlm.rlm_cRoleDesc = infoObject.rlm_cRoleDesc;
                    //    rlm.rlm_cLast = infoObject.rlm_cLast;
                    //    rlm.rlm_dLastDate = infoObject.rlm_dLastDate.Value;

                    //    if (infoObject.userMasterList != null && infoObject.userMasterList.Count > 0)
                    //    {
                    //        for (int i = 0; i < infoObject.userMasterList.Count; i++)
                    //        {
                    //            Sys_UserMaster_usm_Info usmInfo = infoObject.userMasterList[i];

                    //            Sys_UserRoles_usr item = new Sys_UserRoles_usr();

                    //            item.usr_cUserLoginID = usmInfo.usm_cUserLoginID;
                    //            item.usr_cRoleID = infoObject.rlm_cRoleID;
                    //            //courseitem.cum_cNumber;
                    //            rlm.Sys_UserRoles_usrs.Add(item);
                    //            //db.Sys_UserRoles_usrs.InsertOnSubmit(item);
                    //        }
                    //    }
                    //    db.SubmitChanges();
                    //}
                    return true;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            Sys_RoleMaster_rlm_Info info = null;
            try
            {
                info = KeyObject as Sys_RoleMaster_rlm_Info;
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    Sys_RoleMaster_rlm rlm = db.Sys_RoleMaster_rlms.Single<Sys_RoleMaster_rlm>(i => i.rlm_iRecordID == info.rlm_iRecordID);

                    for (int i = 0; i < rlm.Sys_UserRoles_usrs.Count; i++)
                    {
                        Sys_UserRoles_usr sta = rlm.Sys_UserRoles_usrs[i];
                        db.Sys_UserRoles_usrs.DeleteOnSubmit(sta);
                    }

                    db.Sys_RoleMaster_rlms.DeleteOnSubmit(rlm);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Sys_RoleMaster_rlm_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            Sys_RoleMaster_rlm_Info rlm = new Sys_RoleMaster_rlm_Info();
            Sys_RoleMaster_rlm_Info info = new Sys_RoleMaster_rlm_Info();
            rlm = KeyObject as Sys_RoleMaster_rlm_Info;
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_RoleMaster_rlm> taQuery =
                        (from ta in db.Sys_RoleMaster_rlms
                         where ta.rlm_iRecordID == rlm.rlm_iRecordID
                         //orderby dpms.dpm_iRecordID ascending
                         select ta).Take(1);

                    if (taQuery != null)
                    {
                        //foreach (var t in taQuery)
                        foreach (Sys_RoleMaster_rlm t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_RoleMaster_rlm, Sys_RoleMaster_rlm_Info>(t);
                            FindUserRole(info);
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

        public List<Sys_RoleMaster_rlm_Info> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " rlm_iRecordID," + Environment.NewLine;
            sqlString += " rlm_cRoleID," + Environment.NewLine;
            sqlString += " rlm_cRoleDesc," + Environment.NewLine;
            //sqlString += " usm_cPasswork," + Environment.NewLine;
            //sqlString += " usm_cEMail," + Environment.NewLine;
            //sqlString += " (CASE usm_iLock WHEN 1 THEN N'已锁' WHEN 0 THEN N'未锁' END) AS iLock," + Environment.NewLine;
            //sqlString += " usm_cRemark," + Environment.NewLine;
            sqlString += " rlm_cAdd," + Environment.NewLine;
            sqlString += " rlm_dAddDate," + Environment.NewLine;
            sqlString += " rlm_cLast," + Environment.NewLine;
            sqlString += " rlm_dLastDate " + Environment.NewLine;
            sqlString += " FROM Sys_RoleMaster_rlm" + Environment.NewLine;


            Sys_RoleMaster_rlm_Info info = null;

            info = searchCondition as Sys_RoleMaster_rlm_Info;

            if (info != null)
            {
                whereString = " WHERE 1=1 ";
                if (info.rlm_cRoleID.Trim() != "")
                {
                    if (info.rlm_cRoleID.ToString().Contains("*") || info.rlm_cRoleID.ToString().Contains("?"))
                    {
                        whereString += " AND rlm_cRoleID LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.rlm_cRoleID) + "'";
                    }
                    else
                    {
                        whereString += "AND rlm_cRoleID = N'" + info.rlm_cRoleID.ToString().Trim() + "'";
                    }
                }
            }

            sqlString += whereString;

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

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        #endregion
    }
}
