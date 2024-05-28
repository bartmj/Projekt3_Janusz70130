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
            if (!bjSpróbujPobraæFloatIObs³ó¿bjB³¹d(bjTxtX.Text, out float bjX, bjTxtX, "X"))
            {
                return;
            }
            // zgszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
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


        private bool bjSpróbujPobraæFloatIObs³ó¿bjB³¹d(string bjText, out float bjLiczbaFloat, Control bjKontrolka, string bjNazwaWspó³czynnika)
        {
            if (!float.TryParse(bjText, out bjLiczbaFloat))
            {
                bjErrorProvider2.SetError(bjKontrolka, $"ERROR: w zapisie wartoœci wspó³czynnika '{bjNazwaWspó³czynnika}' wyst¹pi³ niedozwolony znak!");
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
            bjErrorProvider2.Dispose();
            // 2. Pobranie danych wejœciowych 
            if (!bjPobranieDanychWejœciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH))
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
            bjTablicowanieWartoœciFunkcjiFx(bjXd, bjXg, bjH, out float[,] bjTWFx);
            // 5. Wpisanie do Kontrolki DataGridView wierszy danych tablicy zmian wartoœci F(X)
            // wywo³anie metody przepisania wierszy tablicy TWFx do kontrolki DataGridView
            bjWpiszWierszeDanychDoKontrolkiDataGridView(bjTWFx, ref bjDgvTWFx);
            // modyfikator ref oznacza, ¿e dany parametr metordy jest parametrem wejœciowo-wyjœciowym
        }

        private void bjBtnWizualizacjaGraficznaFx_Click(object sender, EventArgs e)
        {

            // 1. Zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            // 2. Pobranie danych wejœciowych 
            if (!bjPobranieDanychWejœciowychDlaTablicowania(out float bjXd, out float bjXg, out float bjH))
            {
                // by³ b³¹d, to go sygnalizuje 
                bjErrorProvider2.SetError(bjBtnWizualizacjaGraficznaFx, "ERROR: w zapisie danych wejœciowych wyst¹pi³ " +
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

        private void zapiszWierszeDanychKontrolkiDataGridViewWPlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // wygaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            if (bjDgvTWFx == null || !bjDgvTWFx.Visible || bjDgvTWFx.Rows.Count <= 0)
            {
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx, "ERROR: " +
                    " polecenie nie mo¿e byæ zrealizowane bo Kontrolka DataGridView jest " +
                    " niewidoczna lub pusta");
                // przerwanie dalszej obs³ugi zdarzenia Click
                return;
            }

            // utworzenie egzemplarza okna dialogowego: bjOknoPlikuDoZapisu
            SaveFileDialog OknoPlikuDoZapisu = new SaveFileDialog
            {
                Title = "Wybór pliku do wpisania wierszy danych z kontrolki " +
                "DataGridView",
                Filter = "txtfiles(*.txt)|*.txt|all files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true,
                InitialDirectory = "C:\\"
            };
            // wizualizacja okna bjOknoPlikuDoZapisu i odczytanie informacji o wyborze pliku
            if (OknoPlikuDoZapisu.ShowDialog() == DialogResult.OK)
            {
                // plik zosta³ wybrany lub zosta³ utworzony nowy plik 
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
                catch (Exception bjB³¹d)
                {
                    MessageBox.Show("ERROR: wyst¹pi³ nieoczekiwany b³¹d podczas zapisu " +
                        "wierszy danych z kontrolki DataGridView (komunikat systemowy: " +
                        bjB³¹d.Message + " )");
                }
                finally
                {
                    bjPlikZnakowy.Close();
                    bjPlikZnakowy.Dispose();
                }
                // zmkniêcie okna dialogowego bjOknoPlikuDoZapisu 
                OknoPlikuDoZapisu.Dispose();
            }
        }

        private void pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // zgaszenie kontrolki errorProvider
            bjErrorProvider2.Dispose();
            // sprawdzenie, czy kontrolka DataGridView jest ods³oniêta
            if (!bjDgvTWFx.Visible)
            {
                // kontrolka DataGridView nie jest ods³oniêta
                bjErrorProvider2.SetError(bjBtnWizualizacjaTabelarycznaFx,
                    "ERROR: operacja nie mo¿e byæ zrealizowana, gdy¿ kontrolka " +
                    "DataGridView nie jest ods³oniêta (nie jest widoczna na formularzu)");
                // przerwanie dalszej obs³ugi zdarzenia Click:
                return;
            }

            // usuniêcie danych w kontrolce DataGridView
            bjDgvTWFx.Rows.Clear();
            // wycentrowanie zapisów w poszczególnych komórkach (kolumnach) kontrolki DataGridView
            bjDgvTWFx.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            bjDgvTWFx.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            bjDgvTWFx.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            /* utworzenie okna dialogowego dla otwarcia (wskazania) pliku, z którego zostan¹ pobrane dane i wpisane do kontrolki DataGridView */
            OpenFileDialog bjOknoWyboruPiku = new OpenFileDialog();
            // ustalenie tytu³u okna dialogowego
            bjOknoWyboruPiku.Title = "Wybór pliku do pobrania wierszy danych dla kontrolki DataGridView";
            // ustawienie filtru dla "pokazywania" plików
            bjOknoWyboruPiku.Filter = "txtfiles (*.txt)|*.txt|all files (*.*)|*.*";
            // ustawienie filtru domyœlnego: *.txt
            bjOknoWyboruPiku.FilterIndex = 1;
            // przywrócenie bie¿¹cego ustawienia po zamkniêciu okna dialogowego
            bjOknoWyboruPiku.RestoreDirectory = true;
            // ustawienie domyœlnego dysku, gdzie jest plik do pobrania
            bjOknoWyboruPiku.InitialDirectory = "C:\\";

            // wizualizacja okna dialogowego: bjOknoWyboruPiku
            if (bjOknoWyboruPiku.ShowDialog() == DialogResult.OK)
            {
                // plik zosta³ wybrany, to musimy go otworzyæ w trybie strumieni znaków
                // musimy przy tym pamiêtaæ jak ten plik zosta³ zapisany:
                // jako ci¹g wierszy, w którym poszczególne dane liczbowe oddzielone s¹ œrednikiem
                // deklaracje pomocnicze
                string bjWierszDanych; // dla przechowania wiersza danych (³añcucha znaków) wczytanych z pliku znakowego
                string[] bjElementyWierszaDanych; // dla przechowania pojedynczych danych (liczby), które s¹ zapisane w tym wczytanym wierszu danych: bjWierszDanych

                // Krok 1: utworzenie i otwarcie egzemplarza strumienia znaków do odczytu,
                // co umo¿liwi wykonywanie na nim operacji "podobnych" do operacji wykonywanych w oknie konsoli: Console, co poznaliœmy podczas realizacji Projektu Nr1
                StreamReader bjPlikZnakowy = new StreamReader(bjOknoWyboruPiku.FileName);
                //lub: new StreamReader(OknoOdczytuPliku.OpenFile());

                // Krok 2: odczytywanie pliku znakowego "wiersz po wierszu" i wpisanie danych do kontrolki DataGridView
                try
                {
                    int NrWiersza = 0; // ustalenie warunku brzegowego
                                       // wczytywanie wierszy z pliku znakowego a¿ do 'znacznika' koñca pliku
                    while (!bjPlikZnakowy.EndOfStream)
                    {
                        // wczytywanie wiersza (linii) z pliku znakowego
                        bjWierszDanych = bjPlikZnakowy.ReadLine();
                        // "rozpakowanie" (jego podzia³) pobranego wiersza tekstowego na "czêœci", które s¹ oddzielane separatorem (znakiem) ';'
                        bjElementyWierszaDanych = bjWierszDanych.Split(';');
                        // gdy dane w wierszu mog¹ byæ oddzielone ró¿nymi separatorami (na przyk³ad, jednym z separatorów: ; lub : lub |),
                        // to 'rozpakowanie' stringu bjWierszDanych (liczb) zapisaliœmy tak:
                        bjElementyWierszaDanych = bjWierszDanych.Split(new char[] { ';', ':', '|' });

                        // w wierszach tablicy bjElementyWierszaDanych bêd¹ sk³adniki wczytane z pliku:
                        // Numer przedzia³u; wartoœæ X; wartoœæ F(X)
                        // usuniêcie ewentualnych spacji w poszczególnych wierszach tablicy bjElementyWierszaDanych
                        bjElementyWierszaDanych[0].Trim();
                        bjElementyWierszaDanych[1].Trim();
                        bjElementyWierszaDanych[2].Trim();
                        // dodanie nowego wiersza do kolekcji wierszy Rows kontrolki DataGridView
                        bjDgvTWFx.Rows.Add();
                        // wpisanie danych do nowego (dodadnego) wiersza kontrolki DataGridView

                        // numer przedzia³u
                        bjDgvTWFx.Rows[NrWiersza].Cells[0].Value = bjElementyWierszaDanych[0];
                        // wartoœci zmiennej X
                        bjDgvTWFx.Rows[NrWiersza].Cells[1].Value = bjElementyWierszaDanych[1];
                        // wartoœci funkcji F(X)
                        bjDgvTWFx.Rows[NrWiersza].Cells[2].Value = bjElementyWierszaDanych[2];
                        NrWiersza++; // zwiêkszenie licznika wierszy wpisanych do kontrolki DataGridView
                    }

                    // Krok 3: ods³oniêcie kontrolki DataGridView (+ ukrycie kontrolki chtyWykresFx)
                    bjDgvTWFx.Visible = true;
                    bjChrt.Visible = false; // ukrycie kontrolki Chart
                    // ustawienie braku aktywnoœci, w pozycji Plik menu poziomego, polecenia 'pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem'
                    // pobierzZPlikuWierszeDanychDoKontrolkiDataGridViewToolStripMenuItem.Enabled = false;
                }
                catch (IndexOutOfRangeException bjB³¹d1)
                {
                    // powiadomienie u¿ytkownika o zaistnia³ym b³êdzie
                    MessageBox.Show("ERROR: wyst¹pi³o przekroczenie wartoœci indeksu" +
                                    "wierszy danych kontrolki DataGridView (zg³oszony komunikat systemowy: " + bjB³¹d1.Message + " )");
                }
                catch (IOException bjB³¹d2)
                {
                    // powiadomienie u¿ytkownika o zaistnia³ym b³êdzie
                    MessageBox.Show("ERROR: wyst¹pi³ nieoczekiwany b³¹d przy pobieraniu" +
                                    "(wczytywaniu) wierszy danych z pliku " +
                                    "(zg³oszony komunikat systemowy: " + bjB³¹d2.Message + " )");
                }
                finally
                {
                    // zamkniêcie i zwolnienie przydzielonych zasobów (zwi¹zanych z operacjami na pliku)
                    bjPlikZnakowy.Close();
                    // zwolnienie pliku
                    bjPlikZnakowy.Dispose();
                }
            }
            else
            {
                // wyœwietlenie komunikatu o niewybraniu pliku do odczytu
                MessageBox.Show("UWAGA: plik do odczytu nie zosta³ wybrany i operacje " +
                                "pobrania danych z pliku nie mo¿e byæ zrealizowana", this.Name, MessageBoxButtons.OK);
            }

            // zwolnienie okna dialogowego: bjOknoWyboruPiku
            bjOknoWyboruPiku.Dispose();

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
                bjChrt.SaveImage(bjOknoPlikuDoZapisu.FileName, bjFormat);
            }
        }
    }
}
