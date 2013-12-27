using System;

namespace DotNetExtensions
{
    public static class EnumExtensions
    {
        public static string EnumToString(this Enum val)
        {
            return val.ToString().ToLower();
        }
    }
}
