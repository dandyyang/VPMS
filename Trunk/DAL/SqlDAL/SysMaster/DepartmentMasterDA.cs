using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.SysMaster;
using model=Model.SysMaster;
using LinqToSQLModel;
using DAL.SqlDAL.LocalLayer;
using System.Data.Linq.SqlClient;
using Common;

namespace DAL.SqlDAL.SysMaster
{
    class DepartmentMasterDA:IDepartmentMasterDA
    {

        #region IDataBaseCommandDA<DepartmentMaster_dpm_Info> Members

        public Model.SysMaster.DepartmentMaster_dpm_Info GetRecord_First()
        {
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<DepartmentMaster_dpm> taQuery =
                        (from dpms in db.DepartmentMaster_dpms                         
                         orderby dpms.dpm_iRecordID ascending
                         select dpms).Take(1);
                    Model.SysMaster.DepartmentMaster_dpm_Info info = new Model.SysMaster.DepartmentMaster_dpm_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DepartmentMaster_dpm, model.DepartmentMaster_dpm_Info>(t);
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

        public Model.SysMaster.DepartmentMaster_dpm_Info GetRecord_Last()
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<DepartmentMaster_dpm> taQuery =
                        (from dpms in db.DepartmentMaster_dpms
                         orderby dpms.dpm_iRecordID descending
                         select dpms).Take(1);
                    Model.SysMaster.DepartmentMaster_dpm_Info info = new Model.SysMaster.DepartmentMaster_dpm_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DepartmentMaster_dpm, model.DepartmentMaster_dpm_Info>(t);
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

        public Model.SysMaster.DepartmentMaster_dpm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Model.SysMaster.DepartmentMaster_dpm_Info info = null;
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
                    IQueryable<DepartmentMaster_dpm> taQuery =
                        (from dpms in db.DepartmentMaster_dpms
                         where dpms.dpm_iRecordID < Convert.ToInt32(iRecordId)
                         orderby dpms.dpm_iRecordID descending
                         select dpms).Take(1);
                    
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DepartmentMaster_dpm, model.DepartmentMaster_dpm_Info>(t);
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

        public Model.SysMaster.DepartmentMaster_dpm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {

            Model.SysMaster.DepartmentMaster_dpm_Info info = null;
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
                    IQueryable<DepartmentMaster_dpm> taQuery =
                        (from dpms in db.DepartmentMaster_dpms
                         where dpms.dpm_iRecordID > Convert.ToInt32(iRecordId)
                         orderby dpms.dpm_iRecordID ascending
                         select dpms).Take(1);
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DepartmentMaster_dpm, model.DepartmentMaster_dpm_Info>(t);
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
                DepartmentMaster_dpm dpm = new DepartmentMaster_dpm();
                try
                {
                    dpm = Common.General.CopyObjectValue<object, DepartmentMaster_dpm>(KeyObject);

                    IQueryable<DepartmentMaster_dpm> taQuery =
                            (from dpms in db.DepartmentMaster_dpms where dpms.dpm_cNumber == dpm.dpm_cNumber select dpms);
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

        public Model.IModel.IModelObject GetTableFieldLenght()
        {
            LocalGeneral general = new LocalGeneral();
            model.DepartmentMaster_dpm_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<model.DepartmentMaster_dpm_Info>("DepartmentMaster_dpm");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;

        }

        #endregion

        #region IMainDA<DepartmentMaster_dpm_Info> Members

        public bool InsertRecord(Model.SysMaster.DepartmentMaster_dpm_Info infoObject)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                DepartmentMaster_dpm dpm = new DepartmentMaster_dpm();
                try
                {
                    dpm = Common.General.CopyObjectValue<model.DepartmentMaster_dpm_Info, DepartmentMaster_dpm>(infoObject);

                    db.DepartmentMaster_dpms.InsertOnSubmit(dpm);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public bool UpdateRecord(Model.SysMaster.DepartmentMaster_dpm_Info infoObject)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                //DepartmentMaster_dpm dpm = new DepartmentMaster_dpm();
                try
                {
                    //dpm = db.DepartmentMaster_dpms.SingleOrDefault(t => t.dpm_iRecordID == infoObject.dpm_iRecordID);                    
                    //model.DepartmentMaster_dpm_Info info = new Model.SysMaster.DepartmentMaster_dpm_Info();
                    //info = Common.General.CopyObjectValue<DepartmentMaster_dpm, Model.SysMaster.DepartmentMaster_dpm_Info>(dpm);


                    string sqlString = string.Empty;
                    IEnumerable<Model.SysMaster.DepartmentMaster_dpm_Info> IEdpm = null;

                    sqlString += "UPDATE DepartmentMaster_dpm" + Environment.NewLine;
                    sqlString += " SET dpm_cNumber='" + infoObject.dpm_cNumber + "'," + Environment.NewLine;
                    sqlString += " dpm_cName='" + infoObject.dpm_cName + "'," + Environment.NewLine;
                    sqlString += " dpm_cRemark='" + infoObject.dpm_cRemark + "'," + Environment.NewLine;
                    sqlString += " dpm_cLast='" + infoObject.dpm_cLast + "'," + Environment.NewLine;
                    sqlString += " dpm_dLastDate='" + infoObject.dpm_dLastDate.Value.ToString(DefineConstantValue.gc_DateTimeFormat) + "'" + Environment.NewLine;
                    sqlString += " WHERE dpm_iRecordID='" + infoObject.dpm_iRecordID + "'";

                    IEdpm = db.ExecuteQuery<Model.SysMaster.DepartmentMaster_dpm_Info>(sqlString, new object[] { });

                    //if (dpm != null)
                    //{
                    //    dpm.dpm_cName = infoObject.dpm_cName;
                    //    dpm.dpm_cNumber = infoObject.dpm_cNumber;
                    //    dpm.dpm_cRemark = infoObject.dpm_cRemark;
                    //    dpm.dpm_cLast = infoObject.dpm_cLast;
                    //    dpm.dpm_dLastDate = infoObject.dpm_dLastDate.Value;
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
            Model.SysMaster.DepartmentMaster_dpm_Info info = null;
            try
            {
                info = KeyObject as Model.SysMaster.DepartmentMaster_dpm_Info;
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    DepartmentMaster_dpm dpm = db.DepartmentMaster_dpms.Single<DepartmentMaster_dpm>(i => i.dpm_iRecordID == info.dpm_iRecordID);
                    db.DepartmentMaster_dpms.DeleteOnSubmit(dpm);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Model.SysMaster.DepartmentMaster_dpm_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            Model.SysMaster.DepartmentMaster_dpm_Info dpm = new Model.SysMaster.DepartmentMaster_dpm_Info();
            Model.SysMaster.DepartmentMaster_dpm_Info info = new Model.SysMaster.DepartmentMaster_dpm_Info();
            dpm = KeyObject as Model.SysMaster.DepartmentMaster_dpm_Info;
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<DepartmentMaster_dpm> taQuery =
                        (from dpms in db.DepartmentMaster_dpms
                         where dpms.dpm_iRecordID==dpm.dpm_iRecordID
                         //orderby dpms.dpm_iRecordID ascending
                         select dpms).Take(1);
                    
                    if (taQuery!=null)
                    {
                        foreach (var t in taQuery)
                      //foreach (DepartmentMaster_dpm t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<DepartmentMaster_dpm, model.DepartmentMaster_dpm_Info>(t);
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

        public List<Model.SysMaster.DepartmentMaster_dpm_Info> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " dpm_iRecordID," + Environment.NewLine;
            sqlString += " dpm_cNumber," + Environment.NewLine;
            sqlString += " dpm_cName," + Environment.NewLine;
            sqlString += " dpm_cRemark, " + Environment.NewLine;
            sqlString += " dpm_cAdd," + Environment.NewLine;
            sqlString += " dpm_dAddDate," + Environment.NewLine;
            sqlString += " dpm_cLast," + Environment.NewLine;
            sqlString += " dpm_dLastDate " + Environment.NewLine;

            sqlString += " FROM DepartmentMaster_dpm" + Environment.NewLine;


            model.DepartmentMaster_dpm_Info dpm_info = null;

            dpm_info = MDobject as model.DepartmentMaster_dpm_Info;

            if (dpm_info != null)
            {
                whereString = " WHERE 1=1 ";
                if (dpm_info.dpm_cNumber.Trim() != "")
                {
                    if (dpm_info.dpm_cNumber.ToString().Contains("*") || dpm_info.dpm_cNumber.ToString().Contains("?"))
                    {

                        whereString += " AND dpm_cNumber LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(dpm_info.dpm_cNumber) + "'";
                    }
                    else
                    {
                        whereString += "AND dpm_cNumber = N'" + dpm_info.dpm_cNumber.ToString().Trim() + "'";
                    }
                }

                if (dpm_info.dpm_cName.Trim() != "")
                {
                    if (dpm_info.dpm_cName.ToString().Contains("*") || dpm_info.dpm_cName.ToString().Contains("?"))
                    {
                        whereString += " AND dpm_cName LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(dpm_info.dpm_cName) + "'";
                    }
                    else
                    {
                        whereString += "AND dpm_cName = N'" + dpm_info.dpm_cName.ToString().Trim() + "'";
                    }
                }
            }

            sqlString += whereString;

            IEnumerable<model.DepartmentMaster_dpm_Info> infos = null;
            List<Model.SysMaster.DepartmentMaster_dpm_Info> infoList = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<model.DepartmentMaster_dpm_Info>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        infoList = infos.ToList<Model.SysMaster.DepartmentMaster_dpm_Info>();
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


        public bool IsMyLockedRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
