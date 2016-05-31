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

namespace DAL.SqlDAL.Management.Master
{
    class SiteMasterDA:ISiteMasterDA
    {
        #region IDataBaseCommandDA<SiteMaster_stm_info> Members

        public Model.Management.Master.SiteMaster_stm_Info GetRecord_First()
        {
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SiteMaster_stm> taQuery =
                        (from stms in db.SiteMaster_stms
                         orderby stms.stm_iRecordID ascending
                         select stms).Take(1);
                    SiteMaster_stm_Info info = new SiteMaster_stm_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SiteMaster_stm, SiteMaster_stm_Info>(t);
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

        public Model.Management.Master.SiteMaster_stm_Info GetRecord_Last()
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SiteMaster_stm> taQuery =
                        (from stms in db.SiteMaster_stms
                         orderby stms.stm_iRecordID descending
                         select stms).Take(1);
                    SiteMaster_stm_Info info = new SiteMaster_stm_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SiteMaster_stm, model.SiteMaster_stm_Info>(t);
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

        public Model.Management.Master.SiteMaster_stm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SiteMaster_stm_Info info = null;
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
                    IQueryable<SiteMaster_stm> taQuery =
                        (from stms in db.SiteMaster_stms
                         where stms.stm_iRecordID < Convert.ToInt32(iRecordId)
                         orderby stms.stm_iRecordID descending
                         select stms).Take(1);

                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SiteMaster_stm, SiteMaster_stm_Info>(t);
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

        public Model.Management.Master.SiteMaster_stm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SiteMaster_stm_Info info = null;
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
                    IQueryable<SiteMaster_stm> taQuery =
                        (from stms in db.SiteMaster_stms
                         where stms.stm_iRecordID > Convert.ToInt32(iRecordId)
                         orderby stms.stm_iRecordID ascending
                         select stms).Take(1);
                    if (taQuery.Count() > 0)
                    {
                        foreach (var t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SiteMaster_stm, SiteMaster_stm_Info>(t);
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
                SiteMaster_stm_Info stm = new SiteMaster_stm_Info();
                try
                {
                    stm = KeyObject as SiteMaster_stm_Info;
                    //stm = Common.General.CopyObjectValue<object, SiteMaster_stm_Info>(KeyObject);

                    IQueryable<SiteMaster_stm> taQuery =
                            (from stms in db.SiteMaster_stms where stms.stm_cNumber == stm.stm_cNumber select stms);
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
            SiteMaster_stm_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<SiteMaster_stm_Info>("SiteMaster_stm");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region IMainDA<SiteMaster_stm_info> Members

        public bool InsertRecord(Model.Management.Master.SiteMaster_stm_Info infoObject)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                SiteMaster_stm stm = new SiteMaster_stm();
                try
                {
                    stm = Common.General.CopyObjectValue<Model.Management.Master.SiteMaster_stm_Info, SiteMaster_stm>(infoObject);


                    db.SiteMaster_stms.InsertOnSubmit(stm);
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public bool UpdateRecord(Model.Management.Master.SiteMaster_stm_Info infoObject)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                SiteMaster_stm stm = new SiteMaster_stm();
                try
                {

                    stm = db.SiteMaster_stms.SingleOrDefault(t => t.stm_iRecordID == infoObject.stm_iRecordID);
                    if (stm != null)
                    {
                        stm.stm_cBuildingNumber = infoObject.stm_cBuildingNumber;
                        stm.stm_cName = infoObject.stm_cName;
                        stm.stm_cNumber = infoObject.stm_cNumber;
                        stm.stm_cRemark = infoObject.stm_cRemark;
                        stm.stm_cLast = infoObject.stm_cLast;

                        stm.stm_dLastDate = infoObject.stm_dLastDate.Value;

                        db.SubmitChanges();
                    }
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
            SiteMaster_stm_Info info = null;
            try
            {
                info = KeyObject as SiteMaster_stm_Info;
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    SiteMaster_stm stm = db.SiteMaster_stms.Single<SiteMaster_stm>(i => i.stm_iRecordID == info.stm_iRecordID);
                    db.SiteMaster_stms.DeleteOnSubmit(stm);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Model.Management.Master.SiteMaster_stm_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            SiteMaster_stm_Info stm = new SiteMaster_stm_Info();
            SiteMaster_stm_Info info = new SiteMaster_stm_Info();
            stm = KeyObject as SiteMaster_stm_Info;
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SiteMaster_stm> taQuery =
                        (from stms in db.SiteMaster_stms
                         where stms.stm_iRecordID == stm.stm_iRecordID
                         //orderby dpms.dpm_iRecordID ascending
                         select stms).Take(1);

                    if (taQuery != null)
                    {
                        //foreach (var t in taQuery)
                        foreach (SiteMaster_stm t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SiteMaster_stm, SiteMaster_stm_Info>(t);
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

        public List<Model.Management.Master.SiteMaster_stm_Info> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " stm.stm_iRecordID," + Environment.NewLine;
            sqlString += " stm.stm_cNumber," + Environment.NewLine;
            sqlString += " bdm.bdm_cName AS BuildingName," + Environment.NewLine;
            sqlString += " stm.stm_cName," + Environment.NewLine;
            sqlString += " stm.stm_cRemark, " + Environment.NewLine;
            sqlString += " stm.stm_cAdd," + Environment.NewLine;
            sqlString += " stm.stm_dAddDate," + Environment.NewLine;
            sqlString += " stm.stm_cLast," + Environment.NewLine;
            sqlString += " stm.stm_dLastDate " + Environment.NewLine;
            sqlString += " FROM SiteMaster_stm stm"+Environment.NewLine;
            sqlString += " LEFT JOIN BuildingMaster_bdm bdm" + Environment.NewLine;
            sqlString += " ON stm.stm_cBuildingNumber=bdm.bdm_cNumber" + Environment.NewLine;


            model.SiteMaster_stm_Info stm_info = null;

            stm_info = MDobject as model.SiteMaster_stm_Info;

            if (stm_info != null)
            {
                whereString = " WHERE 1=1 ";

                if (stm_info.stm_cBuildingNumber.Trim() != "")
                {
                    whereString += " AND stm_cBuildingNumber='" + stm_info.stm_cBuildingNumber.Trim()+"'"+Environment.NewLine;
                }

                if (stm_info.stm_cNumber.Trim() != "")
                {
                    if (stm_info.stm_cNumber.ToString().Contains("*") || stm_info.stm_cNumber.ToString().Contains("?"))
                    {
                        whereString += " AND stm_cNumber LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(stm_info.stm_cNumber) + "'";
                    }
                    else
                    {
                        whereString += "AND stm_cNumber = N'" + stm_info.stm_cNumber.ToString().Trim() + "'";
                    }
                }

                if (stm_info.stm_cName.Trim() != "")
                {
                    if (stm_info.stm_cName.ToString().Contains("*") || stm_info.stm_cName.ToString().Contains("?"))
                    {

                        whereString += " AND stm_cName LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(stm_info.stm_cName) + "'";
                    }
                    else
                    {
                        whereString += "AND stm_cName = N'" + stm_info.stm_cName.ToString().Trim() + "'";
                    }
                }
            }

            sqlString += whereString;

             IEnumerable<model.SiteMaster_stm_Info> infos = null;
            List<model.SiteMaster_stm_Info> infoList = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<model.SiteMaster_stm_Info>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        infoList = infos.ToList<model.SiteMaster_stm_Info>();
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
