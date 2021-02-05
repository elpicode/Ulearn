using System.Collections;
using System;
using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var origX = original.GetLength(0);
            var origY = original.GetLength(1);
            var pixels = FillPixels (original, origX, origY);
            var t = FindT(original, whitePixelsFraction, pixels);
            for (int i = 0; i < origX; i++)
            {
                for (int j = 0; j < origY; j++)
                    original[i, j] = IsOvercoming(original[i, j], t);
            }
            return original;
        }
		
        public static List<double> FillPixels (double[,] original, int origX, int origY)
        {
            var pixels = new List<double>();
            for (int i = 0; i < origX; i++)
            {
                for (int j = 0; j < origY; j++)
                    pixels.Add(original[i, j]);
            }
            pixels.Sort();
            return pixels;
        }

        public static double FindT(double[,] original, double whitePixelsFraction, List<double> pixels )
        {
            var whitePixelsCount = (int)(original.Length * whitePixelsFraction);
            return (whitePixelsCount == 0) ? int.MaxValue : pixels[(int)(pixels.Count - whitePixelsCount)];
        }
		
        public static double IsOvercoming(double pixel, double t)
        {
            return (pixel >= t) ? 1.0 : 0.0;
        }
    }
}