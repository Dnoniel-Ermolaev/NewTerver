using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Ter_Ver_Lab_Ermolaev
{
    public partial class Form1 : Form
    {
        List<double> xmas = new List<double>();
        List<double> Xj = new List<double>();
        List<double> Xi = new List<double>();
        List<double> Nnj = new List<double>();
        double Fn(double x, double a)
        {
            double res = 0;
            if (x < -1) { res = 0; }
            else if ((x<0)&&(x>=(0 - Math.Asin(a/2.0)/a))) { res = (2 * Math.Sin(a * x) + a) / (2 * a); }
            else if ((x < 1) && (x >= 0)) { res = (1.0/2.0-(1.0/2.0)*(x-2)*x); }
            return res; 
        }

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
        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            xmas.Clear();
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            // try
            //{
                double a = Double.Parse(textBox1.Text);
                int N = Int32.Parse(textBox2.Text);
              
                double En = (1.0/6.0) - ((Math.Sqrt(4-a*a)+a*Math.Asin(a/2.0)-2)/(2*a*a)); //есть
                double x_p = 0; // есть 
                double Ex = 0;// есть
                double Dn = ((2*Math.Sqrt(4-a*a)*Math.Asin(a/2.0)-2*a+a*Math.Pow(Math.Asin((a/2.0)),2))/(2*a*a*a)+1.0/12.0) - Math.Pow((1.0 / 6.0) - ((Math.Sqrt(4 - a * a) + a * Math.Asin(a / 2.0) - 2) / (2 * a * a)),2); //есть
                double S2 = 0;// есть
                double DnS2 = 0;//есть
                double Me = 0; // есть
                double R = 0; // есть 
                double sum = 0; // есть
                double sum2 = 0;// есть 
                for (int i = 0; i < N; i++)
                {
                    double result;
                    double x = GetRandomNumber(0.0, 1.0);
                    xmas.Add(x);
                    sum += x;

                    if (x <= 0.5)
                    {
                        result = Math.Asin(x * a - a / 2) / a;
                    }
                    else
                    {
                        result = 1 - Math.Sqrt(2 * (1 - x));
                    }

                    listBox1.Items.Add("x = " + x + ";\t G(x) = " + result + "\n");
                }

                xmas.Sort();
                x_p = sum / N;
                
                R = xmas[N-1] - xmas[0]; // если что, сюда воткнуть проверку на то, скока экспериментов было выполнено.

                if (N % 2 == 0)
                    Me = (xmas[N / 2 + 1] + xmas[N / 2]) / 2;
                else
                    Me = xmas[(N - 1) / 2 + 1];

                for (int i = 0; i < N; i++)
                {
                    sum2 += (xmas[i] - x_p)*(xmas[i] - x_p);
                }

                S2 = sum2 / N;
                Ex = Math.Abs(En - x_p);
                DnS2 = Math.Abs(Dn-S2);

                dataGridView1.Rows.Add(En, x_p, Ex, Dn, S2, DnS2, Me, R);

               
                int k = 0;
                int col;
                double D = 0;
                while (k < xmas.Count){
                chart1.Series[1].Points.AddXY(xmas[k], Fn(xmas[k], a));
                chart1.Series[0].Points.AddXY(xmas[k], Fn(xmas[k],a));
                int j = 0;
                col = 0;
                while(j<k){
                    if (xmas[j] < xmas[k]) col++;
                    j++;
                }
                chart1.Series[2].Points.AddXY(xmas[k], (double)col/(double)N);
                chart1.Series[3].Points.AddXY(xmas[k], (double)col / (double)N);
                if ((Math.Abs(((double)col / (double)N) - Fn(xmas[k], a))) > D) D = (Math.Abs(((double)col / (double)N) - Fn(xmas[k], a)));               
                k++;
                
                }
            textBox5.Paste(D.ToString());
            // }
            //catch
            //{
            //    MessageBox.Show("Правильно заполните поля!");
            // }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int t = 0;
            while (t < Xi.Count())
            {
                chart2.Series[0].Points.AddXY(Xi[t], Nnj[t]);

                chart2.Series[0].Points.AddXY(Xj[t]- Xi[t], Nnj[t]);
                t++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double a = Double.Parse(textBox1.Text);
            int N = Int32.Parse(textBox2.Text);
            double xi = Double.Parse(textBox3.Text);
            double xj = Double.Parse(textBox4.Text);
            double z = (xi + xj) / 2.0;
            double fn = 0.0;
            double nnj = 0.0;
            double nj = 0.0;
            int r = 0;
           

            if ((z < 0) && (z >= (0 - Math.Asin(a / 2.0) / a))) fn = Math.Cos(a * z);
            else fn = 1 - z;

            while (r < xmas.Count) {
                if ((xmas[r] <= xj) && (xmas[r] >= xi)) nj++;
                r++;
            }

            nnj = nj / ((double)N*(Math.Abs(xj-xi)));



            dataGridView2.Rows.Add(xi,xj,z, fn, nnj);
            Xi.Add(xi);
            Xj.Add(xj);
            Nnj.Add(nnj);
        }
    }
}
