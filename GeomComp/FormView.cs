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
            if (bgWorker.IsBusy)
            {
                // cancel pressed
                bgWorker.CancelAsync();
            }
            else
            {
                // start pressed
                btnStart.Text = "Cancel";
                Tuple<int, int, int, int, int> dimensions = new Tuple<int, int, int, int, int>(25, 100, 350, 100, 350);
                bgWorker.RunWorkerAsync(dimensions);
            }

        }

        private void drawPoints(List<Point> pPoints)
        {
            using (Graphics gx = Graphics.FromImage(image))
            {
                // uncomment for higher quality output
                gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                gx.Clear(Solarized.ImgBckg);

                foreach (Point point in pPoints)
                {
                    drawPoint(point, gx);
                }
            }
            Frame.Invalidate();
        }

        private void drawPoint(Point p, Graphics gx)
        {
            Point corner = p;
            corner.Offset(-1, -1);
            gx.DrawRectangle(new Pen(Solarized.Text), corner.X, corner.Y, 3, 3);
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            Program.CreatePoints(worker, e);

        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                flashMessage("Canceled!");
            }
            else if (e.Error != null)
            {
                flashMessage("E: " + e.Error.Message);
            }
            else
            {
                drawPoints(((Result)e.Result).Points);
                flashMessage("DONE!");
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
