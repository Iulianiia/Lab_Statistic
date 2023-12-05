using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace matStat
{
     public class Comparison
     {
        public List <double> Compare (List<double> arr1, List <double> arr2, int n)
        {
            List<double> z = new List<double>();
            for ( int i = 0; i < n; i++)
            {
                z.Add(arr1[i] - arr2[i]);
            }
            return z;
        }
     }
}
