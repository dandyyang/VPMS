namespace ReaderEquipment.ReaderImplement
{
    partial class ULineHFReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ULineHFReader));
            this.mscom4ULine = new AxMSCommLib.AxMSComm();
            ((System.ComponentModel.ISupportInitialize)(this.mscom4ULine)).BeginInit();
            this.SuspendLayout();
            // 
            // mscom4ULine
            // 
            this.mscom4ULine.Enabled = true;
            this.mscom4ULine.Location = new System.Drawing.Point(181, 97);
            this.mscom4ULine.Name = "mscom4ULine";
            this.mscom4ULine.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("mscom4ULine.OcxState")));
            this.mscom4ULine.Size = new System.Drawing.Size(38, 38);
            this.mscom4ULine.TabIndex = 2;
            // 
            // ULineUHFReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.mscom4ULine);
            this.Name = "ULineUHFReader";
            this.Text = "ULineUHFReader";
            ((System.ComponentModel.ISupportInitialize)(this.mscom4ULine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMSCommLib.AxMSComm mscom4ULine;
    }
}