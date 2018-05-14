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
    public partial class SpisForm : Form
    {
        private string _connStr = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Projects\\RemaGUM\\RemaGUM.mdb"; //połaczenie z bazą danych

        private string _helpFile = Application.StartupPath + "\\RemaGUM.chm"; //plik pomocy RemaGUM

        private nsAccess2DB.MaszynyBUS _MaszynyBUS;
        private nsAccess2DB.KategoriaBUS _KategoriaBUS;
        private nsAccess2DB.Osoba_zarzadzajacaBUS _Osoba_zarzadzajacaBUS;
        private nsAccess2DB.DzialBUS _DzialBUS;
        private nsAccess2DB.CzestotliwoscBUS _CzestotliwoscBUS;
        private nsAccess2DB.PropozycjaBUS _PropozycjaBUS;
        private nsAccess2DB.Stan_technicznyBUS _Stan_technicznyBUS;
        private nsAccess2DB.Operator_maszynyBUS _Operator_maszynyBUS;

        private ToolTip _tt; //podpowiedzi dla niektórych kontolek

        private int _interwalPrzegladow = 2;    //w latach
        
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
            comboBoxKategoria.TabIndex = 1;
            textBoxNazwa.TabIndex = 2;
            textBoxTyp.TabIndex = 3;
            textBoxNr_inwentarzowy.TabIndex = 4;
            textBoxNr_fabryczny.TabIndex = 5;
            textBoxRok_produkcji.TabIndex = 6;
            textBoxProducent.TabIndex = 7;
            pictureBox1.TabIndex = 8;
            comboBoxOsoba_zarzadzajaca.TabIndex = 9;
            comboBoxOperator_maszyny.TabIndex = 10;
            textBoxNr_pom.TabIndex = 11;
            comboBoxDzial.TabIndex = 12;
            textBoxNr_prot_BHP.TabIndex = 13;
            dateTimePickerData_ost_przegl.TabIndex = 14;
            dateTimePickerData_kol_przegl.TabIndex = 15;
            richTextBoxUwagi.TabIndex = 16;
            //ankietka
            comboBoxWykorzystanie.TabIndex = 17;
            comboBoxStan_techniczny.TabIndex = 18;
            comboBoxPropozycja.TabIndex = 19;
            //przyciski zapisz/edytuj itp
            buttonNowa.TabIndex = 20;
            buttonZapisz.TabIndex = 21;
            buttonAnuluj.TabIndex = 22;
            buttonUsun.TabIndex = 23;
            //sortowanie po radio buttonach
            radioButtonTyp.TabIndex = 24;
            radioButtonNr_inwentarzowy.TabIndex = 25;
            radioButtonNr_fabryczny.TabIndex = 26;
            radioButtonNr_pomieszczenia.TabIndex = 27;
            radioButtonNazwa.TabIndex = 28;
            radioButtonData_kol_przegladu.TabIndex = 29;
            //wyszukiwanie po wpisanej nazwie ???
            textBoxWyszukiwanie.TabIndex = 30;
            buttonSzukaj.TabIndex = 31;
            //toolStripButton
            

            _MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _CzestotliwoscBUS = new nsAccess2DB.CzestotliwoscBUS(_connStr);
            _KategoriaBUS = new nsAccess2DB.KategoriaBUS(_connStr);
            _DzialBUS = new nsAccess2DB.DzialBUS(_connStr);
            _PropozycjaBUS = new nsAccess2DB.PropozycjaBUS(_connStr);
            _Stan_technicznyBUS = new nsAccess2DB.Stan_technicznyBUS(_connStr);
            _Osoba_zarzadzajacaBUS = new nsAccess2DB.Osoba_zarzadzajacaBUS(_connStr);
            _Operator_maszynyBUS = new nsAccess2DB.Operator_maszynyBUS(_connStr);
            
            _MaszynyBUS.select();

            WypelnijMaszynyNazwami();
            WypelnijOsoba_zarzadzajaca();
            WypelnijCzestotliwosc();
            WypelnijKategorie();
            WypelnijDzial();            
            WypelnijPropozycje();
            WypelnijStan_techniczny();
            WypelnijOperator_maszyny();
           
           if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }

            _tt = new ToolTip();

            _tt.SetToolTip(listBoxMaszyny, "Lista maszyn, przyrządów i urządzeń itp.");
            _tt.SetToolTip(comboBoxKategoria, "Kategoria maszyn, przyrządów, urządzeń np. maszyny warsztatowe, lub przyrządy pomiarowe.");
            _tt.SetToolTip(textBoxNazwa, "Nazwa maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxTyp, "Typ maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxNr_inwentarzowy, "Numer naklejki GUM (inwentarzowy) maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxNr_fabryczny, "Numer fabryczny maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxRok_produkcji, "Rok wyprodukowania.");
            _tt.SetToolTip(textBoxProducent, "Producent maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(pictureBox1, "Zdjęcie maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(comboBoxOsoba_zarzadzajaca, "Opiekun maszyny.");
            _tt.SetToolTip(comboBoxOperator_maszyny, "Główna osoba użytkująca maszynę (posiadająca odpowiednie uprawnienia).");
            _tt.SetToolTip(textBoxNr_pom, "Numer pomieszczenia GUM, gdzie znajuje się maszyna, przyrząd lub urządzenie.");
            _tt.SetToolTip(comboBoxDzial, "Nazwa dział  lista maszyn, przyrządów i urządzeń itp.");
            _tt.SetToolTip(textBoxNr_prot_BHP, "Numer nadany w protokole kontroli dostosowania maszyny do minimalnych wymagań w zakresie BHP z dnia 12.06.2006 r.");
            _tt.SetToolTip(dateTimePickerData_ost_przegl, "Data ostatniej kontroli dostosowania do minimalnych wymagań w zakresie BHP, lub innego dokumentu.");
            _tt.SetToolTip(dateTimePickerData_kol_przegl, "Spodziewana data kolejnej kontroli dostosowania do minimalnych wymagań w zakresie BHP, lub innej dotyczacej okresowych przeglądów.");
            _tt.SetToolTip(richTextBoxUwagi, "Opis wszelkich innych zdarzeń/statusów mających istotny wpływ na maszynę, przyrząd lub urządzenie.");
            _tt.SetToolTip(comboBoxWykorzystanie, "Częstotliwość wykorzystania: nieuzywana, rzadziej niż kilka razy w roku, kilka razy w roku, kilka razy w okresie pół roku, kilka razy w kwartale, kilka razy w miesiącu.");
            _tt.SetToolTip(comboBoxStan_techniczny, "Stan techniczy: złom, do naprawy, dobry.");
            _tt.SetToolTip(comboBoxPropozycja, "Propozycja: do likwidacji, do remontu, zachować.");
            _tt.SetToolTip(buttonNowa, "Nowa pozycja w bazie.");
            _tt.SetToolTip(buttonZapisz, "Zapis nowej maszyny, przyrządu lub urządzenia lub edycja wybranej pozycji.");
            _tt.SetToolTip(buttonAnuluj, "Anulowanie zmiany.");
            _tt.SetToolTip(buttonUsun, "Usuwa pozycja w bazie.");
            _tt.SetToolTip(radioButtonTyp, "Sortuj po typie.");
            _tt.SetToolTip(radioButtonNr_inwentarzowy, "Sortuj po numerze inwentarzowym.");
            _tt.SetToolTip(radioButtonNr_fabryczny, "Sortuj po numerze fabrycznym.");
            _tt.SetToolTip(radioButtonNr_pomieszczenia, "Sortuj po numerze pomieszczenia.");
            _tt.SetToolTip(radioButtonNazwa, "Sortuj po nazwie maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(radioButtonData_ost_przegl, "Sortuj po dacie ostatniego przegladu.");
            _tt.SetToolTip(radioButtonData_kol_przegladu, "Sortuj po dacie kolejnego przeglądu.");
            _tt.SetToolTip(textBoxWyszukiwanie, "Wpisz czego szukasz.");
            _tt.SetToolTip(buttonSzukaj, "Szukanie w bazie.");
            
        }//public SpisForm()
               
        //  //  //  //  //  //  //  wyświetlanie w liście maszyn
        //wyświetla listę maszyn po nazwie
        private void WypelnijMaszynyNazwami()
        {
            nsAccess2DB.MaszynyVO VO;
            listBoxMaszyny.Items.Clear();
            _MaszynyBUS.select();

            while (!_MaszynyBUS.eof)
            {
                VO = _MaszynyBUS.VO;
                listBoxMaszyny.Items.Add(VO.Nazwa+ " - " + _MaszynyBUS.VO.Nr_fabryczny.ToString());
                _MaszynyBUS.skip();
            }
           
        }//wypelnijMaszynyNazwami

        //////////////////////////////////////////////////////////////         Rabio buttony
        private void radioButtonNazwa_CheckedChanged(object sender, EventArgs e)
        {
            listBoxMaszyny.Items.Clear();

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nazwa ASC;");
            while (!_MaszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(_MaszynyBUS.VO.Nazwa);
                _MaszynyBUS.skip();
            }

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }
        }//radioButtonNazwa_CheckedChanged

        private void radioButton_Nr_Inwentarzowy_CheckedChanged(object sender, EventArgs e)
        {
            listBoxMaszyny.Items.Clear();

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_inwentarzowy ASC;");
            while (!_MaszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(_MaszynyBUS.VO.Nr_inwentarzowy + " -> " + _MaszynyBUS.VO.Nazwa);
                _MaszynyBUS.skip();
            }

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }
        }//radioButton_Nr_Inwentarzowy_CheckedChanged

        private void radioButton_Typ_CheckedChanged(object sender, EventArgs e)
        {
            listBoxMaszyny.Items.Clear();
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Typ ASC;");
            while (!_MaszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(_MaszynyBUS.VO.Typ + " -> " + _MaszynyBUS.VO.Nazwa);
                _MaszynyBUS.skip();
            }

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }
        }//radioButton_Typ_CheckedChanged
        private void radioButtonNr_fabrycznyCheckedChanged(object sender, EventArgs e)
        {
            listBoxMaszyny.Items.Clear();
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_fabryczny ASC;");
            while (!_MaszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(_MaszynyBUS.VO.Nr_fabryczny + " -> " + _MaszynyBUS.VO.Nazwa);
                _MaszynyBUS.skip();
            }

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }
        }//radioButtonNr_fabrycznyCheckedChanged


        private void radioButton_Nr_Pomieszczenia_CheckedChanged(object sender, EventArgs e)
        {
            listBoxMaszyny.Items.Clear();

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_pom ASC;");
            while (!_MaszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(_MaszynyBUS.VO.Nr_pom + " -> " + _MaszynyBUS.VO.Nazwa);
                _MaszynyBUS.skip();
            }

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }
        }//radioButton_Nr_Pomieszczenia_CheckedChanged

        private void radioButtonData_ost_przegladu_CheckedChanged(object sender, EventArgs e)
        {
            listBoxMaszyny.Items.Clear();
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Data_ost_przegl ASC;");
            while (!_MaszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(_MaszynyBUS.VO.Dz_ost_przeg + "-" + _MaszynyBUS.VO.Mc_ost_przeg + "-" + _MaszynyBUS.VO.Rok_ost_przeg + " r. -> " + _MaszynyBUS.VO.Nazwa);
                _MaszynyBUS.skip();
            }

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }
        }//radioButton_Nr_Pomieszczenia_CheckedChanged
       
        private void radioButtonData_kol_przegladu_CheckedChanged(object sender, EventArgs e)
        {
            listBoxMaszyny.Items.Clear();
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Data_kol_przegl ASC;");
            while (!_MaszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(_MaszynyBUS.VO.Dz_kol_przeg + "-" + _MaszynyBUS.VO.Mc_kol_przeg + "-" + _MaszynyBUS.VO.Rok_kol_przeg + " r. -> " + _MaszynyBUS.VO.Nazwa);
                _MaszynyBUS.skip();
            }

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }
        }//radioButton_Nr_Pomieszczenia_CheckedChanged

        /// <summary>
        /// wyszukuje maszynę po wpisaniu dowolnego ciągu wyrazów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void buttonSzukaj_Click(object sender, EventArgs e)
        {
            listBoxMaszyny.Items.Clear();

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.selectQuery("SELECT * FROM Maszyny WHERE Nazwa LIKE '"+textBoxWyszukiwanie.Text + "%' OR Nazwa LIKE '%"+textBoxWyszukiwanie.Text+"%';");
            //Nr_inwentarzowy LIKE '" + textBoxWyszukiwanie.Text + "%' OR
            while (!_MaszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(_MaszynyBUS.VO.Nazwa + " -> " +_MaszynyBUS.VO.Nr_inwentarzowy );
                _MaszynyBUS.skip();
            }

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }
 
        }//buttonSzukaj_Click

        /// <summary>
        /// zmiana indeksu w list box maszyny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxMaszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            _MaszynyBUS.idx = listBoxMaszyny.SelectedIndex;
            nsAccess2DB.MaszynyVO VO = _MaszynyBUS.VO;

            listBoxMaszyny.Tag = _MaszynyBUS.VO.ID_maszyny;
            toolStripStatusLabelIDVal.Text = _MaszynyBUS.VO.ID_maszyny.ToString();
            comboBoxKategoria.Text = _MaszynyBUS.VO.Kategoria;
            textBoxNazwa.Text = _MaszynyBUS.VO.Nazwa;
            textBoxTyp.Text = _MaszynyBUS.VO.Typ;
            textBoxNr_inwentarzowy.Text = _MaszynyBUS.VO.Nr_inwentarzowy;
            textBoxNr_fabryczny.Text = _MaszynyBUS.VO.Nr_fabryczny;
            textBoxRok_produkcji.Text = _MaszynyBUS.VO.Rok_produkcji;
            textBoxProducent.Text = _MaszynyBUS.VO.Producent;
            pictureBox1.Text = _MaszynyBUS.VO.Zdjecie1;
            comboBoxOsoba_zarzadzajaca.Text = _MaszynyBUS.VO.Osoba_zarzadzajaca;
            textBoxNr_pom.Text = _MaszynyBUS.VO.Nr_pom;
            comboBoxDzial.Text = _MaszynyBUS.VO.Dzial;
            textBoxNr_prot_BHP.Text = _MaszynyBUS.VO.Nr_prot_BHP;
            
            dateTimePickerData_ost_przegl.Value = new DateTime(VO.Rok_ost_przeg, VO.Mc_ost_przeg, VO.Dz_ost_przeg);
            dateTimePickerData_kol_przegl.Value = new DateTime(VO.Rok_kol_przeg, VO.Mc_kol_przeg, VO.Dz_kol_przeg);
           
            richTextBoxUwagi.Text = _MaszynyBUS.VO.Uwagi;
            comboBoxWykorzystanie.Text = _MaszynyBUS.VO.Wykorzystanie;
            comboBoxStan_techniczny.Text = _MaszynyBUS.VO.Stan_techniczny;
            comboBoxPropozycja.Text = _MaszynyBUS.VO.Propozycja;
            comboBoxOperator_maszyny.Text = _MaszynyBUS.VO.Operator_maszyny;

        }//listBoxMaszyny_SelectedIndexChanged

        // // // // // //  // /list boxy z tabeli accessa

        /// <summary>
        /// wypełnia listbox kategoriami maszyn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WypelnijKategorie()
        {
            nsAccess2DB.KategoriaVO VO;
            comboBoxKategoria.Items.Clear();

            _KategoriaBUS.select();
            _KategoriaBUS.top();
            while (!_KategoriaBUS.eof)
            {
                VO = _KategoriaBUS.VO;
                comboBoxKategoria.Items.Add(VO.Nazwa);
                _KategoriaBUS.skip();
            }
        }//WypelnijKategorie
        
        private void comboBoxKategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            _KategoriaBUS.idx = comboBoxKategoria.SelectedIndex;
        }//comboBoxKategoria_SelectedIndexChanged

        private void WypelnijOsoba_zarzadzajaca()
        {
            nsAccess2DB.Osoba_zarzadzajacaVO VO;
            comboBoxOsoba_zarzadzajaca.Items.Clear();

            _Osoba_zarzadzajacaBUS.select();
            _Osoba_zarzadzajacaBUS.top();
            while (!_Osoba_zarzadzajacaBUS.eof)
            {
                VO = _Osoba_zarzadzajacaBUS.VO;
                comboBoxOsoba_zarzadzajaca.Items.Add(VO.Osoba_zarzadzajaca);
                _Osoba_zarzadzajacaBUS.skip();
            }
        }//WypełnijOsobyOdp()

        private void comboBox_Osoba_zarzadzajaca_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Osoba_zarzadzajacaBUS.idx = comboBoxOsoba_zarzadzajaca.SelectedIndex;
        }// comboBox_Osoba_zarzadzajaca_SelectedIndexChanged

        private void WypelnijOperator_maszyny()
        {
            nsAccess2DB.Operator_maszynyVO VO;
            comboBoxOperator_maszyny.Items.Clear();

            _Operator_maszynyBUS.select();
            _Operator_maszynyBUS.top();
            while (!_Operator_maszynyBUS.eof)
            {
                VO = _Operator_maszynyBUS.VO;
                comboBoxOperator_maszyny.Items.Add(VO.Operator_maszyny);
                _Operator_maszynyBUS.skip();
            }
        }// WypelnijOperator_maszyny()

        private void comboBox_Operator_maszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Operator_maszynyBUS.idx = comboBoxOperator_maszyny.SelectedIndex;
        }// comboBox_Operator_maszyny_SelectedIndexChanged


        /// <summary>
        /// wypełnia listbox Działami w których znajdują się maszyny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WypelnijDzial()
        {
            nsAccess2DB.DzialVO VO;
            comboBoxDzial.Items.Clear();

            _DzialBUS.select();
            _DzialBUS.top();
            while (!_DzialBUS.eof)
            {
                VO = _DzialBUS.VO;
                comboBoxDzial.Items.Add(VO.Nazwa);
                _DzialBUS.skip();
            }
        }//WypelnijDzial

        private void comboBoxDzial_SelectedIndexChanged(object sender, EventArgs e)
        {
            _DzialBUS.idx = comboBoxDzial.SelectedIndex;
            comboBoxDzial.Tag = _DzialBUS.VO.Nazwa;
        }//comboBoxDzial_SelectedIndexChanged

        /// <summary>
        /// wypełnia listbox pozycjami częstotliwości użycia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WypelnijCzestotliwosc()
        {
            nsAccess2DB.CzestotliwoscVO VO;
            comboBoxWykorzystanie.Items.Clear();

            _CzestotliwoscBUS.select();
            _CzestotliwoscBUS.top();
            while (!_CzestotliwoscBUS.eof)
            {
                VO = _CzestotliwoscBUS.VO;
                comboBoxWykorzystanie.Items.Add(VO.Nazwa);
                _CzestotliwoscBUS.skip();
            }
        }// wypelnijCzestotliwosc

        // przypisuje w combo identyfikator = nazwę częstotliwości wykorzystania
        private void ComboBoxWykorzystanie_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CzestotliwoscBUS.idx = comboBoxWykorzystanie.SelectedIndex;
            comboBoxWykorzystanie.Tag = _CzestotliwoscBUS.VO.Nazwa;
        }//comboboxWykorzystanie_SelectedIndexChanged
              

        /// <summary>
        /// wypełnia listbox propozycjami co zrobić z maszyną (zachować/złomować itp)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WypelnijPropozycje()
        {
            nsAccess2DB.PropozycjaVO VO;
            comboBoxPropozycja.Items.Clear();

            _PropozycjaBUS.select();
            _PropozycjaBUS.top();
            while (!_PropozycjaBUS.eof)
            {
                VO = _PropozycjaBUS.VO;
                comboBoxPropozycja.Items.Add(VO.Nazwa);
                _PropozycjaBUS.skip();
            }
        }// WypelnijPropozycje()
        private void comboBoxPropozycja_SelectedIndexChanged(object sender, EventArgs e)
        {
            _PropozycjaBUS.idx = comboBoxPropozycja.SelectedIndex;
        }// comboBoxPropozycja_SelectedIndexChanged
        
        private void WypelnijStan_techniczny()
        {
            nsAccess2DB.Stan_technicznyVO VO;
            comboBoxStan_techniczny.Items.Clear();

            _Stan_technicznyBUS.select();
            _Stan_technicznyBUS.top();
            while (!_Stan_technicznyBUS.eof)
            {
                VO = _Stan_technicznyBUS.VO;
                comboBoxStan_techniczny.Items.Add(VO.Nazwa);
                _Stan_technicznyBUS.skip();
            }
        }// WypelnijStan_techniczny()
        private void comboBoxStan_techniczny_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Stan_technicznyBUS.idx = comboBoxStan_techniczny.SelectedIndex;
        }

        ///////////////////////////////////////////////////////////////////// // // // ///   przyciski
        //przycisk Nowa czyści formularz
        private void ButtonNowa_Click(object sender, EventArgs e)
        {
            comboBoxKategoria.SelectedIndex = -1;
            comboBoxKategoria.Enabled = true;
            comboBoxKategoria.SelectedIndex = 0;
            comboBoxKategoria.Refresh();

            textBoxNazwa.Text = string.Empty;
            textBoxTyp.Text = string.Empty;
            textBoxNr_inwentarzowy.Text = string.Empty;
            textBoxNr_fabryczny.Text = string.Empty;
            textBoxRok_produkcji.Text = string.Empty;
            textBoxProducent.Text = string.Empty;
            pictureBox1.Text = string.Empty;

            comboBoxOsoba_zarzadzajaca.SelectedIndex = -1;
            comboBoxOsoba_zarzadzajaca.Enabled = true;
            comboBoxOsoba_zarzadzajaca.SelectedIndex = 0;
            comboBoxOsoba_zarzadzajaca.Refresh();

            textBoxNr_pom.Text = string.Empty;
           
            comboBoxDzial.SelectedIndex = -1;
            comboBoxDzial.Enabled = true;
            comboBoxDzial.SelectedIndex = 0;
            comboBoxDzial.Refresh();
            
            textBoxNr_prot_BHP.Text = string.Empty;
            dateTimePickerData_ost_przegl.Text = string.Empty;
            dateTimePickerData_kol_przegl.Text = string.Empty;
            richTextBoxUwagi.Text = string.Empty;

            comboBoxWykorzystanie.SelectedIndex = -1;
            comboBoxWykorzystanie.Enabled = true;
            comboBoxWykorzystanie.SelectedIndex = 0;
            comboBoxWykorzystanie.Refresh();

            comboBoxStan_techniczny.SelectedIndex = -1;
            comboBoxStan_techniczny.Enabled = true;
            comboBoxStan_techniczny.SelectedIndex = 0;
            comboBoxStan_techniczny.Refresh();

            comboBoxPropozycja.SelectedIndex = -1;
            comboBoxPropozycja.Enabled = true;
            comboBoxPropozycja.SelectedIndex = 0;
            comboBoxPropozycja.Refresh();

            comboBoxOperator_maszyny.SelectedIndex = -1;
            comboBoxOperator_maszyny.Enabled = true;
            comboBoxOperator_maszyny.SelectedIndex = 0;
            comboBoxOperator_maszyny.Refresh();
            
            buttonAnuluj.Enabled = true;
            WypelnijMaszynyNazwami();
        }//ButtonNowa_Click
               
        private void buttonAnuluj_Click(object sender, EventArgs e)
        {
            int idx = listBoxMaszyny.SelectedIndex;

            comboBoxKategoria.Text = string.Empty;
            textBoxNazwa.Text = string.Empty;
            textBoxTyp.Text = string.Empty;
            textBoxNr_inwentarzowy.Text = string.Empty;
            textBoxNr_fabryczny.Text = string.Empty;
            textBoxRok_produkcji.Text = string.Empty;
            textBoxProducent.Text = string.Empty;
            pictureBox1.Text = string.Empty;
            comboBoxOsoba_zarzadzajaca.Text = string.Empty;
            textBoxNr_pom.Text = string.Empty;
            comboBoxDzial.Text = string.Empty;
            textBoxNr_prot_BHP.Text = string.Empty;
            dateTimePickerData_ost_przegl.Text = string.Empty;
            dateTimePickerData_kol_przegl.Text = string.Empty;
            richTextBoxUwagi.Text = string.Empty;
            comboBoxWykorzystanie.Text = string.Empty;
            comboBoxStan_techniczny.Text = string.Empty;
            comboBoxPropozycja.Text = string.Empty;
            comboBoxOperator_maszyny.Text = string.Empty;

            WypelnijMaszynyNazwami();
            listBoxMaszyny.SelectedIndex = idx;
        }//buttonAnuluj_Click
        
        private void buttonUsun_Click(object sender, EventArgs e)
        {
            _MaszynyBUS.delete((int)listBoxMaszyny.Tag);

            comboBoxKategoria.Text = string.Empty;
            textBoxNazwa.Text = string.Empty;
            textBoxTyp.Text = string.Empty;
            textBoxNr_inwentarzowy.Text = string.Empty;
            textBoxNr_fabryczny.Text = string.Empty;
            textBoxRok_produkcji.Text = string.Empty;
            textBoxProducent.Text = string.Empty;
            pictureBox1.Text = string.Empty;
            comboBoxOsoba_zarzadzajaca.Text = string.Empty;
            textBoxNr_pom.Text = string.Empty;
            comboBoxDzial.Text = string.Empty;
            textBoxNr_prot_BHP.Text = string.Empty;
            dateTimePickerData_ost_przegl.Text = string.Empty;
            dateTimePickerData_kol_przegl.Text = string.Empty;
            richTextBoxUwagi.Text = string.Empty;
            comboBoxWykorzystanie.Text = string.Empty;
            comboBoxStan_techniczny.Text = string.Empty;
            comboBoxPropozycja.Text = string.Empty;
            comboBoxOperator_maszyny.Text = string.Empty;
            
            WypelnijMaszynyNazwami();
           
        }//buttonUsun_Click

        private void buttonZapisz_Click(object sender, EventArgs e)
        {
            if (textBoxNazwa.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij nazwę", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            buttonNowa.Enabled = true;
                     
            nsAccess2DB.MaszynyVO VO = new nsAccess2DB.MaszynyVO();

            //int idx = listBoxMaszyny.SelectedIndex;
            int idx = -1;

            VO.Kategoria = comboBoxKategoria.Text;
            VO.Nazwa = textBoxNazwa.Text.Trim();
            VO.Typ = textBoxTyp.Text.Trim();
            VO.Nr_inwentarzowy = textBoxNr_inwentarzowy.Text.Trim();
            VO.Nr_fabryczny = textBoxNr_fabryczny.Text.Trim();
            VO.Rok_produkcji = textBoxRok_produkcji.Text.Trim();
            VO.Producent = textBoxProducent.Text.Trim();
            VO.Zdjecie1 = pictureBox1.Text;  /////                ???????????????? obrazek
            VO.Osoba_zarzadzajaca = comboBoxOsoba_zarzadzajaca.Text.Trim();
            VO.Nr_pom = textBoxNr_pom.Text;
            VO.Dzial = comboBoxDzial.Text;
            VO.Nr_prot_BHP = textBoxNr_prot_BHP.Text;

            VO.Rok_ost_przeg = dateTimePickerData_ost_przegl.Value.Year;
            VO.Mc_ost_przeg = dateTimePickerData_ost_przegl.Value.Month;
            VO.Dz_ost_przeg = dateTimePickerData_ost_przegl.Value.Day;
            VO.Data_ost_przegl = int.Parse(VO.Rok_ost_przeg.ToString() + VO.Mc_ost_przeg.ToString("00") + VO.Dz_ost_przeg.ToString("00"));

            DateTime dt = new DateTime(dateTimePickerData_ost_przegl.Value.Ticks);
            dt = dt.AddYears(_interwalPrzegladow);
            VO.Rok_kol_przeg = dt.Year;
            VO.Mc_kol_przeg = dt.Month;
            VO.Dz_kol_przeg = dt.Day;
            VO.Data_kol_przegl = int.Parse(dt.Year.ToString() + dt.Month.ToString("00") + dt.Day.ToString("00"));
            
            /* ręczne ustawienie daty
            VO.Rok_kol_przeg = dateTimePickerData_kol_przegl.Value.Year;
            VO.Mc_kol_przeg = dateTimePickerData_kol_przegl.Value.Month;
            VO.Dz_kol_przeg = dateTimePickerData_kol_przegl.Value.Day;
            VO.Data_kol_przegl = int.Parse(VO.Rok_kol_przeg.ToString() + VO.Mc_kol_przeg.ToString("00") + VO.Dz_kol_przeg.ToString("00"));
            */

            VO.Uwagi = richTextBoxUwagi.Text.Trim();
            VO.Wykorzystanie = comboBoxWykorzystanie.Text;
            VO.Stan_techniczny = comboBoxStan_techniczny.Text;
            VO.Propozycja = comboBoxPropozycja.Text;
            VO.Operator_maszyny = comboBoxOperator_maszyny.Text;
                   
            listBoxMaszyny.SelectedIndex = idx;
            
            //nowa pozycja na liście maszyn
            if (toolStripStatusLabelIDVal.Text == string.Empty)
            { VO.ID_maszyny = -1; }
            else
            {
              VO.ID_maszyny = int.Parse(toolStripStatusLabelIDVal.Text);
            }

            buttonUsun.Enabled = listBoxMaszyny.Items.Count > 0;
            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = listBoxMaszyny.Items.Count - 1;
            }
                     
             _MaszynyBUS.write(VO);

            MessageBox.Show("Pozycja zapisana w bazie", "komunikat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            WypelnijMaszynyNazwami();
        }//buttonZapisz_Click

        private void toolStripButtonOdswiez_Click(object sender, EventArgs e)
        {
            WypelnijMaszynyNazwami();
        }//toolStripButtonOdswiez_Click

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

        private void toolStripButtonOperator_Click(object sender, EventArgs e)
        {
            Operator_maszynyForm frame = new Operator_maszynyForm();
            frame.Show();

        }

        private void toolStripButtonOs_zarzadzajaca_Click(object sender, EventArgs e)
        {
            Os_zarzadzajacaForm frame = new Os_zarzadzajacaForm();
            frame.Show();
        }
    }// public partial class SpisForm : Form
       
}//namespace RemaGUM
