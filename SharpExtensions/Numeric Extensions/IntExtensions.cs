using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpExtensions
{
    public static partial class NumericExtensions
    {
        #region TimeSpan

        /// <summary>
        /// Converts the <see cref="int"/> to years using the <see cref="Constants.SecondsInAJulianYear"/>. A Julian Year is defined at 365.25 days.
        /// </summary>
        /// <param name="val">The number of years</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Years(this int val)
        {
            return new TimeSpan(((long)val) * Constants.SecondsInAJulianYear * TimeSpan.TicksPerSecond);
        }


        /// <summary>
        /// Convert the <see cref="int"/> to Months. This is not implemented since each month varies in length.
        /// </summary>
        /// <param name="val">The number of months</param>
        /// <returns>A <see cref="TimeSpan"/></returns>
        /// <exception cref="NotImplementedException">This throws because the method can never be implemented</exception>
        public static TimeSpan Months(this int val)
        {
            throw new NotImplementedException("This TimeSpan is inaccurate as each month is of varying length.");
        }

        /// <summary>
        /// Converts the <see cref="int"/> to days.
        /// </summary>
        /// <param name="val">The number of days</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Days(this int val)
        {
            return TimeSpan.FromDays(val);
        }

        /// <summary>
        /// Converts the <see cref="int"/> to hours.
        /// </summary>
        /// <param name="val">The number of hours</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Hours(this int val)
        {
            return TimeSpan.FromHours(val);
        }

        /// <summary>
        /// Converts the <see cref="int"/> to minutes.
        /// </summary>
        /// <param name="val">The number of minutes</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Minutes(this int val)
        {
            return TimeSpan.FromMinutes(val);
        }

        /// <summary>
        /// Converts the <see cref="int"/> to seconds.
        /// </summary>
        /// <param name="val">The number of seconds</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Seconds(this int val)
        {
            return TimeSpan.FromSeconds(val);
        }

        /// <summary>
        /// Converts the <see cref="int"/> to milliseconds.
        /// </summary>
        /// <param name="val">The number of milliseconds</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Milliseconds(this int val)
        {
            return TimeSpan.FromMilliseconds(val);
        }

        #endregion

        #region multipliers

        /// <summary>
        /// Return the number times one thousands.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int Thousand(this int val)
        {
            checked
            {
                return val * 1000;
            }
        }

        /// <summary>
        /// Return the number times one millions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int Million(this int val)
        {
            checked
            {
                return val * 1.Thousand().Thousand();
            }
        }

        /// <summary>
        /// Return the number times one billions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int Billion(this int val)
        {
            checked
            {
                return val * 1.Thousand().Million();
            }
        }

        /// <summary>
        /// Multiple the <paramref name="val"/> by 10 to the <paramref name="exp"/>.
        /// </summary>
        /// <param name="val">The value to multiply</param>
        /// <param name="exp">The exponent to which 10 will be raised.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static int Pow10(this int val, int exp)
        {
            checked
            {
                var returnExp = 1;
                for (var i = 0; i < exp; i++)
                {
                    returnExp = returnExp * 10;
                }
                return val * returnExp;
            }
        }

        #endregion

        /// <summary>
        /// Perform the <see cref="Action"/> this many times.
        /// </summary>
        /// <param name="val">The number of times to execute the <see cref="Action"/></param>
        /// <param name="action">The <see cref="Action"/> to be executed</param>
        public static void Times(this int val, Action action)
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
        public static IEnumerable<long> To(this int val, long end)
        {
            for (var i = val; i < end; i++)
            {
                yield return i;
            }
        }
    }
}
