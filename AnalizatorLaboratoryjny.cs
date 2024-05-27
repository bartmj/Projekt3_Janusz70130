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
                    MessageBox.Show("Czy na pewno chcesz zamkn¹æ ten formularz?",
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

        private void btnObliczWartoœæFx_Click(object sender, EventArgs e)
        {
            // zgszenie kontrolki errorProvider
            errorProvider1.Dispose();
            // deklaracja zmiennych (pojemników) dla przechowania danych wejœciowych
            float A, B, C, X;
            // pobranie danych wejœciowych
            if (!PobierzDaneDlaObliczeniaWartoœciRównania(out A, out B, out C, out X))
            {
                // by³ b³¹d, to go sygnalizuje 
                errorProvider1.SetError(btnObliczWartoœæFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " + 
                    "niedozwolony znak.");
                // przerwanie obs³ugi zdarzenia Click: btnObliczWartoœæFx_Click
                return;
            }
            // deklaracja zmiennej dla przechowania wartoœci równania
            float WartoœæRównania;
            // obliczenie wartoœci równania kwadratowego 
            WartoœæRównania = ObliczWartoœæRównaniaKwadratowego(A, B, C, X);
            // wizualizacja wyniku obliczeñ 
            txtWartoœæRównania.Text = WartoœæRównania.ToString();
            // inna (lepsza) forma wizualizacji
            txtWartoœæRównania.Text = String.Format("{0: 0.00}", WartoœæRównania);
            txtWartoœæRównania.Text = String.Format("{0:F3}", WartoœæRównania);
            // ustawienie stanu braku aktywnoœci dla obs³ugiwanego przycisku poleceñ
            btnObliczWartoœæFx.Enabled = false;
            // ustawienie stanu braku aktywnoœci dla pola tekstowego
            txtWartoœæRównania.Enabled = false;
        }

        // deklaracja regionu z metodami pomocniczymi
        #region Metody pomocnicze dla potrzeb tablicowania wartoœci równania (funkcji F(X))

        void TablicowanieWartoœciFx(float A, float B, float C, float Xd,
            float Xg, float h, out float[,] TWFx)
        // gdzie TWFx, to TabelaWartoœciFunkcjiZmian wartoœci zmiennej x
        {
            TWFx = null; // pomocniczo

            // wyznaczenieliczby podprzedzia³ów przedzia³u [Xd, Xg] z krokiem h
            int n = (int)((Xg - Xd) / h) + 1; /* +1, gdy¿ wynik dzielenia ((Xg-Xd)/h) bêdzie liczb¹ u³amkow¹ i powinniœmy go
                                       * zaokr¹gliæ do najbli¿szej liczby ca³kowitej "w górê" */
            // utworzenie egzemplarza tablicy TWS
            TWFx = new float[n, 3];
            // deklaracje pomocnicze
            float X; // zmienna niezale¿na X
            int i; // numer w podprzedziale w przedziale [Xd, Xg]
            for (X = Xd, i = 0; i < TWFx.GetLength(0); i++, X = Xd + i * h)
            {
                TWFx[i, 0] = i;
                TWFx[i, 1] = X;
                TWFx[i, 2] = ObliczWartoœæRównaniaKwadratowego(A, B, C, X);
            }
        }

        void WpiszWierszeDanychDoKontrolkiDataGridView(float[,] TWFx, ref DataGridView dgvTWFx)
        {
            // deklaracja odstêpu miêdzy kontrolk¹ label1 a kontrolk¹ DataGridView i Chart
            /*const ushort Odstêp = 50;
            // lokalizacja kontrolki Chart na formularzu
            dgvTWFx.Location = new Point(label1.Left, label1.Top + Odstêp);
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
             * (najpierw dodajemy nowy wiersz, a nastêpnie do dodanego wiersza wpisujemy dane)*/
            for (int i = 0; i < TWFx.GetLength(0); i++)
            {
                // dodanie nowego wiersza
                dgvTWFx.Rows.Add(TWFx);
                // wpisanie wartoœci numeru przedzia³u X
                dgvTWFx.Rows[i].Cells[0].Value = String.Format("{0}", TWFx[i, 0]);
                // wpisanie wartoœci zmiennej niezale¿nej X
                dgvTWFx.Rows[i].Cells[1].Value = String.Format("{0:0.00}", TWFx[i, 1]);
                // wpisanie wartoœci równania
                dgvTWFx.Rows[i].Cells[2].Value = String.Format("{0:F3}", TWFx[i, 2]);
            }
        }

        #endregion
        // deklaracja regionu z metodami pomocniczymi
        #region metody pomocnicze dla obliczenia wartoœci równania

        bool PobierzDaneDlaObliczeniaWartoœciRównania(out float A, out float B, out float C, out float X)
        {
            /* pomocnicze przypisanie wartoœci dla parametrów wyjœciowych */
            A = B = C = X = 0.0F;
            // pobranie wspó³czynnika A
            if (!float.TryParse(txtA.Text, out A))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtA, "ERROR: w zapisie wartoœci wspó³czynnika 'a' " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // sprawdzenie warunku dla wspó³czynnika 'a': a != 0.0
            if (A == 0.0F)
            {
                // jest b³¹d, to go sygnalizujemy
                errorProvider1.SetError(txtA, "ERROR: wartoœæ wspó³czynnika 'a' musi byæ " +
                    "ró¿na od zera!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // pobranie wspó³czynnika B
            if (!float.TryParse(txtB.Text, out B))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtB, "ERROR: w zapisie wartoœci wspó³czynnika 'b' " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // pobranie wspó³czynnika C
            if (!float.TryParse(txtC.Text, out C))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtC, "ERROR: w zapisie wartoœci wspó³czynnika 'c' " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // pobranie wartoœci zmiennej niezale¿nej X
            if (!float.TryParse(txtX.Text, out X))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtX, "ERROR: w zapisie wartoœci wspó³czynnika 'X' " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // gdy nie by³o ¿adnego b³êdu to sterowanie przejdzie "tutaj" 
            // ustawienie stanu braku aktywnoœci dla kontrolek z danymi wejœciowymi
            txtA.Enabled = false;
            txtB.Enabled = false;
            txtC.Enabled = false;
            txtX.Enabled = false;
            /* musimy przekazaæ informacjê, ¿e b³êdu nie by³o */
            return true;
        }

        float ObliczWartoœæRównaniaKwadratowego(float A, float B, float C, float X)
        {
            // obliczenie wartoœci równania i jej zwrotne przekazanie
            return ((A * X + B) * X + C);
        }

        private bool PobranieDanychWejœciowychDlaTablicowania(out float A, out float B, out float C,
            out float Xd, out float Xg, out float h)
        {
            /* pomocnicze przypisanie wartoœci dla parametrów wyjœciowych */
            A = B = C = Xd = Xg = h = 0.0F;

            // pobranie wspó³czynnika A
            if (!float.TryParse(txtA.Text, out A))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtA, "ERROR: w zapisie wartoœci wspó³czynnika 'a' " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // sprawdzenie warunku dla wspó³czynnika 'a': a != 0.0
            if (A == 0.0F)
            {
                // jest b³¹d, to go sygnalizujemy
                errorProvider1.SetError(txtA, "ERROR: wartoœæ wspó³czynnika 'a' musi byæ " +
                    "ró¿na od zera!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // pobranie wspó³czynnika B
            if (!float.TryParse(txtB.Text, out B))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtB, "ERROR: w zapisie wartoœci wspó³czynnika 'b' " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // pobranie wspó³czynnika C
            if (!float.TryParse(txtC.Text, out C))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtC, "ERROR: w zapisie wartoœci wspó³czynnika 'c' " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }

            // pobranie dolnej ganicy Xd
            if (!float.TryParse(txtXd.Text, out Xd))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtXd, "ERROR: w zapisie dolnej granicy Xd " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }

            // pobranie górnej granicy Xg
            if (!float.TryParse(txtXg.Text, out Xg))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtXg, "ERROR: w zapisie wartoœci wspó³czynnika 'Xg' " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // pobranie przyrostu (kroku) h
            if (!float.TryParse(txth.Text, out h))
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txth, "ERROR: w zapisie wartoœci przyrostu (kroku) h " +
                    "wyst¹pi³ niedozwolony znak!");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }

            /* 
            TUTAJ TRZEBA WSTAWIÆ SPRAWDZENIE WARUNKU DLA:
            Xd oraz Xg : Xd < Xg 
            h : 0 < h <= (Xg - Xd) / 2
            */
            if(Xd >= Xg)
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtXd, "ERROR: dolna granica Xd musi byæ mniejsza od górnej granicy Xg");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            if(h <= 0.0)
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txth, "ERROR: nie zosta³ spe³niony warunek h > 0");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }

            if (h > Math.Abs(Xg - Xd) / 2)
            {
                // by³ b³¹d, to go sygnalizujemy 
                errorProvider1.SetError(txtXg, "ERROR: nie zosta³ spe³niony warunek h <= (Xg - Xd) / 2");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }

            // gdy nie by³o ¿adnego b³êdu to sterowanie przejdzie "tutaj" 
            // ustawienie stanu braku aktywnoœci dla kontrolek z danymi wejœciowymi
            txtA.Enabled = false;
            txtB.Enabled = false;
            txtC.Enabled = false;
            txtXd.Enabled = false;
            txtXg.Enabled = false;
            txth.Enabled = false;
            /* musimy przekazaæ informacjê, ¿e b³êdu nie by³o */
            return true;
        }

        private void btnWizualizacjaTabelarycznaFx_Click(object sender, EventArgs e)
        {
            // 1. Zgaszenie kontrolki errorProvider
            errorProvider1.Dispose();
            // deklaracja zmiennych dla przechowania danych wejœciowych 
            float A, B, C, Xd, Xg, h;
            // 2. Pobranie danych wejœciowych 
            if (!PobranieDanychWejœciowychDlaTablicowania(out A, out B, out C, out Xd, out Xg, out h))
            {
                // by³ b³¹d, to go sygnalizuje 
                errorProvider1.SetError(btnWizualizacjaTabelarycznaFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " +
                    "niedozwolony znak.");
                // przerwanie obs³ugi zdarzenia Click: btnWizualizacjaTabelarycznaFx_Click
                return;
            }
            
            // ukrycie wizualizacji graficznej 
            chrtFx.Visible = false;
            // uaktywnienie przycisku wizualizacji graficznej, gdyby u¿ytkownik chcia³ przechodziæ pomiêdzy wizualizacjami
            btnWizualizacjaGraficznaFx.Enabled = true;
            
            /* 3. Tablicowanie wartoœci równania (funkcji F(X)) w przedziale [Xd, Xg] 
            z przyrostem 'h' */
            // deklaracja zmiennej tablicowej 
            float[,] TWFx;
            // wywo³anie metody tablicowania zmian wartoœci F(x) w podanym przedziale: [Xd, Xg] z 'h'
            TablicowanieWartoœciFx(A, B, C, Xd, Xg, h, out TWFx);

            // 4. Wpisanie do Kontrolki DataGridView wierszy danych tablicy zmian wartoœci F(X)
            // wywo³anie metody przepisania wierszy tablicy TWFx do kontrolki DataGridView
            WpiszWierszeDanychDoKontrolkiDataGridView(TWFx, ref dgvTWFx);
            // modyfikator ref oznacza, ¿e dany parametr metordy jest parametrem wejœciowo-wyjœciowym

            // 5. Ods³oniêcie kontrolki DataGridView
            dgvTWFx.Visible = true;

            // 6. Ustawienie stanu braku aktywnoœci dla obs³ugiwanego przycisku poleceñ btnWizualizacjaTabelarycznaFx
            btnWizualizacjaTabelarycznaFx.Enabled = false;

        }

        private void btnWizualizacjaGraficznaFx_Click(object sender, EventArgs e)
        {
            // deklaracja zmiennych dla przechowania danych wejœciowych 
            float A, B, C, Xd, Xg, h;
            // deklaracja zmiennej tablicowej 
            float[,] TBFx;
            // pobranie danych wejœciowych 
            if (!PobranieDanychWejœciowychDlaTablicowania(out A, out B, out C, out Xd, out Xg, out h))
            {
                // by³ b³¹d, to go sygnalizuje 
                errorProvider1.SetError(btnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " +
                    "niedozwolony znak.");
                // przerwanie obs³ugi zdarzenia Click: btnWizualizacjaTabelarycznaFx_Click
                return;
            }

            // ukrycie wizualizacji tabelarycznej 
            dgvTWFx.Visible = false;
            // czyszczenie wizualizacji graficznej
            chrtFx.Series.Clear();
            // odkrycie wizualizacji graficznej 
            chrtFx.Visible = true;
            // uaktywnienie przycisku wizualizacji tabelarycznej, gdyby u¿ytkownik chcia³ przechodziæ pomiêdzy wizualizacjami
            btnWizualizacjaTabelarycznaFx.Enabled = true;

            // dodanie krzywej do obszaru wykresu
            Series Krzywa = new Series("Wartoœæ równania")
            {
                ChartType = SeriesChartType.Line
            };
            chrtFx.Series.Add(Krzywa);

            // deklaracja zmiennych do przechowywania wartoœci równania
            float WartoœæRównania;
            // deklaracja i inicjalizacja Indeksu dla punktów
            int Index = 0;

            for (float X = Xd; X <= Xg; X += h)
            {
                WartoœæRównania = ObliczWartoœæRównaniaKwadratowego(A, B, C, X);
                Krzywa.Points.AddXY(X, WartoœæRównania);
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
            // odblokowanie przycisków
            btnWizualizacjaTabelarycznaFx.Enabled = true;
            btnObliczWartoœæFx.Enabled = true;
            btnWizualizacjaGraficznaFx.Enabled = true;
            // odblokowanie okienek z tekstem
            txtA.Enabled = true;
            txtB.Enabled = true;
            txtC.Enabled = true;
            txtX.Enabled = true;
            txtXd.Enabled = true;
            txtXg.Enabled = true;
            txth.Enabled = true;
            txtWartoœæRównania.Enabled = true;
            // wyzerowanie okienek z tekstem
            txtA.Clear();
            txtB.Clear();
            txtC.Clear();
            txtX.Clear();
            txtXd.Clear();
            txtXg.Clear();
            txth.Clear();
            txtWartoœæRównania.Clear();
        }

        private void zapiszWierszeDanychKontrolkiDataGridViewWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // wygaszenie kontrolki errorProvider
            errorProvider1.Dispose();
            // sprawdzenie, czy kontrolka DataGridView jest widoczna 
            if (dgvTWFx == null || !dgvTWFx.Visible || dgvTWFx.Rows.Count <= 0)
            {
                errorProvider1.SetError(btnWizualizacjaTabelarycznaFx, "ERROR: " +
                    " polecenie nie mo¿e byæ zrealizowane bo Kontrolka DataGridView jest " +
                    " niewidoczna lub pusta");
                // przerwanie dalszej obs³ugi 
                return;
            }
            // 1. utworzenie egzemplarza okna dialogowego: OknoPlikuDoZapisu
            SaveFileDialog OknoPlikuDoZapisu = new SaveFileDialog();
            // 2. ustawienie atrybutu okna dialogowego OknoPlikuDoZapisu
            OknoPlikuDoZapisu.Title = "Wybór pliku do wpisania wierszy danych z kontrolki " +
                "DataGridView";
            OknoPlikuDoZapisu.Filter = "txtfiles(*.txt)|*.txt|all files (*.*)|*.*";
            OknoPlikuDoZapisu.FilterIndex = 1;
            OknoPlikuDoZapisu.RestoreDirectory = true;
            OknoPlikuDoZapisu.InitialDirectory = "C:\\";
            // wizualizacja okna OknoPlikuDoZapisu i odczytanie informacji o wyborze pliku
            if (OknoPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                // plik zosta³ wybrany lub zosta³ utworzony nowy plik 
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
                catch (Exception B³¹d)
                {
                    MessageBox.Show("ERROR: wyst¹pi³ nieoczekiwany b³¹d podczas zapisu " +
                        "wierszy danych z kontrolki DataGridView (komunikat systemowy: " +
                        B³¹d.Message + " )");
                }
                finally
                {
                    PlikZnakowy.Close();
                    PlikZnakowy.Dispose();
                }
                // zmkniêcie okna dialogowego OknoPlikuDoZapisu 
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
                OknoPlikuDoOdczytu.Title = "Wybór pliku do wczytania wierszy danych do kontrolki " +
                    "DataGridView";
                OknoPlikuDoOdczytu.Filter = "txtfiles(*.txt)|*.txt|all files (*.*)|*.*";
                OknoPlikuDoOdczytu.FilterIndex = 1;
                OknoPlikuDoOdczytu.RestoreDirectory = true;
                OknoPlikuDoOdczytu.InitialDirectory = "C:\\";

                // wizualizacja okna OknoPlikuDoOdczytu i odczytanie informacji o wyborze pliku
                if (OknoPlikuDoOdczytu.ShowDialog() == DialogResult.OK)
                {
                    // plik zosta³ wybrany 
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
                    catch (Exception B³¹d)
                    {
                        MessageBox.Show("ERROR: wyst¹pi³ nieoczekiwany b³¹d podczas odczytu " +
                            "wierszy danych z pliku (komunikat systemowy: " +
                            B³¹d.Message + " )");
                    }
                    finally
                    {
                        PlikZnakowy.Close();
                        PlikZnakowy.Dispose();
                    }
                    // zmkniêcie okna dialogowego OknoPlikuDoOdczytu 
                    OknoPlikuDoOdczytu.Dispose();

                    // 5. Ods³oniêcie kontrolki DataGridView
                    dgvTWFx.Visible = true;

                    // 6. Ustawienie stanu braku aktywnoœci dla obs³ugiwanego przycisku poleceñ btnWizualizacjaTabelarycznaFx
                    btnWizualizacjaTabelarycznaFx.Enabled = false;
                }
            }
        }
    }
#endregion

