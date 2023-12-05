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
            return Math.Round(r,3);
        }
        public static (double rn, double rv) interv_assess_coef_corr(double r, int N, double u)
        {
            double add = r * (1 - r * r) / (2 * N);
            double n_or_v = u * (1 - r * r) / Math.Sqrt(N - 1);
            double rn = r + add - n_or_v;
            double rv = r + add + n_or_v;
            return (rn, rv);
        }
        public static double corr_relation (Two_dimensional_sample sampleXY)
        {
            int m= sampleXY.X.m;
            double h = sampleXY.X.h;
            double y_aver = sampleXY.Y.s_charac.average;
            double S2y_aver = 0;
            double S2y = 0;
            List<XY> remarkY = new List<XY>();
            for (int i =0; i < m; i++)
            {

                
                if (i == m - 1)
                {
                    remarkY = sampleXY.xy.FindAll(rem => rem.x  <= (sampleXY.X.Min + h * (i + 0.5) + 0.000001) && rem.x  >= (i * h + sampleXY.X.Min));
                }
                else
                {
                    remarkY = sampleXY.xy.FindAll(rem => rem.x  < (sampleXY.X.Min + h * (i + 0.5)) && rem.x  >= (i * h + sampleXY.X.Min));
                }
                double mi = remarkY.Count();
                double yi_aver =0;
                if (mi > 0)
                {
                    yi_aver = Sum(remarkY) / mi;
                    S2y_aver += mi * Math.Pow(yi_aver - y_aver, 2);
                }

                for(int j =0; j< mi; j++)
                {
                    S2y += Math.Pow(remarkY[j].y  - y_aver, 2);
                }
            }
            double po = S2y_aver / S2y;
            return Math.Round(po,4);

        }
        public static double Sum(List<XY> xyList)
        {
            double sum = 0;
            foreach (XY xy in xyList)
            {
                sum += xy.y;
            }
            return sum;
        }
        public static double Interv_Spirm(double ts, int N)
        {
            double q_ts = Math.Sqrt((1 - ts * ts) / (double)(N - 2));
            return Math.Round(q_ts, 4);
        }
        public static double Interv_Kendall( int N)
        {
            double q_tk = Math.Sqrt((4 * N + 10.0) / (9.0 * (N * N - N)));
            return Math.Round(q_tk, 4);
        }

    }
}
