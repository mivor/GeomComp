using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeomComp
{
    public partial class View : Form
    {
        private Bitmap image;
        string windowName;

        public View()
        {
            InitializeComponent();

            // solarize app
            this.BackColor = Solarized.AppBckg;
            this.ForeColor = Solarized.Text;
            Frame.BackColor = Solarized.ImgBckg;
            btnStart.BackColor = Solarized.Blue;
            btnStart.ForeColor = Solarized.Base2;
            windowName = this.Text;

            image = new Bitmap(Frame.Width, Frame.Height);
            using (Graphics gx = Graphics.FromImage(image))
            {
                // uncomment for higher quality output
                gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // test
                gx.DrawEllipse(new Pen(Solarized.Red), 10, 10, 333, 333);
                gx.DrawRectangle(new Pen(Solarized.Green), 50, 50, 200, 200);
            }
        }

        private void Frame_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, 0, 0);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(bgWorker.IsBusy)
            {
                // cancel pressed
                bgWorker.CancelAsync();
            }
            else
            {
                // start pressed
                btnStart.Text = "Cancel";
                bgWorker.RunWorkerAsync();
            }

        }

        private void draw()
        {
            using (Graphics gx = Graphics.FromImage(image))
            {
                // uncomment for higher quality output
                gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // test
                gx.DrawEllipse(new Pen(Solarized.Blue), 100, 100, 33, 33);
                gx.DrawRectangle(new Pen(Solarized.Cyan), 75, 222, 200, 200);
                Frame.Invalidate();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Program.work(worker, e);

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                flashMessage("Canceled!");
            }
            else if(e.Error != null)
            {
                flashMessage("E: " + e.Error.Message);
            }
            else
            {
                draw();
                flashMessage(e.Result.ToString() + "DONE!");
            }
            btnStart.Text = "Start";
        }

        private async void flashMessage(string msg)
        {
            this.Text = msg;
            await Task.Delay(2000);
            this.Text = windowName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bgWorker.CancelAsync();
        }
    }
}
