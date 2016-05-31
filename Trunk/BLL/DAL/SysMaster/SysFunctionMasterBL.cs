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
    class SysFunctionMasterBL:ISysFunctionMasterBL
    {
        ISysFunctionMasterDA _sysFunctionMasterDA;
        public SysFunctionMasterBL()
        {
            this._sysFunctionMasterDA = MasterDAFactory.GetDAL<ISysFunctionMasterDA>(MasterDAFactory.SysFunctionMaster);
        }

        #region IDataBaseCommandBL<Sys_FunctionMaster_fum_Info> Members

        public Sys_FunctionMaster_fum_Info GetRecord_First()
        {
            Sys_FunctionMaster_fum_Info info = null;
            try
            {
                info = this._sysFunctionMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public Sys_FunctionMaster_fum_Info GetRecord_Last()
        {
            Sys_FunctionMaster_fum_Info info = null;
            try
            {
                info = this._sysFunctionMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public Sys_FunctionMaster_fum_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_FunctionMaster_fum_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._sysFunctionMasterDA.GetRecord_Previous(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return info;
            }
        }

        public Sys_FunctionMaster_fum_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_FunctionMaster_fum_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._sysFunctionMasterDA.GetRecord_Next(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return info;
            }
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
                info = _sysFunctionMasterDA.GetTableFieldLenght();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
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
                Sys_FunctionMaster_fum_Info info = null;
                try
                {
                    info = this._sysFunctionMasterDA.DisplayRecord(itemEntity);
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
            List<Sys_FunctionMaster_fum_Info> info = new List<Sys_FunctionMaster_fum_Info>();
            try
            {
                info = _sysFunctionMasterDA.SearchRecords(itemEntity);
                foreach (Sys_FunctionMaster_fum_Info i in info)
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
            Sys_FunctionMaster_fum_Info info = null;
            info = itemEntity as Sys_FunctionMaster_fum_Info;

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
                        b = this._sysFunctionMasterDA.DeleteRecord(info);
                        if (b == true)
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:

                        b = this._sysFunctionMasterDA.IsExistRecord(info);
                        if (b == false)
                        {
                            this._sysFunctionMasterDA.InsertRecord(info);
                        }
                        else
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  功能编号重复！";
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        b = this._sysFunctionMasterDA.UpdateRecord(info);
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
    }
}
