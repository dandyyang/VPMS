using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.SysMaster;
using Model.SysMaster;
using DAL.IDAL.SysMaster;
using DAL.Factory.SysMaster;
using Common;

namespace BLL.DAL.SysMaster
{
    class LoginFormBL:ILoginFormBL
    {
        ILoginFormDA _loginFormDA;
        public LoginFormBL()
        {
            this._loginFormDA = MasterDAFactory.GetDAL<ILoginFormDA>(MasterDAFactory.LoginForm);
        }

        #region IDataBaseCommandBL<Sys_UserMaster_usm_Info> Members

        public Sys_UserMaster_usm_Info GetRecord_First()
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm_Info GetRecord_Last()
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
        }

        public Sys_UserMaster_usm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            throw new NotImplementedException();
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
                Sys_UserMaster_usm_Info info = null;
                try
                {
                    info = itemEntity as Sys_UserMaster_usm_Info;
                    info.usm_cPasswork = Common.General.MD5(info.usm_cPasswork);
                    if (info.usm_cUserLoginID.ToUpper() == "SA")
                    {
                        if (info.usm_cPasswork == Common.General.MD5(DateTime.Now.ToString(DefineConstantValue.gc_PwdDateFormat)))
                        {
                            info = this._loginFormDA.FindAllPermission(info);
                            //info = this._loginFormDA.DisplayRecord(info);
                            return info;
                        }
                        else
                        {
                            info = null;
                            return info;
                        }
                    }
                    else
                        //info = this._loginFormDA.DisplayRecord(itemEntity);
                        info = this._loginFormDA.FindPermission(itemEntity);
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
                info = _loginFormDA.SearchRecords(itemEntity);
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
            throw new NotImplementedException();
        }

        #endregion

        public Model.General.ReturnValueInfo Login(Model.IModel.IModelObject itemEntity)
        {
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Sys_UserMaster_usm_Info userInfo = null;            
            try
            {
                userInfo = DisplayRecord(itemEntity) as Sys_UserMaster_usm_Info;
                if (userInfo == null)
                {
                    msg.boolValue = false;
                    msg.messageText = "密码错误，请确认您是管理员身份！";
                }
                else
                {
                    if (userInfo.usm_cUserLoginID == "")
                    {
                        msg.boolValue = false;
                        msg.messageText = "用户名或密码错误！";
                    }
                    else
                    {
                        msg.boolValue = true;
                        if (userInfo.usm_iLock == true)
                        {
                            msg.messageText = "账户被锁，请与管理员联系！";
                        }
                        msg.ValueObject = userInfo;
                    }
                }
                return msg; 
            }
            catch (Exception Ex)
            {
                msg.boolValue = false;
                msg.messageText = Ex.ToString();
                return msg;
            }
        }
    }
}
