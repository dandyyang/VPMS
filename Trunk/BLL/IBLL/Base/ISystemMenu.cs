using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.General;
using Model.SysMaster;

namespace BLL.IBLL.Base
{
    public interface ISystemMenu
    {
        TreeNodeInfo[] GetMenuTreeNodes();
        TreeNodeInfo[] GetMenuTreeNodes(Sys_UserMaster_usm_Info userInfo);
        TreeNodeInfo[] CheckUser(Sys_UserMaster_usm_Info userInfo);
    }
}
