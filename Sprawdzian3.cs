using Projekt2_Janusz70130;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Projekt3_Janusz70130
{
    public partial class Sprawdzian3 : Form
    {
        // tablica wartości
        private float[,] bjTWFx;
        // parametry aplikacji
        private float bjXd;
        private float bjXg;
        private float bjH;
        // pomocnicza flaga aktualności danych służąca do śledzenia stanu aplikacji i spójności danych
        private bool bjFlagaAktualnosciDanych;
        // zmienna służąca do śledzenia czy użytkownik chce wyjść z aplikacji bezpośrednio pomiajając wychodzenie do kokpitu
        public static bool bjCzyZamknac { get; set; } = false;
        public Sprawdzian3()
        {
            InitializeComponent();

            // Wyłączenie dodatkowego wiersza w kontrolce DGataGridView
            bjDgvTWFx.AllowUserToAddRows = false;

            // Dodajemy opcje do menu zmiany formatu czcionki
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Domyślna (Microsoft Sans Serif)", null, bjZmianaFormatuCzcionki_Click_1); // Dodana czcionka Tahoma
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Arial", null, bjZmianaFormatuCzcionki_Click_1);
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Times New Roman", null, bjZmianaFormatuCzcionki_Click_1);
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Verdana", null, bjZmianaFormatuCzcionki_Click_1); // Dodana czcionka Verdana
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Tahoma", null, bjZmianaFormatuCzcionki_Click_1); // Dodana czcionka Tahoma

            // Dodajemy opcje do menu zmiany koloru czcionki
            bjZmianaKoloruCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Czarny", null, bjZmianaKoloruCzcionki_Click_1);
            bjZmianaKoloruCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruCzcionki_Click_1);
            bjZmianaKoloruCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruCzcionki_Click_1);
            bjZmianaKoloruCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruCzcionki_Click_1);

            // Dodajemy opcje do menu zmiany koloru siatki
            bjZmianaKoloruSiatKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Czarny", null, bjZmianaKoloruSiatki_Click_1);
            bjZmianaKoloruSiatKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruSiatki_Click_1);
            bjZmianaKoloruSiatKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruSiatki_Click_1);
            bjZmianaKoloruSiatKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruSiatki_Click_1);

            // Dodajemy opcje zmiany koloru tła kontrolki Chart
            zmianaKoloruTłaWykresuToolStripMenuItem.DropDownItems.Add("Biały", null, bjZmianaKoloruTłaWykresuToolStripMenuItem_Click_1);
            zmianaKoloruTłaWykresuToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruTłaWykresuToolStripMenuItem_Click_1);
            zmianaKoloruTłaWykresuToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruTłaWykresuToolStripMenuItem_Click_1);
            zmianaKoloruTłaWykresuToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruTłaWykresuToolStripMenuItem_Click_1);

            // Dodajemy opcje zmiany koloru linii wykresu
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Biały", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click_1);
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click_1);
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click_1);
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click_1);

            // Dodajemy opcje zmiany czcionki wykresu
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Domyślna (Microsoft Sans Serif)", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click_1);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Arial", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click_1);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Times New Roman", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click_1);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Verdana", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click_1);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Tahoma", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click_1);

            // Dodanie opcji zmiany stylu linii wykresu
            zmianaStyluLiniiWykresuToolStripMenuItem.DropDownItems.Add("Ciągła", null, bjZmianaStyluLiniiWykresuToolStripMenuItem_Click_1);
            zmianaStyluLiniiWykresuToolStripMenuItem.DropDownItems.Add("Kreskowana", null, bjZmianaStyluLiniiWykresuToolStripMenuItem_Click_1);
            zmianaStyluLiniiWykresuToolStripMenuItem.DropDownItems.Add("Kropkowana", null, bjZmianaStyluLiniiWykresuToolStripMenuItem_Click_1);

            // Dodanie opcji zmiany typu wykresu
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Liniowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click_1);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Kolumnowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click_1);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Słupkowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click_1);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Punktowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click_1);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Obszarowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click_1);

            // Dodanie opcji usunięcia wykresu
            bjUsunięcieWykresuBitMapyToolStripMenuItem.Click += bjUsunięcieWykresuBitMapyToolStripMenuItem_Click_1;
        }
        /**/
        #region funkcje pomocnicze analizatora indywidualnego
        private void bjBtnObliczWartośćFx_Click_1(object sender, EventArgs e)
        {
            // Pobranie zmiennej X
            if (!bjSpróbujPobraćFloatIObsłóżbjBłąd(bjTxtX.Text, out float bjX, bjTxtX, "X"))
            {
                return;
            }
            // zgszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            // obliczenie wartości funkcji
            float bjWartośćFunkcji = bjObliczWartośćFunkcji(bjX);
            // wizualizacja wyniku obliczeń 
            bjTxtFX.Text = String.Format("{0:F2}", bjWartośćFunkcji);
            // ustawienie stanu braku aktywności dla obsługiwanego przycisku poleceń
            bjBtnObliczWartośćFx.Enabled = false;
            // ustawienie stanu braku aktywności dla pola tekstowego
            bjTxtFX.Enabled = false;
            // zablokowanie przycisku X 
            bjTxtX.Enabled = false;
        }

        private bool bjSpróbujPobraćFloatIObsłóżbjBłąd(string bjText, out float bjLiczbaFloat, Control bjKontrolka, string bjNazwaWspółczynnika)
        {
            if (!float.TryParse(bjText, out bjLiczbaFloat))
            {
                bjErrorProvider2.SetError(bjKontrolka, $"ERROR: w zapisie wartości współczynnika '{bjNazwaWspółczynnika}' wystąpił niedozwolony znak!");
                return false;
            }
            return true;
        }

        private float bjObliczWartośćFunkcji(float bjX)
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

        private bool bjPobranieDanychWejściowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH)
        {
            /* pomocnicze przypisanie wartości dla parametrów wyjściowych */
            bjXd = bjXg = bjH = 0.0F;

            // Pobranie współczynnika bjXd
            if (!bjSpróbujPobraćFloatIObsłóżbjBłąd(bjTxtXd.Text, out bjXd, bjTxtXd, "Xd"))
            {
                return false;
            }

            // Pobranie współczynnika bjXg
            if (!bjSpróbujPobraćFloatIObsłóżbjBłąd(bjTxtXg.Text, out bjXg, bjTxtXg, "Xg"))
            {
                return false;
            }

            // Pobranie współczynnika bjH
            if (!bjSpróbujPobraćFloatIObsłóżbjBłąd(bjTxtH.Text, out bjH, bjTxtH, "H"))
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
                // sygnalizujemy błąd
                bjErrorProvider2.SetError(bjTxtXd, "ERROR: dolna granica Xd musi być mniejsza od górnej granicy Xg");
                // zwrotne przekazanie informacji, że zaistniał błąd 
                return false;
            }
            if (bjH <= 0.0)
            {
                // sygnalizujemy błąd
                bjErrorProvider2.SetError(bjTxtH, "ERROR: nie został spełniony warunek h > 0");
                // zwrotne przekazanie informacji, że zaistniał błąd 
                return false;
            }

            if (bjH > Math.Abs(bjXg - bjXd) / 2)
            {
                // sygnalizujemy błąd
                bjErrorProvider2.SetError(bjTxtH, "ERROR: nie został spełniony warunek h <= (Xg - Xd) / 2");
                // zwrotne przekazanie informacji, że zaistniał błąd 
                return false;
            }
            // gdy nie było żadnego błędu to sterowanie przejdzie "tutaj" 
            // ustawienie stanu braku aktywności dla kontrolek z danymi wejściowymi
            bjZablokujPolaTekstowe();
            /* musimy przekazać informację, że błędu nie było */
            return true;
        }

        private void bjZablokujPolaTekstowe()
        {
            bjTxtXd.Enabled = false;
            bjTxtXg.Enabled = false;
            bjTxtH.Enabled = false;
        }

        private void bjTablicowanieWartościFunkcjiFx(float bjXd, float bjXg, float bjH)
        {
            // wyznaczenie liczby podprzedziałów przedziału [Xd, Xg] z krokiem h
            int bjN = (int)((bjXg - bjXd) / bjH) + 1;
            // +1, gdyż wynik dzielenia ((Xg-Xd)/h) będzie liczbą ułamkową
            // i powinniśmy go zaokrąglić do najbliższej liczby całkowitej "w górę" 

            // utworzenie egzemplarza tablicy bjTWFx
            bjTWFx = new float[bjN, 3];
            // deklaracje pomocnicze
            float bjX; // zmienna niezależna X
            int bjI; // numer w podprzedziale w przedziale [Xd, Xg]
            for (bjX = bjXd, bjI = 0; bjI < bjTWFx.GetLength(0); bjI++, bjX = bjXd + bjI * bjH)
            {
                bjTWFx[bjI, 0] = bjI;
                bjTWFx[bjI, 1] = bjX;
                bjTWFx[bjI, 2] = bjObliczWartośćFunkcji(bjX);
            }
        }

        private void bjWpiszWierszeDanychDoKontrolkiDataGridView(float[,] bjTWFx, ref DataGridView bjDgvTWFx)
        {
            // Wyczyść wszystkie wiersze z DataGridView
            bjDgvTWFx.Rows.Clear();

            // Dodaj wiersze z tablicy bjTWFx do DataGridView
            for (int bjI = 0; bjI < bjTWFx.GetLength(0); bjI++)
            {
                // dodanie nowego wiersza
                bjDgvTWFx.Rows.Add();

                // wpisanie wartości numeru przedziału X
                bjDgvTWFx.Rows[bjI].Cells[0].Value = String.Format("{0}", bjTWFx[bjI, 0]);

                // wpisanie wartości zmiennej niezależnej X
                bjDgvTWFx.Rows[bjI].Cells[1].Value = String.Format("{0:F2}", bjTWFx[bjI, 1]);

                // wpisanie wartości równania
                bjDgvTWFx.Rows[bjI].Cells[2].Value = String.Format("{0:F2}", bjTWFx[bjI, 2]);
            }
        }

        private void bjWpiszWierszeDanychDoKontrolkiChart(float[,] bjTWFx, ref Chart bjChrt)
        {
            // Wyczyść wszystkie serie z wykresu
            bjChrt.Series.Clear();

            // Dodanie nowej serii danych
            Series bjKrzywa = new Series("Wartość funkcji F(X)")
            {
                ChartType = SeriesChartType.Line
            };

            // Dodanie serii do wykresu
            bjChrt.Series.Add(bjKrzywa);

            // Dodawanie punktów do serii z tablicy
            for (int bjI = 0; bjI < bjTWFx.GetLength(0); bjI++)
            {
                bjKrzywa.Points.AddXY(bjTWFx[bjI, 1], bjTWFx[bjI, 2]);
            }
        }


        private void bjBtnReset_Click(object sender, EventArgs e)
        {
            // zgaszenie sygnalizacji błędu
            bjErrorProvider2.Clear();
            // ukrycie i czyszczenie wszystkich wizualizacji 
            bjDgvTWFx.Visible = false;
            bjChrt.Visible = false;
            bjDgvTWFx.Rows.Clear();
            bjChrt.Series.Clear();
            // odblokowanie przycisków
            bjBtnObliczWartośćFx.Enabled = true;
            bjBtnWizualizacjaGraficznaFx.Enabled = true;
            //bjBtnWizualizacjaTabelarycznaFx.Enabled = true;
            // odblokowanie i czyszczenie okienek z tekstem
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
            // Ustawienie flagi aktualnośći danych na false
            bjFlagaAktualnosciDanych = false;
        }

        private void bjAnalizatorIndywidualnyForm_Closing(object sender, FormClosingEventArgs e)
        {
            // sprawdzenie czy użytkownik chce obejść wychodznie z aplikacji przez kokpit
            if (!bjCzyZamknac)
            {
                // Normalne zachowanie zamykania formularza
                DialogResult OknoMessage =
                    MessageBox.Show("Czy na pewno chcesz zamknąć ten formularz?",
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
            // użytkonik chce ominąć zamykanie aplikacji przez wychodzenie z kokpitu,
            // wyświetla sie tylko okienko użyte w funkcji bjZakończDziałanieProgramuToolStripMenuItem_Click_1
            else
            {
                // Pominięcie dodatkowych działań przy zamykaniu
                e.Cancel = false;
            }
        }

        private void zapiszWierszeDanychKontrolkiDataGridViewDoPlikuToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // wygaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

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
                // Zapisanie zawartości DataGridView do wybranego pliku
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
                    // Zapisanie każdego wiersza z DataGridView do pliku
                    for (int i = 0; i < bjDgvTWFx.Rows.Count; i++)
                    {
                        bjPlikZnakowy.Write(bjDgvTWFx.Rows[i].Cells[0].Value); // Zapisanie wartości z pierwszej komórki
                        bjPlikZnakowy.Write(";"); // Separator
                        bjPlikZnakowy.Write(bjDgvTWFx.Rows[i].Cells[1].Value); // Zapisanie wartości z drugiej komórki
                        bjPlikZnakowy.Write(";"); // Separator
                        bjPlikZnakowy.WriteLine(bjDgvTWFx.Rows[i].Cells[2].Value); // Zapisanie wartości z trzeciej komórki i przejście do nowej linii
                    }
                }
                catch (Exception bjBłąd)
                {
                    // Wyświetlenie komunikatu o błędzie w przypadku wystąpienia wyjątku
                    MessageBox.Show("ERROR: wystąpił nieoczekiwany błąd podczas zapisu " +
                        "wierszy danych z kontrolki DataGridView (komunikat systemowy: " +
                        bjBłąd.Message + " )");
                }
            }
        }
        private void pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // Utworzenie okna dialogowego dla otwarcia (wskazania) pliku, z którego zostaną pobrane dane
            OpenFileDialog bjOknoWyboru = new OpenFileDialog
            {
                Title = "Wybór pliku do pobrania wierszy danych dla kontrolki DataGridView",
                Filter = "txt files (*.txt)|*.txt|all files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };

            if (bjOknoWyboru.ShowDialog() == DialogResult.OK)
            {
                // 1. Wczytanie danych tekstowych z pliku do tablicy
                bjWczytajDaneTekstoweDoTablicy(bjOknoWyboru, "");
                // 2. Aktualizacja pól tekstowych dla podgądu użytkownika
                bjAktualizacjaZmiennychDlaPolTekstowych();
                // 3. Wypełnienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 4. Wypełnienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualnośći danych
                bjFlagaAktualnosciDanych = true;
                // 7. Odkrycie kontrolki DataGridView i zakrycie Chart
                bjDgvTWFx.Visible = true;
                bjChrt.Visible = false;
                // 8. Zablokowanie pól tekstowych
                bjZablokujPolaTekstowe();
                // ustawienie odpowiedniego stanu przycisków
                bjUstawPrzyciskiDlaWizualizacji(true);
            }
        }

        private void bjUstawPrzyciskiDlaWizualizacji(bool bjCzyTabelaryczna)
        {
            if (bjCzyTabelaryczna)
            {
                //bjBtnWizualizacjaTabelarycznaFx.Enabled = false;
                bjBtnWizualizacjaGraficznaFx.Enabled = true;
            }
            else
            {
                //bjBtnWizualizacjaTabelarycznaFx.Enabled = true;
                bjBtnWizualizacjaGraficznaFx.Enabled = false;
            }
        }

        private void bjWczytajDaneTekstoweDoTablicy(OpenFileDialog bjOknoWyboruPliku, string bjSuffix)
        {
            // Plik został wybrany, otwarcie go w trybie strumieni znaków
            string bjWierszDanych; // Przechowanie wiersza danych wczytanych z pliku znakowego
            string[] bjElementyWierszaDanych; // Przechowanie pojedynczych danych z wczytanego wiersza danych
            string bjNazwaPlikuDanychTekstowychDlaKontrolkiChart = bjOknoWyboruPliku.FileName + bjSuffix;
            int liczbaWierszy = File.ReadAllLines(bjNazwaPlikuDanychTekstowychDlaKontrolkiChart).Length;
            // Pomocniczo lokalna tablica
            float[,] bjTWFxLokalna = new float[liczbaWierszy, 3]; // Inicjalizacja tablicy

            // Utworzenie i otwarcie strumienia znaków do odczytu
            using (StreamReader bjPlikZnakowy = new StreamReader(bjNazwaPlikuDanychTekstowychDlaKontrolkiChart))
            {
                try
                {
                    int NrWiersza = 0; // Ustalenie warunku brzegowego

                    // Wczytywanie wierszy z pliku znakowego aż do 'znacznika' końca pliku
                    while (!bjPlikZnakowy.EndOfStream)
                    {
                        // Wczytywanie wiersza (linii) z pliku znakowego
                        bjWierszDanych = bjPlikZnakowy.ReadLine();

                        // "Rozpakowanie" (podział) pobranego wiersza tekstowego na części oddzielane separatorem ';'
                        bjElementyWierszaDanych = bjWierszDanych.Split(new char[] { ';', ':', '|' });

                        // Usunięcie ewentualnych spacji w poszczególnych wierszach tablicy bjElementyWierszaDanych
                        bjElementyWierszaDanych[0] = bjElementyWierszaDanych[0].Trim();
                        bjElementyWierszaDanych[1] = bjElementyWierszaDanych[1].Trim();
                        bjElementyWierszaDanych[2] = bjElementyWierszaDanych[2].Trim();

                        // Przypisanie wartości do tablicy bjTWFx
                        bjTWFxLokalna[NrWiersza, 0] = float.Parse(bjElementyWierszaDanych[0]);
                        bjTWFxLokalna[NrWiersza, 1] = float.Parse(bjElementyWierszaDanych[1]);
                        bjTWFxLokalna[NrWiersza, 2] = float.Parse(bjElementyWierszaDanych[2]);

                        NrWiersza++; // Zwiększenie licznika wierszy wpisanych do tablicy
                    }
                }
                catch (Exception bjBłąd)
                {
                    // Wyświetlenie komunikatu o błędzie w przypadku wystąpienia wyjątku
                    MessageBox.Show("ERROR: wystąpił nieoczekiwany błąd podczas odczytu " +
                        "wierszy danych z pliku (komunikat systemowy: " +
                        bjBłąd.Message + " )");
                }
            }

            // Aktualizacja zmiennej klasy
            bjTWFx = bjTWFxLokalna;
        }


        private void bjAktualizacjaZmiennychDlaPolTekstowych()
        {
            // Sprawdzenie, czy tablica bjTWFx jest zainicjalizowana i ma co najmniej dwa wiersze
            if (bjTWFx != null && bjTWFx.GetLength(0) > 1)
            {
                // Pierwsza wartość z tablicy to bjXd
                bjXd = bjTWFx[0, 1];

                // Ostatnia wartość z tablicy to bjXg
                bjXg = bjTWFx[bjTWFx.GetLength(0) - 1, 1];

                // Różnica pomiędzy kolejnymi wartościami w tablicy to bjH
                // Zakładając, że wartości X są równomiernie rozłożone, możemy obliczyć bjH jako różnicę między dwiema kolejnymi wartościami
                if (bjTWFx.GetLength(0) > 1)
                {
                    bjH = bjTWFx[1, 1] - bjTWFx[0, 1];
                }
                // aktualizacja pól tekstowych
                bjTxtXd.Text = bjXd.ToString();
                bjTxtXg.Text = bjXg.ToString();
                bjTxtH.Text = bjH.ToString();
            }
            else
            {
                // Obsługa przypadku, gdy tablica jest pusta lub ma mniej niż dwa wiersze
                MessageBox.Show("Tablica bjTWFx jest pusta lub ma za mało danych do obliczenia zmiennych.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usuńWierszeDanychToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // sprawdzenie widoczności kontrolki DataGridView
            /*if (!bjDgvTWFx.Visible)
            {
                // jest błąd
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: " +
                    "kontrolka DataGridView nie została odsłonięta");
                // przerwanie obsługi zdarzenia "Click_1"
                return;
            }*/
            DialogResult bjOknoMessage = MessageBox.Show("UWAGA: w kontrolce są dane. Czy na pewno chcesz je utracić?",
                this.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            // rozpoznanie reakcji użytkownika
            if (bjOknoMessage == DialogResult.No)
            {
                MessageBox.Show("KOMUNIKAT: polecenie pobrania danych z pliku zostało anulowane",
                    this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // przerywamy dalszą obsługę zdarzenia Click_1
                return;
            }
            // usunięcie wierszy danych w kolekcji Rows kontrolki DataGridView
            bjDgvTWFx.Rows.Clear();
        }

        private void zapiszBitMapęKontrolkiChartWPlikuToolStripMenuItem_Click_1(object sender, EventArgs e)
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
                // Ustawienie formatu pliku aby użyć funkcji SaveImage, format będzie jednak zależał od tego wybranego z okienka bjOknoPlikuDoZapisu
                ChartImageFormat bjFormat = ChartImageFormat.Bmp;
                try
                {
                    // Zapisanie wykresu jako plik graficzny
                    bjChrt.SaveImage(bjOknoPlikuDoZapisu.FileName, bjFormat);
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show($"ERROR: {ex.Message}", "Błąd zapisu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // 2. Zapis dodatkowych danych DataGridView do pliku tekstowego
                bjZapiszDaneKontrolkiDataGridViewDoPliku(bjOknoPlikuDoZapisu.FileName + ".Chart.Data");
            }
        }

        private void pobierzBitMapęZPlikuIPodepnijDoKontrolkiChartToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog bjOknoWyboru = new OpenFileDialog
            {
                Title = "Wybór pliku graficznego do załadowania",
                Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPEG Image|*.jpg",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };

            if (bjOknoWyboru.ShowDialog() == DialogResult.OK)
            {
                // 1. Wczytanie danych tekstowych z pliku do tablicy
                bjWczytajDaneTekstoweDoTablicy(bjOknoWyboru, ".Chart.Data");
                // 2. Aktualizacja pól tekstowych dla podgądu użytkownika
                bjAktualizacjaZmiennychDlaPolTekstowych();
                // 3. Wypełnienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 4. Wypełnienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualnośći danych
                bjFlagaAktualnosciDanych = true;
                // 7. Odkrycie kontrolki Chart i zakrycie DataGridView
                bjDgvTWFx.Visible = false;
                bjChrt.Visible = true;
                // ustawienie odpowiedniego stanu przycisków
                bjUstawPrzyciskiDlaWizualizacji(false);
                // zablokowanie pól tekstowych
                bjZablokujPolaTekstowe();
            }
        }


        private void bjZamknijFormularziPrzejdźDoMenuGłównegoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult bjOknoWiadomosci =
                    MessageBox.Show("Czy na pewno chcesz zamknąć ten formularz?",
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

        private void bjZakończDziałanieProgramuToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Ustawienie flagi informującej o zamykaniu aplikacji do pomięcia wychodzenia z formularza AnalizatoraIndywidualnego
            AnalizatorIndywidualny.bjCzyZamknac = true;
            Application.Exit();
        }

        private void bjZmianaFormatuCzcionki_Click_1(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            if (bjElementMenu != null)
            {
                string bjNazwaCzcionki = bjElementMenu.Text;
                if (bjNazwaCzcionki == "Domyślna (Microsoft Sans Serif)")
                {
                    bjDgvTWFx.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F); // lub inna domyślna czcionka
                }
                else
                {
                    bjDgvTWFx.DefaultCellStyle.Font = new Font(bjNazwaCzcionki, bjDgvTWFx.DefaultCellStyle.Font.Size);
                }
            }
        }
        private void bjZmianaKoloruCzcionki_Click_1(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            if (bjElementMenu != null)
            {
                string bjNazwaKoloru = bjElementMenu.Text;
                if (bjNazwaKoloru == "Domyślny")
                {
                    bjDgvTWFx.DefaultCellStyle.ForeColor = Color.Black; // lub inny domyślny kolor
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

        private void bjZmianaKoloruSiatki_Click_1(object sender, EventArgs e)
        {
            bjZmienKolor(sender, bjDgvTWFx, (control, bjColor) => ((DataGridView)control).GridColor = bjColor);
        }

        private void bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            string bjNazwaCzcionki = bjElementMenu.Text;

            // Domyślna czcionka do zastosowania
            Font bjNowaCzcionka;
            if (bjNazwaCzcionki == "Domyślna (Microsoft Sans Serif)")
            {
                bjNowaCzcionka = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            }
            else
            {
                bjNowaCzcionka = new Font(bjNazwaCzcionki, 12, FontStyle.Regular);
            }

            // Ustawienie czcionki dla tytułu wykresu (sprawdzenie, czy istnieją tytuły)
            if (bjChrt.Titles.Count > 0)
            {
                bjChrt.Titles[0].Font = bjNowaCzcionka;
            }

            // Ustawienie czcionki dla legendy wykresu (sprawdzenie, czy istnieją legendy)
            if (bjChrt.Legends.Count > 0)
            {
                bjChrt.Legends[0].Font = bjNowaCzcionka;
            }

            // Ustawienie czcionki dla osi X (sprawdzenie, czy istnieją obszary wykresu)
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

        private void bjZmianaStyluLiniiWykresuToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            string bjWybranyStyl = bjElementMenu.Text;
            System.Windows.Forms.DataVisualization.Charting.ChartDashStyle dashStyle;

            // Określenie stylu linii na podstawie wybranego tekstu
            foreach (var series in bjChrt.Series)
            {
                switch (bjWybranyStyl)
                {
                    case "Ciągła":
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                        break;
                    case "Kreskowana":
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                        series.BorderWidth = 2; // Grubsza kreska dla lepszej widoczności
                        break;
                    case "Kropkowana":
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                        series.BorderWidth = 2; // Grubsza kreska dla lepszej widoczności
                        break;
                    default:
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                        break;
                }
            }
        }

        private void bjZmianaKoloruTłaWykresuToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            bjZmienKolor(sender, bjChrt, (control, bjColor) => ((Chart)control).BackColor = bjColor);
        }

        private void bjZmianaTypuWykresuToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            string bjWybranyTypWykresu = bjElementMenu.Text;

            // Określenie typu wykresu na podstawie wybranego tekstu
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType bjTypWykresu;

            switch (bjWybranyTypWykresu)
            {
                case "Liniowy":
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    break;
                case "Kolumnowy":
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    break;
                case "Słupkowy":
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

        private void bjUsunięcieWykresuBitMapyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Wyczyść wszystkie dane wykresu zamiast jego usuwania
            if (bjChrt != null)
            {
                bjChrt.Series.Clear();
                bjChrt.Titles.Clear();
                bjChrt.Legends.Clear();
                //bjChrt.ChartAreas.Clear();
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
                case "Żółty":
                    return Color.Yellow;
                case "Pomarańczowy":
                    return Color.Orange;
                case "Fioletowy":
                    return Color.Purple;
                case "Różowy":
                    return Color.Pink;
                case "Biały":
                    return Color.White;
                case "Czarny":
                    return Color.Black;
                default:
                    return Color.Black;
            }
        }

        private void bjBtnWizualizacjaTabelarycznaFx_Click(object sender, EventArgs e)
        {
            // zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // 1. Jeżeli flaga nie jest ustawiona, wystarczy odkryć kontrolkę DGV i ukryć inne,
            // jeżeli jest to trzeba podjąć kroki do wizualizacji tabelarycznej
            if (!bjFlagaAktualnosciDanych)
            {
                // 2. Pobranie danych z pól tekstowych
                /*
                if (!bjPobranieDanychWejściowychDlaTablicowania(out bjXd, out bjXg, out bjH))
                {
                    // był błąd, to go sygnalizuje 
                    bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: w zapisie danych wejściowych wystąpił " +
                        "niedozwolony znak.");
                    // przerwanie obsługi zdarzenia Click_1: bjBtnWizualizacjaTabelarycznaFx_Click_1
                    return;
                }*/
                // 3. Tablicowanie wartośći 
                bjTablicowanieWartościFunkcjiFx(bjXd, bjXg, bjH);
                // 4. Wypełnienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 5. Wypełnienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualnośći danych
                bjFlagaAktualnosciDanych = true;
            }
            // 7. Odkrycie kontrolki Chart i zakrycie DataGridView
            bjDgvTWFx.Visible = true;
            bjChrt.Visible = false;
            // ustawienie odpowiedniego stanu przycisków
            bjUstawPrzyciskiDlaWizualizacji(true);
        }

        private void bjBtnWizualizacjaGraficznaFx_Click(object sender, EventArgs e)
        {
            // zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // 1. Jeżeli flaga nie jest ustawiona, wystarczy odkryć kontrolkę Chart i ukryć inne,
            // jeżeli jest to trzeba podjąć kroki do wizualizacji graficznej
            if (!bjFlagaAktualnosciDanych)
            {
                // 2. Pobranie danych z pól tekstowych
                if (!bjPobranieDanychWejściowychDlaTablicowania(out bjXd, out bjXg, out bjH))
                {
                    // sygnalizowanie błędu
                    bjErrorProvider2.SetError(bjBtnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wejściowych wystąpił " +
                        "niedozwolony znak.");
                    // przerwanie obsługi zdarzenia Click_1: btnWizualizacjaGraficznaFx_Click_1
                    return;
                }
                // 3. Tablicowanie wartośći
                bjTablicowanieWartościFunkcjiFx(bjXd, bjXg, bjH);
                // 4. Wypełnienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 5. Wypełnienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualnośći danych
                bjFlagaAktualnosciDanych = true;

            }
            // 7. Odkrycie kontrolki Chart i zakrycie DataGridView
            bjDgvTWFx.Visible = false;
            bjChrt.Visible = true;

            // ustawienie odpowiedniego stanu przycisków
            bjUstawPrzyciskiDlaWizualizacji(false);
        }


        #endregion
        /**/
    }
}
