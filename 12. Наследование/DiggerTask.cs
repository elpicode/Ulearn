using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework.Constraints;

namespace Digger
{
    public class Monster : ICreature
    {
        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            var dX = 0;
            var dY = 0;
            
            if (IsPlayerOnMap(out int  pX, out int pY))
            {
                if (x > pX)
                    dX = -1;
                else if (x < pX)
                    dX = 1;
                else if (y > pY)
                    dY = -1;
                else if (y < pY)
                    dY = 1;
            }
            
            if (!CanMove(x + dX, y + dY))
            {
                dX = 0;
                dY = 0;
            }
            return new CreatureCommand(){DeltaX = dX, DeltaY = dY};
        }

        public bool IsPlayerOnMap(out int pX, out int pY)
        {
            pX = -1;
            pY = -1;
            
            for (int i = 0; i <GameState.newGame.MapWidth; i++)
            {
                for (int j = 0; j < GameState.newGame.MapHeight; j++)
                {
                    if (GameState.newGame.Map[i, j] is Player)
                    {
                        pX = i;
                        pY = j;
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        public bool CanMove(int x, int y)
        {
            return !(x < 0 || y < 0 || GameState.newGame.Map[x, y] is Sack || GameState.newGame.Map[x, y] is Monster 
                     || GameState.newGame.Map[x, y] is Terrain || x > GameState.newGame.MapWidth - 1 || y > GameState.newGame.MapHeight - 1 );
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster ;
        }
    }
    
    public class Sack : ICreature
    {
        private int falls;
        public string GetImageFileName()
        {
            return "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            if (CanFall(x, y ))
            {
                falls++;
                return new CreatureCommand(){DeltaY = 1};
            }
            if (falls > 1)
            {
                falls = 0;
                return new CreatureCommand() {TransformTo = new Gold()};
            }
            falls = 0;
            return new CreatureCommand();
        }

        public bool CanFall(int x, int y)
        {
            if (y + 1 < GameState.newGame.MapHeight)
                return (GameState.newGame.Map[x, y + 1] is Player && falls > 0) || (GameState.newGame.Map[x, y + 1] == null) 
                                                                   ||(GameState.newGame.Map[x, y + 1] is Monster && falls > 0) ;

            return false;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Player)
            {
                GameState.newGame.Scores += 10;
                return true;
            }

            return conflictedObject is Monster;
        }
    }
    
    public class Player : ICreature
    {
        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            var dX = 0;
            var dY = 0;
            if (Game.KeyPressed == Keys.Left)
                dX = -1;

            if (Game.KeyPressed == Keys.Right )
                dX = 1;

            if (Game.KeyPressed == Keys.Down)
                dY = 1;

            if (Game.KeyPressed == Keys.Up )
                dY = -1;

            if (!CanMoveTo(x + dX, y + dY))
            {
                dX = 0;
                dY = 0;
            }
            
            return new CreatureCommand(){DeltaX = dX, DeltaY = dY};
        }
        
        public bool CanMoveTo(int x, int y)
        {
            return !(x < 0 || y < 0 || x > GameState.newGame.MapWidth - 1 || y > GameState.newGame.MapHeight - 1 || GameState.newGame.Map[x,y] is Sack);
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }
    }
    
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.GetImageFileName() == "Digger.png" ;
        }
    }
}