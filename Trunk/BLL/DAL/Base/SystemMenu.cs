using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.General;
using BLL.LocalLayer;
using BLL.IBLL.Base;
using DAL.Factory.Base;
using DAL.IDAL.Base;
using BLL.Factory.Base;
using Model.SysMaster;

namespace BLL.DAL.Base
{
    class SystemMenu:ISystemMenu
    {
        ISystemMenuDA _systemMenuDA;

        public SystemMenu()
        {
            this._systemMenuDA = SystemMenuDALFactory.Instance.GetSystemMenuDA();
        }

        public TreeNodeInfo[] GetMenuTreeNodes()
        {
            TreeNodeInfo[] treeNodeInfos = null; 

            try
            {
                treeNodeInfos = _systemMenuDA.GetMenuTreeNodes();
            }
            catch 
            {                
                throw;
            }

            return treeNodeInfos;
        }

        public TreeNodeInfo[] GetMenuTreeNodes(Sys_UserMaster_usm_Info userInfo)
        {
            TreeNodeInfo[] treeNodeInfos = null;

            try
            {
                treeNodeInfos = _systemMenuDA.GetMenuTreeNodes(userInfo);
            }
            catch 
            {
                throw;
            }

            return treeNodeInfos;
        }

        public TreeNodeInfo[] CheckUser(Sys_UserMaster_usm_Info userInfo)
        {
            TreeNodeInfo[] treeNodeInfos = null;
            if (userInfo.usm_cUserLoginID.ToUpper()== "SA")
            {
                treeNodeInfos = GetMenuTreeNodes();
            }
            else
            {
                treeNodeInfos = GetMenuTreeNodes(userInfo);
            }

            return treeNodeInfos;
        }
    }
}
