namespace ReaderEquipment.ReaderImplement
{
    partial class VerayoHFReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerayoHFReader));
            this.mscom4Reader = new AxMSCommLib.AxMSComm();
            ((System.ComponentModel.ISupportInitialize)(this.mscom4Reader)).BeginInit();
            this.SuspendLayout();
            // 
            // mscom4Reader
            // 
            this.mscom4Reader.Enabled = true;
            this.mscom4Reader.Location = new System.Drawing.Point(36, 143);
            this.mscom4Reader.Name = "mscom4Reader";
            this.mscom4Reader.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mscom4Reader.OcxState")));
            this.mscom4Reader.Size = new System.Drawing.Size(38, 38);
            this.mscom4Reader.TabIndex = 1;
            this.mscom4Reader.OnComm += new System.EventHandler(this.ReadDataForCOM);
            // 
            // VerayoUHFReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.mscom4Reader);
            this.Name = "VerayoUHFReader";
            this.Text = "frmVerayoUHFReader";
            ((System.ComponentModel.ISupportInitialize)(this.mscom4Reader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSCommLib.AxMSComm mscom4Reader;
    }
}