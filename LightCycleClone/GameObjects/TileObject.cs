using LightCycleClone.GameObjects.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.GameObjects
{
    public class TileObject : IGameObject, IDisposable
    {
        protected Guid Guid;
        protected Point Pos;

        public Guid Id
        {
            get
            {
                return Guid;
            }
        }

        public virtual Point Position
        {
            get
            {
                return Pos;
            }
        }
        
        public TileObject()
        {
            Guid = Guid.NewGuid();
            Pos = Point.Zero;
        }

        public virtual void Dispose()
        {
            
        }
    }
}
