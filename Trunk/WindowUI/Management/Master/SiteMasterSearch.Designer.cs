namespace WindowUI.Management.Master
{
    partial class SiteMasterSearch
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
            this.lbliCount = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lvwMstr = new System.Windows.Forms.ListView();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtcChinaName = new System.Windows.Forms.TextBox();
            this.txtCNum = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbliCount
            // 
            this.lbliCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbliCount.Location = new System.Drawing.Point(609, 37);
            this.lbliCount.Name = "lbliCount";
            this.lbliCount.Size = new System.Drawing.Size(62, 20);
            this.lbliCount.TabIndex = 58;
            this.lbliCount.Text = "0";
            this.lbliCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSelect);
            this.groupBox2.Controls.Add(this.lvwMstr);
            this.groupBox2.Location = new System.Drawing.Point(0, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(738, 296);
            this.groupBox2.TabIndex = 54;
            this.groupBox2.TabStop = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelect.Enabled = false;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSelect.Location = new System.Drawing.Point(12, 267);
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
            this.columnHeader8,
            this.columnHeader1,
            this.columnHeader9,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvwMstr.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvwMstr.FullRowSelect = true;
            this.lvwMstr.GridLines = true;
            this.lvwMstr.HideSelection = false;
            this.lvwMstr.Location = new System.Drawing.Point(3, 17);
            this.lvwMstr.MultiSelect = false;
            this.lvwMstr.Name = "lvwMstr";
            this.lvwMstr.Size = new System.Drawing.Size(732, 241);
            this.lvwMstr.TabIndex = 16;
            this.lvwMstr.UseCompatibleStateImageBehavior = false;
            this.lvwMstr.View = System.Windows.Forms.View.Details;
            this.lvwMstr.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwMstr_MouseDoubleClick);
            this.lvwMstr.SelectedIndexChanged += new System.EventHandler(this.lvwMstr_SelectedIndexChanged);
            this.lvwMstr.DoubleClick += new System.EventHandler(this.lvwMstr_DoubleClick);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Tag = "stm_iRecordID";
            this.columnHeader8.Text = "记录ID";
            this.columnHeader8.Width = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "stm_cNumber";
            this.columnHeader1.Text = "地点编号";
            this.columnHeader1.Width = 105;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Tag = "BuildingName";
            this.columnHeader9.Text = "建築物名";
            this.columnHeader9.Width = 115;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "stm_cName";
            this.columnHeader2.Text = "地点名称";
            this.columnHeader2.Width = 105;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Tag = "stm_cRemark";
            this.columnHeader3.Text = "备注";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Tag = "stm_cAdd";
            this.columnHeader4.Text = "新增者";
            this.columnHeader4.Width = 80;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Tag = "stm_dAddDate";
            this.columnHeader5.Text = "新增时间";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Tag = "stm_cLast";
            this.columnHeader6.Text = "修改者";
            this.columnHeader6.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Tag = "stm_dLastDate";
            this.columnHeader7.Text = "修改时间";
            this.columnHeader7.Width = 80;
            // 
            // btnExit
            // 
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExit.Location = new System.Drawing.Point(650, 366);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 22);
            this.btnExit.TabIndex = 55;
            this.btnExit.Text = "&X 退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnFind
            // 
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFind.Location = new System.Drawing.Point(633, 8);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(86, 22);
            this.btnFind.TabIndex = 56;
            this.btnFind.Text = "&F 搜寻";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtcChinaName
            // 
            this.txtcChinaName.Location = new System.Drawing.Point(256, 8);
            this.txtcChinaName.Name = "txtcChinaName";
            this.txtcChinaName.Size = new System.Drawing.Size(283, 21);
            this.txtcChinaName.TabIndex = 53;
            // 
            // txtCNum
            // 
            this.txtCNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCNum.Location = new System.Drawing.Point(65, 8);
            this.txtCNum.Name = "txtCNum";
            this.txtCNum.Size = new System.Drawing.Size(120, 21);
            this.txtCNum.TabIndex = 50;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(678, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 57;
            this.label3.Text = "条记录";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 51;
            this.label1.Text = "地点编号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(196, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 52;
            this.label2.Text = "地点名称:";
            // 
            // SiteMasterSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 396);
            this.Controls.Add(this.lbliCount);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtcChinaName);
            this.Controls.Add(this.txtCNum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SiteMasterSearch";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TabText = "地点主档搜索";
            this.Text = "地点主档搜索";
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbliCount;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ListView lvwMstr;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtcChinaName;
        private System.Windows.Forms.TextBox txtCNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader9;

    }
}