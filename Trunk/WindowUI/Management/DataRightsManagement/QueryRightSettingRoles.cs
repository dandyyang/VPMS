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
    public partial class QueryRightSettingRoles : BaseForm
    {
        IDataRightsRoleBL _dataRightsRoleBL;
        string _userNumber;
        List<DataRightsRole_drr_Info> _userRoleList;

        public QueryRightSettingRoles(string userNumber, List<DataRightsRole_drr_Info> userRoleList)
        {
            InitializeComponent();

            this._userNumber = userNumber;
            this._userRoleList = userRoleList;

            this._dataRightsRoleBL = MasterBLLFactory.GetBLL<IDataRightsRoleBL>(MasterBLLFactory.DataRightsRole);

            LoadData();
            TrimUserRole();
        }

        private void LoadData()
        {
            List<DataRightsRole_drr_Info> roleList = null;

            try
            {
                roleList = _dataRightsRoleBL.GetAllRoleList();
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);

                return;
            }

            if (roleList != null)
            {
                foreach (DataRightsRole_drr_Info item in roleList)
                {
                    ListViewItem li = new ListViewItem();
                    li.SubItems[0].Text = item.drr_cName;
                    li.SubItems[0].Name = item.drr_cNumber;
                    li.ImageIndex = 0;
                    li.ToolTipText = item.drr_cRemark;

                    lvwRole.Items.Add(li);
                }
            }
        }

        void TrimUserRole()
        {
            if (this._userRoleList != null && this._userRoleList.Count > 0 && this.lvwRole.Items != null && this.lvwRole.Items.Count > 0)
            {
                for (int i = 0; i < this._userRoleList.Count; i++)
                {
                    for (int j = 0; j < this.lvwRole.Items.Count; j++)
                    {
                        if (this.lvwRole.Items[j].Name.Trim() == this._userRoleList[i].drr_cNumber.Trim())
                        {
                            this.lvwRole.Items.RemoveAt(j);
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
            List<string> roleList = new List<string>();
            foreach (ListViewItem item in lvwRole.CheckedItems)
            {
                roleList.Add(item.Name);
            }

            ReturnValueInfo retunInfo = null;
            try
            {
                retunInfo = _dataRightsRoleBL.SaveUserRole(this._userNumber,roleList);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            return retunInfo.boolValue;
        }

    }
}
