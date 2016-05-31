using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.SysMaster;
using Model.SysMaster;
using Model.Base;
using Model.IModel;
using Model.General;
using LinqToSQLModel;

namespace DAL.SqlDAL.SysMaster
{
    public class SupplierMasterDA : ISupplierMasterDA
    {

        #region IDataBaseCommandDA<Sys_SupplierMaster_slm_Info> Members

        public Sys_SupplierMaster_slm_Info GetRecord_First()
        {
            Sys_SupplierMaster_slm_Info supplierMasterInfo = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {

                    IEnumerable<SupplierMaster_slm> query = db.SupplierMaster_slms.OrderBy(t => t.slm_iRecordID);

                    if (query != null && query.Count() > 0)
                    {
                        SupplierMaster_slm supplierMaster = query.First();

                        if (supplierMaster != null)
                        {
                            supplierMasterInfo = Common.General.CopyObjectValue<SupplierMaster_slm, Sys_SupplierMaster_slm_Info>(supplierMaster);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return supplierMasterInfo;
        }

        public Sys_SupplierMaster_slm_Info GetRecord_Last()
        {
            Sys_SupplierMaster_slm_Info supplierMasterInfo = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    IEnumerable<SupplierMaster_slm> query = db.SupplierMaster_slms.OrderByDescending(t => t.slm_iRecordID);

                    if (query != null && query.Count() > 0)
                    {
                        SupplierMaster_slm supplierMaster = query.First();

                        if (supplierMaster != null)
                        {
                            supplierMasterInfo = Common.General.CopyObjectValue<SupplierMaster_slm, Sys_SupplierMaster_slm_Info>(supplierMaster);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return supplierMasterInfo;
        }

        public Sys_SupplierMaster_slm_Info GetRecord_Previous(DataBaseCommandInfo commandInfo)
        {
            Sys_SupplierMaster_slm_Info supplierMasterInfo = null;

            if (commandInfo != null && commandInfo.KeyInfoList != null && commandInfo.KeyInfoList[0] != null && commandInfo.KeyInfoList[0].KeyValue != "")
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        SupplierMaster_slm supplierMaster = (from t in db.SupplierMaster_slms
                                                             where t.slm_iRecordID < Convert.ToInt32(commandInfo.KeyInfoList[0].KeyValue)
                                                             orderby t.slm_iRecordID descending
                                                             select t).FirstOrDefault();

                        if (supplierMaster != null)
                        {
                            supplierMasterInfo = Common.General.CopyObjectValue<SupplierMaster_slm, Sys_SupplierMaster_slm_Info>(supplierMaster);
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }


            return supplierMasterInfo;
        }

        public Sys_SupplierMaster_slm_Info GetRecord_Next(DataBaseCommandInfo commandInfo)
        {
            Sys_SupplierMaster_slm_Info supplierMasterInfo = null;

            if (commandInfo != null && commandInfo.KeyInfoList != null && commandInfo.KeyInfoList[0] != null && commandInfo.KeyInfoList[0].KeyValue != "")
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        SupplierMaster_slm supplierMaster = (from t in db.SupplierMaster_slms
                                                             where t.slm_iRecordID > Convert.ToInt32(commandInfo.KeyInfoList[0].KeyValue)
                                                             orderby t.slm_iRecordID ascending
                                                             select t).FirstOrDefault();

                        if (supplierMaster != null)
                        {
                            supplierMasterInfo = Common.General.CopyObjectValue<SupplierMaster_slm, Sys_SupplierMaster_slm_Info>(supplierMaster);
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }


            return supplierMasterInfo;
        }

        #endregion

        #region IExtraDA Members

        public bool IsExistRecord(object KeyObject)
        {
            bool isExist = true;

            Sys_SupplierMaster_slm_Info supplierMasterInfo = KeyObject as Sys_SupplierMaster_slm_Info;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    SupplierMaster_slm supplierMaster = db.SupplierMaster_slms.FirstOrDefault(t => t.slm_cClientNum == supplierMasterInfo.slm_cClientNum);

                    if (supplierMaster != null)
                    {
                        isExist = true;
                    }
                    else
                    {
                        isExist = false;
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return isExist;
        }

        public ReturnValueInfo LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public ReturnValueInfo UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public bool IsMyLockedRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public IModelObject GetTableFieldLenght()
        {
            Sys_SupplierMaster_slm_Info supplierMasterInfo = new Sys_SupplierMaster_slm_Info();

            supplierMasterInfo.slm_cClientNumLength = 30;

            supplierMasterInfo.slm_cChinaNameLength = 100;

            supplierMasterInfo.slm_cEnglishNameLength = 50;

            supplierMasterInfo.slm_cTaxNumberLength = 100;

            supplierMasterInfo.slm_cLinkmanLength = 100;

            supplierMasterInfo.slm_cAddressLength = 100;

            supplierMasterInfo.slm_cPhoneLength = 50;

            supplierMasterInfo.slm_cFaxLength = 50;

            supplierMasterInfo.slm_cWebSiteLength = 50;

            supplierMasterInfo.slm_cRemarkLength = 100;

            return supplierMasterInfo;
        }

        #endregion

        #region IMainDA<Sys_SupplierMaster_slm_Info> Members

        public bool InsertRecord(Sys_SupplierMaster_slm_Info infoObject)
        {
            bool isSuccess = false;

            if (infoObject != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        SupplierMaster_slm supplierMaster = Common.General.CopyObjectValue<Sys_SupplierMaster_slm_Info, SupplierMaster_slm>(infoObject);

                        db.SupplierMaster_slms.InsertOnSubmit(supplierMaster);

                        db.SubmitChanges();

                        isSuccess = true;
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return isSuccess;
        }

        public bool UpdateRecord(Sys_SupplierMaster_slm_Info infoObject)
        {
            bool isSuccess = false;

            if (infoObject != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        SupplierMaster_slm supplierMaster = db.SupplierMaster_slms.FirstOrDefault(t => t.slm_iRecordID == infoObject.slm_iRecordID);

                        if (supplierMaster != null)
                        {
                            supplierMaster.slm_cChinaName = infoObject.slm_cChinaName;

                            supplierMaster.slm_cEnglishName = infoObject.slm_cEnglishName;

                            supplierMaster.slm_cTaxNumber = infoObject.slm_cTaxNumber;

                            supplierMaster.slm_cLinkman = infoObject.slm_cLinkman;

                            supplierMaster.slm_cAddress = infoObject.slm_cAddress;

                            supplierMaster.slm_cPhone = infoObject.slm_cPhone;

                            supplierMaster.slm_cFax = infoObject.slm_cFax;

                            supplierMaster.slm_cWebSite = infoObject.slm_cWebSite;

                            supplierMaster.slm_cRemark = infoObject.slm_cRemark;

                            supplierMaster.slm_dLastDate = DateTime.Now;

                            supplierMaster.slm_cLast = infoObject.slm_cLast;

                            db.SubmitChanges();

                            isSuccess = true;
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return isSuccess;
        }

        public bool DeleteRecord(IModelObject KeyObject)
        {
            bool isSuccess = false;

            if (KeyObject != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        SupplierMaster_slm supplierMaster = db.SupplierMaster_slms.FirstOrDefault(t => t.slm_iRecordID == (KeyObject as Sys_SupplierMaster_slm_Info).slm_iRecordID);

                        if (supplierMaster != null)
                        {
                            db.SupplierMaster_slms.DeleteOnSubmit(supplierMaster);

                            db.SubmitChanges();

                            isSuccess = true;
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }


            return isSuccess;
        }

        public Sys_SupplierMaster_slm_Info DisplayRecord(IModelObject KeyObject)
        {
            Sys_SupplierMaster_slm_Info displayInfo = null;

            Sys_SupplierMaster_slm_Info queryInfo = KeyObject as Sys_SupplierMaster_slm_Info;

            if (queryInfo != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        SupplierMaster_slm supplierMaster = db.SupplierMaster_slms.FirstOrDefault(t => t.slm_iRecordID == queryInfo.slm_iRecordID);

                        if (supplierMaster != null)
                        {
                            displayInfo = Common.General.CopyObjectValue<SupplierMaster_slm, Sys_SupplierMaster_slm_Info>(supplierMaster);
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return displayInfo;
        }

        public List<Sys_SupplierMaster_slm_Info> SearchRecords(IModelObject searchCondition)
        {
            List<Sys_SupplierMaster_slm_Info> resultList = null;

            string sqlScript = string.Empty;

            Sys_SupplierMaster_slm_Info queryInfo = searchCondition as Sys_SupplierMaster_slm_Info;

            if (queryInfo != null)
            {
                resultList = new List<Sys_SupplierMaster_slm_Info>();

                sqlScript += "select * from dbo.SupplierMaster_slm" + Environment.NewLine;

                sqlScript += "where 1=1" + Environment.NewLine;

                if (queryInfo.slm_cClientNum != null && queryInfo.slm_cClientNum != "")
                {
                    sqlScript += "and slm_cClientNum like '%" + queryInfo.slm_cClientNum + "%'" + Environment.NewLine;
                }

                if (queryInfo.slm_cChinaName != null && queryInfo.slm_cChinaName != "")
                {
                    sqlScript += "and slm_cChinaName like '%" + queryInfo.slm_cChinaName + "%'" + Environment.NewLine;
                }

                IEnumerable<Sys_SupplierMaster_slm_Info> infos = null;
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        infos = db.ExecuteQuery<Sys_SupplierMaster_slm_Info>(sqlScript, new object[] { });

                        if (infos != null)
                        {
                            foreach (Sys_SupplierMaster_slm_Info item in infos)
                            {
                                resultList.Add(item);
                            }
                        }

                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }

            return resultList;
        }

        #endregion
    }
}
