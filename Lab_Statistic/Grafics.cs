using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;


namespace Lab_Statistic
{
  public static  class Grafics
    {
        public static Series Fdistribut(Series Fdistrib, List<double> oldarray, double h, double m)
        {

            if (oldarray != null)
            {
                int count = oldarray.Count;
                double[] array = new double[count];
                oldarray.CopyTo(array);
                Array.Sort(array);
                Fdistrib.ChartType = SeriesChartType.Point;
                Fdistrib.Color = Color.Gray;
                double k = 0;
                //  int remarkcount;

                for (int i = 0; i < count; i++)
                {
                    k = (double)(i + 1) / count;
                    Fdistrib.Points.AddXY(array[i], k);
                }
            }
            return Fdistrib;
        }

        public static double[][] Pij(int index1, int index2)
        {
            Sample sample1 = SampleManager.samples[index1];
            Sample sample2 = SampleManager.samples[index2];
            int N1 = sample1.N;
            int N2 = sample2.N;
            int N;
            if (N1 < N2)
                N = N1;
            else
                N = N2;
            (int m1, double h1) =(sample1.m, sample1.h);
            (int m2, double h2) = (sample2.m, sample2.h);


            List<PointD> points = new List<PointD>();

            for (int i = 0; i < N; i++)
            {
                points.Add(new PointD(sample1.Data[i], sample2.Data[i]));
            }

            double[][] p = new double[m1][];

            for (int i = 0; i < m1; i++)
            {
                p[i] = new double[m2];

                List<PointD> remarkX;
                if (i == m1 - 1)
                {
                    remarkX = points.FindAll(rem => rem.X <= (sample1.Min + h1 * (i + 1) + 0.000001) && rem.X >= (i * h1 + sample1.Min));
                }
                else
                {
                    remarkX = points.FindAll(rem => rem.X < (sample1.Min + h1 * (i + 1)) && rem.X >= (i * h1 + sample1.Min));
                }

                for (int j = 0; j < m2; j++)
                {
                    List<PointD> remarkY;
                    if (j == m2 - 1)
                    {
                        remarkY = remarkX.FindAll(rem => rem.Y <= (sample2.Min + h2 * (j + 1) + 0.000001) && rem.Y >= (j * h2 + sample2.Min));
                    }
                    else
                    {
                        remarkY = remarkX.FindAll(rem => rem.Y < (sample2.Min + h2 * (j + 1)) && rem.Y >= (j * h2 + sample2.Min));
                    }

                    double remarkcount = remarkY.Count;
                    p[i][j] = (double)remarkcount / (N);
                }
            }
            return p;
        }
        public static double[][] Pij_(int index1, int index2, double r)
        {
            Sample sample1 = SampleManager.samples[index1];
            Sample sample2 = SampleManager.samples[index2];
            (int m1, double h1) = (sample1.m, sample1.h);
            (int m2, double h2) = (sample2.m, sample2.h);
            double aver_x = sample1.s_charac.average;
            double aver_y = sample2.s_charac.average;
            double sx = sample1.s_charac.rms;
            double sy = sample2.s_charac.rms;
            double[][] fxy = new double [m1][];

            double[][] p_ = new double[m1][] ;
            for (int i = 0; i < m1; i++)
            {
                fxy[i] = new double[m2];
                p_[i] = new double[m2];
                for (int j = 0; j < m2; j++)
                {

                    fxy[i][j] = Reproduction.NormalDistribution(sample1.Min + h1 * (i + 0.5), aver_x, aver_y, sample2.Min + h2 * (j + 0.5), sx, sy, r);
                    p_[i][j] =  fxy[i][j] * h1 * h2;
                }
            }
            return p_;
        }
        public class PointD
        {
            public double X { get; set; }
            public double Y { get; set; }

            public PointD(double x, double y)
            {
                X = x;
                Y = y;
            }
        }


        public static void fxy_2D_(Chart chart,  int m1, double h1, int m2, double h2, double[][] p, double Min1, double Min2)
        {



            if (chart.Series.Count > 0)
                chart.Series[0].Points.Clear();
            chart.Series.Clear();
            chart.ChartAreas[0].AxisX.Interval = Math.Round(h1, 4);
            chart.ChartAreas[0].AxisY.Interval = Math.Round(h2, 4);


            for (int i = 0; i < m1; i++)
            {


                for (int j = 0; j < m2; j++)
                {


                    Series s = new Series();
                    s.ChartType = SeriesChartType.Line;
                    int numberOfLines = 7; // Значення за замовчуванням
                    if(p[i][j] == 0)
                    {
                        numberOfLines = 0;
                    }
                    else if (p[i][j] >= 0.013 && p[i][j] < 0.015)
                    {
                        numberOfLines = 7;
                    }
                    else if (p[i][j] >= 0.015 && p[i][j] < 0.02)
                    {
                        numberOfLines = 13;
                    }
                    else if (p[i][j] >= 0.02 && p[i][j] < 0.027)
                    {
                        numberOfLines = 19;
                    }
                    else if (p[i][j] >= 0.027 && p[i][j] < 0.035)
                    {
                        numberOfLines = 23;
                    }
                    else if (p[i][j] >= 0.035 && p[i][j] < 0.05)
                    {
                        numberOfLines = 29;
                    }
                    else if (p[i][j] >= 0.05 && p[i][j] < 0.09)
                    {
                        numberOfLines = 35;
                    }
                    else if (p[i][j] >= 0.09 && p[i][j] < 0.12)
                    {
                        numberOfLines = 43;
                    }
                    else if (p[i][j] >= 0.12 && p[i][j] < 0.16)
                    {
                        numberOfLines = 55;
                    }
                    else if (p[i][j] >= 0.16 && p[i][j] < 0.18)
                    {
                        numberOfLines = 63;
                    }
                    else if (p[i][j] >= 0.18)
                    {
                        numberOfLines = 137;
                    }

                    double h = h1 / (numberOfLines);
                    for (int k = 0; k <= numberOfLines; k++)
                    {
                        if (k % 2 == 0)
                            s.Points.AddXY(Min1 + h1 * i + h * k, Min2 + h2 * j);
                        else
                            s.Points.AddXY(Min1 + h1 * i + h * k, Min2 + h2 * (j + 1));
                    }
                    s.Color = Color.Gray;
                    chart.Series.Add(s);
                }
            }
        }
        public static void Fxy_2D_(Chart chart, int m1, double h1, int m2, double h2, double[][] p, double Min1, double Min2)
        { 

            if (chart.Series.Count > 0)
                chart.Series[0].Points.Clear();
            chart.Series.Clear();
            chart.ChartAreas[0].AxisX.Interval = Math.Round(h1, 4);
            chart.ChartAreas[0].AxisY.Interval = Math.Round(h2, 4);
            double im = 0;
            for (int i = 0; i < m1; i++)
            {
               

                for (int j = 0; j < m2; j++)
                {
                 
                   
                    Series s = new Series();
                    s.ChartType = SeriesChartType.Line;
                    im += p[i][j];
                    // Визначте кількість ліній на прямокутник в залежності від p
                    int numberOfLines = 7; // Значення за замовчуванням
                    if (im >= 0 && im < 0.1)
                    {
                        numberOfLines = 7;
                    }
                    else if (im >= 0.1 && im < 0.25)
                    {
                        numberOfLines = 14;
                    }
                    else if (im >= 0.25 && im < 0.4)
                    {
                        numberOfLines = 21;
                    }
                    else if (im >= 0.4 && im < 0.65)
                    {
                        numberOfLines = 43;
                    }
                    else if (im >= 0.65 && im < 0.9)
                    {
                        numberOfLines = 51;
                    }
                    else if (im >= 0.9 && im < 0.99)
                    {
                        numberOfLines = 63;
                    }
                    else if (im >= 0.99)
                    {
                        numberOfLines = 137;
                    }

                    double h = h1 / (numberOfLines );
                    for (int k = 0; k <= numberOfLines; k++)
                    {
                        if (k % 2 == 0)
                            s.Points.AddXY(Min1 + h1 * i + h * k, Min2 + h2 * j);
                        else
                            s.Points.AddXY(Min1 + h1 * i + h * k, Min2 + h2 * (j + 1));
                    }
                    s.Color = Color.Gray;
                    chart.Series.Add(s);
                }
            }
        }





        public static Series PaintGrid(Series line, double y, double firstX, double lastX)
        {
            line.ChartType = SeriesChartType.Line;
            line.Color = Color.Black;
            line.BorderWidth = 1;
            line.Points.AddXY(firstX, y);
            line.Points.AddXY(lastX, y);
            return line;
        }
        public static Series CreateHistogram(Series histogram, List<double> array, double h, double m)
        {
            if (array != null)
            {
                //MessageBox.Show(m.ToString(), Math.Round((double)h * (2 + 1), 1).ToString());
                histogram.ChartType = SeriesChartType.Column;
                histogram.BorderWidth = 1;
                histogram.BorderColor = Color.Black;
                histogram.Color = Color.Gray;
                //   double max = array.Max();
                double min = array.Min();
                int count = array.Count;
                int remarkcount = 0;
                //  h = count / m;
                double k = 0;
                for (int i = 0; i < m; i++)
                {
                    // MessageBox.Show(" count = " , count.ToString());
                    List<double> remark;
                    if (i == m - 1)
                        remark = array.FindAll(rem => rem <= (min + h * (i + 1) + 0.000001) & rem >= (i * h + min));
                    else
                        remark = array.FindAll(rem => rem < (min + h * (i + 1)) & rem >= (i * h + min));
                    remarkcount = remark.Count;
                    // MessageBox.Show( count.ToString(),remark.Count.ToString());
                    double v1 = (double)remarkcount / count + k;
                    k = v1;
                    double v = h * (i + 0.5);
                    histogram.Points.AddXY(min + v, (double)remarkcount / count);
                    //   i = i + h;
                }
                //  MessageBox.Show(k.ToString());
            }
            return histogram;
        }
        public static Series Fdistclas(Series fdistclas, List<double> array, double h, double m)
        {
            fdistclas.ChartType = SeriesChartType.Point;
            fdistclas.Color = Color.Black;
            fdistclas.MarkerStyle = MarkerStyle.Circle;
            fdistclas.BorderWidth = 1;
            int count = array.Count;
            int remarkcount = 0;
            double min = array.Min();
            double k = 0;
            double p = 0;
            for (int i = 0; i < m; i++)
            {
                List<double> remark;
                if (i == m - 1)
                {
                    remark = array.FindAll(rem => rem <= (min + h * (i + 1) + 0.000001) & rem >= (i * h + min));
                    p = 100;
                }
                else
                {
                    remark = array.FindAll(rem => rem < (min + h * (i + 1)) & rem >= (i * h + min));
                    p = 50;
                }
                remarkcount = remark.Count;
                k = (double)remarkcount / count + k;
                fdistclas.Points.AddXY(min + h * i, k);
                for (int j = 1; j < p - 2; j++)
                {
                    double v = min + h * (i + (double)j / 50);
                    fdistclas.Points.AddXY(v, k);
                }

                //   i = i + h;
            }
            // MessageBox.Show(fdistclas.Points[1].ToString());
            return fdistclas;
        }
        public static Series MMPq(Series mmp, List<double> oldarray, double qinf, double qsup, double m, ref double min, ref double max)
        {
            mmp.ChartType = SeriesChartType.Line;
            mmp.Color = Color.Black;
            mmp.BorderWidth = 1;
            double[] array = new double[oldarray.Count];
            Array.Sort(array);
            int count = array.Length;
            double k = 0, sum = 0, x;
            for (int i = 0; k + qinf < qsup; i++)
            {
                k = (double)i / 100.0;
                x = k + qinf;
                sum = 0;
                for (int j = 0; j < count; j++)
                {
                    sum += Math.Log(1 / (x * Math.Sqrt(2 * Math.PI)) * Math.Exp(-Math.Pow(array[j] - m, 2) / (2 * Math.Pow(x, 2))));
                }
                mmp.Points.AddXY(Math.Round(x, 3), sum);
                if (i == 0)
                    min = sum;
            }
            max = sum;
            return mmp;
        }
        public static Series MMPm(Series mmp, List<double> array, double minf, double msup, double q, ref double min, ref double max)
        {
            mmp.ChartType = SeriesChartType.Line;
            mmp.Color = Color.Black;
            mmp.BorderWidth = 1;
            // array.Sort();
            int count = array.Count;
            double k = 0, sum = 0, x;
            for (int i = 0; k + minf < msup; i++)
            {
                k = (double)i / 100.0;
                x = k + minf;
                sum = 0;
                for (int j = 0; j < count; j++)
                {
                    sum += Math.Log(1 / (q * Math.Sqrt(2 * Math.PI)) * Math.Exp(-Math.Pow(array[j] - x, 2) / (2 * Math.Pow(q, 2))));
                }
                mmp.Points.AddXY(Math.Round(x, 3), sum);
                if (i == 0)
                {
                    min = sum;
                    max = sum;
                }
                if (max < sum)
                    max = sum;
            }
            return mmp;
        }


        public static  void fxy_2D ( Chart chart,int index1, int index2)
        {
           // chart.Series.Add("fx");
            Series s = new Series() ;
            //s.Color = Color.Yellow; 
            Sample sample1 = SampleManager.samples[index1];
            Sample sample2 = SampleManager.samples[index2];
            int N1 = sample1.N;
            int N2 = sample2.N;
            int N;
            if (N1 < N2)
                N = N1;
            else
                N = N2;
            // Series s = new Series();
            s.ChartType = SeriesChartType.Point;
            s.MarkerStyle = MarkerStyle.Cross; 
            s.BorderWidth = 4;
            s.Color = Color.Red;
            for(int i =0; i< N; i++)
            {
                s.Points.AddXY(sample1.Data[i], sample2.Data[i]);
            }
            chart.Series.Add(s);
          //  s.Color = C
        }


    }
}
