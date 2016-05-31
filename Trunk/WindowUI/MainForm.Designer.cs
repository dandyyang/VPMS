namespace WindowUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.meuExit = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuItem19 = new System.Windows.Forms.MenuItem();
            this.meuMenu = new System.Windows.Forms.MenuItem();
            this.menuItem34 = new System.Windows.Forms.MenuItem();
            this.menuItem35 = new System.Windows.Forms.MenuItem();
            this.menuItem36 = new System.Windows.Forms.MenuItem();
            this.btnLogOut = new System.Windows.Forms.MenuItem();
            this.MainStatusBar = new System.Windows.Forms.StatusBar();
            this.sbPanelLoginNameLbl = new System.Windows.Forms.StatusBarPanel();
            this.sbPanelLoginName = new System.Windows.Forms.StatusBarPanel();
            this.sbPanelUserNameLbl = new System.Windows.Forms.StatusBarPanel();
            this.sbPanelUserName = new System.Windows.Forms.StatusBarPanel();
            this.sbPanelTime = new System.Windows.Forms.StatusBarPanel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.dpnlContainer = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelLoginNameLbl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelLoginName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelUserNameLbl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelUserName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelTime)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.meuExit,
            this.menuItem1,
            this.meuMenu,
            this.menuItem34,
            this.btnLogOut});
            // 
            // meuExit
            // 
            this.meuExit.Index = 0;
            this.meuExit.Text = "&X 退出系统";
            this.meuExit.Click += new System.EventHandler(this.meuExit_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12,
            this.menuItem15,
            this.menuItem17,
            this.menuItem18,
            this.menuItem19});
            this.menuItem1.Text = "&1 主档";
            this.menuItem1.Visible = false;
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 0;
            this.menuItem12.Text = "&1 计量单位主档";
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 1;
            this.menuItem15.Text = "&2 供应商主档";
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 2;
            this.menuItem17.Text = "&3 部门主档";
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 3;
            this.menuItem18.Text = "&4 公司资料设置";
            // 
            // menuItem19
            // 
            this.menuItem19.Index = 4;
            this.menuItem19.Text = "&5 客户主档";
            // 
            // meuMenu
            // 
            this.meuMenu.Index = 2;
            this.meuMenu.Text = "&5 主页";
            this.meuMenu.Visible = false;
            // 
            // menuItem34
            // 
            this.menuItem34.Index = 3;
            this.menuItem34.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem35,
            this.menuItem36});
            this.menuItem34.Text = "&6 用户设定";
            this.menuItem34.Visible = false;
            // 
            // menuItem35
            // 
            this.menuItem35.Index = 0;
            this.menuItem35.Text = "&1 用户资料设定";
            // 
            // menuItem36
            // 
            this.menuItem36.Index = 1;
            this.menuItem36.Text = "&2 用户更改密码";
            // 
            // btnLogOut
            // 
            this.btnLogOut.Index = 4;
            this.btnLogOut.Text = "&L 注销";
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // MainStatusBar
            // 
            this.MainStatusBar.Location = new System.Drawing.Point(0, 439);
            this.MainStatusBar.Name = "MainStatusBar";
            this.MainStatusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.sbPanelLoginNameLbl,
            this.sbPanelLoginName,
            this.sbPanelUserNameLbl,
            this.sbPanelUserName,
            this.sbPanelTime});
            this.MainStatusBar.ShowPanels = true;
            this.MainStatusBar.Size = new System.Drawing.Size(820, 23);
            this.MainStatusBar.TabIndex = 2;
            // 
            // sbPanelLoginNameLbl
            // 
            this.sbPanelLoginNameLbl.Name = "sbPanelLoginNameLbl";
            this.sbPanelLoginNameLbl.Text = "登录名:";
            this.sbPanelLoginNameLbl.Width = 55;
            // 
            // sbPanelLoginName
            // 
            this.sbPanelLoginName.Name = "sbPanelLoginName";
            // 
            // sbPanelUserNameLbl
            // 
            this.sbPanelUserNameLbl.Name = "sbPanelUserNameLbl";
            this.sbPanelUserNameLbl.Text = "用户名:";
            this.sbPanelUserNameLbl.Width = 55;
            // 
            // sbPanelUserName
            // 
            this.sbPanelUserName.Name = "sbPanelUserName";
            // 
            // sbPanelTime
            // 
            this.sbPanelTime.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.sbPanelTime.Name = "sbPanelTime";
            this.sbPanelTime.Width = 130;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // MainTimer
            // 
            this.MainTimer.Enabled = true;
            this.MainTimer.Interval = 500;
            // 
            // dpnlContainer
            // 
            this.dpnlContainer.ActiveAutoHideContent = null;
            this.dpnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dpnlContainer.DockLeftPortion = 0.15;
            this.dpnlContainer.Location = new System.Drawing.Point(0, 0);
            this.dpnlContainer.Name = "dpnlContainer";
            this.dpnlContainer.Size = new System.Drawing.Size(820, 439);
            this.dpnlContainer.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(820, 462);
            this.Controls.Add(this.dpnlContainer);
            this.Controls.Add(this.MainStatusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "江門利奧信領科技系統";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelLoginNameLbl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelLoginName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelUserNameLbl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelUserName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sbPanelTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.StatusBar MainStatusBar;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.StatusBarPanel sbPanelUserName;
        private System.Windows.Forms.StatusBarPanel sbPanelUserNameLbl;
        private System.Windows.Forms.StatusBarPanel sbPanelTime;
        private System.Windows.Forms.Timer MainTimer;
        private System.Windows.Forms.MenuItem meuExit;
        private System.Windows.Forms.MenuItem meuMenu;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuItem19;
        private System.Windows.Forms.StatusBarPanel sbPanelLoginNameLbl;
        private System.Windows.Forms.StatusBarPanel sbPanelLoginName;
        private System.Windows.Forms.MenuItem menuItem34;
        private System.Windows.Forms.MenuItem menuItem35;
        private System.Windows.Forms.MenuItem menuItem36;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dpnlContainer;
        private System.Windows.Forms.MenuItem btnLogOut;

    }
}

