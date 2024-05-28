using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Projekt2_Janusz70130
{
    public partial class AnalizatorIndywidualny : Form
    {
        public AnalizatorIndywidualny()
        {
            InitializeComponent();
        }

        private void bjBtnObliczWartoœæFx_Click(object sender, EventArgs e)
        {
            // Pobranie zmiennej X
            if (!bjSpróbujPobraæFloatIObs³ó¿B³¹d(bjTxtX.Text, out float bjX, bjTxtX, "X"))
            {
                return;
            }
            // zgszenie kontrolki errorProvider
            errorProvider2.Dispose();
            // obliczenie wartoœci funkcji
            float bjWartoœæFunkcji = bjObliczWartoœæFunkcji(bjX);
            // wizualizacja wyniku obliczeñ 
            bjTxtFX.Text = String.Format("{0:F3}", bjWartoœæFunkcji);
            // ustawienie stanu braku aktywnoœci dla obs³ugiwanego przycisku poleceñ
            bjBtnObliczWartoœæFx.Enabled = false;
            // ustawienie stanu braku aktywnoœci dla pola tekstowego
            bjTxtFX.Enabled = false;
            // zablokowanie przycisku X 
            bjTxtX.Enabled = false;
        }


        private bool bjSpróbujPobraæFloatIObs³ó¿B³¹d(string bjText, out float bjLiczbaFloat, Control bjKontrolka, string bjNazwaWspó³czynnika)
        {
            if (!float.TryParse(bjText, out bjLiczbaFloat))
            {
                errorProvider2.SetError(bjKontrolka, $"ERROR: w zapisie wartoœci wspó³czynnika '{bjNazwaWspó³czynnika}' wyst¹pi³ niedozwolony znak!");
                return false;
            }
            return true;
        }


        #region funkcje pomocnicze analizatora indywidualnego
        private float bjObliczWartoœæFunkcji(float bjX)
        {
            if (bjX <= -1)
            {
                return (float)(Math.Exp(Math.Abs(bjX)) * Math.Sin(bjX));
            }
            else if  (bjX > -1 && bjX < 1) 
            {
                return 1 + bjX + (bjX * bjX) / 2;
            } else
                return (float)(Math.Sin(1 + bjX + (Math.Pow(bjX, 2) / 2)) * Math.Log(bjX));
        }

        private bool bjPobranieDanychWejœciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH)
        {
            /* pomocnicze przypisanie wartoœci dla parametrów wyjœciowych */
            bjXd = bjXg = bjH = 0.0F;

            // Pobranie wspó³czynnika bjXd
            if (!bjSpróbujPobraæFloatIObs³ó¿B³¹d(bjTxtXd.Text, out bjXd, bjTxtXd, "Xd"))
            {
                return false;
            }

            // Pobranie wspó³czynnika bjXg
            if (!bjSpróbujPobraæFloatIObs³ó¿B³¹d(bjTxtXg.Text, out bjXg, bjTxtXg, "Xg"))
            {
                return false;
            }

            // Pobranie wspó³czynnika bjH
            if (!bjSpróbujPobraæFloatIObs³ó¿B³¹d(bjTxtH.Text, out bjH, bjTxtH, "H"))
            {
                return false;
            }

            /* 
            SPRAWDZENIE WARUNKU DLA:
            Xd oraz Xg : Xd < Xg 
            RAZ
            h : 0 < h <= (Xg - Xd) / 2
            */

            if (bjXd >= bjXg)
            {
                // sygnalizujemy b³¹d
                errorProvider2.SetError(bjTxtXd, "ERROR: dolna granica Xd musi byæ mniejsza od górnej granicy Xg");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            if (bjH <= 0.0)
            {
                // sygnalizujemy b³¹d
                errorProvider2.SetError(bjTxtH, "ERROR: nie zosta³ spe³niony warunek h > 0");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }

            if (bjH > Math.Abs(bjXg - bjXd) / 2)
            {
                // sygnalizujemy b³¹d
                errorProvider2.SetError(bjTxtH, "ERROR: nie zosta³ spe³niony warunek h <= (Xg - Xd) / 2");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // gdy nie by³o ¿adnego b³êdu to sterowanie przejdzie "tutaj" 
            // ustawienie stanu braku aktywnoœci dla kontrolek z danymi wejœciowymi
            bjTxtXd.Enabled = false;
            bjTxtXg.Enabled = false;
            bjTxtH.Enabled = false;
            /* musimy przekazaæ informacjê, ¿e b³êdu nie by³o */
            return true;
        }

        private void bjTablicowanieWartoœciFunkcjiFx(float bjXd, float bjXg, float bjH, out float[,] bjTWFx)
        {
            // wyznaczenie liczby podprzedzia³ów przedzia³u [Xd, Xg] z krokiem h
            int bjN = (int)((bjXg - bjXd) / bjH) + 1;
            // +1, gdy¿ wynik dzielenia ((Xg-Xd)/h) bêdzie liczb¹ u³amkow¹
            // i powinniœmy go zaokr¹gliæ do najbli¿szej liczby ca³kowitej "w górê" 

            // utworzenie egzemplarza tablicy bjTWFx
            bjTWFx = new float[bjN, 3];
            // deklaracje pomocnicze
            float bjX; // zmienna niezale¿na X
            int bjI; // numer w podprzedziale w przedziale [Xd, Xg]
            for (bjX = bjXd, bjI = 0; bjI < bjTWFx.GetLength(0); bjI++, bjX = bjXd + bjI * bjH)
            {
                bjTWFx[bjI, 0] = bjI;
                bjTWFx[bjI, 1] = bjX;
                bjTWFx[bjI, 2] = bjObliczWartoœæFunkcji(bjX);
            }
        }

        private void bjWpiszWierszeDanychDoKontrolkiDataGridView(float[,] bjTWFx, ref DataGridView bjDgvTWFx)
        {
            /* przepisanie wierszy danych z tablicy bjTWFx do kontrolki DataGridView 
             * (najpierw dodajemy nowy wiersz, a nastêpnie do dodanego wiersza wpisujemy dane)*/
            for (int bjI = 0; bjI < bjTWFx.GetLength(0); bjI++)
            {
                // dodanie nowego wiersza
                bjDgvTWFx.Rows.Add(bjTWFx);
                // wpisanie wartoœci numeru przedzia³u X
                bjDgvTWFx.Rows[bjI].Cells[0].Value = String.Format("{0}", bjTWFx[bjI, 0]);
                // wpisanie wartoœci zmiennej niezale¿nej X
                bjDgvTWFx.Rows[bjI].Cells[1].Value = String.Format("{0:F3}", bjTWFx[bjI, 1]);
                // wpisanie wartoœci równania
                bjDgvTWFx.Rows[bjI].Cells[2].Value = String.Format("{0:F3}", bjTWFx[bjI, 2]);
            }
        }

        private void bjWpiszWierszeDanychDoKontrolkiChart(float[,] bjTWFx, ref Chart bjChrt)
        {

            // dodanie krzywej do obszaru wykresu
            Series bjKrzywa = new Series("Wartoœæ funkcji F(X)")
            {
                ChartType = SeriesChartType.Line
            };
            bjChrt.Series.Add(bjKrzywa);

            for (int bjI = 0; bjI < bjTWFx.GetLength(0); bjI++)
            {
                // dodawanie do krzywej punktów z tablicy 
                bjKrzywa.Points.AddXY(bjTWFx[bjI, 1], bjTWFx[bjI, 2]);
            }
        }
                #endregion

        private void bjBtnWizualizacjaTabelarycznaFx_Click(object sender, EventArgs e)
        {
            // 1. zgaszenie kontrolki errorProvider
            errorProvider2.Dispose();
            // 2. Pobranie danych wejœciowych 
            if (!bjPobranieDanychWejœciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH))
            {
                // by³ b³¹d, to go sygnalizuje 
                errorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " +
                    "niedozwolony znak.");
                // przerwanie obs³ugi zdarzenia Click: btnWizualizacjaTabelarycznaFx_Click
                return;
            }

            // 3. Po poprawnym za³adowaniu dnaych ustawienie stanu kontrolek
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

            /* 4.Tablicowanie wartoœci równania(funkcji F(X)) w przedziale[Xd, Xg]
            z przyrostem 'h' */
            // wywo³anie metody tablicowania zmian wartoœci F(x) w podanym przedziale: [Xd, Xg] z 'h'
            bjTablicowanieWartoœciFunkcjiFx(bjXd, bjXg, bjH, out float[,] bjTWFx);
            // 5. Wpisanie do Kontrolki DataGridView wierszy danych tablicy zmian wartoœci F(X)
            // wywo³anie metody przepisania wierszy tablicy TWFx do kontrolki DataGridView
            bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
            // modyfikator ref oznacza, ¿e dany parametr metordy jest parametrem wejœciowo-wyjœciowym
        }

        private void bjBtnWizualizacjaGraficznaFx_Click(object sender, EventArgs e)
        {

            // 1. Zgaszenie kontrolki errorProvider
            errorProvider2.Dispose();
            // 2. Pobranie danych wejœciowych 
            if (!bjPobranieDanychWejœciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH))
            {
                // by³ b³¹d, to go sygnalizuje 
                errorProvider2.SetError(bjBtnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " +
                    "niedozwolony znak.");
                // przerwanie obs³ugi zdarzenia Click: btnWizualizacjaGraficznaFx_Click
                return;
            }
            // 3. Po poprawnym za³adowaniu dnaych ustawienie odpowedniego stanu kontrolek
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
            // 4.Tablicowanie wartoœci równania(funkcji F(X)) w przedziale[Xd, Xg] z przyrostem 'h' */
            // wywo³anie metody tablicowania 
            bjTablicowanieWartoœciFunkcjiFx(bjXd, bjXg, bjH, out float[,] bjTWFx);
            // 5. Wpisanie do kontrolki Chart wierszy danych tablicy zmian wartoœci F(X)
            // wywo³anie metody przepisania wierszy tablicy TWFx do kontrolki Chart
            bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
            // modyfikator ref oznacza, ¿e dany parametr metordy jest parametrem wejœciowo-wyjœciowym
        }

        private void bjBtnReset_Click(object sender, EventArgs e)
        {
            errorProvider2.Clear();
            // ukrycie wizualizacji 
            bjDgvTWFx.Visible = false;
            bjChrt.Visible = false;
            // czyszczenie wizualizacji
            bjDgvTWFx.Rows.Clear();
            bjChrt.Series.Clear();
            // odblokowanie przycisków
            bjBtnObliczWartoœæFx.Enabled = true;
            bjBtnWizualizacjaGraficznaFx.Enabled = true;
            bjBtnWizualizacjaTabelarycznaFx.Enabled = true;
            // odblokowanie i czyszczenie okienek z tekstem
            List<TextBox> bjTextBoxy = new List<TextBox>
            {
                bjTxtFX,
                bjTxtX,
                bjTxtXd,
                bjTxtXg,
                bjTxtH
            };
            foreach (TextBox bjTB in bjTextBoxy)
            {
                bjTB.Enabled = true;
                bjTB.Clear();
            }
        }

        private void bjAnalizatorIndywidualnyForm_Closing(object sender, FormClosingEventArgs e)
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

        private void AnalizatorIndywidualny_Load(object sender, EventArgs e)
        {

        }

        private void zapiszWierszeDanychKontrolkiDataGridViewWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // wygaszenie kontrolki errorProvider
            errorProvider2.Dispose();
           /* // sprawdzenie, czy kontrolka DataGridView jest widoczna 
            if (dgvTWFx == null || !dgvTWFx.Visible || dgvTWFx.Rows.Count <= 0)
            {
                errorProvider1.SetError(btnWizualizacjaTabelarycznaFx, "ERROR: " +
                    " polecenie nie mo¿e byæ zrealizowane bo Kontrolka DataGridView jest " +
                    " niewidoczna lub pusta");
                // przerwanie dalszej obs³ugi zdarzenia Click
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
                        // wpisanie do pliku i-tego wiersza kontrolki DataGridView
                        PlikZnakowy.Write(dgvTWFx.Rows[i].Cells[0].Value);
                        // wpisanie separatora danych 
                        PlikZnakowy.Write(";");
                        PlikZnakowy.Write(dgvTWFx.Rows[i].Cells[1].Value);
                        // wpisanie separatora danych 
                        PlikZnakowy.Write(";");
                        PlikZnakowy.WriteLine(dgvTWFx.Rows[i].Cells[2].Value);
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
            }*/
        }
    }
}
