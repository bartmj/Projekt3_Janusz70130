namespace Projekt2_Janusz70130
{
    partial class AnalizatorLaboratoryjny
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.label1 = new System.Windows.Forms.Label();
            this.btnObliczWartoœæFx = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtC = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWartoœæRównania = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnWizualizacjaTabelarycznaFx = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtXg = new System.Windows.Forms.TextBox();
            this.txtXd = new System.Windows.Forms.TextBox();
            this.txth = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvTWFx = new System.Windows.Forms.DataGridView();
            this.NrPrzedzia³u = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WartoœæX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WartoœæFx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnReset = new System.Windows.Forms.Button();
            this.chrtFx = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnWizualizacjaGraficznaFx = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTWFx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtFx)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(235, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(537, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Analizator Laboratoryjny równania kwadratowego";
            // 
            // btnObliczWartoœæFx
            // 
            this.btnObliczWartoœæFx.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnObliczWartoœæFx.Location = new System.Drawing.Point(793, 151);
            this.btnObliczWartoœæFx.Name = "btnObliczWartoœæFx";
            this.btnObliczWartoœæFx.Size = new System.Drawing.Size(230, 90);
            this.btnObliczWartoœæFx.TabIndex = 15;
            this.btnObliczWartoœæFx.Text = "Obliczenie wartoœci równania kwadratowego funkcji f(x)";
            this.btnObliczWartoœæFx.UseVisualStyleBackColor = true;
            this.btnObliczWartoœæFx.Click += new System.EventHandler(this.btnObliczWartoœæFx_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtB);
            this.groupBox1.Controls.Add(this.txtC);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtA);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(12, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 176);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wspó³czynniki równania kwadratowego";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(24, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 22);
            this.label2.TabIndex = 9;
            this.label2.Text = "a:";
            // 
            // txtB
            // 
            this.txtB.Location = new System.Drawing.Point(63, 85);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(60, 26);
            this.txtB.TabIndex = 4;
            // 
            // txtC
            // 
            this.txtC.Location = new System.Drawing.Point(63, 126);
            this.txtC.Name = "txtC";
            this.txtC.Size = new System.Drawing.Size(60, 26);
            this.txtC.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(26, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 22);
            this.label4.TabIndex = 7;
            this.label4.Text = "c:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(25, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 22);
            this.label3.TabIndex = 6;
            this.label3.Text = "b:";
            // 
            // txtA
            // 
            this.txtA.Location = new System.Drawing.Point(63, 45);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(60, 26);
            this.txtA.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(12, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 38);
            this.label5.TabIndex = 6;
            this.label5.Text = "Wartoœæ zmiennej \r\nniezale¿nej x";
            // 
            // txtX
            // 
            this.txtX.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.txtX.Location = new System.Drawing.Point(16, 277);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 26);
            this.txtX.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(789, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(246, 38);
            this.label6.TabIndex = 8;
            this.label6.Text = "Obliczona wartoœæ równania \r\nkwadratowego dla podanej zmiennej X";
            // 
            // txtWartoœæRównania
            // 
            this.txtWartoœæRównania.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWartoœæRównania.Location = new System.Drawing.Point(793, 90);
            this.txtWartoœæRównania.Name = "txtWartoœæRównania";
            this.txtWartoœæRównania.Size = new System.Drawing.Size(100, 29);
            this.txtWartoœæRównania.TabIndex = 14;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // btnWizualizacjaTabelarycznaFx
            // 
            this.btnWizualizacjaTabelarycznaFx.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnWizualizacjaTabelarycznaFx.Location = new System.Drawing.Point(793, 247);
            this.btnWizualizacjaTabelarycznaFx.Name = "btnWizualizacjaTabelarycznaFx";
            this.btnWizualizacjaTabelarycznaFx.Size = new System.Drawing.Size(230, 90);
            this.btnWizualizacjaTabelarycznaFx.TabIndex = 16;
            this.btnWizualizacjaTabelarycznaFx.Text = "Wizualizacja tabelaryczna zmian wartoœci równania kwadratowego w podanym przedzia" +
    "le zmian X\r\n";
            this.btnWizualizacjaTabelarycznaFx.UseVisualStyleBackColor = true;
            this.btnWizualizacjaTabelarycznaFx.Click += new System.EventHandler(this.btnWizualizacjaTabelarycznaFx_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtXg);
            this.groupBox2.Controls.Add(this.txtXd);
            this.groupBox2.Controls.Add(this.txth);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.Location = new System.Drawing.Point(16, 313);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(196, 192);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Przedzia³ zmian wartoœci zmiennej X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(20, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 22);
            this.label7.TabIndex = 15;
            this.label7.Text = "Xd";
            // 
            // txtXg
            // 
            this.txtXg.Location = new System.Drawing.Point(59, 86);
            this.txtXg.Name = "txtXg";
            this.txtXg.Size = new System.Drawing.Size(60, 26);
            this.txtXg.TabIndex = 11;
            // 
            // txtXd
            // 
            this.txtXd.Location = new System.Drawing.Point(59, 46);
            this.txtXd.Name = "txtXd";
            this.txtXd.Size = new System.Drawing.Size(60, 26);
            this.txtXd.TabIndex = 10;
            // 
            // txth
            // 
            this.txth.Location = new System.Drawing.Point(59, 127);
            this.txth.Name = "txth";
            this.txth.Size = new System.Drawing.Size(60, 26);
            this.txth.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(21, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 22);
            this.label9.TabIndex = 12;
            this.label9.Text = "Xg";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(22, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 22);
            this.label8.TabIndex = 13;
            this.label8.Text = "h";
            // 
            // dgvTWFx
            // 
            this.dgvTWFx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTWFx.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NrPrzedzia³u,
            this.WartoœæX,
            this.WartoœæFx});
            this.dgvTWFx.Location = new System.Drawing.Point(257, 79);
            this.dgvTWFx.Name = "dgvTWFx";
            this.dgvTWFx.Size = new System.Drawing.Size(493, 426);
            this.dgvTWFx.TabIndex = 16;
            this.dgvTWFx.Visible = false;
            // 
            // NrPrzedzia³u
            // 
            this.NrPrzedzia³u.HeaderText = "Numer przedzia³u dla zmiennej X";
            this.NrPrzedzia³u.Name = "NrPrzedzia³u";
            this.NrPrzedzia³u.Width = 80;
            // 
            // WartoœæX
            // 
            this.WartoœæX.HeaderText = "Wartoœæ zmiennej niezale¿nej X";
            this.WartoœæX.Name = "WartoœæX";
            this.WartoœæX.Width = 150;
            // 
            // WartoœæFx
            // 
            this.WartoœæFx.HeaderText = "Wartoœæ funkcji F(x)";
            this.WartoœæFx.Name = "WartoœæFx";
            this.WartoœæFx.Width = 220;
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnReset.Location = new System.Drawing.Point(793, 439);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(230, 66);
            this.btnReset.TabIndex = 18;
            this.btnReset.Text = "RESETUJ\r\n(ustaw stan pocz¹tkowy)";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // chrtFx
            // 
            this.chrtFx.BackColor = System.Drawing.Color.LightSkyBlue;
            chartArea1.AxisX.Title = "Wartoœæ X";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisX2.TitleFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisY.Title = "Wartoœæ równania";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.AxisY2.TitleFont = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 72.87241F;
            chartArea1.Position.Width = 84F;
            chartArea1.Position.X = 3F;
            chartArea1.Position.Y = 12.8231F;
            this.chrtFx.ChartAreas.Add(chartArea1);
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            legend1.TitleFont = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chrtFx.Legends.Add(legend1);
            this.chrtFx.Location = new System.Drawing.Point(257, 79);
            this.chrtFx.Margin = new System.Windows.Forms.Padding(0);
            this.chrtFx.Name = "chrtFx";
            series1.ChartArea = "ChartArea1";
            series1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chrtFx.Series.Add(series1);
            this.chrtFx.Size = new System.Drawing.Size(493, 290);
            this.chrtFx.TabIndex = 19;
            this.chrtFx.Text = "chart1";
            title1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            title1.Name = "Wykres zmian wartoœci równania kwadratowego";
            title1.Text = "Wykres zmian wartoœci równania kwadratowego";
            this.chrtFx.Titles.Add(title1);
            this.chrtFx.Visible = false;
            // 
            // btnWizualizacjaGraficznaFx
            // 
            this.btnWizualizacjaGraficznaFx.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnWizualizacjaGraficznaFx.Location = new System.Drawing.Point(793, 343);
            this.btnWizualizacjaGraficznaFx.Name = "btnWizualizacjaGraficznaFx";
            this.btnWizualizacjaGraficznaFx.Size = new System.Drawing.Size(230, 90);
            this.btnWizualizacjaGraficznaFx.TabIndex = 20;
            this.btnWizualizacjaGraficznaFx.Text = "Wizualizacja graficzna zmian wartoœci równania kwadratowego";
            this.btnWizualizacjaGraficznaFx.UseVisualStyleBackColor = true;
            this.btnWizualizacjaGraficznaFx.Click += new System.EventHandler(this.btnWizualizacjaGraficznaFx_Click);
            // 
            // AnalizatorLaboratoryjny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 557);
            this.Controls.Add(this.btnWizualizacjaGraficznaFx);
            this.Controls.Add(this.chrtFx);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.dgvTWFx);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnWizualizacjaTabelarycznaFx);
            this.Controls.Add(this.txtWartoœæRównania);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnObliczWartoœæFx);
            this.Controls.Add(this.label1);
            this.Name = "AnalizatorLaboratoryjny";
            this.Text = "Analizator Laboratoryjny";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnalizatorLaboratoryjnyForm_Closing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTWFx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chrtFx)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnObliczWartoœæFx;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWartoœæRównania;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnWizualizacjaTabelarycznaFx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtXg;
        private System.Windows.Forms.TextBox txtXd;
        private System.Windows.Forms.TextBox txth;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvTWFx;
        private System.Windows.Forms.DataGridViewTextBoxColumn NrPrzedzia³u;
        private System.Windows.Forms.DataGridViewTextBoxColumn WartoœæX;
        private System.Windows.Forms.DataGridViewTextBoxColumn WartoœæFx;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataVisualization.Charting.Chart chrtFx;
        private System.Windows.Forms.Button btnWizualizacjaGraficznaFx;
    }
}
