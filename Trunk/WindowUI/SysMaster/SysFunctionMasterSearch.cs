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
    public partial class SysFunctionMasterSearch : BaseForm
    {
        public string displayRecordID;
        ISysFunctionMasterBL _sysFunctionMasterBL;
        public List<Sys_FunctionMaster_fum_Info> _RtvInfo;
        public SysFunctionMasterSearch()
        {
            InitializeComponent();
            this._sysFunctionMasterBL = MasterBLLFactory.GetBLL<ISysFunctionMasterBL>(MasterBLLFactory.SysFunctionMaster);
        }

        private void SysFunctionMasterSearch_Load(object sender, EventArgs e)
        {
            this.txtcNum.Focus();
        }

        private void DataBind(List<Model.IModel.IModelObject> list)
        {
            Sys_FunctionMaster_fum_Info info = new Sys_FunctionMaster_fum_Info();
            lvwMstr.Items.Clear();
            List<Sys_FunctionMaster_fum_Info> infoList = new List<Sys_FunctionMaster_fum_Info>();
            try
            {
                foreach (var t in list)
                {
                    info = t as Sys_FunctionMaster_fum_Info;
                    infoList.Add(info);
                }
                lvwMstr.SetDataSource<Sys_FunctionMaster_fum_Info>(infoList);
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
            lbliCount.Text = list.Count().ToString();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                Sys_FunctionMaster_fum_Info info = new Sys_FunctionMaster_fum_Info();

                info.fum_cFunctionNumber = txtcNum.Text;

                DataBind(_sysFunctionMasterBL.SearchRecords(info));
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }

        private enum enmLvwMaster
        {
            fum_iRecordID,
            fum_cFunctionNumber,
            fum_cFunctionDesc
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            Sys_FunctionMaster_fum_Info smcitem = null;
            _RtvInfo = new List<Sys_FunctionMaster_fum_Info>();
            try
            {
                if (lvwMstr.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < lvwMstr.SelectedItems.Count; i++)
                    {
                        smcitem = new Sys_FunctionMaster_fum_Info();
                        smcitem.fum_iRecordID = Convert.ToInt32(lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.fum_iRecordID].Text);
                        smcitem.fum_cFunctionNumber = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.fum_cFunctionNumber].Text;
                        smcitem.fum_cFunctionDesc = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.fum_cFunctionDesc].Text;
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

    }
}
