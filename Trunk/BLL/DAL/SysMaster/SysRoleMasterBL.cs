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
    class SysRoleMasterBL:ISysRoleMasterBL
    {
        ISysRoleMasterDA _sysRoleMasterDA;
        public SysRoleMasterBL()
        {
            this._sysRoleMasterDA = MasterDAFactory.GetDAL<ISysRoleMasterDA>(MasterDAFactory.SysRoleMaster);
        }

        #region IDataBaseCommandBL<Sys_RoleMaster_rlm_Info> Members

        public Sys_RoleMaster_rlm_Info GetRecord_First()
        {
            Sys_RoleMaster_rlm_Info info = null;
            try
            {
                info = this._sysRoleMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public Sys_RoleMaster_rlm_Info GetRecord_Last()
        {
            Sys_RoleMaster_rlm_Info info = null;
            try
            {
                info = this._sysRoleMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public Sys_RoleMaster_rlm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_RoleMaster_rlm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._sysRoleMasterDA.GetRecord_Previous(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return info;
            }
        }

        public Sys_RoleMaster_rlm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_RoleMaster_rlm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._sysRoleMasterDA.GetRecord_Next(commandInfo);
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
                info = _sysRoleMasterDA.GetTableFieldLenght();
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
                Sys_RoleMaster_rlm_Info info = null;
                try
                {
                    info = this._sysRoleMasterDA.DisplayRecord(itemEntity);
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
            List<Sys_RoleMaster_rlm_Info> info = new List<Sys_RoleMaster_rlm_Info>();
            try
            {
                info = _sysRoleMasterDA.SearchRecords(itemEntity);
                foreach (Sys_RoleMaster_rlm_Info i in info)
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
            Sys_RoleMaster_rlm_Info info = null;
            info = itemEntity as Sys_RoleMaster_rlm_Info;

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
                        b = this._sysRoleMasterDA.DeleteRecord(info);
                        if (b == true)
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:

                        b = this._sysRoleMasterDA.IsExistRecord(info);
                        if (b == false)
                        {
                            this._sysRoleMasterDA.InsertRecord(info);
                        }
                        else
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  角色ID重復！";
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        b = this._sysRoleMasterDA.UpdateRecord(info);
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
