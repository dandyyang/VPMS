using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysFunction;
using LinqToSQLModel;
using DAL.IDAL.SysFunction;

namespace DAL.SqlDAL.SysFunction
{
    public class LogDetailDA : ILogDetailDA
    {
        public LogDetailDA()
        {

        }

        /// <summary>
        /// 取得日志
        /// </summary>
        /// <param name="p_strIP">IP</param>
        /// <param name="p_strSystemName">系统名</param>
        /// <param name="p_strModel">模组</param>
        /// <param name="p_strType">类型</param>
        /// <param name="p_strOperator">操作人</param>
        /// <returns></returns>
        public List<LogDetail_lgd_Info> GetLogs(string p_strIP, string p_strSystemName, string p_strModel, string p_strType, string p_strOperator)
        {
            StringBuilder l_strSQL = new StringBuilder();

            l_strSQL.AppendLine("Select Top 200 lgd_iID,");
            l_strSQL.AppendLine("lgd_cIpAddr,");
            l_strSQL.AppendLine("lgd_cSysName,");
            l_strSQL.AppendLine("lgd_cClassMethodName,");
            l_strSQL.AppendLine("lgd_cLogType,");
            l_strSQL.AppendLine("lgd_cLogMessage,");
            l_strSQL.AppendLine("lgd_cRemark,");
            l_strSQL.AppendLine("lgd_cOperator,");
            l_strSQL.AppendLine("lgd_dOperateDateTime");
            l_strSQL.AppendLine("From LogDetail_lgd");

            l_strSQL.AppendLine("Where 1=1");

            if (p_strIP != "")
            {
                l_strSQL.AppendLine("And lgd_cIpAddr = '" + p_strIP + "'");
            }

            if (p_strSystemName != "")
            {
                l_strSQL.AppendLine("And lgd_cSysName = '" + p_strSystemName + "'");
            }

            if (p_strModel != "")
            {
                l_strSQL.AppendLine("And lgd_cClassMethodName = '" + p_strModel + "'");
            }

            if (p_strType != "")
            {
                l_strSQL.AppendLine("And lgd_cLogType = '" + p_strType + "'");
            }

            if (p_strOperator != "")
            {
                l_strSQL.AppendLine("And lgd_cOperator = '" + p_strOperator + "'");
            }

            l_strSQL.AppendLine("Order By lgd_dOperateDateTime Desc");

            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                return db.ExecuteQuery<LogDetail_lgd_Info>(l_strSQL.ToString(), new object[] { }).ToList();
            }
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="lgdInfo"></param>
        /// <returns></returns>
        public List<LogDetail_lgd_Info> GetLogs(LogDetail_lgd_Info lgdInfo)
        {
            try
            {
                StringBuilder l_strSQL = new StringBuilder();

                l_strSQL.AppendLine("Select lgd_iID,");
                l_strSQL.AppendLine("lgd_cIpAddr,");
                l_strSQL.AppendLine("lgd_cSysName,");
                l_strSQL.AppendLine("lgd_cClassMethodName,");
                l_strSQL.AppendLine("lgd_cLogType,");
                l_strSQL.AppendLine("lgd_cLogMessage,");
                l_strSQL.AppendLine("lgd_cRemark,");
                l_strSQL.AppendLine("lgd_cOperator,");
                l_strSQL.AppendLine("lgd_dOperateDateTime");
                l_strSQL.AppendLine("From LogDetail_lgd");

                l_strSQL.AppendLine("Where 1=1");

                if (!string.IsNullOrEmpty(lgdInfo.lgd_cIpAddr))
                {
                    l_strSQL.AppendLine("And lgd_cIpAddr = '" + lgdInfo.lgd_cIpAddr + "'");
                }

                if (!string.IsNullOrEmpty(lgdInfo.lgd_cSysName))
                {
                    l_strSQL.AppendLine("And lgd_cSysName = '" + lgdInfo.lgd_cSysName + "'");
                }

                if (!string.IsNullOrEmpty(lgdInfo.lgd_cClassMethodName))
                {
                    l_strSQL.AppendLine("And lgd_cClassMethodName = '" + lgdInfo.lgd_cClassMethodName + "'");
                }

                if (!string.IsNullOrEmpty(lgdInfo.lgd_cLogType))
                {
                    l_strSQL.AppendLine("And lgd_cLogType = '" + lgdInfo.lgd_cLogType + "'");
                }

                if (!string.IsNullOrEmpty(lgdInfo.lgd_cOperator))
                {
                    l_strSQL.AppendLine("And lgd_cOperator = '" + lgdInfo.lgd_cOperator + "'");
                }

                if (lgdInfo.lgd_dOperateDateTime != DateTime.MinValue)
                {
                    l_strSQL.AppendLine("And lgd_dOperateDateTime >= '" + lgdInfo.lgd_dOperateDateTime.ToString() + "'");
                }

                l_strSQL.AppendLine("Order By lgd_dOperateDateTime Desc");

                using (VPMSDBDataContext db = new VPMSDBDataContext())
                {
                    return db.ExecuteQuery<LogDetail_lgd_Info>(l_strSQL.ToString(), new object[] { }).ToList();
                }

            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="p_objLog"></param>
        public void InsertLog(LogDetail_lgd_Info p_objLog)
        {
            using (VPMSDBDataContext db = new VPMSDBDataContext())
            {
                LogDetail_lgd l_objNewLog = Common.General.CopyObjectValue<LogDetail_lgd_Info, LogDetail_lgd>(p_objLog);
                db.LogDetail_lgds.InsertOnSubmit(l_objNewLog);
                db.SubmitChanges();
            }
        }
    }
}
