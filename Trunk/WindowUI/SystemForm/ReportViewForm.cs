using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Model.General;
using Common;

namespace WindowUI.SystemForm
{
    public partial class ReportViewForm : Form
    {
        private DefineConstantValue.SystemMessage _systemMessageText = new DefineConstantValue.SystemMessage("");

        public ReportViewForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示报表
        /// </summary>
        /// <param name="RprtD">ReportDocument</param>
        public void ShowForm(ReportDocument RprtD)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                this.reportViewer.ReportSource = RprtD;
                this.ShowDialog();
            }
            catch (Exception Ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(Ex.Message.Trim(), this._systemMessageText.strMessageTitle + this._systemMessageText.strSystemError.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
