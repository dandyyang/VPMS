using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace DAL.IDAL.SysMaster
{
    public interface IUserPurviewDA : IDataBaseCommandDA<Sys_UserPurview_usp_Info>, IExtraDA, IMainDA<Sys_UserPurview_usp_Info>
    {
    }
}
