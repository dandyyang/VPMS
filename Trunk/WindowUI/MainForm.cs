using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Model.Base;
using Model.IModel;
using Common;
using BLL.Base;
using WindowUI.ClassLibrary.Public;
using systemForm = ProgramMode.SystemForm;
using WeifenLuo.WinFormsUI.Docking;
using Model.SysMaster;


namespace WindowUI
{
    public partial class MainForm : Form
    {
        private DefineConstantValue.SystemMessage _systemMessageText = new DefineConstantValue.SystemMessage(string.Empty);
        MenuToolForm _menuToolForm;
        public List<IModelObject> _formFunctionList;
        bool _isLogin = false;
        LoginForm _win = new LoginForm();
        Sys_UserMaster_usm_Info _usm = null;

        public MainForm()
        {
            InitializeComponent();

            //this._formFunctionList = null;

            Cursor.Current = Cursors.WaitCursor;

            //LoadSystemData();

            this.Text = this._systemMessageText.strSystemText;

            Cursor.Current = Cursors.Arrow;

        }

        /// <summary>
        /// 初始化系統數據


        /// </summary>
        private void LoadSystemData()
        {
            //plc_ConstValue.userInformationInfo = new UserInformationInfo();
            //plc_ConstValue.g_stuFormColor = Color.FromKnownColor(KnownColor.Control);
            //Application.EnableVisualStyles();
            //this.sbPanelLoginName.Text = plc_ConstValue.userInformationInfo.UsmCUserID;
            //this.sbPanelUserName.Text = plc_ConstValue.userInformationInfo.UsmCChaName;   
            this.sbPanelLoginName.Text = _usm.usm_cUserLoginID;
            this.sbPanelUserName.Text = _usm.usm_cChaName;
        }

        private void meuCodeMstr_Click(object sender, EventArgs e)
        {

        }

        private void meuExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this._systemMessageText.strMessageText_Q_ExitApplication, this._systemMessageText.strMessageTitle + this._systemMessageText.strQuestion.Trim(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowLoginFrm();
            this.Text += " Ver:" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this._isLogin = false;
            this._menuToolForm.Dispose();
            _win = new LoginForm();
            ShowLoginFrm();
        }
        private void ShowLoginFrm()
        {
            this.Hide();
            _usm = new Sys_UserMaster_usm_Info();
            _win.ShowDialog();
            if (_win._login)
            {
                this.Show();
                this._isLogin = true;
                this._formFunctionList = _win.userObjs;
                //加东西


                //this._formFunctionList = _win._userInfo.functionMasterList;
            }
            else
            {
                this.Close();
                return;
            }

            _usm = _win._userInfo;

            LoadSystemData();

            this._menuToolForm = new MenuToolForm(this.dpnlContainer, this._formFunctionList, _win._userInfo);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this._isLogin)
            {
                if (MessageBox.Show(this._systemMessageText.strMessageText_Q_ExitApplication, this._systemMessageText.strMessageTitle + this._systemMessageText.strQuestion.Trim(), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

    }
}
