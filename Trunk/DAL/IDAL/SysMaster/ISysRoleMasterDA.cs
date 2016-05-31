using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace DAL.IDAL.SysMaster
{
    public interface ISysRoleMasterDA : IDataBaseCommandDA<Sys_RoleMaster_rlm_Info>, IExtraDA, IMainDA<Sys_RoleMaster_rlm_Info>
    {
    }
}
