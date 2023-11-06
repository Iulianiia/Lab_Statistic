using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_Statistic
{
    public partial class Form1
    {
        public void Grafics_2D( int now_index1, int now_index2, Sample sample1, Sample sample2)
        {
            chart1.ChartAreas[0].AxisX.Maximum = Math.Round(sample1.Max, 4);
            chart1.ChartAreas[0].AxisX.Minimum = Math.Round(sample1.Min, 4);
            chart1.ChartAreas[0].AxisY.Maximum = Math.Round(sample2.Max, 4);
            chart1.ChartAreas[0].AxisY.Minimum = Math.Round(sample2.Min, 4);

            chart2.ChartAreas[0].AxisX.Maximum = Math.Round(sample1.Max, 4);
            chart2.ChartAreas[0].AxisX.Minimum = Math.Round(sample1.Min, 4);
            chart2.ChartAreas[0].AxisY.Maximum = Math.Round(sample2.Max, 4);
            chart2.ChartAreas[0].AxisY.Minimum = Math.Round(sample2.Min, 4);

            (int m1, double h1) = M_H.m_h(now_index1);
            (int m2, double h2) = M_H.m_h(now_index2);
            double[][] p = Grafics.Pij(now_index1, now_index2);
            Grafics.fxy_2D_(chart1, m1, h1, m2, h2, p, sample1.Min, sample2.Min);
            Grafics.fxy_2D(chart1, now_index1, now_index2);
            Grafics.Fxy_2D_(chart2, m1, h1, m2, h2, p, sample1.Min, sample2.Min);
            Grafics.fxy_2D(chart2, now_index1, now_index2);



        }
        public void Corr_analys(int N, int now_index1, int now_index2, Sample sample1, Sample sample2)
        {
            double aver_xy = Statistics_for_2samples.Aver_xy(N, now_index1, now_index2);
            double r = Correlation.correlation(aver_xy  ,sample1.s_charac.average , sample2.s_charac.average, sample1.s_charac.rms,sample2.s_charac.rms,N);
            double criticalValue = QuantileT.quantileT(0.1, N - 1);
            double t_test = T_test_correl.t_test_correl(r, N);

            richTextBox1.Text = "Математичне сподівання" + "\n---------------------------------\n" +
   "СКВ";
            richTextBox2.Text = Math.Round((sample1.s_charac.average - criticalValue * Sigma.sigmaAv(sample1.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample2.s_charac.average - criticalValue * Sigma.sigmaAv(sample2.N, now_index2)), 4).ToString() +
                "\n---------------------------------\n" +
               Math.Round((sample1.s_charac.rms - criticalValue * Sigma.sigmaRMS(sample2.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample1.s_charac.rms - criticalValue * Sigma.sigmaRMS(sample2.N, now_index2)), 4).ToString();


            richTextBox3.Text = Math.Round(sample1.s_charac.average, 4).ToString() + "; " +
                Math.Round(sample2.s_charac.average, 4).ToString()
                + "\n---------------------------------\n" +
                   Math.Round(sample1.s_charac.rms, 4).ToString() + "; " +
                    Math.Round(sample2.s_charac.rms, 4).ToString();
            richTextBox4.Text = Math.Round((sample1.s_charac.average + criticalValue * Sigma.sigmaAv(sample1.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample2.s_charac.average + criticalValue * Sigma.sigmaAv(sample2.N, now_index2)), 4).ToString()
                + "\n---------------------------------\n" +
               Math.Round((sample1.s_charac.rms + criticalValue * Sigma.sigmaRMS(sample1.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample2.s_charac.rms + criticalValue * Sigma.sigmaRMS(sample2.N, now_index2)), 4).ToString();
            richTextBox5.Text = Math.Round(Sigma.sigmaAv(sample1.N, now_index1), 4).ToString() + "; " +
                Math.Round(Sigma.sigmaAv(sample2.N, now_index2), 4).ToString()
                + "\n---------------------------------\n" +
               Math.Round(Sigma.sigmaRMS(sample1.N, now_index1), 4).ToString() + "; " +
                Math.Round(Sigma.sigmaRMS(sample2.N, now_index2), 4).ToString();

        }
    }
}
