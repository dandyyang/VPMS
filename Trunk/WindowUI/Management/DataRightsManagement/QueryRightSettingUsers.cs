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
    public partial class QueryRightSettingUsers : BaseForm
    {
        IDataRightsRoleBL _dataRightsRoleBL;
        string _roleNumber;
        List<Sys_UserMaster_usm_Info> _roleUserList;

        public QueryRightSettingUsers(string roleNumber,List<Sys_UserMaster_usm_Info> roleUserList)
        {
            InitializeComponent();

            this._roleNumber = roleNumber;
            this._roleUserList = roleUserList;

            this._dataRightsRoleBL = MasterBLLFactory.GetBLL<IDataRightsRoleBL>(MasterBLLFactory.DataRightsRole);

            LoadData();
            TrimRoleUser();
        }

        private void LoadData()
        {
            List<Sys_UserMaster_usm_Info> usmList=null;

            try
            {
                usmList = _dataRightsRoleBL.GetAllSysUserList();
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);

                return;
            }

            if (usmList != null)
            {
                foreach (Sys_UserMaster_usm_Info item in usmList)
                {
                    ListViewItem li = new ListViewItem();
                    li.SubItems[0].Text = item.usm_cChaName;
                    li.SubItems[0].Name = item.usm_cUserLoginID;
                    li.ImageIndex = 0;
                    lvwUser.Items.Add(li);
                }
            }
        }

        void TrimRoleUser()
        {
            if (this._roleUserList != null && this._roleUserList.Count > 0 && this.lvwUser.Items!=null && this.lvwUser.Items.Count>0)
            {
                for (int i = 0; i < this._roleUserList.Count; i++)
                {
                    for (int j = 0; j < this.lvwUser.Items.Count; j++)
                    {
                        if (this.lvwUser.Items[j].Name.Trim() == this._roleUserList[i].usm_cUserLoginID.Trim())
                        {
                            this.lvwUser.Items.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (SaveRecord())
            {
                this.Close();
            }
        }

        bool SaveRecord()
        {
            DataRightsRole_drr_Info roleInfo = new DataRightsRole_drr_Info();
            roleInfo.drr_cNumber = this._roleNumber;
            List<Sys_UserMaster_usm_Info> userList = new List<Sys_UserMaster_usm_Info>();
            foreach (ListViewItem item in lvwUser.CheckedItems)
            {
                Sys_UserMaster_usm_Info userInfo = new Sys_UserMaster_usm_Info();
                userInfo.usm_cUserLoginID = item.Name;
                userList.Add(userInfo);
            }
            roleInfo.RoleUserList = userList;

            ReturnValueInfo retunInfo = null;
            try
            {
                retunInfo = _dataRightsRoleBL.SaveRoleUser(roleInfo);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            return retunInfo.boolValue;
        }

        

    }
}
