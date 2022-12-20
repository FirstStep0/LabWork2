using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace LabWork
{
    public partial class Form1 : Form
    {
        DataTable table;
        int dx, dy;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            DataTable_table.DataSource = table;
        }
        private void тестоваяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //setFunction();
        }

        private static void AddText(FileStream fs, string text){
            byte[] bytes = new byte[text.Length];
            for (int i = 0; i < text.Length; ++i) {
                bytes[i] = (byte)text[i];
            }
            fs.Write(bytes, 0, text.Length);
        }

        private static string[] string_erase(string[] strs, string str) {
            int count = 0;
            for (int i = 0; i < strs.Length; i++) {
                if (strs[i] != str) count++;
            }
            string[] ans = new string[count];
            count = 0;
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i] != str) {
                    ans[count] = new string(strs[i]);
                    count++;
                }
            }
            return ans;
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            double u0, u1;
            int N;
            u0 = u1 = 0;
            N = 0;
            textBox_info.Text = "";
            if (!double.TryParse(textBox_u0.Text, out u0)) textBox_info.Text += "Некорректное значение u0\n";
            else if (!double.TryParse(textBox_u1.Text, out u1)) textBox_info.Text += "Некорректное значение u1\n";
            else if (!int.TryParse(textBox_N.Text, out N) || (N < 2)) textBox_info.Text += "Некорректное значение N\n";
            else
            {
                //create path
                DirectoryInfo dir = new DirectoryInfo("./tmp");
                dir.Create();

                if (!File.Exists(@"./tmp/LabWork2.exe"))
                {
                    MessageBox.Show("'./tmp/LabWork2.exe' not found");
                    return;
                }

                //create result.txt
                FileInfo path = new FileInfo(@"./tmp/result.txt");
                FileStream output_txt = path.Open(FileMode.Create);
                output_txt.Close();

                //create input.txt
                path = new FileInfo(@"./tmp/input.txt");
                FileStream input_txt = path.Open(FileMode.Create);
                string options = "";

                int number_task = (comboBox1.SelectedIndex);

                options += number_task + " ";
                options += u0 + " ";
                options += u1 + " ";
                options += N + " ";

                options = options.Replace(',', '.');
                AddText(input_txt, options);

                input_txt.Close();

                //run numerical_metods
                string command = @".\tmp\LabWork2.exe .\tmp\input.txt > .\tmp\result.txt";

                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C " + command;
                process.Start();
                process.WaitForExit();

                //get result
                StreamReader reader = new StreamReader(@".\tmp\result.txt");
                try
                {
                    string refer = "";
                    reader.ReadLine();
                    for (int j = 0; j < 3; ++j)
                    {
                        refer += reader.ReadLine() + '\n';
                    }
                    reader.ReadLine();

                    string[] info = refer.Split(' ', '\n', '\t');
                    info = string_erase(info, "");

                    //set info
                    switch (number_task) {
                        case 0:
                            textBox_info.Text += "Для решения задачи использована равномерная сетка с числом разбиений n = " + info[0] + " " + Environment.NewLine;
                            textBox_info.Text += "Задача должна быть решена с погрешностью не более ε = 0.5⋅10 –6" + Environment.NewLine;
                            textBox_info.Text += "Задача решена с погрешностью ε1 = " + info[1] + " " + Environment.NewLine;
                            textBox_info.Text += "Максимальное отклонение аналитического и численного решений наблюдается в точке x = " + info[2] + "" + Environment.NewLine;
                            break;
                        case 1:
                            textBox_info.Text += "Для решения задачи использована равномерная сетка с числом разбиений n = " + info[0] + " " + Environment.NewLine;
                            textBox_info.Text += "Задача должна быть решена с заданной точностью ε = 0.5⋅10 –6" + Environment.NewLine;
                            textBox_info.Text += "Задача решена с точностью ε2 = " + info[1] + " " + Environment.NewLine;
                            textBox_info.Text += "Максимальная разность численных решений в общих узлах сетки наблюдается в точке x = " + info[2] + "" + Environment.NewLine;
                            break;
                    }

                    //get firstLine
                    string firstLine = reader.ReadLine();
                    string[] name_columns = firstLine.Split('\t');
                    //name_columns = string_erase(name_columns, "");

                    //clear table
                    table.Columns.Clear();
                    table.Rows.Clear();

                    //set column names
                    for (int i = 0; i < name_columns.Length; ++i)
                    {
                        table.Columns.Add(name_columns[i], typeof(string));
                    }

                    //fill table values
                    while (reader.Peek() > -1)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split('\t');
                        //values = string_erase(values, "");
                        table.Rows.Add(values);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Incorrect result.txt");
                }
                reader.Close();

                //run graph.exe
                command = "";
                switch (number_task)
                {
                    case 0:
                        command += "0";
                        break;
                    case 1:
                        command += "1";
                        break;
                }
                process = new Process();
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                if (!File.Exists(@".\tmp\graph.py"))
                {
                    MessageBox.Show(@"'.\tmp\graph.py' not found");
                    return;
                }
                else
                {
                    process.StartInfo.FileName = "python";
                    command = @".\tmp\graph.py " + command;
                    process.StartInfo.Arguments = "" + command;
                }
                process.Start();
            }

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            DataTable_table.Size = new Size(control.Size.Width - DataTable_table.Location.X - dx, control.Size.Height - DataTable_table.Location.Y - dy);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            dx = control.Size.Width - (DataTable_table.Location.X + DataTable_table.Size.Width);
            dy = control.Size.Height - (DataTable_table.Location.Y + DataTable_table.Size.Height);
        }
    }
}
