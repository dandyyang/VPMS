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
    public partial class DepartmentMaster : BaseForm
    {
        IDepartmentMasterBL _departmentBL;
        string iRecordID = string.Empty;
        

        public DepartmentMaster()
        {
            InitializeComponent();
            this._departmentBL = MasterBLLFactory.GetBLL<IDepartmentMasterBL>(MasterBLLFactory.DepartmentMaster);            
        }    

        private void DepartmentMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);
            SetControlLength();
            SetOpenToolBar();
            ToolBar.BtnNextEnabled = false;
            ToolBar.BtnLastEnabled = false;
            handel(Common.DefineConstantValue.GetReocrdEnum.GR_Last);
        }
        //設置文本可輸入的長度
        private void SetControlLength()
        {
            DepartmentMaster_dpm_Info info = null;
            try
            {
                info = this._departmentBL.GetTableFieldLenght() as DepartmentMaster_dpm_Info;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            if (info != null)
            {
                this.txtcNumber.MaxLength = info.dpm_cNumber_Length;
                this.txtcName.MaxLength = info.dpm_cName_Length;
                this.txtcRemark.MaxLength = info.dpm_cRemark_Length;
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
                    txtcName.Enabled = false;
                    txtcName.TextBoxSetStatus(true);
                    txtcNumber.Enabled = false;
                    txtcNumber.TextBoxSetStatus(true);
                    txtcRemark.Enabled = false;
                    txtcRemark.TextBoxSetStatus(true);
                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    txtcName.Enabled = true;
                    txtcName.TextBoxSetStatus(false);
                    txtcNumber.Enabled = true;
                    txtcNumber.TextBoxSetStatus(false);
                    txtcRemark.Enabled = true;
                    txtcRemark.TextBoxSetStatus(false);
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    txtcName.Enabled = true;
                    txtcName.TextBoxSetStatus(false);
                    txtcNumber.Enabled = false;
                    txtcNumber.TextBoxSetStatus(true);
                    txtcRemark.Enabled = true;
                    txtcRemark.TextBoxSetStatus(false);
                    break;
            }
        }
        //顯示數據
        private void showdpm(DepartmentMaster_dpm_Info info)
        {
            try
            {
                if (info != null)
                {
                    iRecordID = info.dpm_iRecordID.ToString();
                    txtcAdd.Text = info.dpm_cAdd.ToString();
                    txtcLast.Text = info.dpm_cLast.ToString();
                    txtcName.Text = info.dpm_cName.ToString();
                    txtcNumber.Text = info.dpm_cNumber.ToString();
                    txtcRemark.Text = info.dpm_cRemark.ToString();
                    txtdAddDate.Text = info.dpm_dAddDate != null ? info.dpm_dAddDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";
                    txtdLastDate.Text = info.dpm_dLastDate != null ? info.dpm_dLastDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";
                }
            }
            catch (Exception Ex)
            { ShowErrorMessage(Ex); }
        }
        //處理　首條，上條，下條，尾條
        private void handel(Common.DefineConstantValue.GetReocrdEnum type)
        {

            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Model.Base.DataBaseCommandInfo com = new Model.Base.DataBaseCommandInfo();
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            comkey.KeyValue = iRecordID;
            com.KeyInfoList.Add(comkey);

            DepartmentMaster_dpm_Info info = null;
            switch (type)
            {                   
                case Common.DefineConstantValue.GetReocrdEnum.GR_First:
                    try
                    {
                        info = _departmentBL.GetRecord_First();
                        if (info.dpm_iRecordID != 0)
                        {
                            showdpm(info);
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            ToolBar.BtnModifyEnabled = false;
                            ToolBar.BtnDeleteEnabled = false;
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                            ToolBar.BtnSearchEnabled = false;
                            txtcName.Text = "";
                            txtcNumber.Text = "";
                            txtcRemark.Text = "";
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
                case Common.DefineConstantValue.GetReocrdEnum.GR_Next:
                    try
                    {
                        info = _departmentBL.GetRecord_Next(com);
                        if (info == null)
                        {
                            info = _departmentBL.GetRecord_Last();
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                        }
                        else
                        {
                            info = _departmentBL.GetRecord_Next(com);
                        }
                        showdpm(info);
                        SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
                case Common.DefineConstantValue.GetReocrdEnum.GR_Previous:
                    try
                    {
                        info = _departmentBL.GetRecord_Previous(com);
                        if (info == null)
                        {
                            info = _departmentBL.GetRecord_First();
                            ToolBar.BtnFirstEnabled = false;
                            ToolBar.BtnPreviousEnabled = false;
                        }
                        else
                        {
                            info = _departmentBL.GetRecord_Previous(com);
                        }

                        showdpm(info);
                        SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
                case Common.DefineConstantValue.GetReocrdEnum.GR_Last:
                    try
                    {
                        info = _departmentBL.GetRecord_Last();
                        if (info.dpm_iRecordID != 0)
                        {
                            showdpm(info);
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            ToolBar.BtnPreviousEnabled = false;
                            ToolBar.BtnFirstEnabled = false;
                            ToolBar.BtnModifyEnabled = false;
                            ToolBar.BtnDeleteEnabled = false;
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                            ToolBar.BtnSearchEnabled = false;
                            txtcName.Text = "";
                            txtcNumber.Text = "";
                            txtcRemark.Text = "";
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
            }
        }

        private void ToolBar_BtnFirstClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            ToolBar.BtnFirstEnabled = false;
            ToolBar.BtnPreviousEnabled = false;
            handel(Common.DefineConstantValue.GetReocrdEnum.GR_First);
        }

        private void ToolBar_BtnPreviousClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            handel(Common.DefineConstantValue.GetReocrdEnum.GR_Previous);
        }

        private void ToolBar_BtnNextClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            handel(Common.DefineConstantValue.GetReocrdEnum.GR_Next);
        }

        private void ToolBar_BtnLastClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            ToolBar.BtnLastEnabled = false;
            ToolBar.BtnNextEnabled = false;
            handel(Common.DefineConstantValue.GetReocrdEnum.GR_Last);
        }
        //刪除按鈕
        private void ToolBar_BtnDeleteClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_Delete;
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Model.Base.DataBaseCommandInfo com = new Model.Base.DataBaseCommandInfo();
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            Model.SysMaster.DepartmentMaster_dpm_Info dpm_Info = new DepartmentMaster_dpm_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    dpm_Info.dpm_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    _departmentBL.Save(dpm_Info, Common.DefineConstantValue.EditStateEnum.OE_Delete);

                   // SetOpenToolBar();
                    handel(Common.DefineConstantValue.GetReocrdEnum.GR_Next);
                    if (dpm_Info.dpm_iRecordID == int.Parse(iRecordID))
                    {
                        handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
                    }
                    SetOpenToolBar();
                    if (dpm_Info.dpm_iRecordID == int.Parse(iRecordID))
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
        //新增按鈕
        private void ToolBar_BtnNewClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_Insert;
            SetTxtBox(DefineConstantValue.EditStateEnum.OE_Insert);
            txtcAdd.Text = "";
            txtcLast.Text = "";
            txtcName.Text = "";
            txtcNumber.Text = "";
            txtcNumber.Focus();
            txtcRemark.Text = "";
            txtdAddDate.Text = DateTime.Now.ToString(DefineConstantValue.gc_DateFormat);
            txtdLastDate.Text = DateTime.Now.ToString(DefineConstantValue.gc_DateFormat);
            //iRecordID = null;
        }


        private bool ChecktxtcName()
        {
            DataTypeVerifyResultInfo vInfo = null;
            vInfo = General.VerifyDataType(txtcNumber.Text, DataType.ChinaChar);

            if (!vInfo.IsMatch)
            {
                ShowWarningMessage("部門編號不能" + vInfo.Message);
                this.txtcNumber.Focus();

                return false;
            }
            else
                return true;
        }
        //保存按鈕
        private void ToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            DepartmentMaster_dpm_Info info = new DepartmentMaster_dpm_Info();

            if (txtcNumber.Text == "")
            {
                msg.messageText = "科室編號" + Common.DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
                ShowWarningMessage(msg.messageText);
                txtcNumber.Focus();
            }
            else if (txtcName.Text == "")
            {
                msg.messageText = "科室名" + Common.DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
                ShowWarningMessage(msg.messageText);
                txtcName.Focus();
            }
            else
            {
                try
                {
                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        info.dpm_iRecordID = Convert.ToInt32(iRecordID);
                    info.dpm_cNumber = txtcNumber.Text;
                    info.dpm_cName = txtcName.Text;
                    info.dpm_cRemark = txtcRemark.Text;

                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                    {
                        info.dpm_cAdd = UserInformation.usm_cUserLoginID;
                        info.dpm_dAddDate = DateTime.Now;
                    }
                    info.dpm_cLast = UserInformation.usm_cUserLoginID;
                    info.dpm_dLastDate = DateTime.Now;

                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        msg = _departmentBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                    else
                    {
                        msg = _departmentBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
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
        //取消按鈕
        private void ToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            DepartmentMaster_dpm_Info dpm_Info = new DepartmentMaster_dpm_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    dpm_Info.dpm_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    Model.IModel.IModelObject result = _departmentBL.DisplayRecord(dpm_Info);
                    dpm_Info = result as DepartmentMaster_dpm_Info;
                    showdpm(dpm_Info);
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
        //更新按鈕
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
        //搜索按鈕
        private void ToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            DepartmentMasterSearch dpmSearch = new DepartmentMasterSearch();
            dpmSearch.ShowDialog();

            if (dpmSearch.DialogResult == DialogResult.OK)
            {
                iRecordID = dpmSearch.displayRecordID;
                DepartmentMaster_dpm_Info info = new DepartmentMaster_dpm_Info();
                try
                {
                    info.dpm_iRecordID = Convert.ToInt32(iRecordID);
                    Model.IModel.IModelObject result = _departmentBL.DisplayRecord(info);
                    info = result as DepartmentMaster_dpm_Info;
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }
                this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                SetOpenToolBar();
                showdpm(info);
            }

            dpmSearch.Dispose();//釋放窗口
            dpmSearch = null;
        }                
    }
}
