namespace WindowUI.SysMaster
{
    partial class SysUserMaster
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
            this.ToolBar = new WindowControls.UserToolBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnDelDataRole = new System.Windows.Forms.Button();
            this.btnAddDataRole = new System.Windows.Forms.Button();
            this.lvwRole = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnUserSearch = new System.Windows.Forms.Button();
            this.cbisTrue = new System.Windows.Forms.CheckBox();
            this.txtCardNum = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtdAddDate = new System.Windows.Forms.TextBox();
            this.txtcAdd = new System.Windows.Forms.TextBox();
            this.txtdLastDate = new System.Windows.Forms.TextBox();
            this.txtcLast = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPurviewSetting = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lvwMaster = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label7 = new System.Windows.Forms.Label();
            this.chkLock = new System.Windows.Forms.CheckBox();
            this.txtcRemark = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtcMail = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtcPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcName = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolBar
            // 
            this.ToolBar.AutoSetStatus = true;
            this.ToolBar.BtnCancelEnabled = true;
            this.ToolBar.BtnCancelVisible = true;
            this.ToolBar.BtnCardIssuanceEnabled = true;
            this.ToolBar.BtnCardIssuanceVisible = false;
            this.ToolBar.BtnCardMissingEnabled = true;
            this.ToolBar.BtnCardMissingVisible = false;
            this.ToolBar.BtnCardRecoveryEnabled = true;
            this.ToolBar.BtnCardRecoveryVisible = false;
            this.ToolBar.BtnCardReturnEnabled = true;
            this.ToolBar.BtnCardReturnVisible = false;
            this.ToolBar.BtnCardScrapEnabled = true;
            this.ToolBar.BtnCardScrapVisible = false;
            this.ToolBar.BtnDataExportEnabled = true;
            this.ToolBar.BtnDataExportVisible = false;
            this.ToolBar.BtnDataInputEnabled = true;
            this.ToolBar.BtnDataInputVisible = false;
            this.ToolBar.BtnDeleteEnabled = true;
            this.ToolBar.BtnDeleteVisible = true;
            this.ToolBar.BtnExpCusDataEnabled = true;
            this.ToolBar.BtnExpCusDataVisible = false;
            this.ToolBar.BtnExportCardUserPhotoEnabled = true;
            this.ToolBar.BtnExportCardUserPhotoVisible = false;
            this.ToolBar.BtnExportTemplateEnabled = true;
            this.ToolBar.BtnExportTemplateVisible = false;
            this.ToolBar.btnExportDataEnabled = true;
            this.ToolBar.btnExportDataVisible = false;
            this.ToolBar.BtnFirstEnabled = true;
            this.ToolBar.BtnFirstVisible = true;
            this.ToolBar.BtnGroupPersonEnabled = true;
            this.ToolBar.BtnGroupPersonVisible = false;
            this.ToolBar.BtnImportCardUserDataEnabled = true;
            this.ToolBar.BtnImportCardUserDataVisible = false;
            this.ToolBar.BtnImportPhotoEnabled = true;
            this.ToolBar.BtnImportPhotoVisible = false;
            this.ToolBar.btnImportDataEnabled = true;
            this.ToolBar.btnImportDataVisible = false;
            this.ToolBar.BtnLastEnabled = true;
            this.ToolBar.BtnLastVisible = true;
            this.ToolBar.BtnModifyEnabled = true;
            this.ToolBar.BtnModifyVisible = true;
            this.ToolBar.BtnNewEnabled = true;
            this.ToolBar.BtnNewVisible = true;
            this.ToolBar.BtnNextEnabled = true;
            this.ToolBar.BtnNextVisible = true;
            this.ToolBar.BtnPreviousEnabled = true;
            this.ToolBar.BtnPreviousVisible = true;
            this.ToolBar.BtnSaveEnabled = true;
            this.ToolBar.BtnSaveVisible = true;
            this.ToolBar.BtnSearchEnabled = true;
            this.ToolBar.BtnSearchVisible = true;
            this.ToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBar.Location = new System.Drawing.Point(0, 0);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.RecordExistPosition = WindowControls.UserToolBar.RecordIndexType.None;
            this.ToolBar.Size = new System.Drawing.Size(785, 31);
            this.ToolBar.TabIndex = 5;
            this.ToolBar.toolStripSeparator11Visible = true;
            this.ToolBar.toolStripSeparator12Visible = true;
            this.ToolBar.toolStripSeparator21Visible = true;
            this.ToolBar.toolStripSeparator22Visible = true;
            this.ToolBar.BtnLastClick += new System.EventHandler(this.ToolBar_BtnLastClick);
            this.ToolBar.BtnNextClick += new System.EventHandler(this.ToolBar_BtnNextClick);
            this.ToolBar.BtnPreviousClick += new System.EventHandler(this.ToolBar_BtnPreviousClick);
            this.ToolBar.BtnNewClick += new System.EventHandler(this.ToolBar_BtnNewClick);
            this.ToolBar.BtnModifyClick += new System.EventHandler(this.ToolBar_BtnModifyClick);
            this.ToolBar.BtnFirstClick += new System.EventHandler(this.ToolBar_BtnFirstClick);
            this.ToolBar.BtnSaveClick += new System.EventHandler(this.ToolBar_BtnSaveClick);
            this.ToolBar.BtnCancelClick += new System.EventHandler(this.ToolBar_BtnCancelClick);
            this.ToolBar.BtnSearchClick += new System.EventHandler(this.ToolBar_BtnSearchClick);
            this.ToolBar.BtnDeleteClick += new System.EventHandler(this.ToolBar_BtnDeleteClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(4, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(781, 561);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnDelDataRole);
            this.groupBox5.Controls.Add(this.btnAddDataRole);
            this.groupBox5.Controls.Add(this.lvwRole);
            this.groupBox5.Enabled = false;
            this.groupBox5.Location = new System.Drawing.Point(539, 127);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(221, 344);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "数据权限角角";
            // 
            // btnDelDataRole
            // 
            this.btnDelDataRole.Location = new System.Drawing.Point(118, 315);
            this.btnDelDataRole.Name = "btnDelDataRole";
            this.btnDelDataRole.Size = new System.Drawing.Size(93, 23);
            this.btnDelDataRole.TabIndex = 2;
            this.btnDelDataRole.Text = "删除角色";
            this.btnDelDataRole.UseVisualStyleBackColor = true;
            this.btnDelDataRole.Click += new System.EventHandler(this.btnDelDataRole_Click);
            // 
            // btnAddDataRole
            // 
            this.btnAddDataRole.Location = new System.Drawing.Point(13, 315);
            this.btnAddDataRole.Name = "btnAddDataRole";
            this.btnAddDataRole.Size = new System.Drawing.Size(93, 23);
            this.btnAddDataRole.TabIndex = 1;
            this.btnAddDataRole.Text = "添加角色";
            this.btnAddDataRole.UseVisualStyleBackColor = true;
            this.btnAddDataRole.Click += new System.EventHandler(this.btnAddDataRole_Click);
            // 
            // lvwRole
            // 
            this.lvwRole.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvwRole.FullRowSelect = true;
            this.lvwRole.GridLines = true;
            this.lvwRole.Location = new System.Drawing.Point(13, 20);
            this.lvwRole.Name = "lvwRole";
            this.lvwRole.Size = new System.Drawing.Size(198, 286);
            this.lvwRole.TabIndex = 0;
            this.lvwRole.UseCompatibleStateImageBehavior = false;
            this.lvwRole.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "角色";
            this.columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "角色描述";
            this.columnHeader4.Width = 120;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnUserSearch);
            this.groupBox3.Controls.Add(this.cbisTrue);
            this.groupBox3.Controls.Add(this.txtCardNum);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(537, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(223, 96);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            // 
            // btnUserSearch
            // 
            this.btnUserSearch.Location = new System.Drawing.Point(186, 37);
            this.btnUserSearch.Name = "btnUserSearch";
            this.btnUserSearch.Size = new System.Drawing.Size(31, 23);
            this.btnUserSearch.TabIndex = 5;
            this.btnUserSearch.Text = "...";
            this.btnUserSearch.UseVisualStyleBackColor = true;
            this.btnUserSearch.Click += new System.EventHandler(this.btnUserSearch_Click);
            // 
            // cbisTrue
            // 
            this.cbisTrue.AutoSize = true;
            this.cbisTrue.Location = new System.Drawing.Point(78, 73);
            this.cbisTrue.Name = "cbisTrue";
            this.cbisTrue.Size = new System.Drawing.Size(15, 14);
            this.cbisTrue.TabIndex = 4;
            this.cbisTrue.UseVisualStyleBackColor = true;
            this.cbisTrue.CheckedChanged += new System.EventHandler(this.cbisTrue_CheckedChanged);
            // 
            // txtCardNum
            // 
            this.txtCardNum.Location = new System.Drawing.Point(8, 39);
            this.txtCardNum.Name = "txtCardNum";
            this.txtCardNum.Size = new System.Drawing.Size(172, 21);
            this.txtCardNum.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "是否有效：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "用户编号：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtdAddDate);
            this.groupBox4.Controls.Add(this.txtcAdd);
            this.groupBox4.Controls.Add(this.txtdLastDate);
            this.groupBox4.Controls.Add(this.txtcLast);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(3, 474);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(775, 84);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            // 
            // txtdAddDate
            // 
            this.txtdAddDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtdAddDate.Enabled = false;
            this.txtdAddDate.Location = new System.Drawing.Point(72, 49);
            this.txtdAddDate.Name = "txtdAddDate";
            this.txtdAddDate.Size = new System.Drawing.Size(125, 21);
            this.txtdAddDate.TabIndex = 7;
            // 
            // txtcAdd
            // 
            this.txtcAdd.BackColor = System.Drawing.SystemColors.Info;
            this.txtcAdd.Enabled = false;
            this.txtcAdd.Location = new System.Drawing.Point(72, 24);
            this.txtcAdd.Name = "txtcAdd";
            this.txtcAdd.Size = new System.Drawing.Size(125, 21);
            this.txtcAdd.TabIndex = 6;
            // 
            // txtdLastDate
            // 
            this.txtdLastDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtdLastDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtdLastDate.Enabled = false;
            this.txtdLastDate.Location = new System.Drawing.Point(643, 51);
            this.txtdLastDate.Name = "txtdLastDate";
            this.txtdLastDate.Size = new System.Drawing.Size(125, 21);
            this.txtdLastDate.TabIndex = 5;
            // 
            // txtcLast
            // 
            this.txtcLast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtcLast.BackColor = System.Drawing.SystemColors.Info;
            this.txtcLast.Enabled = false;
            this.txtcLast.Location = new System.Drawing.Point(643, 26);
            this.txtcLast.Name = "txtcLast";
            this.txtcLast.Size = new System.Drawing.Size(125, 21);
            this.txtcLast.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(583, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "修改时间:";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label11.Location = new System.Drawing.Point(583, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "修改者:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label12.Location = new System.Drawing.Point(13, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "新增时间:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label13.Location = new System.Drawing.Point(13, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "新增者:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPurviewSetting);
            this.groupBox2.Controls.Add(this.btnDel);
            this.groupBox2.Controls.Add(this.btnNew);
            this.groupBox2.Controls.Add(this.lvwMaster);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.chkLock);
            this.groupBox2.Controls.Add(this.txtcRemark);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtcMail);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtcPwd);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtcName);
            this.groupBox2.Controls.Add(this.txtUserID);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(524, 457);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // btnPurviewSetting
            // 
            this.btnPurviewSetting.Location = new System.Drawing.Point(454, 239);
            this.btnPurviewSetting.Name = "btnPurviewSetting";
            this.btnPurviewSetting.Size = new System.Drawing.Size(64, 31);
            this.btnPurviewSetting.TabIndex = 24;
            this.btnPurviewSetting.Text = "权限设置";
            this.btnPurviewSetting.UseVisualStyleBackColor = true;
            this.btnPurviewSetting.Click += new System.EventHandler(this.btnPurviewSetting_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(382, 427);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(65, 23);
            this.btnDel.TabIndex = 23;
            this.btnDel.Text = "刪除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(301, 427);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(65, 23);
            this.btnNew.TabIndex = 22;
            this.btnNew.Text = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // lvwMaster
            // 
            this.lvwMaster.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvwMaster.FullRowSelect = true;
            this.lvwMaster.GridLines = true;
            this.lvwMaster.Location = new System.Drawing.Point(91, 239);
            this.lvwMaster.Name = "lvwMaster";
            this.lvwMaster.Size = new System.Drawing.Size(357, 180);
            this.lvwMaster.TabIndex = 21;
            this.lvwMaster.UseCompatibleStateImageBehavior = false;
            this.lvwMaster.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "rlm_cRoleID";
            this.columnHeader1.Text = "角色ID";
            this.columnHeader1.Width = 81;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "rlm_cRoleDesc";
            this.columnHeader2.Text = "角色描述";
            this.columnHeader2.Width = 120;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 242);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "所属角色：";
            // 
            // chkLock
            // 
            this.chkLock.AutoSize = true;
            this.chkLock.Location = new System.Drawing.Point(470, 13);
            this.chkLock.Name = "chkLock";
            this.chkLock.Size = new System.Drawing.Size(48, 16);
            this.chkLock.TabIndex = 19;
            this.chkLock.Text = "锁定";
            this.chkLock.UseVisualStyleBackColor = true;
            // 
            // txtcRemark
            // 
            this.txtcRemark.Location = new System.Drawing.Point(91, 120);
            this.txtcRemark.Multiline = true;
            this.txtcRemark.Name = "txtcRemark";
            this.txtcRemark.Size = new System.Drawing.Size(357, 102);
            this.txtcRemark.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "备注：";
            // 
            // txtcMail
            // 
            this.txtcMail.Location = new System.Drawing.Point(91, 92);
            this.txtcMail.Name = "txtcMail";
            this.txtcMail.Size = new System.Drawing.Size(357, 21);
            this.txtcMail.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "邮件地址：";
            // 
            // txtcPwd
            // 
            this.txtcPwd.Location = new System.Drawing.Point(91, 65);
            this.txtcPwd.Name = "txtcPwd";
            this.txtcPwd.PasswordChar = '*';
            this.txtcPwd.Size = new System.Drawing.Size(357, 21);
            this.txtcPwd.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "密码：";
            // 
            // txtcName
            // 
            this.txtcName.Location = new System.Drawing.Point(91, 39);
            this.txtcName.Name = "txtcName";
            this.txtcName.Size = new System.Drawing.Size(357, 21);
            this.txtcName.TabIndex = 12;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(91, 13);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(357, 21);
            this.txtUserID.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "用户名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户登录ID：";
            // 
            // SysUserMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 583);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.groupBox1);
            this.Name = "SysUserMaster";
            this.TabText = "用戶主档";
            this.Text = "用戶主档";
            this.Load += new System.EventHandler(this.SysUserMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private WindowControls.UserToolBar ToolBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtdAddDate;
        private System.Windows.Forms.TextBox txtcAdd;
        private System.Windows.Forms.TextBox txtdLastDate;
        private System.Windows.Forms.TextBox txtcLast;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtcName;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtcPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtcMail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtcRemark;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkLock;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ListView lvwMaster;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnPurviewSetting;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cbisTrue;
        private System.Windows.Forms.TextBox txtCardNum;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnUserSearch;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnAddDataRole;
        private System.Windows.Forms.ListView lvwRole;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnDelDataRole;

    }
}