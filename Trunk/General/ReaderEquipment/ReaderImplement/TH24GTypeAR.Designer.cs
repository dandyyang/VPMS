namespace ReaderEquipment.ReaderImplement
{
    partial class TH24GTypeAR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TH24GTypeAR));
            this.axMSCommE8 = new AxMSCommLib.AxMSComm();
            this.axMSComm05 = new AxMSCommLib.AxMSComm();
            this.axMSComm1 = new AxMSCommLib.AxMSComm();
            ((System.ComponentModel.ISupportInitialize)(this.axMSCommE8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm05)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMSCommE8
            // 
            this.axMSCommE8.Enabled = true;
            this.axMSCommE8.Location = new System.Drawing.Point(0, 0);
            this.axMSCommE8.Name = "axMSCommE8";
            this.axMSCommE8.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMSCommE8.OcxState")));
            this.axMSCommE8.Size = new System.Drawing.Size(38, 38);
            this.axMSCommE8.TabIndex = 0;
            // 
            // axMSComm05
            // 
            this.axMSComm05.Enabled = true;
            this.axMSComm05.Location = new System.Drawing.Point(74, 0);
            this.axMSComm05.Name = "axMSComm05";
            this.axMSComm05.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMSComm05.OcxState")));
            this.axMSComm05.Size = new System.Drawing.Size(38, 38);
            this.axMSComm05.TabIndex = 1;
            this.axMSComm05.OnComm += new System.EventHandler(this.ReadDataFromCOM);
            // 
            // axMSComm1
            // 
            this.axMSComm1.Enabled = true;
            this.axMSComm1.Location = new System.Drawing.Point(198, 96);
            this.axMSComm1.Name = "axMSComm1";
            this.axMSComm1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMSComm1.OcxState")));
            this.axMSComm1.Size = new System.Drawing.Size(38, 38);
            this.axMSComm1.TabIndex = 2;
            // 
            // TH24GTypeAR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.axMSComm1);
            this.Controls.Add(this.axMSComm05);
            this.Controls.Add(this.axMSCommE8);
            this.Name = "TH24GTypeAR";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axMSCommE8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm05)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSCommLib.AxMSComm axMSCommE8;
        private AxMSCommLib.AxMSComm axMSComm05;
        private AxMSCommLib.AxMSComm axMSComm1;

    }
}