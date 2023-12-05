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
        public List<double> rangs;

        public List<double> Data { get;  set; }
        public double W { get; private set; }
        public string Name { get; private set; }
        public int N { get; private set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public double h { get; set; }
        public int m { get; set; }
        public int Ind { get; set; }
       

        public Sample(List<double> data, double w, int ind,string name)
        {
            Characteristics characteristics = new Characteristics();
            s_charac = characteristics;


            Data = data;
            W = w;
            Name = name;
            N = data.Count;
            Max = data.Max();
            Min = data.Min();
            Ind = ind;


  
            // Ініціалізація контролів (кнопки, панелі і т. д.) і додавання обробника кліку
            button = new Button();
            table = new TableLayoutPanel();
            panel = new Panel();

            button.Text = Ind.ToString(); // Приклад встановлення тексту кнопки на основі імені вибірки
            // Додавання обробника кліку для кнопки
            button.Click += Button_Click;
            button.MouseDown += Button_MouseDown;
            //  panel.Click += Panel_MouseClick;

            table.Controls.Add(button);
            table.AutoSize = true;
            table.AutoScroll = true;
            TextBox textBox1 = new TextBox();
            textBox1.Text = "max = " +  Math.Round(data.Max(),1).ToString() + ", min = " + Math.Round(data.Min(),1).ToString() + ", N = " + N.ToString();
            textBox1.Size = new Size(150, 25);
            textBox1.Dock = DockStyle.Fill;
            table.Controls.Add(textBox1);
            TextBox textBox2 = new TextBox();
            textBox2.Text = "name = " + name;
            textBox2.Size = new Size(85, 25);
            textBox2.Dock = DockStyle.Fill;
            table.Controls.Add(textBox2);
            table.RowCount = 1;
            table.ColumnCount = N+ 3;
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
            m_h();

        }
        private void m_h()
        {
            
            (m, h) = M_H.m_h(this);
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
