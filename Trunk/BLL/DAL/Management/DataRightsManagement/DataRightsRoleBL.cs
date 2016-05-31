using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Management.DataRightsManagement;
using BLL.IBLL.Management.DataRightsManagement;
//using DAL.IDAL.Management;
using DAL.Factory.Management;
using DAL.IDAL.Management.DataRightsManagement;
using Model.SysMaster;
using Model.General;

namespace BLL.DAL.Management.DataRightsManagement
{
    class DataRightsRoleBL : IDataRightsRoleBL
    {
        IDataRightsRoleDA _dataRightsRoleDA;
        public DataRightsRoleBL()
        {
            this._dataRightsRoleDA = MasterDAFactory.GetDAL<IDataRightsRoleDA>(MasterDAFactory.DataRightsRole);
        }
        #region IDataBaseCommandBL<DataRightsRole_drr_Info> Members

        public DataRightsRole_drr_Info GetRecord_First()
        {
            return _dataRightsRoleDA.GetRecord_First();
        }

        public DataRightsRole_drr_Info GetRecord_Last()
        {
            return _dataRightsRoleDA.GetRecord_Last();
        }

        public DataRightsRole_drr_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            if (commandInfo.KeyInfoList == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return this._dataRightsRoleDA.GetRecord_Previous(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public DataRightsRole_drr_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            if (commandInfo.KeyInfoList == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return this._dataRightsRoleDA.GetRecord_Next(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
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
            throw new NotImplementedException();
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
                try
                {
                    return this._dataRightsRoleDA.DisplayRecord(itemEntity);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
            }
        }

        public List<Model.IModel.IModelObject> SearchRecords(Model.IModel.IModelObject itemEntity)
        {
            List<Model.IModel.IModelObject> Object = new List<Model.IModel.IModelObject>();
            List<DataRightsRole_drr_Info> info = new List<DataRightsRole_drr_Info>();
            try
            {
                info = _dataRightsRoleDA.SearchRecords(itemEntity);
                foreach (DataRightsRole_drr_Info i in info)
                {
                    Object.Add(i);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return Object;
        }

        public Model.General.ReturnValueInfo Save(Model.IModel.IModelObject itemEntity, Common.DefineConstantValue.EditStateEnum EditMode)
        {
            bool _check;
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            DataRightsRole_drr_Info info = null;
            info = itemEntity as DataRightsRole_drr_Info;
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
                        _check = this._dataRightsRoleDA.DeleteRecord(info);
                        if (_check)
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:

                        _check = this._dataRightsRoleDA.IsExistRecord(info);
                        if (!_check)
                        {
                            msg.boolValue = this._dataRightsRoleDA.InsertRecord(info);
                        }
                        else
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  表单编号重复！";
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        msg.boolValue = this._dataRightsRoleDA.UpdateRecord(info);
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

        #region IDataRightsRoleBL Members

        public List<DataRightsRole_drr_Info> GetAllRoleList()
        {
            try
            {
                return _dataRightsRoleDA.GetAllRoleList();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public List<Sys_UserMaster_usm_Info> GetAllSysUserList()
        {
            try
            {
                return _dataRightsRoleDA.GetAllSysUserList();
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public ReturnValueInfo SaveRoleUser(DataRightsRole_drr_Info Entity)
        {
            try
            {
                return _dataRightsRoleDA.SaveRoleUser(Entity);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public ReturnValueInfo DeleteRoleUser(DataRightsRole_drr_Info Entity)
        {
            try
            {
                return _dataRightsRoleDA.DeleteRoleUser(Entity);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public ReturnValueInfo SaveUserToRole(List<Sys_UserMaster_usm_Info> userList, List<DataRightsRole_drr_Info> roleList, bool iClearUserRole, bool iClearRoleUser)
        {
            try
            {
                return _dataRightsRoleDA.SaveUserToRole(userList, roleList, iClearUserRole, iClearRoleUser);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }


        public List<DataRightsRole_drr_Info> GetUserRoleList(Sys_UserMaster_usm_Info Entity)
        {
            try
            {
                return _dataRightsRoleDA.GetUserRoleList(Entity);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public List<DataRightsRole_drr_Info> GetUserRoles(string userLoginID)
        {
            try
            {
                return _dataRightsRoleDA.GetUserRoles(userLoginID);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public List<Sys_UserMaster_usm_Info> GetRoleUserList(DataRightsRole_drr_Info Entity)
        {
            try
            {
                return _dataRightsRoleDA.GetRoleUserList(Entity);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public List<Sys_UserMaster_usm_Info> GetRoleUsers(string roleNumber)
        {
            try
            {
                return _dataRightsRoleDA.GetRoleUsers(roleNumber);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        public ReturnValueInfo SaveUserRole(string userNumber, List<string> roleNumList)
        {
            return _dataRightsRoleDA.SaveUserRole(userNumber, roleNumList);
        }

        public ReturnValueInfo DeleteUserRole(string userNumber, List<string> roleNumList)
        {
            return _dataRightsRoleDA.DeleteUserRole(userNumber, roleNumList);
        }

        #endregion
    }
}
