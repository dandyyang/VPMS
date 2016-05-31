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
    public class CodeMasterBL : ICodeMasterBL
    {
        ICodeMasterDA _codeMasterDA;

        public CodeMasterBL()
        {
            this._codeMasterDA = MasterDAFactory.GetDAL<ICodeMasterDA>(MasterDAFactory.CodeMaster_cmt);
        }

        #region IDataBaseCommandBL<CodeMaster_cmt_Info> Members

        public CodeMaster_cmt_Info GetRecord_First()
        {
            try
            {
                return _codeMasterDA.GetRecord_First();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public CodeMaster_cmt_Info GetRecord_Last()
        {
            try
            {
                return _codeMasterDA.GetRecord_Last();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public CodeMaster_cmt_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            try
            {
                return _codeMasterDA.GetRecord_Previous(commandInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public CodeMaster_cmt_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            try
            {
                return _codeMasterDA.GetRecord_Next(commandInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region IMainBL Members

        public Model.IModel.IModelObject DisplayRecord(Model.IModel.IModelObject itemEntity)
        {
            try
            {
                return _codeMasterDA.DisplayRecord(itemEntity);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<Model.IModel.IModelObject> SearchRecords(Model.IModel.IModelObject itemEntity)
        {
            List<Model.IModel.IModelObject> list = new List<Model.IModel.IModelObject>();
            List<CodeMaster_cmt_Info> returnList = new List<CodeMaster_cmt_Info>();
            try
            {
                returnList = _codeMasterDA.SearchRecords(itemEntity);
                if (returnList != null)
                {
                    foreach (CodeMaster_cmt_Info t in returnList)
                    {
                        list.Add(t);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return list;
        }

        public Model.General.ReturnValueInfo Save(Model.IModel.IModelObject itemEntity, Common.DefineConstantValue.EditStateEnum EditMode)
        {
            Model.General.ReturnValueInfo returnValue = new Model.General.ReturnValueInfo();
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            returnValue.messageText = "";
            info = itemEntity as CodeMaster_cmt_Info;
            try
            {
                switch (EditMode)
                {
                    case Common.DefineConstantValue.EditStateEnum.OE_Insert:
                        bool isExist = false;

                        isExist = _codeMasterDA.IsExistRecord(info);
                        if (!isExist)
                        {
                            returnValue.boolValue = _codeMasterDA.InsertRecord(info);
                        }
                        else
                        {
                            returnValue.boolValue = false;
                            returnValue.messageText = "数据重复！";
                        }
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Update:
                        returnValue.boolValue = _codeMasterDA.UpdateRecord(info);
                        break;
                    case Common.DefineConstantValue.EditStateEnum.OE_Delete:
                        returnValue.boolValue = _codeMasterDA.DeleteRecord(info);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return returnValue;
        }

        #endregion

        #region IExtraBL Members

        public bool IsExistRecord(object KeyObject)
        {
            try
            {
                return _codeMasterDA.IsExistRecord(KeyObject);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
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

        #region ICodeMasterBL Members

        public List<CodeMaster_cmt_Info> FindRecord(CodeMaster_cmt_Info info)
        {
            //throw new NotImplementedException();
            //List<Model.IModel.IModelObject> list = new List<Model.IModel.IModelObject>();
            List<CodeMaster_cmt_Info> returnList = new List<CodeMaster_cmt_Info>();
            try
            {
                returnList = _codeMasterDA.FindRecord(info);
                //if (returnList != null)
                //{
                //    foreach (CodeMaster_cmt_Info t in returnList)
                //    {
                //        list.Add(t);
                //    }
                //}
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return returnList;
        }

        #endregion
    }
}
