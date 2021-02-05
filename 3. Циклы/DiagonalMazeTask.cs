using System;

namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            var countStepsToLarge = (Math.Max(width, height) - 3) / (Math.Min(width, height) - 2);

            for (int i = 1; i < Math.Max(width, height); i++)
            {
                if (!robot.Finished)
                    MoveToMax(robot, countStepsToLarge, WhoIsMax(width, height));
                if (!robot.Finished)
                    robot.MoveTo(WhoIsMin(width, height));
            }
        }

        public static void MoveToMax(Robot robot, int countSteps, Direction dir)
        {
            for (int i = 0; i < countSteps; i++)
                robot.MoveTo(dir);
        }
		
        public static Direction WhoIsMax (int width, int height )
        {
            if (width > height)
                return Direction.Right;
            return Direction.Down;
        }

        public static Direction WhoIsMin (int width, int height )
        {
            if (width > height)
                return Direction.Down;
            return Direction.Right;
        }
    }
}