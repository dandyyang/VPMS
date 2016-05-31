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
using BLL.IBLL.Management.Master;
using RF = BLL.Factory.Management;
using Model.Management.Master;
using WindowUI.Management.Master;
using WindowUI.Management.DataRightsManagement;
using Model.Management.DataRightsManagement;
using BLL.IBLL.Management.DataRightsManagement;

namespace WindowUI.SysMaster
{
    public partial class SysUserMaster : BaseForm
    {
        List<DataRightsRole_drr_Info> _list = new List<DataRightsRole_drr_Info>();
        List<DataRightsRole_drr_Info> _listShow = new List<DataRightsRole_drr_Info>();
        ISysUserMasterBL _sysUserMasterBL;
        IDataRightsRoleBL _dataRightsRoleBL;
        //IRFIDCardManageBL _rfidCardManageBL;
        string iRecordID = string.Empty;
        string pwd = "!!!aaa111";
        Guid _guid = Guid.Empty;

        bool isSaveTrue = false;

        public SysUserMaster()
        {
            InitializeComponent();
            this._sysUserMasterBL = MasterBLLFactory.GetBLL<ISysUserMasterBL>(MasterBLLFactory.SysUserMaster);
            this._dataRightsRoleBL = BLL.Factory.Management.MasterBLLFactory.GetBLL<IDataRightsRoleBL>(BLL.Factory.Management.MasterBLLFactory.DataRightsRole);
        }

        private void SysUserMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);
            SetControlLength();
            SetOpenToolBar();
            ToolBar.BtnNextEnabled = false;
            ToolBar.BtnLastEnabled = false;
            Handel(Common.DefineConstantValue.GetReocrdEnum.GR_Last);

            #region 2011-9-7
            //label10.Visible = false;
            //txtChaName.Visible = false;
            #endregion
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
            Sys_UserMaster_usm_Info info = null;
            try
            {
                info = this._sysUserMasterBL.GetTableFieldLenght() as Sys_UserMaster_usm_Info;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            if (info != null)
            {
                this.txtUserID.MaxLength = info.usm_cUserLoginID_Length;
                txtcName.MaxLength = info.usm_cChaName_Length;
                txtcPwd.MaxLength = info.usm_cPasswork_Length;
                txtcMail.MaxLength = info.usm_cEMail_Length;
                txtcRemark.MaxLength = info.usm_cRemark_Length;
            }
        }

        //設置文本狀態


        private void SetTxtBox(DefineConstantValue.EditStateEnum type)
        {
            switch (type)
            {
                case DefineConstantValue.EditStateEnum.OE_ReaOnly:
                    txtUserID.Enabled = false;
                    txtUserID.TextBoxSetStatus(true);
                    txtcName.Enabled = false;
                    txtcName.TextBoxSetStatus(true);
                    chkLock.Enabled = false;

                    txtcPwd.Enabled = false;
                    txtcPwd.TextBoxSetStatus(true);
                    txtcMail.Enabled = false;
                    txtcMail.TextBoxSetStatus(true);

                    txtcRemark.Enabled = false;
                    txtcRemark.TextBoxSetStatus(true);
                    btnNew.Enabled = false;
                    btnDel.Enabled = false;
                    btnPurviewSetting.Enabled = false;
                    lvwMaster.Enabled = false;

                    groupBox5.Enabled = false;


                    txtCardNum.Enabled = false;
                    txtCardNum.TextBoxSetStatus(true);
                    cbisTrue.Enabled = false;

                    btnUserSearch.Enabled = false;

                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    txtUserID.Enabled = true;
                    txtUserID.TextBoxSetStatus(false);
                    txtcName.Enabled = true;
                    txtcName.TextBoxSetStatus(false);
                    chkLock.Enabled = true;

                    txtcPwd.Enabled = true;
                    txtcPwd.TextBoxSetStatus(false);
                    txtcMail.Enabled = true;
                    txtcMail.TextBoxSetStatus(false);

                    txtcRemark.Enabled = true;
                    txtcRemark.TextBoxSetStatus(false);
                    btnNew.Enabled = true;
                    btnDel.Enabled = true;
                    btnPurviewSetting.Enabled = true;
                    lvwMaster.Enabled = true;

                    txtCardNum.Enabled = true;
                    txtCardNum.TextBoxSetStatus(false);
                    cbisTrue.Enabled = true;

                    btnUserSearch.Enabled = true;

                    if (cbisTrue.Checked)
                    {
                        groupBox5.Enabled = true;
                    }
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    txtUserID.Enabled = false;
                    txtUserID.TextBoxSetStatus(true);
                    txtcName.Enabled = false;
                    txtcName.TextBoxSetStatus(true);
                    chkLock.Enabled = true;

                    txtcPwd.Enabled = true;
                    txtcPwd.TextBoxSetStatus(false);
                    txtcMail.Enabled = true;
                    txtcMail.TextBoxSetStatus(false);

                    txtcRemark.Enabled = true;
                    txtcRemark.TextBoxSetStatus(false);
                    btnNew.Enabled = true;
                    btnDel.Enabled = true;
                    btnPurviewSetting.Enabled = true;
                    lvwMaster.Enabled = true;


                    txtCardNum.Enabled = true;
                    txtCardNum.TextBoxSetStatus(false);
                    cbisTrue.Enabled = true;

                    btnUserSearch.Enabled = true;
                    if (cbisTrue.Checked)
                    {
                        groupBox5.Enabled = true;
                    }
                    break;
            }
        }

        private bool ChecktxtcName()
        {
            DataTypeVerifyResultInfo vInfo = null;
            vInfo = General.VerifyDataType(txtUserID.Text, DataType.ChinaChar);

            if (!vInfo.IsMatch)
            {
                ShowWarningMessage("登陆ID不能" + vInfo.Message);
                this.txtUserID.Focus();

                return false;
            }
            else
                return true;
        }
        //顯示數據
        private void ShowInfo(Sys_UserMaster_usm_Info info)
        {
            try
            {
                if (info != null)
                {
                    iRecordID = info.usm_iRecordID.ToString();
                    txtcAdd.Text = info.usm_cAdd.ToString();
                    txtcLast.Text = info.usm_cLast.ToString();

                    if (info.usm_iLock == true)
                    {
                        chkLock.Checked = true;
                    }
                    else
                    {
                        chkLock.Checked = false;
                    }
                    txtUserID.Text = info.usm_cUserLoginID.ToString();
                    txtcName.Text = info.usm_cChaName.ToString();

                    pwd = info.usm_cPasswork.ToString();
                    txtcPwd.Text = "";

                    txtcMail.Text = info.usm_cEMail.ToString();

                    lvwMaster.Items.Clear();

                    lvwMaster.SetDataSource<Sys_RoleMaster_rlm_Info>(info.roleMasterList);

                    txtcRemark.Text = info.usm_cRemark.ToString();
                    txtdAddDate.Text = info.usm_dAddDate != null ? info.usm_dAddDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";
                    txtdLastDate.Text = info.usm_dLastDate != null ? info.usm_dLastDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";

                    Sys_UserMaster_usm_Info usmInfo = new Sys_UserMaster_usm_Info();
                    try
                    {
                        _listShow = _dataRightsRoleBL.GetUserRoleList(usmInfo);
                    }
                    catch (Exception Ex)
                    {
                        ShowErrorMessage(Ex);
                    }

                    FullDataRoleList();
                    groupBox5.Enabled = false;


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

            Sys_UserMaster_usm_Info info = null;
            switch (type)
            {
                case Common.DefineConstantValue.GetReocrdEnum.GR_First:
                    try
                    {
                        info = _sysUserMasterBL.GetRecord_First();
                        if (info.usm_iRecordID != 0)
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
                            txtcName.Text = "";
                            //txtcNumber.Text = "";
                            txtcRemark.Text = "";
                            lvwMaster.Items.Clear();
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
                case Common.DefineConstantValue.GetReocrdEnum.GR_Next:
                    try
                    {
                        info = _sysUserMasterBL.GetRecord_Next(com);
                        if (info == null)
                        {
                            Handel(DefineConstantValue.GetReocrdEnum.GR_Last);
                            //info = _sysUserMasterBL.GetRecord_Last();
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                        }
                        else
                        {
                            info = _sysUserMasterBL.GetRecord_Next(com);
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
                        info = _sysUserMasterBL.GetRecord_Previous(com);
                        if (info == null)
                        {
                            info = _sysUserMasterBL.GetRecord_First();
                            ToolBar.BtnFirstEnabled = false;
                            ToolBar.BtnPreviousEnabled = false;
                        }
                        else
                        {
                            info = _sysUserMasterBL.GetRecord_Previous(com);
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
                        info = _sysUserMasterBL.GetRecord_Last();
                        if (info.usm_iRecordID != 0)
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
                            txtcName.Text = "";
                            //txtcNumber.Text = "";
                            txtcRemark.Text = "";
                            lvwMaster.Items.Clear();
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                    break;
            }
        }

        private void btnPurviewSetting_Click(object sender, EventArgs e)
        {
            SysPurviewMaster win = new SysPurviewMaster();
            win._isByCall = true;

            win.ShowDialog();

            win.Dispose();
            win = null;
        }

        private void ToolBar_BtnCancelClick(object sender, EventArgs e)
        {
            this.EditState = DefineConstantValue.EditStateEnum.OE_ReaOnly;
            Model.Base.DataBaseCommandKeyInfo comkey = new Model.Base.DataBaseCommandKeyInfo();
            Sys_UserMaster_usm_Info Info = new Sys_UserMaster_usm_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    Info.usm_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    Model.IModel.IModelObject result = _sysUserMasterBL.DisplayRecord(Info);
                    Info = result as Sys_UserMaster_usm_Info;
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
            Sys_UserMaster_usm_Info Info = new Sys_UserMaster_usm_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    Info.usm_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    _sysUserMasterBL.Save(Info, Common.DefineConstantValue.EditStateEnum.OE_Delete);

                    SetOpenToolBar();
                    Handel(Common.DefineConstantValue.GetReocrdEnum.GR_Next);
                    if (Info.usm_iRecordID == int.Parse(iRecordID))
                    {
                        Handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
                    }
                    if (Info.usm_iRecordID == int.Parse(iRecordID))
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
            txtUserID.Text = "";
            txtUserID.Focus();
            chkLock.Checked = false;
            txtcName.Text = "";
            txtcPwd.Text = "";
            txtcMail.Text = "";
            txtcRemark.Text = "";
            lvwMaster.Items.Clear();
            txtcAdd.Text = "";
            txtcLast.Text = "";
            txtdAddDate.Text = "";
            txtdLastDate.Text = "";
            //txtdAddDate.Text = DateTime.Now.ToString(DefineConstantValue.gc_DateFormat);
            //txtdLastDate.Text = DateTime.Now.ToString(DefineConstantValue.gc_DateFormat);

            txtCardNum.Text = "";
            //txtChaName.Text = "";
            cbisTrue.Checked = false;
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
            if (txtCardNum.Text != "")
            {
                //RFIDCardManage_rcm_Info rcm = new RFIDCardManage_rcm_Info();
                //rcm.rcm_cCardNum = txtCardNum.Text;

                //rcm = _sysUserMasterBL.CheckRCM(txtCardNum.Text);

                if (true)
                {
                    SaveUSM();
                    if (isSaveTrue)
                    {
                        SaveWUIB(txtCardNum.Text);
                    }
                }
                else
                {
                    if (MessageBox.Show("没有对该用户发卡,確認要继续保存用户信息吗？", "系統信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        SaveUSM();
                        #region 9-20
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        {
                            SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                        else
                        {
                            Handel(DefineConstantValue.GetReocrdEnum.GR_Last);
                        }
                        #endregion
                    }
                    else
                        return;
                }

            }
            else
            {
                if (MessageBox.Show("您没填写绑定的卡号,確認要继续保存用户信息吗？", "系統信息", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveUSM();
                    #region 9-20
                    if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                    {
                        SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                    }
                    else
                    {
                        Handel(DefineConstantValue.GetReocrdEnum.GR_Last);

                    }
                    #endregion
                }
                else
                    return;
            }
        }

        void SaveUSM()
        {
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Sys_UserMaster_usm_Info info = new Sys_UserMaster_usm_Info();
            Sys_RoleMaster_rlm_Info userInfo = null;
            if (ChecktxtcName())
            {
                if (txtUserID.Text == "")
                {
                    msg.messageText = "登陆ID" + Common.DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
                    ShowWarningMessage(msg.messageText);
                    txtUserID.Focus();
                }
                else if (txtcName.Text == "")
                {
                    msg.messageText = "中文名" + Common.DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
                    ShowWarningMessage(msg.messageText);
                    txtcName.Focus();
                }
                else
                {
                    try
                    {
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        {
                            info.usm_iRecordID = Convert.ToInt32(iRecordID);
                        }
                        info.usm_cUserLoginID = txtUserID.Text;
                        info.usm_cChaName = txtcName.Text;
                        if (txtcPwd.Text != "")
                        {
                            info.usm_cPasswork = Common.General.MD5(txtcPwd.Text);
                        }
                        else
                        {
                            if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                                info.usm_cPasswork = Common.General.MD5(pwd);
                            else
                                info.usm_cPasswork = pwd;
                        }
                        //**
                        //if (Common.General.CheckIsMail(txtcMail.Text))
                        //{
                        //    info.usm_cEMail = txtcMail.Text;
                        //}
                        //else
                        //{
                        //    msg.messageText = "邮箱填写有误!";
                        //    ShowInformationMessage(msg.messageText);
                        //    return;
                        //}
                        info.usm_cEMail = txtcMail.Text;
                        //**
                        info.usm_cRemark = txtcRemark.Text;

                        if (chkLock.Checked == true)
                        {
                            info.usm_iLock = true;
                        }
                        else
                        {
                            info.usm_iLock = false;
                        }
                        for (int i = 0; i < lvwMaster.Items.Count; i++)
                        {
                            userInfo = new Sys_RoleMaster_rlm_Info();
                            userInfo.rlm_cRoleID = lvwMaster.Items[i].SubItems[0].Text;
                            info.roleMasterList.Add(userInfo);
                        }
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                        {
                            info.usm_cAdd = UserInformation.usm_cUserLoginID;
                            info.usm_dAddDate = DateTime.Now;
                        }
                        info.usm_cLast = UserInformation.usm_cUserLoginID;
                        info.usm_dLastDate = DateTime.Now;
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        {
                            msg = _sysUserMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                            isSaveTrue = msg.boolValue;
                        }
                        else
                        {
                            msg = _sysUserMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
                            isSaveTrue = msg.boolValue;
                            //if (msg.boolValue)
                            //{
                            //    Handel(DefineConstantValue.GetReocrdEnum.GR_Last);
                            //}
                        }


                        //保存数据角色 2011-11-02 Ximon
                        #region

                        //_dataRightsRoleBL
                        List<Sys_UserMaster_usm_Info> userList = new List<Sys_UserMaster_usm_Info>();
                        List<DataRightsRole_drr_Info> roleList = new List<DataRightsRole_drr_Info>();

                        Sys_UserMaster_usm_Info userI = new Sys_UserMaster_usm_Info();
                        userI.usm_cUserLoginID = txtCardNum.Text;
                        userList.Add(userI);

                        foreach (DataRightsRole_drr_Info item in _listShow)
                        {
                            DataRightsRole_drr_Info roleI = new DataRightsRole_drr_Info();
                            roleI.drr_cNumber = item.drr_cNumber;
                            roleList.Add(roleI);
                        }

                        try
                        {
                            ReturnValueInfo returnInfo = _dataRightsRoleBL.SaveUserToRole(userList, roleList,true,false);
                        }
                        catch (Exception Ex)
                        {
                            
                            ShowErrorMessage(Ex);
                        }

                        #endregion



                        if (msg.messageText != null)
                            ShowInformationMessage(msg.messageText);
                        else
                        {
                            SetOpenToolBar();
                            //SetTxtBox(DefineConstantValue.EditStateEnum.OE_ReaOnly);
                        }
                    }
                    catch (Exception Ex)
                    { ShowErrorMessage(Ex); }
                }
            }
        }

        void SaveWUIB(string cardNum)
        {
        }

        private void ToolBar_BtnSearchClick(object sender, EventArgs e)
        {
            SysUserMasterSearch win = new SysUserMasterSearch();
            win.ShowDialog();

            if (win.DialogResult == DialogResult.OK)
            {
                //iRecordID = win.displayRecordID;
                iRecordID = win._RtvInfo[0].usm_iRecordID.ToString();
                Sys_UserMaster_usm_Info info = new Sys_UserMaster_usm_Info();
                try
                {
                    info.usm_iRecordID = Convert.ToInt32(iRecordID);
                    Model.IModel.IModelObject result = _sysUserMasterBL.DisplayRecord(info);
                    info = result as Sys_UserMaster_usm_Info;
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
            win.Dispose();//釋放窗口
            win = null;
        }

        private enum enmLvwMaster
        {
            //usm_iRecordID,
            rlm_cRoleID,
            rlm_cRoleDesc
        }

        private bool IsExistsItem(string text)
        {
            for (int i = 0; i < lvwMaster.Items.Count; i++)
            {

                if (lvwMaster.Items[i].SubItems[(int)enmLvwMaster.rlm_cRoleID].Text == text)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SysRoleMasterSearch win = new SysRoleMasterSearch();
            win.ShowDialog();


            List<Sys_RoleMaster_rlm_Info> items = win._RtvInfo;
            if (items != null)
            {
                try
                {

                    List<Sys_RoleMaster_rlm_Info> cumList = new List<Sys_RoleMaster_rlm_Info>();

                    foreach (Sys_RoleMaster_rlm_Info usm in items)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = usm.rlm_cRoleID.ToString();
                        if (!IsExistsItem(list.Text))
                        {
                            //cumList.Add(cum);

                            ListViewItem it = new ListViewItem(usm.rlm_cRoleID.ToString());
                            it.SubItems.Add(usm.rlm_cRoleDesc.ToString());
                            lvwMaster.Items.Add(it);
                        }
                    }
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

        private void btnUserSearch_Click(object sender, EventArgs e)
        {
            CardUserMasterSearch win = new CardUserMasterSearch();
            CardUserMaster_cus_Info info = new CardUserMaster_cus_Info();

            win.ShowForm(info);

            if (info.cus_cNumber != null && info.cus_cNumber != "")
            {
                txtCardNum.Text = info.cus_cNumber;
            }

            //win._check = true;
            //win.ShowDialog();

            //if (win._listinfo != null && win._listinfo.Count > 0)
            //{
            //    try
            //    {
            //        txtCardNum.Text = win._listinfo[0].cus_cNumber;
            //        //txtChaName.Text = win._listinfo[0].cus_cChaName;
            //    }
            //    catch (Exception Ex)
            //    { ShowErrorMessage(Ex); }
            //}
            win.Dispose();
            win = null;
        }

        private void btnAddDataRole_Click(object sender, EventArgs e)
        {
            DataRightsRoleSettingSearch win = new DataRightsRoleSettingSearch();
            _list.Clear();
            win.ShowForm(_list);

            foreach (DataRightsRole_drr_Info item in _list)
            {
                DataRightsRole_drr_Info tab = _listShow.SingleOrDefault(t => t.drr_cNumber == item.drr_cNumber);
                if (tab == null)
                {
                    _listShow.Add(item);
                }

            }

            FullDataRoleList();
        }

        private void cbisTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (cbisTrue.Checked)
            {
                groupBox5.Enabled = true;
            }
            else
            {
                groupBox5.Enabled = false;
            }
        }

        private void btnDelDataRole_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvwRole.SelectedItems)
            {
                DataRightsRole_drr_Info tab = _listShow.SingleOrDefault(t => t.drr_cNumber == item.SubItems[0].Name);
                if (tab != null)
                {
                    _listShow.Remove(tab);
                }

            }

            FullDataRoleList();
        }

        private void FullDataRoleList()
        {
            lvwRole.Items.Clear();
            foreach (DataRightsRole_drr_Info item in _listShow)
            {
                ListViewItem li = new ListViewItem();
                li.SubItems[0].Text = item.drr_cName;
                li.SubItems[0].Name = item.drr_cNumber;

                System.Windows.Forms.ListViewItem.ListViewSubItem liSub = new System.Windows.Forms.ListViewItem.ListViewSubItem();
                liSub.Text = item.drr_cRemark;

                li.SubItems.Add(liSub);
                lvwRole.Items.Add(li);
            }
        }
    }
}
