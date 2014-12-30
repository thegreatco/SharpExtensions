using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpExtensions
{
    public static partial class NumericExtensions
    {
        #region TimeSpan

        /// <summary>
        /// Converts the <see cref="long"/> to years using the <see cref="Constants.SecondsInAJulianYear"/>. A Julian Year is defined at 365.25 days.
        /// </summary>
        /// <param name="val">The number of years</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Years(this long val)
        {
            return TimeSpan.FromSeconds(val * Constants.SecondsInAJulianYear);
        }

        /// <summary>
        /// Convert the <see cref="long"/> to Months. This is not implemented since each month varies in length.
        /// </summary>
        /// <param name="val">The number of months</param>
        /// <returns>A <see cref="TimeSpan"/></returns>
        /// <exception cref="NotImplementedException">This throws because the method can never be implemented</exception>
        public static TimeSpan Months(this long val)
        {
            throw new NotImplementedException("This TimeSpan is inaccurate as each month is of varying length.");
        }

        /// <summary>
        /// Converts the <see cref="long"/> to days.
        /// </summary>
        /// <param name="val">The number of days</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Days(this long val)
        {
            return TimeSpan.FromDays(val);
        }

        /// <summary>
        /// Converts the <see cref="long"/> to hours.
        /// </summary>
        /// <param name="val">The number of hours</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Hours(this long val)
        {
            return TimeSpan.FromHours(val);
        }

        /// <summary>
        /// Converts the <see cref="long"/> to minutes.
        /// </summary>
        /// <param name="val">The number of minutes</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Minutes(this long val)
        {
            return TimeSpan.FromMinutes(val);
        }

        /// <summary>
        /// Converts the <see cref="long"/> to seconds.
        /// </summary>
        /// <param name="val">The number of seconds</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Seconds(this long val)
        {
            return TimeSpan.FromSeconds(val);
        }

        /// <summary>
        /// Converts the <see cref="long"/> to milliseconds.
        /// </summary>
        /// <param name="val">The number of milliseconds</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Milliseconds(this long val)
        {
            return TimeSpan.FromMilliseconds(val);
        }

        #endregion

        /// <summary>
        /// Perform the <see cref="Action"/> this many times.
        /// </summary>
        /// <param name="val">The number of times to execute the <see cref="Action"/></param>
        /// <param name="action">The <see cref="Action"/> to be executed</param>
        public static void Times(this long val, Action action)
        {
            for (var i = 0; i < val; i++)
            {
                action();
            }
        }

        /// <summary>
        /// Generate an enumerable range starting with <paramref name="val"/> going to <paramref name="end"/>.
        /// </summary>
        /// <param name="val">The number at which to start the <see cref="Enumerable"/></param>
        /// <param name="end">The number at which to end the <see cref="Enumerable"/></param>
        /// <returns></returns>
        public static IEnumerable<long> To(this long val, long end)
        {
            for (var i = val; i < end; i++)
            {
                yield return i;
            }
        }
    }
}
