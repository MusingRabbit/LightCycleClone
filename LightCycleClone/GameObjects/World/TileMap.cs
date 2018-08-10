using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.GameObjects.World
{
    public class TileMap : IDisposable
    {
        private Point _dimensions;
        private TileNode[,] _tileMap;

        public Point Dimensions
        {
            get
            {
                return _dimensions;
            }
        }

        public TileMap()
        {
            _dimensions = Point.Zero;
        }

        public TileNode GetTile(Point point)
        {
            return _tileMap[point.X, point.Y];
        }

        public IEnumerable<TileNode> GetAllTiles()
        {
            for (int x = 0; x < _tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < _tileMap.GetLength(1); y++)
                {
                    yield return _tileMap[x, y];
                }
            }
        }
        
        public void CreateLevel(int size)
        {
            CreateLevel(new Point(size, size));
        }

        public void CreateLevel(Point dimensions)
        {
            _dimensions = dimensions;

            _tileMap = new TileNode[dimensions.X, dimensions.Y];

            for (int x = 0; x < dimensions.X; x++)
            {
                for (int y = 0; y < dimensions.Y; y++)
                {
                    var mapEdge = x < 1 || y < 1 || y >= dimensions.Y - 1 || x >= dimensions.X - 1;

                    _tileMap[x, y] = new TileNode(x, y)
                    {
                        State = mapEdge ? TileState.Wall : TileState.Free,
                    };
                }
            }

            MapNodes(_tileMap);
        }

        private void MapNodes(TileNode[,] tileNodes)
        {
            for (int x = 0; x < tileNodes.GetLength(0); x++)
            {
                for (int y = 0; y < tileNodes.GetLength(1); y++)
                {
                    var currNode = tileNodes[x, y];
                    currNode.North = y < 1 ? null : _tileMap[x, y - 1];
                    currNode.South = y >= _dimensions.Y - 1 ? null : _tileMap[x, y + 1];
                    currNode.West = x < 1 ? null : _tileMap[x - 1, y];
                    currNode.East = x >= _dimensions.X - 1 ? null : _tileMap[x + 1, y];
                }
            }
        }

        public TileMap DeepCopy()
        {
            var result = new TileMap();

            var sizeX = _dimensions.X;
            var sizeY = _dimensions.Y;

            result._dimensions = new Point(sizeX, sizeY);
            result._tileMap = new TileNode[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    var currTile = _tileMap[x, y];
                    result._tileMap[x, y] = new TileNode(currTile);
                }
            }

            MapNodes(result._tileMap);

            return result;
        }

        public void Dispose()
        {
            _tileMap = null;
            GC.SuppressFinalize(this);
        }
    }
}
