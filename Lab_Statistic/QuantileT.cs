using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
    public static class QuantileT
    {
        public static double quantileT(double alpha, double count)
        {
            var chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            double t = chart.DataManipulator.Statistics.InverseTDistribution(alpha, (int)count);
            return t;
        }
    }
}
