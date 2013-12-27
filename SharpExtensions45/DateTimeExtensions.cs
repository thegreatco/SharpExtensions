using System;

namespace SharpExtensions
{
    public static partial class DateTimeExtensions
    {
        public static DateTime FromUnixTime(this long val)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(val);
        }
    }
}
