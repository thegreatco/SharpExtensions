using System;

namespace SharpExtensions
{
    public static partial class DateTimeExtensions
    {
        /// <summary>
        /// Convert a long value of seconds to a DateTime from Unix Epoch.
        /// </summary>
        /// <param name="val"> The seconds since Epoch. </param>
        /// <returns> A <see cref="DateTime"/>. </returns>
        public static DateTime FromUnixTime(this long val)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(val);
        }

        /// <summary>
        /// Convert a long value of seconds to a DateTime from Unix Epoch.
        /// </summary>
        /// <param name="val"> The seconds since Epoch. </param>
        /// <returns> A <see cref="DateTime"/>. </returns>
        public static DateTime FromUnixTime(this int val)
        {
            return FromUnixTime((long) val);
        }
    }
}
