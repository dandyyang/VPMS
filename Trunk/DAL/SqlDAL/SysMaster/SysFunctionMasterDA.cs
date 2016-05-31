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
    class SysFunctionMasterDA:ISysFunctionMasterDA
    {
        private Sys_FunctionMaster_fum_Info FindFormPurview(Sys_FunctionMaster_fum_Info info)
        {
            string sqlString = string.Empty;
            sqlString += "SELECT fom_cFormNumber,fom_cFormDesc " + Environment.NewLine;
            sqlString += "FROM Sys_FormMaster_fom " + Environment.NewLine;
            sqlString += "LEFT JOIN  Sys_FormPurview_frp" + Environment.NewLine;
            sqlString += "ON fom_cFormNumber=frp_cFormNumber" + Environment.NewLine;
            sqlString += "LEFT JOIN Sys_FunctionMaster_fum" + Environment.NewLine;
            sqlString += "ON frp_cFunctionNumber=fum_cFunctionNumber WHERE fum_iRecordID='" + info.fum_iRecordID + "'";

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
                    foreach (Sys_FormMaster_fom_Info t in infoList)
                    {
                        info.formMaster.Add(t);
                    }
                    return info;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }


        #region IDataBaseCommandDA<Sys_FunctionMaster_fum_Info> Members

        public Sys_FunctionMaster_fum_Info GetRecord_First()
        {
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_FunctionMaster_fum> taQuery =
                        (from ta in db.Sys_FunctionMaster_fums
                         orderby ta.fum_iRecordID ascending
                         select ta).Take(1);
                    Sys_FunctionMaster_fum_Info info = new Sys_FunctionMaster_fum_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_FunctionMaster_fum, Sys_FunctionMaster_fum_Info>(t);
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

        public Sys_FunctionMaster_fum_Info GetRecord_Last()
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_FunctionMaster_fum> taQuery =
                        (from ta in db.Sys_FunctionMaster_fums
                         orderby ta.fum_iRecordID descending
                         select ta).Take(1);
                    Sys_FunctionMaster_fum_Info info = new Sys_FunctionMaster_fum_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_FunctionMaster_fum, Sys_FunctionMaster_fum_Info>(t);
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

        public Sys_FunctionMaster_fum_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_FunctionMaster_fum_Info info = null;
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
                    IQueryable<Sys_FunctionMaster_fum> taQuery =
                        (from ta in db.Sys_FunctionMaster_fums
                         where ta.fum_iRecordID < Convert.ToInt32(iRecordId)
                         orderby ta.fum_iRecordID descending
                         select ta).Take(1);

                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_FunctionMaster_fum, Sys_FunctionMaster_fum_Info>(t);
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

        public Sys_FunctionMaster_fum_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_FunctionMaster_fum_Info info = null;
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
                    IQueryable<Sys_FunctionMaster_fum> taQuery =
                        (from ta in db.Sys_FunctionMaster_fums
                         where ta.fum_iRecordID > Convert.ToInt32(iRecordId)
                         orderby ta.fum_iRecordID ascending
                         select ta).Take(1);
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_FunctionMaster_fum, Sys_FunctionMaster_fum_Info>(t);
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
                Sys_FunctionMaster_fum fum = new Sys_FunctionMaster_fum();
                try
                {
                    fum = Common.General.CopyObjectValue<object, Sys_FunctionMaster_fum>(KeyObject);

                    IQueryable<Sys_FunctionMaster_fum> taQuery =
                            (from ta in db.Sys_FunctionMaster_fums where ta.fum_cFunctionNumber == fum.fum_cFunctionNumber select ta);
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
            Sys_FunctionMaster_fum_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<Sys_FunctionMaster_fum_Info>("Sys_FunctionMaster_fum");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region IMainDA<Sys_FunctionMaster_fum_Info> Members

        public bool InsertRecord(Sys_FunctionMaster_fum_Info infoObject)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                Sys_FunctionMaster_fum fum = new Sys_FunctionMaster_fum();
                try
                {
                    fum = Common.General.CopyObjectValue<Sys_FunctionMaster_fum_Info, Sys_FunctionMaster_fum>(infoObject);


                    if (infoObject.formMaster != null && infoObject.formMaster.Count > 0)
                    {
                        for (int i = 0; i < infoObject.formMaster.Count; i++)
                        {

                            Sys_FormMaster_fom_Info usmInfo = infoObject.formMaster[i];

                            Sys_FormPurview_frp item = new Sys_FormPurview_frp();

                            item.frp_cFunctionNumber = infoObject.fum_cFunctionNumber;
                            item.frp_cFormNumber =  usmInfo.fom_cFormNumber;

                            item.frp_cPurviewCode = item.frp_cFunctionNumber + item.frp_cFormNumber;
                            //courseitem.cum_cNumber;
                            fum.Sys_FormPurview_frps.Add(item);
                            //db.Sys_UserRoles_usrs.InsertOnSubmit(item);
                        }
                    }



                    db.Sys_FunctionMaster_fums.InsertOnSubmit(fum);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public bool UpdateRecord(Sys_FunctionMaster_fum_Info infoObject)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                Sys_FunctionMaster_fum fum = new Sys_FunctionMaster_fum();
                try
                {

                    //fum = db.Sys_FunctionMaster_fums.SingleOrDefault(t => t.fum_iRecordID == infoObject.fum_iRecordID);

                    for (int i = 0; i < fum.Sys_FormPurview_frps.Count; i++)
                    {
                        Sys_FormPurview_frp sta = fum.Sys_FormPurview_frps[i];
                        db.Sys_FormPurview_frps.DeleteOnSubmit(sta);
                    }


                    string sqlString = string.Empty;
                    Sys_FunctionMaster_fum_Info info = new Sys_FunctionMaster_fum_Info();

                    //info = Common.General.CopyObjectValue<Sys_FunctionMaster_fum, Sys_FunctionMaster_fum_Info>(fum);

                    IEnumerable<Sys_FunctionMaster_fum_Info> IEfum = null;
                    IEnumerable<Sys_FormPurview_frp_Info> IEfrp = null;

                    sqlString += "UPDATE Sys_FunctionMaster_fum" + Environment.NewLine;
                    sqlString += " SET fum_cFunctionNumber='" + infoObject.fum_cFunctionNumber + "'," + Environment.NewLine;
                    sqlString += " fum_cFunctionDesc='" + infoObject.fum_cFunctionDesc + "'," + Environment.NewLine;
                    sqlString += " fum_cRemark='" + infoObject.fum_cRemark + "'," + Environment.NewLine;
                    sqlString += " fum_cLast='" + infoObject.fum_cLast + "'," + Environment.NewLine;
                    sqlString += " fum_dLastDate='" + infoObject.fum_dLastDate.Value.ToString(DefineConstantValue.gc_DateTimeFormat) + "'" + Environment.NewLine;

                    sqlString += " WHERE fum_iRecordID='" + infoObject.fum_iRecordID + "'";

                    IEfum = db.ExecuteQuery<Sys_FunctionMaster_fum_Info>(sqlString, new object[] { });

                    sqlString = string.Empty;
                    sqlString += "DELETE FROM Sys_FormPurview_frp WHERE frp_cFunctionNumber='" + infoObject.fum_cFunctionNumber + "'";
                    IEfrp = db.ExecuteQuery<Sys_FormPurview_frp_Info>(sqlString, new object[] { });


                    if (infoObject.formMaster != null && infoObject.formMaster.Count > 0)
                    {
                        for (int i = 0; i < infoObject.formMaster.Count; i++)
                        {
                            sqlString = string.Empty;
                            Sys_FormMaster_fom_Info fomInfo = infoObject.formMaster[i];

                            string frp_code = infoObject.fum_cFunctionNumber + fomInfo.fom_cFormNumber;

                            sqlString += "INSERT INTO Sys_FormPurview_frp" + Environment.NewLine;
                            sqlString += "(frp_cPurviewCode, frp_cFunctionNumber, frp_cFormNumber)" + Environment.NewLine;
                            sqlString += " VALUES " + Environment.NewLine;
                            sqlString += "('" + frp_code + "','" + infoObject.fum_cFunctionNumber + "','" + fomInfo.fom_cFormNumber + "')";

                            IEfrp = db.ExecuteQuery<Sys_FormPurview_frp_Info>(sqlString, new object[] { });
                        }
                    }



                    //if (fum != null)
                    //{
                    //    ////usm = Common.General.SetDataToLingQEntity(usm, infoObject);//时间溢出
                    //    //rlm.rlm_cRoleDesc = infoObject.rlm_cRoleDesc;
                    //    //rlm.rlm_cLast = infoObject.rlm_cLast;
                    //    //rlm.rlm_dLastDate = infoObject.rlm_dLastDate;

                    //    fum.fum_cFunctionNumber = infoObject.fum_cFunctionNumber;
                    //    fum.fum_cFunctionDesc = infoObject.fum_cFunctionDesc;
                    //    fum.fum_cRemark = infoObject.fum_cRemark;
                        
                    //    fum.fum_cLast = infoObject.fum_cLast;
                    //    fum.fum_dLastDate = infoObject.fum_dLastDate.Value;


                    //    if (infoObject.formMaster != null && infoObject.formMaster.Count > 0)
                    //    {
                    //        for (int i = 0; i < infoObject.formMaster.Count; i++)
                    //        {

                    //            Sys_FormMaster_fom_Info usmInfo = infoObject.formMaster[i];

                    //            Sys_FormPurview_frp item = new Sys_FormPurview_frp();

                    //            item.frp_cFunctionNumber = infoObject.fum_cFunctionNumber;
                    //            item.frp_cFormNumber = usmInfo.fom_cFormNumber;

                    //            item.frp_cPurviewCode = item.frp_cFunctionNumber + item.frp_cFormNumber;
                    //            //courseitem.cum_cNumber; 
                    //            fum.Sys_FormPurview_frps.Add(item);
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
            Sys_FunctionMaster_fum_Info info = null;
            try
            {
                info = KeyObject as Sys_FunctionMaster_fum_Info;
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    Sys_FunctionMaster_fum fum = db.Sys_FunctionMaster_fums.Single<Sys_FunctionMaster_fum>(i => i.fum_iRecordID == info.fum_iRecordID);

                    for (int i = 0; i < fum.Sys_FormPurview_frps.Count; i++)
                    {
                        Sys_FormPurview_frp sta = fum.Sys_FormPurview_frps[i];
                        db.Sys_FormPurview_frps.DeleteOnSubmit(sta);
                    }


                    db.Sys_FunctionMaster_fums.DeleteOnSubmit(fum);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Sys_FunctionMaster_fum_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            Sys_FunctionMaster_fum_Info fum = new Sys_FunctionMaster_fum_Info();
            Sys_FunctionMaster_fum_Info info = new Sys_FunctionMaster_fum_Info();
            fum = KeyObject as Sys_FunctionMaster_fum_Info;
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<Sys_FunctionMaster_fum> taQuery =
                        (from ta in db.Sys_FunctionMaster_fums
                         where ta.fum_iRecordID == fum.fum_iRecordID
                         //orderby dpms.dpm_iRecordID ascending
                         select ta).Take(1);

                    if (taQuery != null)
                    {
                        //foreach (var t in taQuery)
                        foreach (Sys_FunctionMaster_fum t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<Sys_FunctionMaster_fum, Sys_FunctionMaster_fum_Info>(t);
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

        public List<Sys_FunctionMaster_fum_Info> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " fum_iRecordID," + Environment.NewLine;
            sqlString += " fum_cFunctionNumber," + Environment.NewLine;
            sqlString += " fum_cFunctionDesc," + Environment.NewLine;
            sqlString += " fum_cRemark," + Environment.NewLine;
            sqlString += " fum_cAdd," + Environment.NewLine;
            sqlString += " fum_dAddDate," + Environment.NewLine;
            sqlString += " fum_cLast," + Environment.NewLine;
            sqlString += " fum_dLastDate " + Environment.NewLine;
            sqlString += " FROM Sys_FunctionMaster_fum" + Environment.NewLine;


            Sys_FunctionMaster_fum_Info info = null;

            info = searchCondition as Sys_FunctionMaster_fum_Info;

            if (info != null)
            {
                whereString = " WHERE 1=1 ";
                if (info.fum_cFunctionNumber.Trim() != "")
                {
                    if (info.fum_cFunctionNumber.ToString().Contains("*") || info.fum_cFunctionNumber.ToString().Contains("?"))
                    {
                        whereString += " AND fum_cFunctionNumber LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(info.fum_cFunctionNumber) + "'";
                    }
                    else
                    {
                        whereString += "AND fum_cFunctionNumber = N'" + info.fum_cFunctionNumber.ToString().Trim() + "'";
                    }
                }
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
