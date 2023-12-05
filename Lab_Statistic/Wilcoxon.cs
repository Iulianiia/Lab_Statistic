using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matStat
{
    public struct RankItem
    {
        public double Value { get; set; }
        public string Group { get; set; }
    }
    static public class Wilcoxon
    {

        static public double  wilcoxon_rank(List<double> rang1, List<double> rang2, int n1, int n2)
        {
            double W1 = rang1.Sum();
            double W2 = rang2.Sum();
            double Ew = (n1 * (n1 + n2 + 1)) / 2.0;
            double Dw = n1 * n2 * (n1 + n2 + 1) / 12.0;
            double w;
            w = (W1 - Ew) / Math.Sqrt(Dw); 
           
           return w;
        }
        static public double maxW(List<double> rang1, List<double> rang2)
        {
            double W1 = rang1.Sum();
            double W2 = rang2.Sum();

            if (W1 > W2)
            {
                return W1;
            }
            else
            {
                return W2;
            }



        }
        static public double Kruskal_Wallis (List<List<double>> r, List<int> n)
        {
            int count = r.Count;
            double[] W = new double[count];
            double[] Ew = new double[count];
            double[] Dw = new double[count];
            for (int i = 0; i< count; i++)
            {
                W[i] = 1.0 / n[i] * r[i].Sum();
            }
            int N = n.Sum();
            double H = 0;
            for(int i =0; i< count; i++)
            {
                H += Math.Pow((W[i] - (N + 1) / 2.0), 2) / ((N + 1) * (N - n[i]) / (12 * n[i])) * (1 - (double)n[i] / N);
            }
            return H;

        }
    }
}
