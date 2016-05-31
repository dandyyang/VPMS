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
    public partial class SysFormMasterSearch : BaseForm
    {
        public string displayRecordID;
        ISysFormMasterBL _sysFormMasterBL;
        public List<Sys_FormMaster_fom_Info> _RtvInfo;
        public SysFormMasterSearch()
        {
            InitializeComponent();
            this._sysFormMasterBL = MasterBLLFactory.GetBLL<ISysFormMasterBL>(MasterBLLFactory.SysFormMaster);
        }

        private void SysFormMasterSearch_Load(object sender, EventArgs e)
        {
            this.txtcNum.Focus();
        }
        private enum enmLvwMaster
        {
            fom_iRecordID,
            fom_cFormNumber,
            fom_cFormDesc
        }
        private void DataBind(List<Model.IModel.IModelObject> list)
        {
            Sys_FormMaster_fom_Info info = new Sys_FormMaster_fom_Info();
            lvwMstr.Items.Clear();
            List<Sys_FormMaster_fom_Info> infoList = new List<Sys_FormMaster_fom_Info>();
            try
            {
                foreach (var t in list)
                {
                    info = t as Sys_FormMaster_fom_Info;
                    infoList.Add(info);
                }
                lvwMstr.SetDataSource<Sys_FormMaster_fom_Info>(infoList);
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
            lbliCount.Text = list.Count().ToString();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                Sys_FormMaster_fom_Info info = new Sys_FormMaster_fom_Info();
                
                info.fom_cFormNumber = txtcNum.Text;

                DataBind(_sysFormMasterBL.SearchRecords(info));
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Sys_FormMaster_fom_Info smcitem = null;
            _RtvInfo = new List<Sys_FormMaster_fom_Info>();
            try
            {
                if (lvwMstr.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < lvwMstr.SelectedItems.Count; i++)
                    {
                        smcitem = new Sys_FormMaster_fom_Info();
                        smcitem.fom_iRecordID = Convert.ToInt32(lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.fom_iRecordID].Text);
                        smcitem.fom_cFormNumber = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.fom_cFormNumber].Text;
                        smcitem.fom_cFormDesc = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.fom_cFormDesc].Text;
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
            ListView lv = (ListView)sender;
            displayRecordID = lv.SelectedItems[0].SubItems[0].Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
