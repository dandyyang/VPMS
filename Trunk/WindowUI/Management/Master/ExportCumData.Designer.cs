namespace WindowUI.Management.Master
{
    partial class ExportCumData
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
            this.cbcSpecialty = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.dtpGraduationPeriod = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbcSchool = new System.Windows.Forms.ComboBox();
            this.cbcIdentityNum = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lstClass = new System.Windows.Forms.ListBox();
            this.lstDept = new System.Windows.Forms.ListBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbcSpecialty
            // 
            this.cbcSpecialty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcSpecialty.FormattingEnabled = true;
            this.cbcSpecialty.Location = new System.Drawing.Point(111, 112);
            this.cbcSpecialty.Name = "cbcSpecialty";
            this.cbcSpecialty.Size = new System.Drawing.Size(264, 20);
            this.cbcSpecialty.TabIndex = 49;
            this.cbcSpecialty.SelectedIndexChanged += new System.EventHandler(this.cbcSpecialty_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(23, 112);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 12);
            this.label16.TabIndex = 48;
            this.label16.Text = "专业：";
            // 
            // dtpGraduationPeriod
            // 
            this.dtpGraduationPeriod.CustomFormat = "yyyy";
            this.dtpGraduationPeriod.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGraduationPeriod.Location = new System.Drawing.Point(111, 139);
            this.dtpGraduationPeriod.Name = "dtpGraduationPeriod";
            this.dtpGraduationPeriod.ShowUpDown = true;
            this.dtpGraduationPeriod.Size = new System.Drawing.Size(264, 21);
            this.dtpGraduationPeriod.TabIndex = 47;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(23, 167);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 45;
            this.label15.Text = "班级：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 43;
            this.label10.Text = "科室：";
            // 
            // cbcSchool
            // 
            this.cbcSchool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcSchool.FormattingEnabled = true;
            this.cbcSchool.Location = new System.Drawing.Point(111, 38);
            this.cbcSchool.Name = "cbcSchool";
            this.cbcSchool.Size = new System.Drawing.Size(264, 20);
            this.cbcSchool.TabIndex = 42;
            this.cbcSchool.SelectedIndexChanged += new System.EventHandler(this.cbcSchool_SelectedIndexChanged);
            // 
            // cbcIdentityNum
            // 
            this.cbcIdentityNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcIdentityNum.FormattingEnabled = true;
            this.cbcIdentityNum.Location = new System.Drawing.Point(111, 11);
            this.cbcIdentityNum.Name = "cbcIdentityNum";
            this.cbcIdentityNum.Size = new System.Drawing.Size(264, 20);
            this.cbcIdentityNum.TabIndex = 41;
            this.cbcIdentityNum.SelectedIndexChanged += new System.EventHandler(this.cbcIdentityNum_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 40;
            this.label8.Text = "届别：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 39;
            this.label7.Text = "院系部：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 38;
            this.label6.Text = "身份：";
            // 
            // BtnOK
            // 
            this.BtnOK.Location = new System.Drawing.Point(220, 235);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(75, 23);
            this.BtnOK.TabIndex = 50;
            this.BtnOK.Text = "确认";
            this.BtnOK.UseVisualStyleBackColor = true;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(301, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 51;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 273);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(407, 22);
            this.statusStrip1.TabIndex = 52;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // lstClass
            // 
            this.lstClass.FormattingEnabled = true;
            this.lstClass.ItemHeight = 12;
            this.lstClass.Location = new System.Drawing.Point(111, 167);
            this.lstClass.Name = "lstClass";
            this.lstClass.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstClass.Size = new System.Drawing.Size(262, 40);
            this.lstClass.TabIndex = 53;
            // 
            // lstDept
            // 
            this.lstDept.FormattingEnabled = true;
            this.lstDept.ItemHeight = 12;
            this.lstDept.Location = new System.Drawing.Point(111, 65);
            this.lstDept.Name = "lstDept";
            this.lstDept.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lstDept.Size = new System.Drawing.Size(262, 40);
            this.lstDept.TabIndex = 54;
            // 
            // ExportCumData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 295);
            this.Controls.Add(this.lstDept);
            this.Controls.Add(this.lstClass);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.cbcSpecialty);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dtpGraduationPeriod);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbcSchool);
            this.Controls.Add(this.cbcIdentityNum);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExportCumData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TabText = "ExportCumData";
            this.Text = "导出卡用户数据";
            this.Load += new System.EventHandler(this.ExportCumData_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbcSpecialty;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker dtpGraduationPeriod;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbcSchool;
        private System.Windows.Forms.ComboBox cbcIdentityNum;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListBox lstClass;
        private System.Windows.Forms.ListBox lstDept;

    }
}