using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var origX = original.GetLength(0);
            var origY = original.GetLength(1);
            var image = new double[origX, origY];
			
            for (int i = 0; i < origX; i++)
            {
                for (int j = 0; j < origY; j++)
                    image[i, j] =  FindMedian(i, j, original, origX, origY);
            }
            return image;
        }
		
        public static double FindMedian (int x, int y, double[,] original, int origX, int origY)
        {
            var currentMedian = new List<double>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var xi = x + i;
                    var yj = y + j;
                    if (xi>= 0 && yj >= 0 && xi < origX && yj < origY)
                        currentMedian.Add(original[xi, yj]);
                }
            }
            currentMedian.Sort();
            if (currentMedian.Count % 2 == 0)
                return (currentMedian[currentMedian.Count / 2] + currentMedian[(currentMedian.Count / 2) - 1]) / 2;
            else
                return currentMedian[currentMedian.Count / 2];
        }
    }
}