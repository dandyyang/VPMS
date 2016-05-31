using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowUI.Management.Common;
using Model.IModel;
using Model.Management.Master;
using BLL.Factory.Management;
using BLL.Factory.General;
using BLL.IBLL.Management.Master;
using WindowUI.ClassLibrary.Public;
using Common;
using Common.DataTypeVerify;

namespace WindowUI.Management.Master
{
    public partial class SpecialtyMaster : BaseForm
    {
        ISpecialtyMasterBL _specialtyMasterBL;
        string iRecordID = string.Empty;

        public SpecialtyMaster()
        {
            InitializeComponent();
            this._specialtyMasterBL = MasterBLLFactory.GetBLL<ISpecialtyMasterBL>(MasterBLLFactory.SpecialtyMaster);
        }

        private enum enmLvwMaster
        {
            //cut_iRecordID,
            cum_cNumber,
            cum_cName

        }

        private void SpecialtyMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);
            SetControlLength();
            SetOpenToolBar();
            ToolBar.BtnNextEnabled = false;
            ToolBar.BtnLastEnabled = false;
            handel(DefineConstantValue.GetReocrdEnum.GR_Last);


        }
        //設置文本可輸入的長度
        private void SetControlLength()
        {
            SpecialtyMaster_spm_Info info = null;
            try
            {
                info = this._specialtyMasterBL.GetTableFieldLenght() as SpecialtyMaster_spm_Info;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            if (info != null)
            {
                this.txtcName.MaxLength = info.spm_cName_Length;
                this.txtcRemark.MaxLength = info.spm_cRemark_Length;
            }
        }
        //設置按鈕狀態

        private void SetOpenToolBar()
        {
            ToolBar.BtnNewEnabled = true;
            ToolBar.BtnPreviousEnabled = true;
            ToolBar.BtnLastEnabled = true;
            ToolBar.BtnFirstEnabled = true;
            ToolBar.BtnSaveEnabled = false;
            ToolBar.BtnCancelEnabled = false;
            ToolBar.BtnNextEnabled = true;
            ToolBar.BtnModifyEnabled = true;
            ToolBar.BtnDeleteEnabled = true;
            ToolBar.BtnSearchEnabled = true;
        }
        //設置文本狀態

        private void SetTxtBox(DefineConstantValue.EditStateEnum type)
        {
            switch (type)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    txtcNum.Enabled = false;
                    txtcNum.TextBoxSetStatus(true);
                    txtcName.Enabled = false;
                    txtcName.TextBoxSetStatus(true);
                    txtcRemark.Enabled = false;
                    txtcRemark.TextBoxSetStatus(true);
                    btnDel.Enabled = false;
                    btnNew.Enabled = false;

                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    txtcNum.Enabled = true;
                    txtcNum.TextBoxSetStatus(false);
                    txtcName.Enabled = true;
                    txtcName.TextBoxSetStatus(false);
                    txtcRemark.Enabled = true;
                    txtcRemark.TextBoxSetStatus(false);
                    btnDel.Enabled = true;
                    btnNew.Enabled = true;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    txtcNum.Enabled = false;
                    txtcNum.TextBoxSetStatus(true);
                    txtcName.Enabled = true;
                    txtcName.TextBoxSetStatus(false);
                    txtcRemark.Enabled = true;
                    txtcRemark.TextBoxSetStatus(false);
                    btnDel.Enabled = true;
                    btnNew.Enabled = true;
                    break;
            }
        }
        //顯示數據
        private void showspm(SpecialtyMaster_spm_Info info)
        {
            try
            {
                if (info != null)
                {
                    iRecordID = info.spm_iRecordID.ToString();

                    txtcAdd.Text = info.spm_cAdd.ToString();
                    txtcLast.Text = info.spm_cLast.ToString();
                    txtcName.Text = info.spm_cName.ToString();
                    txtcNum.Text = info.spm_cNumber.ToString();
                    txtcRemark.Text = info.spm_cRemark.ToString();
                    txtdAddDate.Text = info.spm_dAddDate != null ? info.spm_dAddDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                    txtdLastDate.Text = info.spm_dLastDate != null ? info.spm_dLastDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }
        /// <summary>
        /// 數據綁定
        /// </summary>
        /// <param name="list"></param>
        private void SmcBind(SpecialtyMaster_spm_Info list)
        {
            try
            {
                lvwMaster.Items.Clear();
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }
        //處理　首條，上條，下條，尾條

        private void handel(DefineConstantValue.GetReocrdEnum type)
        {

            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Model.Base.DataBaseCommandInfo com = new Model.Base.DataBaseCommandInfo();
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            comkey.KeyValue = iRecordID;
            com.KeyInfoList.Add(comkey);

            SpecialtyMaster_spm_Info info = null;
            switch (type)
            {
                case DefineConstantValue.GetReocrdEnum.GR_First:
                    try
                    {
                        info = _specialtyMasterBL.GetRecord_First();
                        if (info.spm_iRecordID != 0)
                        {
                            showspm(info);
                            SmcBind(info);
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            ToolBar.BtnModifyEnabled = false;
                            ToolBar.BtnDeleteEnabled = false;
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                            ToolBar.BtnSearchEnabled = false;
                            txtcNum.Text = "";
                            txtcName.Text = "";
                            txtcRemark.Text = "";
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                    break;
                case DefineConstantValue.GetReocrdEnum.GR_Next:
                    try
                    {
                        info = _specialtyMasterBL.GetRecord_Next(com);
                        if (info == null)
                        {
                            info = _specialtyMasterBL.GetRecord_Last();
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                        }
                        else
                        {
                            info = _specialtyMasterBL.GetRecord_Next(com);
                        }
                        showspm(info);
                        SmcBind(info);
                        SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                    break;
                case DefineConstantValue.GetReocrdEnum.GR_Previous:
                    try
                    {
                        info = _specialtyMasterBL.GetRecord_Previous(com);
                        if (info == null)
                        {
                            info = _specialtyMasterBL.GetRecord_First();
                            ToolBar.BtnFirstEnabled = false;
                            ToolBar.BtnPreviousEnabled = false;
                        }
                        else
                        {
                            info = _specialtyMasterBL.GetRecord_Previous(com);
                        }

                        showspm(info);
                        SmcBind(info);
                        SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                    break;
                case DefineConstantValue.GetReocrdEnum.GR_Last:
                    try
                    {
                        info = _specialtyMasterBL.GetRecord_Last();
                        if (info.spm_iRecordID != 0)
                        {
                            showspm(info);
                            SmcBind(info);
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            ToolBar.BtnModifyEnabled = false;
                            ToolBar.BtnDeleteEnabled = false;
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                            ToolBar.BtnSearchEnabled = false;
                            ToolBar.BtnFirstEnabled = false;
                            ToolBar.BtnPreviousEnabled = false;
                            txtcNum.Text = "";
                            txtcName.Text = "";
                            txtcRemark.Text = "";
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                    break;
            }
        }

        private void ToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            SpecialtyMaster_spm_Info spm_Info = new SpecialtyMaster_spm_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    spm_Info.spm_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    Model.IModel.IModelObject result = _specialtyMasterBL.DisplayRecord(spm_Info);
                    spm_Info = result as SpecialtyMaster_spm_Info;

                    showspm(spm_Info);
                    SmcBind(spm_Info);

                    SetOpenToolBar();
                    SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                }
                else
                {
                    handel(DefineConstantValue.GetReocrdEnum.GR_Last);
                }

            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void ToolBar_BtnDeleteClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_Delete;
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Model.Base.DataBaseCommandInfo com = new Model.Base.DataBaseCommandInfo();
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            Model.Management.Master.SpecialtyMaster_spm_Info spm_Info = new SpecialtyMaster_spm_Info();
            //SpecialtyMasterCourse_smc_Info smc_info = new SpecialtyMasterCourse_smc_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    spm_Info.spm_iRecordID = Convert.ToInt32(comkey.KeyValue);

                    _specialtyMasterBL.Save(spm_Info, DefineConstantValue.EditStateEnum.OE_Delete);
                    // SetOpenToolBar();
                    handel(DefineConstantValue.GetReocrdEnum.GR_Next);
                    if (spm_Info.spm_iRecordID == int.Parse(iRecordID))
                    {
                        handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
                    }
                    SetOpenToolBar();
                    if (spm_Info.spm_iRecordID == int.Parse(iRecordID))
                    {
                        SetOpenToolBar();
                        ToolBar.BtnPreviousEnabled = false;
                        ToolBar.BtnLastEnabled = false;
                        ToolBar.BtnFirstEnabled = false;
                        ToolBar.BtnNextEnabled = false;
                        ToolBar.BtnModifyEnabled = false;
                        ToolBar.BtnDeleteEnabled = false;
                        ToolBar.BtnSearchEnabled = false;
                    }
                }
            }
            catch (Exception Ex)
            {
                this.ShowErrorMessage(Ex);
            }

        }

        private void ToolBar_BtnFirstClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            ToolBar.BtnFirstEnabled = false;
            ToolBar.BtnPreviousEnabled = false;
            handel(DefineConstantValue.GetReocrdEnum.GR_First);
        }

        private void ToolBar_BtnLastClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            ToolBar.BtnLastEnabled = false;
            ToolBar.BtnNextEnabled = false;
            handel(DefineConstantValue.GetReocrdEnum.GR_Last);
        }

        private void ToolBar_BtnModifyClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_Update;
            SetTxtBox(DefineConstantValue.EditStateEnum.OE_Update);
            ToolBar.BtnNewEnabled = false;
            ToolBar.BtnPreviousEnabled = false;
            ToolBar.BtnLastEnabled = false;
            ToolBar.BtnFirstEnabled = false;
            ToolBar.BtnSaveEnabled = true;
            ToolBar.BtnCancelEnabled = true;
            ToolBar.BtnNextEnabled = false;
            ToolBar.BtnModifyEnabled = false;
            ToolBar.BtnDeleteEnabled = false;
            ToolBar.BtnSearchEnabled = false;
        }

        private void ToolBar_BtnNewClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_Insert;
            SetTxtBox(DefineConstantValue.EditStateEnum.OE_Insert);
            txtcAdd.Text = "";
            txtcLast.Text = "";
            txtcName.Text = "";
            txtcNum.Text = "";
            txtcNum.Focus();
            txtcRemark.Text = "";
            txtdAddDate.Text = DateTime.Now.ToString(DefineConstantValue.gc_DateFormat);
            txtdLastDate.Text = DateTime.Now.ToString(DefineConstantValue.gc_DateFormat);
            //iRecordID = null;
            //smc.Clear();
            lvwMaster.Items.Clear();
        }

        private void ToolBar_BtnNextClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            handel(DefineConstantValue.GetReocrdEnum.GR_Next);
        }

        private void ToolBar_BtnPreviousClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
        }

        private bool ChecktxtcName()
        {
            DataTypeVerifyResultInfo vInfo = null;
            vInfo = General.VerifyDataType(txtcNum.Text, DataType.ChinaChar);

            if (!vInfo.IsMatch)
            {
                ShowWarningMessage("專業編號不能" + vInfo.Message);
                this.txtcNum.Focus();

                return false;
            }
            else
                return true;
        }

        private void ToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            SpecialtyMaster_spm_Info info = new SpecialtyMaster_spm_Info();

            if (txtcName.Text == "")
            {
                msg.messageText = "專業名" + DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
                ShowWarningMessage(msg.messageText);
                txtcName.Focus();
            }
            else
            {
                try
                {
                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        info.spm_iRecordID = Convert.ToInt32(iRecordID);
                    info.spm_cNumber = txtcNum.Text;
                    info.spm_cName = txtcName.Text;
                    info.spm_cRemark = txtcRemark.Text;

                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                    {
                        info.spm_cAdd = UserInformation.usm_cUserLoginID;
                        info.spm_dAddDate = DateTime.Now;
                    }
                    info.spm_cLast = UserInformation.usm_cUserLoginID;
                    info.spm_dLastDate = DateTime.Now;

                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        msg = _specialtyMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                    else
                    {
                        msg = _specialtyMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
                        if (msg.boolValue)
                        {
                            handel(DefineConstantValue.GetReocrdEnum.GR_Last);
                        }
                    }
                    if (msg.messageText != null)
                        ShowInformationMessage(msg.messageText);
                    else
                    {
                        //showspm(info);
                        SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        SetOpenToolBar();
                    }
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }
            }
        }
        //判斷紀錄是否重復（listview和傳過來的list）

        private bool IsExistsItem(string text)
        {
            for (int i = 0; i < lvwMaster.Items.Count; i++)
            {

                if (lvwMaster.Items[i].SubItems[(int)enmLvwMaster.cum_cNumber].Text == text)
                {
                    return true;
                }
            }
            return false;
        }
        //新增list裡面的數據

        private void btnNew_Click(object sender, EventArgs e)
        {

        }
        //搜索
        private void ToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            SpecialtyMasterSearch spm = new SpecialtyMasterSearch();
            spm.ShowDialog();

            if (spm.DialogResult == DialogResult.OK)
            {
                iRecordID = spm.displayRecordID;
                SpecialtyMaster_spm_Info info = new SpecialtyMaster_spm_Info();
                try
                {
                    info.spm_iRecordID = Convert.ToInt32(iRecordID);
                    Model.IModel.IModelObject result = _specialtyMasterBL.DisplayRecord(info);
                    info = result as SpecialtyMaster_spm_Info;
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }
                this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                SetOpenToolBar();
                showspm(info);
                SmcBind(info);
            }

            spm.Dispose();
            spm = null;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwMaster.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("您確認要删除吗？", "系統信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        lvwMaster.Items.Remove(lvwMaster.SelectedItems[0]);
                    }
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }
    }
}
