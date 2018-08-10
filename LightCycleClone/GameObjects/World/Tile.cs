using LightCycleClone.GameObjects.Character;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.GameObjects.World
{
    public class TileNode : TileObject
    {
        public TileState State { get; set; }
        public Player Owner { get; set; }

        private TileNode[] _nodes;

        public TileNode North
        {
            get
            {
                return _nodes[(int)Direction.North];
            }
            set
            {
                _nodes[(int)Direction.North] = value;
            }
        }
        public TileNode South
        {
            get
            {
                return _nodes[(int)Direction.South];
            }
            set
            {
                _nodes[(int)Direction.South] = value;
            }
        }
        public TileNode East
        {
            get
            {
                return _nodes[(int)Direction.East];
            }
            set
            {
                _nodes[(int)Direction.East] = value;
            }
        }
        public TileNode West
        {
            get
            {
                return _nodes[(int)Direction.West];
            }
            set
            {
                _nodes[(int)Direction.West] = value;
            }
        }
        
        public TileNode(int x, int y)
            : base()
        {
            State = TileState.Free;
            Pos.X = x;
            Pos.Y = y;
            _nodes = new TileNode[4];
        }

        public TileNode(TileNode rhs)
        {
            State = rhs.State;
            Pos.X = rhs.Pos.X;
            Pos.Y = rhs.Pos.Y;
            _nodes = new TileNode[4];
        }


        public int Utility(Func<TileNode, int> func, Direction dir)
        {
            var visted = new Dictionary<Guid, TileNode>();
            return Utility(func, visted, dir);
        }

        public int Utility(Func<TileNode, int> func, Dictionary<Guid, TileNode> visted, Direction dir)
        {
            var result = func.Invoke(this);
            visted[Id] = this;

            if (result > 0 && visted.Count < 50)
            {
                foreach (var node in _nodes)
                {
                    if (node == null)
                    {
                        continue;
                    }

                    var correctDir = (dir == Direction.East && node.Pos.X > Pos.X)
                        || (dir == Direction.West && node.Pos.X < Pos.X)
                        || (dir == Direction.South && node.Pos.Y > Pos.Y)
                        || (dir == Direction.North && node.Pos.Y < Pos.Y);
                    
                    var isEmpty = node.State == TileState.Free;
                    var notTraversed = !visted.ContainsKey(node.Id);

                    if (correctDir && isEmpty && notTraversed)
                    {
                        result += node.Utility(func, visted, dir);
                    }
                }
            }

            return result;
        }

        public void SetOccupiedBy(Player obj)
        {
            State = TileState.Claimed;
            Owner = obj;
            obj.CurrentNode = this;
        }

        public void ClearOccupiedBy()
        {
            State = TileState.Claimed;
        }
    }
}
