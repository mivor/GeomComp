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
        public List<Point> Points {get; private set;}

        public PointCloud(IEnumerable<Point> points)
        {
            // TODO: Complete member initialization
            this.Points = new List<Point>(points);
        }

        public PointCloud(IEnumerable<int> valuesX, IEnumerable<int> valuesY)
        {
            Points = new List<Point>(valuesX.Zip<int, int, Point>(valuesY, (x, y) => new Point(x, y)));
        }

        public List<Point> GetSimpleMinAreaRect()
        {
            throw new NotImplementedException();
        }
    }
}
