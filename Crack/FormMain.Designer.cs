namespace Crack
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxConcrete = new System.Windows.Forms.ComboBox();
            this.textBoxFtk = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCover = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEs = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxN1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSD1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxN2 = new System.Windows.Forms.TextBox();
            this.textBoxSD2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.textBoxH = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxMoment = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxCrack = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxCas = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "混凝土标号";
            // 
            // comboBoxConcrete
            // 
            this.comboBoxConcrete.FormattingEnabled = true;
            this.comboBoxConcrete.Location = new System.Drawing.Point(94, 24);
            this.comboBoxConcrete.Name = "comboBoxConcrete";
            this.comboBoxConcrete.Size = new System.Drawing.Size(75, 20);
            this.comboBoxConcrete.TabIndex = 1;
            this.comboBoxConcrete.Text = "C30";
            // 
            // textBoxFtk
            // 
            this.textBoxFtk.Location = new System.Drawing.Point(94, 62);
            this.textBoxFtk.Name = "textBoxFtk";
            this.textBoxFtk.Size = new System.Drawing.Size(75, 21);
            this.textBoxFtk.TabIndex = 2;
            this.textBoxFtk.Text = "2.01";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "ftk(N/mm2)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cover(mm)";
            // 
            // textBoxCover
            // 
            this.textBoxCover.Location = new System.Drawing.Point(94, 136);
            this.textBoxCover.Name = "textBoxCover";
            this.textBoxCover.Size = new System.Drawing.Size(75, 21);
            this.textBoxCover.TabIndex = 2;
            this.textBoxCover.Text = "30";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "Es(N/mm2)";
            // 
            // textBoxEs
            // 
            this.textBoxEs.Location = new System.Drawing.Point(94, 97);
            this.textBoxEs.Name = "textBoxEs";
            this.textBoxEs.Size = new System.Drawing.Size(75, 21);
            this.textBoxEs.TabIndex = 2;
            this.textBoxEs.Text = "200000";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(252, 174);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "n1根";
            // 
            // textBoxN1
            // 
            this.textBoxN1.Location = new System.Drawing.Point(255, 97);
            this.textBoxN1.Name = "textBoxN1";
            this.textBoxN1.Size = new System.Drawing.Size(72, 21);
            this.textBoxN1.TabIndex = 2;
            this.textBoxN1.Text = "5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(334, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "d1(mm)";
            // 
            // textBoxSD1
            // 
            this.textBoxSD1.Location = new System.Drawing.Point(379, 97);
            this.textBoxSD1.Name = "textBoxSD1";
            this.textBoxSD1.Size = new System.Drawing.Size(72, 21);
            this.textBoxSD1.TabIndex = 2;
            this.textBoxSD1.Text = "20";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(201, 139);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "n2根";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(334, 141);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "d2(mm)";
            // 
            // textBoxN2
            // 
            this.textBoxN2.Location = new System.Drawing.Point(255, 136);
            this.textBoxN2.Name = "textBoxN2";
            this.textBoxN2.Size = new System.Drawing.Size(72, 21);
            this.textBoxN2.TabIndex = 2;
            this.textBoxN2.Text = "5";
            // 
            // textBoxSD2
            // 
            this.textBoxSD2.Location = new System.Drawing.Point(379, 136);
            this.textBoxSD2.Name = "textBoxSD2";
            this.textBoxSD2.Size = new System.Drawing.Size(72, 21);
            this.textBoxSD2.TabIndex = 2;
            this.textBoxSD2.Text = "20";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(376, 174);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "退出";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(201, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "B(mm)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(340, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "H(mm)";
            // 
            // textBoxB
            // 
            this.textBoxB.Location = new System.Drawing.Point(255, 23);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(72, 21);
            this.textBoxB.TabIndex = 2;
            // 
            // textBoxH
            // 
            this.textBoxH.Location = new System.Drawing.Point(379, 23);
            this.textBoxH.Name = "textBoxH";
            this.textBoxH.Size = new System.Drawing.Size(72, 21);
            this.textBoxH.TabIndex = 2;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(196, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 0;
            this.label11.Text = "Mk(N*mm)";
            // 
            // textBoxMoment
            // 
            this.textBoxMoment.Location = new System.Drawing.Point(255, 62);
            this.textBoxMoment.Name = "textBoxMoment";
            this.textBoxMoment.Size = new System.Drawing.Size(72, 21);
            this.textBoxMoment.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 203);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 0;
            this.label12.Text = "裂缝(mm)";
            // 
            // textBoxCrack
            // 
            this.textBoxCrack.Location = new System.Drawing.Point(94, 200);
            this.textBoxCrack.Name = "textBoxCrack";
            this.textBoxCrack.Size = new System.Drawing.Size(75, 21);
            this.textBoxCrack.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 171);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "as(mm)";
            // 
            // textBoxCas
            // 
            this.textBoxCas.Location = new System.Drawing.Point(94, 168);
            this.textBoxCas.Name = "textBoxCas";
            this.textBoxCas.Size = new System.Drawing.Size(75, 21);
            this.textBoxCas.TabIndex = 2;
            this.textBoxCas.Text = "50";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 244);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxSD2);
            this.Controls.Add(this.textBoxN2);
            this.Controls.Add(this.textBoxH);
            this.Controls.Add(this.textBoxCrack);
            this.Controls.Add(this.textBoxMoment);
            this.Controls.Add(this.textBoxB);
            this.Controls.Add(this.textBoxSD1);
            this.Controls.Add(this.textBoxN1);
            this.Controls.Add(this.textBoxCas);
            this.Controls.Add(this.textBoxCover);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxEs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxFtk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxConcrete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxConcrete;
        private System.Windows.Forms.TextBox textBoxFtk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCover;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxEs;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxN1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSD1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxN2;
        private System.Windows.Forms.TextBox textBoxSD2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.TextBox textBoxH;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxMoment;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxCrack;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxCas;
    }
}