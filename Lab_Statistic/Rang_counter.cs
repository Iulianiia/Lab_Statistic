using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matStat
{
    public static class Rang_counter
    {
      
        public class Rang_Items
        {

            public double Value { get; set;}
            public int Group { get; set; }

            public double Rang { get; set; }
            public int i { get; set; }
        }
        static public (List<double>,bool) CalculateRanks(List<double> arr, int n)
        {
            List<Rang_Items> ranks = new List<Rang_Items>();
              for (int j = 0; j < n; j++)
              {
                Rang_Items el = new Rang_Items();
                el.Value = arr[j];
                el.i = j;
                el.Group = 0;
                ranks.Add(el);
              }
            ranks = ranks.OrderBy(x => x.Value).ToList();
            int N = n;
            int numTied = 0;
            bool alarm = false;
            double lastValue = ranks[n-1].Rang ;
            for (int i = 0; i < N; i++)
            {
                if (ranks[i].Value == lastValue & i > 0)
                {
                    numTied++;
                    if (i == N - 1)
                        for (int k = i - numTied; k < i; k++)
                        {
                            ranks[k].Rang = i - numTied / 2.0 + 1;
                        }
                    alarm = true;
                }
                else
                {
                    if (numTied > 0)
                    {
                        for (int k = i - 1 - numTied; k < i; k++)
                        {
                            ranks[k].Rang = i - numTied / 2.0;
                        }
                        ranks[i].Rang = i + 1;
                        numTied = 0;
                    }
                    else
                    {
                        ranks[i].Rang = i + 1;
                    }
                }
                lastValue = ranks[i].Value;
            }
           List<double> ranges = new List<double>();
            for (int i = 0; i < N; i++)
            {
                ranges.Add(0);
            }
            for (int i = 0; i < N; i++)
            {
                ranges[ranks[i].i ] =ranks[i].Rang;
            }
            return (ranges, alarm);
        }

    static public List<List<double>> CalculateRanks(List<List<double>> arr, List<int> n)
        {
            List<Rang_Items> ranks = new List<Rang_Items>();
            int K = arr.Count;
            for (int i = 0; i < K; i++)
            {
               // arr[i].Sort();
               for(int j =0; j < n[i]; j++)
                {
                    Rang_Items el = new Rang_Items();
                    el.Value = arr[i][j];
                    el.i = j;
                    el.Group = i;
                    ranks.Add(el);
                }
            }
            ranks = ranks.OrderBy(x => x.Value).ToList();
            int N = n.Sum();
            double[] indexes = new double[K];
            int numTied = 0;
            double lastValue = ranks[0].Value;
            for(int i = 0; i < N; i++)
            {
                if (ranks[i].Value == lastValue & i >0)
                {
                    numTied++;
                    if (i == N -1)
                        for (int k = i - numTied; k < i; k++)
                        {
                            ranks[k].Rang = i - numTied / 2.0  +1 ;
                        }
                }
                else
                {
                    if (numTied > 0)
                    {
                        for (int k = i -1 - numTied; k < i; k++)
                        {
                            ranks[k].Rang = i - numTied / 2.0 ;
                        }
                        ranks[i].Rang = i + 1;
                        numTied = 0;
                    }
                    else
                    {
                        ranks[i].Rang = i + 1;
                    }
                }
                lastValue = ranks[i].Value;
            }
            List<List<double>> ranges = new List<List<double>>();
            for(int i = 0; i < K; i++)
            {
                ranges.Add(new List<double>());
            }
            for (int i = 0; i < N; i++)
            {
                ranges[ranks[i].Group].Add(0);
            }
            for (int i = 0; i < N; i++)
            {
                ranges[ranks[i].Group][ranks[i].i] = ranks[i].Rang;
            }
             return ranges;
        }

    }
}
