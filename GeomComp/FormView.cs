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

        private Color appBckg;
        private Color imgBckg;
        private Color altContent;
        private Color base00;
        private Color content;
        private Color empContent;
        private Color base2;
        private Color base3;
        private Color yellow;
        private Color orange;
        private Color red;
        private Color magenta;
        private Color violet;
        private Color blue;
        private Color cyan;
        private Color green;

        public View()
        {
            InitializeComponent();

            appBckg = Color.FromArgb(0, 43, 54);
            imgBckg = Color.FromArgb(7, 54, 66);
            altContent = Color.FromArgb(88, 110, 117);
            base00 = Color.FromArgb(101, 123, 131);
            content = Color.FromArgb(131, 148, 150);
            empContent = Color.FromArgb(147, 161, 161);
            base2 = Color.FromArgb(238, 232, 213);
            base3 = Color.FromArgb(253, 246, 227);
            yellow = Color.FromArgb(181, 137, 0);
            orange = Color.FromArgb(203, 75, 22);
            red = Color.FromArgb(220, 50, 47);
            magenta = Color.FromArgb(211, 54, 130);
            violet = Color.FromArgb(108, 113, 196);
            blue = Color.FromArgb(38, 139, 210);
            cyan = Color.FromArgb(42, 161, 152);
            green = Color.FromArgb(133, 153, 0);

            image = new Bitmap(Frame.Width, Frame.Height);
            using (Graphics gx = Graphics.FromImage(image))
            {
                // uncomment for higher quality output
                gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                gx.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                gx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gx.Clear(imgBckg);
                gx.DrawEllipse(new Pen(red), 10, 10, 333, 333);
                gx.DrawRectangle(new Pen(green), 50, 50, 200, 200);
            }
        }

        private void Frame_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(image, 0, 0);
        }
    }
}
