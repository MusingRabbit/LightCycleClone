using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.Util
{
    public static class Texture2DHelper
    {
        public static Texture2D CreateWhiteTexture2D(GraphicsDevice device, int width, int height)
        {
            return CreateBlankTexture2D(device, width, height, Color.White);
        }

        public static Texture2D CreateWhiteTextureWithBorder(GraphicsDevice device, int width, int height, int padding)
        {
            var result = new Texture2D(device, width, height);

            var colourArr = new Color[width, height];
            var data = new List<Color>();

            for (int x = 0; x < colourArr.GetLength(0); x++)
            {
                for (int y = 0; y < colourArr.GetLength(1); y++)
                {
                    var edge = x < padding || y < padding || x >= (width - padding) || y >= (height - padding);
                    data.Add(edge ? Color.Black : Color.White);
                }
            }

            result.SetData(data.ToArray());

            return result;
        }

        public static Texture2D CreateBlankTexture2D(GraphicsDevice device, int width, int height, Color color)
        {
            var result = new Texture2D(device, width, height);

            var data = new Color[width * height];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            result.SetData(data);

            return result;
        }
    }
}
