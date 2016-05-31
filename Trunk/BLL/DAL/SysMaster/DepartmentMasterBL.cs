using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.SysMaster;
using Model.SysMaster;
using DAL.IDAL.SysMaster;
using DAL.Factory.SysMaster;

namespace BLL.DAL.SysMaster
{
    class DepartmentMasterBL : IDepartmentMasterBL
    {
        IDepartmentMasterDA _departmentMasterDA;
        public DepartmentMasterBL()
        {
            this._departmentMasterDA = MasterDAFactory.GetDAL<IDepartmentMasterDA>(MasterDAFactory.DepartmentMaster);
        }
        #region IDataBaseCommandBL<DepartmentMaster_dpm_Info> Members

        public DepartmentMaster_dpm_Info GetRecord_First()
        {
            DepartmentMaster_dpm_Info info = null;
            try
            {
                info = this._departmentMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public DepartmentMaster_dpm_Info GetRecord_Last()
        {
            DepartmentMaster_dpm_Info info = null;
            try
            {
                info = this._departmentMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public DepartmentMaster_dpm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {

            DepartmentMaster_dpm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._departmentMasterDA.GetRecord_Previous(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return info;
            }
        }

        public DepartmentMaster_dpm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            DepartmentMaster_dpm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._departmentMasterDA.GetRecord_Next(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return info;
            }
        }

        #endregion

        #region IMainBL Members

        public Model.IModel.IModelObject DisplayRecord(Model.IModel.IModelObject itemEntity)
        {
            if (itemEntity == null)
            {
                return null;
            }
            else
            {
                DepartmentMaster_dpm_Info info = null;
                try
                {
                    info = this._departmentMasterDA.DisplayRecord(itemEntity);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                return info;
            }
        }

        public List<Model.IModel.IModelObject> SearchRecords(Model.IModel.IModelObject itemEntity)
        {
            List<Model.IModel.IModelObject> info_imo = new List<Model.IModel.IModelObject>();
            List<DepartmentMaster_dpm_Info> info = new List<DepartmentMaster_dpm_Info>();
            try
            {
                info = _departmentMasterDA.SearchRecords(itemEntity);
                foreach (DepartmentMaster_dpm_Info i in info)
                {
                    info_imo.Add(i);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info_imo;
        }

        public Model.General.ReturnValueInfo Save(Model.IModel.IModelObject itemEntity, Common.DefineConstantValue.EditStateEnum EditMode)
        {
            bool b;
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            DepartmentMaster_dpm_Info info = null;
            info = itemEntity as DepartmentMaster_dpm_Info;

            if (info == null)
            {
                msg.boolValue = false;
                msg.messageText = "传入的数据为空！";

                return msg;
            }

            try
            {
                switch (EditMode)
                {
                    case Common.DefineConstantValue.EditStateEnum.OE_Delete:
                        b = this._departmentMasterDA.DeleteRecord(info);
                        if (b == true)
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:

                        b = this._departmentMasterDA.IsExistRecord(info);
                        if (b ==false)
                        {
                            this._departmentMasterDA.InsertRecord(info);
                        }
                        else
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  科室编号重复！";
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        b = this._departmentMasterDA.UpdateRecord(info);
                        //if (b == true)
                        //{
                        //    msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateSuccess;
                        //}
                        //else
                        //{
                        //    msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateFail;
                        //}
                        break;
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }          
            return msg;
        }

        #endregion

        #region IExtraBL Members

        public bool IsExistRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.IModel.IModelObject GetTableFieldLenght()
        {
            Model.IModel.IModelObject info = null;
            try
            {
                info = _departmentMasterDA.GetTableFieldLenght();
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
