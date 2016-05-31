using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.General;
using BLL.IBLL.General;
using BLL.Factory.General;
using Common;
using WindowUI.ClassLibrary.Public;
using Model.Management.DataRightsManagement;
using WindowUI.Management.Master;
using Model.Management.Master;
using Model.Base;
using BLL.IBLL.Management.DataRightsManagement;
using BLL.Factory.Management;

namespace WindowUI.Management.DataRightsManagement
{
    public partial class DataRightsRoleSettingSearch : BaseForm//Form  BaseForm //
    {
        List<DataRightsRole_drr_Info> _list = null;
        
        IDataRightsRoleBL _dateRightRoleBL;
        public string displayRecordID = string.Empty;
        public DataRightsRoleSettingSearch()
        {
            InitializeComponent();
            this._dateRightRoleBL = MasterBLLFactory.GetBLL<IDataRightsRoleBL>(MasterBLLFactory.DataRightsRole);
        }

        public void ShowForm(List<DataRightsRole_drr_Info> list) 
        {
            _list = list;
            lvwMstr.MultiSelect = true;
            this.ShowDialog();
        }

        private void DataBind(List<Model.IModel.IModelObject> list)
        {
            DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();
            lvwMstr.Items.Clear();
            List<DataRightsRole_drr_Info> infoList = new List<DataRightsRole_drr_Info>();
            try
            {
                foreach (var t in list)
                {
                    info = t as DataRightsRole_drr_Info;
                    infoList.Add(info);
                }
                lvwMstr.SetDataSource<DataRightsRole_drr_Info>(infoList);
            }
            catch (Exception Ex)
            { 
                ShowErrorMessage(Ex); 
            }
            lbliCount.Text = list.Count().ToString();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();

                if (txtNum.Text != "")
                    info.drr_cNumber = txtNum.Text;
                if (txtName.Text != "")
                    info.drr_cName = txtName.Text;

                DataBind(_dateRightRoleBL.SearchRecords(info));
            }
            catch (Exception Ex)
            { 
                ShowErrorMessage(Ex); 
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (_list!=null)
            {
                foreach (ListViewItem item in lvwMstr.SelectedItems)
                {
                    DataRightsRole_drr_Info info = new DataRightsRole_drr_Info();
                    info.drr_cNumber = item.SubItems[1].Text;
                    info.drr_cName = item.SubItems[2].Text;
                    info.drr_cRemark = item.SubItems[3].Text;

                    _list.Add(info);

                } 
            }
            displayRecordID = lvwMstr.SelectedItems[0].SubItems[0].Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lvwMstr_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListView lv = (ListView)sender;
                displayRecordID = lv.SelectedItems[0].SubItems[0].Text;
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }
    }
}
