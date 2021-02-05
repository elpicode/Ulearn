using System;

namespace GeometryTasks
{
    public class Vector
    {
        public double X;
        public double Y;

        public double GetLength()
        {
            return Geometry.GetLength(new Vector() {X = X, Y = Y});
        }

        public Vector Add(Vector vector)
        {
            return Geometry.Add(vector, new Vector() {X = X, Y = Y});
        }

        public bool Belongs(Segment segment)
        {
            return Geometry.IsVectorInSegment(new Vector() {X = X, Y = Y}, segment);
        }
    }

    public static class Geometry
    {
        public static double GetLength(Vector vector)
        {
            return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static double GetLength(Segment segment)
        {
            var dX = segment.End.X - segment.Begin.X;
            var dY = segment.End.Y - segment.Begin.Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }

        public static bool IsVectorInSegment(Vector vector, Segment segment)
        {
            var firstSegment = GetLength(new Segment(){Begin = segment.Begin, End = vector});
            var secondSegment = GetLength(new Segment(){Begin = vector, End = segment.End});
            return firstSegment + secondSegment == GetLength(segment);
        }

        public static Vector Add(Vector vOne, Vector vTwo)
        {
            return new Vector(){X = vOne.X + vTwo.X, Y = vOne.Y + vTwo.Y};
        }
    }

    public class Segment
    {
        public Vector Begin;
        public Vector End;

        public double GetLength()
        {
            return Geometry.GetLength(new Segment(){Begin = Begin, End = End});
        }

        public bool Contains(Vector vector)
        {
            return Geometry.IsVectorInSegment(vector, new Segment() {Begin = Begin, End = End});
        }
    }
}