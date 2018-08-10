using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone.Util
{
    public static class EnumUtil
    {
        public static TEnum Parse<TEnum>(string name)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return (TEnum)Enum.Parse(typeof(TEnum), name);
        }

        public static List<TEnum> GetList<TEnum>()
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var result = new List<TEnum>();
            var type = typeof(TEnum);

            if (!type.IsEnum)
            {
                throw new ArgumentException("Given Type <TEnum> must be an Enum");
            }

            var values = Enum.GetValues(type).Cast<TEnum>();

            foreach(var item in values)
            {
                result.Add(item);
            }

            return result;
        }

        public static Dictionary<int, string> GetDictionary<TEnum>()
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            return GetDictionary<TEnum, int>();
        }

        public static Dictionary<TVal, string> GetDictionary<TEnum, TVal>()
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var result = new Dictionary<TVal, string>();
            var type = typeof(TEnum);

            if (!type.IsEnum)
            {
                throw new ArgumentException("Given Type <TEnum> must be an Enum");
            }

            var values = Enum.GetValues(type).Cast<TVal>();

            foreach(var value in values)
            {
                result.Add(value, Enum.GetName(type, value));
            }

            return result;
        }
    }
}
