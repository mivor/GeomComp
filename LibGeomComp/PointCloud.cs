using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LibGeomComp
{
    public class PointCloud
    {
        public List<Point> Points {get; private set; }
        public List<Point> MinAreaRect { get; private set; }

        public PointCloud(IEnumerable<Point> points)
        {
            // TODO: Complete member initialization
            this.Points = new List<Point>(points);
        }

        public PointCloud(IEnumerable<int> valuesX, IEnumerable<int> valuesY)
        {
            Points = new List<Point>(valuesX.Zip<int, int, Point>(valuesY, (x, y) => new Point(x, y)));
        }

        public IEnumerable<Point> GetSimpleMinAreaRect()
        {
            Point min, max, maxXY, maxYX;
            min = new Point(int.MaxValue, int.MaxValue);
            max = new Point(-1, -1);

            foreach (Point p in Points)
            {
                if (p.X < min.X) min.X = p.X;
                if (p.Y < min.Y) min.Y = p.Y;
                if (p.X > max.X) max.X = p.X;
                if (p.Y > max.Y) max.Y = p.Y;
            }
            maxXY = new Point(max.X, min.Y);
            maxYX = new Point(min.X, max.Y);
            // xx, xy, yy, yx
            MinAreaRect = new List<Point> {min, maxXY, max, maxYX};
            return MinAreaRect;
        }
    }
}
