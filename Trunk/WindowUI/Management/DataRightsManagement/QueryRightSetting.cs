using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.IBLL.Management.DataRightsManagement;
using BLL.Factory.Management;
using Model.SysMaster;
using Model.Management.DataRightsManagement;
using Model.General;

namespace WindowUI.Management.DataRightsManagement
{
    public partial class QueryRightSetting : BaseForm
    {
        IDataRightsRoleBL _dataRightsRoleBL;
        List<Sys_UserMaster_usm_Info> _usmList;
        List<DataRightsRole_drr_Info> _drrList;

        public QueryRightSetting()
        {
            InitializeComponent();
            
            _usmList = null;
            _drrList = null;

            this.btnAddUser.Enabled = false;

            this._dataRightsRoleBL = MasterBLLFactory.GetBLL<IDataRightsRoleBL>(MasterBLLFactory.DataRightsRole);
        }

        private void QueryRightSetting_Load(object sender, EventArgs e)
        {
            ResetSources();
        }

        private void ResetSources()
        {
            try
            {
                _usmList = _dataRightsRoleBL.GetAllSysUserList();
                _drrList = _dataRightsRoleBL.GetAllRoleList();

                FullRoleList(lvwRole);
                groupBox3.Text = "系统用户列表";

                FullUserList(lvwUserList);

            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        void LoadRoleUserList()
        {
            this.lvwUser.Items.Clear();
            if (lvwRole.SelectedItems.Count > 0)
            {
                string roleNum = lvwRole.SelectedItems[0].Name;
                List<Sys_UserMaster_usm_Info> roleUsers = null;

                try
                {
                    roleUsers = this._dataRightsRoleBL.GetRoleUsers(roleNum);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                if (roleUsers != null && roleUsers.Count > 0)
                {
                    foreach (Sys_UserMaster_usm_Info user in roleUsers)
                    {
                        ListViewItem li = new ListViewItem();
                        li.SubItems[0].Text = user.usm_cChaName;
                        li.SubItems[0].Name = user.usm_cUserLoginID;
                        li.ImageIndex = 0;
                        lvwUser.Items.Add(li);
                    }
                }
            }
        }

        void LoadUserRoleList()
        {
            this.lvwUserRoleList.Items.Clear();
            if (lvwUserList.SelectedItems.Count > 0)
            {
                string userLoginID = lvwUserList.SelectedItems[0].Name;
                List<DataRightsRole_drr_Info> userRoles = null;

                try
                {
                    userRoles = this._dataRightsRoleBL.GetUserRoles(userLoginID);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }

                if (userRoles != null && userRoles.Count > 0)
                {
                    foreach (DataRightsRole_drr_Info role in userRoles)
                    {
                        ListViewItem li = new ListViewItem();
                        li.SubItems[0].Text = role.drr_cName;
                        li.SubItems[0].Name = role.drr_cNumber;
                        li.ImageIndex = 0;
                        this.lvwUserRoleList.Items.Add(li);
                    }
                }
            }
        }

        private void FullUserList(ListView lvw)
        {
            lvw.Items.Clear();
            foreach (Sys_UserMaster_usm_Info item in _usmList)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = item.usm_cChaName;
                li.SubItems[0].Name = item.usm_cUserLoginID;
                li.ImageIndex = 0;
                lvw.Items.Add(li);

            }
        }

        private void FullRoleList(ListView lvw)
        {
            lvw.Items.Clear();
            foreach (DataRightsRole_drr_Info item in _drrList)
            {
                ListViewItem li = new ListViewItem();

                li.SubItems[0].Text = item.drr_cName;
                li.SubItems[0].Name = item.drr_cNumber;
                li.ImageIndex = 0;

                li.ToolTipText = item.drr_cRemark;
                lvw.Items.Add(li);
            }
        }

        private void lvwRole_Click(object sender, EventArgs e)
        {
            string roleTitle = string.Empty;

            if (this.lvwRole.SelectedItems.Count > 0)
            {
                this.btnAddUser.Enabled = true;
                this.btnDeleteUser.Enabled = true;

                roleTitle = "[" + lvwRole.SelectedItems[0].Text + "] 用户列表";
            }
            else
            {
                this.btnAddUser.Enabled = false;
                this.btnDeleteUser.Enabled = false;

                roleTitle = "用户列表";
            }

            try
            {
                LoadRoleUserList();
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            groupBox3.Text = roleTitle;

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            if (this.lvwRole.SelectedItems.Count > 0)
            {
                List<Sys_UserMaster_usm_Info> roleUsers = null;
                string roleNum = lvwRole.SelectedItems[0].Name;

                try
                {
                    roleUsers = this._dataRightsRoleBL.GetRoleUsers(roleNum);
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                    return;
                }

                QueryRightSettingUsers frm = new QueryRightSettingUsers(lvwRole.SelectedItems[0].Name, roleUsers);

                frm.ShowDialog();

                LoadRoleUserList();
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (this.lvwRole.SelectedItems.Count == 0)
            {
                ShowWarningMessage("请选择相应的角色！");
                return;
            }

            if (this.lvwUser.Items != null && this.lvwUser.Items.Count > 0)
            {
                DataRightsRole_drr_Info roleUserInfo = new DataRightsRole_drr_Info();

                roleUserInfo.drr_cNumber = this.lvwRole.SelectedItems[0].Name;

                if (this.lvwUser.CheckedItems.Count > 0)
                {
                    if (!ShowQuestionMessage(this.SystemMessageText.strMessageText_Q_Delete))
                    {
                        return;
                    }

                    roleUserInfo.RoleUserList = new List<Sys_UserMaster_usm_Info>();
                    for (int i = 0; i < this.lvwUser.CheckedItems.Count; i++)
                    {
                        Sys_UserMaster_usm_Info userInfo = new Sys_UserMaster_usm_Info();

                        userInfo.usm_cUserLoginID = this.lvwUser.CheckedItems[i].Name;
                        roleUserInfo.RoleUserList.Add(userInfo);
                    }

                    ReturnValueInfo returnInfo =null;
                    try
                    {
                        returnInfo = this._dataRightsRoleBL.DeleteRoleUser(roleUserInfo);
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }

                    if (returnInfo != null)
                    {
                        if (!returnInfo.boolValue)
                        {
                            ShowWarningMessage(returnInfo.messageText);
                        }
                        else
                        {
                            LoadRoleUserList();
                        }
                    }
                }
            }
        }

        private void lvwUserList_Click(object sender, EventArgs e)
        {
            string userTitle = string.Empty;

            if (this.lvwUserList.SelectedItems.Count > 0)
            {
                this.btnAddRole.Enabled = true;
                this.btnDeleteRole.Enabled = true;

                userTitle = "[" + this.lvwUserList.SelectedItems[0].Text + "] 角色列表";
            }
            else
            {
                this.btnAddRole.Enabled = false;
                this.btnDeleteRole.Enabled = false;

                userTitle = "角色列表";
            }

            try
            {
                LoadUserRoleList();
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            groupBox7.Text = userTitle;
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            if (this.lvwUserList.SelectedItems.Count > 0)
            {
                List<DataRightsRole_drr_Info> userRoles = null;
                string userNum = lvwUserList.SelectedItems[0].Name;

                try
                {
                    userRoles = this._dataRightsRoleBL.GetUserRoles(userNum);
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                    return;
                }

                QueryRightSettingRoles frm = new QueryRightSettingRoles(userNum, userRoles);

                frm.ShowDialog();

                LoadUserRoleList();
            }
        }

        private void btnDeleteRole_Click(object sender, EventArgs e)
        {
            if (this.lvwUserList.SelectedItems.Count == 0)
            {
                ShowWarningMessage("请选择相应的用户！");
                return;
            }

            if (this.lvwUserRoleList.Items != null && this.lvwUserRoleList.Items.Count > 0)
            {
                if (this.lvwUserRoleList.CheckedItems.Count > 0)
                {
                    if (!ShowQuestionMessage(this.SystemMessageText.strMessageText_Q_Delete))
                    {
                        return;
                    }

                    List<string> userRoles = new List<string>();

                    for (int i = 0; i < this.lvwUserRoleList.CheckedItems.Count; i++)
                    {
                        userRoles.Add(this.lvwUserRoleList.CheckedItems[i].Name);
                    }

                    ReturnValueInfo returnInfo = null;
                    try
                    {
                        returnInfo = this._dataRightsRoleBL.DeleteUserRole(this.lvwUserList.SelectedItems[0].Name, userRoles);
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }

                    if (returnInfo != null)
                    {
                        if (!returnInfo.boolValue)
                        {
                            ShowWarningMessage(returnInfo.messageText);
                        }
                        else
                        {
                            LoadUserRoleList();
                        }
                    }
                }
            }
        }


    }
}
