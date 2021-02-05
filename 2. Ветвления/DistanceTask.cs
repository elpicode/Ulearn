using System;

namespace DistanceTask
{
    public class DistanceTask
    {
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            var delta = Math.Abs((bx - ax) * (y - ay) - (by - ay) * (x - ax));
            var lengthAB = Math.Sqrt((bx - ax) * (bx - ax) + (by - ay) * (by - ay));
            var fromDotToA = Math.Sqrt((y - ay) * (y - ay) + (x - ax) * (x - ax));
            var fromDotToB = Math.Sqrt((x - bx) * (x - bx) + (y - by) * (y - by));
            if (ax == bx && ay == by)
                return fromDotToA;
            if (0 > (x - bx) * (ax - bx) + (y - by) * (ay - by) || 0 > (x - ax) * (bx - ax) + (by - ay) * (y - ay))
                return Math.Min(fromDotToA, fromDotToB);
            return delta / lengthAB;
        }
    }
}