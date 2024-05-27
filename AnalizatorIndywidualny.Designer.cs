namespace Projekt2_Janusz70130
{
    partial class AnalizatorIndywidualny
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalizatorIndywidualny));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.bjTxtX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bjTxtFX = new System.Windows.Forms.TextBox();
            this.bjBtnObliczWartoœæFx = new System.Windows.Forms.Button();
            this.bjBtnWizualizacjaTabelarycznaFx = new System.Windows.Forms.Button();
            this.bjBtnWizualizacjaGraficznaFx = new System.Windows.Forms.Button();
            this.bjBtnReset = new System.Windows.Forms.Button();
            this.bjDgvTWFx = new System.Windows.Forms.DataGridView();
            this.bjNrPrzedzia³u = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bjWartoœæX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bjWartoœæFx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bjTxtXg = new System.Windows.Forms.TextBox();
            this.bjTxtXd = new System.Windows.Forms.TextBox();
            this.bjTxtH = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bjChrt = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bjDgvTWFx)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bjChrt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wartoœæ zmiennej \r\nniezale¿nej X";
            // 
            // bjTxtX
            // 
            this.bjTxtX.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjTxtX.Location = new System.Drawing.Point(77, 154);
            this.bjTxtX.Name = "bjTxtX";
            this.bjTxtX.Size = new System.Drawing.Size(80, 26);
            this.bjTxtX.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(310, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(392, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Analizator indywidualny funkcji F(x)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(307, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(398, 119);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(804, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 38);
            this.label3.TabIndex = 4;
            this.label3.Text = "Obliczona wartoœæ \r\nfunkcji F(X) w punkcie X";
            // 
            // bjTxtFX
            // 
            this.bjTxtFX.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjTxtFX.Location = new System.Drawing.Point(846, 154);
            this.bjTxtFX.Name = "bjTxtFX";
            this.bjTxtFX.Size = new System.Drawing.Size(80, 26);
            this.bjTxtFX.TabIndex = 5;
            // 
            // bjBtnObliczWartoœæFx
            // 
            this.bjBtnObliczWartoœæFx.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjBtnObliczWartoœæFx.Location = new System.Drawing.Point(771, 198);
            this.bjBtnObliczWartoœæFx.Name = "bjBtnObliczWartoœæFx";
            this.bjBtnObliczWartoœæFx.Size = new System.Drawing.Size(230, 90);
            this.bjBtnObliczWartoœæFx.TabIndex = 6;
            this.bjBtnObliczWartoœæFx.Text = "Oblicz wartoœæ funkcji \r\nw punkcie X";
            this.bjBtnObliczWartoœæFx.UseVisualStyleBackColor = true;
            this.bjBtnObliczWartoœæFx.Click += new System.EventHandler(this.bjBtnObliczWartoœæFx_Click);
            // 
            // bjBtnWizualizacjaTabelarycznaFx
            // 
            this.bjBtnWizualizacjaTabelarycznaFx.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjBtnWizualizacjaTabelarycznaFx.Location = new System.Drawing.Point(771, 294);
            this.bjBtnWizualizacjaTabelarycznaFx.Name = "bjBtnWizualizacjaTabelarycznaFx";
            this.bjBtnWizualizacjaTabelarycznaFx.Size = new System.Drawing.Size(230, 90);
            this.bjBtnWizualizacjaTabelarycznaFx.TabIndex = 17;
            this.bjBtnWizualizacjaTabelarycznaFx.Text = "Wizualizacja tabelaryczna zmian wartoœci funkcji F(X) w podanym przedziale zmian " +
    "X\r\n";
            this.bjBtnWizualizacjaTabelarycznaFx.UseVisualStyleBackColor = true;
            this.bjBtnWizualizacjaTabelarycznaFx.Click += new System.EventHandler(this.bjBtnWizualizacjaTabelarycznaFx_Click);
            // 
            // bjBtnWizualizacjaGraficznaFx
            // 
            this.bjBtnWizualizacjaGraficznaFx.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjBtnWizualizacjaGraficznaFx.Location = new System.Drawing.Point(771, 390);
            this.bjBtnWizualizacjaGraficznaFx.Name = "bjBtnWizualizacjaGraficznaFx";
            this.bjBtnWizualizacjaGraficznaFx.Size = new System.Drawing.Size(230, 90);
            this.bjBtnWizualizacjaGraficznaFx.TabIndex = 21;
            this.bjBtnWizualizacjaGraficznaFx.Text = "Wizualizacja graficzna zmian wartoœci Funkcji F(X)";
            this.bjBtnWizualizacjaGraficznaFx.UseVisualStyleBackColor = true;
            this.bjBtnWizualizacjaGraficznaFx.Click += new System.EventHandler(this.bjBtnWizualizacjaGraficznaFx_Click);
            // 
            // bjBtnReset
            // 
            this.bjBtnReset.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjBtnReset.Location = new System.Drawing.Point(771, 486);
            this.bjBtnReset.Name = "bjBtnReset";
            this.bjBtnReset.Size = new System.Drawing.Size(230, 66);
            this.bjBtnReset.TabIndex = 22;
            this.bjBtnReset.Text = "RESETUJ\r\n(ustaw stan pocz¹tkowy)";
            this.bjBtnReset.UseVisualStyleBackColor = true;
            this.bjBtnReset.Click += new System.EventHandler(this.bjBtnReset_Click);
            // 
            // bjDgvTWFx
            // 
            this.bjDgvTWFx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bjDgvTWFx.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bjNrPrzedzia³u,
            this.bjWartoœæX,
            this.bjWartoœæFx});
            this.bjDgvTWFx.Location = new System.Drawing.Point(260, 198);
            this.bjDgvTWFx.Name = "bjDgvTWFx";
            this.bjDgvTWFx.Size = new System.Drawing.Size(493, 426);
            this.bjDgvTWFx.TabIndex = 23;
            this.bjDgvTWFx.Visible = false;
            // 
            // bjNrPrzedzia³u
            // 
            this.bjNrPrzedzia³u.HeaderText = "Numer przedzia³u dla zmiennej X";
            this.bjNrPrzedzia³u.Name = "bjNrPrzedzia³u";
            this.bjNrPrzedzia³u.Width = 80;
            // 
            // bjWartoœæX
            // 
            this.bjWartoœæX.HeaderText = "Wartoœæ zmiennej niezale¿nej X";
            this.bjWartoœæX.Name = "bjWartoœæX";
            this.bjWartoœæX.Width = 150;
            // 
            // bjWartoœæFx
            // 
            this.bjWartoœæFx.HeaderText = "Wartoœæ funkcji F(x)";
            this.bjWartoœæFx.Name = "bjWartoœæFx";
            this.bjWartoœæFx.Width = 220;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.bjTxtXg);
            this.groupBox2.Controls.Add(this.bjTxtXd);
            this.groupBox2.Controls.Add(this.bjTxtH);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox2.Location = new System.Drawing.Point(12, 198);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 215);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Przedzia³ wartoœci zmiennej X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(24, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 22);
            this.label7.TabIndex = 15;
            this.label7.Text = "Xd";
            // 
            // bjTxtXg
            // 
            this.bjTxtXg.Location = new System.Drawing.Point(65, 86);
            this.bjTxtXg.Name = "bjTxtXg";
            this.bjTxtXg.Size = new System.Drawing.Size(80, 26);
            this.bjTxtXg.TabIndex = 3;
            // 
            // bjTxtXd
            // 
            this.bjTxtXd.Location = new System.Drawing.Point(65, 46);
            this.bjTxtXd.Name = "bjTxtXd";
            this.bjTxtXd.Size = new System.Drawing.Size(80, 26);
            this.bjTxtXd.TabIndex = 2;
            // 
            // bjTxtH
            // 
            this.bjTxtH.Location = new System.Drawing.Point(65, 127);
            this.bjTxtH.Name = "bjTxtH";
            this.bjTxtH.Size = new System.Drawing.Size(80, 26);
            this.bjTxtH.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(25, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 22);
            this.label9.TabIndex = 12;
            this.label9.Text = "Xg";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(26, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 22);
            this.label8.TabIndex = 13;
            this.label8.Text = "h";
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // bjChrt
            // 
            chartArea1.Name = "ChartArea1";
            this.bjChrt.ChartAreas.Add(chartArea1);
            legend1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.BottomLeft;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.TitleFont = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bjChrt.Legends.Add(legend1);
            this.bjChrt.Location = new System.Drawing.Point(260, 198);
            this.bjChrt.Name = "bjChrt";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.bjChrt.Series.Add(series1);
            this.bjChrt.Size = new System.Drawing.Size(493, 300);
            this.bjChrt.TabIndex = 25;
            this.bjChrt.Text = "chart1";
            this.bjChrt.Visible = false;
            // 
            // AnalizatorIndywidualny
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 654);
            this.Controls.Add(this.bjChrt);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bjDgvTWFx);
            this.Controls.Add(this.bjBtnReset);
            this.Controls.Add(this.bjBtnWizualizacjaGraficznaFx);
            this.Controls.Add(this.bjBtnWizualizacjaTabelarycznaFx);
            this.Controls.Add(this.bjBtnObliczWartoœæFx);
            this.Controls.Add(this.bjTxtFX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bjTxtX);
            this.Controls.Add(this.label1);
            this.Name = "AnalizatorIndywidualny";
            this.Text = "Analizator Indywidualny";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.bjAnalizatorIndywidualnyForm_Closing);
            this.Load += new System.EventHandler(this.AnalizatorIndywidualny_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bjDgvTWFx)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bjChrt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox bjTxtX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox bjTxtFX;
        private System.Windows.Forms.Button bjBtnObliczWartoœæFx;
        private System.Windows.Forms.Button bjBtnWizualizacjaTabelarycznaFx;
        private System.Windows.Forms.Button bjBtnWizualizacjaGraficznaFx;
        private System.Windows.Forms.Button bjBtnReset;
        private System.Windows.Forms.DataGridView bjDgvTWFx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox bjTxtXg;
        private System.Windows.Forms.TextBox bjTxtXd;
        private System.Windows.Forms.TextBox bjTxtH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.DataGridViewTextBoxColumn bjNrPrzedzia³u;
        private System.Windows.Forms.DataGridViewTextBoxColumn bjWartoœæX;
        private System.Windows.Forms.DataGridViewTextBoxColumn bjWartoœæFx;
        private System.Windows.Forms.DataVisualization.Charting.Chart bjChrt;
    }
}
