using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
   public static class Correlation
    {
        public static double correlation(double aver_xy, double aver_x, double aver_y, double sx, double sy, int N)
        {
            double r= N/(N-1.0);
            r *= (aver_xy - aver_x * aver_y) / (sx * sy);
            return r;
        }

    }
}
