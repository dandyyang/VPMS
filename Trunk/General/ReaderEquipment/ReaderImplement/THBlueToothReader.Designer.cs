namespace ReaderEquipment.ReaderImplement
{
    partial class THBlueToothReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(THBlueToothReader));
            this.axMSComm1 = new AxMSCommLib.AxMSComm();
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).BeginInit();
            this.SuspendLayout();
            // 
            // axMSComm1
            // 
            this.axMSComm1.Enabled = true;
            this.axMSComm1.Location = new System.Drawing.Point(12, 12);
            this.axMSComm1.Name = "axMSComm1";
            this.axMSComm1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMSComm1.OcxState")));
            this.axMSComm1.Size = new System.Drawing.Size(38, 38);
            this.axMSComm1.TabIndex = 0;
            // 
            // THBlueToothReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.axMSComm1);
            this.Name = "THBlueToothReader";
            this.Text = "THBlueToothReader";
            ((System.ComponentModel.ISupportInitialize)(this.axMSComm1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSCommLib.AxMSComm axMSComm1;

    }
}