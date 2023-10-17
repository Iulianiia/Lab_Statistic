using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
    public static class Transformation
    {
        public static List<double> ToSquare(List<double> array, int num)
        {
            List<double> arrayIN2 = new List<double>();
            for (int i = 0; i < num; i++)
            {
                arrayIN2.Add(Math.Pow(array[i], 2));
            }
            return arrayIN2;
        }
        public static List<double> Walsh(List<double> array, int num)
        {
            List<double> Walshsarray = new List<double>();
            int y = 0;
            for (int i = 0; i < num; i++)
            {
                for (int j = i + 1; j < num; j++)
                {
                    Walshsarray.Add((array[i] + array[j]) / 2);
                    y++;
                }
            }
            //  MessageBox.Show( Walshsarray.Count().ToString());

            return Walshsarray;
        }
        public static List<double> diferens(List<double> array, int num, double mediana)
        {

            List<double> Arrdiferens = new List<double>();
            for (int i = 0; i < num; i++)
            {
                Arrdiferens.Add(Math.Abs(array[i] - mediana));
                //  MessageBox.Show((array[i] - median).ToString());
            }
            return Arrdiferens;
        }

        public static List<double> standartization(List<double> array, int num, double exp, double rms)
        {
            List<double> standart = new List<double>();
            for (int i = 0; i < num; i++)
            {
                standart.Add(Math.Round((array[i] - exp) / rms, 4));
            }
            return standart;
        }
        public static List<double> landslide(List<double> array, int num)
        {
            double min = array.Min();
            List<double> landSlide = new List<double>();
            for (int i = 0; i < num; i++)
            {
                landSlide.Add(array[i] + Math.Abs(min) + 0.001);
            }
            return landSlide;
        }
        public static List<double> landslidefrom1(List<double> array, int num)
        {
            double min = array.Min();
            List<double> landSlide = new List<double>();
            for (int i = 0; i < num; i++)
            {
                landSlide.Add(array[i] + Math.Abs(min) + 1);
            }
            return landSlide;
        }
        public static List<double> logarif(List<double> array, int num)
        {
            List<double> log = new List<double>();
            for (int i = 0; i < num; i++)
            {
                log.Add(Math.Round(Math.Log(array[i]), 4));
            }
            return log;
        }
        public static List<double> clearAnomal(List<double> array, int num, double x, double s, double contr_kur)
        {
            List<double> newarr = new List<double>();
            double t = 1.2 + 3.6 * (1 - contr_kur) * Math.Log((double)num / 10.0);
            double a , b ;
            a = x - t * s;
            b = x + t * s;
            if (a > b)
            {
                t = 1.55 + 0.8 * Math.Sqrt(Math.Abs((1 - contr_kur ))) * Math.Log((double)num / 10.0);
                a = x - t * s;
                b = x + t * s;
            }
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i] > a & array[i] < b)
                    newarr.Add(array[i]);
            }
            return newarr;
        }

    }
}
