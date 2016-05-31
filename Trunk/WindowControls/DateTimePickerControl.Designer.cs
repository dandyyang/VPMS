namespace WindowControls
{
    partial class DateTimePickerControl
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
            this.txtShowText = new System.Windows.Forms.TextBox();
            this.dtpSelectDateTime = new System.Windows.Forms.DateTimePicker();
            this.btnClear = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            this.SuspendLayout();
            // 
            // txtShowText
            // 
            this.txtShowText.Location = new System.Drawing.Point(3, 3);
            this.txtShowText.Name = "txtShowText";
            this.txtShowText.Size = new System.Drawing.Size(196, 21);
            this.txtShowText.TabIndex = 0;
            this.txtShowText.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            // 
            // dtpSelectDateTime
            // 
            this.dtpSelectDateTime.CustomFormat = "yyyy年MM月dd日 HH:mm";
            this.dtpSelectDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSelectDateTime.Location = new System.Drawing.Point(3, 3);
            this.dtpSelectDateTime.Name = "dtpSelectDateTime";
            this.dtpSelectDateTime.Size = new System.Drawing.Size(163, 21);
            this.dtpSelectDateTime.TabIndex = 1;
            this.dtpSelectDateTime.Visible = false;
            this.dtpSelectDateTime.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // btnClear
            // 
            this.btnClear.Image = global::WindowControls.Properties.Resources.close;
            this.btnClear.Location = new System.Drawing.Point(165, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(33, 21);
            this.btnClear.TabIndex = 2;
            this.btnClear.TabStop = false;
            this.btnClear.Visible = false;
            this.btnClear.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.btnClear.Click += new System.EventHandler(this.pictureBox1_Click);
            this.btnClear.MouseHover += new System.EventHandler(this.pictureBox1_MouseHover);
            // 
            // DateTimePickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dtpSelectDateTime);
            this.Controls.Add(this.txtShowText);
            this.Name = "DateTimePickerControl";
            this.Size = new System.Drawing.Size(203, 27);
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtShowText;
        private System.Windows.Forms.DateTimePicker dtpSelectDateTime;
        private System.Windows.Forms.PictureBox btnClear;
    }
}
