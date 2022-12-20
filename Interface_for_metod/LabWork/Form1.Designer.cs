
namespace LabWork
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataTable_table = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_u0 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.textBox_info = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_u1 = new System.Windows.Forms.TextBox();
            this.textBox_N = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataTable_table)).BeginInit();
            this.SuspendLayout();
            // 
            // DataTable_table
            // 
            this.DataTable_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataTable_table.Location = new System.Drawing.Point(12, 216);
            this.DataTable_table.Name = "DataTable_table";
            this.DataTable_table.RowHeadersWidth = 51;
            this.DataTable_table.Size = new System.Drawing.Size(876, 271);
            this.DataTable_table.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "u(0) =";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "x ∈ [0,1] ";
            // 
            // textBox_u0
            // 
            this.textBox_u0.Location = new System.Drawing.Point(66, 71);
            this.textBox_u0.Name = "textBox_u0";
            this.textBox_u0.Size = new System.Drawing.Size(125, 27);
            this.textBox_u0.TabIndex = 10;
            this.textBox_u0.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 20);
            this.label10.TabIndex = 20;
            this.label10.Text = "N = ";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Тестовая задача",
            "Основная задача"});
            this.comboBox1.Location = new System.Drawing.Point(5, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(186, 28);
            this.comboBox1.TabIndex = 21;
            this.comboBox1.Tag = "";
            // 
            // buttonSolve
            // 
            this.buttonSolve.Location = new System.Drawing.Point(13, 179);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(178, 29);
            this.buttonSolve.TabIndex = 22;
            this.buttonSolve.Text = "Построить";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // textBox_info
            // 
            this.textBox_info.Location = new System.Drawing.Point(197, 7);
            this.textBox_info.Multiline = true;
            this.textBox_info.Name = "textBox_info";
            this.textBox_info.ReadOnly = true;
            this.textBox_info.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_info.Size = new System.Drawing.Size(691, 199);
            this.textBox_info.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 29;
            this.label6.Text = "u(1) = ";
            // 
            // textBox_u1
            // 
            this.textBox_u1.Location = new System.Drawing.Point(66, 100);
            this.textBox_u1.Name = "textBox_u1";
            this.textBox_u1.Size = new System.Drawing.Size(125, 27);
            this.textBox_u1.TabIndex = 28;
            this.textBox_u1.Text = "0";
            // 
            // textBox_N
            // 
            this.textBox_N.Location = new System.Drawing.Point(66, 130);
            this.textBox_N.Name = "textBox_N";
            this.textBox_N.Size = new System.Drawing.Size(125, 27);
            this.textBox_N.TabIndex = 31;
            this.textBox_N.Text = "10";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(79, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 20);
            this.label11.TabIndex = 32;
            this.label11.Text = "ξ = 0.5";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 499);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox_N);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_u1);
            this.Controls.Add(this.textBox_info);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox_u0);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DataTable_table);
            this.MinimumSize = new System.Drawing.Size(918, 546);
            this.Name = "Form1";
            this.Text = "Команда 4. Лабораторная работа №2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeBegin += new System.EventHandler(this.Form1_ResizeBegin);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DataTable_table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataTable_table;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_u0;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.TextBox textBox_info;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_u1;
        private System.Windows.Forms.TextBox textBox_N;
        private System.Windows.Forms.Label label11;
    }
}

