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
using BLL.IBLL.Management.Master;
using WindowUI.ClassLibrary.Public;
using Common;
using Common.DataTypeVerify;
using BLL.IBLL.General;
using BLL.Factory.General;
using Model.General;
using System.IO;
using WindowUI.Management.Common;
using BLL.IBLL.Management.DeleteData;
using Model.Management.DeleteData;
using Common.FileMgtService;
using WindowUI.Management.LocalGeneral;
using System.Threading;

namespace WindowUI.Management.Master
{
    public partial class CardUserMaster : BaseForm
    {
        ICardUserMasterBL _cardUserMasterBL;
        IGeneralBL _generalBL;
        ISiteMasterBL _siteBL;
        string UserImage = string.Empty;
        int fileSize;
        CardUserMaster_cus_Info info_Public;

        #region MyRegion
        //記錄基本信息
        string UserSex = string.Empty;
        string UserIdentity = string.Empty;
        string UserSchool = string.Empty;
        string UserDepartment = string.Empty;
        string UserSpecialty = string.Empty;
        string UesrGraduationPeriod;
        string UserClass = string.Empty;
        bool UserValid;
        //宿舍地点
        string dormitorySiteNum = string.Empty;
        string groupNum = string.Empty;

        string cus_iRecordID;
        string displayRecordID;
        #endregion


        private OpenFileDialog importDialog;
        private Thread invokeOpenDialogThread;
        private DialogResult result;

        public CardUserMaster()
        {
            InitializeComponent();
            this._cardUserMasterBL = MasterBLLFactory.GetBLL<ICardUserMasterBL>(MasterBLLFactory.CardUserMaster);
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
            this._siteBL = MasterBLLFactory.GetBLL<ISiteMasterBL>(MasterBLLFactory.SiteMaster);
        }

        private void CardUserMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            CardUserMaster_cus_Info info = null;
            try
            {
                SetControlLength();
                BindCombox(DefineConstantValue.MasterType.CardUserSex);
                BindCombox(DefineConstantValue.MasterType.CardUserIdentity);
                BindCombox(DefineConstantValue.MasterType.SchoolMaster);
                BindCombox(DefineConstantValue.MasterType.SpecialtyMaster);
                BindCombox(DefineConstantValue.MasterType.CardUserClass);
                BindCombox(DefineConstantValue.MasterType.DepartmentMaster);
                BindCombox(DefineConstantValue.MasterType.GoToSchoolType);
                cus_iRecordID = "0";
                info = _cardUserMasterBL.GetRecord_Last();
                showData(info);
                txtcNumber.TextBoxSetStatus(true);
                setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                if (cus_iRecordID == "0")
                {
                    setNullDataStatc();
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void BindCombox(DefineConstantValue.MasterType mType)
        {
            List<IModelObject> result = new List<IModelObject>();
            try
            {
                result = _generalBL.GetMasterDataInformations(mType);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            switch (mType)
            {
                case DefineConstantValue.MasterType.CardUserSex:
                    cbcSexNum.SetDataSource(result);
                    cbcSexNum.Text = "";
                    break;
                case DefineConstantValue.MasterType.SchoolMaster:
                    cbcSchool.SetDataSource(result);
                    break;
                case DefineConstantValue.MasterType.CardUserIdentity:
                    cbcIdentityNum.SetDataSource(result);
                    cbcIdentityNum.Text = "";
                    break;
                case DefineConstantValue.MasterType.SpecialtyMaster:
                    cbcSpecialty.SetDataSource(result);
                    cbcSpecialty.Text = "";
                    break;
                case DefineConstantValue.MasterType.CardUserClass:
                    cbcClass.SetDataSource(result);
                    cbcClass.Text = "";
                    break;
                case DefineConstantValue.MasterType.DepartmentMaster:
                    cbcDepartment.SetDataSource(result);
                    cbcDepartment.Text = "";
                    break;

                case DefineConstantValue.MasterType.GoToSchoolType:
                    cbcGotoSchoolType.SetDataSource(result);
                    //cbcGotoSchoolType.Text = "";
                    break;
                default:
                    break;
            }
        }

        private void SetControlStatus(DefineConstantValue.EditStateEnum editStatus)
        {
            switch (editStatus)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    this.txtcNumber.TextBoxSetStatus(true);
                    this.txtcChaName.TextBoxSetStatus(true);
                    this.txtcEngName.TextBoxSetStatus(true);
                    this.txtcSMSReceivePhone1.TextBoxSetStatus(true);
                    this.txtcSMSReceivePhone2.TextBoxSetStatus(true);
                    this.txtcSMSReceivePhone3.TextBoxSetStatus(true);
                    this.txtcSMSReceivePhone4.TextBoxSetStatus(true);

                    this.txtStudentID.TextBoxSetStatus(true);
                    cbcGotoSchoolType.Enabled = false;
                    ckbCashPay.Enabled = false;

                    this.txtcMailAddress.TextBoxSetStatus(true);
                    this.txtPosition.TextBoxSetStatus(true);
                    this.txtcBedNum.TextBoxSetStatus(true); //add by justinleung 2011/09/05

                    this.cbcSexNum.ComboBoxSetStatc(true);
                    this.cbcIdentityNum.ComboBoxSetStatc(true);
                    this.cbcSchool.ComboBoxSetStatc(true);
                    this.cbcDepartment.ComboBoxSetStatc(true);
                    this.cbcSpecialty.ComboBoxSetStatc(true);
                    this.cbcClass.ComboBoxSetStatc(true);

                    this.dtpGraduationPeriod.Enabled = false;
                    this.ckbiIsSendSMS.Enabled = false;
                    this.ckbiIsSendMail.Enabled = false;
                    this.ckbiValid.Enabled = false;
                    this.chkSet.Enabled = true;
                    this.bntcOwenPic.Enabled = false;

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

                    btnCouseAdd.Enabled = false;
                    btnCouseDel.Enabled = false;
                    lvwCouseList.Enabled = true;
                    btnSelectSite.Enabled = false;
                    btnSelectGroup.Enabled = false;

                    tbPhone1.Enabled = false;
                    tbPhone2.Enabled = false;
                    tbPhone3.Enabled = false;
                    tbPhone4.Enabled = false;

                    btnUpdateClass.Enabled = true;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    this.txtcNumber.TextBoxSetStatus(true);
                    this.txtcChaName.TextBoxSetStatus(false);
                    this.txtcEngName.TextBoxSetStatus(false);
                    this.txtcSMSReceivePhone1.TextBoxSetStatus(false);
                    this.txtcSMSReceivePhone2.TextBoxSetStatus(false);
                    this.txtcSMSReceivePhone3.TextBoxSetStatus(false);
                    this.txtcSMSReceivePhone4.TextBoxSetStatus(false);

                    this.txtStudentID.TextBoxSetStatus(false);
                    cbcGotoSchoolType.Enabled = true;
                    ckbCashPay.Enabled = true;

                    this.txtcMailAddress.TextBoxSetStatus(false);
                    this.txtPosition.TextBoxSetStatus(false);
                    this.txtcBedNum.TextBoxSetStatus(false);   //add by justinleung 2011/09/05

                    this.cbcSexNum.Enabled = true;
                    this.cbcIdentityNum.Enabled = true;
                    this.cbcSchool.Enabled = true;
                    this.cbcDepartment.Enabled = true;
                    this.dtpGraduationPeriod.Enabled = true;
                    this.cbcClass.Enabled = true;
                    this.ckbiIsSendSMS.Enabled = true;
                    this.ckbiValid.Enabled = true;
                    this.chkSet.Enabled = false;
                    this.bntcOwenPic.Enabled = true;
                    this.cbcSpecialty.Enabled = true;
                    this.ckbiIsSendMail.Enabled = true;


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

                    btnCouseAdd.Enabled = true;
                    btnCouseDel.Enabled = true;
                    btnSelectSite.Enabled = true;
                    btnSelectGroup.Enabled = true;

                    tbPhone1.Enabled = true;
                    tbPhone2.Enabled = true;
                    tbPhone3.Enabled = true;
                    tbPhone4.Enabled = true;

                    btnUpdateClass.Enabled = false;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    this.txtcNumber.TextBoxSetStatus(false);
                    this.txtcChaName.TextBoxSetStatus(false);
                    this.txtcEngName.TextBoxSetStatus(false);
                    this.txtcSMSReceivePhone1.TextBoxSetStatus(false);
                    this.txtcSMSReceivePhone2.TextBoxSetStatus(false);
                    this.txtcSMSReceivePhone3.TextBoxSetStatus(false);
                    this.txtcSMSReceivePhone4.TextBoxSetStatus(false);

                    this.txtStudentID.TextBoxSetStatus(false);
                    cbcGotoSchoolType.Enabled = true;
                    ckbCashPay.Enabled = true;

                    this.txtcMailAddress.TextBoxSetStatus(false);
                    this.txtPosition.TextBoxSetStatus(false);
                    this.txtcBedNum.TextBoxSetStatus(false);   //add by justinleung 2011/09/05
                    this.cbcSexNum.Enabled = true;
                    this.cbcIdentityNum.Enabled = true;
                    this.cbcSchool.Enabled = true;
                    this.cbcDepartment.Enabled = true;
                    this.dtpGraduationPeriod.Enabled = true;
                    this.cbcClass.Enabled = true;
                    this.ckbiIsSendSMS.Enabled = true;
                    this.ckbiValid.Enabled = true;
                    this.chkSet.Enabled = true;
                    this.bntcOwenPic.Enabled = true;
                    this.cbcSpecialty.Enabled = true;
                    this.ckbiIsSendMail.Enabled = true;

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

                    btnCouseAdd.Enabled = true;
                    btnCouseDel.Enabled = true;
                    btnSelectSite.Enabled = true;
                    btnSelectGroup.Enabled = true;

                    tbPhone1.Enabled = true;
                    tbPhone2.Enabled = true;
                    tbPhone3.Enabled = true;
                    tbPhone4.Enabled = true;

                    btnUpdateClass.Enabled = false;
                    break;
                default:

                    break;
            }
        }

        private void SetControlLength()
        {
            CardUserMaster_cus_Info info = null;
            try
            {
                info = this._cardUserMasterBL.GetTableFieldLenght() as CardUserMaster_cus_Info;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (info != null)
            {
                this.txtcNumber.MaxLength = info.cus_cNumber_Length;
                this.txtcChaName.MaxLength = info.cus_cChaName_Length;
                this.txtcEngName.MaxLength = info.cus_cEngName_Length;
                this.txtcSMSReceivePhone1.MaxLength = info.cus_cSMSReceivePhone_Length;
                this.txtcMailAddress.MaxLength = info.cus_cMailAddress_Length;
            }
        }

        private void BindListView()
        {
        }

        //處理頁面數據顯示
        private void showData(CardUserMaster_cus_Info info)
        {
            if (info != null)
            {
                info_Public = info;

                //顯視數據
                #region MyRegion
                cus_iRecordID = info.cus_iRecordID.ToString();
                txtcNumber.Text = info.cus_cNumber;
                txtcChaName.Text = info.cus_cChaName;
                txtcEngName.Text = info.cus_cEngName;
                txtcSMSReceivePhone1.Text = info.cus_cSMSReceivePhone;
                txtcSMSReceivePhone2.Text = info.cus_cAppendPhone1;
                txtcSMSReceivePhone3.Text = info.cus_cAppendPhone2;
                txtcSMSReceivePhone4.Text = info.cus_cAppendPhone3;

                txtStudentID.Text = info.cus_cStudentId;
                cbcGotoSchoolType.SelectedValue = info.cus_cGotoSchoolType;
                ckbCashPay.Checked = info.cus_lCashPay;

                txtcAdd.Text = info.cus_cAdd;
                txtcMailAddress.Text = info.cus_cMailAddress;
                txtdAddDate.Text = info.cus_dAddDate != null ? info.cus_dAddDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                txtcLast.Text = info.cus_cLast;
                txtdLastDate.Text = info.cus_dLastDate != null ? info.cus_dLastDate.Value.ToString(DefineConstantValue.gc_DateFormat) : "";
                txtPosition.Text = info.cus_cPosition;

                ckbiValid.Checked = info.cus_lValid;
                ckbiIsSendSMS.Checked = info.cus_lIsSendSMS;
                ckbiIsSendMail.Checked = info.cus_lIsSendEmail;

                cbcSexNum.SelectedValue = info.cus_cSexNum;
                cbcIdentityNum.SelectedValue = info.cus_cIdentityNum;
                cbcSchool.SelectedValue = info.cus_cSchoolNum;
                cbcDepartment.SelectedValue = info.cus_cDepartmentNum;
                if (info.cus_cGraduationPeriod.Trim() != "")
                {
                    dtpGraduationPeriod.Value = Convert.ToDateTime(info.cus_cGraduationPeriod + "/1/1");
                }
                cbcClass.SelectedValue = info.cus_cClassNum;
                cbcSpecialty.SelectedValue = info.cus_cSpecialtyNum;


                if (info.cus_cDormitorySiteNum != string.Empty)
                {
                    IModelObject cond = new SiteMaster_stm_Info { stm_cNumber = info.cus_cDormitorySiteNum };
                    var stmInfos = _siteBL.SearchRecords(cond);
                    if (stmInfos.Count() == 0)
                    {
                        txtcDormitorySiteName.Text = "";
                    }
                    else
                    {
                        SiteMaster_stm_Info stmInfo = stmInfos[0] as SiteMaster_stm_Info;
                        txtcDormitorySiteName.Text = stmInfo.BuildingName + "--" + stmInfo.stm_cName;
                        dormitorySiteNum = stmInfo.stm_cNumber;
                    }
                }
                else
                {
                    txtcDormitorySiteName.Text = info.cus_cDormitorySiteName;
                }
                //add by justinleung 2011/09/05 床位
                txtcBedNum.Text = info.cus_cBedNum;

                if (info.cus_cGroupNum != string.Empty)
                {

                }
                else
                {
                    txtcGroupName.Text = "";
                }
                #endregion

                try
                {
                    if (info.byte_cus_imgPhoto != null)
                    {
                        #region
                        //Byte[] l_b = info.byte_cus_imgPhoto;
                        //if (l_b.Length > 0)
                        //{
                        //    MemoryStream MS = new MemoryStream(l_b);
                        //    this.pictureBoxOwerpic.Image = Image.FromStream(MS);
                        //}
                        //else
                        //{
                        //    this.pictureBoxOwerpic.Image = null;
                        //} 
                        #endregion


                        //FileMgtSoapClient soapClient = WebSrvFactory.GetFileMgt();
                        //Byte[] l_b = soapClient.GetFileBytes(info.cus_guidPhotoKey);
                        if (info.byte_cus_imgPhoto != null && info.byte_cus_imgPhoto.Length > 0)
                        {
                            MemoryStream MS = new MemoryStream(info.byte_cus_imgPhoto);
                            this.pictureBoxOwerpic.Image = Image.FromStream(MS);
                        }
                        else
                        {
                            this.pictureBoxOwerpic.Image = null;
                        }
                    }
                    else
                    {
                        this.pictureBoxOwerpic.Image = null;
                    }
                }
                catch
                {
                    this.pictureBoxOwerpic.Image = null;
                }
                //亲情号码
                if (info.CardUserPhoneNum != null)
                {
                    tbPhone1.Text = (info.CardUserPhoneNum.cup_Phone1 != null ? info.CardUserPhoneNum.cup_Phone1 : "");
                    tbPhone2.Text = (info.CardUserPhoneNum.cup_Phone2 != null ? info.CardUserPhoneNum.cup_Phone2 : "");
                    tbPhone3.Text = (info.CardUserPhoneNum.cup_Phone3 != null ? info.CardUserPhoneNum.cup_Phone3 : "");
                    tbPhone4.Text = (info.CardUserPhoneNum.cup_Phone4 != null ? info.CardUserPhoneNum.cup_Phone4 : "");
                }
                else
                {
                    tbPhone1.Text = "";
                    tbPhone2.Text = "";
                    tbPhone3.Text = "";
                    tbPhone4.Text = "";
                }
                SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            }
            else
            {

            }




        }

        private void setToolBarViewStatc(DefineConstantValue.GetReocrdEnum statc)
        {
            switch (statc)
            {
                case DefineConstantValue.GetReocrdEnum.GR_First:
                    ToolBar.BtnFirstEnabled = false;
                    ToolBar.BtnPreviousEnabled = false;
                    ToolBar.BtnNextEnabled = true;
                    ToolBar.BtnLastEnabled = true;
                    break;

                case DefineConstantValue.GetReocrdEnum.GR_Last:
                    ToolBar.BtnFirstEnabled = true;
                    ToolBar.BtnPreviousEnabled = true;
                    ToolBar.BtnNextEnabled = false;
                    ToolBar.BtnLastEnabled = false;
                    break;

                case DefineConstantValue.GetReocrdEnum.GR_Middle:
                    ToolBar.BtnFirstEnabled = true;
                    ToolBar.BtnPreviousEnabled = true;
                    ToolBar.BtnNextEnabled = true;
                    ToolBar.BtnLastEnabled = true;
                    break;
                case DefineConstantValue.GetReocrdEnum.GR_Null:
                    ToolBar.BtnFirstEnabled = false;
                    ToolBar.BtnPreviousEnabled = false;
                    ToolBar.BtnNextEnabled = false;
                    ToolBar.BtnLastEnabled = false;
                    break;
                default: break;
            }
        }

        private void setNullDataStatc()
        {
            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Null);
            ToolBar.BtnModifyEnabled = false;
            ToolBar.BtnDeleteEnabled = false;
            ToolBar.BtnModifyEnabled = false;
            ToolBar.BtnSearchEnabled = false;
            ToolBar.BtnNewEnabled = true;

            cbcSpecialty.SelectedValue = "";
            cbcSexNum.SelectedValue = "";
            cbcIdentityNum.SelectedValue = "";
            cbcSchool.SelectedValue = "";
            cbcDepartment.SelectedValue = "";
            dtpGraduationPeriod.Value = DateTime.Now;
            cbcClass.SelectedValue = "";

            txtcNumber.Text = "";
            txtcChaName.Text = "";
            txtcEngName.Text = "";
            txtcMailAddress.Text = "";
            txtcSMSReceivePhone1.Text = "";
            txtcSMSReceivePhone2.Text = "";
            txtcSMSReceivePhone3.Text = "";
            txtcSMSReceivePhone4.Text = "";
            txtcMailAddress.Text = "";

            txtStudentID.Text = "";
            cbcGotoSchoolType.SelectedValue = "";
            ckbCashPay.Checked = false;

            txtcAdd.Text = "";
            txtdAddDate.Text = "";
            txtcLast.Text = "";
            txtdLastDate.Text = "";
            cus_iRecordID = "0";
            tbPhone1.Text = "";
            tbPhone2.Text = "";
            tbPhone3.Text = "";
            tbPhone4.Text = "";


            this.pictureBoxOwerpic.Image = null;
            lvwCouseList.Items.Clear();
        }

        /// <summary>
        /// 首條、尾條、上條、下條事件
        /// </summary>
        #region
        private void ToolBar_BtnPreviousClick(object sender, EventArgs e)
        {
            HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Previous);
        }

        private void ToolBar_BtnNextClick(object sender, EventArgs e)
        {
            HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Next);
        }

        private void ToolBar_BtnFirstClick(object sender, EventArgs e)
        {
            HandelResult_FirstOrLast(DefineConstantValue.GetReocrdEnum.GR_First);
        }

        private void ToolBar_BtnLastClick(object sender, EventArgs e)
        {
            HandelResult_FirstOrLast(DefineConstantValue.GetReocrdEnum.GR_Last);
        }

        private void HandelResult_FirstOrLast(DefineConstantValue.GetReocrdEnum statc)
        {
            CardUserMaster_cus_Info info = null;
            try
            {
                switch (statc)
                {
                    case DefineConstantValue.GetReocrdEnum.GR_First: info = _cardUserMasterBL.GetRecord_First();
                        break;
                    case DefineConstantValue.GetReocrdEnum.GR_Last: info = _cardUserMasterBL.GetRecord_Last();
                        break;
                    default: break;
                }

                //設置ToolBar狀態





                setToolBarViewStatc(statc);

                //數據顯示
                showData(info);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum statc)
        {
            CardUserMaster_cus_Info info = null;
            try
            {
                Model.Base.DataBaseCommandInfo com = new Model.Base.DataBaseCommandInfo();
                switch (statc)
                {
                    case DefineConstantValue.GetReocrdEnum.GR_Next: com.CommandType = Model.Base.DataBaseCommandType.Next;
                        break;
                    case DefineConstantValue.GetReocrdEnum.GR_Previous: com.CommandType = Model.Base.DataBaseCommandType.Previous;
                        break;
                    default:
                        break;
                }

                Model.Base.DataBaseCommandKeyInfo comKey = new Model.Base.DataBaseCommandKeyInfo();
                comKey.KeyValue = cus_iRecordID;
                com.KeyInfoList.Add(comKey);

                switch (statc)
                {
                    case DefineConstantValue.GetReocrdEnum.GR_Next:
                        info = _cardUserMasterBL.GetRecord_Next(com);
                        if (info != null)
                        {
                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                        }
                        else
                        {
                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                        }
                        break;
                    case DefineConstantValue.GetReocrdEnum.GR_Previous:
                        info = _cardUserMasterBL.GetRecord_Previous(com);
                        if (info != null)
                        {
                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                        }
                        else
                        {
                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_First);
                        }
                        break;
                    default:
                        break;
                }

                //顯視數據處理
                showData(info);

            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

        }
        #endregion

        private void ToolBar_BtnNewClick(object sender, EventArgs e)
        {
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Insert);
            this.EditState = DefineConstantValue.EditStateEnum.OE_Insert;
            txtcNumber.Text = "";
            txtcChaName.Text = "";
            txtcEngName.Text = "";
            txtcBedNum.Text = "";//add by justinleung 2011/09/05
            txtcSMSReceivePhone1.Text = "";
            txtcSMSReceivePhone2.Text = "";
            txtcSMSReceivePhone3.Text = "";
            txtcSMSReceivePhone4.Text = "";

            txtStudentID.Text = "";
            cbcGotoSchoolType.SelectedValue = "";
            ckbCashPay.Checked = false;

            txtcMailAddress.Text = "";
            tbPhone1.Text = "";
            tbPhone2.Text = "";
            tbPhone3.Text = "";
            tbPhone4.Text = "";

            lvwCouseList.Items.Clear();

            if (chkSet.Checked)
            {
                //固定基本信息
                ckbiValid.Checked = UserValid;

                cbcSexNum.SelectedValue = UserSex;
                cbcIdentityNum.SelectedValue = UserIdentity;
                cbcSchool.SelectedValue = UserSchool;
                cbcSpecialty.SelectedValue = UserSpecialty;
                cbcDepartment.SelectedValue = UserDepartment;
                dtpGraduationPeriod.Value = Convert.ToDateTime(UesrGraduationPeriod + "/1/1");
                cbcClass.SelectedValue = UserClass;

            }
            else
            {
                ckbiValid.Checked = true;


                cbcSpecialty.SelectedValue = "";
                cbcSexNum.SelectedValue = "";
                cbcIdentityNum.SelectedValue = "";
                cbcSchool.SelectedValue = "";
                cbcDepartment.SelectedValue = "";
                dtpGraduationPeriod.Value = DateTime.Now;
                cbcClass.SelectedValue = "";
            }

            if (cbcIdentityNum.SelectedValue != null)
            {
                SelectIdentity();
            }

            txtcAdd.Text = "";
            txtdAddDate.Text = "";
            txtcLast.Text = "";
            txtdLastDate.Text = "";
            pictureBoxOwerpic.Image = null;

            ckbiIsSendSMS.Checked = false;
            ckbiIsSendMail.Checked = false;
            displayRecordID = cus_iRecordID;
            cus_iRecordID = null;
            ckbiValid.Checked = true;
            UserImage = "";
            txtcNumber.Focus();
        }

        private void ToolBar_BtnSaveClick(object sender, EventArgs e)
        {
            if (txtcNumber.Text == "")
            {
                txtcNumber.Text = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);
            }

            DataTypeVerifyResultInfo vInfo = null;
            vInfo = General.VerifyDataType(txtcNumber.Text, DataType.ChinaChar);
            if (vInfo.IsMatch)
            {
                DataTypeVerifyResultInfo vInfo1 = null;
                DataTypeVerifyResultInfo vInfo2 = null;
                DataTypeVerifyResultInfo vInfo3 = null;
                DataTypeVerifyResultInfo vInfo4 = null;
                vInfo1 = General.VerifyDataType(tbPhone1.Text, DataType.NumberChar);
                vInfo2 = General.VerifyDataType(tbPhone2.Text, DataType.NumberChar);
                vInfo3 = General.VerifyDataType(tbPhone3.Text, DataType.NumberChar);
                vInfo4 = General.VerifyDataType(tbPhone4.Text, DataType.NumberChar);



                if (vInfo1.IsMatch && vInfo2.IsMatch && vInfo3.IsMatch && vInfo4.IsMatch)
                {
                    CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();
                    Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
                    info.cus_cNumber = txtcNumber.Text.Trim();
                    info.cus_cChaName = txtcChaName.Text.Trim();
                    info.cus_cEngName = txtcEngName.Text.Trim();
                    info.cus_cMailAddress = txtcMailAddress.Text.Trim();

                    info.cus_cIdentityNum = "";
                    //add by justinleung 2011/09/05
                    info.cus_cBedNum = txtcBedNum.Text.Trim();
                    info.cus_cPosition = txtPosition.Text;

                    if (cbcSexNum.SelectedValue != null)
                    {
                        info.cus_cSexNum = cbcSexNum.SelectedValue.ToString();
                    }
                    if (cbcIdentityNum.SelectedValue != null)
                    {
                        info.cus_cIdentityNum = cbcIdentityNum.SelectedValue.ToString();
                    }
                    if (cbcSchool.SelectedValue != null)
                    {
                        info.cus_cSchoolNum = cbcSchool.SelectedValue.ToString();
                    }
                    if (cbcDepartment.SelectedValue != null)
                    {
                        info.cus_cDepartmentNum = cbcDepartment.SelectedValue.ToString();
                    }
                    if (cbcSpecialty.SelectedValue != null)
                    {
                        info.cus_cSpecialtyNum = cbcSpecialty.SelectedValue.ToString();
                    }
                    if (cbcClass.SelectedValue != null)
                    {
                        info.cus_cClassNum = cbcClass.SelectedValue.ToString();
                    }

                    if (!string.IsNullOrEmpty(dormitorySiteNum))
                    {
                        info.cus_cDormitorySiteNum = dormitorySiteNum;
                    }

                    if (!string.IsNullOrEmpty(groupNum))
                    {
                        info.cus_cGroupNum = groupNum;
                    }



                    info.cus_cGraduationPeriod = dtpGraduationPeriod.Value.Year.ToString();

                    info.cus_cSMSReceivePhone = txtcSMSReceivePhone1.Text.Trim();
                    info.cus_cAppendPhone1 = txtcSMSReceivePhone2.Text.Trim();
                    info.cus_cAppendPhone2 = txtcSMSReceivePhone3.Text.Trim();
                    info.cus_cAppendPhone3 = txtcSMSReceivePhone4.Text.Trim();

                    info.cus_cStudentId = txtStudentID.Text;
                    info.cus_lCashPay = ckbCashPay.Checked;
                    if (cbcGotoSchoolType.SelectedValue != null)
                    {
                        info.cus_cGotoSchoolType = cbcGotoSchoolType.SelectedValue.ToString();
                    }


                    info.cus_lValid = ckbiValid.Checked;
                    info.cus_lIsSendEmail = ckbiIsSendMail.Checked;
                    info.cus_lIsSendSMS = ckbiIsSendSMS.Checked;

                    //UserImage = @"d:\我的文档\图片收藏\china.jpg";
                    if (UserImage != "" && UserImage != "openFileDialog1")
                    {
                        if (DefineConstantValue.CardUserPicture_MaxSize >= fileSize)
                        {
                            FileStream fs = File.OpenRead(UserImage);
                            Byte[] l_b = new byte[fs.Length];
                            fs.Read(l_b, 0, (int)(fs.Length - 1));
                            info.byte_cus_imgPhoto = l_b;


                            info.PhotoPath = UserImage;
                        }
                    }

                    try
                    {

                        #region 亲情号码设定
                        info.CardUserPhoneNum = new CardUserPhoneNumMaster_cup_Info();
                        info.CardUserPhoneNum.cup_Phone1 = tbPhone1.Text.Trim();
                        info.CardUserPhoneNum.cup_Phone2 = tbPhone2.Text.Trim();
                        info.CardUserPhoneNum.cup_Phone3 = tbPhone3.Text.Trim();
                        info.CardUserPhoneNum.cup_Phone4 = tbPhone4.Text.Trim();
                        #endregion

                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                        {
                            //新增記錄
                            info.cus_cAdd = this.UserInformation.usm_cUserLoginID;
                            info.cus_dAddDate = DateTime.Now;
                            info.cus_cLast = this.UserInformation.usm_cUserLoginID;
                            info.cus_dLastDate = DateTime.Now;

                            msg = _cardUserMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
                        }
                        else
                        {
                            if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                            {
                                //修改記錄
                                info.cus_iRecordID = Convert.ToInt32(cus_iRecordID);
                                info.cus_cLast = this.UserInformation.usm_cUserLoginID;
                                info.cus_dLastDate = DateTime.Now;


                                msg = _cardUserMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                                UserImage = string.Empty;
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }
                    if (!string.IsNullOrEmpty(msg.messageText))
                    {
                        ShowInformationMessage(msg.messageText);
                    }

                    if (msg.boolValue)
                    {
                        SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                        {
                            //新增完成後跳至瀏覽最後一跳記錄



                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Last);
                            info = null;
                            try
                            {
                                SetControlLength();
                                info = _cardUserMasterBL.GetRecord_Last();
                                showData(info);
                                txtcNumber.TextBoxSetStatus(true);
                            }
                            catch (Exception Ex)
                            {
                                ShowErrorMessage(Ex);
                            }
                        }
                        else
                        {
                            //修改完成後跳至瀏覽相應記錄
                            setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);
                            info = _cardUserMasterBL.DisplayRecord(info) as CardUserMaster_cus_Info;
                            txtcNumber.Text = info.cus_cNumber;
                            //txtcName.Text = info.scm_cName;
                            //txtcRemark.Text = info.scm_cRemark;
                            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    else
                    {
                        //編號重復
                        txtcNumber.Focus();
                    }
                }
                else
                {
                    ShowInformationMessage("亲情号码输入有误！");
                }
            }
            else
            {
                ShowInformationMessage("用戶編號不能" + vInfo.Message);
            }


        }

        private void ToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();
            try
            {
                info.cus_iRecordID = Convert.ToInt32(displayRecordID);
                Model.IModel.IModelObject result = _cardUserMasterBL.DisplayRecord(info);
                info = result as CardUserMaster_cus_Info;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            SetControlStatus(this.EditState);
            showData(info);
            if (cus_iRecordID == "0")
            {
                setNullDataStatc();
            }
        }

        private void ToolBar_BtnDeleteClick(object sender, EventArgs e)
        {
            CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();
            info.cus_iRecordID = Convert.ToInt32(cus_iRecordID);
            //msg = _cardUserMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Delete);
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            try
            {
                msg = _cardUserMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Delete);
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_ReaOnly);
            if (!msg.boolValue && msg.messageText != "")
            {
                ShowInformationMessage(msg.messageText);
            }
            else
            {
                HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Next);

                if (cus_iRecordID == info.cus_iRecordID.ToString())
                {
                    HandelResult_PreviousOrNext(DefineConstantValue.GetReocrdEnum.GR_Previous);
                }

                if (cus_iRecordID == info.cus_iRecordID.ToString())
                {
                    setNullDataStatc();
                }
            }

        }

        private void ToolBar_BtnModifyClick(object sender, EventArgs e)
        {
            displayRecordID = cus_iRecordID;
            SetControlStatus(DefineConstantValue.EditStateEnum.OE_Update);
            this.EditState = DefineConstantValue.EditStateEnum.OE_Update;
            if (cbcDepartment.Text == "")
            {
                cbcDepartment.Enabled = false;
            }
            if (cbcSpecialty.Text == "")
            {
                cbcSpecialty.Enabled = false;
            }
            if (cbcClass.Text == "")
            {
                cbcClass.Enabled = false;
            }
            if (cbcIdentityNum.SelectedValue.ToString() == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff)
            {
                dtpGraduationPeriod.Enabled = false;
            }

        }

        private void ToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            CardUserMasterSearch win = new CardUserMasterSearch();
            CardUserMaster_cus_Info tempInfo = new CardUserMaster_cus_Info();
            win.ShowForm(tempInfo);

            if (win.DialogResult == DialogResult.OK)
            {
                cus_iRecordID = win.displayRecordID;
                CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();
                try
                {

                    info.cus_iRecordID = Convert.ToInt32(cus_iRecordID);
                    Model.IModel.IModelObject result = _cardUserMasterBL.DisplayRecord(info);
                    info = result as CardUserMaster_cus_Info;
                    this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
                    SetControlStatus(this.EditState);

                    showData(info);
                    setToolBarViewStatc(DefineConstantValue.GetReocrdEnum.GR_Middle);

                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }



            }
            win.Dispose();
            win = null;
        }

        private void chkSet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSet.Checked)
            {
                UserSex = (cbcSexNum.SelectedValue != null ? cbcSexNum.SelectedValue.ToString() : "");
                UserIdentity = (cbcIdentityNum.SelectedValue != null ? cbcIdentityNum.SelectedValue.ToString() : "");
                UserSchool = (cbcSchool.SelectedValue != null ? cbcSchool.SelectedValue.ToString() : "");
                UserDepartment = (cbcDepartment.SelectedValue != null ? cbcDepartment.SelectedValue.ToString() : "");
                UserSpecialty = (cbcSpecialty.SelectedValue != null ? cbcSpecialty.SelectedValue.ToString() : "");
                UesrGraduationPeriod = dtpGraduationPeriod.Value.Year.ToString();
                UserClass = (cbcClass.SelectedValue != null ? cbcClass.SelectedValue.ToString() : "");
                UserValid = ckbiValid.Checked;

            }
            else
            {
                UserSex = "";
                UserIdentity = "";
                UserSchool = "";
                UserDepartment = "";
                UserSpecialty = "";
                UesrGraduationPeriod = "";
                UserClass = "";
                UserValid = true;

            }
        }

        private void bntcOwenPic_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            UserImage = openFileDialog1.FileName;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string tempStr = openFileDialog1.FileName.Substring(openFileDialog1.FileName.Length - 6, 6);
            string fileType = tempStr.Substring(tempStr.LastIndexOf(".") + 1, 3).ToLower();


            //判斷上傳文件類型是否允許
            if (DefineConstantValue.CardUserPicture_FileType.IndexOf(fileType) >= 0)
            {
                FileStream fs = File.OpenRead(openFileDialog1.FileName);
                Byte[] l_b = new byte[fs.Length];
                fileSize = l_b.Length / 1024;
                //判斷上傳文件大小是否在允許以內

                if (DefineConstantValue.CardUserPicture_MaxSize >= fileSize)
                {
                    pictureBoxOwerpic.ImageLocation = openFileDialog1.FileName;
                }
                else
                {
                    ShowInformationMessage("圖片大小超過限制--" + DefineConstantValue.CardUserPicture_MaxSize.ToString() + "KB !");
                }
            }
            else
            {
                ShowInformationMessage("請選擇圖片文件!");
            }
        }

        private void cbcIdentityNum_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.EditState != DefineConstantValue.EditStateEnum.OE_ReaOnly)
            {
                if (cbcIdentityNum.SelectedValue != null)
                {
                    SelectIdentity();
                }
            }
        }

        private void SelectIdentity()
        {
            if (cbcIdentityNum.SelectedValue.ToString() == DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff.ToString())
            {
                cbcSpecialty.Enabled = false;
                cbcSpecialty.SelectedValue = "";
                dtpGraduationPeriod.Enabled = false;
                cbcClass.Enabled = false;
                cbcClass.SelectedValue = "";
                cbcDepartment.Enabled = true;

                btnCouseAdd.Enabled = true;
                btnCouseDel.Enabled = true;
            }
            else
            {
                cbcSpecialty.Enabled = true;
                dtpGraduationPeriod.Enabled = true;
                cbcClass.Enabled = true;
                cbcDepartment.Enabled = false;
                cbcDepartment.SelectedValue = "";

                btnCouseAdd.Enabled = false;
                btnCouseDel.Enabled = false;
            }
        }

        private void btnCouseAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnCouseDel_Click(object sender, EventArgs e)
        {

        }

        private void btnDownloadTemp_Click(object sender, EventArgs e)
        {

        }

        private void ToolBar_BtnDataInputClick(object sender, EventArgs e)
        {

        }

        private void ToolBar_BtnDataExportClick(object sender, EventArgs e)
        {



        }

        private void ToolBar_BtnExportTemplateClick(object sender, EventArgs e)
        {

            BaseForm frm = new SelectExpTpt();

            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {

            }
        }

        private void ToolBar_BtnExpCusDataClick(object sender, EventArgs e)
        {

            BaseForm frm = new ExportCumData();

            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {

            }
        }

        private void ToolBar_BtnImportCardUserDataClick(object sender, EventArgs e)
        {
            importDialog = new OpenFileDialog();
            importDialog.Filter = "Excle文件(*.xls)|*.xls|所有文件(*.*)|*.*";
            importDialog.FileName = "";

            invokeOpenDialogThread = new Thread(new ThreadStart(InvokeMethod));
            invokeOpenDialogThread.SetApartmentState(ApartmentState.STA);
            invokeOpenDialogThread.Start();
            invokeOpenDialogThread.Join();



            if (result == DialogResult.OK && importDialog.FileName != "")
            {
                try
                {
                    string fileType = importDialog.FileName.Substring(importDialog.FileName.LastIndexOf(".") + 1, 3).ToLower();
                    if (fileType == "xls" || fileType == "xlsx")
                    {
                        BaseForm frmCardUserInput = new CardUserMasterInput(importDialog.FileName);
                        frmCardUserInput.UserInformation = this.UserInformation;
                        frmCardUserInput.Show();
                    }
                    else
                    {
                        ShowInformationMessage("请选择xls文件!");
                    }
                }
                catch (Exception Ex)
                {
                    ShowErrorMessage(Ex);
                }
            }
        }
        private void InvokeMethod()
        {

            result = importDialog.ShowDialog();
        }

        private void ToolBar_BtnImportCardUserPhotoClick(object sender, EventArgs e)
        {
            BaseForm frm = new ImportCardUserPhoto();

            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {

            }
        }

        private void ToolBar_btnExportCardUserPhotoClick(object sender, EventArgs e)
        {
            BaseForm frm = new ExportCardUserPhoto();

            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {

            }
        }

        private void btnSelectSite_Click(object sender, EventArgs e)
        {
            SiteMaster_stm_Info classroomInfo = new SiteMaster_stm_Info();
            ClassroomSearch frm = new ClassroomSearch();

            frm.ShowForm(classroomInfo);

            if (classroomInfo != null && classroomInfo.stm_cNumber.Trim() != "")
            {
                string classroomName = string.Empty;
                classroomName = ManagementGeneral.GetClassroomName(classroomInfo);
                dormitorySiteNum = classroomInfo.stm_cNumber;
                this.txtcDormitorySiteName.Text = classroomName;
            }
        }

        private void btnSelectGroup_Click(object sender, EventArgs e)
        {


        }

        private void ToolBar_BtnGroupPersonClick(object sender, EventArgs e)
        {
            CardUserMasterMonitorGroup groupFrm = new CardUserMasterMonitorGroup();
            groupFrm.ShowDialog();
        }

        private void txtcSMSReceivePhone1_Leave(object sender, EventArgs e)
        {
            if (this.tbPhone1.Text.Trim() == "")
            {
                tbPhone1.Text = txtcSMSReceivePhone1.Text;
            }
        }

        private void txtcSMSReceivePhone2_Leave(object sender, EventArgs e)
        {
            if (this.tbPhone2.Text.Trim() == "")
            {
                tbPhone2.Text = txtcSMSReceivePhone2.Text;
            }
        }

        private void txtcSMSReceivePhone3_Leave(object sender, EventArgs e)
        {
            if (this.tbPhone3.Text.Trim() == "")
            {
                tbPhone3.Text = txtcSMSReceivePhone3.Text;
            }
        }

        private void txtcSMSReceivePhone4_Leave(object sender, EventArgs e)
        {
            if (this.tbPhone4.Text.Trim() == "")
            {
                tbPhone4.Text = txtcSMSReceivePhone4.Text;
            }
        }

        private void txtcNumber_Leave(object sender, EventArgs e)
        {
            txtStudentID.Text = txtcNumber.Text;
        }

        private void cbcGotoSchoolType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbcGotoSchoolType.SelectedValue != null)
                {
                    if (cbcGotoSchoolType.SelectedValue.ToString() == "Stay")
                    {
                        ckbCashPay.Checked = true;
                    }
                    else
                    {
                        ckbCashPay.Checked = false;
                    }
                }
            }
            catch (Exception Ex)
            {

                ShowErrorMessage(Ex);
            }
        }

        private void btnUpdateClass_Click(object sender, EventArgs e)
        {

        }
    }
}
