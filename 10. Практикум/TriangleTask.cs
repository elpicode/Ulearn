using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            if (a < 0 || b < 0 || c < 0)
                return double.NaN;
            if (a + b < c || a + c < b || b + c < a)
                return double.NaN;
            return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(8, 15,17,Math.PI / 2)]
        [TestCase(-1,5, 7, double.NaN)]
        [TestCase(3, 7, 1, double.NaN)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var angle = TriangleTask.GetABAngle(a, b, c);
            if (expectedAngle == double.NaN || angle == double.NaN )
                Assert.AreEqual(angle, expectedAngle);
            else Assert.AreEqual(angle, expectedAngle, 1e-5);
        }
    }
}