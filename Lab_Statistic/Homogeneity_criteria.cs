using Accord.Statistics.Distributions.Univariate;
using MathNet.Numerics.Distributions;
using matStat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab_Statistic
{
    public static class Homogeneity_criteria
    {
        public static void homogeneity_criteria( ToolStripComboBox toolStripComboBox1, RichTextBox richTextBox7,  TextBox textBox1)
        {
            if (toolStripComboBox1.SelectedIndex == 0)
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    string[] sample = textBox1.Text.Split(' ');

                    int i1, i2;

                    int.TryParse(sample[0], out i1);
                    int.TryParse(sample[1], out i2);
                    i1 = i1 - 1; i2 = i2 - 1;
                    Comparison comparison1 = new Comparison();
                    if (i1 != i2 & i1 <= SampleManager.i  & i2 <= SampleManager.i )
                    {
                        List<double> arr1 = SampleManager.samples[i1].Data;
                        List<double> arr2 = SampleManager.samples[i2].Data;
                        int n = 0;

                        if (arr1.Count == arr2.Count)
                        {
                            n = arr1.Count;
                            List<double> z = comparison1.Compare(arr1, arr2, n);
                            double[] U = new double[n];
                            int cut = 0;
                            for (int i = 0; i < n; i++)
                            {
                                if (z[i] > 0)
                                    U[i] = 1;
                                else if (z[i] < 0)
                                    U[i] = 0;
                                else
                                {
                                    cut++;
                                    i--;
                                }
                            }

                            U = U.Skip(0).Take(n - cut).ToArray();
                            double S1 = U.Sum();
                            double S8 = (2 * S1 - 1 - n) / Math.Sqrt(n);
                            NormalDistribution normal = new NormalDistribution();
                            double u12 = normal.InverseDistributionFunction(1 - 0.05);
                            if (S8 < u12)
                                richTextBox7.Text = "Вибірки мають однаковий розподіл зідно критерію знаків, S* = " + Math.Round(S8, 4).ToString() + "\n";
                            else
                            {
                                double med = Statistics_Calculator.Mediana(z, n);
                                richTextBox7.Text = "Вибірки мають різний розподіл зідно критерію знаків, S* = " + Math.Round(S8, 4).ToString()
                                    + "\nПараметр зсуву = " + Math.Round(med, 4).ToString();

                            }

                            double Zav = Statistics_Calculator.Average(z, n, z.Sum());
                            double Zskv =(Statistics_Calculator.RMS_shifted(z, n,Zav));
                            double t = Zav * Math.Sqrt(n) / Zskv;
                            StudentT distribution = new StudentT();
                            double quantile = distribution.InverseCumulativeDistribution(n - 1);
                            if (Math.Abs(t) > 1 - quantile)
                                richTextBox7.Text += "\nСередні не співпадають, t = " + t.ToString();
                            else
                                richTextBox7.Text += "\nCередні співпадають, t = " + t.ToString();
                        }
                        else
                            MessageBox.Show("Вибірки різної довжини!");

                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(toolStripComboBox1.Text))
                {
                    int i1, i2;

                    string[] sample = textBox1.Text.Split(' ');
                    int count = sample.Length;
                    if (count > 2)
                    {
                        int[] indexes = new int[count];
                        int index;
                        List<List<double>> arr = new List<List<double>>();
                        List<int> n = new List<int>();
                        //  int i = 0;
                        foreach (var word in sample)
                        {
                            int.TryParse(word, out index);
                            index = index - 1;
                            if (index > SampleManager.i )
                            {
                                MessageBox.Show("Не існує вибірки з таким індексом");
                                break;

                            }
                            arr.Add(SampleManager.samples[index].Data);
                            n.Add(SampleManager.samples[index].Data.Count);
                            //  i++;
                        }
                        double[] xi = new double[count];
                        double[] S2i = new double[count];
                        double x_av = 0;
                        int N = 0;
                        double numerator = 0;
                        List<List<double>> r = Rang_counter.CalculateRanks(arr, n);
                        double H = Wilcoxon.Kruskal_Wallis(r, n);
                        int numberofel = 0, denominator = 0;
                        for (int i = 0; i < count; i++)
                        {
                            numberofel = arr[i].Count;
                            double Sum = Statistics_Calculator.Sum(arr[i], numberofel);
                            xi[i] = Statistics_Calculator.Average(arr[i], numberofel, Sum);
                            S2i[i] = Statistics_Calculator.RMS_shifted (arr[i], numberofel, xi[i]);
                            numerator += S2i[i] * (numberofel - 1);
                            denominator += (numberofel - 1);

                            x_av += numberofel * xi[i];
                            N += numberofel;
                        }
                        double S2 = numerator / denominator;
                        x_av = 1.0 / N * x_av;
                        double S2m = 0, S2b = 0;
                        double B = 0, C = 0;
                        double forC1 = 0, forC2 = 0;
                        for (int i = 0; i < count; i++)
                        {
                            numberofel = arr[i].Count;
                            B += (numberofel - 1) * Math.Log(S2i[i] / S2);
                            forC1 += 1 / (numberofel - 1);
                            forC2 += numberofel - 1;

                            S2m += numberofel * Math.Pow(xi[i] - x_av, 2);
                            S2b += (numberofel - 1) * S2i[i];
                        }
                        C = 1 + 1 / (3 * (count - 1)) * (forC1 - 1 / forC2);
                        double Xi2 = -B / C;
                        ChiSquareDistribution chiSquare = new ChiSquareDistribution(count - 1);
                        if (Xi2 <= chiSquare.InverseDistributionFunction(1 - 0.05))
                            richTextBox7.Text = "Дисперсії співпадають згідно критерія Барлета, χ² = " + Math.Round(Xi2, 4).ToString();
                        else
                            richTextBox7.Text = "Дисперсії не співпадають згідно критерія Барлета, χ² = " + Math.Round(Xi2, 4).ToString();

                        S2m = 1.0 / (N - 1) * S2m;
                        richTextBox7.Text += "\nМіжгрупова варіація = " + Math.Round(S2m, 4).ToString();

                        S2b = 1.0 / (N - count) * S2b;
                        richTextBox7.Text += "\nВаріація в середені кожної вибірки = " + Math.Round(S2b, 4).ToString();
                        FDistribution fDist = new FDistribution(count - 1, N - count);
                        FDistribution fDist2 = new FDistribution(count - 1, count - 1);
                        double F = S2m / S2b;
                        double Znach_u = fDist.InverseDistributionFunction(1 - 0.05);
                        double Znach_u2 = fDist2.InverseDistributionFunction(1 - 0.05);
                        if (F <= 3)
                            richTextBox7.Text += "\nВибірки належать до однієї генеральної сукупності згідно теста Фішера, F = " + Math.Round(F, 4).ToString();
                        else
                            richTextBox7.Text += "\nВибірки не належать до однієї генеральної сукупності згідно теста Фішера, F = " + Math.Round(F, 4).ToString();

                        if (H <= 2.5)
                            richTextBox7.Text += "\nВибірки мають однаковий закон розподілу згідно критерія Крускала-Уіліса, H = " + Math.Round(H, 4).ToString();
                        else
                            richTextBox7.Text += "\nВибірки мають різний закон розподілу згідно критерія Крускала-Уіліса, H = " + Math.Round(H, 4).ToString();

                    }
                    if (count == 2)
                    {
                        int.TryParse(sample[0], out i1);
                        int.TryParse(sample[1], out i2);
                        i1 = i1-1; i2 = i2 - 1;
                        Comparison comparison1 = new Comparison();
                        if (i1 != i2 & i1 <= SampleManager.i & i2 <= SampleManager.i)
                        {

                           
                            Sample arr1 = SampleManager.samples[i1];

                            Sample arr2 = SampleManager.samples[i2];
                            int n1 = 0, n2 = 0;
                            n1 = arr1.N;
                            n2 = arr2.N;
                            // List<double> z = comparison1.Compare(arr1, arr2, n);
                            double Arr1_av = arr1.s_charac.average;
                            double Arr2_av = arr2.s_charac.average;
                            double Arr1_skv = arr1.s_charac.rms;
                            double Arr2_skv = arr2.s_charac.rms;
                            double Arr1_q_av = Math.Round(Arr1_skv / Math.Sqrt(n1), 4);
                            double Arr2_q_av = Math.Round(Arr2_skv / Math.Sqrt(n2), 4);
                            FDistribution fDist = new FDistribution(n1 - 1, n2 - 1);
                            double f = 0;
                            if (Arr1_skv > Arr2_skv)
                                f = Arr1_skv / Arr2_skv;
                            else
                                f = Arr2_skv / Arr1_skv;
                            if (f <= 2)
                            {
                                richTextBox7.Text = "Дисперсії співпадають, f = " + Math.Round(f, 4).ToString();
                                double t = Math.Round((Arr1_av - Arr2_av) / Math.Sqrt(Arr1_q_av + Arr2_q_av), 4);
                                double criticalValue = QuantileT.quantileT(0.1, n1+n2 - 1);

                                if (Math.Abs(t) > 1 - criticalValue)
                                    richTextBox7.Text += "\nСередні не співпадають, t = " + Math.Round(t, 4).ToString();
                                else
                                    richTextBox7.Text += "\nCередні співпадають, t = " + Math.Round(t, 4).ToString();
                            }
                            else
                                richTextBox7.Text = "Дисперсії не співпадають, f = " + Math.Round(f, 4).ToString();
                            List<double> join = arr1.Data.Concat(arr2.Data).ToList();
                            List<double> arr1_ = new List<double>();
                            List<double> arr2_ = new List<double>();
                            arr1_.AddRange(arr1.Data); arr2_.AddRange(arr2.Data);
                            join.Sort();
                            arr1_.Sort();
                            arr2_.Sort();
                            List<List<double>> toarr = new List<List<double>>();
                            List<int> ton = new List<int>();
                            toarr.Add(arr1_); toarr.Add(arr2_);
                            ton.Add(n1); ton.Add(n2);
                            List<List<double>> torang = Rang_counter.CalculateRanks(toarr, ton);
                            List<double> rang1 = torang[0];
                            List<double> rang2 = torang[1];
                            double w = Wilcoxon.wilcoxon_rank(rang1, rang2, n1, n2);
                            double v = Average_rang.average_rang(rang1, rang2, n1, n2);


                            int nOFjoin = join.Count;
                            double[] fDistrub1 = new double[nOFjoin];
                            double[] fDistrub2 = new double[nOFjoin];
                            int k = 0, p = 0;
                            int j = 0, l = 0;
                            for (int i = 0; i < nOFjoin; i++)
                            {
                                // k = 0;
                                for (; j < n1; j++)
                                {
                                    if (join[i] >= arr1_[j])
                                        k++;
                                }
                                fDistrub1[i] = k / (double)n1;
                                // k = 0;
                                for (; l < n2; l++)
                                {
                                    if (join[i] >= arr2_[l])
                                        p++;
                                }
                                fDistrub2[i] = p / (double)n2;
                            }
                            double max_dif = Math.Abs(fDistrub1[0] - fDistrub2[0]);
                            for (int i = 0; i < nOFjoin; i++)
                            {
                                if (Math.Abs(fDistrub1[i] - fDistrub2[i]) > max_dif)
                                    max_dif = Math.Abs(fDistrub1[i] - fDistrub2[i]);
                            }
                            double z = max_dif;
                            double Lz = 1 - Math.Exp(-2 * z * z) * (1 - (2 * z) / (3 * Math.Sqrt(nOFjoin)) + (2 * z * z) / 3 * nOFjoin) * (1 - (2 * z * z) / 3) + 4 * z / (9 * Math.Pow(nOFjoin, (double)3 / 2)) * (0.2 - 19 * z * z / 15 + 2 * z * z * z * z / 3);
                            if (1 - Lz > 0.05)
                                richTextBox7.Text += "\nВибірки однорідні відповідно критерія Смирнова Колмагорова, L(z) = " + Math.Round(Lz, 4).ToString();
                            else
                                richTextBox7.Text += "\nВибірки не однорідні відповідно критерія Смирнова Колмагорова, L(z) = " + Math.Round(Lz, 4).ToString();


                            NormalDistribution normal = new NormalDistribution(n1 + n2);
                            double u12 = normal.InverseDistributionFunction(1 - 0.05 / 2.0);
                            if (w < u12)
                                richTextBox7.Text += "\nВибірки різняться випадково відповідно критерію Вілкоксона, w = " + Math.Round(w, 4).ToString();
                            else
                                richTextBox7.Text += "\nВибірки різняться систематично відповідно критерію Вілкоксона, w = " + Math.Round(w, 4).ToString();
                            double W = Wilcoxon.maxW(rang1, rang2);
                            double U = n1 * n2 + (n1 * (n1 - n1) / 2.0) - W;
                            double Eu = n1 * n2 / 2.0;
                            double Du = n1 * n2 * (n1 + n2 + 1) / 12.0;
                            double u = (U - Eu) / Math.Sqrt(Du);
                            if (u < u12)
                                richTextBox7.Text += "\nВибірки мають однаковий розподіл відповідно критерію Мана Уїтні, u = " + Math.Round(u, 4).ToString();
                            else
                                richTextBox7.Text += "\nВибірки мають різний розподіл відповідно критерію Мана Уїтні, u = " + Math.Round(u, 4).ToString();
                            if (Math.Abs(v) <= u12)
                                richTextBox7.Text += "\nВибірки мають однаковий розподіл відповідно критерію різниці середніх рангів, v = " + Math.Round(v, 4).ToString();
                            else
                                richTextBox7.Text += "\nВибірки мають однаковий розподіл відповідно критерію різниці середніх рангів, v = " + Math.Round(v, 4).ToString();
                        }
                    }
                }
            }
        }
    }
}
