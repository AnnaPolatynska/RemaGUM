﻿using System;
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

        private nsAccess2DB.OperatorBUS _OperatorBUS;
        private nsAccess2DB.Maszyny_OperatorBUS _Maszyny_OperatorBUS;
        private nsAccess2DB.MaszynyBUS _MaszynyBUS;
        private nsAccess2DB.DzialBUS _DzialBUS;

        private ToolTip _tt; //podpowiedzi dla niektórych kontolek

        private int[] _maszynaTag; // przechowuje identyfikatory maszyn
        
        /// <summary>
        /// Konstruktor formularza Operator maszyny.
        /// </summary>
        /// <param name="connStr">Połaczenie z bazą talela Nazwa_operatora.</param>
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

            _OperatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            _Maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            _MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            _DzialBUS = new nsAccess2DB.DzialBUS(_connString);

            _OperatorBUS.select();
            _Maszyny_OperatorBUS.select();
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
            nsAccess2DB.OperatorBUS operator_maszynyBUS = new nsAccess2DB.OperatorBUS(_connString);
            _OperatorBUS.selectQuery("SELECT * FROM Operator ORDER BY op_nazwisko ASC;");
            listBoxOperator_maszyny.Items.Clear();

            while (!_OperatorBUS.eof)
            {
                listBoxOperator_maszyny.Items.Add(_OperatorBUS.VO.Op_nazwisko + " " + _OperatorBUS.VO.Op_imie);
                _OperatorBUS.skip();
            }

            if (listBoxOperator_maszyny.Items.Count > 0)
            {
                listBoxOperator_maszyny.SelectedIndex = 0;
            }
        }//WypelnijOperatorowDanymi()
        
        // wyświetla listę maszyn dla danego operatora
        private void WypelnijOperatorowMaszynami()
        {
            nsAccess2DB.Maszyny_OperatorBUS operator_maszyny_MaszynyBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            nsAccess2DB.MaszynyBUS MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            listBox_maszyny.Items.Clear();

            _Maszyny_OperatorBUS.select();

            while (!_MaszynyBUS.eof)
            {
                listBox_maszyny.Items.Add(MaszynyBUS.VO.Nazwa);
                
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
            _OperatorBUS.idx = listBoxOperator_maszyny.SelectedIndex;
            listBoxOperator_maszyny.Text = _OperatorBUS.VO.Identyfikator.ToString();

            ListBox lb = (ListBox)sender;
            if (lb.SelectedIndex < 0)
            {
                return;
            }
      
            // listBox_maszyny.Text = _Maszyny_OperatorBUS.VO.ID_op_maszyny.ToString();

            textBoxImie.Text = _OperatorBUS.VO.Op_imie;
            textBoxNazwisko.Text = _OperatorBUS.VO.Op_nazwisko;
            comboBoxDzial_operator_maszyny.Text = _OperatorBUS.VO.Nazwa_dzial;
            textBoxUprawnienie.Text = _OperatorBUS.VO.Uprawnienie;
            dateTimePickerData_konca_upr.Value = new DateTime(_OperatorBUS.VO.Rok, _OperatorBUS.VO.Mc, _OperatorBUS.VO.Dzien);
            toolStripStatusLabel_ID_Operatora.Text = _OperatorBUS.VO.Identyfikator.ToString(); // ID operatora maszyny
            toolStripStatusLabel_ID_Operatora.Tag = _OperatorBUS.VO.Identyfikator; // ID operatora maszyny

            //wypełnij listę maszyn obsługiwanych przez wybranego operatora - zmiana indeksu operatora ma zmieniać listę podległych mu maszyn
            nsAccess2DB.Maszyny_OperatorBUS OMM_BUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            nsAccess2DB.MaszynyBUS MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            
            OMM_BUS.select((int)toolStripStatusLabel_ID_Operatora.Tag);

            listBox_maszyny.Items.Clear();
            _maszynaTag = new int[OMM_BUS.count];
            int idx = 0;

            while (!OMM_BUS.eof)
            {
                listBox_maszyny.Items.Add(_MaszynyBUS.VO.Nazwa); // zmienić na nazwę maszyny
                _maszynaTag[idx] = OMM_BUS.VO.ID_maszyny;
                OMM_BUS.skip();
                idx++;
            }

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
            nsAccess2DB.OperatorBUS operator_maszynyBUS = new nsAccess2DB.OperatorBUS(_connString);
            _OperatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Op_nazwisko ASC;");
            while (!_OperatorBUS.eof)
            {
                listBoxOperator_maszyny.Items.Add(_OperatorBUS.VO.Op_imie + " " + _OperatorBUS.VO.Op_nazwisko + " -> " + _OperatorBUS.VO.Rok + "-" + _OperatorBUS.VO.Mc + "-" + _OperatorBUS.VO.Dzien);
                _OperatorBUS.skip();
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

            nsAccess2DB.OperatorBUS operator_maszynyBUS = new nsAccess2DB.OperatorBUS(_connString);
            _OperatorBUS.selectQuery("SELECT * FROM Operator WHERE Op_nazwisko LIKE '" + textBoxWyszukiwanie.Text + "%' OR Op_imie LIKE '%" + textBoxWyszukiwanie.Text + "%';");
            while (!_OperatorBUS.eof)
            {
                listBoxOperator_maszyny.Items.Add(_OperatorBUS.VO.Nazwa_operatora + " -> " + _OperatorBUS.VO.Nazwa_dzial);
                _OperatorBUS.skip();
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

            nsAccess2DB.OperatorVO VO = new nsAccess2DB.OperatorVO();
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
                listBoxOperator_maszyny.SelectedIndex = _OperatorBUS.getIdx(VO.Identyfikator);
            }

            _OperatorBUS.write(VO);
            //  _Maszyny_OperatorBUS.write(VO);
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
            _OperatorBUS.delete((int)listBoxOperator_maszyny.Tag);
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
