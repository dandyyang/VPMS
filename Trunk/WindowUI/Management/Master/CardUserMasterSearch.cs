using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.IBLL.Management.Master;
using BLL.IBLL.General;
using BLL.Factory.Management;
using BLL.Factory.General;
using Model.IModel;
using Common;
using WindowUI.ClassLibrary.Public;
using Model.Management.Master;

namespace WindowUI.Management.Master
{
    public partial class CardUserMasterSearch : BaseForm
    {
        List<Model.IModel.IModelObject> _result;
        ICardUserMasterBL _cardUserMasterBL;
        IGeneralBL _generalBL;
        bool lFullSelect;
        bool lisGraduation;
        public List<CardUserMaster_cus_Info> _graduationList;

        public CardUserMaster_cus_Info _info;
        public string displayRecordID;
        public List<CardUserMaster_cus_Info> _listinfo;
        public bool _check;

        public bool _isTeacher;

        #region 2012-04-27 LunLin
        private bool _isMeeting = false;
        #endregion

        public CardUserMasterSearch()
        {
            InitializeComponent();
            this._cardUserMasterBL = MasterBLLFactory.GetBLL<ICardUserMasterBL>(MasterBLLFactory.CardUserMaster);
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
            lFullSelect = false;
            this._result = new List<IModelObject>();
            this._info = null;
            this.displayRecordID = string.Empty;
            this._listinfo = null;
            this._check = false;
            this._isTeacher = false;
            this.lisGraduation = false;
            this._graduationList = null;

            this.TabText = "打印卡片";
            this.Text = "打印卡片";

        }

        public void ShowForm(CardUserMaster_cus_Info info)
        {
            _info = info;
            btnPrinter.Visible = false;
            cboModel.Visible = false;
            btnSelect.Visible = true;
            btnSelectAll.Visible = false;
            cboModel.Visible = false;
            lvwMstr.CheckBoxes = false;
            lvwMstr.Columns[0].Width = 0;
            label4.Visible = btnPrinter.Visible;

            this.TabText = "卡用户搜索";
            this.Text = "卡用户搜索";

            this.ShowDialog();
        }

        public void SelectMulitUser(List<CardUserMaster_cus_Info> list)
        {
            _listinfo = list;
            btnPrinter.Visible = false;
            cboModel.Visible = false;
            label4.Visible = false;
            this.ShowDialog();
        }

        public void GraduationForm(List<CardUserMaster_cus_Info> returnList)
        {
            this._graduationList = returnList;

            lisGraduation = true;
            btnGraduation.Visible = false;
            label4.Visible = false;
            cboModel.Visible = false;
            btnSelectAll.Visible = false;
            btnPrinter.Visible = false;

            btnSelectM.Visible = false;
            btnSelectGraduationStudent.Visible = true;

            this.ShowDialog();

        }

        public void PrintForm(CardUserMaster_cus_Info info)
        {
            _info = info;
            btnPrinter.Visible = true;
            cboModel.Visible = true;
            cboModel.SetDataSource(_generalBL.GetMasterDataInformations(DefineConstantValue.MasterType.IDCardPrinterModel));
            btnSelect.Visible = false;
            lvwMstr.CheckBoxes = true;
            lvwMstr.Columns[0].Width = 25;
            btnSelectAll.Visible = true;
            label4.Visible = btnPrinter.Visible;

            this.ShowDialog();
        }

        public void SelectAll()
        {
            //_listinfo = new List<CardUserMaster_cus_Info>();            
            lvwMstr.MultiSelect = true;
            this.ShowDialog();
        }

        #region 2011-10-14
        public void SelectTeacher()
        {
            _isTeacher = true;
            this.ShowDialog();
        }
        #endregion


        #region 2012-04-27 LunLin
        public void SelectMeeting()
        {
            _isMeeting = true;
            lvwMstr.MultiSelect = true;
            btnPrinter.Visible = false;
            btnSelect.Visible = true;
            this.ShowDialog();
        }
        #endregion


        private void BindCombox(DefineConstantValue.MasterType mType)
        {
            List<IModelObject> result = new List<IModelObject>();
            try
            {
                if (mType == DefineConstantValue.MasterType.SchoolIDCardModel)
                {
                    result = _generalBL.GetMasterDataInformations(mType, System.Configuration.ConfigurationManager.AppSettings["ClientKey"].ToString());
                }
                else
                {
                    result = _generalBL.GetMasterDataInformations(mType);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            switch (mType)
            {
                case DefineConstantValue.MasterType.CardUserSex:
                    //cbcSexNum.SetDataSource(result);
                    //cbcSexNum.SelectedValue = "";
                    break;
                case DefineConstantValue.MasterType.SchoolMaster:
                    cbcSchool.SetDataSource(result);
                    cbcSchool.SelectedValue = "";
                    break;
                case DefineConstantValue.MasterType.CardUserIdentity:
                    cbcIdentityNum.SetDataSource(result);
                    cbcIdentityNum.SelectedValue = "";
                    break;
                case DefineConstantValue.MasterType.SpecialtyMaster:
                    cbcSpecialty.SetDataSource(result);
                    cbcSpecialty.SelectedValue = "";
                    break;
                case DefineConstantValue.MasterType.CardUserClass:
                    cbcClass.SetDataSource(result);
                    cbcClass.SelectedValue = "";
                    break;
                case DefineConstantValue.MasterType.DepartmentMaster:
                    cbcDepartment.SetDataSource(result);
                    cbcDepartment.SelectedValue = "";
                    break;
                case DefineConstantValue.MasterType.SchoolIDCardModel:

                    break;
                case DefineConstantValue.MasterType.IDCardPrinterModel:
                    cboModel.SetDataSource(result);
                    cboModel.SelectedValue = "";
                    break;
                default:
                    break;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            #region 2012-04-27 LunLin
            if (_isMeeting)
                btnSelect.Enabled = true;
            #endregion


            CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();

            if (txtcNumber.Text.Trim() != "")
            {
                info.cus_cNumber = txtcNumber.Text.Trim();
            }
            if (txtcChaName.Text.Trim() != "")
            {
                info.cus_cChaName = txtcChaName.Text.Trim();
            }
            if (txtcEngName.Text.Trim() != "")
            {
                info.cus_cEngName = txtcEngName.Text.Trim();
            }
            if (txtcStudentID.Text.Trim() != "")
            {
                info.cus_cStudentId = txtcStudentID.Text.Trim();
            }
            //if (txtcSMSReceivePhone.Text.Trim() != "")
            //{
            //    info.cus_cSMSReceivePhone = txtcSMSReceivePhone.Text.Trim();
            //}
            //if (txtcMailAddress.Text.Trim() != "")
            //{
            //    info.cus_cMailAddress = txtcMailAddress.Text.Trim();
            //}
            if (txtdGraduationPeriod.Text.Trim() != "")
            {
                info.cus_cGraduationPeriod = txtdGraduationPeriod.Text.Trim();
            }

            //if (cbcSexNum.SelectedValue != null) 
            //{
            //    info.cus_cSexNum = cbcSexNum.SelectedValue.ToString();
            //}
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
            if (cbcClass.SelectedValue != null)
            {
                info.cus_cClassNum = cbcClass.SelectedValue.ToString();
            }
            if (cbcSpecialty.SelectedValue != null)
            {
                info.cus_cSpecialtyNum = cbcSpecialty.SelectedValue.ToString();
            }
            try
            {
                _result = _cardUserMasterBL.SearchRecords(info);
                //bindData(_result);
                //lvwMstr.SetDataSource(_result);
                if (_result != null)
                {
                    List<CardUserMaster_cus_Info> listCusInfo = new List<CardUserMaster_cus_Info>();
                    foreach (var item in _result)
                    {
                        CardUserMaster_cus_Info cusInfo = item as CardUserMaster_cus_Info;
                        if (cusInfo != null)
                        {
                            listCusInfo.Add(cusInfo);
                        }
                    }
                    lvwMstr.SetDataSource<CardUserMaster_cus_Info>(listCusInfo);
                    lbliCount.Text = listCusInfo.Count.ToString();
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void CardUserMasterSearch_Load(object sender, EventArgs e)
        {


            #region 2011-9-7
            if (_check)
            {
                lvwMstr.MultiSelect = false;
                btnPrinter.Visible = false;

                btnSelect.Visible = true;
            }
            #endregion
            try
            {
                BindCombox(DefineConstantValue.MasterType.CardUserSex);
                BindCombox(DefineConstantValue.MasterType.CardUserIdentity);
                BindCombox(DefineConstantValue.MasterType.SchoolMaster);
                BindCombox(DefineConstantValue.MasterType.SpecialtyMaster);
                BindCombox(DefineConstantValue.MasterType.CardUserClass);
                BindCombox(DefineConstantValue.MasterType.DepartmentMaster);
                BindCombox(DefineConstantValue.MasterType.IDCardPrinterModel);

            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
            if (_isTeacher)
            {
                lvwMstr.DoubleClick -= lvwMstr_DoubleClick;

                lvwMstr.MultiSelect = true;
                lvwMstr.Columns[0].Width = 0;
                btnSelect.Visible = true;
                btnPrinter.Visible = false;
                btnSelectAll.Visible = false;

                txtcNumber.Enabled = false;
                txtcChaName.Enabled = false;
                txtcEngName.Enabled = false;
                txtdGraduationPeriod.Enabled = false;
                cbcClass.Enabled = false;
                cbcDepartment.Enabled = false;
                cbcIdentityNum.Enabled = false;
                cbcSchool.Enabled = false;
                cbcSpecialty.Enabled = false;
                cbcIdentityNum.SelectedValue = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff;
            }

            cboModel.Visible = btnPrinter.Visible;
            label4.Visible = btnPrinter.Visible;

            if (lisGraduation)
            {
                cbcDepartment.Enabled = false;
                cbcIdentityNum.SelectedValue = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Student;
                cbcIdentityNum.Enabled = false;
                if (cbcSchool.Items.Count == 1)
                {

                }
            }
        }

        private void bindData(List<Model.IModel.IModelObject> list)
        {
            CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();
            lvwMstr.Items.Clear();
            foreach (var t in list)
            {
                info = t as CardUserMaster_cus_Info;
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = info.cus_iRecordID.ToString();
                li.SubItems.Add(info.cus_cNumber);
                li.SubItems.Add(info.cus_cStudentId);
                li.SubItems.Add(info.cus_cChaName);
                li.SubItems.Add(info.cus_cEngName);
                li.SubItems.Add(info.cus_cSexNum);
                li.SubItems.Add(info.cus_cIdentityNum);
                li.SubItems.Add(info.cus_cSchoolNum);
                li.SubItems.Add(info.cus_cDepartmentNum);
                li.SubItems.Add(info.cus_cClassNum);
                li.SubItems.Add(info.cus_cSpecialtyNum);
                li.SubItems.Add(info.cus_cGraduationPeriod);
                li.SubItems.Add(info.cus_cSMSReceivePhone);
                li.SubItems.Add(info.cus_cMailAddress);
                lvwMstr.Items.Add(li);
            }
            lbliCount.Text = list.Count().ToString();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            #region 2011-9-7
            if (_check)
            {
                CardUserMaster_cus_Info smcitem = null;
                _listinfo = new List<CardUserMaster_cus_Info>();
                try
                {
                    if (lvwMstr.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < lvwMstr.SelectedItems.Count; i++)
                        {
                            smcitem = new CardUserMaster_cus_Info();
                            smcitem.cus_cNumber = lvwMstr.SelectedItems[i].SubItems[1].Text;
                            smcitem.cus_cChaName = lvwMstr.SelectedItems[i].SubItems[2].Text;

                            //smcitem.usm_cChaName = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.usm_cChaName].Text;
                            _listinfo.Add(smcitem);
                        }
                    }

                }
                catch (Exception Ex)
                { ShowErrorMessage(Ex); }

                this.DialogResult = DialogResult.OK;
            }

            #endregion
            else
            {


                if (lvwMstr.MultiSelect != true)
                    selectData();
                else
                {
                    CardUserMaster_cus_Info smcitem = null;
                    _listinfo = new List<CardUserMaster_cus_Info>();
                    try
                    {
                        /*Modify By Leoth LunLin 2012-04-27*/
                        if (!_isMeeting)
                        {
                            if (lvwMstr.SelectedItems.Count > 0)
                            {
                                for (int i = 0; i < lvwMstr.SelectedItems.Count; i++)
                                {
                                    smcitem = new CardUserMaster_cus_Info();

                                    smcitem.cus_cNumber = lvwMstr.SelectedItems[i].SubItems[1].Text;
                                    smcitem.cus_cChaName = lvwMstr.SelectedItems[i].SubItems[2].Text;
                                    smcitem.cus_cSMSReceivePhone = lvwMstr.SelectedItems[i].SubItems[11].Text;
                                    smcitem.cus_cDepartmentNum = lvwMstr.SelectedItems[i].SubItems[7].Text;
                                    //smcitem.usm_cChaName = lvwMstr.SelectedItems[i].SubItems[(int)enmLvwMaster.usm_cChaName].Text;
                                    if (_isTeacher)
                                        smcitem.cus_cIdentityNum = lvwMstr.SelectedItems[i].SubItems[7].Text;


                                    _listinfo.Add(smcitem);
                                }
                            }
                        }
                        /*Add By Leoth LunLin 2012-04-27*/
                        else
                        {
                            for (int i = 0; i < lvwMstr.CheckedItems.Count; i++)
                            {
                                smcitem = new CardUserMaster_cus_Info();
                                smcitem.cus_cNumber = lvwMstr.CheckedItems[i].SubItems[1].Text;
                                smcitem.cus_cChaName = lvwMstr.CheckedItems[i].SubItems[2].Text;
                                smcitem.cus_cEngName = lvwMstr.CheckedItems[i].SubItems[3].Text;
                                smcitem.cus_cIdentityNum = lvwMstr.CheckedItems[i].SubItems[5].Text;
                                smcitem.cus_cSchool = lvwMstr.CheckedItems[i].SubItems[6].Text;
                                smcitem.cus_cDepartment = lvwMstr.CheckedItems[i].SubItems[7].Text;
                                smcitem.cus_cClass = lvwMstr.CheckedItems[i].SubItems[8].Text;
                                smcitem.cus_cSpecialty = lvwMstr.CheckedItems[i].SubItems[9].Text;
                                smcitem.cus_cGraduationPeriod = lvwMstr.CheckedItems[i].SubItems[10].Text;

                                _listinfo.Add(smcitem);
                            }
                        }

                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }

                    this.DialogResult = DialogResult.OK;
                }


            }
        }

        private void selectData()
        {
            try
            {
                if (lvwMstr.SelectedItems.Count > 0)
                {
                    displayRecordID = lvwMstr.SelectedItems[0].Text;

                    CardUserMaster_cus_Info masterInfo = new CardUserMaster_cus_Info();
                    masterInfo.RecordID = Convert.ToInt32(lvwMstr.SelectedItems[0].Text);
                    masterInfo.cus_iRecordID = Convert.ToInt32(lvwMstr.SelectedItems[0].Text);
                    Model.IModel.IModelObject result = _cardUserMasterBL.DisplayRecord(masterInfo);
                    //_info = new CardUserMaster_cus_Info();
                    if (result != null)
                    {
                        try
                        {
                            General.SetDataToLingQEntity<CardUserMaster_cus_Info>(_info, result);
                        }
                        catch (Exception Ex)
                        {
                            ShowErrorMessage(Ex);
                        }
                    }
                }
            }
            catch (Exception exx)
            {
                ShowErrorMessage(exx);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lvwMstr_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = true;
        }

        private void lvwMstr_DoubleClick(object sender, EventArgs e)
        {
            if (!_check)
            {
                selectData();
            }
        }

        private void btnPrinter_Click(object sender, EventArgs e)
        {
            if (cboModel.SelectedValue != null)
            {
            }
            else
            {
                MessageBox.Show("请选择模板！");
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            lFullSelect = !lFullSelect;
            foreach (ListViewItem t in lvwMstr.Items)
            {
                if (lFullSelect == true)
                {
                    t.Checked = true;
                }
                else
                {
                    t.Checked = false;

                }
            }
            if (lFullSelect == true)
            {
                btnSelectAll.Text = "全部取消选择";
            }
            else
            {
                btnSelectAll.Text = "全部选择";
            }
        }

        private void btnGraduation_Click(object sender, EventArgs e)
        {
            List<CardUserMaster_cus_Info> _GraduationList = new List<CardUserMaster_cus_Info>();
            foreach (ListViewItem li in lvwMstr.CheckedItems)
            {
                foreach (CardUserMaster_cus_Info t in _result)
                {
                    if (t.cus_iRecordID.ToString() == li.Text)
                    {
                        _GraduationList.Add(t);
                    }
                }
            }

            bool isSuccess;
            try
            {
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void btnSelectM_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in lvwMstr.CheckedItems)
            {
                foreach (CardUserMaster_cus_Info t in _result)
                {
                    if (t.cus_iRecordID.ToString() == li.Text)
                    {
                        _listinfo.Add(t);
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSelectGraduationStudent_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem li in lvwMstr.CheckedItems)
            {
                foreach (CardUserMaster_cus_Info t in _result)
                {
                    if (t.cus_iRecordID.ToString() == li.Text)
                    {
                        this._graduationList.Add(t);
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
