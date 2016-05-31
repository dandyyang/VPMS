using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.Management.Master;
using BLL.IBLL.Management.Master;
using BLL.Factory.Management;
using Common;

namespace WindowUI.Management.Master
{
    public partial class BuildingMasterSearch : BaseForm
    {
        BuildingMaster_bdm_Info _info;
        public string displayRecordID;
        IBuildingMasterBL _buildingMasterBL;

        public BuildingMasterSearch()
        {
            InitializeComponent();
            this._buildingMasterBL = MasterBLLFactory.GetBLL<IBuildingMasterBL>(MasterBLLFactory.BuildingMaster);
            txtcCNum.Focus();
        }

        public void ShowForm(BuildingMaster_bdm_Info info) 
        {
            _info = info;
            this.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            BuildingMaster_bdm_Info info = new BuildingMaster_bdm_Info();

            info.bdm_cNumber = txtcCNum.Text.Trim();
            info.bdm_cName = txtcChinaName.Text.Trim();

            //綁定數據
            try
            {
                bindData(_buildingMasterBL.SearchRecords(info));
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

        }

        //綁定數據處理
        private void bindData(List<Model.IModel.IModelObject> list)
        {
            BuildingMaster_bdm_Info info = new BuildingMaster_bdm_Info();
            lvwMstr.Items.Clear();
            foreach (var t in list)
            {
                info = t as BuildingMaster_bdm_Info;
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = info.bdm_iRecordID.ToString();
                li.SubItems.Add(info.bdm_cNumber);
                li.SubItems.Add(info.bdm_cName);
                li.SubItems.Add(info.bdm_cRemark);
                li.SubItems.Add(info.bdm_cAdd);
                li.SubItems.Add(info.bdm_dAddDate != null ? info.bdm_dAddDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "");
                li.SubItems.Add(info.bdm_cLast);
                li.SubItems.Add(info.bdm_dLastDate != null ? info.bdm_dLastDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "");
                lvwMstr.Items.Add(li);
            }
            lbliCount.Text = list.Count().ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void lvwMstr_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lv = (ListView)sender;
            displayRecordID = lv.SelectedItems[0].SubItems[0].Text;
            this.DialogResult = DialogResult.OK;
        }

        private void lvwMstr_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lvwMstr.SelectedItems.Count > 0)
            {
               _info.bdm_iRecordID=Convert.ToInt32(lvwMstr.SelectedItems[0].Text);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void lvwMstr_DoubleClick(object sender, EventArgs e)
        {
            if (lvwMstr.SelectedItems.Count > 0)
            {
                _info.bdm_iRecordID = Convert.ToInt32(lvwMstr.SelectedItems[0].Text);
                this.DialogResult = DialogResult.OK;
            }
        }

    }
}
