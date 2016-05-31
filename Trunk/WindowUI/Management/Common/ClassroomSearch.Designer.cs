namespace WindowUI.Management.Common
{
    partial class ClassroomSearch
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lvwMstr = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbliCount = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtcClassroomName = new System.Windows.Forms.TextBox();
            this.txtcNum = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboBuilding = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSelect);
            this.groupBox2.Controls.Add(this.lvwMstr);
            this.groupBox2.Location = new System.Drawing.Point(3, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(548, 247);
            this.groupBox2.TabIndex = 55;
            this.groupBox2.TabStop = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect.Enabled = false;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSelect.Location = new System.Drawing.Point(12, 218);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(72, 22);
            this.btnSelect.TabIndex = 17;
            this.btnSelect.Text = "&S 选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lvwMstr
            // 
            this.lvwMstr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader9,
            this.columnHeader2,
            this.columnHeader3});
            this.lvwMstr.FullRowSelect = true;
            this.lvwMstr.GridLines = true;
            this.lvwMstr.HideSelection = false;
            this.lvwMstr.Location = new System.Drawing.Point(3, 10);
            this.lvwMstr.MultiSelect = false;
            this.lvwMstr.Name = "lvwMstr";
            this.lvwMstr.Size = new System.Drawing.Size(542, 203);
            this.lvwMstr.TabIndex = 16;
            this.lvwMstr.UseCompatibleStateImageBehavior = false;
            this.lvwMstr.View = System.Windows.Forms.View.Details;
            this.lvwMstr.SelectedIndexChanged += new System.EventHandler(this.lvwMstr_SelectedIndexChanged);
            this.lvwMstr.DoubleClick += new System.EventHandler(this.lvwMstr_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "stm_cNumber";
            this.columnHeader1.Text = "地点编号";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Tag = "BuildingName";
            this.columnHeader9.Text = "建筑物";
            this.columnHeader9.Width = 132;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "stm_cName";
            this.columnHeader2.Text = "课室";
            this.columnHeader2.Width = 215;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Tag = "stm_cRemark";
            this.columnHeader3.Text = "备注";
            this.columnHeader3.Width = 80;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lbliCount);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtcClassroomName);
            this.groupBox1.Controls.Add(this.txtcNum);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboBuilding);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(548, 72);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            // 
            // lbliCount
            // 
            this.lbliCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbliCount.Location = new System.Drawing.Point(417, 43);
            this.lbliCount.Name = "lbliCount";
            this.lbliCount.Size = new System.Drawing.Size(62, 15);
            this.lbliCount.TabIndex = 61;
            this.lbliCount.Text = "0";
            this.lbliCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnFind
            // 
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFind.Location = new System.Drawing.Point(441, 14);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(86, 22);
            this.btnFind.TabIndex = 59;
            this.btnFind.Text = "&F 搜寻";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Location = new System.Drawing.Point(486, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 60;
            this.label4.Text = "条记录";
            // 
            // txtcClassroomName
            // 
            this.txtcClassroomName.Location = new System.Drawing.Point(251, 38);
            this.txtcClassroomName.Name = "txtcClassroomName";
            this.txtcClassroomName.Size = new System.Drawing.Size(158, 21);
            this.txtcClassroomName.TabIndex = 57;
            // 
            // txtcNum
            // 
            this.txtcNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcNum.Location = new System.Drawing.Point(65, 38);
            this.txtcNum.Name = "txtcNum";
            this.txtcNum.Size = new System.Drawing.Size(120, 21);
            this.txtcNum.TabIndex = 54;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 55;
            this.label2.Text = "课室编号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(191, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 56;
            this.label3.Text = "课室名称:";
            // 
            // cboBuilding
            // 
            this.cboBuilding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBuilding.FormattingEnabled = true;
            this.cboBuilding.Location = new System.Drawing.Point(65, 14);
            this.cboBuilding.Name = "cboBuilding";
            this.cboBuilding.Size = new System.Drawing.Size(344, 20);
            this.cboBuilding.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 27;
            this.label1.Text = "建筑物：";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExit.Location = new System.Drawing.Point(476, 323);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 22);
            this.btnExit.TabIndex = 57;
            this.btnExit.Text = "&X 退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // ClassroomSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 355);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClassroomSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TabText = "课室搜寻";
            this.Text = "课室搜寻";
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ListView lvwMstr;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboBuilding;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtcClassroomName;
        private System.Windows.Forms.TextBox txtcNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbliCount;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExit;
    }
}