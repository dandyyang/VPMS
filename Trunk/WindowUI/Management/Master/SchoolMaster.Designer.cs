namespace WindowUI.Management.Master
{
    partial class SchoolMaster
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
            this.txtcRemark = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtcName = new System.Windows.Forms.TextBox();
            this.txtcNumber = new System.Windows.Forms.TextBox();
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
            this.ToolBar.BtnCancelEnabled = false;
            this.ToolBar.BtnCancelVisible = true;
            this.ToolBar.BtnCardMissingEnabled = true;
            this.ToolBar.BtnCardMissingVisible = false;
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
            this.ToolBar.BtnSaveEnabled = false;
            this.ToolBar.BtnSaveVisible = true;
            this.ToolBar.BtnSearchEnabled = true;
            this.ToolBar.BtnSearchVisible = true;
            this.ToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBar.Location = new System.Drawing.Point(0, 0);
            this.ToolBar.Name = "ToolBar";
            this.ToolBar.RecordExistPosition = WindowControls.UserToolBar.RecordIndexType.None;
            this.ToolBar.Size = new System.Drawing.Size(757, 26);
            this.ToolBar.TabIndex = 5;
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
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 329);
            this.groupBox1.TabIndex = 4;
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
            this.groupBox4.Location = new System.Drawing.Point(3, 242);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(751, 84);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            // 
            // txtdAddDate
            // 
            this.txtdAddDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtdAddDate.Enabled = false;
            this.txtdAddDate.Location = new System.Drawing.Point(72, 49);
            this.txtdAddDate.Name = "txtdAddDate";
            this.txtdAddDate.Size = new System.Drawing.Size(125, 22);
            this.txtdAddDate.TabIndex = 7;
            // 
            // txtcAdd
            // 
            this.txtcAdd.BackColor = System.Drawing.SystemColors.Info;
            this.txtcAdd.Enabled = false;
            this.txtcAdd.Location = new System.Drawing.Point(72, 24);
            this.txtcAdd.Name = "txtcAdd";
            this.txtcAdd.Size = new System.Drawing.Size(125, 22);
            this.txtcAdd.TabIndex = 6;
            // 
            // txtdLastDate
            // 
            this.txtdLastDate.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtdLastDate.BackColor = System.Drawing.SystemColors.Info;
            this.txtdLastDate.Enabled = false;
            this.txtdLastDate.Location = new System.Drawing.Point(619, 51);
            this.txtdLastDate.Name = "txtdLastDate";
            this.txtdLastDate.Size = new System.Drawing.Size(125, 22);
            this.txtdLastDate.TabIndex = 5;
            // 
            // txtcLast
            // 
            this.txtcLast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtcLast.BackColor = System.Drawing.SystemColors.Info;
            this.txtcLast.Enabled = false;
            this.txtcLast.Location = new System.Drawing.Point(619, 26);
            this.txtcLast.Name = "txtcLast";
            this.txtcLast.Size = new System.Drawing.Size(125, 22);
            this.txtcLast.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(559, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "修改时间:";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label11.Location = new System.Drawing.Point(559, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "修改者:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label12.Location = new System.Drawing.Point(13, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "新增时间:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label13.Location = new System.Drawing.Point(13, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "新增者:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtcRemark);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtcName);
            this.groupBox2.Controls.Add(this.txtcNumber);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 166);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // txtcRemark
            // 
            this.txtcRemark.BackColor = System.Drawing.SystemColors.Window;
            this.txtcRemark.Location = new System.Drawing.Point(85, 68);
            this.txtcRemark.Multiline = true;
            this.txtcRemark.Name = "txtcRemark";
            this.txtcRemark.Size = new System.Drawing.Size(286, 81);
            this.txtcRemark.TabIndex = 25;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 71);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 24;
            this.label14.Text = "备注：";
            // 
            // txtcName
            // 
            this.txtcName.Location = new System.Drawing.Point(85, 39);
            this.txtcName.Name = "txtcName";
            this.txtcName.Size = new System.Drawing.Size(286, 22);
            this.txtcName.TabIndex = 12;
            // 
            // txtcNumber
            // 
            this.txtcNumber.Location = new System.Drawing.Point(85, 13);
            this.txtcNumber.Name = "txtcNumber";
            this.txtcNumber.Size = new System.Drawing.Size(286, 22);
            this.txtcNumber.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(8, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "院系部名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(8, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "院系部编号：";
            // 
            // SchoolMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 357);
            this.Controls.Add(this.ToolBar);
            this.Controls.Add(this.groupBox1);
            this.Name = "SchoolMaster";
            this.TabText = "院系部主档";
            this.Text = "院系部主档";
            this.Load += new System.EventHandler(this.SchoolMaster_Load);
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
        private System.Windows.Forms.TextBox txtcRemark;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtcName;
        private System.Windows.Forms.TextBox txtcNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}