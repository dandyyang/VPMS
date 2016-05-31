using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.IBLL.SysFunction;
using DAL.IDAL.SysFunction;
using DAL.Factory.SysFunction;
using Model.SysFunction;

namespace BLL.DAL.SysFunction
{
    public class LogDetailBL : ILogDetailBL
    {
        private ILogDetailDA _ILogDetailDA;

        public LogDetailBL()
        {
            this._ILogDetailDA = SysDALFactory.GetDAL<ILogDetailDA>(SysDALFactory.LogDetail);
        }

        #region ILogDetailBL Members

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="p_strIP">所在服务器IP</param>
        /// <param name="p_strSystemName">系统名称</param>
        /// <param name="p_strModel">模组</param>
        /// <param name="p_strType">日志类型</param>
        /// <param name="p_strOperator">操作人</param>
        /// <returns></returns>
        public List<LogDetail_lgd_Info> GetLogs(string p_strIP, string p_strSystemName, string p_strModel, string p_strType, string p_strOperator)
        {
            List<LogDetail_lgd_Info> listLgdInfos = new List<LogDetail_lgd_Info>();
            try
            {
                listLgdInfos = this._ILogDetailDA.GetLogs(p_strIP, p_strSystemName, p_strModel, p_strType, p_strOperator);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return listLgdInfos;
        }

        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="lgdInfo">日志类</param>
        /// <returns></returns>
        public List<LogDetail_lgd_Info> GetLogs(Model.SysFunction.LogDetail_lgd_Info lgdInfo)
        {
            List<LogDetail_lgd_Info> listLgdInfos = new List<LogDetail_lgd_Info>();
            try
            {
                listLgdInfos = this._ILogDetailDA.GetLogs(lgdInfo);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
            return listLgdInfos;
        }

        /// <summary>
        /// 插入日志记录
        /// </summary>
        /// <param name="p_objLog"></param>
        public void InsertLog(LogDetail_lgd_Info p_objLog)
        {
            try
            {
                this._ILogDetailDA.InsertLog(p_objLog);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        #endregion
    }
}
