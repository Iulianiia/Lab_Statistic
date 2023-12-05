using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Statistic
{
    public class XY
    {
        public double x { get; set; }
        public double y { get; set; }
        public void Add_Rangs_x_y(double X, double Y)
        {
            x = X;
            y = Y;
        }
       
    }
    public  class Two_dimensional_sample
    {
        public event EventHandler Sample2D_Clicked;
        public Button button { get; private set; }

       public Sample X { get; set; }
       public Sample Y { get; set; }
       public List<XY> xy { get; set; }
       public int N { get; set; }

        public string Ind { get; set; }

        public Two_dimensional_sample(Sample x, Sample y, string ind)
        {
            X = x;
            Y = y;
            Ind = ind;
            button = new Button();
            int N1 = x.N;
            int N2 = y.N;
            if (N1 < N2)
                N = N1;
            else
                N = N2;

            button.Text = $"{Ind} {{{x.Ind}; {y.Ind}}}";
            button.Dock = DockStyle.Top;
            button.Click += Button_Click;
            xy = new List<XY>();
            for(int i =0; i< N; i++)
            {
                XY el = new XY();
                el.Add_Rangs_x_y(X.Data[i], Y.Data[i]);
                xy.Add(el);
            }
         //   Form1.panel1.Controls.Add(button);
        }
        private void Button_Click(object sender, EventArgs e)
        {
            // Обробник кліку на кнопку вибірки
            // Викликаємо подію SampleClicked, щоб забезпечити можливість підписатися на неї ззовні
            Sample2D_Clicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
