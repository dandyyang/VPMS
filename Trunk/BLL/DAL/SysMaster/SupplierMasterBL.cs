using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.SysMaster;
using Model.SysMaster;
using Model.Base;
using Model.IModel;
using Model.General;
using Common;
using DAL.IDAL.SysMaster;
using DAL.Factory.SysMaster;

namespace BLL.DAL.SysMaster
{
    public class SupplierMasterBL : ISupplierMasterBL
    {
        ISupplierMasterDA _supplierMasterDA;

        public SupplierMasterBL()
        {
            this._supplierMasterDA = MasterDAFactory.GetDAL<ISupplierMasterDA>(MasterDAFactory.SupplierMaster);
        }

        #region IDataBaseCommandBL<Sys_SupplierMaster_slm_Info> Members

        public Sys_SupplierMaster_slm_Info GetRecord_First()
        {
            try
            {
                return _supplierMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public Sys_SupplierMaster_slm_Info GetRecord_Last()
        {
            try
            {
                return _supplierMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public Sys_SupplierMaster_slm_Info GetRecord_Previous(DataBaseCommandInfo commandInfo)
        {
            try
            {
                return _supplierMasterDA.GetRecord_Previous(commandInfo);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public Sys_SupplierMaster_slm_Info GetRecord_Next(DataBaseCommandInfo commandInfo)
        {
            try
            {
                return _supplierMasterDA.GetRecord_Next(commandInfo);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region IExtraBL Members

        public bool IsExistRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public IModelObject LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public IModelObject UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public IModelObject GetTableFieldLenght()
        {
            try
            {
                return _supplierMasterDA.GetTableFieldLenght();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion

        #region IMainBL Members

        public IModelObject DisplayRecord(IModelObject itemEntity)
        {
            try
            {
                return _supplierMasterDA.DisplayRecord(itemEntity);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public List<IModelObject> SearchRecords(IModelObject itemEntity)
        {
            List<IModelObject> resultList = new List<IModelObject>();

            List<Sys_SupplierMaster_slm_Info> objList = null;

            try
            {
                objList = _supplierMasterDA.SearchRecords(itemEntity);

                if (objList != null)
                {
                    foreach (Sys_SupplierMaster_slm_Info supplierMaster in objList)
                    {
                        resultList.Add(supplierMaster);
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return resultList;
        }

        public ReturnValueInfo Save(IModelObject itemEntity, DefineConstantValue.EditStateEnum EditMode)
        {
            ReturnValueInfo returnValueInfo = new ReturnValueInfo();

            Sys_SupplierMaster_slm_Info supplierMaster = itemEntity as Sys_SupplierMaster_slm_Info;

            try
            {
                switch (EditMode)
                {
                    case DefineConstantValue.EditStateEnum.OE_Insert:

                        if (!_supplierMasterDA.IsExistRecord(supplierMaster))
                        {
                            supplierMaster.slm_dAddDate = DateTime.Now;

                            supplierMaster.slm_dLastDate = DateTime.Now;

                            returnValueInfo.boolValue = _supplierMasterDA.InsertRecord(supplierMaster);
                        }
                        else 
                        {
                            returnValueInfo.boolValue = false;

                            returnValueInfo.messageText = "编号重复！";
                        }

                        break;
                    case DefineConstantValue.EditStateEnum.OE_Update:

                        supplierMaster.slm_dLastDate = DateTime.Now;

                        returnValueInfo.boolValue = _supplierMasterDA.UpdateRecord(supplierMaster);

                        break;
                    case DefineConstantValue.EditStateEnum.OE_Delete:

                        returnValueInfo.boolValue = _supplierMasterDA.DeleteRecord(itemEntity);

                        break;

                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return returnValueInfo;
        }

        #endregion
    }
}
