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
    /// <summary>
    /// Class SpisForm.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class SpisForm : Form
    {
        // łączenie z bazą danych na sztywno
        //private string _connString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Projects\\RemaGUM\\RemaGUM.mdb"; //połaczenie z bazą danych

        //ustawienia połączenie z bazą poprzez xml.
        private settings _settings;

        /// <summary>
        /// The connection string
        /// </summary>
        private string _connString;

        /// <summary>
        /// The rest
        /// </summary>
        private Rest _rest = new Rest();

        /// <summary>
        /// The help file- plik pomocy RemaGUM.
        /// </summary>
        private string _helpFile = Application.StartupPath + "\\help.chm";

        /// <summary>
        /// zawartosc pliku-dane odczytane z pliku zdjęcia.
        /// </summary>
        private byte[] _zawartoscPliku;

        /// <summary>
        /// dir nazwa -nazwa katalogu tymczasowego.
        /// </summary>
        string _dirNazwa = "C:\\tempRemaGUM";

        /// <summary>
        /// dir pelna nazwa katalogu tymczasowego - pelna nazwa.
        /// </summary>
        string _dirPelnaNazwa = string.Empty;

        /// <summary>
        /// Enum _status- status działania formularza.
        /// </summary>
        enum _status { edycja, nowy };

        /// <summary>
        /// status form - wartośc statusu formularza.
        /// </summary>
        private byte _statusForm;

        /// <summary>
        /// The maszyna tag przechowuje identyfikatory maszyn dla operatora.
        /// </summary>
        private int[] _maszynaTag;

        /// <summary>
        /// The maszyna identifier
        /// </summary>
        int _maszynaId = -1; // identyfikator maszyny przy starcie programu.

        /// <summary>
        /// indeks szukanej maszyny.
        /// </summary>
        int _maszynaSzukajIdx = 0;
        /// <summary>
        /// indeks szukanego dysponenta.
        /// </summary>
        int _dysponentSzukajIdx = 0;
        /// <summary>
        /// indeks szukanego materiału.
        /// </summary>
        int _materialSzukajIdx = 0;
        /// <summary>
        /// </summary>
        private ToolTip _tt;

        /// <summary>
        /// The interwal przegladow
        /// </summary>
        private int _interwalPrzegladow = 365;    //w dniach = 1 rok

        public System.Drawing.Image ErrorImage { get; set; }

        private nsAccess2DB.OperatorBUS _OperatorBUS;
        private nsAccess2DB.MaszynyBUS _maszynyBUS;
        private nsAccess2DB.MaterialyBUS _materialyBUS;

        /// <summary>
        /// Klasa umożliwiająca połączenie bazy danych za pomocą connectionStringa zapisanego w pliku xml 
        /// </summary>
        public class settings
        {
            private string _XMLfile;
            private string _connectionString;   //connectionString
            private string _dBcopyFile;         //pełna ścieżka kopii bazy danych

            private int _copyInterval;          //interwał sporzadzania kopii bazy
            private string _dBhistoryDir;       //pełna nazwa katalogu historii bazy
            private int _historyInterval;       //interwał sprzadzania kopii w katalogu hsitorii bazy

            /// <summary>
            /// Konstruktor wymiany danych ustawień programu w XML.
            /// </summary>
            public settings()
            { }//settings

            /// <summary>
            /// Czyta plik.
            /// </summary>
            public void read()
            {
                read(_XMLfile);
            }//read

            /// <summary>
            /// Czyta plik.
            /// </summary>
            /// <param name="XMLfile">Nazwa pliku xml</param>
            public void read(string XMLfile)
            {
                _XMLfile = XMLfile;
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(XMLfile);
                XmlNode node = doc.FirstChild;

                while (node != null)
                {
                    node = node.NextSibling;
                    if (node.Name.ToUpper() == "settings".ToUpper())
                    {
                        node = node.FirstChild;
                        goto childNodes;
                    }
                }
                childNodes:;
                while (node != null)
                {
                    if (node.Name.ToUpper() == "ConnectionString".ToUpper()) _connectionString = node.InnerText;
                    if (node.Name.ToUpper() == "dBcopyFile".ToUpper()) _dBcopyFile = node.InnerText;
                    if (node.Name.ToUpper() == "copyInterval".ToUpper()) _copyInterval = int.Parse(node.InnerText);
                    if (node.Name.ToUpper() == "dBhistoryDir".ToUpper()) _dBhistoryDir = node.InnerText;
                    if (node.Name.ToUpper() == "historyInterval".ToUpper()) _historyInterval = int.Parse(node.InnerText);
                    node = node.NextSibling;
                }
            }//read

            /// <summary>
            /// Zapisuje do pliku.
            /// </summary>
            public void write()
            {
                try
                {
                    XmlTextWriter writer = new XmlTextWriter(_XMLfile, System.Text.Encoding.UTF8);
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartDocument();

                    writer.WriteStartElement("settings");

                    writer.WriteElementString("ConnectionString", _connectionString);

                    writer.WriteEndDocument();
                    writer.Flush();
                    writer.Close();
                }
                catch
                {
                    MessageBox.Show("Error writing file", "ECP",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }//write

            /// <summary>
            /// Connection string.
            /// </summary>
            public string connectionString
            {
                get { return _connectionString; }
                set { _connectionString = value; }
            }

            /// <summary>
            /// Connection string.
            /// </summary>
            public string XMLfile
            {
                get { return _XMLfile; }
                set { _XMLfile = value; }
            }

            /// <summary>
            /// Pełna ścieżka kopii bazy danych.
            /// </summary>
            public string dBcopyFile
            {
                get { return _dBcopyFile; }
                set { _dBcopyFile = value; }
            }

            /// <summary>
            /// Interwał w minutach sporzadzania kopii bazy danych.
            /// </summary>
            public int copyInterval
            {
                get { return _copyInterval; }
                set { _copyInterval = value; }
            }

            /// <summary>
            /// Pełna nazwa katalogu historii bazy.
            /// </summary>
            public string dBhistoryDir
            {
                get { return _dBhistoryDir; }
                set { _dBhistoryDir = value; }
            }

            /// <summary>
            /// Interwał w dniach sporządzania kopii bazy w katalogu historii.
            /// </summary>
            public int historyInterval
            {
                get { return _historyInterval; }
                set { _historyInterval = value; }
            }


        }//class settings


        /// <summary>
        /// Konstruktor formularza.
        /// </summary>
        /// <param name="connStr">Połaczenie z bazą.</param>
        public SpisForm()
        {        InitializeComponent();

            //połączenie z bazą danych na sztywno
           // nsRest.Rest rest = new nsRest.Rest();
           // _connString += rest.dbConnection(_connString);

            //połączenie z bazą danych przez connection string z xml.
            _settings = new settings();
            _settings.XMLfile = Application.StartupPath + "\\settings.xml";
            _settings.read();

            _connString = _rest.dbConnection(_settings.connectionString);

            _statusForm = (int)_status.edycja;

            _OperatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            _maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            _materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);

            radioButtonNazwa.Checked = true; // przy starcie programu zaznaczone sortowanie po nazwie.
            WypelnijCzestotliwosc();
            WypelnijKategorie();
            WypelnijDzial();
            WypelnijPropozycje();
            WypelnijStan_techniczny();
            WypelnijOperatorow_maszyn(checkedListBoxOperatorzy_maszyn);// wypełniam operatorów maszyn na poczatku uruchomienia programu.
            WypelnijDysponent();

            timer1.Interval = 1000;
            timer1.Start();
           

            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }

            // Indeksowanie w zakładkach
            //------------------------------------ Zakładka Maszyny
            //dane formularza Maszyny
            //listBoxMaszyny.TabIndex = 0;
            comboBoxKategoria.TabIndex = 1;
            textBoxNazwa.TabIndex = 2;
            textBoxTyp.TabIndex = 3;
            textBoxNr_inwentarzowy.TabIndex = 4;
            textBoxNr_fabryczny.TabIndex = 5;
            richTextBoxProducent.TabIndex = 6;
            textBoxRok_produkcji.TabIndex = 7;
            textBoxNr_pom.TabIndex = 8;
            dateTimePickerData_ost_przegl.TabIndex = 9;
            comboBoxDzial.TabIndex = 10;
            //richTextBoxUwaga.TabIndex = 11; //pole nieaktywne na komunikaty systemu
            //dateTimePickerData_kol_przegl.TabIndex = 12;
            comboBoxDysponent.TabIndex = 13;
            checkedListBoxOperatorzy_maszyn.TabIndex = 14;
            richTextBoxUwagi.TabIndex = 15;
            //wyszukiwanie Maszyny po wpisanej nazwie 
            textBoxWyszukiwanie.TabIndex = 16;
            buttonSzukaj.TabIndex = 17;
            //zdjęcie i przyciski dot obsł zdjęcia
            //pictureBox1.TabIndex = 18;
            buttonPokazZdj.TabIndex = 19;
            buttonUsunZdj.TabIndex = 20;
            //ankietka dot. Maszyn
            comboBoxStan_techniczny.TabIndex = 21;
            comboBoxWykorzystanie.TabIndex = 22;
            comboBoxPropozycja.TabIndex = 23;
            textBoxNr_prot_BHP.TabIndex = 24;
            //przyciski zapisz/edytuj itp
            buttonNowa.TabIndex = 25;
            buttonZapisz.TabIndex = 26;
            buttonAnuluj.TabIndex = 27;
            buttonUsun.TabIndex = 28;
            //sortowanie Maszyn po radio buttonach
            radioButtonNazwa.TabIndex = 29;
            radioButtonTyp.TabIndex = 30;
            radioButtonNr_fabryczny.TabIndex = 31;
            radioButtonNr_inwentarzowy.TabIndex = 32;
            radioButtonNr_pomieszczenia.TabIndex = 33;
            radioButtonData_ost_przegl.TabIndex = 34;

            //------------------------------------------------ Zakładka Operatorzy maszyn
            //sortowanie po comboBoxOperator
            comboBoxOperator.TabIndex = 1;
            // lista operatorów
            //listBoxOperator.TabIndex = 2;
            //wyszukiwanie
            textBoxWyszukiwanieOperator.TabIndex = 3;
            buttonSzukajOperator.TabIndex = 4;
            //dane forlumarza
            textBoxImieOperator.TabIndex = 5;
            textBoxNazwiskoOperator.TabIndex = 6;
            comboBoxDzialOperator.TabIndex = 7;
            textBoxUprawnienieOperator.TabIndex = 8;
            dateTimePickerDataKoncaUprOp.TabIndex = 9;
            richTextBoxUprawnieniaOperatora.TabIndex = 10;
            //listBoxMaszynyOperatora.TabIndex = 11;
            //przyciski zapisz/edytuj itp
            buttonNowaOperator.TabIndex = 12;
            buttonZapiszOperator.TabIndex = 13;
            buttonAnulujOperator.TabIndex = 14;
            buttonUsunOperator.TabIndex = 15;

            // ------------------------------------------- Zakładka Dysponenci maszyn.
            //lista dysponentów maszyn
            //listBoxDysponent.TabIndex = 1;
            //wyszukiwanie
            textBoxWyszukiwanieDysponent.TabIndex = 2;
            buttonSzukajDysponent.TabIndex = 3;
            //dane dysponenta
            textBoxImieDysponent.TabIndex = 4;
            textBoxNazwiskoDysponent.TabIndex = 5;
            comboBoxDzialDysponent.TabIndex = 6;
            richTextBoxDysponent_dane.TabIndex = 7;
            //przyciski zapisz/edytuj itp
            buttonNowaDysponent.TabIndex = 8;
            buttonZapiszDysponent.TabIndex = 9;
            buttonAnulujDysponent.TabIndex = 10;
            buttonUsunDysponent.TabIndex = 11;
            //lista maszyn, którymi zarządza dysponent
            //listBoxMaszynyDysponenta.TabIndex = 12;

            // --------------------------------------------- Zakładka Materiały
            //sortowanie Materiału po radio buttonach
            radioButtonNazwa_mat.TabIndex = 1;
            radioButtonTyp_mat.TabIndex = 2;
            radioButtonStan_min_mat.TabIndex = 3;
            radioButtonMagazyn_ilosc_mat.TabIndex = 4;
            //lista materiałów/ normaliów
            //listBoxMaterialy.TabIndex = 5;
            //wyszukiwanie Materiału po wpisanej nazwie
            textBoxWyszukaj_mat.TabIndex = 6;
            buttonSzukaj_mat.TabIndex = 7;
            //dane formularza Magazyn
            comboBoxWyborMagazyn.TabIndex = 8;
            textBoxNazwaMat.TabIndex = 9;
            textBoxTypMat.TabIndex = 10;
            comboBoxRodzajMat.TabIndex = 11;
            comboBoxJednostkaMat.TabIndex = 12;
            //richTextBoxKomunikatMaterialy.TabIndex = 13;// komunikaty dotyczące dostępności materiału.
            //Gospodarka magazynowa
            textBoxMagazynMat.TabIndex = 14;
            textBoxZuzycieMat.TabIndex = 15;
            textBoxOdpadMat.TabIndex = 16;
            textBoxMinMat.TabIndex = 17;
            textBoxZapotrzebowanieMat.TabIndex = 18;
            //przyciski zapisz/edytuj itp
            buttonNowaMat.TabIndex = 19;
            buttonZapiszMat.TabIndex = 20;
            buttonAnulujMat.TabIndex = 21;
            buttonUsunMat.TabIndex = 22;
            //dane dostawców Materiałów
            checkedListBoxDostawcyMat.TabIndex = 23;
            richTextBoxDaneDodatkoweDostawca.TabIndex = 24;
            linkLabelDostawcaMat2.TabIndex = 25;

            // -----------------------------------------------Zakładka Dostawcy
            //lista dostawców
            //listBoxDostawcy.TabIndex = 1;
            // dane dostawców
            textBoxNazwaDostawcy.TabIndex = 2;
            richTextBoxDostawca.TabIndex = 3;
            textBoxLink.TabIndex = 4;
            linkLabelDostawcaMat.TabIndex = 5;
            //przyciski zapisz/edytuj itp
            buttonNowyDostawca.TabIndex = 6;
            buttonZapiszDostawca.TabIndex = 7;
            buttonAnulujDostawca.TabIndex = 8;
            buttonUsunDostawca.TabIndex = 9;


            // ---------------------------------------------Zakladka Przyrzad
            //lista przyrzadów
            //listBoxPrzyrzady.TabIndex = 1;



            // tool tipy
            _tt = new ToolTip();
            _tt.SetToolTip(listBoxMaszyny, "Lista maszyn, przyrządów i urządzeń itp.");
            _tt.SetToolTip(comboBoxKategoria, "Kategoria maszyn, przyrządów, urządzeń np. maszyny warsztatowe, lub przyrządy pomiarowe.");
            _tt.SetToolTip(textBoxNazwa, "Nazwa maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxTyp, "Typ maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxNr_inwentarzowy, "Numer naklejki GUM (inwentarzowy) maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(textBoxNr_fabryczny, "Numer fabryczny maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(richTextBoxProducent, "Producent maszyny, przyrządu lub urządzenia, dane adresowe, adres strony www i inne dodatkowe dane.");
            _tt.SetToolTip(textBoxRok_produkcji, "Rok wyprodukowania.");
            _tt.SetToolTip(textBoxNr_pom, "Numer pomieszczenia GUM, gdzie znajuje się maszyna, przyrząd lub urządzenie.");
            _tt.SetToolTip(dateTimePickerData_ost_przegl, "Data ostatniej kontroli dostosowania do minimalnych wymagań w zakresie BHP, lub innego dokumentu.");
            _tt.SetToolTip(comboBoxDzial, "Nazwa dział  lista maszyn, przyrządów i urządzeń itp.");
            _tt.SetToolTip(richTextBoxUwaga, "Komunikaty programu dotyczące terminu przeglądu wybranej maszyny. Pole nieaktywne."); //pole nieaktywne na komunikaty systemu
            //_tt.SetToolTip(dateTimePickerData_kol_przegl, "Data kolejnej kontroli dostosowania do minimalnych wymagań w zakresie BHP, lub innego dokumentu.");
            _tt.SetToolTip(comboBoxDysponent, "Osoba zarządzająca maszynami (dysponent maszyny).");
            _tt.SetToolTip(checkedListBoxOperatorzy_maszyn, "Główna osoba użytkująca maszynę (posiadająca odpowiednie uprawnienia).");
            _tt.SetToolTip(richTextBoxUwagi, "Opis wszelkich innych zdarzeń/statusów mających istotny wpływ na maszynę, przyrząd lub urządzenie.");
            //wyszukiwanie Maszyny po wpisanej nazwie 
            _tt.SetToolTip(textBoxWyszukiwanie, "Wpisz jakiej maszyny szukasz.");
            _tt.SetToolTip(buttonSzukaj, "Szukanie w bazie maszyn.");
            //zdjęcie i przyciski dot obsł zdjęcia
            _tt.SetToolTip(pictureBox1, "Zdjęcie maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(buttonPokazZdj, "Przycisk dodania zdjęcia z wybranego katalogu do bazy danych MS ACCESS.");
            _tt.SetToolTip(buttonUsunZdj, "Przycisk usunięcia zdjęcia z bazy danych.");
            //ankietka dot. Maszyn
            _tt.SetToolTip(comboBoxStan_techniczny, "Stan techniczy: złom, do naprawy, dobry.");
            _tt.SetToolTip(comboBoxWykorzystanie, "Częstotliwość wykorzystania: nieuzywana, rzadziej niż kilka razy w roku, kilka razy w roku, kilka razy w okresie pół roku, kilka razy w kwartale, kilka razy w miesiącu.");
            _tt.SetToolTip(comboBoxPropozycja, "Propozycja: do likwidacji, do remontu, zachować.");
            _tt.SetToolTip(textBoxNr_prot_BHP, "Numer nadany w protokole kontroli dostosowania maszyny do minimalnych wymagań w zakresie BHP z dnia 12.06.2006 r.");
            //przyciski zapisz/edytuj itp
            _tt.SetToolTip(buttonNowa, "Nowa pozycja w bazie.");
            _tt.SetToolTip(buttonZapisz, "Zapis nowej maszyny, przyrządu lub urządzenia lub edycja wybranej pozycji.");
            _tt.SetToolTip(buttonAnuluj, "Anulowanie zmiany.");
            _tt.SetToolTip(buttonUsun, "Usuwa pozycję z bazy.");
            //sortowanie Maszyn po radio buttonach
            _tt.SetToolTip(radioButtonNazwa, "Sortuj po nazwie maszyny, przyrządu lub urządzenia.");
            _tt.SetToolTip(radioButtonTyp, "Sortuj po typie.");
            _tt.SetToolTip(radioButtonNr_fabryczny, "Sortuj po numerze fabrycznym.");
            _tt.SetToolTip(radioButtonNr_inwentarzowy, "Sortuj po numerze inwentarzowym.");
            _tt.SetToolTip(radioButtonNr_pomieszczenia, "Sortuj po numerze pomieszczenia.");
            _tt.SetToolTip(radioButtonData_ost_przegl, "Sortuj po dacie ostatniego przegladu.");

            //------------------------------------------------ Zakładka Operatorzy maszyn
            //sortowanie po comboBoxOperator
            _tt.SetToolTip(comboBoxOperator, "Sortowanie operatorow po dacie końca uprawnień.");
            // lista operatorów
            _tt.SetToolTip(listBoxOperator, "Lista wszystkich operatorów maszyn.");
            //wyszukiwanie
            _tt.SetToolTip(textBoxWyszukiwanieOperator, "Wpisz nazwisko operatora, którego szukasz.");
            _tt.SetToolTip(buttonSzukajOperator, "Szukanie w bazie operatora.");
            //dane forlumarza
            _tt.SetToolTip(textBoxImieOperator, "Imię operatora maszyny.");
            _tt.SetToolTip(textBoxNazwiskoOperator, "Nazwisko operatora maszyny.");
            _tt.SetToolTip(comboBoxDzialOperator, "Nazwa działu, do którego należy operator maszyny.");
            _tt.SetToolTip(textBoxUprawnienieOperator, "Rodzaj uprawnienia, jakie posiada operator.");
            _tt.SetToolTip(dateTimePickerDataKoncaUprOp, "Data końca uprawnień operatora maszyny.");
            _tt.SetToolTip(richTextBoxUprawnieniaOperatora, "Uprawnienia jaki posiada wybrany operator.");
            _tt.SetToolTip(listBoxMaszynyOperatora, "Lista obsługiwanych przez operatora maszyn.");
            //przyciski zapisz/edytuj itp
            _tt.SetToolTip(buttonNowaOperator, "Nowa pozycja w bazie.");
            _tt.SetToolTip(buttonZapiszOperator, "Zapis nowego operatora lub edycja wybranej pozycji.");
            _tt.SetToolTip(buttonAnulujOperator, "Anulowanie zmiany.");
            _tt.SetToolTip(buttonUsunOperator, "Usuwa pozycję z bazy.");

            // ------------------------------------------- Zakładka Dysponenci maszyn.
            //lista dysponentów maszyn
            _tt.SetToolTip(listBoxDysponent, "Lista dysponentów maszyn.");
            //wyszukiwanie
            _tt.SetToolTip(textBoxWyszukiwanieDysponent, "Wpisz nazwisko dysponenta, którego szukasz.");
            _tt.SetToolTip(buttonSzukajDysponent, "Szukanie w bazie dysponenta.");
            //dane dysponenta
            _tt.SetToolTip(textBoxImieDysponent, "Imię dysponenta maszyny.");
            _tt.SetToolTip(textBoxNazwiskoDysponent, "Nazwisko dysponenta maszyny.");
            _tt.SetToolTip(comboBoxDzialDysponent, "Nazwa działu, do którego należy dysponent maszyny.");
            _tt.SetToolTip(richTextBoxDysponent_dane, "Dodatkowe dane dysponenta maszyny.");
            //przyciski zapisz/edytuj itp
            _tt.SetToolTip(buttonNowaDysponent, "Nowa pozycja w bazie.");
            _tt.SetToolTip(buttonZapiszDysponent, "Zapis nowego dysponenta lub edycja wybranej pozycji.");
            _tt.SetToolTip(buttonAnulujDysponent, "Anulowanie zmiany.");
            _tt.SetToolTip(buttonUsunDysponent, "Usuwa pozycję z bazy.");
            //lista maszyn, którymi zarządza dysponent
            _tt.SetToolTip(listBoxMaszynyDysponenta, "Maszyny, którymi dysponuje dysponent.");

            // --------------------------------------------- Zakładka Materiały
            //sortowanie Materiału po radio buttonach
            _tt.SetToolTip(radioButtonNazwa_mat, "Sortuj po nazwie materiałów / normaliów.");
            _tt.SetToolTip(radioButtonTyp_mat, "Sortuj po typie materiałów / normaliów.");
            _tt.SetToolTip(radioButtonStan_min_mat, "Sortuj po cenie materiałów / normaliów.");
            _tt.SetToolTip(radioButtonMagazyn_ilosc_mat, "Sortuj po dostępnych ilościach w magazynie.");
            //lista materiałów/ normaliów
            _tt.SetToolTip(listBoxMaterialy, "Lista materiałów / normaliów.");
            //wyszukiwanie Materiału po wpisanej nazwie
            _tt.SetToolTip(textBoxWyszukaj_mat, "Wpisz czego szukasz.");
            _tt.SetToolTip(buttonSzukaj_mat, "Szukanie w bazie materiałów / normaliów.");
            //dane formularza Magazyn
            _tt.SetToolTip(comboBoxWyborMagazyn, "Wybór magazynu materiałów lub normaliów.");
            _tt.SetToolTip(textBoxNazwaMat, "Nazwa materiałów / normaliów.");
            _tt.SetToolTip(textBoxTypMat, "Typ materiałów / normaliów.");
            _tt.SetToolTip(comboBoxRodzajMat, "Rodzaj materiałów / normaliów.");
            _tt.SetToolTip(comboBoxJednostkaMat, "Jednostka miary materiałów / normaliów.");
            _tt.SetToolTip(richTextBoxKomunikatMaterialy, "Komunikaty systemowe, dotyczące dostępności materiału.");// komunikaty dotyczące dostępności materiału.
            //Gospodarka magazynowa
            _tt.SetToolTip(textBoxMagazynMat, "Stan magazynowy materiałów / normaliów.");
            _tt.SetToolTip(textBoxZuzycieMat, "Wpisz ile podlega zużyciu.");
            _tt.SetToolTip(textBoxOdpadMat, "Wpisz ile jest odpadem.");
            _tt.SetToolTip(textBoxMinMat, "Wpisz stan minimalny.");
            _tt.SetToolTip(textBoxZapotrzebowanieMat, "Wpisz zapotrzebowanie materiałów / normaliów.");
            //przyciski zapisz/edytuj itp
            _tt.SetToolTip(buttonNowaMat, "Nowa pozycja w bazie materiałów / normaliów.");
            _tt.SetToolTip(buttonZapiszMat, "Zapis nowej pozycji w bazie materiałów / normaliów lub edycja wybranej pozycji.");
            _tt.SetToolTip(buttonAnulujMat, "Anulowanie zmiany.");
            _tt.SetToolTip(buttonUsunMat, "Usuwa pozycję z bazy.");
            //dane dostawców Materiałów
            _tt.SetToolTip(checkedListBoxDostawcyMat, "Dostawcy wybranego materiału.");
            _tt.SetToolTip(richTextBoxDaneDodatkoweDostawca, "Wszelkie dodatkowe informacje dotyczące dostawcy np. dane adresowe, telefony, informacje dotyczące otrzymanych upustów.");
            _tt.SetToolTip(linkLabelDostawcaMat2, "link do strony dostawcy materiałów / normaliów.");

            // -----------------------------------------------Zakładka Dostawcy
            //lista dostawców
            _tt.SetToolTip(listBoxDostawcy, "Lista dostawców.");
            // dane dostawców
            _tt.SetToolTip(textBoxNazwaDostawcy, "Nazwa wybranego dostawcy.");
            _tt.SetToolTip(richTextBoxDostawca, "Wszelkie dodatkowe informacje dotyczące dostawcy np.dane adresowe, telefony, informacje dotyczące otrzymanych upustów.");
            _tt.SetToolTip(textBoxLink, "Wpisanie linku wybranego dostawcy.");
            _tt.SetToolTip(linkLabelDostawcaMat, "Wyświetlenie linku wybranego dostawcy.");
            //przyciski zapisz/edytuj itp
            _tt.SetToolTip(buttonNowyDostawca, "Nowa pozycja w bazie dostawców materiałów / normaliów.");
            _tt.SetToolTip(buttonZapiszDostawca, "Zapis nowej pozycji w bazie dostawców lub edycja wybranej pozycji.");
            _tt.SetToolTip(buttonAnulujDostawca, "Anulowanie zmiany.");
            _tt.SetToolTip(buttonUsunDostawca, "Usuwa pozycję z bazy.");


        }//public SpisForm()

        private void SpisForm_Shown(object sender, EventArgs e)
        {
            try
            {
                Process[] ps = Process.GetProcessesByName(Application.ProductName);

                if (ps.Length > 1)
                {
                    MessageBox.Show("Uwaga! RemaGUM został wcześniej uruchomiony i jest aktywny.", "RemaGUM",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    return;
                }
            }
            catch { }
        }

        // Wyświetla komunikaty chwilowe w programie.
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
            Cursor.Current = Cursors.WaitCursor;

            // ----------------------------------Zakładka Maszyny.
            if (v.SelectedIndex == 0)
            {
                OdswiezListeMaszyn();
                listBoxMaszyny.SelectedIndex = _maszynyBUS.getIdx(_maszynaId);

                radioButtonNazwa.Checked = true;

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

            // --------------------------------- Zakładka Operator.
            if (v.SelectedIndex == 1)
            {
                comboBoxOperator.SelectedIndex = 0;//ustawia sortowanie po nazwisku operatora
                WypelnijOperatorowDanymi();
                WypelnijDzialOperatora();

                if (listBoxOperator.Items.Count > 0)
                {
                    listBoxOperator.SelectedIndex = 0;
                }

            }// Zakładka Operator

            // ----------------------------------Zakładka Dysponent.
            if (v.SelectedIndex == 2)
            {
                WypelnijDysponentowDanymi();
                WypelnijDysponentowMaszynami();

                WypelnijDzialDysponenta();

                if (listBoxDysponent.Items.Count > 0)
                {
                    listBoxDysponent.SelectedIndex = 0;
                }
            }// Zakładka Dysponent

            // --------------------------------- Zakładka Materiały.
            if (v.SelectedIndex == 3)
            {
                radioButtonNazwa_mat.Checked = true; // przy przejściu do zakładki materiały zaznaczone sortowanie po nazwie.
                WybierzMagazyn();
                WypelnijMaterialyNazwami();
                WypelnijJednostka_miar();
                WypelnijRodzaj_mat();
                WypelnijCheckedBoxDostawcow(checkedListBoxDostawcyMat);// wypełnia checked boxy dostawców materialów.

                if (listBoxMaterialy.Items.Count > 0)
                {
                    listBoxMaterialy.SelectedIndex = 0;
                }
            }// Zakładka Materiały

            // --------------------------------Zakładka Dostawcy.
            if (v.SelectedIndex == 4)
            {
                WypelnijListeDostawcowDanymi(); // wypełnia listBoxDostawcy

                if (listBoxDostawcy.Items.Count > 0)
                {
                    listBoxDostawcy.SelectedIndex = 0;
                }
            } // Zakładka Dostawcy

           
            Cursor.Current = Cursors.Default;
        } //tabControl1_SelectedIndexChanged

        /// <summary>
        /// Przycisk uruchamiający okienko o programie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonOProgramie_Click(object sender, EventArgs e)
        {
            Form formORemaGUM = new FormORemaGUM();
            formORemaGUM.ShowDialog();
            formORemaGUM.Dispose();
        }// toolStripButtonOProgramie_Click

        /// <summary>
        /// Przycisk uruchomiający pomoc programu RemaGUM.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        // TODO //  //  //  //  //  //  //  //  //  //  //  //  //  // ZAKŁADKA MASZYNY.

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
                richTextBoxProducent.Text = string.Empty;

                richTextBoxUwaga.Text = string.Empty;

                labelZdjecieNazwa.Text = string.Empty;
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
            richTextBoxUwaga.Text = string.Empty;

            // uaktualnienie danych maszyny po zmianie sposobu sortowania
            if (radioButtonNazwa.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nazwa ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
            }
            else if (radioButtonTyp.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Typ ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
            }
            else if (radioButtonNr_fabryczny.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_fabryczny ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
            }
            else if (radioButtonNr_inwentarzowy.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_inwentarzowy ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
            }
            else if (radioButtonNr_pomieszczenia.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_pomieszczenia ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
            }
            else if (radioButtonData_ost_przegl.Checked)
            {
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Data_ost_przegl ASC;");
                maszynyBUS.idx = listBoxMaszyny.SelectedIndex;
            }

            try
            {
                listBoxMaszyny.Tag = maszynyBUS.VO.Identyfikator;

                comboBoxKategoria.Text = maszynyBUS.VO.Kategoria;
                textBoxNazwa.Text = maszynyBUS.VO.Nazwa;
                textBoxTyp.Text = maszynyBUS.VO.Typ;
                textBoxNr_inwentarzowy.Text = maszynyBUS.VO.Nr_inwentarzowy;
                textBoxNr_fabryczny.Text = maszynyBUS.VO.Nr_fabryczny;
                textBoxRok_produkcji.Text = maszynyBUS.VO.Rok_produkcji;
                richTextBoxProducent.Text = maszynyBUS.VO.Producent;
                textBoxNr_pom.Text = maszynyBUS.VO.Nr_pomieszczenia;

                // wyświetlenie zdjecia przy zmianie indeksu
                if (File.Exists(maszynyBUS.VO.Zdjecie))
                {
                    File.Delete(maszynyBUS.VO.Zdjecie);
                }

                labelZdjecieNazwa.Text = maszynyBUS.VO.Zdjecie;

                _zawartoscPliku = maszynyBUS.VO.Zawartosc_pliku; // zawartość zdjęcia.
                pokazZdjecie(labelZdjecieNazwa.Text); // zmiana zdjęcia przy zmianie indeksu maszyny.
                comboBoxDysponent.Text = maszynyBUS.VO.Nazwa_dysponent;  // wypełnia dysponenta

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
                    richTextBoxUwaga.Text = ("Uwaga w dniu: " + maszynyBUS.VO.Dz_kol_przeg.ToString("00") + "." + maszynyBUS.VO.Mc_kol_przeg.ToString("00") + "." + maszynyBUS.VO.Rok_kol_przeg.ToString() + " mija termin przeglądu dla maszyny " + maszynyBUS.VO.Nazwa.ToString() + " o numerze inwentarzowym: " + maszynyBUS.VO.Nr_inwentarzowy);
                }
                else if (timeSpan.Days < 0)
                {
                    richTextBoxUwaga.Text = ("Termin przeglądu maszyny " + maszynyBUS.VO.Nazwa.ToString() + " o nr: " + maszynyBUS.VO.Nr_inwentarzowy + " minął w dniu: " + maszynyBUS.VO.Dz_kol_przeg.ToString("00") + "." + maszynyBUS.VO.Mc_kol_przeg.ToString("00") + "." + maszynyBUS.VO.Rok_kol_przeg.ToString() + "r.");
                }
                else { richTextBoxUwaga.Text = string.Empty; }

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
                toolStripStatusLabelID_Maszyny.Text = maszynyBUS.VO.Identyfikator.ToString();

            }
            catch { }
        }//listBoxMaszyny_SelectedIndexChanged

        /// <summary>
        /// Odświeża listę maszyn w listBoxMaszyny.
        /// </summary>
        private void OdswiezListeMaszyn()
        {
            listBoxMaszyny.BeginUpdate();
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            listBoxMaszyny.Items.Clear();
            maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nazwa ASC;");

            while (!maszynyBUS.eof)
            {
                listBoxMaszyny.Items.Add(maszynyBUS.VO.Nazwa);
                maszynyBUS.skip();
            }
            // ustawia indeks listy maszyn na pozycji 1
            if (listBoxMaszyny.Items.Count > 0)
            {
                listBoxMaszyny.SelectedIndex = 0;
            }

            listBoxMaszyny.EndUpdate();


        }// OdswiezListeMaszyn()

        //************* wypełnia CheckedListBox nazwiskami i imionami operatorów maszyn.
        /// <summary>
        /// Wypelnij operatorow maszyn - checked -boxy.
        /// </summary>
        /// <param name="v">The v.</param>
        private void WypelnijOperatorow_maszyn(CheckedListBox v)
        {
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);

            v.Items.Clear();
            operatorBUS.select();

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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxKategoria control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Obsługa comboBox_Dysponent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBox_Dysponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            dysponentBUS.idx = comboBoxDysponent.SelectedIndex;
        }//comboBox_Dysponent_SelectedIndexChanged()

        //////////////////////////////////////////////////////////////         Rabio buttony

        /// <summary>
        /// Sortuje po nazwie maszyny.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    listBoxMaszyny.Items.Add(maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }
                if (listBoxMaszyny.Items.Count > 0)
                {
                    listBoxMaszyny.SelectedIndex = 0;
                }
            }
        }//radioButtonNazwa_CheckedChanged
        
        
        /// <summary>
        /// Sortuje po numerze inwentarzowym maszyny.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    listBoxMaszyny.Items.Add("nr: " + maszynyBUS.VO.Nr_inwentarzowy + " -> " + maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }
                if (listBoxMaszyny.Items.Count > 0)
                {
                    listBoxMaszyny.SelectedIndex = 0;
                }
            }
        }//radioButton_Nr_Inwentarzowy_CheckedChanged

        /// <summary>
        /// Sortuje po typie maszyny.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    listBoxMaszyny.Items.Add("Typ: " + maszynyBUS.VO.Typ + " -> " + maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }
                if (listBoxMaszyny.Items.Count > 0)
                {
                    listBoxMaszyny.SelectedIndex = 0;
                }
            }
        }//radioButton_Typ_CheckedChanged

        /// <summary>
        /// Sortuje po numerze fabrycznym maszyny.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    listBoxMaszyny.Items.Add("Nr: "+maszynyBUS.VO.Nr_fabryczny + " -> " + maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }
                if (listBoxMaszyny.Items.Count > 0)
                {
                    listBoxMaszyny.SelectedIndex = 0;
                }
            }

        }//radioButtonNr_fabrycznyCheckedChanged


        /// <summary>
        /// Sortuje po numerze pomieszczenia.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonNr_pomieszczenia_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNr_pomieszczenia.Checked)
            {
                listBoxMaszyny.Items.Clear();
                CzyscDaneMaszyny();

                nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
                maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nr_pomieszczenia ASC;");
                while (!maszynyBUS.eof)
                {
                    listBoxMaszyny.Items.Add("Nr: " + maszynyBUS.VO.Nr_pomieszczenia + " -> " + maszynyBUS.VO.Nazwa);
                    maszynyBUS.skip();
                }
                if (listBoxMaszyny.Items.Count > 0)
                {
                    listBoxMaszyny.SelectedIndex = 0;
                }
            }
        }//radioButtonNr_pomieszczenia

        /// <summary>
        /// Sortuje po dacie ostatniego przeglądu maszyny.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (listBoxMaszyny.Items.Count > 0)
                {
                    listBoxMaszyny.SelectedIndex = 0;
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

            //_maszynaSzukajIdx = _maszynyBUS.count;

            Cursor.Current = Cursors.Default;

        }//buttonSzukaj_Click

        /// <summary>
        /// Wypełnia listbox Działami w których znajdują się maszyny.
        /// </summary>
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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxDzial control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBoxDzial_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.DzialBUS dzialBUS = new nsAccess2DB.DzialBUS(_connString);
            dzialBUS.idx = comboBoxDzial.SelectedIndex;
            comboBoxDzial.Tag = dzialBUS.VO.Nazwa;
        }//comboBoxDzial_SelectedIndexChanged

        /// <summary>
        /// Wypełnia listbox pozycjami częstotliwości Wykorzystania.
        /// </summary>
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
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ComboBoxWykorzystanie control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ComboBoxWykorzystanie_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.WykorzystanieBUS wykorzystanieBUS = new nsAccess2DB.WykorzystanieBUS(_connString);
            wykorzystanieBUS.idx = comboBoxWykorzystanie.SelectedIndex;
            comboBoxWykorzystanie.Tag = wykorzystanieBUS.VO.Wykorzystanie;
        }//comboboxWykorzystanie_SelectedIndexChanged

        /// <summary>
        /// wypełnia listbox propozycjami co zrobić z maszyną (zachować/złomować itp)
        /// </summary>
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
        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxPropozycja control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBoxPropozycja_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.PropozycjaBUS propozycjaBUS = new nsAccess2DB.PropozycjaBUS(_connString);
            propozycjaBUS.idx = comboBoxPropozycja.SelectedIndex;
        }// comboBoxPropozycja_SelectedIndexChanged

        /// <summary>
        /// Wypełnia stan techniczy maszyny.
        /// </summary>
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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxStan_techniczny control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBoxStan_techniczny_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.Stan_technicznyBUS stan_TechnicznyBUS = new nsAccess2DB.Stan_technicznyBUS(_connString);
            stan_TechnicznyBUS.idx = comboBoxStan_techniczny.SelectedIndex;
        }// comboBoxStan_techniczny_SelectedIndexChanged

        /// <summary>
        /// Klawisz Nowa czyści formularz (maszyna).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNowa_Click(object sender, EventArgs e)
        {
            CzyscDaneMaszyny();

            radioButtonNazwa.Checked = true; // przy nowej maszynie przed zapisem wymusza zaznaczenie sortowanie po nazwie maszyny.

            // dezaktywacja list boxa
            listBoxMaszyny.Enabled = false;
            listBoxMaszyny.BackColor = Color.Bisque;

            //dezaktywacja pola wyszukiwania i przycisku wyszukaj
            textBoxWyszukiwanie.Enabled = false;
            textBoxWyszukiwanie.BackColor = Color.Bisque;
            buttonSzukaj.Enabled = false;

            // dezaktywacja sortowania
            groupBoxSortowanie.Enabled = false;

            buttonNowa.Enabled = false;
            buttonZapisz.Enabled = true;
            buttonAnuluj.Enabled = true;
            buttonUsun.Enabled = false;

            _statusForm = (int)_status.nowy;
        }//ButtonNowa_Click

        /// <summary>
        /// Klawisz Anuluj (maszyna).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnuluj_Click(object sender, EventArgs e)
        {
            CzyscDaneMaszyny();

            // aktywacja list boxa
            listBoxMaszyny.Enabled = true;
            listBoxMaszyny.BackColor = Color.White;

            // aktywacja pola wyszukiwania i przycisku szukaj
            textBoxWyszukiwanie.Enabled = true;
            textBoxWyszukiwanie.BackColor = Color.White;
            buttonSzukaj.Enabled = true;

            // aktywacja sortowania
            groupBoxSortowanie.Enabled = true;

            buttonNowa.Enabled = true;
            buttonZapisz.Enabled = true;
            buttonAnuluj.Enabled = true;
            buttonUsun.Enabled = true;

            _statusForm = (int)_status.edycja;
        }//buttonAnuluj_Click

        /// <summary>
        /// Klawisz Usuń (maszyna).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUsun_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć maszynę: " + textBoxNazwa.Text + "? ", "RemaGUM", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                goto no;
            }

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            nsAccess2DB.Maszyny_OperatorBUS maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            nsAccess2DB.Maszyny_DysponentBUS maszyny_DysponentBUS = new nsAccess2DB.Maszyny_DysponentBUS(_connString);

            listBoxMaszyny.Enabled = true; // aktywacja list boxa
            try
            {
                comboBoxKategoria.Text = string.Empty;
                textBoxNazwa.Text = string.Empty;
                textBoxTyp.Text = string.Empty;
                textBoxNr_inwentarzowy.Text = string.Empty;
                textBoxNr_fabryczny.Text = string.Empty;
                textBoxRok_produkcji.Text = string.Empty;
                richTextBoxProducent.Text = string.Empty;

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
                labelZdjecieNazwa.Text = string.Empty;

            }
            catch { }

            maszynyBUS.delete((int)listBoxMaszyny.Tag);// usunięcie z tabeli maszyna
            maszyny_OperatorBUS.delete((int)listBoxMaszyny.Tag); // usunięcie z tabeli relacji maszyna operator.

            // po usunięciu maszyny odświeża danę z listy maszyn przez ponowne zaznaczenie sortowania po nazwie.
            radioButtonNr_fabryczny.Checked = true;
            radioButtonNazwa.Checked = true;

            WypelnijOperatorow_maszyn(checkedListBoxOperatorzy_maszyn);
            _statusForm = (int)_status.edycja;

            no:;
        }//buttonUsun_Click

        /// <summary>
        /// Działania po wciśnięciu klawisza Zapisz (maszyny).
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

            if (textBoxTyp.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij typ maszyny.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxNr_inwentarzowy.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij nr inwentarzowy maszyny.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxRok_produkcji.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij rok produkcji maszyny.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxNr_pom.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij numer pomieszczenia.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            nsAccess2DB.Maszyny_OperatorBUS maszyny_OperatorBUS = new nsAccess2DB.Maszyny_OperatorBUS(_connString);
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);

            nsAccess2DB.MaszynyVO maszynyVO = new nsAccess2DB.MaszynyVO();
            nsAccess2DB.OperatorVO operatorVO = new nsAccess2DB.OperatorVO();
            nsAccess2DB.Maszyny_OperatorVO maszyny_operatorVO = new nsAccess2DB.Maszyny_OperatorVO();
            nsAccess2DB.DysponentVO dysponentVO = new nsAccess2DB.DysponentVO();

            maszynyVO = maszynyBUS.VO;
            operatorVO = operatorBUS.VO;
            maszyny_operatorVO = maszyny_OperatorBUS.VO;
            dysponentVO = dysponentBUS.VO;

            if (_zawartoscPliku == null) _zawartoscPliku = new byte[] { }; // zapisz nowy plik zdjęcia podczas edycji maszyny.
            if (_statusForm == (int)_status.nowy)
            {
                maszynyBUS.select();
              
                maszynyBUS.idx = maszynyBUS.count - 1;
                maszynyBUS.VO.Identyfikator = maszynyBUS.VO.Identyfikator + 1;
                
                maszynyVO.Kategoria = comboBoxKategoria.Text;
                maszynyVO.Nazwa = textBoxNazwa.Text.Trim();
                
                maszynyVO.Typ = textBoxTyp.Text.Trim();
                maszynyVO.Nr_inwentarzowy = textBoxNr_inwentarzowy.Text.Trim();
                maszynyVO.Nr_fabryczny = textBoxNr_fabryczny.Text.Trim();
                maszynyVO.Rok_produkcji = textBoxRok_produkcji.Text.Trim();
                maszynyVO.Producent = richTextBoxProducent.Text.Trim();

                //pokazuje zdjęcie
                maszynyVO.Zdjecie = labelZdjecieNazwa.Text;  //zdjęcie nazwa
                maszynyVO.Zawartosc_pliku = _zawartoscPliku;//zdjęcie zawartość

                dysponentBUS.select();
                maszynyVO.Nazwa_dysponent = dysponentVO.Dysp_nazwisko + dysponentVO.Dysp_imie;
                maszynyVO.Nazwa_dysponent = comboBoxDysponent.Text.Trim();

                maszynyVO.Nr_pomieszczenia = textBoxNr_pom.Text;
                maszynyVO.Dzial = comboBoxDzial.Text;
                maszynyVO.Nr_prot_BHP = textBoxNr_prot_BHP.Text;
                maszynyVO.Rok_ost_przeg = dateTimePickerData_ost_przegl.Value.Year;
                maszynyVO.Mc_ost_przeg = dateTimePickerData_ost_przegl.Value.Month;
                maszynyVO.Dz_ost_przeg = dateTimePickerData_ost_przegl.Value.Day;
                maszynyVO.Data_ost_przegl = int.Parse(maszynyVO.Rok_ost_przeg.ToString() + maszynyVO.Mc_ost_przeg.ToString("00") + maszynyVO.Dz_ost_przeg.ToString("00"));
                DateTime dt = new DateTime(dateTimePickerData_ost_przegl.Value.Ticks); // dodaje interwał przeglądów do data_ost_przegl
                                                                                       // dt = dt.AddYears(_interwalPrzegladow);
                dt = dt.AddDays(_interwalPrzegladow); // w dniach
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
                //listBoxMaszyny.SelectedIndex = maszynyBUS.getIdx(maszynyBUS.VO.Identyfikator);// ustawienie zaznaczenia w tabeli maszyn.

                // Zapis operatorów/operatora maszyny przypisanych do maszyny.
                
                maszyny_OperatorBUS.select(maszynyVO.Identyfikator);
                operatorBUS.select();

                
                for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++)
                {
                    if (checkedListBoxOperatorzy_maszyn.GetItemChecked(i))
                    {
                        operatorBUS.idx = i;
                        maszyny_OperatorBUS.insert(maszynyBUS.VO.Identyfikator, operatorBUS.VO.Identyfikator, maszynyBUS.VO.Nazwa);
                        
                    }
                    
                }
                
                maszynyBUS.write(maszynyVO); // zpis danych do tabeli maszyny

                maszyny_OperatorBUS.updateNazwa(maszynyBUS.VO.Nazwa);

                //maszyny_OperatorBUS.write(maszyny_operatorVO);

                //listBoxMaszyny.SelectedIndex = maszynyBUS.getIdx(maszynyBUS.VO.Identyfikator);// ustawienie zaznaczenia w tabeli maszyn.

                //maszynyBUS.selectQuery("SELECT * FROM Maszyny ORDER BY Nazwa ASC;");
                //listBoxMaszyny.SelectedIndex = maszynyBUS.getIdx(maszynaId);


            }//if - nowy

            else if (_statusForm == (int)_status.edycja)
            {
                try
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
                        maszynyVO.Producent = richTextBoxProducent.Text.Trim();
                        
                        maszynyVO.Nazwa_dysponent = comboBoxDysponent.Text.Trim();
                        maszynyVO.Nr_pomieszczenia = textBoxNr_pom.Text;
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

                        //pokazuje zdjęcie
                        maszynyVO.Zawartosc_pliku = _zawartoscPliku;//zdjęcie zawartość
                        int dlugosc = maszynyBUS.VO.Zawartosc_pliku.Length;
                        if (dlugosc == 0)
                        {
                            maszynyVO.Zdjecie = string.Empty; //wykasowuje nazwę zdjęcia jeżeli nie ma pliku.
                            //maszynyBUS.deleteZdjecie(maszynyBUS.VO.Zdjecie);
                        }
                        else if (maszynyVO.Zdjecie == "Nazwa zdjęcia")
                        {
                            maszynyVO.Zdjecie = string.Empty; //wykasowuje nazwę zdjęcia jeżeli nie ma pliku.
                        }
                        else
                        {
                            maszynyVO.Zdjecie = labelZdjecieNazwa.Text;  //zdjęcie nazwa
                            maszynyVO.Zawartosc_pliku = _zawartoscPliku;//zdjęcie zawartość
                        }
                        
                        maszyny_OperatorBUS.delete(maszynyBUS.VO.Identyfikator);
                        maszyny_OperatorBUS.select(maszynyBUS.VO.Identyfikator);

                        //Zapis operatorów/ operatora maszyny przypisanych do maszyny.
                        operatorBUS.select();


                        maszynyBUS.write(maszynyVO);// zapis edycji 

                        for (int i = 0; i < checkedListBoxOperatorzy_maszyn.Items.Count; i++)
                        {
                            if (checkedListBoxOperatorzy_maszyn.GetItemChecked(i))
                            {
                                operatorBUS.idx = i;
                                maszyny_OperatorBUS.insert(maszynyVO.Identyfikator, operatorBUS.VO.Identyfikator, maszynyBUS.VO.Nazwa); //wpisanie nazwy maszyny
                                
                            }
                        }
                              
                        maszynyBUS.write(maszynyVO);// zapis edycji 
                        
                    }
                    //listBoxMaszyny.SelectedIndex = maszynyBUS.getIdx(maszynyBUS.VO.Identyfikator);// ustawienie zaznaczenia w tabeli maszyn.

                }
                catch { };

            }//else if - edycja

            OdswiezListeMaszyn();// po utworzeniu nowej maszyny odświeża danę z listy maszyn przez zaznaczenie sortowania po nazwie.
            WypelnijOperatorow_maszyn(checkedListBoxOperatorzy_maszyn);
            //WypelnijOperatorowMaszynami();

            /// aktywacja list boxa
            listBoxMaszyny.Enabled = true;
            listBoxMaszyny.BackColor = Color.White;

            // aktywacja pola wyszukiwania i przycisku szukaj
            textBoxWyszukiwanie.Enabled = true;
            textBoxWyszukiwanie.BackColor = Color.White;
            buttonSzukaj.Enabled = true;

            // aktywacja sortowania
            groupBoxSortowanie.Enabled = true;

            buttonNowa.Enabled = true;
            buttonZapisz.Enabled = true;
            buttonAnuluj.Enabled = true;
            buttonUsun.Enabled = true;

            pokazKomunikat("Pozycja zapisana w bazie");
            _statusForm = (int)_status.edycja;

            // radioButtonNazwa.Checked = true; // przy zapisie wymusza zaznaczenie sortowanie po nazwie edytowanej maszyny.
        }//buttonZapisz_Click        /// <summary>
        /// Pokazuje zdjęcie wybranej maszyny.
        /// </summary>
        /// <param name="Zdjecie"></param>
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
                string sciezkazdjecia = _dirNazwa + "\\" + maszynyBUS.VO.Zdjecie;
                                
                if (File.Exists(sciezkazdjecia))
                {
                    Bitmap bmp = (Bitmap)Bitmap.FromFile(sciezkazdjecia);
                    pictureBox1.Image = Bitmap.FromFile(sciezkazdjecia);

                    buttonUsunZdj.Enabled = true;
                    buttonPokazZdj.Enabled = false;
                    return;
                }
                else
                {
                    pictureBox1.Image = ErrorImage; // brak zdjęcia 
                    //pokazKomunikat("Brak zdjęcia.");

                    buttonPokazZdj.Enabled = true;
                    buttonUsunZdj.Enabled = false;       
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Brak wgranego zdjęcia lub problem z prezentacją zdjęcia. Błąd: " + ex.Message, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Cursor = Cursors.Default;
        }//pokazZdjecie

        /// <summary>
        /// Przycisk Wgraj/pokaż zdjęcie maszyny.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWgrajZdj_Click(object sender, EventArgs e)
        {
            pokazKomunikat("Uwaga dopuszczalne formaty zdjęć to jpg, png, tiff oraz btm.");

            //tylko pliki obrazów 
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPG|*.jpg|PNG|*.png|TIFF|*.tiff|BMP|*.bmp";

            // dopuszczenie zczytania wszystkich plików
            //ofd.Title = "wybierz *.* plik";
            //ofd.Filter = "pliki (*.*)|*.*|wszystkie pliki (*.*)|*.*";
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.OpenOrCreate, FileAccess.Read);
                    _zawartoscPliku = new byte[fs.Length];
                    fs.Read(_zawartoscPliku, 0, System.Convert.ToInt32(fs.Length));

                    fs.Close();

                    FileInfo fi = new FileInfo(ofd.FileName);
                    labelZdjecieNazwa.Text = fi.Name;
                    maszynyBUS.VO.Zawartosc_pliku = _zawartoscPliku; //zawartosc zdjęcia
                    //maszynyBUS.VO.Zdjecie = fi.Name;// nazwa zdjęcia

                    pokazZdjecie(labelZdjecieNazwa.Text); // pokazuje zdjęcie.
                    pokazKomunikat("Wgranie zdjęcia nastąpi po wciśnięciu przycisku Zapisz.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z prezentacją zdjęcia. Błąd: " + ex.Message);
                }
            }
        }//buttonWgrajZdj_Click

        /// <summary>
        /// usuwa zdjęcie maszyny.
        /// </summary>
        private void UsunZdjecie()
        {
            nsAccess2DB.MaszynyBUS maszynyBUS = new nsAccess2DB.MaszynyBUS(_connString);
           
            maszynyBUS.VO.Zdjecie = string.Empty; // nazwa zdjęcia.
            maszynyBUS.VO.Zawartosc_pliku = new byte[0] { }; // zawartość pliku zdjęcia.
            labelZdjecieNazwa.Text = string.Empty;
        }

        /// <summary>
        /// Przycisk Usuń zdjęcie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUsunZdj_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Usunięcie zdjęcia nastąpi po wciśnięciu przycisku Zapisz. Czy chcesz usunąć zdjęcie maszyny: " + textBoxNazwa.Text + "? ", "RemaGUM", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                goto no;
            }
            UsunZdjecie();

            no:; // rezygnacja z wykasowania zdjęcia.
        }//buttonUsunZdj_Click


        // TODO //  //  //  //  //  //  //  //  //  //  //  //  //  //   ZAKŁADKA MATERIAŁY.

        // --------------------------------------- wypełnianie  listBoxMaterialy.
        // wyświetla listę Materiałów po nazwie

        /// <summary>
        /// Wypełnia listBoxMaterialy.
        /// </summary>
        private void WypelnijMaterialyNazwami()
        {
            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Nazwa_mat ASC;");

            //nsAccess2DB.Dostawca_matBUS dostawca_MatBUS = new nsAccess2DB.Dostawca_matBUS(_connString);
            //dostawca_MatBUS.select();

            listBoxMaterialy.Items.Clear();

            //materialyBUS.idx = listBoxMaterialy.SelectedIndex;

            while (!materialyBUS.eof)
            {
                listBoxMaterialy.Items.Add(materialyBUS.VO.Nazwa_mat);
                materialyBUS.skip();
            }

            if (listBoxMaterialy.Items.Count > 0)
            {
                materialyBUS.idx = 0;
                listBoxMaterialy.SelectedIndex = 0;
                listBoxMaterialy.Tag = materialyBUS.VO.Identyfikator;
            }
        }// WypelnijMaterialyNazwami()



        /// <summary>
        /// Zmiana indeksu w list box Materialy.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxMaterialy_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            if (radioButtonNazwa_mat.Checked)
            {
                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Nazwa_mat ASC;");
                materialyBUS.idx = listBoxMaterialy.SelectedIndex;
                //toolStripStatusLabelID_Materialu.Text = materialyBUS.VO.Identyfikator.ToString(); //  toolStripStatusLabelID_Materialu
            }
            else if (radioButtonStan_min_mat.Checked)
            {
                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Stan_min_mat ASC;");
                materialyBUS.idx = listBoxMaterialy.SelectedIndex;
                //toolStripStatusLabelID_Materialu.Text = materialyBUS.VO.Identyfikator.ToString();
            }
            else if (radioButtonTyp_mat.Checked)
            {
                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Typ_mat ASC;");
                materialyBUS.idx = listBoxMaterialy.SelectedIndex;
                //toolStripStatusLabelID_Materialu.Text = materialyBUS.VO.Identyfikator.ToString();
            }
            else if (radioButtonMagazyn_ilosc_mat.Checked)
            {
                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Stan_mat ASC;");
                materialyBUS.idx = listBoxMaterialy.SelectedIndex;
            }
            // materialyBUS.select();
            try
            {
                //toolStripStatusLabelID_Materialu.Text = materialyBUS.VO.Identyfikator;
                listBoxMaterialy.Tag = materialyBUS.VO.Identyfikator;

                comboBoxWyborMagazyn.Text = materialyBUS.VO.Magazyn;
                textBoxTypMat.Text = materialyBUS.VO.Typ_mat;
                comboBoxRodzajMat.Text = materialyBUS.VO.Rodzaj_mat;
                textBoxNazwaMat.Text = materialyBUS.VO.Nazwa_mat;
                string s = materialyBUS.VO.Jednostka_miar_mat;
                comboBoxJednostkaMat.Text = s;

                // jednostki wyświetlane labelach dane z comboBoxJednostkaMat.
                labelJednostkaDostepnosc.Text = s;
                labelJednostkaMin.Text = s;
                labelJednostkaZuzycie.Text = s;
                labelJednostkaOdpad.Text = s;
                labelJednostkaZapotrzebowanie.Text = s;

                textBoxMagazynMat.Text = materialyBUS.VO.Stan_mat.ToString();
                textBoxZuzycieMat.Text = "0";
                textBoxOdpadMat.Text = "0";
                textBoxMinMat.Text = materialyBUS.VO.Stan_min_mat.ToString(); // wartość minimalna - zawsze widoczna, podlega edycji i zapisywana do bazy.
                textBoxZapotrzebowanieMat.Text = "0";

                // komunikat o niskim stanie magazynowym.
                if (materialyBUS.VO.Stan_mat <= materialyBUS.VO.Stan_min_mat)
                {
                    richTextBoxKomunikatMaterialy.Text = ("Uwaga niski stan magazynowy ( " + materialyBUS.VO.Stan_mat + " " + materialyBUS.VO.Jednostka_miar_mat +" ) produktu: " + materialyBUS.VO.Nazwa_mat.ToString() + " typ - " + materialyBUS.VO.Typ_mat.ToString() + ". ");

                }
                else
                {
                    richTextBoxKomunikatMaterialy.Text = string.Empty;
                }

                textBoxMagazynMat.Text = materialyBUS.VO.Stan_mat.ToString();

                textBoxMagazynMat.Enabled = false; // zablokowanie pola magazynu po zapisie.
                textBoxMagazynMat.BackColor = Color.Bisque;

                nsAccess2DB.Dostawca_matBUS dostawca_matBUS = new nsAccess2DB.Dostawca_matBUS(_connString);
                //dostawca_matBUS.select();
                dostawca_matBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC;"); //odświeża wybrane checkboxy dostawcy.
                nsAccess2DB.Dostawca_MaterialBUS dostawca_materialBUS = new nsAccess2DB.Dostawca_MaterialBUS(_connString);
                dostawca_materialBUS.select((int)listBoxMaterialy.Tag);

                //wypełnia Dostawców matreiałów w polu checkedListBoxDostawcyMat ************************
                for (int i = 0; i < checkedListBoxDostawcyMat.Items.Count; i++)
                {
                    checkedListBoxDostawcyMat.SetItemChecked(i, false);
                }

                int idxD = -1;
                while (!dostawca_materialBUS.eof)
                {
                    idxD = dostawca_matBUS.getIdx(dostawca_materialBUS.VO.ID_dostawca_mat);
                    if (idxD > -1) checkedListBoxDostawcyMat.SetItemChecked(idxD, true);
                    dostawca_materialBUS.skip();
                }
              
                toolStripStatusLabelID_Materialu.Text = materialyBUS.VO.Identyfikator.ToString();
            }

            catch { }
        } // listBoxMaterialy_SelectedIndexChanged

        /// <summary>
        /// Wypełnia CheckedListBoxDostawcyMat (zakładka Materiały).
        /// </summary>
        /// <param name="v">CheckedListBoxDostawcyMat</param>
        private void WypelnijCheckedBoxDostawcow(CheckedListBox v)
        {
            nsAccess2DB.Dostawca_matBUS dostawca_MatBUS = new nsAccess2DB.Dostawca_matBUS(_connString);

            v.Items.Clear();
            //dostawca_MatBUS.select();
            dostawca_MatBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC;"); //odświeża wybrane checkboxy dostawcy.
            while (!dostawca_MatBUS.eof)
            {
                v.Items.Add(dostawca_MatBUS.VO.Nazwa_dostawca_mat);
                dostawca_MatBUS.skip();
            }

            if (v.Items.Count > 0)
            {
                dostawca_MatBUS.idx = 0;
                v.SelectedIndex = 0;
                v.Tag = dostawca_MatBUS.VO.Identyfikator;
            }
        }// WypelnijDostawcowMaterialow(CheckedListBox v)

        /// <summary>
        /// Zmiana indeksu dostawcy w checkedListBoxDostawcyMat (aktualizacja pól do odczytu danych wybranego dostawcy).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBoxDostawcyMat_SelectedIndex(object sender, EventArgs e)
        {
            nsAccess2DB.Dostawca_matBUS dostawca_matBUS = new nsAccess2DB.Dostawca_matBUS(_connString);

            //dostawca_matBUS.select();
            dostawca_matBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC;");

            dostawca_matBUS.idx = checkedListBoxDostawcyMat.SelectedIndex;
            checkedListBoxDostawcyMat.Tag = dostawca_matBUS.VO.Identyfikator;
            labelDostawca.Text = dostawca_matBUS.VO.Nazwa_dostawca_mat.ToString();

            richTextBoxDaneDodatkoweDostawca.Text = dostawca_matBUS.VO.Dod_info_dostawca_mat.ToString();
            linkLabelDostawcaMat2.Text = dostawca_matBUS.VO.Link_dostawca_mat.ToString();
           
            toolStripStatusLabel_ID_Dostawcy.Text = dostawca_matBUS.VO.Identyfikator.ToString();
          
        }// private void checkedListBoxDostawcyMat_SelectedIndex(object sender, EventArgs e)

        /// <summary>
        /// Czyści dane w formularzu Materiały.
        /// </summary>
        private void CzyscDaneMaterialy()
        {
            try {
                toolStripStatusLabelID_Materialu.Text = "";
                textBoxTypMat.Text = string.Empty;

                // czyści wybór przynależności do magazynu materiałów / normaliów.
                comboBoxWyborMagazyn.SelectedIndex = -1;
                comboBoxWyborMagazyn.Enabled = true;
                comboBoxWyborMagazyn.SelectedIndex = 0;
                comboBoxWyborMagazyn.Refresh();

                comboBoxRodzajMat.Text = string.Empty;
                comboBoxRodzajMat.SelectedIndex = -1;
                comboBoxRodzajMat.Enabled = true;
                comboBoxRodzajMat.SelectedIndex = 0;
                comboBoxRodzajMat.Refresh();

                textBoxNazwaMat.Text = string.Empty;
                labelDostawca.Text = string.Empty;

                comboBoxJednostkaMat.Text = string.Empty;
                comboBoxJednostkaMat.SelectedIndex = -1;
                comboBoxJednostkaMat.Enabled = true;
                comboBoxJednostkaMat.SelectedIndex = 0;
                comboBoxJednostkaMat.Refresh();

                richTextBoxKomunikatMaterialy.Text = string.Empty;// czyści komunikaty dotyczące materiały.

                textBoxMagazynMat.Text = string.Empty;
                textBoxZuzycieMat.Text = string.Empty;
                textBoxOdpadMat.Text = string.Empty;
                textBoxMinMat.Text = string.Empty;
                textBoxZapotrzebowanieMat.Text = string.Empty;

                //czyści checkboxy DostawcyMat
                for (int i = 0; i < checkedListBoxDostawcyMat.Items.Count; i++)
                {
                    checkedListBoxDostawcyMat.SetItemChecked(i, false);
                }

                linkLabelDostawcaMat.Text = string.Empty;
                richTextBoxDostawca.Text = string.Empty;
            }
            catch { }
        }//CzyscDaneMaterialy()

        /// <summary>
        /// Odswieża dane w zakładce materiały.
        /// </summary>
        private void OdswiezMaterialy()
        {
            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            listBoxMaterialy.Items.Clear();
            materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Nazwa_mat ASC;");

            while (!materialyBUS.eof)
            {
                listBoxMaterialy.Items.Add(materialyBUS.VO.Nazwa_mat);
                materialyBUS.skip();
            }
        }// OdswiezMaterialy()

        /// <summary>
        /// Odświeża checked boxy dostawców w zakładce materiały.
        /// </summary>
        private void OdswiezDostawcowWMaterialach()
        {
            nsAccess2DB.Dostawca_matBUS dostawca_MatBUS = new nsAccess2DB.Dostawca_matBUS(_connString);
            //czyści checkboxy DostawcyMat
            //for (int i = 0; i < checkedListBoxDostawcyMat.Items.Count; i++)
            //{
            //    checkedListBoxDostawcyMat.SetItemChecked(i, false);
            //}

            //textBoxNazwaDostawcy.Text = string.Empty;
            //linkLabelDostawcaMat.Text = string.Empty;
            //richTextBoxDostawca.Text = string.Empty;

            //dostawca_MatBUS.select(); //odświeża wybrane checkboxy dostawcy.
           
            //dostawca_MatBUS.idx = checkedListBoxDostawcyMat.SelectedIndex;
            //toolStripStatusLabel_ID_Dostawcy.Text = dostawca_MatBUS.VO.Identyfikator.ToString();

            //textBoxNazwaDostawcy.Text = dostawca_MatBUS.VO.Nazwa_dostawca_mat.ToString();
            //linkLabelDostawcaMat.Text = dostawca_MatBUS.VO.Link_dostawca_mat.ToString();
            //richTextBoxDostawca.Text = dostawca_MatBUS.VO.Dod_info_dostawca_mat.ToString();

            //dostawca_MatBUS.select(); //odświeża wybrane checkboxy dostawcy.
            dostawca_MatBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC;"); //odświeża wybrane checkboxy dostawcy.

            radioButtonNazwa_mat.Checked = true; // przy przejściu do zakładki materiały zaznaczone sortowanie po nazwie.
            WybierzMagazyn();
            WypelnijMaterialyNazwami();
            WypelnijJednostka_miar();
            WypelnijRodzaj_mat();
            WypelnijCheckedBoxDostawcow(checkedListBoxDostawcyMat);// wypełnia checked boxy dostawców materialów.

            if (listBoxMaterialy.Items.Count > 0)
            {
                listBoxMaterialy.SelectedIndex = 0;
            }

        }// OdswiezDostawców

        /// <summary>
        /// Wybiera rodzaj magazynu (materiały lub normalia).
        /// </summary>
        private void WybierzMagazyn()
        {
            nsAccess2DB.Wybor_magazynuBUS wybor_MagazynuBUS = new nsAccess2DB.Wybor_magazynuBUS(_connString);
            nsAccess2DB.Wybor_magazynuVO wybor_MagazynuVO = new nsAccess2DB.Wybor_magazynuVO();

            comboBoxWyborMagazyn.Items.Clear();

            wybor_MagazynuBUS.select();
            wybor_MagazynuBUS.top();
            while (!wybor_MagazynuBUS.eof)
            {
                wybor_MagazynuVO = wybor_MagazynuBUS.VO;
                comboBoxWyborMagazyn.Items.Add(wybor_MagazynuVO.Magazyn);
                wybor_MagazynuBUS.skip();
            }
        }// WybierzMagazyn()

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxWyborMagazyn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBoxWyborMagazyn_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.Wybor_magazynuBUS wybor_MagazynuBUS = new nsAccess2DB.Wybor_magazynuBUS(_connString);
            wybor_MagazynuBUS.idx = comboBoxWyborMagazyn.SelectedIndex;
        }// comboBoxWyborMagazyn_SelectedIndexChanged(object sender, EventArgs e)

        /// <summary>
        /// Wypełnia jednostkę miar dla Materiałów i Normaliów.
        /// </summary>
        private void WypelnijJednostka_miar()
        {
            nsAccess2DB.Jednostka_miarBUS jednostka_MiarBUS = new nsAccess2DB.Jednostka_miarBUS(_connString);
            nsAccess2DB.Jednostka_miarVO VO;
            comboBoxJednostkaMat.Items.Clear();

            jednostka_MiarBUS.select();
            jednostka_MiarBUS.top();
            while (!jednostka_MiarBUS.eof)
            {
                VO = jednostka_MiarBUS.VO;
                comboBoxJednostkaMat.Items.Add(VO.Nazwa_jednostka_miar);
                jednostka_MiarBUS.skip();
            }
        }// WypelnijJednostka_miar()

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxJednostka_mat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBoxJednostka_mat_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.Jednostka_miarBUS jednostka_MiarBUS = new nsAccess2DB.Jednostka_miarBUS(_connString);
            jednostka_MiarBUS.idx = comboBoxJednostkaMat.SelectedIndex;
            // jednostki wyświetlane labelach dane z comboBoxJednostkaMat.
            string s = comboBoxJednostkaMat.Text;

            labelJednostkaDostepnosc.Text = s;
            labelJednostkaMin.Text = s;
            labelJednostkaZuzycie.Text = s;
            labelJednostkaOdpad.Text = s;
            labelJednostkaZapotrzebowanie.Text = s;
        }// comboBoxJednostka_mat_SelectedIndexChanged

        /// <summary>
        /// Wypełnia rodzaj materiału (zakładka materiały).
        /// </summary>
        private void WypelnijRodzaj_mat()
        {
            nsAccess2DB.Rodzaj_matBUS rodzaj_MatBUS = new nsAccess2DB.Rodzaj_matBUS(_connString);
            nsAccess2DB.Rodzaj_matVO VO;
            comboBoxRodzajMat.Items.Clear();

            rodzaj_MatBUS.select();
            rodzaj_MatBUS.top();
            while (!rodzaj_MatBUS.eof)
            {
                VO = rodzaj_MatBUS.VO;
                comboBoxRodzajMat.Items.Add(VO.Nazwa_rodzaj_mat);
                rodzaj_MatBUS.skip();
            }
        }// WypelnijRodzaj_mat()

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxRodzaj control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBoxRodzaj_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.Rodzaj_matBUS rodzaj_MatBUS = new nsAccess2DB.Rodzaj_matBUS(_connString);
            rodzaj_MatBUS.idx = comboBoxJednostkaMat.SelectedIndex;
        }// comboBoxRodzaj_SelectedIndexChanged(object sender, EventArgs e)

        /// <summary>
        /// Klinknięcie w link otwiera stronę z linka.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelDostawcaMat2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelDostawcaMat2.LinkVisited = true; //zmiana koloru przy odwiedzeniu linka.

            string link = linkLabelDostawcaMat2.Text;
            System.Diagnostics.Process.Start(link); //nawigacja do strony.
        }//linkLabelDostawcaMat2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)

        // //////////////////////////////////////////////////  Radio buttony zakładki materiały.

        /// <summary>
        /// Sortowanie po nazwie materiału (zakładka materiał).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonNazwa_mat_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonNazwa_mat.Checked)
            {
                listBoxMaterialy.Items.Clear();
                CzyscDaneMaterialy();

                nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Nazwa_mat ASC;");
                while (!materialyBUS.eof)
                {
                    listBoxMaterialy.Items.Add(materialyBUS.VO.Nazwa_mat);
                    materialyBUS.skip();
                }

                if (listBoxMaterialy.Items.Count > 0)
                {
                    listBoxMaterialy.SelectedIndex = 0;
                }
            }
        }// radioButtonNazwa_mat_CheckedChanged(object sender, EventArgs e)

        /// <summary>
        /// Sortowanie po typie materiału (zakładka materiał).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonTyp_mat_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTyp_mat.Checked)
            {
                listBoxMaterialy.Items.Clear();
                CzyscDaneMaterialy();

                nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Typ_mat ASC;");
                while (!materialyBUS.eof)
                {
                    listBoxMaterialy.Items.Add(materialyBUS.VO.Typ_mat + " -> " + materialyBUS.VO.Nazwa_mat);
                    materialyBUS.skip();
                }
                if (listBoxMaterialy.Items.Count > 0)
                {
                    listBoxMaterialy.SelectedIndex = 0;
                }
            }
        }//radioButtonTyp_mat_CheckedChanged(object sender, EventArgs e)

        /// <summary>
        /// Sortowanie po stanie minimalnym materiału (zakładka materiał).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonStan_min_mat_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonStan_min_mat.Checked)
            {
                listBoxMaterialy.Items.Clear();
                CzyscDaneMaterialy();

                nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
                nsAccess2DB.Jednostka_miarBUS jednostka_MiarBUS = new nsAccess2DB.Jednostka_miarBUS(_connString);

                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Stan_min_mat ASC;");
                while (!materialyBUS.eof)
                {
                    listBoxMaterialy.Items.Add(materialyBUS.VO.Stan_min_mat + " " + materialyBUS.VO.Jednostka_miar_mat + " -> " + materialyBUS.VO.Nazwa_mat);
                    materialyBUS.skip();
                }
                if (listBoxMaterialy.Items.Count > 0)
                {
                    listBoxMaterialy.SelectedIndex = 0;
                }

            }
        }//radioButtonStan_min_mat_CheckedChanged(object sender, EventArgs e)

        /// <summary>
        /// Sortowanie po ilości materiału w magazynie (zakładka materiał).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonMagazyn_ilosc_mat_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMagazyn_ilosc_mat.Checked)
            {
                listBoxMaterialy.Items.Clear();
                CzyscDaneMaterialy();

                nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Stan_mat ASC;");
                while (!materialyBUS.eof)
                {
                    listBoxMaterialy.Items.Add(materialyBUS.VO.Stan_mat + " " + materialyBUS.VO.Jednostka_miar_mat + " -> " + materialyBUS.VO.Nazwa_mat  );
                    materialyBUS.skip();
                }
                if (listBoxMaterialy.Items.Count > 0)
                {
                    listBoxMaterialy.SelectedIndex = 0;
                }
            }
        }// radioButtonMagazyn_ilosc_mat_CheckedChanged(object sender, EventArgs e)


        // --------- ---------------------------------------przyciski Formularz Materialy

        /// <summary>
        /// Działania po wciśnięciu klawisza Nowa (materiały).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNowa_mat_Click(object sender, EventArgs e)
        {
            CzyscDaneMaterialy();

            textBoxMagazynMat.Enabled = true; // dla nowego materiału odblokowanie pola stanu magazynowego by wpisać pierwszą wartość na start.
            textBoxMagazynMat.BackColor = Color.White;

            textBoxZuzycieMat.Enabled = false; // zablokowanie pola rozchód - Bieżące zużycie i Odpad.
            textBoxZuzycieMat.BackColor = Color.LightSalmon;
            textBoxOdpadMat.Enabled = false;
            textBoxOdpadMat.BackColor = Color.LightSalmon;

            textBoxZapotrzebowanieMat.Enabled = false; // zablokowanie pola przychód - Zakup.
            textBoxZapotrzebowanieMat.BackColor = Color.DarkKhaki;

            // zablokowanie listy materiałów
            listBoxMaterialy.Enabled = false;
            listBoxMaterialy.BackColor = Color.Bisque;
            
            // zablokowanie sortowania materiałów
            groupBoxSorowanieMaterialow.Enabled = false; 

            // zablokowanie wyszukiwania
            textBoxWyszukaj_mat.Enabled = false;
            textBoxWyszukaj_mat.BackColor = Color.Bisque;
            buttonSzukaj_mat.Enabled = false;

            radioButtonNazwa_mat.Checked = false; // wymusza sortowanie po nazwie materiału przy zapisie nowej pozycji Materiału.

            buttonAnulujMat.Enabled = true;
            buttonZapiszMat.Enabled = true;
            buttonUsunMat.Enabled = false;
            buttonNowaMat.Enabled = false;

            //OdswiezMaterialy();
            _statusForm = (int)_status.nowy;
        }//ButtonNowa_Click

        /// <summary>
        /// Aktywuje pola panelu materiałów bez pola stan magazynowy
        /// </summary>
        private void AktywujPanelMaterial()
        {
            // odblokowanie pola rozchód - Bieżące zużycie i Odpad.
            textBoxZuzycieMat.Enabled = true;
            textBoxZuzycieMat.BackColor = Color.White;
            textBoxOdpadMat.Enabled = true;
            textBoxOdpadMat.BackColor = Color.White;

            // odblokowanie pola przychód - Zakup.
            textBoxZapotrzebowanieMat.Enabled = true;
            textBoxZapotrzebowanieMat.BackColor = Color.White;

            // odblokowanie listy materiałów
            listBoxMaterialy.Enabled = true;
            listBoxMaterialy.BackColor = Color.White;

            // odblokowanie sortowania materiałów
            groupBoxSorowanieMaterialow.Enabled = true;

            // odblokowanie wyszukiwania
            textBoxWyszukaj_mat.Enabled = true;
            textBoxWyszukaj_mat.BackColor = Color.White;
            buttonSzukaj_mat.Enabled = true;

            //odblokowanie przycisków
            buttonAnulujMat.Enabled = true;
            buttonZapiszMat.Enabled = true;
            buttonUsunMat.Enabled = true;
            buttonNowaMat.Enabled = true;
        }// AktywujPanelMaterial()

        /// <summary>
        /// Działania po wciśnięciu klawisza Anuluj (materiały).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnuluj_mat_Click(object sender, EventArgs e)
        {
            int idx = listBoxMaterialy.SelectedIndex;

            CzyscDaneMaterialy();
            AktywujPanelMaterial();

            radioButtonNazwa_mat.Checked = true; // przy przejściu do zakładki materiały zaznaczone sortowanie po nazwie.
            WybierzMagazyn();
            WypelnijMaterialyNazwami();
            WypelnijJednostka_miar();
            WypelnijRodzaj_mat();
            WypelnijCheckedBoxDostawcow(checkedListBoxDostawcyMat);// wypełnia checked boxy dostawców materialów.

            if (listBoxMaterialy.Items.Count > 0)
            {
                listBoxMaterialy.SelectedIndex = 0;
            }
            //listBoxMaterialy.SelectedIndex = idx;
           
        }//buttonAnuluj_mat_Click


        /// <summary>
        /// Działania po wciśnięciu klawisza Usuń (materiały).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUsun_mat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć materiał: " + textBoxNazwaMat.Text + "? ", "RemaGUM", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                goto no;
            }

            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            materialyBUS.delete((int)listBoxMaterialy.Tag);

            CzyscDaneMaterialy();
            WypelnijCheckedBoxDostawcow(checkedListBoxDostawcyMat);// wypełnia checked boxy dostawców materialów.
            WypelnijMaterialyNazwami();

            AktywujPanelMaterial();
           
            _statusForm = (int)_status.edycja;

            no:;
        }//buttonUsun_mat_Click

        /// <summary>
        /// Działania po wciśnięciu klawisza Zapisz (materiały).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonZapiszMat_Click(object sender, EventArgs e)
        {
            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            nsAccess2DB.Dostawca_matBUS dostawca_MatBUS = new nsAccess2DB.Dostawca_matBUS(_connString);
            nsAccess2DB.Dostawca_MaterialBUS dostawca_MaterialBUS = new nsAccess2DB.Dostawca_MaterialBUS(_connString);
            nsAccess2DB.MaterialyVO materialy_VO = new nsAccess2DB.MaterialyVO();
            nsAccess2DB.Dostawca_matVO dostawca_MatVO = new nsAccess2DB.Dostawca_matVO();
            nsAccess2DB.Dostawca_MaterialVO dostawca_MaterialVO = new nsAccess2DB.Dostawca_MaterialVO();

            materialy_VO = materialyBUS.VO;

            if (_statusForm == (int)_status.nowy)
            {
                textBoxMagazynMat.Enabled = true; // odblokowanie pola magazynu dla nowego materiału.
                textBoxMagazynMat.BackColor = Color.White;

                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Identyfikator ASC;");
                //materialyBUS.select();
                materialyBUS.idx = materialyBUS.count - 1;
                materialyBUS.VO.Identyfikator = materialyBUS.VO.Identyfikator + 1;

                int materialId = materialyBUS.VO.Identyfikator + 1;

                materialy_VO.Magazyn = comboBoxWyborMagazyn.Text.Trim();
               // komunikaty, które wymuszają wpisanie tekstu przed zapisem materiału.
                // komunikat przy braku nazwy materiału.
                if (textBoxNazwaMat.Text == string.Empty)
                {
                    MessageBox.Show("Uzupełnij nazwę materiału", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                materialy_VO.Nazwa_mat = textBoxNazwaMat.Text.Trim(); // uzupełnia nazwę materiału (nazwa pojawia się w komunikatach).

                //komunikat przy braku wyboru typu materiału.
                if (textBoxTypMat.Text == string.Empty)
                {
                    MessageBox.Show("Proszę wybrać typ materiału dla: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    materialy_VO.Typ_mat = textBoxTypMat.Text.Trim();
                }

                // komunikat przy braku wyboru rodzaju materiału.
                if (comboBoxRodzajMat.Text == string.Empty)
                {
                    MessageBox.Show("Proszę wybrać rodzaj materiału dla: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    materialy_VO.Rodzaj_mat = comboBoxRodzajMat.Text.Trim();
                }

                //komunikat przy braku wyboru typu materiału.
                if (comboBoxJednostkaMat.Text == string.Empty)
                {
                    MessageBox.Show("Proszę wybrać jednostkę dla materiału: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    materialy_VO.Jednostka_miar_mat = comboBoxJednostkaMat.Text.Trim();
                }

                // Stan magazynowy - pole jedynie do odczytu (wyliczane automatycznie). Pole przy nowej pozycji nie może być puste. Komunikat o konieczności wprowadzenia na stan przez pole Zakup. 

                int v;
                if (int.TryParse(textBoxMagazynMat.Text.Trim(), out v))
                {
                    if (textBoxMagazynMat.Text == string.Empty)
                    {
                        MessageBox.Show("Uwaga! Proszę wprowadzić stan magazynowy: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        materialy_VO.Stan_mat = int.Parse(textBoxMagazynMat.Text.Trim()); // pole zablokowane do edycji - jedynie wartość wyliczana automatycznie.
                       
                        if (int.Parse(textBoxMagazynMat.Text.Trim()) < 0)
                        {
                            MessageBox.Show("Uwaga wprowadzono liczbę ujemną! Wprowadz liczbę dodatnią do pola stan magazynowy.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Uwaga nie wprowadzono prawidłowej wartości! Wprowadz liczbę całkowitą do pola stan magazynowy.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // komunikat o błędzie gdy brak wprowadzonego stanu minimalnego materiału.
                if (int.TryParse(textBoxMinMat.Text.Trim(), out v))
                {
                    if (textBoxMinMat.Text == string.Empty)
                    {
                        MessageBox.Show("Uwaga! Proszę wprowadzić stan minimalny dla materiału: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        materialy_VO.Stan_min_mat = int.Parse(textBoxMinMat.Text.Trim());
                        if (int.Parse(textBoxMinMat.Text.Trim()) < 0)
                        {
                            MessageBox.Show("Uwaga wprowadzono liczbę ujemną! Wprowadz liczbę dodatnia do pola stan minimalny dla materiału.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Uwaga nie wprowadzono prawidłowej wartości! Wprowadz liczbę całkowitą do pola stan minimalny materiałów.",  "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // pola uzupełniane zerami w przypadku braku wpisu użytkownika.
               
                //wstawienie 0 w przypadku braku wpisu w pole textBoxZuzycieMat.
                if (textBoxZuzycieMat.Text == string.Empty)
                {
                    textBoxZuzycieMat.Text = "0";
                }
               
                //wstawienie 0 w przypadku braku wpisu w pole textBoxOdpadMat.
                if (textBoxOdpadMat.Text == string.Empty)
                {
                    textBoxOdpadMat.Text = "0";
                }
               
                //wstawienie 0 w przypadku braku wpisu w pole textBoxZapotrzebowanieMat.
                if (textBoxZapotrzebowanieMat.Text == string.Empty)
                {
                    textBoxZapotrzebowanieMat.Text = "0";
                }
              
                // Zapis dostawców przypisanych do danego materiału.

                dostawca_MatBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC;");
                //dostawca_MaterialBUS.select(materialyBUS.VO.Identyfikator);

                for (int i = 0; i < checkedListBoxDostawcyMat.Items.Count; i++)
                {
                    if (checkedListBoxDostawcyMat.GetItemChecked(i))
                    {
                        dostawca_MatBUS.idx = i;
                        dostawca_MaterialBUS.insert(materialyBUS.VO.Identyfikator, dostawca_MatBUS.VO.Identyfikator, materialyBUS.VO.Nazwa_mat);
                    }
                    //dostawca_MatBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC;"); //odświeża wybrane checkboxy dostawcy.
                    
                }
                //listBoxMaterialy.SelectedIndex = materialyBUS.getIdx(materialyBUS.VO.Identyfikator); // ustawienie zaznaczenia w tabeli materiały.
                materialyBUS.write(materialy_VO);

                textBoxZuzycieMat.Enabled = true; // odblokowanie pola rozchód - Bieżące zużycie i Odpad.
                textBoxZuzycieMat.BackColor = Color.White;
                textBoxOdpadMat.Enabled = true;
                textBoxOdpadMat.BackColor = Color.White;

                textBoxZapotrzebowanieMat.Enabled = true; // odblokowanie pola przychód - Zakup.
                textBoxZapotrzebowanieMat.BackColor = Color.White;

                WypelnijMaterialyNazwami();

                materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Nazwa_mat ASC;");
                listBoxMaterialy.SelectedIndex = materialyBUS.getIdx(materialId);

                //dostawca_MatBUS.idx = checkedListBoxDostawcyMat.SelectedIndex;
                //toolStripStatusLabel_ID_Dostawcy.Text = dostawca_MatBUS.VO.Identyfikator.ToString();
                //dostawca_MaterialBUS.select(materialy_VO.Identyfikator);
                //dostawca_MatBUS.select();

            } // if nowy

            else if (_statusForm == (int)_status.edycja)
            {
                materialyBUS.select((int)listBoxMaterialy.Tag);
                if (materialyBUS.count < 1)
                {
                    MessageBox.Show("Materiał o identyfikatorze " + listBoxMaterialy.Tag.ToString() + "nie istnieje w bazie materiałów", "RemaGUM",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    materialy_VO.Identyfikator = (int)listBoxMaterialy.Tag;
                    materialy_VO.Magazyn = comboBoxWyborMagazyn.Text.Trim();

                    // komunikaty, które wymuszają wpisanie tekstu przed zapisem materiału.
                    //komunikat przy braku nazwy materiału.
                    if (textBoxNazwaMat.Text == string.Empty)
                    {
                        MessageBox.Show("Uzupełnij nazwę materiału", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        materialy_VO.Nazwa_mat = textBoxNazwaMat.Text.Trim(); // uzupełnia nazwę materiału (nazwa pojawia się w komunikatach).
                    }

                    // komunikat przy braku wyboru rodzaju materiału.
                    if (comboBoxRodzajMat.Text == string.Empty)
                    {
                        MessageBox.Show("Proszę wybrać rodzaj materiału dla: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        materialy_VO.Rodzaj_mat = comboBoxRodzajMat.Text.Trim();
                    }

                    // komunikat przy braku wyboru typu materiału.
                    if (textBoxTypMat.Text == string.Empty)
                    {
                        MessageBox.Show("Proszę wybrać typ materiału dla: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        materialy_VO.Typ_mat = textBoxTypMat.Text.Trim();
                    }

                    //komunikat przy braku wyboru typu materiału.
                    if (comboBoxJednostkaMat.Text == string.Empty)
                    {
                        MessageBox.Show("Proszę wybrać jednostkę dla materiału: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        materialy_VO.Jednostka_miar_mat = comboBoxJednostkaMat.Text.Trim();
                    }


                    //************************** pola wartości **********
                   
                    int v;


                    if (int.TryParse(textBoxMagazynMat.Text.Trim(), out v))
                    {
                        if (textBoxMagazynMat.Text == string.Empty)
                        {
                            MessageBox.Show("Uwaga! Proszę wprowadzić stan magazynowy: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {

                            int aktualny_stan_Material = int.Parse(textBoxMagazynMat.Text.Trim()) - int.Parse(textBoxZuzycieMat.Text.Trim()) - int.Parse(textBoxOdpadMat.Text.Trim()) + int.Parse(textBoxZapotrzebowanieMat.Text.Trim());
                            //materialyBUS.VO.Stan_mat - materialyBUS.VO.Zuzycie_mat - materialyBUS.VO.Odpad_mat + materialyBUS.VO.Zapotrzebowanie_mat;

                            //materialy_VO.Stan_mat = int.Parse(textBoxMagazynMat.Text.Trim()); // pole zablokowane do edycji - jedynie wartość wyliczana automatycznie.
                            materialy_VO.Stan_mat = aktualny_stan_Material;

                            if (int.Parse(textBoxMagazynMat.Text.Trim()) < 0)
                            {
                                MessageBox.Show("Uwaga wprowadzono liczbę ujemną! Wprowadz liczbę dodatnią do pola stan magazynowy.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Uwaga nie wprowadzono prawidłowej wartości! Wprowadz liczbę całkowitą do pola stan magazynowy.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (int.TryParse(textBoxMinMat.Text.Trim(), out v))
                    {
                        if (textBoxMinMat.Text == string.Empty)
                        {
                            MessageBox.Show("Uwaga! Wprowadz wymagany stan minimalny dla materiału: " + materialy_VO.Nazwa_mat, "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);   // komunikat o błędzie gdy brak wprowadzonego stanu minimalnego materiału.
                            return;
                        }
                        else
                        {
                            materialy_VO.Stan_min_mat = int.Parse(textBoxMinMat.Text.Trim());
                            if (int.Parse(textBoxMinMat.Text.Trim()) < 0)
                            {
                                MessageBox.Show("Uwaga wprowadzono liczbę ujemną! Wprowadz liczbę dodatnią do pola stan minimalny dla materiału.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Uwaga nie wprowadzono prawidłowej wartości! Wprowadz liczbę całkowitą do pola stan minimalny materiałów.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (int.TryParse(textBoxZuzycieMat.Text.Trim(), out v))
                    {
                        if (textBoxZuzycieMat.Text == string.Empty) //wstawienie 0 w przypadku braku wpisu w pole textBoxZuzycieMat.
                        {
                            textBoxZuzycieMat.Text = "0";
                        }
                        else
                        {
                            materialy_VO.Zuzycie_mat = int.Parse(textBoxZuzycieMat.Text.Trim());
                            if (int.Parse(textBoxZuzycieMat.Text.Trim()) < 0)
                            {
                                MessageBox.Show("Uwaga wprowadzono liczbę ujemną! Wprowadz liczbę dodatnią do pola bieżące zużycie materiału.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Uwaga nie wprowadzono prawidłowej wartości! Wprowadz liczbę całkowitą do pola bieżące zużycie materiału.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    if (int.TryParse(textBoxOdpadMat.Text.Trim(), out v))
                    {
                        if (textBoxOdpadMat.Text == string.Empty)
                        {
                            textBoxOdpadMat.Text = "0";  //wstawienie 0 w przypadku braku wpisu w pole textBoxOdpadMat.
                        }
                        else
                        {
                            materialy_VO.Odpad_mat = int.Parse(textBoxOdpadMat.Text.Trim());
                            if (int.Parse(textBoxOdpadMat.Text.Trim()) < 0)
                            {
                                MessageBox.Show("Uwaga wprowadzono liczbę ujemną! Wprowadz liczbę dodatnią do pola odpad materiału.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Uwaga nie wprowadzono prawidłowej wartości! Wprowadz liczbę całkowitą do pola odpad materiału.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



                    if (int.TryParse(textBoxZapotrzebowanieMat.Text.Trim(), out v))
                    {
                        //wstawienie 0 w przypadku braku wpisu w pole textBoxZapotrzebowanieMat.
                        if (textBoxZapotrzebowanieMat.Text == string.Empty)
                        {
                            textBoxZapotrzebowanieMat.Text = "0";
                        }
                        else
                        {
                            materialy_VO.Zapotrzebowanie_mat = int.Parse(textBoxZapotrzebowanieMat.Text.Trim());
                            if (int.Parse(textBoxOdpadMat.Text.Trim()) < 0)
                            {
                                MessageBox.Show("Uwaga wprowadzono liczbę ujemną! Wprowadz liczbę dodatnią do pola zakup materiału.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Uwaga nie wprowadzono prawidłowej wartości! Wprowadz liczbę całkowitą do pola zakup materiału.", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                    materialyBUS.write(materialy_VO); // ZAPIS

                    dostawca_MaterialBUS.select(materialyBUS.VO.Identyfikator);


                    int aktualizacjaStanuMagazyn = materialyBUS.VO.Stan_mat - materialyBUS.VO.Zuzycie_mat - materialyBUS.VO.Odpad_mat + materialyBUS.VO.Zapotrzebowanie_mat;

                    textBoxMagazynMat.Text = aktualizacjaStanuMagazyn.ToString();

                    // Zapis dostawców przypisanych do danego materiału.
                    dostawca_MatBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC;");
                    //dostawca_MatBUS.select();

                    for (int i = 0; i < checkedListBoxDostawcyMat.Items.Count; i++)
                    {
                        if (checkedListBoxDostawcyMat.GetItemChecked(i))
                        {
                            dostawca_MatBUS.idx = i;

                            dostawca_MatVO = dostawca_MatBUS.VO;

                            dostawca_MaterialBUS.insert(materialy_VO.Identyfikator, dostawca_MatVO.Identyfikator, materialy_VO.Nazwa_mat);
                        }
                    }
                    //OdswiezDostawcow();
                    dostawca_MatBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC;"); //odświeża wybrane checkboxy dostawcy.
                    //dostawca_MatBUS.select();
                    dostawca_MatBUS.idx = checkedListBoxDostawcyMat.SelectedIndex;
                    toolStripStatusLabel_ID_Dostawcy.Text = dostawca_MatBUS.VO.Identyfikator.ToString();
                }

                listBoxMaterialy.SelectedIndex = materialyBUS.getIdx(materialyBUS.VO.Identyfikator);
            }//else if edycja

            //OdswiezMaterialy();
            WypelnijCheckedBoxDostawcow(checkedListBoxDostawcyMat);// wypełnia checked boxy dostawców materialów.
            OdswiezDostawcowWMaterialach();

            textBoxMagazynMat.Enabled = false; // zablokowanie pola magazynu po zapisie.
            textBoxMagazynMat.BackColor = Color.Bisque;

            listBoxMaterialy.Enabled = true;

            buttonAnulujMat.Enabled = true;
            buttonZapiszMat.Enabled = true;
            buttonUsunMat.Enabled = true;
            buttonNowaMat.Enabled = true;


            pokazKomunikat("Pozycja zapisana w bazie");

            _statusForm = (int)_status.edycja;
        }//buttonZapiszMat_Clic


        //TODO wyszukiwarka materiałów.
        /// <summary>
        /// Wyszukuje materiał po dowolnym ciągu znaków w nazwie lub rodzaju materiału.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSzukaj_mat_Click(object sender, EventArgs e)
        {
            radioButtonNazwa_mat.Checked = true;

            textBoxWyszukaj_mat.Text = textBoxWyszukaj_mat.Text.Trim();

            if (textBoxWyszukaj_mat.Text == string.Empty)
            {
            pokazKomunikat("Proszę wpisać tekst do pola wyszukiwania. Szukanie anulowane.");
            return;
            }
            Cursor.Current = Cursors.WaitCursor;
           
            nsAccess2DB.MaterialyBUS materialyBUS = new nsAccess2DB.MaterialyBUS(_connString);
            nsAccess2DB.MaterialyVO materialyVO = new nsAccess2DB.MaterialyVO();

            materialyBUS.selectQuery("SELECT * FROM Materialy ORDER BY Nazwa_mat ASC;");

            string s1 = textBoxWyszukaj_mat.Text.ToUpper();
            string s2;

            for (int i = _materialSzukajIdx; i < materialyBUS.count; i++)
            {
                materialyBUS.idx = i;
                materialyVO = materialyBUS.VO;

                s2 = materialyVO.Nazwa_mat.ToUpper();

                if (s2.Contains(s1))
                {
                    _materialSzukajIdx = i;
                    listBoxMaterialy.SelectedIndex = i;
                    if (MessageBox.Show("czy szukać dalej ?", "RemaGUM", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                    {
                        goto cancel;
                    }
                }
            }
            pokazKomunikat("Aby szukać od poczatku wciśnij szukaj.");
            cancel:;
            _materialSzukajIdx = 0;
            Cursor.Current = Cursors.Default;
        }// button buttonSzukaj_mat_Click

        // TODO //  //  //  //  //  //  //  //  //  //  //  //  //  //  // ZAKŁADKA DOSTAWCY MATERIAŁóW

        /// <summary>
        /// Wypełnia listę dostawców wg nazwy.
        /// </summary>
        private void WypelnijListeDostawcowDanymi()
        {
            nsAccess2DB.Dostawca_matBUS dostawca_MatBUS = new nsAccess2DB.Dostawca_matBUS(_connString);

            listBoxDostawcy.Items.Clear();
            dostawca_MatBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC");
           
            while (!dostawca_MatBUS.eof)
            {
                listBoxDostawcy.Items.Add(dostawca_MatBUS.VO.Nazwa_dostawca_mat);
                dostawca_MatBUS.skip();
            }
        }// WypelnijDostawcowDanymi()

        /// <summary>
        /// Zmiana indeksu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxDostawcy_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.Dostawca_matBUS dostawca_MatBUS = new nsAccess2DB.Dostawca_matBUS(_connString);
            dostawca_MatBUS.selectQuery("SELECT * FROM Dostawca_mat ORDER BY Nazwa_dostawca_mat ASC");

            dostawca_MatBUS.idx = listBoxDostawcy.SelectedIndex;
            listBoxDostawcy.Tag = dostawca_MatBUS.VO.Identyfikator;
            toolStripStatusLabelDostawcy.Text = dostawca_MatBUS.VO.Identyfikator.ToString();

            textBoxNazwaDostawcy.Text = dostawca_MatBUS.VO.Nazwa_dostawca_mat;
            richTextBoxDostawca.Text = dostawca_MatBUS.VO.Dod_info_dostawca_mat;
            linkLabelDostawcaMat.Text = dostawca_MatBUS.VO.Link_dostawca_mat;
            textBoxLink.Text = dostawca_MatBUS.VO.Link_dostawca_mat;
        } // private void listBoxDostawcy_SelectedIndexChanged

        /// <summary>
        /// Tworzy nowego dostawcę (wymaga przycisku zapisz).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNowyDostawca_Click(object sender, EventArgs e)
        {
            CzyscDaneDostawcy();
           
            // aktywacja pola na wpisanie linku nowego dostawcy.
            textBoxLink.Text = string.Empty;
            textBoxLink.Enabled = true;
            textBoxLink.BackColor = Color.White;

            listBoxDostawcy.Enabled = false; // zablokowanie listy dostawców.

            // przyciski panelu dostawcy
            buttonZapiszDostawca.Enabled = true;
            buttonAnulujDostawca.Enabled = true;
            buttonUsunDostawca.Enabled = false;
            buttonNowyDostawca.Enabled = false;

            _statusForm = (int)_status.nowy;
        }// buttonNowyDostawca_Click

        /// <summary>
        /// otwiera link przy kliknięciu.
        /// </summary>
        /// <param name = "sender" ></ param >
        /// < param name= "e" ></ param >
        private void linkLabelDostawcaMat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelDostawcaMat.LinkVisited = true; //zmiana koloru przy odwiedzeniu linka.

            string link = linkLabelDostawcaMat.Text;
            System.Diagnostics.Process.Start(link); //nawigacja do strony.
        }// linkLabelDostawcaMat_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)


        /// <summary>
        /// Usuwa link z obiektu (wymaga uzycia klawisza zapisz).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUsunLink_Click(object sender, EventArgs e)
        {
            linkLabelDostawcaMat2.Text = string.Empty;

            pokazKomunikat("Usunięcie linku wymaga zatwierdzenia przyciskiem Zapisz.");

        }//buttonUsunLink_Click

        /// <summary>
        /// Akcje po wciśnięciu przycisku Zapisz.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonZapiszDostawca_Click(object sender, EventArgs e)
        {
            nsAccess2DB.Dostawca_matBUS dostawca_MatBUS = new nsAccess2DB.Dostawca_matBUS(_connString);
            nsAccess2DB.Dostawca_matVO dostawca_MatVO = new nsAccess2DB.Dostawca_matVO();

            if (textBoxNazwaDostawcy.Text == string.Empty)
            {
                MessageBox.Show("Uzupełnij nazwę dostawcy", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (_statusForm == (int)_status.nowy)
            {
                dostawca_MatBUS.select();
                dostawca_MatBUS.idx = dostawca_MatBUS.count - 1;
                dostawca_MatVO.Identyfikator = dostawca_MatBUS.VO.Identyfikator + 1;

                dostawca_MatVO.Nazwa_dostawca_mat = textBoxNazwaDostawcy.Text.Trim();
                dostawca_MatVO.Dod_info_dostawca_mat = richTextBoxDostawca.Text.Trim();
                dostawca_MatVO.Link_dostawca_mat = textBoxLink.Text.Trim();

                dostawca_MatBUS.write(dostawca_MatVO);
                dostawca_MatBUS.select();

                listBoxDostawcy.SelectedIndex = dostawca_MatBUS.getIdx(dostawca_MatBUS.VO.Identyfikator); // ustawienie znacznika w liście dostawcy
            }// if nowy
            else if (_statusForm == (int)_status.edycja)
            {
                dostawca_MatBUS.select((int)listBoxDostawcy.Tag);
                if (dostawca_MatBUS.count < 1)
                {
                    MessageBox.Show("Dostawca o identyfikatorze " + listBoxDostawcy.Tag.ToString() + "nie istnieje w bazie dostawców", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    dostawca_MatVO.Identyfikator = (int)listBoxDostawcy.Tag;
                    dostawca_MatVO.Nazwa_dostawca_mat = textBoxNazwaDostawcy.Text.Trim();
                    dostawca_MatVO.Dod_info_dostawca_mat = richTextBoxDostawca.Text.Trim();
                    dostawca_MatVO.Link_dostawca_mat = textBoxLink.Text.Trim();

                    dostawca_MatBUS.write(dostawca_MatVO);
                    dostawca_MatBUS.select(dostawca_MatVO.Identyfikator);
                }
                listBoxDostawcy.SelectedIndex = dostawca_MatBUS.getIdx(dostawca_MatBUS.VO.Identyfikator); //znacznik id dostawcy.
            }// else if -> edycja
            WypelnijListeDostawcowDanymi();
           
            //aktywacja przycisków Dostawcy
            buttonNowyDostawca.Enabled = true;
            buttonAnulujDostawca.Enabled = true;
            buttonUsunDostawca.Enabled = true;
            buttonZapiszDostawca.Enabled = true;
            buttonUsunLink.Enabled = true;

            listBoxDostawcy.Enabled = true; // aktywacja listy dostawców

            pokazKomunikat("Dostawca zapisany w bazie");
            _statusForm = (int)_status.edycja;
        }// private void buttonZapiszDostawca_Click

        /// <summary>
        /// działania po wciśnięciu przycisku Anuluj.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnulujDostawca_Click(object sender, EventArgs e)
        {
            CzyscDaneDostawcy(); // po zapisie linku pole puste

            textBoxLink.Enabled = true; // aktywacja pola linku dostawcy
            textBoxLink.BackColor = Color.White;

            checkedListBoxDostawcyMat.Enabled = true; // anulowanie zapisu aktywuje całośc formularza

            //aktywacja przycisków Dostawcy
            buttonNowyDostawca.Enabled = true;
            buttonAnulujDostawca.Enabled = true;
            buttonUsunDostawca.Enabled = true;
            buttonZapiszDostawca.Enabled = true;
            buttonUsunLink.Enabled = true;

            listBoxDostawcy.Enabled = true; // aktywacja listy dostawców
            //OdswiezDostawcow();
            _statusForm = (int)_status.edycja;
        }// private void buttonAnulujDostawca_Click

        /// <summary>
        /// Czyści pola dane dostawcy w zakładce Dostawca.
        /// </summary>
        private void CzyscDaneDostawcy()
        {
            //toolStripStatusLabelDostawcy.Text = "";
            try
            {
                textBoxNazwaDostawcy.Text = string.Empty;
                richTextBoxDostawca.Text = string.Empty;
                textBoxLink.Text = string.Empty;
                linkLabelDostawcaMat.Text = string.Empty;
            }
            catch { }
        }// CzyscDaneDostawcy()

        /// <summary>
        /// Usuwa wybranego dostawcę po Identyfikatorze.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUsunDostawca_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć dostawcę: " + textBoxNazwaDostawcy.Text + "? ", "RemaGUM", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                goto no;
            }
            
            nsAccess2DB.Dostawca_matBUS dostawca_MatBUS = new nsAccess2DB.Dostawca_matBUS(_connString);
            nsAccess2DB.Dostawca_MaterialBUS dostawca_MaterialBUS = new nsAccess2DB.Dostawca_MaterialBUS(_connString);

            CzyscDaneDostawcy();

            dostawca_MatBUS.delete((int)listBoxDostawcy.Tag); // usunięcie danych dysponenta.
            dostawca_MaterialBUS.delete((int)listBoxDostawcy.Tag); // 

            WypelnijListeDostawcowDanymi();
            _statusForm = (int)_status.edycja;

            no:;
        }//  private void buttonUsunDostawca_Click

        // TODO //  //  //  //  //  //  //  //  //  //  //  //  //  //   ZAKŁADKA OPERATORZY MASZYN.

        /// <summary>
        /// wyświetla listę operatorów maszyn po imieniu i nazwisku.
        /// </summary>
        private void WypelnijOperatorowDanymi()
        {

            nsAccess2DB.OperatorBUS operatorBUS = new nsAccess2DB.OperatorBUS(_connString);
            operatorBUS.selectQuery("SELECT * FROM Operator ORDER BY Op_nazwisko ASC;");

            listBoxOperator.BeginUpdate();
            listBoxOperator.Items.Clear();
          
            while (!operatorBUS.eof)
            {
                listBoxOperator.Items.Add(operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                operatorBUS.skip();
            }

            listBoxOperator.EndUpdate();

            //if (listBoxOperator.Items.Count > 0)
            //{
            //    listBoxOperator.SelectedIndex = 0;
            //}
        }//WypelnijOperatorowDanymi()

        /// <summary>
        /// Czyści dane formularza operatorzy maszyn (zakładka operatorzy maszyn).
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

            richTextBoxUprawnieniaOperatora.Text = string.Empty;

            textBoxUprawnienieOperator.Text = string.Empty;
            dateTimePickerDataKoncaUprOp.Text = string.Empty;
            listBoxMaszynyOperatora.Items.Clear();
        }//CzyscDaneOperatora()

        /// <summary>
        /// Odświeża listBoxOperator.
        /// </summary>
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

            //czyści pole Uprawnienia Operatora przy zmianie indeksu.
            richTextBoxUprawnieniaOperatora.Text = string.Empty;

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

                // komunikat o zbliżającym się terminie końca uprawnień operatora (wybraną ilośc dni (od 1 do 14) przed końcem).
                DateTime currentDate = DateTime.Now;
                long terminUpr = dateTimePickerDataKoncaUprOp.Value.Ticks - currentDate.Ticks;
                TimeSpan timeSpan = new TimeSpan(terminUpr);

                if ((timeSpan.Days >= 1) && (timeSpan.Days <= 14))
                {
                    richTextBoxUprawnieniaOperatora.Text = ("Uwaga uprawnienia pracownika: " + operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie + " kończą się w dniu: " + operatorBUS.VO.Rok + "-" + operatorBUS.VO.Mc + "-" + operatorBUS.VO.Dzien + ".");
                }
                else if (timeSpan.Days <= 0)
                {
                    richTextBoxUprawnieniaOperatora.Text = ("Uwaga uprawnienia pracownika: " + operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie + " skończyły się w dniu: " + operatorBUS.VO.Rok + "-" + operatorBUS.VO.Mc + "-" + operatorBUS.VO.Dzien + ".");
                }
                else
                {
                    richTextBoxUprawnieniaOperatora.Text = string.Empty;
                }

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
                    _maszynaTag[idx] = maszyny_OperatorBUS.VO.ID_maszyny;

                    maszynyBUS.selectQuery("SELECT * FROM Maszyny WHERE Identyfikator = " + maszyny_OperatorBUS.VO.ID_maszyny);

                    listBoxMaszynyOperatora.Items.Add(maszynyBUS.VO.Nazwa);
                    maszyny_OperatorBUS.skip();
                    idx++;
                }

            }
            catch { }
        }//listBoxOperator_maszyny_SelectedIndexChanged

        /// <summary>
        /// wyświetla listę maszyn dla danego operatora.
        /// </summary>
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

        /// <summary>
        /// Zmiana indeksu dla comboBoxOperator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    listBoxOperator.Items.Add(operatorBUS.VO.Dzien + "-" + operatorBUS.VO.Mc + "-" + operatorBUS.VO.Rok + " (" + operatorBUS.VO.Uprawnienie + ") " + operatorBUS.VO.Op_nazwisko + " " + operatorBUS.VO.Op_imie);
                    operatorBUS.skip();
                }
            }
            if (comboBoxOperator.SelectedIndex == 4)
            {
                operatorBUS.selectQuery("SELECT * FROM Operator WHERE Op_nazwisko LIKE '" + textBoxWyszukiwanieOperator.Text + "%' OR Op_imie LIKE '" + textBoxWyszukiwanieOperator.Text + "%';");
                operatorBUS.idx = listBoxOperator.SelectedIndex;
                toolStripStatusLabelIDOperatora.Text = operatorBUS.VO.Identyfikator.ToString();
            }
            //if (listBoxOperator.Items.Count > 0)
            //{
            //    listBoxOperator.SelectedIndex = 0;
            //}
        }//comboBoxOperator_SelectedIndexChanged

        /// <summary>
        /// Wypełnia dział wskazanego Operatora.
        /// </summary>
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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxDzial_operator_maszyny control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBoxDzial_operator_maszyny_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.DzialBUS dzialBUS = new nsAccess2DB.DzialBUS(_connString);
            dzialBUS.idx = comboBoxDzialOperator.SelectedIndex;
            comboBoxDzialOperator.Tag = dzialBUS.VO.Nazwa;
        }//comboBoxDzial_SelectedIndexChanged


        // -------------------  Przyciski w zakładce Operator.
        /// <summary>
        /// Działania po wciśnięciu klawisza Nowa (zakładka operator).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNowaOperator_Click(object sender, EventArgs e)
        {
            CzyscDaneOperatora();

            listBoxOperator.Enabled = false; //dezaktywacja listBoxOperator
            listBoxOperator.BackColor = Color.Bisque;

            //dezaktywacja pola sortowania
            comboBoxOperator.Enabled = false;
            
            //dezaktywacja pola wyszukiwania i przycisku szukania operatora
            textBoxWyszukiwanieOperator.Enabled = false;
            textBoxWyszukiwanieOperator.BackColor = Color.Bisque;
            buttonSzukajOperator.Enabled = false;

            // listBoxMaszynyOperatora jest z założenia nieaktywny ma jedynie zmienić kolor
            listBoxMaszynyOperatora.BackColor = Color.Bisque;

            buttonNowaOperator.Enabled = false;
            buttonZapiszOperator.Enabled = true;
            buttonAnulujOperator.Enabled = true;
            buttonUsunOperator.Enabled = false;

            _statusForm = (int)_status.nowy;
        }//buttonNowaOperator_Click

        /// <summary>
        /// Działania po wciśnięciu klawisza Zapisz (zakładka operator).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            if (textBoxUprawnienieOperator.Text == string.Empty)// textBoxUprawnienia nie może być puste
            {
                MessageBox.Show("Proszę uzupełnij rodzaj uprawnień operatora maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //komunikat o złej dacie końca uprawnień
            DateTime currentDate = DateTime.Now;
            long terminUpr = dateTimePickerDataKoncaUprOp.Value.Ticks - currentDate.Ticks;
            TimeSpan timeSpan = new TimeSpan(terminUpr);

            if ((timeSpan.Days < 0))
            {
                if (MessageBox.Show("Czy chcesz ustawić datę końca uprawnień na datę wcześniejszą niż dzisiejsza.", "RemaGUM", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    goto no;
                }
            }
            else if ((timeSpan.Days == 0))
            {
                if (MessageBox.Show("Czy chcesz ustawić datę końca uprawnień na dzień dzisiejszy.", "RemaGUM", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    goto no;
                }
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
            WypelnijOperatorowMaszynami();
           //OdswiezOperatorowMaszyn();
            comboBoxOperator.SelectedIndex = 0;

            //aktywacja listBoxOperator
            listBoxOperator.BackColor = Color.White;
            listBoxOperator.Enabled = true; 

            //aktywacja pola sortowania
            comboBoxOperator.Enabled = true;
            
            //aktywacja pola wyszukiwania i przycisku szukania operatora
            textBoxWyszukiwanieOperator.Enabled = true;
            textBoxWyszukiwanieOperator.BackColor = Color.White;
            buttonSzukajOperator.Enabled = true;

            // listBoxMaszynyOperatora jest z założenia nieaktywny ma jedynie zmienić kolor
            listBoxMaszynyOperatora.BackColor = Color.White;

            buttonNowaOperator.Enabled = true;
            buttonZapiszOperator.Enabled = true;
            buttonAnulujOperator.Enabled = true;
            buttonUsunOperator.Enabled = true;
            
            pokazKomunikat("Pozycja zapisana w bazie");

            _statusForm = (int)_status.edycja;
            no:;
        }// buttonZapiszOperator_Click

        /// <summary>
        /// Działania po wciśnięciu klawisza Anuluj (zakładka operator).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnulujOperator_Click(object sender, EventArgs e)
        {
            CzyscDaneOperatora();

            int idx = listBoxOperator.SelectedIndex;

            //aktywacja listBoxOperator
            listBoxOperator.BackColor = Color.White;
            listBoxOperator.Enabled = true;

            //aktywacja pola sortowania
            comboBoxOperator.Enabled = true;
            
            //aktywacja pola wyszukiwania i przycisku szukania operatora
            textBoxWyszukiwanieOperator.Enabled = true;
            textBoxWyszukiwanieOperator.BackColor = Color.White;
            buttonSzukajOperator.Enabled = true;

            // listBoxMaszynyOperatora jest z założenia nieaktywny ma jedynie zmienić kolor
            listBoxMaszynyOperatora.BackColor = Color.White;

            buttonNowaOperator.Enabled = true;
            buttonZapiszOperator.Enabled = true;
            buttonAnulujOperator.Enabled = true;
            buttonUsunOperator.Enabled = true;

            _statusForm = (int)_status.edycja;

        } //buttonAnulujOperator_Click

        /// <summary>
        /// Działania po wciśnięciu klawisza Usuń (zakładka operator).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUsunOperator_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Czy na pewno chcesz usunąć operatora: " + textBoxImieOperator.Text + " "+ textBoxNazwiskoOperator.Text + "?", "RemaGUM", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                goto no;
            }
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

            no:;
        }// buttonUsunOperator_Clik

        /// <summary>
        /// Działania po wciśnięciu klawisza Szukaj (zakładka operator).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            if (listBoxOperator.Text == string.Empty)
            { pokazKomunikat("Nie znaleziono szukanego tekstu. Wyszukaj ponownie."); }
        }// buttonSzukajOperator_Click

        // TODO // // // // // // // // // // // // // // // // // ZAKŁADKA DYSPONENT MASZYNY.

        /// <summary>
        /// wyświetla listę dysponentów maszyn - nazwisko i imię.
        /// </summary>
        private void WypelnijDysponentowDanymi()
        {
            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            listBoxDysponent.Items.Clear();
            dysponentBUS.selectQuery("SELECT * FROM Dysponent ORDER BY Dysp_nazwisko ASC;");

            listBoxDysponent.BeginUpdate();
            while (!dysponentBUS.eof)
            {
                //dysponentVO = dysponentBUS.VO;
                listBoxDysponent.Items.Add(dysponentBUS.VO.Dysp_nazwisko + " " + dysponentBUS.VO.Dysp_imie);
                dysponentBUS.skip();
            }
            listBoxDysponent.EndUpdate();

        }// WypelnijDysponentowDanymi()

        /// <summary>
        /// Czyści dane w zakładce dysponent maszyn.
        /// </summary>
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

        /// <summary>
        /// Odświeża listBoxDysponent.
        /// </summary>
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

        /// <summary>
        /// Wypełnia dane listBoxMaszynyDysponenta
        /// </summary>
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
        }// WypelnijDysponentowMaszynami()

        /// <summary>
        /// Wypełnia dział zakładka dysponent.
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

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBoxDzial_dysponent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBoxDzial_dysponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            nsAccess2DB.DzialBUS dzialBUS = new nsAccess2DB.DzialBUS(_connString);
            dzialBUS.idx = comboBoxDzialDysponent.SelectedIndex;
            comboBoxDzialDysponent.Tag = dzialBUS.VO.Nazwa;
        }// comboBoxDzial_dysponent_SelectedIndexChanged



        // ----------------------------------------------Przyciski w zakładce Dysponent.
        /// <summary>
        /// Działania po wciśnięciu klawisza Nowa (zakładka dysponent).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNowaDysponent_Click(object sender, EventArgs e)
        {
            CzyscDaneDysponenta();

            //zablokowanie listy dysponentów
            listBoxDysponent.Enabled = false;
            listBoxDysponent.BackColor = Color.Bisque;

            //zablokowanie wyszukiwania i przycisku wyszukaj dysponenta
            textBoxWyszukiwanieDysponent.Enabled = false;
            textBoxWyszukiwanieDysponent.BackColor = Color.Bisque;
            buttonSzukajDysponent.Enabled = false;

            //zablokowanie listy maszyn Dysponenta (pole jest z założenia nieaktywne ma jedynie zmienić kolor)
            listBoxMaszynyDysponenta.BackColor = Color.Bisque;


            buttonNowaDysponent.Enabled = false;
            buttonZapiszDysponent.Enabled = true;
            buttonAnulujDysponent.Enabled = true;
            buttonUsunDysponent.Enabled = false;

            _statusForm = (int)_status.nowy;
        }// buttonNowaDysponent_Click

        /// <summary>
        /// Działania po wciśnięciu klawisza Zapisz (zakładka dysponent).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                MessageBox.Show("Uzupełnij nazwisko dysponenta maszyny", "RemaGUM", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //odblokowanie listy dysponentów
            listBoxDysponent.Enabled = true;
            listBoxDysponent.BackColor = Color.White;

            //odblokowanie wyszukiwania i przycisku wyszukaj dysponenta
            textBoxWyszukiwanieDysponent.Enabled = true;
            textBoxWyszukiwanieDysponent.BackColor = Color.White;
            buttonSzukajDysponent.Enabled = true;

            //odblokowanie listy maszyn Dysponenta (pole jest z założenia nieaktywne ma jedynie zmienić kolor)
            listBoxMaszynyDysponenta.BackColor = Color.White;

            buttonNowaDysponent.Enabled = true;
            buttonZapiszDysponent.Enabled = true;
            buttonAnulujDysponent.Enabled = true;
            buttonUsunDysponent.Enabled = true;

            pokazKomunikat("Pozycja zapisana w bazie");
            _statusForm = (int)_status.edycja;
        }// buttonZapiszDysponent_Click

        /// <summary>
        /// Działania po wciśnięciu klawisza Anuluj (zakładka dysponent).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnulujDysponent_Click(object sender, EventArgs e)
        {
            int idx = listBoxDysponent.SelectedIndex;

            CzyscDaneDysponenta();
            
            //odblokowanie listy dysponentów
            listBoxDysponent.Enabled = true;
            listBoxDysponent.BackColor = Color.White;

            //odblokowanie wyszukiwania i przycisku wyszukaj dysponenta
            textBoxWyszukiwanieDysponent.Enabled = true;
            textBoxWyszukiwanieDysponent.BackColor = Color.White;
            buttonSzukajDysponent.Enabled = true;

            //odblokowanie listy maszyn Dysponenta (pole jest z założenia nieaktywne ma jedynie zmienić kolor)
            listBoxMaszynyDysponenta.BackColor = Color.White;

            buttonNowaDysponent.Enabled = true;
            buttonZapiszDysponent.Enabled = true;
            buttonAnulujDysponent.Enabled = true;
            buttonUsunDysponent.Enabled = true;

            _statusForm = (int)_status.edycja;
        }// buttonAnulujDysponent_Click

        /// <summary>
        /// Działania po wciśnięciu klawisza Usuń (zakładka dysponent).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUsunDysponent_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć dysponenta: " + textBoxImieDysponent.Text + " " + textBoxNazwiskoDysponent.Text + "? ", "RemaGUM", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                goto no;
            }

            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
            nsAccess2DB.Maszyny_DysponentBUS maszyny_DysponentBUS = new nsAccess2DB.Maszyny_DysponentBUS(_connString);

            CzyscDaneDysponenta();
            
            dysponentBUS.delete((int)listBoxDysponent.Tag); // usunięcie danych dysponenta.
            maszyny_DysponentBUS.delete((int)listBoxDysponent.Tag); // usunięcie relacji z tabeli maszyny - dysponent

            WypelnijDysponentowDanymi();
            _statusForm = (int)_status.edycja;

            no:;
        }// buttonUsunDysponent_Click

        /// <summary>
        /// Działania po wciśnięciu klawisza Szukaj (zakładka dysponent).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSzukajDysponent_Click(object sender, EventArgs e)
        {
            textBoxWyszukiwanieDysponent.Text = textBoxWyszukiwanieDysponent.Text.Trim();

            if (textBoxWyszukiwanieDysponent.Text == string.Empty)
            {
                pokazKomunikat("Proszę wpisać tekst do pola wyszukiwania. Szukanie anulowane.");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;

            nsAccess2DB.DysponentBUS dysponentBUS = new nsAccess2DB.DysponentBUS(_connString);
           
            dysponentBUS.selectQuery("SELECT * FROM Dysponent ORDER BY Dysp_nazwisko ASC;");
            nsAccess2DB.DysponentVO dysponentVO;
           
            string s1 = textBoxWyszukiwanieDysponent.Text.ToUpper();
            string s2;

           for (int i = _dysponentSzukajIdx; i < dysponentBUS.count; i++)
            {
                dysponentBUS.idx = i;
                dysponentVO = dysponentBUS.VO;

                s2 = dysponentVO.Dysp_nazwisko.ToUpper() + " " + dysponentVO.Dysp_imie.ToUpper();
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
       
            Cursor.Current = Cursors.Default;
        }//buttonSzukajDysponent_Click

     
    }// public partial class SpisForm : Form

}//namespace RemaGUM
