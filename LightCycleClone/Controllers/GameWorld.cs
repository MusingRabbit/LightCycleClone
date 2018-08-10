using LightCycleClone.GameObjects.Character;
using LightCycleClone.GameObjects.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone
{
    public class GameWorld : IDisposable
    {
        private Dictionary<Guid, Player> _playerDict;

        private TileMap _tileMap;

        public TileMap TileMap
        {
            get
            {
                return _tileMap;
            }
        }

        public GameWorld()
        {
            _playerDict = new Dictionary<Guid, Player>();
        }

        public GameWorld(GameWorld rhs)
            :this()
        {
            _tileMap = rhs._tileMap.DeepCopy();
            
            foreach (var srcPlayer in rhs._playerDict.Values)
            {
                _playerDict[srcPlayer.Id] = new Player(srcPlayer);
            }

            Initialise();
        }

        public GameWorld Copy()
        {
            return new GameWorld(this);
        }

        public void Initialise()
        {
            foreach (var player in _playerDict.Values)
            {
                var tile = _tileMap.GetTile(player.Position);
                tile.SetOccupiedBy(player);
                player.CurrentNode = tile;
            }
        }

        public void Update()
        {
            var livePlayers = _playerDict.Values.Where(x => x.State == PlayerState.Alive);

            foreach (var player in livePlayers)
            {
                var prevTile = _tileMap.GetTile(player.Position);
                player.Update();

                var nexTile = _tileMap.GetTile(player.Position);

                if (nexTile.State != TileState.Free)
                {
                    player.State = PlayerState.Dead;
                }
                else
                {
                    prevTile.ClearOccupiedBy();
                    nexTile.SetOccupiedBy(player);
                }
            }
        }

        public void AddPlayer(Player player)
        {
            if (_playerDict.ContainsKey(player.Id))
            {
                throw new Exception($"Player with Id [{player.Id}] has already been added to game world.");
            }

            _playerDict[player.Id] = player;
        }

        public Player GetPlayer(Guid id)
        {
            return _playerDict[id];
        }

        public void SetTileMap(TileMap map)
        {
            _tileMap = map;
        }

        public IEnumerable<Player> GetPlayers()
        {
            return _playerDict.Values.ToList();
        }

        public IEnumerable<Player> GetLivePlayers()
        {
            foreach (var val in _playerDict.Values)
            {
                if (val.State == PlayerState.Alive)
                {
                    yield return val;
                }
            }
        }

        public void Dispose()
        {
            _playerDict.Clear();
            _tileMap.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
