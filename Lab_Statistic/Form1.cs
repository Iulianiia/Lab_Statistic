using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using matStat;

namespace Lab_Statistic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel1.AutoScroll = true; panel1.AutoSize = true;


        }



        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string fname = openFileDialog1.FileName;
            var columns = DataReader.ReadMultiColumnData(fname);

            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];
                List<double> data = new List<double>();
                int w = 0;
                foreach (var value in column)
                {
                    if (value < 0)
                        w = -1;
                    data.Add((double)value);
                }
                SampleManager.AddSample(data, w, Path.GetFileName(fname));
                int index = SampleManager.i - 1;
                panel1.Controls.Add(SampleManager.samples[index].panel);
                SampleManager.samples[index].SampleClicked += Sample_Clicked;
                SampleManager.samples[index].PanelClicked += Panel_Clicked;
            }


        }

        public void Panel_Clicked(object sender, EventArgs e)
        {

            ContextMenu1 contextmenu = new ContextMenu1(SampleManager.samples[SampleManager.now_index].button);
            contextmenu.MenuItemClicked += Contextmenu_MenuItemClicked;


        }

        private void Contextmenu_MenuItemClicked(object sender, EventArgs e)
        {
            if (ContextMenu1.ind == 1)
            {
                SampleManager.AddSample(ContextMenu1.Standartarray, ContextMenu1.w, SampleManager.samples[SampleManager.now_index].button.Name + "(stand)");
                int index = SampleManager.i - 1;
                panel1.Controls.Add(SampleManager.samples[index].panel);
                SampleManager.samples[index].SampleClicked += Sample_Clicked;
                SampleManager.samples[index].PanelClicked += Panel_Clicked;

            }
            else if (ContextMenu1.ind == 2)
            {
                SampleManager.AddSample(ContextMenu1.Logarray, ContextMenu1.w, SampleManager.samples[SampleManager.now_index].button.Name + "(log)");
                int index = SampleManager.i - 1;
                panel1.Controls.Add(SampleManager.samples[index].panel);
                SampleManager.samples[index].SampleClicked += Sample_Clicked;
                SampleManager.samples[index].PanelClicked += Panel_Clicked;
            }
            else if (ContextMenu1.ind == 3)
            {
                SampleManager.AddSample(ContextMenu1.WithoutAnom, ContextMenu1.w, SampleManager.samples[SampleManager.now_index].button.Name + "(without anom)");
                int index = SampleManager.i - 1;
                panel1.Controls.Add(SampleManager.samples[index].panel);
                SampleManager.samples[index].SampleClicked += Sample_Clicked;
                SampleManager.samples[index].PanelClicked += Panel_Clicked;
            }
            else if (ContextMenu1.ind == 4)
            {
                SampleManager.AddSample(ContextMenu1.Binar, ContextMenu1.w, SampleManager.samples[SampleManager.now_index].button.Name + "(binar)");
                int index = SampleManager.i - 1;
                panel1.Controls.Add(SampleManager.samples[index].panel);
                SampleManager.samples[index].SampleClicked += Sample_Clicked;
                SampleManager.samples[index].PanelClicked += Panel_Clicked;
            }
        }
        public void  Sample2D_Clicked(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add("graf");
            chart1.Series[0].Points.Clear();

            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();

            Two_dimensional_sample clickedSample = (Two_dimensional_sample)sender; // Приведення `sender` до типу `Sample`
            string sampleName = clickedSample.Ind;
            int index;
            int.TryParse(sampleName, out index);
            SampleManager_two_dimensional.now_index = index - 1;
            Grafics_2D(clickedSample.X.Ind-1, clickedSample.Y.Ind-1, clickedSample.X, clickedSample.Y);

            if (clickedSample.X.Data[1] == 0 || clickedSample.X.Data[1] == 1)
            {
                Corr_an_bin(clickedSample.N, clickedSample.X.Ind - 1, clickedSample.Y.Ind - 1, clickedSample.X, clickedSample.Y);
            }
            else
            {
                Corr_analys(clickedSample.N, clickedSample.X.Ind - 1, clickedSample.Y.Ind - 1, SampleManager_two_dimensional.samples2D[SampleManager_two_dimensional.now_index]);
            }


        }

        public void Sample_Clicked(object sender, EventArgs e)
        {
            Sample clickedSample = (Sample)sender; // Приведення `sender` до типу `Sample`
            string sampleName = clickedSample.Ind.ToString();
            int index;
            int.TryParse(sampleName, out index);
            SampleManager.now_index = index - 1; int now_index = index - 1;
            Sample sample1 = SampleManager.samples[SampleManager.now_index];
            int m; double h;
            (m, h) = (sample1.m, sample1.h);


            //sample1.rangs = Rang_counter.CalculateRanks(sample1.Data, sample1.N);

            chart1.Series.Clear();
            chart1.Series.Add("graf");
            chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.Maximum = Math.Round(sample1.Max, 2);
            chart1.ChartAreas[0].AxisX.Minimum = Math.Round(sample1.Min, 2);
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Maximum = Double.NaN;
            chart1.ChartAreas[0].AxisX.Interval = Math.Round(h, 1);
            chart1.ChartAreas[0].AxisY.Interval = 0.1;
            chart1.Series[0]["PointWidth"] = "1";

            chart2.Series.Clear();
            chart2.Series.Add("Fx");
            chart2.Series.Add("Fx_class");
            chart2.ChartAreas[0].AxisX.Maximum = Math.Round(sample1.Max, 2);
            chart2.ChartAreas[0].AxisX.Minimum = Math.Round(sample1.Min, 2);
            chart2.ChartAreas[0].AxisY.Maximum = 1;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Interval = 0.05;
            chart2.ChartAreas[0].AxisY.IntervalOffset = 0.05;



            chart1.Series[0] = Grafics.CreateHistogram(chart1.Series[0], sample1.Data, h, m);
            chart2.Series[0] = Grafics.Fdistclas(chart2.Series[0], sample1.Data, h, m);
            chart2.Series[1] = Grafics.Fdistribut(chart2.Series[1], sample1.Data, h, m);

            double criticalValue = QuantileT.quantileT(0.1, sample1.N - 1);
            richTextBox1.Text = "Математичне сподівання" + "\n---------------------------------\n" +
                "СКВ" +
                 "\n---------------------------------\n" +
                "Коефіцієнт асиметрії" +
                "\n---------------------------------\n" +
                "Коефіціент Ексцесу" +
                "\n---------------------------------\n" +
                "Коефіціент Контрксцесу" +
                "\n---------------------------------\n" +
                "Коефіціент Пірсона" +
                "\n---------------------------------\n" +
                "Медіана" +
                "\n---------------------------------\n" +
                "Мадіана Уолша" +
                "\n---------------------------------\n" +
                "Усічене середнє" +
                "\n---------------------------------\n" +
                "MAD" +
                "\n---------------------------------\n" +
                "Коефіціент варіації";


            richTextBox2.Text = Math.Round((sample1.s_charac.average - criticalValue * Sigma.sigmaAv(sample1.N, now_index)), 4).ToString() + "\n---------------------------------\n" +
                Math.Round((sample1.s_charac.rms - criticalValue * Sigma.sigmaRMS(sample1.N, now_index)), 4).ToString() +
                 "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.asym_coef - criticalValue * Sigma.sigmaAs(sample1.N), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.kurt_coef - criticalValue * Sigma.sigmaKurt(sample1.N), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.ckurt_coef - criticalValue * Sigma.sigmaCkurt(sample1.N, now_index), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.pirs_coef - criticalValue * Sigma.sigmaW(sample1.N, now_index), 4).ToString();



            richTextBox3.Text = Math.Round(sample1.s_charac.average, 4).ToString() + "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.rms, 4).ToString() +
                 "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.asym_coef, 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.kurt_coef, 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.ckurt_coef, 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.pirs_coef, 4).ToString() +
                                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.median, 4).ToString() +
                                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.walsh_med, 4).ToString() +
                                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.turn_mean, 4).ToString() +
                                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.mad, 4).ToString() +
                                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.variat_coef, 4).ToString();



            richTextBox4.Text = Math.Round((sample1.s_charac.average + criticalValue * Sigma.sigmaAv(sample1.N, now_index)), 4).ToString() + "\n---------------------------------\n" +
                Math.Round((sample1.s_charac.rms + criticalValue * Sigma.sigmaRMS(sample1.N, now_index)), 4).ToString() +
                 "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.asym_coef + criticalValue * Sigma.sigmaAs(sample1.N), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.kurt_coef + criticalValue * Sigma.sigmaKurt(sample1.N), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.ckurt_coef + criticalValue * Sigma.sigmaCkurt(sample1.N, now_index), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.pirs_coef + criticalValue * Sigma.sigmaW(sample1.N, now_index), 4).ToString();



            richTextBox5.Text = Math.Round(Sigma.sigmaAv(sample1.N, now_index), 4).ToString() + "\n---------------------------------\n" +
                Math.Round(Sigma.sigmaRMS(sample1.N, now_index), 4).ToString() +
                 "\n---------------------------------\n" +
                Math.Round(Sigma.sigmaAs(sample1.N), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(Sigma.sigmaKurt(sample1.N), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(Sigma.sigmaCkurt(sample1.N, now_index), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(Sigma.sigmaW(sample1.N, now_index), 4).ToString();

        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                int now_index1, now_index2;
                string[] indexes = textBox1.Text.Split(' ');
                int.TryParse(indexes[0], out now_index1);
                int.TryParse(indexes[1], out now_index2);
                now_index1 -= 1;
                now_index2 -= 1;
                Sample sample1 = SampleManager.samples[now_index1];
                Sample sample2 = SampleManager.samples[now_index2];
                int N1 = sample1.N;
                int N2 = sample2.N;
                int N;
                if (N1 < N2)
                    N = N1;
                else
                    N = N2;

                SampleManager_two_dimensional.AddSample (sample1,sample2);
                int index = SampleManager_two_dimensional.i - 1;
                panel10.Controls.Add(SampleManager_two_dimensional.samples2D[index].button);
                SampleManager_two_dimensional.samples2D[index].Sample2D_Clicked  += Sample2D_Clicked;
                Grafics_2D(now_index1, now_index2, sample1, sample2);
                if (sample1.Data[1] == 0 || sample1.Data[1] == 1)
                {
                    Corr_an_bin(N, now_index1, now_index2, sample1, sample2);
                }
                else
                {
                    Corr_analys(N, now_index1, now_index2, SampleManager_two_dimensional.samples2D[SampleManager_two_dimensional.now_index ] );
                }
              
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Homogeneity_criteria.homogeneity_criteria(toolStripComboBox1, richTextBox7,textBox1 );
        }
    }
}

       