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
    class LoginFormDA : ILoginFormDA
    {
        private Sys_UserMaster_usm_Info FindForm(Sys_UserMaster_usm_Info info)
        {
            string sqlString = string.Empty;

            sqlString += "SELECT distinct fom_iRecordID, fom_iParentID, fom_iIndex, fom_cFormNumber, fom_cFormDesc, fom_cExePath,fom_cWebPath,fom_iWebForm " + Environment.NewLine;
            sqlString += "FROM Sys_FormMaster_fom " + Environment.NewLine;
            sqlString += "LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
            sqlString += "ON fom_cFormNumber=frp_cFormNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN  Sys_UserPurview_usp" + Environment.NewLine;
            sqlString += "ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;
            sqlString += "LEFT JOIN Sys_UserMaster_usm" + Environment.NewLine;
            sqlString += "ON usp_cUserLoginID=usm_cUserLoginID WHERE usm_cUserLoginID='" + info.usm_cUserLoginID + "'";
            //sqlString += "ON usp_cUserLoginID=usm_cUserLoginID WHERE usm_cUserLoginID='" + info.usm_cUserLoginID + "' AND fom_iWebForm='false'";





            string sqlRole = string.Empty;

            sqlRole += "SELECT distinct fom_iRecordID, fom_iParentID, fom_iIndex, fom_cFormNumber, fom_cFormDesc, fom_cExePath,fom_cWebPath,fom_iWebForm " + Environment.NewLine;
            sqlRole += "FROM Sys_FormMaster_fom " + Environment.NewLine;
            sqlRole += "LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
            sqlRole += "ON fom_cFormNumber=frp_cFormNumber" + Environment.NewLine;
            sqlRole += "LEFT JOIN  Sys_UserPurview_usp" + Environment.NewLine;
            sqlRole += "ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;
            sqlRole += "LEFT JOIN  Sys_RoleMaster_rlm" + Environment.NewLine;
            sqlRole += "ON usp_cRoleID=rlm_cRoleID" + Environment.NewLine;
            sqlRole += "LEFT JOIN  Sys_UserRoles_usr" + Environment.NewLine;
            sqlRole += "ON rlm_cRoleID=usr_cRoleID" + Environment.NewLine;

            sqlRole += "LEFT JOIN Sys_UserMaster_usm" + Environment.NewLine;
            sqlRole += "ON usr_cUserLoginID=usm_cUserLoginID WHERE usm_cUserLoginID='" + info.usm_cUserLoginID + "'";


            string sql = "SELECT distinct a.* FROM( (" + sqlString + ") union (" + sqlRole + "))  as  a";
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    Sys_UserMaster_usm_Info cond = new Sys_UserMaster_usm_Info();
                    //// 请处理


                    cond.usm_cUserLoginID = info.usm_cUserLoginID;
                    //////
                    List<Sys_FormMaster_fom_Info> list = new List<Sys_FormMaster_fom_Info>();

                    var a = db.ExecuteQuery<Sys_FormMaster_fom_Info>(sql, new object[] { });
                    foreach (Sys_FormMaster_fom_Info t in a)
                    {
                        info.formMasterList.Add(t);

                        ////////////////////// 请处理

                        cond.formMasterList.Clear();
                        cond.formMasterList.Add(t);
                        t.functionMaster = SearchRecords(cond)[0].functionMasterList;
                        //////////////////////
                    }


                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        private Sys_UserMaster_usm_Info FindFunction(Sys_UserMaster_usm_Info info)
        {
            string sqlString = string.Empty;
            sqlString += "SELECT fum_iRecordID, fum_cFunctionNumber, fum_cFunctionDesc, fum_cRemark " + Environment.NewLine;
            sqlString += "FROM Sys_FunctionMaster_fum " + Environment.NewLine;
            sqlString += "LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
            sqlString += "ON fum_cFunctionNumber=frp_cFunctionNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN  Sys_UserPurview_usp" + Environment.NewLine;
            sqlString += "ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;
            sqlString += "LEFT JOIN Sys_UserMaster_usm" + Environment.NewLine;
            sqlString += "ON usp_cUserLoginID=usm_cUserLoginID WHERE usm_cUserLoginID='" + info.usm_cUserLoginID + "'";


            IEnumerable<Sys_FunctionMaster_fum_Info> infos = null;
            List<Sys_FunctionMaster_fum_Info> infoList = null;

            string sqlRole = string.Empty;

            sqlRole += "SELECT fum_iRecordID, fum_cFunctionNumber, fum_cFunctionDesc, fum_cRemark " + Environment.NewLine;
            sqlRole += "FROM Sys_FunctionMaster_fum " + Environment.NewLine;
            sqlRole += "LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
            sqlRole += "ON fum_cFunctionNumber=frp_cFunctionNumber" + Environment.NewLine;
            sqlRole += "LEFT JOIN  Sys_UserPurview_usp" + Environment.NewLine;
            sqlRole += "ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;
            sqlRole += "LEFT JOIN  Sys_RoleMaster_rlm" + Environment.NewLine;
            sqlRole += "ON usp_cRoleID=rlm_cRoleID" + Environment.NewLine;
            sqlRole += "LEFT JOIN  Sys_UserRoles_usr" + Environment.NewLine;
            sqlRole += "ON rlm_cRoleID=usr_cRoleID" + Environment.NewLine;

            sqlRole += "LEFT JOIN Sys_UserMaster_usm" + Environment.NewLine;
            sqlRole += "ON usr_cUserLoginID=usm_cUserLoginID WHERE usm_cUserLoginID='" + info.usm_cUserLoginID + "'";
            IEnumerable<Sys_FunctionMaster_fum_Info> roleInfos = null;
            List<Sys_FunctionMaster_fum_Info> roleInfoList = null;

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

                    roleInfos = db.ExecuteQuery<Sys_FunctionMaster_fum_Info>(sqlRole, new object[] { });

                    if (roleInfos != null)
                    {
                        roleInfoList = roleInfos.ToList<Sys_FunctionMaster_fum_Info>();
                    }
                    foreach (Sys_FunctionMaster_fum_Info t in roleInfoList)
                    {
                        info.functionMasterList.Add(t);
                    }


                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        private Sys_UserMaster_usm_Info FindAllFunction(Sys_UserMaster_usm_Info info)
        {
            string sqlString = string.Empty;
            sqlString += "SELECT * " + Environment.NewLine;
            sqlString += "FROM Sys_FunctionMaster_fum " + Environment.NewLine;

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
                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        private Sys_UserMaster_usm_Info FindAllForm(Sys_UserMaster_usm_Info info)
        {
            string sqlString = string.Empty;

            sqlString += "SELECT * " + Environment.NewLine;
            sqlString += "FROM Sys_FormMaster_fom" + Environment.NewLine;
            //sqlString += "FROM Sys_FormMaster_fom WHERE fom_iWebForm='false'" + Environment.NewLine;

            IEnumerable<Sys_FormMaster_fom_Info> infos = null;
            List<Sys_FormMaster_fom_Info> infoList = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<Sys_FormMaster_fom_Info>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        infoList = infos.ToList<Sys_FormMaster_fom_Info>();
                    }
                    Sys_UserMaster_usm_Info cond = new Sys_UserMaster_usm_Info();
                    //// 请处理

                    cond.usm_cUserLoginID = info.usm_cUserLoginID;
                    //////
                    foreach (Sys_FormMaster_fom_Info t in infoList)
                    {
                        info.formMasterList.Add(t);

                        //////////////////////// 请处理

                        cond.formMasterList.Clear();
                        cond.formMasterList.Add(t);
                        t.functionMaster = SearchRecords(cond)[0].functionMasterList;
                        //////////////////////
                    }
                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public Sys_UserMaster_usm_Info FindAllPermission(Sys_UserMaster_usm_Info usm)
        {
            try
            {
                FindAllForm(usm);
                FindAllFunction(usm);
                return usm;
            }
            catch
            {
                throw;
            }
        }

        public Sys_UserMaster_usm_Info FindPermission(Model.IModel.IModelObject KeyObject)
        {
            Sys_UserMaster_usm_Info usm = new Sys_UserMaster_usm_Info();
            Sys_UserMaster_usm_Info info = new Sys_UserMaster_usm_Info();
            usm = KeyObject as Sys_UserMaster_usm_Info;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_UserMaster_usm> taQuery =
                        (from ta in db.Sys_UserMaster_usms
                         where ta.usm_cUserLoginID == usm.usm_cUserLoginID && ta.usm_cPasswork == usm.usm_cPasswork
                         //orderby dpms.dpm_iRecordID ascending
                         select ta).Take(1);

                    if (taQuery != null)
                    {
                        foreach (Sys_UserMaster_usm t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_UserMaster_usm, Sys_UserMaster_usm_Info>(t);
                            FindForm(info);
                            FindFunction(info);
                        }
                    }
                    return info;
                }
            }
            catch
            {
                throw;
            }
        }

        #region IDataBaseCommandDA<Sys_UserMaster_usm_Info> Members

        public Sys_UserMaster_usm_Info GetRecord_First()
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm_Info GetRecord_Last()
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion

        #region IMainDA<Sys_UserMaster_usm_Info> Members

        public bool InsertRecord(Sys_UserMaster_usm_Info infoObject)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRecord(Sys_UserMaster_usm_Info infoObject)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            throw new NotImplementedException();
            //Sys_UserMaster_usm_Info usm = new Sys_UserMaster_usm_Info();
            //Sys_UserMaster_usm_Info info = new Sys_UserMaster_usm_Info();
            //usm = KeyObject as Sys_UserMaster_usm_Info;
            //try
            //{
            //    if (usm.usm_cUserLoginID.ToUpper() == "SA")
            //    {
            //        FindAllForm(usm);
            //        FindAllFunction(usm);
            //        return usm;
            //    }
            //    else
            //    {
            //        using (VPMSDBDataContext db = new VPMSDBDataContext())
            //        {
            //            IQueryable<Sys_UserMaster_usm> taQuery =
            //                (from ta in db.Sys_UserMaster_usms
            //                 where ta.usm_cUserLoginID == usm.usm_cUserLoginID && ta.usm_cPasswork == usm.usm_cPasswork
            //                 //orderby dpms.dpm_iRecordID ascending
            //                 select ta).Take(1);

            //            if (taQuery != null)
            //            {
            //                foreach (Sys_UserMaster_usm t in taQuery)
            //                {
            //                    info = Common.General.CopyObjectValue<Sys_UserMaster_usm, Sys_UserMaster_usm_Info>(t);
            //                    FindForm(info);
            //                    FindFunction(info);
            //                }
            //            }
            //            return info;
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    throw Ex;
            //}
        }

        public List<Sys_UserMaster_usm_Info> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            Sys_UserMaster_usm_Info info = null;

            info = searchCondition as Sys_UserMaster_usm_Info;

            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = " SELECT  distinct TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " fum_cFunctionNumber " + Environment.NewLine;
            sqlString += " FROM Sys_FunctionMaster_fum" + Environment.NewLine;
            sqlString += " LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
            sqlString += " ON frp_cFunctionNumber=fum_cFunctionNumber" + Environment.NewLine;
            //****
            sqlString += " LEFT JOIN  Sys_UserPurview_usp" + Environment.NewLine;
            sqlString += " ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;
            sqlString += " LEFT JOIN  Sys_UserMaster_usm" + Environment.NewLine;
            sqlString += " ON usp_cUserLoginID=usm_cUserLoginID" + Environment.NewLine;
            //****
            sqlString += " LEFT JOIN  Sys_FormMaster_fom" + Environment.NewLine;
            sqlString += " ON fom_cFormNumber=frp_cFormNumber" + Environment.NewLine;

            Sys_FormMaster_fom_Info fomInfo = new Sys_FormMaster_fom_Info();
            fomInfo = info.formMasterList[0];

            if (info != null)
            {
                whereString = " WHERE 1=1 ";
                if (fomInfo.fom_cFormNumber.Trim() != "")
                {
                    if (fomInfo.fom_cFormNumber.ToString().Contains("*") || fomInfo.fom_cFormNumber.ToString().Contains("?"))
                    {
                        whereString += " AND fom_cFormNumber LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(fomInfo.fom_cFormNumber) + "'";
                    }
                    else
                    {
                        whereString += "AND fom_cFormNumber = N'" + fomInfo.fom_cFormNumber.ToString().Trim() + "'";
                    }
                }
                //**
                if (info.usm_cUserLoginID.ToString().ToUpper() != "SA")
                {
                    if (info.usm_cUserLoginID != "")
                    {
                        if (info.usm_cUserLoginID.ToString().Contains("*") || info.usm_cUserLoginID.ToString().Contains("?"))
                        {
                            whereString += " AND usm_cUserLoginID LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.usm_cUserLoginID) + "'";
                        }
                        else
                        {
                            whereString += "AND usm_cUserLoginID = N'" + info.usm_cUserLoginID.ToString().Trim() + "'";
                        }
                    }
                }
                //*****
            }

            sqlString += whereString;


            IEnumerable<Sys_FunctionMaster_fum_Info> infos = null;
            List<Sys_FunctionMaster_fum_Info> infoList = null;
            List<Sys_UserMaster_usm_Info> usmInfoList = new List<Sys_UserMaster_usm_Info>();

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<Sys_FunctionMaster_fum_Info>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        infoList = infos.ToList<Sys_FunctionMaster_fum_Info>();
                        info.functionMasterList = infoList;
                        usmInfoList.Add(info);
                        RoleFunction(usmInfoList);
                    }

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return usmInfoList;
        }

        private List<Sys_UserMaster_usm_Info> RoleFunction(List<Sys_UserMaster_usm_Info> info)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = " SELECT distinct TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " fum_cFunctionNumber " + Environment.NewLine;
            sqlString += " FROM Sys_FunctionMaster_fum" + Environment.NewLine;
            sqlString += " LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
            sqlString += " ON frp_cFunctionNumber=fum_cFunctionNumber" + Environment.NewLine;
            //****
            sqlString += " LEFT JOIN  Sys_UserPurview_usp" + Environment.NewLine;
            sqlString += " ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;
            sqlString += " LEFT JOIN  Sys_RoleMaster_rlm" + Environment.NewLine;
            sqlString += " ON rlm_cRoleID=usp_cRoleID" + Environment.NewLine;
            sqlString += " LEFT JOIN  Sys_UserRoles_usr" + Environment.NewLine;
            sqlString += " ON usr_cRoleID=rlm_cRoleID" + Environment.NewLine;
            sqlString += " LEFT JOIN  Sys_UserMaster_usm" + Environment.NewLine;
            sqlString += " ON usr_cUserLoginID=usm_cUserLoginID" + Environment.NewLine;
            //****
            sqlString += " LEFT JOIN  Sys_FormMaster_fom" + Environment.NewLine;
            sqlString += " ON fom_cFormNumber=frp_cFormNumber" + Environment.NewLine;


            if (info != null)
            {
                whereString = " WHERE 1=1 ";
                if (info[0].formMasterList[0].fom_cFormNumber.Trim() != "")
                {
                    if (info[0].formMasterList[0].fom_cFormNumber.ToString().Contains("*") || info[0].formMasterList[0].fom_cFormNumber.ToString().Contains("?"))
                    {
                        whereString += " AND fom_cFormNumber LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info[0].formMasterList[0].fom_cFormNumber) + "'";
                    }
                    else
                    {
                        whereString += "AND fom_cFormNumber = N'" + info[0].formMasterList[0].fom_cFormNumber.ToString().Trim() + "'";
                    }
                }
                //**
                if (info[0].usm_cUserLoginID.ToString().ToUpper() != "SA")
                {
                    if (info[0].usm_cUserLoginID != "")
                    {
                        if (info[0].usm_cUserLoginID.ToString().Contains("*") || info[0].usm_cUserLoginID.ToString().Contains("?"))
                        {
                            whereString += " AND usm_cUserLoginID LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info[0].usm_cUserLoginID) + "'";
                        }
                        else
                        {
                            whereString += "AND usm_cUserLoginID = N'" + info[0].usm_cUserLoginID.ToString().Trim() + "'";
                        }
                    }
                }
                //*****
            }

            sqlString += whereString;

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
                        foreach (Sys_FunctionMaster_fum_Info item in infoList)
                        {
                            if (info[0].functionMasterList.SingleOrDefault(d => d.fum_cFunctionNumber == item.fum_cFunctionNumber) == null)
                            {
                                info[0].functionMasterList.Add(item);
                            }
                        }
                    }

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        #endregion
    }
}
