using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;

namespace Lab_Statistic
{
    public partial  class Form1 : Form
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
                SampleManager.AddSample(data, w);
                int index = SampleManager.i - 1;
                panel1.Controls.Add(SampleManager.samples[index ].panel);
                SampleManager.samples[index].SampleClicked += Sample_Clicked;
                SampleManager.samples[index].PanelClicked += Panel_Clicked; ;
            }

        }

        public void Panel_Clicked(object sender, EventArgs e)
        {

                ContextMenu contextmenu = new ContextMenu(SampleManager.samples[SampleManager.now_index].button);

        }


        public void Sample_Clicked(object sender, EventArgs e)
        {
            Sample clickedSample = (Sample)sender; // Приведення `sender` до типу `Sample`
            string sampleName = clickedSample.Name;
            int index;
            int.TryParse(sampleName, out index );
            SampleManager.now_index = index - 1; int now_index = index - 1;
            Sample sample1 = SampleManager.samples[SampleManager.now_index];
            int m; double h;
            (m, h) = M_H.m_h();
            chart1.Series.Clear();
            chart1.Series.Add("graf"); 
            chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.Maximum = Math.Round (sample1 .Max,2);
            chart1.ChartAreas[0].AxisX.Minimum = Math.Round(sample1 .Min,2);
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Interval = Math.Round(h, 1);
            chart1.ChartAreas[0].AxisY.Interval = 0.1;
            chart1.Series[0]["PointWidth"] = "1";


            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.ChartAreas[0].AxisX.Maximum = Math.Round(sample1 .Max,2);
            chart2.ChartAreas[0].AxisX.Minimum = Math.Round(sample1 .Min,2);
            chart2.ChartAreas[0].AxisY.Maximum = 1;
            chart2.ChartAreas[0].AxisY.Minimum = 0;
            chart2.ChartAreas[0].AxisY.Interval = 0.05;
            chart2.ChartAreas[0].AxisY.IntervalOffset = 0.05;



            chart1.Series[0] = Grafics.CreateHistogram(chart1.Series[0], sample1 .Data , h, m);
            chart2.Series[0] = Grafics.Fdistclas(chart2.Series[0], sample1 .Data, h, m);
            chart2.Series[1] = Grafics.Fdistribut(chart2.Series[1], sample1 .Data, h, m);

            double criticalValue = QuantileT.quantileT(0.1, sample1.N -1 );
            richTextBox1.Text = "Математичне сподівання" + "\n---------------------------------\n" +
                "СКВ" +
                 "\n---------------------------------\n"+
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
                Math.Round(sample1.s_charac.walsh_med , 4).ToString() +
                                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.turn_mean , 4).ToString() +
                                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.mad, 4).ToString() +
                                "\n---------------------------------\n" +
                Math.Round(sample1.s_charac.variat_coef , 4).ToString();



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
                Math.Round( Sigma.sigmaKurt(sample1.N), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(Sigma.sigmaCkurt(sample1.N, now_index), 4).ToString() +
                "\n---------------------------------\n" +
                Math.Round(Sigma.sigmaW(sample1.N, now_index), 4).ToString();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int now_index1, now_index2;
            string[] indexes = textBox1.Text.Split(' ');
            int.TryParse(indexes[0], out now_index1);
            int.TryParse(indexes[1], out now_index2);
            now_index1 -= 1; now_index2 -= 1;
            Sample sample1 = SampleManager.samples[now_index1];
            Sample sample2 = SampleManager.samples[now_index2];

            //  chart1.Series[0].Points.Clear();
            chart1.ChartAreas[0].AxisX.Maximum = Math.Round(sample1.Max, 4);
            chart1.ChartAreas[0].AxisX.Minimum = Math.Round(sample1.Min, 4);
            chart1.ChartAreas[0].AxisY.Maximum = Math.Round(sample2.Max, 4);
            chart1.ChartAreas[0].AxisY.Minimum = Math.Round(sample2.Min, 4);

            chart2.ChartAreas[0].AxisX.Maximum = Math.Round(sample1.Max, 4);
            chart2.ChartAreas[0].AxisX.Minimum = Math.Round(sample1.Min, 4);
            chart2.ChartAreas[0].AxisY.Maximum = Math.Round(sample2.Max, 4);
            chart2.ChartAreas[0].AxisY.Minimum = Math.Round(sample2.Min, 4);

            // chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            //  chart1.Series[0].MarkerColor = Color.FromArgb(128, Color.Blue); // Задає прозорість маркера
            //  chart1.Series[0].MarkerBorderColor = Color.Blue; // Колір межі маркера
            (int m1, double h1) = M_H.m_h(now_index1 );
            (int m2, double h2) = M_H.m_h(now_index2);
            double[][] p = Grafics.Pij(now_index1, now_index2);
            Grafics.fxy_2D_(chart1, m1, h1, m2, h2, p, sample1.Min, sample2.Min);
            Grafics.fxy_2D(chart1, now_index1, now_index2);
            Grafics.Fxy_2D_(chart2, m1, h1, m2, h2, p, sample1.Min, sample2.Min);
            Grafics.fxy_2D(chart2, now_index1, now_index2);


            double criticalValue = QuantileT.quantileT(0.1, sample1.N - 1);
            richTextBox1.Text = "Математичне сподівання" + "\n---------------------------------\n" +
               "СКВ";
            richTextBox2.Text = Math.Round((sample1.s_charac.average - criticalValue * Sigma.sigmaAv(sample1.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample2.s_charac.average - criticalValue * Sigma.sigmaAv(sample2.N, now_index2)), 4).ToString() +
                "\n---------------------------------\n" +
               Math.Round((sample1.s_charac.rms - criticalValue * Sigma.sigmaRMS(sample2.N, now_index1)), 4).ToString() + "; " +
                Math.Round((sample1.s_charac.rms - criticalValue * Sigma.sigmaRMS(sample2.N, now_index2)), 4).ToString();


            richTextBox3.Text = Math.Round(sample1.s_charac.average, 4).ToString()+"; " +
                Math.Round(sample2.s_charac.average, 4).ToString() 
                + "\n---------------------------------\n" +
                   Math.Round(sample1.s_charac.rms, 4).ToString() + "; " +
                    Math.Round(sample2.s_charac.rms, 4).ToString();
            richTextBox4.Text = Math.Round((sample1.s_charac.average + criticalValue * Sigma.sigmaAv(sample1.N, now_index1)), 4).ToString()+ "; "+
                Math.Round((sample2.s_charac.average + criticalValue * Sigma.sigmaAv(sample2.N, now_index2)), 4).ToString() 
                + "\n---------------------------------\n" +
               Math.Round((sample1.s_charac.rms + criticalValue * Sigma.sigmaRMS(sample1.N, now_index1)), 4).ToString() + "; "+
                Math.Round((sample2.s_charac.rms + criticalValue * Sigma.sigmaRMS(sample2.N, now_index2)), 4).ToString();
            richTextBox5.Text = Math.Round(Sigma.sigmaAv(sample1.N, now_index1), 4).ToString()+"; "+
                Math.Round(Sigma.sigmaAv(sample2.N, now_index2), 4).ToString() 
                + "\n---------------------------------\n" +
               Math.Round(Sigma.sigmaRMS(sample1.N, now_index1), 4).ToString()+"; " +
                Math.Round(Sigma.sigmaRMS(sample2.N, now_index2), 4).ToString();


        }


    }
}
