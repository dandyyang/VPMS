using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.Master;
using WindowUI.ClassLibrary.Public;
using Model.General;
using Common;
using Common.DataTypeVerify;
using Model.SysMaster;
using BLL.IBLL.SysMaster;
using BLL.Factory.SysMaster;

namespace WindowUI.SysMaster
{
    public partial class SysPurviewMaster : BaseForm
    {
        ISysFormMasterBL _sysFormMasterBL;
        ISysFunctionMasterBL _sysFunctionMasterBL;
        IUserPurviewBL _userPurviewBL;
        string iRecordID = string.Empty;

        DefineConstantValue.EditStateEnum _type;
        string _num = string.Empty;
        string _lvwUserType = string.Empty;

        public bool _isByCall = false;
        string _formNumber = string.Empty;
        Sys_UserPurview_usp_Info uspInfoTemp = null;
        Sys_UserMaster_usm_Info usmTemp = null;
        Sys_RoleMaster_rlm_Info rlmTemp = null;
        Sys_UserPurview_usp_Info uspInfo = null;

        /// <summary>
        /// 对象类型
        /// </summary>
        enum UserType
        {
            /// <summary>
            /// 用户
            /// </summary>
            User,
            /// <summary>
            /// 角色
            /// </summary>
            Role,
            /// <summary>
            /// 
            /// </summary>
            Type
        }

        public SysPurviewMaster()
        {
            InitializeComponent();
            this._sysFormMasterBL = MasterBLLFactory.GetBLL<ISysFormMasterBL>(MasterBLLFactory.SysFormMaster);
            this._sysFunctionMasterBL = MasterBLLFactory.GetBLL<ISysFunctionMasterBL>(MasterBLLFactory.SysFunctionMaster);
            this._userPurviewBL = MasterBLLFactory.GetBLL<IUserPurviewBL>(MasterBLLFactory.UserPurview);
        }

        private void SysPurviewMaster_Load(object sender, EventArgs e)
        {
            if (!_isByCall)
                SetPurview(this.userToolBar1);
            else
                this.userToolBar1.BtnModifyVisible = true;
            BindTreeView();
            //BindFunctionMaster();
            SetOpenToolBar();
            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
        }

        private void ShowInfo(Sys_UserPurview_usp_Info info)
        {
            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            try
            {
                if (info != null)
                {
                    iRecordID = info.usp_iRecordID.ToString();
                    lvwUser.Items.Clear();
                    if (info.roleMasterList.Count != 0)
                    {
                        foreach (Sys_RoleMaster_rlm_Info role in info.roleMasterList)
                        {
                            ListViewItem list = new ListViewItem();
                            ListViewItem it = new ListViewItem(role.rlm_cRoleID.ToString());
                            it.SubItems.Add(role.rlm_cRoleDesc.ToString());
                            it.SubItems.Add(UserType.Role.ToString());
                            lvwUser.Items.Add(it);
                        }
                    }
                    if (info.userMasterList.Count != 0)
                    {
                        foreach (Sys_UserMaster_usm_Info user in info.userMasterList)
                        {
                            ListViewItem list = new ListViewItem();
                            ListViewItem it = new ListViewItem(user.usm_cUserLoginID.ToString());
                            it.SubItems.Add(user.usm_cChaName.ToString());
                            it.SubItems.Add(UserType.User.ToString());
                            lvwUser.Items.Add(it);
                        }
                    }

                    lvwPur.Items.Clear();
                    for (int i = 0; i < lvwPur.Items.Count; i++)
                    {
                        lvwPur.Items[i].Checked = false;
                    }
                    if (info.functionMasterList.Count != 0)
                    {
                        List<Sys_FunctionMaster_fum_Info> infoList = new List<Sys_FunctionMaster_fum_Info>();
                        foreach (Sys_FunctionMaster_fum_Info function in info.functionMasterList)
                        {
                            infoList.Add(function);
                        }
                        lvwPur.SetDataSource<Sys_FunctionMaster_fum_Info>(infoList);
                    }
                    txtcAdd.Text = info.usp_cAdd.ToString();
                    txtcLast.Text = info.usp_cLast.ToString();
                    txtdAddDate.Text = info.usp_dAddDate != null ? info.usp_dAddDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";
                    txtdLastDate.Text = info.usp_dLastDate != null ? info.usp_dLastDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";
                }
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }

        private void SetTxtBox(DefineConstantValue.EditStateEnum type)
        {
            switch (type)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    btnDel.Enabled = false;
                    btnNRole.Enabled = false;
                    btnNUser.Enabled = false;
                    tvwMain.Enabled = true;
                    lvwPur.Enabled = false;
                    //lvwUser.Enabled = false;
                    _type = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    btnDel.Enabled = true;
                    btnNRole.Enabled = true;
                    btnNUser.Enabled = true;
                    tvwMain.Enabled = false;
                    lvwPur.Enabled = true;
                    //lvwUser.Enabled = true;
                    _type = DefineConstantValue.EditStateEnum.OE_Insert;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    btnDel.Enabled = true;
                    btnNRole.Enabled = true;
                    btnNUser.Enabled = true;
                    tvwMain.Enabled = false;
                    lvwPur.Enabled = true;
                    //lvwUser.Enabled = true;
                    _type = DefineConstantValue.EditStateEnum.OE_Update;
                    break;
            }
        }

        private void SetOpenToolBar()
        {
            userToolBar1.BtnSaveEnabled = false;
            userToolBar1.BtnCancelEnabled = false;
            userToolBar1.BtnModifyEnabled = true;
        }

        private void BindTreeView()
        {
            Sys_FormMaster_fom_Info info = new Sys_FormMaster_fom_Info();
            try
            {
                //增加根结点
                this.tvwMain.Nodes.Clear();

                //foreach (var t in _sysFormMasterBL.SearchRecords(info))
                //{
                info = _sysFormMasterBL.GetRecord_First();
                TreeNode root = new TreeNode(info.fom_cFormDesc);
                root.Name = info.fom_iRecordID.ToString();
                root.Tag = info.fom_cFormNumber.ToString();
                this.tvwMain.Nodes.Add(root);


                //TreeNode root2 = new TreeNode();
                //root2.Text = "tag2";
                //root2.Name = "tag2";
                //root2.Tag ="tag2";
                //this.tvwMain.Nodes.Add(root2);


                //info.fom_iParentID = info.fom_iRecordID;
                info = new Sys_FormMaster_fom_Info();
                info.fom_iParentID = int.Parse(root.Name.ToString());
                //增加子结点
                foreach (var chile in _sysFormMasterBL.SearchRecords(info))
                {

                    info = chile as Sys_FormMaster_fom_Info;
                    if (info.fom_cFormDesc != "开发人员设置")
                    {
                        TreeNode node = new TreeNode(info.fom_cFormDesc);
                        node.Name = info.fom_iRecordID.ToString();
                        node.Tag = info.fom_cFormNumber.ToString();
                        root.Nodes.Add(node);



                        //info.fom_iParentID = info.fom_iRecordID;
                        info = new Sys_FormMaster_fom_Info();
                        info.fom_iParentID = int.Parse(node.Name.ToString());
                        //增加孙结点
                        foreach (var q in _sysFormMasterBL.SearchRecords(info))
                        {
                            info = q as Sys_FormMaster_fom_Info;
                            TreeNode nd = new TreeNode(info.fom_cFormDesc);
                            nd.Name = info.fom_iRecordID.ToString();
                            nd.Tag = info.fom_cFormNumber.ToString();
                            node.Nodes.Add(nd);
                        }
                    }
                }


                //读取Web权限
                Sys_FormMaster_fom_Info webinfo = new Sys_FormMaster_fom_Info();
                try
                {
                    webinfo = _sysFormMasterBL.GetWebTreeRoot();
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                    return;
                }

                //////
                TreeNode webRoot = new TreeNode();
                webRoot.Text = webinfo.fom_cFormDesc;
                webRoot.Name = webinfo.fom_iRecordID.ToString();
                webRoot.Tag = webinfo.fom_cFormNumber.ToString();


                List<Sys_FormMaster_fom_Info> webList = new List<Sys_FormMaster_fom_Info>();
                try
                {
                    webList = _sysFormMasterBL.GetWebTreeNode(webinfo);
                    if (webList.Count > 0)
                    {
                        foreach (Sys_FormMaster_fom_Info t in webList)
                        {

                            TreeNode tempNode = new TreeNode();
                            tempNode.Text = t.fom_cFormDesc.ToString();
                            tempNode.Name = t.fom_iRecordID.ToString();
                            tempNode.Tag = t.fom_cFormNumber.ToString();

                            List<Sys_FormMaster_fom_Info> tempNodeList = new List<Sys_FormMaster_fom_Info>();
                            tempNodeList = _sysFormMasterBL.GetWebTreeNode(t);
                            if (tempNodeList.Count > 0)
                            {
                                foreach (Sys_FormMaster_fom_Info tt in tempNodeList)
                                {
                                    TreeNode tempNode2 = new TreeNode();
                                    tempNode2.Text = tt.fom_cFormDesc.ToString();
                                    tempNode2.Name = tt.fom_iRecordID.ToString();
                                    tempNode2.Tag = tt.fom_cFormNumber.ToString();

                                    tempNode.Nodes.Add(tempNode2);
                                }
                            }

                            webRoot.Nodes.Add(tempNode);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                    return;
                }
                this.tvwMain.Nodes.Add(webRoot);



                //tvwMain.SelectedNode = tvwMain.Nodes[0];
                //tvwMain.SelectedNode.ExpandAll();
                tvwMain.ExpandAll();
                //}
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }

        //private void BindFunctionMaster()
        //{
        //    Sys_FunctionMaster_fum_Info info = new Sys_FunctionMaster_fum_Info();
        //    List<Model.IModel.IModelObject> list = _sysFunctionMasterBL.SearchRecords(info);
        //    lvwPur.Items.Clear();
        //    List<Sys_FunctionMaster_fum_Info> infoList = new List<Sys_FunctionMaster_fum_Info>();
        //    try
        //    {
        //        foreach (var t in list)
        //        {
        //            info = t as Sys_FunctionMaster_fum_Info;
        //            infoList.Add(info);
        //        }
        //        lvwPur.SetDataSource<Sys_FunctionMaster_fum_Info>(infoList);
        //    }
        //    catch (Exception Ex)
        //    { ShowErrorMessage(Ex); }
        //}

        private void userToolBar1_BtnModifyClick(object sender, EventArgs e)
        {
            SetTxtBox(DefineConstantValue.EditStateEnum.OE_Update);
            userToolBar1.BtnCancelEnabled = true;
            userToolBar1.BtnSaveEnabled = true;
            userToolBar1.BtnModifyEnabled = false;
            lvwPur.Enabled = true;
            lvwUser.Enabled = true;
            tvwMain.Enabled = false;
        }

        private void userToolBar1_BtnCancelClick(object sender, EventArgs e)
        {
            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            SetOpenToolBar();

            uspInfoTemp = uspInfo;
        }

        private void userToolBar1_BtnSaveClick(object sender, EventArgs e)
        {
            Change();   

            Sys_UserPurview_usp_Info usp = new Sys_UserPurview_usp_Info();
            usp = uspInfoTemp;


            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();

            try
            {

                usp.usp_cAdd = UserInformation.usm_cUserLoginID;
                usp.usp_dAddDate = DateTime.Now;
                usp.usp_cLast = UserInformation.usm_cUserLoginID;
                usp.usp_dLastDate = DateTime.Now;

                msg = _userPurviewBL.Save(usp, DefineConstantValue.EditStateEnum.OE_Insert);

            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            SetOpenToolBar();
        }

        private bool IsExistsItem(string text)
        {
            for (int i = 0; i < lvwUser.Items.Count; i++)
            {

                if (lvwUser.Items[i].SubItems[0].Text == text)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnNUser_Click(object sender, EventArgs e)
        {
            SysUserMasterSearch win = new SysUserMasterSearch();
            win.ShowDialog();

            List<Sys_UserMaster_usm_Info> items = win._RtvInfo;
            if (items != null)
            {
                try
                {

                    List<Sys_UserMaster_usm_Info> cumList = new List<Sys_UserMaster_usm_Info>();

                    foreach (Sys_UserMaster_usm_Info user in items)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = user.usm_cUserLoginID.ToString();
                        if (!IsExistsItem(list.Text))
                        {
                            ListViewItem it = new ListViewItem(user.usm_cUserLoginID.ToString());
                            it.SubItems.Add(user.usm_cChaName.ToString());
                            it.SubItems.Add(UserType.User.ToString());
                            lvwUser.Items.Add(it);
                            uspInfoTemp.userMasterList.Add(user);
                        }
                    }

                }
                catch (Exception Ex)
                { ShowErrorMessage(Ex); }
            }

            win.Dispose();
            win = null;
        }

        private void btnNRole_Click(object sender, EventArgs e)
        {
            SysRoleMasterSearch win = new SysRoleMasterSearch();
            win.ShowDialog();

            List<Sys_RoleMaster_rlm_Info> items = win._RtvInfo;
            if (items != null)
            {
                try
                {

                    List<Sys_RoleMaster_rlm_Info> cumList = new List<Sys_RoleMaster_rlm_Info>();

                    foreach (Sys_RoleMaster_rlm_Info role in items)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = role.rlm_cRoleID.ToString();
                        if (!IsExistsItem(list.Text))
                        {
                            ListViewItem it = new ListViewItem(role.rlm_cRoleID.ToString());
                            it.SubItems.Add(role.rlm_cRoleDesc.ToString());
                            it.SubItems.Add(UserType.Role.ToString());
                            lvwUser.Items.Add(it);
                            uspInfoTemp.roleMasterList.Add(role);
                        }
                    }

                }
                catch (Exception Ex)
                { ShowErrorMessage(Ex); }
            }


            win.Dispose();
            win = null;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwUser.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("您確認要删除吗？", "系統信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (lvwUser.SelectedItems[0].SubItems[2].Text == UserType.Role.ToString())
                        {
                            for (int i = 0; i < uspInfoTemp.roleMasterList.Count; i++)
                            {
                                if (uspInfoTemp.roleMasterList[i].rlm_cRoleID == lvwUser.SelectedItems[0].SubItems[0].Text)
                                    uspInfoTemp.roleMasterList.Remove(uspInfoTemp.roleMasterList[i]);
                                i = 0;
                            }
                        }
                        if (lvwUser.SelectedItems[0].SubItems[2].Text == UserType.User.ToString())
                        {
                            for (int i = 0; i < uspInfoTemp.userMasterList.Count; i++)
                            {
                                if (uspInfoTemp.userMasterList[i].usm_cUserLoginID == lvwUser.SelectedItems[0].SubItems[0].Text)
                                    uspInfoTemp.userMasterList.Remove(uspInfoTemp.userMasterList[i]);
                                i = 0;
                            }
                        }

                        lvwUser.Items.Remove(lvwUser.SelectedItems[0]);
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        void ClearColor(TreeNode node)
        {
            node.ForeColor = Color.Black;
            foreach (TreeNode var in node.Nodes)
            {
                var.ForeColor = Color.Black;
                ClearColor(var);
            }
        }

        private void tvwMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //for (int i = 0; i < tvwMain.Nodes.Count; i++)
            //{
            //    ClearColor(tvwMain.Nodes[i]);
            //}
            //tvwMain.SelectedNode.ForeColor = Color.Red;
            //try
            //{
            //    //tvwMain.SelectedNode.Text = "*" + tvwMain.SelectedNode.Text;
            //    Sys_FormMaster_fom_Info info = new Sys_FormMaster_fom_Info();
            //    uspInfo = new Sys_UserPurview_usp_Info();
            //    info.fom_cFormNumber = tvwMain.SelectedNode.Tag.ToString();
            //    _formNumber = tvwMain.SelectedNode.Tag.ToString();
            //    uspInfo.formMasterList.Add(info);
            //    Model.IModel.IModelObject result = _userPurviewBL.DisplayRecord(uspInfo);
            //    uspInfo = result as Sys_UserPurview_usp_Info;

            //    uspInfoTemp = new Sys_UserPurview_usp_Info();
            //    uspInfoTemp = uspInfo;

            //    ShowInfo(uspInfo);
            //    _lvwUserType = string.Empty;
            //    _num = string.Empty;
            //}
            //catch (Exception Ex)
            //{
            //    ShowErrorMessage(Ex);
            //}
        }

        private void lvwUser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //try
            //{
            //    _num = lvwUser.SelectedItems[0].SubItems[0].Text;
            //    _lvwUserType = lvwUser.SelectedItems[0].SubItems[2].Text;

            //    for (int j = 0; j < lvwPur.Items.Count; j++)
            //    {
            //        lvwPur.Items[j].Checked = false;
            //    }

            //    for (int i = 0; i < uspInfoTemp.userMasterList.Count; i++)
            //    {
            //        if (uspInfoTemp.userMasterList[i].usm_cUserLoginID == _num)
            //        {
            //            foreach (Sys_FunctionMaster_fum_Info item in uspInfoTemp.userMasterList[i].functionMasterList)
            //            {
            //                for (int j = 0; j < lvwPur.Items.Count; j++)
            //                {
            //                    if (item.fum_cFunctionNumber == lvwPur.Items[j].SubItems[0].Text)
            //                    {
            //                        lvwPur.Items[j].Checked = true;
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    for (int i = 0; i < uspInfoTemp.roleMasterList.Count; i++)
            //    {
            //        if (uspInfoTemp.roleMasterList[i].rlm_cRoleID == _num)
            //        {
            //            foreach (Sys_FunctionMaster_fum_Info item in uspInfoTemp.roleMasterList[i].functionMasterList)
            //            {
            //                for (int j = 0; j < lvwPur.Items.Count; j++)
            //                {
            //                    if (item.fum_cFunctionNumber == lvwPur.Items[j].SubItems[0].Text)
            //                    {
            //                        lvwPur.Items[j].Checked = true;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    ShowErrorMessage(Ex);
            //}
            lvwClick();
        }

        private void lvwUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            Change();   
        }

        void Change()
        {
            Sys_FunctionMaster_fum_Info fumInfo = new Sys_FunctionMaster_fum_Info();
            usmTemp = new Sys_UserMaster_usm_Info();
            rlmTemp = new Sys_RoleMaster_rlm_Info();

            try
            {
                if (_lvwUserType == UserType.User.ToString())
                {
                    usmTemp.usm_cUserLoginID = _num;
                    usmTemp.functionMasterList.Clear();
                    for (int i = 0; i < lvwPur.Items.Count; i++)
                    {
                        if (lvwPur.Items[i].Checked == true)
                        {
                            fumInfo = new Sys_FunctionMaster_fum_Info();
                            fumInfo.fum_cFunctionNumber = lvwPur.Items[i].SubItems[0].Text;
                            usmTemp.functionMasterList.Add(fumInfo);
                        }
                    }
                    for (int i = 0; i < uspInfoTemp.userMasterList.Count; i++)
                    {
                        if (uspInfoTemp.userMasterList[i].usm_cUserLoginID == _num)
                        {
                            uspInfoTemp.userMasterList[i] = usmTemp;
                        }
                    }
                }
                else
                {
                    rlmTemp.rlm_cRoleID = _num;
                    rlmTemp.functionMasterList.Clear();
                    for (int i = 0; i < lvwPur.Items.Count; i++)
                    {
                        if (lvwPur.Items[i].Checked == true)
                        {
                            fumInfo = new Sys_FunctionMaster_fum_Info();
                            fumInfo.fum_cFunctionNumber = lvwPur.Items[i].SubItems[0].Text;
                            rlmTemp.functionMasterList.Add(fumInfo);
                        }
                    }
                    for (int i = 0; i < uspInfoTemp.roleMasterList.Count; i++)
                    {
                        if (uspInfoTemp.roleMasterList[i].rlm_cRoleID == _num)
                        {
                            uspInfoTemp.roleMasterList[i] = rlmTemp;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void tvwMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            for (int i = 0; i < tvwMain.Nodes.Count; i++)
            {
                ClearColor(tvwMain.Nodes[i]);
            }
            tvwMain.SelectedNode.ForeColor = Color.Red;
            try
            {
                //tvwMain.SelectedNode.Text = "*" + tvwMain.SelectedNode.Text;
                Sys_FormMaster_fom_Info info = new Sys_FormMaster_fom_Info();
                uspInfo = new Sys_UserPurview_usp_Info();
                info.fom_cFormNumber = tvwMain.SelectedNode.Tag.ToString();
                _formNumber = tvwMain.SelectedNode.Tag.ToString();
                uspInfo.formMasterList.Add(info);
                Model.IModel.IModelObject result = _userPurviewBL.DisplayRecord(uspInfo);
                uspInfo = result as Sys_UserPurview_usp_Info;

                uspInfoTemp = new Sys_UserPurview_usp_Info();
                uspInfoTemp = uspInfo;

                ShowInfo(uspInfo);
                _lvwUserType = string.Empty;
                _num = string.Empty;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        void lvwClick()
        {
            try
            {
                _num = lvwUser.SelectedItems[0].SubItems[0].Text;
                _lvwUserType = lvwUser.SelectedItems[0].SubItems[2].Text;

                for (int j = 0; j < lvwPur.Items.Count; j++)
                {
                    lvwPur.Items[j].Checked = false;
                }

                for (int i = 0; i < uspInfoTemp.userMasterList.Count; i++)
                {
                    if (uspInfoTemp.userMasterList[i].usm_cUserLoginID == _num)
                    {
                        foreach (Sys_FunctionMaster_fum_Info item in uspInfoTemp.userMasterList[i].functionMasterList)
                        {
                            for (int j = 0; j < lvwPur.Items.Count; j++)
                            {
                                if (item.fum_cFunctionNumber == lvwPur.Items[j].SubItems[0].Text)
                                {
                                    lvwPur.Items[j].Checked = true;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < uspInfoTemp.roleMasterList.Count; i++)
                {
                    if (uspInfoTemp.roleMasterList[i].rlm_cRoleID == _num)
                    {
                        foreach (Sys_FunctionMaster_fum_Info item in uspInfoTemp.roleMasterList[i].functionMasterList)
                        {
                            for (int j = 0; j < lvwPur.Items.Count; j++)
                            {
                                if (item.fum_cFunctionNumber == lvwPur.Items[j].SubItems[0].Text)
                                {
                                    lvwPur.Items[j].Checked = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void lvwUser_MouseClick(object sender, MouseEventArgs e)
        {
        //    try
        //    {
        //        _num = lvwUser.SelectedItems[0].SubItems[0].Text;
        //        _lvwUserType = lvwUser.SelectedItems[0].SubItems[2].Text;

        //        for (int j = 0; j < lvwPur.Items.Count; j++)
        //        {
        //            lvwPur.Items[j].Checked = false;
        //        }

        //        for (int i = 0; i < uspInfoTemp.userMasterList.Count; i++)
        //        {
        //            if (uspInfoTemp.userMasterList[i].usm_cUserLoginID == _num)
        //            {
        //                foreach (Sys_FunctionMaster_fum_Info item in uspInfoTemp.userMasterList[i].functionMasterList)
        //                {
        //                    for (int j = 0; j < lvwPur.Items.Count; j++)
        //                    {
        //                        if (item.fum_cFunctionNumber == lvwPur.Items[j].SubItems[0].Text)
        //                        {
        //                            lvwPur.Items[j].Checked = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        for (int i = 0; i < uspInfoTemp.roleMasterList.Count; i++)
        //        {
        //            if (uspInfoTemp.roleMasterList[i].rlm_cRoleID == _num)
        //            {
        //                foreach (Sys_FunctionMaster_fum_Info item in uspInfoTemp.roleMasterList[i].functionMasterList)
        //                {
        //                    for (int j = 0; j < lvwPur.Items.Count; j++)
        //                    {
        //                        if (item.fum_cFunctionNumber == lvwPur.Items[j].SubItems[0].Text)
        //                        {
        //                            lvwPur.Items[j].Checked = true;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        ShowErrorMessage(Ex);
        //    }
            lvwClick();
        }

    }
}
