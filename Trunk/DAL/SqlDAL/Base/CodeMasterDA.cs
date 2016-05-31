using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Model.Base;
using Model.IModel;
//using DAL.LocalDefine.Base;
using DAL.LocalDefine;
using DAL.SqlDAL.LocalLayer;
using DAL.IDAL.Base;

namespace DAL.SqlDAL.Base
{
    class CodeMasterDA : ICodeMasterDA
    {
        private Dictionary<string, string> _dataBaseDic;
        private string _newLineSybm = GeneralDefine.Instance.NewLineSymbol;
        public CodeMasterDA()
        {
            //this._dataBaseDic = DataBaseDictionary.Instance.GetCodeMasterDBD();
        }

        #region IMainDA<CodeMasterInfo> Members

        public List<CodeMasterInfo> FindRecord(CodeMasterFindInfo codeMasterFindInfo)
        {
            string sqlString = string.Empty;
            string sqlWhere = string.Empty;

            if (codeMasterFindInfo == null)
            {
                return null;
            }

            sqlWhere = " WHERE 1=1 ";
            if (codeMasterFindInfo.CmtCKey1.Trim() != "")
            {
                sqlWhere += " AND " + this._dataBaseDic[CodeMasterInfoEnum.CmtCKey1.ToString()] + "='" + codeMasterFindInfo.CmtCKey1.Replace("'", "''") + "'";
            }
            if (codeMasterFindInfo.CmtCKey2.Trim() != "")
            {
                sqlWhere += " AND " + this._dataBaseDic[CodeMasterInfoEnum.CmtCKey2.ToString()] + "='" + codeMasterFindInfo.CmtCKey2.Replace("'", "''") + "'";
            }
            if (codeMasterFindInfo.CmtCValue.Trim() != "")
            {
                sqlWhere += " AND " + this._dataBaseDic[CodeMasterInfoEnum.CmtCValue.ToString()] + "='" + codeMasterFindInfo.CmtCValue.Replace("'", "''") + "'";
            }

            sqlString = "SELECT * FROM " + this._dataBaseDic[CodeMasterInfoEnum.TableName.ToString()] + sqlWhere;

            DataBaseCommand<CodeMasterInfo> dbCommand = new DataBaseCommand<CodeMasterInfo>();
            List<CodeMasterInfo> codeMasterList=null;

            try
            {
                //codeMasterList = dbCommand.GetRecordList(sqlString, DataBaseDictionary.Instance.GetCodeMasterDBD());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return codeMasterList;
        }

        public CodeMasterInfo DisplayRecord(IModelObject KeyObject)
        {
            string sqlString = string.Empty;

            sqlString = "SELECT * FROM CodeMaster_cmt WHERE " + this._dataBaseDic[CodeMasterInfoEnum.CmtIRecID.ToString()] + "=" + KeyObject.RecordID.ToString();

            DataBaseCommand<CodeMasterInfo> dbCommand = new DataBaseCommand<CodeMasterInfo>();
            CodeMasterInfo codeMaster=null;

            try
            {
                //codeMaster = dbCommand.GetRecord(sqlString, DataBaseDictionary.Instance.GetCodeMasterDBD());
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return codeMaster;

        }

        public bool InsertRecord(CodeMasterInfo infoObject)
        {
            string sqlString = string.Empty;
            bool isSuccess = false;

            if (infoObject == null)
            {
                return isSuccess;
            }

            sqlString = "INSERT INTO " + this._dataBaseDic[CodeMasterInfoEnum.TableName.ToString()] + "(" + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCKey1.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCKey2.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCValue.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtFNumber.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCRemark.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCRemark2.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCAdd.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtDAddDate.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCLast.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtDLastDate.ToString()] + ")" + this._newLineSybm;
            sqlString += "VALUES(" + this._newLineSybm;
            sqlString += "'" + infoObject.CmtCKey1.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += "'" + infoObject.CmtCKey2.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += "'" + infoObject.CmtCValue.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += infoObject.CmtFNumber.ToString() + "," + this._newLineSybm;
            sqlString += "'" + infoObject.CmtCRemark.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += "'" + infoObject.CmtCRemark2.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += "'" + infoObject.CmtCAdd.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            if (infoObject.CmtDAddDate == null)
            {
                sqlString += "NULL," + this._newLineSybm;
            }
            else
            {
                sqlString += "'" + infoObject.CmtDAddDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'," + this._newLineSybm;
            }
            sqlString += "'" + infoObject.CmtCLast.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            if (infoObject.CmtDLastDate == null)
            {
                sqlString += "NULL)" + this._newLineSybm;
            }
            else
            {
                sqlString += "'" + infoObject.CmtDLastDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "')" + this._newLineSybm;
            }

            DataAccessLayer dal = new DataAccessLayer();
            try
            {
                isSuccess = dal.ExecNonQuery(sqlString);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isSuccess;

        }

        public bool UpdateRecord(CodeMasterInfo infoObject)
        {
            string sqlString = string.Empty;
            bool isSuccess = false;

            if (infoObject == null)
            {
                return isSuccess;
            }

            sqlString = "UPDATE " + this._dataBaseDic[CodeMasterInfoEnum.TableName.ToString()] + " SET " + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCValue.ToString()] + "='" + infoObject.CmtCValue.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtFNumber.ToString()] + "=" + infoObject.CmtFNumber.ToString() + "," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCRemark.ToString()] + "='" + infoObject.CmtCRemark.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCRemark2.ToString()] + "='" + infoObject.CmtCRemark2.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtCLast.ToString()] + "='" + infoObject.CmtCLast.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            if (infoObject.CmtDLastDate == null)
            {
                sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtDLastDate.ToString()] + "=NULL" + this._newLineSybm;
            }
            else
            {
                sqlString += this._dataBaseDic[CodeMasterInfoEnum.CmtDLastDate.ToString()] + "='" + infoObject.CmtDLastDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" + this._newLineSybm;
            }
            sqlString += " WHERE " + this._dataBaseDic[CodeMasterInfoEnum.CmtIRecID.ToString()] + "=" + infoObject.CmtIRecID.ToString();

            DataAccessLayer dal = new DataAccessLayer();
            try
            {
                isSuccess = dal.ExecNonQuery(sqlString);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isSuccess;
        }

        public bool DeleteRecord(IModelObject KeyObject)
        {
            string sqlString = string.Empty;
            bool isSuccess = false;

            sqlString = "DELETE FROM " + this._dataBaseDic[CodeMasterInfoEnum.TableName.ToString()] + this._newLineSybm;

            sqlString += " WHERE " + this._dataBaseDic[CodeMasterInfoEnum.CmtIRecID.ToString()] + "=" + KeyObject.RecordID.ToString();

            DataAccessLayer dal = new DataAccessLayer();
            try
            {
                isSuccess = dal.ExecNonQuery(sqlString);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isSuccess;
        }

        public bool IsExistRecord(string key1, string key2, string value)
        {
            bool isExist = false;
            string sqlString = string.Empty;
            object valueObject = null;

            sqlString = "SELECT COUNT(1) FROM " + this._dataBaseDic[CodeMasterInfoEnum.TableName.ToString()] + this._newLineSybm;
            sqlString += " WHERE " + this._dataBaseDic[CodeMasterInfoEnum.CmtCKey1.ToString()] + "='" + key1.Replace("'", "''").Trim() + "'" + this._newLineSybm;
            sqlString += " AND " + this._dataBaseDic[CodeMasterInfoEnum.CmtCKey2.ToString()] + "='" + key2.Replace("'", "''").Trim() + "'" + this._newLineSybm;
            //sqlString += " AND " + this._dataBaseDic[CodeMasterInfoEnum.CmtCValue.ToString()] + "='" + value.Replace("'", "''").Trim() + "'" + this._newLineSybm;

            DataBaseCommand<CodeMasterInfo> dataBaseCommand = new DataBaseCommand<CodeMasterInfo>();

            try
            {
                valueObject = dataBaseCommand.GetValue(sqlString, typeof(int));
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            int count = (int)valueObject;
            if (count > 0)
            {
                isExist = true;
            }

            return isExist;
        }

        public bool IsLockRecord(long recordID)
        {
            return false;
        }

        public CodeMasterInfo GetTableFieldLenght()
        {
            CodeMasterInfo codeMasterInfo = null;

            codeMasterInfo = new CodeMasterInfo();

            codeMasterInfo.CmtCKey1_Lenght = 30;
            codeMasterInfo.CmtCKey2_Lenght = 30;
            codeMasterInfo.CmtCValue_Lenght = 30;
            codeMasterInfo.CmtCRemark_Lenght = 200;
            codeMasterInfo.CmtCRemark2_Lenght = 200;
            codeMasterInfo.CmtCAdd_Lenght = 20;
            codeMasterInfo.CmtCLast_Lenght = 20;

            #region MyRegion

            //Dictionary<string, int> dic = null;

            //DataBaseCommand<CodeMasterInfo> dbc = new DataBaseCommand<CodeMasterInfo>();

            //try
            //{
            //    dic = dbc.GetTableFieldLenght(this._dataBaseDic[CodeMasterInfoEnum.TableName.ToString()]);
            //}
            //catch (Exception Ex)
            //{
            //    throw Ex;
            //}

            //if (dic != null)
            //{
            //    codeMasterInfo = new CodeMasterInfo();

            //    codeMasterInfo.CmtCKey1_Lenght = dic[this._dataBaseDic[CodeMasterInfoEnum.CmtCKey1.ToString()]];
            //    codeMasterInfo.CmtCKey2_Lenght = dic[this._dataBaseDic[CodeMasterInfoEnum.CmtCKey2.ToString()]];
            //    codeMasterInfo.CmtCValue_Lenght = dic[this._dataBaseDic[CodeMasterInfoEnum.CmtCValue.ToString()]];
            //    codeMasterInfo.CmtCRemark_Lenght = dic[this._dataBaseDic[CodeMasterInfoEnum.CmtCRemark.ToString()]];
            //    codeMasterInfo.CmtCRemark2_Lenght = dic[this._dataBaseDic[CodeMasterInfoEnum.CmtCKey2.ToString()]];
            //    codeMasterInfo.CmtCAdd_Lenght = dic[this._dataBaseDic[CodeMasterInfoEnum.CmtCAdd.ToString()]];
            //    codeMasterInfo.CmtCLast_Lenght = dic[this._dataBaseDic[CodeMasterInfoEnum.CmtCLast.ToString()]];

            //}

            #endregion

            return codeMasterInfo;
        }

        public List<CodeMasterInfo> GetKey1s()
        {
            string sqlString = string.Empty;
            List<CodeMasterInfo> codeMasterList;
            DataBaseCommand<CodeMasterInfo> dbCommand = new DataBaseCommand<CodeMasterInfo>();

            sqlString = LocalLayer.LocalDefine.Instance.GetInitComboBoxSQL(this._dataBaseDic[CodeMasterInfoEnum.TableName.ToString()], this._dataBaseDic[CodeMasterInfoEnum.CmtCKey1.ToString()], "");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(CodeMasterInfoEnum.CmtCKey1.ToString(), this._dataBaseDic[CodeMasterInfoEnum.CmtCKey1.ToString()]);
            try
            {
                codeMasterList = dbCommand.GetRecordList(sqlString, dic);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return codeMasterList;

        }

        public List<CodeMasterInfo> GetKey2s(string key1)
        {
            string sqlString = string.Empty;
            List<CodeMasterInfo> codeMasterList;
            DataBaseCommand<CodeMasterInfo> dbCommand = new DataBaseCommand<CodeMasterInfo>();

            sqlString = "SELECT DISTINCT " + this._dataBaseDic[CodeMasterInfoEnum.CmtCKey2.ToString()] + " FROM " + this._dataBaseDic[CodeMasterInfoEnum.TableName.ToString()];
            sqlString += " WHERE " + this._dataBaseDic[CodeMasterInfoEnum.CmtCKey1.ToString()] + "='" + key1.Replace("'", "''") + "'";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(CodeMasterInfoEnum.CmtCKey2.ToString(), this._dataBaseDic[CodeMasterInfoEnum.CmtCKey2.ToString()]);
            try
            {
                codeMasterList = dbCommand.GetRecordList(sqlString, dic);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return codeMasterList;
        }

        #endregion

        #region IMainDA<CodeMasterInfo> Members

        public List<CodeMasterInfo> SearchRecords(Model.IModel.IModelObject MDobject)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
