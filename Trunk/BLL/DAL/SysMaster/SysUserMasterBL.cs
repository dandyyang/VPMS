using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.SysMaster;
using Model.SysMaster;
using DAL.IDAL.SysMaster;
using DAL.Factory.SysMaster;
using Model.Management.Master;

namespace BLL.DAL.SysMaster
{
    class SysUserMasterBL : ISysUserMasterBL
    {
        ISysUserMasterDA _sysUserMasterDA;
        public SysUserMasterBL()
        {
            this._sysUserMasterDA = MasterDAFactory.GetDAL<ISysUserMasterDA>(MasterDAFactory.SysUserMaster);
        }

        #region IDataBaseCommandBL<Sys_UserMaster_usm_Info> Members

        public Sys_UserMaster_usm_Info GetRecord_First()
        {
            Sys_UserMaster_usm_Info info = null;
            try
            {
                info = this._sysUserMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public Sys_UserMaster_usm_Info GetRecord_Last()
        {
            Sys_UserMaster_usm_Info info = null;
            try
            {
                info = this._sysUserMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public Sys_UserMaster_usm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_UserMaster_usm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._sysUserMasterDA.GetRecord_Previous(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return info;
            }
        }

        public Sys_UserMaster_usm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_UserMaster_usm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._sysUserMasterDA.GetRecord_Next(commandInfo);
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
                info = _sysUserMasterDA.GetTableFieldLenght();
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
                Sys_UserMaster_usm_Info info = null;
                try
                {
                    info = this._sysUserMasterDA.DisplayRecord(itemEntity);
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
            List<Sys_UserMaster_usm_Info> info = new List<Sys_UserMaster_usm_Info>();
            try
            {
                info = _sysUserMasterDA.SearchRecords(itemEntity);
                foreach (Sys_UserMaster_usm_Info i in info)
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
            Sys_UserMaster_usm_Info info = null;
            info = itemEntity as Sys_UserMaster_usm_Info;

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
                        b = this._sysUserMasterDA.DeleteRecord(info);
                        if (b == true)
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:

                        b = this._sysUserMasterDA.IsExistRecord(info);
                        if (b == false)
                        {
                            msg.boolValue = this._sysUserMasterDA.InsertRecord(info);
                        }
                        else
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  登陆ID重復！";
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        msg.boolValue = this._sysUserMasterDA.UpdateRecord(info);
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
