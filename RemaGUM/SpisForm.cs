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
    public partial class SpisForm : Form
    {
        private string _connString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Projects\\RemaGUM\\RemaGUM.mdb"; //połaczenie z bazą danych

        private string _helpFile = Application.StartupPath + "\\pomoc.chm"; //plik pomocy RemaGUM.

        private byte[] _zawartoscPliku; //dane odczytane z pliku zdjęcia.

        string _dirNazwa = "C:\\tempRemaGUM"; //nazwa katalogu tymczasowego.
        string _dirPelnaNazwa = string.Empty; // katalog tymczasowy - pelna nazwa.

        enum _status { edycja, nowy };  //status działania formularza.
        private byte _statusForm; // wartośc statusu formularza.

        private int[] _maszynaTag; // przechowuje identyfikatory maszyn.
        private int[] _maszynaTagD; // przechowuje identyfikatory maszyn Dysponenta. 
        int _maszynaId = -1; // identyfikator maszyny.
        int _maszynaSzukajIdx = 0; // indeks szukanej maszyny.

        int _dysponentId = -1; // identyfikator dysponenta.
        int _dysponentSzukajIdx = 0; //indeks szukanego dysponenta.

        private ToolTip _tt; //podpowiedzi dla niektórych kontolek.

        private int _interwalPrzegladow = 365;    //w dniach = 1 rok


        private nsAccess2DB.OperatorBUS _OperatorBUS;
        private nsAccess2DB.MaszynyBUS _maszynyBUS;

        /// <summary>
        /// Konstruktor formularza.
        /// </summary>
        /// <param name="connStr">Połaczenie z bazą.</param>
        public SpisForm()
        {
            InitializeComponent();
            nsRest.Rest rest = new nsRest.Rest();
            _connString += rest.dbConnection(_connString);

            _statusForm = (int)_status.edycja;

            _OperatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            _maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);

            radioButtonNazwa.Checked = true; // przy starcie programu zaznaczone sortowanie po nazwie.
            WypelnijCzestotliwosc();
            WypelnijKategorie();
            WypelnijDzial();
            WypelnijPropozycje();
            WypelnijStan_techniczny();
            WypelnijOperatorow_maszyn(checkedListBoxOperatorzy_maszyn);// wypełniam operatorów maszyn na poczatku uruchomienia programu.
            WypelnijDysponent();

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }

            //------------------------------------ Zakładka Maszyny
            //dane formularza Maszyny
            listBoxMaszyny.TabIndex = 0;
            comboBoxKategoria.TabIndex = 1;
            textBoxNazwa.TabIndex = 2;
            textBoxTyp.TabIndex = 3;
            textBoxNr_inwentarzowy.TabIndex = 4;
            textBoxNr_fabryczny.TabIndex = 5;
            textBoxRok_produkcji.TabIndex = 6;
            textBoxProducent.TabIndex = 7;
            pictureBox1.TabIndex = 8;
            comboBoxDysponent.TabIndex = 9;
            checkedListBoxOperatorzy_maszyn.TabIndex = 10;
            textBoxNr_pom.TabIndex = 11;
            comboBoxDzial.TabIndex = 12;
            textBoxNr_prot_BHP.TabIndex = 13;
            dateTimePickerData_ost_przegl.TabIndex = 14;
            dateTimePickerData_kol_przegl.TabIndex = 15;
            richTextBoxUwagi.TabIndex = 16;
            //ankietka dot. Maszyn
            comboBoxWykorzystanie.TabIndex = 17;
            comboBoxStan_techniczny.TabIndex = 18;
            comboBoxPropozycja.TabIndex = 19;
            //przyciski zapisz/edytuj itp
            buttonNowa.TabIndex = 20;
            buttonZapisz.TabIndex = 21;
            buttonAnuluj.TabIndex = 22;
            buttonUsun.TabIndex = 23;
            //sortowanie Maszyn po radio buttonach
            radioButtonTyp.TabIndex = 24;
            radioButtonNr_inwentarzowy.TabIndex = 25;
            radioButtonNr_fabryczny.TabIndex = 26;
            radioButtonNr_pomieszczenia.TabIndex = 27;
            radioButtonNazwa.TabIndex = 28;
            radioButtonData_ost_przegl.TabIndex = 29;
            //wyszukiwanie Maszyny po wpisanej nazwie 
            textBoxWyszukiwanie.TabIndex = 31;
            buttonSzukaj.TabIndex = 32;

            // --------------------------------------------- Zakładka Materiały
            //dane formularza Materiały
            listBoxMaterialy.TabIndex = 33;
            textBoxTyp_materialu.TabIndex = 34;
            comboBoxRodzaj.TabIndex = 35;
            textBoxNazwa_materialu.TabIndex = 36;
            comboBoxJednostka_mat.TabIndex = 37;
            textBoxMagazyn_mat.TabIndex = 38;
            textBoxZuzycie.TabIndex = 39;
            textBoxOdpad.TabIndex = 40;
            textBoxMin_materialu.TabIndex = 41;
            textBoxZapotrzebowanie.TabIndex = 42;
            //dane dostawców Materiałów
            comboBoxDostawca1.TabIndex = 43;
            linkLabelDostawca1.TabIndex = 44;
            richTextBoxDostawca1.TabIndex = 45;
            comboBoxDostawca2.TabIndex = 46;
            linkLabelDostawca2.TabIndex = 47;
            richTextBoxDostawca2.TabIndex = 48;
            //przyciski zapisz/edytuj itp
            buttonNowa_mat.TabIndex = 49;
            buttonZapisz_mat.TabIndex = 50;
            buttonAnuluj_mat.TabIndex = 51;
            buttonUsun_mat.TabIndex = 52;
            //sortowanie Materiału po radio buttonach
            radioButtonNazwa_mat.TabIndex = 53;
            radioButtonTyp_mat.TabIndex = 54;
            radioButtonCena_mat.TabIndex = 55;
            radioButtonMagazyn_ilosc_mat.TabIndex = 56;
            //wyszukiwanie Materiału po wpisanej nazwie
            textBoxWyszukaj_mat.TabIndex = 57;
            buttonSzukaj_mat.TabIndex = 58;

            //------------------------------------------------ Zakładka Operator
            //dane forlumarza
            listBoxOperator.TabIndex = 1;
            textBoxImieOperator.TabIndex = 2;
            textBoxNazwiskoOperator.TabIndex = 3;
            comboBoxDzialOperator.TabIndex = 4;
            textBoxUprawnienieOperator.TabIndex = 5;
            dateTimePickerDataKoncaUprOp.TabIndex = 6;
            listBoxMaszynyOperatora.TabIndex = 7;
            //wyszukiwanie
            textBoxWyszukiwanieOperator.TabIndex = 8;
            buttonSzukajOperator.TabIndex = 9;
            //przyciski zapisz/edytuj itp
            buttonNowaOperator.TabIndex = 10;
            buttonZapiszOperator.TabIndex = 11;
            buttonAnulujOperator.TabIndex = 12;
            buttonUsunOperator.TabIndex = 13;
            //sortowanie po comboBoxOperator
            comboBoxOperator.TabIndex = 14;

            // ------------------------------------------- Zakładka Dysponent.
            listBoxDysponent.TabIndex = 1;
            textBoxImieDysponent.TabIndex = 2;
            textBoxNazwiskoDysponent.TabIndex = 3;
            comboBoxDzialDysponent.TabIndex = 4;
            richTextBoxDysponent_dane.TabIndex = 5;
            listBoxMaszynyDysponenta.TabIndex = 6;
            //wyszukiwanie
            textBoxWyszukiwanieDysponent.TabIndex = 7;
            buttonSzukajDysponent.TabIndex = 8;
            //przyciski zapisz/edytuj itp
            buttonNowaDysponent.TabIndex = 9;
            buttonZapiszDysponent.TabIndex = 10;
            buttonAnulujDysponent.TabIndex = 11;
            buttonUsunDysponent.TabIndex = 12;


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
            _tt.SetToolTip(comboBoxDysponent, "Osoba zarządzająca maszynami (dysponent maszyny).");
            _tt.SetToolTip(checkedListBoxOperatorzy_maszyn, "Główna osoba użytkująca maszynę (posiadająca odpowiednie uprawnienia).");
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
            _tt.SetToolTip(buttonUsun, "Usuwa pozycję z bazy.");
            _tt.SetToolTip(radioButtonTyp, "Sortuj po typie.");
            _tt.SetToolTip(radioButtonNr_inwentarzowy, "Sortuj po numerze inwentarzowym.");
            _tt.SetToolTip(radioButtonNr_fabryczny, "Sortuj po numerze fabrycznym.");
            _tt.SetToolTip(radioButtonNr_pomieszczenia, "Sortuj po numerze pomieszczenia.");
            _tt.SetToolTip(radioButtonNazwa, "Sortuj po nazwie maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(radioButtonData_ost_przegl, "Sortuj po dacie ostatniego przegladu.");
            _tt.SetToolTip(textBoxWyszukiwanie, "Wpisz jakiej maszyny szukasz.");
            _tt.SetToolTip(buttonSzukaj, "Szukanie w bazie maszyn.");
            // -------------------------------------- Zakładka Materiały
            _tt.SetToolTip(listBoxMaterialy, "Lista materiałów.");
            _tt.SetToolTip(textBoxTyp_materialu, "Typ materiału.");
            _tt.SetToolTip(comboBoxRodzaj, "Rodzaj materiału.");
            _tt.SetToolTip(textBoxNazwa_materialu, "Nazwa materiału.");
            _tt.SetToolTip(comboBoxJednostka_mat, "Jednostka miary.");
            _tt.SetToolTip(textBoxMagazyn_mat, "Stan magazynowy materiału.");
            _tt.SetToolTip(textBoxZuzycie, "Wpisz ile materiału podlega zużyciu.");
            _tt.SetToolTip(textBoxOdpad, "Wpisz ile materiału jest odpadem.");
            _tt.SetToolTip(textBoxMin_materialu, "Stan minimalny materiału.");
            _tt.SetToolTip(textBoxZapotrzebowanie, "Zapotrzebowanie materiału.");
            _tt.SetToolTip(comboBoxDostawca1, "Dane dostawcy głównego.");
            _tt.SetToolTip(linkLabelDostawca1, "link do strony dostawcy głównego.");
            _tt.SetToolTip(richTextBoxDostawca1, "Opis dostawcy głównego, dane kontaktowe, szczegóły dotyczące składania zamówienia (np. proponowane upusty cenowe).");
            _tt.SetToolTip(comboBoxDostawca2, "Dane dostawcy alternatywnego.");
            _tt.SetToolTip(linkLabelDostawca2, "link do strony dostawcy alternatywnego.");
            _tt.SetToolTip(richTextBoxDostawca2, "Opis dostawcy alternatywnego, dane kontaktowe, szczegóły dotyczące składania zamówienia (np. proponowane upusty cenowe).");
            _tt.SetToolTip(radioButtonNazwa_mat, "Sortuj po nazwie materiału.");
            _tt.SetToolTip(radioButtonTyp_mat, "Sortuj po typie materiału.");
            _tt.SetToolTip(radioButtonCena_mat, "Sortuj po cenie materiału.");
            _tt.SetToolTip(radioButtonMagazyn_ilosc_mat, "Sortuj po dostępnych ilościach w magazynie.");
            _tt.SetToolTip(buttonNowa_mat, "Nowa pozycja w bazie.");
            _tt.SetToolTip(buttonZapisz_mat, "Zapis nowej maszyny, przyrządu lub urządzenia lub edycja wybranej pozycji.");
            _tt.SetToolTip(buttonAnuluj_mat, "Anulowanie zmiany.");
            _tt.SetToolTip(buttonUsun_mat, "Usuwa pozycję z bazy.");
            _tt.SetToolTip(textBoxWyszukaj_mat, "Wpisz jakiego materiału szukasz.");
            _tt.SetToolTip(buttonSzukaj_mat, "Szukanie w bazie materiałów.");
            //---------------------------------------Zakładka operator
            _tt = new ToolTip();
            _tt.SetToolTip(listBoxOperator, "Lista wszystkich operatorów maszyn.");
            _tt.SetToolTip(textBoxImieOperator, "Imię i nazwisko operatora maszyny.");
            _tt.SetToolTip(comboBoxDzialOperator, "Nazwa działu, do którego należy operator maszyny.");
            _tt.SetToolTip(textBoxUprawnienieOperator, "Rodzaj uprawnienia, jakie posiada operator.");
            _tt.SetToolTip(dateTimePickerDataKoncaUprOp, "Data końca uprawnień operatora maszyny.");
            _tt.SetToolTip(listBoxMaszynyOperatora, "Lista obsługiwanych przez operatora maszyn.");
            _tt.SetToolTip(buttonNowaOperator, "Nowa pozycja w bazie.");
            _tt.SetToolTip(buttonZapiszOperator, "Zapis nowego operatora lub edycja wybranych pozycji.");
            _tt.SetToolTip(buttonAnulujOperator, "Anulowanie zmiany.");
            _tt.SetToolTip(buttonUsunOperator, "Usuwa pozycję z bazy.");
            _tt.SetToolTip(textBoxWyszukiwanieOperator, "Wpisz nazwisko operatora, którego szukasz.");
            _tt.SetToolTip(buttonSzukajOperator, "Szukanie w bazie operatora.");
            _tt.SetToolTip(comboBoxOperator, "Sortowanie operatorow po dacie końca uprawnień.");

        }//public SpisForm()

        private void pokazKomunikat(string tresc)
        {
            Frame frame = new Frame(tresc);
            frame.Show();
            frame.Refresh();
            Thread.Sleep(2000);
            frame.Close();
            frame.Dispose();
        }// pokazKomunikat


        /// <summary>
        /// Wyświetla komponenty w zależności od indeksu zakładki.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlZakladki_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl v = (TabControl)sender;
            //Cursor.Current = Cursors.WaitCursor;

            // ----------------------------------Zakładka Maszyny.
            if (v.SelectedIndex == 0)
            {
                OdswiezListeMaszyn();
                listBoxMaszyny.SelectedIndex = _maszynyBUS.getIdx(_maszynaId);

                WypelnijCzestotliwosc();
                WypelnijKategorie();
                WypelnijDzial();
                WypelnijPropozycje();
                WypelnijStan_techniczny();
                WypelnijOperatorow_maszyn(checkedListBoxOperatorzy_maszyn);// wypełniam operatorów maszyn na poczatku uruchomienia programu.
                WypelnijDysponent();

                if (listBoxMaszyny.Items.Count > 0)
                {
                    listBoxMaszyny.SelectedIndex = 0;
                }
            }// Zakładka Maszyny.

            // --------------------------------- Zakładka Materialy.
            if (v.SelectedIndex == 1)
            {
                WypelnijMaterialyNazwami();
                WypelnijJednostka_miar();
                WypelnijRodzaj_mat();

                if (listBoxMaterialy.Items.Count > 0)
                {
                    listBoxMaterialy.SelectedIndex = 0;
                }
            }// Zakładka Materiały

            // ----------------------------------Zakładka Normalia.
            if (v.SelectedIndex == 2)
            {

            }// Zakładka Normalia

            // --------------------------------- Zakładka operator.
            if (v.SelectedIndex == 3)
            {
                comboBoxOperator.SelectedIndex = 0;//ustawia sortowanie po nazwisku operatora
                WypelnijOperatorowDanymi();
                WypelnijDzialOperatora();

                if (listBoxOperator.Items.Count > 0)
                {
                    listBoxOperator.SelectedIndex = 0;
                }


            }// Zakładka operator

            // --------------------------------Zakładka dysponent.
            if (v.SelectedIndex == 4)
            {
                WypelnijDysponentowDanymi();
                WypelnijDysponentowMaszynami();

                WypelnijDzialDysponenta();

                if (listBoxDysponent.Items.Count > 0)
                {
                    listBoxDysponent.SelectedIndex = 0;
                }
            }

            Cursor.Current = Cursors.Default;
        } //tabControl1_SelectedIndexChanged


        //  //  //  //  //  //  //  //  //  //  //  //  //-------------------------wyświetlanie w zakładce Maszyny.

        /// <summary>
        /// Czyści dane z formularza maszyny.
        /// </summary>
        private void CzyscDaneMaszyny()
        {
            toolStripStatusLabelID_Maszyny.Text = "";
            try
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

                linkLabelNazwaZdjecia.Text = string.Empty;
                pictureBox1.Image = null;
                _zawartoscPliku = new byte[] { };

                comboBoxDysponent.SelectedIndex = -1;
                comboBoxDysponent.Enabled = true;
                comboBoxDysponent.SelectedIndex = 0;
                comboBoxDysponent.Refresh();

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

                //czyści checkedListBoxOperatorzy_maszyn
                for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++) //czyści checkboxy operatorzy maszyny
                {
                    checkedListBoxOperatorzy_maszyn.SetItemChecked(i, false);
                }
            }
            catch { }

        }// CzyscDaneMaszyny()

        /// <summary>
        /// zmiana indeksu w list box maszyny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxMaszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);

            //listBoxMaszyny.Items.Clear();

            // uaktualnienie danych maszyny po zmianie sposobu sortowania
            if (radioButtonNazwa.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nazwa ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
            }
            else if (radioButtonTyp.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Typ ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
            }
            else if (radioButtonNr_fabryczny.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_fabryczny ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
            }
            else if (radioButtonNr_inwentarzowy.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_inwentarzowy ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
            }
            else if (radioButtonNr_pomieszczenia.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_pomieszczenia ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
            }
            else if (radioButtonData_ost_przegl.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Data_ost_przegl ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
            }

            try
            {
                toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString(); // toolStripStatusLabelID_Maszyny
                listBoxMaszyny.Tag = maszynyBUS.VO.Identyfikator;
                comboBoxKategoria.Text = maszynyBUS.VO.Kategoria;

                textBoxNazwa.Text = maszynyBUS.VO.Nazwa;
                textBoxTyp.Text = maszynyBUS.VO.Typ;
                textBoxNr_inwentarzowy.Text = maszynyBUS.VO.Nr_inwentarzowy;
                textBoxNr_fabryczny.Text = maszynyBUS.VO.Nr_fabryczny;
                textBoxRok_produkcji.Text = maszynyBUS.VO.Rok_produkcji;
                textBoxProducent.Text = maszynyBUS.VO.Producent;

                linkLabelNazwaZdjecia.Text = maszynyBUS.VO.Zdjecie;// zdjęcie nazwa
                if (File.Exists(maszynyBUS.VO.Zdjecie))
                {
                    File.Delete(maszynyBUS.VO.Zdjecie);
                }
                _zawartoscPliku = maszynyBUS.VO.Zawartosc_pliku; //zawartość zdjęcia
                pokazZdjecie(linkLabelNazwaZdjecia.Text); // zmiana zdjęcia przy zmianie indeksu maszyny.

                comboBoxDysponent.Text = maszynyBUS.VO.Nazwa_dysponent;  // wypełnia dysponenta

                textBoxNr_pom.Text = maszynyBUS.VO.Nr_pom;
                comboBoxDzial.Text = maszynyBUS.VO.Dzial;
                textBoxNr_prot_BHP.Text = maszynyBUS.VO.Nr_prot_BHP;

                dateTimePickerData_ost_przegl.Value = new DateTime(maszynyBUS.VO.Rok_ost_przeg, maszynyBUS.VO.Mc_ost_przeg, maszynyBUS.VO.Dz_ost_przeg);
                dateTimePickerData_kol_przegl.Value = new DateTime(maszynyBUS.VO.Rok_kol_przeg, maszynyBUS.VO.Mc_kol_przeg, maszynyBUS.VO.Dz_kol_przeg);
                
                // komunikat o zbliżającym się terminie przeglądu maszyny lub o upłynięciu terminu przeglądu.
                DateTime currentDate = DateTime.Now;
                long termin = dateTimePickerData_kol_przegl.Value.Ticks - currentDate.Ticks;
                TimeSpan timeSpan = new TimeSpan(termin);
                               
                if ((timeSpan.Days >= 1) && (timeSpan.Days <= 7))
                {
                    MessageBox.Show(("Uwaga w dniu " + maszynyBUS.VO.Dz_kol_przeg.ToString("00") + "." + maszynyBUS.VO.Mc_kol_przeg.ToString("00") + "." + maszynyBUS.VO.Rok_kol_przeg.ToString() + " mija termin przeglądu dla maszyny " + maszynyBUS.VO.Nazwa.ToString()), "remagum", MessageBoxButtons.OK);
                }
                
                else if (timeSpan.Days < 0)
                {
                    MessageBox.Show(("Termin przeglądu maszyny "+ maszynyBUS.VO.Nazwa.ToString() + " minął."),"RemaGUM",MessageBoxButtons.OK);
                }
                else { }

                richTextBoxUwagi.Text = maszynyBUS.VO.Uwagi;
                comboBoxWykorzystanie.Text = maszynyBUS.VO.Wykorzystanie;
                comboBoxStan_techniczny.Text = maszynyBUS.VO.Stan_techniczny;
                comboBoxPropozycja.Text = maszynyBUS.VO.Propozycja;

                // wypełnia operatorów maszyny w polu checkedListBoxOperatorzy_maszyn.    *****************************
                nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
                nsAccess2DB.Maszyny_OperatorBUS maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);

                operatorBUS.select();
                maszyny_OperatorBUS.select((int)listBoxMaszyny.Tag);

                for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++)
                {
                    checkedListBoxOperatorzy_maszyn.SetItemChecked(i, false);
                }

                int idx = -1; // int idx = listBoxMaszyny.SelectedIndex;

                while (!maszyny_OperatorBUS.eof)
                {
                    idx = operatorBUS.getIdx(maszyny_OperatorBUS.VO.ID_op_maszyny);
                    if (idx > -1) checkedListBoxOperatorzy_maszyn.SetItemChecked(idx, true);
                    maszyny_OperatorBUS.skip();
                }

            }
            catch { }
        }//listBoxMaszyny_SelectedIndexChanged

        /// <summary>
        /// Odświeża listę maszyn w listBoxMaszyny
        /// </summary>
        private void OdswiezListeMaszyn()
        {
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            listBoxMaszyny.Items.Clear();
            maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nazwa ASC;");


            while (!maszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(maszynyBUS.VO.Nazwa + " -> " + maszynyBUS.VO.Nr_fabryczny);
                maszynyBUS.skip();
            }

        }// OdswiezListeMaszyn()

        //************* wypełnia CheckedListBox nazwiskami i imionami operatorów maszyn.
        private void WypelnijOperatorow_maszyn(CheckedListBox v)
        {
            nsAccess2DB.Maszyny_OperatorBUS maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);

            v.Items.Clear();
            operatorBUS.select();
            // maszyny_OperatorBUS.select();

            while (!operatorBUS.eof)
            {
                v.Items.Add(operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                operatorBUS.skip();
            }

            if (v.Items.Count > 0)
            {
                operatorBUS.idx = 0;
                v.SelectedIndex = 0;
                v.Tag = operatorBUS.VO.Identyfikator;
            }
        } // WypelnijOperatorow_maszyn(CheckedListBox v)

        // // // // // //  // /list boxy z tabeli accessa

        /// <summary>
        /// wypełnia listbox kategoriami maszyn
        /// </summary>
        private void WypelnijKategorie()
        {
            nsAccess2DB.KategoriaBUS kategoriaBUS = new nsAccess2DB.KategoriaBUS(_connString);
            nsAccess2DB.KategoriaVO kategoriaVO;
            comboBoxKategoria.Items.Clear();

            kategoriaBUS.select();
            kategoriaBUS.top();
            while (!kategoriaBUS.eof)
            {
                kategoriaVO = kategoriaBUS.VO;
                comboBoxKategoria.Items.Add(kategoriaVO.NazwaKategoria);
                kategoriaBUS.skip();
            }
        }//WypelnijKategorie

        private void comboBoxKategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.KategoriaBUS kategoriaBUS = new nsAccess2DB.KategoriaBUS(_connString);
            kategoriaBUS.idx = comboBoxKategoria.SelectedIndex;
        }//comboBoxKategoria_SelectedIndexChanged

        /// <summary>
        /// Wypełnia danymi Dysponentów w zakładce Maszyny.
        /// </summary>
        private void WypelnijDysponent()
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            nsAccess2DB.DysponentVO dysponentVO;
            comboBoxDysponent.Items.Clear();

            dysponentBUS.select();
            dysponentBUS.top();
            while (!dysponentBUS.eof)
            {
                dysponentVO = dysponentBUS.VO;
                comboBoxDysponent.Items.Add(dysponentVO.Dysp_nazwisko + " " + dysponentVO.Dysp_imie);
                dysponentBUS.skip();
            }
        }//WypełnijDysponent()

        private void comboBox_Dysponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            dysponentBUS.idx = comboBoxDysponent.SelectedIndex;
        }//comboBox_Dysponent_SelectedIndexChanged()

        //////////////////////////////////////////////////////////////         Rabio buttony
        private void radioButtonNazwa_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNazwa.Checked)
            {
                listBoxMaszyny.Items.Clear();
                CzyscDaneMaszyny();

                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nazwa ASC;");
                while (!maszynyBUS.eof)
                {
                    listBoxMaszyny.Items.Add(maszynyBUS.VO.Nazwa + " -> " + maszynyBUS.VO.Nr_fabryczny);
                    maszynyBUS.skip();
                }
            }
        }//radioButtonNazwa_CheckedChanged

        private void radioButton_Nr_Inwentarzowy_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNr_inwentarzowy.Checked)
            {
                listBoxMaszyny.Items.Clear();
                CzyscDaneMaszyny();

                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_inwentarzowy ASC;");
                while (!maszynyBUS.eof)
                {
                    listBoxMaszyny.Items.Add(maszynyBUS.VO.Nr_inwentarzowy + " -> " + maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }
            }
        }//radioButton_Nr_Inwentarzowy_CheckedChanged

        private void radioButton_Typ_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTyp.Checked)
            {
                listBoxMaszyny.Items.Clear();
                CzyscDaneMaszyny();

                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Typ ASC;");
                while (!maszynyBUS.eof)
                {
                    listBoxMaszyny.Items.Add(maszynyBUS.VO.Typ + " -> " + maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }

            }
        }//radioButton_Typ_CheckedChanged
        private void radioButtonNr_fabrycznyCheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNr_fabryczny.Checked)
            {
                listBoxMaszyny.Items.Clear();
                CzyscDaneMaszyny();

                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_fabryczny ASC;");
                while (!maszynyBUS.eof)
                {
                    listBoxMaszyny.Items.Add(maszynyBUS.VO.Nr_fabryczny + " -> " + maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }

            }
        }//radioButtonNr_fabrycznyCheckedChanged

        private void radioButton_Nr_Pomieszczenia_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNr_pomieszczenia.Checked)
            {
                listBoxMaszyny.Items.Clear();
                CzyscDaneMaszyny();

                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_pom ASC;");
                while (!maszynyBUS.eof)
                {
                    listBoxMaszyny.Items.Add(maszynyBUS.VO.Nr_pom + " -> " + maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }

            }
        }//radioButton_Nr_Pomieszczenia_CheckedChanged

        private void radioButtonData_ost_przegladu_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonData_ost_przegl.Checked)
            {
                listBoxMaszyny.Items.Clear();
                CzyscDaneMaszyny();

                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Data_ost_przegl ASC;");
                while (!maszynyBUS.eof)
                {
                    listBoxMaszyny.Items.Add(maszynyBUS.VO.Dz_ost_przeg + "-" + maszynyBUS.VO.Mc_ost_przeg + "-" + maszynyBUS.VO.Rok_ost_przeg + " r. -> " + maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }

            }
        }//radioButton_Nr_Pomieszczenia_CheckedChanged
        
        
        /// <summary>
        /// wyszukuje maszynę po wpisaniu dowolnego ciągu wyrazów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void buttonSzukaj_Click(object sender, EventArgs e)
        {
            radioButtonNazwa.Checked = true;

            textBoxWyszukiwanie.Text = textBoxWyszukiwanie.Text.Trim();

            if (textBoxWyszukiwanie.Text == string.Empty)
            {
                pokazKomunikat("Proszę wpisać tekst do pola wyszukiwania. Szukanie anulowane.");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);

            // maszynyBUS.select();
            maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nazwa ASC;");

            nsAccess2DB.MaszynyVO maszynyVO;
            maszynyVO = maszynyBUS.VO;

            string s1 = textBoxWyszukiwanie.Text.ToUpper();
            string s2;



            for (int i = _maszynaSzukajIdx; i < maszynyBUS.count; i++)
            {
                maszynyBUS.idx = i;
                maszynyVO = maszynyBUS.VO;

                s2 = maszynyVO.Nazwa.ToUpper();
                if (s2.Contains(s1))
                {
                    _maszynaSzukajIdx = i;
                    listBoxMaszyny.SelectedIndex = i;

                    if (MessageBox.Show("czy szukać dalej ?", "RemaGUM", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    {
                        goto cancel;
                    }
                }

            }
            pokazKomunikat("Aby szukać od poczatku wciśnij szukaj.");

            cancel:;

            _maszynaSzukajIdx = 0;
            listBoxMaszyny.ForeColor = Color.Black;

            _maszynaSzukajIdx = _maszynyBUS.count;

            //pokazKomunikat("nie znaleziono tekstu - powtórz szukanie");
            Cursor.Current = Cursors.Default;

        }//buttonSzukaj_Click

        /// <summary>
        /// wypełnia listbox Działami w których znajdują się maszyny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WypelnijDzial()
        {
            nsAccess2DB.DzialBUS dzialBUS = new nsAccess2DB.DzialBUS(_connString);
            nsAccess2DB.DzialVO VO;
            comboBoxDzial.Items.Clear();

            dzialBUS.select();
            dzialBUS.top();
            while (!dzialBUS.eof)
            {
                VO = dzialBUS.VO;
                comboBoxDzial.Items.Add(VO.Nazwa);
                dzialBUS.skip();
            }
        }//WypelnijDzial

        private void comboBoxDzial_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.DzialBUS dzialBUS = new nsAccess2DB.DzialBUS(_connString);
            dzialBUS.idx = comboBoxDzial.SelectedIndex;
            comboBoxDzial.Tag = dzialBUS.VO.Nazwa;
        }//comboBoxDzial_SelectedIndexChanged

        /// <summary>
        /// wypełnia listbox pozycjami częstotliwości Wykorzystania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WypelnijCzestotliwosc()
        {
            nsAccess2DB.WykorzystanieBUS wykorzystanieBUS = new nsAccess2DB.WykorzystanieBUS(_connString);
            nsAccess2DB.WykorzystanieVO VO;
            comboBoxWykorzystanie.Items.Clear();

            wykorzystanieBUS.select();
            wykorzystanieBUS.top();
            while (!wykorzystanieBUS.eof)
            {
                VO = wykorzystanieBUS.VO;
                comboBoxWykorzystanie.Items.Add(VO.Wykorzystanie);
                wykorzystanieBUS.skip();
            }
        }// wypelnijCzestotliwosc

        // przypisuje w combo identyfikator = nazwę częstotliwości wykorzystania
        private void ComboBoxWykorzystanie_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.WykorzystanieBUS wykorzystanieBUS = new nsAccess2DB.WykorzystanieBUS(_connString);
            wykorzystanieBUS.idx = comboBoxWykorzystanie.SelectedIndex;
            comboBoxWykorzystanie.Tag = wykorzystanieBUS.VO.Wykorzystanie;
        }//comboboxWykorzystanie_SelectedIndexChanged

        /// <summary>
        /// wypełnia listbox propozycjami co zrobić z maszyną (zachować/złomować itp)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WypelnijPropozycje()
        {
            nsAccess2DB.PropozycjaBUS propozycjaBUS = new nsAccess2DB.PropozycjaBUS(_connString);
            nsAccess2DB.PropozycjaVO VO;
            comboBoxPropozycja.Items.Clear();

            propozycjaBUS.select();
            propozycjaBUS.top();
            while (!propozycjaBUS.eof)
            {
                VO = propozycjaBUS.VO;
                comboBoxPropozycja.Items.Add(VO.Nazwa);
                propozycjaBUS.skip();
            }
        }// WypelnijPropozycje()
        private void comboBoxPropozycja_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.PropozycjaBUS propozycjaBUS = new nsAccess2DB.PropozycjaBUS(_connString);
            propozycjaBUS.idx = comboBoxPropozycja.SelectedIndex;
        }// comboBoxPropozycja_SelectedIndexChanged

        private void WypelnijStan_techniczny()
        {
            nsAccess2DB.Stan_technicznyBUS stan_TechnicznyBUS = new nsAccess2DB.Stan_technicznyBUS(_connString);
            nsAccess2DB.Stan_technicznyVO VO;
            comboBoxStan_techniczny.Items.Clear();

            stan_TechnicznyBUS.select();
            stan_TechnicznyBUS.top();
            while (!stan_TechnicznyBUS.eof)
            {
                VO = stan_TechnicznyBUS.VO;
                comboBoxStan_techniczny.Items.Add(VO.Nazwa);
                stan_TechnicznyBUS.skip();
            }
        }// WypelnijStan_techniczny()
        private void comboBoxStan_techniczny_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.Stan_technicznyBUS stan_TechnicznyBUS = new nsAccess2DB.Stan_technicznyBUS(_connString);
            stan_TechnicznyBUS.idx = comboBoxStan_techniczny.SelectedIndex;
        }// comboBoxStan_techniczny_SelectedIndexChanged

        //przycisk Nowa czyści formularz
        private void ButtonNowa_Click(object sender, EventArgs e)
        {

            CzyscDaneMaszyny();

            buttonNowa.Enabled = false;
            buttonZapisz.Enabled = true;
            buttonAnuluj.Enabled = true;
            buttonUsun.Enabled = false;

            _statusForm = (int)_status.nowy;

        }//ButtonNowa_Click

        private void buttonAnuluj_Click(object sender, EventArgs e)
        {
            CzyscDaneMaszyny();

            buttonNowa.Enabled = true;
            buttonZapisz.Enabled = true;
            buttonAnuluj.Enabled = true;
            buttonUsun.Enabled = false;

            _statusForm = (int)_status.edycja;

        }//buttonAnuluj_Click

        private void buttonUsun_Click(object sender, EventArgs e)
        {
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            nsAccess2DB.Maszyny_OperatorBUS maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            nsAccess2DB.Maszyny_DysponentBUS maszyny_DysponentBUS = new nsAccess2DB.Maszyny_DysponentBUS(_connString);

            try
            {
                comboBoxKategoria.Text = string.Empty;
                textBoxNazwa.Text = string.Empty;
                textBoxTyp.Text = string.Empty;
                textBoxNr_inwentarzowy.Text = string.Empty;
                textBoxNr_fabryczny.Text = string.Empty;
                textBoxRok_produkcji.Text = string.Empty;
                textBoxProducent.Text = string.Empty;

                pictureBox1.Text = string.Empty;
                _zawartoscPliku = new byte[] { }; //Czyści pictureBox1

                comboBoxDysponent.Text = string.Empty;
                textBoxNr_pom.Text = string.Empty;
                comboBoxDzial.Text = string.Empty;
                textBoxNr_prot_BHP.Text = string.Empty;
                dateTimePickerData_ost_przegl.Text = string.Empty;
                dateTimePickerData_kol_przegl.Text = string.Empty;
                richTextBoxUwagi.Text = string.Empty;
                comboBoxWykorzystanie.Text = string.Empty;
                comboBoxStan_techniczny.Text = string.Empty;
                comboBoxPropozycja.Text = string.Empty;
                linkLabelNazwaZdjecia.Text = string.Empty;

            }
            catch { }

            maszynyBUS.delete((int)listBoxMaszyny.Tag);// usunięcie z tabeli maszyna
            maszyny_OperatorBUS.delete((int)listBoxMaszyny.Tag); // usunięcie z tabeli relacji maszyna operator.

            // po usunięciu maszyny odświeża danę z listy maszyn przez ponowne zaznaczenie sortowania po nazwie.
            radioButtonNr_fabryczny.Checked = true;
            radioButtonNazwa.Checked = true;

            WypelnijOperatorow_maszyn(checkedListBoxOperatorzy_maszyn);
            _statusForm = (int)_status.edycja;
        }//buttonUsun_Click



        /// <summary>
        /// Działania po wciśnięciu przycisku Zapisz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonZapisz_Click(object sender, EventArgs e)
        {

            if (textBoxNazwa.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij nazwę maszyny.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            nsAccess2DB.Maszyny_OperatorBUS maszyny_operatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
           
            nsAccess2DB.MaszynyVO maszynyVO = new nsAccess2DB.MaszynyVO();
            nsAccess2DB.OperatorVO operatorVO = new nsAccess2DB.OperatorVO();
            nsAccess2DB.Maszyny_OperatorVO operator_maszynyVO = new nsAccess2DB.Maszyny_OperatorVO();
            
            if (_zawartoscPliku == null) _zawartoscPliku = new byte[] { }; // zapisz nowy plik zdjęcia podczas edycji maszyny.
            if (_statusForm == (int)_status.nowy)
            {
                //maszynyBUS.write(maszynyVO);
                maszynyBUS.select();
                maszynyBUS.idx = maszynyBUS.count - 1;
                maszynyVO.Identyfikator = maszynyBUS.VO.Identyfikator + 1;

                maszynyVO.Kategoria = comboBoxKategoria.Text;
                maszynyVO.Nazwa = textBoxNazwa.Text.Trim();
                maszynyVO.Typ = textBoxTyp.Text.Trim();
                maszynyVO.Nr_inwentarzowy = textBoxNr_inwentarzowy.Text.Trim();
                maszynyVO.Nr_fabryczny = textBoxNr_fabryczny.Text.Trim();
                maszynyVO.Rok_produkcji = textBoxRok_produkcji.Text.Trim();
                maszynyVO.Producent = textBoxProducent.Text.Trim();

                maszynyVO.Zdjecie = linkLabelNazwaZdjecia.Text;  //zdjęcie nazwa
                maszynyVO.Zawartosc_pliku = _zawartoscPliku;//zdjęcie zawartość

                maszynyVO.Nr_pom = textBoxNr_pom.Text;
                maszynyVO.Dzial = comboBoxDzial.Text;
                maszynyVO.Nr_prot_BHP = textBoxNr_prot_BHP.Text;

                
                maszynyVO.Rok_ost_przeg = dateTimePickerData_ost_przegl.Value.Year;
                maszynyVO.Mc_ost_przeg = dateTimePickerData_ost_przegl.Value.Month;
                maszynyVO.Dz_ost_przeg = dateTimePickerData_ost_przegl.Value.Day;
                maszynyVO.Data_ost_przegl = int.Parse(maszynyVO.Rok_ost_przeg.ToString() + maszynyVO.Mc_ost_przeg.ToString("00") + maszynyVO.Dz_ost_przeg.ToString("00"));

                DateTime dt = new DateTime(dateTimePickerData_ost_przegl.Value.Ticks); // dodaje interwał przeglądów do data_ost_przegl
                dt = dt.AddDays(_interwalPrzegladow);
                  
                maszynyVO.Rok_kol_przeg = dt.Year;
                maszynyVO.Mc_kol_przeg = dt.Month;
                maszynyVO.Dz_kol_przeg = dt.Day;
                maszynyVO.Data_kol_przegl = int.Parse(dt.Year.ToString() + dt.Month.ToString("00") + dt.Day.ToString("00"));

                maszynyVO.Uwagi = richTextBoxUwagi.Text.Trim();
                maszynyVO.Wykorzystanie = comboBoxWykorzystanie.Text;
                maszynyVO.Stan_techniczny = comboBoxStan_techniczny.Text;
                maszynyVO.Propozycja = comboBoxPropozycja.Text;

                // zpis dysponenta w tabeli maszyny i w tabeli maszyny_dysponenta
                maszynyVO.Nazwa_dysponent = comboBoxDysponent.Text.Trim();// Zapis dysponenta maszyny w zakładce maszyny, przy utworzeniu nowej maszyny.
                
                listBoxMaszyny.SelectedIndex = maszynyBUS.getIdx(maszynyBUS.VO.Identyfikator);// ustawienie zaznaczenia w tabeli maszyn.
                                
                maszynyBUS.write(maszynyVO);
                
                // Zapis operatorów/operatora maszyny przypisanych do maszyny.
                operatorBUS.select();
                for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++)
                {
                    if (checkedListBoxOperatorzy_maszyn.GetItemChecked(i))
                    {
                        operatorBUS.idx = i;
                        maszyny_operatorBUS.insert(maszynyVO.Identyfikator, operatorBUS.VO.Identyfikator, maszynyBUS.VO.Nazwa);
                    }
                }
            listBoxMaszyny.SelectedIndex = maszynyBUS.getIdx(maszynyBUS.VO.Identyfikator);// ustawienie zaznaczenia w tabeli maszyn.
            }//if - nowy

            else if (_statusForm == (int)_status.edycja)
            {
                maszynyBUS.select((int)listBoxMaszyny.Tag);
                if (maszynyBUS.count < 1)
                {
                    MessageBox.Show("Maszyna o identyfikatorze " + listBoxMaszyny.Tag.ToString() + "nie istnieje w bazie maszyn", "RemaGUM",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    maszynyVO.Identyfikator = (int)listBoxMaszyny.Tag;
                    maszynyVO.Kategoria = comboBoxKategoria.Text;
                    maszynyVO.Nazwa = textBoxNazwa.Text.Trim();
                    maszynyVO.Typ = textBoxTyp.Text.Trim();
                    maszynyVO.Nr_inwentarzowy = textBoxNr_inwentarzowy.Text.Trim();
                    maszynyVO.Nr_fabryczny = textBoxNr_fabryczny.Text.Trim();
                    maszynyVO.Rok_produkcji = textBoxRok_produkcji.Text.Trim();
                    maszynyVO.Producent = textBoxProducent.Text.Trim();

                    maszynyVO.Zdjecie = linkLabelNazwaZdjecia.Text;  //zdjęcie nazwa
                    maszynyVO.Zawartosc_pliku = _zawartoscPliku;//zdjęcie zawartość

                    maszynyVO.Nazwa_dysponent = comboBoxDysponent.Text.Trim();

                    maszynyVO.Nr_pom = textBoxNr_pom.Text;
                    maszynyVO.Dzial = comboBoxDzial.Text;
                    maszynyVO.Nr_prot_BHP = textBoxNr_prot_BHP.Text;
                    maszynyVO.Rok_ost_przeg = dateTimePickerData_ost_przegl.Value.Year;
                    maszynyVO.Mc_ost_przeg = dateTimePickerData_ost_przegl.Value.Month;
                    maszynyVO.Dz_ost_przeg = dateTimePickerData_ost_przegl.Value.Day;
                    maszynyVO.Data_ost_przegl = int.Parse(maszynyVO.Rok_ost_przeg.ToString() + maszynyVO.Mc_ost_przeg.ToString("00") + maszynyVO.Dz_ost_przeg.ToString("00"));
                    DateTime dt = new DateTime(dateTimePickerData_ost_przegl.Value.Ticks); // dodaje interwał przeglądów do data_ost_przegl
                    dt = dt.AddDays(_interwalPrzegladow);
                    maszynyVO.Rok_kol_przeg = dt.Year;
                    maszynyVO.Mc_kol_przeg = dt.Month;
                    maszynyVO.Dz_kol_przeg = dt.Day;
                    maszynyVO.Data_kol_przegl = int.Parse(dt.Year.ToString() + dt.Month.ToString("00") + dt.Day.ToString("00"));
                    maszynyVO.Uwagi = richTextBoxUwagi.Text.Trim();
                    maszynyVO.Wykorzystanie = comboBoxWykorzystanie.Text;
                    maszynyVO.Stan_techniczny = comboBoxStan_techniczny.Text;
                    maszynyVO.Propozycja = comboBoxPropozycja.Text;

                    maszynyBUS.write(maszynyVO);

                    maszyny_operatorBUS.delete(maszynyBUS.VO.Identyfikator);
                    maszyny_operatorBUS.select(maszynyBUS.VO.Identyfikator);
                    
                    // Zapis operatorów/operatora maszyny przypisanych do maszyny.
                    operatorBUS.select();
                    for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++)
                    {
                        if (checkedListBoxOperatorzy_maszyn.GetItemChecked(i))
                        {
                            operatorBUS.idx = i;
                            maszyny_operatorBUS.insert(maszynyVO.Identyfikator, operatorBUS.VO.Identyfikator, maszynyBUS.VO.Nazwa);
                        }
                    }
                }
            listBoxMaszyny.SelectedIndex = maszynyBUS.getIdx(maszynyBUS.VO.Identyfikator);// ustawienie zaznaczenia w tabeli maszyn.
            }//else if - edycja

            WypelnijOperatorow_maszyn(checkedListBoxOperatorzy_maszyn);

            buttonNowa.Enabled = true;
            buttonZapisz.Enabled = true;
            buttonAnuluj.Enabled = true;
            buttonUsun.Enabled = true;

            OdswiezListeMaszyn();// po utworzeniu nowej maszyny odświeża danę z listy maszyn przez zaznaczenie sortowania po nazwie.
            pokazKomunikat("Pozycja zapisana w bazie");
            _statusForm = (int)_status.edycja;
        }//buttonZapisz_Click

        private void toolStripButtonOdswiez_Click(object sender, EventArgs e)
        {
            OdswiezListeMaszyn();
            WypelnijOperatorowDanymi();
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

         /// <summary>
        /// Pokazuje zdjęcie po kliknięciu na link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelNazwaZdjecia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pokazZdjecie(linkLabelNazwaZdjecia.Text);
        } //linkLabelNazwaZdjecia_LinkClicked

        private void pokazZdjecie(string Zdjecie)   
        {
            try
            {
                if (!Directory.Exists(_dirNazwa))
                {
                    Directory.CreateDirectory(_dirNazwa);
                }
                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
                //maszynyBUS.select();
         
                if (radioButtonNazwa.Checked)
                {
                    maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nazwa ASC;");
                    //while (!maszynyBUS.eof)
                    //{
                    //    listBoxMaszyny.Items.Add(maszynyBUS.VO.Nazwa);
                    //    maszynyBUS.skip();
                    //}

                    maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                    toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
                }

                else if (radioButtonTyp.Checked)
                {
                    maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Typ ASC;");
                    maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                    toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
                }
                else if (radioButtonNr_fabryczny.Checked)
                {
                    maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_fabryczny ASC;");
                    maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                    toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
                }
                else if (radioButtonNr_inwentarzowy.Checked)
                {
                    maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_inwentarzowy ASC;");
                    maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                    toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
                }
                else if (radioButtonNr_pomieszczenia.Checked)
                {
                    maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_pomieszczenia ASC;");
                    maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                    toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
                }
                else if (radioButtonData_ost_przegl.Checked)
                {
                    maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Data_ost_przegl ASC;");
                    maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                    toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();
                }
                

                //Uaktualnia ID i dane maszyny po Sortowaniu();
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
                listBoxMaszyny.Tag = maszynyBUS.VO.Identyfikator;

                nsDocInDb.docInDb docInDb = new nsDocInDb.docInDb();
                docInDb.dirNazwa = _dirNazwa;
                docInDb.zdjecieNazwa = maszynyBUS.VO.Zdjecie;
                docInDb.zapiszNaNaped(maszynyBUS.VO.Zawartosc_pliku);
                string zdjecie = _dirNazwa + "\\" + maszynyBUS.VO.Zdjecie;
                if (File.Exists(zdjecie))
                {
                    Bitmap bmp = (Bitmap)Bitmap.FromFile(zdjecie);

                    pictureBox1.Image = Bitmap.FromFile(zdjecie);
                    return;
                }

                //Czyszczenie pictureBox1 przy zmianie indeksu wraz z komunikatem o braku zdjęcia.
                if (maszynyBUS.VO.Zdjecie == string.Empty)
                {
                    pictureBox1.Image = null;
                    // MessageBox.Show("Maszyna nie posiada zdjęcia w bazie danych.", "komunikat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //pokazKomunikat("Wybrana maszyna nie posiada zdjęcia w bazie danych."); // wyskakujący komunikat.
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Problem z prezentacją zdjęcia. Błąd: " + ex.Message, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
        }//pokazZdjecie

        /// <summary>
        /// button Wgraj/pokaż
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPokazZdj_Click(object sender, EventArgs e)
        {
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "wybierz *.* plik";
            ofd.Filter = "pliki (*.*)|*.*|wszystkie pliki (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate, FileAccess.Read);
                    _zawartoscPliku = new byte[fs.Length];
                    fs.Read(_zawartoscPliku, 0, System.Convert.ToInt32(fs.Length));

                    fs.Close();

                    FileInfo fi = new FileInfo(ofd.FileName);
                    linkLabelNazwaZdjecia.Text = fi.Name;
                    maszynyBUS.VO.Zawartosc_pliku = _zawartoscPliku; //zawartosc zdjęcia
                    maszynyBUS.VO.Zdjecie = fi.Name;// nazwa zdjęcia
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z prezentacją zdjęcia. Błąd: " + ex.Message);
                }
                
            }
        }//buttonPokazZdj_Click

        private void buttonUsunZdj_Click(object sender, EventArgs e)
        {
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            maszynyBUS.VO.Zdjecie = string.Empty; // nazwa zdjęcia.
            maszynyBUS.VO.Zawartosc_pliku = new byte[0] { }; // zawartość pliku zdjęcia.
            linkLabelNazwaZdjecia.Text = string.Empty;
        }//buttonUsunZdj_Click




        //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  // //  //  //  //  //  //  //  //  wyświetlanie w zakładce Materiały.
        
        // --------------------------------------- wypełnianie  listBoxMaterialy.
        // wyświetla listę Materiałów po nazwie
        private void WypelnijMaterialyNazwami()
        {
            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            nsAccess2DB.MaterialyVO VO;
            listBoxMaterialy.Items.Clear();
            materialyBUS.select();

            while (!materialyBUS.eof)
            {
                VO = materialyBUS.VO;
                listBoxMaterialy.Items.Add(VO.Nazwa_mat + " - " + VO.Stan_mat + " " + VO.Jednostka_miar_mat);
                materialyBUS.skip();
            }
        }// WypelnijMaterialyNazwami()
       
        /// <summary>
        /// zmiana indeksu w list box Materialy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxMaterialy_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            materialyBUS.select();

            materialyBUS.idx = listBoxMaterialy.SelectedIndex;

            toolStripStatusLabelID_Materialu.Text = materialyBUS.VO.Identyfikator.ToString(); //  toolStripStatusLabelID_Materialu
            listBoxMaterialy.Tag = materialyBUS.VO.Identyfikator;
            textBoxTyp_materialu.Text = materialyBUS.VO.Typ_mat;
            comboBoxRodzaj.Text = materialyBUS.VO.Rodzaj_mat;
            textBoxNazwa_materialu.Text = materialyBUS.VO.Nazwa_mat;
            comboBoxJednostka_mat.Text = materialyBUS.VO.Jednostka_miar_mat;
            textBoxMagazyn_mat.Text = materialyBUS.VO.Stan_mat.ToString();
            textBoxZuzycie.Text = materialyBUS.VO.Zuzycie_mat.ToString();
            textBoxOdpad.Text = materialyBUS.VO.Odpad_mat.ToString();
            textBoxMin_materialu.Text = materialyBUS.VO.Stan_min_mat.ToString();
            textBoxZapotrzebowanie.Text = materialyBUS.VO.Zapotrzebowanie_mat.ToString();
            //dane dostawców Materiałów
            comboBoxDostawca1.Text = materialyBUS.VO.Dostawca_mat;

            //TODO zrobić kwerendę z dostawcami i ją uruchomić
            //linkLabelDostawca1.Text = dostawca_matBUS.kategoriaVO.Link_dostawca_mat;
            //richTextBoxDostawca1.Text = dostawca_matBUS.kategoriaVO.Dod_info_dostawca_mat;

            comboBoxDostawca2.Text = materialyBUS.VO.Dostawca_mat;
            //TODO zrobić kwerendę z dostawcami i ją uruchomić
            //linkLabelDostawca2.Text = dostawca_matBUS.kategoriaVO.Link_dostawca_mat;
            //richTextBoxDostawca2.Text = dostawca_matBUS.kategoriaVO.Dod_info_dostawca_mat;

        } // listBoxMaterialy_SelectedIndexChanged

              
       
        private void WypelnijJednostka_miar()
        {
            nsAccess2DB.Jednostka_miarBUS jednostka_MiarBUS = new nsAccess2DB.Jednostka_miarBUS(_connString);
            nsAccess2DB.Jednostka_miarVO VO;
            comboBoxJednostka_mat.Items.Clear();

            jednostka_MiarBUS.select();
            jednostka_MiarBUS.top();
            while (!jednostka_MiarBUS.eof)
            {
                VO = jednostka_MiarBUS.VO;
                comboBoxJednostka_mat.Items.Add(VO.Nazwa_jednostka_miar);
                jednostka_MiarBUS.skip();
            }
        }// WypelnijJednostka_miar()

        private void comboBoxJednostka_mat_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.Jednostka_miarBUS jednostka_MiarBUS = new nsAccess2DB.Jednostka_miarBUS(_connString);
            jednostka_MiarBUS.idx = comboBoxJednostka_mat.SelectedIndex;
        }// comboBoxJednostka_mat_SelectedIndexChanged

        private void WypelnijRodzaj_mat()
        {
            nsAccess2DB.Rodzaj_matBUS rodzaj_MatBUS = new nsAccess2DB.Rodzaj_matBUS(_connString);
            nsAccess2DB.Rodzaj_matVO VO;
            comboBoxRodzaj.Items.Clear();
            
            rodzaj_MatBUS.select();
            rodzaj_MatBUS.top();
            while (!rodzaj_MatBUS.eof)
            {
                VO = rodzaj_MatBUS.VO;
                comboBoxRodzaj.Items.Add(VO.Nazwa_rodzaj_mat);
                rodzaj_MatBUS.skip();
            }
        }
        private void comboBoxRodzaj_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.Rodzaj_matBUS rodzaj_MatBUS = new nsAccess2DB.Rodzaj_matBUS(_connString);
            rodzaj_MatBUS.idx = comboBoxJednostka_mat.SelectedIndex;
        }

        // --------- --------------------------------------- Formularz Materialy
        private void ButtonNowa_mat_Click(object sender, EventArgs e)
        {
            //toolStripStatusLabelID_Materialu.Text = string.Empty;

            textBoxTyp_materialu.Text = string.Empty;

            comboBoxRodzaj.SelectedIndex = -1;
            comboBoxRodzaj.Enabled = true;
            comboBoxRodzaj.SelectedIndex = 0;
            comboBoxRodzaj.Refresh();

            textBoxNazwa_materialu.Text = string.Empty;

            comboBoxJednostka_mat.SelectedIndex = -1;
            comboBoxJednostka_mat.Enabled = true;
            comboBoxJednostka_mat.SelectedIndex = 0;
            comboBoxJednostka_mat.Refresh();

            textBoxMagazyn_mat.Text = string.Empty;
            textBoxZuzycie.Text = string.Empty;
            textBoxOdpad.Text = string.Empty;
            textBoxMin_materialu.Text = string.Empty;
            textBoxZapotrzebowanie.Text = string.Empty;
            //dane dostawców Materiałów

            //-----------------------------------------------------TO DO Zrobić dostawców 1+2
            comboBoxDostawca1.SelectedIndex = -1;
            comboBoxDostawca1.Enabled = true;
            comboBoxDostawca1.SelectedIndex = 0;
            comboBoxDostawca1.Refresh();

            linkLabelDostawca1.Text = string.Empty;
            richTextBoxDostawca1.Text = string.Empty;
            //----------------------------------------------------TO DO Zrobić dostawców 1+2
            comboBoxDostawca2.SelectedIndex = -1;
            comboBoxDostawca2.Enabled = true;
            comboBoxDostawca2.SelectedIndex = 0;
            comboBoxDostawca2.Refresh();

            linkLabelDostawca2.Text = string.Empty;
            richTextBoxDostawca2.Text = string.Empty;

            buttonAnuluj.Enabled = true;

            _statusForm = (int)_status.nowy;

            //WypelnijMaterialyNazwami();
        }//ButtonNowa_Click

        private void buttonAnuluj_mat_Click(object sender, EventArgs e)
        {
            int idx = listBoxMaterialy.SelectedIndex;

            textBoxTyp_materialu.Text = string.Empty;
            comboBoxRodzaj.Text = string.Empty;
            textBoxNazwa_materialu.Text = string.Empty;
            comboBoxJednostka_mat.Text = string.Empty;
            textBoxMagazyn_mat.Text = string.Empty;
            textBoxZuzycie.Text = string.Empty;
            textBoxOdpad.Text = string.Empty;
            textBoxMin_materialu.Text = string.Empty;
            textBoxZapotrzebowanie.Text = string.Empty;
            //dane dostawców Materiałów
            //-----------------------------------------------------TO DO Zrobić dostawców 1+2
            comboBoxDostawca1.Text = string.Empty;
            linkLabelDostawca1.Text = string.Empty;
            richTextBoxDostawca1.Text = string.Empty;
            //----------------------------------------------------TO DO Zrobić dostawców 1+2
            comboBoxDostawca2.Text = string.Empty;
            linkLabelDostawca2.Text = string.Empty;
            richTextBoxDostawca2.Text = string.Empty;

            WypelnijMaterialyNazwami();
            listBoxMaterialy.SelectedIndex = idx;

        }//buttonAnuluj_mat_Click

        private void buttonUsun_mat_Click(object sender, EventArgs e)
        {
            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            materialyBUS.delete((int)listBoxMaterialy.Tag);

            textBoxTyp_materialu.Text = string.Empty;
            comboBoxRodzaj.Text = string.Empty;
            textBoxNazwa_materialu.Text = string.Empty;
            comboBoxJednostka_mat.Text = string.Empty;
            textBoxMagazyn_mat.Text = string.Empty;
            textBoxZuzycie.Text = string.Empty;
            textBoxOdpad.Text = string.Empty;
            textBoxMin_materialu.Text = string.Empty;
            textBoxZapotrzebowanie.Text = string.Empty;
            //dane dostawców Materiałów
            //-----------------------------------------------------TO DO Zrobić dostawców 1+2
            comboBoxDostawca1.Text = string.Empty;
            linkLabelDostawca1.Text = string.Empty;
            richTextBoxDostawca1.Text = string.Empty;
            //----------------------------------------------------TO DO Zrobić dostawców 1+2
            comboBoxDostawca2.Text = string.Empty;
            linkLabelDostawca2.Text = string.Empty;
            richTextBoxDostawca2.Text = string.Empty;

            WypelnijMaterialyNazwami();
        }//buttonUsun_mat_Click

        private void buttonZapisz_mat_Click(object sender, EventArgs e)
        {
            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            if (textBoxNazwa_materialu.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij nazwę materiału", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            buttonNowa.Enabled = true;

            nsAccess2DB.MaterialyVO Mat_VO = new nsAccess2DB.MaterialyVO();

            Mat_VO.Rodzaj_mat = comboBoxRodzaj.Text.Trim();
            Mat_VO.Typ_mat = textBoxTyp_materialu.Text.Trim();
            Mat_VO.Nazwa_mat = textBoxNazwa_materialu.Text.Trim();
            Mat_VO.Jednostka_miar_mat = comboBoxJednostka_mat.Text.Trim();
            Mat_VO.Stan_mat = int.Parse(textBoxMagazyn_mat.Text.Trim());
            Mat_VO.Zuzycie_mat = int.Parse(textBoxZuzycie.Text.Trim());
            Mat_VO.Odpad_mat = int.Parse(textBoxOdpad.Text.Trim());
            Mat_VO.Stan_min_mat = int.Parse(textBoxMin_materialu.Text.Trim());
            Mat_VO.Zapotrzebowanie_mat = int.Parse(textBoxZapotrzebowanie.Text.Trim());

            if (toolStripStatusLabelID_Materialu.Text == string.Empty) //nowa pozycja w tabeli materialów
            {
                Mat_VO.Identyfikator = -1;
            }
            else
                Mat_VO.Identyfikator = int.Parse(toolStripStatusLabelID_Materialu.Text);

            buttonUsun.Enabled = listBoxMaterialy.Items.Count > 0;

            if (toolStripStatusLabelID_Materialu.Text == string.Empty)
            {
                listBoxMaterialy.SelectedIndex = listBoxMaterialy.Items.Count - 1;
            }
            else
            {
                listBoxMaterialy.SelectedIndex = materialyBUS.getIdx(Mat_VO.Identyfikator);
            }

            materialyBUS.write(Mat_VO);

            pokazKomunikat("Pozycja zapisana w bazie");

            WypelnijMaterialyNazwami();
        }//buttonZapisz_mat_Click


        private void buttonSzukaj_mat_Click(object sender, EventArgs e)
        {
            listBoxMaterialy.Items.Clear();

            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            materialyBUS.selectQuery("SELECT * FROM Materialy WHERE Nazwa_mat LIKE'" + textBoxWyszukaj_mat + "%' OR Rodzaj_mat LIKE '%" + textBoxWyszukaj_mat + "%';");
            while (!materialyBUS.eof)
            {
                listBoxMaterialy.Items.Add(materialyBUS.VO.Nazwa_mat + " " + materialyBUS.VO.Rodzaj_mat);
                materialyBUS.skip();
            }
            if (listBoxMaterialy.Items.Count > 0)
            {
                listBoxMaterialy.SelectedIndex = 0;
            }
        }// button buttonSzukaj_mat_Click










        //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  ---------------------------Operatorzy maszyn.
        
       
        //wyświetla listę operatorów maszyn po imieniu i nazwisku
        private void WypelnijOperatorowDanymi()
        {
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            listBoxOperator.Items.Clear();
            operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Op_nazwisko ASC;");
           
            while (!operatorBUS.eof)
            {
                listBoxOperator.Items.Add(operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                operatorBUS.skip();
            }

            //if (listBoxOperator.Items.Count > 0)
            //{
            //    listBoxOperator.SelectedIndex = 0;
            //}
        }//WypelnijOperatorowDanymi()

        /// <summary>
        /// Czyści dane formularza operatorzy maszyn
        /// </summary>
        private void CzyscDaneOperatora()
        {
            toolStripStatusLabelIDOperatora.Text = "";
            textBoxImieOperator.Text = string.Empty;
            textBoxNazwiskoOperator.Text = string.Empty;

            comboBoxDzialOperator.SelectedIndex = -1;
            comboBoxDzialOperator.Enabled = true;
            comboBoxDzialOperator.SelectedIndex = 0;
            comboBoxDzialOperator.Refresh();

            textBoxUprawnienieOperator.Text = string.Empty;
            dateTimePickerDataKoncaUprOp.Text = string.Empty;
            listBoxMaszynyOperatora.Items.Clear();
        }//CzyscDaneOperatora()

        private void OdswiezOperatorowMaszyn()
        {
           // listBoxOperator.BeginUpdate();
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            listBoxOperator.Items.Clear();
            operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Op_nazwisko ASC;");

            while (!operatorBUS.eof)
            {
                listBoxOperator.Items.Add(operatorBUS.VO.Op_nazwisko + operatorBUS.VO.Op_imie);
                operatorBUS.skip();
            }
        }//OdswiezOperatorowMaszyn()

        /// <summary>
        /// zmiana indeksu w list box operator maszyny
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxOperator_maszyny_SelectedIndexChanged(object sender, EventArgs e)
        {

            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            // uaktualnia dane operatorów maszyn po zmianie ich sortowania.
            if (comboBoxOperator.SelectedIndex == 0)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Op_nazwisko ASC;");
                operatorBUS.idx = listBoxOperator.SelectedIndex;
                toolStripStatusLabelIDOperatora.Text = operatorBUS.VO.Identyfikator.ToString();
            }
            else if (comboBoxOperator.SelectedIndex == 1)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Dzial;");
                operatorBUS.idx = listBoxOperator.SelectedIndex;
                toolStripStatusLabelIDOperatora.Text = operatorBUS.VO.Identyfikator.ToString();
            }
            else if (comboBoxOperator.SelectedIndex == 2)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Uprawnienie;");
                operatorBUS.idx = listBoxOperator.SelectedIndex;
                toolStripStatusLabelIDOperatora.Text = operatorBUS.VO.Identyfikator.ToString();
            }
            else if (comboBoxOperator.SelectedIndex == 3)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Data_konca_upr;");
                operatorBUS.idx = listBoxOperator.SelectedIndex;
                toolStripStatusLabelIDOperatora.Text = operatorBUS.VO.Identyfikator.ToString();
            }
            else if (comboBoxOperator.SelectedIndex == 4) //szukanie po ciągu znaków z imienia lub nazwiska.
            {
                operatorBUS.selectQuery("SELECT * FROM Operator WHERE Op_nazwisko LIKE '" + textBoxWyszukiwanieOperator.Text + "%' OR Op_imie LIKE '" + textBoxWyszukiwanieOperator.Text + "%';");
                operatorBUS.idx = listBoxOperator.SelectedIndex;
                toolStripStatusLabelIDOperatora.Text = operatorBUS.VO.Identyfikator.ToString();
                buttonSzukajOperator.Enabled = true;
            }

            try
            {
                // operatorBUS.select();
                toolStripStatusLabelIDOperatora.Text = operatorBUS.VO.Identyfikator.ToString(); //  toolStripStatusLabelIDOperatora
                operatorBUS.idx = listBoxOperator.SelectedIndex;

                listBoxOperator.Tag = operatorBUS.VO.Identyfikator;

                textBoxImieOperator.Text = operatorBUS.VO.Op_imie;
                textBoxNazwiskoOperator.Text = operatorBUS.VO.Op_nazwisko;
                comboBoxDzialOperator.Text = operatorBUS.VO.Dzial;
                textBoxUprawnienieOperator.Text = operatorBUS.VO.Uprawnienie;
                dateTimePickerDataKoncaUprOp.Value = new DateTime(operatorBUS.VO.Rok, operatorBUS.VO.Mc, operatorBUS.VO.Dzien);
                
                //wypełnia listę maszyn obsługiwanych przez wybranego operatora - zmiana indeksu operatora ma zmieniać listę podległych mu maszyn.
                nsAccess2DB.Maszyny_OperatorBUS maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);

                listBoxMaszynyOperatora.Items.Clear();
                maszynyBUS.select();

                maszyny_OperatorBUS.selectOperator((int)listBoxOperator.Tag);

                _maszynaTag = new int[maszyny_OperatorBUS.count];// przechowuje ID_maszyny.

                int idx = 0;
                while (!maszyny_OperatorBUS.eof)
                {
                    listBoxMaszynyOperatora.Items.Add(maszyny_OperatorBUS.VO.Maszyny_nazwa);
                    _maszynaTag[idx] = maszyny_OperatorBUS.VO.ID_maszyny;
                    maszyny_OperatorBUS.skip();
                    idx++;
                }

            }
            catch { }
        }//listBoxOperator_maszyny_SelectedIndexChanged

        // wyświetla listę maszyn dla danego operatora
        private void WypelnijOperatorowMaszynami()
        {
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            nsAccess2DB.Maszyny_OperatorBUS maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            listBoxMaszynyOperatora.Items.Clear();
          
            maszynyBUS.select();
            maszyny_OperatorBUS.selectOperator((int)operatorBUS.VO.Identyfikator);

            while (!maszynyBUS.eof)
            {
                listBoxMaszynyOperatora.Items.Add(maszynyBUS.VO.Nazwa);
                maszynyBUS.skip();
            }

            if (listBoxMaszynyOperatora.Items.Count > 0)
            {
                maszynyBUS.idx = 0;
                listBoxMaszynyOperatora.SelectedIndex = 0;
                listBoxMaszynyOperatora.Tag = maszynyBUS.VO.Identyfikator;
            }
        }//WypelnijOperatorowMaszynami()


        private void comboBoxOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxOperator.Items.Clear();
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);

            // sortowanie po dacie końca uprawnień
            if (comboBoxOperator.SelectedIndex == 0)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Op_nazwisko;");
                while (!operatorBUS.eof)
                {
                    listBoxOperator.Items.Add(operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                    operatorBUS.skip();
                }
            }
            if (comboBoxOperator.SelectedIndex == 1)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Dzial;");
                while (!operatorBUS.eof)
                {
                    listBoxOperator.Items.Add(operatorBUS.VO.Dzial + " - " + operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                    operatorBUS.skip();
                }
            }
            if (comboBoxOperator.SelectedIndex == 2)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Uprawnienie;");
                while (!operatorBUS.eof)
                {
                    listBoxOperator.Items.Add(operatorBUS.VO.Uprawnienie + " - " + operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                    operatorBUS.skip();
                }
            }
            if (comboBoxOperator.SelectedIndex == 3)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Data_konca_upr;");
                while (!operatorBUS.eof)
                {
                    listBoxOperator.Items.Add(operatorBUS.VO.Dzien + ":" + operatorBUS.VO.Mc + ":" + operatorBUS.VO.Rok + " - (" + operatorBUS.VO.Uprawnienie + ") - " + operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                    operatorBUS.skip();
                }
            }
            if (comboBoxOperator.SelectedIndex == 4)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator WHERE Op_nazwisko LIKE '" + textBoxWyszukiwanieOperator.Text + "%' OR Op_imie LIKE '" + textBoxWyszukiwanieOperator.Text + "%';");
                operatorBUS.idx = listBoxOperator.SelectedIndex;
                toolStripStatusLabelIDOperatora.Text = operatorBUS.VO.Identyfikator.ToString();
            }


            if (listBoxOperator.Items.Count > 0)
            {
                listBoxOperator.SelectedIndex = 0;
            }
        }//comboBoxOperator_SelectedIndexChanged


        private void WypelnijDzialOperatora()
        {
            nsAccess2DB.DzialBUS dzialBUS = new nsAccess2DB.DzialBUS(_connString);
            nsAccess2DB.DzialVO VO;
            comboBoxDzialOperator.Items.Clear();

            dzialBUS.select();
            dzialBUS.top();
            while (!dzialBUS.eof)
            {
                VO = dzialBUS.VO;
                comboBoxDzialOperator.Items.Add(VO.Nazwa);
                dzialBUS.skip();
            }
        }//WypelnijDzialOperatora()
        private void comboBoxDzial_operator_maszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.DzialBUS dzialBUS = new nsAccess2DB.DzialBUS(_connString);
            dzialBUS.idx = comboBoxDzialOperator.SelectedIndex;
            comboBoxDzialOperator.Tag = dzialBUS.VO.Nazwa;
        }//comboBoxDzial_SelectedIndexChanged


        // -------------------  Przyciski w zakładce Operator.

        private void buttonNowaOperator_Click(object sender, EventArgs e)
        {
            CzyscDaneOperatora();
            
            buttonNowaOperator.Enabled = false;
            buttonZapiszOperator.Enabled = true;
            buttonAnulujOperator.Enabled = true;
            //buttonUsunOperator.Enabled = false;

            _statusForm = (int)_status.nowy;
        }//buttonNowaOperator_Click

        private void buttonZapiszOperator_Click(object sender, EventArgs e)
        {
            if (textBoxNazwiskoOperator.Text == string.Empty) // textBoxNazwisko nie może być puste
            {
                MessageBox.Show("Uzupełnij nazwisko operatora maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxImieOperator.Text == string.Empty)// textBoxImie nie może być puste
            {
                MessageBox.Show("Proszę uzupełnij imię operatora maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            nsAccess2DB.Maszyny_OperatorBUS maszyny_operatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);

            nsAccess2DB.OperatorVO operatorVO = new nsAccess2DB.OperatorVO();
            nsAccess2DB.Maszyny_OperatorVO maszyny_operatorVO = new nsAccess2DB.Maszyny_OperatorVO();
            nsAccess2DB.MaszynyVO maszynyVO = new nsAccess2DB.MaszynyVO();

            if (_statusForm == (int)_status.nowy)
            {
                operatorBUS.select();
                operatorBUS.idx = operatorBUS.count - 1;
                operatorVO.Identyfikator = operatorBUS.VO.Identyfikator + 1;

                operatorVO.Op_imie = textBoxImieOperator.Text.Trim();
                operatorVO.Op_nazwisko = textBoxNazwiskoOperator.Text.Trim();
                operatorVO.Dzial = comboBoxDzialOperator.Text.Trim();
                operatorVO.Uprawnienie = textBoxUprawnienieOperator.Text.Trim();
                operatorVO.Rok = dateTimePickerDataKoncaUprOp.Value.Year;
                operatorVO.Mc = dateTimePickerDataKoncaUprOp.Value.Month;
                operatorVO.Dzien = dateTimePickerDataKoncaUprOp.Value.Day;
                //operatorVO.Data_konca_upr = int.Parse(operatorVO.Rok.ToString() + operatorVO.Mc.ToString("00") + operatorVO.Dzien.ToString("00"));
                operatorVO.Data_konca_upr = dateTimePickerDataKoncaUprOp.Value;

                //TODO komunikat o zbliżającej się dacie końca uprawnień operatora. Ustal jak być powinno.
                //DateTime dt = new DateTime(dateTimePickerDataKoncaUprOp.Value.Ticks);
                //dt = dt.AddDays(31);

                //operatorVO.Identyfikator = int.Parse(toolStripStatusLabelIDOperatora.Text);

                operatorBUS.write(operatorVO);
                maszyny_operatorBUS.selectOperator(operatorVO.Identyfikator);
                operatorBUS.select();

                listBoxOperator.SelectedIndex = operatorBUS.getIdx(operatorBUS.VO.Identyfikator); // ustawia zaznaczenie w tab operator.
            }// if nowy
            else if (_statusForm == (int)_status.edycja)
            {
                operatorBUS.select((int)listBoxOperator.Tag);
                if (operatorBUS.count < 1)
                {
                    MessageBox.Show("Operator o identyfikatorze " + listBoxOperator.Tag.ToString() + "nie istnieje w bazie operatorów", "RemaGUM",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    operatorVO.Identyfikator = (int)listBoxOperator.Tag;
                    operatorVO.Op_imie = textBoxImieOperator.Text.Trim();
                    operatorVO.Op_nazwisko = textBoxNazwiskoOperator.Text.Trim();
                    operatorVO.Dzial = comboBoxDzialOperator.Text.Trim();
                    operatorVO.Uprawnienie = textBoxUprawnienieOperator.Text.Trim();
                    operatorVO.Rok = dateTimePickerDataKoncaUprOp.Value.Year;
                    operatorVO.Mc = dateTimePickerDataKoncaUprOp.Value.Month;
                    operatorVO.Dzien = dateTimePickerDataKoncaUprOp.Value.Day;
                    operatorVO.Data_konca_upr = dateTimePickerDataKoncaUprOp.Value;
                    
                    operatorBUS.write(operatorVO);

                    operatorBUS.select(operatorBUS.VO.Identyfikator);
                }
            listBoxOperator.SelectedIndex = operatorBUS.getIdx(operatorBUS.VO.Identyfikator); // usatwia zaznaczenie w tab operator. 
            }// else if - edycja

            WypelnijOperatorowDanymi();

            comboBoxOperator.SelectedIndex = 0;

            buttonNowaOperator.Enabled = true;
            buttonZapiszOperator.Enabled = true;
            buttonAnulujOperator.Enabled = true;
            buttonUsunOperator.Enabled = true;
            
            WypelnijOperatorowMaszynami();
            OdswiezOperatorowMaszyn();

            pokazKomunikat("Pozycja zapisana w bazie");
            _statusForm = (int)_status.edycja;
        }// buttonZapiszOperator_Click

        private void buttonAnulujOperator_Click(object sender, EventArgs e)
        {
            int idx = listBoxOperator.SelectedIndex;

            CzyscDaneOperatora();

            buttonNowaOperator.Enabled = true;
            buttonZapiszOperator.Enabled = true;
            buttonAnulujOperator.Enabled = true;
            buttonUsunOperator.Enabled = false;

            _statusForm = (int)_status.edycja;

        } //buttonAnulujOperator_Click

        private void buttonUsunOperator_Click(object sender, EventArgs e)
        {
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            nsAccess2DB.Maszyny_OperatorBUS maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            try
            {
                textBoxImieOperator.Text = string.Empty;
                textBoxNazwiskoOperator.Text = string.Empty;
                comboBoxDzialOperator.Text = string.Empty;
                textBoxUprawnienieOperator.Text = string.Empty;
                dateTimePickerDataKoncaUprOp.Text = string.Empty;
                listBoxMaszynyOperatora.Items.Clear();
            }
            catch { }

            operatorBUS.delete((int)listBoxOperator.Tag); // usunięcie operatora maszyny z tabeli operator.
            maszyny_OperatorBUS.delete((int)listBoxOperator.Tag);// usunięcie relacji z tabeli Maszyny_Operator.

            comboBoxOperator.SelectedIndex = 1;// odświeża listę operatorów
            comboBoxOperator.SelectedIndex = 0;
            //WypelnijOperatorowDanymi();

        }// buttonUsunOperator_Clik

        private void buttonSzukajOperator_Click(object sender, EventArgs e)
        {
            listBoxOperator.Items.Clear();
            comboBoxOperator.SelectedIndex = 4; // ustawia comboboxa  na sortowanie po nazwisku operatora.
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            operatorBUS.selectQuery("SELECT * FROM Operator WHERE Op_nazwisko LIKE '" + textBoxWyszukiwanieOperator.Text + "%' OR Op_imie LIKE '" + textBoxWyszukiwanieOperator.Text + "%';");

            while (!operatorBUS.eof)
            {
                listBoxOperator.Items.Add(operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                operatorBUS.skip();
            }

            if (listBoxOperator.Items.Count > 0)
            {
                listBoxOperator.SelectedIndex = 0;
            }
        }// buttonSzukajOperator_Click

        // // // // // // // // // // // // // // // // // // // // // // // // -------------------------> Dysponent maszyny.

        /// <summary>
        /// wyświetla listę dysponentów maszyn - nazwisko i imię.
        /// </summary>
        private void WypelnijDysponentowDanymi()
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            listBoxDysponent.Items.Clear();
            dysponentBUS.selectQuery("SELECT * FROM Dysponent ORDER BY Dysp_nazwisko ASC;");
          
            while (!dysponentBUS.eof)
            {
                //dysponentVO = dysponentBUS.VO;
                listBoxDysponent.Items.Add(dysponentBUS.VO.Dysp_nazwisko + " " + dysponentBUS.VO.Dysp_imie);
                dysponentBUS.skip();
            }
        }// WypelnijDysponentowDanymi()


        private void CzyscDaneDysponenta()
        {
            toolStripStatusLabelDysponenta.Text = "";

            textBoxImieDysponent.Text = string.Empty;
            textBoxNazwiskoDysponent.Text = string.Empty;

            comboBoxDzialDysponent.SelectedIndex = -1;
            comboBoxDzialDysponent.Enabled = true;
            comboBoxDzialDysponent.SelectedIndex = 0;
            comboBoxDzialDysponent.Refresh();

            richTextBoxDysponent_dane.Text = string.Empty;
            listBoxMaszynyDysponenta.Items.Clear();
           
        }// CzyscDaneDysponenta()

        private void OdswiezDysponentowMaszyn()
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            listBoxDysponent.Items.Clear();
            dysponentBUS.selectQuery("SELECT * FROM Dysponent ORDER BY Dysp_nazwisko ASC;");

            while (!dysponentBUS.eof)
            {
                listBoxDysponent.Items.Add(dysponentBUS.VO.Dysp_nazwisko + dysponentBUS.VO.Dysp_imie);
                dysponentBUS.skip();
            }
        }// OdswiezDysponentowMaszyn()

        /// <summary>
        /// Zmiana indeksu w list box dysponent.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxDysponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);

            dysponentBUS.selectQuery("SELECT * FROM Dysponent ORDER BY Dysp_nazwisko ASC;");
            dysponentBUS.idx = listBoxDysponent.SelectedIndex;
            toolStripStatusLabelDysponenta.Text = dysponentBUS.VO.Identyfikator.ToString(); // toolStripStatusLabelIDDysponenta

            try
            {
                listBoxDysponent.Tag = dysponentBUS.VO.Identyfikator;

                textBoxImieDysponent.Text = dysponentBUS.VO.Dysp_imie;
                textBoxNazwiskoDysponent.Text = dysponentBUS.VO.Dysp_nazwisko;
                comboBoxDzialDysponent.Text = dysponentBUS.VO.Dzial;
                richTextBoxDysponent_dane.Text = dysponentBUS.VO.Dysp_dane;



                //wypełnia listę maszyn, którymi zarządza wybrany z listy dysponent.
                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
                //nsAccess2DB.Maszyny_DysponentBUS maszyny_DysponentBUS = new nsAccess2DB.Maszyny_DysponentBUS(_connString);
                dysponentBUS.select();
                maszynyBUS.select();

                listBoxMaszynyDysponenta.Items.Clear();
                WypelnijDysponentowMaszynami();
                
                // maszyny_DysponentBUS.select();
                //maszyny_DysponentBUS.selectDysponent((int)listBoxDysponent.Tag);

               //_maszynaTagD = new int[maszyny_DysponentBUS.count]; // przechowuje ID maszyny Dysponenta.

               //int idx = 0;

               //while (!maszyny_DysponentBUS.eof)
               //{
              // maszynyBUS.selectQuery("SELECT * FROM Maszyny WHERE Nazwa_dysponent = 'Aleja Anna'");
                    //listBoxMaszynyDysponenta.Items.Add(maszyny_DysponentBUS.VO.Maszyny_nazwa_D);
                    //_maszynaTagD[idx] = maszyny_DysponentBUS.VO.ID_maszyny;
                    //maszyny_DysponentBUS.skip();
                    //idx++;
                //}
            }
            catch { }
        }// listBoxDysponent_SelectedIndexChanged


        private void WypelnijDysponentowMaszynami()
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            nsAccess2DB.Maszyny_DysponentBUS maszyny_DysponentBUS = new nsAccess2DB.Maszyny_DysponentBUS(_connString);
            
            listBoxMaszynyDysponenta.Items.Clear();
            maszynyBUS.selectQuery("SELECT * FROM Maszyny WHERE Nazwa_dysponent = '" + textBoxNazwiskoDysponent.Text + " " + textBoxImieDysponent.Text + "'");
            //maszynyBUS.select();

            while (!maszynyBUS.eof)
            {
                listBoxMaszynyDysponenta.Items.Add(maszynyBUS.VO.Nazwa + " " + maszynyBUS.VO.Nr_fabryczny);
                maszynyBUS.skip();
            }

            if (listBoxMaszynyDysponenta.Items.Count > 0)
            {
                maszynyBUS.idx = 0;
                listBoxMaszynyDysponenta.SelectedIndex = 0;
                listBoxMaszynyDysponenta.Tag = maszynyBUS.VO.Identyfikator;
            }
        }// WypelnijDysponentowMaszynami()

        /// <summary>
        /// Wypełnia dział 
        /// </summary>
        private void WypelnijDzialDysponenta()
        {
            nsAccess2DB.DzialBUS dzialBUS = new nsAccess2DB.DzialBUS(_connString);
            nsAccess2DB.DzialVO VO;
            comboBoxDzialDysponent.Items.Clear();

            dzialBUS.select();
            dzialBUS.top();
            while (!dzialBUS.eof)
            {
                VO = dzialBUS.VO;
                comboBoxDzialDysponent.Items.Add(VO.Nazwa);
                dzialBUS.skip();
            }
        }//WypelnijDzialDysponenta()

        private void comboBoxDzial_dysponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.DzialBUS dzialBUS = new nsAccess2DB.DzialBUS(_connString);
            dzialBUS.idx = comboBoxDzialDysponent.SelectedIndex;
            comboBoxDzialDysponent.Tag = dzialBUS.VO.Nazwa;
        }// comboBoxDzial_dysponent_SelectedIndexChanged

        

        // ----------------------------------------------Przyciski w zakładce Dysponent.
        private void buttonNowaDysponent_Click(object sender, EventArgs e)
        {
            CzyscDaneDysponenta();
            buttonNowaDysponent.Enabled = false;
            buttonZapiszDysponent.Enabled = true;
            buttonAnulujDysponent.Enabled = true;

            _statusForm = (int)_status.nowy;
        }// buttonNowaDysponent_Click

        private void buttonZapiszDysponent_Click(object sender, EventArgs e)
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            nsAccess2DB.DysponentVO dysponentVO = new nsAccess2DB.DysponentVO();
            nsAccess2DB.Maszyny_DysponentBUS maszyny_DysponentBUS = new nsAccess2DB.Maszyny_DysponentBUS(_connString);
            nsAccess2DB.Maszyny_DysponentVO maszyny_DysponentVO = new nsAccess2DB.Maszyny_DysponentVO();
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            nsAccess2DB.MaszynyVO maszynyVO = new nsAccess2DB.MaszynyVO();
            if (textBoxImieDysponent.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij imię dysponenta maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxNazwiskoDysponent.Text == string.Empty)
            {
                MessageBox.Show("Proszę uzupełnij nazwisko dysponenta maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_statusForm == (int)_status.nowy)
            {
                dysponentBUS.select();
                dysponentBUS.idx = dysponentBUS.count - 1;
                dysponentVO.Identyfikator = dysponentBUS.VO.Identyfikator + 1;

                dysponentVO.Dysp_imie = textBoxImieDysponent.Text.Trim();
                dysponentVO.Dysp_nazwisko = textBoxNazwiskoDysponent.Text.Trim();
                dysponentVO.Dzial = comboBoxDzialDysponent.Text.Trim();
                dysponentVO.Dysp_dane = richTextBoxDysponent_dane.Text.Trim();
                dysponentVO.Dysp_nazwa = textBoxNazwiskoDysponent.Text.Trim() + " " + textBoxImieDysponent.Text.Trim(); // zapis nazwy dysponenta


                dysponentBUS.write(dysponentVO);
                dysponentBUS.select();

                listBoxDysponent.SelectedIndex = dysponentBUS.getIdx(dysponentBUS.VO.Identyfikator); // ustawia zaznaczenie w tabeli dysponent.
                // zapis maszyn przypisanych do dysponenta.
                maszyny_DysponentBUS.selectDysponent(dysponentVO.Identyfikator);
                maszynyBUS.select();

            }// if -> nowy
            else if (_statusForm == (int)_status.edycja)
            {
                dysponentBUS.select((int)listBoxDysponent.Tag);
                if (dysponentBUS.count < 1)
                {
                    MessageBox.Show("Dysponent o identyfikatorze " + listBoxDysponent.Tag.ToString() + "nie istnieje w bazie operatorów", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    dysponentVO.Identyfikator = (int)listBoxDysponent.Tag;
                    dysponentVO.Dysp_imie = textBoxImieDysponent.Text.Trim();
                    dysponentVO.Dysp_nazwisko = textBoxNazwiskoDysponent.Text.Trim();
                    dysponentVO.Dzial = comboBoxDzialDysponent.Text.Trim();
                    dysponentVO.Dysp_dane = richTextBoxDysponent_dane.Text.Trim();
                    dysponentVO.Dysp_nazwa = textBoxNazwiskoDysponent.Text.Trim() + " " + textBoxImieDysponent.Text.Trim(); // zapis nazwy dysponenta

                    dysponentBUS.write(dysponentVO);
                    dysponentBUS.select(dysponentVO.Identyfikator);

                    // edycja zapisu maszyn przypisanych do Dysponenta.
                    maszyny_DysponentBUS.select(maszynyVO.Identyfikator, dysponentVO.Identyfikator);
                    maszynyBUS.select();
                }
                listBoxDysponent.SelectedIndex = dysponentBUS.getIdx(dysponentBUS.VO.Identyfikator); // znacznik id dysponenta.
            } // else if -> edycja

            WypelnijDysponentowDanymi();
            WypelnijDysponentowMaszynami();

            buttonNowaDysponent.Enabled = true;
            buttonZapiszDysponent.Enabled = true;
            buttonAnulujDysponent.Enabled = true;
            buttonUsunDysponent.Enabled = true;

            pokazKomunikat("Pozycja zapisana w bazie");
            _statusForm = (int)_status.edycja;

        }// buttonZapiszDysponent_Click

        private void buttonAnulujDysponent_Click(object sender, EventArgs e)
        {
            int idx = listBoxDysponent.SelectedIndex;

            CzyscDaneDysponenta();

            buttonNowaDysponent.Enabled = true;
            buttonZapiszDysponent.Enabled = true;
            buttonAnulujDysponent.Enabled = true;
            buttonUsunDysponent.Enabled = false;

            _statusForm = (int)_status.edycja;
        }// buttonAnulujDysponent_Click

        private void buttonUsunDysponent_Click(object sender, EventArgs e)
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            nsAccess2DB.Maszyny_DysponentBUS maszyny_DysponentBUS = new nsAccess2DB.Maszyny_DysponentBUS(_connString);

            CzyscDaneDysponenta();
            
            dysponentBUS.delete((int)listBoxDysponent.Tag); // usunięcie danych dysponenta.
            maszyny_DysponentBUS.delete((int)listBoxDysponent.Tag); // usunięcie relacji z tabeli maszyny - dysponent

            WypelnijDysponentowDanymi();
            _statusForm = (int)_status.edycja;

        }// buttonUsunDysponent_Click

        //TODO gubi SelectedIndex - napraw.

        private void buttonSzukajDysponent_Click(object sender, EventArgs e)
        {
            if (textBoxWyszukiwanieDysponent.Text == string.Empty)
            {
                pokazKomunikat("Proszę wpisać tekst do pola wyszukiwania. Szukanie anulowane.");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            nsAccess2DB.DysponentVO dysponentVO;
            dysponentVO = dysponentBUS.VO;

            listBoxDysponent.Items.Clear();

            dysponentBUS.selectQuery("SELECT * FROM Dysponent ORDER BY Dysp_nazwisko ASC;");
            textBoxWyszukiwanieDysponent.Text = textBoxWyszukiwanieDysponent.Text.Trim();

            string s1 = textBoxWyszukiwanieDysponent.Text.ToUpper();
            string s2;

           for (int i = _dysponentSzukajIdx; i < dysponentBUS.count; i++)
            {
                dysponentBUS.idx = i;
                dysponentVO = dysponentBUS.VO;

                s2 = dysponentVO.Dysp_nazwisko.ToUpper();
                if (s2.Contains(s1))
                {
                    _dysponentSzukajIdx = i;
                    listBoxDysponent.SelectedIndex = i;
                    if (MessageBox.Show("czy szukać dalej ?", "RemaGUM", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    {
                        goto cancel;
                    }
                }
            }
            pokazKomunikat("Aby szukać od poczatku wciśnij szukaj.");

            cancel:;
            _dysponentSzukajIdx = 0;
           
            _dysponentSzukajIdx = dysponentBUS.count;
            

        }

    }// public partial class SpisForm : Form

}//namespace RemaGUM
