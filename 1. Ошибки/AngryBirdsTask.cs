using System;

namespace AngryBirds
{
    public static class AngryBirdsTask
    {
       
        public static double FindSightAngle(double v, double distance)
        {
            var g = 9.8;
            var sin2a = (distance * g) / (v * v);
            return Math.Asin(sin2a) / 2;
        }
    }
}