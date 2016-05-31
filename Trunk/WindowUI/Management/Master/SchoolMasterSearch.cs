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
    public partial class SchoolMasterSearch : BaseForm
    {
        public string displayRecordID;
        ISchoolMasterBL _schoolMasterBL;
        public SchoolMasterSearch()
        {
            InitializeComponent();

            this._schoolMasterBL = MasterBLLFactory.GetBLL<ISchoolMasterBL>(MasterBLLFactory.SchoolMaster);
            txtcCNum.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            SchoolMaster_scm_Info info = new SchoolMaster_scm_Info();

                    info.scm_cNumber = txtcCNum.Text.Trim();
                    info.scm_cName = txtcChinaName.Text.Trim();

                //綁定數據
                    try
                    {
                        bindData(_schoolMasterBL.SearchRecords(info));
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }

        }

        //綁定數據處理
        private void bindData(List<Model.IModel.IModelObject> list) 
        {
            SchoolMaster_scm_Info info = new SchoolMaster_scm_Info();
            lvwMstr.Items.Clear();
            foreach (var t in list) 
            {
                info=t as SchoolMaster_scm_Info;
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = info.scm_iRecordID.ToString();
                li.SubItems.Add(info.scm_cNumber);
                li.SubItems.Add(info.scm_cName);
                li.SubItems.Add(info.scm_cRemark);
                li.SubItems.Add(info.scm_cAdd);
                li.SubItems.Add(info.scm_dAddDate!=null?info.scm_dAddDate.Value.ToString(DefineConstantValue.gc_DateFormat):"");
                li.SubItems.Add(info.scm_cLast);
                li.SubItems.Add(info.scm_dLastDate!=null?info.scm_dLastDate.Value.ToString(DefineConstantValue.gc_DateFormat):"");
                lvwMstr.Items.Add(li);
            }
            lbliCount.Text = list.Count().ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult=DialogResult.Cancel;
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
                displayRecordID = lvwMstr.SelectedItems[0].Text;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
