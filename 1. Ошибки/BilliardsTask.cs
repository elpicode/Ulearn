using System;

namespace Billiards
{
    public class BilliardsTask
    {
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="directionRadians">Угол направелния движения шара</param>
            /// <param name="wallInclinationRadians">Угол</param>
            /// <returns></returns>
            return wallInclinationRadians - directionRadians + wallInclinationRadians;
        }
    }
}