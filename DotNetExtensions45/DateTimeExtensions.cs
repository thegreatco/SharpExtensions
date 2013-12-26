using System;

namespace DotNetExtensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUnixTime(this long val)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(val);
        }
    }
}
