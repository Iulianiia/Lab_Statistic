using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
   public static  class Sigma
    {

        public static double sigmaAv( int num, int now_index)
        {
            double s = SampleManager.samples[now_index].s_charac.rms ;
            //  MessageBox.Show(s.ToString());
            double q = s / Math.Sqrt(num);
            return Math.Round(q, 4);
        }



        public static double sigmaAs( int num)
        {
            double res = Math.Sqrt(6 * (double)(num - 2) / ((num + 1) * (num + 3)));
            return Math.Round(res, 4);
        }

        public static double sigmaKurt( int num)
        {
            double numerator = Math.Sqrt(24 * num * Math.Pow(num - 1, 2));
            double denominator = Math.Sqrt((num - 3) * (num - 2) * (num + 3) * (num + 5));
            double res = numerator / denominator;
            return Math.Round(res, 4);
        }

        public static double sigmaCkurt(int num, int now_index)
        {
            double one = Math.Sqrt(Math.Abs(SampleManager.samples[now_index].s_charac.kurt_coef) / (29 * num));
            double two = Math.Pow(Math.Pow(Math.Abs(Math.Pow(SampleManager.samples[now_index].s_charac.kurt_coef, 2) - 1), 3), (double)1 / 4);
            double res = one * two;
            return Math.Round(res, 4);
        }


        public static double sigmaW( int num, int now_index)
        {
            double res = SampleManager.samples[now_index].s_charac.pirs_coef  * Math.Sqrt((1 + 2 * Math.Pow(SampleManager.samples[now_index].s_charac.pirs_coef , 2)) / (2 * num));
            return Math.Round(res, 4);
        }

        public static double sigmaRMS( int num, int now_index)
        {
            double s2 = Math.Pow(SampleManager.samples[now_index].s_charac.rms , 2);
            double res = Math.Sqrt(2.0 / (num - 1)) * s2;
            return Math.Round(Math.Sqrt(res), 4);
        }


    }
}
