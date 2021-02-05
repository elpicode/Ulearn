using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
    public static class GeometryExtensions
    {
        public static Dictionary<Segment, Color> Colors = new Dictionary<Segment, Color>();
        
        
        public static Color GetColor(this Segment segment)
        {
            if (!Colors.ContainsKey(segment))
                Colors[segment] = System.Drawing.Color.Black;
            return Colors[segment];
        }

        public static void SetColor(this Segment segment, Color newColor)
        {
            Colors[segment] = newColor;
        }
    }
}
