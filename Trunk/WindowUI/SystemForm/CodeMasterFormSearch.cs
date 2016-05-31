using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.IBLL.General;
using BLL.Factory.General;
using Common;
using Model.IModel;
using BLL.IBLL.Management.Master;
using BLL.Factory.Management;
using WindowUI.ClassLibrary.Public;
using Model.Management.Master;

namespace WindowUI.SystemForm
{
    public partial class CodeMasterFormSearch : BaseForm
    {
        IGeneralBL _generalBL;
        ICodeMasterBL _codeMasterBL;
        CodeMaster_cmt_Info _info;
        public CodeMasterFormSearch()
        {
            InitializeComponent();
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
            this._codeMasterBL = MasterBLLFactory.GetBLL<ICodeMasterBL>(MasterBLLFactory.CodeMaster_cmt);
            BindCombox(DefineConstantValue.MasterType.CodeMaster_Key1, null);
        }

        private void BindCombox(DefineConstantValue.MasterType mType, string key)
        {
            List<IModelObject> result = new List<IModelObject>();
            try
            {
                if (key != null)
                {
                    result = _generalBL.GetMasterDataInformations(mType, key);
                }
                else
                {
                    result = _generalBL.GetMasterDataInformations(mType);
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage (Ex);
            }
            switch (mType)
            {
                case DefineConstantValue.MasterType.CodeMaster_Key1:
                    cbocKey1.SetDataSource(result,999);

                    break;
                case DefineConstantValue.MasterType.CodeMaster_Key2:
                    cbocKey2.SetDataSource(result,999);

                    break;
                default:
                    break;
            }
        }

        private void cbocKey1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbocKey1.SelectedValue != null)
            {
                BindCombox(DefineConstantValue.MasterType.CodeMaster_Key2, cbocKey1.SelectedValue.ToString());
            }
        }

        public void ShowForm(CodeMaster_cmt_Info info) 
        {
            _info = info;
            this.ShowDialog();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            List<Model.IModel.IModelObject> list = new List<IModelObject>();
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            info.cmt_cKey1 = cbocKey1.Text;
            info.cmt_cKey2 = cbocKey2.Text;
            info.cmt_cValue = txtcValue.Text;
            if (txtfNum.Text != "")
            {
                info.cmt_fNumber = Convert.ToDecimal(txtfNum.Text);
            }
            try
            {
                list = _codeMasterBL.SearchRecords(info);
            }
            catch (Exception Ex)
            {
                
                ShowErrorMessage(Ex);
            }
            if (list != null)
            {
                lvwMstr.SetDataSource<Model.IModel.IModelObject>(list);
                lbliCount.Text = lvwMstr.Items.Count.ToString();
            }

        }

        private void lvwMstr_DoubleClick(object sender, EventArgs e)
        {
            HanDel();
        }

        private void HanDel() 
        {
            CodeMaster_cmt_Info info = new CodeMaster_cmt_Info();
            info.cmt_iRecordID = Convert.ToInt32(lvwMstr.SelectedItems[0].SubItems[0].Text);
            try
            {
                info = _codeMasterBL.DisplayRecord(info) as CodeMaster_cmt_Info;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            _info.cmt_iRecordID = info.cmt_iRecordID;
            _info.cmt_cKey1 = info.cmt_cKey1;
            _info.cmt_cKey2 = info.cmt_cKey2;
            _info.cmt_cValue = info.cmt_cValue;
            _info.cmt_fNumber = info.cmt_fNumber;
            _info.cmt_cRemark = info.cmt_cRemark;
            _info.cmt_cRemark2 = info.cmt_cRemark2;
            _info.cmt_cAdd = info.cmt_cAdd;
            _info.cmt_dAddDate = info.cmt_dAddDate;
            _info.cmt_cLast = info.cmt_cLast;
            _info.cmt_dLastDate = info.cmt_dLastDate;

            this.DialogResult = DialogResult.OK;
        }

        private void lvwMstr_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            HanDel();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
