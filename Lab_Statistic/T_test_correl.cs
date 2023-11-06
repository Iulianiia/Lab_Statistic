using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
   public static class T_test_correl
    {
        public static double  t_test_correl (double r, int N)
        {
            double t = r * Math.Sqrt(N - 2) / Math.Sqrt(1 - r * r);
            return Math.Round(r, 4);
        }
    }
}
