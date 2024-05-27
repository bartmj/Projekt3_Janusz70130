using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projekt2_Janusz70130
{
    public partial class KokpitNr2 : Form
    {
        public KokpitNr2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAnalizatorLaboratoryjnyProjektuNr2_Click(object sender, EventArgs e)
        {
            // Sprawdzenie, czy egzemplarz formularza AnalizatorLaboratoryjny by� ju� stworzony
            foreach (Form FormularzZnaleziony in Application.OpenForms)
            {
                // Sprawdzenie, czy jest to poszukiwany formularz
                if (FormularzZnaleziony.Name == "AnalizatorLaboratoryjny")
                {
                    // Ukrycie bie��cego formularza
                    this.Hide();
                    // Ods�oni�cie formularza znalezionego
                    FormularzZnaleziony.Show();
                    // Zako�czenie metody, aby nie tworzy� nowego formularza
                    return;
                }
            }

            // Gdy b�dziemy tutaj, to b�dzie to oznacza�o, �e formularz AnalizatorLaboratoryjny nie zosta� znaleziony
            // Tworzymy egzemplarz formularza AnalizatorLaboratoryjny
            AnalizatorLaboratoryjny FormAnalizatorLaboratoryjny = new AnalizatorLaboratoryjny();
            this.Hide(); // Ukrycie "starego" formularza
            FormAnalizatorLaboratoryjny.Show(); // Ods�oni�cie nowego formularza
        }

        private void Kokpit_Load(object sender, EventArgs e)
        {

        }

        private void KokpitForm_Closing(object sender, FormClosingEventArgs e)
        {
            /* Utworzenie egzemplarza okna dialogowego ""MessageBox z pytaniem, czy naprawd� 
             * nale�y zamkn�� ten formularz */
            DialogResult OknoMessage = MessageBox.Show("Czy na pewno chcesz zamkn�� ten formularz?",
             this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (OknoMessage == DialogResult.Yes)
            {
                foreach (Form mainForm in Application.OpenForms)
                {
                    if (mainForm.Name == "KokpitNr2")
                    {
                        this.Hide();
                        mainForm.Show();
                        return;
                    }
                }
            }
            else
            if (OknoMessage == DialogResult.No)
                e.Cancel = true;
            else
                e.Cancel = true;
        }

        private void btnAnalizatorIndywidualnyProjektuNr2_Click(object sender, EventArgs e)
        {
            // Sprawdzenie, czy egzemplarz formularza AnalizatorIndywidualny by� ju� stworzony
            foreach (Form FormularzZnaleziony in Application.OpenForms)
            {
                // Sprawdzenie, czy jest to poszukiwany formularz
                if (FormularzZnaleziony.Name == "AnalizatorIndywidualny")
                {
                    // Ukrycie bie��cego formularza
                    this.Hide();
                    // Ods�oni�cie formularza znalezionego
                    FormularzZnaleziony.Show();
                    // Zako�czenie metody, aby nie tworzy� nowego formularza
                    return;
                }
            }

            // Gdy b�dziemy tutaj, to b�dzie to oznacza�o, �e formularz AnalizatorIndywidualny nie zosta� znaleziony
            // Tworzymy egzemplarz formularza AnalizatorIndywidualny
            AnalizatorIndywidualny FormAnalizatorLaboratoryjny = new AnalizatorIndywidualny();
            this.Hide(); // Ukrycie "starego" formularza
            FormAnalizatorLaboratoryjny.Show(); // Ods�oni�cie nowego formularza
        }

        private void bjBtnSprawdzian_Click(object sender, EventArgs e)
        {
            // Sprawdzenie, czy egzemplarz formularza by� ju� stworzony
            foreach (Form FormularzZnaleziony in Application.OpenForms)
            {
                // Sprawdzenie, czy jest to poszukiwany formularz
                if (FormularzZnaleziony.Name == "AnalizatorSprawdzian")
                {
                    // Ukrycie bie��cego formularza
                    this.Hide();
                    // Ods�oni�cie formularza znalezionego
                    FormularzZnaleziony.Show();
                    // Zako�czenie metody, aby nie tworzy� nowego formularza
                    return;
                }
            }

            AnalizatorSprawdzian FormAnalizatorSprawdzian = new AnalizatorSprawdzian();
            this.Hide(); // Ukrycie "starego" formularza
            FormAnalizatorSprawdzian.Show(); // Ods�oni�cie nowego formularza
        }
    }
 }

