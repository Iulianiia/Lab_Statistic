
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
            aver_xy /= N;
            return aver_xy;
        }
        public static double Chsi_cryt(double[][] p, double[][] p_,double Mx, double My)
        {
            double chsi2 =0;
            for (int i = 0; i < Mx; i++)
            {
                for (int j = 0; j < My; j++)
                {
                    if(p_[i][j] !=0)
                       chsi2 += Math.Pow(p[i][j] - p_[i][j],2) / p_[i][j];
                }
            }
            return chsi2;
        }
    }
}
