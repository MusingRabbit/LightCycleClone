using LightCycleClone.GameObjects.Character;
using LightCycleClone.GameObjects.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.AI
{
    public class ReflexAgent
    {
        private static int _agentCount = 0;

        private int _id;
        private Player _player;
        private PlayerAction _action;

        public ReflexAgent(Player player)
        {
            _player = player;
            _agentCount++;
            _id = _agentCount;
        }

        public void Update(GameWorld gameState)
        {
            var currNode = _player.CurrentNode;

            int[] values = new int[4];

            values[(int)Direction.North] = currNode.North?.Utility(x => CalcNodeValue(x, gameState), Direction.North) ?? 0;
            values[(int)Direction.South] = currNode.South?.Utility(x => CalcNodeValue(x, gameState), Direction.South) ?? 0;
            values[(int)Direction.East] = currNode.East?.Utility(x => CalcNodeValue(x, gameState), Direction.East) ?? 0;
            values[(int)Direction.West] = currNode.West?.Utility(x => CalcNodeValue(x, gameState), Direction.West) ?? 0;

            int ans = (int)_player.Direction;

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] > values[ans])
                {
                    ans = i;
                }
            }

            _action = GetPlayerAction((Direction)ans);
            _player.SetAction(_action);
        }

        private PlayerAction GetPlayerAction(Direction dir)
        {
            switch (dir)
            {
                case Direction.North:
                    return PlayerAction.MoveUp;
                case Direction.East:
                    return PlayerAction.MoveRight;
                case Direction.South:
                    return PlayerAction.MoveDown;
                case Direction.West:
                    return PlayerAction.MoveLeft;
            }

            return PlayerAction.NoAction;
        }

        private int GetBaseScore(TileNode node)
        {
            return node?.State == TileState.Free ? 1 : -5;
        }
        
        private Player GetClosestPlayer(Point pos, GameWorld gameState)
        {
            Player result = null;
            Point smallest = gameState.TileMap.Dimensions;
            var players = gameState.GetLivePlayers();

            foreach (var opp in players)
            {
                if (opp.Id == _player.Id)
                {
                    continue;
                }

                if (smallest.X > opp.Position.X && smallest.Y > opp.Position.Y)
                {
                    smallest = opp.Position;
                    result = opp;
                }
            }

            return result;
        }

        private int CalcNodeValue(TileNode node, GameWorld gameState)
        {
            var players = gameState.GetLivePlayers();
            var currPos = _player.Position;

            var result = GetBaseScore(node);

            if (result > 0)
            {
                var tgtOpponent = GetClosestPlayer(_player.Position, gameState);

                if (tgtOpponent != null)
                {
                    var pDetla = _player.Position - tgtOpponent.Position;

                    if (pDetla.X < 10 && pDetla.Y < 10)
                    {
                        var oDelta = node.Position - tgtOpponent.Position;

                        var dirX = oDelta.X > 0 ? Direction.East : Direction.West;
                        var dirY = oDelta.Y > 0 ? Direction.South : Direction.North;
                        var oppDir = tgtOpponent.Direction;

                        var mapSize = gameState.TileMap.Dimensions;

                        var nodeOffset = new Point
                        {
                            X = dirX == Direction.East ? mapSize.X - node.Position.X : node.Position.X,
                            Y = dirY == Direction.South ? mapSize.Y - node.Position.Y : node.Position.Y
                        };

                        var opponentOffset = new Point
                        {
                            X = dirX == Direction.East ? mapSize.X - tgtOpponent.Position.X : tgtOpponent.Position.X,
                            Y = dirY == Direction.South ? mapSize.Y - tgtOpponent.Position.Y : tgtOpponent.Position.Y
                        };

                        //result += (nodeOffset.X < opponentOffset.X) ? 1 : -1;
                        //result += (nodeOffset.Y < opponentOffset.Y) ? 1 : -1;

                        result += (oppDir == Direction.East && oDelta.X > 0) ? 2 : -1;
                        result += (oppDir == Direction.West && oDelta.X < 0) ? 2 : -1;
                        result += (oppDir == Direction.North && oDelta.Y < 0) ? 2 : -1;
                        result += (oppDir == Direction.South && oDelta.Y > 0) ? 2 : -1;
                    }
                }
            }

            return result;
        }

        public PlayerAction GetAction()
        {
            return _action;
        }
    }
}