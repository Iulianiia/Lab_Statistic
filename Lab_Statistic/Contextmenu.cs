using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Statistic
{
    public class ContextMenu : Form1
    {
        private ContextMenuStrip contextMenu;
        private Button button;

        public ContextMenu(Button button)
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
           List<double> Standartarray = Transformation.standartization(SampleManager.samples[SampleManager.now_index].Data,
                 SampleManager.samples[SampleManager.now_index].N, SampleManager.samples[SampleManager.now_index].s_charac.average, SampleManager.samples[SampleManager.now_index].s_charac.rms);
           
            
            int index = SampleManager.i - 1;
            SampleManager.AddSample(Standartarray, SampleManager.samples[SampleManager.now_index].W);
            panel1.Controls.Add(SampleManager.samples[index].panel);
            SampleManager.samples[index].SampleClicked += Sample_Clicked;
            SampleManager.samples[index].PanelClicked += Panel_Clicked; ;


        }

        private void MenuItem_Click2(object sender, EventArgs e)
        {
            List<double> Logarray = Transformation.logarif(SampleManager.samples[SampleManager.now_index].Data, SampleManager.samples[SampleManager.now_index].N);


            int index = SampleManager.i - 1;
            SampleManager.AddSample(Logarray, SampleManager.samples[SampleManager.now_index].W);
            panel1.Controls.Add(SampleManager.samples[index].panel);
            SampleManager.samples[index].SampleClicked += Sample_Clicked;
            SampleManager.samples[index].PanelClicked += Panel_Clicked; 
        }

        private void MenuItem_Click3(object sender, EventArgs e)
        {
            List<double> WithoutAnom = Transformation.clearAnomal(SampleManager.samples[SampleManager.now_index].Data,
                  SampleManager.samples[SampleManager.now_index].N, SampleManager.samples[SampleManager.now_index].s_charac.average, 
                  SampleManager.samples[SampleManager.now_index].s_charac.rms, SampleManager.samples[SampleManager.now_index].s_charac.ckurt_coef);


            int index = SampleManager.i - 1;
            SampleManager.AddSample(WithoutAnom , SampleManager.samples[SampleManager.now_index].W);
            panel1.Controls.Add(SampleManager.samples[index].panel);
            SampleManager.samples[index].SampleClicked += Sample_Clicked;
            SampleManager.samples[index].PanelClicked += Panel_Clicked;
        }
    }
}
