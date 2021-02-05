using System.Diagnostics;
namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            MoveToWall(robot, Direction.Right, width - 2);
            MoveToWall(robot, Direction.Down, height - 2);
        }
		
        static void MoveToWall(Robot robot, Direction dir, int stepCount)
        {
            for(int i = 1; i < stepCount; i++)
                robot.MoveTo(dir);
        }
    }
}