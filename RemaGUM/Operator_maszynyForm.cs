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
        private nsAccess2DB.DzialBUS _DzialBUS;

        private ToolTip _tt; //podpowiedzi dla niektórych kontolek

        /// <summary>
        /// Konstruktor formularza Operator maszyny.
        /// </summary>
        /// <param name="connStr">Połaczenie z bazą talela Nazwa_op_maszyny.</param>
        public Operator_maszynyForm()
        {
            InitializeComponent();
            nsRest.Rest rest = new nsRest.Rest();
            _connString += rest.dbConnection(_connString);

            //dane forlumarza
            listBoxOperator_maszyny.TabIndex = 0;
            textBoxImie.TabIndex = 1;
            textBoxNazwisko.TabIndex = 2;
            comboBoxDzial_operator_maszyny.TabIndex = 3;
            textBoxUprawnienie.TabIndex = 4;
            dateTimePickerData_konca_upr.TabIndex = 5;
            listBox_maszyny.TabIndex = 6;
            //przyciski zapisz/edytuj itp
            buttonNowa.TabIndex = 7;
            buttonZapisz.TabIndex = 8;
            buttonAnuluj.TabIndex = 9;
            buttonUsun.TabIndex = 10;
            //wyszukiwanie
            textBoxWyszukiwanie.TabIndex = 11;
            buttonSzukaj.TabIndex = 12;
            //sortowanie po radio buttonach
            radioButtonData_konca_upr.TabIndex = 13;

            _Operator_maszynyBUS = new nsAccess2DB.Operator_maszynyBUS(_connString);
            _Operator_maszyny_MaszynyBUS = new nsAccess2DB.Operator_maszyny_MaszynyBUS(_connString);
            _MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            _DzialBUS = new nsAccess2DB.DzialBUS(_connString);

            _Operator_maszynyBUS.select();
            _Operator_maszyny_MaszynyBUS.select();
            _MaszynyBUS.select();

            WypelnijOperatorowDanymi();
            WypelnijOperatorowMaszynami();
            WypelnijDzial();

            if (listBoxOperator_maszyny.Items.Count > 0)
            {
                listBoxOperator_maszyny.SelectedIndex = 0;
            }

            // podpowiedzi dla niektórych formantów
            _tt = new ToolTip();
            _tt.SetToolTip(listBoxOperator_maszyny, "Lista wszystkich operatorów maszyn, przyrządów i urządzeń itp.");
            _tt.SetToolTip(textBoxImie, "Imię i nazwisko operatora maszyny.");
            _tt.SetToolTip(comboBoxDzial_operator_maszyny, "Nazwa działu, do którego należy operator maszyny.");
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

        // Wypełnia danymi słownikowymi pole dział
        private void WypelnijDzial()
        {
            nsAccess2DB.DzialVO VO;
            comboBoxDzial_operator_maszyny.Items.Clear();

            _DzialBUS.select();
            _DzialBUS.top();
            while (!_DzialBUS.eof)
            {
                VO = _DzialBUS.VO;
                comboBoxDzial_operator_maszyny.Items.Add(VO.Nazwa);
                _DzialBUS.skip();
            }
        }//WypelnijDzial

        private void comboBoxDzial_SelectedIndexChanged(object sender, EventArgs e)
        {
            _DzialBUS.idx = comboBoxDzial_operator_maszyny.SelectedIndex;
            comboBoxDzial_operator_maszyny.Tag = _DzialBUS.VO.Nazwa;
        }//comboBoxDzial_SelectedIndexChanged*/



 
        //wyświetla listę operatorów maszyn po imieniu i nazwisku
        private void WypelnijOperatorowDanymi()
        {
            listBoxOperator_maszyny.Items.Clear();
            nsAccess2DB.Operator_maszynyBUS operator_maszynyBUS = new nsAccess2DB.Operator_maszynyBUS(_connString);
            _Operator_maszynyBUS.selectQuery("SELECT * FROM Operator_maszyny ORDER BY op_nazwisko ASC;");
            while (!_Operator_maszynyBUS.eof)
            {
                listBoxOperator_maszyny.Items.Add(_Operator_maszynyBUS.VO.Op_nazwisko + " " + _Operator_maszynyBUS.VO.Op_imie);
                _Operator_maszynyBUS.skip();
            }

            if (listBoxOperator_maszyny.Items.Count > 0)
            {
                listBoxOperator_maszyny.SelectedIndex = 0;
            }
        }//WypelnijOperatorowDanymi()
        
        // wyświetla listę maszyn dla danego operatora
        private void WypelnijOperatorowMaszynami()
        {
            listBox_maszyny.Items.Clear();
            nsAccess2DB.Operator_maszyny_MaszynyBUS operator_maszyny_MaszynyBUS = new nsAccess2DB.Operator_maszyny_MaszynyBUS(_connString);
            nsAccess2DB.MaszynyBUS MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            _Operator_maszyny_MaszynyBUS.selectQuery("SELECT Maszyny.Nazwa, Operator_maszyny.Identyfikator AS ID_op_maszyny, " +
                "Maszyny.Identyfikator AS ID_maszyny, " +
                "Operator_maszyny.Nazwa_op_maszyny FROM Operator_maszyny " +
                "INNER JOIN(Maszyny INNER JOIN Operator_maszyny_Maszyny ON " +
                "Maszyny.[Identyfikator] = Operator_maszyny_Maszyny.[ID_maszyny]) ON " +
                "Operator_maszyny.[Identyfikator] = Operator_maszyny_Maszyny.[ID_op_maszyny] " +
                "WHERE(((Operator_maszyny_Maszyny.[ID_op_maszyny]) = " + toolStripStatusLabel_ID_Operatora.Text
               + "));");

            while (!_MaszynyBUS.eof)
            {
                listBox_maszyny.Items.Add(_MaszynyBUS.VO.Nazwa);
                _MaszynyBUS.skip();
            }

            if (listBox_maszyny.Items.Count > 0)
            {
                listBox_maszyny.SelectedIndex = 0;
            }
        }

        // TO DO -> wypełnić listBox_maszyny, listą obsługiwanych przez operatora maszyn. (pozyskać dane) WHERE (([ID_maszyny]=" + ID_maszyny.ToString() +

        /// <summary>
        /// zmiana indeksu w list box operator maszyny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxOperator_maszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.Operator_maszyny_MaszynyBUS operator_maszyny_MaszynyBUS = new nsAccess2DB.Operator_maszyny_MaszynyBUS(_connString);
            _Operator_maszynyBUS.idx = listBoxOperator_maszyny.SelectedIndex;
            listBoxOperator_maszyny.Text = _Operator_maszynyBUS.VO.Identyfikator.ToString();

            //TODO zmiana indeksu operatora ma zmieniać listę podległych mu maszyn
            listBox_maszyny.Text = _Operator_maszyny_MaszynyBUS.VO.ID_op_maszyny.ToString();

            toolStripStatusLabel_ID_Operatora.Text = _Operator_maszynyBUS.VO.Identyfikator.ToString(); // ID operatora maszyny

            textBoxImie.Text = _Operator_maszynyBUS.VO.Op_imie;
            textBoxNazwisko.Text = _Operator_maszynyBUS.VO.Op_nazwisko;
            comboBoxDzial_operator_maszyny.Text = _Operator_maszynyBUS.VO.Nazwa_dzial;
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
            _Operator_maszynyBUS.selectQuery("SELECT * FROM Operator_maszyny ORDER BY Op_nazwisko ASC;");
            while (!_Operator_maszynyBUS.eof)
            {
                listBoxOperator_maszyny.Items.Add(_Operator_maszynyBUS.VO.Op_imie + " " + _Operator_maszynyBUS.VO.Op_nazwisko + " -> " + _Operator_maszynyBUS.VO.Rok + "-" + _Operator_maszynyBUS.VO.Mc + "-" + _Operator_maszynyBUS.VO.Dzien);
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
            _Operator_maszynyBUS.selectQuery("SELECT * FROM Operator_maszyny WHERE Op_nazwisko LIKE '" + textBoxWyszukiwanie.Text + "%' OR Op_imie LIKE '%" + textBoxWyszukiwanie.Text + "%';");
            while (!_Operator_maszynyBUS.eof)
            {
                listBoxOperator_maszyny.Items.Add(_Operator_maszynyBUS.VO.Nazwa_op_maszyny + " -> " + _Operator_maszynyBUS.VO.Nazwa_dzial);
                _Operator_maszynyBUS.skip();
            }

            if (listBoxOperator_maszyny.Items.Count > 0)
            {
                listBoxOperator_maszyny.SelectedIndex = 0;
            }
        }//buttonSzukaj_Click

        private void buttonNowa_Click(object sender, EventArgs e)
        {
            textBoxImie.Text = string.Empty;
            textBoxNazwisko.Text = string.Empty;

            comboBoxDzial_operator_maszyny.SelectedIndex = -1;
            comboBoxDzial_operator_maszyny.Enabled = true;
            comboBoxDzial_operator_maszyny.SelectedIndex = 0;
            comboBoxDzial_operator_maszyny.Refresh();

            textBoxUprawnienie.Text = string.Empty;
            dateTimePickerData_konca_upr.Text = string.Empty;

            buttonAnuluj.Enabled = true;
            //WypelnijOperatorowDanymi();
        }

        private void buttonZapisz_Click(object sender, EventArgs e)
        {            
            if (textBoxNazwisko.Text == string.Empty) // textBoxNazwisko nie może być puste
            {
                MessageBox.Show("Proszę uzupełnij nazwisko operatora maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (textBoxImie.Text == string.Empty)// textBoxImie nie może być puste
            {
                MessageBox.Show("Proszę uzupełnij imię operatora maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            buttonNowa.Enabled = true; //nieaktywny przycisk nowa przy zapisie
            buttonUsun.Enabled = true; //nieaktywny przycisk Usuń przy zapisie

            nsAccess2DB.Operator_maszynyVO VO = new nsAccess2DB.Operator_maszynyVO();
            VO.Op_imie = textBoxImie.Text;
            VO.Op_nazwisko = textBoxNazwisko.Text;
            VO.Nazwa_dzial = comboBoxDzial_operator_maszyny.Text;
            VO.Uprawnienie = textBoxUprawnienie.Text;
            VO.Rok = dateTimePickerData_konca_upr.Value.Year;
            VO.Mc = dateTimePickerData_konca_upr.Value.Month;
            VO.Data_konca_upr = (VO.Rok.ToString() + VO.Mc.ToString("00") + VO.Dzien.ToString("00"));
            VO.Dzien = dateTimePickerData_konca_upr.Value.Day;

            if (toolStripStatusLabel_ID_Operatora.Text == string.Empty) //nowa pozycja w tabeli operator maszyny
            {
                VO.Identyfikator = -1;
            }
            else
            {
                VO.Identyfikator = int.Parse(toolStripStatusLabel_ID_Operatora.Text);
            }

            if (toolStripStatusLabel_ID_Operatora.Text == string.Empty)
            {
                listBoxOperator_maszyny.SelectedIndex = listBoxOperator_maszyny.Items.Count - 1;
            }
            else
            {
                listBoxOperator_maszyny.SelectedIndex = _Operator_maszynyBUS.getIdx(VO.Identyfikator);
            }

            _Operator_maszynyBUS.write(VO);
            //  _Operator_maszyny_MaszynyBUS.write(VO);
            //TODO write w Operator_maszyny_Maszyny

            MessageBox.Show("Pozycja zapisana w bazie", "komunikat", MessageBoxButtons.OK, MessageBoxIcon.Information);

            WypelnijOperatorowDanymi();
            WypelnijOperatorowMaszynami();
        }           

        private void buttonAnuluj_Click(object sender, EventArgs e)
        {
            int idx = listBoxOperator_maszyny.SelectedIndex;
            textBoxImie.Text = string.Empty;
            textBoxNazwisko.Text = string.Empty;
            comboBoxDzial_operator_maszyny.Text = string.Empty;
            textBoxUprawnienie.Text = string.Empty;
            dateTimePickerData_konca_upr.Text = string.Empty;

            WypelnijOperatorowDanymi();
            WypelnijOperatorowMaszynami();
            listBoxOperator_maszyny.SelectedIndex = idx;
        }

        private void buttonUsun_Click(object sender, EventArgs e)
        {
            _Operator_maszynyBUS.delete((int)listBoxOperator_maszyny.Tag);
            textBoxImie.Text = string.Empty;
            comboBoxDzial_operator_maszyny.Text = string.Empty;
            textBoxUprawnienie.Text = string.Empty;
            dateTimePickerData_konca_upr.Text = string.Empty;
            WypelnijOperatorowDanymi();
        }// buttonUsun_Click

        // button odświez
        private void toolStripButtonOdswiez_Click(object sender, EventArgs e)
        {
            listBoxOperator_maszyny.Items.Clear();
            WypelnijOperatorowDanymi();
        }//toolStripButtonOdswiez_Click

        // button pomocy - wywołuje plik pomocy do programu
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
