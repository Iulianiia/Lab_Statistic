using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
    public static class T_test_correl
    {
        public static double t_test_correl(double r, int N)
        {
            double t = r * Math.Sqrt(N - 2) / Math.Sqrt(1 - r * r);
            return Math.Round(r, 4);
        }
        public static double t_test_corr_relation(double po, int N)
        {
            double n = po * Math.Sqrt(N - 2);
            double m = Math.Sqrt(1 - po * po);
            double t = n / m;
            return t;
        }
        public static double t_test_spirmen(double ts, int N)
        {
            double t = ts * Math.Sqrt(N - 2) / Math.Sqrt(1 - ts * ts);
            return Math.Round(t, 4);
        }
        public static double test_Kendall(double tk, int N)
        {
            double u_1 = 3 * tk / Math.Sqrt(2.0 * (2.0 * N + 5));
            double u_2 = Math.Sqrt(N * (N - 1));
            double u = u_1 * u_2;
            return Math.Round(u, 4);
        }

        public static double test_Stuard(double P, double Q, double AB, int N, double min_nm)
        {
            double q1 = 2 * min_nm / (N * N * N * (min_nm - 1));
            double q2 = Math.Sqrt(N * N * AB - 4 * N * (P - Q));
            double Q_ts = q1 * q2;
            return Math.Round(Q_ts, 4);
        }
        public static double t_test_Fi(int N, double Fi)
        {
            double chsi2 = N * Fi * Fi;
            return Math.Round(chsi2, 4);
        }
        public static double t_test_Jula(int N00, int N01, int N10, int N11, double Q)
        {
            double Sq = 1.0 / 2.0 * (1 - Q * Q) * Math.Sqrt(1.0 / N00 + 1.0 / N01 + 1.0 / N10 + 1.0 / N11);
            return Math.Round(Sq, 4);
        }
    }
}
