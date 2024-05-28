namespace Projekt2_Janusz70130
{
    partial class KokpitNr2
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyœæ wszystkie u¿ywane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, je¿eli zarz¹dzane zasoby powinny zostaæ zlikwidowane; Fa³sz w przeciwnym wypadku.</param>
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
        /// Metoda wymagana do obs³ugi projektanta — nie nale¿y modyfikowaæ
        /// jej zawartoœci w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnAnalizatorLaboratoryjnyProjektuNr3 = new System.Windows.Forms.Button();
            this.btnAnalizatorIndywidualnyProjektuNr3 = new System.Windows.Forms.Button();
            this.btnSpr3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(682, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "Analizator tabelaryczny i graficzny funkcji matematycznej w okreœlonym przedziale" +
    " zmian wartoœæi zmiennej niezale¿nej X";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnAnalizatorLaboratoryjnyProjektuNr3
            // 
            this.btnAnalizatorLaboratoryjnyProjektuNr3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnAnalizatorLaboratoryjnyProjektuNr3.Location = new System.Drawing.Point(116, 134);
            this.btnAnalizatorLaboratoryjnyProjektuNr3.Name = "btnAnalizatorLaboratoryjnyProjektuNr3";
            this.btnAnalizatorLaboratoryjnyProjektuNr3.Size = new System.Drawing.Size(195, 60);
            this.btnAnalizatorLaboratoryjnyProjektuNr3.TabIndex = 1;
            this.btnAnalizatorLaboratoryjnyProjektuNr3.Text = "Analizator Laboratoryjny Projektu nr 3";
            this.btnAnalizatorLaboratoryjnyProjektuNr3.UseVisualStyleBackColor = true;
            this.btnAnalizatorLaboratoryjnyProjektuNr3.Click += new System.EventHandler(this.btnAnalizatorLaboratoryjnyProjektuNr2_Click);
            // 
            // btnAnalizatorIndywidualnyProjektuNr3
            // 
            this.btnAnalizatorIndywidualnyProjektuNr3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnAnalizatorIndywidualnyProjektuNr3.Location = new System.Drawing.Point(391, 134);
            this.btnAnalizatorIndywidualnyProjektuNr3.Name = "btnAnalizatorIndywidualnyProjektuNr3";
            this.btnAnalizatorIndywidualnyProjektuNr3.Size = new System.Drawing.Size(195, 60);
            this.btnAnalizatorIndywidualnyProjektuNr3.TabIndex = 2;
            this.btnAnalizatorIndywidualnyProjektuNr3.Text = "Analizator Indywidualny Projektu nr 3";
            this.btnAnalizatorIndywidualnyProjektuNr3.UseVisualStyleBackColor = true;
            this.btnAnalizatorIndywidualnyProjektuNr3.Click += new System.EventHandler(this.btnAnalizatorIndywidualnyProjektuNr2_Click);
            // 
            // btnSpr3
            // 
            this.btnSpr3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSpr3.Location = new System.Drawing.Point(391, 231);
            this.btnSpr3.Name = "btnSpr3";
            this.btnSpr3.Size = new System.Drawing.Size(195, 60);
            this.btnSpr3.TabIndex = 4;
            this.btnSpr3.Text = "Sprawdzian 3";
            this.btnSpr3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(116, 231);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(195, 60);
            this.button2.TabIndex = 3;
            this.button2.Text = "Sprawdzian 2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.bjBtnSprawdzian_Click);
            // 
            // KokpitNr2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(717, 331);
            this.Controls.Add(this.btnSpr3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAnalizatorIndywidualnyProjektuNr3);
            this.Controls.Add(this.btnAnalizatorLaboratoryjnyProjektuNr3);
            this.Controls.Add(this.label1);
            this.Name = "KokpitNr2";
            this.Text = "Kokpit_ProjektuNr2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KokpitForm_Closing);
            this.Load += new System.EventHandler(this.Kokpit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAnalizatorLaboratoryjnyProjektuNr3;
        private System.Windows.Forms.Button btnAnalizatorIndywidualnyProjektuNr3;
        private System.Windows.Forms.Button btnSpr3;
        private System.Windows.Forms.Button button2;
    }
}

