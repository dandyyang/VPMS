using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.SysMaster;
using DAL.IDAL.SysMaster;
using DAL.Factory.SysMaster;
using Model.SysMaster;
using Model.General;
using Model.IModel;
using Common;

namespace BLL.DAL.SysMaster
{
    public class ArticleTypeDefineBL : IArticleTypeDefineBL
    {
        IArticleTypeDefineDA _articleTypeDefineDA;

        public ArticleTypeDefineBL()
        {
            this._articleTypeDefineDA = MasterDAFactory.GetDAL<IArticleTypeDefineDA>(MasterDAFactory.ArticleTypeDefine);
        }

        #region IArticleTypeDefineBL Members

        public List<ArticleTypeDefine_atd_Info> GetAllChildren(ArticleTypeDefine_atd_Info ArticleTypeInfo)
        {
            try
            {
                return _articleTypeDefineDA.GetAllChildren(ArticleTypeInfo);
            }
            catch (Exception Ex)
            {
                
                throw Ex;
            }
        }

        public ArticleTypeDefine_atd_Info GetTreeRoot(string codeMasterDefineKey2)
        {
            try
            {
                return _articleTypeDefineDA.GetTreeRoot(codeMasterDefineKey2);
            }
            catch (Exception Ex)
            {
                
                throw Ex;
            }
        }

        #endregion

        #region IMainBL Members

        public IModelObject DisplayRecord(IModelObject itemEntity)
        {
            throw new NotImplementedException();
        }

        public List<IModelObject> SearchRecords(IModelObject itemEntity)
        {
            throw new NotImplementedException();
        }

        public ReturnValueInfo Save(IModelObject itemEntity, DefineConstantValue.EditStateEnum EditMode)
        {
            ReturnValueInfo returnInfo = new ReturnValueInfo();

            switch (EditMode)
            {
                case DefineConstantValue.EditStateEnum.OE_Insert:

                    if (!_articleTypeDefineDA.IsExistRecord(itemEntity))
                    {
                        returnInfo.boolValue = _articleTypeDefineDA.InsertRecord(itemEntity as ArticleTypeDefine_atd_Info);
                    }
                    else 
                    {
                        returnInfo.boolValue = false;

                        returnInfo.messageText = "编号重复！";
                    }
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:

                    returnInfo.boolValue = _articleTypeDefineDA.UpdateRecord(itemEntity as ArticleTypeDefine_atd_Info);

                    break;
                case DefineConstantValue.EditStateEnum.OE_Delete:

                    returnInfo.boolValue = _articleTypeDefineDA.DeleteRecord(itemEntity as ArticleTypeDefine_atd_Info);

                    break;
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    break;
                default:
                    break;
            }


            return returnInfo;
        }

        #endregion

        #region IExtraBL Members

        public bool IsExistRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public IModelObject LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public IModelObject UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public IModelObject GetTableFieldLenght()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
