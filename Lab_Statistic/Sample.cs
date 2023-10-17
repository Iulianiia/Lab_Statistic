using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Statistic
{
    public  class Sample
    {
        public event EventHandler PanelClicked;
        public event EventHandler SampleClicked;

        public Button button { get; private set; }
        public TableLayoutPanel table { get; private set; }
        public Panel panel { get; private set; }

        public int SInd { get; set; }
        public int LInd { get; set; }
        public int AInd { get; set; }

        public class Transformed_sample
        {
            public  List<double> Standartarray { get; set; }
            public List<double> Logarray { get; set; }
            public List<double> WithoutAnom { get; set; }
            public List<double> NativeSample { get;  set; }


        }

        public  class Characteristics 
        {
            public double sum { get; set; }
            public double average {get;set;}
            public double rms {get;set;}
            public double asym_coef {get;set;}
            public double kurt_coef{get;set;}
            public double ckurt_coef{get;set;}
            public double pirs_coef{get;set;}
            public double median {get;set;}
            public double walsh_med {get;set;}
            public double turn_mean{get;set;}
            public double mad {get;set;}
            public double variat_coef {get;set; }

        }
        public Characteristics s_charac { get; private set; }
        public Transformed_sample s_transf { get;  set; }

        public List<double> Data { get;  set; }
        public double W { get; private set; }
        public string Name { get; private set; }
        public int N { get; private set; }
        public double Max { get; set; }
        public double Min { get; set; }

        public Sample(List<double> data, double w, string name)
        {
            Transformed_sample transformed_Sample = new Transformed_sample();
            Characteristics characteristics = new Characteristics();
            s_charac = characteristics;
            s_transf = transformed_Sample;
            transformed_Sample.NativeSample = data;


            Data = data;
            W = w;
            Name = name;
            N = data.Count;
            Max = data.Max();
            Min = data.Min(); 

            // Ініціалізація контролів (кнопки, панелі і т. д.) і додавання обробника кліку
            button = new Button();
            table = new TableLayoutPanel();
            panel = new Panel();

            button.Text = Name; // Приклад встановлення тексту кнопки на основі імені вибірки
            // Додавання обробника кліку для кнопки
            button.Click += Button_Click;
            button.MouseDown += Button_MouseDown;
            //  panel.Click += Panel_MouseClick;

            table.Controls.Add(button);
            table.AutoSize = true;
            table.AutoScroll = true;
            TextBox textBox1 = new TextBox();
            textBox1.Text = "max = " +  Math.Round(data.Max(),1).ToString() + ", min = " + Math.Round(data.Min(),1).ToString() + ", l = " + N.ToString();
            textBox1.Size = new Size(150, 25);
            textBox1.Dock = DockStyle.Fill;
            table.Controls.Add(textBox1);
            table.RowCount = 1;
            table.ColumnCount = N+ 2;
            foreach (var value in data)
            {
                TextBox textBox = new TextBox();
                textBox.Text = Math.Round(value,4).ToString();
                textBox.Size = new Size(50, 25);
                textBox.Dock = DockStyle.Fill;
                table.Controls.Add(textBox);  
            }
            panel.Controls.Add(table);
            panel.AutoScroll = true; panel.AutoSize = true;
            panel.Dock = DockStyle.Top;



            int n = data.Count;
            characteristics.sum = Statistics_Calculator.Sum(Data, n);
            characteristics.average = Statistics_Calculator.Average(Data, n, characteristics.sum);
            characteristics.rms = Statistics_Calculator.RMS(Data, n, characteristics.average);
            characteristics.asym_coef = Statistics_Calculator.Asym_Coef_unmoved(Data, n, characteristics.average, characteristics.rms);
            characteristics.kurt_coef = Statistics_Calculator.Kurt_coef_unmoved(Data, n, characteristics.average, characteristics.rms);
            characteristics.ckurt_coef = Statistics_Calculator.Kontr_kurt(Data, n, characteristics.kurt_coef);
            characteristics.pirs_coef = Statistics_Calculator.Pirs_coef(Data, n, characteristics.average, characteristics.rms);
            characteristics.median = Statistics_Calculator.Mediana(Data, n);
            characteristics.walsh_med = Statistics_Calculator.Walsh_mediana(Data, n);
            characteristics.turn_mean = Statistics_Calculator.Truncated_Mean(Data, n, 0.3);
            characteristics.mad = Statistics_Calculator.MAD(Data, n, characteristics.median);
            characteristics.variat_coef = Statistics_Calculator.Variat_coef(Data, n, characteristics.mad);


        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                PanelClicked?.Invoke(this, e);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Обробник кліку на кнопку вибірки
            // Викликаємо подію SampleClicked, щоб забезпечити можливість підписатися на неї ззовні
            SampleClicked?.Invoke(this, EventArgs.Empty);
        }
    }

}
