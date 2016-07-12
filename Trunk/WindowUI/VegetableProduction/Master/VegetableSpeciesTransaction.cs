using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowUI.Management.Common;

namespace WindowUI.VegetableProduction.Master
{
    public partial class VegetableSpeciesTransaction : Form
    {
        public VegetableSpeciesTransaction()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
        }

        private void btnMAdd_Click(object sender, EventArgs e)
        {
            MaterialUseQuantity frm = new MaterialUseQuantity();
            frm.ShowDialog();
        }
    }
}
