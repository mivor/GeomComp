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

        public View()
        {
            InitializeComponent();

            // solarize app
            this.BackColor = Solarized.AppBckg;
            this.ForeColor = Solarized.Text;
            Frame.BackColor = Solarized.ImgBckg;

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
    }
}
