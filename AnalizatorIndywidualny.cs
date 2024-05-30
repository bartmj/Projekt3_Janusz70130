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
        // tablica warto�ci
        private float[,] bjTWFx;
        // parametry aplikacji
        private float bjXd;
        private float bjXg;
        private float bjH;
        // pomocnicza flaga aktualno�ci danych s�u��ca do �ledzenia stanu aplikacji i sp�jno�ci danych
        private bool bjFlagaAktualnosciDanych;
        // zmienna s�u��ca do �ledzenia czy u�ytkownik chce wyj�� z aplikacji bezpo�rednio pomiajaj�c wychodzenie do kokpitu
        public static bool bjCzyZamknac { get; set; } = false;

        public AnalizatorIndywidualny()
        {
            InitializeComponent();
            // Wy��czenie dodatkowego wiersza w kontrolce DGataGridView
            bjDgvTWFx.AllowUserToAddRows = false;

            // Dodajemy opcje do menu zmiany formatu czcionki
            bjZmianaFormatuCzcionkiKontrolkiDataGridViewToolStripMenuItem.DropDownItems.Add("Domy�lna (Microsoft Sans Serif)", null, bjZmianaFormatuCzcionki_Click); // Dodana czcionka Tahoma
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

            // Dodajemy opcje zmiany koloru t�a kontrolki Chart
            zmianaKoloruT�aWykresuToolStripMenuItem.DropDownItems.Add("Bia�y", null, bjZmianaKoloruT�aWykresuToolStripMenuItem_Click);
            zmianaKoloruT�aWykresuToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruT�aWykresuToolStripMenuItem_Click);
            zmianaKoloruT�aWykresuToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruT�aWykresuToolStripMenuItem_Click);
            zmianaKoloruT�aWykresuToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruT�aWykresuToolStripMenuItem_Click);

            // Dodajemy opcje zmiany koloru linii wykresu
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Bia�y", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click);
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Czerwony", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click);
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Niebieski", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click);
            bjZmianaKoloruLiniiWykresuToolStripMenuItem.DropDownItems.Add("Zielony", null, bjZmianaKoloruLiniiWykresuToolStripMenuItem_Click);

            // Dodajemy opcje zmiany czcionki wykresu
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Domy�lna (Microsoft Sans Serif)", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Arial", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Times New Roman", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Verdana", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);
            bjZmianaFormatuCzcionkiToolStripMenuItem.DropDownItems.Add("Tahoma", null, bjZmianaFormatuCzcionkiWykresuToolStripMenuItem_Click);

            // Dodanie opcji zmiany stylu linii wykresu
            zmianaStyluLiniiWykresuToolStripMenuItem.DropDownItems.Add("Ci�g�a", null, bjZmianaStyluLiniiWykresuToolStripMenuItem_Click);
            zmianaStyluLiniiWykresuToolStripMenuItem.DropDownItems.Add("Kreskowana", null, bjZmianaStyluLiniiWykresuToolStripMenuItem_Click);
            zmianaStyluLiniiWykresuToolStripMenuItem.DropDownItems.Add("Kropkowana", null, bjZmianaStyluLiniiWykresuToolStripMenuItem_Click);

            // Dodanie opcji zmiany typu wykresu
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Liniowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Kolumnowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("S�upkowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Punktowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);
            bjZmianaTypuWykresuToolStripMenuItem.DropDownItems.Add("Obszarowy", null, bjZmianaTypuWykresuToolStripMenuItem_Click);

            // Dodanie opcji usuni�cia wykresu
            bjUsuni�cieWykresuBitMapyToolStripMenuItem.Click += bjUsuni�cieWykresuBitMapyToolStripMenuItem_Click;

        }


        #region funkcje pomocnicze analizatora indywidualnego
        private void bjBtnObliczWarto��Fx_Click(object sender, EventArgs e)
        {
            // Pobranie zmiennej X
            if (!bjSpr�bujPobra�FloatIObs��bjB��d(bjTxtX.Text, out float bjX, bjTxtX, "X"))
            {
                return;
            }
            // zgszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            // obliczenie warto�ci funkcji
            float bjWarto��Funkcji = bjObliczWarto��Funkcji(bjX);
            // wizualizacja wyniku oblicze� 
            bjTxtFX.Text = String.Format("{0:F2}", bjWarto��Funkcji);
            // ustawienie stanu braku aktywno�ci dla obs�ugiwanego przycisku polece�
            bjBtnObliczWarto��Fx.Enabled = false;
            // ustawienie stanu braku aktywno�ci dla pola tekstowego
            bjTxtFX.Enabled = false;
            // zablokowanie przycisku X 
            bjTxtX.Enabled = false;
        }

        private bool bjSpr�bujPobra�FloatIObs��bjB��d(string bjText, out float bjLiczbaFloat, Control bjKontrolka, string bjNazwaWsp�czynnika)
        {
            if (!float.TryParse(bjText, out bjLiczbaFloat))
            {
                bjErrorProvider2.SetError(bjKontrolka, $"ERROR: w zapisie warto�ci wsp�czynnika '{bjNazwaWsp�czynnika}' wyst�pi� niedozwolony znak!");
                return false;
            }
            return true;
        }

        private float bjObliczWarto��Funkcji(float bjX)
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

        private bool bjPobranieDanychWej�ciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH)
        {
            /* pomocnicze przypisanie warto�ci dla parametr�w wyj�ciowych */
            bjXd = bjXg = bjH = 0.0F;

            // Pobranie wsp�czynnika bjXd
            if (!bjSpr�bujPobra�FloatIObs��bjB��d(bjTxtXd.Text, out bjXd, bjTxtXd, "Xd"))
            {
                return false;
            }

            // Pobranie wsp�czynnika bjXg
            if (!bjSpr�bujPobra�FloatIObs��bjB��d(bjTxtXg.Text, out bjXg, bjTxtXg, "Xg"))
            {
                return false;
            }

            // Pobranie wsp�czynnika bjH
            if (!bjSpr�bujPobra�FloatIObs��bjB��d(bjTxtH.Text, out bjH, bjTxtH, "H"))
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
                // sygnalizujemy b��d
                bjErrorProvider2.SetError(bjTxtXd, "ERROR: dolna granica Xd musi by� mniejsza od g�rnej granicy Xg");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            if (bjH <= 0.0)
            {
                // sygnalizujemy b��d
                bjErrorProvider2.SetError(bjTxtH, "ERROR: nie zosta� spe�niony warunek h > 0");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }

            if (bjH > Math.Abs(bjXg - bjXd) / 2)
            {
                // sygnalizujemy b��d
                bjErrorProvider2.SetError(bjTxtH, "ERROR: nie zosta� spe�niony warunek h <= (Xg - Xd) / 2");
                // zwrotne przekazanie informacji, �e zaistnia� b��d 
                return false;
            }
            // gdy nie by�o �adnego b��du to sterowanie przejdzie "tutaj" 
            // ustawienie stanu braku aktywno�ci dla kontrolek z danymi wej�ciowymi
            bjZablokujPolaTekstowe();
            /* musimy przekaza� informacj�, �e b��du nie by�o */
            return true;
        }

        private void bjZablokujPolaTekstowe()
        {
            bjTxtXd.Enabled = false;
            bjTxtXg.Enabled = false;
            bjTxtH.Enabled = false;
        }

        private void bjTablicowanieWarto�ciFunkcjiFx(float bjXd, float bjXg, float bjH)
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
            // Wyczy�� wszystkie wiersze z DataGridView
            bjDgvTWFx.Rows.Clear();

            // Dodaj wiersze z tablicy bjTWFx do DataGridView
            for (int bjI = 0; bjI < bjTWFx.GetLength(0); bjI++)
            {
                // dodanie nowego wiersza
                bjDgvTWFx.Rows.Add();

                // wpisanie warto�ci numeru przedzia�u X
                bjDgvTWFx.Rows[bjI].Cells[0].Value = String.Format("{0}", bjTWFx[bjI, 0]);

                // wpisanie warto�ci zmiennej niezale�nej X
                bjDgvTWFx.Rows[bjI].Cells[1].Value = String.Format("{0:F2}", bjTWFx[bjI, 1]);

                // wpisanie warto�ci r�wnania
                bjDgvTWFx.Rows[bjI].Cells[2].Value = String.Format("{0:F2}", bjTWFx[bjI, 2]);
            }
        }

        private void bjWpiszWierszeDanychDoKontrolkiChart(float[,] bjTWFx, ref Chart bjChrt)
        {
            // Wyczy�� wszystkie serie z wykresu
            bjChrt.Series.Clear();

            // Dodanie nowej serii danych
            Series bjKrzywa = new Series("Warto�� funkcji F(X)")
            {
                ChartType = SeriesChartType.Line
            };

            // Dodanie serii do wykresu
            bjChrt.Series.Add(bjKrzywa);

            // Dodawanie punkt�w do serii z tablicy
            for (int bjI = 0; bjI < bjTWFx.GetLength(0); bjI++)
            {
                bjKrzywa.Points.AddXY(bjTWFx[bjI, 1], bjTWFx[bjI, 2]);
            }
        }


        private void bjBtnWizualizacjaTabelarycznaFx_Click(object sender, EventArgs e)
        {
            // zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // 1. Je�eli flaga nie jest ustawiona, wystarczy odkry� kontrolk� DGV i ukry� inne,
            // je�eli jest to trzeba podj�� kroki do wizualizacji tabelarycznej
            if (!bjFlagaAktualnosciDanych)
            {
                // 2. Pobranie danych z p�l tekstowych
                if (!bjPobranieDanychWej�ciowychDlaTablicowania(out bjXd, out bjXg, out bjH))
                {
                    // by� b��d, to go sygnalizuje 
                    bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: w zapisie danych wej�ciowych wyst�pi� " +
                        "niedozwolony znak.");
                    // przerwanie obs�ugi zdarzenia Click: bjBtnWizualizacjaTabelarycznaFx_Click
                    return;
                }
                // 3. Tablicowanie warto��i 
                bjTablicowanieWarto�ciFunkcjiFx(bjXd, bjXg, bjH);
                // 4. Wype�nienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 5. Wype�nienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualno��i danych
                bjFlagaAktualnosciDanych = true;
            }
            // 7. Odkrycie kontrolki Chart i zakrycie DataGridView
            bjDgvTWFx.Visible = true;
            bjChrt.Visible = false;
            // ustawienie odpowiedniego stanu przycisk�w
            bjUstawPrzyciskiDlaWizualizacji(true);
        }

        private void bjBtnWizualizacjaGraficznaFx_Click(object sender, EventArgs e)
        {
            // zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // 1. Je�eli flaga nie jest ustawiona, wystarczy odkry� kontrolk� Chart i ukry� inne,
            // je�eli jest to trzeba podj�� kroki do wizualizacji graficznej
            if (!bjFlagaAktualnosciDanych)
            {
                // 2. Pobranie danych z p�l tekstowych
                if (!bjPobranieDanychWej�ciowychDlaTablicowania(out bjXd, out bjXg, out bjH))
                {
                    // sygnalizowanie b��du
                    bjErrorProvider2.SetError(bjBtnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wej�ciowych wyst�pi� " +
                        "niedozwolony znak.");
                    // przerwanie obs�ugi zdarzenia Click: btnWizualizacjaGraficznaFx_Click
                    return;
                }
                // 3. Tablicowanie warto��i
                bjTablicowanieWarto�ciFunkcjiFx(bjXd, bjXg, bjH);
                // 4. Wype�nienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 5. Wype�nienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualno��i danych
                bjFlagaAktualnosciDanych = true;

            }
            // 7. Odkrycie kontrolki Chart i zakrycie DataGridView
            bjDgvTWFx.Visible = false;
            bjChrt.Visible = true;

            // ustawienie odpowiedniego stanu przycisk�w
            bjUstawPrzyciskiDlaWizualizacji(false);
        }

        private void bjBtnReset_Click(object sender, EventArgs e)
        {
            // zgaszenie sygnalizacji b��du
            bjErrorProvider2.Clear();
            // ukrycie i czyszczenie wszystkich wizualizacji 
            bjDgvTWFx.Visible = false;
            bjChrt.Visible = false;
            bjDgvTWFx.Rows.Clear();
            bjChrt.Series.Clear();
            // odblokowanie przycisk�w
            bjBtnObliczWarto��Fx.Enabled = true;
            bjBtnWizualizacjaGraficznaFx.Enabled = true;
            bjBtnWizualizacjaTabelarycznaFx.Enabled = true;
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
            // Ustawienie flagi aktualno��i danych na false
            bjFlagaAktualnosciDanych = false;
        }

        private void bjAnalizatorIndywidualnyForm_Closing(object sender, FormClosingEventArgs e)
        {
            // sprawdzenie czy u�ytkownik chce obej�� wychodznie z aplikacji przez kokpit
            if (!bjCzyZamknac)
            {
                // Normalne zachowanie zamykania formularza
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
            // u�ytkonik chce omin�� zamykanie aplikacji przez wychodzenie z kokpitu,
            // wy�wietla sie tylko okienko u�yte w funkcji bjZako�czDzia�anieProgramuToolStripMenuItem_Click
            else
            {
                // Pomini�cie dodatkowych dzia�a� przy zamykaniu
                e.Cancel = false;
            }
        }

        private void zapiszWierszeDanychKontrolkiDataGridViewDoPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // wygaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // Utworzenie egzemplarza okna dialogowego do zapisu pliku
            SaveFileDialog bjOknoPlikuDoZapisu = new SaveFileDialog
            {
                Title = "Wyb�r pliku do zapisania danych z kontrolki DataGridView",
                Filter = "txt files (*.txt)|*.txt|all files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };

            // Wizualizacja okna dialogowego i odczytanie informacji o wyborze pliku
            if (bjOknoPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                // Zapisanie zawarto�ci DataGridView do wybranego pliku
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
                    // Zapisanie ka�dego wiersza z DataGridView do pliku
                    for (int i = 0; i < bjDgvTWFx.Rows.Count; i++)
                    {
                        bjPlikZnakowy.Write(bjDgvTWFx.Rows[i].Cells[0].Value); // Zapisanie warto�ci z pierwszej kom�rki
                        bjPlikZnakowy.Write(";"); // Separator
                        bjPlikZnakowy.Write(bjDgvTWFx.Rows[i].Cells[1].Value); // Zapisanie warto�ci z drugiej kom�rki
                        bjPlikZnakowy.Write(";"); // Separator
                        bjPlikZnakowy.WriteLine(bjDgvTWFx.Rows[i].Cells[2].Value); // Zapisanie warto�ci z trzeciej kom�rki i przej�cie do nowej linii
                    }
                }
                catch (Exception bjB��d)
                {
                    // Wy�wietlenie komunikatu o b��dzie w przypadku wyst�pienia wyj�tku
                    MessageBox.Show("ERROR: wyst�pi� nieoczekiwany b��d podczas zapisu " +
                        "wierszy danych z kontrolki DataGridView (komunikat systemowy: " +
                        bjB��d.Message + " )");
                }
            }
        }
        private void pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();

            // Utworzenie okna dialogowego dla otwarcia (wskazania) pliku, z kt�rego zostan� pobrane dane
            OpenFileDialog bjOknoWyboru = new OpenFileDialog
            {
                Title = "Wyb�r pliku do pobrania wierszy danych dla kontrolki DataGridView",
                Filter = "txt files (*.txt)|*.txt|all files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };

            if (bjOknoWyboru.ShowDialog() == DialogResult.OK)
            {
                // 1. Wczytanie danych tekstowych z pliku do tablicy
                bjWczytajDaneTekstoweDoTablicy(bjOknoWyboru, "");
                // 2. Aktualizacja p�l tekstowych dla podg�du u�ytkownika
                bjAktualizacjaZmiennychDlaPolTekstowych();
                // 3. Wype�nienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 4. Wype�nienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualno��i danych
                bjFlagaAktualnosciDanych = true;
                // 7. Odkrycie kontrolki DataGridView i zakrycie Chart
                bjDgvTWFx.Visible = true;
                bjChrt.Visible = false;
                // 8. Zablokowanie p�l tekstowych
                bjZablokujPolaTekstowe();
                // ustawienie odpowiedniego stanu przycisk�w
                bjUstawPrzyciskiDlaWizualizacji(true);
            }
        }

        private void bjUstawPrzyciskiDlaWizualizacji(bool bjCzyTabelaryczna)
        {
            if (bjCzyTabelaryczna)
            {
                bjBtnWizualizacjaTabelarycznaFx.Enabled = false;
                bjBtnWizualizacjaGraficznaFx.Enabled = true;
            }
            else
            {
                bjBtnWizualizacjaTabelarycznaFx.Enabled = true;
                bjBtnWizualizacjaGraficznaFx.Enabled = false;
            }
        }

        private void bjWczytajDaneTekstoweDoTablicy(OpenFileDialog bjOknoWyboruPliku, string bjSuffix)
        {
            // Plik zosta� wybrany, otwarcie go w trybie strumieni znak�w
            string bjWierszDanych; // Przechowanie wiersza danych wczytanych z pliku znakowego
            string[] bjElementyWierszaDanych; // Przechowanie pojedynczych danych z wczytanego wiersza danych
            string bjNazwaPlikuDanychTekstowychDlaKontrolkiChart = bjOknoWyboruPliku.FileName + bjSuffix;
            int liczbaWierszy = File.ReadAllLines(bjNazwaPlikuDanychTekstowychDlaKontrolkiChart).Length;
            // Pomocniczo lokalna tablica
            float[,] bjTWFxLokalna = new float[liczbaWierszy, 3]; // Inicjalizacja tablicy

            // Utworzenie i otwarcie strumienia znak�w do odczytu
            using (StreamReader bjPlikZnakowy = new StreamReader(bjNazwaPlikuDanychTekstowychDlaKontrolkiChart))
            {
                try
                {
                    int NrWiersza = 0; // Ustalenie warunku brzegowego

                    // Wczytywanie wierszy z pliku znakowego a� do 'znacznika' ko�ca pliku
                    while (!bjPlikZnakowy.EndOfStream)
                    {
                        // Wczytywanie wiersza (linii) z pliku znakowego
                        bjWierszDanych = bjPlikZnakowy.ReadLine();

                        // "Rozpakowanie" (podzia�) pobranego wiersza tekstowego na cz�ci oddzielane separatorem ';'
                        bjElementyWierszaDanych = bjWierszDanych.Split(new char[] { ';', ':', '|' });

                        // Usuni�cie ewentualnych spacji w poszczeg�lnych wierszach tablicy bjElementyWierszaDanych
                        bjElementyWierszaDanych[0] = bjElementyWierszaDanych[0].Trim();
                        bjElementyWierszaDanych[1] = bjElementyWierszaDanych[1].Trim();
                        bjElementyWierszaDanych[2] = bjElementyWierszaDanych[2].Trim();

                        // Przypisanie warto�ci do tablicy bjTWFx
                        bjTWFxLokalna[NrWiersza, 0] = float.Parse(bjElementyWierszaDanych[0]);
                        bjTWFxLokalna[NrWiersza, 1] = float.Parse(bjElementyWierszaDanych[1]);
                        bjTWFxLokalna[NrWiersza, 2] = float.Parse(bjElementyWierszaDanych[2]);

                        NrWiersza++; // Zwi�kszenie licznika wierszy wpisanych do tablicy
                    }
                }
                catch (Exception bjB��d)
                {
                    // Wy�wietlenie komunikatu o b��dzie w przypadku wyst�pienia wyj�tku
                    MessageBox.Show("ERROR: wyst�pi� nieoczekiwany b��d podczas odczytu " +
                        "wierszy danych z pliku (komunikat systemowy: " +
                        bjB��d.Message + " )");
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
                // Pierwsza warto�� z tablicy to bjXd
                bjXd = bjTWFx[0, 1];

                // Ostatnia warto�� z tablicy to bjXg
                bjXg = bjTWFx[bjTWFx.GetLength(0) - 1, 1];

                // R�nica pomi�dzy kolejnymi warto�ciami w tablicy to bjH
                // Zak�adaj�c, �e warto�ci X s� r�wnomiernie roz�o�one, mo�emy obliczy� bjH jako r�nic� mi�dzy dwiema kolejnymi warto�ciami
                if (bjTWFx.GetLength(0) > 1)
                {
                    bjH = bjTWFx[1, 1] - bjTWFx[0, 1];
                }
                // aktualizacja p�l tekstowych
                bjTxtXd.Text = bjXd.ToString();
                bjTxtXg.Text = bjXg.ToString();
                bjTxtH.Text = bjH.ToString();
            }
            else
            {
                // Obs�uga przypadku, gdy tablica jest pusta lub ma mniej ni� dwa wiersze
                MessageBox.Show("Tablica bjTWFx jest pusta lub ma za ma�o danych do obliczenia zmiennych.", "B��d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usu�WierszeDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // sprawdzenie widoczno�ci kontrolki DataGridView
            if (!bjDgvTWFx.Visible)
            {
                // jest b��d
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: " +
                    "kontrolka DataGridView nie zosta�a ods�oni�ta");
                // przerwanie obs�ugi zdarzenia "Click"
                return;
            }
            DialogResult bjOknoMessage = MessageBox.Show("UWAGA: w kontrolce s� dane. Czy na pewno chcesz je utraci�?",
                this.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            // rozpoznanie reakcji u�ytkownika
            if (bjOknoMessage == DialogResult.No)
            {
                MessageBox.Show("KOMUNIKAT: polecenie pobrania danych z pliku zosta�o anulowane",
                    this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                // przerywamy dalsz� obs�ug� zdarzenia Click
                return;
            }
            // usuni�cie wierszy danych w kolekcji Rows kontrolki DataGridView
            bjDgvTWFx.Rows.Clear();
        }

        private void zapiszBitMap�KontrolkiChartWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //. 1. Zapis BitMapy do pliku 
            // utworzenie egzemplarza okna dialogowego: bjOknoPlikuDoZapisu
            SaveFileDialog bjOknoPlikuDoZapisu = new SaveFileDialog
            {
                Title = "Wyb�r pliku do zapisania BitMapy z kontrolki Chart",
                Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPEG Image|*.jpg",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };

            if (bjOknoPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                // Ustawienie formatu pliku aby u�y� funkcji SaveImage, format b�dzie jednak zale�a� od tego wybranego z okienka bjOknoPlikuDoZapisu
                ChartImageFormat bjFormat = ChartImageFormat.Bmp;
                try
                {
                    // Zapisanie wykresu jako plik graficzny
                    bjChrt.SaveImage(bjOknoPlikuDoZapisu.FileName, bjFormat);
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show($"ERROR: {ex.Message}", "B��d zapisu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                // 2. Zapis dodatkowych danych DataGridView do pliku tekstowego
                bjZapiszDaneKontrolkiDataGridViewDoPliku(bjOknoPlikuDoZapisu.FileName + ".Chart.Data");
            }
        }

        private void pobierzBitMap�ZPlikuIPodepnijDoKontrolkiChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog bjOknoWyboru = new OpenFileDialog
            {
                Title = "Wyb�r pliku graficznego do za�adowania",
                Filter = "Bitmap Image|*.bmp|PNG Image|*.png|JPEG Image|*.jpg",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };

            if (bjOknoWyboru.ShowDialog() == DialogResult.OK)
            {
                // 1. Wczytanie danych tekstowych z pliku do tablicy
                bjWczytajDaneTekstoweDoTablicy(bjOknoWyboru, ".Chart.Data");
                // 2. Aktualizacja p�l tekstowych dla podg�du u�ytkownika
                bjAktualizacjaZmiennychDlaPolTekstowych();
                // 3. Wype�nienie Kontrolki Chart danymi
                bjWpiszWierszeDanychDoKontrolkiChart(bjTWFx, ref bjChrt);
                // 4. Wype�nienie Kontrolki DataGridView danymi
                bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
                // 6. Ustawienie flagi aktualno��i danych
                bjFlagaAktualnosciDanych = true;
                // 7. Odkrycie kontrolki Chart i zakrycie DataGridView
                bjDgvTWFx.Visible = false;
                bjChrt.Visible = true;
                // ustawienie odpowiedniego stanu przycisk�w
                bjUstawPrzyciskiDlaWizualizacji(false);
                // zablokowanie p�l tekstowych
                bjZablokujPolaTekstowe();
            }
        }

        
        private void bjZamknijFormularziPrzejd�DoMenuG��wnegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult bjOknoWiadomosci =
                    MessageBox.Show("Czy na pewno chcesz zamkn�� ten formularz?",
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

        private void bjZako�czDzia�anieProgramuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ustawienie flagi informuj�cej o zamykaniu aplikacji do pomi�cia wychodzenia z formularza AnalizatoraIndywidualnego
            AnalizatorIndywidualny.bjCzyZamknac = true;
            Application.Exit();
        }

        private void bjZmianaFormatuCzcionki_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            if (bjElementMenu != null)
            {
                string bjNazwaCzcionki = bjElementMenu.Text;
                if (bjNazwaCzcionki == "Domy�lna (Microsoft Sans Serif)")
                {
                    bjDgvTWFx.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F); // lub inna domy�lna czcionka
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
                    if (bjNazwaKoloru == "Domy�lny")
                    {
                        bjDgvTWFx.DefaultCellStyle.ForeColor = Color.Black; // lub inny domy�lny kolor
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

            // Domy�lna czcionka do zastosowania
            Font bjNowaCzcionka;
            if (bjNazwaCzcionki == "Domy�lna (Microsoft Sans Serif)")
            {
                bjNowaCzcionka = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            }
            else
            {
                bjNowaCzcionka = new Font(bjNazwaCzcionki, 12, FontStyle.Regular);
            }

            // Ustawienie czcionki dla tytu�u wykresu (sprawdzenie, czy istniej� tytu�y)
            if (bjChrt.Titles.Count > 0)
            {
                bjChrt.Titles[0].Font = bjNowaCzcionka;
            }

            // Ustawienie czcionki dla legendy wykresu (sprawdzenie, czy istniej� legendy)
            if (bjChrt.Legends.Count > 0)
            {
                bjChrt.Legends[0].Font = bjNowaCzcionka;
            }

            // Ustawienie czcionki dla osi X (sprawdzenie, czy istniej� obszary wykresu)
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

            // Okre�lenie stylu linii na podstawie wybranego tekstu
            foreach (var series in bjChrt.Series)
            {
                switch (bjWybranyStyl)
                {
                    case "Ci�g�a":
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                        break;
                    case "Kreskowana":
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                        series.BorderWidth = 2; // Grubsza kreska dla lepszej widoczno�ci
                        break;
                    case "Kropkowana":
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
                        series.BorderWidth = 2; // Grubsza kreska dla lepszej widoczno�ci
                        break;
                    default:
                        series.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                        break;
                }
            }
        }

        private void bjZmianaKoloruT�aWykresuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bjZmienKolor(sender, bjChrt, (control, bjColor) => ((Chart)control).BackColor = bjColor); 
        }

        private void bjZmianaTypuWykresuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem bjElementMenu = sender as ToolStripMenuItem;
            string bjWybranyTypWykresu = bjElementMenu.Text;

            // Okre�lenie typu wykresu na podstawie wybranego tekstu
            System.Windows.Forms.DataVisualization.Charting.SeriesChartType bjTypWykresu;

            switch (bjWybranyTypWykresu)
            {
                case "Liniowy":
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    break;
                case "Kolumnowy":
                    bjTypWykresu = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    break;
                case "S�upkowy":
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

        private void bjUsuni�cieWykresuBitMapyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Wyczy�� wszystkie dane wykresu zamiast jego usuwania
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
                case "��ty":
                    return Color.Yellow;
                case "Pomara�czowy":
                    return Color.Orange;
                case "Fioletowy":
                    return Color.Purple;
                case "R�owy":
                    return Color.Pink;
                case "Bia�y":
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