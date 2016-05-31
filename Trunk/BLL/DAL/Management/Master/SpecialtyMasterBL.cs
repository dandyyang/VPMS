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
    class SpecialtyMasterBL : ISpecialtyMasterBL
    {
        ISpecialtyMasterDA _specialtyMasterDA;
        public SpecialtyMasterBL()
        {
            this._specialtyMasterDA = MasterDAFactory.GetDAL<ISpecialtyMasterDA>(MasterDAFactory.SpecialtyMaster);
        }

        #region IDataBaseCommandBL<SpecialtyMaster_spm_Info> Members

        public SpecialtyMaster_spm_Info GetRecord_First()
        {
            SpecialtyMaster_spm_Info info = null;
            try
            {
                info = this._specialtyMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public SpecialtyMaster_spm_Info GetRecord_Last()
        {
            SpecialtyMaster_spm_Info info = null;
            try
            {
                info = this._specialtyMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public SpecialtyMaster_spm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SpecialtyMaster_spm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._specialtyMasterDA.GetRecord_Previous(commandInfo);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                return info;
            }
        }

        public SpecialtyMaster_spm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SpecialtyMaster_spm_Info info = null;
            if (commandInfo.KeyInfoList == null)
            {
                return info;
            }
            else
            {
                try
                {
                    info = this._specialtyMasterDA.GetRecord_Next(commandInfo);
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
                info = _specialtyMasterDA.GetTableFieldLenght();
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
                SpecialtyMaster_spm_Info info = null;
                try
                {
                    info = this._specialtyMasterDA.DisplayRecord(itemEntity);
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
            List<SpecialtyMaster_spm_Info> info = new List<SpecialtyMaster_spm_Info>();
            try
            {
                info = _specialtyMasterDA.SearchRecords(itemEntity);
                foreach (SpecialtyMaster_spm_Info stm in info)
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
            SpecialtyMaster_spm_Info info = null;
            info = itemEntity as SpecialtyMaster_spm_Info;
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
                        checkstm = this._specialtyMasterDA.DeleteRecord(info);
                        if (checkstm == true)
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:
                        checkstm = this._specialtyMasterDA.IsExistRecord(info);
                        if (checkstm == false)
                        {
                            this._specialtyMasterDA.InsertRecord(info);
                        }
                        else
                        {
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + " 专业编号重复！";
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        checkstm = this._specialtyMasterDA.UpdateRecord(info);
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
