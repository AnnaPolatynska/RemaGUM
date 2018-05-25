using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Threading;

namespace RemaGUM
{
    public partial class Operator_maszynyForm : Form
    {
        private string _connString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Projects\\RemaGUM\\RemaGUM.mdb"; //połaczenie z bazą danych

        private string _helpFile = Application.StartupPath + "\\RemaGUM.chm"; //plik pomocy RemaGUM

        private nsAccess2DB.Operator_maszynyBUS _Operator_maszynyBUS;
        private nsAccess2DB.Operator_maszyny_MaszynyBUS _Operator_maszyny_MaszynyBUS;
        private nsAccess2DB.MaszynyBUS _MaszynyBUS;
        
        private ToolTip _tt; //podpowiedzi dla niektórych kontolek

        /// <summary>
        /// Konstruktor formularza Operator maszyny.
        /// </summary>
        /// <param name="connStr">Połaczenie z bazą talela Operator_maszyny.</param>
        public Operator_maszynyForm()
        {
            InitializeComponent();
            nsRest.Rest rest = new nsRest.Rest();
            _connString += rest.dbConnection(_connString);

            //dane forlumarza
            listBoxOperator_maszyny.TabIndex = 0;
            textBoxOperator_maszyny.TabIndex = 1;
            textBoxNazwa_Dzial.TabIndex = 2;
            textBoxUprawnienie.TabIndex = 3;
            dateTimePickerData_konca_upr.TabIndex = 4;
            listBox_maszyny.TabIndex = 5;
            //przyciski zapisz/edytuj itp
            buttonNowa.TabIndex = 6;
            buttonZapisz.TabIndex = 7;
            buttonAnuluj.TabIndex = 8;
            buttonUsun.TabIndex = 9;
            //wyszukiwanie
            textBoxWyszukiwanie.TabIndex = 10;
            buttonSzukaj.TabIndex = 11;
            //sortowanie po radio buttonach
            radioButtonData_konca_upr.TabIndex = 12;

            _Operator_maszynyBUS = new nsAccess2DB.Operator_maszynyBUS(_connString);
            _Operator_maszyny_MaszynyBUS = new nsAccess2DB.Operator_maszyny_MaszynyBUS(_connString);
            _MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);

            _Operator_maszynyBUS.select();
            _MaszynyBUS.select();


            WypelnijOperatorowDanymi();

            if (listBoxOperator_maszyny.Items.Count > 0)
            {
                listBoxOperator_maszyny.SelectedIndex = 0;
            }

            // podpowiedzi dla niektórych formantów
            _tt = new ToolTip();
            _tt.SetToolTip(listBoxOperator_maszyny, "Lista wszystkich operatorów maszyn, przyrządów i urządzeń itp.");
            _tt.SetToolTip(textBoxOperator_maszyny, "Imię i nazwisko operatora maszyny.");
            _tt.SetToolTip(textBoxNazwa_Dzial, "Nazwa działu, do którego należy operator maszyny.");
            _tt.SetToolTip(textBoxUprawnienie, "Rodzaj uprawnienia, jakie posiada operator.");
            _tt.SetToolTip(dateTimePickerData_konca_upr, "Data końca uprawnień operatora maszyny.");
            _tt.SetToolTip(listBox_maszyny, "Lista obsługiwanych przez operatora maszyn.");
            _tt.SetToolTip(buttonNowa, "Nowa pozycja w bazie.");
            _tt.SetToolTip(buttonZapisz, "Zapis nowej maszyny, przyrządu lub urządzenia lub edycja wybranej pozycji.");
            _tt.SetToolTip(buttonAnuluj, "Anulowanie zmiany.");
            _tt.SetToolTip(buttonUsun, "Usuwa pozycję z bazy.");
            _tt.SetToolTip(textBoxWyszukiwanie, "Wpisz czego szukasz.");
            _tt.SetToolTip(buttonSzukaj, "Szukanie w bazie.");
            _tt.SetToolTip(radioButtonData_konca_upr, "Sortowanie operatorow po dacie końca uprawnień.");
        }//Operator_maszynyForm()

        
        //wyświetla listę operatorów maszyn po imieniu i nazwisku
        private void WypelnijOperatorowDanymi()
        {
            nsAccess2DB.Operator_maszyny_MaszynyVO VO;
            listBoxOperator_maszyny.Items.Clear();
            //_Operator_maszynyBUS.select();

            _Operator_maszyny_MaszynyBUS.idx = 0;
            while (!_Operator_maszyny_MaszynyBUS.eof)
            {
                VO = _Operator_maszyny_MaszynyBUS.VO;
                listBoxOperator_maszyny.Items.Add(VO.Operator_maszyny);
                _Operator_maszyny_MaszynyBUS.skip();
            }

        }//WypelnijOperatorowDanymi()
        
        // TO DO -> wypełnić listBox_maszyny, listą obsługiwanych przez operatora maszyn. (pozyskać dane)

        /// <summary>
        /// zmiana indeksu w list box operator maszyny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxOperator_maszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nsAccess2DB.Operator_maszynyBUS operator_maszynyBUS = new nsAccess2DB.Operator_maszynyBUS(_connString);
            _Operator_maszynyBUS.idx = listBoxOperator_maszyny.SelectedIndex;
            listBoxOperator_maszyny.Text = _Operator_maszynyBUS.VO.ID_op_maszyny.ToString();
            toolStripStatusLabel_ID_Operatora.Text = _Operator_maszynyBUS.VO.ID_op_maszyny.ToString();

            textBoxOperator_maszyny.Text = _Operator_maszynyBUS.VO.Operator_maszyny;
           
            textBoxUprawnienie.Text = _Operator_maszynyBUS.VO.Uprawnienie;

            dateTimePickerData_konca_upr.Value = new DateTime(_Operator_maszynyBUS.VO.Rok, _Operator_maszynyBUS.VO.Mc, _Operator_maszynyBUS.VO.Dzien);
         
        }//listBoxOperator_maszyny_SelectedIndexChanged

            /// /////////////////////////////////////////////// /// ///            toolStripButton
        private void toolStripButtonOs_zarzadzajaca_Click(object sender, EventArgs e)
        {
            Os_zarzadzajacaForm frame = new Os_zarzadzajacaForm();
            frame.Show();
        }//toolStripButtonOs_zarzadzajaca_Click

        private void toolStripButtonSpisForm_Click(object sender, EventArgs e)
        {
            SpisForm frame = new SpisForm();
            frame.Show();
        }//toolStripButtonSpisForm_Click

        //////////////////////////////////////////////////////////////         Rabio button
        private void radioButtonData_konca_upr_CheckedChanged(object sender, EventArgs e)
        {
            listBoxOperator_maszyny.Items.Clear();
            nsAccess2DB.Operator_maszynyBUS operator_maszynyBUS = new nsAccess2DB.Operator_maszynyBUS(_connString);
            _Operator_maszynyBUS.selectQuery("SELECT * FROM Operator_maszyny ORDER BY Operator_maszyny ASC;");
            while (!_Operator_maszynyBUS.eof)
            {
                listBoxOperator_maszyny.Items.Add(_Operator_maszynyBUS.VO.Operator_maszyny + " -> " + _Operator_maszynyBUS.VO.Rok + "-" + _Operator_maszynyBUS.VO.Mc + "-" + _Operator_maszynyBUS.VO.Dzien);
                _Operator_maszynyBUS.skip();
            }

            if (listBoxOperator_maszyny.Items.Count > 0)
            {
                listBoxOperator_maszyny.SelectedIndex = 0;
            }
        }//radioButtonData_konca_upr_CheckedChanged



        // ////////////////////////////////////////////////////////            Button
        /// <summary>
        /// wyszukuje operatora maszyny po wpisaniu dowolnego ciągu znaków imienia, nazwiska lub działu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSzukaj_Click(object sender, EventArgs e)
        {
            listBoxOperator_maszyny.Items.Clear();

            nsAccess2DB.Operator_maszynyBUS operator_maszynyBUS = new nsAccess2DB.Operator_maszynyBUS(_connString);
            _Operator_maszynyBUS.selectQuery("SELECT * FROM Operator_maszyny WHERE Operator_maszyny LIKE '" + textBoxWyszukiwanie.Text + "%' OR Nazwa_dzial LIKE '%" + textBoxWyszukiwanie.Text + "%';");
            while (!_Operator_maszynyBUS.eof)
            {
                listBoxOperator_maszyny.Items.Add(_Operator_maszynyBUS.VO.Operator_maszyny + " -> " + _Operator_maszynyBUS.VO.Nazwa_dzial);
                _Operator_maszynyBUS.skip();
            }

            if (listBoxOperator_maszyny.Items.Count > 0)
            {
                listBoxOperator_maszyny.SelectedIndex = 0;
            }
        }//buttonSzukaj_Click

        private void buttonNowa_Click(object sender, EventArgs e)
        {
            textBoxOperator_maszyny.Text = string.Empty;
            textBoxNazwa_Dzial.Text = string.Empty;
            textBoxUprawnienie.Text = string.Empty;
            dateTimePickerData_konca_upr.Text = string.Empty;

            //WypelnijOperatorowDanymi();
        }

        private void buttonZapisz_Click(object sender, EventArgs e)
        {
            //todo zapisz
        }

        private void buttonAnuluj_Click(object sender, EventArgs e)
        {

        }

        private void buttonUsun_Click(object sender, EventArgs e)
        {
          // todo  _Operator_maszynyBUS.delete((int)listBoxOperator_maszyny.Tag);
          
            textBoxOperator_maszyny.Text = string.Empty;
            textBoxNazwa_Dzial.Text = string.Empty;
            textBoxUprawnienie.Text = string.Empty;
            dateTimePickerData_konca_upr.Text = string.Empty;

           // WypelnijOperatorowDanymi();
        }

        // button odświez
        private void toolStripButtonOdswiez_Click(object sender, EventArgs e)
        {
            listBoxOperator_maszyny.Items.Clear();
           // WypelnijOperatorowDanymi();
        }

        // button pomocy
        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            if (File.Exists(_helpFile))
            {
                Help.ShowHelp(this, _helpFile);
            }
            else
            {
                MessageBox.Show("Plik pomocy RemaGUM.chm nie istnieje w katalogu help.", "RemaGUM",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }// toolStripButtonHelp_Click
    }
}
