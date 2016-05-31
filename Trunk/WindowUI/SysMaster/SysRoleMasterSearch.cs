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
    public partial class SysRoleMasterSearch : BaseForm
    {
        ISysRoleMasterBL _sysRoleMasterBL;
        public string displayRecordID;
        public List<Sys_RoleMaster_rlm_Info> _RtvInfo;

        private enum enmLvwMaster
        {
            rlm_iRecordID,
            rlm_cRoleID,
            rlm_cRoleDesc
        }

        public SysRoleMasterSearch()
        {
            InitializeComponent();
            this._sysRoleMasterBL = MasterBLLFactory.GetBLL<ISysRoleMasterBL>(MasterBLLFactory.SysRoleMaster);
        }

        private void SysRoleMasterSearch_Load(object sender, EventArgs e)
        {
            this.txtcNum.Focus();
        }

        private void DataBind(List<Model.IModel.IModelObject> list)
        {
            Sys_RoleMaster_rlm_Info info = new Sys_RoleMaster_rlm_Info();
            lvwMstr.Items.Clear();
            List<Sys_RoleMaster_rlm_Info> infoList = new List<Sys_RoleMaster_rlm_Info>();
            try
            {
                foreach (var t in list)
                {
                    info = t as Sys_RoleMaster_rlm_Info;
                    infoList.Add(info);
                }
                lvwMstr.SetDataSource<Sys_RoleMaster_rlm_Info>(infoList);
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
            lbliCount.Text = list.Count().ToString();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                Sys_RoleMaster_rlm_Info info = new Sys_RoleMaster_rlm_Info();

                info.rlm_cRoleID = txtcNum.Text;

                DataBind(_sysRoleMasterBL.SearchRecords(info));
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Find();
        }

        void Find()
        {
            Sys_RoleMaster_rlm_Info smcitem = null;
            _RtvInfo = new List<Sys_RoleMaster_rlm_Info>();
            try
            {
                if (lvwMstr.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < lvwMstr.SelectedItems.Count; i++)
                    {
                        smcitem = new Sys_RoleMaster_rlm_Info();
                        smcitem.rlm_iRecordID = Convert.ToInt32(lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.rlm_iRecordID].Text);
                        smcitem.rlm_cRoleID = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.rlm_cRoleID].Text;
                        smcitem.rlm_cRoleDesc = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.rlm_cRoleDesc].Text;
                        _RtvInfo.Add(smcitem);
                    }
                }

            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvwMstr_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = true;
        }

        private void lvwMstr_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Find();
            //Sys_RoleMaster_rlm_Info temp = new Sys_RoleMaster_rlm_Info();
            //_RtvInfo = new List<Sys_RoleMaster_rlm_Info>();
            //try
            //{
            //    ListView lv = (ListView)sender;
            //    temp.rlm_iRecordID = Convert.ToInt32(lv.SelectedItems[0].SubItems[0].Text);
            //    _RtvInfo.Add(temp);
            //    //displayRecordID = lv.SelectedItems[0].SubItems[0].Text;
            //    this.DialogResult = DialogResult.OK;
            //}
            //catch (Exception Ex)
            //{
            //    ShowErrorMessage(Ex);
            //}
        }
    }
}
