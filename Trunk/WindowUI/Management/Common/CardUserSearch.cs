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

namespace WindowUI.Management.Common
{
    public partial class CardUserSearch : BaseForm
    {
        List<string> _propertyNameList;
        ICardUserMasterBL _cardUserMasterBL;
        IGeneralBL _generalBL;
        List <CardUserMaster_cus_Info> _teacherList;
        List<Model.IModel.IModelObject>  objList;

       /// <summary>
       /// ListView栏位定义
       /// </summary>
       private enum enuLvwColumn
       {
           /// <summary>
           /// 用戶編號
           /// </summary>
           cus_cNumber,
           /// <summary>
           /// 所屬科室

           /// </summary>
           cus_cDepartmentNum,
           /// <summary>
           /// 用戶名稱
           /// </summary>
           cus_cChaName,
           /// <summary>
           /// 短信接收電話
           /// </summary>
           cus_cSMSReceivePhone,
           /// <summary>
           /// 電郵地址
           /// </summary>
           cus_cMailAddress,
           /// <summary>
           /// 用戶身份
           /// </summary>
           cus_cIdentityNum
          
       }

        public CardUserSearch()
        {
            InitializeComponent();

            this._propertyNameList = null;
            this._teacherList = null;
            this._cardUserMasterBL = MasterBLLFactory.GetBLL<ICardUserMasterBL>(MasterBLLFactory.CardUserMaster);
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
            this.objList = null;

            SetLVWPropertyName();
        }

        public void ShowForm(List<CardUserMaster_cus_Info> teacherList)
        {
            this._teacherList = teacherList;
            this.ShowDialog();
        }

        private void SetLVWPropertyName()
        {
            this._propertyNameList = new List<string>();
            this._propertyNameList.Add(enuLvwColumn.cus_cNumber.ToString());
            this._propertyNameList.Add(enuLvwColumn.cus_cDepartmentNum.ToString());
            this._propertyNameList.Add(enuLvwColumn.cus_cChaName.ToString());
            this._propertyNameList.Add(enuLvwColumn.cus_cSMSReceivePhone.ToString());
            this._propertyNameList.Add(enuLvwColumn.cus_cMailAddress.ToString());
            this._propertyNameList.Add(enuLvwColumn.cus_cIdentityNum.ToString());
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();
            if (txtcCNum.Text.Trim() != "") 
            {
                info.cus_cNumber = txtcCNum.Text.Trim();
            }
            if (txtcChinaName.Text.Trim()!="")
            {
                info.cus_cChaName = txtcChinaName.Text.Trim();
            }

            //只查詢身份為教師記錄
            info.cus_cIdentityNum = DefineConstantValue.CodeMasterDefine.KEY2_SIOT_CardUserIdentity_Staff ;

            try
            {
                BindData(_cardUserMasterBL.SearchRecords(info));
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
                throw;
            }
        }

        private void BindData(List<Model.IModel.IModelObject> list)
        {
            lvwMstr.Items.Clear();
            objList = list;
            plc_InitControlData.InitListViewItem<Model.IModel.IModelObject>(list, this._propertyNameList, this.lvwMstr);

            lbliCount.Text = list.Count().ToString();
        }

        private void BtnFinishSelect_Click(object sender, EventArgs e)
        {
            SelectSub();
         }

        private void SelectSub() 
        {
            foreach (ListViewItem t in this.lvwMstr.SelectedItems)
            {
                CardUserMaster_cus_Info newItem = new CardUserMaster_cus_Info();
                newItem.cus_cNumber = t.SubItems[(int)enuLvwColumn.cus_cNumber].Text;
                newItem.cus_cDepartmentNum = t.SubItems[(int)enuLvwColumn.cus_cDepartmentNum].Text;
                newItem.cus_cChaName = t.SubItems[(int)enuLvwColumn.cus_cChaName].Text;
                newItem.cus_cSMSReceivePhone = t.SubItems[(int)enuLvwColumn.cus_cSMSReceivePhone].Text;
                newItem.cus_cMailAddress = t.SubItems[(int)enuLvwColumn.cus_cMailAddress].Text;
                _teacherList.Add(newItem);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvwMstr_DoubleClick(object sender, EventArgs e)
        {
            SelectSub();
        }
    }
}
