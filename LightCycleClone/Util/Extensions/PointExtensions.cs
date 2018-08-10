using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.Util.Extensions
{
    public static class PointExtensions
    {
        public static Point Multiply(this Point point, int value)
        {
            return new Point
            {
                X = point.X * value,
                Y = point.Y * value
            };
        }
    }
}
