using LightCycleClone.GameObjects;
using LightCycleClone.GameObjects.Character;
using LightCycleClone.GameObjects.World;
using LightCycleClone.Util.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone
{
    public class Renderer
    {
        private ResourceController _resources;
        private GraphicsDeviceManager _deviceManager;

        public Renderer(ResourceController resourceController, GraphicsDeviceManager graphicsDeviceManager)
        {
            _resources = resourceController;
            _deviceManager = graphicsDeviceManager;
        }

        public void Render(SpriteBatch spriteBatch, GameWorld world)
        {
            var tileMap = world.TileMap;
            var tileWidth = _deviceManager.PreferredBackBufferWidth / tileMap.Dimensions.X;
            var tileHeight = _deviceManager.PreferredBackBufferHeight / tileMap.Dimensions.Y;

            var players = world.GetPlayers();

            foreach (var tile in tileMap.GetAllTiles())
            {
                Render(spriteBatch, tile, GetTileColour(tile), tileWidth, tileHeight);
            }
            
            foreach(var player in world.GetPlayers())
            {
                Render(spriteBatch, player, player.Colour, tileWidth, tileHeight);
            }
        }

        private Color GetTileColour(TileNode tile)
        {
            var result = Color.Black;

            if (tile.State == TileState.Wall)
            {
                result = Color.Gray;
            }
            if (tile.State == TileState.Claimed)
            {
                var owner = tile.Owner as Player;
                result = owner.Colour;
            }

            return result;
        }


        public void Render(SpriteBatch spriteBatch, TileObject tileObject, Color colour, int tileWidth, int tileHeight)
        {
            var txr = _resources.GetTexture2D("tileTexture");
            var sizePoint = new Point(tileWidth, tileHeight);
            var posPoint = tileObject.Position * sizePoint;
            var rect = new Rectangle(posPoint, sizePoint);
            spriteBatch.Draw(txr, rect, colour);
        }
    }
}
