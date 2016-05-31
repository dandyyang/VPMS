using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowControls
{
    public partial class DateTimePickerControl : UserControl
    {
        /// <summary>
        /// 翻回日期时间字串
        /// </summary>
        public string _selectValue;

        /// <summary>
        /// 控制返回日期时间字串格式 (yyyy-MM-dd 或 yyyy-MM-dd HH:mm)
        /// </summary>
        string _datetimeFormat;

        public DateTimePickerControl()
        {
            InitializeComponent();

            this._selectValue = string.Empty;

            this._datetimeFormat = "yyyy-MM-dd HH:mm";

            this.dtpSelectDateTime.CustomFormat = this._datetimeFormat;

            this.dtpSelectDateTime.Format = DateTimePickerFormat.Custom;

        }

        public string CustomFormat
        {
            set
            {
                this.dtpSelectDateTime.Format = DateTimePickerFormat.Custom;

                this.dtpSelectDateTime.CustomFormat = value;

                this._datetimeFormat = value;
            }
        }


        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            txtShowText.Visible = false;

            dtpSelectDateTime.Visible = true;

            btnClear.Visible = true;

            dtpSelectDateTime.Focus();

            this._selectValue = dtpSelectDateTime.Value.ToString(this._datetimeFormat);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            txtShowText.Text = dtpSelectDateTime.Value.ToString(this._datetimeFormat);

            this._selectValue = dtpSelectDateTime.Value.ToString(this._datetimeFormat.Replace("年", "-").Replace("月", "-").Replace("日", "").Replace("时", ":").Replace("分", ""));

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            txtShowText.Visible = true;

            dtpSelectDateTime.Visible = false;

            btnClear.Visible = false;

            txtShowText.Text = "";

            this._selectValue = "";
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            btnClear.Image = WindowControls.Properties.Resources.close_b;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            btnClear.Image = WindowControls.Properties.Resources.close;
        }
    }
}
