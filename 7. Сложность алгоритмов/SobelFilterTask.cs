using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sy = TransposeOfAMatrix(sx);
            var edge = sx.GetLength(0) / 2;
            for (int x = edge; x < width - edge; x++)
            {
                for (int y = edge; y < height - edge; y++)
                    result[x, y] = FindConvolution(sx, sy, g, x, y);
            }
            return result;
        }

        public static double[,] TransposeOfAMatrix(double[,] sx)
        {
            var x = sx.GetLength(0);
            var y = sx.GetLength(1);
            var matrix = new double[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                    matrix[i, j] = sx[j, i];
            }
            return matrix;
        }
        
        public static double Multiply(double[,] g, double[,] s, int x, int y)
        {
            double res = 0;
            var edge = s.GetLength(0) / 2;
            var len = s.GetLength(0);
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                    res += s[i, j] * g[x + i - edge, y + j - edge];
            }
            return res;
        }

        public static double FindConvolution(double [,] sx, double [,] sy, double [,] g, int x, int y)
        {
            var gx = Multiply(g, sx, x, y);
            var gy = Multiply(g, sy, x, y);
            return Math.Sqrt(gx * gx + gy * gy);
        }
    }
}