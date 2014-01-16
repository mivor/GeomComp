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
        public List<Tuple<Point, Point>> ConvexHull {get; private set; }

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

        public IEnumerable<Point> GetMinAreaRect()
        {
            getStubConvexHull();
            List<Point> dirEdge = new List<Point>();
            List<float> lengthEdge = new List<float>();
            List<PointF> unitDirEdge = new List<PointF>();
            List<PointF> normDirEdge = new List<PointF>();

            List<PointF> edgeExtremVect = new List<PointF>();
            List<PointF> normExtremVect = new List<PointF>();


            // analyze convex hull edges
            foreach (var edge in ConvexHull)
	        {
		        dirEdge.Add(new Point(edge.Item2.X - edge.Item1.X, edge.Item2.Y - edge.Item1.Y));
                lengthEdge.Add((float)Math.Sqrt(Math.Pow(dirEdge.Last().X, 2) + Math.Pow(dirEdge.Last().Y, 2)));
                unitDirEdge.Add(new PointF(dirEdge.Last().X / lengthEdge.Last(), dirEdge.Last().Y / lengthEdge.Last()));
                normDirEdge.Add(new PointF(0 - unitDirEdge.Last().Y, unitDirEdge.Last().X));
	        }

            PointF minArea = new PointF(-1, float.MaxValue);
                //List<double> areas = new List<double>();
            // find MAR
            for (int k = 0; k < unitDirEdge.Count; k++)
            {
                PointF edgeExtrem = new PointF();
                PointF normExtrem = new PointF();

                for (int i = 0; i < ConvexHull.Count; i++)
                {
                    Point vertex = ConvexHull[i].Item1;
                    float edgeProduct = unitDirEdge[k].X * vertex.X + unitDirEdge[k].Y * vertex.Y;
                    float normProduct = normDirEdge[k].X * vertex.X + normDirEdge[k].Y * vertex.Y;
                    if (i == 0)
                    {
                        edgeExtrem.X = edgeProduct;
                        edgeExtrem.Y = edgeProduct;
                        normExtrem.X = normProduct;
                        normExtrem.Y = normProduct;
                    }
                    else { 
                        if (edgeProduct < edgeExtrem.X) edgeExtrem.X = edgeProduct;
                        if (edgeProduct > edgeExtrem.Y) edgeExtrem.Y = edgeProduct;
                        if (normProduct < normExtrem.X) normExtrem.X = normProduct;
                        if (normProduct > normExtrem.Y) normExtrem.Y = normProduct;
                    }
                }
                //foreach (var edge in ConvexHull)
                //{
                //    Point vertex = edge.Item1;
                //    float productEdge = unitDirEdge[i].X * vertex.X + unitDirEdge[i].Y * vertex.Y;
                //    float productNorm = normDirEdge[i].X * vertex.X + normDirEdge[i].Y * vertex.Y;
                //    if (productEdge < edgeExtrem.X) edgeExtrem.X = productEdge;
                //    if (productEdge > edgeExtrem.Y) edgeExtrem.Y = productEdge;
                //    if (productNorm < normExtrem.X) normExtrem.X = productNorm;
                //    if (productNorm > normExtrem.Y) normExtrem.Y = productNorm;
                //}
                edgeExtremVect.Add(edgeExtrem);
                normExtremVect.Add(normExtrem);
                // norm1 - 2 edge1 -2
                float area = (normExtrem.X - normExtrem.Y) * (edgeExtrem.X - edgeExtrem.Y);
                if (area < minArea.Y)
                {
                    minArea.Y = area;
                    minArea.X = k;
                }
            }

            int q = (int)minArea.X;
            Point xx = new Point((int)(edgeExtremVect[q].X * unitDirEdge[q].X + normExtremVect[q].X * normDirEdge[q].X),
                (int)(edgeExtremVect[q].X * unitDirEdge[q].Y + normExtremVect[q].X * normDirEdge[q].Y));

            Point yx = new Point((int)(edgeExtremVect[q].Y * unitDirEdge[q].X + normExtremVect[q].X * normDirEdge[q].X),
                (int)(edgeExtremVect[q].Y * unitDirEdge[q].Y + normExtremVect[q].X * normDirEdge[q].Y));

            Point yy = new Point((int)(edgeExtremVect[q].Y * unitDirEdge[q].X + normExtremVect[q].Y * normDirEdge[q].X),
                (int)(edgeExtremVect[q].Y * unitDirEdge[q].Y + normExtremVect[q].Y * normDirEdge[q].Y));

            Point xy = new Point((int)(edgeExtremVect[q].X * unitDirEdge[q].X + normExtremVect[q].Y * normDirEdge[q].X),
                (int)(edgeExtremVect[q].X * unitDirEdge[q].Y + normExtremVect[q].Y * normDirEdge[q].Y));

            MinAreaRect = new List<Point> { xx, yx, yy, xy };

            return MinAreaRect;
        }

        private void getStubConvexHull()
        {
            Tuple<Point, Point> edge;
            ConvexHull = new List<Tuple<Point, Point>>();
            for (int i = 0; i < Points.Count-1; i++)
            {
                ConvexHull.Add(edge = new Tuple<Point, Point>(Points[i], Points[i+1]));
            }
            ConvexHull.Add(edge = new Tuple<Point, Point>(Points[Points.Count-1], Points[0]));
        }
    }
}