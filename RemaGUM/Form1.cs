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
    public partial class Frame : Form
    {
        public Frame(string s)
        {
            InitializeComponent();
            labelTekst.Text = s;

            labelTekst.Left = (this.Width - labelTekst.Width) / 2;
            labelTekst.Top = (this.Height - labelTekst.Height) / 2;
        }// Frame(string s)

    }// class Frame : Form
}// namespace RemaGUM
