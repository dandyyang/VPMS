namespace WindowUI.Management.Master
{
    partial class CardUserMasterInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardUserMasterInput));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSchool = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSpecialty = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblGraduationPeriod = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lblClass = new System.Windows.Forms.ToolStripLabel();
            this.btnExpEorData = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.gvwCardUser = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvwCardUser)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.lblSchool,
            this.toolStripSeparator1,
            this.lblSpecialty,
            this.toolStripSeparator2,
            this.lblGraduationPeriod,
            this.toolStripSeparator4,
            this.lblClass,
            this.btnExpEorData,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(974, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // lblSchool
            // 
            this.lblSchool.Name = "lblSchool";
            this.lblSchool.Size = new System.Drawing.Size(44, 22);
            this.lblSchool.Text = "院系部";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lblSpecialty
            // 
            this.lblSpecialty.Name = "lblSpecialty";
            this.lblSpecialty.Size = new System.Drawing.Size(32, 22);
            this.lblSpecialty.Text = "专业";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // lblGraduationPeriod
            // 
            this.lblGraduationPeriod.Name = "lblGraduationPeriod";
            this.lblGraduationPeriod.Size = new System.Drawing.Size(32, 22);
            this.lblGraduationPeriod.Text = "届别";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // lblClass
            // 
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(32, 22);
            this.lblClass.Text = "班级";
            // 
            // btnExpEorData
            // 
            this.btnExpEorData.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExpEorData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExpEorData.Image = ((System.Drawing.Image)(resources.GetObject("btnExpEorData.Image")));
            this.btnExpEorData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExpEorData.Name = "btnExpEorData";
            this.btnExpEorData.Size = new System.Drawing.Size(84, 22);
            this.btnExpEorData.Text = "导出错误数据";
            this.btnExpEorData.Click += new System.EventHandler(this.btnExpEorData_Click);
            // 
            // btnSave
            // 
            this.btnSave.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 22);
            this.btnSave.Text = "确认";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvwCardUser
            // 
            this.gvwCardUser.AllowUserToAddRows = false;
            this.gvwCardUser.AllowUserToDeleteRows = false;
            this.gvwCardUser.BackgroundColor = System.Drawing.Color.Silver;
            this.gvwCardUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvwCardUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvwCardUser.Location = new System.Drawing.Point(0, 25);
            this.gvwCardUser.MultiSelect = false;
            this.gvwCardUser.Name = "gvwCardUser";
            this.gvwCardUser.ReadOnly = true;
            this.gvwCardUser.RowTemplate.Height = 23;
            this.gvwCardUser.Size = new System.Drawing.Size(974, 445);
            this.gvwCardUser.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(974, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // CardUserMasterInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(974, 470);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gvwCardUser);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CardUserMasterInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "";
            this.Text = "导入卡用户信息";
            this.Shown += new System.EventHandler(this.CardUserMasterInput_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvwCardUser)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.DataGridView gvwCardUser;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripLabel lblSchool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblSpecialty;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblClass;
        private System.Windows.Forms.ToolStripLabel lblGraduationPeriod;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnExpEorData;

    }
}
