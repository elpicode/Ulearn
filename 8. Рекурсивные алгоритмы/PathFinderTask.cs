using System;
using System.Drawing;
 
namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var bestOrder = MakeTrivialPermutation(checkpoints.Length);
            MakePermutation(new int[checkpoints.Length], 0, 1, bestOrder, checkpoints);
            return bestOrder;
        }
 
        static void MakePermutation(int[] order, double size,  int position, int[] bestOrder, Point[] checkpoints)
        {
            if (position == order.Length)
            {
                order.CopyTo(bestOrder, 0);
                return;
            }
            for (int i = 1; i < order.Length; i++)
            {
                if (Array.IndexOf(order, i, 0, position) != -1)
                    continue;
                order[position] = i;
                var newSize = size + PointExtensions.DistanceTo(checkpoints[order[position - 1]],
                    checkpoints[order[position]]);
                if (newSize < checkpoints.GetPathLength(bestOrder))
                    MakePermutation(order, newSize, position + 1, bestOrder, checkpoints);
            }
        }
 
        public static int[] MakeTrivialPermutation(int size)
        {
            var trivialPermutation = new int[size];
            for (int i = 0; i < size; i++)
                trivialPermutation[i] = i;
            return trivialPermutation;
        }
    }
}