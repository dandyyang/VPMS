using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Management.Master;
using Model.Management.Master;
using LinqToSQLModel;

namespace DAL.SqlDAL.Management.Master
{
    class CodeMasterDA:ICodeMasterDA
    {
        #region IDataBaseCommandDA<CodeMaster_cmt_Info> Members

        public CodeMaster_cmt_Info GetRecord_First()
        {
            CodeMaster_cmt_Info info;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt query = db.CodeMaster_cmts.OrderBy(t => t.cmt_iRecordID).FirstOrDefault();
                    info = Common.General.CopyObjectValue<CodeMaster_cmt, CodeMaster_cmt_Info>(query);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public CodeMaster_cmt_Info GetRecord_Last()
        {
            CodeMaster_cmt_Info info;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt query = db.CodeMaster_cmts.OrderByDescending(t => t.cmt_iRecordID).FirstOrDefault();
                    info = Common.General.CopyObjectValue<CodeMaster_cmt, CodeMaster_cmt_Info>(query);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public CodeMaster_cmt_Info GetRecord_Previous(Model.Base.DataBaseCommandInfo commandInfo)
        {
            CodeMaster_cmt_Info info;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt query = db.CodeMaster_cmts
                                                     .Where(t => t.cmt_iRecordID < Convert.ToInt32(commandInfo.KeyInfoList[0].KeyValue))
                                                     .OrderByDescending(t => t.cmt_iRecordID)
                                                     .FirstOrDefault();
                    info = Common.General.CopyObjectValue<CodeMaster_cmt, CodeMaster_cmt_Info>(query);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public CodeMaster_cmt_Info GetRecord_Next(Model.Base.DataBaseCommandInfo commandInfo)
        {
            CodeMaster_cmt_Info info;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt query = db.CodeMaster_cmts
                                                     .Where(t => t.cmt_iRecordID > Convert.ToInt32(commandInfo.KeyInfoList[0].KeyValue))
                                                     .OrderBy(t => t.cmt_iRecordID)
                                                     .FirstOrDefault();
                    info = Common.General.CopyObjectValue<CodeMaster_cmt, CodeMaster_cmt_Info>(query);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        #endregion

        #region IMainDA<CodeMaster_cmt_Info> Members

        public bool InsertRecord(CodeMaster_cmt_Info infoObject)
        {
            bool isSuccess = false;
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            info = infoObject;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt newTab = Common.General.CopyObjectValue<CodeMaster_cmt_Info, CodeMaster_cmt>(info);
                    db.CodeMaster_cmts.InsertOnSubmit(newTab);
                    db.SubmitChanges();
                    isSuccess = true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public bool UpdateRecord(CodeMaster_cmt_Info infoObject)
        {
            bool isSuccess = false;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                 
                    CodeMaster_cmt query = db.CodeMaster_cmts.SingleOrDefault(t => t.cmt_iRecordID == infoObject.cmt_iRecordID);
                    if (query != null)
                    {
                        query.cmt_cKey1 = infoObject.cmt_cKey1;
                        query.cmt_cKey2 = infoObject.cmt_cKey2;
                        query.cmt_cValue = infoObject.cmt_cValue;
                        query.cmt_fNumber = infoObject.cmt_fNumber;
                        query.cmt_cRemark = infoObject.cmt_cRemark;
                        query.cmt_cRemark2 = infoObject.cmt_cRemark2;

                        query.cmt_cLast = infoObject.cmt_cLast;
                        query.cmt_dLastDate = infoObject.cmt_dLastDate;
                        db.SubmitChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public bool DeleteRecord(Model.IModel.IModelObject KeyObject)
        {
            bool isSuccess = false;
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            info=KeyObject as CodeMaster_cmt_Info;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt delTab = db.CodeMaster_cmts.SingleOrDefault(t => t.cmt_iRecordID == info.cmt_iRecordID);
                    if (delTab != null)
                    {
                        db.CodeMaster_cmts.DeleteOnSubmit(delTab);
                        db.SubmitChanges();
                        isSuccess = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return isSuccess;
        }

        public CodeMaster_cmt_Info DisplayRecord(Model.IModel.IModelObject KeyObject)
        {
            CodeMaster_cmt_Info info=new CodeMaster_cmt_Info();
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt disTab = db.CodeMaster_cmts.SingleOrDefault(t => t.cmt_iRecordID == ((KeyObject) as CodeMaster_cmt_Info).cmt_iRecordID);
                    if (disTab != null)
                    {
                        info = Common.General.CopyObjectValue<CodeMaster_cmt, CodeMaster_cmt_Info>(disTab);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return info;
        }

        public List<CodeMaster_cmt_Info> SearchRecords(Model.IModel.IModelObject searchCondition)
        {
            List<CodeMaster_cmt_Info> list = new List<CodeMaster_cmt_Info>();
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            info = searchCondition as CodeMaster_cmt_Info;

            string sqlString = string.Empty;
            string whereString = string.Empty;
            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += "*" + Environment.NewLine;
            sqlString += "FROM dbo.CodeMaster_cmt" + Environment.NewLine;
            whereString = "WHERE 1=1" + Environment.NewLine;

            if (info.cmt_cKey1!="")
            {
                whereString += "AND cmt_cKey1='" + info.cmt_cKey1 + "' " + Environment.NewLine; 
            }
            if (info.cmt_cKey2!="")
            {
                whereString += "AND cmt_cKey2='" + info.cmt_cKey2 + "' " + Environment.NewLine; 
            }
            if (info.cmt_cValue!="")
            {
                if (info.cmt_cValue.ToString().Contains("*") || info.cmt_cValue.ToString().Contains("?"))
                {
                    whereString += "AND cmt_cValue LIKE N'" + info.cmt_cValue.ToString().Replace("*","%").Replace("?","_") + "' " + Environment.NewLine;  
                }
                else 
                {
                    whereString += "AND cmt_cValue LIKE N'%" + info.cmt_cValue + "%' " + Environment.NewLine;  
                }
            }
            if (info.cmt_fNumber!=0)
            {
                whereString += "AND cmt_fNumber=Convert(decimal," + info.cmt_fNumber.ToString() + ")" + Environment.NewLine; 
            }
            IEnumerable<CodeMaster_cmt_Info> infos = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<CodeMaster_cmt_Info>(sqlString + whereString, new object[] { });

                    if (infos != null)
                    {
                        list = infos.ToList<CodeMaster_cmt_Info>();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return list;
        }

        #endregion

        #region IExtraDA Members

        public bool IsExistRecord(object KeyObject)
        {
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            info = KeyObject as CodeMaster_cmt_Info;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    CodeMaster_cmt query = db.CodeMaster_cmts.SingleOrDefault(t => t.cmt_cKey1 == info.cmt_cKey1 && t.cmt_cKey2 == info.cmt_cKey2);
                    if (query != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return false;
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

        public Model.IModel.IModelObject GetTableFieldLenght()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ICodeMasterDA Members

        public List<CodeMaster_cmt_Info> FindRecord(CodeMaster_cmt_Info info)
        {
            //throw new NotImplementedException();
            List<CodeMaster_cmt_Info> list = new List<CodeMaster_cmt_Info>();
            //CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            //info = searchCondition as CodeMaster_cmt_Info;

            string sqlString = string.Empty;
            string whereString = string.Empty;
            sqlString = "SELECT TOP " + Common.DefineConstantValue.ListRecordMaxCount.ToString() + Environment.NewLine;
            sqlString += "*" + Environment.NewLine;
            sqlString += "FROM dbo.CodeMaster_cmt" + Environment.NewLine;
            whereString = "WHERE 1=1" + Environment.NewLine;

            if (info.cmt_cKey1 != "")
            {
                whereString += "AND cmt_cKey1='" + info.cmt_cKey1 + "' " + Environment.NewLine;
            }
            if (info.cmt_cKey2 != "")
            {
                whereString += "AND cmt_cKey2='" + info.cmt_cKey2 + "' " + Environment.NewLine;
            }
            if (info.cmt_cValue != "")
            {
                if (info.cmt_cValue.ToString().Contains("*") || info.cmt_cValue.ToString().Contains("?"))
                {
                    whereString += "AND cmt_cValue LIKE N'" + info.cmt_cValue.ToString().Replace("*", "%").Replace("?", "_") + "' " + Environment.NewLine;
                }
                else
                {
                    whereString += "AND cmt_cValue LIKE N'%" + info.cmt_cValue + "%' " + Environment.NewLine;
                }
            }
            if (info.cmt_fNumber != 0)
            {
                whereString += "AND cmt_fNumber=Convert(decimal," + info.cmt_fNumber.ToString() + ")" + Environment.NewLine;
            }
            IEnumerable<CodeMaster_cmt_Info> infos = null;
            try
            {
                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    infos = db.ExecuteQuery<CodeMaster_cmt_Info>(sqlString + whereString, new object[] { });

                    if (infos != null)
                    {
                        list = infos.ToList<CodeMaster_cmt_Info>();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return list;
        }

        #endregion
    }
}
