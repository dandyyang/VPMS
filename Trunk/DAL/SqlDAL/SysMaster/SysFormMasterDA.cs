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
    class SysFormMasterDA:ISysFormMasterDA
    {
        #region IDataBaseCommandDA<Sys_FormMaster_frm_Info> Members

        public Sys_FormMaster_fom_Info GetRecord_First()
        {
            Sys_FormMaster_fom_Info info = null;
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    Sys_FormMaster_fom taQuery = db.Sys_FormMaster_foms.OrderBy(t=>t.fom_iParentID).First();

                    if (taQuery!=null)
                    {
                        info = Common.General.CopyObjectValue<Sys_FormMaster_fom, Sys_FormMaster_fom_Info>(taQuery);
                            FindFormPurview(info);
                    }
                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Sys_FormMaster_fom_Info GetRecord_Last()
        {
            Sys_FormMaster_fom_Info info = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_FormMaster_fom> taQuery =
                        (from ta in db.Sys_FormMaster_foms
                         orderby ta.fom_iRecordID descending
                         select ta).Take(1);
                    //Sys_FormMaster_frm_Info info = new Sys_FormMaster_frm_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_FormMaster_fom, Sys_FormMaster_fom_Info>(t);
                            FindFormPurview(info);
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

        public Sys_FormMaster_fom_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_FormMaster_fom_Info info = null;
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
                    IQueryable<Sys_FormMaster_fom> taQuery =
                        (from ta in db.Sys_FormMaster_foms
                         where ta.fom_iRecordID < Convert.ToInt32(iRecordId)
                         orderby ta.fom_iRecordID descending
                         select ta).Take(1);

                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_FormMaster_fom, Sys_FormMaster_fom_Info>(t);
                            FindFormPurview(info);
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

        public Sys_FormMaster_fom_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_FormMaster_fom_Info info = null;
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
                    IQueryable<Sys_FormMaster_fom> taQuery =
                        (from ta in db.Sys_FormMaster_foms
                         where ta.fom_iRecordID > Convert.ToInt32(iRecordId)
                         orderby ta.fom_iRecordID ascending
                         select ta).Take(1);
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_FormMaster_fom, Sys_FormMaster_fom_Info>(t);
                            FindFormPurview(info);
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
                Sys_FormMaster_fom frm = new Sys_FormMaster_fom();
                try
                {
                    frm = Common.General.CopyObjectValue<object, Sys_FormMaster_fom>(KeyObject);

                    IQueryable<Sys_FormMaster_fom> taQuery =
                            (from ta in db.Sys_FormMaster_foms where ta.fom_cFormNumber == frm.fom_cFormNumber select ta);
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
            Sys_FormMaster_fom_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<Sys_FormMaster_fom_Info>("Sys_FormMaster_fom");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        private Sys_FormMaster_fom_Info FindFormPurview(Sys_FormMaster_fom_Info info)
        {
            string sqlString = string.Empty;
            sqlString += "SELECT fum_cFunctionNumber,fum_cFunctionDesc " + Environment.NewLine;
            sqlString += "FROM Sys_FunctionMaster_fum " + Environment.NewLine;
            sqlString += "LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
            sqlString += "ON fum_cFunctionNumber=frp_cFunctionNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN Sys_FormMaster_fom" + Environment.NewLine;
            sqlString += "ON frp_cFormNumber=fom_cFormNumber WHERE fom_iRecordID='" + info.fom_iRecordID + "'";

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
                        info.functionMaster.Add(t);
                    }
                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        #region IMainDA<Sys_FormMaster_frm_Info> Members

        public bool InsertRecord(Sys_FormMaster_fom_Info infoObject)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                Sys_FormMaster_fom frm = new Sys_FormMaster_fom();
                try
                {
                    frm = Common.General.CopyObjectValue<Sys_FormMaster_fom_Info, Sys_FormMaster_fom>(infoObject);


                    if (infoObject.functionMaster != null && infoObject.functionMaster.Count > 0)
                    {
                        for (int i = 0; i < infoObject.functionMaster.Count; i++)
                        {

                            Sys_FunctionMaster_fum_Info usmInfo = infoObject.functionMaster[i];

                            Sys_FormPurview_frp item = new Sys_FormPurview_frp();

                            item.frp_cFunctionNumber = usmInfo.fum_cFunctionNumber;
                            item.frp_cFormNumber = infoObject.fom_cFormNumber;

                            item.frp_cPurviewCode = item.frp_cFunctionNumber + item.frp_cFormNumber;
                            //courseitem.cum_cNumber;
                            frm.Sys_FormPurview_frps.Add(item);
                            //db.Sys_UserRoles_usrs.InsertOnSubmit(item);
                        }
                    }



                    db.Sys_FormMaster_foms.InsertOnSubmit(frm);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public bool UpdateRecord(Sys_FormMaster_fom_Info infoObject)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                Sys_FormMaster_fom frm = new Sys_FormMaster_fom();
                try
                {

                    frm = db.Sys_FormMaster_foms.SingleOrDefault(t => t.fom_iRecordID == infoObject.fom_iRecordID);

                    for (int i = 0; i < frm.Sys_FormPurview_frps.Count; i++)
                    {
                        Sys_FormPurview_frp sta = frm.Sys_FormPurview_frps[i];
                        db.Sys_FormPurview_frps.DeleteOnSubmit(sta);
                    }



                    string sqlString = string.Empty;
                    Sys_FormMaster_fom_Info info = new Sys_FormMaster_fom_Info();

                    info = Common.General.CopyObjectValue<Sys_FormMaster_fom, Sys_FormMaster_fom_Info>(frm);

                    IEnumerable<Sys_FormMaster_fom_Info> IEfom = null;
                    IEnumerable<Sys_FormPurview_frp_Info> IEfrp = null;

                    sqlString += "UPDATE Sys_FormMaster_fom" + Environment.NewLine;
                    sqlString += " SET fom_cFormNumber='" + infoObject.fom_cFormNumber + "'," + Environment.NewLine;
                    sqlString += " fom_cFormDesc='" + infoObject.fom_cFormDesc + "'," + Environment.NewLine;
                    sqlString += " fom_cExePath='" + infoObject.fom_cExePath + "'," + Environment.NewLine;
                    sqlString += " fom_cWebPath='" + infoObject.fom_cWebPath + "'," + Environment.NewLine;

                    sqlString += " fom_iWebForm='" + infoObject.fom_iWebForm + "'," + Environment.NewLine;
                    //sqlString += " fom_cExePath='" + infoObject.rlm_cLast + "'," + Environment.NewLine;
                    sqlString += " fom_iParentID='" + infoObject.fom_iParentID + "'," + Environment.NewLine;
                    sqlString += " fom_iIndex='" + infoObject.fom_iIndex + "'," + Environment.NewLine;
                    sqlString += " fom_cLast='" + infoObject.fom_cLast + "'," + Environment.NewLine;
                    sqlString += " fom_dLastDate='" + infoObject.fom_dLastDate.Value.ToString(DefineConstantValue.gc_DateTimeFormat) + "'" + Environment.NewLine;

                    sqlString += " WHERE fom_iRecordID='" + infoObject.fom_iRecordID + "'";

                    IEfom = db.ExecuteQuery<Sys_FormMaster_fom_Info>(sqlString, new object[] { });

                    sqlString = string.Empty;
                    sqlString += "DELETE FROM Sys_FormPurview_frp WHERE frp_cFormNumber='" + infoObject.fom_cFormNumber + "'";
                    IEfrp = db.ExecuteQuery<Sys_FormPurview_frp_Info>(sqlString, new object[] { });


                    if (infoObject.functionMaster != null && infoObject.functionMaster.Count > 0)
                    {
                        for (int i = 0; i < infoObject.functionMaster.Count; i++)
                        {
                            sqlString = string.Empty;
                            Sys_FunctionMaster_fum_Info fomInfo = infoObject.functionMaster[i];

                            string frp_code = fomInfo.fum_cFunctionNumber + infoObject.fom_cFormNumber;

                            sqlString += "INSERT INTO Sys_FormPurview_frp" + Environment.NewLine;
                            sqlString += "(frp_cPurviewCode, frp_cFunctionNumber, frp_cFormNumber)" + Environment.NewLine;
                            sqlString += " VALUES " + Environment.NewLine;
                            sqlString += "('" + frp_code + "','" + fomInfo.fum_cFunctionNumber + "','" + infoObject.fom_cFormNumber + "')";

                            IEfrp = db.ExecuteQuery<Sys_FormPurview_frp_Info>(sqlString, new object[] { });
                        }
                    }


                    //if (frm != null)
                    //{
                    //    frm.fom_cFormNumber = infoObject.fom_cFormNumber;
                    //    frm.fom_cFormDesc = infoObject.fom_cFormDesc;
                    //    frm.fom_cExePath = infoObject.fom_cExePath;

                    //    frm.fom_cWebPath = infoObject.fom_cWebPath;
                    //    frm.fom_iWebForm = infoObject.fom_iWebForm;

                    //    //frm.fom_iImageIndex = infoObject.fom_iImageIndex;

                    //    frm.fom_iParentID = infoObject.fom_iParentID;
                    //    frm.fom_iIndex = infoObject.fom_iIndex;

                    //    frm.fom_cLast = infoObject.fom_cLast;
                    //    frm.fom_dLastDate = infoObject.fom_dLastDate.Value;


                    //    if (infoObject.functionMaster != null && infoObject.functionMaster.Count > 0)
                    //    {
                    //        for (int i = 0; i < infoObject.functionMaster.Count; i++)
                    //        {
                    //            Sys_FunctionMaster_fum_Info usmInfo = infoObject.functionMaster[i];

                    //            Sys_FormPurview_frp item = new Sys_FormPurview_frp();

                    //            item.frp_cFunctionNumber = usmInfo.fum_cFunctionNumber;
                    //            item.frp_cFormNumber = infoObject.fom_cFormNumber;

                    //            item.frp_cPurviewCode = item.frp_cFunctionNumber + item.frp_cFormNumber;
                    //            //courseitem.cum_cNumber;
                    //            frm.Sys_FormPurview_frps.Add(item);
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
            Sys_FormMaster_fom_Info info = null;
            try
            {
                info = KeyObject as Sys_FormMaster_fom_Info;
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    Sys_FormMaster_fom frm = db.Sys_FormMaster_foms.Single<Sys_FormMaster_fom>(i => i.fom_iRecordID == info.fom_iRecordID);


                    for (int i = 0; i < frm.Sys_FormPurview_frps.Count; i++)
                    {
                        Sys_FormPurview_frp sta = frm.Sys_FormPurview_frps[i];
                        db.Sys_FormPurview_frps.DeleteOnSubmit(sta);
                    }

                    db.Sys_FormMaster_foms.DeleteOnSubmit(frm);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Sys_FormMaster_fom_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            Sys_FormMaster_fom_Info frm = new Sys_FormMaster_fom_Info();
            Sys_FormMaster_fom_Info info = new Sys_FormMaster_fom_Info();
            frm = KeyObject as Sys_FormMaster_fom_Info;
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_FormMaster_fom> taQuery =
                        (from ta in db.Sys_FormMaster_foms
                         where ta.fom_iRecordID == frm.fom_iRecordID
                         //orderby dpms.dpm_iRecordID ascending
                         select ta).Take(1);

                    if (taQuery != null)
                    {
                        //foreach (var t in taQuery)
                        foreach (Sys_FormMaster_fom t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_FormMaster_fom, Sys_FormMaster_fom_Info>(t);
                            FindFormPurview(info);
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

        public List<Sys_FormMaster_fom_Info> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " fom_iRecordID," + Environment.NewLine;
            sqlString += " fom_iParentID," + Environment.NewLine;
            sqlString += " fom_iIndex," + Environment.NewLine;
            sqlString += " fom_cFormNumber," + Environment.NewLine;
            sqlString += " fom_cFormDesc," + Environment.NewLine;
            sqlString += " fom_cExePath," + Environment.NewLine;
            sqlString += " fom_cWebPath," + Environment.NewLine;
            sqlString += " fom_iWebForm," + Environment.NewLine;
            sqlString += " fom_iImageIndex," + Environment.NewLine;
            sqlString += " fom_cRemark," + Environment.NewLine;
            sqlString += " fom_cAdd," + Environment.NewLine;
            sqlString += " fom_dAddDate," + Environment.NewLine;
            sqlString += " fom_cLast," + Environment.NewLine;
            sqlString += " fom_dLastDate " + Environment.NewLine;
            sqlString += " FROM Sys_FormMaster_fom" + Environment.NewLine;            


            Sys_FormMaster_fom_Info info = null;

            info = searchCondition as Sys_FormMaster_fom_Info;

            if (info != null)
            {
                whereString = " WHERE 1=1 ";
                if (info.fom_iParentID != 0)
                {
                    if (info.fom_iParentID.ToString().Contains("*") || info.fom_iParentID.ToString().Contains("?"))
                    {
                        whereString += " AND fom_iParentID LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.fom_iParentID.ToString()) + "'";
                    }
                    else
                    {
                        whereString += "AND fom_iParentID = N'" + info.fom_iParentID.ToString().Trim() + "'";
                    }
                }
                //
                if (info.fom_iIndex != 0)
                {
                    if (info.fom_iIndex.ToString().Contains("*") || info.fom_iIndex.ToString().Contains("?"))
                    {
                        whereString += " AND fom_iIndex LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.fom_iIndex.ToString()) + "'";
                    }
                    else
                    {
                        whereString += "AND fom_iIndex = N'" + info.fom_iIndex.ToString().Trim() + "'";
                    }
                }
                //
                if (info.fom_cFormNumber.ToString().Trim() != "")
                {
                    if (info.fom_cFormNumber.ToString().Contains("*") || info.fom_cFormNumber.ToString().Contains("?"))
                    {
                        whereString += " AND fom_cFormNumber LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.fom_cFormNumber.ToString()) + "'";
                    }
                    else
                    {
                        whereString += "AND fom_cFormNumber = N'" + info.fom_cFormNumber.ToString().Trim() + "'";
                    }
                }
            }

            sqlString += whereString + Environment.NewLine;
            sqlString += " ORDER BY fom_iIndex" + Environment.NewLine;

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

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }

        public List<Sys_FormMaster_fom_Info> SearchRecords(Model.IModel.IModelObject searchCondition,string all)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " fom_iRecordID," + Environment.NewLine;
            sqlString += " fom_iParentID," + Environment.NewLine;
            sqlString += " fom_iIndex," + Environment.NewLine;
            sqlString += " fom_cFormNumber," + Environment.NewLine;
            sqlString += " fom_cFormDesc," + Environment.NewLine;
            sqlString += " fom_cExePath," + Environment.NewLine;
            sqlString += " fom_cWebPath," + Environment.NewLine;
            sqlString += " fom_iWebForm," + Environment.NewLine;
            sqlString += " fom_iImageIndex," + Environment.NewLine;
            sqlString += " fom_cRemark," + Environment.NewLine;
            sqlString += " fom_cAdd," + Environment.NewLine;
            sqlString += " fom_dAddDate," + Environment.NewLine;
            sqlString += " fom_cLast," + Environment.NewLine;
            sqlString += " fom_dLastDate " + Environment.NewLine;
            sqlString += " FROM Sys_FormMaster_fom" + Environment.NewLine;


            Sys_FormMaster_fom_Info info = null;

            info = searchCondition as Sys_FormMaster_fom_Info;

            if (info != null)
            {
                whereString = " WHERE 1=1 AND fom_iWebForm='false' ";
                if (info.fom_iParentID != 0)
                {
                    if (info.fom_iParentID.ToString().Contains("*") || info.fom_iParentID.ToString().Contains("?"))
                    {
                        whereString += " AND fom_iParentID LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.fom_iParentID.ToString()) + "'";
                    }
                    else
                    {
                        whereString += "AND fom_iParentID = N'" + info.fom_iParentID.ToString().Trim() + "'";
                    }
                }
                //
                if (info.fom_iIndex != 0)
                {
                    if (info.fom_iIndex.ToString().Contains("*") || info.fom_iIndex.ToString().Contains("?"))
                    {
                        whereString += " AND fom_iIndex LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.fom_iIndex.ToString()) + "'";
                    }
                    else
                    {
                        whereString += "AND fom_iIndex = N'" + info.fom_iIndex.ToString().Trim() + "'";
                    }
                }
                //
                if (info.fom_cFormNumber.ToString().Trim() != "")
                {
                    if (info.fom_cFormNumber.ToString().Contains("*") || info.fom_cFormNumber.ToString().Contains("?"))
                    {
                        whereString += " AND fom_cFormNumber LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.fom_cFormNumber.ToString()) + "'";
                    }
                    else
                    {
                        whereString += "AND fom_cFormNumber = N'" + info.fom_cFormNumber.ToString().Trim() + "'";
                    }
                }
            }

            sqlString += whereString + Environment.NewLine;
            sqlString += " ORDER BY fom_iIndex" + Environment.NewLine;

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

                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        } 

        #endregion

        public List<Sys_FormMaster_fom_Info> SearchRecords(Model.IModel.IModelObject searchCondition, Sys_UserMaster_usm_Info userInfo)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "SELECT DISTINCT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " fom_iRecordID," + Environment.NewLine;
            sqlString += " fom_iParentID," + Environment.NewLine;
            sqlString += " fom_iIndex," + Environment.NewLine;
            sqlString += " fom_cFormNumber," + Environment.NewLine;
            sqlString += " fom_cFormDesc," + Environment.NewLine;
            sqlString += " fom_cExePath," + Environment.NewLine;
            sqlString += " fom_cWebPath," + Environment.NewLine;
            sqlString += " fom_iWebForm," + Environment.NewLine;
            sqlString += " fom_iImageIndex," + Environment.NewLine;
            sqlString += " fom_cRemark," + Environment.NewLine;
            sqlString += " fom_cAdd," + Environment.NewLine;
            sqlString += " fom_dAddDate," + Environment.NewLine;
            sqlString += " fom_cLast," + Environment.NewLine;
            sqlString += " fom_dLastDate " + Environment.NewLine;
            sqlString += " FROM Sys_FormMaster_fom" + Environment.NewLine;
            sqlString += " LEFT JOIN Sys_FormPurview_frp " + Environment.NewLine;
            sqlString += " ON fom_cFormNumber=frp_cFormNumber" + Environment.NewLine;
            sqlString += " LEFT JOIN Sys_UserPurview_usp" + Environment.NewLine;
            sqlString += " ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;
            sqlString += " LEFT JOIN Sys_UserMaster_usm" + Environment.NewLine;
            sqlString += " ON usp_cUserLoginID=usm_cUserLoginID" + Environment.NewLine;

            /**/
            string sqlRole = string.Empty;

            sqlRole = "SELECT DISTINCT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlRole += " fom_iRecordID," + Environment.NewLine;
            sqlRole += " fom_iParentID," + Environment.NewLine;
            sqlRole += " fom_iIndex," + Environment.NewLine;
            sqlRole += " fom_cFormNumber," + Environment.NewLine;
            sqlRole += " fom_cFormDesc," + Environment.NewLine;
            sqlRole += " fom_cExePath," + Environment.NewLine;
            sqlRole += " fom_cWebPath," + Environment.NewLine;
            sqlRole += " fom_iWebForm," + Environment.NewLine;
            sqlRole += " fom_iImageIndex," + Environment.NewLine;
            sqlRole += " fom_cRemark," + Environment.NewLine;
            sqlRole += " fom_cAdd," + Environment.NewLine;
            sqlRole += " fom_dAddDate," + Environment.NewLine;
            sqlRole += " fom_cLast," + Environment.NewLine;
            sqlRole += " fom_dLastDate " + Environment.NewLine;
            sqlRole += " FROM Sys_FormMaster_fom" + Environment.NewLine;
            sqlRole += " LEFT JOIN Sys_FormPurview_frp " + Environment.NewLine;
            sqlRole += " ON fom_cFormNumber=frp_cFormNumber" + Environment.NewLine;
            sqlRole += " LEFT JOIN Sys_UserPurview_usp" + Environment.NewLine;
            sqlRole += " ON frp_cPurviewCode=usp_cPurviewCode" + Environment.NewLine;

            sqlRole += " LEFT JOIN Sys_RoleMaster_rlm" + Environment.NewLine;
            sqlRole += " ON usp_cRoleID=rlm_cRoleID" + Environment.NewLine;

            sqlRole += " LEFT JOIN Sys_UserRoles_usr" + Environment.NewLine;
            sqlRole += " ON usr_cRoleID=rlm_cRoleID" + Environment.NewLine;

            sqlRole += " LEFT JOIN Sys_UserMaster_usm" + Environment.NewLine;
            sqlRole += " ON usr_cUserLoginID=usm_cUserLoginID" + Environment.NewLine;

            /**/

            Sys_FormMaster_fom_Info info = null;

            info = searchCondition as Sys_FormMaster_fom_Info;

            if (info != null)
            {
                whereString = " WHERE 1=1 AND fom_iWebForm='false' ";
                if (info.fom_iParentID != 0)
                {
                    if (info.fom_iParentID.ToString().Contains("*") || info.fom_iParentID.ToString().Contains("?"))
                    {
                        whereString += " AND fom_iParentID LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.fom_iParentID.ToString()) + "'";
                    }
                    else
                    {
                        whereString += "AND fom_iParentID = N'" + info.fom_iParentID.ToString().Trim() + "'";
                    }
                }
                if (userInfo.usm_cUserLoginID != "")
                {
                    if (userInfo.usm_cUserLoginID.ToString().Contains("*") || userInfo.usm_cUserLoginID.ToString().Contains("?"))
                    {
                        whereString += " AND usm_cUserLoginID LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(userInfo.usm_cUserLoginID.ToString()) + "'";
                    }
                    else
                    {
                        whereString += "AND usm_cUserLoginID = N'" + userInfo.usm_cUserLoginID.ToString().Trim() + "'";
                    }
                }
            }

            sqlString += whereString + Environment.NewLine;
            sqlString += " ORDER BY fom_iIndex" + Environment.NewLine;
            /**/
            sqlRole += whereString + Environment.NewLine;
            sqlRole += " ORDER BY fom_iIndex" + Environment.NewLine;
            IEnumerable<Sys_FormMaster_fom_Info> roleInfos = null;
            List<Sys_FormMaster_fom_Info> roleInfoList = null;
            /**/
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

                    /**/
                    roleInfos = db.ExecuteQuery<Sys_FormMaster_fom_Info>(sqlRole, new object[] { });

                    if (roleInfos != null)
                    {
                        roleInfoList = roleInfos.ToList<Sys_FormMaster_fom_Info>();
                    }
                    /**/
                    //删除重复的再添加
                    for (int i = 0; i < infoList.Count; i++)
                    {
                        for (int j = 0; j < roleInfoList.Count; j++)
                        {
                            if (infoList[i].fom_cFormNumber == roleInfoList[j].fom_cFormNumber)
                            {
                                roleInfoList.Remove(roleInfoList[j]);
                                j = 0;
                            }
                        }
                    }
                    foreach (Sys_FormMaster_fom_Info t in roleInfoList)
                    {
                        infoList.Add(t);
                    }
                    //********
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return infoList;
        }


        #region ISysFormMasterDA 成员


        public Sys_FormMaster_fom_Info GetWebTreeRoot()
        {
            Sys_FormMaster_fom_Info info=new Sys_FormMaster_fom_Info();
            using (VPMSDBDataContext db=new VPMSDBDataContext())
            {
                Sys_FormMaster_fom taQuery = db.Sys_FormMaster_foms.SingleOrDefault(t=>t.fom_iParentID==0 && t.fom_iWebForm==true);
                if (taQuery != null) 
                {
                    info = Common.General.CopyObjectValue<Sys_FormMaster_fom, Sys_FormMaster_fom_Info>(taQuery);
                }
            }
            return info;
        }

        #endregion

        #region ISysFormMasterDA 成员


        public List<Sys_FormMaster_fom_Info> GetWebTreeNode(Sys_FormMaster_fom_Info info)
        {
            List<Sys_FormMaster_fom_Info> retunList = new List<Sys_FormMaster_fom_Info>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_FormMaster_fom> list = from t in db.Sys_FormMaster_foms
                                                          where t.fom_iWebForm == true && t.fom_iParentID == info.fom_iRecordID
                                                          select t;
                    if (list.Count() > 0)
                    {
                        foreach (Sys_FormMaster_fom t in list)
                        {
                            Sys_FormMaster_fom_Info temp = new Sys_FormMaster_fom_Info();
                            temp = Common.General.CopyObjectValue<Sys_FormMaster_fom, Sys_FormMaster_fom_Info>(t);
                            retunList.Add(temp);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return retunList;
        }

        #endregion
    }
}
