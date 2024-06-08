namespace Projekt3_Janusz70130
{
    partial class Sprawdzian3
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sprawdzian3));
            this.bjChrt = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bjErrorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bjBtnReset = new System.Windows.Forms.Button();
            this.bjBtnWizualizacjaGraficznaFx = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bjTxtXg = new System.Windows.Forms.TextBox();
            this.bjTxtXd = new System.Windows.Forms.TextBox();
            this.bjTxtH = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.bjDgvTWFx = new System.Windows.Forms.DataGridView();
            this.bjNrPrzedziału = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bjWartośćX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bjWartośćFx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bjBtnObliczWartośćFx = new System.Windows.Forms.Button();
            this.bjTxtFX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bjTxtX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bjChrt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bjErrorProvider2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bjDgvTWFx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bjChrt
            // 
            chartArea4.Name = "ChartArea1";
            this.bjChrt.ChartAreas.Add(chartArea4);
            legend4.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.BottomLeft;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            legend4.IsTextAutoFit = false;
            legend4.Name = "Legend1";
            legend4.TitleFont = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bjChrt.Legends.Add(legend4);
            this.bjChrt.Location = new System.Drawing.Point(277, 218);
            this.bjChrt.Name = "bjChrt";
            this.bjChrt.Size = new System.Drawing.Size(493, 426);
            this.bjChrt.TabIndex = 39;
            this.bjChrt.Text = "chart1";
            this.bjChrt.Visible = false;
            // 
            // bjErrorProvider2
            // 
            this.bjErrorProvider2.ContainerControl = this;
            // 
            // bjBtnReset
            // 
            this.bjBtnReset.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjBtnReset.Location = new System.Drawing.Point(792, 503);
            this.bjBtnReset.Name = "bjBtnReset";
            this.bjBtnReset.Size = new System.Drawing.Size(230, 66);
            this.bjBtnReset.TabIndex = 36;
            this.bjBtnReset.Text = "RESETUJ\r\n(ustaw stan początkowy)";
            this.bjBtnReset.UseVisualStyleBackColor = true;
            this.bjBtnReset.Click += new System.EventHandler(this.bjBtnReset_Click);
            // 
            // bjBtnWizualizacjaGraficznaFx
            // 
            this.bjBtnWizualizacjaGraficznaFx.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjBtnWizualizacjaGraficznaFx.Location = new System.Drawing.Point(792, 407);
            this.bjBtnWizualizacjaGraficznaFx.Name = "bjBtnWizualizacjaGraficznaFx";
            this.bjBtnWizualizacjaGraficznaFx.Size = new System.Drawing.Size(230, 90);
            this.bjBtnWizualizacjaGraficznaFx.TabIndex = 35;
            this.bjBtnWizualizacjaGraficznaFx.Text = "Wizualizacja graficzna zmian wartości Funkcji F(X)";
            this.bjBtnWizualizacjaGraficznaFx.UseVisualStyleBackColor = true;
            this.bjBtnWizualizacjaGraficznaFx.Click += new System.EventHandler(this.bjBtnWizualizacjaGraficznaFx_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(46, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 22);
            this.label8.TabIndex = 13;
            this.label8.Text = "h";
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
            this.groupBox2.Location = new System.Drawing.Point(26, 311);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 215);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Przedział wartości zmiennej X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(44, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 22);
            this.label7.TabIndex = 15;
            this.label7.Text = "Xd";
            // 
            // bjTxtXg
            // 
            this.bjTxtXg.Location = new System.Drawing.Point(85, 86);
            this.bjTxtXg.Name = "bjTxtXg";
            this.bjTxtXg.Size = new System.Drawing.Size(80, 26);
            this.bjTxtXg.TabIndex = 3;
            // 
            // bjTxtXd
            // 
            this.bjTxtXd.Location = new System.Drawing.Point(85, 46);
            this.bjTxtXd.Name = "bjTxtXd";
            this.bjTxtXd.Size = new System.Drawing.Size(80, 26);
            this.bjTxtXd.TabIndex = 2;
            // 
            // bjTxtH
            // 
            this.bjTxtH.Location = new System.Drawing.Point(85, 127);
            this.bjTxtH.Name = "bjTxtH";
            this.bjTxtH.Size = new System.Drawing.Size(80, 26);
            this.bjTxtH.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label9.Location = new System.Drawing.Point(45, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 22);
            this.label9.TabIndex = 12;
            this.label9.Text = "Xg";
            // 
            // bjDgvTWFx
            // 
            this.bjDgvTWFx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.bjDgvTWFx.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bjNrPrzedziału,
            this.bjWartośćX,
            this.bjWartośćFx});
            this.bjDgvTWFx.Location = new System.Drawing.Point(277, 218);
            this.bjDgvTWFx.Name = "bjDgvTWFx";
            this.bjDgvTWFx.Size = new System.Drawing.Size(493, 426);
            this.bjDgvTWFx.TabIndex = 37;
            this.bjDgvTWFx.Visible = false;
            // 
            // bjNrPrzedziału
            // 
            this.bjNrPrzedziału.HeaderText = "Numer przedziału dla zmiennej X";
            this.bjNrPrzedziału.Name = "bjNrPrzedziału";
            this.bjNrPrzedziału.Width = 80;
            // 
            // bjWartośćX
            // 
            this.bjWartośćX.HeaderText = "Wartość zmiennej niezależnej X";
            this.bjWartośćX.Name = "bjWartośćX";
            this.bjWartośćX.Width = 150;
            // 
            // bjWartośćFx
            // 
            this.bjWartośćFx.HeaderText = "Wartość funkcji F(x)";
            this.bjWartośćFx.Name = "bjWartośćFx";
            this.bjWartośćFx.Width = 220;
            // 
            // bjBtnObliczWartośćFx
            // 
            this.bjBtnObliczWartośćFx.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjBtnObliczWartośćFx.Location = new System.Drawing.Point(792, 311);
            this.bjBtnObliczWartośćFx.Name = "bjBtnObliczWartośćFx";
            this.bjBtnObliczWartośćFx.Size = new System.Drawing.Size(230, 90);
            this.bjBtnObliczWartośćFx.TabIndex = 33;
            this.bjBtnObliczWartośćFx.Text = "Oblicz wartość funkcji \r\nw punkcie X";
            this.bjBtnObliczWartośćFx.UseVisualStyleBackColor = true;
            this.bjBtnObliczWartośćFx.Click += new System.EventHandler(this.bjBtnObliczWartośćFx_Click_1);
            // 
            // bjTxtFX
            // 
            this.bjTxtFX.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjTxtFX.Location = new System.Drawing.Point(867, 267);
            this.bjTxtFX.Name = "bjTxtFX";
            this.bjTxtFX.Size = new System.Drawing.Size(80, 26);
            this.bjTxtFX.TabIndex = 32;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(825, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 38);
            this.label3.TabIndex = 31;
            this.label3.Text = "Obliczona wartość \r\nfunkcji F(X) w punkcie X";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(26, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(996, 119);
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(366, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(404, 26);
            this.label2.TabIndex = 29;
            this.label2.Text = "SPRAWDZIAN Nr 3 (samoocena: 4.5) ";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // bjTxtX
            // 
            this.bjTxtX.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bjTxtX.Location = new System.Drawing.Point(91, 267);
            this.bjTxtX.Name = "bjTxtX";
            this.bjTxtX.Size = new System.Drawing.Size(80, 26);
            this.bjTxtX.TabIndex = 28;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 38);
            this.label1.TabIndex = 27;
            this.label1.Text = "Wartość zmiennej \r\nniezależnej X";
            // 
            // Sprawdzian3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 706);
            this.Controls.Add(this.bjChrt);
            this.Controls.Add(this.bjBtnReset);
            this.Controls.Add(this.bjBtnWizualizacjaGraficznaFx);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bjDgvTWFx);
            this.Controls.Add(this.bjBtnObliczWartośćFx);
            this.Controls.Add(this.bjTxtFX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bjTxtX);
            this.Controls.Add(this.label1);
            this.Name = "Sprawdzian3";
            this.Text = "Sprawdzian3";
            ((System.ComponentModel.ISupportInitialize)(this.bjChrt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bjErrorProvider2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bjDgvTWFx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart bjChrt;
        private System.Windows.Forms.ErrorProvider bjErrorProvider2;
        private System.Windows.Forms.Button bjBtnReset;
        private System.Windows.Forms.Button bjBtnWizualizacjaGraficznaFx;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox bjTxtXg;
        private System.Windows.Forms.TextBox bjTxtXd;
        private System.Windows.Forms.TextBox bjTxtH;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView bjDgvTWFx;
        private System.Windows.Forms.DataGridViewTextBoxColumn bjNrPrzedziału;
        private System.Windows.Forms.DataGridViewTextBoxColumn bjWartośćX;
        private System.Windows.Forms.DataGridViewTextBoxColumn bjWartośćFx;
        private System.Windows.Forms.Button bjBtnObliczWartośćFx;
        private System.Windows.Forms.TextBox bjTxtFX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox bjTxtX;
        private System.Windows.Forms.Label label1;
    }
}