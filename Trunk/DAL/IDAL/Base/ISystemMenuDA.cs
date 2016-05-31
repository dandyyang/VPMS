using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.General;
using Model.SysMaster;

namespace DAL.IDAL.Base
{
    public interface ISystemMenuDA
	{
        TreeNodeInfo[] GetMenuTreeNodes();
        TreeNodeInfo[] GetMenuTreeNodes(Sys_UserMaster_usm_Info _userInfo);
	}
}
