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
    class BuildingMasterBL : IBuildingMasterBL
    {

        IBuildingMasterDA _buildingMasterDA;

        public BuildingMasterBL()
        {
            this._buildingMasterDA = MasterDAFactory.GetDAL<IBuildingMasterDA>(MasterDAFactory.BuildingMaster);
        }

        #region IDataBaseCommandBL<BuildingMaster_bdm_Info> Members

        public BuildingMaster_bdm_Info GetRecord_First()
        {
            BuildingMaster_bdm_Info info = null;
            try
            {
                info = this._buildingMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public BuildingMaster_bdm_Info GetRecord_Last()
        {
            BuildingMaster_bdm_Info info = null;
            try
            {
                info = this._buildingMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public BuildingMaster_bdm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            BuildingMaster_bdm_Info info = null;
            try
            {
                info = this._buildingMasterDA.GetRecord_Previous(commandInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public BuildingMaster_bdm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            BuildingMaster_bdm_Info info = null;
            try
            {
                info = this._buildingMasterDA.GetRecord_Next(commandInfo);
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
            Model.IModel.IModelObject info = null;
            info = _buildingMasterDA.DisplayRecord(itemEntity);
            return info;
        }

        public List<Model.IModel.IModelObject> SearchRecords(Model.IModel.IModelObject itemEntity)
        {
            List<Model.Management.Master.BuildingMaster_bdm_Info> list = _buildingMasterDA.SearchRecords(itemEntity);
            List<Model.IModel.IModelObject> objectList = new List<Model.IModel.IModelObject>();
            foreach (BuildingMaster_bdm_Info item in list)
            {
                objectList.Add(item);
            }
            return objectList;
        }

        public Model.General.ReturnValueInfo Save(Model.IModel.IModelObject itemEntity, Common.DefineConstantValue.EditStateEnum EditMode)
        {
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            msg.boolValue = false;
            bool tab = false;
            switch (EditMode)
            {
                case Common.DefineConstantValue.EditStateEnum.OE_Insert:
                    if ((itemEntity as BuildingMaster_bdm_Info).bdm_cName != "" && (itemEntity as BuildingMaster_bdm_Info).bdm_cNumber != "")
                    {
                        tab = InsertRecord(itemEntity as BuildingMaster_bdm_Info);
                        if (tab == true)
                        {
                            msg.boolValue = true;
                            msg.messageText = "";
                            //msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddSuccess;
                        }
                        else
                        {
                            msg.boolValue = false;
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "建筑物编号重复!";
                        }
                    }
                    else
                    {
                        msg.boolValue = false;
                        msg.messageText = "建筑物编号或名称不能为空!";
                    }

                    break;
                case Common.DefineConstantValue.EditStateEnum.OE_Update:
                    tab = UpdateRecord(itemEntity as BuildingMaster_bdm_Info);
                    if ((itemEntity as BuildingMaster_bdm_Info).bdm_cName != "")
                    {
                        if (tab == true)
                        {
                            msg.boolValue = true;
                            msg.messageText = "";
                            //msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateSuccess;
                        }
                        else
                        {
                            msg.boolValue = false;
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateFail;
                        }
                    }
                    else
                    {
                        msg.boolValue = false;
                        msg.messageText = "建筑物名称不能为空!";
                    }

                    break;
                case Common.DefineConstantValue.EditStateEnum.OE_Delete:
                    tab = _buildingMasterDA.DeleteRecord(itemEntity);
                    if (tab == true)
                    {
                        msg.boolValue = true;
                        msg.messageText = "";
                        //msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                    }
                    else
                    {
                        msg.boolValue = false;
                        msg.messageText = "删除操作" + Common.DefineConstantValue.SystemMessageText.strSystemError;
                    }
                    break;
                default:
                    break;
            }
            return msg;
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            return _buildingMasterDA.DeleteRecord(KeyObject);
        }

        private bool InsertRecord(Model.Management.Master.BuildingMaster_bdm_Info infoObject)
        {
            bool tabB = _buildingMasterDA.IsExistRecord((infoObject as BuildingMaster_bdm_Info).bdm_cNumber);
            if (!tabB)
            {
                return _buildingMasterDA.InsertRecord(infoObject);
            }
            return false;
        }

        private bool UpdateRecord(Model.Management.Master.BuildingMaster_bdm_Info infoObject)
        {
            return _buildingMasterDA.UpdateRecord(infoObject);
        }

        #endregion

        #region IExtraBL Members

        public bool IsExistRecord(object KeyObject)
        {
            return _buildingMasterDA.IsExistRecord(KeyObject);
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
                info = _buildingMasterDA.GetTableFieldLenght();
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
