using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.SysMaster;

namespace BLL.IBLL.SysMaster
{
    public interface IUserPurviewBL : IDataBaseCommandBL<Sys_UserPurview_usp_Info>, IExtraBL, IMainBL
    {
    }
}
