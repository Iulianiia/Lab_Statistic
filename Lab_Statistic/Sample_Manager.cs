using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
    public static class SampleManager
    {
        public static List<Sample> samples = new List<Sample>();
        public static int i { get; set; }
        public static int now_index =i;
        public static void AddSample(List<double> data, double w, string name)
        {
            i = samples.Count + 1;
            Sample sample = new Sample(data, w, (i).ToString(), name);
            samples.Add(sample);
        }

        
    }

}
