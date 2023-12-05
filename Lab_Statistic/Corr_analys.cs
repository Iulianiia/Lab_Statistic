using Accord.Math;
using MathNet.Numerics.Distributions;
using matStat;
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

            (int m1, double h1) =(sample1.m, sample1.h);
            (int m2, double h2) = (sample2.m, sample2.h);
            double[][] p = Grafics.Pij(now_index1, now_index2);
            Grafics.fxy_2D_(chart1, m1, h1, m2, h2, p, sample1.Min, sample2.Min);
            Grafics.fxy_2D(chart1, now_index1, now_index2);
            Grafics.Fxy_2D_(chart2, m1, h1, m2, h2, p, sample1.Min, sample2.Min);
            Grafics.fxy_2D(chart2, now_index1, now_index2);



        }
        private class Rangs_x_y
        {
          public  double Rx { get; set; }
          public  double Ry { get; set; }
          public  void Add_Rangs_x_y(double rx, double ry)
            {
                Rx = rx;
                Ry = ry;
            }
        }
       private bool Rangs_maker(Sample sample1, Sample sample2, int N)
        {
            bool alarm1 = false, alarm2 = false, alarm = false;
            if (sample1.rangs == null)
            {
                (sample1.rangs, alarm1) = Rang_counter.CalculateRanks(sample1.Data, N);
            }
            if (sample2.rangs == null)
            {
                (sample2.rangs, alarm2) = Rang_counter.CalculateRanks(sample2.Data, N);
            }
            if (alarm1 || alarm2)
                alarm = true;
            /*     List<Rangs_x_y> rangs_X_Y = new List<Rangs_x_y>();
                 for (int i = 0; i < N; i++)
                 {
                     Rangs_x_y xy = new Rangs_x_y();
                     xy.Add_Rangs_x_y(sample1.rangs[i], sample2.rangs[i]);
                     rangs_X_Y.Add(xy);

                 }
                 return (rangs_X_Y, alarm);
            */
            return alarm;
        }
        public void Corr_an_bin(int N, int now_index1, int now_index2, Sample sample1, Sample sample2)
        {
            int N00 = 0;
            int N11 = 0;
            int N01 = 0;
            int N10 = 0;

            for(int i = 0; i < N; i++)
            {
                if(sample1.Data[i]==0 &  sample2.Data[i] ==0)
                {
                    N00 += 1;
                }
                else if(sample1.Data[i]==1 &  sample2.Data[i] ==1)
                {
                    N11 += 1;
                }
                else if(sample2.Data[i]==0 & sample1.Data[i] == 1)
                {
                    N01 += 1;
                }
                else if (sample2.Data[i] == 1 & sample1.Data[i] == 0)
                {
                    N10 += 1;
                }
            }
            int N0 = N00 + N01;
            int N1 = N11 + N10;
            int M0 = N00 + N10;
            int M1 = N01 + N11;
            double Feh = Statistics_Binar.Fenhers_Index(N00, N01, N10, N11);
            double Fi = Statistics_Binar.Coeff_Fi(N00, N01, N10, N11, N0, N1, M0, M1);
            double Jula = Statistics_Binar.Coeff_Jula(N00, N01, N10, N11);

            double probability = 0.95; // Наприклад, для отримання 95% к
            double standardNormalQuantile = Accord.Math.Normal.Inverse(probability);
            double degreesOfFreedom = N - 1; // де v - ступені свобод
            double chiSquareQuantile = ChiSquared.InvCDF(degreesOfFreedom, probability);

            double chsi2_charac = T_test_correl.t_test_Fi(N, Fi);
            double t_test_Jula = T_test_correl.t_test_Jula(N00, N01, N10, N11, Jula);

            richTextBox6.Text = "Індекс Фехнера " + "\n---------------------------------\n" +
                              "Коєфіцієнт сполучень Фі" + "\n---------------------------------\n" +
                              "Кофіцієнт зв'язку Юла Q" + "\n---------------------------------\n" ;
            richTextBox9.Text = string.Format("{0} \n---------------------------------\n{1}" +
                "\n---------------------------------\n{2} ",
                Feh,
                Fi,
                Jula
); if (chsi2_charac < chiSquareQuantile)
            {
                richTextBox11.Text = string.Format("H1 : Ф != 0 -> χ² = {0} < {1}", Math.Round(chsi2_charac, 3), Math.Round(chiSquareQuantile, 3));
            }
            else
            {
                richTextBox11.Text = string.Format("H0 : Ф = 0 - χ² = {0} >= {1}", Math.Round(chsi2_charac, 3), Math.Round(chiSquareQuantile, 3));
            }

            if (Math.Abs(t_test_Jula) <= standardNormalQuantile)
            {
                richTextBox11.Text += string.Format("\nH0 : Q = 0 -> |u_Q| = {0} <= {1}", Math.Round(t_test_Jula, 3), Math.Round(standardNormalQuantile, 3));
            }
            else
            {
                richTextBox11.Text += string.Format("\nH1 : Q != 0 -> |u_Q| = {0} > {1}", Math.Round(t_test_Jula, 3), Math.Round(standardNormalQuantile, 3));
            }
            richTextBox10.Text = "";
            richTextBox8.Text = "";

        }

        public void Corr_analys(int N, int now_index1, int now_index2, Two_dimensional_sample sampleXY)
        {
            Sample sample1 = sampleXY.X , sample2 = sampleXY.Y;

            (int m1, double h1) = (sample1.m, sample1.h);
            (int m2, double h2) = (sample2.m, sample2.h);

            double aver_xy = Statistics_for_2samples.Aver_xy(N, now_index1, now_index2);
            double r = Correlation.correlation(aver_xy  ,sample1.s_charac.average , sample2.s_charac.average, sample1.s_charac.rms,sample2.s_charac.rms,N);
            double[][] p = Grafics.Pij(now_index1, now_index2);
            double[][] p_ = Grafics.Pij_(now_index1, now_index2, r);
            
            double po = Correlation.corr_relation(sampleXY);

            bool alarm = Rangs_maker(sample1, sample2, N);
            double ts =0;
           
            List<List<double>> two_samples = new List<List<double>>();
            two_samples.Add(sample1.Data);two_samples.Add(sample2.Data);
            List<int> n = new List<int>(); n.Add(N); n.Add(N);

            List<List<double>> ry = Rang_counter.CalculateRanks(two_samples, n);
            double tk = Statistics_for_2samples.Kendall_coeff( ry[1], N);
            double chsi2 = Statistics_for_2samples.Chsi_cryt(p, p_, m1, m2);

            if (alarm)
            {
                ts = Statistics_for_2samples.Spirmens_cryt(sample1.rangs , sample2.rangs , N, alarm);
            }
            else
            {
                ts = Statistics_for_2samples.Spirmens_cryt(sample1.rangs, sample2.rangs, N);
            }

            double criticalValue = QuantileT.quantileT(0.1, N-1);
            double probability = 0.95; // Наприклад, для отримання 95% к
            double standardNormalQuantile = Accord.Math.Normal.Inverse(probability); 
            double degreesOfFreedom = (m1 - 1) * (m2 - 1); // де v - ступені свобод
            double chiSquareQuantile = ChiSquared.InvCDF(degreesOfFreedom, probability);


            double t_test = T_test_correl.t_test_correl(r, N);
            double t_test_Spir = T_test_correl.t_test_spirmen(ts, N);
            double test_Kend = T_test_correl.test_Kendall(tk, N);
            (double rn, double rv) = Correlation.interv_assess_coef_corr(r, N, criticalValue);
            double t_test_c_rel = T_test_correl.t_test_corr_relation(po, N);
           // double t_test_Pirs = T_test_correl.

            double q_ts = Correlation.Interv_Spirm(ts, N);
            double q_tk = Correlation.Interv_Kendall( N);

           (double chsi2_for_Pirs, double stat_table_comb, double []for_test)   =  Statistics_for_2samples.stochastic_connection(sample2, sample1, N);
            double t_test_stuard = 0;
            if(m1 != m2)
            {
                double min_nm = 0;
                if (m1 < m2)
                    min_nm = m1;
                else
                    min_nm = m2;
                t_test_stuard = T_test_correl.test_Stuard(for_test[1], for_test[2], for_test[0], N, min_nm);
            }
            double coeff_Pirs = Statistics_for_2samples.Coeff_Pirson(chsi2_for_Pirs, N);
            //      Rangs_maker(sample1,sample2,N);
            if(t_test <= standardNormalQuantile)
            {
                richTextBox11.Text = string.Format("H0 : r = 0 -> t = {0} <= {1}", Math.Round(t_test , 3), Math.Round(standardNormalQuantile, 3));

            }
            else
            {
                richTextBox11.Text = string.Format("H1 : r =! 0 -> t = {0} > {1}", Math.Round(t_test , 3), Math.Round(standardNormalQuantile, 3));

            }
            if (chsi2 <= chiSquareQuantile)
            {
                richTextBox11.Text += string.Format("\nH1 : C = 0 -> χ² = {0} <= {1}", Math.Round(chsi2, 3), Math.Round(chiSquareQuantile, 3));
            }
            else
            {
                richTextBox11.Text += string.Format("\nH0 : C = 0 -> χ² = {0} > {1}", Math.Round(chsi2, 3), Math.Round(chiSquareQuantile, 3));
            }

            if (t_test_Spir <= standardNormalQuantile)
            {
                richTextBox11.Text += string.Format("\nH0 : τc = 0 -> t = {0} <= {1}", Math.Round(t_test_Spir, 3), Math.Round(standardNormalQuantile, 3));
            }
            else
            {
                richTextBox11.Text += string.Format("\nH1 : τc != 0 -> t = {0} <= {1}", Math.Round(t_test_Spir, 3), Math.Round(standardNormalQuantile, 3));
            }

            if (test_Kend <= standardNormalQuantile)
            {
                richTextBox11.Text += string.Format("\nH0 : τk = 0 -> u = {0} <= {1}", Math.Round(test_Kend, 3), Math.Round(standardNormalQuantile, 3));
            }
            else
            {
                richTextBox11.Text += string.Format("\nH1 : τk != 0 -> u = {0} <= {1}", Math.Round(test_Kend, 3), Math.Round(standardNormalQuantile, 3));
            }

            if (t_test_c_rel <= standardNormalQuantile)
            {
                richTextBox11.Text += string.Format("\nH0 : ρ η/ξ = 0 -> u = {0} <= {1}", Math.Round(t_test_c_rel, 3), Math.Round(standardNormalQuantile, 3));
            }
            else
            {
                richTextBox11.Text += string.Format("\nH1 : ρ η/ξ != 0 -> u = {0} <= {1}", Math.Round(t_test_c_rel, 3), Math.Round(standardNormalQuantile, 3));
            }

            if (m1 != m2)
            {
                if (t_test_stuard <= chiSquareQuantile)
                {
                    richTextBox11.Text += string.Format("\nH0 : ρ η/ξ = 0 -> u = {0} <= {1}", Math.Round(t_test_stuard, 3), Math.Round(chiSquareQuantile, 3));
                }
                else
                {
                    richTextBox11.Text += string.Format("\nH1 : ρ η/ξ != 0 -> u = {0} <= {1}", Math.Round(t_test_stuard, 3), Math.Round(chiSquareQuantile, 3));
                }
            }

            richTextBox1.Text = "Математичне сподівання" + "\n---------------------------------\n" +
                                "СКВ" + "\n---------------------------------\n" +
                                "χ²-критерій" + "\n---------------------------------\n";
            richTextBox2.Text = string.Format( "{0}; {1}\n---------------------------------\n{2}; {3}", 
    Math.Round((sample1.s_charac.average - criticalValue * Sigma.sigmaAv(sample1.N, now_index1)), 4),
    Math.Round((sample2.s_charac.average - criticalValue * Sigma.sigmaAv(sample2.N, now_index2)), 4),
    Math.Round((sample1.s_charac.rms - criticalValue * Sigma.sigmaRMS(sample2.N, now_index1)), 4),
    Math.Round((sample1.s_charac.rms - criticalValue * Sigma.sigmaRMS(sample2.N, now_index2)), 4)

) ;



            richTextBox3.Text = string.Format("{0}; {1}\n---------------------------------\n{2}; {3} \n---------------------------------\n{4}",
     Math.Round(sample1.s_charac.average, 4),
     Math.Round(sample2.s_charac.average, 4),
     Math.Round(sample1.s_charac.rms, 4),
     Math.Round(sample2.s_charac.rms, 4),
     Math.Round(chsi2, 4)

 );

            richTextBox4.Text = string.Format(
    "{0}; {1}\n---------------------------------\n{2}; {3}",
    Math.Round((sample1.s_charac.average + criticalValue * Sigma.sigmaAv(sample1.N, now_index1)), 4),
    Math.Round((sample2.s_charac.average + criticalValue * Sigma.sigmaAv(sample2.N, now_index2)), 4),
    Math.Round((sample1.s_charac.rms + criticalValue * Sigma.sigmaRMS(sample2.N, now_index1)), 4),
    Math.Round((sample1.s_charac.rms + criticalValue * Sigma.sigmaRMS(sample2.N, now_index2)), 4)
);
            richTextBox5.Text = string.Format(
     "{0}; {1}\n---------------------------------\n{2}; {3}",
     Math.Round(Sigma.sigmaAv(sample1.N, now_index1), 4),
     Math.Round(Sigma.sigmaAv(sample2.N, now_index2), 4),
     Math.Round(Sigma.sigmaRMS(sample1.N, now_index1), 4),
     Math.Round(Sigma.sigmaRMS(sample2.N, now_index2), 4)
 );

            if (m1 == m2)
            {

                richTextBox6.Text = 
                                    "Коефіцієнт кореляції" + "\n---------------------------------\n" +
                                    "Коефіцієнт кореляційного відношення ρ" + "\n---------------------------------\n" +
                                    "Ранговий коефіцієнт Спірмена" + "\n---------------------------------\n" +
                                    "Ранговий коефіцієнт Кендалла" + "\n---------------------------------\n" +
                                    "Коефіцієнт сполучень Пірсона" + "\n---------------------------------\n" +
                                    "Міра зв’язку Кендалла" + "\n---------------------------------\n";



            }
            else
            {
                richTextBox6.Text = "Коефіцієнт кореляції" + "\n---------------------------------\n" +
                                    "Коефіцієнт кореляційного відношення ρ" + "\n---------------------------------\n" +
                                    "Ранговий коефіцієнт Спірмена" + "\n---------------------------------\n" +
                                    "Ранговий коефіцієнт Кендалла" + "\n---------------------------------\n" +
                                    "Коефіцієнт сполучень Пірсона" + "\n---------------------------------\n" +
                                    "Cтатистика Стюарда" + "\n---------------------------------\n";
            }
            richTextBox8.Text = string.Format("\n---------------------------------\n \n---------------------------------\n{0} \n---------------------------------\n " +
                "\n---------------------------------\n{1}\n---------------------------------\n{2}",
                  Math.Round(rn, 4),
                  Math.Round(ts - criticalValue * q_ts, 4),
                  Math.Round(tk - standardNormalQuantile * q_tk, 4)
);


            richTextBox9.Text = string.Format("{0} \n---------------------------------\n{1}" +
"\n---------------------------------\n{2} \n---------------------------------\n{3}" +
"\n---------------------------------\n{4} \n---------------------------------\n{5}",
r,
po,
ts,
tk,
coeff_Pirs,
stat_table_comb


);

richTextBox10.Text = string.Format("\n---------------------------------\n \n---------------------------------\n{0} \n---------------------------------\n " +
                 "\n---------------------------------\n{1}\n---------------------------------\n{2}",
                  Math.Round(rv, 4),
                  Math.Round(ts + criticalValue * q_ts, 4),
                  Math.Round(tk + standardNormalQuantile * q_tk, 4)
);

        }
    }
}
