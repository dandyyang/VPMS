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
    class BuildingMasterDA:IBuildingMasterDA
    {

        #region IDataBaseCommandDA<BuildingMaster_bdm_Info> Members

        public Model.Management.Master.BuildingMaster_bdm_Info GetRecord_First()
        {
            IQueryable<BuildingMaster_bdm> taQuery = null;
            Model.Management.Master.BuildingMaster_bdm_Info tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    taQuery =
                        (from ta in db.BuildingMaster_bdms
                         orderby ta.bdm_iRecordID ascending
                         select ta).Take(1);

                    foreach (BuildingMaster_bdm t in taQuery)
                    {
                        tab = Common.General.CopyObjectValue<BuildingMaster_bdm, BuildingMaster_bdm_Info>(t);
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return tab;
        }

        public Model.Management.Master.BuildingMaster_bdm_Info GetRecord_Last()
        {
            IQueryable<BuildingMaster_bdm> taQuery = null;
            Model.Management.Master.BuildingMaster_bdm_Info tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    taQuery =
                     (from ta in db.BuildingMaster_bdms
                      orderby ta.bdm_iRecordID descending
                      select ta).Take(1);

                    foreach (BuildingMaster_bdm t in taQuery)
                    {
                        tab = Common.General.CopyObjectValue<BuildingMaster_bdm, Model.Management.Master.BuildingMaster_bdm_Info>(t);
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return tab;
        }

        public Model.Management.Master.BuildingMaster_bdm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            string RecordID = null;
            IQueryable<BuildingMaster_bdm> taQuery = null;
            Model.Management.Master.BuildingMaster_bdm_Info tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {

                    if (commandInfo.KeyInfoList != null)
                    {
                        //foreach (var t in commandInfo.KeyInfoList)
                        //{
                        //    if (t.Key == "bdm_iRecordID")
                        //    {
                        //        RecordID = t.KeyValue.ToString();
                        //        break;
                        //    }
                        //}
                        RecordID = commandInfo.KeyInfoList[0].KeyValue.ToString();
                        taQuery =
                           (from ta in db.BuildingMaster_bdms
                            where ta.bdm_iRecordID < Convert.ToInt32(RecordID)
                            orderby ta.bdm_iRecordID descending
                            select ta).Take(1);

                        if (taQuery.Count() > 0)
                        {
                            foreach (BuildingMaster_bdm t in taQuery)
                            {
                                tab = Common.General.CopyObjectValue<BuildingMaster_bdm, BuildingMaster_bdm_Info>(t);
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
            return tab;
        }

        public Model.Management.Master.BuildingMaster_bdm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            IQueryable<BuildingMaster_bdm> taQuery = null;
            Model.Management.Master.BuildingMaster_bdm_Info tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    string RecordID = null;
                    if (commandInfo.KeyInfoList != null)
                    {
                        //foreach (var t in commandInfo.KeyInfoList)
                        //{
                        //    if (t.Key == "bdm_iRecordID")
                        //    {
                        //        RecordID = t.KeyValue.ToString();
                        //        break;
                        //    }
                        //}
                        RecordID = commandInfo.KeyInfoList[0].KeyValue.ToString();
                        taQuery =
                           (from ta in db.BuildingMaster_bdms
                            where ta.bdm_iRecordID > Convert.ToInt32(RecordID)
                            orderby ta.bdm_iRecordID ascending
                            select ta).Take(1);

                        if (taQuery.Count() > 0)
                        {
                            foreach (BuildingMaster_bdm t in taQuery)
                            {
                                tab = Common.General.CopyObjectValue<BuildingMaster_bdm, BuildingMaster_bdm_Info>(t);
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
            return tab;
        }

        #endregion

        #region IMainDA<BuildingMaster_bdm_Info> Members

        public bool InsertRecord(Model.Management.Master.BuildingMaster_bdm_Info infoObject)
        {
            BuildingMaster_bdm tab = null;
            bool isSuccess = false;
            tab = Common.General.CopyObjectValue<BuildingMaster_bdm_Info, BuildingMaster_bdm>(infoObject);
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    db.BuildingMaster_bdms.InsertOnSubmit(tab);
                    db.SubmitChanges();
                    isSuccess = true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public bool UpdateRecord(Model.Management.Master.BuildingMaster_bdm_Info infoObject)
        {
            BuildingMaster_bdm tab = new BuildingMaster_bdm();
            bool isSuccess = false;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    tab = db.BuildingMaster_bdms.SingleOrDefault(t => t.bdm_cNumber == infoObject.bdm_cNumber);
                    if (tab != null)
                    {
                        tab.bdm_cName = infoObject.bdm_cName;
                        tab.bdm_cRemark = infoObject.bdm_cRemark;
                        tab.bdm_cLast = infoObject.bdm_cLast;
                        tab.bdm_dLastDate = (infoObject.bdm_dLastDate!=null?infoObject.bdm_dLastDate.Value:DateTime.Now);
                    }
                    db.SubmitChanges();
                    isSuccess = true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            bool Btab = false;
            BuildingMaster_bdm tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    tab = db.BuildingMaster_bdms.SingleOrDefault(t => t.bdm_iRecordID == KeyObject.RecordID);
                    if (tab != null)
                    {
                        db.BuildingMaster_bdms.DeleteOnSubmit(tab);
                        db.SubmitChanges();
                        Btab = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Btab;
        }

        public Model.Management.Master.BuildingMaster_bdm_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            model.BuildingMaster_bdm_Info info = null;
            BuildingMaster_bdm tab = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    info = new model.BuildingMaster_bdm_Info();
                    tab = db.BuildingMaster_bdms.SingleOrDefault(t => t.bdm_iRecordID == (KeyObject as BuildingMaster_bdm_Info).bdm_iRecordID);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (tab != null)
            {
                info = Common.General.CopyObjectValue<BuildingMaster_bdm, BuildingMaster_bdm_Info>(tab);
            }
            return info;
        }

        public List<Model.Management.Master.BuildingMaster_bdm_Info> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            BuildingMaster_bdm_Info info = null;
            IQueryable<BuildingMaster_bdm> taQuery = null;
            info = Common.General.CopyObjectValue<Model.IModel.IModelObject, BuildingMaster_bdm_Info>(MDobject);
            List<Model.Management.Master.BuildingMaster_bdm_Info> list = new List<BuildingMaster_bdm_Info>();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    taQuery = db.BuildingMaster_bdms.Take(Common.DefineConstantValue.ListRecordMaxCount);
                    if (info.bdm_cName.ToString() != "")
                    {
                        if (info.bdm_cName.ToString().IndexOf("*") != -1 || info.bdm_cName.ToString().IndexOf("?") != -1)
                        {
                            taQuery = from t in taQuery
                                      where SqlMethods.Like(t.bdm_cName, info.bdm_cName.ToString().Replace("*", "%").Replace("?", "_"))
                                      select t;
                        }
                        else
                        {
                            taQuery = taQuery.Where(t => t.bdm_cName.Contains(info.bdm_cName));
                        }
                    }
                    if (info.bdm_cNumber.ToString() != "")
                    {
                        if (info.bdm_cNumber.ToString().IndexOf("*") != -1 || info.bdm_cNumber.ToString().IndexOf("?") != -1)
                        {
                            taQuery = from t in taQuery
                                      where SqlMethods.Like(t.bdm_cNumber, info.bdm_cNumber.ToString().Replace("*", "%").Replace("?", "_"))
                                      select t;
                        }
                        else
                        {
                            taQuery = taQuery.Where(t => t.bdm_cNumber.Contains(info.bdm_cNumber));
                        }
                    }
                    if (taQuery != null)
                    {
                        foreach (BuildingMaster_bdm t in taQuery.ToList())
                        {
                            info = Common.General.CopyObjectValue<BuildingMaster_bdm, BuildingMaster_bdm_Info>(t);
                            list.Add(info);
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

        #endregion

        #region IExtraDA Members

        public bool IsExistRecord(object KeyObject)
        {
            BuildingMaster_bdm info = null;
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                info = db.BuildingMaster_bdms.SingleOrDefault(t => t.bdm_cNumber == KeyObject.ToString().Trim());
                if (info != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsLockRecord(object KeyObject)
        {
            return true;
        }

        public bool IsUnLockRecord(object KeyObject)
        {
            return true;
        }

        public Model.IModel.IModelObject GetTableFieldLenght()
        {
            LocalGeneral general = new LocalGeneral();
            BuildingMaster_bdm_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<BuildingMaster_bdm_Info>("BuildingMaster_bdm");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region IExtraDA Members


        public Model.General.ReturnValueInfo LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
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
