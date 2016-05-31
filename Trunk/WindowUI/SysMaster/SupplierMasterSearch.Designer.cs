namespace WindowUI.SysMaster
{
    partial class SupplierMasterSearch
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
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbliCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtcChinaName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtcCNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lvwMstr = new System.Windows.Forms.ListView();
            this.slm_iRecordID = new System.Windows.Forms.ColumnHeader();
            this.slm_cClientNum = new System.Windows.Forms.ColumnHeader();
            this.slm_cChinaName = new System.Windows.Forms.ColumnHeader();
            this.slm_cEnglishName = new System.Windows.Forms.ColumnHeader();
            this.slm_cLinkman = new System.Windows.Forms.ColumnHeader();
            this.slm_cAddress = new System.Windows.Forms.ColumnHeader();
            this.slm_cPhone = new System.Windows.Forms.ColumnHeader();
            this.slm_cFax = new System.Windows.Forms.ColumnHeader();
            this.slm_cWebSite = new System.Windows.Forms.ColumnHeader();
            this.slm_cRemark = new System.Windows.Forms.ColumnHeader();
            this.slm_cAdd = new System.Windows.Forms.ColumnHeader();
            this.slm_dAddDate = new System.Windows.Forms.ColumnHeader();
            this.slm_cLast = new System.Windows.Forms.ColumnHeader();
            this.slm_dLastDate = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnExit.Location = new System.Drawing.Point(583, 424);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 22);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "&X 退出";
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
            this.groupBox1.Size = new System.Drawing.Size(676, 69);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // lbliCount
            // 
            this.lbliCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbliCount.Location = new System.Drawing.Point(547, 45);
            this.lbliCount.Name = "lbliCount";
            this.lbliCount.Size = new System.Drawing.Size(62, 20);
            this.lbliCount.TabIndex = 49;
            this.lbliCount.Text = "0";
            this.lbliCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Location = new System.Drawing.Point(614, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 48;
            this.label7.Text = "条记录";
            // 
            // btnFind
            // 
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnFind.ImageIndex = 0;
            this.btnFind.Location = new System.Drawing.Point(569, 16);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(86, 22);
            this.btnFind.TabIndex = 47;
            this.btnFind.Text = "&F 搜寻";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtcChinaName
            // 
            this.txtcChinaName.Location = new System.Drawing.Point(274, 16);
            this.txtcChinaName.Name = "txtcChinaName";
            this.txtcChinaName.Size = new System.Drawing.Size(287, 21);
            this.txtcChinaName.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(204, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "供应商名称:";
            // 
            // txtcCNum
            // 
            this.txtcCNum.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtcCNum.Location = new System.Drawing.Point(75, 16);
            this.txtcCNum.Name = "txtcCNum";
            this.txtcCNum.Size = new System.Drawing.Size(120, 21);
            this.txtcCNum.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "供应商编号:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSelect);
            this.groupBox2.Controls.Add(this.lvwMstr);
            this.groupBox2.Location = new System.Drawing.Point(2, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 343);
            this.groupBox2.TabIndex = 21;
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
            this.btnSelect.TabIndex = 17;
            this.btnSelect.Text = "&S 选择";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lvwMstr
            // 
            this.lvwMstr.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.slm_iRecordID,
            this.slm_cClientNum,
            this.slm_cChinaName,
            this.slm_cEnglishName,
            this.slm_cLinkman,
            this.slm_cAddress,
            this.slm_cPhone,
            this.slm_cFax,
            this.slm_cWebSite,
            this.slm_cRemark,
            this.slm_cAdd,
            this.slm_dAddDate,
            this.slm_cLast,
            this.slm_dLastDate});
            this.lvwMstr.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvwMstr.FullRowSelect = true;
            this.lvwMstr.GridLines = true;
            this.lvwMstr.HideSelection = false;
            this.lvwMstr.Location = new System.Drawing.Point(3, 17);
            this.lvwMstr.MultiSelect = false;
            this.lvwMstr.Name = "lvwMstr";
            this.lvwMstr.Size = new System.Drawing.Size(666, 291);
            this.lvwMstr.TabIndex = 16;
            this.lvwMstr.UseCompatibleStateImageBehavior = false;
            this.lvwMstr.View = System.Windows.Forms.View.Details;
            this.lvwMstr.DoubleClick += new System.EventHandler(this.lvwMstr_DoubleClick);
            // 
            // slm_iRecordID
            // 
            this.slm_iRecordID.Tag = "slm_iRecordID";
            this.slm_iRecordID.Text = "记录ID";
            this.slm_iRecordID.Width = 0;
            // 
            // slm_cClientNum
            // 
            this.slm_cClientNum.Tag = "slm_cClientNum";
            this.slm_cClientNum.Text = "供应商编号";
            this.slm_cClientNum.Width = 100;
            // 
            // slm_cChinaName
            // 
            this.slm_cChinaName.Tag = "slm_cChinaName";
            this.slm_cChinaName.Text = "中文名称";
            this.slm_cChinaName.Width = 200;
            // 
            // slm_cEnglishName
            // 
            this.slm_cEnglishName.Tag = "slm_cEnglishName";
            this.slm_cEnglishName.Text = "英文名称";
            this.slm_cEnglishName.Width = 87;
            // 
            // slm_cLinkman
            // 
            this.slm_cLinkman.Tag = "slm_cLinkman";
            this.slm_cLinkman.Text = "联系人";
            // 
            // slm_cAddress
            // 
            this.slm_cAddress.Tag = "slm_cAddress";
            this.slm_cAddress.Text = "地址";
            this.slm_cAddress.Width = 100;
            // 
            // slm_cPhone
            // 
            this.slm_cPhone.Tag = "slm_cPhone";
            this.slm_cPhone.Text = "电话";
            // 
            // slm_cFax
            // 
            this.slm_cFax.Tag = "slm_cFax";
            this.slm_cFax.Text = "传真";
            // 
            // slm_cWebSite
            // 
            this.slm_cWebSite.Tag = "slm_cWebSite";
            this.slm_cWebSite.Text = "网站";
            // 
            // slm_cRemark
            // 
            this.slm_cRemark.Tag = "slm_cRemark";
            this.slm_cRemark.Text = "备注";
            this.slm_cRemark.Width = 100;
            // 
            // slm_cAdd
            // 
            this.slm_cAdd.Tag = "slm_cAdd";
            this.slm_cAdd.Text = "新增者";
            this.slm_cAdd.Width = 80;
            // 
            // slm_dAddDate
            // 
            this.slm_dAddDate.Tag = "slm_dAddDate";
            this.slm_dAddDate.Text = "新增日期";
            this.slm_dAddDate.Width = 80;
            // 
            // slm_cLast
            // 
            this.slm_cLast.Tag = "slm_cLast";
            this.slm_cLast.Text = "修改者";
            this.slm_cLast.Width = 80;
            // 
            // slm_dLastDate
            // 
            this.slm_dLastDate.Tag = "slm_dLastDate";
            this.slm_dLastDate.Text = "修改时间";
            this.slm_dLastDate.Width = 80;
            // 
            // SupplierMasterSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 451);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SupplierMasterSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "供应商搜寻";
            this.Text = "供应商搜寻";
            this.Load += new System.EventHandler(this.SupplierMasterSearch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbliCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtcChinaName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtcCNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ListView lvwMstr;
        private System.Windows.Forms.ColumnHeader slm_iRecordID;
        private System.Windows.Forms.ColumnHeader slm_cClientNum;
        private System.Windows.Forms.ColumnHeader slm_cChinaName;
        private System.Windows.Forms.ColumnHeader slm_cEnglishName;
        private System.Windows.Forms.ColumnHeader slm_cLinkman;
        private System.Windows.Forms.ColumnHeader slm_cAddress;
        private System.Windows.Forms.ColumnHeader slm_cPhone;
        private System.Windows.Forms.ColumnHeader slm_cFax;
        private System.Windows.Forms.ColumnHeader slm_cWebSite;
        private System.Windows.Forms.ColumnHeader slm_cRemark;
        private System.Windows.Forms.ColumnHeader slm_cAdd;
        private System.Windows.Forms.ColumnHeader slm_dAddDate;
        private System.Windows.Forms.ColumnHeader slm_cLast;
        private System.Windows.Forms.ColumnHeader slm_dLastDate;
    }
}