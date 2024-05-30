using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Projekt2_Janusz70130
{
    public partial class AnalizatorIndywidualny : Form
    {
        // tablica wartoœci
        private float[,] bjTWFx;
        // parametry aplikacji
        private float bjXd;
        private float bjXg;
        private float bjH;
        // pomocnicza flaga aktualnoœci danych s³u¿¹ca do œledzenia stanu aplikacji i spójnoœci danych
        private bool bjFlagaAktualnosciDanych;
        // zmienna s³u¿¹ca do œledzenia czy u¿ytkownik chce wyjœæ z aplikacji bezpoœrednio pomiajaj¹c wychodzenie do kokpitu
        public static bool bjCzyZamknac { get; set; } = false;

        public AnalizatorIndywidualny()
        {
            InitializeComponent();
            // Wy³¹czenie dodatkowego wiersza w kontrolce DGataGridView
            bjDgvTWFx.AllowUserToAddRows = false;

            // Dodajemy opcje do menu zmiany formatu czcionki
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Domyœlna (Microsoft Sans Serif)", null, bjZmianaFormatuCzcionki_Click); // Dodana czcionka Tahoma
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Arial", null, bjZmianaFormatuCzcionki_Click);
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Times New Roman", null, bjZmianaFormatuCzcionki_Click);
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Verdana", null, bjZmianaFormatuCzcionki_Click); // Dodana czcionka Verdana
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Tahoma", null, bjZmianaFormatuCzcionki_Click); // Dodana czcionka Tahoma

            // Dodajemy opcje do menu zmiany koloru czcionki
            bjZmianaKoloruCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Czarny", null, bjZmianaKoloruCzcionki_Click);
            bjZmianaKoloruCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruCzcionki_Click);
            bjZmianaKoloruCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruCzcionki_Click);
            bjZmianaKoloruCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruCzcionki_Click);

            // Dodajemy opcje do menu zmiany koloru siatki
            bjZmianaKoloruSiatKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Czarny", null, bjZmianaKoloruSiatki_Click);
            bjZmianaKoloruSiatKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruSiatki_Click);
            bjZmianaKoloruSiatKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruSiatki_Click);
            bjZmianaKoloruSiatKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruSiatki_Click);

            // Dodajemy opcje zmiany koloru t³a kontrolki Chart
            zmianaKoloruT³aWykresuToolStripMenuItem.DropDownItems.Add("Bia³y", null, bjZmianaKoloruT³aWykresuToolStripMenuItem_Click);
            zmianaKoloruT³aWykresuToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruT³aWykresuToolStripMenuItem_Click);
            zmianaKoloruT³aWykresuToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruT³aWykresuToolStripMenuItem_Click);
            zmianaKoloruT³aWykresuToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruT³aWykresuToolStripMenuItem_Click);

            // Dodajemy opcje zmiany koloru linii wykresu
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Bia³y", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click);
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click);
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click);
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click);

            // Dodajemy opcje zmiany czcionki wykresu
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Domyœlna (Microsoft Sans Serif)", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Arial", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Times New Roman", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Verdana", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Tahoma", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);

            // Dodanie opcji zmiany stylu linii wykresu
            zmianaStyluLiniiWykresuToolStripMenuItem.DropDownItems.Add("Ci¹g³a", null, bjZmianaStyluLiniiWykresuToolStripMenuItem_Click);
            zmianaStyluLiniiWykresuToolStripMenuItem.DropDownItems.Add("Kreskowana", null, bjZmianaStyluLiniiWykresuToolStripMenuItem_Click);
            zmianaStyluLiniiWykresuToolStripMenuItem.DropDownItems.Add("Kropkowana", null, bjZmianaStyluLiniiWykresuToolStripMenuItem_Click);

            // Dodanie opcji zmiany typu wykresu
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Liniowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Kolumnowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("S³upkowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Punktowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Obszarowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);

            // Dodanie opcji usuniêcia wykresu
            bjUsuniêcieWykresuBitMapyToolStripMenuItem.Click += bjUsuniêcieWykresuBitMapyToolStripMenuItem_Click;

        }


        #region funkcje pomocnicze analizatora indywidualnego
        private void bjBtnObliczWartoœæFx_Click(object sender, EventArgs e)
        {
            // Pobranie zmiennej X
            if (!bjSpróbujPobraæFloatIObs³ó¿bjB³¹d(bjTxtX.Text, out float bjX, bjTxtX, "X"))
            {
                return;
            }
            // zgszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            // obliczenie wartoœci funkcji
            float bjWartoœæFunkcji = bjObliczWartoœæFunkcji(bjX);
            // wizualizacja wyniku obliczeñ 
            bjTxtFX.Text = String.Format("{0:F2}", bjWartoœæFunkcji);
            // ustawienie stanu braku aktywnoœci dla obs³ugiwanego przycisku poleceñ
            bjBtnObliczWartoœæFx.Enabled = false;
            // ustawienie stanu braku aktywnoœci dla pola tekstowego
            bjTxtFX.Enabled = false;
            // zablokowanie przycisku X 
            bjTxtX.Enabled = false;
        }

        private bool bjSpróbujPobraæFloatIObs³ó¿bjB³¹d(string bjText, out float bjLiczbaFloat, Control bjKontrolka, string bjNazwaWspó³czynnika)
        {
            if (!float.TryParse(bjText, out bjLiczbaFloat))
            {
                bjErrorProvider2.SetError(bjKontrolka, $"ERROR: w zapisie wartoœci wspó³czynnika '{bjNazwaWspó³czynnika}' wyst¹pi³ niedozwolony znak!");
                return false;
            }
            return true;
        }

        private float bjObliczWartoœæFunkcji(float bjX)
        {
            if (bjX <= -1)
            {
                return (float)(Math.Exp(Math.Abs(bjX)) * Math.Sin(bjX));
            }
            else if (bjX > -1 && bjX < 1)
            {
                return 1 + bjX + (bjX * bjX) / 2;
            }
            else
                return (float)(Math.Sin(1 + bjX + (Math.Pow(bjX, 2) / 2)) * Math.Log(bjX));
        }

        private bool bjPobranieDanychWejœciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH)
        {
            /* pomocnicze przypisanie wartoœci dla parametrów wyjœciowych */
            bjXd = bjXg = bjH = 0.0F;

            // Pobranie wspó³czynnika bjXd
            if (!bjSpróbujPobraæFloatIObs³ó¿bjB³¹d(bjTxtXd.Text, out bjXd, bjTxtXd, "Xd"))
            {
                return false;
            }

            // Pobranie wspó³czynnika bjXg
            if (!bjSpróbujPobraæFloatIObs³ó¿bjB³¹d(bjTxtXg.Text, out bjXg, bjTxtXg, "Xg"))
            {
                return false;
            }

            // Pobranie wspó³czynnika bjH
            if (!bjSpróbujPobraæFloatIObs³ó¿bjB³¹d(bjTxtH.Text, out bjH, bjTxtH, "H"))
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
                bjErrorProvider2.SetError(bjTxtXd, "ERROR: dolna granica Xd musi byæ mniejsza od górnej granicy Xg");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            if (bjH <= 0.0)
            {
                // sygnalizujemy b³¹d
                bjErrorProvider2.SetError(bjTxtH, "ERROR: nie zosta³ spe³niony warunek h > 0");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }

            if (bjH > Math.Abs(bjXg - bjXd) / 2)
            {
                // sygnalizujemy b³¹d
                bjErrorProvider2.SetError(bjTxtH, "ERROR: nie zosta³ spe³niony warunek h <= (Xg - Xd) / 2");
                // zwrotne przekazanie informacji, ¿e zaistnia³ b³¹d 
                return false;
            }
            // gdy nie by³o ¿adnego b³êdu to sterowanie przejdzie "tutaj" 
            // ustawienie stanu braku aktywnoœci dla kontrolek z danymi wejœciowymi
            bjZablokujPolaTekstowe();
            /* musimy przekazaæ informacjê, ¿e b³êdu nie by³o */
            return true;
        }

        private void bjZablokujPolaTekstowe()
        {
            bjTxtXd.Enabled = false;
            bjTxtXg.Enabled = false;
            bjTxtH.Enabled = false;
        }

        private void bjTablicowanieWartoœciFunkcjiFx(float bjXd, float bjXg, float bjH)
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
                bjDgvTWFx.Rows[bjI].Cells[1].Value = String.Format("{0:F2}", bjTWFx[bjI, 1]);
                // wpisanie wartoœci równania
                bjDgvTWFx.Rows[bjI].Cells[2].Value = String.Format("{0:F2}", bjTWFx[bjI, 2]);
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
        

        private void bjBtnWizualizacjaTabelarycznaFx_Click(object sender, EventArgs e)
        {
            // zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // 1. Je¿eli flaga nie jest ustawiona, wystarczy odkryæ kontrolkê DGV i ukryæ inne,
            // je¿eli jest to trzeba podj¹æ kroki do wizualizacji tabelarycznej
            if (!bjFlagaAktualnosciDanych)
            {
                // 2. Pobranie danych z pól tekstowych
                if (!bjPobranieDanychWejœciowychDlaTablicowania(out bjXd, out bjXg, out bjH))
                {
                    // by³ b³¹d, to go sygnalizuje 
                    bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " +
                        "niedozwolony znak.");
                    // przerwanie obs³ugi zdarzenia Click: bjBtnWizualizacjaTabelarycznaFx_Click
                    return;
                }
                // 3. Tablicowanie wartoœæi 
                bjTablicowanieWartoœciFunkcjiFx(bjXd, bjXg, bjH);
                // 4. Wype³nienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 5. Wype³nienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualnoœæi danych
                bjFlagaAktualnosciDanych = true;
            }
            // 7. Odkrycie kontrolki Chart i zakrycie DataGridView
            bjDgvTWFx.Visible = true;
            bjChrt.Visible = false;

            // pobranie danych wejœciowych 
            /*if (!bjPobranieDanychWejœciowychDlaTablicowania(out bjXd, out bjXg, out bjH))
            {
                // by³ b³¹d, to go sygnalizuje 
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " +
                    "niedozwolony znak.");
                // przerwanie obs³ugi zdarzenia Click: bjBtnWizualizacjaTabelarycznaFx_Click
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
            //bjTablicowanieWartoœciFunkcjiFx(bjXd, bjXg, bjH);
            // 5. Wpisanie do Kontrolki DataGridView wierszy danych tablicy zmian wartoœci F(X)
            // wywo³anie metody przepisania wierszy tablicy TWFx do kontrolki DataGridView
            //bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
            // modyfikator ref oznacza, ¿e dany parametr metordy jest parametrem wejœciowo-wyjœciowym
        }

        private void bjBtnWizualizacjaGraficznaFx_Click(object sender, EventArgs e)
        {
            // zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // 1. Je¿eli flaga nie jest ustawiona, wystarczy odkryæ kontrolkê Chart i ukryæ inne,
            // je¿eli jest to trzeba podj¹æ kroki do wizualizacji graficznej
            if (!bjFlagaAktualnosciDanych)
            {
                // 2. Pobranie danych z pól tekstowych
                if (!bjPobranieDanychWejœciowychDlaTablicowania(out bjXd, out bjXg, out bjH))
                {
                    // sygnalizowanie b³êdu
                    bjErrorProvider2.SetError(bjBtnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " +
                        "niedozwolony znak.");
                    // przerwanie obs³ugi zdarzenia Click: btnWizualizacjaGraficznaFx_Click
                    return;
                }
                // 3. Tablicowanie wartoœæi
                bjTablicowanieWartoœciFunkcjiFx(bjXd, bjXg, bjH);
                // 4. Wype³nienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 5. Wype³nienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualnoœæi danych
                bjFlagaAktualnosciDanych = true;
                
            }
            // 7. Odkrycie kontrolki Chart i zakrycie DataGridView
            bjDgvTWFx.Visible = false;
            bjChrt.Visible = true;

            /*
            // zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            // deklaracja zmiannych pomocniczych 
            float bjXd, bjXg, bjH;
            // pobranie danych wejœciowych 
            if (!bjPobranieDanychWejœciowychDlaTablicowania(out bjXd, out bjXg, out bjH))
            {
                // by³ b³¹d, to go sygnalizuje 
                bjErrorProvider2.SetError(bjBtnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " +
                    "niedozwolony znak.");
                // przerwanie obs³ugi zdarzenia Click: btnWizualizacjaGraficznaFx_Click
                return;
            }

            // zresetowanie kontrolki
            bjResetChart();
            
            // po poprawnym za³adowaniu dnaych ustawienie odpowedniego stanu kontrolek
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
            // tablicowanie wartoœci równania(funkcji F(X)) w przedziale[Xd, Xg] z przyrostem 'h' */
            // wywo³anie metody tablicowania 
            //bjTablicowanieWartoœciFunkcjiFx(bjXd, bjXg, bjH);
            // wpisanie do kontrolki Chart wierszy danych tablicy zmian wartoœci F(X)
            // wywo³anie metody przepisania wierszy tablicy TWFx do kontrolki Chart
            //bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
            // modyfikator ref oznacza, ¿e dany parametr metordy jest parametrem wejœciowo-wyjœciowym

        }

        private void bjBtnReset_Click(object sender, EventArgs e)
        {
            bjErrorProvider2.Clear();
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
            bjWyczyscPolaTekstowe();
        }

        private void bjAnalizatorIndywidualnyForm_Closing(object sender, FormClosingEventArgs e)
        {
            // sprawdzenie czy u¿ytkownik chce obejœæ wychodznie z aplikacji przez kokpit
            if (!bjCzyZamknac)
            {
                // Normalne zachowanie zamykania formularza
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
            // u¿ytkonik chce omin¹æ zamykanie aplikacji przez wychodzenie z kokpitu,
            // wyœwietla sie tylko okienko u¿yte w funkcji bjZakoñczDzia³anieProgramuToolStripMenuItem_Click
            else
            {
                // Pominiêcie dodatkowych dzia³añ przy zamykaniu
                e.Cancel = false;
            }
        }

        private void zapiszWierszeDanychKontrolkiDataGridViewDoPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // wygaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            /*
            if (bjDgvTWFx == null || !bjDgvTWFx.Visible || bjDgvTWFx.Rows.Count <= 0)
            {
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: " +
                    " polecenie nie mo¿e byæ zrealizowane bo Kontrolka DataGridView jest " +
                    " niewidoczna lub pusta");
                // przerwanie dalszej obs³ugi zdarzenia Click
                return;
            }
            */
            // utworzenie egzemplarza okna dialogowego: bjOknoPlikuDoZapisu
            
            // Utworzenie egzemplarza okna dialogowego do zapisu pliku
            SaveFileDialog bjOknoPlikuDoZapisu = new SaveFileDialog
            {
                Title = "Wybór pliku do zapisania danych z kontrolki DataGridView",
                Filter = "txt files (*.txt)|*.txt|all files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };

            // Wizualizacja okna dialogowego i odczytanie informacji o wyborze pliku
            if (bjOknoPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                // Zapisanie zawartoœci DataGridView do wybranego pliku
                bjZapiszDaneKontrolkiDataGridViewDoPliku(bjOknoPlikuDoZapisu.FileName);
            }
        }

        private void bjZapiszDaneKontrolkiDataGridViewDoPliku(string bjNazwaPliku)
        {
            // Utworzenie egzemplarza StreamWriter do zapisu wierszy DataGridView do pliku
            using (StreamWriter bjPlikZnakowy = new StreamWriter(bjNazwaPliku))
            {
                try
                {
                    // Zapisanie ka¿dego wiersza z DataGridView do pliku
                    for (int i = 0; i < bjDgvTWFx.Rows.Count; i++)
                    {
                        bjPlikZnakowy.Write(bjDgvTWFx.Rows[i].Cells[0].Value); // Zapisanie wartoœci z pierwszej komórki
                        bjPlikZnakowy.Write(";"); // Separator
                        bjPlikZnakowy.Write(bjDgvTWFx.Rows[i].Cells[1].Value); // Zapisanie wartoœci z drugiej komórki
                        bjPlikZnakowy.Write(";"); // Separator
                        bjPlikZnakowy.WriteLine(bjDgvTWFx.Rows[i].Cells[2].Value); // Zapisanie wartoœci z trzeciej komórki i przejœcie do nowej linii
                    }
                }
                catch (Exception bjB³¹d)
                {
                    // Wyœwietlenie komunikatu o b³êdzie w przypadku wyst¹pienia wyj¹tku
                    MessageBox.Show("ERROR: wyst¹pi³ nieoczekiwany b³¹d podczas zapisu " +
                        "wierszy danych z kontrolki DataGridView (komunikat systemowy: " +
                        bjB³¹d.Message + " )");
                }
            }
        }
        private void pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // Utworzenie okna dialogowego dla otwarcia (wskazania) pliku, z którego zostan¹ pobrane dane
            OpenFileDialog bjOknoWyboruPliku = new OpenFileDialog
            {
                Title = "Wybór pliku do pobrania wierszy danych dla kontrolki DataGridView",
                Filter = "txt files (*.txt)|*.txt|all files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };

            // 1. Wczytanie danych tekstowych z pliku do tablicy
            bjWczytajDaneTekstoweDoTablicy(bjOknoWyboruPliku, "");
            // 2. Aktualizacja pól tekstowych dla podg¹du u¿ytkownika
            bjAktualizacjaZmiennychDlaPolTekstowych();
            // 3. Wype³nienie Kontrolki Chart danymi
            bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
            // 4. Wype³nienie Kontrolki DataGridView danymi
            bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
            // 6. Ustawienie flagi aktualnoœæi danych
            bjFlagaAktualnosciDanych = true;
            // 7. Odkrycie kontrolki DataGridView i zakrycie Chart
            bjDgvTWFx.Visible = true;
            bjChrt.Visible = false;
        }

        private static void bjWczytajDaneTekstoweDoTablicy(OpenFileDialog bjOknoWyboruPliku, string bjSuffix)
        {
            if (bjOknoWyboruPliku.ShowDialog() == DialogResult.OK)
            {
                // Plik zosta³ wybrany, otwarcie go w trybie strumieni znaków
                string bjWierszDanych; // Przechowanie wiersza danych wczytanych z pliku znakowego
                string[] bjElementyWierszaDanych; // Przechowanie pojedynczych danych z wczytanego wiersza danych
                int liczbaWierszy = File.ReadAllLines(bjOknoWyboruPliku.FileName).Length;
                double[,] bjTWFx = new double[liczbaWierszy, 3]; // Inicjalizacja tablicy

                // Utworzenie i otwarcie strumienia znaków do odczytu
                using (StreamReader bjPlikZnakowy = new StreamReader(bjOknoWyboruPliku.FileName + bjSuffix))
                {
                    try
                    {
                        int NrWiersza = 0; // Ustalenie warunku brzegowego

                        // Wczytywanie wierszy z pliku znakowego a¿ do 'znacznika' koñca pliku
                        while (!bjPlikZnakowy.EndOfStream)
                        {
                            // Wczytywanie wiersza (linii) z pliku znakowego
                            bjWierszDanych = bjPlikZnakowy.ReadLine();

                            // "Rozpakowanie" (podzia³) pobranego wiersza tekstowego na czêœci oddzielane separatorem ';'
                            bjElementyWierszaDanych = bjWierszDanych.Split(new char[] { ';', ':', '|' });

                            // Usuniêcie ewentualnych spacji w poszczególnych wierszach tablicy bjElementyWierszaDanych
                            bjElementyWierszaDanych[0] = bjElementyWierszaDanych[0].Trim();
                            bjElementyWierszaDanych[1] = bjElementyWierszaDanych[1].Trim();
                            bjElementyWierszaDanych[2] = bjElementyWierszaDanych[2].Trim();

                            // Przypisanie wartoœci do tablicy bjTWFx
                            bjTWFx[NrWiersza, 0] = double.Parse(bjElementyWierszaDanych[0]);
                            bjTWFx[NrWiersza, 1] = double.Parse(bjElementyWierszaDanych[1]);
                            bjTWFx[NrWiersza, 2] = double.Parse(bjElementyWierszaDanych[2]);

                            NrWiersza++; // Zwiêkszenie licznika wierszy wpisanych do tablicy
                        }
                    }
                    catch (Exception bjB³¹d)
                    {
                        // Wyœwietlenie komunikatu o b³êdzie w przypadku wyst¹pienia wyj¹tku
                        MessageBox.Show("ERROR: wyst¹pi³ nieoczekiwany b³¹d podczas odczytu " +
                            "wierszy danych z pliku (komunikat systemowy: " +
                            bjB³¹d.Message + " )");
                    }
                }
            }
        }

        private void bjAktualizacjaZmiennychDlaPolTekstowych()
        {
            {
                // Sprawdzenie, czy DataGridView zawiera co najmniej jeden wiersz
                if (bjDgvTWFx.Rows.Count > 0)
                {
                    // Pobranie wartoœci z pierwszego wiersza, kolumna 1 (indeks 0)
                    var firstRowValue = bjDgvTWFx.Rows[0].Cells[1].Value;
                    bjXd = Convert.ToSingle(firstRowValue);
              

                    // Pobranie wartoœci z ostatniego wiersza, kolumna 1 (indeks 0)
                    int lastRowIndex = bjDgvTWFx.Rows.Count - 1;
                    var lastRowValue = bjDgvTWFx.Rows[lastRowIndex].Cells[1].Value;
                    bjXg = Convert.ToSingle(lastRowValue);

                    // Obliczenie ró¿nicy miêdzy wartoœci¹ pierwszego a nastêpnego wiersza
                    if (bjDgvTWFx.Rows.Count > 1)
                    {
                        // Pobranie wartoœci z drugiego wiersza, kolumna 1 (indeks 0)
                        var nextRowValue = bjDgvTWFx.Rows[1].Cells[1].Value;
                        float nextValue = Convert.ToSingle(nextRowValue);

                        // Obliczenie ró¿nicy miêdzy wartoœci¹ pierwszego a nastêpnego wiersza
                        bjH = Math.Abs(nextValue - bjXd);
                    }
                    else
                    {
                        // Jeœli DataGridView zawiera tylko jeden wiersz, ustawienie bjH na 0
                        bjH = 0;
                    }

                    bjTxtXd.Text = bjXd.ToString();
                    bjTxtXg.Text = bjXg.ToString();
                    bjTxtH.Text = bjH.ToString();

                }
                else
                {
                    // Obs³uga przypadku, gdy DataGridView jest puste
                    MessageBox.Show("DataGridView is empty. Cannot retrieve values.");
                }
            }
        }

        private void bjWyczyscPolaTekstowe()
        {
            bjTxtFX.Clear();
            bjTxtX.Clear();
            bjTxtXd.Clear();
            bjTxtXg.Clear();
            bjTxtH.Clear();

            bjTxtFX.Enabled = true;
            bjTxtX.Enabled = true;
            bjTxtXd.Enabled = true;
            bjTxtXg.Enabled = true;
            bjTxtH.Enabled = true;
        }

        private void usuñWierszeDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // sprawdzenie widocznoœci kontrolki DataGridView
            if (!bjDgvTWFx.Visible)
            {
                // jest b³¹d
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: " +
                    "kontrolka DataGridView nie zosta³a ods³oniêta");
                // przerwanie obs³ugi zdarzenia "Click"
                return;
            }
            DialogResult bjOknoMessage = MessageBox.Show("UWAGA: w kontrolce s¹ dane. Czy na pewno chcesz je utraciæ?",
                this.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            // rozpoznanie reakcji u¿ytkownika
            if (bjOknoMessage == DialogResult.No)
            {
                MessageBox.Show("KOMUNIKAT: polecenie pobrania danych z pliku zosta³o anulowane",
                    this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // przerywamy dalsz¹ obs³ugê zdarzenia Click
                return;
            }
            // usuniêcie wierszy danych w kolekcji Rows kontrolki DataGridView
            bjDgvTWFx.Rows.Clear();
        }

        private void zapiszBitMapêKontrolkiChartWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //. 1. Zapis BitMapy do pliku 
            // utworzenie egzemplarza okna dialogowego: bjOknoPlikuDoZapisu
            SaveFileDialog bjOknoPlikuDoZapisu = new SaveFileDialog
            {
                Title = "Wybór pliku do zapisania BitMapy z kontrolki Chart",
                Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPEG Image|*.jpg",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };

            if (bjOknoPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                // Ustawienie formatu pliku aby u¿yæ funkcji SaveImage, format bêdzie jednak zale¿a³ od tego wybranego z okienka bjOknoPlikuDoZapisu
                ChartImageFormat bjFormat = ChartImageFormat.Bmp;
                try
                {
                    // Zapisanie wykresu jako plik graficzny
                    bjChrt.SaveImage(bjOknoPlikuDoZapisu.FileName, bjFormat);
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show($"ERROR: {ex.Message}", "B³¹d zapisu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // 2. Zapis dodatkowych danych DataGridView do pliku tekstowego
            bjZapiszDaneKontrolkiDataGridViewDoPliku(bjOknoPlikuDoZapisu.FileName + ".Chart.Data");
        }

        private void pobierzBitMapêZPlikuIPodepnijDoKontrolkiChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog bjOknoWyboruPliku = new OpenFileDialog
            {
                Title = "Wybór pliku graficznego do za³adowania",
                Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPEG Image|*.jpg",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };
            // 1. Wczytanie danych tekstowych z pliku do tablicy
            bjWczytajDaneTekstoweDoTablicy(bjOknoWyboruPliku, ".Chart.Data");
            // 2. Aktualizacja pól tekstowych dla podg¹du u¿ytkownika
            bjAktualizacjaZmiennychDlaPolTekstowych();
            // 3. Wype³nienie Kontrolki Chart danymi
            bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
            // 4. Wype³nienie Kontrolki DataGridView danymi
            bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
            // 6. Ustawienie flagi aktualnoœæi danych
            bjFlagaAktualnosciDanych = true;
            // 7. Odkrycie kontrolki Chart i zakrycie DataGridView
            bjDgvTWFx.Visible = false;
            bjChrt.Visible = true;
        }

        private void bjResetChart()
        {
            // Sprawdzenie, czy kontrolka bjChrt istnieje
            if (bjChrt != null)
            {
                // Usuniêcie wszystkich serii danych, jeœli istniej¹
                if (bjChrt.Series != null)
                {
                    bjChrt.Series.Clear();
                }

                // Usuniêcie wszystkich obszarów wykresu, jeœli istniej¹
                if (bjChrt.ChartAreas != null)
                {
                    bjChrt.ChartAreas.Clear();
                }

                // Usuniêcie wszystkich legend, jeœli istniej¹
                if (bjChrt.Legends != null)
                {
                    bjChrt.Legends.Clear();
                }

                // Usuniêcie wszystkich tytu³ów, jeœli istniej¹
                if (bjChrt.Titles != null)
                {
                    bjChrt.Titles.Clear();
                }

                // Opcjonalnie: Resetowanie t³a wykresu
                bjChrt.ChartAreas.Add(new ChartArea("Default"));
                bjChrt.ChartAreas[0].BackImage = "";

                // Opcjonalnie: Dodaj domyœln¹ seriê i obszar wykresu, jeœli to konieczne
                var series = new Series("Default");
                bjChrt.Series.Add(series);
            }
        }


        private void bjZamknijFormularziPrzejdŸDoMenuG³ównegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult bjOknoWiadomosci =
                    MessageBox.Show("Czy na pewno chcesz zamkn¹æ ten formularz?",
                    this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (bjOknoWiadomosci == DialogResult.Yes)
            {
                foreach (Form bjFormularzGlowny in Application.OpenForms)
                {
                    if (bjFormularzGlowny.Name == "KokpitNr2")
                    {
                        this.Hide();
                        bjFormularzGlowny.Show();
                        return;
                    }
                }
            }
        }

        private void bjZakoñczDzia³anieProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ustawienie flagi informuj¹cej o zamykaniu aplikacji do pomiêcia wychodzenia z formularza AnalizatoraIndywidualnego
            AnalizatorIndywidualny.bjCzyZamknac = true;
            Application.Exit();
        }

        private void bjZmianaFormatuCzcionki_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            if (bjElementMenu != null)
            {
                string bjNazwaCzcionki = bjElementMenu.Text;
                if (bjNazwaCzcionki == "Domyœlna (Microsoft Sans Serif)")
                {
                    bjDgvTWFx.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F); // lub inna domyœlna czcionka
                }
                else
                {
                    bjDgvTWFx.DefaultCellStyle.Font = new Font(bjNazwaCzcionki, bjDgvTWFx.DefaultCellStyle.Font.Size);
                }
            }
        }
            private void bjZmianaKoloruCzcionki_Click(object sender, EventArgs e)
            {
                ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
                if (bjElementMenu != null)
                {
                    string bjNazwaKoloru = bjElementMenu.Text;
                    if (bjNazwaKoloru == "Domyœlny")
                    {
                        bjDgvTWFx.DefaultCellStyle.ForeColor = Color.Black; // lub inny domyœlny kolor
                    }
                    else
                    {
                        bjDgvTWFx.DefaultCellStyle.ForeColor = bjPobierzNazweKoloru(bjNazwaKoloru);
                    }
                }
            }

        private void bjZmienKolor(object sender, Control bjKontrolka, Action<Control, Color> bjAkcjaZmianyKoloru)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            bjAkcjaZmianyKoloru(bjKontrolka, bjPobierzNazweKoloru(menuItem.Text));
        }

        private void bjZmianaKoloruSiatki_Click(object sender, EventArgs e)
        {
            bjZmienKolor(sender, bjDgvTWFx, (control, bjColor) => ((DataGridView)control).GridColor = bjColor);
        }

        private void bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bjZmienKolor(sender, bjChrt, (bjKontrolkaLambda, bjKolorLambda) =>
            {
                Chart bjChartCastowany = (Chart)bjKontrolkaLambda;
                foreach (var bjSeria in bjChartCastowany.Series)
                {
                    bjSeria.Color = bjKolorLambda;
                }
            });
        }

        private void bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            string bjNazwaCzcionki = bjElementMenu.Text;

            // Domyœlna czcionka do zastosowania
            Font bjNowaCzcionka;
            if (bjNazwaCzcionki == "Domyœlna (Microsoft Sans Serif)")
            {
                bjNowaCzcionka = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            }
            else
            {
                bjNowaCzcionka = new Font(bjNazwaCzcionki, 12, FontStyle.Regular);
            }

            // Ustawienie czcionki dla tytu³u wykresu (sprawdzenie, czy istniej¹ tytu³y)
            if (bjChrt.Titles.Count > 0)
            {
                bjChrt.Titles[0].Font = bjNowaCzcionka;
            }

            // Ustawienie czcionki dla legendy wykresu (sprawdzenie, czy istniej¹ legendy)
            if (bjChrt.Legends.Count > 0)
            {
                bjChrt.Legends[0].Font = bjNowaCzcionka;
            }

            // Ustawienie czcionki dla osi X (sprawdzenie, czy istniej¹ obszary wykresu)
            if (bjChrt.ChartAreas.Count > 0)
            {
                bjChrt.ChartAreas[0].AxisX.LabelStyle.Font = bjNowaCzcionka;
                bjChrt.ChartAreas[0].AxisX.TitleFont = bjNowaCzcionka;

                // Ustawienie czcionki dla osi Y
                bjChrt.ChartAreas[0].AxisY.LabelStyle.Font = bjNowaCzcionka;
                bjChrt.ChartAreas[0].AxisY.TitleFont = bjNowaCzcionka;
            }

            // Ustawienie czcionki dla serii danych
            foreach (var series in bjChrt.Series)
            {
                series.Font = bjNowaCzcionka;
            }
        }

        private void bjZmianaStyluLiniiWykresuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            string bjWybranyStyl = bjElementMenu.Text;
            System.Windows.Forms.DataVisualization.Charting.ChartDashStyle dashStyle;

            // Okreœlenie stylu linii na podstawie wybranego tekstu
            foreach (var series in bjChrt.Series)
            {
                switch (bjWybranyStyl)
                {
                    case "Ci¹g³a":
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                        break;
                    case "Kreskowana":
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                        series.BorderWidth = 2; // Grubsza kreska dla lepszej widocznoœci
                        break;
                    case "Kropkowana":
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                        series.BorderWidth = 2; // Grubsza kreska dla lepszej widocznoœci
                        break;
                    default:
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                        break;
                }
            }
        }

        private void bjZmianaKoloruT³aWykresuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bjZmienKolor(sender, bjChrt, (control, bjColor) => ((Chart)control).BackColor = bjColor); 
        }

        private void bjZmianaTypuWykresuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            string bjWybranyTypWykresu = bjElementMenu.Text;

            // Okreœlenie typu wykresu na podstawie wybranego tekstu
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType bjTypWykresu;

            switch (bjWybranyTypWykresu)
            {
                case "Liniowy":
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    break;
                case "Kolumnowy":
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    break;
                case "S³upkowy":
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
                    break;
                case "Punktowy":
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                    break;
                case "Obszarowy":
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
                    break;
                default:
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    break;
            }

            // Zastosowanie wybranego typu wykresu do wszystkich serii
            foreach (var bjSerie in bjChrt.Series)
            {
                bjSerie.ChartType = bjTypWykresu;
            }
        }

        private void bjUsuniêcieWykresuBitMapyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Wyczyœæ wszystkie dane wykresu zamiast jego usuwania
            if (bjChrt != null)
            {
                bjChrt.Series.Clear();
                bjChrt.Titles.Clear();
                bjChrt.Legends.Clear();
                bjChrt.ChartAreas.Clear();
            }
        }

        private Color bjPobierzNazweKoloru(string bjNazwaKoloru)
        {
            switch (bjNazwaKoloru)
            {
                case "Czerwony":
                    return Color.Red;
                case "Niebieski":
                    return Color.Blue;
                case "Zielony":
                    return Color.Green;
                case "¯ó³ty":
                    return Color.Yellow;
                case "Pomarañczowy":
                    return Color.Orange;
                case "Fioletowy":
                    return Color.Purple;
                case "Ró¿owy":
                    return Color.Pink;
                case "Bia³y":
                    return Color.White;
                case "Czarny":
                    return Color.Black;
                default:
                    return Color.Black;
            }
        }
    }
}

#endregion