namespace Mazes
{
    public static class SnakeMazeTask
    {
        public static void MoveOut(Robot robot, int width, int height)
        {
            int line = 0;
            while (!robot.Finished)
            {
                Move(robot, Direction.Right, width - 3);
                Move(robot, Direction.Down, 2);
                Move(robot, Direction.Left, width - 3);
                if (line != (height / 4) - 1)
                    Move(robot, Direction.Down, 2);
                line ++; 
            }
        }

        public static void Move (Robot robot, Direction dir, int steps)
        {
            for (int i = 0; i < steps; i++)
                robot.MoveTo(dir);
        }
    }
}