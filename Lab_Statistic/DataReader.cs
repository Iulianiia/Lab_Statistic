using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab_Statistic
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class DataReader
    {
        public static List<List<double>> ReadMultiColumnData(string fname)
        {
            List<List<double>> columns = new List<List<double>>();

            using (StreamReader sr = new StreamReader(fname))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    while (columns.Count < parts.Length)
                    {
                        columns.Add(new List<double>());
                    }

                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (double.TryParse(parts[i], out double value))
                        {
                            columns[i].Add(value);
                        }
                    }
                }
            }

            return columns;
        }
    }

}