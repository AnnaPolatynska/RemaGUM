﻿namespace RemaGUM
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
            this.toolStripButtonOdswiez = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelID = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelIDVal = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxSortowanie = new System.Windows.Forms.GroupBox();
            this.radioButtonNazwa = new System.Windows.Forms.RadioButton();
            this.radioButtonNr_pomieszczenia = new System.Windows.Forms.RadioButton();
            this.radioButtonNr_fabryczny = new System.Windows.Forms.RadioButton();
            this.radioButtonNr_inwentarzowy = new System.Windows.Forms.RadioButton();
            this.radioButtonTyp = new System.Windows.Forms.RadioButton();
            this.buttonSzukaj = new System.Windows.Forms.Button();
            this.textBoxWyszukiwanie = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Maszyny = new System.Windows.Forms.TabPage();
            this.Magazyn = new System.Windows.Forms.TabPage();
            this.radioButtonData_przegladu = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBoxSortowanie.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Maszyny.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.BackColor = System.Drawing.Color.Bisque;
            this.groupBox1.Controls.Add(this.listBoxMaszyny);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBox1.Location = new System.Drawing.Point(6, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 716);
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
            this.listBoxMaszyny.Size = new System.Drawing.Size(309, 680);
            this.listBoxMaszyny.TabIndex = 0;
            this.listBoxMaszyny.SelectedIndexChanged += new System.EventHandler(this.listBoxMaszyny_SelectedIndexChanged);
            // 
            // buttonAnuluj
            // 
            this.buttonAnuluj.BackColor = System.Drawing.Color.Linen;
            this.buttonAnuluj.Location = new System.Drawing.Point(221, 19);
            this.buttonAnuluj.Name = "buttonAnuluj";
            this.buttonAnuluj.Size = new System.Drawing.Size(75, 23);
            this.buttonAnuluj.TabIndex = 3;
            this.buttonAnuluj.Text = "anuluj";
            this.buttonAnuluj.UseVisualStyleBackColor = false;
            this.buttonAnuluj.Click += new System.EventHandler(this.buttonAnuluj_Click);
            // 
            // buttonNowa
            // 
            this.buttonNowa.BackColor = System.Drawing.Color.Linen;
            this.buttonNowa.Location = new System.Drawing.Point(9, 19);
            this.buttonNowa.Name = "buttonNowa";
            this.buttonNowa.Size = new System.Drawing.Size(75, 23);
            this.buttonNowa.TabIndex = 2;
            this.buttonNowa.Text = "nowa";
            this.buttonNowa.UseVisualStyleBackColor = false;
            this.buttonNowa.Click += new System.EventHandler(this.ButtonNowa_Click);
            // 
            // buttonZapisz
            // 
            this.buttonZapisz.BackColor = System.Drawing.Color.Linen;
            this.buttonZapisz.Location = new System.Drawing.Point(119, 19);
            this.buttonZapisz.Name = "buttonZapisz";
            this.buttonZapisz.Size = new System.Drawing.Size(75, 23);
            this.buttonZapisz.TabIndex = 3;
            this.buttonZapisz.Text = "zapisz";
            this.buttonZapisz.UseVisualStyleBackColor = false;
            this.buttonZapisz.Click += new System.EventHandler(this.buttonZapisz_Click);
            // 
            // buttonUsun
            // 
            this.buttonUsun.BackColor = System.Drawing.Color.Linen;
            this.buttonUsun.Location = new System.Drawing.Point(333, 19);
            this.buttonUsun.Name = "buttonUsun";
            this.buttonUsun.Size = new System.Drawing.Size(75, 23);
            this.buttonUsun.TabIndex = 4;
            this.buttonUsun.Text = "usuń";
            this.buttonUsun.UseVisualStyleBackColor = false;
            this.buttonUsun.Click += new System.EventHandler(this.buttonUsun_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Bisque;
            this.groupBox2.Controls.Add(this.buttonNowa);
            this.groupBox2.Controls.Add(this.buttonUsun);
            this.groupBox2.Controls.Add(this.buttonZapisz);
            this.groupBox2.Controls.Add(this.buttonAnuluj);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(758, 729);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(419, 56);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "edycja spisu";
            // 
            // textBoxNazwa
            // 
            this.textBoxNazwa.Location = new System.Drawing.Point(11, 67);
            this.textBoxNazwa.Name = "textBoxNazwa";
            this.textBoxNazwa.Size = new System.Drawing.Size(397, 20);
            this.textBoxNazwa.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Nazwa";
            // 
            // textBoxTyp
            // 
            this.textBoxTyp.Location = new System.Drawing.Point(11, 116);
            this.textBoxTyp.Name = "textBoxTyp";
            this.textBoxTyp.Size = new System.Drawing.Size(397, 20);
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
            this.label4.Location = new System.Drawing.Point(11, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nr inwentarzowy";
            // 
            // textBoxNr_inwentarzowy
            // 
            this.textBoxNr_inwentarzowy.Location = new System.Drawing.Point(11, 221);
            this.textBoxNr_inwentarzowy.Name = "textBoxNr_inwentarzowy";
            this.textBoxNr_inwentarzowy.Size = new System.Drawing.Size(397, 20);
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
            this.label6.Location = new System.Drawing.Point(12, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Nr fabryczny";
            // 
            // textBoxNr_fabryczny
            // 
            this.textBoxNr_fabryczny.Location = new System.Drawing.Point(11, 274);
            this.textBoxNr_fabryczny.Name = "textBoxNr_fabryczny";
            this.textBoxNr_fabryczny.Size = new System.Drawing.Size(397, 20);
            this.textBoxNr_fabryczny.TabIndex = 14;
            // 
            // textBoxRok_produkcji
            // 
            this.textBoxRok_produkcji.Location = new System.Drawing.Point(11, 170);
            this.textBoxRok_produkcji.Name = "textBoxRok_produkcji";
            this.textBoxRok_produkcji.Size = new System.Drawing.Size(86, 20);
            this.textBoxRok_produkcji.TabIndex = 15;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Bisque;
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
            this.panel1.Location = new System.Drawing.Point(333, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(419, 716);
            this.panel1.TabIndex = 17;
            // 
            // comboBoxOperator_maszyny
            // 
            this.comboBoxOperator_maszyny.FormattingEnabled = true;
            this.comboBoxOperator_maszyny.Location = new System.Drawing.Point(11, 433);
            this.comboBoxOperator_maszyny.Name = "comboBoxOperator_maszyny";
            this.comboBoxOperator_maszyny.Size = new System.Drawing.Size(397, 21);
            this.comboBoxOperator_maszyny.TabIndex = 41;
            this.comboBoxOperator_maszyny.SelectedIndexChanged += new System.EventHandler(this.comboBox_Operator_maszyny_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 417);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 13);
            this.label17.TabIndex = 40;
            this.label17.Text = "Operator maszyny";
            // 
            // comboBoxOsoba_zarzadzajaca
            // 
            this.comboBoxOsoba_zarzadzajaca.FormattingEnabled = true;
            this.comboBoxOsoba_zarzadzajaca.Location = new System.Drawing.Point(11, 377);
            this.comboBoxOsoba_zarzadzajaca.Name = "comboBoxOsoba_zarzadzajaca";
            this.comboBoxOsoba_zarzadzajaca.Size = new System.Drawing.Size(397, 21);
            this.comboBoxOsoba_zarzadzajaca.TabIndex = 39;
            this.comboBoxOsoba_zarzadzajaca.SelectedIndexChanged += new System.EventHandler(this.comboBox_Osoba_zarzadzajaca_SelectedIndexChanged);
            // 
            // textBoxNr_pom
            // 
            this.textBoxNr_pom.Location = new System.Drawing.Point(11, 491);
            this.textBoxNr_pom.Name = "textBoxNr_pom";
            this.textBoxNr_pom.Size = new System.Drawing.Size(397, 20);
            this.textBoxNr_pom.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(11, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Kategoria ";
            // 
            // dateTimePickerData_kol_przegl
            // 
            this.dateTimePickerData_kol_przegl.Location = new System.Drawing.Point(255, 170);
            this.dateTimePickerData_kol_przegl.Name = "dateTimePickerData_kol_przegl";
            this.dateTimePickerData_kol_przegl.Size = new System.Drawing.Size(153, 20);
            this.dateTimePickerData_kol_przegl.TabIndex = 37;
            // 
            // comboBoxDzial
            // 
            this.comboBoxDzial.FormattingEnabled = true;
            this.comboBoxDzial.Location = new System.Drawing.Point(11, 539);
            this.comboBoxDzial.Name = "comboBoxDzial";
            this.comboBoxDzial.Size = new System.Drawing.Size(397, 21);
            this.comboBoxDzial.TabIndex = 33;
            this.comboBoxDzial.SelectedIndexChanged += new System.EventHandler(this.comboBoxDzial_SelectedIndexChanged);
            // 
            // dateTimePickerData_ost_przegl
            // 
            this.dateTimePickerData_ost_przegl.Location = new System.Drawing.Point(103, 170);
            this.dateTimePickerData_ost_przegl.Name = "dateTimePickerData_ost_przegl";
            this.dateTimePickerData_ost_przegl.Size = new System.Drawing.Size(146, 20);
            this.dateTimePickerData_ost_przegl.TabIndex = 36;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(253, 154);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(138, 13);
            this.label13.TabIndex = 27;
            this.label13.Text = "Data nastepnego przeglądu";
            // 
            // comboBoxKategoria
            // 
            this.comboBoxKategoria.FormattingEnabled = true;
            this.comboBoxKategoria.Location = new System.Drawing.Point(11, 16);
            this.comboBoxKategoria.Name = "comboBoxKategoria";
            this.comboBoxKategoria.Size = new System.Drawing.Size(397, 21);
            this.comboBoxKategoria.TabIndex = 31;
            this.comboBoxKategoria.SelectedIndexChanged += new System.EventHandler(this.comboBoxKategoria_SelectedIndexChanged);
            // 
            // richTextBoxUwagi
            // 
            this.richTextBoxUwagi.Location = new System.Drawing.Point(11, 641);
            this.richTextBoxUwagi.Name = "richTextBoxUwagi";
            this.richTextBoxUwagi.Size = new System.Drawing.Size(397, 79);
            this.richTextBoxUwagi.TabIndex = 30;
            this.richTextBoxUwagi.Text = "";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(100, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(131, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "Data ostatniego przegladu";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(11, 625);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(37, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "Uwagi";
            // 
            // textBoxNr_prot_BHP
            // 
            this.textBoxNr_prot_BHP.Location = new System.Drawing.Point(11, 594);
            this.textBoxNr_prot_BHP.Name = "textBoxNr_prot_BHP";
            this.textBoxNr_prot_BHP.Size = new System.Drawing.Size(397, 20);
            this.textBoxNr_prot_BHP.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 578);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Nr wg protokołu BHP";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 523);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Dział";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 475);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Nr pomieszczenia";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 361);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Osoba zarządzająca";
            // 
            // textBoxProducent
            // 
            this.textBoxProducent.Location = new System.Drawing.Point(11, 324);
            this.textBoxProducent.Name = "textBoxProducent";
            this.textBoxProducent.Size = new System.Drawing.Size(397, 20);
            this.textBoxProducent.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 308);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Producent";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(758, 69);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(419, 490);
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 66);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "Częstotliwość wykorzystania";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(135, 13);
            this.label16.TabIndex = 31;
            this.label16.Text = "Ocena stanu technicznego";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 106);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 13);
            this.label18.TabIndex = 33;
            this.label18.Text = "Propozycja";
            // 
            // comboBoxStan_techniczny
            // 
            this.comboBoxStan_techniczny.FormattingEnabled = true;
            this.comboBoxStan_techniczny.Location = new System.Drawing.Point(9, 42);
            this.comboBoxStan_techniczny.Name = "comboBoxStan_techniczny";
            this.comboBoxStan_techniczny.Size = new System.Drawing.Size(399, 21);
            this.comboBoxStan_techniczny.TabIndex = 38;
            this.comboBoxStan_techniczny.SelectedIndexChanged += new System.EventHandler(this.comboBoxStan_techniczny_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Bisque;
            this.groupBox3.Controls.Add(this.comboBoxPropozycja);
            this.groupBox3.Controls.Add(this.comboBoxWykorzystanie);
            this.groupBox3.Controls.Add(this.comboBoxStan_techniczny);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Location = new System.Drawing.Point(758, 565);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(419, 158);
            this.groupBox3.TabIndex = 39;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Stan techniczny i częstotliwość wykorzystania";
            // 
            // comboBoxPropozycja
            // 
            this.comboBoxPropozycja.FormattingEnabled = true;
            this.comboBoxPropozycja.Location = new System.Drawing.Point(9, 122);
            this.comboBoxPropozycja.Name = "comboBoxPropozycja";
            this.comboBoxPropozycja.Size = new System.Drawing.Size(399, 21);
            this.comboBoxPropozycja.TabIndex = 41;
            this.comboBoxPropozycja.SelectedIndexChanged += new System.EventHandler(this.comboBoxPropozycja_SelectedIndexChanged);
            // 
            // comboBoxWykorzystanie
            // 
            this.comboBoxWykorzystanie.FormattingEnabled = true;
            this.comboBoxWykorzystanie.Location = new System.Drawing.Point(9, 82);
            this.comboBoxWykorzystanie.Name = "comboBoxWykorzystanie";
            this.comboBoxWykorzystanie.Size = new System.Drawing.Size(399, 21);
            this.comboBoxWykorzystanie.TabIndex = 40;
            this.comboBoxWykorzystanie.SelectedIndexChanged += new System.EventHandler(this.ComboBoxWykorzystanie_SelectedIndexChanged);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonHelp,
            this.toolStripButtonOdswiez});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(1243, 43);
            this.toolStrip.TabIndex = 41;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonHelp
            // 
            this.toolStripButtonHelp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonHelp.Image")));
            this.toolStripButtonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButtonHelp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonHelp.Name = "toolStripButtonHelp";
            this.toolStripButtonHelp.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripButtonHelp.RightToLeftAutoMirrorImage = true;
            this.toolStripButtonHelp.Size = new System.Drawing.Size(38, 34);
            this.toolStripButtonHelp.Click += new System.EventHandler(this.toolStripButtonHelp_Click);
            // 
            // toolStripButtonOdswiez
            // 
            this.toolStripButtonOdswiez.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOdswiez.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOdswiez.Image")));
            this.toolStripButtonOdswiez.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonOdswiez.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOdswiez.Name = "toolStripButtonOdswiez";
            this.toolStripButtonOdswiez.Size = new System.Drawing.Size(40, 40);
            this.toolStripButtonOdswiez.Text = "toolStripButtonOdswiez";
            this.toolStripButtonOdswiez.Click += new System.EventHandler(this.toolStripButtonOdswiez_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelID,
            this.toolStripStatusLabelIDVal});
            this.statusStrip1.Location = new System.Drawing.Point(0, 874);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1243, 22);
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
            this.groupBoxSortowanie.BackColor = System.Drawing.Color.Bisque;
            this.groupBoxSortowanie.Controls.Add(this.radioButtonData_przegladu);
            this.groupBoxSortowanie.Controls.Add(this.radioButtonNazwa);
            this.groupBoxSortowanie.Controls.Add(this.radioButtonNr_pomieszczenia);
            this.groupBoxSortowanie.Controls.Add(this.radioButtonNr_fabryczny);
            this.groupBoxSortowanie.Controls.Add(this.radioButtonNr_inwentarzowy);
            this.groupBoxSortowanie.Controls.Add(this.radioButtonTyp);
            this.groupBoxSortowanie.ForeColor = System.Drawing.Color.Red;
            this.groupBoxSortowanie.Location = new System.Drawing.Point(6, 6);
            this.groupBoxSortowanie.Name = "groupBoxSortowanie";
            this.groupBoxSortowanie.Size = new System.Drawing.Size(558, 57);
            this.groupBoxSortowanie.TabIndex = 43;
            this.groupBoxSortowanie.TabStop = false;
            this.groupBoxSortowanie.Text = "sortowanie";
            // 
            // radioButtonNazwa
            // 
            this.radioButtonNazwa.AutoSize = true;
            this.radioButtonNazwa.ForeColor = System.Drawing.SystemColors.Desktop;
            this.radioButtonNazwa.Location = new System.Drawing.Point(6, 24);
            this.radioButtonNazwa.Name = "radioButtonNazwa";
            this.radioButtonNazwa.Size = new System.Drawing.Size(58, 17);
            this.radioButtonNazwa.TabIndex = 4;
            this.radioButtonNazwa.TabStop = true;
            this.radioButtonNazwa.Text = "Nazwa";
            this.radioButtonNazwa.UseVisualStyleBackColor = true;
            this.radioButtonNazwa.CheckedChanged += new System.EventHandler(this.radioButtonNazwa_CheckedChanged);
            // 
            // radioButtonNr_pomieszczenia
            // 
            this.radioButtonNr_pomieszczenia.AutoSize = true;
            this.radioButtonNr_pomieszczenia.ForeColor = System.Drawing.SystemColors.Desktop;
            this.radioButtonNr_pomieszczenia.Location = new System.Drawing.Point(327, 24);
            this.radioButtonNr_pomieszczenia.Name = "radioButtonNr_pomieszczenia";
            this.radioButtonNr_pomieszczenia.Size = new System.Drawing.Size(108, 17);
            this.radioButtonNr_pomieszczenia.TabIndex = 3;
            this.radioButtonNr_pomieszczenia.TabStop = true;
            this.radioButtonNr_pomieszczenia.Text = "Nr pomieszczenia";
            this.radioButtonNr_pomieszczenia.UseVisualStyleBackColor = true;
            this.radioButtonNr_pomieszczenia.CheckedChanged += new System.EventHandler(this.radioButton_Nr_Pomieszczenia_CheckedChanged);
            // 
            // radioButtonNr_fabryczny
            // 
            this.radioButtonNr_fabryczny.AutoSize = true;
            this.radioButtonNr_fabryczny.ForeColor = System.Drawing.SystemColors.Desktop;
            this.radioButtonNr_fabryczny.Location = new System.Drawing.Point(119, 24);
            this.radioButtonNr_fabryczny.Name = "radioButtonNr_fabryczny";
            this.radioButtonNr_fabryczny.Size = new System.Drawing.Size(84, 17);
            this.radioButtonNr_fabryczny.TabIndex = 2;
            this.radioButtonNr_fabryczny.TabStop = true;
            this.radioButtonNr_fabryczny.Text = "Nr fabryczny";
            this.radioButtonNr_fabryczny.UseVisualStyleBackColor = true;
            this.radioButtonNr_fabryczny.CheckedChanged += new System.EventHandler(this.radioButtonNr_fabrycznyCheckedChanged);
            // 
            // radioButtonNr_inwentarzowy
            // 
            this.radioButtonNr_inwentarzowy.AutoSize = true;
            this.radioButtonNr_inwentarzowy.ForeColor = System.Drawing.SystemColors.Desktop;
            this.radioButtonNr_inwentarzowy.Location = new System.Drawing.Point(212, 24);
            this.radioButtonNr_inwentarzowy.Name = "radioButtonNr_inwentarzowy";
            this.radioButtonNr_inwentarzowy.Size = new System.Drawing.Size(103, 17);
            this.radioButtonNr_inwentarzowy.TabIndex = 1;
            this.radioButtonNr_inwentarzowy.TabStop = true;
            this.radioButtonNr_inwentarzowy.Text = "Nr inwentarzowy";
            this.radioButtonNr_inwentarzowy.UseVisualStyleBackColor = true;
            this.radioButtonNr_inwentarzowy.CheckedChanged += new System.EventHandler(this.radioButton_Nr_Inwentarzowy_CheckedChanged);
            // 
            // radioButtonTyp
            // 
            this.radioButtonTyp.AutoSize = true;
            this.radioButtonTyp.ForeColor = System.Drawing.SystemColors.Desktop;
            this.radioButtonTyp.Location = new System.Drawing.Point(70, 24);
            this.radioButtonTyp.Name = "radioButtonTyp";
            this.radioButtonTyp.Size = new System.Drawing.Size(43, 17);
            this.radioButtonTyp.TabIndex = 0;
            this.radioButtonTyp.TabStop = true;
            this.radioButtonTyp.Text = "Typ";
            this.radioButtonTyp.UseVisualStyleBackColor = true;
            this.radioButtonTyp.CheckedChanged += new System.EventHandler(this.radioButton_Typ_CheckedChanged);
            // 
            // buttonSzukaj
            // 
            this.buttonSzukaj.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSzukaj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSzukaj.BackgroundImage")));
            this.buttonSzukaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSzukaj.Location = new System.Drawing.Point(520, 16);
            this.buttonSzukaj.Name = "buttonSzukaj";
            this.buttonSzukaj.Size = new System.Drawing.Size(35, 34);
            this.buttonSzukaj.TabIndex = 4;
            this.buttonSzukaj.UseVisualStyleBackColor = false;
            this.buttonSzukaj.Click += new System.EventHandler(this.buttonSzukaj_Click);
            // 
            // textBoxWyszukiwanie
            // 
            this.textBoxWyszukiwanie.Location = new System.Drawing.Point(6, 24);
            this.textBoxWyszukiwanie.Name = "textBoxWyszukiwanie";
            this.textBoxWyszukiwanie.Size = new System.Drawing.Size(508, 20);
            this.textBoxWyszukiwanie.TabIndex = 44;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Bisque;
            this.groupBox5.Controls.Add(this.buttonSzukaj);
            this.groupBox5.Controls.Add(this.textBoxWyszukiwanie);
            this.groupBox5.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox5.ForeColor = System.Drawing.Color.Red;
            this.groupBox5.Location = new System.Drawing.Point(616, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(561, 57);
            this.groupBox5.TabIndex = 45;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "wyszukiwanie po nazwie";
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Maszyny);
            this.tabControl1.Controls.Add(this.Magazyn);
            this.tabControl1.Location = new System.Drawing.Point(5, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1234, 820);
            this.tabControl1.TabIndex = 47;
            // 
            // Maszyny
            // 
            this.Maszyny.BackColor = System.Drawing.Color.Bisque;
            this.Maszyny.Controls.Add(this.groupBox5);
            this.Maszyny.Controls.Add(this.pictureBox1);
            this.Maszyny.Controls.Add(this.groupBox1);
            this.Maszyny.Controls.Add(this.groupBox2);
            this.Maszyny.Controls.Add(this.panel1);
            this.Maszyny.Controls.Add(this.groupBox3);
            this.Maszyny.Controls.Add(this.groupBoxSortowanie);
            this.Maszyny.Location = new System.Drawing.Point(4, 22);
            this.Maszyny.Name = "Maszyny";
            this.Maszyny.Padding = new System.Windows.Forms.Padding(3);
            this.Maszyny.Size = new System.Drawing.Size(1226, 794);
            this.Maszyny.TabIndex = 0;
            this.Maszyny.Text = "Maszyny";
            // 
            // Magazyn
            // 
            this.Magazyn.Location = new System.Drawing.Point(4, 22);
            this.Magazyn.Name = "Magazyn";
            this.Magazyn.Padding = new System.Windows.Forms.Padding(3);
            this.Magazyn.Size = new System.Drawing.Size(1152, 766);
            this.Magazyn.TabIndex = 1;
            this.Magazyn.Text = "Magazyn";
            this.Magazyn.UseVisualStyleBackColor = true;
            // 
            // radioButtonData_przegladu
            // 
            this.radioButtonData_przegladu.AutoSize = true;
            this.radioButtonData_przegladu.ForeColor = System.Drawing.SystemColors.Desktop;
            this.radioButtonData_przegladu.Location = new System.Drawing.Point(450, 24);
            this.radioButtonData_przegladu.Name = "radioButtonData_przegladu";
            this.radioButtonData_przegladu.Size = new System.Drawing.Size(97, 17);
            this.radioButtonData_przegladu.TabIndex = 5;
            this.radioButtonData_przegladu.TabStop = true;
            this.radioButtonData_przegladu.Text = "Data przeglądu";
            this.radioButtonData_przegladu.UseVisualStyleBackColor = true;
            this.radioButtonData_przegladu.CheckedChanged += new System.EventHandler(this.radioButtonData_nast_przegladu_CheckedChanged);
            // 
            // SpisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 896);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip);
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
            this.tabControl1.ResumeLayout(false);
            this.Maszyny.ResumeLayout(false);
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
        private System.Windows.Forms.RadioButton radioButtonNr_pomieszczenia;
        private System.Windows.Forms.RadioButton radioButtonNr_fabryczny;
        private System.Windows.Forms.RadioButton radioButtonNr_inwentarzowy;
        private System.Windows.Forms.RadioButton radioButtonTyp;
        private System.Windows.Forms.Button buttonSzukaj;
        private System.Windows.Forms.TextBox textBoxWyszukiwanie;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButtonNazwa;
        private System.Windows.Forms.TextBox textBoxNr_pom;
        private System.Windows.Forms.ComboBox comboBoxOsoba_zarzadzajaca;
        private System.Windows.Forms.ComboBox comboBoxOperator_maszyny;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Maszyny;
        private System.Windows.Forms.TabPage Magazyn;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.ToolStripButton toolStripButtonOdswiez;
        private System.Windows.Forms.RadioButton radioButtonData_przegladu;
    }
}

