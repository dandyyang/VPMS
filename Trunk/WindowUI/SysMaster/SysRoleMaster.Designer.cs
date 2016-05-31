namespace WindowUI.SysMaster
{
    partial class SysRoleMaster
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
            this.btnDel = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.lvwMaster = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.label7 = new System.Windows.Forms.Label();
            this.txtcDesc = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolBar
            // 
            this.ToolBar.AutoSetStatus = true;
            this.ToolBar.BtnCancelEnabled = true;
            this.ToolBar.BtnCancelVisible = true;
            this.ToolBar.BtnCardMissingEnabled = true;
            this.ToolBar.BtnCardMissingVisible = false;
            this.ToolBar.BtnCardRecoveryEnabled = true;
            this.ToolBar.BtnCardRecoveryVisible = false;
            this.ToolBar.BtnCardReturnEnabled = true;
            this.ToolBar.BtnCardReturnVisible = false;
            this.ToolBar.BtnCardScrapEnabled = true;
            this.ToolBar.BtnCardScrapVisible = false;
            this.ToolBar.BtnDeleteEnabled = true;
            this.ToolBar.BtnDeleteVisible = true;
            this.ToolBar.BtnFirstEnabled = true;
            this.ToolBar.BtnFirstVisible = true;
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
            this.ToolBar.Size = new System.Drawing.Size(749, 31);
            this.ToolBar.TabIndex = 7;
            this.ToolBar.toolStripSeparator11Visible = true;
            this.ToolBar.toolStripSeparator12Visible = true;
            this.ToolBar.toolStripSeparator21Visible = true;
            this.ToolBar.toolStripSeparator22Visible = true;
            this.ToolBar.BtnPreviousClick += new System.EventHandler(this.ToolBar_BtnPreviousClick);
            this.ToolBar.BtnDeleteClick += new System.EventHandler(this.ToolBar_BtnDeleteClick);
            this.ToolBar.BtnSaveClick += new System.EventHandler(this.ToolBar_BtnSaveClick);
            this.ToolBar.BtnFirstClick += new System.EventHandler(this.ToolBar_BtnFirstClick);
            this.ToolBar.BtnNextClick += new System.EventHandler(this.ToolBar_BtnNextClick);
            this.ToolBar.BtnNewClick += new System.EventHandler(this.ToolBar_BtnNewClick);
            this.ToolBar.BtnCancelClick += new System.EventHandler(this.ToolBar_BtnCancelClick);
            this.ToolBar.BtnLastClick += new System.EventHandler(this.ToolBar_BtnLastClick);
            this.ToolBar.BtnSearchClick += new System.EventHandler(this.ToolBar_BtnSearchClick);
            this.ToolBar.BtnModifyClick += new System.EventHandler(this.ToolBar_BtnModifyClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(0, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 437);
            this.groupBox1.TabIndex = 6;
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
            this.groupBox4.Location = new System.Drawing.Point(3, 350);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(743, 84);
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
            this.txtdLastDate.Location = new System.Drawing.Point(611, 51);
            this.txtdLastDate.Name = "txtdLastDate";
            this.txtdLastDate.Size = new System.Drawing.Size(125, 21);
            this.txtdLastDate.TabIndex = 5;
            // 
            // txtcLast
            // 
            this.txtcLast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtcLast.BackColor = System.Drawing.SystemColors.Info;
            this.txtcLast.Enabled = false;
            this.txtcLast.Location = new System.Drawing.Point(611, 26);
            this.txtcLast.Name = "txtcLast";
            this.txtcLast.Size = new System.Drawing.Size(125, 21);
            this.txtcLast.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(551, 59);
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
            this.label11.Location = new System.Drawing.Point(551, 29);
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
            this.groupBox2.Controls.Add(this.btnDel);
            this.groupBox2.Controls.Add(this.btnNew);
            this.groupBox2.Controls.Add(this.lvwMaster);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtcDesc);
            this.groupBox2.Controls.Add(this.txtUserID);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 297);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(241, 263);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(65, 23);
            this.btnDel.TabIndex = 27;
            this.btnDel.Text = "刪除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(160, 263);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(65, 23);
            this.btnNew.TabIndex = 26;
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
            this.lvwMaster.Location = new System.Drawing.Point(91, 67);
            this.lvwMaster.Name = "lvwMaster";
            this.lvwMaster.Size = new System.Drawing.Size(214, 192);
            this.lvwMaster.TabIndex = 25;
            this.lvwMaster.UseCompatibleStateImageBehavior = false;
            this.lvwMaster.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "usm_cUserLoginID";
            this.columnHeader1.Text = "用戶ID";
            this.columnHeader1.Width = 81;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "usm_cChaName";
            this.columnHeader2.Text = "用户名称";
            this.columnHeader2.Width = 120;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 24;
            this.label7.Text = "拥有用户：";
            // 
            // txtcDesc
            // 
            this.txtcDesc.Location = new System.Drawing.Point(91, 39);
            this.txtcDesc.Name = "txtcDesc";
            this.txtcDesc.Size = new System.Drawing.Size(214, 21);
            this.txtcDesc.TabIndex = 12;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(91, 13);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(214, 21);
            this.txtUserID.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "角色描述：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "角色ID：";
            // 
            // SysRoleMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 472);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.groupBox1);
            this.Name = "SysRoleMaster";
            this.TabText = "角色主档";
            this.Text = "角色主档";
            this.Load += new System.EventHandler(this.SysRoleMaster_Load);
            this.groupBox1.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox txtcDesc;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.ListView lvwMaster;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label label7;
    }
}