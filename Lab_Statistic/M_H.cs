using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
  public static  class M_H
    {
        public static (int m, double h) m_h()
        {
            Sample sample1 = SampleManager.samples[SampleManager.now_index];
            int m; double h;
            if (sample1.N < 100)
            {
                m = (int)Math.Sqrt(sample1.N);
                if (m % 2 == 0)
                {
                    m--;
                }
            }
            else
            {
                m = (int)Math.Pow(sample1.N, (double)1 / 3);
                if (m % 2 == 0)
                {
                    m--;
                }
            }

            h = (sample1.Max - sample1.Min) / m;
            return (m, h);
        }
        public static (int m, double h) m_h(int index)
        {
            Sample sample1 = SampleManager.samples[index];
            int m; double h;
            if (sample1.N < 100)
            {
                m = (int)Math.Sqrt(sample1.N);
                if (m % 2 == 0)
                {
                    m--;
                }
            }
            else
            {
                m = (int)Math.Pow(sample1.N, (double)1 / 3);
                if (m % 2 == 0)
                {
                    m--;
                }
            }

            h = (sample1.Max - sample1.Min) / m;
            return (m, h);
        }
    }
}
