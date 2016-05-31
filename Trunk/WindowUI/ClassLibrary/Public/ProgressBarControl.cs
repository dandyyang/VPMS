using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WindowUI.ClassLibrary.Public
{
    class ProgressBarControl
    {
        private Panel _progressPanel = null; 
        private ProgressBar _progressBar = null; 
        private Label _progressBarCaption = null; 
        private int _progressBarMaxvalue; 
        private Button _cancelButton = null; 
        private bool _stop = false;

        public ProgressBarControl(int progressBarMaxvalue, string formCaption, bool isCancel, Control container) 
        { 
            this._progressPanel = new Panel(); 
            this._progressPanel.BackColor = Color.DarkSeaGreen; 
            
            this._progressPanel.Width = 380; 
            this._progressPanel.Height = 60; 
            this._progressPanel.BorderStyle = BorderStyle.FixedSingle; 

            this._progressBarCaption = new Label(); 
            this._progressBarCaption.Top=11; 
            this._progressBarCaption.Left = 15; 

            this._progressBarCaption.Parent = this._progressPanel; 

            this._progressBar = new ProgressBar(); 
            this._progressBar.Top = 9; 
            this._progressBar.Left = 100 + 15; 
            this._progressBar.Width = 250; 
            this._progressBar.Maximum = progressBarMaxvalue; 
            this._progressBarMaxvalue = progressBarMaxvalue; 
            this._progressBar.Parent = this._progressPanel; 

            if (isCancel) 
            {
                this._progressPanel.Height = 85; 

                this._cancelButton = new Button(); 
                this._cancelButton.Text = "&Cancel"; 
                this._cancelButton.Height = 25; 
                this._cancelButton.Width = 80; 
                this._cancelButton.Top = 50; 
                this._cancelButton.Left = 280; 
                this._cancelButton.BackColor = Color.FromKnownColor(KnownColor.Control); 

                this._cancelButton.Parent = this._progressPanel; 
                this._cancelButton.Click += new EventHandler(this.CancelButton_Click); 
            } 

            this._progressPanel.Parent = container; 

            this._progressPanel.Top = (container.Height / 2 - this._progressPanel.Height/2); 
            this._progressPanel.Left = (container.Width / 2 - this._progressPanel.Width/2); 
            this._progressPanel.BringToFront(); 
            this._progressPanel.Focus(); 
        } 

        private void CancelButton_Click(object Sender, EventArgs e) 
        { 
            if (MessageBox.Show("您確定要終止嗎？", "終止", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)

            { 
                this._stop = true; 
            } 
            else 
            { 
                this._stop = false; 
            } 
        } 

        public void Dispose() 
        { 
            if (this._progressPanel != null) 
            { 
                this._progressBar.Dispose(); 
                this._progressPanel.Dispose(); 
            } 
        } 

        public bool ProgressStep(int step) 
        { 
            if (this._stop) 
            { 
                this.Dispose(); 
                return true; 
            } 

            if (this._progressBar.Value > this._progressBar.Maximum) 
            { 
                this.Dispose(); 
                return true; 
            } 

            this._progressBar.Value += step; 
            this._progressBarCaption.Text = "目前已完成："+(this._progressBar.Value*100/this._progressBar.Maximum)+"%";
            Application.DoEvents(); 

            return false; 
        } 


    }
}
