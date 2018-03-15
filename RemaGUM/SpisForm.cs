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

        string _Program = "RemaGUM";

        private nsAccess2DB.MaszynyBUS _MaszynyBUS;
        private nsAccess2DB.CzestotliwoscBUS _CzestotliwoscBUS;
       
        private ToolTip _tt;            //podpowiedzi dla niektórych kontolek
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
            textBoxNr_GUM.TabIndex = 9;
            comboBoxNr_pom.TabIndex = 10;
            comboBoxDzial.TabIndex = 11;
            textBoxNr_prot_BHP.TabIndex = 12;
            dateTimePickerData_ost_przegl.TabIndex = 13;
            dateTimePickerData_kol_przegl.TabIndex = 14;
            richTextBoxUwagi.TabIndex = 15;
            //ankietka
            comboBoxWykorzystanie.TabIndex = 16;
            comboBoxStan_techniczny.TabIndex = 17;
            comboBoxPriorytet.TabIndex = 18;
            comboBoxPropozycja.TabIndex = 19;
            textBoxPunktacja.TabIndex = 20;
            //przyciski zapisz/edytuj itp
            buttonNowa.TabIndex = 21;
            buttonZapisz.TabIndex = 22;
            buttonAnuluj.TabIndex = 23;
            buttonUsun.TabIndex = 24;
            //sortowanie po radio buttonach
            radioButton_Typ.TabIndex = 25;
            radioButton_Nr_Inwentarzowy.TabIndex = 26;
            radioButton_Nr_Fabryczny.TabIndex = 27;
            radioButton_Nr_Pomieszczenia.TabIndex = 28;
            radioButton_Nazwa.TabIndex = 29;
            //wyszukiwanie po wpisanej nazwie ???
            textBoxWyszukiwanie.TabIndex = 30;
            buttonSzukaj.TabIndex = 31;
            

            _MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _CzestotliwoscBUS = new nsAccess2DB.CzestotliwoscBUS(_connStr);

            _MaszynyBUS.select();

            WypelnijMaszynyNazwami();
            wypelnijCzestotliwosc();

            if (listBoxMaszyny.Items.Count > 0) listBoxMaszyny.SelectedIndex = 0;

            _tt = new ToolTip();

            _tt.SetToolTip(listBoxMaszyny, "lista maszyn, przyrządów i urządzeń itp.");
            _tt.SetToolTip(comboBoxKategoria, "kategoria maszyn, przyrządów, urządzeń np. maszyny warsztatowe, lub przyrządy pomiarowe.");
            _tt.SetToolTip(textBoxNazwa, "nazwa maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxTyp, "typ maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxNr_inwentarzowy, "numer inwentarzowy maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxNr_fabryczny, "numer fabryczny maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxRok_produkcji, "rok wyprodukowania.");
            _tt.SetToolTip(textBoxProducent, "Producent maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(pictureBox1, "zdjęcie maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxNr_GUM, "numer naklejki GUM znajdującej się na maszynie, przyrządzie lub urządzeniu.");
            _tt.SetToolTip(comboBoxNr_pom, "numer pomieszczenia GUM, gdzie znajuje się maszyna, przyrząd lub urządzenie.");
            _tt.SetToolTip(comboBoxDzial, "nazwa dział  lista maszyn, przyrządów i urządzeń itp.");
            _tt.SetToolTip(textBoxNr_prot_BHP, "numer nadany w protokole kontroli dostosowania maszyny do minimalnych wymagań w zakresie BHP z dnia 12.06.2006 r.");
            _tt.SetToolTip(dateTimePickerData_ost_przegl, "data ostatniej kontroli dostosowania do minimalnych wymagań w zakresie BHP, lub innego dokumentu.");
            _tt.SetToolTip(dateTimePickerData_kol_przegl, "spodziewana data kolejnej kontroli dostosowania do minimalnych wymagań w zakresie BHP, lub innej dotyczacej okresowych przeglądów.");
            _tt.SetToolTip(richTextBoxUwagi, "opis wszelkich innych zdarzeń/statusów mających istotny wpływ na maszynę, przyrząd lub urządzenie.");
            _tt.SetToolTip(comboBoxWykorzystanie, "częstotliwość wykorzystania: nieuzywana, rzadziej niż kilka razy w roku, kilka razy w roku, kilka razy w okresie pół roku, kilka razy w kwartale, kilka razy w miesiącu.");
            _tt.SetToolTip(comboBoxStan_techniczny, "stan techniczy: złom, do naprawy, dobry.");
            _tt.SetToolTip(comboBoxPriorytet, "priorytet naprawy/wymiany: nie pilne, w miarę możliwości, pilne, bardzo pilne.");
            _tt.SetToolTip(comboBoxPropozycja, "propozycja: do likwidacji,do remontu, zachować.");
            _tt.SetToolTip(textBoxPunktacja, "liczba zdobytych punktów, okreslająca przydatność danej maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(buttonNowa, "nowa pozycja w bazie.");
            _tt.SetToolTip(buttonZapisz, "zapis nowej maszyny, przyrządu lub urządzenia lub edycja wybranej pozycji.");
            _tt.SetToolTip(buttonAnuluj, "anulowanie zmiany.");
            _tt.SetToolTip(buttonUsun, "usuwa pozycja w bazie.");
            _tt.SetToolTip(radioButton_Typ, "sortuj po typie.");
            _tt.SetToolTip(radioButton_Nr_Inwentarzowy, "sortuj po numerze inwentarzowym.");
            _tt.SetToolTip(radioButton_Nr_Fabryczny, "sortuj po numerze fabrycznym.");
            _tt.SetToolTip(radioButton_Nr_Pomieszczenia, "sortuj po numerze pomieszczenia.");
            _tt.SetToolTip(radioButton_Nazwa, "sortuj po nazwie maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxWyszukiwanie, "wpisz czego szukasz.");
            _tt.SetToolTip(buttonSzukaj, "szukanie w bazie.");
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
            //Przycisk zapisz nieaktywny na starcie
           // buttonZapisz.Enabled = listBoxMaszyny.SelectedIndex > -1;
        }//wypelnijMaszynyNazwami
        
        /// <summary>
        /// wyświetla listę maszyn po Typie
        /// </summary>
        private void WypelnijMaszynyTypami() {
            nsAccess2DB.MaszynyVO VO;
            listBoxMaszyny.Items.Clear();

            _MaszynyBUS.select();

            while (!_MaszynyBUS.eof)
            {
                VO = _MaszynyBUS.VO;
                listBoxMaszyny.Items.Add(VO.Nazwa + " - " + _MaszynyBUS.VO.Typ);
                _MaszynyBUS.skip();
            }
        }//WypelnijMaszynyTypami

        /// <summary>
        /// wypełnia nr inwentarzowy maszyny
        /// </summary>
        private void WypełnijMaszynyNrInwentarzowym() {
            nsAccess2DB.MaszynyVO VO;
            listBoxMaszyny.Items.Clear();

            _MaszynyBUS.select();

            while (!_MaszynyBUS.eof)
            {
                VO = _MaszynyBUS.VO;
                listBoxMaszyny.Items.Add(VO.Nazwa + " - " + _MaszynyBUS.VO.Nr_inwentarzowy);
                _MaszynyBUS.skip();
            }
        }//WypełnijMaszynyNrInwentarzowym
        
        /// <summary>
        /// wypełnia nr fabryczny maszyny
        /// </summary>
        private void WypełnijMaszynyNrFabryczny()
        {
            nsAccess2DB.MaszynyVO VO;
            listBoxMaszyny.Items.Clear();

            _MaszynyBUS.select();

            while (!_MaszynyBUS.eof)
            {
                VO = _MaszynyBUS.VO;
                listBoxMaszyny.Items.Add(VO.Nazwa + " - " + _MaszynyBUS.VO.Nr_fabryczny);
                _MaszynyBUS.skip();
            }
        }//WypełnijMaszynyNrFabryczny

        /// <summary>
        /// wypełnia nr fabryczny maszyny
        /// </summary>
        private void WypełnijMaszynyNr_pom()
        {
            nsAccess2DB.MaszynyVO VO;
            listBoxMaszyny.Items.Clear();

            _MaszynyBUS.select();

            while (!_MaszynyBUS.eof)
            {
                VO = _MaszynyBUS.VO;
                listBoxMaszyny.Items.Add(VO.Nazwa + " - " + _MaszynyBUS.VO.Nr_pom);
                _MaszynyBUS.skip();
            }
        }//WypełnijMaszynyNr_pom

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
            textBoxNr_GUM.Text = _MaszynyBUS.VO.Nr_GUM;
            comboBoxNr_pom.Text = _MaszynyBUS.VO.Nr_pom;
            comboBoxDzial.Text = _MaszynyBUS.VO.Dzial;
            textBoxNr_prot_BHP.Text = _MaszynyBUS.VO.Nr_prot_BHP;
            dateTimePickerData_ost_przegl.Text = _MaszynyBUS.VO.Data_ost_przegl;
            dateTimePickerData_kol_przegl.Text = _MaszynyBUS.VO.Data_kol_przegl;
            richTextBoxUwagi.Text = _MaszynyBUS.VO.Uwagi;
            comboBoxWykorzystanie.Text = _MaszynyBUS.VO.Wykorzystanie;
            comboBoxStan_techniczny.Text = _MaszynyBUS.VO.Stan_techniczny;
            comboBoxPriorytet.Text = _MaszynyBUS.VO.Priorytet;
            comboBoxPropozycja.Text = _MaszynyBUS.VO.Propozycja;
            textBoxPunktacja.Text = _MaszynyBUS.VO.Punktacja.ToString();

            WypelnijMaszynyNazwami();
        }//listBoxMaszyny_SelectedIndexChanged

        // // // // // //  // /list boxy z tabeli accessa
        /// <summary>
        /// wypełnia listbox pozycjami częstotliwości użycia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wypelnijCzestotliwosc()
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



        //                                // // // // ///   przyciski
        //przycisk Nowa czyści formularz
        private void ButtonNowa_Click(object sender, EventArgs e)
        {
            comboBoxKategoria.Text = string.Empty;
            textBoxNazwa.Text = string.Empty;
            textBoxTyp.Text = string.Empty;
            textBoxNr_inwentarzowy.Text = string.Empty;
            textBoxNr_fabryczny.Text = string.Empty;
            textBoxRok_produkcji.Text = string.Empty;
            textBoxProducent.Text = string.Empty;
            pictureBox1.Text = string.Empty;
            textBoxNr_GUM.Text = string.Empty;
            comboBoxNr_pom.Text = string.Empty;
            comboBoxDzial.Text = string.Empty;
            textBoxNr_prot_BHP.Text = string.Empty;
            dateTimePickerData_ost_przegl.Text = string.Empty;
            dateTimePickerData_kol_przegl.Text = string.Empty;
            richTextBoxUwagi.Text = string.Empty;
            comboBoxWykorzystanie.Text = string.Empty;
            comboBoxStan_techniczny.Text = string.Empty;
            comboBoxPriorytet.Text = string.Empty;
            comboBoxPropozycja.Text = string.Empty;
            textBoxPunktacja.Text = string.Empty;

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
            textBoxNr_GUM.Text = string.Empty;
            comboBoxNr_pom.Text = string.Empty;
            comboBoxDzial.Text = string.Empty;
            textBoxNr_prot_BHP.Text = string.Empty;
            //dateTimePickerData_ost_przegl.Text = string.Empty;
            //dateTimePickerData_kol_przegl.Text = string.Empty;
            richTextBoxUwagi.Text = string.Empty;
            comboBoxWykorzystanie.Text = string.Empty;
            comboBoxStan_techniczny.Text = string.Empty;
            comboBoxPriorytet.Text = string.Empty;
            comboBoxPropozycja.Text = string.Empty;
            textBoxPunktacja.Text = string.Empty;

            WypelnijMaszynyNazwami();
            listBoxMaszyny.SelectedIndex = idx;

        }//buttonAnuluj_Click


        private void buttonUsun_Click(object sender, EventArgs e)
        {
           // if (listBoxMaszyny.Tag != null)
            //{ _maszynyBUS
            //}

        }//buttonUsun_Click

        private void buttonZapisz_Click(object sender, EventArgs e)
        {
            int idx = listBoxMaszyny.SelectedIndex;
            
            nsAccess2DB.MaszynyVO maszynyVO = new nsAccess2DB.MaszynyVO();

            maszynyVO.Kategoria = comboBoxKategoria.Text;
            maszynyVO.Nazwa = textBoxNazwa.Text.Trim();
            maszynyVO.Typ = textBoxTyp.Text.Trim();
            maszynyVO.Nr_inwentarzowy = textBoxNr_inwentarzowy.Text.Trim();
            maszynyVO.Nr_fabryczny = textBoxNr_fabryczny.Text.Trim();
            maszynyVO.Rok_produkcji = textBoxRok_produkcji.Text.Trim();
            maszynyVO.Producent = textBoxProducent.Text.Trim();
            maszynyVO.Zdjecie1 = pictureBox1.Text;  /////                ???????????????? obrazek
            maszynyVO.Nr_GUM = textBoxNr_GUM.Text.Trim();
            maszynyVO.Nr_pom = comboBoxNr_pom.Text;
            maszynyVO.Dzial = comboBoxDzial.Text;
            maszynyVO.Nr_prot_BHP = textBoxNr_prot_BHP.Text;
            //maszynyVO.Data_ost_przegl = dateTimePickerData_ost_przegl.Text;
            //maszynyVO.Data_kol_przegl = dateTimePickerData_kol_przegl.Text;
            maszynyVO.Uwagi = richTextBoxUwagi.Text.Trim();
            maszynyVO.Wykorzystanie = comboBoxWykorzystanie.Text;
            maszynyVO.Stan_techniczny = comboBoxStan_techniczny.Text;
            maszynyVO.Priorytet = comboBoxPriorytet.Text;
            maszynyVO.Propozycja = comboBoxPropozycja.Text;
            //maszynyVO.Punktacja = int.Parse(textBoxPunktacja.Text);

            /*if (toolStripStatusLabelIDVal.Text == string.Empty)
            { maszynyVO.ID_maszyny = -1; }
            else
            {
                maszynyVO.ID_maszyny = int.Parse(toolStripStatusLabelIDVal.Text);
            }
           */
            _MaszynyBUS.write(maszynyVO);
                       
            WypelnijMaszynyNazwami();
          
            if (listBoxMaszyny.Items.Count > 0) {
                listBoxMaszyny.SelectedIndex = listBoxMaszyny.Items.Count - 1;
                            }


        }//buttonZapisz_Click

      
    }// public partial class SpisForm : Form
       
}//namespace RemaGUM
