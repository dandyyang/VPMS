namespace WindowUI.SystemForm
{
    partial class CodeMasterForm
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
            this.txtcRemark2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbocKey1 = new System.Windows.Forms.ComboBox();
            this.txtcRemark = new System.Windows.Forms.TextBox();
            this.txtfNum = new System.Windows.Forms.TextBox();
            this.txtcValue = new System.Windows.Forms.TextBox();
            this.cbocKey2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtdAddDate = new System.Windows.Forms.TextBox();
            this.txtcAdd = new System.Windows.Forms.TextBox();
            this.txtdLastDate = new System.Windows.Forms.TextBox();
            this.txtcLast = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolBar
            // 
            this.ToolBar.AutoSetStatus = true;
            this.ToolBar.BtnCancelEnabled = false;
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
            this.ToolBar.BtnDataInputEnabled = true;
            this.ToolBar.BtnDataInputVisible = false;
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
            this.ToolBar.BtnSaveEnabled = false;
            this.ToolBar.BtnSaveVisible = true;
            this.ToolBar.BtnSearchEnabled = true;
            this.ToolBar.BtnSearchVisible = true;
            this.ToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBar.Location = new System.Drawing.Point(0, 0);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.RecordExistPosition = WindowControls.UserToolBar.RecordIndexType.None;
            this.ToolBar.Size = new System.Drawing.Size(813, 26);
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
            this.groupBox1.Controls.Add(this.txtcRemark2);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.cbocKey1);
            this.groupBox1.Controls.Add(this.txtcRemark);
            this.groupBox1.Controls.Add(this.txtfNum);
            this.groupBox1.Controls.Add(this.txtcValue);
            this.groupBox1.Controls.Add(this.cbocKey2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(813, 519);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // txtcRemark2
            // 
            this.txtcRemark2.Location = new System.Drawing.Point(81, 228);
            this.txtcRemark2.Multiline = true;
            this.txtcRemark2.Name = "txtcRemark2";
            this.txtcRemark2.Size = new System.Drawing.Size(492, 88);
            this.txtcRemark2.TabIndex = 51;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label11.Location = new System.Drawing.Point(29, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 50;
            this.label11.Text = "备注2:";
            // 
            // cbocKey1
            // 
            this.cbocKey1.Location = new System.Drawing.Point(79, 20);
            this.cbocKey1.Name = "cbocKey1";
            this.cbocKey1.Size = new System.Drawing.Size(494, 20);
            this.cbocKey1.TabIndex = 49;
            this.cbocKey1.SelectedValueChanged += new System.EventHandler(this.cbocKey1_SelectedValueChanged);
            // 
            // txtcRemark
            // 
            this.txtcRemark.Location = new System.Drawing.Point(81, 132);
            this.txtcRemark.Multiline = true;
            this.txtcRemark.Name = "txtcRemark";
            this.txtcRemark.Size = new System.Drawing.Size(492, 88);
            this.txtcRemark.TabIndex = 48;
            // 
            // txtfNum
            // 
            this.txtfNum.Location = new System.Drawing.Point(81, 104);
            this.txtfNum.Name = "txtfNum";
            this.txtfNum.Size = new System.Drawing.Size(492, 21);
            this.txtfNum.TabIndex = 47;
            // 
            // txtcValue
            // 
            this.txtcValue.Location = new System.Drawing.Point(81, 76);
            this.txtcValue.Name = "txtcValue";
            this.txtcValue.Size = new System.Drawing.Size(492, 21);
            this.txtcValue.TabIndex = 46;
            // 
            // cbocKey2
            // 
            this.cbocKey2.Location = new System.Drawing.Point(81, 49);
            this.cbocKey2.Name = "cbocKey2";
            this.cbocKey2.Size = new System.Drawing.Size(492, 20);
            this.cbocKey2.TabIndex = 45;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label6.Location = new System.Drawing.Point(29, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 44;
            this.label6.Text = "备注:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Location = new System.Drawing.Point(29, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 43;
            this.label5.Text = "数字值:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(29, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "Value:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(29, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 41;
            this.label3.Text = "主键二:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(29, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 40;
            this.label2.Text = "主键一:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtdAddDate);
            this.groupBox4.Controls.Add(this.txtcAdd);
            this.groupBox4.Controls.Add(this.txtdLastDate);
            this.groupBox4.Controls.Add(this.txtcLast);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(-2, 443);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(816, 76);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            // 
            // txtdAddDate
            // 
            this.txtdAddDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtdAddDate.Enabled = false;
            this.txtdAddDate.Location = new System.Drawing.Point(86, 46);
            this.txtdAddDate.Name = "txtdAddDate";
            this.txtdAddDate.Size = new System.Drawing.Size(150, 21);
            this.txtdAddDate.TabIndex = 7;
            // 
            // txtcAdd
            // 
            this.txtcAdd.BackColor = System.Drawing.SystemColors.Info;
            this.txtcAdd.Enabled = false;
            this.txtcAdd.Location = new System.Drawing.Point(86, 22);
            this.txtcAdd.Name = "txtcAdd";
            this.txtcAdd.Size = new System.Drawing.Size(150, 21);
            this.txtcAdd.TabIndex = 6;
            // 
            // txtdLastDate
            // 
            this.txtdLastDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtdLastDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtdLastDate.Enabled = false;
            this.txtdLastDate.Location = new System.Drawing.Point(658, 48);
            this.txtdLastDate.Name = "txtdLastDate";
            this.txtdLastDate.Size = new System.Drawing.Size(150, 21);
            this.txtdLastDate.TabIndex = 5;
            // 
            // txtcLast
            // 
            this.txtcLast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtcLast.BackColor = System.Drawing.SystemColors.Info;
            this.txtcLast.Enabled = false;
            this.txtcLast.Location = new System.Drawing.Point(658, 24);
            this.txtcLast.Name = "txtcLast";
            this.txtcLast.Size = new System.Drawing.Size(150, 21);
            this.txtcLast.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label10.Location = new System.Drawing.Point(586, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "修改时间:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label9.Location = new System.Drawing.Point(586, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "修改者:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label8.Location = new System.Drawing.Point(16, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "新增时间:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Location = new System.Drawing.Point(16, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "新增者:";
            // 
            // CodeMasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 545);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ToolBar);
            this.Name = "CodeMasterForm";
            this.TabText = "字码主档";
            this.Text = "字码主档";
            this.Load += new System.EventHandler(this.CodeMasterForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtcRemark2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbocKey1;
        private System.Windows.Forms.TextBox txtcRemark;
        private System.Windows.Forms.TextBox txtfNum;
        private System.Windows.Forms.TextBox txtcValue;
        private System.Windows.Forms.ComboBox cbocKey2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}