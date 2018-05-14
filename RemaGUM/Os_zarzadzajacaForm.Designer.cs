namespace RemaGUM
{
    partial class Os_zarzadzajacaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Os_zarzadzajacaForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSpisForm = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOs_zarzadzajaca = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOperator = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonHelp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOdswiez = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listBoxOs_zarzadzajaca_maszyny = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNazwa_Dzial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxOperator_maszyny = new System.Windows.Forms.TextBox();
            this.groupBoxSortowanie = new System.Windows.Forms.GroupBox();
            this.radioButtonData_ost_przegl = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonSzukaj = new System.Windows.Forms.Button();
            this.textBoxWyszukiwanie = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonNowa = new System.Windows.Forms.Button();
            this.buttonUsun = new System.Windows.Forms.Button();
            this.buttonZapisz = new System.Windows.Forms.Button();
            this.buttonAnuluj = new System.Windows.Forms.Button();
            this.groupBoxOs_zarzadzajaca = new System.Windows.Forms.GroupBox();
            this.listBoxOs_zarzadzajaca = new System.Windows.Forms.ListBox();
            this.toolStrip.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxSortowanie.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxOs_zarzadzajaca.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSpisForm,
            this.toolStripButtonOs_zarzadzajaca,
            this.toolStripButtonOperator,
            this.toolStripSeparator1,
            this.toolStripButtonHelp,
            this.toolStripButtonOdswiez});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size(734, 43);
            this.toolStrip.TabIndex = 50;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonSpisForm
            // 
            this.toolStripButtonSpisForm.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSpisForm.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSpisForm.Image")));
            this.toolStripButtonSpisForm.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSpisForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSpisForm.Name = "toolStripButtonSpisForm";
            this.toolStripButtonSpisForm.RightToLeftAutoMirrorImage = true;
            this.toolStripButtonSpisForm.Size = new System.Drawing.Size(39, 40);
            this.toolStripButtonSpisForm.Text = "HOME";
            this.toolStripButtonSpisForm.ToolTipText = "Powrót do strony startowej";
            this.toolStripButtonSpisForm.Click += new System.EventHandler(this.toolStripButtonSpisForm_Click);
            // 
            // toolStripButtonOs_zarzadzajaca
            // 
            this.toolStripButtonOs_zarzadzajaca.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOs_zarzadzajaca.Enabled = false;
            this.toolStripButtonOs_zarzadzajaca.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOs_zarzadzajaca.Image")));
            this.toolStripButtonOs_zarzadzajaca.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonOs_zarzadzajaca.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOs_zarzadzajaca.Name = "toolStripButtonOs_zarzadzajaca";
            this.toolStripButtonOs_zarzadzajaca.Size = new System.Drawing.Size(37, 40);
            this.toolStripButtonOs_zarzadzajaca.Text = "toolStripButton1";
            this.toolStripButtonOs_zarzadzajaca.ToolTipText = "Osoby zarządzające";
            // 
            // toolStripButtonOperator
            // 
            this.toolStripButtonOperator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOperator.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOperator.Image")));
            this.toolStripButtonOperator.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonOperator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOperator.Name = "toolStripButtonOperator";
            this.toolStripButtonOperator.Size = new System.Drawing.Size(36, 40);
            this.toolStripButtonOperator.Text = "toolStripButton2";
            this.toolStripButtonOperator.ToolTipText = "Operatorzy maszyn.";
            this.toolStripButtonOperator.Click += new System.EventHandler(this.toolStripButtonOperator_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.AutoSize = false;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 36);
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
            this.toolStripButtonHelp.ToolTipText = "Pomoc programu.";
            // 
            // toolStripButtonOdswiez
            // 
            this.toolStripButtonOdswiez.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOdswiez.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOdswiez.Image")));
            this.toolStripButtonOdswiez.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonOdswiez.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOdswiez.Name = "toolStripButtonOdswiez";
            this.toolStripButtonOdswiez.Size = new System.Drawing.Size(40, 40);
            this.toolStripButtonOdswiez.ToolTipText = "Odświeżenie danych.";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 522);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(734, 22);
            this.statusStrip1.TabIndex = 56;
            this.statusStrip1.Text = "ID operatora";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Bisque;
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.listBoxOs_zarzadzajaca_maszyny);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBoxNazwa_Dzial);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.textBoxOperator_maszyny);
            this.groupBox3.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox3.ForeColor = System.Drawing.Color.Red;
            this.groupBox3.Location = new System.Drawing.Point(339, 109);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(391, 342);
            this.groupBox3.TabIndex = 55;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dane osoby zarządzającej";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label4.Location = new System.Drawing.Point(6, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 13);
            this.label4.TabIndex = 51;
            this.label4.Text = "Osoba sprawuje nadzór nad maszynami:";
            // 
            // listBoxOs_zarzadzajaca_maszyny
            // 
            this.listBoxOs_zarzadzajaca_maszyny.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxOs_zarzadzajaca_maszyny.BackColor = System.Drawing.Color.Linen;
            this.listBoxOs_zarzadzajaca_maszyny.FormattingEnabled = true;
            this.listBoxOs_zarzadzajaca_maszyny.Location = new System.Drawing.Point(6, 140);
            this.listBoxOs_zarzadzajaca_maszyny.Name = "listBoxOs_zarzadzajaca_maszyny";
            this.listBoxOs_zarzadzajaca_maszyny.Size = new System.Drawing.Size(374, 186);
            this.listBoxOs_zarzadzajaca_maszyny.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "Dział";
            // 
            // textBoxNazwa_Dzial
            // 
            this.textBoxNazwa_Dzial.BackColor = System.Drawing.Color.Linen;
            this.textBoxNazwa_Dzial.Location = new System.Drawing.Point(6, 84);
            this.textBoxNazwa_Dzial.Name = "textBoxNazwa_Dzial";
            this.textBoxNazwa_Dzial.Size = new System.Drawing.Size(374, 20);
            this.textBoxNazwa_Dzial.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Imię i Nazwisko";
            // 
            // textBoxOperator_maszyny
            // 
            this.textBoxOperator_maszyny.BackColor = System.Drawing.Color.Linen;
            this.textBoxOperator_maszyny.Location = new System.Drawing.Point(6, 35);
            this.textBoxOperator_maszyny.Name = "textBoxOperator_maszyny";
            this.textBoxOperator_maszyny.Size = new System.Drawing.Size(374, 20);
            this.textBoxOperator_maszyny.TabIndex = 44;
            // 
            // groupBoxSortowanie
            // 
            this.groupBoxSortowanie.BackColor = System.Drawing.Color.Bisque;
            this.groupBoxSortowanie.Controls.Add(this.radioButtonData_ost_przegl);
            this.groupBoxSortowanie.ForeColor = System.Drawing.Color.Red;
            this.groupBoxSortowanie.Location = new System.Drawing.Point(12, 46);
            this.groupBoxSortowanie.Name = "groupBoxSortowanie";
            this.groupBoxSortowanie.Size = new System.Drawing.Size(321, 57);
            this.groupBoxSortowanie.TabIndex = 54;
            this.groupBoxSortowanie.TabStop = false;
            this.groupBoxSortowanie.Text = "sortowanie";
            // 
            // radioButtonData_ost_przegl
            // 
            this.radioButtonData_ost_przegl.AutoSize = true;
            this.radioButtonData_ost_przegl.ForeColor = System.Drawing.SystemColors.Desktop;
            this.radioButtonData_ost_przegl.Location = new System.Drawing.Point(6, 24);
            this.radioButtonData_ost_przegl.Name = "radioButtonData_ost_przegl";
            this.radioButtonData_ost_przegl.Size = new System.Drawing.Size(133, 17);
            this.radioButtonData_ost_przegl.TabIndex = 6;
            this.radioButtonData_ost_przegl.TabStop = true;
            this.radioButtonData_ost_przegl.Text = "Data końca uprawnień";
            this.radioButtonData_ost_przegl.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Bisque;
            this.groupBox5.Controls.Add(this.buttonSzukaj);
            this.groupBox5.Controls.Add(this.textBoxWyszukiwanie);
            this.groupBox5.Cursor = System.Windows.Forms.Cursors.Default;
            this.groupBox5.ForeColor = System.Drawing.Color.Red;
            this.groupBox5.Location = new System.Drawing.Point(339, 46);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(391, 57);
            this.groupBox5.TabIndex = 53;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "wyszukiwanie po nazwie";
            // 
            // buttonSzukaj
            // 
            this.buttonSzukaj.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonSzukaj.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSzukaj.BackgroundImage")));
            this.buttonSzukaj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSzukaj.Location = new System.Drawing.Point(350, 15);
            this.buttonSzukaj.Name = "buttonSzukaj";
            this.buttonSzukaj.Size = new System.Drawing.Size(35, 34);
            this.buttonSzukaj.TabIndex = 4;
            this.buttonSzukaj.UseVisualStyleBackColor = false;
            // 
            // textBoxWyszukiwanie
            // 
            this.textBoxWyszukiwanie.BackColor = System.Drawing.Color.Linen;
            this.textBoxWyszukiwanie.Location = new System.Drawing.Point(6, 23);
            this.textBoxWyszukiwanie.Name = "textBoxWyszukiwanie";
            this.textBoxWyszukiwanie.Size = new System.Drawing.Size(327, 20);
            this.textBoxWyszukiwanie.TabIndex = 44;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.BackColor = System.Drawing.Color.Bisque;
            this.groupBox2.Controls.Add(this.buttonNowa);
            this.groupBox2.Controls.Add(this.buttonUsun);
            this.groupBox2.Controls.Add(this.buttonZapisz);
            this.groupBox2.Controls.Add(this.buttonAnuluj);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.ForeColor = System.Drawing.Color.Red;
            this.groupBox2.Location = new System.Drawing.Point(339, 463);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(333, 56);
            this.groupBox2.TabIndex = 52;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "edycja spisu osób zarządzających";
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
            // 
            // buttonUsun
            // 
            this.buttonUsun.BackColor = System.Drawing.Color.Linen;
            this.buttonUsun.Location = new System.Drawing.Point(252, 19);
            this.buttonUsun.Name = "buttonUsun";
            this.buttonUsun.Size = new System.Drawing.Size(75, 23);
            this.buttonUsun.TabIndex = 4;
            this.buttonUsun.Text = "usuń";
            this.buttonUsun.UseVisualStyleBackColor = false;
            // 
            // buttonZapisz
            // 
            this.buttonZapisz.BackColor = System.Drawing.Color.Linen;
            this.buttonZapisz.Location = new System.Drawing.Point(90, 19);
            this.buttonZapisz.Name = "buttonZapisz";
            this.buttonZapisz.Size = new System.Drawing.Size(75, 23);
            this.buttonZapisz.TabIndex = 3;
            this.buttonZapisz.Text = "zapisz";
            this.buttonZapisz.UseVisualStyleBackColor = false;
            // 
            // buttonAnuluj
            // 
            this.buttonAnuluj.BackColor = System.Drawing.Color.Linen;
            this.buttonAnuluj.Location = new System.Drawing.Point(171, 19);
            this.buttonAnuluj.Name = "buttonAnuluj";
            this.buttonAnuluj.Size = new System.Drawing.Size(75, 23);
            this.buttonAnuluj.TabIndex = 3;
            this.buttonAnuluj.Text = "anuluj";
            this.buttonAnuluj.UseVisualStyleBackColor = false;
            // 
            // groupBoxOs_zarzadzajaca
            // 
            this.groupBoxOs_zarzadzajaca.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxOs_zarzadzajaca.BackColor = System.Drawing.Color.Bisque;
            this.groupBoxOs_zarzadzajaca.Controls.Add(this.listBoxOs_zarzadzajaca);
            this.groupBoxOs_zarzadzajaca.ForeColor = System.Drawing.SystemColors.Desktop;
            this.groupBoxOs_zarzadzajaca.Location = new System.Drawing.Point(12, 109);
            this.groupBoxOs_zarzadzajaca.Name = "groupBoxOs_zarzadzajaca";
            this.groupBoxOs_zarzadzajaca.Size = new System.Drawing.Size(321, 410);
            this.groupBoxOs_zarzadzajaca.TabIndex = 51;
            this.groupBoxOs_zarzadzajaca.TabStop = false;
            this.groupBoxOs_zarzadzajaca.Text = "Osoba zarządzająca";
            // 
            // listBoxOs_zarzadzajaca
            // 
            this.listBoxOs_zarzadzajaca.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxOs_zarzadzajaca.BackColor = System.Drawing.Color.Linen;
            this.listBoxOs_zarzadzajaca.FormattingEnabled = true;
            this.listBoxOs_zarzadzajaca.Location = new System.Drawing.Point(6, 19);
            this.listBoxOs_zarzadzajaca.Name = "listBoxOs_zarzadzajaca";
            this.listBoxOs_zarzadzajaca.Size = new System.Drawing.Size(309, 368);
            this.listBoxOs_zarzadzajaca.TabIndex = 0;
            // 
            // Os_zarzadzajacaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(734, 544);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxSortowanie);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxOs_zarzadzajaca);
            this.Controls.Add(this.toolStrip);
            this.Name = "Os_zarzadzajacaForm";
            this.Text = "Osoba zarządzająca";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxSortowanie.ResumeLayout(false);
            this.groupBoxSortowanie.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBoxOs_zarzadzajaca.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonOs_zarzadzajaca;
        private System.Windows.Forms.ToolStripButton toolStripButtonOperator;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonHelp;
        private System.Windows.Forms.ToolStripButton toolStripButtonOdswiez;
        private System.Windows.Forms.ToolStripButton toolStripButtonSpisForm;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBoxOs_zarzadzajaca_maszyny;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNazwa_Dzial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxOperator_maszyny;
        private System.Windows.Forms.GroupBox groupBoxSortowanie;
        private System.Windows.Forms.RadioButton radioButtonData_ost_przegl;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonSzukaj;
        private System.Windows.Forms.TextBox textBoxWyszukiwanie;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonNowa;
        private System.Windows.Forms.Button buttonUsun;
        private System.Windows.Forms.Button buttonZapisz;
        private System.Windows.Forms.Button buttonAnuluj;
        private System.Windows.Forms.GroupBox groupBoxOs_zarzadzajaca;
        private System.Windows.Forms.ListBox listBoxOs_zarzadzajaca;
    }
}