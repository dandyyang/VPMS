using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.IModel;
using Model.Management.Master;
using BLL.Factory.Management;
using BLL.Factory.General;
using BLL.IBLL.Management.Master;
using WindowUI.ClassLibrary.Public;
using Common;
using BLL.IBLL.General;
using Model.General;

namespace WindowUI.Management.Master
{
    public partial class SpecialtyMasterSearch : BaseForm
    {
        public string displayRecordID;
        ISpecialtyMasterBL _specialtyMasterBL;
        public SpecialtyMasterSearch()
        {
            InitializeComponent();
            this._specialtyMasterBL = MasterBLLFactory.GetBLL<ISpecialtyMasterBL>(MasterBLLFactory.SpecialtyMaster);
        }

        //數據綁定
        private void DataBind(List<Model.IModel.IModelObject> list)
        {
            SpecialtyMaster_spm_Info info = new SpecialtyMaster_spm_Info();
            List<SpecialtyMaster_spm_Info> infoList = new List<SpecialtyMaster_spm_Info>();
            lvwMstr.Items.Clear();
            try
            {
                foreach (var t in list)
                {
                    info = t as SpecialtyMaster_spm_Info;
                    infoList.Add(info);
                }
                lvwMstr.SetDataSource<SpecialtyMaster_spm_Info>(infoList);
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
            lbliCount.Text = list.Count().ToString();
        }

        private void SpecialtyMasterSearch_Load(object sender, EventArgs e)
        {
            txtCNum.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                SpecialtyMaster_spm_Info info = new SpecialtyMaster_spm_Info();

                info.spm_cName = txtcChinaName.Text.ToString();
                info.spm_cNumber = txtCNum.Text.ToString();

                DataBind(_specialtyMasterBL.SearchRecords(info));
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lvwMstr.SelectedItems.Count > 0)
            {
                displayRecordID = lvwMstr.SelectedItems[0].Text;
                this.DialogResult = DialogResult.OK;
            }
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

        private void lvwMstr_DoubleClick(object sender, EventArgs e)
        {
            if (lvwMstr.SelectedItems.Count > 0)
            {
                displayRecordID = lvwMstr.SelectedItems[0].Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
