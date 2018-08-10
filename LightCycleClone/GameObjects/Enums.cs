using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone
{
    public enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public enum ControlAction
    {
        MoveUp = 1,
        MoveDown = 2,
        MoveLeft = 3,
        MoveRight = 4
    }

    public enum PlayerState
    {
        Alive,
        Dead
    }

    public enum TileState
    {
        Free = 0,
        Wall = 1,
        Claimed = 2,
    }
}
