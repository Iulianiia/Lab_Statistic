using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
   public static class Statistics_Binar
    {
        public static double Fenhers_Index (int N00, int N01, int N10, int N11)
        {
            double I = (N00 + N11 - N10 - N01) / (double)(N00 + N11 + N10 + N01);
            return Math.Round(I, 4);
        }
        public static double Coeff_Fi(int N00, int N01, int N10, int N11, int N0, int N1, int M0, int M1)
        {
            double F = (N00 * N11 - N01 * N10) / Math.Sqrt(N0 * N1 * M0 * M1);
            return Math.Round(F, 4);
        }
        public static double Coeff_Jula(int N00, int N01, int N10, int N11)
        {
            double Q = (N00*N11 - N01*N10)/(double)(N00 * N11 + N10 * N01);
            return Math.Round(Q, 4);
        }
    }
}
