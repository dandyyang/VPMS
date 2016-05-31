using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace DAL.IDAL.SysMaster
{
    public interface ISysFunctionMasterDA : IDataBaseCommandDA<Sys_FunctionMaster_fum_Info>, IExtraDA, IMainDA<Sys_FunctionMaster_fum_Info>
    {
    }
}
