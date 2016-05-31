namespace WindowUI.Management.Common
{
    partial class CardUserSearch
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
            this.lbliCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtcChinaName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtcCNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFinishSelect = new System.Windows.Forms.Button();
            this.lvwMstr = new System.Windows.Forms.ListView();
            this.cus_cNumber = new System.Windows.Forms.ColumnHeader();
            this.cus_cDepartmentNum = new System.Windows.Forms.ColumnHeader();
            this.cus_cChaName = new System.Windows.Forms.ColumnHeader();
            this.cus_cSMSReceivePhone = new System.Windows.Forms.ColumnHeader();
            this.cus_cMailAddress = new System.Windows.Forms.ColumnHeader();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbliCount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.txtcChinaName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtcCNum);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(508, 69);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // lbliCount
            // 
            this.lbliCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbliCount.Location = new System.Drawing.Point(412, 42);
            this.lbliCount.Name = "lbliCount";
            this.lbliCount.Size = new System.Drawing.Size(39, 12);
            this.lbliCount.TabIndex = 49;
            this.lbliCount.Text = "0";
            this.lbliCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Location = new System.Drawing.Point(457, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 48;
            this.label7.Text = "条记录";
            // 
            // btnFind
            // 
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFind.ImageIndex = 0;
            this.btnFind.Location = new System.Drawing.Point(414, 17);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(86, 22);
            this.btnFind.TabIndex = 47;
            this.btnFind.Text = "&F 搜寻";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtcChinaName
            // 
            this.txtcChinaName.Location = new System.Drawing.Point(258, 19);
            this.txtcChinaName.Name = "txtcChinaName";
            this.txtcChinaName.Size = new System.Drawing.Size(140, 21);
            this.txtcChinaName.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(198, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "用戶名称:";
            // 
            // txtcCNum
            // 
            this.txtcCNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcCNum.Location = new System.Drawing.Point(66, 18);
            this.txtcCNum.Name = "txtcCNum";
            this.txtcCNum.Size = new System.Drawing.Size(107, 21);
            this.txtcCNum.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "用戶编号:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnFinishSelect);
            this.groupBox2.Controls.Add(this.lvwMstr);
            this.groupBox2.Location = new System.Drawing.Point(3, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(504, 261);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // btnFinishSelect
            // 
            this.btnFinishSelect.Location = new System.Drawing.Point(6, 229);
            this.btnFinishSelect.Name = "btnFinishSelect";
            this.btnFinishSelect.Size = new System.Drawing.Size(75, 23);
            this.btnFinishSelect.TabIndex = 26;
            this.btnFinishSelect.Text = "&S 选择";
            this.btnFinishSelect.UseVisualStyleBackColor = true;
            this.btnFinishSelect.Click += new System.EventHandler(this.BtnFinishSelect_Click);
            // 
            // lvwMstr
            // 
            this.lvwMstr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwMstr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cus_cNumber,
            this.cus_cDepartmentNum,
            this.cus_cChaName,
            this.cus_cSMSReceivePhone,
            this.cus_cMailAddress});
            this.lvwMstr.FullRowSelect = true;
            this.lvwMstr.GridLines = true;
            this.lvwMstr.HideSelection = false;
            this.lvwMstr.Location = new System.Drawing.Point(3, 18);
            this.lvwMstr.Name = "lvwMstr";
            this.lvwMstr.Size = new System.Drawing.Size(500, 206);
            this.lvwMstr.TabIndex = 16;
            this.lvwMstr.UseCompatibleStateImageBehavior = false;
            this.lvwMstr.View = System.Windows.Forms.View.Details;
            this.lvwMstr.DoubleClick += new System.EventHandler(this.lvwMstr_DoubleClick);
            // 
            // cus_cNumber
            // 
            this.cus_cNumber.Text = "用戶编号";
            this.cus_cNumber.Width = 0;
            // 
            // cus_cDepartmentNum
            // 
            this.cus_cDepartmentNum.Text = "所属科室";
            this.cus_cDepartmentNum.Width = 104;
            // 
            // cus_cChaName
            // 
            this.cus_cChaName.Text = "姓名";
            this.cus_cChaName.Width = 72;
            // 
            // cus_cSMSReceivePhone
            // 
            this.cus_cSMSReceivePhone.Text = "短信接收电话";
            this.cus_cSMSReceivePhone.Width = 108;
            // 
            // cus_cMailAddress
            // 
            this.cus_cMailAddress.Text = "电邮地址";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExit.Location = new System.Drawing.Point(423, 340);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 22);
            this.btnExit.TabIndex = 25;
            this.btnExit.Text = "&X 退出";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // CardUserSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 373);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CardUserSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "讲师搜寻";
            this.Text = "讲师搜寻";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbliCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtcChinaName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtcCNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvwMstr;
        private System.Windows.Forms.ColumnHeader cus_cNumber;
        private System.Windows.Forms.ColumnHeader cus_cChaName;
        private System.Windows.Forms.Button btnFinishSelect;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ColumnHeader cus_cSMSReceivePhone;
        private System.Windows.Forms.ColumnHeader cus_cMailAddress;
        private System.Windows.Forms.ColumnHeader cus_cDepartmentNum;
    }
}