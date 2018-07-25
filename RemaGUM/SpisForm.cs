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
        private string _connString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Projects\\RemaGUM\\RemaGUM.mdb"; //połaczenie z bazą danych
      
        private string _helpFile = Application.StartupPath + "\\RemaGUM.chm"; //plik pomocy RemaGUM

        private byte[] _zawartoscPliku; //dane odczytane z pliku zdjęcia

        string _dirNazwa = "tempRemaGUM"; //nazwa katalogu tymczasowego
        string _dirPelnaNazwa = string.Empty; // katalog tymczasowy - pelna nazwa

        enum _status { edycja, nowy, usun, zapisz, anuluj };  //status działania formularza
        private byte _statusForm; // wartośc statusu formularza

        private int[] _maszynaTag; // przechowuje identyfikatory maszyn.
        private int[] _operatorTag; // przechowuje identyfikatory operatorów.

        private nsAccess2DB.MaszynyBUS _MaszynyBUS;
        private nsAccess2DB.KategoriaBUS _KategoriaBUS;
        private nsAccess2DB.Osoba_zarzadzajacaBUS _Osoba_zarzadzajacaBUS;
        private nsAccess2DB.DzialBUS _DzialBUS;
        private nsAccess2DB.WykorzystanieBUS _WykorzystanieBUS;
        private nsAccess2DB.PropozycjaBUS _PropozycjaBUS;
        private nsAccess2DB.Stan_technicznyBUS _Stan_technicznyBUS;
        private nsAccess2DB.OperatorBUS _OperatorBUS;
        private nsAccess2DB.Maszyny_OperatorBUS _Maszyny_OperatorBUS;
        private nsAccess2DB.MaterialyBUS _MaterialyBUS;
        private nsAccess2DB.Jednostka_miarBUS _Jednostka_miarBUS;
        private nsAccess2DB.Rodzaj_matBUS _Rodzaj_matBUS;
        private nsAccess2DB.Dostawca_MaterialBUS _Dostawca_MaterialBUS;
        private nsAccess2DB.Dostawca_matBUS _Dostawca_matBUS;

        //private int[] _operatorTag; // przechowuje identyfikatory osób //********************************************

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
            _connString += rest.dbConnection(_connString);

            _statusForm = (int)_status.edycja;

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
            comboBoxOsoba_zarzadzajaca.TabIndex = 9;
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
            radioButtonData_kol_przegladu.TabIndex = 29;
            //wyszukiwanie Maszyny po wpisanej nazwie 
            textBoxWyszukiwanie.TabIndex = 30;
            buttonSzukaj.TabIndex = 31;
            // --------------------------------------------- Zakładka Materiały
            //dane formularza Materiały
            listBoxMaterialy.TabIndex = 32;
            textBoxTyp_materialu.TabIndex = 33;
            comboBoxRodzaj.TabIndex = 34;
            textBoxNazwa_materialu.TabIndex = 35;
            comboBoxJednostka_mat.TabIndex = 36;
            textBoxMagazyn_mat.TabIndex = 37;
            textBoxZuzycie.TabIndex = 38;
            textBoxOdpad.TabIndex = 39;
            textBoxMin_materialu.TabIndex = 40;
            textBoxZapotrzebowanie.TabIndex = 41;
            //dane dostawców Materiałów
            comboBoxDostawca1.TabIndex = 42;
            linkLabelDostawca1.TabIndex = 43;
            richTextBoxDostawca1.TabIndex = 44;
            comboBoxDostawca2.TabIndex = 45;
            linkLabelDostawca2.TabIndex = 46;
            richTextBoxDostawca2.TabIndex = 47;
            //przyciski zapisz/edytuj itp
            buttonNowa_mat.TabIndex = 48;
            buttonZapisz_mat.TabIndex = 49;
            buttonAnuluj_mat.TabIndex = 50;
            buttonUsun_mat.TabIndex = 51;
            //sortowanie Materiału po radio buttonach
            radioButtonNazwa_mat.TabIndex = 52;
            radioButtonTyp_mat.TabIndex = 53;
            radioButtonCena_mat.TabIndex = 54;
            radioButtonMagazyn_ilosc_mat.TabIndex = 55;
            //wyszukiwanie Materiału po wpisanej nazwie
            textBoxWyszukaj_mat.TabIndex = 56;
            buttonSzukaj_mat.TabIndex = 57;

            //------------------------------------------------ Zakładka Operator
            //dane forlumarza
            listBoxOperator.TabIndex = 58;
            textBoxImieOperator.TabIndex = 59;
            textBoxNazwiskoOperator.TabIndex = 60;
            comboBoxDzialOperator.TabIndex = 61;
            textBoxUprawnienieOperator.TabIndex = 62;
            dateTimePickerDataKoncaUprOp.TabIndex = 63;
            listBoxMaszynyOperatora.TabIndex = 64;
            //przyciski zapisz/edytuj itp
            buttonNowaOperator.TabIndex = 65;
            buttonZapiszOperator.TabIndex = 66;
            buttonAnulujOperator.TabIndex = 67;
            buttonUsunOperator.TabIndex = 68;
            //wyszukiwanie
            textBoxWyszukiwanieOperator.TabIndex = 69;
            buttonSzukajOperator.TabIndex = 70;
            //sortowanie po radio buttonach
            radioButtonDataKoncaUprOp.TabIndex = 71;

            _MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            _WykorzystanieBUS = new nsAccess2DB.WykorzystanieBUS(_connString);
            _KategoriaBUS = new nsAccess2DB.KategoriaBUS(_connString);
            _DzialBUS = new nsAccess2DB.DzialBUS(_connString);
            _PropozycjaBUS = new nsAccess2DB.PropozycjaBUS(_connString);
            _Stan_technicznyBUS = new nsAccess2DB.Stan_technicznyBUS(_connString);
            _Osoba_zarzadzajacaBUS = new nsAccess2DB.Osoba_zarzadzajacaBUS(_connString);
            _OperatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            _Maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            _MaterialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            _Jednostka_miarBUS = new nsAccess2DB.Jednostka_miarBUS(_connString);
            _Rodzaj_matBUS = new nsAccess2DB.Rodzaj_matBUS(_connString);

            _MaszynyBUS.select();
            _OperatorBUS.select();
            _Maszyny_OperatorBUS.select();

            WypelnijMaszynyNazwami();
            WypelnijOsoba_zarzadzajaca();
            WypelnijCzestotliwosc();
            WypelnijKategorie();
            WypelnijDzial();
            WypelnijPropozycje();
            WypelnijStan_techniczny();
            WypelnijOperatorow_maszyn(checkedListBoxOperatorzy_maszyn);// wypełniam operatorów maszyn na poczatku uruchomienia programu.
            WypelnijJednostka_miar();
            WypelnijRodzaj_mat();

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }

            if (listBoxMaterialy.Items.Count > 0)
            {
                listBoxMaterialy.SelectedIndex = 0;
            }

            if (listBoxOperator.Items.Count > 0)
            {
                listBoxOperator.SelectedIndex = 0;
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
            _tt.SetToolTip(radioButtonData_kol_przegladu, "Sortuj po dacie kolejnego przeglądu.");
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
            _tt.SetToolTip(radioButtonDataKoncaUprOp, "Sortowanie operatorow po dacie końca uprawnień.");
        }//public SpisForm()

        /// <summary>
        /// Wyświetla komponenty w zależności od indeksu zakładki.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlZakladki_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl v = (TabControl)sender;
            Cursor.Current = Cursors.WaitCursor;

            //zakładka Maszyny
            if (v.SelectedIndex == 0)
            {
                WypelnijMaszynyNazwami();
                WypelnijCzestotliwosc();
                WypelnijKategorie();
                WypelnijDzial();
                WypelnijPropozycje();
                WypelnijStan_techniczny();
            }

            // zakładka Materialy
            if (v.SelectedIndex == 1)
            {
                WypelnijMaterialyNazwami();
                WypelnijJednostka_miar();
                WypelnijRodzaj_mat();
                
                if (listBoxMaterialy.Items.Count > 0)
                {
                    listBoxMaterialy.SelectedIndex = 0;
                }
            }

            // zakładka Normalia
            if (v.SelectedIndex == 2)
            {

            }

            if (v.SelectedIndex == 3)
            { 
                WypelnijOperatorowDanymi();
                WypelnijOperatorowMaszynami();
                WypelnijDzialOperatora();
            }
            Cursor.Current = Cursors.Default;
        } //tabControl1_SelectedIndexChanged

       //  //  //  //  //  //  //  //  //  //  //  //  //-------------------------wyświetlanie w zakładce Maszyny.

        // --------------------------------------- wypełnianie listBoxMaszyny
        //wyświetla listę maszyn po nazwie
        private void WypelnijMaszynyNazwami()
        {
            nsAccess2DB.MaszynyVO VO;
            listBoxMaszyny.Items.Clear();
            _MaszynyBUS.select();

            while (!_MaszynyBUS.eof)
            {
                int idx = 0;
                VO = _MaszynyBUS.VO;
                listBoxMaszyny.Items.Add(VO.Nazwa + " - " + _MaszynyBUS.VO.Nr_fabryczny.ToString());
                idx++;
                _MaszynyBUS.skip();
            }
        }//wypelnijMaszynyNazwami

         /// <summary>
         /// zmiana indeksu w list box maszyny
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void listBoxMaszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(_MaszynyBUS.VO.Zdjecie))
                {
                    File.Delete(_MaszynyBUS.VO.Zdjecie);
                }
            }
            catch { }

            _MaszynyBUS.idx = listBoxMaszyny.SelectedIndex;

            toolStripStatusLabelID_Maszyny.Text = _MaszynyBUS.VO.Identyfikator.ToString(); // toolStripStatusLabelID_Maszyny
            listBoxMaszyny.Tag = _MaszynyBUS.VO.Identyfikator;
            comboBoxKategoria.Text = _MaszynyBUS.VO.Kategoria;

            textBoxNazwa.Text = _MaszynyBUS.VO.Nazwa;
            textBoxTyp.Text = _MaszynyBUS.VO.Typ;
            textBoxNr_inwentarzowy.Text = _MaszynyBUS.VO.Nr_inwentarzowy;
            textBoxNr_fabryczny.Text = _MaszynyBUS.VO.Nr_fabryczny;
            textBoxRok_produkcji.Text = _MaszynyBUS.VO.Rok_produkcji;
            textBoxProducent.Text = _MaszynyBUS.VO.Producent;

            linkLabelNazwaZdjecia.Text = _MaszynyBUS.VO.Zdjecie;// zdjęcie nazwa
            
           _zawartoscPliku = _MaszynyBUS.VO.Zawartosc_pliku; //zawartość zdjęcia

            //wyświetlanie pliku w pictureBox1
            //MemoryStream mStream = new MemoryStream();
            //pictureBox1.Image.Save(System.Drawing.Imaging.ImageFormat.Bmp);
            //_MaszynyBUS.VO.Zawartosc_pliku.ToArray();
            //pictureBox1.Image = 



            comboBoxOsoba_zarzadzajaca.Text = _MaszynyBUS.VO.Nazwa_os_zarzadzajaca;

            textBoxNr_pom.Text = _MaszynyBUS.VO.Nr_pom;
            comboBoxDzial.Text = _MaszynyBUS.VO.Dzial;
            textBoxNr_prot_BHP.Text = _MaszynyBUS.VO.Nr_prot_BHP;

            dateTimePickerData_ost_przegl.Value = new DateTime(_MaszynyBUS.VO.Rok_ost_przeg, _MaszynyBUS.VO.Mc_ost_przeg, _MaszynyBUS.VO.Dz_ost_przeg);
            dateTimePickerData_kol_przegl.Value = new DateTime(_MaszynyBUS.VO.Rok_kol_przeg, _MaszynyBUS.VO.Mc_kol_przeg, _MaszynyBUS.VO.Dz_kol_przeg);

            richTextBoxUwagi.Text = _MaszynyBUS.VO.Uwagi;
            comboBoxWykorzystanie.Text = _MaszynyBUS.VO.Wykorzystanie;
            comboBoxStan_techniczny.Text = _MaszynyBUS.VO.Stan_techniczny;
            comboBoxPropozycja.Text = _MaszynyBUS.VO.Propozycja;

            // wypełnia operatorów maszyny w polu checkedListBoxOperatorzy_maszyn.    *****************************
          
            _OperatorBUS.select();
            _Maszyny_OperatorBUS.select((int)listBoxMaszyny.Tag);

            for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++)
            {
                checkedListBoxOperatorzy_maszyn.SetItemChecked(i, false);
            }

            int idx = -1; // int idx = listBoxMaszyny.SelectedIndex;

            while (!_Maszyny_OperatorBUS.eof)
            {
                idx = _OperatorBUS.getIdx(_Maszyny_OperatorBUS.VO.ID_op_maszyny);
                if (idx > -1) checkedListBoxOperatorzy_maszyn.SetItemChecked(idx, true);
                _Maszyny_OperatorBUS.skip();
            }

            }//listBoxMaszyny_SelectedIndexChanged


        //************* wypełnia CheckedListBox nazwiskami i imionami operatorów maszyn.
        private void WypelnijOperatorow_maszyn(CheckedListBox v)
        {
            v.Items.Clear();
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            operatorBUS.select();
            _Maszyny_OperatorBUS.select();

            while (!operatorBUS.eof)
            {
                v.Items.Add(operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                operatorBUS.skip();
            }

            if(v.Items.Count > 0)
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
                comboBoxKategoria.Items.Add(VO.NazwaKategoria);
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
                comboBoxOsoba_zarzadzajaca.Items.Add(VO.Nazwa_os_zarzadzajaca);
                _Osoba_zarzadzajacaBUS.skip();
            }
        }//WypełnijOsobyOdp()

        private void comboBox_Osoba_zarzadzajaca_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Osoba_zarzadzajacaBUS.idx = comboBoxOsoba_zarzadzajaca.SelectedIndex;
        }// comboBox_Osoba_zarzadzajaca_SelectedIndexChanged

        //////////////////////////////////////////////////////////////         Rabio buttony
        private void radioButtonNazwa_CheckedChanged(object sender, EventArgs e)
        {
            listBoxMaszyny.Items.Clear();

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            _MaszynyBUS.select();
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

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
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
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
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
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
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

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
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
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
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
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
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

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            _MaszynyBUS.selectQuery("SELECT * FROM Maszyny WHERE Nazwa LIKE '" + textBoxWyszukiwanie.Text + "%' OR Nazwa LIKE '%" + textBoxWyszukiwanie.Text + "%';");
            //Nr_inwentarzowy LIKE '" + textBoxWyszukiwanie.Text + "%' OR
            while (!_MaszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(_MaszynyBUS.VO.Nazwa + " -> " + _MaszynyBUS.VO.Nr_inwentarzowy);
                _MaszynyBUS.skip();
            }

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }

        }//buttonSzukaj_Click

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

        // ----------------------------------------Operator
        private void WypelnijDzialOperatora()
        {
            nsAccess2DB.DzialVO VO;
            comboBoxDzialOperator.Items.Clear();

            _DzialBUS.select();
            _DzialBUS.top();
            while (!_DzialBUS.eof)
            {
                VO = _DzialBUS.VO;
                comboBoxDzialOperator.Items.Add(VO.Nazwa);
                _DzialBUS.skip();
            }
        }//WypelnijDzialOperatora()
        private void comboBoxDzial_operator_maszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            _DzialBUS.idx = comboBoxDzialOperator.SelectedIndex;
            comboBoxDzialOperator.Tag = _DzialBUS.VO.Nazwa;
        }//comboBoxDzial_SelectedIndexChanged


        /// <summary>
        /// wypełnia listbox pozycjami częstotliwości Wykorzystania
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WypelnijCzestotliwosc()
        {
            nsAccess2DB.WykorzystanieVO VO;
            comboBoxWykorzystanie.Items.Clear();

            _WykorzystanieBUS.select();
            _WykorzystanieBUS.top();
            while (!_WykorzystanieBUS.eof)
            {
                VO = _WykorzystanieBUS.VO;
                comboBoxWykorzystanie.Items.Add(VO.Wykorzystanie);
                _WykorzystanieBUS.skip();
            }
        }// wypelnijCzestotliwosc

        // przypisuje w combo identyfikator = nazwę częstotliwości wykorzystania
        private void ComboBoxWykorzystanie_SelectedIndexChanged(object sender, EventArgs e)
        {
            _WykorzystanieBUS.idx = comboBoxWykorzystanie.SelectedIndex;
            comboBoxWykorzystanie.Tag = _WykorzystanieBUS.VO.Wykorzystanie;
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
        }// comboBoxStan_techniczny_SelectedIndexChanged

        //przycisk Nowa czyści formularz
        private void ButtonNowa_Click(object sender, EventArgs e)
        {
            // toolStripStatusLabelID_Maszyny.Text = "";

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
            _zawartoscPliku = new byte[] { };

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

            //WypelnijOperatorow_maszyn(checkedListBoxOperator_maszyny); //************************************
            for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++) //czyści checkboxy operatorzy maszyny
            {
                checkedListBoxOperatorzy_maszyn.SetItemChecked(i, false);
            }

            _statusForm = (int)_status.nowy;

            //WypelnijMaszynyNazwami();

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

            linkLabelNazwaZdjecia.Text = _MaszynyBUS.VO.Zdjecie;
            _zawartoscPliku = _MaszynyBUS.VO.Zawartosc_pliku;
            //pictureBox1.Text = string.Empty;

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

            WypelnijMaszynyNazwami();
            listBoxMaszyny.SelectedIndex = idx;

            _statusForm = (int)_status.edycja;

        }//buttonAnuluj_Click

        private void buttonUsun_Click(object sender, EventArgs e)
        {
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
            }
            catch { }

            _MaszynyBUS.delete((int)listBoxMaszyny.Tag);// usunięcie z tabeli maszyna
            _Maszyny_OperatorBUS.delete((int)listBoxMaszyny.Tag); // usunięcie z tabeli relacji maszyna operator

            WypelnijMaszynyNazwami();
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

                maszynyVO.Zdjecie = linkLabelNazwaZdjecia.Text;  //zdjęcie nazwa                ???????????????? zdjęcie
                maszynyVO.Zawartosc_pliku = _zawartoscPliku;//zdjęcie zawartość

                maszynyVO.Nazwa_os_zarzadzajaca = comboBoxOsoba_zarzadzajaca.Text.Trim();
                maszynyVO.Nr_pom = textBoxNr_pom.Text;
                maszynyVO.Dzial = comboBoxDzial.Text;
                maszynyVO.Nr_prot_BHP = textBoxNr_prot_BHP.Text;
                maszynyVO.Rok_ost_przeg = dateTimePickerData_ost_przegl.Value.Year;
                maszynyVO.Mc_ost_przeg = dateTimePickerData_ost_przegl.Value.Month;
                maszynyVO.Dz_ost_przeg = dateTimePickerData_ost_przegl.Value.Day;
                maszynyVO.Data_ost_przegl = int.Parse(maszynyVO.Rok_ost_przeg.ToString() + maszynyVO.Mc_ost_przeg.ToString("00") + maszynyVO.Dz_ost_przeg.ToString("00"));
                DateTime dt = new DateTime(dateTimePickerData_ost_przegl.Value.Ticks); // dodaje interwał przeglądów do data_ost_przegl
                dt = dt.AddYears(_interwalPrzegladow);
                maszynyVO.Rok_kol_przeg = dt.Year;
                maszynyVO.Mc_kol_przeg = dt.Month;
                maszynyVO.Dz_kol_przeg = dt.Day;
                maszynyVO.Data_kol_przegl = int.Parse(dt.Year.ToString() + dt.Month.ToString("00") + dt.Day.ToString("00"));
                maszynyVO.Uwagi = richTextBoxUwagi.Text.Trim();
                maszynyVO.Wykorzystanie = comboBoxWykorzystanie.Text;
                maszynyVO.Stan_techniczny = comboBoxStan_techniczny.Text;
                maszynyVO.Propozycja = comboBoxPropozycja.Text;

                maszynyBUS.write(maszynyVO);
                maszyny_operatorBUS.select(maszynyVO.Identyfikator);

                // Zapis operatorów/operatora maszyny przypisanych do maszyny.
                operatorBUS.select();

                for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++)
                {
                    if (checkedListBoxOperatorzy_maszyn.GetItemChecked(i))
                    {
                        operatorBUS.idx = i;
                        maszyny_operatorBUS.insert(maszynyBUS.VO.Identyfikator + 1, operatorBUS.VO.Identyfikator);
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

                    maszynyVO.Zdjecie = linkLabelNazwaZdjecia.Text;  //zdjęcie nazwa                ???????????????? zdjęcie
                    maszynyVO.Zawartosc_pliku = _zawartoscPliku;//zdjęcie zawartość

                    maszynyVO.Nazwa_os_zarzadzajaca = comboBoxOsoba_zarzadzajaca.Text.Trim();
                    maszynyVO.Nr_pom = textBoxNr_pom.Text;
                    maszynyVO.Dzial = comboBoxDzial.Text;
                    maszynyVO.Nr_prot_BHP = textBoxNr_prot_BHP.Text;
                    maszynyVO.Rok_ost_przeg = dateTimePickerData_ost_przegl.Value.Year;
                    maszynyVO.Mc_ost_przeg = dateTimePickerData_ost_przegl.Value.Month;
                    maszynyVO.Dz_ost_przeg = dateTimePickerData_ost_przegl.Value.Day;
                    maszynyVO.Data_ost_przegl = int.Parse(maszynyVO.Rok_ost_przeg.ToString() + maszynyVO.Mc_ost_przeg.ToString("00") + maszynyVO.Dz_ost_przeg.ToString("00"));
                    DateTime dt = new DateTime(dateTimePickerData_ost_przegl.Value.Ticks); // dodaje interwał przeglądów do data_ost_przegl
                    dt = dt.AddYears(_interwalPrzegladow);
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
                            maszyny_operatorBUS.insert(maszynyVO.Identyfikator, operatorBUS.VO.Identyfikator);
                        }
                    }
                }
                listBoxMaszyny.SelectedIndex = maszynyBUS.getIdx(maszynyBUS.VO.Identyfikator);// ustawienie zaznaczenia w tabeli maszyn.
            }//else if - edycja

            //maszyny_operatorBUS.select(maszynyVO.Identyfikator);

            // Zapis operatorów/operatora maszyny przypisanych do maszyny.
            //operatorBUS.select();


            //for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++)
            //{
            //    if (checkedListBoxOperatorzy_maszyn.GetItemChecked(i))
            //    { 
            //        operatorBUS.idx = i;
            //        if (_statusForm == (int)_status.edycja)
            //        {
            //            maszyny_operatorBUS.insert(maszynyBUS.VO.Identyfikator, operatorBUS.VO.Identyfikator);
            //        }
            //        else if (_statusForm == (int)_status.nowy)
            //        {
            //            maszyny_operatorBUS.insert(maszynyVO.Identyfikator, operatorBUS.VO.Identyfikator);
            //        }
            //    }
            //}

            //Wybierz na liście maszynę.
            WypelnijMaszynyNazwami();


            MessageBox.Show("Pozycja zapisana w bazie", "komunikat", MessageBoxButtons.OK, MessageBoxIcon.Information);


            _statusForm = (int)_status.edycja;
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

        private void toolStripButtonOs_zarzadzajaca_Click(object sender, EventArgs e)
        {
            Os_zarzadzajacaForm frame = new Os_zarzadzajacaForm();
            frame.Show();
        }
        /// <summary>
        /// Ppokazuje zdjęcie po kliknięciu na link.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelNazwaZdjecia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pokazZdjecie(linkLabelNazwaZdjecia.Text);
        } //linkLabelNazwaZdjecia_LinkClicked

        /// <summary>
        /// Sprawdza istnienie zdjęcia.
        /// </summary>
        /// <param name="zdjecie"></param>
        /// <returns>Zwraca wartość logiczną istnienia pliku.</returns>
        private bool zdjecieIstnieje(string zdjecie)
        {
            FileInfo fi = new FileInfo(zdjecie);
            return fi.Exists;
        }// zdjecieIstnieje

        /// <summary>
        /// Zwraca obiekt informacji o napędzie dostepny na stacji.
        /// </summary>
        /// <returns>Obiekt informacji o napędzie.</returns>
        private DirectoryInfo zwrocNaped()
        {
            DirectoryInfo di;
            string[] napedy = new string[] { "C:\\", "D:\\", "E:\\", "F:\\", "G:\\", "H:\\" };

            for (int i = 0; i < napedy.Length; i++)
            {
                di = new DirectoryInfo(napedy[i]);
                if (di.Exists)
                {
                    return di;
                }
            }
            return null;
        }//zwrocNaped

        private bool dirIstnieje(string sciezka)
        {
            DirectoryInfo di = new DirectoryInfo(sciezka);
            return di.Exists;
        }//dirIstnieje
        /// <summary>
        /// Tworzy plik - zdjęcia na dysku.
        /// </summary>
        /// <param name="zdjecie"></param>
        private void zapiszZdjecieNaDysku(string zdjecie)
        {
            FileStream fs = new FileStream(zdjecie, FileMode.OpenOrCreate, FileAccess.Read);
            _zawartoscPliku = new byte[fs.Length];
            fs.Read(_zawartoscPliku, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();

        }//zapiszZdjecieNaDysku

        private void pokazZdjecie(string zdjecie)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (zdjecie.Length == 0)
                {
                    MessageBox.Show("Pusta nazwa zdjęcia zapisanego w bazie", "RemaGUM",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                DirectoryInfo di = zwrocNaped(); //napęd dostępny na stacji.
                if (di != null)
                {
                    _dirPelnaNazwa = di.FullName + _dirNazwa;
                    string pelnaNazwaPliku = _dirPelnaNazwa + "\\" + zdjecie;

                    if (zdjecieIstnieje(pelnaNazwaPliku))
                    {
                        System.Diagnostics.Process.Start(pelnaNazwaPliku);
                        return;
                    }

                    if (!dirIstnieje(_dirPelnaNazwa))
                    {
                        Directory.CreateDirectory(_dirPelnaNazwa);
                    }

                    if (dirIstnieje(_dirPelnaNazwa))
                    {
                        int dlugosc = _zawartoscPliku.Length;

                        FileStream fs = new FileStream(pelnaNazwaPliku, FileMode.OpenOrCreate, FileAccess.Write);
                        fs.Write(_zawartoscPliku, 0, dlugosc);
                        fs.Close();

                        System.Diagnostics.Process.Start(pelnaNazwaPliku);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z prezentacją zdjęcia. Błąd: " + ex.Message);
                Cursor = Cursors.Default;
            }
            Cursor = Cursors.Default;
        }//pokazZdjecie

        //button Wgraj/pokaż
        private void buttonPokazZdj_Click(object sender, EventArgs e)
        {
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
                    _MaszynyBUS.VO.Zawartosc_pliku = _zawartoscPliku; //zawartosc zdjęcia
                    _MaszynyBUS.VO.Zdjecie = fi.Name;// nazwa zdjęcia
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z prezentacją zdjęcia. Błąd: " + ex.Message);
                }
            }
        }//buttonPokazZdj_Click

        private void buttonUsunZdj_Click(object sender, EventArgs e)
        {
            _MaszynyBUS.VO.Zawartosc_pliku = new byte[] { };
            _MaszynyBUS.VO.Zdjecie = string.Empty;
            linkLabelNazwaZdjecia.Text = string.Empty;
        }//buttonUsunZdj_Click




        //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  ------------wyświetlanie w zakładce Materiały.
        // --------------------------------------- wypełnianie  listBoxMaterialy.
        // wyświetla listę Materiałów po nazwie
        private void WypelnijMaterialyNazwami()
        {
            nsAccess2DB.MaterialyVO VO;
            listBoxMaterialy.Items.Clear();
            _MaterialyBUS.select();

            while (!_MaterialyBUS.eof)
            {
                VO = _MaterialyBUS.VO;
                listBoxMaterialy.Items.Add(VO.Nazwa_mat + " - " + VO.Stan_mat + " " + VO.Jednostka_miar_mat);
                _MaterialyBUS.skip();
            }
        }// WypelnijMaterialyNazwami()
       
        /// <summary>
        /// zmiana indeksu w list box Materialy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxMaterialy_SelectedIndexChanged(object sender, EventArgs e)
        {

            _MaterialyBUS.idx = listBoxMaterialy.SelectedIndex;

            toolStripStatusLabelID_Materialu.Text = _MaterialyBUS.VO.Identyfikator.ToString(); //  toolStripStatusLabelID_Materialu
            listBoxMaterialy.Tag = _MaterialyBUS.VO.Identyfikator;
            textBoxTyp_materialu.Text = _MaterialyBUS.VO.Typ_mat;
            comboBoxRodzaj.Text = _MaterialyBUS.VO.Rodzaj_mat;
            textBoxNazwa_materialu.Text = _MaterialyBUS.VO.Nazwa_mat;
            comboBoxJednostka_mat.Text = _MaterialyBUS.VO.Jednostka_miar_mat;
            textBoxMagazyn_mat.Text = _MaterialyBUS.VO.Stan_mat.ToString();
            textBoxZuzycie.Text = _MaterialyBUS.VO.Zuzycie_mat.ToString();
            textBoxOdpad.Text = _MaterialyBUS.VO.Odpad_mat.ToString();
            textBoxMin_materialu.Text = _MaterialyBUS.VO.Stan_min_mat.ToString();
            textBoxZapotrzebowanie.Text = _MaterialyBUS.VO.Zapotrzebowanie_mat.ToString();
            //dane dostawców Materiałów
            comboBoxDostawca1.Text = _MaterialyBUS.VO.Dostawca_mat;

            //TODO zrobić kwerendę z dostawcami i ją uruchomić
            //linkLabelDostawca1.Text = _Dostawca_matBUS.VO.Link_dostawca_mat;
            //richTextBoxDostawca1.Text = _Dostawca_matBUS.VO.Dod_info_dostawca_mat;

            comboBoxDostawca2.Text = _MaterialyBUS.VO.Dostawca_mat;
            //TODO zrobić kwerendę z dostawcami i ją uruchomić
            //linkLabelDostawca2.Text = _Dostawca_matBUS.VO.Link_dostawca_mat;
            //richTextBoxDostawca2.Text = _Dostawca_matBUS.VO.Dod_info_dostawca_mat;

        } // listBoxMaterialy_SelectedIndexChanged

              
       
        private void WypelnijJednostka_miar()
        {
            nsAccess2DB.Jednostka_miarVO VO;
            comboBoxJednostka_mat.Items.Clear();

            _Jednostka_miarBUS.select();
            _Jednostka_miarBUS.top();
            while (!_Jednostka_miarBUS.eof)
            {
                VO = _Jednostka_miarBUS.VO;
                comboBoxJednostka_mat.Items.Add(VO.Nazwa_jednostka_miar);
                _Jednostka_miarBUS.skip();
            }
        }// WypelnijJednostka_miar()

        private void comboBoxJednostka_mat_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Jednostka_miarBUS.idx = comboBoxJednostka_mat.SelectedIndex;
        }// comboBoxJednostka_mat_SelectedIndexChanged

        private void WypelnijRodzaj_mat()
        {
            nsAccess2DB.Rodzaj_matVO VO;
            comboBoxRodzaj.Items.Clear();
            
            _Rodzaj_matBUS.select();
            _Rodzaj_matBUS.top();
            while (!_Rodzaj_matBUS.eof)
            {
                VO = _Rodzaj_matBUS.VO;
                comboBoxRodzaj.Items.Add(VO.Nazwa_rodzaj_mat);
                _Rodzaj_matBUS.skip();
            }
        }
        private void comboBoxRodzaj_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Rodzaj_matBUS.idx = comboBoxJednostka_mat.SelectedIndex;
        }

        // --------- --------------------------------------- Formularz Materialy
        private void ButtonNowa_mat_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelID_Materialu.Text = string.Empty;

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
            WypelnijMaterialyNazwami();
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
            _MaterialyBUS.delete((int)listBoxMaterialy.Tag);

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

            if (toolStripStatusLabelID_Maszyny.Text == string.Empty) //nowa pozycja w tabeli materialów
            {
                Mat_VO.Identyfikator = -1;
            }
            else
                Mat_VO.Identyfikator = int.Parse(toolStripStatusLabelID_Maszyny.Text);

            buttonUsun.Enabled = listBoxMaterialy.Items.Count > 0;

            if (toolStripStatusLabelID_Maszyny.Text == string.Empty)
            {
                listBoxMaterialy.SelectedIndex = listBoxMaterialy.Items.Count - 1;
            }
            else
            {
                listBoxMaszyny.SelectedIndex = _MaterialyBUS.getIdx(Mat_VO.Identyfikator);
            }

            _MaterialyBUS.write(Mat_VO);

            MessageBox.Show("Pozycja zapisana w bazie", "komunikat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            WypelnijMaterialyNazwami();
        }//buttonZapisz_mat_Click


        //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  //  ---------------------------Operatorzy maszyn.
        //wyświetla listę operatorów maszyn po imieniu i nazwisku
        private void WypelnijOperatorowDanymi()
        {
            nsAccess2DB.OperatorBUS operator_maszynyBUS = new nsAccess2DB.OperatorBUS(_connString);
            _OperatorBUS.selectQuery("SELECT * FROM Operator;");
            listBoxOperator.Items.Clear();

            while (!_OperatorBUS.eof)
            {
                listBoxOperator.Items.Add(_OperatorBUS.VO.Op_nazwisko + " " + _OperatorBUS.VO.Op_imie);
                _OperatorBUS.skip();
            }

            if (listBoxOperator.Items.Count > 0)
            {
                listBoxOperator.SelectedIndex = 0;
            }
        }//WypelnijOperatorowDanymi()
         
        /// <summary>
         /// zmiana indeksu w list box operator maszyny
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void listBoxOperator_maszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            _OperatorBUS.idx = listBoxOperator.SelectedIndex;

            listBoxOperator.Tag = _OperatorBUS.VO.Identyfikator;
                      
            textBoxImieOperator.Text = _OperatorBUS.VO.Op_imie;
            textBoxNazwiskoOperator.Text = _OperatorBUS.VO.Op_nazwisko;
            comboBoxDzialOperator.Text = _OperatorBUS.VO.Nazwa_dzial;
            textBoxUprawnienieOperator.Text = _OperatorBUS.VO.Uprawnienie;
            dateTimePickerDataKoncaUprOp.Value = new DateTime(_OperatorBUS.VO.Rok, _OperatorBUS.VO.Mc, _OperatorBUS.VO.Dzien);

            toolStripStatusLabelIDOperatora.Text = _OperatorBUS.VO.Identyfikator.ToString(); // ID operatora maszyny

            //wypełnij listę maszyn obsługiwanych przez wybranego operatora - zmiana indeksu operatora ma zmieniać listę podległych mu maszyn.
            nsAccess2DB.Maszyny_OperatorBUS maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            listBoxMaszynyOperatora.Items.Clear();

            maszynyBUS.select();
            maszyny_OperatorBUS.select();

          
            _maszynaTag = new int[maszyny_OperatorBUS.count];// przechowuje ID_maszyny.
            int idx = 0;

            while (!maszyny_OperatorBUS.eof)
            {
                listBoxMaszynyOperatora.Items.Add(maszyny_OperatorBUS.VO.ID_maszyny); //TODO zmienić na nazwę maszyny
                _maszynaTag[idx] = maszyny_OperatorBUS.VO.ID_maszyny;
                maszyny_OperatorBUS.skip();
                idx++;
            }

        }//listBoxOperator_maszyny_SelectedIndexChanged

        // wyświetla listę maszyn dla danego operatora
        private void WypelnijOperatorowMaszynami()
        {
            listBoxMaszynyOperatora.Items.Clear();
          
            _MaszynyBUS.select();
            _Maszyny_OperatorBUS.select((int)_OperatorBUS.VO.Identyfikator);

            while (!_MaszynyBUS.eof)
            {
                listBoxMaszynyOperatora.Items.Add(_MaszynyBUS.VO.Nazwa);
                _MaszynyBUS.skip();
            }

            if (listBoxMaszynyOperatora.Items.Count > 0)
            {
                _MaszynyBUS.idx = 0;
                listBoxMaszynyOperatora.SelectedIndex = 0;
                listBoxMaszynyOperatora.Tag = _MaszynyBUS.VO.Identyfikator;
            }
        }//WypelnijOperatorowMaszynami()




        ///////////////////////////////////////////////////////////////////// // // // ///  Przyciski

        private void buttonNowaOperator_Click(object sender, EventArgs e)
        {
            textBoxImieOperator.Text = string.Empty;
            textBoxNazwiskoOperator.Text = string.Empty;

            comboBoxDzialOperator.SelectedIndex = -1;
            comboBoxDzialOperator.Enabled = true;
            comboBoxDzialOperator.SelectedIndex = 0;
            comboBoxDzialOperator.Refresh();

            textBoxUprawnienieOperator.Text = string.Empty;
            dateTimePickerDataKoncaUprOp.Text = string.Empty;

            buttonAnuluj.Enabled = true;
        }//WypelnijOperatorowDanymi();

        private void buttonZapiszOperator_Click(object sender, EventArgs e)
        {
            if (textBoxNazwiskoOperator.Text == string.Empty) // textBoxNazwisko nie może być puste
            {
                MessageBox.Show("Proszę uzupełnij nazwisko operatora maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxImieOperator.Text == string.Empty)// textBoxImie nie może być puste
            {
                MessageBox.Show("Proszę uzupełnij imię operatora maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            buttonNowa.Enabled = true; //nieaktywny przycisk nowa przy zapisie
            buttonUsun.Enabled = true; //nieaktywny przycisk Usuń przy zapisie

            nsAccess2DB.OperatorVO _operatorVO = new nsAccess2DB.OperatorVO();
            _operatorVO.Op_imie = textBoxImieOperator.Text;
            _operatorVO.Op_nazwisko = textBoxNazwiskoOperator.Text;
            _operatorVO.Nazwa_dzial = comboBoxDzialOperator.Text;
            _operatorVO.Uprawnienie = textBoxUprawnienieOperator.Text;
            //_operatorVO.Rok = dateTimePickerDataKoncaUprOp.Value.Year;
            //_operatorVO.Mc = dateTimePickerDataKoncaUprOp.Value.Month;
            //_operatorVO.Dzien = dateTimePickerDataKoncaUprOp.Value.Day;
            _operatorVO.Data_konca_upr = (_operatorVO.Rok.ToString() + _operatorVO.Mc.ToString("00") + _operatorVO.Dzien.ToString("00"));
         
            if (toolStripStatusLabelIDOperatora.Text == string.Empty) //nowa pozycja w tabeli operator maszyny
            {
                _operatorVO.Identyfikator = -1;
            }
            else
            {
                _operatorVO.Identyfikator = int.Parse(toolStripStatusLabelIDOperatora.Text);
            }

            if (toolStripStatusLabelIDOperatora.Text == string.Empty)
            {
                listBoxOperator.SelectedIndex = listBoxOperator.Items.Count - 1;
            }
            else
            {
                listBoxOperator.SelectedIndex = _OperatorBUS.getIdx(_operatorVO.Identyfikator);
            }

            _OperatorBUS.write(_operatorVO);
            _Maszyny_OperatorBUS.write(_Maszyny_OperatorBUS.VO);
            //TODO write w Operator_maszyny_Maszyny

            MessageBox.Show("Pozycja zapisana w bazie", "komunikat", MessageBoxButtons.OK, MessageBoxIcon.Information);

            WypelnijOperatorowDanymi();
            WypelnijOperatorowMaszynami();
        }// buttonZapiszOperator_Click

        private void buttonAnulujOperator_Click(object sender, EventArgs e)
        {
            int idx = listBoxOperator.SelectedIndex;
            textBoxImieOperator.Text = string.Empty;
            textBoxNazwiskoOperator.Text = string.Empty;
            comboBoxDzialOperator.Text = string.Empty;
            textBoxUprawnienieOperator.Text = string.Empty;
            dateTimePickerDataKoncaUprOp.Text = string.Empty;

            WypelnijOperatorowDanymi();
            WypelnijOperatorowMaszynami();
            listBoxOperator.SelectedIndex = idx;
        } //buttonAnulujOperator_Click

        private void buttonUsunOperator_Click(object sender, EventArgs e)
        {
            try
            {
                _OperatorBUS.delete((int)listBoxOperator.Tag);
                textBoxImieOperator.Text = string.Empty;
                comboBoxDzialOperator.Text = string.Empty;
                textBoxUprawnienieOperator.Text = string.Empty;
                dateTimePickerDataKoncaUprOp.Text = string.Empty;
                
            }
            catch { }
            _OperatorBUS.delete((int)listBoxMaszynyOperatora.Tag); //usunięcie pozycji z tabeli operator maszyny.
            _Maszyny_OperatorBUS.delete((int)listBoxMaszynyOperatora.Tag); // usunięcie relacji z tabeli Maszyny_Operator.

            WypelnijOperatorowDanymi();
            
        }// buttonUsunOperator_Click
    }// public partial class SpisForm : Form
       
}//namespace RemaGUM
