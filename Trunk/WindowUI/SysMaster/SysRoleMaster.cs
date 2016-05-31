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
    public partial class SysRoleMaster : BaseForm
    {
        ISysRoleMasterBL _sysRoleMasterBL;
        string iRecordID = string.Empty;
        public SysRoleMaster()
        {
            InitializeComponent();
            this._sysRoleMasterBL = MasterBLLFactory.GetBLL<ISysRoleMasterBL>(MasterBLLFactory.SysRoleMaster);
        }

        private void SysRoleMaster_Load(object sender, EventArgs e)
        {
            SetPurview(this.ToolBar);
            SetControlLength();
            SetOpenToolBar();
            ToolBar.BtnNextEnabled = false;
            ToolBar.BtnLastEnabled = false;
            Handel(Common.DefineConstantValue.GetReocrdEnum.GR_Last);
        }

        //------
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
            Sys_RoleMaster_rlm_Info info = null;
            try
            {
                info = this._sysRoleMasterBL.GetTableFieldLenght() as Sys_RoleMaster_rlm_Info;
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }

            if (info != null)
            {
                this.txtUserID.MaxLength = info.rlm_cRoleID_Length;
                txtcDesc.MaxLength = info.rlm_cRoleDesc_Length;
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

                    txtcDesc.Enabled = false;
                    txtcDesc.TextBoxSetStatus(true);
                    lvwMaster.Enabled = false;
                    btnNew.Enabled = false;
                    btnDel.Enabled = false;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Insert:
                    txtUserID.Enabled = true;
                    txtUserID.TextBoxSetStatus(false);

                    txtcDesc.Enabled = true;
                    txtcDesc.TextBoxSetStatus(false);
                    lvwMaster.Enabled = true;
                    btnNew.Enabled = true;
                    btnDel.Enabled = true;
                    break;
                case DefineConstantValue.EditStateEnum.OE_Update:
                    txtUserID.Enabled = false;
                    txtUserID.TextBoxSetStatus(true);

                    txtcDesc.Enabled = true;
                    txtcDesc.TextBoxSetStatus(false);
                    lvwMaster.Enabled = true;
                    btnNew.Enabled = true;
                    btnDel.Enabled = true;
                    break;
            }
        }

        private bool ChecktxtcName()
        {
            DataTypeVerifyResultInfo vInfo = null;
            vInfo = General.VerifyDataType(txtUserID.Text, DataType.ChinaChar);

            if (!vInfo.IsMatch)
            {
                ShowWarningMessage("角色ID不能" + vInfo.Message);
                this.txtUserID.Focus();

                return false;
            }
            else
                return true;
        }
        //顯示數據
        private void ShowInfo(Sys_RoleMaster_rlm_Info info)
        {
            try
            {
                if (info != null)
                {
                    iRecordID = info.rlm_iRecordID.ToString();
                    txtcAdd.Text = info.rlm_cAdd.ToString();
                    txtcLast.Text = info.rlm_cLast.ToString();

                    txtUserID.Text = info.rlm_cRoleID.ToString();
                    txtcDesc.Text = info.rlm_cRoleDesc.ToString();

                     lvwMaster.Items.Clear();

                     lvwMaster.SetDataSource<Sys_UserMaster_usm_Info>(info.userMasterList);
                   

                    txtdAddDate.Text = info.rlm_dAddDate != null ? info.rlm_dAddDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";
                    txtdLastDate.Text = info.rlm_dLastDate != null ? info.rlm_dLastDate.Value.ToString(Common.DefineConstantValue.gc_DateFormat) : "";
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

            Sys_RoleMaster_rlm_Info info = null;
            switch (type)
            {
                case Common.DefineConstantValue.GetReocrdEnum.GR_First:
                    try
                    {
                        info = _sysRoleMasterBL.GetRecord_First();
                        if (info.rlm_iRecordID != 0)
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
                            txtUserID.Text = "";
                            txtcDesc.Text = "";
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
                        info = _sysRoleMasterBL.GetRecord_Next(com);
                        if (info == null)
                        {
                            info = _sysRoleMasterBL.GetRecord_Last();
                            ToolBar.BtnLastEnabled = false;
                            ToolBar.BtnNextEnabled = false;
                        }
                        else
                        {
                            info = _sysRoleMasterBL.GetRecord_Next(com);
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
                        info = _sysRoleMasterBL.GetRecord_Previous(com);
                        if (info == null)
                        {
                            info = _sysRoleMasterBL.GetRecord_First();
                            ToolBar.BtnFirstEnabled = false;
                            ToolBar.BtnPreviousEnabled = false;
                        }
                        else
                        {
                            info = _sysRoleMasterBL.GetRecord_Previous(com);
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
                        info = _sysRoleMasterBL.GetRecord_Last();
                        if (info.rlm_iRecordID != 0)
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
                            txtUserID.Text = "";
                            txtcDesc.Text = "";
                            lvwMaster.Items.Clear();
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
            Sys_RoleMaster_rlm_Info Info = new Sys_RoleMaster_rlm_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    Info.rlm_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    Model.IModel.IModelObject result = _sysRoleMasterBL.DisplayRecord(Info);
                    Info = result as Sys_RoleMaster_rlm_Info;
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
            Sys_RoleMaster_rlm_Info Info = new Sys_RoleMaster_rlm_Info();
            try
            {
                comkey.KeyValue = iRecordID;
                if (comkey.KeyValue != string.Empty && comkey.KeyValue != "0")
                {
                    Info.rlm_iRecordID = Convert.ToInt32(comkey.KeyValue);
                    _sysRoleMasterBL.Save(Info, Common.DefineConstantValue.EditStateEnum.OE_Delete);

                    // SetOpenToolBar();
                    Handel(Common.DefineConstantValue.GetReocrdEnum.GR_Next);
                    if (Info.rlm_iRecordID == int.Parse(iRecordID))
                    {
                        Handel(DefineConstantValue.GetReocrdEnum.GR_Previous);
                    }
                    SetOpenToolBar();
                    if (Info.rlm_iRecordID == int.Parse(iRecordID))
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
            txtcDesc.Text = "";
            txtcAdd.Text = "";
            txtcLast.Text = "";
            txtdAddDate.Text = "";
            txtdLastDate.Text = "";
            lvwMaster.Items.Clear();
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
            Sys_RoleMaster_rlm_Info info = new Sys_RoleMaster_rlm_Info();
            Sys_UserMaster_usm_Info userInfo = null;
            if (ChecktxtcName())
            {
                if (txtUserID.Text == "")
                {
                    msg.messageText = "登陆ID" + Common.DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
                    ShowWarningMessage(msg.messageText);
                    txtUserID.Focus();
                }
                //else if (txtcName.Text == "")
                //{
                //    msg.messageText = "中文名" + Common.DefineConstantValue.SystemMessageText.strMessageText_W_CannotEmpty;
                //    ShowWarningMessage(msg.messageText);
                //    txtcName.Focus();
                //}
                else
                {
                    try
                    {
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        {
                            info.rlm_iRecordID = Convert.ToInt32(iRecordID);
                        }
                        info.rlm_cRoleID = txtUserID.Text;
                        info.rlm_cRoleDesc = txtcDesc.Text;

                        for (int i = 0; i < lvwMaster.Items.Count; i++)
                        {
                            userInfo = new Sys_UserMaster_usm_Info();
                            userInfo.usm_cUserLoginID = lvwMaster.Items[i].SubItems[0].Text;
                            //userInfo.cum_cName = lvwMaster.Items[i].SubItems[1].Text;
                            info.userMasterList.Add(userInfo);
                        }

                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Insert)
                        {
                            info.rlm_cAdd = UserInformation.usm_cUserLoginID;
                            info.rlm_dAddDate = DateTime.Now;
                        }
                        info.rlm_cLast = UserInformation.usm_cUserLoginID;
                        info.rlm_dLastDate = DateTime.Now;
                        if (this.EditState == DefineConstantValue.EditStateEnum.OE_Update)
                        {
                            msg = _sysRoleMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Update);
                        }
                        else
                        {
                            msg = _sysRoleMasterBL.Save(info, DefineConstantValue.EditStateEnum.OE_Insert);
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
            SysRoleMasterSearch win = new SysRoleMasterSearch();
            win.ShowDialog();

            if (win.DialogResult == DialogResult.OK)
            {
                //iRecordID = win.displayRecordID;
                iRecordID = win._RtvInfo[0].rlm_iRecordID.ToString();
                Sys_RoleMaster_rlm_Info info = new Sys_RoleMaster_rlm_Info();
                try
                {
                    info.rlm_iRecordID = Convert.ToInt32(iRecordID);
                    Model.IModel.IModelObject result = _sysRoleMasterBL.DisplayRecord(info);
                    info = result as Sys_RoleMaster_rlm_Info;
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
            //usm_iRecordID,
            usm_cUserLoginID,
            usm_cChaName

        }

        private bool IsExistsItem(string text)
        {
            for (int i = 0; i < lvwMaster.Items.Count; i++)
            {

                if (lvwMaster.Items[i].SubItems[(int)enmLvwMaster.usm_cUserLoginID].Text == text)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SysUserMasterSearch win = new SysUserMasterSearch();
            win.ShowDialog();


            List<Sys_UserMaster_usm_Info> items = win._RtvInfo;
            if (items != null)
            {
                try
                {

                    List<Sys_UserMaster_usm_Info> cumList = new List<Sys_UserMaster_usm_Info>();

                    foreach (Sys_UserMaster_usm_Info usm in items)
                    {
                        ListViewItem list = new ListViewItem();
                        list.Text = usm.usm_cUserLoginID.ToString();
                        if (!IsExistsItem(list.Text))
                        {
                            //cumList.Add(cum);

                            ListViewItem it = new ListViewItem(usm.usm_cUserLoginID.ToString());
                            it.SubItems.Add(usm.usm_cChaName.ToString());
                            lvwMaster.Items.Add(it);
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
