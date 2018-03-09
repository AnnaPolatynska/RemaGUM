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
        private byte _statusForm;           //wartość statusu formularza

        private nsAccess2DB.MaszynyBUS _MaszynyBUS;

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
            comboBoxWykorzystanie.TabIndex = 16;
            comboBoxStan_techniczny.TabIndex = 17;
            comboBoxPriorytet.TabIndex = 18;
            comboBoxPropozycja.TabIndex = 19;
            textBoxPunktacja.TabIndex = 20;

            buttonNowa.TabIndex = 21;
            buttonZapisz.TabIndex = 22;
            buttonAnuluj.TabIndex = 23;
            buttonUsun.TabIndex = 24;

            _MaszynyBUS = new nsAccess2DB.MaszynyBUS(_connStr);
            _MaszynyBUS.select();

            wypelnijMaszyny();
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


        private void ButtonNowa_Click(object sender, EventArgs e)
        {

        }//ButtonNowa_Click

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

        }//buttonZapisz_Click

        private void buttonAnuluj_Click(object sender, EventArgs e)
        {

        }//buttonAnuluj_Click


        private void buttonUsun_Click(object sender, EventArgs e)
        {

        }//buttonUsun_Click


    }// public partial class SpisForm : Form
       
}//namespace RemaGUM
