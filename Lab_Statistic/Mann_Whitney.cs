using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matStat
{
    public static class Average_rang
    {
        public static double average_rang(List<double> rang1, List<double> rang2, int n1, int n2)
        {
            double rx = 1.0 / n1 * rang1.Sum();
            double ry = 1.0 / n2 * rang2.Sum();
            double v = (rx - ry) / ((n1 + n2) * Math.Sqrt((n1 + n2 + 1) / (double)(12 * n1 * n2)));
            return v;
        }
    }
}
