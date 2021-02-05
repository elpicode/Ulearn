using System;
using System.Drawing;

namespace Fractals
{
    internal static class DragonFractalTask
    {
        public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
        {
            Random random = new Random(seed);
            double x = 1;
            double y = 0;
            pixels.SetPixel(x,y);
            for (int i = 0; i < iterationsCount; i++)
            {
                var randomNumber = random.Next(2);
                var angleRadian = Math.PI / 2 * randomNumber + Math.PI / 4;
                var nextX = randomNumber + (x * Math.Cos(angleRadian) - y * Math.Sin(angleRadian)) / Math.Sqrt(2.0);
                y = (y * Math.Cos(angleRadian) + x * Math.Sin(angleRadian)) / Math.Sqrt(2.0);
                x = nextX;
                pixels.SetPixel(x, y);
            }
        }
    }
}