using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LibGeomComp;
using System.Drawing;

namespace TestGeomComp
{
    [TestFixture]
    public class TestPointCloud
    {
        PointCloud cloud;
        IEnumerable<Point> result;
        List<Point> expected;

        [SetUp]
        public void SetUp()
        {
            expected = new List<Point>();
        }

        [Test]
        public void ConstructorArraysToList()
        {
            int[] valX = new int[] { 20, 35, 50 };
            int[] valY = new int[] { 20, 50, 15 };

            expected.Add(new Point(20, 20));
            expected.Add(new Point(35, 50));
            expected.Add(new Point(50, 15));

            cloud = new PointCloud(valX, valY);

            result = cloud.Points;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void SimpleMinAreaRect()
        {
            int[] valX = new int[] { 20, 35, 50 };
            int[] valY = new int[] { 20, 50, 15 };

            expected.Add(new Point(20, 15));
            expected.Add(new Point(50, 15));
            expected.Add(new Point(50, 50));
            expected.Add(new Point(20, 50));

            cloud = new PointCloud(valX, valY);

            result = cloud.GetSimpleMinAreaRect();

            Assert.That(result, Is.EquivalentTo(expected));
            Assert.That(cloud.MinAreaRect, Is.EquivalentTo(expected));
        }

        [Test]
        public void MinAreaRect()
        {
            int[] valX = new int[] { 20, 50, 35 };
            int[] valY = new int[] { 20, 15, 50 };

            expected.Add(new Point(20, 19));
            expected.Add(new Point(50, 14));
            expected.Add(new Point(55, 46));
            expected.Add(new Point(25, 51));

            cloud = new PointCloud(valX, valY);

            result = cloud.GetMinAreaRect();

            Assert.That(result, Is.EquivalentTo(expected));
            Assert.That(cloud.MinAreaRect, Is.EquivalentTo(expected));
        }
    }
}
