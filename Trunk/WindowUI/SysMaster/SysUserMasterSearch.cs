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
    public partial class SysUserMasterSearch : BaseForm
    {
        ISysUserMasterBL _sysUserMasterBL;
        public string displayRecordID;
        public List<Sys_UserMaster_usm_Info> _RtvInfo;
        public List<Sys_UserMaster_usm_Info> _returnList;

        private enum enmLvwMaster
        {
            usm_iRecordID,
            usm_cUserLoginID,
            usm_cChaName

        }
        public SysUserMasterSearch()
        {
            InitializeComponent();
            this._sysUserMasterBL = MasterBLLFactory.GetBLL<ISysUserMasterBL>(MasterBLLFactory.SysUserMaster);
        }

        private void SysUserMasterSearch_Load(object sender, EventArgs e)
        {
            this.txtcNum.Focus();
        }

        private void DataBind(List<Model.IModel.IModelObject> list)
        {
            Sys_UserMaster_usm_Info info = new Sys_UserMaster_usm_Info();
            lvwMstr.Items.Clear();
            List<Sys_UserMaster_usm_Info> infoList = new List<Sys_UserMaster_usm_Info>();
            try
            {
                foreach (var t in list)
                {
                    info = t as Sys_UserMaster_usm_Info;
                    infoList.Add(info);
                }
                lvwMstr.SetDataSource<Sys_UserMaster_usm_Info>(infoList);
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
            lbliCount.Text = list.Count().ToString();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                Sys_UserMaster_usm_Info info = new Sys_UserMaster_usm_Info();

                info.usm_cUserLoginID = txtcNum.Text;
                info.usm_cChaName = txtcChinaName.Text;
                info.usm_cEMail = txtcEmail.Text;

                DataBind(_sysUserMasterBL.SearchRecords(info));
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }

        public void ShowForm(List<Sys_UserMaster_usm_Info> list)
        {
            _returnList = list;
            this.ShowDialog();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Find();
        }

        void Find()
        {
            Sys_UserMaster_usm_Info smcitem = null;
            _RtvInfo = new List<Sys_UserMaster_usm_Info>();
            try
            {
                if (lvwMstr.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < lvwMstr.SelectedItems.Count; i++)
                    {
                        smcitem = new Sys_UserMaster_usm_Info();
                        smcitem.usm_iRecordID = Convert.ToInt32(lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.usm_iRecordID].Text);
                        smcitem.usm_cUserLoginID = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.usm_cUserLoginID].Text;
                        smcitem.usm_cChaName = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.usm_cChaName].Text;
                        if (_RtvInfo!=null)
                        {
                            _RtvInfo.Add(smcitem); 
                        }
                        if (_returnList!=null)
                        {
                            _returnList.Add(smcitem); 
                        }
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
            //Sys_UserMaster_usm_Info temp = new Sys_UserMaster_usm_Info();
            //_RtvInfo = new List<Sys_UserMaster_usm_Info>();
            //try
            //{
            //    ListView lv = (ListView)sender;
            //    temp.usm_iRecordID = Convert.ToInt32(lv.SelectedItems[0].SubItems[0].Text);
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
