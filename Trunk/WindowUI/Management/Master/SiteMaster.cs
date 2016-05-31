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
using Common.DataTypeVerify;
using BLL.IBLL.General;
using Model.General;

namespace WindowUI.Management.Master
{
    public partial class SiteMaster : BaseForm
    {
        ISiteMasterBL _siteMasterBL;
        IGeneralBL _generalBL;
        string iRecordID = string.Empty;

        public SiteMaster()
        {
            InitializeComponent();
            this._siteMasterBL = MasterBLLFactory.GetBLL<ISiteMasterBL>(MasterBLLFactory.SiteMaster);
        }

        private void SiteMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);    
            BindComBox();
            SetControlLength();
            SetOpenToolBar();
            ToolBar.BtnNextEnabled = false;
            ToolBar.BtnLastEnabled = false;
            handel(DefineConstantValue.GetReocrdEnum.GR_Last);


        }
        //combox數據綁定  
        private void BindComBox()
        {
            List<IModelObject> result = new List<IModelObject>();
            List<BuildingMaster_bdm_Info> bdm_info = new List<BuildingMaster_bdm_Info>();
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
            try
            {
                result = _generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.BuildingMaster);
                cmbcNumber.SetDataSource(result);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }
        //設置文本可輸入的長度
        private void SetControlLength()
        {
            SiteMaster_stm_Info info = null;
            try
            {
                info = this._siteMasterBL.GetTableFieldLenght() as SiteMaster_stm_Info;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            if (info != null)
            {
                this.txtcName.MaxLength = info.stm_cName_Length;
                this.txtcRemark.MaxLength = info.stm_cRemark_Length;
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
                    cmbcNumber.Enabled = false;
                    txtcRemark.Enabled = false;
                    txtcRemark.TextBoxSetStatus(true);
                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    txtcNum.Enabled = true;
                    txtcNum.TextBoxSetStatus(false);
                    txtcName.Enabled = true;
                    txtcName.TextBoxSetStatus(false);
                    cmbcNumber.Enabled = true;
                    txtcRemark.Enabled = true;
                    txtcRemark.TextBoxSetStatus(false);
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    txtcNum.Enabled = false;
                    txtcNum.TextBoxSetStatus(true);
                    txtcName.Enabled = true;
                    txtcName.TextBoxSetStatus(false);
                    cmbcNumber.Enabled = true;
                    txtcRemark.Enabled = true;
                    txtcRemark.TextBoxSetStatus(false);
                    break;
            }
        }
        //顯示數據
        private void showstm(SiteMaster_stm_Info info)
        {
            try
            {
                if (info != null)
                {
                    iRecordID = info.stm_iRecordID.ToString();

                    txtcAdd.Text = info.stm_cAdd.ToString();
                    txtcLast.Text = info.stm_cLast.ToString();
                    txtcName.Text = info.stm_cName.ToString();

                    cmbcNumber.SelectedValue = info.stm_cBuildingNumber;

                    txtcNum.Text = info.stm_cNumber.ToString();
                    txtcRemark.Text = info.stm_cRemark.ToString();
                    txtdAddDate.Text = info.stm_dAddDate != null ? info.stm_dAddDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                    txtdLastDate.Text = info.stm_dLastDate != null ? info.stm_dLastDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                }
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

            SiteMaster_stm_Info info = null;
            switch (type)
            {
                case DefineConstantValue.GetReocrdEnum.GR_First:
                    try
                    {
                        info = _siteMasterBL.GetRecord_First();
                        if (info.stm_iRecordID != 0)
                        {
                            showstm(info);
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
                        info = _siteMasterBL.GetRecord_Next(com);
                        if (info == null)
                        {
                            info = _siteMasterBL.GetRecord_Last();
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                        }
                        else
                        {
                            info = _siteMasterBL.GetRecord_Next(com);
                        }
                        showstm(info);
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
                        info = _siteMasterBL.GetRecord_Previous(com);
                        if (info == null)
                        {
                            info = _siteMasterBL.GetRecord_First();
                            ToolBar.BtnFirstEnabled = false;
                            ToolBar.BtnPreviousEnabled = false;
                        }
                        else
                        {
                            info = _siteMasterBL.GetRecord_Previous(com);
                        }

                        showstm(info);
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
                        info = _siteMasterBL.GetRecord_Last();
                        if (info.stm_iRecordID != 0)
                        {
                            showstm(info);
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

        private void ToolBar_BtnFirstClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            ToolBar.BtnFirstEnabled = false;
            ToolBar.BtnPreviousEnabled = false;
            handel(DefineConstantValue.GetReocrdEnum.GR_First);
        }

        private void ToolBar_BtnPreviousClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
        }

        private void ToolBar_BtnNextClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            handel(DefineConstantValue.GetReocrdEnum.GR_Next);
        }

        private void ToolBar_BtnLastClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetOpenToolBar();
            ToolBar.BtnLastEnabled = false;
            ToolBar.BtnNextEnabled = false;
            handel(DefineConstantValue.GetReocrdEnum.GR_Last);
        }
        //刪除按鈕
        private void ToolBar_BtnDeleteClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_Delete;
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Model.Base.DataBaseCommandInfo com = new Model.Base.DataBaseCommandInfo();
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            Model.Management.Master.SiteMaster_stm_Info stm_Info = new SiteMaster_stm_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    stm_Info.stm_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    _siteMasterBL.Save(stm_Info, DefineConstantValue.EditStateEnum.OE_Delete);
                    handel(DefineConstantValue.GetReocrdEnum.GR_Next);
                    if (stm_Info.stm_iRecordID == int.Parse(iRecordID))
                    {
                        handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
                    }
                    SetOpenToolBar();
                    if (stm_Info.stm_iRecordID == int.Parse(iRecordID))
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
                ShowErrorMessage(Ex);
            }
           
        }
        //新增按鈕
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
        }

        private bool ChecktxtcName()
        {
            DataTypeVerifyResultInfo vInfo = null;
            vInfo = General.VerifyDataType(txtcNum.Text, DataType.ChinaChar);

            if (!vInfo.IsMatch)
            {
                ShowWarningMessage("地點編號不能" + vInfo.Message);
                this.txtcNum.Focus();

                return false;
            }
            else
                return true;
        }
        //保存按鈕
        private void ToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            SiteMaster_stm_Info info = new SiteMaster_stm_Info();

            if (txtcName.Text == "")
            {
                msg.messageText = "地點名" + DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
                ShowWarningMessage(msg.messageText);
                txtcName.Focus();
            }
            else
            {
                try
                {
                    if (cmbcNumber.SelectedValue == null)
                    {
                        MessageBox.Show("请选择建筑物");
                    }
                    else
                    {
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                            info.stm_iRecordID = Convert.ToInt32(iRecordID);
                        info.stm_cBuildingNumber = cmbcNumber.SelectedValue.ToString();
                        info.stm_cNumber = txtcNum.Text;
                        info.stm_cName = txtcName.Text;
                        info.stm_cRemark = txtcRemark.Text;
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                        {
                            info.stm_cAdd = UserInformation.usm_cUserLoginID;
                            info.stm_dAddDate = DateTime.Now;
                        }
                        info.stm_cLast = UserInformation.usm_cUserLoginID;
                        info.stm_dLastDate = DateTime.Now;
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                            msg = _siteMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                        else
                        {
                            msg = _siteMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
                            if (msg.boolValue == true)
                            {
                                handel(DefineConstantValue.GetReocrdEnum.GR_Last);
                            }
                        }
                        if (msg.messageText != null)
                            ShowInformationMessage(msg.messageText);
                        else
                        {
                            //showstm(info);
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                            SetOpenToolBar();
                        }
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
            Model.Management.Master.SiteMaster_stm_Info stm_Info = new SiteMaster_stm_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    stm_Info.stm_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    Model.IModel.IModelObject result = _siteMasterBL.DisplayRecord(stm_Info);
                    stm_Info = result as SiteMaster_stm_Info;

                    showstm(stm_Info);
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
            SiteMasterSearch stmSearch = new SiteMasterSearch();
            stmSearch.ShowDialog();

            if (stmSearch.DialogResult == DialogResult.OK)
            {
                iRecordID = stmSearch.displayRecordID;
                SiteMaster_stm_Info info = new SiteMaster_stm_Info();
                try
                {
                    info.stm_iRecordID = Convert.ToInt32(iRecordID);
                    Model.IModel.IModelObject result = _siteMasterBL.DisplayRecord(info);
                    info = result as SiteMaster_stm_Info;
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }
                this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                SetOpenToolBar();
                showstm(info);
            }

            stmSearch.Dispose();//釋放窗口
            stmSearch = null;
        }
    }
}