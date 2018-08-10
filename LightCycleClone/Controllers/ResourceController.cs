using LightCycleClone.Exceptions;
using LightCycleClone.Util;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone
{
    public class ResourceController
    {
        private Dictionary<string, Texture2D> _textureDict;
        private ContentManager _contentManager;

        public ResourceController(ContentManager contentManager)
        {
            _textureDict = new Dictionary<string, Texture2D>();
            _contentManager = contentManager;
        }

        public void SetTexture2D(string name, Texture2D texture)
        {
            _textureDict.Add(name, texture);
        }

        public Texture2D GetTexture2D(string name)
        {
            if (!_textureDict.ContainsKey(name))
            {
                throw new ResourceNotFoundException(name);
            }

            return _textureDict[name];
        }
    }
}
