using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowPos = new PointF(FindCoordinate(0, Manipulator.UpperArm ,  shoulder, "x") , 
                FindCoordinate( 0, Manipulator.UpperArm, shoulder, "y"));
            var wristXAngle = elbow + shoulder - Math.PI;
            var wristPos = new PointF(FindCoordinate(elbowPos.X, Manipulator.Forearm, wristXAngle,"x"),
                FindCoordinate(elbowPos.Y, Manipulator.Forearm, wristXAngle,"y"));
            var palmAngle = wrist + wristXAngle - Math.PI;
            var palmEndPos = new PointF(FindCoordinate(wristPos.X, Manipulator.Palm, palmAngle, "x") ,
                FindCoordinate(wristPos.Y, Manipulator.Palm, palmAngle, "y"));
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        public static float FindCoordinate(float start, float length, double angle, string coord)
        {
            float len;
            if (coord == "x")
                len = length * (float)Math.Cos(angle);
            else
                len = length * (float)Math.Sin(angle);
            return start + len;
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI/ 2, Math.PI / 2, Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]
        [TestCase(Math.PI / 2, 3 * Math.PI / 2, 3 * Math.PI / 2, - Manipulator.Forearm, Manipulator.UpperArm - Manipulator.Palm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(FindDistance(joints[0], new PointF(0, 0)), Manipulator.UpperArm);
            Assert.AreEqual(FindDistance(joints[0], joints[1]), Manipulator.Forearm);
            Assert.AreEqual(FindDistance(joints[1], joints[2]), Manipulator.Palm);
        }
        public double FindDistance(PointF firstPoint, PointF secondPoint)
        {
            var differX = (firstPoint.X - secondPoint.X) * (firstPoint.X - secondPoint.X);
            var differY = (firstPoint.Y - secondPoint.Y) * (firstPoint.Y - secondPoint.Y);
            return Math.Sqrt(differX + differY);
        }
    }
}