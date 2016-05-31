using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowUI.ClassLibrary.Public;
using WeifenLuo.WinFormsUI.Docking;
using UtilityLibrary.WinControls;
using Model.General;
using Model.IModel;
using BLL.IBLL.Base;
using BLL.IBLL.SysMaster;
using BLL.Factory.Base;
using Model.SysMaster;
using BLL.Factory.SysMaster;
using WindowUI.Management.Master;

namespace WindowUI
{
    public partial class MenuToolForm : BaseForm
    {
        private Dictionary<string, BaseForm> _childrenForms;
        private OutlookBar _outlookBar;
        DockPanel _dockPanel;
        public  List<IModelObject> _formFunctionList;

        ILoginFormBL _loginFormBL;

        Sys_UserMaster_usm_Info _userInfo;

        public MenuToolForm(DockPanel dockPanel,List<IModelObject> formFunctionList,Sys_UserMaster_usm_Info userInfo)
        {
            InitializeComponent();
            this._loginFormBL = MasterBLLFactory.GetBLL<ILoginFormBL>(MasterBLLFactory.LoginForm);

            this._childrenForms = new Dictionary<string, BaseForm>();
            this._dockPanel = dockPanel;

            this._formFunctionList = formFunctionList;
            //
            this._userInfo = userInfo;
            //
            UserInformation = userInfo;

            InitializeOutlookbar(userInfo);

            this.Show(this._dockPanel, DockState.DockLeft);
        }

        private void InitializeOutlookbar(Sys_UserMaster_usm_Info userInfo)
        {
            this._outlookBar = new OutlookBar();
            
            TreeNodeInfo[] treeNodeInfos = null;
            _outlookBar.AnimationSpeed = 10;
            try
            {
                if (userInfo.usm_cUserLoginID.ToUpper() == "SA")
                    treeNodeInfos = SystemMenuBLLFactory.Instance.GetISystemMenuBLL().GetMenuTreeNodes();
                else
                    treeNodeInfos = SystemMenuBLLFactory.Instance.GetISystemMenuBLL().CheckUser(userInfo);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (treeNodeInfos != null && treeNodeInfos.Length > 0)
            {
                TreeNodeInfo rootNodeInfo = treeNodeInfos[0];

                if (rootNodeInfo == null || rootNodeInfo.TreeNodeInfos==null || rootNodeInfo.TreeNodeInfos.Length == 0)
                {
                    return;
                }

                OutlookBarBand outlookBarBand = null;
                OutlookBarItem outlookBarItem = null;
                TreeNodeInfo tNodeInfo = null;

                for (int i = 0; i < rootNodeInfo.TreeNodeInfos.Length; i++)
                {
                    tNodeInfo = rootNodeInfo.TreeNodeInfos[i];
                    outlookBarBand = new OutlookBarBand(tNodeInfo.Text);
                    outlookBarBand.SmallImageList = this.imgLW;
                    outlookBarBand.LargeImageList = this.imgLW;

                    if(tNodeInfo.TreeNodeInfos!=null && tNodeInfo.TreeNodeInfos.Length>0)
                    {
                        for (int j = 0; j < tNodeInfo.TreeNodeInfos.Length; j++)
                        {
                            outlookBarItem = new OutlookBarItem();
                            
                            outlookBarItem.Text = tNodeInfo.TreeNodeInfos[j].Text;
                            outlookBarItem.Tag = tNodeInfo.TreeNodeInfos[j];
                            
                            outlookBarItem.ImageIndex = tNodeInfo.TreeNodeInfos[j].ImageIndex;
                            
                            #region MyRegion
                            //switch (tNodeInfo.TreeNodeInfos[j].Text) 
                            //{
                            //    case "缺勤登记": 
                            //        outlookBarItem.ImageIndex = 3;
                            //        break;
                            //    case "学校调课设置": 
                            //        outlookBarItem.ImageIndex = 4;
                            //        break;
                            //    case "班级调课":
                            //        outlookBarItem.ImageIndex = 5;
                            //        break;
                            //    case "教职人员考勤查询":
                            //        outlookBarItem.ImageIndex = 6;
                            //        break;
                            //    case "学生课程出席情况查询":
                            //        outlookBarItem.ImageIndex = 7;
                            //        break;
                            //    case "教职人员考勤个人查询":
                            //        outlookBarItem.ImageIndex = 8;
                            //        break;
                            //    case "学生活动情况查询":
                            //        outlookBarItem.ImageIndex = 9;
                            //        break;
                            //    case "发卡管理":
                            //        outlookBarItem.ImageIndex = 10;
                            //        break;
                            //    case "用户卡管理":
                            //        outlookBarItem.ImageIndex = 11;
                            //        break;
                            //    case "卡管理主档":
                            //        outlookBarItem.ImageIndex = 12;
                            //        break;
                            //    case "考勤时段设置":
                            //        outlookBarItem.ImageIndex = 13;
                            //        break;
                            //    case "上课时段设置":
                            //        outlookBarItem.ImageIndex = 14;
                            //        break;
                            //    case "班级课程及考勤设置":
                            //        outlookBarItem.ImageIndex = 15;
                            //        break;
                            //    case "节假日设置":
                            //        outlookBarItem.ImageIndex = 16;
                            //        break;
                            //    case "缺勤类型设置":
                            //        outlookBarItem.ImageIndex = 17;
                            //        break;
                            //    case "院系部主档":
                            //        outlookBarItem.ImageIndex = 18;
                            //        break;
                            //    case "科室主档":
                            //        outlookBarItem.ImageIndex = 19;
                            //        break;
                            //    case "课程主档":
                            //        outlookBarItem.ImageIndex = 20;
                            //        break;
                            //    case "专业主档":
                            //        outlookBarItem.ImageIndex = 21;
                            //        break;
                            //    case "卡用户管理主档":
                            //        outlookBarItem.ImageIndex = 22;
                            //        break;
                            //    case "建筑物主档":
                            //        outlookBarItem.ImageIndex = 23;
                            //        break;
                            //    case "地点主档":
                            //        outlookBarItem.ImageIndex = 24;
                            //        break;
                            //    case "考勤机管理":
                            //        outlookBarItem.ImageIndex = 25;
                            //        break;
                            //    case "2.4G读写器管理":
                            //        outlookBarItem.ImageIndex = 26;
                            //        break;
                            //    case "2.4G读写器监控设置":
                            //        outlookBarItem.ImageIndex = 27;
                            //        break;
                            //    case "用户主档":
                            //        outlookBarItem.ImageIndex = 28;
                            //        break;
                            //    case "角色主档":
                            //        outlookBarItem.ImageIndex = 29;
                            //        break;
                            //    case "权限设定":
                            //        outlookBarItem.ImageIndex = 30;
                            //        break;
                            //    case "表单主档":
                            //        outlookBarItem.ImageIndex = 31;
                            //        break;
                            //    case "功能主档":
                            //        outlookBarItem.ImageIndex = 32;
                            //        break;
                            //    case "字码主档":
                            //        outlookBarItem.ImageIndex = 33;
                            //        break;
                            //    case "教职工考勤签卡":
                            //        outlookBarItem.ImageIndex = 34;
                            //        break;
                            //    default: 
                            //        outlookBarItem.ImageIndex = tNodeInfo.TreeNodeInfos[j].ImageIndex;
                            //        break;
                            //} 
                            #endregion
                            
                            outlookBarBand.Items.Add(outlookBarItem);

                        }
                    }

                    //outlookBarBand.Background = SystemColors.AppWorkspace;
                    //outlookBarBand.Background = System.Drawing.Color.FromArgb(191,191,191);
                    //BuildingMaster win = new BuildingMaster();
                    //skin.GetColor("BackColor");
                    //outlookBarBand.Background = win.BackColor;
                   
                    outlookBarBand.Background = this.BackColor;
                    outlookBarBand.TextColor = this.ForeColor;
                    //win.Dispose();
                    //win = null;

                    
                    this._outlookBar.Bands.Add(outlookBarBand);
                    
                }
            }

            
            this._outlookBar.Dock = DockStyle.Fill;
            this._outlookBar.SetCurrentBand(0);
            this._outlookBar.ItemClicked += new OutlookBarItemClickedHandler(outlookBar_ItemClicked);
            this.pnlContainer.Controls.AddRange(new Control[] { this._outlookBar });
        }

        private BaseForm GetFormIn(string formName)
        {
            BaseForm frm = null;
            Type type = Type.GetType(formName, false);
            if (type != null)
            {
                
                frm = Activator.CreateInstance(type) as BaseForm;
                
            }

            return frm;
        }

        void MenuToolForm_ItemClicked(string menuItemTag)
        {
            BaseForm frm = null;

            string[] formNames = null;
            string formName = string.Empty;

            formNames = menuItemTag.Split('.');

            if (formNames.Length > 0)
            {
                formName = formNames[formNames.Length - 1];
            }

            IDockContent[] idcs = this._dockPanel.Contents.ToArray();
            string itemFormName = string.Empty;
            bool isExistForm = false;

            foreach (IDockContent idc in idcs)
            {
                DockContentHandler dch = idc.DockHandler;
                itemFormName = dch.Form.Name;
                if (itemFormName == formName)
                {
                    isExistForm = true;
                    frm = dch.Form as BaseForm;
                    break;
                }
            }

            if (!isExistForm)
            {
                frm = GetFormIn(menuItemTag);
            }

            if (frm != null)
            {
                Sys_UserMaster_usm_Info usmInfo = new Sys_UserMaster_usm_Info();
                usmInfo = _userInfo;
                Sys_FormMaster_fom_Info fomInfo = new Sys_FormMaster_fom_Info();
                List<Sys_UserMaster_usm_Info> usmInfoList = new List<Sys_UserMaster_usm_Info>();
                fomInfo.fom_cFormNumber = GetFormIn(menuItemTag).Name.ToString();
                usmInfo.formMasterList.Clear();
                usmInfo.formMasterList.Add(fomInfo);
                foreach (Sys_UserMaster_usm_Info usm in _loginFormBL.SearchRecords(usmInfo))
                {
                    usmInfoList.Add(usm);
                }
                frm._setFunctionList = usmInfoList[0].functionMasterList;
                frm.UserInformation = this.UserInformation;
                frm.Show(this._dockPanel);
            }

        }

        void RuningEXE(string fileName, string workingDirectory)
        {
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            info.FileName = fileName;
            info.Arguments = "text.txt";
            info.WorkingDirectory = workingDirectory;
            System.Diagnostics.Process pro;
            try
            {
                pro = System.Diagnostics.Process.Start(info);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show("系统找不到指定的文件。\r{0}");
                return;
            }
        }

        void outlookBar_ItemClicked(OutlookBarBand band, OutlookBarItem item)
        {
            if (item.Tag != null)
            {
                TreeNodeInfo tNodeInfo = null;

                tNodeInfo = item.Tag as TreeNodeInfo;
                if (tNodeInfo != null)
                {
                    if (tNodeInfo.Remark == "EXE")
                    {
                        RuningEXE(tNodeInfo.FileName, tNodeInfo.WorkingDirectory);
                    }
                    else
                    {
                        MenuToolForm_ItemClicked(tNodeInfo.Tag);
                    }
                }
            }

        }

    }
}
