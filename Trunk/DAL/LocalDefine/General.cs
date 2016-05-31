using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Base;
using Model.General;
using Model.Base;
using DAL.Factory.Base;

namespace DAL.LocalDefine
{
    class General
    {
        public General()
        {

        }

        /// <summary>
        /// 从字码主档中获得Combobox数据列表
        /// </summary>
        /// <param name="codeMasterKey1">主键一</param>
        /// <returns></returns>
        public List<ComboboxDataInfo> GetComboboxDataInfoListFromCodeMaster(string codeMasterKey1)
        {
            List<ComboboxDataInfo> comboboxDataInfoList = null;
            List<CodeMasterInfo> codeMasterInfoList = null;
            CodeMasterFindInfo codeMasterFindInfo = new CodeMasterFindInfo();
            ICodeMasterDA iCodeMasterDA = null;

            codeMasterFindInfo.CmtCKey1 = codeMasterKey1;

            iCodeMasterDA = CodeMasterDAFactory.Instance.GetCodeMasterDA();

            try
            {
                codeMasterInfoList = iCodeMasterDA.FindRecord(codeMasterFindInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (codeMasterInfoList != null)
            {
                if (codeMasterInfoList.Count > 0)
                {
                    ComboboxDataInfo comboboxDataInfo = null;
                    comboboxDataInfoList = new List<ComboboxDataInfo>();
                    for (int i = 0; i < codeMasterInfoList.Count; i++)
                    {
                        comboboxDataInfo = new ComboboxDataInfo();
                        comboboxDataInfo.DisplayMember = codeMasterInfoList[i].CmtCValue;
                        comboboxDataInfo.ValueMember = codeMasterInfoList[i].CmtCKey2;

                        comboboxDataInfoList.Add(comboboxDataInfo);
                    }
                }
            }

            return comboboxDataInfoList;
        }

        public static string ReplaceSQLLikeCondition(string value)
        {
            value=value.Replace("?", "_").Replace("*", "%").Replace("'","''");

            return value;
        }
    }
}
