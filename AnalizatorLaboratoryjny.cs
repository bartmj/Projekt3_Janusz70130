using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Projekt2_Janusz70130
{
    public partial class AnalizatorLaboratoryjny : Form
    {
        public AnalizatorLaboratoryjny()
        {
            InitializeComponent();
        }

        private void AnalizatorLaboratoryjnyForm_Closing(object sender, FormClosingEventArgs e)
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

        private void btnObliczWarto��Fx_Click(object sender, EventArgs e)
        {
            // zgszenie kontrolki errorProvider
            errorProvider1.Dispose();
            // deklaracja zmiennych (pojemnik�w) dla przechowania danych wej�ciowych
            float A, B, C, X;
            // pobranie danych wej�ciowych
            if (!PobierzDaneDlaObliczeniaWarto�ciR�wnania(out A, out B, out C, out X))
            {
                // by� b��d, to go sygnalizuje 
                errorProvider1.SetError(btnObliczWarto��Fx, "ERROR: w zapisie danych wej�ciowych wyst�pi� " + 
                    "niedozwolony znak.");
                // przerwanie obs�ugi zdarzenia Click: btnObliczWarto��Fx_Click
                return;
            }
            // deklaracja zmiennej dla przechowania warto�ci r�wnania
            float Warto��R�wnania;
            // obliczenie warto�ci r�wnania kwadratowego 
            Warto��R�wnania = ObliczWarto��R�wnaniaKwadratowego(A, B, C, X);
            // wizualizacja wyniku oblicze� 
            txtWarto��R�wnania.Text = Warto��R�wnania.ToString();
            // inna (lepsza) forma wizualizacji
            txtWarto��R�wnania.Text = String.Format("{0: 0.00}", Warto��R�wnania);
            txtWarto��R�wnania.Text = String.Format("{0:F3}", Warto��R�wnania);
            // ustawienie stanu braku aktywno�ci dla obs�ugiwanego przycisku polece�
            btnObliczWarto��Fx.Enabled = false;
            // ustawienie stanu braku aktywno�ci dla pola tekstowego
            txtWarto��R�wnania.Enabled = false;
        }

        // deklaracja regionu z metodami pomocniczymi
        #region Metody pomocnicze dla potrzeb tablicowania warto�ci r�wnania (funkcji F(X))

        void TablicowanieWarto�ciFx(float A, float B, float C, float Xd,
            float Xg, float h, out float[,] TWFx)
        // gdzie TWFx, to TabelaWarto�ciFunkcjiZmian warto�ci zmiennej x
        {
            TWFx = null; // pomocniczo

            // wyznaczenieliczby podprzedzia��w przedzia�u [Xd, Xg] z krokiem h
            int n = (int)((Xg - Xd) / h) + 1; /* +1, gdy� wynik dzielenia ((Xg-Xd)/h) b�dzie liczb� u�amkow� i powinni�my go
                                       * zaokr�gli� do najbli�szej liczby ca�kowitej "w g�r�" */
            // utworzenie egzemplarza tablicy TWS
            TWFx = new float[n, 3];
            // deklaracje pomocnicze
            float X; // zmienna niezale�na X
            int i; // numer w podprzedziale w przedziale [Xd, Xg]
            for (X = Xd, i = 0; i < TWFx.GetLength(0); i++, X = Xd + i * h)
            {
                TWFx[i, 0] = i;
                TWFx[i, 1] = X;
                TWFx[i, 2] = ObliczWarto��R�wnaniaKwadratowego(A, B, C, X);
            }
        }

        void WpiszWierszeDanychDoKontrolkiDataGridView(float[,] TWFx, ref DataGridView dgvTWFx)
        {
            // deklaracja odst�pu mi�dzy kontrolk� label1 a kontrolk� DataGridView i Chart
            /*const ushort Odst�p = 50;
            // lokalizacja kontrolki Chart na formularzu
            dgvTWFx.Location = new Point(label1.Left, label1.Top + Odst�p);
            dgvTWFx.Width = (int)(this.Width * 0.4F);
            dgvTWFx.Height = (int)(this.Width * 0.5F);*/

            // wyzerowanie wierszy danych kontrolki DataGridView: dgvTWS
            dgvTWFx.Rows.Clear();
            // wycentrowanie zapisu w kolumnach
            dgvTWFx.Columns[0].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvTWFx.Columns[1].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;
            dgvTWFx.Columns[2].DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            /* przepisanie wierszy danych z tablicy TWFx do kontrolki DataGridView 
             * (najpierw dodajemy nowy wiersz, a nast�pnie do dodanego wiersza wpisujemy dane)*/
            for (int i = 0; i < TWFx.GetLength(0); i++)
            {
                // dodanie nowego wiersza
                dgvTWFx.Rows.Add(TWFx);
                // wpisanie warto�ci numeru przedzia�u X
                dgvTWFx.Rows[i].Cells[0].Value = String.Format("{0}", TWFx[i, 0]);
                // wpisanie warto�ci zmiennej niezale�nej X
                dgvTWFx.Rows[i].Cells[1].Value = String.Format("{0:0.00}", TWFx[i, 1]);
                // wpisanie warto�ci r�wnania
                dgvTWFx.Rows[i].Cells[2].Value = String.Format("{0:F3}", TWFx[i, 2]);
            }
        }

        #endregion
        // deklaracja regionu z metodami pomocniczymi
        #region metody pomocnicze dla obliczenia warto�ci r�wnania

        bool PobierzDaneDlaObliczeniaWarto�ciR�wnania(out float A, out float B, out float C, out float X)
        {
            /* pomocnicze przypisanie warto�ci dla parametr�w wyj�ciowych */
            A = B = C = X = 0.0F;
            // pobranie wsp�czynnika A
            if (!float.TryParse(txtA.Text, out A))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtA, "ERROR: w zapisie warto�ci wsp�czynnika 'a' " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // sprawdzenie warunku dla wsp�czynnika 'a': a != 0.0
            if (A == 0.0F)
            {
                // jest b��d, to go sygnalizujemy
                errorProvider1.SetError(txtA, "ERROR: warto�� wsp�czynnika 'a' musi by� " +
                    "r�na od zera!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // pobranie wsp�czynnika B
            if (!float.TryParse(txtB.Text, out B))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtB, "ERROR: w zapisie warto�ci wsp�czynnika 'b' " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // pobranie wsp�czynnika C
            if (!float.TryParse(txtC.Text, out C))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtC, "ERROR: w zapisie warto�ci wsp�czynnika 'c' " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // pobranie warto�ci zmiennej niezale�nej X
            if (!float.TryParse(txtX.Text, out X))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtX, "ERROR: w zapisie warto�ci wsp�czynnika 'X' " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // gdy nie by�o �adnego b��du to sterowanie przejdzie "tutaj" 
            // ustawienie stanu braku aktywno�ci dla kontrolek z danymi wej�ciowymi
            txtA.Enabled = false;
            txtB.Enabled = false;
            txtC.Enabled = false;
            txtX.Enabled = false;
            /* musimy przekaza� informacj�, �e b��du nie by�o */
            return true;
        }

        float ObliczWarto��R�wnaniaKwadratowego(float A, float B, float C, float X)
        {
            // obliczenie warto�ci r�wnania i jej zwrotne przekazanie
            return ((A * X + B) * X + C);
        }

        private bool PobranieDanychWej�ciowychDlaTablicowania(out float A, out float B, out float C,
            out float Xd, out float Xg, out float h)
        {
            /* pomocnicze przypisanie warto�ci dla parametr�w wyj�ciowych */
            A = B = C = Xd = Xg = h = 0.0F;

            // pobranie wsp�czynnika A
            if (!float.TryParse(txtA.Text, out A))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtA, "ERROR: w zapisie warto�ci wsp�czynnika 'a' " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // sprawdzenie warunku dla wsp�czynnika 'a': a != 0.0
            if (A == 0.0F)
            {
                // jest b��d, to go sygnalizujemy
                errorProvider1.SetError(txtA, "ERROR: warto�� wsp�czynnika 'a' musi by� " +
                    "r�na od zera!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // pobranie wsp�czynnika B
            if (!float.TryParse(txtB.Text, out B))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtB, "ERROR: w zapisie warto�ci wsp�czynnika 'b' " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // pobranie wsp�czynnika C
            if (!float.TryParse(txtC.Text, out C))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtC, "ERROR: w zapisie warto�ci wsp�czynnika 'c' " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }

            // pobranie dolnej ganicy Xd
            if (!float.TryParse(txtXd.Text, out Xd))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtXd, "ERROR: w zapisie dolnej granicy Xd " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }

            // pobranie g�rnej granicy Xg
            if (!float.TryParse(txtXg.Text, out Xg))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtXg, "ERROR: w zapisie warto�ci wsp�czynnika 'Xg' " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // pobranie przyrostu (kroku) h
            if (!float.TryParse(txth.Text, out h))
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txth, "ERROR: w zapisie warto�ci przyrostu (kroku) h " +
                    "wyst�pi� niedozwolony znak!");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }

            /* 
            TUTAJ TRZEBA WSTAWI� SPRAWDZENIE WARUNKU DLA:
            Xd oraz Xg : Xd < Xg 
            h : 0 < h <= (Xg - Xd) / 2
            */
            if(Xd >= Xg)
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtXd, "ERROR: dolna granica Xd musi by� mniejsza od g�rnej granicy Xg");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            if(h <= 0.0)
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txth, "ERROR: nie zosta� spe�niony warunek h > 0");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }

            if (h > Math.Abs(Xg - Xd) / 2)
            {
                // by� b��d, to go sygnalizujemy 
                errorProvider1.SetError(txtXg, "ERROR: nie zosta� spe�niony warunek h <= (Xg - Xd) / 2");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }

            // gdy nie by�o �adnego b��du to sterowanie przejdzie "tutaj" 
            // ustawienie stanu braku aktywno�ci dla kontrolek z danymi wej�ciowymi
            txtA.Enabled = false;
            txtB.Enabled = false;
            txtC.Enabled = false;
            txtXd.Enabled = false;
            txtXg.Enabled = false;
            txth.Enabled = false;
            /* musimy przekaza� informacj�, �e b��du nie by�o */
            return true;
        }

        private void btnWizualizacjaTabelarycznaFx_Click(object sender, EventArgs e)
        {
            // 1. Zgaszenie kontrolki errorProvider
            errorProvider1.Dispose();
            // deklaracja zmiennych dla przechowania danych wej�ciowych 
            float A, B, C, Xd, Xg, h;
            // 2. Pobranie danych wej�ciowych 
            if (!PobranieDanychWej�ciowychDlaTablicowania(out A, out B, out C, out Xd, out Xg, out h))
            {
                // by� b��d, to go sygnalizuje 
                errorProvider1.SetError(btnWizualizacjaTabelarycznaFx, "ERROR: w zapisie danych wej�ciowych wyst�pi� " +
                    "niedozwolony znak.");
                // przerwanie obs�ugi zdarzenia Click: btnWizualizacjaTabelarycznaFx_Click
                return;
            }
            
            // ukrycie wizualizacji graficznej 
            chrtFx.Visible = false;
            // uaktywnienie przycisku wizualizacji graficznej, gdyby u�ytkownik chcia� przechodzi� pomi�dzy wizualizacjami
            btnWizualizacjaGraficznaFx.Enabled = true;
            
            /* 3. Tablicowanie warto�ci r�wnania (funkcji F(X)) w przedziale [Xd, Xg] 
            z przyrostem 'h' */
            // deklaracja zmiennej tablicowej 
            float[,] TWFx;
            // wywo�anie metody tablicowania zmian warto�ci F(x) w podanym przedziale: [Xd, Xg] z 'h'
            TablicowanieWarto�ciFx(A, B, C, Xd, Xg, h, out TWFx);

            // 4. Wpisanie do Kontrolki DataGridView wierszy danych tablicy zmian warto�ci F(X)
            // wywo�anie metody przepisania wierszy tablicy TWFx do kontrolki DataGridView
            WpiszWierszeDanychDoKontrolkiDataGridView(TWFx, ref dgvTWFx);
            // modyfikator ref oznacza, �e dany parametr metordy jest parametrem wej�ciowo-wyj�ciowym

            // 5. Ods�oni�cie kontrolki DataGridView
            dgvTWFx.Visible = true;

            // 6. Ustawienie stanu braku aktywno�ci dla obs�ugiwanego przycisku polece� btnWizualizacjaTabelarycznaFx
            btnWizualizacjaTabelarycznaFx.Enabled = false;

        }

        private void btnWizualizacjaGraficznaFx_Click(object sender, EventArgs e)
        {
            // deklaracja zmiennych dla przechowania danych wej�ciowych 
            float A, B, C, Xd, Xg, h;
            // deklaracja zmiennej tablicowej 
            float[,] TBFx;
            // pobranie danych wej�ciowych 
            if (!PobranieDanychWej�ciowychDlaTablicowania(out A, out B, out C, out Xd, out Xg, out h))
            {
                // by� b��d, to go sygnalizuje 
                errorProvider1.SetError(btnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wej�ciowych wyst�pi� " +
                    "niedozwolony znak.");
                // przerwanie obs�ugi zdarzenia Click: btnWizualizacjaTabelarycznaFx_Click
                return;
            }

            // ukrycie wizualizacji tabelarycznej 
            dgvTWFx.Visible = false;
            // czyszczenie wizualizacji graficznej
            chrtFx.Series.Clear();
            // odkrycie wizualizacji graficznej 
            chrtFx.Visible = true;
            // uaktywnienie przycisku wizualizacji tabelarycznej, gdyby u�ytkownik chcia� przechodzi� pomi�dzy wizualizacjami
            btnWizualizacjaTabelarycznaFx.Enabled = true;

            // dodanie krzywej do obszaru wykresu
            Series Krzywa = new Series("Warto�� r�wnania")
            {
                ChartType = SeriesChartType.Line
            };
            chrtFx.Series.Add(Krzywa);

            // deklaracja zmiennych do przechowywania warto�ci r�wnania
            float Warto��R�wnania;
            // deklaracja i inicjalizacja Indeksu dla punkt�w
            int Index = 0;

            for (float X = Xd; X <= Xg; X += h)
            {
                Warto��R�wnania = ObliczWarto��R�wnaniaKwadratowego(A, B, C, X);
                Krzywa.Points.AddXY(X, Warto��R�wnania);
                Index++;
            }

            btnWizualizacjaGraficznaFx.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            // ukrycie wizualizacji 
            dgvTWFx.Visible = false;
            chrtFx.Visible = false;
            // czyszczenie wizualizacji
            chrtFx.Series.Clear();
            dgvTWFx.Rows.Clear();
            // odblokowanie przycisk�w
            btnWizualizacjaTabelarycznaFx.Enabled = true;
            btnObliczWarto��Fx.Enabled = true;
            btnWizualizacjaGraficznaFx.Enabled = true;
            // odblokowanie okienek z tekstem
            txtA.Enabled = true;
            txtB.Enabled = true;
            txtC.Enabled = true;
            txtX.Enabled = true;
            txtXd.Enabled = true;
            txtXg.Enabled = true;
            txth.Enabled = true;
            txtWarto��R�wnania.Enabled = true;
            // wyzerowanie okienek z tekstem
            txtA.Clear();
            txtB.Clear();
            txtC.Clear();
            txtX.Clear();
            txtXd.Clear();
            txtXg.Clear();
            txth.Clear();
            txtWarto��R�wnania.Clear();
        }

        private void zapiszWierszeDanychKontrolkiDataGridViewWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // wygaszenie kontrolki errorProvider
            errorProvider1.Dispose();
            // sprawdzenie, czy kontrolka DataGridView jest widoczna 
            if (dgvTWFx == null || !dgvTWFx.Visible || dgvTWFx.Rows.Count <= 0)
            {
                errorProvider1.SetError(btnWizualizacjaTabelarycznaFx, "ERROR: " +
                    " polecenie nie mo�e by� zrealizowane bo Kontrolka DataGridView jest " +
                    " niewidoczna lub pusta");
                // przerwanie dalszej obs�ugi 
                return;
            }
            // 1. utworzenie egzemplarza okna dialogowego: OknoPlikuDoZapisu
            SaveFileDialog OknoPlikuDoZapisu = new SaveFileDialog();
            // 2. ustawienie atrybutu okna dialogowego OknoPlikuDoZapisu
            OknoPlikuDoZapisu.Title = "Wyb�r pliku do wpisania wierszy danych z kontrolki " +
                "DataGridView";
            OknoPlikuDoZapisu.Filter = "txtfiles(*.txt)|*.txt|all files (*.*)|*.*";
            OknoPlikuDoZapisu.FilterIndex = 1;
            OknoPlikuDoZapisu.RestoreDirectory = true;
            OknoPlikuDoZapisu.InitialDirectory = "C:\\";
            // wizualizacja okna OknoPlikuDoZapisu i odczytanie informacji o wyborze pliku
            if (OknoPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                // plik zosta� wybrany lub zosta� utworzony nowy plik 
                // utworzenie egzemplarza strumienia do zapisu 
                StreamWriter PlikZnakowy = new StreamWriter(OknoPlikuDoZapisu.FileName);
                try
                { // wypisaywanie do pliku wierszy danych kontrolki DataGridView
                    for (int i = 0; i < dgvTWFx.Rows.Count; i++)
                    {
                        if ((dgvTWFx.Rows[i].Cells[0].Value != null) &&
                            (dgvTWFx.Rows[i].Cells[1].Value != null) &&
                            (dgvTWFx.Rows[i].Cells[2].Value != null)
                            )
                        {
                            PlikZnakowy.Write(dgvTWFx.Rows[i].Cells[0].Value.ToString());
                            PlikZnakowy.Write(";");
                            PlikZnakowy.Write(dgvTWFx.Rows[i].Cells[1].Value.ToString());
                            PlikZnakowy.Write(";");
                            PlikZnakowy.WriteLine(dgvTWFx.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
                catch (Exception B��d)
                {
                    MessageBox.Show("ERROR: wyst�pi� nieoczekiwany b��d podczas zapisu " +
                        "wierszy danych z kontrolki DataGridView (komunikat systemowy: " +
                        B��d.Message + " )");
                }
                finally
                {
                    PlikZnakowy.Close();
                    PlikZnakowy.Dispose();
                }
                // zmkni�cie okna dialogowego OknoPlikuDoZapisu 
                OknoPlikuDoZapisu.Dispose();
            }
        }

        private void pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
                // wygaszenie kontrolki errorProvider
                errorProvider1.Dispose();
                // 1. utworzenie egzemplarza okna dialogowego: OknoPlikuDoOdczytu
                OpenFileDialog OknoPlikuDoOdczytu = new OpenFileDialog();
                // 2. ustawienie atrybutu okna dialogowego OknoPlikuDoOdczytu
                OknoPlikuDoOdczytu.Title = "Wyb�r pliku do wczytania wierszy danych do kontrolki " +
                    "DataGridView";
                OknoPlikuDoOdczytu.Filter = "txtfiles(*.txt)|*.txt|all files (*.*)|*.*";
                OknoPlikuDoOdczytu.FilterIndex = 1;
                OknoPlikuDoOdczytu.RestoreDirectory = true;
                OknoPlikuDoOdczytu.InitialDirectory = "C:\\";

                // wizualizacja okna OknoPlikuDoOdczytu i odczytanie informacji o wyborze pliku
                if (OknoPlikuDoOdczytu.ShowDialog() == DialogResult.OK)
                {
                    // plik zosta� wybrany 
                    // utworzenie egzemplarza strumienia do odczytu 
                    StreamReader PlikZnakowy = new StreamReader(OknoPlikuDoOdczytu.FileName);
                    try
                    {
                        // wczytywanie wierszy danych do kontrolki DataGridView
                        string linia;
                        while ((linia = PlikZnakowy.ReadLine()) != null)
                        {
                            string[] dane = linia.Split(';');
                            if (dane.Length == 3)
                            {
                                dgvTWFx.Rows.Add(dane[0], dane[1], dane[2]);
                            }
                        }
                    }
                    catch (Exception B��d)
                    {
                        MessageBox.Show("ERROR: wyst�pi� nieoczekiwany b��d podczas odczytu " +
                            "wierszy danych z pliku (komunikat systemowy: " +
                            B��d.Message + " )");
                    }
                    finally
                    {
                        PlikZnakowy.Close();
                        PlikZnakowy.Dispose();
                    }
                    // zmkni�cie okna dialogowego OknoPlikuDoOdczytu 
                    OknoPlikuDoOdczytu.Dispose();

                    // 5. Ods�oni�cie kontrolki DataGridView
                    dgvTWFx.Visible = true;

                    // 6. Ustawienie stanu braku aktywno�ci dla obs�ugiwanego przycisku polece� btnWizualizacjaTabelarycznaFx
                    btnWizualizacjaTabelarycznaFx.Enabled = false;
                }
            }
        }
    }
#endregion

