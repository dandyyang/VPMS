namespace WindowUI.SystemForm
{
    partial class CodeMasterFormSearch
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtfNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtcValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbliCount = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbocKey2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbocKey1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lvwMstr = new System.Windows.Forms.ListView();
            this.cmt_iRecordID = new System.Windows.Forms.ColumnHeader();
            this.cmt_cKey1 = new System.Windows.Forms.ColumnHeader();
            this.cmt_cKey2 = new System.Windows.Forms.ColumnHeader();
            this.cmt_cValue = new System.Windows.Forms.ColumnHeader();
            this.cmt_fNumber = new System.Windows.Forms.ColumnHeader();
            this.cmt_cRemark = new System.Windows.Forms.ColumnHeader();
            this.cmt_cRemark2 = new System.Windows.Forms.ColumnHeader();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtfNum);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtcValue);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbliCount);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbocKey2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbocKey1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtfNum
            // 
            this.txtfNum.Location = new System.Drawing.Point(355, 44);
            this.txtfNum.Name = "txtfNum";
            this.txtfNum.Size = new System.Drawing.Size(227, 21);
            this.txtfNum.TabIndex = 71;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Location = new System.Drawing.Point(308, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 70;
            this.label5.Text = "数字值：";
            // 
            // txtcValue
            // 
            this.txtcValue.Location = new System.Drawing.Point(52, 45);
            this.txtcValue.Name = "txtcValue";
            this.txtcValue.Size = new System.Drawing.Size(227, 21);
            this.txtcValue.TabIndex = 69;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(16, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 68;
            this.label4.Text = "键值：";
            // 
            // lbliCount
            // 
            this.lbliCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbliCount.Location = new System.Drawing.Point(657, 46);
            this.lbliCount.Name = "lbliCount";
            this.lbliCount.Size = new System.Drawing.Size(62, 20);
            this.lbliCount.TabIndex = 67;
            this.lbliCount.Text = "0";
            this.lbliCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnFind
            // 
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFind.Location = new System.Drawing.Point(681, 18);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(86, 22);
            this.btnFind.TabIndex = 65;
            this.btnFind.Text = "&F 搜寻";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(726, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 66;
            this.label3.Text = "条记录";
            // 
            // cbocKey2
            // 
            this.cbocKey2.FormattingEnabled = true;
            this.cbocKey2.Location = new System.Drawing.Point(355, 19);
            this.cbocKey2.Name = "cbocKey2";
            this.cbocKey2.Size = new System.Drawing.Size(227, 20);
            this.cbocKey2.TabIndex = 64;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(312, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 63;
            this.label2.Text = "主键2：";
            // 
            // cbocKey1
            // 
            this.cbocKey1.FormattingEnabled = true;
            this.cbocKey1.Location = new System.Drawing.Point(52, 19);
            this.cbocKey1.Name = "cbocKey1";
            this.cbocKey1.Size = new System.Drawing.Size(227, 20);
            this.cbocKey1.TabIndex = 62;
            this.cbocKey1.SelectedValueChanged += new System.EventHandler(this.cbocKey1_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "主键1：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSelect);
            this.groupBox2.Controls.Add(this.lvwMstr);
            this.groupBox2.Location = new System.Drawing.Point(0, 86);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(784, 340);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect.Enabled = false;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSelect.Location = new System.Drawing.Point(12, 314);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(72, 22);
            this.btnSelect.TabIndex = 18;
            this.btnSelect.Text = "&S 选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lvwMstr
            // 
            this.lvwMstr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwMstr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cmt_iRecordID,
            this.cmt_cKey1,
            this.cmt_cKey2,
            this.cmt_cValue,
            this.cmt_fNumber,
            this.cmt_cRemark,
            this.cmt_cRemark2});
            this.lvwMstr.FullRowSelect = true;
            this.lvwMstr.GridLines = true;
            this.lvwMstr.HideSelection = false;
            this.lvwMstr.Location = new System.Drawing.Point(3, 16);
            this.lvwMstr.MultiSelect = false;
            this.lvwMstr.Name = "lvwMstr";
            this.lvwMstr.Size = new System.Drawing.Size(781, 294);
            this.lvwMstr.TabIndex = 17;
            this.lvwMstr.UseCompatibleStateImageBehavior = false;
            this.lvwMstr.View = System.Windows.Forms.View.Details;
            this.lvwMstr.SelectedIndexChanged += new System.EventHandler(this.lvwMstr_SelectedIndexChanged);
            this.lvwMstr.DoubleClick += new System.EventHandler(this.lvwMstr_DoubleClick);
            // 
            // cmt_iRecordID
            // 
            this.cmt_iRecordID.Tag = "cmt_iRecordID";
            this.cmt_iRecordID.Text = "紀錄ID";
            this.cmt_iRecordID.Width = 0;
            // 
            // cmt_cKey1
            // 
            this.cmt_cKey1.Tag = "cmt_cKey1";
            this.cmt_cKey1.Text = "主键1";
            this.cmt_cKey1.Width = 180;
            // 
            // cmt_cKey2
            // 
            this.cmt_cKey2.Tag = "cmt_cKey2";
            this.cmt_cKey2.Text = "主键2";
            this.cmt_cKey2.Width = 70;
            // 
            // cmt_cValue
            // 
            this.cmt_cValue.Tag = "cmt_cValue";
            this.cmt_cValue.Text = "键值";
            this.cmt_cValue.Width = 150;
            // 
            // cmt_fNumber
            // 
            this.cmt_fNumber.Tag = "cmt_fNumber";
            this.cmt_fNumber.Text = "数值";
            // 
            // cmt_cRemark
            // 
            this.cmt_cRemark.Tag = "cmt_cRemark";
            this.cmt_cRemark.Text = "备注1";
            this.cmt_cRemark.Width = 220;
            // 
            // cmt_cRemark2
            // 
            this.cmt_cRemark2.Tag = "cmt_cRemark2";
            this.cmt_cRemark2.Text = "备注2";
            this.cmt_cRemark2.Width = 80;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExit.Location = new System.Drawing.Point(700, 430);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 22);
            this.btnExit.TabIndex = 30;
            this.btnExit.Text = "&X 退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // CodeMasterFormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 458);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CodeMasterFormSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TabText = "字码搜寻";
            this.Text = "字码搜寻";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbocKey1;
        private System.Windows.Forms.ComboBox cbocKey2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbliCount;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtcValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtfNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lvwMstr;
        private System.Windows.Forms.ColumnHeader cmt_iRecordID;
        private System.Windows.Forms.ColumnHeader cmt_cKey1;
        private System.Windows.Forms.ColumnHeader cmt_cKey2;
        private System.Windows.Forms.ColumnHeader cmt_cValue;
        private System.Windows.Forms.ColumnHeader cmt_fNumber;
        private System.Windows.Forms.ColumnHeader cmt_cRemark;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ColumnHeader cmt_cRemark2;
    }
}