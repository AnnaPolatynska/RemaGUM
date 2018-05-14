using System;
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
    public partial class Operator_maszynyForm : Form
    {
        public Operator_maszynyForm()
        {
            InitializeComponent();

        }//Operator_maszyny()

        private void toolStripButtonOs_zarzadzajaca_Click(object sender, EventArgs e)
        {
            Os_zarzadzajacaForm frame = new Os_zarzadzajacaForm();
            frame.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SpisForm frame = new SpisForm();
            frame.Show();
        }
    }
}
