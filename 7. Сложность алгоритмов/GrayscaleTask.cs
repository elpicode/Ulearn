namespace Recognizer
{
    public static class GrayscaleTask
    {
        public static double[,] ToGrayscale(Pixel[,] original)
        {
            var origX = original.GetLength(0);
            var origY = original.GetLength(1);
            var scale = new double[ origX , origY];
            for (int i = 0; i < origX ; i++)
            for (int j = 0; j < origY; j++)
            {
                var pixel =  original[i, j]; 
                scale[i, j] = (0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B) / 255;
            }
            return scale;   
        }
    }
}