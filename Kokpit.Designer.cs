namespace Projekt2_Janusz70130
{
    partial class KokpitNr2
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczy�� wszystkie u�ywane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, je�eli zarz�dzane zasoby powinny zosta� zlikwidowane; Fa�sz w przeciwnym wypadku.</param>
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
        /// Metoda wymagana do obs�ugi projektanta � nie nale�y modyfikowa�
        /// jej zawarto�ci w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnAnalizatorLaboratoryjnyProjektuNr2 = new System.Windows.Forms.Button();
            this.btnAnalizatorIndywidualnyProjektuNr2 = new System.Windows.Forms.Button();
            this.bjBtnSprawdzian = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(682, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "Analizator tabelaryczny i graficzny funkcji matematycznej w okre�lonym przedziale" +
    " zmian warto��i zmiennej niezale�nej X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnAnalizatorLaboratoryjnyProjektuNr2
            // 
            this.btnAnalizatorLaboratoryjnyProjektuNr2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnAnalizatorLaboratoryjnyProjektuNr2.Location = new System.Drawing.Point(116, 134);
            this.btnAnalizatorLaboratoryjnyProjektuNr2.Name = "btnAnalizatorLaboratoryjnyProjektuNr2";
            this.btnAnalizatorLaboratoryjnyProjektuNr2.Size = new System.Drawing.Size(195, 60);
            this.btnAnalizatorLaboratoryjnyProjektuNr2.TabIndex = 1;
            this.btnAnalizatorLaboratoryjnyProjektuNr2.Text = "Analizator Laboratoryjny Projektu nr 2";
            this.btnAnalizatorLaboratoryjnyProjektuNr2.UseVisualStyleBackColor = true;
            this.btnAnalizatorLaboratoryjnyProjektuNr2.Click += new System.EventHandler(this.btnAnalizatorLaboratoryjnyProjektuNr2_Click);
            // 
            // btnAnalizatorIndywidualnyProjektuNr2
            // 
            this.btnAnalizatorIndywidualnyProjektuNr2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnAnalizatorIndywidualnyProjektuNr2.Location = new System.Drawing.Point(391, 134);
            this.btnAnalizatorIndywidualnyProjektuNr2.Name = "btnAnalizatorIndywidualnyProjektuNr2";
            this.btnAnalizatorIndywidualnyProjektuNr2.Size = new System.Drawing.Size(195, 60);
            this.btnAnalizatorIndywidualnyProjektuNr2.TabIndex = 2;
            this.btnAnalizatorIndywidualnyProjektuNr2.Text = "Analizator Indywidualny Projektu nr 2";
            this.btnAnalizatorIndywidualnyProjektuNr2.UseVisualStyleBackColor = true;
            this.btnAnalizatorIndywidualnyProjektuNr2.Click += new System.EventHandler(this.btnAnalizatorIndywidualnyProjektuNr2_Click);
            // 
            // bjBtnSprawdzian
            // 
            this.bjBtnSprawdzian.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bjBtnSprawdzian.Location = new System.Drawing.Point(261, 231);
            this.bjBtnSprawdzian.Name = "bjBtnSprawdzian";
            this.bjBtnSprawdzian.Size = new System.Drawing.Size(195, 60);
            this.bjBtnSprawdzian.TabIndex = 3;
            this.bjBtnSprawdzian.Text = "Sprawdzian 2";
            this.bjBtnSprawdzian.UseVisualStyleBackColor = true;
            this.bjBtnSprawdzian.Click += new System.EventHandler(this.bjBtnSprawdzian_Click);
            // 
            // KokpitNr2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(717, 331);
            this.Controls.Add(this.bjBtnSprawdzian);
            this.Controls.Add(this.btnAnalizatorIndywidualnyProjektuNr2);
            this.Controls.Add(this.btnAnalizatorLaboratoryjnyProjektuNr2);
            this.Controls.Add(this.label1);
            this.Name = "KokpitNr2";
            this.Text = "Kokpit_ProjektuNr2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KokpitForm_Closing);
            this.Load += new System.EventHandler(this.Kokpit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAnalizatorLaboratoryjnyProjektuNr2;
        private System.Windows.Forms.Button btnAnalizatorIndywidualnyProjektuNr2;
        private System.Windows.Forms.Button bjBtnSprawdzian;
    }
}

