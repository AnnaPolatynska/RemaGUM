namespace RemaGUM
{
    partial class Frame
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
            this.labelTekst = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTekst
            // 
            this.labelTekst.AutoEllipsis = true;
            this.labelTekst.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelTekst.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTekst.ForeColor = System.Drawing.Color.Red;
            this.labelTekst.Location = new System.Drawing.Point(38, 9);
            this.labelTekst.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTekst.Name = "labelTekst";
            this.labelTekst.Size = new System.Drawing.Size(328, 69);
            this.labelTekst.TabIndex = 1;
            this.labelTekst.Text = "Tekst";
            this.labelTekst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Frame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.OldLace;
            this.ClientSize = new System.Drawing.Size(398, 98);
            this.Controls.Add(this.labelTekst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frame";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTekst;
    }
}