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
    public partial class SiteMasterSearch : BaseForm
    {
        public string displayRecordID;
        ISiteMasterBL _siteBL;
        public SiteMasterSearch()
        {
            InitializeComponent();
            this._siteBL = MasterBLLFactory.GetBLL<ISiteMasterBL>(MasterBLLFactory.SiteMaster);
        }

        private void DepartmentMasterSearch_Load(object sender, EventArgs e)
        {
            txtCNum.Focus();
        }

        //退出按鈕


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //搜索按鈕
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                SiteMaster_stm_Info info = new SiteMaster_stm_Info();

                info.stm_cName = txtcChinaName.Text.ToString();
                info.stm_cNumber = txtCNum.Text.ToString();

                DataBind(_siteBL.SearchRecords(info));
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }
        //數據綁定
        private void DataBind(List<Model.IModel.IModelObject> list)
        {
            SiteMaster_stm_Info info = new SiteMaster_stm_Info();
            lvwMstr.Items.Clear();
            List<SiteMaster_stm_Info> infoList = new List<SiteMaster_stm_Info>();
            try
            {
                foreach (var t in list)
                {
                    info = t as SiteMaster_stm_Info;
                    infoList.Add(info);
                }
                lvwMstr.SetDataSource<SiteMaster_stm_Info>(infoList);
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
            lbliCount.Text = list.Count().ToString();
        }
        //雙擊list
        private void lvwMstr_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lv = (ListView)sender;
            displayRecordID = lv.SelectedItems[0].SubItems[0].Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwMstr.SelectedItems.Count > 0)
                {
                    displayRecordID = lvwMstr.SelectedItems[0].Text;
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }
        private void lvwMstr_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = true;
        }

        private void lvwMstr_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvwMstr.SelectedItems.Count > 0)
                {
                    displayRecordID = lvwMstr.SelectedItems[0].Text;
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }
    }
}