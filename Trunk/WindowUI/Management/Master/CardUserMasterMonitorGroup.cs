using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;
using WindowUI.ClassLibrary.Public;
using Model.IModel;
using BLL.Factory.Management;
using BLL.IBLL.Management.Master;
using BLL.IBLL.General;
using BLL.Factory.General;
using Model.General;
using Model.Management.Master;

namespace WindowUI.Management.Master
{
    /// <summary>
    /// 批量设定卡用户的 监控规则组别 窗口
    /// </summary>
    public partial class CardUserMasterMonitorGroup : BaseForm
    {
        ICardUserMasterBL _cardUserMasterBL;
        IGeneralBL _generalBL;

        private string _defaultGroup;
        public CardUserMasterMonitorGroup()
        {
            InitializeComponent();
            SetFormSelectState();

            this._cardUserMasterBL = MasterBLLFactory.GetBLL<ICardUserMasterBL>(MasterBLLFactory.CardUserMaster);
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
        }
        public CardUserMasterMonitorGroup(string defaultGroup)
            : this()
        {
            _defaultGroup = defaultGroup;
        }
        private void CardUserMasterMonitorGroup_Load(object sender, EventArgs e)
        {

            BindCombox(DefineConstantValue.MasterType.CardUserSex);
            BindCombox(DefineConstantValue.MasterType.CardUserIdentity);
            BindCombox(DefineConstantValue.MasterType.SchoolMaster);
            BindCombox(DefineConstantValue.MasterType.SpecialtyMaster);
            BindCombox(DefineConstantValue.MasterType.CardUserClass);
            BindCombox(DefineConstantValue.MasterType.DepartmentMaster);
            BindCombox(DefineConstantValue.MasterType.UserActivityMonitorItemSettingGroup);

            if (_defaultGroup != null)
            {
                cbcMonitorGroup.SelectedValue = _defaultGroup;
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
                    cbcSexNum.SetDataSource(result, 0);
                    cbcSexNum.Text = "";
                    break;
                case DefineConstantValue.MasterType.SchoolMaster:
                    cbcSchool.SetDataSource(result, 0);
                    cbcSchool.Text = "";
                    break;
                case DefineConstantValue.MasterType.CardUserIdentity:
                    cbcIdentityNum.SetDataSource(result, 0);
                    cbcIdentityNum.Text = "";
                    break;
                case DefineConstantValue.MasterType.SpecialtyMaster:
                    cbcSpecialty.SetDataSource(result, 0);
                    cbcSpecialty.Text = "";
                    break;
                case DefineConstantValue.MasterType.CardUserClass:
                    cbcClass.SetDataSource(result, 0);
                    cbcClass.Text = "";
                    break;
                case DefineConstantValue.MasterType.DepartmentMaster:
                    cbcDepartment.SetDataSource(result, 0);
                    cbcDepartment.Text = "";
                    break;
                case DefineConstantValue.MasterType.UserActivityMonitorItemSettingGroup:
                    cbcMonitorGroup.SetDataSource(result, 0);
                    cbcMonitorGroup.Text = "";
                    break;
                default:
                    break;
            }

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            BindLvwData();
        }

        private void BindLvwData()
        {

            CardUserMaster_cus_Info cusInfo = new CardUserMaster_cus_Info();
            cusInfo.cus_cIdentityNum = cbcIdentityNum.SelectedValue.ToString();
            cusInfo.cus_cSchoolNum = cbcSchool.SelectedValue.ToString();
            cusInfo.cus_cSexNum = cbcSexNum.SelectedValue.ToString();
            cusInfo.cus_cClassNum = cbcClass.SelectedValue.ToString();
            cusInfo.cus_cSpecialtyNum = cbcSpecialty.SelectedValue.ToString();
            cusInfo.cus_cGraduationPeriod = dtpGraduationPeriod.Text;
            cusInfo.cus_lValid = true;
            var data = _cardUserMasterBL.SearchRecords(cusInfo);

            if (data == null)
            {
                return;
            }

            List<CardUserMaster_cus_Info> listCadInfo = new List<CardUserMaster_cus_Info>();
            foreach (CardUserMaster_cus_Info item in data)
            {
                if (item != null)
                {
                    listCadInfo.Add(item);
                }
            }
            listCadInfo = listCadInfo.OrderBy(x => x.cus_cStudentId).ToList();

            lvwPerson.Items.Clear();

            foreach (var info in listCadInfo)
            {
                ListViewItem li = new ListViewItem();

                li.SubItems.Add(info.cus_iRecordID.ToString());
                li.SubItems.Add(info.cus_cChaName);
                li.SubItems.Add(info.cus_cEngName);
                li.SubItems.Add(info.cus_cSexNum);
                li.SubItems.Add(info.cus_cGraduationPeriod);
                li.SubItems.Add(info.cus_cSpecialtyNum);
                li.SubItems.Add(info.cus_cClassNum);
                li.SubItems.Add(info.cus_cGroupNum);
                lvwPerson.Items.Add(li);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwPerson.Items)
            {
                item.Checked = true;
            }
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwPerson.Items)
            {
                item.Checked = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> cardUserIds = new List<int>();
                foreach (ListViewItem item in lvwPerson.Items)
                {
                    if (item.Checked)
                    {
                        cardUserIds.Add(int.Parse(item.SubItems[1].Text));
                    }
                }
                _cardUserMasterBL.UpdateMonitorItemGroup(cbcMonitorGroup.SelectedValue.ToString(), cardUserIds);

                BindLvwData();

                MessageBox.Show("设置成功。");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
