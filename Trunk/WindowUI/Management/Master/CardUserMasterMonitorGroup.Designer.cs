namespace WindowUI.Management.Master
{
    partial class CardUserMasterMonitorGroup
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lvwPerson = new System.Windows.Forms.ListView();
            this.checkbox = new System.Windows.Forms.ColumnHeader();
            this.cus_iRecordID = new System.Windows.Forms.ColumnHeader();
            this.cus_cChaName = new System.Windows.Forms.ColumnHeader();
            this.cus_cEngName = new System.Windows.Forms.ColumnHeader();
            this.cus_cSex = new System.Windows.Forms.ColumnHeader();
            this.cus_cGraduationPeriod = new System.Windows.Forms.ColumnHeader();
            this.spm_cName = new System.Windows.Forms.ColumnHeader();
            this.cus_cClassNum = new System.Windows.Forms.ColumnHeader();
            this.cus_cGroupNum = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbcMonitorGroup = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.cbcIdentityNum = new System.Windows.Forms.ComboBox();
            this.cbcSexNum = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbcSpecialty = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dtpGraduationPeriod = new System.Windows.Forms.DateTimePicker();
            this.cbcClass = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbcDepartment = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbcSchool = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lvwPerson);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.btnExit);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(997, 549);
            this.splitContainer1.SplitterDistance = 702;
            this.splitContainer1.TabIndex = 0;
            // 
            // lvwPerson
            // 
            this.lvwPerson.CheckBoxes = true;
            this.lvwPerson.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.checkbox,
            this.cus_iRecordID,
            this.cus_cChaName,
            this.cus_cEngName,
            this.cus_cSex,
            this.cus_cGraduationPeriod,
            this.spm_cName,
            this.cus_cClassNum,
            this.cus_cGroupNum});
            this.lvwPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwPerson.FullRowSelect = true;
            this.lvwPerson.Location = new System.Drawing.Point(0, 0);
            this.lvwPerson.Name = "lvwPerson";
            this.lvwPerson.Size = new System.Drawing.Size(702, 549);
            this.lvwPerson.TabIndex = 0;
            this.lvwPerson.UseCompatibleStateImageBehavior = false;
            this.lvwPerson.View = System.Windows.Forms.View.Details;
            // 
            // checkbox
            // 
            this.checkbox.Text = "";
            this.checkbox.Width = 20;
            // 
            // cus_iRecordID
            // 
            this.cus_iRecordID.Tag = "cus_iRecordID";
            this.cus_iRecordID.Text = "cus_iRecordID";
            this.cus_iRecordID.Width = 0;
            // 
            // cus_cChaName
            // 
            this.cus_cChaName.Tag = "cus_cChaName";
            this.cus_cChaName.Text = "中文名称";
            this.cus_cChaName.Width = 83;
            // 
            // cus_cEngName
            // 
            this.cus_cEngName.Tag = "cus_cEngName";
            this.cus_cEngName.Text = "英文名称";
            this.cus_cEngName.Width = 95;
            // 
            // cus_cSex
            // 
            this.cus_cSex.Tag = "cus_cSex";
            this.cus_cSex.Text = "性别";
            // 
            // cus_cGraduationPeriod
            // 
            this.cus_cGraduationPeriod.Tag = "cus_cGraduationPeriod";
            this.cus_cGraduationPeriod.Text = "届别";
            // 
            // spm_cName
            // 
            this.spm_cName.Tag = "spm_cName";
            this.spm_cName.Text = "专业";
            this.spm_cName.Width = 115;
            // 
            // cus_cClassNum
            // 
            this.cus_cClassNum.Tag = "cus_cClassNum";
            this.cus_cClassNum.Text = "班级";
            // 
            // cus_cGroupNum
            // 
            this.cus_cGroupNum.Tag = "cus_cGroupNum";
            this.cus_cGroupNum.Text = "规则组别";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbcMonitorGroup);
            this.groupBox2.Location = new System.Drawing.Point(6, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 77);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "规则组别";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 50;
            this.label1.Text = "组别：";
            // 
            // cbcMonitorGroup
            // 
            this.cbcMonitorGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcMonitorGroup.FormattingEnabled = true;
            this.cbcMonitorGroup.Location = new System.Drawing.Point(50, 31);
            this.cbcMonitorGroup.Name = "cbcMonitorGroup";
            this.cbcMonitorGroup.Size = new System.Drawing.Size(210, 20);
            this.cbcMonitorGroup.TabIndex = 8;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(211, 498);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 39);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "&E 退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(130, 498);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 39);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "&O 设置";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectNone);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.cbcIdentityNum);
            this.groupBox1.Controls.Add(this.cbcSexNum);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbcSpecialty);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.dtpGraduationPeriod);
            this.groupBox1.Controls.Add(this.cbcClass);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.cbcDepartment);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbcSchool);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(2, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 239);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "人员";
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Location = new System.Drawing.Point(142, 207);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(54, 22);
            this.btnSelectNone.TabIndex = 51;
            this.btnSelectNone.Text = "&C 反选";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(83, 207);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(54, 22);
            this.btnSelectAll.TabIndex = 50;
            this.btnSelectAll.Text = "&A 全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(202, 207);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(60, 22);
            this.btnFind.TabIndex = 8;
            this.btnFind.Text = "&F 搜寻";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // cbcIdentityNum
            // 
            this.cbcIdentityNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcIdentityNum.FormattingEnabled = true;
            this.cbcIdentityNum.Location = new System.Drawing.Point(52, 50);
            this.cbcIdentityNum.Name = "cbcIdentityNum";
            this.cbcIdentityNum.Size = new System.Drawing.Size(210, 20);
            this.cbcIdentityNum.TabIndex = 2;
            // 
            // cbcSexNum
            // 
            this.cbcSexNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcSexNum.FormattingEnabled = true;
            this.cbcSexNum.Location = new System.Drawing.Point(52, 24);
            this.cbcSexNum.Name = "cbcSexNum";
            this.cbcSexNum.Size = new System.Drawing.Size(209, 20);
            this.cbcSexNum.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 49;
            this.label6.Text = "身份：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "性別：";
            // 
            // cbcSpecialty
            // 
            this.cbcSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcSpecialty.FormattingEnabled = true;
            this.cbcSpecialty.Location = new System.Drawing.Point(52, 128);
            this.cbcSpecialty.Name = "cbcSpecialty";
            this.cbcSpecialty.Size = new System.Drawing.Size(210, 20);
            this.cbcSpecialty.TabIndex = 5;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(14, 131);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 46;
            this.label16.Text = "专业：";
            // 
            // dtpGraduationPeriod
            // 
            this.dtpGraduationPeriod.CustomFormat = "yyyy";
            this.dtpGraduationPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGraduationPeriod.Location = new System.Drawing.Point(52, 154);
            this.dtpGraduationPeriod.Name = "dtpGraduationPeriod";
            this.dtpGraduationPeriod.ShowUpDown = true;
            this.dtpGraduationPeriod.Size = new System.Drawing.Size(210, 21);
            this.dtpGraduationPeriod.TabIndex = 6;
            // 
            // cbcClass
            // 
            this.cbcClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcClass.FormattingEnabled = true;
            this.cbcClass.Location = new System.Drawing.Point(52, 181);
            this.cbcClass.Name = "cbcClass";
            this.cbcClass.Size = new System.Drawing.Size(210, 20);
            this.cbcClass.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(14, 184);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 43;
            this.label15.Text = "班级：";
            // 
            // cbcDepartment
            // 
            this.cbcDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcDepartment.FormattingEnabled = true;
            this.cbcDepartment.Location = new System.Drawing.Point(52, 102);
            this.cbcDepartment.Name = "cbcDepartment";
            this.cbcDepartment.Size = new System.Drawing.Size(210, 20);
            this.cbcDepartment.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 41;
            this.label10.Text = "科室：";
            // 
            // cbcSchool
            // 
            this.cbcSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcSchool.FormattingEnabled = true;
            this.cbcSchool.Location = new System.Drawing.Point(52, 76);
            this.cbcSchool.Name = "cbcSchool";
            this.cbcSchool.Size = new System.Drawing.Size(210, 20);
            this.cbcSchool.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 39;
            this.label8.Text = "届别：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(2, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 38;
            this.label7.Text = "院系部：";
            // 
            // CardUserMasterMonitorGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 549);
            this.Controls.Add(this.splitContainer1);
            this.Name = "CardUserMasterMonitorGroup";
            this.Text = "卡用户批量添加到监控规则组别";
            this.Load += new System.EventHandler(this.CardUserMasterMonitorGroup_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvwPerson;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbcSpecialty;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpGraduationPeriod;
        private System.Windows.Forms.ComboBox cbcClass;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbcDepartment;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbcSchool;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbcIdentityNum;
        private System.Windows.Forms.ComboBox cbcSexNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.ColumnHeader cus_iRecordID;
        private System.Windows.Forms.ColumnHeader cus_cChaName;
        private System.Windows.Forms.ColumnHeader cus_cEngName;
        private System.Windows.Forms.ColumnHeader cus_cSex;
        private System.Windows.Forms.ColumnHeader cus_cGraduationPeriod;
        private System.Windows.Forms.ColumnHeader spm_cName;
        private System.Windows.Forms.ColumnHeader cus_cClassNum;
        private System.Windows.Forms.ColumnHeader cus_cGroupNum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbcMonitorGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.ColumnHeader checkbox;
        private System.Windows.Forms.Button btnSelectNone;
    }
}