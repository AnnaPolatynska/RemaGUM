﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemaGUM
{
    public partial class Os_zarzadzajacaForm : Form
    {
        public Os_zarzadzajacaForm()
        {
            InitializeComponent();
        }

        private void toolStripButtonSpisForm_Click(object sender, EventArgs e)
        {
            SpisForm frame = new SpisForm();
            frame.Show();
        }

       
    }
}
