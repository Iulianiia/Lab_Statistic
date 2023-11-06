using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Statistic
{
    public class ContextMenu1
    {
        private ContextMenuStrip contextMenu;
        private Button button;
        public event EventHandler MenuItemClicked;

      public static List<double> Standartarray;
      public static List<double> Logarray;
      public static List<double> WithoutAnom;
      public static int ind = 0;
      public static double w;
        public ContextMenu1(Button button)
        {
            this.button = button;
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem menuItem1 = new ToolStripMenuItem("Стандартизувати");
            ToolStripMenuItem menuItem2 = new ToolStripMenuItem("Логарифмізувати");
            ToolStripMenuItem menuItem3 = new ToolStripMenuItem("Прибрати аномалії");

            menuItem1.Click += MenuItem_Click1;
            contextMenu.Items.Add(menuItem1);

            menuItem2.Click += MenuItem_Click2;
            contextMenu.Items.Add(menuItem2);

            menuItem3.Click += MenuItem_Click3;
            contextMenu.Items.Add(menuItem3);


            button.MouseDown += Button_MouseDown;
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenu.Show(button, e.Location);
            }
        }

        private void MenuItem_Click1(object sender, EventArgs e)
        {
                Standartarray = Transformation.standartization(SampleManager.samples[SampleManager.now_index].Data,
                SampleManager.samples[SampleManager.now_index].N, SampleManager.samples[SampleManager.now_index].s_charac.average, SampleManager.samples[SampleManager.now_index].s_charac.rms);
                ind = 1;
                w = -1;
                MenuItemClicked?.Invoke(this, EventArgs.Empty);

        }

        private void MenuItem_Click2(object sender, EventArgs e)
        {
                Logarray = Transformation.logarif(SampleManager.samples[SampleManager.now_index].Data, SampleManager.samples[SampleManager.now_index].N);
                ind = 2;
                w = 0;
                MenuItemClicked?.Invoke(this, EventArgs.Empty);


        }

        private void MenuItem_Click3(object sender, EventArgs e)
        {

                WithoutAnom = Transformation.clearAnomal(SampleManager.samples[SampleManager.now_index].Data,
                      SampleManager.samples[SampleManager.now_index].N, SampleManager.samples[SampleManager.now_index].s_charac.average,
                      SampleManager.samples[SampleManager.now_index].s_charac.rms, SampleManager.samples[SampleManager.now_index].s_charac.ckurt_coef);
                ind = 3;
                w = SampleManager.samples[SampleManager.now_index].W;
                MenuItemClicked?.Invoke(this, EventArgs.Empty);

        }
    }
}
