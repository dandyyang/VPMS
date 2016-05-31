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
using Model.SysMaster;
using BLL.IBLL.SysMaster;
using BLL.Factory.SysMaster;

namespace WindowUI.SysMaster
{
    public partial class DepartmentMasterSearch : BaseForm
    {
        public string displayRecordID;
        IDepartmentMasterBL _departmentBL;
        public DepartmentMasterSearch()
        {
            InitializeComponent();
            this._departmentBL = MasterBLLFactory.GetBLL<IDepartmentMasterBL>(MasterBLLFactory.DepartmentMaster);
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
                DepartmentMaster_dpm_Info info = new DepartmentMaster_dpm_Info();

                info.dpm_cName = txtcChinaName.Text.ToString();//.Replace("*", "%").Replace("?", "_");
                info.dpm_cNumber = txtCNum.Text.ToString();//.Replace("*", "%").Replace("?", "_");

                DataBind(_departmentBL.SearchRecords(info));
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }
        //數據綁定
        private void DataBind(List<Model.IModel.IModelObject> list)
        {
            DepartmentMaster_dpm_Info info = new DepartmentMaster_dpm_Info();
            lvwMstr.Items.Clear();
            List<DepartmentMaster_dpm_Info> infoList = new List<DepartmentMaster_dpm_Info>();
            try
            {
                foreach (var t in list)
                {
                    info = t as DepartmentMaster_dpm_Info;
                    infoList.Add(info);
                }
                lvwMstr.SetDataSource<DepartmentMaster_dpm_Info>(infoList);
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
    }
}
