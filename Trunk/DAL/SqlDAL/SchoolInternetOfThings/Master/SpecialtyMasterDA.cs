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
    class SpecialtyMasterDA : ISpecialtyMasterDA
    {
        #region IDataBaseCommandDA<SpecialtyMaster_spm_Info> Members

        public SpecialtyMaster_spm_Info GetRecord_First()
        {
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SpecialtyMaster_spm> taQuery =
                        (from spms in db.SpecialtyMaster_spms
                         orderby spms.spm_iRecordID ascending
                         select spms).Take(1);
                    SpecialtyMaster_spm_Info info = new SpecialtyMaster_spm_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (SpecialtyMaster_spm t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SpecialtyMaster_spm, SpecialtyMaster_spm_Info>(t);
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

        public SpecialtyMaster_spm_Info GetRecord_Last()
        {
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SpecialtyMaster_spm> taQuery =
                        (from spms in db.SpecialtyMaster_spms
                         orderby spms.spm_iRecordID descending
                         select spms).Take(1);
                    SpecialtyMaster_spm_Info info = new SpecialtyMaster_spm_Info();
                    if (taQuery.Count() > 0)
                    {
                        foreach (SpecialtyMaster_spm t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SpecialtyMaster_spm, model.SpecialtyMaster_spm_Info>(t);
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

        public SpecialtyMaster_spm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SpecialtyMaster_spm_Info info = null;
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
                    IQueryable<SpecialtyMaster_spm> taQuery =
                        (from spms in db.SpecialtyMaster_spms
                         where spms.spm_iRecordID < Convert.ToInt32(iRecordId)
                         orderby spms.spm_iRecordID descending
                         select spms).Take(1);

                    if (taQuery.Count() > 0)
                    {
                        foreach (SpecialtyMaster_spm t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SpecialtyMaster_spm, SpecialtyMaster_spm_Info>(t);
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

        public SpecialtyMaster_spm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SpecialtyMaster_spm_Info info = null;
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
                    IQueryable<SpecialtyMaster_spm> taQuery =
                        (from spms in db.SpecialtyMaster_spms
                         where spms.spm_iRecordID > Convert.ToInt32(iRecordId)
                         orderby spms.spm_iRecordID ascending
                         select spms).Take(1);
                    if (taQuery.Count() > 0)
                    {
                        foreach (SpecialtyMaster_spm t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SpecialtyMaster_spm, SpecialtyMaster_spm_Info>(t);
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
                SpecialtyMaster_spm_Info spm = new SpecialtyMaster_spm_Info();
                try
                {
                    spm = Common.General.CopyObjectValue<object, SpecialtyMaster_spm_Info>(KeyObject);

                    IQueryable<SpecialtyMaster_spm> taQuery =
                            (from spms in db.SpecialtyMaster_spms where spms.spm_cNumber == spm.spm_cNumber select spms);
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
            SpecialtyMaster_spm_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<SpecialtyMaster_spm_Info>("SpecialtyMaster_spm");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region IMainDA<SpecialtyMaster_spm_Info> Members

        public bool InsertRecord(SpecialtyMaster_spm_Info infoObject)
        {
            if (infoObject == null)
            {
                return false;
            }

            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                SpecialtyMaster_spm spm = new SpecialtyMaster_spm();
                try
                {
                    spm = Common.General.CopyObjectValue<Model.Management.Master.SpecialtyMaster_spm_Info, SpecialtyMaster_spm>(infoObject);

                    IQueryable<SpecialtyMaster_spm> taQuery =
                            (from spms in db.SpecialtyMaster_spms where spms.spm_cNumber == spm.spm_cNumber select spms);

                    db.SpecialtyMaster_spms.InsertOnSubmit(spm);
                    db.SubmitChanges();
                    return true;

                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public bool UpdateRecord(SpecialtyMaster_spm_Info infoObject)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                SpecialtyMaster_spm spm = new SpecialtyMaster_spm();
                try
                {

                    spm = db.SpecialtyMaster_spms.SingleOrDefault(t => t.spm_iRecordID == infoObject.spm_iRecordID);
                    for (int i = 0; i < spm.SpecialtyMasterCourse_smcs.Count; i++)
                    {
                        SpecialtyMasterCourse_smc sta = spm.SpecialtyMasterCourse_smcs[i];
                        db.SpecialtyMasterCourse_smcs.DeleteOnSubmit(sta);
                    }
                    if (spm != null)
                    {
                        spm.spm_cName = infoObject.spm_cName;
                        spm.spm_cNumber = infoObject.spm_cNumber;
                        spm.spm_cRemark = infoObject.spm_cRemark;
                        spm.spm_cLast = infoObject.spm_cLast;
                        spm.spm_dLastDate = infoObject.spm_dLastDate.Value;

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
            SpecialtyMaster_spm_Info info = null;
            try
            {
                info = KeyObject as SpecialtyMaster_spm_Info;
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    SpecialtyMaster_spm spm = db.SpecialtyMaster_spms.Single<SpecialtyMaster_spm>(i => i.spm_iRecordID == info.spm_iRecordID);

                    for (int i = 0; i < spm.SpecialtyMasterCourse_smcs.Count; i++)
                    {
                        SpecialtyMasterCourse_smc sta = spm.SpecialtyMasterCourse_smcs[i];
                        db.SpecialtyMasterCourse_smcs.DeleteOnSubmit(sta);
                    }

                    db.SpecialtyMaster_spms.DeleteOnSubmit(spm);

                    db.SubmitChanges();
                }


                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public SpecialtyMaster_spm_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            SpecialtyMaster_spm_Info spm = new SpecialtyMaster_spm_Info();
            SpecialtyMaster_spm_Info info = new SpecialtyMaster_spm_Info();
            spm = KeyObject as SpecialtyMaster_spm_Info;
            try
            {

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IQueryable<SpecialtyMaster_spm> taQuery =
                        (from spms in db.SpecialtyMaster_spms
                         where spms.spm_iRecordID == spm.spm_iRecordID
                         //orderby dpms.dpm_iRecordID ascending
                         select spms).Take(1);

                    if (taQuery != null)
                    {
                        //foreach (var t in taQuery)
                        foreach (SpecialtyMaster_spm t in taQuery)
                        {
                            info = Common.General.CopyObjectValue<SpecialtyMaster_spm, SpecialtyMaster_spm_Info>(t);
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

        public List<SpecialtyMaster_spm_Info> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            string sqlString = string.Empty;
            string whereString = string.Empty;

            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += " spm_iRecordID," + Environment.NewLine;
            sqlString += " spm_cNumber," + Environment.NewLine;
            sqlString += " spm_cName," + Environment.NewLine;
            sqlString += " spm_cDesc," + Environment.NewLine;
            sqlString += " spm_cRemark, " + Environment.NewLine;
            sqlString += " spm_cAdd," + Environment.NewLine;
            sqlString += " spm_dAddDate," + Environment.NewLine;
            sqlString += " spm_cLast," + Environment.NewLine;
            sqlString += " spm_dLastDate " + Environment.NewLine;

            sqlString += " FROM SpecialtyMaster_spm" + Environment.NewLine;


            model.SpecialtyMaster_spm_Info dpm_info = null;

            dpm_info = MDobject as model.SpecialtyMaster_spm_Info;

            if (dpm_info != null)
            {
                whereString = " WHERE 1=1 ";

                if (dpm_info.spm_cNumber.Trim() != "")
                {
                    if (dpm_info.spm_cNumber.ToString().Contains("*") || dpm_info.spm_cNumber.ToString().Contains("?"))
                    {

                        whereString += " AND spm_cNumber LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(dpm_info.spm_cNumber) + "'";
                    }
                    else
                    {
                        whereString += "AND spm_cNumber = N'" + dpm_info.spm_cNumber.ToString().Trim() + "'";
                    }
                }

                if (dpm_info.spm_cName.Trim() != "")
                {
                    if (dpm_info.spm_cName.ToString().Contains("*") || dpm_info.spm_cName.ToString().Contains("?"))
                    {

                        whereString += " AND spm_cName LIKE N'" + LocalDefine.General.ReplaceSQLLikeCondition(dpm_info.spm_cName) + "'";
                    }
                    else
                    {
                        whereString += "AND spm_cName = N'" + dpm_info.spm_cName.ToString().Trim() + "'";
                    }
                }
            }

            sqlString += whereString;

            IEnumerable<model.SpecialtyMaster_spm_Info> infos = null;
            List<SpecialtyMaster_spm_Info> infoList = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<model.SpecialtyMaster_spm_Info>(sqlString, new object[] { });

                    if (infos != null)
                    {
                        infoList = infos.ToList<SpecialtyMaster_spm_Info>();
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
