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
            Sample sample = new Sample(data, w, i, name);
            samples.Add(sample);
        }

        
    }

    public static class SampleManager_two_dimensional
    {
        public static List<Two_dimensional_sample> samples2D = new List<Two_dimensional_sample>();
        public static int i { get; set; }
        public static int now_index { get; set; }
        public static void AddSample(Sample x, Sample y)
        {
            i = samples2D.Count + 1;
            now_index = samples2D.Count;
            Two_dimensional_sample sample2D = new Two_dimensional_sample(x,y, (i).ToString());
            samples2D.Add(sample2D);
        }


    }

}
