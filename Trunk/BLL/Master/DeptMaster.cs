using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Master;
using Model.General;
using DAL.IDAL.Master;
using DAL.Factory.Master;
using Model.IModel;
using Common;

namespace BLL.Master
{
    public class DeptMaster
    {
        private IDeptMasterDA _deptMasterDA = null;
        public DeptMaster() 
        {
            this._deptMasterDA = MasterDAFactory.Instance.GetDeptMasterDA();
        }

        public DeptMasterInfo DisplayRecord(IModelObject KeyObject)
        {
            DeptMasterInfo info = null;
            try
            {
                info = _deptMasterDA.DisplayRecord(KeyObject);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public DeptMasterInfo[] GetRecords()
        {
            DeptMasterInfo[] infos = null;
            List<DeptMasterInfo> deptMasterInfoList = null;
            try
            {
                deptMasterInfoList = _deptMasterDA.GetAllRecord();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (deptMasterInfoList != null)
            {
                infos = deptMasterInfoList.ToArray();
            }

            return infos;
        }

        public DeptMasterInfo GetRecord(string deptNum)
        {
            DeptMasterInfo info = null;
            try
            {
                info = _deptMasterDA.GetRecord(deptNum);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public ReturnValueInfo InsertRecord(DeptMasterInfo deptInfo)
        {
            if (deptInfo == null)
            {
                return null;
            }

            ReturnValueInfo returnValue = new ReturnValueInfo();
            ValidateValue(deptInfo, returnValue);
            if (!returnValue.boolValue)
            {
                return returnValue;
            }

            bool isExistRecord = false;
            try
            {
                isExistRecord = this._deptMasterDA.IsExistRecord(deptInfo.DpmCDeptNumber);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (isExistRecord)
            {
                returnValue.boolValue = false;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_RecordIsExist;

                return returnValue;
            }

            bool isSuccess = false;

            try
            {
                isSuccess = this._deptMasterDA.InsertRecord(deptInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (isSuccess)
            {
                returnValue.boolValue = true;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_AddSuccess;
            }
            else
            {
                returnValue.boolValue = false;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_AddFail;
            }

            return returnValue;
        }

        public ReturnValueInfo UpdateRecord(DeptMasterInfo deptInfo)
        {
            if (deptInfo == null)
            {
                return null;
            }

            ReturnValueInfo returnValue = new ReturnValueInfo();

            bool isSuccess = false;
            try
            {
                isSuccess = this._deptMasterDA.UpdateRecord(deptInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (isSuccess)
            {
                returnValue.boolValue = true;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_UpdateSuccess;
            }
            else
            {
                returnValue.boolValue = false;
                returnValue.messageText = DefineConstantValue.SystemMessageText.strMessageText_I_UpdateFail;
            }

            return returnValue;
        }

        public bool DeleteRecord(IModelObject KeyObject)
        {
            bool isSuccess = false;
            try
            {
                isSuccess = this._deptMasterDA.DeleteRecord(KeyObject);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isSuccess;
        }

        public DeptMasterInfo GetTableFieldLenght()
        {
            DeptMasterInfo info = null;

            try
            {
                info = this._deptMasterDA.GetTableFieldLenght();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return info;
        }

        public bool IsExistRecord(string deptNumber)
        {
            bool isExist = false;

            try
            {
                isExist = this._deptMasterDA.IsExistRecord(deptNumber);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return isExist;
        }

        private void ValidateValue(DeptMasterInfo deptInfo, ReturnValueInfo returnValue)
        {
            string messageText = string.Empty;

            returnValue.boolValue = true;

            if (deptInfo.DpmCDeptNumber.Trim() == string.Empty)
            {
                returnValue.boolValue = false;
                returnValue.messageText = "\"部门编号\"" + DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
            }
        }
    }
}
