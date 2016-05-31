using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.SysMaster;
using Model.Base;
using Model.SysMaster;
using Model.IModel;
using LinqToSQLModel;
using Common;

namespace DAL.SqlDAL.SysMaster
{
    public class ArticleTypeDefineDA : IArticleTypeDefineDA
    {
        #region IExtraDA Members

        public bool IsExistRecord(object KeyObject)
        {
            bool isExis = true;

            ArticleTypeDefine_atd_Info objInfo = KeyObject as ArticleTypeDefine_atd_Info;

            if (objInfo != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        ArticleTypeDefine_atd findInfo = db.ArticleTypeDefine_atds.FirstOrDefault(t => t.atd_cTypeNum == objInfo.atd_cTypeNum);

                        if (findInfo == null)
                        {
                            isExis = false;
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return isExis;
        }

        public Model.General.ReturnValueInfo LockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public Model.General.ReturnValueInfo UnLockRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public bool IsMyLockedRecord(object KeyObject)
        {
            throw new NotImplementedException();
        }

        public IModelObject GetTableFieldLenght()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMainDA<ArticleTypeDefine_atd_Info> Members

        public bool InsertRecord(ArticleTypeDefine_atd_Info infoObject)
        {
            bool isSuccess = false;

            if (infoObject != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        ArticleTypeDefine_atd insertInfo = Common.General.CopyObjectValue<ArticleTypeDefine_atd_Info, ArticleTypeDefine_atd>(infoObject);

                        if (insertInfo != null)
                        {
                            db.ArticleTypeDefine_atds.InsertOnSubmit(insertInfo);

                            db.SubmitChanges();

                            isSuccess = true;
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return isSuccess;
        }

        public bool UpdateRecord(ArticleTypeDefine_atd_Info infoObject)
        {
            bool isSuccess = false;

            if (infoObject != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        ArticleTypeDefine_atd updateInfo = db.ArticleTypeDefine_atds.FirstOrDefault(t => t.atd_iRecordID == infoObject.atd_iRecordID);

                        if (updateInfo != null)
                        {
                            updateInfo.atd_cTypeNum = infoObject.atd_cTypeNum;

                            //updateInfo.atd_cParentRecordNum = infoObject.atd_cParentRecordNum;

                            updateInfo.atd_cDescript = infoObject.atd_cDescript;

                            updateInfo.atd_lValid = infoObject.atd_lValid;

                            db.SubmitChanges();

                            isSuccess = true;
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return isSuccess;
        }

        public bool DeleteRecord(IModelObject KeyObject)
        {
            bool isSuccess = false;

            if (KeyObject != null)
            {
                try
                {
                    try
                    {
                        using (VPMSDBDataContext db = new VPMSDBDataContext())
                        {
                            ArticleTypeDefine_atd_Info deleteInfo = KeyObject as ArticleTypeDefine_atd_Info;

                            ArticleTypeDefine_atd deleteRecord = db.ArticleTypeDefine_atds.FirstOrDefault(t => t.atd_iRecordID == deleteInfo.atd_iRecordID);

                            if (deleteRecord != null)
                            {
                                if (HasChild(deleteRecord))
                                {
                                    //deleteInfo = Common.General.CopyObjectValue<ArticleTypeDefine_atd, ArticleTypeDefine_atd_Info>(deleteRecord);

                                    //foreach (ArticleTypeDefine_atd_Info deleteItem in GetAllChildren(deleteInfo))
                                    //{
                                    //    ArticleTypeDefine_atd delRecord = new ArticleTypeDefine_atd();

                                    //    delRecord.atd_cTypeNum = deleteItem.atd_cTypeNum;

                                        
                                    //}
                                    DeleteChildrenRecord(deleteRecord);

                                }

                                db.ArticleTypeDefine_atds.DeleteOnSubmit(deleteRecord);

                                db.SubmitChanges();

                                isSuccess = true;
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        
                        throw Ex;
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return isSuccess;
        }

        private void DeleteChildrenRecord(ArticleTypeDefine_atd deleteInfo)
        {
            if (deleteInfo != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        IEnumerable<ArticleTypeDefine_atd> delTabs = from t in db.ArticleTypeDefine_atds
                                                                     where t.atd_cParentRecordNum == deleteInfo.atd_cTypeNum
                                                                     select t;

                        if (delTabs != null && delTabs.Count() > 0)
                        {
                            foreach (ArticleTypeDefine_atd deleteItem in delTabs)
                            {
                                if (HasChild(deleteItem))
                                {
                                    DeleteChildrenRecord(deleteItem);
                                }
                            }

                            db.ArticleTypeDefine_atds.DeleteAllOnSubmit(delTabs);

                            db.SubmitChanges();
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }
        }

        public ArticleTypeDefine_atd_Info DisplayRecord(IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        public List<ArticleTypeDefine_atd_Info> SearchRecords(IModelObject searchCondition)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IArticleTypeDefineDA Members

        public List<ArticleTypeDefine_atd_Info>GetAllChildren(ArticleTypeDefine_atd_Info ArticleTypeInfo)
        {
            List<ArticleTypeDefine_atd_Info> ChildrenInfos = new List<ArticleTypeDefine_atd_Info>();

            if (ArticleTypeInfo != null)
            {
                string sqlScript = string.Empty;

                sqlScript += "select * " + Environment.NewLine;

                sqlScript += "from dbo.ArticleTypeDefine_atd" + Environment.NewLine;

                sqlScript += "where atd_lValid=1" + Environment.NewLine;

                sqlScript += "and atd_cParentRecordNum='" + ArticleTypeInfo.atd_cTypeNum + "'" + Environment.NewLine;

                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        IEnumerable<ArticleTypeDefine_atd_Info> IEChildrenInfo = null;

                        IEChildrenInfo = db.ExecuteQuery<ArticleTypeDefine_atd_Info>(sqlScript, new object[] { });

                        if (IEChildrenInfo != null)
                        {
                            foreach (ArticleTypeDefine_atd_Info item in IEChildrenInfo)
                            {
                                item.hasChild = HasChild(item);

                                ChildrenInfos.Add(item);
                            }
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }


            return ChildrenInfos;
        }

        public ArticleTypeDefine_atd_Info GetTreeRoot(string codeMasterDefineKey2)
        {
            ArticleTypeDefine_atd_Info treeRootInfo = null;

            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt root = db.CodeMaster_cmts.FirstOrDefault(t => t.cmt_cKey1 == DefineConstantValue.CodeMasterDefine.KEY1_SIOT_ARTICLETYPEDEFINE && t.cmt_cKey2 == codeMasterDefineKey2.ToString());

                    if (root != null)
                    {
                        treeRootInfo = new ArticleTypeDefine_atd_Info();

                        treeRootInfo.atd_cTypeNum = root.cmt_cKey2;

                        treeRootInfo.atd_cDescript = root.cmt_cValue;

                        treeRootInfo.atd_lValid = true;
                    }
                }
            }
            catch (Exception Ex)
            {

                throw Ex;
            }

            return treeRootInfo;
        }

        private bool HasChild(ArticleTypeDefine_atd_Info checkInfo)
        {
            bool hasChild = false;

            if (checkInfo != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        int count = 0;

                        count = db.ArticleTypeDefine_atds.Where(t => t.atd_cParentRecordNum == checkInfo.atd_cTypeNum).Count();

                        if (count > 0)
                        {
                            hasChild = true;
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return hasChild;
        }

        private bool HasChild(ArticleTypeDefine_atd checkInfo)
        {
            bool hasChild = false;

            if (checkInfo != null)
            {
                try
                {
                    using (VPMSDBDataContext db = new VPMSDBDataContext())
                    {
                        int count = 0;

                        count = db.ArticleTypeDefine_atds.Where(t => t.atd_cParentRecordNum == checkInfo.atd_cTypeNum).Count();

                        if (count > 0)
                        {
                            hasChild = true;
                        }
                    }
                }
                catch (Exception Ex)
                {

                    throw Ex;
                }
            }

            return hasChild;
        }

        #endregion
    }
}
