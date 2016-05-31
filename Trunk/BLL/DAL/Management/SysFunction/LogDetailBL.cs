using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.SqlDAL.SysFunction;
using Model.SysFunction;

namespace BLL.DAL.Management.SysFunction
{
    public class LogDetailBL
    {
        private LogDetailDA m_objLogDA;

        public LogDetailBL()
        {
            m_objLogDA = new LogDetailDA();
        }

        public List<LogDetail_lgd_Info> GetLogs(string p_strIP, string p_strSystemName, string p_strModel, string p_strType, string p_strOperator)
        {
            return m_objLogDA.GetLogs(p_strIP, p_strSystemName, p_strModel, p_strType, p_strOperator);
        }
    }
}
