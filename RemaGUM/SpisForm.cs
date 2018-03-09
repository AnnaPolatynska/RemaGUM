using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace RemaGUM
{
    public partial class SpisForm : Form
    {
        private string _connStr = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Projects\\RemaGUM\\RemaGUM.mdb"; //połaczenie z bazą
        nsAccess2DB.MaszynyBUS _maszynyBUS;
        string _Program = "RemaGUM";

        enum _status { edit, nowy, usun, zapisz, anuluj };      //status działania formularza
        private byte _statusForm;   //wartość statusu formularza

        private nsAccess2DB.MaszynyBUS _MaszynyBUS;

        /// <summary>
        /// Konstruktor formularza.
        /// </summary>
        /// <param name="connStr">Połaczenie z bazą.</param>
        public SpisForm()
        {
            InitializeComponent();

            nsRest.Rest rest = new nsRest.Rest();


            _connStr += rest.dbConnection(_connStr);

            listBoxMaszyny.TabIndex = 0;
            textBoxNazwa.TabIndex = 1;
            textBoxTyp.TabIndex = 2;
            textBoxNr_inwentarzowy.TabIndex = 3;
            textBoxNr_fabryczny.TabIndex = 4;
            textBoxRok_produkcji.TabIndex = 5;
            ButtonNowa.TabIndex = 6;
            buttonZapisz.TabIndex = 7;
            buttonAnuluj.TabIndex = 8;
            buttonUsun.TabIndex = 9;

            _MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.select();

            wypelnijMaszyny();
            if (listBoxMaszyny.Items.Count > 0) listBoxMaszyny.SelectedIndex = 0;


        }//public SpisForm()
        private void wypelnijMaszyny()
        {
            nsAccess2DB.MaszynyVO VO;
            _MaszynyBUS.select();

            listBoxMaszyny.Items.Clear();
            while (!_MaszynyBUS.eof)
            {
                VO = _MaszynyBUS.VO;
                listBoxMaszyny.Items.Add(VO.Nazwa);
                _MaszynyBUS.skip();
            }

            buttonZapisz.Enabled = listBoxMaszyny.SelectedIndex > -1;
        }//wypelnijMaszyny
        private void buttonZapisz_Click(object sender, EventArgs e)
        {
            nsAccess2DB.MaszynyVO VO = new nsAccess2DB.MaszynyVO();

            VO.ID_maszyny = (int)textBoxNazwa.Tag;////////   ???
            VO.Nazwa = textBoxNazwa.Text.Trim();
            VO.Typ = textBoxTyp.Text.Trim();
            VO.Nr_inwentarzowy = textBoxNr_inwentarzowy.Text.Trim();
            VO.Nr_fabryczny = textBoxNr_fabryczny.Text.Trim();
            VO.Rok_produkcji = textBoxRok_produkcji.Text.Trim();
            //VO.Korzysc = comboBoxKorzysc.Text;
            //VO.Priorytet = comboBoxPriorytet.Text;

            //_MaszynyBUS.write(VO);








        }// public partial class SpisForm : Form

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }
    }
    }//namespace RemaGUM
