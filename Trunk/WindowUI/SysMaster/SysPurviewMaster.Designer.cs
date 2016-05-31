namespace WindowUI.SysMaster
{
    partial class SysPurviewMaster
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("江門利奧信領科技");
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnNUser = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnNRole = new System.Windows.Forms.Button();
            this.lvwUser = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.label1 = new System.Windows.Forms.Label();
            this.lvwPur = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label7 = new System.Windows.Forms.Label();
            this.tvwMain = new System.Windows.Forms.TreeView();
            this.userToolBar1 = new WindowControls.UserToolBar();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(0, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(805, 612);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
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
            this.groupBox4.Location = new System.Drawing.Point(3, 525);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(799, 84);
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
            this.txtdLastDate.Location = new System.Drawing.Point(667, 51);
            this.txtdLastDate.Name = "txtdLastDate";
            this.txtdLastDate.Size = new System.Drawing.Size(125, 21);
            this.txtdLastDate.TabIndex = 5;
            // 
            // txtcLast
            // 
            this.txtcLast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtcLast.BackColor = System.Drawing.SystemColors.Info;
            this.txtcLast.Enabled = false;
            this.txtcLast.Location = new System.Drawing.Point(667, 26);
            this.txtcLast.Name = "txtcLast";
            this.txtcLast.Size = new System.Drawing.Size(125, 21);
            this.txtcLast.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(607, 59);
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
            this.label11.Location = new System.Drawing.Point(607, 29);
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
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.tvwMain);
            this.groupBox2.Location = new System.Drawing.Point(6, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(636, 511);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.btnNUser);
            this.groupBox3.Controls.Add(this.btnDel);
            this.groupBox3.Controls.Add(this.btnNRole);
            this.groupBox3.Controls.Add(this.lvwUser);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lvwPur);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(286, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(344, 495);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            // 
            // btnNUser
            // 
            this.btnNUser.Location = new System.Drawing.Point(127, 221);
            this.btnNUser.Name = "btnNUser";
            this.btnNUser.Size = new System.Drawing.Size(65, 23);
            this.btnNUser.TabIndex = 53;
            this.btnNUser.Text = "新增用戶";
            this.btnNUser.UseVisualStyleBackColor = true;
            this.btnNUser.Click += new System.EventHandler(this.btnNUser_Click);
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(271, 222);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(65, 23);
            this.btnDel.TabIndex = 52;
            this.btnDel.Text = "刪除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnNRole
            // 
            this.btnNRole.Location = new System.Drawing.Point(201, 222);
            this.btnNRole.Name = "btnNRole";
            this.btnNRole.Size = new System.Drawing.Size(65, 23);
            this.btnNRole.TabIndex = 51;
            this.btnNRole.Text = "新增角色";
            this.btnNRole.UseVisualStyleBackColor = true;
            this.btnNRole.Click += new System.EventHandler(this.btnNRole_Click);
            // 
            // lvwUser
            // 
            this.lvwUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.lvwUser.FullRowSelect = true;
            this.lvwUser.GridLines = true;
            this.lvwUser.Location = new System.Drawing.Point(77, 19);
            this.lvwUser.MultiSelect = false;
            this.lvwUser.Name = "lvwUser";
            this.lvwUser.Size = new System.Drawing.Size(261, 197);
            this.lvwUser.TabIndex = 50;
            this.lvwUser.UseCompatibleStateImageBehavior = false;
            this.lvwUser.View = System.Windows.Forms.View.Details;
            this.lvwUser.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwUser_MouseDoubleClick);
            this.lvwUser.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwUser_MouseClick);
            this.lvwUser.SelectedIndexChanged += new System.EventHandler(this.lvwUser_SelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "对象ID";
            this.columnHeader3.Width = 81;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "对象描述";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "所属表";
            this.columnHeader5.Width = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "权限对象：";
            // 
            // lvwPur
            // 
            this.lvwPur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lvwPur.CheckBoxes = true;
            this.lvwPur.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvwPur.FullRowSelect = true;
            this.lvwPur.GridLines = true;
            this.lvwPur.Location = new System.Drawing.Point(79, 253);
            this.lvwPur.Name = "lvwPur";
            this.lvwPur.Size = new System.Drawing.Size(261, 236);
            this.lvwPur.TabIndex = 46;
            this.lvwPur.UseCompatibleStateImageBehavior = false;
            this.lvwPur.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "fum_cFunctionNumber";
            this.columnHeader1.Text = "功能编号";
            this.columnHeader1.Width = 133;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "fum_cFunctionDesc";
            this.columnHeader2.Text = "功能描述";
            this.columnHeader2.Width = 120;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 45;
            this.label7.Text = "拥有权限：";
            // 
            // tvwMain
            // 
            this.tvwMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvwMain.Location = new System.Drawing.Point(12, 16);
            this.tvwMain.Name = "tvwMain";
            treeNode1.Name = "Node0";
            treeNode1.Text = "江門利奧信領科技";
            this.tvwMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvwMain.Size = new System.Drawing.Size(246, 489);
            this.tvwMain.TabIndex = 28;
            this.tvwMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvwMain_MouseDoubleClick);
            this.tvwMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwMain_AfterSelect);
            // 
            // userToolBar1
            // 
            this.userToolBar1.AutoSetStatus = true;
            this.userToolBar1.BtnCancelEnabled = true;
            this.userToolBar1.BtnCancelVisible = true;
            this.userToolBar1.BtnCardIssuanceEnabled = true;
            this.userToolBar1.BtnCardIssuanceVisible = false;
            this.userToolBar1.BtnCardMissingEnabled = true;
            this.userToolBar1.BtnCardMissingVisible = false;
            this.userToolBar1.BtnCardRecoveryEnabled = true;
            this.userToolBar1.BtnCardRecoveryVisible = false;
            this.userToolBar1.BtnCardReturnEnabled = true;
            this.userToolBar1.BtnCardReturnVisible = false;
            this.userToolBar1.BtnCardScrapEnabled = true;
            this.userToolBar1.BtnCardScrapVisible = false;
            this.userToolBar1.BtnDataExportEnabled = true;
            this.userToolBar1.BtnDataExportVisible = false;
            this.userToolBar1.BtnDataInputEnabled = true;
            this.userToolBar1.BtnDataInputVisible = false;
            this.userToolBar1.BtnDeleteEnabled = true;
            this.userToolBar1.BtnDeleteVisible = false;
            this.userToolBar1.BtnExpCusDataEnabled = true;
            this.userToolBar1.BtnExpCusDataVisible = false;
            this.userToolBar1.BtnExportCardUserPhotoEnabled = true;
            this.userToolBar1.BtnExportCardUserPhotoVisible = false;
            this.userToolBar1.BtnExportTemplateEnabled = true;
            this.userToolBar1.BtnExportTemplateVisible = false;
            this.userToolBar1.BtnFirstEnabled = true;
            this.userToolBar1.BtnFirstVisible = false;
            this.userToolBar1.BtnGroupPersonEnabled = true;
            this.userToolBar1.BtnGroupPersonVisible = false;
            this.userToolBar1.BtnImportCardUserDataEnabled = true;
            this.userToolBar1.BtnImportCardUserDataVisible = false;
            this.userToolBar1.BtnImportPhotoEnabled = true;
            this.userToolBar1.BtnImportPhotoVisible = false;
            this.userToolBar1.BtnLastEnabled = true;
            this.userToolBar1.BtnLastVisible = false;
            this.userToolBar1.BtnModifyEnabled = true;
            this.userToolBar1.BtnModifyVisible = true;
            this.userToolBar1.BtnNewEnabled = true;
            this.userToolBar1.BtnNewVisible = false;
            this.userToolBar1.BtnNextEnabled = true;
            this.userToolBar1.BtnNextVisible = false;
            this.userToolBar1.BtnPreviousEnabled = true;
            this.userToolBar1.BtnPreviousVisible = false;
            this.userToolBar1.BtnSaveEnabled = true;
            this.userToolBar1.BtnSaveVisible = true;
            this.userToolBar1.BtnSearchEnabled = true;
            this.userToolBar1.BtnSearchVisible = false;
            this.userToolBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.userToolBar1.Location = new System.Drawing.Point(0, 0);
            this.userToolBar1.Name = "userToolBar1";
            this.userToolBar1.RecordExistPosition = WindowControls.UserToolBar.RecordIndexType.None;
            this.userToolBar1.Size = new System.Drawing.Size(805, 26);
            this.userToolBar1.TabIndex = 11;
            this.userToolBar1.toolStripSeparator11Visible = false;
            this.userToolBar1.toolStripSeparator12Visible = false;
            this.userToolBar1.toolStripSeparator21Visible = false;
            this.userToolBar1.toolStripSeparator22Visible = false;
            this.userToolBar1.BtnCancelClick += new System.EventHandler(this.userToolBar1_BtnCancelClick);
            this.userToolBar1.BtnModifyClick += new System.EventHandler(this.userToolBar1_BtnModifyClick);
            this.userToolBar1.BtnSaveClick += new System.EventHandler(this.userToolBar1_BtnSaveClick);
            // 
            // SysPurviewMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 634);
            this.Controls.Add(this.userToolBar1);
            this.Controls.Add(this.groupBox1);
            this.Name = "SysPurviewMaster";
            this.TabText = "权限设定";
            this.Text = "权限设定";
            this.Load += new System.EventHandler(this.SysPurviewMaster_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvwPur;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TreeView tvwMain;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnNRole;
        private System.Windows.Forms.ListView lvwUser;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNUser;
        private WindowControls.UserToolBar userToolBar1;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}