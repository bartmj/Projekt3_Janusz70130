using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Projekt2_Janusz70130
{
    public partial class AnalizatorSprawdzian : Form
    {
        public AnalizatorSprawdzian()
        {
            InitializeComponent();
        }
        
        private void bjBtnObliczWarto��Fx_Click(object sender, EventArgs e)
        {
            // Pobranie zmiennej X
            if (!bjSpr�bujPobra�FloatIObs��B��d(bjTxt.Text, out float bjX, bjTxtFX, "X"))
            {
                return;
            }
            // Sprawdzenie dziedziny
            if (bjX < 0)
            {
                // sygnalizujemy b��d
                bjErrorProvider3.SetError(bjTxt, "ERROR: X nie nale�y do dziedziny funkcji ");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return;
            }
            // zgszenie kontrolki errorProvider
            bjErrorProvider3.Dispose();
            // obliczenie warto�ci funkcji
            float bjWarto��Funkcji = bjObliczWarto��Funkcji(bjX);
            // wizualizacja wyniku oblicze� 
            bjTxtFX.Text = String.Format("{0:F3}", bjWarto��Funkcji);
            // ustawienie stanu braku aktywno�ci dla obs�ugiwanego przycisku polece�
            bjBtnObliczWarto��Fx.Enabled = false;
            // ustawienie stanu braku aktywno�ci dla pola tekstowego
            bjTxtFX.Enabled = false;
            // zablokowanie przycisku X 
            bjTxtFX.Enabled = false;
        }


        private bool bjSpr�bujPobra�FloatIObs��B��d(string bjText, out float bjLiczbaFloat, Control bjKontrolka, string bjNazwaWsp�czynnika)
        {
            if (!float.TryParse(bjText, out bjLiczbaFloat))
            {
                bjErrorProvider3.SetError(bjKontrolka, $"ERROR: w zapisie warto�ci wsp�czynnika '{bjNazwaWsp�czynnika}' wyst�pi� niedozwolony znak!");
                return false;
            }
            return true;
        }


        #region funkcje pomocnicze analizatora indywidualnego
        private float bjObliczWarto��Funkcji(float bjX)
        {
            if (bjX > 0 && bjX <= 2)
            {
                return (float)(Math.Pow(bjX, 2) * Math.Pow(2, -1 * bjX));
            }
            else if (bjX > 2)
                return (float)(bjX * Math.Log(1 + Math.Pow(bjX, 2)));
            else 
                // w innym przypadku zwracamy zero z funkcji bo tylko zero jest mo�liwe,
                // oczyiw�cie po sprawdzeniu wcze�niej warunku dziedziny funkcji 
            return 0;
        }

        private bool bjPobranieDanychWej�ciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH)
        {
            /* pomocnicze przypisanie warto�ci dla parametr�w wyj�ciowych */
            bjXd = bjXg = bjH = 0.0F;

            // Pobranie wsp�czynnika bjXd
            if (!bjSpr�bujPobra�FloatIObs��B��d(bjTxtXd.Text, out bjXd, bjTxtXd, "Xd"))
            {
                return false;
            }

            // Pobranie wsp�czynnika bjXg
            if (!bjSpr�bujPobra�FloatIObs��B��d(bjTxtXg.Text, out bjXg, bjTxtXg, "Xg"))
            {
                return false;
            }

            // Pobranie wsp�czynnika bjH
            if (!bjSpr�bujPobra�FloatIObs��B��d(bjTxtH.Text, out bjH, bjTxtH, "H"))
            {
                return false;
            }

            /* 
            SPRAWDZENIE WARUNKU DLA:
            Xd oraz Xg : Xd < Xg 
            RAZ
            h : 0 < h <= (Xg - Xd) / 2
            */

            // Sprawdzenie dziedziny
            if (bjXd < 0)
            {
                // sygnalizujemy b��d
                bjErrorProvider3.SetError(bjTxtXd, "ERROR: X nie nale�y do dziedziny funkcji ");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            if (bjXd >= bjXg)
            {
                // sygnalizujemy b��d
                bjErrorProvider3.SetError(bjTxtXd, "ERROR: dolna granica Xd musi by� mniejsza od g�rnej granicy Xg");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            if (bjH <= 0.0)
            {
                // sygnalizujemy b��d
                bjErrorProvider3.SetError(bjTxtH, "ERROR: nie zosta� spe�niony warunek h > 0");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }

            if (bjH > Math.Abs(bjXg - bjXd) / 2)
            {
                // sygnalizujemy b��d
                bjErrorProvider3.SetError(bjTxtH, "ERROR: nie zosta� spe�niony warunek h <= (Xg - Xd) / 2");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // gdy nie by�o �adnego b��du to sterowanie przejdzie "tutaj" 
            // ustawienie stanu braku aktywno�ci dla kontrolek z danymi wej�ciowymi
            bjTxtXd.Enabled = false;
            bjTxtXg.Enabled = false;
            bjTxtH.Enabled = false;
            /* musimy przekaza� informacj�, �e b��du nie by�o */
            return true;
        }

        private void bjTablicowanieWarto�ciFunkcjiFx(float bjXd, float bjXg, float bjH, out float[,] bjTWFx)
        {
            // wyznaczenie liczby podprzedzia��w przedzia�u [Xd, Xg] z krokiem h
            int bjN = (int)((bjXg - bjXd) / bjH) + 1;
            // +1, gdy� wynik dzielenia ((Xg-Xd)/h) b�dzie liczb� u�amkow�
            // i powinni�my go zaokr�gli� do najbli�szej liczby ca�kowitej "w g�r�" 

            // utworzenie egzemplarza tablicy bjTWFx
            bjTWFx = new float[bjN, 3];
            // deklaracje pomocnicze
            float bjX; // zmienna niezale�na X
            int bjI; // numer w podprzedziale w przedziale [Xd, Xg]
            for (bjX = bjXd, bjI = 0; bjI < bjTWFx.GetLength(0); bjI++, bjX = bjXd + bjI * bjH)
            {
                bjTWFx[bjI, 0] = bjI;
                bjTWFx[bjI, 1] = bjX;
                bjTWFx[bjI, 2] = bjObliczWarto��Funkcji(bjX);
            }
        }

        private void bjWpiszWierszeDanychDoKontrolkiDataGridView(float[,] bjTWFx, ref DataGridView bjDgvTWFx)
        {
            /* przepisanie wierszy danych z tablicy bjTWFx do kontrolki DataGridView 
             * (najpierw dodajemy nowy wiersz, a nast�pnie do dodanego wiersza wpisujemy dane)*/
            for (int bjI = 0; bjI < bjTWFx.GetLength(0); bjI++)
            {
                // dodanie nowego wiersza
                bjDgvTWFx.Rows.Add(bjTWFx);
                // wpisanie warto�ci numeru przedzia�u X
                bjDgvTWFx.Rows[bjI].Cells[0].Value = String.Format("{0}", bjTWFx[bjI, 0]);
                // wpisanie warto�ci zmiennej niezale�nej X
                bjDgvTWFx.Rows[bjI].Cells[1].Value = String.Format("{0:F3}", bjTWFx[bjI, 1]);
                // wpisanie warto�ci r�wnania
                bjDgvTWFx.Rows[bjI].Cells[2].Value = String.Format("{0:F3}", bjTWFx[bjI, 2]);
            }
        }

        private void bjWpiszWierszeDanychDoKontrolkiChart(float[,] bjTWFx, ref Chart bjChrt)
        {

            // dodanie krzywej do obszaru wykresu
            Series bjKrzywa = new Series("Warto�� funkcji F(X)")
            {
                ChartType = SeriesChartType.Line
            };
            bjChrt.Series.Add(bjKrzywa);

            for (int bjI = 0; bjI < bjTWFx.GetLength(0); bjI++)
            {
                // dodawanie do krzywej punkt�w z tablicy 
                bjKrzywa.Points.AddXY(bjTWFx[bjI, 1], bjTWFx[bjI, 2]);
            }
        }
        #endregion

        private void bjBtnWizualizacjaTabelarycznaFx_Click(object sender, EventArgs e)
        {
            // 1. zgaszenie kontrolki errorProvider
            bjErrorProvider3.Dispose();
            // 2. Pobranie danych wej�ciowych 
            if (!bjPobranieDanychWej�ciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH))
            {
                // by� b��d, to go sygnalizuje 
                bjErrorProvider3.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: w zapisie danych wej�ciowych wyst�pi� " +
                    "niedozwolony znak.");
                // przerwanie obs�ugi zdarzenia Click: btnWizualizacjaTabelarycznaFx_Click
                return;
            }

            // 3. Po poprawnym za�adowaniu dnaych ustawienie stanu kontrolek
            // ukrycie kontrolki wizualizacji graficznej
            bjChrt.Visible = false;
            // resetowanie kontrolki wizualizacji tabelarycznej
            bjDgvTWFx.Rows.Clear();
            // odkrycie kontrolki wizualizacji tabelarycznej
            bjDgvTWFx.Visible = true;
            // zablokowanie przycisku wizualizacji tabelarycznej
            bjBtnWizualizacjaTabelarycznaFx.Enabled = false;
            // odblokowanie przycisku wizualizacji graficznej
            bjBtnWizualizacjaGraficznaFx.Enabled = true;

            /* 4.Tablicowanie warto�ci r�wnania(funkcji F(X)) w przedziale[Xd, Xg]
            z przyrostem 'h' */
            // wywo�anie metody tablicowania zmian warto�ci F(x) w podanym przedziale: [Xd, Xg] z 'h'
            bjTablicowanieWarto�ciFunkcjiFx(bjXd, bjXg, bjH, out float[,] bjTWFx);
            // 5. Wpisanie do Kontrolki DataGridView wierszy danych tablicy zmian warto�ci F(X)
            // wywo�anie metody przepisania wierszy tablicy TWFx do kontrolki DataGridView
            bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
            // modyfikator ref oznacza, �e dany parametr metordy jest parametrem wej�ciowo-wyj�ciowym
        }

        private void bjBtnWizualizacjaGraficznaFx_Click(object sender, EventArgs e)
        {

            // 1. Zgaszenie kontrolki errorProvider
            bjErrorProvider3.Dispose();
            // 2. Pobranie danych wej�ciowych 
            if (!bjPobranieDanychWej�ciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH))
            {
                // by� b��d, to go sygnalizuje 
                bjErrorProvider3.SetError(bjBtnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wej�ciowych wyst�pi� " +
                    "niedozwolony znak.");
                // przerwanie obs�ugi zdarzenia Click: btnWizualizacjaGraficznaFx_Click
                return;
            }
            // 3. Po poprawnym za�adowaniu dnaych ustawienie odpowedniego stanu kontrolek
            // ukrycie kontrolki wizualizacji tabelarycznej
            bjDgvTWFx.Visible = false;
            // resetowanie kontrolki wizualizacji graficznej
            bjChrt.Series.Clear();
            // odkrycie kontrolki wizualizacji graficznej
            bjChrt.Visible = true;
            // odblokowanie przycisku wizualizacji tabelarycznej
            bjBtnWizualizacjaTabelarycznaFx.Enabled = true;
            // zablokowanie przycisku wizualizacji graficznej
            bjBtnWizualizacjaGraficznaFx.Enabled = false;
            // 4.Tablicowanie warto�ci r�wnania(funkcji F(X)) w przedziale[Xd, Xg] z przyrostem 'h' */
            // wywo�anie metody tablicowania 
            bjTablicowanieWarto�ciFunkcjiFx(bjXd, bjXg, bjH, out float[,] bjTWFx);
            // 5. Wpisanie do kontrolki Chart wierszy danych tablicy zmian warto�ci F(X)
            // wywo�anie metody przepisania wierszy tablicy TWFx do kontrolki Chart
            bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
            // modyfikator ref oznacza, �e dany parametr metordy jest parametrem wej�ciowo-wyj�ciowym
        }

        private void bjBtnReset_Click(object sender, EventArgs e)
        {
            bjErrorProvider3.Clear();
            // ukrycie wizualizacji 
            bjDgvTWFx.Visible = false;
            bjChrt.Visible = false;
            // czyszczenie wizualizacji
            bjDgvTWFx.Rows.Clear();
            bjChrt.Series.Clear();
            // odblokowanie przycisk�w
            bjBtnObliczWarto��Fx.Enabled = true;
            bjBtnWizualizacjaGraficznaFx.Enabled = true;
            bjBtnWizualizacjaTabelarycznaFx.Enabled = true;
            // odblokowanie i czyszczenie okienek z tekstem
            List<TextBox> bjTextBoxy = new List<TextBox>
            {
                bjTxtFX,
                bjTxt, 
                bjTxtXd,
                bjTxtXg,
                bjTxtH,

            };
            
            foreach (TextBox bjTB in bjTextBoxy)
            {
                bjTB.Enabled = true;
                bjTB.Clear();
            }
        }

        private void bjAnalizatorSprawdzianForm_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult OknoMessage =
                    MessageBox.Show("Czy na pewno chcesz zamkn�� ten formularz?",
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
    }
}
