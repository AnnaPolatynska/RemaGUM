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

        private nsAccess2DB.MaszynyBUS _MaszynyBUS;
        private nsAccess2DB.KategoriaBUS _KategoriaBUS;
        private nsAccess2DB.Osoba_zarzadzajacaBUS _Osoba_zarzadzajacaBUS;
        private nsAccess2DB.DzialBUS _DzialBUS;
        private nsAccess2DB.WykorzystanieBUS _WykorzystanieBUS;
        private nsAccess2DB.PropozycjaBUS _PropozycjaBUS;
        private nsAccess2DB.Stan_technicznyBUS _Stan_technicznyBUS;
        private nsAccess2DB.Operator_maszynyBUS _Operator_maszynyBUS;
        private nsAccess2DB.Operator_maszyny_MaszynyBUS _Operator_maszyny_MaszynyBUS;

        private nsAccess2DB.MaterialyBUS _MaterialyBUS;

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
            comboBoxOperator_maszyny.TabIndex = 10;
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
            // -------------------------------------- Zakładka Materiały
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


            _MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            _WykorzystanieBUS = new nsAccess2DB.WykorzystanieBUS(_connString);
            _KategoriaBUS = new nsAccess2DB.KategoriaBUS(_connString);
            _DzialBUS = new nsAccess2DB.DzialBUS(_connString);
            _PropozycjaBUS = new nsAccess2DB.PropozycjaBUS(_connString);
            _Stan_technicznyBUS = new nsAccess2DB.Stan_technicznyBUS(_connString);
            _Osoba_zarzadzajacaBUS = new nsAccess2DB.Osoba_zarzadzajacaBUS(_connString);
            _Operator_maszynyBUS = new nsAccess2DB.Operator_maszynyBUS(_connString);
            _Operator_maszyny_MaszynyBUS = new nsAccess2DB.Operator_maszyny_MaszynyBUS(_connString);
            _MaterialyBUS = new nsAccess2DB.MaterialyBUS(_connString);

            _MaszynyBUS.select();
            _Operator_maszynyBUS.select();

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
            _tt.SetToolTip(buttonUsun, "Usuwa pozycję z bazy.");
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl v = (TabControl)sender;
            Cursor.Current = Cursors.WaitCursor;

            //zakładka Maszyny
            if (v.SelectedIndex == 0)
            {
                WypelnijMaszynyNazwami();
                WypelnijOsoba_zarzadzajaca();
                WypelnijCzestotliwosc();
                WypelnijKategorie();
                WypelnijDzial();
                WypelnijPropozycje();
                WypelnijStan_techniczny();
                WypelnijOperator_maszyny();
            }

            // zakładka Materialy
            if (v.SelectedIndex == 1)
            {
                WypelnijMaterialyNazwami();
            }

            // zakładka Normalia
            if (v.SelectedIndex == 2)
            {

            }
            Cursor.Current = Cursors.Default;
        } //tabControl1_SelectedIndexChanged



        //  //  //  //  //  //  //  -------------------------wyświetlanie w liście maszyn
        //wyświetla listę maszyn po nazwie
        private void WypelnijMaszynyNazwami()
        {
            nsAccess2DB.MaszynyVO VO;
            listBoxMaszyny.Items.Clear();
            _MaszynyBUS.select();

            while (!_MaszynyBUS.eof)
            {
                VO = _MaszynyBUS.VO;
                listBoxMaszyny.Items.Add(VO.Nazwa + " - " + _MaszynyBUS.VO.Nr_fabryczny.ToString());
                _MaszynyBUS.skip();
            }
           
        }//wypelnijMaszynyNazwami

        // wyświetla listę Materiałów po nazwie
        private void WypelnijMaterialyNazwami()
        {
            nsAccess2DB.MaterialyVO VO;
            listBoxMaterialy.Items.Clear();
            _MaterialyBUS.select();

            while (!_MaterialyBUS.eof)
            {
                VO = _MaterialyBUS.VO;
                listBoxMaterialy.Items.Add(VO.Nazwa_mat + " - " + _MaterialyBUS.VO.Stan_mat + " " + _MaterialyBUS.VO.Jednostka_miar_mat);
                _MaterialyBUS.skip();
            }
        }

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
            nsAccess2DB.MaszynyBUS MaszynyVO = new nsAccess2DB.MaszynyBUS(_connString);
            nsAccess2DB.Operator_maszynyBUS Operator_maszynyVO = new nsAccess2DB.Operator_maszynyBUS(_connString);
            nsAccess2DB.Operator_maszyny_MaszynyBUS OMMVO = new nsAccess2DB.Operator_maszyny_MaszynyBUS(_connString);

            _MaszynyBUS.idx = listBoxMaszyny.SelectedIndex;
            
            listBoxMaszyny.Tag = _MaszynyBUS.VO.Identyfikator;
            toolStripStatusLabelIDMat.Text = _MaszynyBUS.VO.Identyfikator.ToString();
            comboBoxKategoria.Text = _MaszynyBUS.VO.Kategoria;
            textBoxNazwa.Text = _MaszynyBUS.VO.Nazwa;
            textBoxTyp.Text = _MaszynyBUS.VO.Typ;
            textBoxNr_inwentarzowy.Text = _MaszynyBUS.VO.Nr_inwentarzowy;
            textBoxNr_fabryczny.Text = _MaszynyBUS.VO.Nr_fabryczny;
            textBoxRok_produkcji.Text = _MaszynyBUS.VO.Rok_produkcji;
            textBoxProducent.Text = _MaszynyBUS.VO.Producent;
            pictureBox1.Text = _MaszynyBUS.VO.Zdjecie;

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

            comboBoxOperator_maszyny.Text = _MaszynyBUS.VO.Nazwa_op_maszyny; // nazwa operatora maszyny z tabeli Maszyny
            //comboBoxOperator_maszyny.Text = _Operator_maszynyBUS.VO.Nazwa_op_maszyny;
            //comboBoxOperator_maszyny.Text = _Operator_maszynyBUS.VO.Op_nazwisko + " " + _Operator_maszynyBUS.VO.Op_imie;

        }//listBoxMaszyny_SelectedIndexChanged

        /// <summary>
        /// zmiana indeksu w list box Materialy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxMaterialy_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.MaterialyBUS MatVO = new nsAccess2DB.MaterialyBUS(_connString);

            _MaterialyBUS.idx = listBoxMaterialy.SelectedIndex;
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
            //TODO         linkLabelDostawca1.Text = _MaterialyBUS.VO.Dostawca_mat;
            //richTextBoxDostawca1.Text = _MaterialyBUS.VO.Dostawca_mat;
            comboBoxDostawca2.Text = _MaterialyBUS.VO.Dostawca_mat;
            //linkLabelDostawca2.;
            //richTextBoxDostawca2.;
             
            
        } // listBoxMaterialy_SelectedIndexChanged

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


        // wypełnienia dane słownikowe w comboBoxOperator_maszyny z tabeli Operator_maszyny nazwiskiem i imieniem operatora
        private void WypelnijOperator_maszyny()
        {
            nsAccess2DB.Operator_maszynyVO VO;
            //nsAccess2DB.Operator_maszyny_MaszynyVO operator_maszyny_MaszynyVO;
            comboBoxOperator_maszyny.Items.Clear();

            _Operator_maszynyBUS.select();
            _Operator_maszynyBUS.top();
            while (!_Operator_maszynyBUS.eof)
            {
                VO = _Operator_maszynyBUS.VO;
                comboBoxOperator_maszyny.Items.Add(VO.Op_nazwisko + " " + VO.Op_imie); // dane słownikowe nazwisko + imię
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
        }

        ///////////////////////////////////////////////////////////////////// // // // ///   Button
        //przycisk Nowa czyści formularz
        private void ButtonNowa_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelIDMat.Text = string.Empty;

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

        /// <summary>
        /// Działania po wciśnięciu przycisku Zapisz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonZapisz_Click(object sender, EventArgs e)
        {
            if (textBoxNazwa.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij nazwę", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            buttonNowa.Enabled = true;
                     
            nsAccess2DB.MaszynyVO M_VO = new nsAccess2DB.MaszynyVO();
            nsAccess2DB.Operator_maszynyVO Op_VO = new nsAccess2DB.Operator_maszynyVO();
            nsAccess2DB.Operator_maszyny_MaszynyVO OMM_VO = new nsAccess2DB.Operator_maszyny_MaszynyVO();

            M_VO.Kategoria = comboBoxKategoria.Text;
            M_VO.Nazwa = textBoxNazwa.Text.Trim();
            M_VO.Typ = textBoxTyp.Text.Trim();
            M_VO.Nr_inwentarzowy = textBoxNr_inwentarzowy.Text.Trim();
            M_VO.Nr_fabryczny = textBoxNr_fabryczny.Text.Trim();
            M_VO.Rok_produkcji = textBoxRok_produkcji.Text.Trim();
            M_VO.Producent = textBoxProducent.Text.Trim();
            M_VO.Zdjecie = pictureBox1.Text;  /////                ???????????????? obrazek
            M_VO.Nazwa_os_zarzadzajaca = comboBoxOsoba_zarzadzajaca.Text.Trim();
            M_VO.Nr_pom = textBoxNr_pom.Text;
            M_VO.Dzial = comboBoxDzial.Text;
            M_VO.Nr_prot_BHP = textBoxNr_prot_BHP.Text;
            M_VO.Rok_ost_przeg = dateTimePickerData_ost_przegl.Value.Year;
            M_VO.Mc_ost_przeg = dateTimePickerData_ost_przegl.Value.Month;
            M_VO.Dz_ost_przeg = dateTimePickerData_ost_przegl.Value.Day;
            M_VO.Data_ost_przegl = int.Parse(M_VO.Rok_ost_przeg.ToString() + M_VO.Mc_ost_przeg.ToString("00") + M_VO.Dz_ost_przeg.ToString("00"));

            DateTime dt = new DateTime(dateTimePickerData_ost_przegl.Value.Ticks); // dodaje interwał przeglądów do data_ost_przegl
            dt = dt.AddYears(_interwalPrzegladow);

            M_VO.Rok_kol_przeg = dt.Year;
            M_VO.Mc_kol_przeg = dt.Month;
            M_VO.Dz_kol_przeg = dt.Day;
            M_VO.Data_kol_przegl = int.Parse(dt.Year.ToString() + dt.Month.ToString("00") + dt.Day.ToString("00"));
            M_VO.Uwagi = richTextBoxUwagi.Text.Trim();
            M_VO.Wykorzystanie = comboBoxWykorzystanie.Text;
            M_VO.Stan_techniczny = comboBoxStan_techniczny.Text;
            M_VO.Propozycja = comboBoxPropozycja.Text;


            M_VO.Nazwa_op_maszyny = comboBoxOperator_maszyny.Text.Trim();
           // Op_VO.Nazwa_op_maszyny = comboBoxOperator_maszyny.Text;


            if (toolStripStatusLabelIDMat.Text == string.Empty) //nowa pozycja w tabeli maszyn
            {
                M_VO.Identyfikator = -1;
            }
            else
                M_VO.Identyfikator = int.Parse(toolStripStatusLabelIDMat.Text);

            buttonUsun.Enabled = listBoxMaszyny.Items.Count > 0;

            if (toolStripStatusLabelIDMat.Text == string.Empty)
            {
                listBoxMaszyny.SelectedIndex = listBoxMaszyny.Items.Count - 1;
            }
            else
            {
                listBoxMaszyny.SelectedIndex = _MaszynyBUS.getIdx(M_VO.Identyfikator);
            }
     
             _MaszynyBUS.write(M_VO);
      

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

        private void linkLabelNazwaZdjecia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pokazZdjecie(linkLabelNazwaZdjecia.Text);
        } //linkLabelNazwaZdjecia_LinkClicked

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


        private void pokazZdjecie(string Zdjecie)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (Zdjecie.Length == 0)
                {
                    MessageBox.Show("Pusta nazwa pliku zapisanego w bazie", "RemaGUM",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z prezentacją załącznika. Błąd: " + ex.Message);
                Cursor = Cursors.Default;
            }
            Cursor = Cursors.Default;
        
        }

        private void SpisForm_Load(object sender, EventArgs e)
        {

        }
    }// public partial class SpisForm : Form
       
}//namespace RemaGUM
