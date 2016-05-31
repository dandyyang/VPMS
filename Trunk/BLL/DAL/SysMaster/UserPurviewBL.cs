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
    class UserPurviewBL:IUserPurviewBL
    {
        IUserPurviewDA _userPurviewDA;
        public UserPurviewBL()
        {
            this._userPurviewDA = MasterDAFactory.GetDAL<IUserPurviewDA>(MasterDAFactory.UserPurview);
        }

        #region IDataBaseCommandBL<Sys_UserPurview_usp_Info> Members

        public Sys_UserPurview_usp_Info GetRecord_First()
        {
            Sys_UserPurview_usp_Info info = null;
            try
            {
                info = this._userPurviewDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public Sys_UserPurview_usp_Info GetRecord_Last()
        {
            Sys_UserPurview_usp_Info info = null;
            try
            {
                info = this._userPurviewDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public Sys_UserPurview_usp_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_UserPurview_usp_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._userPurviewDA.GetRecord_Previous(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return info;
            }
        }

        public Sys_UserPurview_usp_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            Sys_UserPurview_usp_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._userPurviewDA.GetRecord_Next(commandInfo);
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
                info = _userPurviewDA.GetTableFieldLenght();
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
                Sys_UserPurview_usp_Info info = null;
                try
                {
                    info = this._userPurviewDA.DisplayRecord(itemEntity);
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
            List<Sys_UserPurview_usp_Info> info = new List<Sys_UserPurview_usp_Info>();
            try
            {
                info = _userPurviewDA.SearchRecords(itemEntity);
                foreach (Sys_UserPurview_usp_Info i in info)
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
            //throw new NotImplementedException();
            bool b;
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Sys_UserPurview_usp_Info info = null;
            info = itemEntity as Sys_UserPurview_usp_Info;

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
                        b = this._userPurviewDA.DeleteRecord(info);
                        if (b == true)
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:
                        this._userPurviewDA.InsertRecord(info);
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        b = this._userPurviewDA.UpdateRecord(info);
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
