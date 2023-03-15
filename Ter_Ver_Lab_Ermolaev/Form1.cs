using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ter_Ver_Lab_Ermolaev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void вариантЗадачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void выкладкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                double a = Double.Parse(textBox1.Text);
                int N = Int32.Parse(textBox2.Text);

                for (int i = 0; i<=N ; i++)
                {
                    double result;
                    double x = GetRandomNumber(0.0, 1.0);
                    if (x <= 0.5)
                    {
                        result = Math.Asin(x * a - a / 2) / a;
                    }
                    else
                    {
                        result = 1 - Math.Sqrt(2 * (1 - x));
                    }
                    listBox1.Items.Add("x = " + x +";\t G(x) = " + result + "\n") ;
                }
            }
            catch
            {
                MessageBox.Show("Правильно заполните поля!");
            }
        }
    }
}
