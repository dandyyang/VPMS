namespace ReaderEquipment.ReaderImplement
{
    partial class TH24GR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TH24GR));
            this.axMSComm1 = new AxMSCommLib.AxMSComm();
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMSComm1
            // 
            this.axMSComm1.Enabled = true;
            this.axMSComm1.Location = new System.Drawing.Point(0, 0);
            this.axMSComm1.Name = "axMSComm1";
            this.axMSComm1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMSComm1.OcxState")));
            this.axMSComm1.Size = new System.Drawing.Size(38, 38);
            this.axMSComm1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.axMSComm1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSCommLib.AxMSComm axMSComm1;

    }
}