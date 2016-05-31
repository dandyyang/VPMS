using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Management.Master;
using model=Model.Management.Master;
using LinqToSQLModel;
using DAL.SqlDAL.LocalLayer;
using Model.Management.Master;
using System.Data.Linq.SqlClient;

namespace DAL.SqlDAL.Management.Master
{
    class SchoolMasterDA:ISchoolMasterDA
    {
        #region IDataBaseCommandDA<SchoolMaster_scm> Members

        public Model.Management.Master.SchoolMaster_scm_Info GetRecord_First()
        {
            IQueryable<SchoolMaster_scm> taQuery = null;
            Model.Management.Master.SchoolMaster_scm_Info tab = null ;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    taQuery =
                        (from ta in db.SchoolMaster_scms
                         orderby ta.scm_iRecordID ascending
                         select ta).Take(1);

                    foreach (SchoolMaster_scm t in taQuery)
                    {
                        tab=Common.General.CopyObjectValue<SchoolMaster_scm, SchoolMaster_scm_Info>(t);
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

        public Model.Management.Master.SchoolMaster_scm_Info GetRecord_Last()
        {
            IQueryable<SchoolMaster_scm> taQuery = null;
            Model.Management.Master.SchoolMaster_scm_Info tab = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    taQuery =
                     (from ta in db.SchoolMaster_scms
                      orderby ta.scm_iRecordID descending
                      select ta).Take(1);

                    foreach (SchoolMaster_scm t in taQuery)
                    {
                        tab = Common.General.CopyObjectValue<SchoolMaster_scm, Model.Management.Master.SchoolMaster_scm_Info>(t);
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
        
        public Model.Management.Master.SchoolMaster_scm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            string RecordID = null;
            IQueryable<SchoolMaster_scm> taQuery = null;
            Model.Management.Master.SchoolMaster_scm_Info tab = null;
            try 
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {

                    if (commandInfo.KeyInfoList != null)
                    {
                        //foreach (var t in commandInfo.KeyInfoList)
                        //{
                        //    if (t.Key == "scm_iRecordID")
                        //    {
                        //        RecordID = t.KeyValue.ToString();
                        //        break;
                        //    }
                        //}
                        RecordID = commandInfo.KeyInfoList[0].KeyValue.ToString();
                        taQuery =
                           (from ta in db.SchoolMaster_scms
                            where ta.scm_iRecordID < Convert.ToInt32(RecordID)
                            orderby ta.scm_iRecordID descending
                            select ta).Take(1);

                        if (taQuery.Count() > 0)
                        {
                            foreach (SchoolMaster_scm t in taQuery)
                            {
                             tab= Common.General.CopyObjectValue<SchoolMaster_scm, SchoolMaster_scm_Info>(t);
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

        public Model.Management.Master.SchoolMaster_scm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            IQueryable<SchoolMaster_scm> taQuery = null;
            Model.Management.Master.SchoolMaster_scm_Info tab = null;
            try 
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    string RecordID = null;
                    if (commandInfo.KeyInfoList != null)
                    {
                        //foreach (var t in commandInfo.KeyInfoList)
                        //{
                        //    if (t.Key == "scm_iRecordID")
                        //    {
                        //        RecordID = t.KeyValue.ToString();
                        //        break;
                        //    }
                        //}
                        RecordID = commandInfo.KeyInfoList[0].KeyValue.ToString();
                        taQuery =
                           (from ta in db.SchoolMaster_scms
                            where ta.scm_iRecordID > Convert.ToInt32(RecordID)
                            orderby ta.scm_iRecordID ascending
                            select ta).Take(1);

                        if (taQuery.Count() > 0)
                        {
                            foreach (SchoolMaster_scm t in taQuery)
                            {
                                tab = Common.General.CopyObjectValue<SchoolMaster_scm, SchoolMaster_scm_Info>(t);
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

        #region IMainDA<SchoolMaster_scm> Members

        public bool InsertRecord(Model.Management.Master.SchoolMaster_scm_Info infoObject)
        {
            SchoolMaster_scm tab = null;
            bool isSuccess = false;
            tab = Common.General.CopyObjectValue<SchoolMaster_scm_Info, SchoolMaster_scm>(infoObject);
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext()) 
                {
                        db.SchoolMaster_scms.InsertOnSubmit(tab);
                        db.SubmitChanges();
                        isSuccess = true;
                }
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public bool UpdateRecord(Model.Management.Master.SchoolMaster_scm_Info infoObject)
        {
            SchoolMaster_scm tab = new SchoolMaster_scm();
            bool isSuccess = false;
            try 
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext()) 
                {
                  tab= db.SchoolMaster_scms.SingleOrDefault(t => t.scm_cNumber == infoObject.scm_cNumber);
                  if (tab != null) 
                  {
                      tab.scm_cName = infoObject.scm_cName;
                      tab.scm_cRemark = infoObject.scm_cRemark;
                      tab.scm_cLast = infoObject.scm_cLast;
                      tab.scm_dLastDate = infoObject.scm_dLastDate;
                      //tab=Common.General.SetDataToLingQEntity(tab, infoObject);
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
            bool isSuccess = false;
            SchoolMaster_scm tab = null;
            try {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    tab = db.SchoolMaster_scms.SingleOrDefault(t => t.scm_iRecordID == KeyObject.RecordID);
                    if (tab != null)
                    {
                        db.SchoolMaster_scms.DeleteOnSubmit(tab);
                        db.SubmitChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public Model.Management.Master.SchoolMaster_scm_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            model.SchoolMaster_scm_Info info = null;
            SchoolMaster_scm tab = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    info = new model.SchoolMaster_scm_Info();
                    tab = db.SchoolMaster_scms.SingleOrDefault(t => t.scm_iRecordID == (KeyObject as SchoolMaster_scm_Info).RecordID);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (tab != null)
            {
                info = Common.General.CopyObjectValue<SchoolMaster_scm, SchoolMaster_scm_Info>(tab);
            }
            return info;
        }

        public List<Model.Management.Master.SchoolMaster_scm_Info> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            SchoolMaster_scm_Info info = null;
            IQueryable<SchoolMaster_scm> taQuery = null;
            info = Common.General.CopyObjectValue<Model.IModel.IModelObject, SchoolMaster_scm_Info>(MDobject);
            List<Model.Management.Master.SchoolMaster_scm_Info> list = new List<SchoolMaster_scm_Info>();
            try 
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    taQuery = db.SchoolMaster_scms.Take(Common.DefineConstantValue.ListRecordMaxCount);
                    if (info.scm_cName.ToString()!="")
                    {
                        if (info.scm_cName.ToString().IndexOf("*") != -1 || info.scm_cName.ToString().IndexOf("?") != -1)
                        {
                            taQuery = from t in taQuery
                                              where SqlMethods.Like(t.scm_cName, info.scm_cName.ToString().Replace("*","%").Replace("?","_"))
                                              select t;
                        }
                        else 
                        {
                            taQuery = taQuery.Where(t => t.scm_cName.Contains(info.scm_cName));
                        }
                    }
                    if (info.scm_cNumber.ToString()!="")
                    {
                        if (info.scm_cNumber.ToString().IndexOf("*") != -1 || info.scm_cNumber.ToString().IndexOf("?") != -1)
                        {
                            taQuery = from t in taQuery
                                              where SqlMethods.Like(t.scm_cNumber, info.scm_cNumber.ToString().Replace("*", "%").Replace("?", "_"))
                                              select t;
                        }
                        else
                        {
                            taQuery = taQuery.Where(t => t.scm_cNumber.Contains(info.scm_cNumber));
                        }
                    }
                    if (taQuery != null) 
                    {
                        foreach (SchoolMaster_scm t in taQuery.ToList())
                        {
                            info = Common.General.CopyObjectValue<SchoolMaster_scm, SchoolMaster_scm_Info>(t);
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
            SchoolMaster_scm info = null;
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                info = db.SchoolMaster_scms.SingleOrDefault(t => t.scm_cNumber == KeyObject.ToString().Trim());
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
            SchoolMaster_scm_Info info = null;

            try
            {
                info = general.GetTableFieldLenght<SchoolMaster_scm_Info>("SchoolMaster_scm");
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
