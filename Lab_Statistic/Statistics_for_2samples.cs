
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
    public static class Statistics_for_2samples
    {
        public static double Aver_xy(int N, int index1, int index2)
        {
            Sample sample1 = SampleManager.samples[index1];
            Sample sample2 = SampleManager.samples[index2];
            double aver_xy = 0;
            for(int i =0; i< N; i++)
            {
                aver_xy += sample1.Data[i] * sample2.Data[i];  
            }
            aver_xy /= (N);
            return aver_xy;
        }
        public static double Chsi_cryt(double[][] p, double[][] p_,double Mx, double My)
        {
            double chsi2 =0;
            for (int i = 0; i < Mx; i++)
            {
                for (int j = 0; j < My; j++)
                {
                    if(Math.Round(p_[i][j] ,5)!=0)
                       chsi2 += Math.Pow(p[i][j] - p_[i][j],2) / p_[i][j];
                }
            }
            return chsi2;
        }
        public static double Spirmens_cryt(List<double> rx, List<double> ry, int N  )
        {
           double d ;
            double Sum_of_d=0;
            for(int i = 0; i < N; i++)
            {
                d = rx[i] - ry[i];
                Sum_of_d += d * d;
            }
            double ts = 1 - 6.0 / (N * (N * N - 1)) * Sum_of_d;
            return Math.Round(ts, 4);
        }
        public static double[] CalculateTies(List<double> ranks)
        {
            ranks.Sort();
            double[] ties = new double[ranks.Count];

            for (int i = 0; i < ranks.Count; i++)
            {
                int tieCount = 1;

                // Порахувати кількість однакових рангів
                while (i + 1 < ranks.Count && ranks[i] == ranks[i + 1])
                {
                    tieCount++;
                    i++;
                }

                // Записати кількість в'язок у вихідний масив
                ties[i - tieCount + 1] = tieCount;
            }

            return ties;
        }
        public static double Ties_sum(double [] ties)
        {
            double ties_sum = 0;
            int n = ties.Length;
            for(int i = 0; i < n; i++)
            {
                ties_sum += Math.Pow(ties[i], 3) - ties[i];
            }
            return 1.0 / 12.0 * ties_sum;
        }
        public static double Spirmens_cryt(List<double> rx, List<double> ry, int N, bool alarm)
        {
            double A = Ties_sum(CalculateTies(rx));
            double B = Ties_sum(CalculateTies(ry));
            double d;
            double Sum_of_d = 0;
            for (int i = 0; i < N; i++)
            {
                d = rx[i] - ry[i];
                Sum_of_d += d * d;
            }
            double ts_1 = 1 /6.0 * (N * (N * N - 1))  -  Sum_of_d - A - B;
            double ts_2 = Math.Pow((1 / 6.0 * (N * (N * N - 1)) - 2 * A) * (1 / 6.0 * (N * (N * N - 1)) - 2 * B), 1.0 / 2.0);
            double ts = ts_1 / ts_2;
            return Math.Round(ts, 4);
        }
        public static int CalculateAlgebraicSum(List<double> y)
        {
            int n = y.Count;
            int algebraicSum = 0;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (y[i] < y[j])
                    {
                        // Елементи мають однаковий порядок в обох послідовностях
                        algebraicSum += 1;
                    }
                    else
                    {
                        algebraicSum -= 1;
                    }
                }
            }

            return algebraicSum;
        }

        public static double Kendall_coeff( List<double> ry, int N)
        {
            double S = CalculateAlgebraicSum( ry);
            double tk = 2 * S / (N*(N - 1.0));
            return Math.Round(tk, 4);
        }

        public static (double, double , double[]) stochastic_connection(Sample sampleX, Sample sampleY, int N)
        {
            int m2 = sampleY.m;
            double h2 = sampleY.h;
            double y_aver = sampleY.s_charac.average;

            int m1 = sampleX.m;
            double h1 = sampleX.h;
            double x_aver = sampleX.s_charac.average;

            double chsi2 = 0;
            //  double S2y_aver = 0;
            //    double S2y = 0;
            List<Grafics.PointD> points = new List<Grafics.PointD>();
            for (int i = 0; i < N; i++)
            {
                points.Add(new Grafics.PointD(sampleX.Data[i], sampleY.Data[i]));
            }

            //    double[][] p = new double[m1][];
            double[] for_P = new double[m1-1];
            double[] for_Q = new double[m1-1];
            double Sum_nij = 0;
            double T1 = 0, T2 = 0;
            double[] A = new double[m1 - 1];
            double[] B = new double[m1 - 1];
            double Sum_A_B = 0;
            for (int i = 0; i < m1; i++)
            {
               // p[i] = new double[m2];

                List<Grafics.PointD> remarkX;
                if (i == m1 - 1)
                {
                    remarkX = points.FindAll(rem => rem.X <= (sampleX.Min + h1 * (i + 1) + 0.000001) && rem.X >= (i * h1 + sampleX.Min));
                }
                else
                {
                    remarkX = points.FindAll(rem => rem.X < (sampleX.Min + h1 * (i + 1)) && rem.X >= (i * h1 + sampleX.Min));
                }


                List<Grafics.PointD> remarkM;
                if (i == m1 - 1)
                {
                    remarkM = points.FindAll(rem => rem.Y <= (sampleY.Min + h2 * (i + 1) + 0.000001) && rem.Y >= (i * h2 + sampleY.Min));
                }
                else
                {
                    remarkM = points.FindAll(rem => rem.Y < (sampleY.Min + h2 * (i + 1)) && rem.Y >= (i * h2 + sampleY.Min));
                }
                double ni = remarkX.Count, mj = remarkM.Count;
                double Nij = ni * mj / (double)N;
                T1 += ni * (ni - 1);
                T2 += mj*(mj - 1);

                for (int j = 0; j < m2; j++)
                {
                    List<Grafics.PointD> remarkY;
                    if (j == m2 - 1)
                    {
                        remarkY = remarkX.FindAll(rem => rem.Y <= (sampleY.Min + h2 * (j + 1) + 0.000001) && rem.Y >= (j * h2 + sampleY.Min));
                    }
                    else
                    {
                        remarkY = remarkX.FindAll(rem => rem.Y < (sampleY.Min + h2 * (j + 1)) && rem.Y >= (j * h2 + sampleY.Min));
                    }
                   
                    
                    double nij = remarkY.Count;
                    
                    chsi2 += Math.Pow(nij - Nij, 2) / Nij;
                    Sum_nij += nij;
                    for (int k =0; k< m1-1; k++)
                    {
                        if (i > k & j > k)
                        {
                            for_P[k] += nij;
                            //A[k] += nij;
                        }
                        if (i > k & j < m1 - k - 1)
                        {
                            for_Q[k] += nij;
                        }
                  
                    }

                    for (int k = 0; k < m1 - 1; k++)
                    {
                        for (int l = 0; l < m2 - 1; l++) {
                            if (i > k & j > l)
                            {
                                A[k] += nij;
                            }
                            if (i < m1 - k - 1 & j < m2 - l - 1)
                            {
                                A[k] += nij;
                            }
                            if (i > k & j < m2 - l - 1)
                            {
                               B[k] += nij;
                            }
                            if (i < m1 - k -1 & j > l)
                            {
                                B[k] += nij;
                            }

                            //Sum_A_B += (A[k] - B[k]) * (A[k] - B[k]);
                        }
                    }
                    Sum_A_B += nij*Math.Pow(A.Sum() - B.Sum(),2);
                  
                }
            }
            T1 = T1 / 2.0;
            T2 = T2 / 2.0;
            double P = for_P.Sum();
            double Q = for_Q.Sum();

            if (m1 == m2)
            {
                double tb = (P - Q) / Math.Sqrt((1.0 / 2.0 * (N * (N - 1) - T1)) * (1.0 / 2.0 * (N * (N - 1) - T2)));
                double[] for_test = new double[3];
                return (Math.Round(chsi2, 4), Math.Round(tb, 4), for_test);
            }
            else
            {
                double min_nm = 0;
                if (m1 < m2)
                    min_nm = m1;
                else
                    min_nm = m2;
                double tst =(2*(P-Q)*min_nm)/(N*N*(min_nm-1));
                double[] for_test = new double[3];
                for_test[0] = Sum_A_B; for_test[1] = P;for_test[2] = Q;
                return (Math.Round(chsi2, 4), Math.Round(tst, 4), for_test );
            }
            

        }
        public static double Coeff_Pirson (double chsi2, int N)
        {
            double C = Math.Sqrt(chsi2 / (N + chsi2));
            return Math.Round(C, 4);
        }
    }
}
