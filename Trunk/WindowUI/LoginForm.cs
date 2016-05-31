//#define M_DEBUG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model.Master;
using Model.IModel;
using WindowUI.ClassLibrary.Public;
using Model.General;
using Common;
using Common.DataTypeVerify;
using Model.SysMaster;
using BLL.IBLL.SysMaster;
using BLL.Factory.SysMaster;
using BLL.IBLL.General;
using BLL.Factory.General;
using BLL.IBLL.Management.Master;
using Model.Management.Master;


namespace WindowUI
{


    public partial class LoginForm : BaseForm
    {


        IUserSkinBL _userSkinBL;
        IGeneralBL _generalBL;
        ILoginFormBL _loginForm;
        public Sys_UserMaster_usm_Info _userInfo;
        public List<IModelObject> userObjs = new List<IModelObject>();
        public bool _login = false;
        public bool isMove = false;
        Point _oldPosition = new Point();
        int dLfet;

        public LoginForm()
        {
            InitializeComponent();
            this._loginForm = MasterBLLFactory.GetBLL<ILoginFormBL>(MasterBLLFactory.LoginForm);
            this._generalBL = GeneralBLLFactory.GetBLL<IGeneralBL>(GeneralBLLFactory.General);
            this._userSkinBL = BLL.Factory.Management.MasterBLLFactory.GetBLL<IUserSkinBL>(BLL.Factory.Management.MasterBLLFactory.UserSkin_urs);
            BindCombox(DefineConstantValue.MasterType.WinFormSkin);

#if M_DEBUG
            txtUserName.Text = "sa";
            txtPwd.Text = DateTime.Now.ToString("yyyy,MM,dd");

#endif




        }

        private void ShowFrom()
        {
            this.Opacity = 0.1;


            dLfet = this.Left;
            this.Left += -40;

            timer1.Enabled = true;
            timer2.Enabled = true;
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
                MessageBox.Show("网络连接错误，请检查网络！");
                //throw Ex;
            }
            cbxSkin.SetDataSource(result);
            cbxSkin.SelectedValue = "";

        }

        private void cbxSkin_SelectedValueChanged(object sender, EventArgs e)
        {


        }

        private void UserSkin()
        {
            if (cbxSkin.SelectedValue != null)
            {
                skin.SkinFile = Application.StartupPath + "\\" + cbxSkin.SelectedValue.ToString();
                if (cbxSkin.SelectedValue.ToString() != "")
                {
                    skin.Active = true;
                }
                else
                {
                    skin.Active = false;
                }
            }
        }

        private void saveUserSkin()
        {
            UserSkin_urs_Info info = new UserSkin_urs_Info();
            info.urs_cUserID = txtUserName.Text;
            info.urs_cSkinName = cbxSkin.Text;
            _userSkinBL.SaveUserSkin(info);
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            UserSkin_urs_Info info = new UserSkin_urs_Info();
            info.urs_cUserID = txtUserName.Text;
            Model.IModel.IModelObject returnTab = _userSkinBL.GetUserSkin(info);
            cbxSkin.Text = (returnTab as UserSkin_urs_Info).urs_cSkinName;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        void Login()
        {
            Model.General.ReturnValueInfo msg = new Model.General.ReturnValueInfo();
            Sys_UserMaster_usm_Info userInfo = new Sys_UserMaster_usm_Info();
            userInfo.usm_cUserLoginID = txtUserName.Text;
            userInfo.usm_cPasswork = txtPwd.Text;
            try
            {
                msg = _loginForm.Login(userInfo);
                if (msg.boolValue == true)
                {
                    userInfo = msg.ValueObject as Sys_UserMaster_usm_Info;
                    //Model.IModel.IModelObject result = _loginForm.DisplayRecord(userInfo);
                    //userInfo = result as Sys_UserMaster_usm_Info;
                    _userInfo = userInfo;
                    if (msg.messageText == null)
                    {
                        _login = true;
                        IModelObject obj = null;
                        //foreach (Sys_FormMaster_fom_Info fomInfo in _userInfo.formMasterList)
                        //{
                        //    obj = fomInfo;
                        //    userObjs.Add(obj);
                        //}

                        foreach (Sys_FunctionMaster_fum_Info item in _userInfo.functionMasterList)
                        {
                            obj = item;
                            userObjs.Add(obj);
                        }
                        //记录用户皮肤
                        saveUserSkin();
                        UserSkin();


                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        ShowInformationMessage(msg.messageText);
                    }
                }
                else
                {
                    txtPwd.Focus();
                    txtPwd.Text = "";
                    ShowInformationMessage(msg.messageText);
                }
            }
            catch (Exception Ex)
            {
                ShowErrorMessage(Ex);
            }
        }

        private void btnResert_Click(object sender, EventArgs e)
        {
            txtPwd.Text = "";
            txtUserName.Text = "";
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                this.Left += Cursor.Position.X - _oldPosition.X;
                this.Top += Cursor.Position.Y - _oldPosition.Y;
                _oldPosition = Cursor.Position;
            }
        }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            _oldPosition = Cursor.Position;
            isMove = true;
        }

        private void LoginForm_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity <= 0.9)
            {
                this.Opacity += 0.07;
            }
            else
            {
                this.Opacity = 1;
                timer1.Enabled = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Left < dLfet)
            {
                this.Left += 1;
            }
            else
            {
                this.Left = dLfet;
                timer2.Enabled = false;
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ShowFrom();
        }


        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, e);
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
