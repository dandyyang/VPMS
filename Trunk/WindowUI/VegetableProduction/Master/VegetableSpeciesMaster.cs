﻿using System;
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
    public partial class VegetableSpeciesMaster : BaseForm
    {
        public VegetableSpeciesMaster()
        {
            InitializeComponent();
        }

        private void btnTBAdd_Click(object sender, EventArgs e)
        {
            VegetableSpeciesTransaction frm = new VegetableSpeciesTransaction();
            frm.ShowDialog();
        }

        private void btnTAAdd_Click(object sender, EventArgs e)
        {
            VegetableSpeciesTransaction frm = new VegetableSpeciesTransaction();
            frm.ShowDialog();
        }

        private void btnMAdd_Click(object sender, EventArgs e)
        {
            MaterialUseQuantity frm = new MaterialUseQuantity();
            frm.ShowDialog();
        }

    }
}
