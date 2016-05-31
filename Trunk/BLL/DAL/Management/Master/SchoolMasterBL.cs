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
    class SchoolMasterBL : ISchoolMasterBL
    {
        ISchoolMasterDA _schoolMasterDA;

        public SchoolMasterBL()
        {
            this._schoolMasterDA = MasterDAFactory.GetDAL<ISchoolMasterDA>(MasterDAFactory.SchoolMaster);
        }

        #region IDataBaseCommandBL<SchoolMaster_scm> Members

        public Model.Management.Master.SchoolMaster_scm_Info GetRecord_First()
        {
            SchoolMaster_scm_Info info = null;
            try
            {
                info = this._schoolMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public Model.Management.Master.SchoolMaster_scm_Info GetRecord_Last()
        {
            SchoolMaster_scm_Info info = null;
            try
            {
                info = this._schoolMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public Model.Management.Master.SchoolMaster_scm_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SchoolMaster_scm_Info info = null;
            try
            {
                info = this._schoolMasterDA.GetRecord_Previous(commandInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;

        }

        public Model.Management.Master.SchoolMaster_scm_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            SchoolMaster_scm_Info info = null;
            try
            {
                info = this._schoolMasterDA.GetRecord_Next(commandInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        #endregion

        #region IMainBL Members

        private bool InsertRecord(Model.Management.Master.SchoolMaster_scm_Info infoObject)
        {
            bool tabB = _schoolMasterDA.IsExistRecord((infoObject as SchoolMaster_scm_Info).scm_cNumber);
            if (!tabB)
            {
                return _schoolMasterDA.InsertRecord(infoObject);
            }
            return false;
        }

        private bool UpdateRecord(Model.Management.Master.SchoolMaster_scm_Info infoObject)
        {
            return _schoolMasterDA.UpdateRecord(infoObject);
        }

        public Model.IModel.IModelObject DisplayRecord(Model.IModel.IModelObject itemEntity)
        {
            Model.IModel.IModelObject info = null;
            info = _schoolMasterDA.DisplayRecord(itemEntity);
            return info;
        }

        public List<Model.IModel.IModelObject> SearchRecords(Model.IModel.IModelObject itemEntity)
        {
            List<Model.Management.Master.SchoolMaster_scm_Info> list = _schoolMasterDA.SearchRecords(itemEntity);
            List<Model.IModel.IModelObject> objectList = new List<Model.IModel.IModelObject>();
            foreach (SchoolMaster_scm_Info item in list)
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
                    if ((itemEntity as SchoolMaster_scm_Info).scm_cName.Trim() != "" && (itemEntity as SchoolMaster_scm_Info).scm_cNumber.Trim() != "")
                    {
                        tab = InsertRecord(itemEntity as SchoolMaster_scm_Info);
                        if (tab == true)
                        {
                            msg.boolValue = true;
                            //msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddSuccess;
                            msg.messageText = "";
                        }
                        else
                        {
                            msg.boolValue = false;
                            msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_AddFail + "  " + "院系部编号重复!";
                        }
                    }
                    else
                    {
                        msg.boolValue = false;
                        msg.messageText = "院系部编号或名称不能为空!";
                    }
                    break;
                case Common.DefineConstantValue.EditStateEnum.OE_Update:
                    if ((itemEntity as SchoolMaster_scm_Info).scm_cName.Trim() != "")
                    {
                        tab = UpdateRecord(itemEntity as SchoolMaster_scm_Info);
                        if (tab == true)
                        {
                            msg.boolValue = true;
                            //msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_UpdateSuccess;
                            msg.messageText = "";
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
                        msg.messageText = "院系部名称不能为空!";
                    }

                    break;
                case Common.DefineConstantValue.EditStateEnum.OE_Delete:
                    tab = _schoolMasterDA.DeleteRecord(itemEntity);
                    if (tab == true)
                    {
                        msg.boolValue = true;
                        msg.messageText = Common.DefineConstantValue.SystemMessageText.strMessageText_I_RecordByDelete;
                    }
                    else
                    {
                        msg.boolValue = false;
                        //msg.messageText = "刪除操作"+Common.DefineConstantValue.SystemMessageText.strSystemError;
                        msg.messageText = "";
                    }
                    break;
                default:
                    break;
            }
            return msg;
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            return _schoolMasterDA.DeleteRecord(KeyObject);
        }

        #endregion

        #region IExtraBL Members

        public bool IsExistRecord(object KeyObject)
        {
            return _schoolMasterDA.IsExistRecord(KeyObject);
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
                info = _schoolMasterDA.GetTableFieldLenght();
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
