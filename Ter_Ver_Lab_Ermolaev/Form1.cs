using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;

namespace Ter_Ver_Lab_Ermolaev
{
    public partial class Form1 : Form
    {
        List<double> xmas = new List<double>();
        List<double> Result = new List<double>();
        List<double> Xj = new List<double>();
        List<double> Xi = new List<double>();
        List<double> Nnj = new List<double>();
        List<double> Fln = new List<double>();

        //Для третьей части лабораторной
        //List<double> x3 = new List<double>();
        List<double> R3 = new List<double>();
        List<double> Xj3 = new List<double>();
        List<double> Xi3 = new List<double>();
        //List<double> Nnj3 = new List<double>();
        List<double> FR3 = new List<double>();
        double alpha3 = 0.0;
        int colroz = 0;
        double a3 = 0.5;
        int colexp = 0;
        bool left3 = false;
        bool right3 = false;
        double y0 =0.0;
        int otclon3 = 0;

        double Fn(double x, double a)
        {
            double res = 0;
            if (x < -1) { res = 0; }
            else if ((x<0)&&(x>=(0 - Math.Asin(a/2.0)/a))) { res = (2 * Math.Sin(a * x) + a) / (2 * a); }
            else if ((x < 1) && (x >= 0)) { res = (1.0/2.0-(1.0/2.0)*(x-2)*x); }
            return res; 
        }
       
        double fn(double x, int r)
        {
            double res = 0.0;
            if (x <= 0) { res = 0.0; }
            else
            {
                res = Math.Pow(2, (double)(-r)/2)*Math.Pow(SpecialFunctions.Gamma((double)(r)/2),(-1))*Math.Pow(x, (((double)(-r)/2)) - 1.0)*Math.Pow(Math.E,(-x/2));
            }
            return res;
        }

        double trapezoidalIntegral(double a, double b, int n,int r) {
        double width = (b-a)/n;
        double trapezoidal_integral = 0;
        for(int step = 0; step < n; step++) {
        double x1 = a + step*width;
        double x2 = a + (step+1)*width;        
        trapezoidal_integral += 0.5*(x2-x1)*(fn(x1,r) + fn(x2,r));
        }

        return trapezoidal_integral;
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
            Random random = new Random(Guid.NewGuid().GetHashCode());
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            xmas.Clear();
            Result.Clear();
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
            }
            // try
            //{
                double a = Double.Parse(textBox1.Text);
                int N = Int32.Parse(textBox2.Text);
                double y0 = -Math.Asin(a / 2) / a;
                label10.Text = ("["+ y0.ToString()+ ";1]");
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
                    

                    if (x <= 0.5)
                    {
                        result = Math.Asin(x * a - a / 2) / a;
                    }
                    else
                    {
                        result = 1 - Math.Sqrt(2 * (1 - x));
                    }
                
                sum += result;
                Result.Add(result);
                    listBox1.Items.Add("x = " + x + ";\t G(x) = " + result + "\n");
                }

                Result.Sort();
                x_p = sum / N;
                
                R = Result[N-1] - Result[0]; // если что, сюда воткнуть проверку на то, скока экспериментов было выполнено.

                if (N % 2 == 0)
                    Me = (Result[N / 2 + 1] + Result[N / 2]) / 2;
                else
                    Me = Result[(N - 1) / 2 + 1];

                for (int i = 0; i < N; i++)
                {
                    sum2 += (Result[i] - x_p)*(Result[i] - x_p);
                }

                S2 = sum2 / N;
                Ex = Math.Abs(En - x_p);
                DnS2 = Math.Abs(Dn-S2);

                dataGridView1.Rows.Add(En, x_p, Ex, Dn, S2, DnS2, Me, R);

               
                int k = 0;
                int col;
                double D = 0;
                while (k < Result.Count){
                chart1.Series[1].Points.AddXY(Result[k], Fn(Result[k], a));
                chart1.Series[0].Points.AddXY(Result[k], Fn(Result[k],a));
                int j = 0;
                col = 0;
                while(j<k){
                    if (Result[j] < Result[k]) col++;
                    j++;
                }
                chart1.Series[2].Points.AddXY(Result[k], (double)col/(double)N);
                chart1.Series[3].Points.AddXY(Result[k], (double)col / (double)N);
                if ((Math.Abs(((double)col / (double)N) - Fn(Result[k], a))) > D) D = (Math.Abs(((double)col / (double)N) - Fn(Result[k], a)));               
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
            double a = Double.Parse(textBox1.Text);
            int N = Int32.Parse(textBox2.Text);
            double fn = 0.0;
            int t = 0;
            double nnj = 0.0;
            double nj = 0.0;
            int r = 0;
            double nnj2 = 0.0;
            double nj2 = 0.0;
            int r2 = 0;
            double y0 = -Math.Asin(a / 2) / a;

            if (leftshit == false)
            {
                while (r < Result.Count)
                {
                    if ((Result[r] <= Xi[0]) && (Result[r] >= y0)) nj++;
                    r++;
                }

                nnj = nj / ((double)N * (Math.Abs(Xi[0] - (y0))));
                chart2.Series[0].Points.AddXY(y0, 0);
                chart2.Series[0].Points.AddXY(y0, nnj);
                chart2.Series[0].Points.AddXY(Xi[0], nnj);
                chart2.Series[0].Points.AddXY(Xi[0], 0);
            }

         

            while (t < Xi.Count())
            {
                chart2.Series[0].Points.AddXY(Xi[t], 0);
                chart2.Series[0].Points.AddXY(Xi[t], Nnj[t]);
                chart2.Series[0].Points.AddXY(Xj[t], Nnj[t]);
                chart2.Series[0].Points.AddXY(Xj[t], 0);
                t++;
            }

            if (rightshit == false)
            {
                while (r2 < Result.Count)
                {
                    if ((Result[r2] <= 1) && (Result[r2] >= Xj[Xj.Count-1])) nj2++;
                    r2++;
                }

                nnj2 = nj2 / ((double)N * (Math.Abs(1 - Xj[Xj.Count-1])));

                chart2.Series[0].Points.AddXY(Xj[Xj.Count-1], 0);
                chart2.Series[0].Points.AddXY(Xj[Xj.Count-1], nnj2);
                chart2.Series[0].Points.AddXY(1, nnj2);
                chart2.Series[0].Points.AddXY(1, 0);
            }
            
            int k = 0;
            while (k < Result.Count())
            {
                if ((Result[k] < 0) && (Result[k] >= (0 - Math.Asin(a / 2.0) / a))) fn = Math.Cos(a * Result[k]);
                else fn = 1 - Result[k];

                chart2.Series[1].Points.AddXY(Result[k], fn);
                k++;
            }

        }
        bool leftshit = false;
        bool rightshit = false;
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
            double y0 = -Math.Asin(a / 2) / a;

            if (xi == y0) leftshit = true;
            if (xj == 1) leftshit = true;

            if ((z < 0) && (z >= (0 - Math.Asin(a / 2.0) / a))) fn = Math.Cos(a * z);
            else fn = 1 - z;

            while (r < Result.Count) {
                if ((Result[r] <= xj) && (Result[r] >= xi)) nj++;
                r++;
            }

            nnj = nj / ((double)N*(Math.Abs(xj-xi)));


                dataGridView2.Rows.Add(xi, xj, z, fn, nnj);
                Fln.Add(fn);
                Xi.Add(xi);
                Xj.Add(xj);
                Nnj.Add(nnj);

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        { 
            a3 = double.Parse(textBox11.Text);
            colexp = int.Parse(textBox12.Text);
            y0 = -Math.Asin(a3 / 2) / a3;
            label14.Text = ("[" + y0.ToString() + ";1]");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            double xi = Double.Parse(textBox9.Text);
            double xj = Double.Parse(textBox8.Text);
            if ((xi != y0)&&(leftshit==false)) {
                dataGridView3.Rows.Add(y0, xi);
                Xi3.Add(y0);
                Xj3.Add(xi);
                leftshit = true;
            }
            dataGridView3.Rows.Add(xi, xj);
            Xi3.Add(xi);
            Xj3.Add(xj);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            alpha3 = double.Parse(textBox6.Text);
            colroz = int.Parse(textBox7.Text);
            otclon3 = 0;
            dataGridView4.Rows.Clear();

            if ((Xj3[Xj3.Count - 1]) != 1) MessageBox.Show("Вы не покрыли доступный отрезок интервалами целиком! \n (Правый конец последнего отрезка не совпадает с 1)");
            else
            {
                
                int i = 0;
                while (i < colroz)
                {
                    R3.Clear();
                    for (int j = 0; j < colexp; j++) //Проводим розыгрыш с нужным нам количеством экспериментов
                    {
                        double result;
                        double x = GetRandomNumber(0.0, 1.0);

                        if (x <= 0.5)
                        {
                            result = Math.Asin(x * a3 - a3 / 2) / a3;
                        }
                        else
                        {
                            result = 1 - Math.Sqrt(2 * (1 - x));
                        }
                        R3.Add(result);
                    }
                    int t = 0;
                    double q = 0.0;
                    double R0 = 0.0;
                    while (t < Xi3.Count())
                    {
                        int r = 0;
                        double Nj3 = 0;
                        q = Fn(Xj3[t], a3) - Fn(Xi3[t], a3); //Расчёт гипотезы через qi = F(x[i]) - F(x[i-1])
                        while (r < R3.Count)
                        {
                            if ((R3[r] <= Xj3[t]) && (R3[r] >= Xi3[t])) Nj3++;
                            r++;
                        } //Расчёт количества точек из R3 что попали в наш нынешний промежуток от Xi[t] до Xj[t]

                        R0 += Math.Pow((Nj3-colexp*q), 2)/(colexp*q); //сумма всех значений для нахождения R0 
                        t++;
                    }//Провели необходимые расчёты для нахождения R0 на данной итерации розыгрыша
                    double FR0 = 0.0;
                    if (R0<0) FR0=1 - trapezoidalIntegral(R0,0,1000,Xi3.Count-1);
                    else FR0 =1 -  trapezoidalIntegral(0, R0, 1000, Xi3.Count - 1);

                    FR3.Add(FR0);
                    if (FR0 < alpha3) { 
                        otclon3++;
                        dataGridView4.Rows.Add(FR0, R0, 1);
                    }
                    else dataGridView4.Rows.Add(FR0, R0, 0);
                    i++;
                }
            }
            textBox10.Text = otclon3.ToString();
            textBox13.Text = ((double)otclon3 / (double)colroz).ToString();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            Xi3.Clear();
            Xj3.Clear();
        }
    }
}
