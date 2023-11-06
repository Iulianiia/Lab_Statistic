using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Statistic
{
    public partial class Form1 
    {
        public  void Characteristics_for_2 (int now_index1, int now_index2, Sample sample1, Sample sample2, int N, double r, double chsi2)
        {
            double criticalValue = QuantileT.quantileT(0.1, N - 1);
            richTextBox1.Text = "Математичне сподівання" + "\n---------------------------------\n" +
                "СКВ" + "\n---------------------------------\n" +
                "Коефіціент кореляції" + "\n---------------------------------\n" +
                "χ² критерій";

            richTextBox2.Text = Math.Round((sample1.s_charac.average - criticalValue * Sigma.sigmaAv(sample1.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample2.s_charac.average - criticalValue * Sigma.sigmaAv(sample2.N, now_index2)), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round((sample1.s_charac.rms - criticalValue * Sigma.sigmaRMS(sample2.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample1.s_charac.rms - criticalValue * Sigma.sigmaRMS(sample2.N, now_index2)), 4).ToString();

            richTextBox3.Text = Math.Round(sample1.s_charac.average, 4).ToString() + "; " +
                Math.Round(sample2.s_charac.average, 4).ToString()
                + "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.rms, 4).ToString() + "; " +
                Math.Round(sample2.s_charac.rms, 4).ToString()
                + "\n---------------------------------\n" +
                Math.Round(r, 4) + "\n---------------------------------\n" +
                Math.Round(chsi2, 4);
            richTextBox4.Text = Math.Round((sample1.s_charac.average + criticalValue * Sigma.sigmaAv(sample1.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample2.s_charac.average + criticalValue * Sigma.sigmaAv(sample2.N, now_index2)), 4).ToString()
                + "\n---------------------------------\n" +
                Math.Round((sample1.s_charac.rms + criticalValue * Sigma.sigmaRMS(sample1.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample2.s_charac.rms + criticalValue * Sigma.sigmaRMS(sample2.N, now_index2)), 4).ToString();
            richTextBox5.Text = Math.Round(Sigma.sigmaAv(sample1.N, now_index1), 4).ToString() + "; " +
                Math.Round(Sigma.sigmaAv(sample2.N, now_index2), 4).ToString()
                + "\n---------------------------------\n" +
                Math.Round(Sigma.sigmaRMS(sample1.N, now_index1), 4).ToString() + "; " +
                Math.Round(Sigma.sigmaRMS(sample2.N, now_index2), 4).ToString();
        }
    }
}
