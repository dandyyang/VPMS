using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysFunction;

namespace DAL.IDAL.SysFunction
{
    public interface ILogDetailDA
    {
        List<LogDetail_lgd_Info> GetLogs(string p_strIP, string p_strSystemName, string p_strModel, string p_strType, string p_strOperator);

        List<LogDetail_lgd_Info> GetLogs(LogDetail_lgd_Info lgdInfo);

        void InsertLog(LogDetail_lgd_Info p_objLog);
    }
}
