using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.IDAL.Base;
using DAL.LocalDefine;
using Model.SysMaster;
using Model.General;
using DAL.IDAL.SysMaster;
using DAL.Factory.SysMaster;

namespace DAL.SqlDAL.Base
{
    class SystemMenuDA:ISystemMenuDA
    {
        #region ISystemMenuDA Members

        ISysFormMasterDA _sysFormMasterDA;        

        public SystemMenuDA()
        {
            this._sysFormMasterDA = MasterDAFactory.GetDAL<ISysFormMasterDA>(MasterDAFactory.SysFormMaster); 
        }

        public TreeNodeInfo[] GetMenuTreeNodes()
        {
            Sys_FormMaster_fom_Info info = new Sys_FormMaster_fom_Info();

            List<TreeNodeInfo> treeNodeInfoList = new List<TreeNodeInfo>();
            TreeNodeInfo rootNodeInfo = null;
            TreeNodeInfo[] treeNodeInfos = null;

            TreeNodeInfo itemNodeInfo = null;

            info = _sysFormMasterDA.GetRecord_First();

            rootNodeInfo = new TreeNodeInfo();
            rootNodeInfo.Text = info.fom_cFormDesc;
            rootNodeInfo.Name = info.fom_cFormNumber;
            rootNodeInfo.Tag = info.fom_cExePath;
            rootNodeInfo.Index = info.fom_iIndex;

            rootNodeInfo.FileName = info.fom_iRecordID.ToString();

            TreeNodeInfo nodeInfo = null;
            info = new Sys_FormMaster_fom_Info();
            info.fom_iParentID = int.Parse(rootNodeInfo.FileName.ToString());
            foreach (var chile in _sysFormMasterDA.SearchRecords(info,"all"))
            {
                info = chile as Sys_FormMaster_fom_Info;
                string Name = info.fom_iRecordID.ToString();
                nodeInfo = null;
                nodeInfo = LocalGeneralMenu.Instance.InsertTreeNode(rootNodeInfo, info.fom_cFormDesc, info.fom_cExePath, info.fom_cFormNumber, info.fom_iIndex, info.fom_iImageIndex);
                info = new Sys_FormMaster_fom_Info();
                info.fom_iParentID = int.Parse(Name.ToString());
                foreach (var q in _sysFormMasterDA.SearchRecords(info, "all"))
                {
                    info = q as Sys_FormMaster_fom_Info;
                    
                    if (info.fom_cFormNumber.ToString().ToUpper() == "DEMO")
                    {
                        itemNodeInfo = LocalGeneralMenu.Instance.InsertTreeNode(nodeInfo, info.fom_cFormDesc, info.fom_cExePath, info.fom_cFormNumber, info.fom_iIndex, info.fom_iImageIndex);
                        itemNodeInfo.Remark = "EXE";
                        itemNodeInfo.FileName = "WPFUI\\WPFUI.exe";
                    }
                    else
                        LocalGeneralMenu.Instance.InsertTreeNode(nodeInfo, info.fom_cFormDesc, info.fom_cExePath, info.fom_cFormNumber, info.fom_iIndex, info.fom_iImageIndex);
                }
            }
            treeNodeInfoList.Add(rootNodeInfo);
            treeNodeInfos = treeNodeInfoList.ToArray();


            return treeNodeInfos;
        }

        public TreeNodeInfo[] GetMenuTreeNodes(Sys_UserMaster_usm_Info _usmInfo)
        {
            Sys_FormMaster_fom_Info info = new Sys_FormMaster_fom_Info();

            List<TreeNodeInfo> treeNodeInfoList = new List<TreeNodeInfo>();
            TreeNodeInfo rootNodeInfo = null;
            TreeNodeInfo[] treeNodeInfos = null;

            TreeNodeInfo itemNodeInfo = null;

            info = _sysFormMasterDA.GetRecord_First();

            rootNodeInfo = new TreeNodeInfo();
            rootNodeInfo.Text = info.fom_cFormDesc;
            rootNodeInfo.Name = info.fom_cFormNumber;
            rootNodeInfo.Tag = info.fom_cExePath;
            rootNodeInfo.Index = info.fom_iIndex;

            rootNodeInfo.FileName = info.fom_iRecordID.ToString();

            TreeNodeInfo nodeInfo = null;
            info = new Sys_FormMaster_fom_Info();
            info.fom_iParentID = int.Parse(rootNodeInfo.FileName.ToString());
            //foreach (var chile in _sysFormMasterDA.SearchRecords(info))
            //{
            //    info = chile as Sys_FormMaster_fom_Info;
            //    string Name = info.fom_iRecordID.ToString();
            //    nodeInfo = null;
            //    nodeInfo = LocalGeneralMenu.Instance.InsertTreeNode(rootNodeInfo, info.fom_cFormDesc, info.fom_cExePath, info.fom_cFormNumber, info.fom_iIndex, 2);
            //    info = new Sys_FormMaster_fom_Info();
            //    info.fom_iParentID = int.Parse(Name.ToString());
            //    foreach (var q in _sysFormMasterDA.SearchRecords(info, _usmInfo))
            //    {
            //        info = q as Sys_FormMaster_fom_Info;
            //        LocalGeneralMenu.Instance.InsertTreeNode(nodeInfo, info.fom_cFormDesc, info.fom_cExePath, info.fom_cFormNumber, info.fom_iIndex, 2);
            //    }
            //}

            /**/
            foreach (var chile in _sysFormMasterDA.SearchRecords(info))
            {
                Sys_FormMaster_fom_Info fom = new Sys_FormMaster_fom_Info();
                info = chile as Sys_FormMaster_fom_Info;
                fom.fom_iParentID = info.fom_iRecordID;
                if (_sysFormMasterDA.SearchRecords(fom, _usmInfo).Count > 0)
                {
                    string Name = info.fom_iRecordID.ToString();
                    nodeInfo = null;
                    nodeInfo = LocalGeneralMenu.Instance.InsertTreeNode(rootNodeInfo, info.fom_cFormDesc, info.fom_cExePath, info.fom_cFormNumber, info.fom_iIndex, info.fom_iImageIndex);

                    foreach (var q in _sysFormMasterDA.SearchRecords(fom, _usmInfo))
                    {
                        info = q as Sys_FormMaster_fom_Info;
                        
                        if (info.fom_cFormNumber.ToString().ToUpper() == "DEMO")
                        {
                            itemNodeInfo = LocalGeneralMenu.Instance.InsertTreeNode(nodeInfo, info.fom_cFormDesc, info.fom_cExePath, info.fom_cFormNumber, info.fom_iIndex, info.fom_iImageIndex);
                            itemNodeInfo.Remark = "EXE";
                            itemNodeInfo.FileName = "WPFUI\\WPFUI.exe";
                        }
                        else
                            LocalGeneralMenu.Instance.InsertTreeNode(nodeInfo, info.fom_cFormDesc, info.fom_cExePath, info.fom_cFormNumber, info.fom_iIndex, info.fom_iImageIndex);
                    }
                }
            }

            /**/

            treeNodeInfoList.Add(rootNodeInfo);
            treeNodeInfos = treeNodeInfoList.ToArray();
            return treeNodeInfos;
        }

        #endregion
    }
}
