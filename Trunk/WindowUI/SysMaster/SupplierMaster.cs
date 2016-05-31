using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.IBLL.SysMaster;
using BLL.Factory.SysMaster;
using Model.SysMaster;
using Common;
using WindowUI.ClassLibrary.Public;
using Model.Base;

namespace WindowUI.SysMaster
{
    public partial class SupplierMaster : BaseForm
    {
        ISupplierMasterBL _supplierMasterBL;

        Sys_SupplierMaster_slm_Info _currentDisplayInfo;

        Sys_SupplierMaster_slm_Info _backupDisplayInfo;

        public SupplierMaster()
        {
            InitializeComponent();

            this._supplierMasterBL = MasterBLLFactory.GetBLL<ISupplierMasterBL>(MasterBLLFactory.SupplierMaster);

            this._currentDisplayInfo = null;

            this._backupDisplayInfo = null;

            SetPurview(this.ToolBar);

            SetFieldLenght();

            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);

            GoLast();
        }

        private void SupplierMaster_Load(object sender, EventArgs e)
        {


        }

        public void AddSupplier(Sys_UserMaster_usm_Info userInformation) 
        {
            this.UserInformation = userInformation;

            //SetPurview(this.ToolBar);

            //SetFieldLenght();

            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);

            this.ToolBar.BtnNewVisible = true;

            this.ToolBar.BtnModifyVisible = true;

            this.ToolBar.BtnDeleteVisible = true;

            this.ShowDialog();

        }

        /// <summary>
        /// 打包DataBaseCommandInfo
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        private DataBaseCommandInfo PageCommandInfo(int recordID)
        {
            DataBaseCommandInfo commonandInfo = new DataBaseCommandInfo();

            DataBaseCommandKeyInfo keyInfo = new DataBaseCommandKeyInfo();

            keyInfo.Key = recordID.ToString();

            keyInfo.KeyValue = recordID.ToString();

            commonandInfo.KeyInfoList.Add(keyInfo);

            return commonandInfo;
        }

        /// <summary>
        /// 显示下一条记录
        /// </summary>
        /// <param name="currentDisplayInfo"></param>
        private void GoNext(Sys_SupplierMaster_slm_Info currentDisplayInfo)
        {
            try
            {
                _backupDisplayInfo = _currentDisplayInfo;

                //PageCommandInfo(currentDisplayInfo.slm_iRecordID);

                _currentDisplayInfo = _supplierMasterBL.GetRecord_Next(PageCommandInfo(currentDisplayInfo.slm_iRecordID));

                if (_currentDisplayInfo == null)
                {
                    SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);

                    _currentDisplayInfo = _backupDisplayInfo;
                }
                else
                {
                    SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                }

                ShowToUI(_currentDisplayInfo);
            }
            catch (Exception Ex)
            {

                ShowErrorMessage(Ex);
            }
        }

        /// <summary>
        /// 显示上一条记录
        /// </summary>
        /// <param name="currentDisplayInfo"></param>
        private void GoPrevious(Sys_SupplierMaster_slm_Info currentDisplayInfo)
        {
            try
            {
                _backupDisplayInfo = _currentDisplayInfo;

                _currentDisplayInfo = _supplierMasterBL.GetRecord_Previous(PageCommandInfo(currentDisplayInfo.slm_iRecordID));

                if (_currentDisplayInfo == null)
                {
                    SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_First);

                    _currentDisplayInfo = _backupDisplayInfo;
                }
                else
                {
                    SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                }

                ShowToUI(_currentDisplayInfo);
            }
            catch (Exception Ex)
            {

                ShowErrorMessage(Ex);
            }

        }

        /// <summary>
        /// 显示最前一条记录
        /// </summary>
        private void GoFirst()
        {
            try
            {

                this._currentDisplayInfo = _supplierMasterBL.GetRecord_First();

                ShowToUI(_currentDisplayInfo);

                if (_currentDisplayInfo != null)
                {
                    SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_First);
                }
                else
                {
                    SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Null);
                }
            }
            catch (Exception Ex)
            {

                ShowErrorMessage(Ex);
            }

        }

        /// <summary>
        /// 显示最后一条记录
        /// </summary>
        private void GoLast()
        {
            try
            {

                this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;

                SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);

                this._currentDisplayInfo = _supplierMasterBL.GetRecord_Last();

                ShowToUI(_currentDisplayInfo);

                if (_currentDisplayInfo != null)
                {
                    SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                }
                else
                {
                    SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Null);
                }
            }
            catch (Exception Ex)
            {

                SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Null);

                ShowErrorMessage(Ex);

            }

        }


        /// <summary>
        /// 设定字段长度限制
        /// </summary>
        private void SetFieldLenght()
        {
            Sys_SupplierMaster_slm_Info fieldLenghtInfo;

            try
            {
                fieldLenghtInfo = _supplierMasterBL.GetTableFieldLenght() as Sys_SupplierMaster_slm_Info;

                txtcNum.MaxLength = fieldLenghtInfo.slm_cClientNumLength;

                txtcChinaName.MaxLength = fieldLenghtInfo.slm_cChinaNameLength;

                txtcEnglishName.MaxLength = fieldLenghtInfo.slm_cEnglishNameLength;

                txtcTaxNumber.MaxLength = fieldLenghtInfo.slm_cTaxNumberLength;

                txtcLinkman.MaxLength = fieldLenghtInfo.slm_cLinkmanLength;

                txtcAddress.MaxLength = fieldLenghtInfo.slm_cAddressLength;

                txtcPhone.MaxLength = fieldLenghtInfo.slm_cPhoneLength;

                txtcFax.MaxLength = fieldLenghtInfo.slm_cFaxLength;

                txtcWebSite.MaxLength = fieldLenghtInfo.slm_cWebSiteLength;

                txtcRemark.MaxLength = fieldLenghtInfo.slm_cRemarkLength;
            }
            catch (Exception Ex)
            {

                ShowErrorMessage(Ex);
            }
        }

        /// <summary>
        /// 显示内容到介面
        /// </summary>
        /// <param name="displayInfo"></param>
        private void ShowToUI(Sys_SupplierMaster_slm_Info displayInfo)
        {

            if (displayInfo != null)
            {

                txtcNum.Text = displayInfo.slm_cClientNum;

                txtcChinaName.Text = displayInfo.slm_cChinaName;

                txtcEnglishName.Text = displayInfo.slm_cEnglishName;

                txtcTaxNumber.Text = displayInfo.slm_cTaxNumber;

                txtcLinkman.Text = displayInfo.slm_cLinkman;

                txtcAddress.Text = displayInfo.slm_cAddress;

                txtcPhone.Text = displayInfo.slm_cPhone;

                txtcFax.Text = displayInfo.slm_cFax;

                txtcWebSite.Text = displayInfo.slm_cWebSite;

                txtcRemark.Text = displayInfo.slm_cRemark;

                txtcAdd.Text = displayInfo.slm_cAdd;

                txtcLast.Text = displayInfo.slm_cLast;

                txtdAddDate.Text = displayInfo.slm_dAddDate.ToString(DefineConstantValue.gc_DateFormat);

                txtdLastDate.Text = displayInfo.slm_dLastDate.ToString(DefineConstantValue.gc_DateFormat);

            }
            else
            {
                ResetForm();
            }
        }

        /// <summary>
        /// 重置介面数据
        /// </summary>
        private void ResetForm()
        {
            txtcNum.Text = "";

            txtcChinaName.Text = "";

            txtcEnglishName.Text = "";

            txtcTaxNumber.Text = "";

            txtcLinkman.Text = "";

            txtcAddress.Text = "";

            txtcPhone.Text = "";

            txtcFax.Text = "";

            txtcWebSite.Text = "";

            txtcRemark.Text = "";

            txtcAdd.Text = "";

            txtcLast.Text = "";

            txtdAddDate.Text = "";

            txtdLastDate.Text = "";
        }

        /// <summary>
        /// 将用户数据打包成实体类
        /// </summary>
        /// <returns></returns>
        private Sys_SupplierMaster_slm_Info UIdataToInfo()
        {
            Sys_SupplierMaster_slm_Info userData = new Sys_SupplierMaster_slm_Info();

            if (this._currentDisplayInfo!=null)
            {
                userData.slm_iRecordID = this._currentDisplayInfo.slm_iRecordID; 
            }

            userData.slm_cClientNum = txtcNum.Text.Trim();

            userData.slm_cChinaName = txtcChinaName.Text.Trim();

            userData.slm_cEnglishName = txtcEnglishName.Text.Trim();

            userData.slm_cTaxNumber = txtcTaxNumber.Text.Trim();

            userData.slm_cLinkman = txtcLinkman.Text.Trim();

            userData.slm_cAddress = txtcAddress.Text.Trim();

            userData.slm_cPhone = txtcPhone.Text.Trim();

            userData.slm_cFax = txtcFax.Text.Trim();

            userData.slm_cWebSite = txtcWebSite.Text.Trim();

            userData.slm_cRemark = txtcRemark.Text.Trim();

            if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
            {
                userData.slm_cAdd = this.UserInformation.usm_cUserLoginID;

                userData.slm_cLast = this.UserInformation.usm_cUserLoginID;
            }
            else if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
            {
                userData.slm_cLast = this.UserInformation.usm_cUserLoginID;
            }

            return userData;
        }

        /// <summary>
        /// 设置表单ToolBar控钮显示状态
        /// </summary>
        /// <param name="statc"></param>
        private void SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum statc)
        {
            switch (statc)
            {
                case DefineConstantValue.GetReocrdEnum.GR_First:
                    this.ToolBar.BtnFirstEnabled = false;
                    this.ToolBar.BtnPreviousEnabled = false;
                    this.ToolBar.BtnNextEnabled = true;
                    this.ToolBar.BtnLastEnabled = true;
                    this.ToolBar.BtnModifyEnabled = true;
                    this.ToolBar.BtnSearchEnabled = true;
                    break;

                case DefineConstantValue.GetReocrdEnum.GR_Last:
                    this.ToolBar.BtnFirstEnabled = true;
                    this.ToolBar.BtnPreviousEnabled = true;
                    this.ToolBar.BtnNextEnabled = false;
                    this.ToolBar.BtnLastEnabled = false;
                    this.ToolBar.BtnModifyEnabled = true;
                    this.ToolBar.BtnSearchEnabled = true;
                    break;

                case DefineConstantValue.GetReocrdEnum.GR_Middle:
                    this.ToolBar.BtnFirstEnabled = true;
                    this.ToolBar.BtnPreviousEnabled = true;
                    this.ToolBar.BtnNextEnabled = true;
                    this.ToolBar.BtnLastEnabled = true;
                    this.ToolBar.BtnModifyEnabled = true;
                    this.ToolBar.BtnSearchEnabled = true;
                    break;
                case DefineConstantValue.GetReocrdEnum.GR_Null:
                    this.ToolBar.BtnNewEnabled = true;
                    this.ToolBar.BtnDeleteEnabled = false;
                    this.ToolBar.BtnFirstEnabled = false;
                    this.ToolBar.BtnPreviousEnabled = false;
                    this.ToolBar.BtnNextEnabled = false;
                    this.ToolBar.BtnLastEnabled = false;
                    this.ToolBar.BtnModifyEnabled = false;
                    this.ToolBar.BtnSearchEnabled = false;

                    break;
                default: break;
            }
        }

        /// <summary>
        /// 设置表单控件状态
        /// </summary>
        /// <param name="editStatus"></param>
        private void SetControlStatus(DefineConstantValue.EditStateEnum editStatus)
        {
            switch (editStatus)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    this.txtcNum.TextBoxSetStatus(true);
                    this.txtcChinaName.TextBoxSetStatus(true);
                    this.txtcEnglishName.TextBoxSetStatus(true);
                    this.txtcTaxNumber.TextBoxSetStatus(true);
                    this.txtcLinkman.TextBoxSetStatus(true);
                    this.txtcAddress.TextBoxSetStatus(true);
                    this.txtcPhone.TextBoxSetStatus(true);
                    this.txtcFax.TextBoxSetStatus(true);
                    this.txtcRemark.TextBoxSetStatus(true);
                    this.txtcWebSite.TextBoxSetStatus(true);


                    this.ToolBar.BtnNewEnabled = true;
                    this.ToolBar.BtnModifyEnabled = true;
                    this.ToolBar.BtnDeleteEnabled = true;
                    this.ToolBar.BtnSaveEnabled = false;
                    this.ToolBar.BtnCancelEnabled = false;
                    this.ToolBar.BtnFirstEnabled = true;
                    this.ToolBar.BtnPreviousEnabled = true;
                    this.ToolBar.BtnNextEnabled = true;
                    this.ToolBar.BtnLastEnabled = true;
                    this.ToolBar.BtnSearchEnabled = true;


                    this.ToolBar.BtnDataInputEnabled = true;
                    this.ToolBar.BtnDataExportEnabled = true;
                    this.ToolBar.BtnExpCusDataEnabled = true;
                    this.ToolBar.BtnExportTemplateEnabled = true;

                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:

                    this.txtcNum.TextBoxSetStatus(true);
                    this.txtcChinaName.TextBoxSetStatus(false);
                    this.txtcEnglishName.TextBoxSetStatus(false);
                    this.txtcTaxNumber.TextBoxSetStatus(false);
                    this.txtcLinkman.TextBoxSetStatus(false);
                    this.txtcAddress.TextBoxSetStatus(false);
                    this.txtcPhone.TextBoxSetStatus(false);
                    this.txtcFax.TextBoxSetStatus(false);
                    this.txtcRemark.TextBoxSetStatus(false);
                    this.txtcWebSite.TextBoxSetStatus(false);


                    this.ToolBar.BtnNewEnabled = false;
                    this.ToolBar.BtnModifyEnabled = false;
                    this.ToolBar.BtnDeleteEnabled = true;
                    this.ToolBar.BtnSaveEnabled = true;
                    this.ToolBar.BtnCancelEnabled = true;
                    this.ToolBar.BtnFirstEnabled = false;
                    this.ToolBar.BtnPreviousEnabled = false;
                    this.ToolBar.BtnNextEnabled = false;
                    this.ToolBar.BtnLastEnabled = false;
                    this.ToolBar.BtnSearchEnabled = false;
                    this.ToolBar.BtnDataInputEnabled = false;
                    this.ToolBar.BtnDataExportEnabled = false;
                    this.ToolBar.BtnExpCusDataEnabled = false;
                    this.ToolBar.BtnExportTemplateEnabled = false;


                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:

                    this.txtcNum.TextBoxSetStatus(false);
                    this.txtcChinaName.TextBoxSetStatus(false);
                    this.txtcEnglishName.TextBoxSetStatus(false);
                    this.txtcTaxNumber.TextBoxSetStatus(false);
                    this.txtcLinkman.TextBoxSetStatus(false);
                    this.txtcAddress.TextBoxSetStatus(false);
                    this.txtcPhone.TextBoxSetStatus(false);
                    this.txtcFax.TextBoxSetStatus(false);
                    this.txtcRemark.TextBoxSetStatus(false);
                    this.txtcWebSite.TextBoxSetStatus(false);

                    this.ToolBar.BtnNewEnabled = false;
                    this.ToolBar.BtnModifyEnabled = false;
                    this.ToolBar.BtnDeleteEnabled = false;
                    this.ToolBar.BtnSaveEnabled = true;
                    this.ToolBar.BtnCancelEnabled = true;
                    this.ToolBar.BtnFirstEnabled = false;
                    this.ToolBar.BtnPreviousEnabled = false;
                    this.ToolBar.BtnNextEnabled = false;
                    this.ToolBar.BtnLastEnabled = false;
                    this.ToolBar.BtnSearchEnabled = false;
                    this.ToolBar.BtnDataInputEnabled = false;
                    this.ToolBar.BtnDataExportEnabled = false;
                    this.ToolBar.BtnExpCusDataEnabled = false;
                    this.ToolBar.BtnExportTemplateEnabled = false;


                    break;
                default:

                    break;
            }
        }

        private void ToolBar_BtnPreviousClick(object sender, EventArgs e)
        {
            GoPrevious(_currentDisplayInfo);
        }

        private void ToolBar_BtnNextClick(object sender, EventArgs e)
        {
            GoNext(_currentDisplayInfo);
        }

        private void ToolBar_BtnLastClick(object sender, EventArgs e)
        {
            GoLast();
        }

        private void ToolBar_BtnFirstClick(object sender, EventArgs e)
        {
            GoFirst();
        }

        private void ToolBar_BtnNewClick(object sender, EventArgs e)
        {
            ResetForm();

            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Insert);

            this.EditState = DefineConstantValue.EditStateEnum.OE_Insert;

        }

        private void ToolBar_BtnModifyClick(object sender, EventArgs e)
        {
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Update);

            this.EditState = DefineConstantValue.EditStateEnum.OE_Update;
        }

        private void ToolBar_BtnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                Model.General.ReturnValueInfo saveInfo;

                Sys_SupplierMaster_slm_Info supplierMaster = UIdataToInfo();

                saveInfo = _supplierMasterBL.Save(supplierMaster, DefineConstantValue.EditStateEnum.OE_Delete);

                if (saveInfo.boolValue)
                {
                    _currentDisplayInfo = _supplierMasterBL.GetRecord_Next(PageCommandInfo(_currentDisplayInfo.slm_iRecordID));

                    if (_currentDisplayInfo != null)
                    {
                        ShowToUI(_currentDisplayInfo);

                        SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                    }
                    else
                    {
                        _currentDisplayInfo = _supplierMasterBL.GetRecord_Last();

                        if (_currentDisplayInfo != null)
                        {
                            ShowToUI(_currentDisplayInfo);

                            SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                        }
                        else
                        {
                            ResetForm();

                            SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Null);
                        }
                    }

                    SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);

                    this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                }
                else
                {
                    ShowErrorMessage("删除失败！");
                }
            }
            catch (Exception Ex)
            {

                ShowErrorMessage(Ex);
            }
        }

        private void ToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            ShowToUI(_currentDisplayInfo);

            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);

            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
        }

        private void ToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            Model.General.ReturnValueInfo saveInfo;

            Sys_SupplierMaster_slm_Info supplierMaster = UIdataToInfo();

            try
            {
                switch (this.EditState)
                {
                    case DefineConstantValue.EditStateEnum.OE_Insert:

                        saveInfo = _supplierMasterBL.Save(supplierMaster, DefineConstantValue.EditStateEnum.OE_Insert);

                        if (saveInfo.boolValue)
                        {
                            GoLast();
                        }
                        else
                        {
                            if (saveInfo.messageText != null && saveInfo.messageText != "")
                            {
                                ShowErrorMessage(saveInfo.messageText);
                            }
                            else 
                            {
                                ShowErrorMessage("保存失败！");
                            }
                        }

                        break;

                    case DefineConstantValue.EditStateEnum.OE_Update:

                        saveInfo = _supplierMasterBL.Save(supplierMaster, DefineConstantValue.EditStateEnum.OE_Update);

                        if (saveInfo.boolValue)
                        {
                            _currentDisplayInfo = _supplierMasterBL.DisplayRecord(_currentDisplayInfo) as Sys_SupplierMaster_slm_Info;

                            ShowToUI(_currentDisplayInfo);

                            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;

                            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);

                        }
                        else
                        {
                            ShowErrorMessage("修改失败！");
                        }

                        break;

                    case DefineConstantValue.EditStateEnum.OE_Delete:

                        break;

                    case DefineConstantValue.EditStateEnum.OE_ReaOnly:

                        break;

                    default:

                        break;
                }
            }
            catch (Exception Ex)
            {

                ShowErrorMessage(Ex);
            }


        }

        private void ToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            SupplierMasterSearch searchFrom = new SupplierMasterSearch();

            searchFrom.ShowForm(_currentDisplayInfo);

            try
            {
                _currentDisplayInfo = _supplierMasterBL.DisplayRecord(_currentDisplayInfo) as Sys_SupplierMaster_slm_Info;

                ShowToUI(_currentDisplayInfo);

                SetToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

    }
}
