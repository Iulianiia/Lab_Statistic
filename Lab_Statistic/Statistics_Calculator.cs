using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
    static class Statistics_Calculator
    {
        public static double Sum(List<double> array, int num)
        {
            double sum = 0;
            foreach (var numb in array)
            {
                sum = sum + numb;
            }
            sum = Math.Round(sum, 4);
            return sum;
        }
        public static double Sumin2(List<double> array, int num)
        {
            double sum = 0;
            foreach (var numb in array)
            {
                sum += Math.Pow(numb, 2);
            }
            sum = Math.Round(sum, 4);
            return sum;
        }
        public static double Averagein2(List<double> array, int num)
        {
            double exp = Sumin2(array, num) / num;
            exp = Math.Round(exp, 4);
            return exp;
        }
        public static double Average(List<double> array, int num, double sum)
        {
            double exp = sum/ num;
            exp = Math.Round(exp, 4);
            return exp;
        }

        public static double Dispersion(List<double> array, int num, double sum)
        {
            List<double> array2 = new List<double>();
            double exp = Averagein2(array, num);
            for (int i = 0; i < array.Count; i++)
                array2.Add(Math.Pow(array[i] - exp, 2));
            double dispersion = sum / (num - 1);
            dispersion = Math.Round(dispersion, 4);
            return dispersion;
        }
        public static double RMS(List<double> array, int num, double average)
        {
            List<double> array2 = new List<double>();
            for (int i = 0; i < array.Count; i++)
                array2.Add(Math.Pow(array[i] - average, 2));
            double sum = Sum(array2, num);
            double s = sum / (num - 1);
            s = Math.Round(Math.Sqrt(s), 4);
            return s;
        }
        public static double Mediana(List<double> array, int num)
        {
            double[] newarr = new double[num];
            array.CopyTo(newarr);
            Array.Sort(newarr);
            double mediana;
            int index = num / 2;
            if (num % 2 == 0)
            {
                mediana = (newarr[index] + newarr[index - 1]) / 2;
            }
            else
            {
                mediana = newarr[index];
            }
            mediana = Math.Round(mediana, 4);
            return mediana;
        }
        public static double Truncated_Mean(List<double> array, int num, double a)
        {
            int k = (int)(num * a);
            double exp;
            double sum = 0;
            for (int i = k; i < num - k + 1; i++)
                sum = sum + array[i];
            exp = sum / (num - 2 * k);
            exp = Math.Round(exp, 4);
            return exp;
        }
        public static double Asym_Coef(List<double> array, int num, double average, double rms)
        {
            rms = Math.Pow(rms, 3);
            double sum = 0;
            for (int i = 0; i < num; i++)
            {
                sum = sum + Math.Pow(array[i] - average, 3);
            }
            double coefass = sum / (rms * num);
            coefass = Math.Round(coefass, 4);
            return coefass;
        }
        public static double Asym_Coef_unmoved (List<double> array, int num, double average, double rms)
        {
            double asym_coef = Asym_Coef(array, num, average, rms);
            double asym_Coef_unmoved = Math.Sqrt(num * (num - 1)) / (num - 2) * asym_coef;

            return Math.Round(asym_Coef_unmoved, 4);
        }
        public static double MAD(List<double> array, int num, double mediana)
        {
            List<double> arr1 = Diference(array, num, mediana);
            double new_mediana = Mediana(arr1, num);
            double Mad = 1.483 * new_mediana;
            return Math.Round(Mad, 4);
        }
        public static List<double> Diference(List<double> array, int num, double mediana)
        {
            List<double> Arrdiference = new List<double>();
            for (int i = 0; i < num; i++)
            {
                Arrdiference.Add(Math.Abs(array[i] - mediana));
            }
            return Arrdiference;
        }
        public static double Kurt_coef (List<double> array, int num, double average, double rms)
        {
            rms = Math.Pow(rms, 4);
            double sum = 0;
            for (int i = 0; i < num; i++)
            {
                sum = sum + Math.Pow(array[i] - average, 4);
            }
            double kurt = sum / (rms * num);
            return kurt;
        }
        public static double Kurt_coef_unmoved (List<double> array, int num, double average, double rms)
        {
            double E = Kurt_coef(array, num, average, rms);
            double E2 = (Math.Pow(num, 2) - 1) / ((num - 2) * (num - 3)) * (E - 3 + 6 / (num + 1));
            return Math.Round(E2, 4);
        }
        public static double Kontr_kurt(List<double> array, int num,double kurt_coef_unmoved)
        {
            return Math.Round(1 / Math.Sqrt(Math.Abs(kurt_coef_unmoved)), 4);
        }
        public static double Variat_coef (List<double> array, int num, double mad)
        {
            double median = Mediana(array, num);
            return Math.Round(mad / median, 4);
        }
        public static double Pirs_coef(List<double> array, int num, double average,double rms)
        {
            if (average != 0)
                return Math.Round(rms / average, 4);
            else
                return 0;
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
            return Walshsarray;
        }
        public static double Walsh_mediana (List<double> array, int num)
        {
            List<double> walsh_arr = Walsh(array, num);
            int n = walsh_arr.Count;
            return Mediana(walsh_arr, n);
        }

        public static double RMS_shifted(List<double> array, int num, double average)
        {
            List<double> array2 = new List<double>();
            for (int i = 0; i < array.Count; i++)
                array2.Add(Math.Pow(array[i], 2) - Math.Pow(average, 2));
            double sum = Math.Sqrt(Sum(array2, num));
            double s = sum * num / (num - 1);
            s = Math.Round(s, 4);
            return s;
        }

    }
}
