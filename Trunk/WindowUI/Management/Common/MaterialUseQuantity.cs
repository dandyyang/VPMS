using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowUI.Management.Common
{
    public partial class MaterialUseQuantity : Form
    {
        public MaterialUseQuantity()
        {
            InitializeComponent();
        }

        private void btnMaterialSearch_Click(object sender, EventArgs e)
        {
            MaterialSearch frm = new MaterialSearch();
            frm.ShowDialog();
        }
    }
}
