using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
    public static class Reproduction
    {

        public static double [][]FnormLine(double sx, double sy, double aver_x, double aver_y,  double r, List<double> sample1, List<double> sample2, int M)
        {
            double[][] fxy = new double [M][];
            // NormalDistribution(x, aver_x ,aver_y , y,sx,sy,r);
            for(int i = 0; i < M; i++)
            {
                for(int j =0; j < M; j++)
                {
                    fxy[i][j] = NormalDistribution(sample1[i], aver_x, aver_y, sample2[j], sx, sy,r);
                }
            }
            return fxy;

        }
        public static double NormalDistribution(double x,double aver_x, double aver_y, double y, double sx, double sy, double r)
        {
            double exponent = -1 / (2 *(1 - r * r)) *( Math.Pow((x-aver_x)/sx ,2)- 2*r*((x-aver_x )/sx)*((y - aver_y )/sy) + Math.Pow((y - aver_y) / sy, 2));
            double coefficient = 1 / (2*Math.PI*sx*sy*Math.Sqrt(1-r*r));
            return coefficient * Math.Exp(exponent );
        }

    }
}
