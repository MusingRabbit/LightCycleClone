using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.Util.Extensions
{
    public static class StringExtensions
    {
        public static TEnum ToEnum<TEnum>(this string name)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return EnumUtil.Parse<TEnum>(name);
        }
    }
}
