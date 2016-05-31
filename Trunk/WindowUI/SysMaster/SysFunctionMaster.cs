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
    public partial class SysFunctionMaster : BaseForm
    {
        ISysFunctionMasterBL _sysFunctionMasterBL;
        ISysFormMasterBL _sysFormMasterBL;
        string iRecordID = string.Empty;

        public SysFunctionMaster()
        {
            InitializeComponent();
            this._sysFunctionMasterBL = MasterBLLFactory.GetBLL<ISysFunctionMasterBL>(MasterBLLFactory.SysFunctionMaster);
            this._sysFormMasterBL = MasterBLLFactory.GetBLL<ISysFormMasterBL>(MasterBLLFactory.SysFormMaster);
        }

        private void SysFunctionMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);
            SetControlLength();
            SetOpenToolBar();
            ToolBar.BtnNextEnabled = false;
            ToolBar.BtnLastEnabled = false;
            Handel(Common.DefineConstantValue.GetReocrdEnum.GR_Last);

           // DataBind();
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

        private void SetControlLength()
        {
            Sys_FunctionMaster_fum_Info info = null;
            try
            {
                info = this._sysFunctionMasterBL.GetTableFieldLenght() as Sys_FunctionMaster_fum_Info;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            if (info != null)
            {
                this.txtcCode.MaxLength = info.fum_cFunctionNumber_Length;
                txtcDesc.MaxLength = info.fum_cFunctionDesc_Length;
            }
        }

        //設置文本狀態

        private void SetTxtBox(DefineConstantValue.EditStateEnum type)
        {
            switch (type)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    txtcCode.Enabled = false;
                    txtcCode.TextBoxSetStatus(true);

                    txtcDesc.Enabled = false;
                    txtcDesc.TextBoxSetStatus(true);
                    lvwFrom.Enabled = false;
                    btnNew.Enabled = false;
                    btnDel.Enabled = false;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    txtcCode.Enabled = true;
                    txtcCode.TextBoxSetStatus(false);

                    txtcDesc.Enabled = true;
                    txtcDesc.TextBoxSetStatus(false);
                    lvwFrom.Enabled = true;
                    btnNew.Enabled = true;
                    btnDel.Enabled = true;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    txtcCode.Enabled = false;
                    txtcCode.TextBoxSetStatus(true);

                    txtcDesc.Enabled = true;
                    txtcDesc.TextBoxSetStatus(false);
                    lvwFrom.Enabled = true;
                    btnNew.Enabled = true;
                    btnDel.Enabled = true;
                    break;
            }
        }

        private bool ChecktxtcName()
        {
            DataTypeVerifyResultInfo vInfo = null;
            vInfo = General.VerifyDataType(txtcCode.Text, DataType.ChinaChar);

            if (!vInfo.IsMatch)
            {
                ShowWarningMessage("功能编号不能" + vInfo.Message);
                this.txtcCode.Focus();

                return false;
            }
            else
                return true;
        }
        //顯示數據
        private void ShowInfo(Sys_FunctionMaster_fum_Info info)
        {
            try
            {
                if (info != null)
                {
                    iRecordID = info.fum_iRecordID.ToString();
                    txtcAdd.Text = info.fum_cAdd.ToString();
                    txtcLast.Text = info.fum_cLast.ToString();

                    txtcCode.Text = info.fum_cFunctionNumber.ToString();
                    txtcDesc.Text = info.fum_cFunctionDesc.ToString();

                    lvwFrom.Items.Clear();

                    lvwFrom.SetDataSource<Sys_FormMaster_fom_Info>(info.formMaster);

                    txtdAddDate.Text = info.fum_dAddDate != null ? info.fum_dAddDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";
                    txtdLastDate.Text = info.fum_dLastDate != null ? info.fum_dLastDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";
                }
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }

        private void Handel(Common.DefineConstantValue.GetReocrdEnum type)
        {

            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Model.Base.DataBaseCommandInfo com = new Model.Base.DataBaseCommandInfo();
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            comkey.KeyValue = iRecordID;
            com.KeyInfoList.Add(comkey);

            Sys_FunctionMaster_fum_Info info = null;
            switch (type)
            {
                case Common.DefineConstantValue.GetReocrdEnum.GR_First:
                    try
                    {
                        info = _sysFunctionMasterBL.GetRecord_First();
                        if (info.fum_iRecordID != 0)
                        {
                            ShowInfo(info);
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            ToolBar.BtnModifyEnabled = false;
                            ToolBar.BtnDeleteEnabled = false;
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                            ToolBar.BtnSearchEnabled = false;
                            txtcCode.Text = "";
                            txtcDesc.Text = "";
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
                case Common.DefineConstantValue.GetReocrdEnum.GR_Next:
                    try
                    {
                        info = _sysFunctionMasterBL.GetRecord_Next(com);
                        if (info == null)
                        {
                            info = _sysFunctionMasterBL.GetRecord_Last();
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                        }
                        else
                        {
                            info = _sysFunctionMasterBL.GetRecord_Next(com);
                        }
                        ShowInfo(info);
                        SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
                case Common.DefineConstantValue.GetReocrdEnum.GR_Previous:
                    try
                    {
                        info = _sysFunctionMasterBL.GetRecord_Previous(com);
                        if (info == null)
                        {
                            info = _sysFunctionMasterBL.GetRecord_First();
                            ToolBar.BtnFirstEnabled = false;
                            ToolBar.BtnPreviousEnabled = false;
                        }
                        else
                        {
                            info = _sysFunctionMasterBL.GetRecord_Previous(com);
                        }

                        ShowInfo(info);
                        SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
                case Common.DefineConstantValue.GetReocrdEnum.GR_Last:
                    try
                    {
                        info = _sysFunctionMasterBL.GetRecord_Last();
                        if (info.fum_iRecordID != 0)
                        {
                            ShowInfo(info);
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            ToolBar.BtnFirstEnabled = false;
                            ToolBar.BtnPreviousEnabled = false;
                            ToolBar.BtnModifyEnabled = false;
                            ToolBar.BtnDeleteEnabled = false;
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                            ToolBar.BtnSearchEnabled = false;
                            txtcCode.Text = "";
                            txtcDesc.Text = "";
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
            }
        }
        //------
        private void ToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            Sys_FunctionMaster_fum_Info Info = new Sys_FunctionMaster_fum_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    Info.fum_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    Model.IModel.IModelObject result = _sysFunctionMasterBL.DisplayRecord(Info);
                    Info = result as Sys_FunctionMaster_fum_Info;
                    ShowInfo(Info);
                    SetOpenToolBar();
                    SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                }

                else
                {
                    Handel(DefineConstantValue.GetReocrdEnum.GR_Last);
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
            Sys_FunctionMaster_fum_Info Info = new Sys_FunctionMaster_fum_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    Info.fum_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    _sysFunctionMasterBL.Save(Info, Common.DefineConstantValue.EditStateEnum.OE_Delete);

                    // SetOpenToolBar();
                    Handel(Common.DefineConstantValue.GetReocrdEnum.GR_Next);
                    if (Info.fum_iRecordID == int.Parse(iRecordID))
                    {
                        Handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
                    }
                    SetOpenToolBar();
                    if (Info.fum_iRecordID == int.Parse(iRecordID))
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
            { ShowErrorMessage(Ex); } 
        }

        private void ToolBar_BtnFirstClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            ToolBar.BtnFirstEnabled = false;
            ToolBar.BtnPreviousEnabled = false;
            Handel(Common.DefineConstantValue.GetReocrdEnum.GR_First);
        }

        private void ToolBar_BtnLastClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            ToolBar.BtnLastEnabled = false;
            ToolBar.BtnNextEnabled = false;
            Handel(Common.DefineConstantValue.GetReocrdEnum.GR_Last);
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
            txtcCode.Text = "";
            txtcCode.Focus();
            txtcDesc.Text = "";
            lvwFrom.Items.Clear();
        }

        private void ToolBar_BtnNextClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            Handel(Common.DefineConstantValue.GetReocrdEnum.GR_Next);
        }

        private void ToolBar_BtnPreviousClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            Handel(Common.DefineConstantValue.GetReocrdEnum.GR_Previous);
        }

        private void ToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Sys_FunctionMaster_fum_Info info = new Sys_FunctionMaster_fum_Info();
            Sys_FormMaster_fom_Info fomInfo = null;
            if (ChecktxtcName())
            {
                if (txtcCode.Text == "")
                {
                    msg.messageText = "功能编号" + Common.DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
                    ShowWarningMessage(msg.messageText);
                    txtcCode.Focus();
                }
                else
                {
                    try
                    {
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        {
                            info.fum_iRecordID = Convert.ToInt32(iRecordID);
                        }
                        info.fum_cFunctionNumber = txtcCode.Text;
                        info.fum_cFunctionDesc = txtcDesc.Text;

                        for (int i = 0; i < lvwFrom.Items.Count; i++)
                        {
                            fomInfo = new Sys_FormMaster_fom_Info();
                            fomInfo.fom_cFormNumber = lvwFrom.Items[i].SubItems[0].Text;
                            //userInfo.cum_cName = lvwMaster.Items[i].SubItems[1].Text;
                            info.formMaster.Add(fomInfo);
                        }

                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                        {
                            info.fum_cAdd = UserInformation.usm_cUserLoginID;
                            info.fum_dAddDate = DateTime.Now;
                        }
                        info.fum_cLast = UserInformation.usm_cUserLoginID;
                        info.fum_dLastDate = DateTime.Now;
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        {
                            msg = _sysFunctionMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                        }
                        else
                        {
                            msg = _sysFunctionMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
                            if (msg.boolValue)
                            {
                                Handel(DefineConstantValue.GetReocrdEnum.GR_Last);
                            }
                        }

                        if (msg.messageText != null)
                            ShowInformationMessage(msg.messageText);
                        else
                        {
                            SetOpenToolBar();
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                }
            }
        }

        private void ToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            SysFunctionMasterSearch win = new SysFunctionMasterSearch();
            win.ShowDialog();

            if (win.DialogResult == DialogResult.OK)
            {
                //iRecordID = win.displayRecordID;
                iRecordID = win._RtvInfo[0].fum_iRecordID.ToString();
                Sys_FunctionMaster_fum_Info info = new Sys_FunctionMaster_fum_Info();
                try
                {
                    info.fum_iRecordID = Convert.ToInt32(iRecordID);
                    Model.IModel.IModelObject result = _sysFunctionMasterBL.DisplayRecord(info);
                    info = result as Sys_FunctionMaster_fum_Info;
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }
                this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                SetOpenToolBar();
                ShowInfo(info);
            }


            win.Dispose();
            win = null;
        }

        private enum enmLvwMaster
        {
            fom_cFormNumber,
            fom_cFormDesc
        }

        private bool IsExistsItem(string text)
        {
            for (int i = 0; i < lvwFrom.Items.Count; i++)
            {

                if (lvwFrom.Items[i].SubItems[(int)enmLvwMaster.fom_cFormNumber].Text == text)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SysFormMasterSearch win = new SysFormMasterSearch();
            win.ShowDialog();


            List<Sys_FormMaster_fom_Info> items = win._RtvInfo;
            if (items != null)
            {
                try
                {

                    List<Sys_FormMaster_fom_Info> cumList = new List<Sys_FormMaster_fom_Info>();

                    foreach (Sys_FormMaster_fom_Info fom in items)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = fom.fom_cFormNumber.ToString();
                        if (!IsExistsItem(list.Text))
                        {
                            //cumList.Add(cum);

                            ListViewItem it = new ListViewItem(fom.fom_cFormNumber.ToString());
                            it.SubItems.Add(fom.fom_cFormDesc.ToString());
                            lvwFrom.Items.Add(it);
                        }
                    }

                    //lvwMaster.SetDataSource<CourseMaster_cum_Info>(cumList,false);

                }
                catch (Exception Ex)
                { ShowErrorMessage(Ex); }
            } 





            win.Dispose();
            win = null;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwFrom.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("您確認要删除吗？", "系統信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        lvwFrom.Items.Remove(lvwFrom.SelectedItems[0]);
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
