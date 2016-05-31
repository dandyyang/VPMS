using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.SysMaster;
using BLL.IBLL.SysMaster;
using BLL.Factory.SysMaster;
using Model.IModel;
using WindowUI.ClassLibrary.Public;

namespace WindowUI.SysMaster
{
    public partial class SupplierMasterSearch : BaseForm
    {
        Sys_SupplierMaster_slm_Info _resultInfo;

        ISupplierMasterBL _supplierMasterBL;

        List<IModelObject> _resultList;

        public SupplierMasterSearch()
        {
            InitializeComponent();

            this._resultInfo = null;

            this._supplierMasterBL = MasterBLLFactory.GetBLL<ISupplierMasterBL>(MasterBLLFactory.SupplierMaster);

            _resultList = null;
        }

        public void ShowForm(Sys_SupplierMaster_slm_Info resultInfo)
        {
            _resultInfo = resultInfo;

            this.ShowDialog();
        }

        private void SupplierMasterSearch_Load(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Sys_SupplierMaster_slm_Info queryInfo = new Sys_SupplierMaster_slm_Info();

            if (txtcCNum.Text != "")
            {
                queryInfo.slm_cClientNum = txtcCNum.Text.Trim();
            }

            if (txtcChinaName.Text != "")
            {
                queryInfo.slm_cChinaName = txtcChinaName.Text.Trim();
            }

            try
            {
                _resultList = _supplierMasterBL.SearchRecords(queryInfo);

                lvwMstr.SetDataSource(_resultList);

                btnSelect.Enabled = true;
            }
            catch (Exception Ex)
            {
                
                throw Ex;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectVoid();
        }

        private void SelectVoid() 
        {
            if (lvwMstr.SelectedItems.Count > 0)
            {
                foreach (Sys_SupplierMaster_slm_Info supplierMaster in _resultList)
                {
                    if (supplierMaster.slm_iRecordID.ToString() == lvwMstr.SelectedItems[0].Text)
                    {
                        _resultInfo.slm_iRecordID = supplierMaster.slm_iRecordID;

                        break;
                    }
                }

                this.Close();
            }
        }

        private void lvwMstr_DoubleClick(object sender, EventArgs e)
        {
            SelectVoid();
        }

    }
}
