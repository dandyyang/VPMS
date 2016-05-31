namespace WindowControls
{
    partial class UserToolBar
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserToolBar));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnIssuance = new System.Windows.Forms.ToolStripButton();
            this.btnModify = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.btnCardReturn = new System.Windows.Forms.ToolStripButton();
            this.btnCardMissing = new System.Windows.Forms.ToolStripButton();
            this.btnCardRecovery = new System.Windows.Forms.ToolStripButton();
            this.btnCardScrap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFirst = new System.Windows.Forms.ToolStripButton();
            this.btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.lblSpace = new System.Windows.Forms.ToolStripLabel();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.btnLast = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnDataInput = new System.Windows.Forms.ToolStripSplitButton();
            this.btnImportCardUserData = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImportCardUserPhoto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnImportData = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDataExport = new System.Windows.Forms.ToolStripSplitButton();
            this.BtnExportTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExpCusData = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExportCardUserPhoto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExportData = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExportTemp = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGroupPerson = new System.Windows.Forms.ToolStripButton();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnIssuance,
            this.btnModify,
            this.btnDelete,
            this.btnSave,
            this.btnCancel,
            this.btnCardReturn,
            this.btnCardMissing,
            this.btnCardRecovery,
            this.btnCardScrap,
            this.toolStripSeparator11,
            this.toolStripSeparator12,
            this.btnFirst,
            this.btnPrevious,
            this.lblSpace,
            this.btnNext,
            this.btnLast,
            this.toolStripSeparator21,
            this.toolStripSeparator22,
            this.btnSearch,
            this.btnDataInput,
            this.btnDataExport,
            this.btnGroupPerson});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(887, 26);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.AutoSize = false;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(60, 22);
            this.btnNew.Text = "新增(&A)";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnIssuance
            // 
            this.btnIssuance.Image = ((System.Drawing.Image)(resources.GetObject("btnIssuance.Image")));
            this.btnIssuance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnIssuance.Name = "btnIssuance";
            this.btnIssuance.Size = new System.Drawing.Size(64, 23);
            this.btnIssuance.Text = "发卡(&I)";
            this.btnIssuance.Visible = false;
            this.btnIssuance.Click += new System.EventHandler(this.btnIssuance_Click);
            // 
            // btnModify
            // 
            this.btnModify.AutoSize = false;
            this.btnModify.Image = ((System.Drawing.Image)(resources.GetObject("btnModify.Image")));
            this.btnModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(70, 22);
            this.btnModify.Text = "修改(&M)";
            this.btnModify.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = false;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 22);
            this.btnDelete.Text = "刪除(&D)";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoSize = false;
            this.btnSave.Enabled = false;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 22);
            this.btnSave.Text = "存档(&S)";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = false;
            this.btnCancel.Enabled = false;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 22);
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCardReturn
            // 
            this.btnCardReturn.Image = ((System.Drawing.Image)(resources.GetObject("btnCardReturn.Image")));
            this.btnCardReturn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardReturn.Name = "btnCardReturn";
            this.btnCardReturn.Size = new System.Drawing.Size(68, 23);
            this.btnCardReturn.Text = "退卡(&R)";
            this.btnCardReturn.Visible = false;
            this.btnCardReturn.Click += new System.EventHandler(this.btnCardReturn_Click);
            // 
            // btnCardMissing
            // 
            this.btnCardMissing.Image = ((System.Drawing.Image)(resources.GetObject("btnCardMissing.Image")));
            this.btnCardMissing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardMissing.Name = "btnCardMissing";
            this.btnCardMissing.Size = new System.Drawing.Size(66, 23);
            this.btnCardMissing.Text = "挂失(&L)";
            this.btnCardMissing.Visible = false;
            this.btnCardMissing.Click += new System.EventHandler(this.btnCardMissing_Click);
            // 
            // btnCardRecovery
            // 
            this.btnCardRecovery.Image = ((System.Drawing.Image)(resources.GetObject("btnCardRecovery.Image")));
            this.btnCardRecovery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardRecovery.Name = "btnCardRecovery";
            this.btnCardRecovery.Size = new System.Drawing.Size(52, 23);
            this.btnCardRecovery.Text = "解挂";
            this.btnCardRecovery.Visible = false;
            this.btnCardRecovery.Click += new System.EventHandler(this.btnCardRecovery_Click);
            // 
            // btnCardScrap
            // 
            this.btnCardScrap.Image = ((System.Drawing.Image)(resources.GetObject("btnCardScrap.Image")));
            this.btnCardScrap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardScrap.Name = "btnCardScrap";
            this.btnCardScrap.Size = new System.Drawing.Size(67, 23);
            this.btnCardScrap.Text = "报废(&S)";
            this.btnCardScrap.Visible = false;
            this.btnCardScrap.Click += new System.EventHandler(this.btnCardScrap_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 26);
            // 
            // btnFirst
            // 
            this.btnFirst.AutoSize = false;
            this.btnFirst.Image = ((System.Drawing.Image)(resources.GetObject("btnFirst.Image")));
            this.btnFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(70, 22);
            this.btnFirst.Text = "首条(&F)";
            this.btnFirst.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.AutoSize = false;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(70, 22);
            this.btnPrevious.Text = "上条(&P)";
            this.btnPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // lblSpace
            // 
            this.lblSpace.AutoSize = false;
            this.lblSpace.Name = "lblSpace";
            this.lblSpace.Size = new System.Drawing.Size(20, 22);
            // 
            // btnNext
            // 
            this.btnNext.AutoSize = false;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 22);
            this.btnNext.Text = "下条(&N)";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.AutoSize = false;
            this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
            this.btnLast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(70, 22);
            this.btnLast.Text = "尾条(&L)";
            this.btnLast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(6, 26);
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = false;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 22);
            this.btnSearch.Text = "搜寻(&S)";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDataInput
            // 
            this.btnDataInput.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImportCardUserData,
            this.btnImportCardUserPhoto,
            this.btnImportData});
            this.btnDataInput.Image = ((System.Drawing.Image)(resources.GetObject("btnDataInput.Image")));
            this.btnDataInput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDataInput.Name = "btnDataInput";
            this.btnDataInput.Size = new System.Drawing.Size(79, 21);
            this.btnDataInput.Text = "导入(&T)";
            this.btnDataInput.Visible = false;
            // 
            // btnImportCardUserData
            // 
            this.btnImportCardUserData.Name = "btnImportCardUserData";
            this.btnImportCardUserData.Size = new System.Drawing.Size(152, 22);
            this.btnImportCardUserData.Text = "卡用户数据";
            this.btnImportCardUserData.Click += new System.EventHandler(this.btnImportCardUserData_Click);
            // 
            // btnImportCardUserPhoto
            // 
            this.btnImportCardUserPhoto.Name = "btnImportCardUserPhoto";
            this.btnImportCardUserPhoto.Size = new System.Drawing.Size(152, 22);
            this.btnImportCardUserPhoto.Text = "卡用户相片";
            this.btnImportCardUserPhoto.Click += new System.EventHandler(this.btnImportCardUserPhoto_Click);
            // 
            // btnImportData
            // 
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.Size = new System.Drawing.Size(152, 22);
            this.btnImportData.Text = "导入数据";
            this.btnImportData.Click += new System.EventHandler(this.btnImportData_Click);
            // 
            // btnDataExport
            // 
            this.btnDataExport.BackColor = System.Drawing.Color.Transparent;
            this.btnDataExport.DropDownButtonWidth = 12;
            this.btnDataExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnExportTemplate,
            this.btnExpCusData,
            this.btnExportCardUserPhoto,
            this.btnExportData,
            this.btnExportTemp});
            this.btnDataExport.Image = ((System.Drawing.Image)(resources.GetObject("btnDataExport.Image")));
            this.btnDataExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDataExport.Name = "btnDataExport";
            this.btnDataExport.Size = new System.Drawing.Size(65, 21);
            this.btnDataExport.Text = "导出";
            this.btnDataExport.Visible = false;
            // 
            // BtnExportTemplate
            // 
            this.BtnExportTemplate.Name = "BtnExportTemplate";
            this.BtnExportTemplate.Size = new System.Drawing.Size(160, 22);
            this.BtnExportTemplate.Text = "卡用户数据模板";
            this.BtnExportTemplate.Visible = false;
            this.BtnExportTemplate.Click += new System.EventHandler(this.btnExportTemplate_Click);
            // 
            // btnExpCusData
            // 
            this.btnExpCusData.Name = "btnExpCusData";
            this.btnExpCusData.Size = new System.Drawing.Size(160, 22);
            this.btnExpCusData.Text = "卡用户数据";
            this.btnExpCusData.Visible = false;
            this.btnExpCusData.Click += new System.EventHandler(this.btnExpCusData_Click);
            // 
            // btnExportCardUserPhoto
            // 
            this.btnExportCardUserPhoto.Name = "btnExportCardUserPhoto";
            this.btnExportCardUserPhoto.Size = new System.Drawing.Size(160, 22);
            this.btnExportCardUserPhoto.Text = "卡用户相片";
            this.btnExportCardUserPhoto.Click += new System.EventHandler(this.btnExportCardUserPhoto_Click);
            // 
            // btnExportData
            // 
            this.btnExportData.Name = "btnExportData";
            this.btnExportData.Size = new System.Drawing.Size(160, 22);
            this.btnExportData.Text = "导出数据";
            this.btnExportData.Click += new System.EventHandler(this.btnExportData_Click);
            // 
            // btnExportTemp
            // 
            this.btnExportTemp.Name = "btnExportTemp";
            this.btnExportTemp.Size = new System.Drawing.Size(160, 22);
            this.btnExportTemp.Text = "导出模版";
            this.btnExportTemp.Click += new System.EventHandler(this.btnExportTemp_Click);
            // 
            // btnGroupPerson
            // 
            this.btnGroupPerson.Image = ((System.Drawing.Image)(resources.GetObject("btnGroupPerson.Image")));
            this.btnGroupPerson.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGroupPerson.Name = "btnGroupPerson";
            this.btnGroupPerson.Size = new System.Drawing.Size(100, 21);
            this.btnGroupPerson.Text = "规则组别人员";
            this.btnGroupPerson.Visible = false;
            this.btnGroupPerson.Click += new System.EventHandler(this.btnGroupPerson_Click);
            // 
            // UserToolBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Name = "UserToolBar";
            this.Size = new System.Drawing.Size(887, 26);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnModify;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripButton btnFirst;
        private System.Windows.Forms.ToolStripButton btnPrevious;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripButton btnLast;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.ToolStripLabel lblSpace;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnCancel;
        private System.Windows.Forms.ToolStripButton btnCardReturn;
        private System.Windows.Forms.ToolStripButton btnCardMissing;
        private System.Windows.Forms.ToolStripButton btnCardScrap;
        private System.Windows.Forms.ToolStripButton btnCardRecovery;
        private System.Windows.Forms.ToolStripButton btnIssuance;
        private System.Windows.Forms.ToolStripSplitButton btnDataExport;
        private System.Windows.Forms.ToolStripMenuItem BtnExportTemplate;
        private System.Windows.Forms.ToolStripMenuItem btnExpCusData;
        private System.Windows.Forms.ToolStripSplitButton btnDataInput;
        private System.Windows.Forms.ToolStripMenuItem btnImportCardUserPhoto;
        private System.Windows.Forms.ToolStripMenuItem btnExportCardUserPhoto;
        private System.Windows.Forms.ToolStripMenuItem btnImportCardUserData;
        private System.Windows.Forms.ToolStripButton btnGroupPerson;
        private System.Windows.Forms.ToolStripMenuItem btnImportData;
        private System.Windows.Forms.ToolStripMenuItem btnExportData;
        private System.Windows.Forms.ToolStripMenuItem btnExportTemp;
    }
}
