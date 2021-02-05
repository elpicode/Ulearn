using System;

namespace Rectangles
{
    public static class RectanglesTask
    {
        // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
        public static bool AreIntersected(Rectangle r1, Rectangle r2)
        {
            var isWidth = Math.Min(r1.Right, r2.Right) >= Math.Max(r1.Left, r2.Left);
            var isHeight = Math.Min(r1.Bottom, r2.Bottom) >= Math.Max(r1.Top, r2.Top);
            return isWidth && isHeight ;
        }

        public static int IntersectionSquare(Rectangle r1, Rectangle r2)
        {
            var leftRight = Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left);
            var bottomTop = Math.Min(r1.Bottom, r2.Bottom) - Math.Max(r1.Top, r2.Top);
            return AreIntersected(r1, r2) ? leftRight * bottomTop: 0;
        }


        public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
        {
            if (FirstInSecond(r1,r2))
                return 0;
            if ( FirstInSecond(r2,r1))
                return 1;
            return -1;
        }
		
        public static bool FirstInSecond(Rectangle r1, Rectangle r2)
        {
            return  r1.Right <= r2.Right && r1.Left >= r2.Left && r1.Top >= r2.Top && r1.Bottom <= r2.Bottom;
        }
    }
}