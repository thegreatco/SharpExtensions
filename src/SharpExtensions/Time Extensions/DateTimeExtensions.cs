﻿using System;

namespace SharpExtensions
{
    /// <summary>
    /// A set of DateTime Extension Methods
    /// </summary>
    public static partial class TimeExtensions
    {
        /// <summary>
        /// Convert a long value of seconds to a DateTime from Unix Epoch.
        /// </summary>
        /// <param name="val"> The seconds since Epoch. </param>
        /// <returns> A <see cref="DateTime"/>. </returns>
        public static DateTime FromUnixTime(this long val)
        {
            return FromUnixTime((double)val);
        }

        /// <summary>
        /// Convert a long value of seconds to a DateTime from Unix Epoch.
        /// </summary>
        /// <param name="val"> The seconds since Epoch. </param>
        /// <returns> A <see cref="DateTime"/>. </returns>
        public static DateTime FromUnixTime(this int val)
        {
            return FromUnixTime((double) val);
        }

        /// <summary>
        /// Convert a double value of seconds to a DateTime from Unix Epoch.
        /// </summary>
        /// <param name="val"> The seconds since Epoch. </param>
        /// <returns> A <see cref="DateTime"/>. </returns>
        public static DateTime FromUnixTimeUtc(this double val)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(val);
        }

        /// <summary>
        /// Convert a double value of seconds to a Local DateTime from Unix Epoch.
        /// </summary>
        /// <param name="val"> The seconds since Epoch. </param>
        /// <returns> A <see cref="DateTime"/>. </returns>
        public static DateTime FromUnixTime(this double val)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            return epoch.AddSeconds(val);
        }
    }
}
