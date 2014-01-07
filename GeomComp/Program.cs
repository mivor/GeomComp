using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace GeomComp
{
    class Result
    {
        public List<Point> Points { get; private set; }
        public string Type { get; private set; }

        public Result(List<Point> pPoints, string pType)
        {
            Points = new List<Point>(pPoints);
            Type = pType;
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new View());
        }
        
        public static void CreatePoints(BackgroundWorker worker, DoWorkEventArgs e)
        {
            Random rnd = new Random();
            List<Point> pointCloud = new List<Point>();
            int n = (int)e.Argument;

            for (int i = 0; i < n; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                int x = rnd.Next(5,711);
                int y = rnd.Next(5,471);
                Point p = new Point(x, y);
                pointCloud.Add(p);
            }
            e.Result = new Result(pointCloud, "point");
        }

        public static void work(BackgroundWorker worker, DoWorkEventArgs e)
        {
            List<int> result = new List<int>();
            int progress = 0;
            int currProgress = 0;

            UInt64 state = 1;
            for (ulong i = 0; i < uint.MaxValue; i += 101)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        state += (ulong)i * 22;
                    }
                    else
                    {
                        state += (ulong)i * 3;
                    }
                    progress = (int)((float)i / (double)(int.MaxValue) * 100);
                    if (progress > currProgress)
                    {
                        currProgress = progress;
                        worker.ReportProgress(currProgress);
                    }
                }
            }
            result.Add(2);
            e.Result = state;
        }
    }
}
