using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Model.Base;

namespace DAL.LocalDefine.Base
{
    class DataBaseDictionary
    {
        public static readonly DataBaseDictionary Instance = new DataBaseDictionary();

        public DataBaseDictionary()
        {

        }

        #region 方法

        /// <summary>
        /// 获得字码主档数据库字典

        /// </summary>
        public Dictionary<string, string> GetCodeMasterDBD()
        {
            Dictionary<string, string> dbDictionary = new Dictionary<string, string>();
            dbDictionary.Add(CodeMasterInfoEnum.CmtIRecID.ToString(), "cmt_iRecID");
            dbDictionary.Add(CodeMasterInfoEnum.CmtCKey1.ToString(), "cmt_cKey1");
            dbDictionary.Add(CodeMasterInfoEnum.CmtCKey2.ToString(), "cmt_cKey2");
            dbDictionary.Add(CodeMasterInfoEnum.CmtCValue.ToString(), "cmt_cValue");
            dbDictionary.Add(CodeMasterInfoEnum.CmtFNumber.ToString(), "cmt_fNumber");
            dbDictionary.Add(CodeMasterInfoEnum.CmtCRemark.ToString(), "cmt_cRemark");
            dbDictionary.Add(CodeMasterInfoEnum.CmtCRemark2.ToString(), "cmt_cRemark2");
            dbDictionary.Add(CodeMasterInfoEnum.CmtCAdd.ToString(), "cmt_cAdd");
            dbDictionary.Add(CodeMasterInfoEnum.CmtDAddDate.ToString(), "cmt_dAddDate");
            dbDictionary.Add(CodeMasterInfoEnum.CmtCLast.ToString(), "cmt_cLast");
            dbDictionary.Add(CodeMasterInfoEnum.CmtDLastDate.ToString(), "cmt_dLastDate");
            dbDictionary.Add(CodeMasterInfoEnum.TableName.ToString(), "CodeMaster_cmt");

            return dbDictionary;
        }

        public Dictionary<string, string> GetUserInformationDBD()
        {
            Dictionary<string, string> dbDictionary = new Dictionary<string, string>();
            dbDictionary.Add(UserInformationInfoEnum.UsmIRecID.ToString(), "usm_iRecID");
            dbDictionary.Add(UserInformationInfoEnum.UsmCUserID.ToString(), "usm_cUserID");
            dbDictionary.Add(UserInformationInfoEnum.UsmCEngName.ToString(), "usm_cEngName");
            dbDictionary.Add(UserInformationInfoEnum.UsmCChaName.ToString(), "usm_cChaName");
            dbDictionary.Add(UserInformationInfoEnum.UsmCDeptNum.ToString(), "usm_cDeptNum");
            dbDictionary.Add(UserInformationInfoEnum.UsmCPasswork.ToString(), "usm_cPasswork");
            dbDictionary.Add(UserInformationInfoEnum.UsmILock.ToString(), "usm_iLock");
            dbDictionary.Add(UserInformationInfoEnum.UsmCRemark.ToString(), "usm_cRemark");
            dbDictionary.Add(UserInformationInfoEnum.UsmCAdd.ToString(), "usm_cAdd");
            dbDictionary.Add(UserInformationInfoEnum.UsmDAddDate.ToString(), "usm_dAddDate");
            dbDictionary.Add(UserInformationInfoEnum.UsmCLast.ToString(), "usm_cLast");
            dbDictionary.Add(UserInformationInfoEnum.UsmDLastDate.ToString(), "usm_dLastDate");
            dbDictionary.Add(UserInformationInfoEnum.TableName.ToString(), "UserMaster_usm");

            return dbDictionary;
        }

        #endregion

    }
}
