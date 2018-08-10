using LightCycleClone.GameObjects.World;
using LightCycleClone.Util;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.GameObjects.Character
{
    public class Player : TileObject
    {
        public static List<PlayerAction> AvailableActions => EnumUtil.GetList<PlayerAction>();

        public PlayerState State { get; set; }
        public Color Colour { get; set; }
        public Direction Direction
        {
            get
            {
                return _direction;
            }
        }
        
        private Direction _direction;
        public TileNode CurrentNode { get; set; }

        public Player(Point startPos, Direction initDir, Color color)
        {
            Pos = startPos;
            _direction = initDir;
            State = PlayerState.Alive;
            Colour = color;
        }

        public Player(Player rhs)
        {
            Guid = rhs.Id;
            Pos = rhs.Pos;
            _direction = rhs._direction;
            State = rhs.State;
            Colour = rhs.Colour;
        }

        public void Update()
        {
            if (State == PlayerState.Alive)
            {
                switch (_direction)
                {
                    case Direction.North:
                        Pos.Y--;
                        break;
                    case Direction.South:
                        Pos.Y++;
                        break;
                    case Direction.East:
                        Pos.X++;
                        break;
                    case Direction.West:
                        Pos.X--;
                        break;
                }
            }
        }

        public void SetAction(PlayerAction action)
        {
            switch (action)
            {
                case PlayerAction.NoAction:
                    break;
                case PlayerAction.MoveUp:
                    SetDirection(Direction.North);
                    break;
                case PlayerAction.MoveDown:
                    SetDirection(Direction.South);
                    break;
                case PlayerAction.MoveLeft:
                    SetDirection(Direction.West);
                    break;
                case PlayerAction.MoveRight:
                    SetDirection(Direction.East);
                    break;
            }
        }

        private void SetDirection(Direction direction)
        {
            var valid = false;

            switch (direction)
            {
                case Direction.North:
                    valid = (_direction != Direction.South);
                    break;
                case Direction.South:
                    valid = (_direction != Direction.North);
                        break;
                case Direction.East:
                    valid = (_direction != Direction.West);
                        break;
                case Direction.West:
                    valid = (_direction != Direction.East);
                        break;
            }

            if (valid)
            {
                _direction = direction;
            }
        }

        public override void Dispose()
        {
            CurrentNode = null;
            base.Dispose();
        }
    }
}