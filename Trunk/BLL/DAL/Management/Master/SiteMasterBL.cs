using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.Management.Master;
using Model.Management.Master;
using DAL.IDAL.Management.Master;
using DAL.Factory.Management;

namespace BLL.DAL.Management.Master
{
    class SiteMasterBL : ISiteMasterBL
    {
        ISiteMasterDA _isteMasterDA;
        public SiteMasterBL()
        {
            this._isteMasterDA = MasterDAFactory.GetDAL<ISiteMasterDA>(MasterDAFactory.SiteMaster);
        }
        #region IDataBaseCommandBL<SiteMaster_stm_info> Members

        public Model.Management.Master.SiteMaster_stm_Info GetRecord_First()
        {
            //Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            SiteMaster_stm_Info info = null;
            try
            {
                info = this._isteMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public Model.Management.Master.SiteMaster_stm_Info GetRecord_Last()
        {
            SiteMaster_stm_Info info = null;
            try
            {
                info = this._isteMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public Model.Management.Master.SiteMaster_stm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SiteMaster_stm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._isteMasterDA.GetRecord_Previous(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return info;
            }
        }

        public Model.Management.Master.SiteMaster_stm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SiteMaster_stm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._isteMasterDA.GetRecord_Next(commandInfo);
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
                info = _isteMasterDA.GetTableFieldLenght();
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
                SiteMaster_stm_Info info = null;
                try
                {
                    info = this._isteMasterDA.DisplayRecord(itemEntity);
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
            List<SiteMaster_stm_Info> info = new List<SiteMaster_stm_Info>();
            try
            {
                info = _isteMasterDA.SearchRecords(itemEntity);
                foreach (SiteMaster_stm_Info stm in info)
                {
                    info_imo.Add(stm);
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
            bool checkstm;
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            SiteMaster_stm_Info info = null;
            info = itemEntity as SiteMaster_stm_Info;
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
                        checkstm = this._isteMasterDA.DeleteRecord(info);
                        if (checkstm == true)
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:
                        checkstm = this._isteMasterDA.IsExistRecord(info);
                        if (checkstm == false)
                        {
                            this._isteMasterDA.InsertRecord(info);
                        }
                        else
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + " 地点编号重复！";
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        checkstm = this._isteMasterDA.UpdateRecord(info);
                        //if (checkstm == true)
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
