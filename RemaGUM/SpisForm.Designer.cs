namespace RemaGUM
{
    partial class SpisForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpisForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxMaszyny = new System.Windows.Forms.ListBox();
            this.buttonAnuluj = new System.Windows.Forms.Button();
            this.buttonNowa = new System.Windows.Forms.Button();
            this.buttonZapisz = new System.Windows.Forms.Button();
            this.buttonUsun = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxNazwa = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTyp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNr_inwentarzowy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxNr_fabryczny = new System.Windows.Forms.TextBox();
            this.textBoxRok_produkcji = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxOperator_maszyny = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.comboBoxOsoba_zarzadzajaca = new System.Windows.Forms.ComboBox();
            this.textBoxNr_pom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePickerData_kol_przegl = new System.Windows.Forms.DateTimePicker();
            this.comboBoxDzial = new System.Windows.Forms.ComboBox();
            this.dateTimePickerData_ost_przegl = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.comboBoxKategoria = new System.Windows.Forms.ComboBox();
            this.richTextBoxUwagi = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxNr_prot_BHP = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxProducent = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBoxStan_techniczny = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.comboBoxPropozycja = new System.Windows.Forms.ComboBox();
            this.comboBoxWykorzystanie = new System.Windows.Forms.ComboBox();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelID = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelIDVal = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxSortowanie = new System.Windows.Forms.GroupBox();
            this.radioButton_Nazwa = new System.Windows.Forms.RadioButton();
            this.radioButton_Nr_Pomieszczenia = new System.Windows.Forms.RadioButton();
            this.radioButton_Nr_Fabryczny = new System.Windows.Forms.RadioButton();
            this.radioButton_Nr_Inwentarzowy = new System.Windows.Forms.RadioButton();
            this.radioButton_Typ = new System.Windows.Forms.RadioButton();
            this.buttonSzukaj = new System.Windows.Forms.Button();
            this.textBoxWyszukiwanie = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonOdswiez = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxSortowanie.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.listBoxMaszyny);
            this.groupBox1.Location = new System.Drawing.Point(12, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 754);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista";
            // 
            // listBoxMaszyny
            // 
            this.listBoxMaszyny.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxMaszyny.FormattingEnabled = true;
            this.listBoxMaszyny.Location = new System.Drawing.Point(6, 19);
            this.listBoxMaszyny.Name = "listBoxMaszyny";
            this.listBoxMaszyny.Size = new System.Drawing.Size(293, 719);
            this.listBoxMaszyny.TabIndex = 0;
            this.listBoxMaszyny.SelectedIndexChanged += new System.EventHandler(this.listBoxMaszyny_SelectedIndexChanged);
            // 
            // buttonAnuluj
            // 
            this.buttonAnuluj.Location = new System.Drawing.Point(212, 19);
            this.buttonAnuluj.Name = "buttonAnuluj";
            this.buttonAnuluj.Size = new System.Drawing.Size(75, 23);
            this.buttonAnuluj.TabIndex = 3;
            this.buttonAnuluj.Text = "anuluj";
            this.buttonAnuluj.UseVisualStyleBackColor = true;
            this.buttonAnuluj.Click += new System.EventHandler(this.buttonAnuluj_Click);
            // 
            // buttonNowa
            // 
            this.buttonNowa.Location = new System.Drawing.Point(12, 19);
            this.buttonNowa.Name = "buttonNowa";
            this.buttonNowa.Size = new System.Drawing.Size(75, 23);
            this.buttonNowa.TabIndex = 2;
            this.buttonNowa.Text = "nowa";
            this.buttonNowa.UseVisualStyleBackColor = true;
            this.buttonNowa.Click += new System.EventHandler(this.ButtonNowa_Click);
            // 
            // buttonZapisz
            // 
            this.buttonZapisz.Location = new System.Drawing.Point(106, 19);
            this.buttonZapisz.Name = "buttonZapisz";
            this.buttonZapisz.Size = new System.Drawing.Size(75, 23);
            this.buttonZapisz.TabIndex = 3;
            this.buttonZapisz.Text = "zapisz";
            this.buttonZapisz.UseVisualStyleBackColor = true;
            this.buttonZapisz.Click += new System.EventHandler(this.buttonZapisz_Click);
            // 
            // buttonUsun
            // 
            this.buttonUsun.Location = new System.Drawing.Point(315, 19);
            this.buttonUsun.Name = "buttonUsun";
            this.buttonUsun.Size = new System.Drawing.Size(75, 23);
            this.buttonUsun.TabIndex = 4;
            this.buttonUsun.Text = "usuń";
            this.buttonUsun.UseVisualStyleBackColor = true;
            this.buttonUsun.Click += new System.EventHandler(this.buttonUsun_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.groupBox2.Controls.Add(this.buttonNowa);
            this.groupBox2.Controls.Add(this.buttonUsun);
            this.groupBox2.Controls.Add(this.buttonZapisz);
            this.groupBox2.Controls.Add(this.buttonAnuluj);
            this.groupBox2.Location = new System.Drawing.Point(324, 757);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 56);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "edycja spisu";
            // 
            // textBoxNazwa
            // 
            this.textBoxNazwa.Location = new System.Drawing.Point(14, 76);
            this.textBoxNazwa.Name = "textBoxNazwa";
            this.textBoxNazwa.Size = new System.Drawing.Size(380, 20);
            this.textBoxNazwa.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nazwa";
            // 
            // textBoxTyp
            // 
            this.textBoxTyp.Location = new System.Drawing.Point(14, 116);
            this.textBoxTyp.Name = "textBoxTyp";
            this.textBoxTyp.Size = new System.Drawing.Size(377, 20);
            this.textBoxTyp.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Typ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nr inwentarzowy";
            // 
            // textBoxNr_inwentarzowy
            // 
            this.textBoxNr_inwentarzowy.Location = new System.Drawing.Point(12, 209);
            this.textBoxNr_inwentarzowy.Name = "textBoxNr_inwentarzowy";
            this.textBoxNr_inwentarzowy.Size = new System.Drawing.Size(377, 20);
            this.textBoxNr_inwentarzowy.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Rok produkcji";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Nr fabryczny";
            // 
            // textBoxNr_fabryczny
            // 
            this.textBoxNr_fabryczny.Location = new System.Drawing.Point(12, 258);
            this.textBoxNr_fabryczny.Name = "textBoxNr_fabryczny";
            this.textBoxNr_fabryczny.Size = new System.Drawing.Size(377, 20);
            this.textBoxNr_fabryczny.TabIndex = 14;
            // 
            // textBoxRok_produkcji
            // 
            this.textBoxRok_produkcji.Location = new System.Drawing.Point(12, 170);
            this.textBoxRok_produkcji.Name = "textBoxRok_produkcji";
            this.textBoxRok_produkcji.Size = new System.Drawing.Size(75, 20);
            this.textBoxRok_produkcji.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.Controls.Add(this.comboBoxOperator_maszyny);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.comboBoxOsoba_zarzadzajaca);
            this.panel1.Controls.Add(this.textBoxNr_pom);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dateTimePickerData_kol_przegl);
            this.panel1.Controls.Add(this.comboBoxDzial);
            this.panel1.Controls.Add(this.dateTimePickerData_ost_przegl);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.comboBoxKategoria);
            this.panel1.Controls.Add(this.richTextBoxUwagi);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.textBoxNr_prot_BHP);
            this.panel1.Controls.Add(this.textBoxRok_produkcji);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBoxProducent);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBoxNr_fabryczny);
            this.panel1.Controls.Add(this.textBoxNazwa);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxNr_inwentarzowy);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBoxTyp);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(324, 67);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 684);
            this.panel1.TabIndex = 17;
            // 
            // comboBoxOperator_maszyny
            // 
            this.comboBoxOperator_maszyny.FormattingEnabled = true;
            this.comboBoxOperator_maszyny.Location = new System.Drawing.Point(11, 409);
            this.comboBoxOperator_maszyny.Name = "comboBoxOperator_maszyny";
            this.comboBoxOperator_maszyny.Size = new System.Drawing.Size(373, 21);
            this.comboBoxOperator_maszyny.TabIndex = 41;
            this.comboBoxOperator_maszyny.SelectedIndexChanged += new System.EventHandler(this.comboBox_Operator_maszyny_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 393);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 13);
            this.label17.TabIndex = 40;
            this.label17.Text = "Operator maszyny";
            // 
            // comboBoxOsoba_zarzadzajaca
            // 
            this.comboBoxOsoba_zarzadzajaca.FormattingEnabled = true;
            this.comboBoxOsoba_zarzadzajaca.Location = new System.Drawing.Point(12, 353);
            this.comboBoxOsoba_zarzadzajaca.Name = "comboBoxOsoba_zarzadzajaca";
            this.comboBoxOsoba_zarzadzajaca.Size = new System.Drawing.Size(373, 21);
            this.comboBoxOsoba_zarzadzajaca.TabIndex = 39;
            this.comboBoxOsoba_zarzadzajaca.SelectedIndexChanged += new System.EventHandler(this.comboBox_Osoba_zarzadzajaca_SelectedIndexChanged);
            // 
            // textBoxNr_pom
            // 
            this.textBoxNr_pom.Location = new System.Drawing.Point(11, 449);
            this.textBoxNr_pom.Name = "textBoxNr_pom";
            this.textBoxNr_pom.Size = new System.Drawing.Size(377, 20);
            this.textBoxNr_pom.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Kategoria ";
            // 
            // dateTimePickerData_kol_przegl
            // 
            this.dateTimePickerData_kol_przegl.Location = new System.Drawing.Point(245, 170);
            this.dateTimePickerData_kol_przegl.Name = "dateTimePickerData_kol_przegl";
            this.dateTimePickerData_kol_przegl.Size = new System.Drawing.Size(153, 20);
            this.dateTimePickerData_kol_przegl.TabIndex = 37;
            // 
            // comboBoxDzial
            // 
            this.comboBoxDzial.FormattingEnabled = true;
            this.comboBoxDzial.Location = new System.Drawing.Point(14, 502);
            this.comboBoxDzial.Name = "comboBoxDzial";
            this.comboBoxDzial.Size = new System.Drawing.Size(373, 21);
            this.comboBoxDzial.TabIndex = 33;
            this.comboBoxDzial.SelectedIndexChanged += new System.EventHandler(this.comboBoxDzial_SelectedIndexChanged);
            // 
            // dateTimePickerData_ost_przegl
            // 
            this.dateTimePickerData_ost_przegl.Location = new System.Drawing.Point(93, 170);
            this.dateTimePickerData_ost_przegl.Name = "dateTimePickerData_ost_przegl";
            this.dateTimePickerData_ost_przegl.Size = new System.Drawing.Size(146, 20);
            this.dateTimePickerData_ost_przegl.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(239, 154);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(138, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Data nastepnego przeglądu";
            // 
            // comboBoxKategoria
            // 
            this.comboBoxKategoria.FormattingEnabled = true;
            this.comboBoxKategoria.Location = new System.Drawing.Point(17, 33);
            this.comboBoxKategoria.Name = "comboBoxKategoria";
            this.comboBoxKategoria.Size = new System.Drawing.Size(373, 21);
            this.comboBoxKategoria.TabIndex = 31;
            this.comboBoxKategoria.SelectedIndexChanged += new System.EventHandler(this.comboBoxKategoria_SelectedIndexChanged);
            // 
            // richTextBoxUwagi
            // 
            this.richTextBoxUwagi.Location = new System.Drawing.Point(14, 607);
            this.richTextBoxUwagi.Name = "richTextBoxUwagi";
            this.richTextBoxUwagi.Size = new System.Drawing.Size(373, 60);
            this.richTextBoxUwagi.TabIndex = 30;
            this.richTextBoxUwagi.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(90, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Data ostatniego przegladu";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 591);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Uwagi";
            // 
            // textBoxNr_prot_BHP
            // 
            this.textBoxNr_prot_BHP.Location = new System.Drawing.Point(14, 556);
            this.textBoxNr_prot_BHP.Name = "textBoxNr_prot_BHP";
            this.textBoxNr_prot_BHP.Size = new System.Drawing.Size(377, 20);
            this.textBoxNr_prot_BHP.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 540);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Nr wg protokołu BHP";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 482);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Dział";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 433);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Nr pomieszczenia";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 337);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Osoba zarządzająca";
            // 
            // textBoxProducent
            // 
            this.textBoxProducent.Location = new System.Drawing.Point(12, 304);
            this.textBoxProducent.Name = "textBoxProducent";
            this.textBoxProducent.Size = new System.Drawing.Size(377, 20);
            this.textBoxProducent.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 288);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Producent";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(734, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(389, 464);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "Częstotliwość wykorzystania";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(135, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "Ocena stanu technicznego";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 138);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 13);
            this.label18.TabIndex = 33;
            this.label18.Text = "Propozycja";
            // 
            // comboBoxStan_techniczny
            // 
            this.comboBoxStan_techniczny.FormattingEnabled = true;
            this.comboBoxStan_techniczny.Location = new System.Drawing.Point(16, 33);
            this.comboBoxStan_techniczny.Name = "comboBoxStan_techniczny";
            this.comboBoxStan_techniczny.Size = new System.Drawing.Size(359, 21);
            this.comboBoxStan_techniczny.TabIndex = 38;
            this.comboBoxStan_techniczny.SelectedIndexChanged += new System.EventHandler(this.comboBoxStan_techniczny_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox3.Controls.Add(this.comboBoxPropozycja);
            this.groupBox3.Controls.Add(this.comboBoxWykorzystanie);
            this.groupBox3.Controls.Add(this.comboBoxStan_techniczny);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Location = new System.Drawing.Point(734, 583);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(389, 230);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Stan techniczny i częstotliwość wykorzystania";
            // 
            // comboBoxPropozycja
            // 
            this.comboBoxPropozycja.FormattingEnabled = true;
            this.comboBoxPropozycja.Location = new System.Drawing.Point(16, 154);
            this.comboBoxPropozycja.Name = "comboBoxPropozycja";
            this.comboBoxPropozycja.Size = new System.Drawing.Size(359, 21);
            this.comboBoxPropozycja.TabIndex = 41;
            this.comboBoxPropozycja.SelectedIndexChanged += new System.EventHandler(this.comboBoxPropozycja_SelectedIndexChanged);
            // 
            // comboBoxWykorzystanie
            // 
            this.comboBoxWykorzystanie.FormattingEnabled = true;
            this.comboBoxWykorzystanie.Location = new System.Drawing.Point(16, 90);
            this.comboBoxWykorzystanie.Name = "comboBoxWykorzystanie";
            this.comboBoxWykorzystanie.Size = new System.Drawing.Size(359, 21);
            this.comboBoxWykorzystanie.TabIndex = 40;
            this.comboBoxWykorzystanie.SelectedIndexChanged += new System.EventHandler(this.ComboBoxWykorzystanie_SelectedIndexChanged);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonHelp});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1135, 25);
            this.toolStrip.TabIndex = 41;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonHelp
            // 
            this.toolStripButtonHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHelp.Image")));
            this.toolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.Size = new System.Drawing.Size(121, 22);
            this.toolStripButtonHelp.Text = "Pomoc programu";
            this.toolStripButtonHelp.Click += new System.EventHandler(this.toolStripButtonHelp_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelID,
            this.toolStripStatusLabelIDVal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 816);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1135, 22);
            this.statusStrip1.TabIndex = 42;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelID
            // 
            this.toolStripStatusLabelID.Name = "toolStripStatusLabelID";
            this.toolStripStatusLabelID.Size = new System.Drawing.Size(21, 17);
            this.toolStripStatusLabelID.Text = "ID:";
            // 
            // toolStripStatusLabelIDVal
            // 
            this.toolStripStatusLabelIDVal.Name = "toolStripStatusLabelIDVal";
            this.toolStripStatusLabelIDVal.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabelIDVal.Text = "id maszyny";
            // 
            // groupBoxSortowanie
            // 
            this.groupBoxSortowanie.Controls.Add(this.radioButton_Nazwa);
            this.groupBoxSortowanie.Controls.Add(this.radioButton_Nr_Pomieszczenia);
            this.groupBoxSortowanie.Controls.Add(this.radioButton_Nr_Fabryczny);
            this.groupBoxSortowanie.Controls.Add(this.radioButton_Nr_Inwentarzowy);
            this.groupBoxSortowanie.Controls.Add(this.radioButton_Typ);
            this.groupBoxSortowanie.Location = new System.Drawing.Point(12, 20);
            this.groupBoxSortowanie.Name = "groupBoxSortowanie";
            this.groupBoxSortowanie.Size = new System.Drawing.Size(504, 41);
            this.groupBoxSortowanie.TabIndex = 43;
            this.groupBoxSortowanie.TabStop = false;
            this.groupBoxSortowanie.Text = "sortowanie";
            // 
            // radioButton_Nazwa
            // 
            this.radioButton_Nazwa.AutoSize = true;
            this.radioButton_Nazwa.Location = new System.Drawing.Point(435, 16);
            this.radioButton_Nazwa.Name = "radioButton_Nazwa";
            this.radioButton_Nazwa.Size = new System.Drawing.Size(58, 17);
            this.radioButton_Nazwa.TabIndex = 4;
            this.radioButton_Nazwa.TabStop = true;
            this.radioButton_Nazwa.Text = "Nazwa";
            this.radioButton_Nazwa.UseVisualStyleBackColor = true;
            this.radioButton_Nazwa.CheckedChanged += new System.EventHandler(this.radioButton_Nazwa_CheckedChanged);
            // 
            // radioButton_Nr_Pomieszczenia
            // 
            this.radioButton_Nr_Pomieszczenia.AutoSize = true;
            this.radioButton_Nr_Pomieszczenia.Location = new System.Drawing.Point(309, 16);
            this.radioButton_Nr_Pomieszczenia.Name = "radioButton_Nr_Pomieszczenia";
            this.radioButton_Nr_Pomieszczenia.Size = new System.Drawing.Size(108, 17);
            this.radioButton_Nr_Pomieszczenia.TabIndex = 3;
            this.radioButton_Nr_Pomieszczenia.TabStop = true;
            this.radioButton_Nr_Pomieszczenia.Text = "Nr pomieszczenia";
            this.radioButton_Nr_Pomieszczenia.UseVisualStyleBackColor = true;
            this.radioButton_Nr_Pomieszczenia.CheckedChanged += new System.EventHandler(this.radioButton_Nr_Pomieszczenia_CheckedChanged);
            // 
            // radioButton_Nr_Fabryczny
            // 
            this.radioButton_Nr_Fabryczny.AutoSize = true;
            this.radioButton_Nr_Fabryczny.Location = new System.Drawing.Point(202, 16);
            this.radioButton_Nr_Fabryczny.Name = "radioButton_Nr_Fabryczny";
            this.radioButton_Nr_Fabryczny.Size = new System.Drawing.Size(84, 17);
            this.radioButton_Nr_Fabryczny.TabIndex = 2;
            this.radioButton_Nr_Fabryczny.TabStop = true;
            this.radioButton_Nr_Fabryczny.Text = "Nr fabryczny";
            this.radioButton_Nr_Fabryczny.UseVisualStyleBackColor = true;
            this.radioButton_Nr_Fabryczny.CheckedChanged += new System.EventHandler(this.radioButton_Nr_Fabryczny_CheckedChanged);
            // 
            // radioButton_Nr_Inwentarzowy
            // 
            this.radioButton_Nr_Inwentarzowy.AutoSize = true;
            this.radioButton_Nr_Inwentarzowy.Location = new System.Drawing.Point(80, 16);
            this.radioButton_Nr_Inwentarzowy.Name = "radioButton_Nr_Inwentarzowy";
            this.radioButton_Nr_Inwentarzowy.Size = new System.Drawing.Size(103, 17);
            this.radioButton_Nr_Inwentarzowy.TabIndex = 1;
            this.radioButton_Nr_Inwentarzowy.TabStop = true;
            this.radioButton_Nr_Inwentarzowy.Text = "Nr inwentarzowy";
            this.radioButton_Nr_Inwentarzowy.UseVisualStyleBackColor = true;
            this.radioButton_Nr_Inwentarzowy.CheckedChanged += new System.EventHandler(this.radioButton_Nr_Inwentarzowy_CheckedChanged);
            // 
            // radioButton_Typ
            // 
            this.radioButton_Typ.AutoSize = true;
            this.radioButton_Typ.Location = new System.Drawing.Point(6, 16);
            this.radioButton_Typ.Name = "radioButton_Typ";
            this.radioButton_Typ.Size = new System.Drawing.Size(43, 17);
            this.radioButton_Typ.TabIndex = 0;
            this.radioButton_Typ.TabStop = true;
            this.radioButton_Typ.Text = "Typ";
            this.radioButton_Typ.UseVisualStyleBackColor = true;
            this.radioButton_Typ.CheckedChanged += new System.EventHandler(this.radioButton_Typ_CheckedChanged);
            // 
            // buttonSzukaj
            // 
            this.buttonSzukaj.Location = new System.Drawing.Point(519, 13);
            this.buttonSzukaj.Name = "buttonSzukaj";
            this.buttonSzukaj.Size = new System.Drawing.Size(75, 23);
            this.buttonSzukaj.TabIndex = 4;
            this.buttonSzukaj.Text = "szukaj";
            this.buttonSzukaj.UseVisualStyleBackColor = true;
            this.buttonSzukaj.Click += new System.EventHandler(this.buttonSzukaj_Click);
            // 
            // textBoxWyszukiwanie
            // 
            this.textBoxWyszukiwanie.Location = new System.Drawing.Point(6, 16);
            this.textBoxWyszukiwanie.Name = "textBoxWyszukiwanie";
            this.textBoxWyszukiwanie.Size = new System.Drawing.Size(507, 20);
            this.textBoxWyszukiwanie.TabIndex = 44;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.buttonSzukaj);
            this.groupBox5.Controls.Add(this.textBoxWyszukiwanie);
            this.groupBox5.Location = new System.Drawing.Point(522, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(601, 41);
            this.groupBox5.TabIndex = 45;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "wyszukiwanie po nazwie maszyny";
            // 
            // buttonOdswiez
            // 
            this.buttonOdswiez.Location = new System.Drawing.Point(1041, 4);
            this.buttonOdswiez.Name = "buttonOdswiez";
            this.buttonOdswiez.Size = new System.Drawing.Size(75, 23);
            this.buttonOdswiez.TabIndex = 46;
            this.buttonOdswiez.Text = "Odśwież";
            this.buttonOdswiez.UseVisualStyleBackColor = true;
            this.buttonOdswiez.Click += new System.EventHandler(this.buttonOdswiez_Click);
            // 
            // SpisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 838);
            this.Controls.Add(this.buttonOdswiez);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBoxSortowanie);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SpisForm";
            this.Text = "RemaGUM - spis maszyn";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxSortowanie.ResumeLayout(false);
            this.groupBoxSortowanie.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAnuluj;
        private System.Windows.Forms.ListBox listBoxMaszyny;
        private System.Windows.Forms.Button buttonNowa;
        private System.Windows.Forms.Button buttonZapisz;
        private System.Windows.Forms.Button buttonUsun;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxNazwa;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTyp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNr_inwentarzowy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxNr_fabryczny;
        private System.Windows.Forms.TextBox textBoxRok_produkcji;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxNr_prot_BHP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxProducent;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTextBoxUwagi;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DateTimePicker dateTimePickerData_ost_przegl;
        private System.Windows.Forms.DateTimePicker dateTimePickerData_kol_przegl;
        private System.Windows.Forms.ComboBox comboBoxDzial;
        private System.Windows.Forms.ComboBox comboBoxKategoria;
        private System.Windows.Forms.ComboBox comboBoxStan_techniczny;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxPropozycja;
        private System.Windows.Forms.ComboBox comboBoxWykorzystanie;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelID;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelIDVal;
        private System.Windows.Forms.GroupBox groupBoxSortowanie;
        private System.Windows.Forms.RadioButton radioButton_Nr_Pomieszczenia;
        private System.Windows.Forms.RadioButton radioButton_Nr_Fabryczny;
        private System.Windows.Forms.RadioButton radioButton_Nr_Inwentarzowy;
        private System.Windows.Forms.RadioButton radioButton_Typ;
        private System.Windows.Forms.Button buttonSzukaj;
        private System.Windows.Forms.TextBox textBoxWyszukiwanie;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton_Nazwa;
        private System.Windows.Forms.Button buttonOdswiez;
        private System.Windows.Forms.TextBox textBoxNr_pom;
        private System.Windows.Forms.ComboBox comboBoxOsoba_zarzadzajaca;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.ComboBox comboBoxOperator_maszyny;
        private System.Windows.Forms.Label label17;
    }
}

