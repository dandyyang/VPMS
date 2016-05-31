using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Master;
//using DAL.LocalDefine.Master;
using Model.Master;
using Model.IModel;
using DAL.LocalDefine;
using DAL.SqlDAL.LocalLayer;

namespace DAL.SqlDAL.Master
{
    class DeptMasterDA : IDeptMasterDA
    {
        private Dictionary<string, string> _dataDictionary;
        private string _newLineSybm = GeneralDefine.Instance.NewLineSymbol;

        public DeptMasterDA()
        {
            //this._dataDictionary = DataDictionary.Instance.GetDeptMasterDBD();
        }

        #region IDeptMasterDA Members

        public List<Model.Master.DeptMasterInfo> GetAllRecord()
        {
            string sqlString = string.Empty;

            sqlString = "SELECT * FROM " + this._dataDictionary[DeptMasterInfoEnum.TableName.ToString()] + this._newLineSybm;

            DataBaseCommand<DeptMasterInfo> dbCommand = new DataBaseCommand<DeptMasterInfo>();
            List<DeptMasterInfo> deptMasterInfoList;

            try
            {
                deptMasterInfoList = dbCommand.GetRecordList(sqlString, this._dataDictionary);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return deptMasterInfoList;
        }

        public bool IsExistRecord(string deptNum)
        {
            bool isExist = false;
            string sqlString = string.Empty;
            object valueObject = null;

            sqlString = "SELECT COUNT(1) FROM " + this._dataDictionary[DeptMasterInfoEnum.TableName.ToString()] + this._newLineSybm;
            sqlString += " WHERE " + this._dataDictionary[DeptMasterInfoEnum.DpmCDeptNumber.ToString()] + "='" + deptNum.Replace("'", "''").Trim() + "'";

            DataBaseCommand<DeptMasterInfo> dataBaseCommand = new DataBaseCommand<DeptMasterInfo>();

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

        public Model.Master.DeptMasterInfo GetTableFieldLenght()
        {
            DeptMasterInfo deptMasterInfo = new DeptMasterInfo();

            deptMasterInfo.DeptName_Lenght = 20;
            deptMasterInfo.DeptNumber_Lenght = 20;
            deptMasterInfo.Remark_Lenght = 50;

            return deptMasterInfo;
        }

        #endregion

        #region IMainDA<DeptMasterInfo> Members

        public bool InsertRecord(Model.Master.DeptMasterInfo infoObject)
        {
            string sqlString = string.Empty;
            bool isSuccess = false;

            if (infoObject == null)
            {
                return isSuccess;
            }

            sqlString = "INSERT INTO " + this._dataDictionary[DeptMasterInfoEnum.TableName.ToString()] + "(" + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.DpmCDeptNumber.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.DpmCDeptName.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.Remark.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.Add.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.AddDate.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.Last.ToString()] + "," + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.LastDate.ToString()] + ")" + this._newLineSybm;
            sqlString += "VALUES(" + this._newLineSybm;
            sqlString += "'" + infoObject.DpmCDeptNumber.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += "'" + infoObject.DpmCDeptName.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += "'" + infoObject.Remark.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += "'" + infoObject.Add.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            if (infoObject.AddDate == null)
            {
                sqlString += "NULL," + this._newLineSybm;
            }
            else
            {
                sqlString += "'" + infoObject.AddDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'," + this._newLineSybm;
            }
            sqlString += "'" + infoObject.Last.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            if (infoObject.LastDate == null)
            {
                sqlString += "NULL)" + this._newLineSybm;
            }
            else
            {
                sqlString += "'" + infoObject.LastDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "')" + this._newLineSybm;
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

        public bool UpdateRecord(Model.Master.DeptMasterInfo infoObject)
        {
            string sqlString = string.Empty;
            bool isSuccess = false;

            if (infoObject == null)
            {
                return isSuccess;
            }

            sqlString = "UPDATE " + this._dataDictionary[DeptMasterInfoEnum.TableName.ToString()] + " SET " + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.DpmCDeptName.ToString()] + "='" + infoObject.DpmCDeptName.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.Remark.ToString()] + "='" + infoObject.Remark.ToString() + "'," + this._newLineSybm;
            sqlString += this._dataDictionary[DeptMasterInfoEnum.Last.ToString()] + "='" + infoObject.Last.Trim().Replace("'", "''") + "'," + this._newLineSybm;
            if (infoObject.LastDate == null)
            {
                sqlString += this._dataDictionary[DeptMasterInfoEnum.LastDate.ToString()] + "=NULL" + this._newLineSybm;
            }
            else
            {
                sqlString += this._dataDictionary[DeptMasterInfoEnum.LastDate.ToString()] + "='" + infoObject.LastDate.Value.ToString("yyyy-MM-dd hh:mm:ss") + "'" + this._newLineSybm;
            }
            sqlString += " WHERE " + this._dataDictionary[DeptMasterInfoEnum.RecordID.ToString()] + "=" + infoObject.RecordID.ToString();

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

            DeptMasterInfo info = KeyObject as DeptMasterInfo;

            sqlString = "DELETE FROM " + this._dataDictionary[DeptMasterInfoEnum.TableName.ToString()] + this._newLineSybm;

            sqlString += " WHERE " + this._dataDictionary[DeptMasterInfoEnum.RecordID.ToString()] + "=" + info.RecordID.ToString();

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

        public Model.Master.DeptMasterInfo DisplayRecord(IModelObject KeyObject)
        {
            string sqlString = string.Empty;

            DeptMasterInfo info = KeyObject as DeptMasterInfo;

            sqlString = "SELECT * FROM " + this._dataDictionary[DeptMasterInfoEnum.TableName.ToString()] + Environment.NewLine;
            sqlString += " WHERE " + this._dataDictionary[DeptMasterInfoEnum.RecordID.ToString()] + "=" + info.RecordID.ToString();

            DataBaseCommand<DeptMasterInfo> dbCommand = new DataBaseCommand<DeptMasterInfo>();
            DeptMasterInfo deptMasterInfo;

            try
            {
                deptMasterInfo = dbCommand.GetRecord(sqlString, this._dataDictionary);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return deptMasterInfo;
        }

        public DeptMasterInfo GetRecord(IModelObject KeyObject)
        {
            string sqlString = string.Empty;

            DeptMasterInfo info = KeyObject as DeptMasterInfo;

            sqlString = "SELECT * FROM " + this._dataDictionary[DeptMasterInfoEnum.TableName.ToString()] + Environment.NewLine;
            sqlString += " WHERE " + this._dataDictionary[DeptMasterInfoEnum.DpmCDeptNumber.ToString()] + "='" + info.DpmCDeptNumber.Replace("'", "''") + "'";

            DataBaseCommand<DeptMasterInfo> dbCommand = new DataBaseCommand<DeptMasterInfo>();
            DeptMasterInfo deptMasterInfo;

            try
            {
                deptMasterInfo = dbCommand.GetRecord(sqlString, this._dataDictionary);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return deptMasterInfo;
        }

        #endregion


        #region IDeptMasterDA Members


        public DeptMasterInfo GetRecord(string deptNum)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMainDA<DeptMasterInfo> Members


        public List<DeptMasterInfo> SearchRecords(IModelObject MDobject)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDeptMasterDA Members

        List<DeptMasterInfo> IDeptMasterDA.GetAllRecord()
        {
            throw new NotImplementedException();
        }

        DeptMasterInfo IDeptMasterDA.GetRecord(string deptNum)
        {
            throw new NotImplementedException();
        }

        bool IDeptMasterDA.IsExistRecord(string deptNum)
        {
            throw new NotImplementedException();
        }

        bool IDeptMasterDA.IsLockRecord(long recordID)
        {
            throw new NotImplementedException();
        }

        DeptMasterInfo IDeptMasterDA.GetTableFieldLenght()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IMainDA<DeptMasterInfo> Members

        bool DAL.IDAL.IMainDA<DeptMasterInfo>.InsertRecord(DeptMasterInfo infoObject)
        {
            throw new NotImplementedException();
        }

        bool DAL.IDAL.IMainDA<DeptMasterInfo>.UpdateRecord(DeptMasterInfo infoObject)
        {
            throw new NotImplementedException();
        }

        bool DAL.IDAL.IMainDA<DeptMasterInfo>.DeleteRecord(IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        DeptMasterInfo DAL.IDAL.IMainDA<DeptMasterInfo>.DisplayRecord(IModelObject KeyObject)
        {
            throw new NotImplementedException();
        }

        List<DeptMasterInfo> DAL.IDAL.IMainDA<DeptMasterInfo>.SearchRecords(IModelObject MDobject)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
