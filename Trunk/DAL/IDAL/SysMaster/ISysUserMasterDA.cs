using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;
using Model.General;
using Model.Management.Master;

namespace DAL.IDAL.SysMaster
{
    public interface ISysUserMasterDA : IDataBaseCommandDA<Sys_UserMaster_usm_Info>, IExtraDA, IMainDA<Sys_UserMaster_usm_Info>
    {
    }
}
