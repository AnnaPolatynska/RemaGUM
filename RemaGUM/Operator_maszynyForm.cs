using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemaGUM
{
    public partial class Operator_maszynyForm : Form
    {
        private string _connStr = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = D:\\Projects\\RemaGUM\\RemaGUM.mdb"; //połaczenie z bazą danych

        private nsAccess2DB.Operator_maszynyBUS _Operator_maszynyBUS;

        private ToolTip _tt; //podpowiedzi dla niektórych kontolek

        /// <summary>
        /// Konstruktor formularza Operator maszyny.
        /// </summary>
        /// <param name="connStr">Połaczenie z bazą talela Operator_maszyny.</param>
        public Operator_maszynyForm()
        {
            InitializeComponent();
            nsRest.Rest rest = new nsRest.Rest();
            _connStr += rest.dbConnection(_connStr);
            
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
            
            _Operator_maszynyBUS = new nsAccess2DB.Operator_maszynyBUS(_connStr);

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

        }//Operator_maszyny()


        /// /////////////////////////////////////////////// /// ///            toolStripButton
        private void toolStripButtonOs_zarzadzajaca_Click(object sender, EventArgs e)
        {
            Os_zarzadzajacaForm frame = new Os_zarzadzajacaForm();
            frame.Show();
        }//toolStripButtonOs_zarzadzajaca_Click

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SpisForm frame = new SpisForm();
            frame.Show();
        }//toolStripButton1_Clic

        // ////////////////////////////////////////////////////////            Button
        private void buttonSzukaj_Click(object sender, EventArgs e)
        {

        }//buttonSzukaj_Click
    }
}
