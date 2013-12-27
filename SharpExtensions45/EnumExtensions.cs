using System;

namespace SharpExtensions
{
    public static partial class EnumExtensions
    {
        public static string EnumToString(this Enum val)
        {
            return val.ToString().ToLower();
        }
    }
}
