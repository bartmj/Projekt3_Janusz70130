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
            bjTxtFX.Text = String.Format("{0:F3}", bjWarto��Funkcji);
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


        #region funkcje pomocnicze analizatora indywidualnego
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
            bjErrorProvider2.Dispose();
            // 2. Pobranie danych wej�ciowych 
            if (!bjPobranieDanychWej�ciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH))
            {
                // by� b��d, to go sygnalizuje 
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: w zapisie danych wej�ciowych wyst�pi� " +
                    "niedozwolony znak.");
                // przerwanie obs�ugi zdarzenia Click: bjBtnWizualizacjaTabelarycznaFx_Click
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
            bjErrorProvider2.Dispose();
            // 2. Pobranie danych wej�ciowych 
            if (!bjPobranieDanychWej�ciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH))
            {
                // by� b��d, to go sygnalizuje 
                bjErrorProvider2.SetError(bjBtnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wej�ciowych wyst�pi� " +
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
            bjErrorProvider2.Clear();
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

        private void zapiszWierszeDanychKontrolkiDataGridViewWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // wygaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            if (bjDgvTWFx == null || !bjDgvTWFx.Visible || bjDgvTWFx.Rows.Count <= 0)
            {
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: " +
                    " polecenie nie mo�e by� zrealizowane bo Kontrolka DataGridView jest " +
                    " niewidoczna lub pusta");
                // przerwanie dalszej obs�ugi zdarzenia Click
                return;
            }

            // utworzenie egzemplarza okna dialogowego: bjOknoPlikuDoZapisu
            SaveFileDialog OknoPlikuDoZapisu = new SaveFileDialog
            {
                Title = "Wyb�r pliku do wpisania wierszy danych z kontrolki " +
                "DataGridView",
                Filter = "txtfiles(*.txt)|*.txt|all files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };
            // wizualizacja okna bjOknoPlikuDoZapisu i odczytanie informacji o wyborze pliku
            if (OknoPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                // plik zosta� wybrany lub zosta� utworzony nowy plik 
                // utworzenie egzemplarza strumienia do zapisu 
                StreamWriter bjPlikZnakowy = new StreamWriter(OknoPlikuDoZapisu.FileName);
                try
                { // wypisaywanie do pliku wierszy danych kontrolki DataGridView
                    for (int i = 0; i < bjDgvTWFx.Rows.Count; i++)
                    {
                        // wpisanie do pliku i-tego wiersza kontrolki DataGridView
                        bjPlikZnakowy.Write(bjDgvTWFx.Rows[i].Cells[0].Value);
                        bjPlikZnakowy.Write(";");
                        bjPlikZnakowy.Write(bjDgvTWFx.Rows[i].Cells[1].Value);
                        bjPlikZnakowy.Write(";");
                        bjPlikZnakowy.WriteLine(bjDgvTWFx.Rows[i].Cells[2].Value);
                    }
                }
                catch (Exception bjB��d)
                {
                    MessageBox.Show("ERROR: wyst�pi� nieoczekiwany b��d podczas zapisu " +
                        "wierszy danych z kontrolki DataGridView (komunikat systemowy: " +
                        bjB��d.Message + " )");
                }
                finally
                {
                    bjPlikZnakowy.Close();
                    bjPlikZnakowy.Dispose();
                }
                // zmkni�cie okna dialogowego bjOknoPlikuDoZapisu 
                OknoPlikuDoZapisu.Dispose();
            }
        }

        private void pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            // sprawdzenie, czy kontrolka DataGridView jest ods�oni�ta
            if (!bjDgvTWFx.Visible)
            {
                // kontrolka DataGridView nie jest ods�oni�ta
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx,
                    "ERROR: operacja nie mo�e by� zrealizowana, gdy� kontrolka " +
                    "DataGridView nie jest ods�oni�ta (nie jest widoczna na formularzu)");
                // przerwanie dalszej obs�ugi zdarzenia Click:
                return;
            }

            // usuni�cie danych w kontrolce DataGridView
            bjDgvTWFx.Rows.Clear();
            // wycentrowanie zapis�w w poszczeg�lnych kom�rkach (kolumnach) kontrolki DataGridView
            bjDgvTWFx.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            bjDgvTWFx.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            bjDgvTWFx.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            /* utworzenie okna dialogowego dla otwarcia (wskazania) pliku, z kt�rego zostan� pobrane dane i wpisane do kontrolki DataGridView */
            OpenFileDialog bjOknoWyboruPiku = new OpenFileDialog();
            // ustalenie tytu�u okna dialogowego
            bjOknoWyboruPiku.Title = "Wyb�r pliku do pobrania wierszy danych dla kontrolki DataGridView";
            // ustawienie filtru dla "pokazywania" plik�w
            bjOknoWyboruPiku.Filter = "txtfiles (*.txt)|*.txt|all files (*.*)|*.*";
            // ustawienie filtru domy�lnego: *.txt
            bjOknoWyboruPiku.FilterIndex = 1;
            // przywr�cenie bie��cego ustawienia po zamkni�ciu okna dialogowego
            bjOknoWyboruPiku.RestoreDirectory = true;
            // ustawienie domy�lnego dysku, gdzie jest plik do pobrania
            bjOknoWyboruPiku.InitialDirectory = "C:\\";

            // wizualizacja okna dialogowego: bjOknoWyboruPiku
            if (bjOknoWyboruPiku.ShowDialog() == DialogResult.OK)
            {
                // plik zosta� wybrany, to musimy go otworzy� w trybie strumieni znak�w
                // musimy przy tym pami�ta� jak ten plik zosta� zapisany:
                // jako ci�g wierszy, w kt�rym poszczeg�lne dane liczbowe oddzielone s� �rednikiem
                // deklaracje pomocnicze
                string bjWierszDanych; // dla przechowania wiersza danych (�a�cucha znak�w) wczytanych z pliku znakowego
                string[] bjElementyWierszaDanych; // dla przechowania pojedynczych danych (liczby), kt�re s� zapisane w tym wczytanym wierszu danych: bjWierszDanych

                // Krok 1: utworzenie i otwarcie egzemplarza strumienia znak�w do odczytu,
                // co umo�liwi wykonywanie na nim operacji "podobnych" do operacji wykonywanych w oknie konsoli: Console, co poznali�my podczas realizacji Projektu Nr1
                StreamReader bjPlikZnakowy = new StreamReader(bjOknoWyboruPiku.FileName);
                //lub: new StreamReader(OknoOdczytuPliku.OpenFile());

                // Krok 2: odczytywanie pliku znakowego "wiersz po wierszu" i wpisanie danych do kontrolki DataGridView
                try
                {
                    int NrWiersza = 0; // ustalenie warunku brzegowego
                                       // wczytywanie wierszy z pliku znakowego a� do 'znacznika' ko�ca pliku
                    while (!bjPlikZnakowy.EndOfStream)
                    {
                        // wczytywanie wiersza (linii) z pliku znakowego
                        bjWierszDanych = bjPlikZnakowy.ReadLine();
                        // "rozpakowanie" (jego podzia�) pobranego wiersza tekstowego na "cz�ci", kt�re s� oddzielane separatorem (znakiem) ';'
                        bjElementyWierszaDanych = bjWierszDanych.Split(';');
                        // gdy dane w wierszu mog� by� oddzielone r�nymi separatorami (na przyk�ad, jednym z separator�w: ; lub : lub |),
                        // to 'rozpakowanie' stringu bjWierszDanych (liczb) zapisali�my tak:
                        bjElementyWierszaDanych = bjWierszDanych.Split(new char[] { ';', ':', '|' });

                        // w wierszach tablicy bjElementyWierszaDanych b�d� sk�adniki wczytane z pliku:
                        // Numer przedzia�u; warto�� X; warto�� F(X)
                        // usuni�cie ewentualnych spacji w poszczeg�lnych wierszach tablicy bjElementyWierszaDanych
                        bjElementyWierszaDanych[0].Trim();
                        bjElementyWierszaDanych[1].Trim();
                        bjElementyWierszaDanych[2].Trim();
                        // dodanie nowego wiersza do kolekcji wierszy Rows kontrolki DataGridView
                        bjDgvTWFx.Rows.Add();
                        // wpisanie danych do nowego (dodadnego) wiersza kontrolki DataGridView

                        // numer przedzia�u
                        bjDgvTWFx.Rows[NrWiersza].Cells[0].Value = bjElementyWierszaDanych[0];
                        // warto�ci zmiennej X
                        bjDgvTWFx.Rows[NrWiersza].Cells[1].Value = bjElementyWierszaDanych[1];
                        // warto�ci funkcji F(X)
                        bjDgvTWFx.Rows[NrWiersza].Cells[2].Value = bjElementyWierszaDanych[2];
                        NrWiersza++; // zwi�kszenie licznika wierszy wpisanych do kontrolki DataGridView
                    }

                    // Krok 3: ods�oni�cie kontrolki DataGridView (+ ukrycie kontrolki chtyWykresFx)
                    bjDgvTWFx.Visible = true;
                    bjChrt.Visible = false; // ukrycie kontrolki Chart
                    // ustawienie braku aktywno�ci, w pozycji Plik menu poziomego, polecenia 'pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem'
                    // pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem.Enabled = false;
                }
                catch (IndexOutOfRangeException bjB��d1)
                {
                    // powiadomienie u�ytkownika o zaistnia�ym b��dzie
                    MessageBox.Show("ERROR: wyst�pi�o przekroczenie warto�ci indeksu" +
                                    "wierszy danych kontrolki DataGridView (zg�oszony komunikat systemowy: " + bjB��d1.Message + " )");
                }
                catch (IOException bjB��d2)
                {
                    // powiadomienie u�ytkownika o zaistnia�ym b��dzie
                    MessageBox.Show("ERROR: wyst�pi� nieoczekiwany b��d przy pobieraniu" +
                                    "(wczytywaniu) wierszy danych z pliku " +
                                    "(zg�oszony komunikat systemowy: " + bjB��d2.Message + " )");
                }
                finally
                {
                    // zamkni�cie i zwolnienie przydzielonych zasob�w (zwi�zanych z operacjami na pliku)
                    bjPlikZnakowy.Close();
                    // zwolnienie pliku
                    bjPlikZnakowy.Dispose();
                }
            }
            else
            {
                // wy�wietlenie komunikatu o niewybraniu pliku do odczytu
                MessageBox.Show("UWAGA: plik do odczytu nie zosta� wybrany i operacje " +
                                "pobrania danych z pliku nie mo�e by� zrealizowana", this.Name, MessageBoxButtons.OK);
            }

            // zwolnienie okna dialogowego: bjOknoWyboruPiku
            bjOknoWyboruPiku.Dispose();

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
                bjChrt.SaveImage(bjOknoPlikuDoZapisu.FileName, bjFormat);
            }
        }
    }
}
