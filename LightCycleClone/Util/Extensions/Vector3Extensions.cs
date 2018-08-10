using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.Util.Extensions
{
    public static class Vector3Extensions
    {
        public static Point ToPoint(this Vector3 src)
        {
            return new Point((int)src.X, (int)src.Y);
        }
    }
}
