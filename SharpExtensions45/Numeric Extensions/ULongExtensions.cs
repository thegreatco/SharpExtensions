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
        /// Converts the <see cref="ulong"/> to years using the <see cref="Constants.SecondsInAJulianYear"/>. A Julian Year is defined at 365.25 days.
        /// </summary>
        /// <param name="val">The number of years</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Years(this ulong val)
        {
            return TimeSpan.FromSeconds(val * Constants.SecondsInAJulianYear);
        }

        /// <summary>
        /// Convert the <see cref="ulong"/> to Months. This is not implemented since each month varies in length.
        /// </summary>
        /// <param name="val">The number of months</param>
        /// <returns>A <see cref="TimeSpan"/></returns>
        /// <exception cref="NotImplementedException">This throws because the method can never be implemented</exception>
        public static TimeSpan Months(this ulong val)
        {
            throw new NotImplementedException("This TimeSpan is inaccurate as each month is of varying length.");
        }

        /// <summary>
        /// Converts the <see cref="ulong"/> to days.
        /// </summary>
        /// <param name="val">The number of days</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Days(this ulong val)
        {
            return TimeSpan.FromDays(val);
        }

        /// <summary>
        /// Converts the <see cref="ulong"/> to hours.
        /// </summary>
        /// <param name="val">The number of hours</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Hours(this ulong val)
        {
            return TimeSpan.FromHours(val);
        }

        /// <summary>
        /// Converts the <see cref="ulong"/> to minutes.
        /// </summary>
        /// <param name="val">The number of minutes</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Minutes(this ulong val)
        {
            return TimeSpan.FromMinutes(val);
        }

        /// <summary>
        /// Converts the <see cref="ulong"/> to seconds.
        /// </summary>
        /// <param name="val">The number of seconds</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Seconds(this ulong val)
        {
            return TimeSpan.FromSeconds(val);
        }

        /// <summary>
        /// Converts the <see cref="ulong"/> to milliseconds.
        /// </summary>
        /// <param name="val">The number of milliseconds</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Milliseconds(this ulong val)
        {
            return TimeSpan.FromMilliseconds(val);
        }

        #endregion

        /// <summary>
        /// Return the number times one thousands.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static ulong Thousand(this ulong val)
        {
            checked
            {
                return val * 1000L;
            }
        }

        /// <summary>
        /// Return the number times one millions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static ulong Million(this ulong val)
        {
            checked
            {
                return val * 1UL.Thousand().Thousand();
            }
        }

        /// <summary>
        /// Return the number times one billions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static ulong Billion(this ulong val)
        {
            checked
            {
                return val * 1UL.Thousand().Million();
            }
        }

        /// <summary>
        /// Return the number times one billions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static ulong Trillion(this ulong val)
        {
            checked
            {
                return val * 1UL.Thousand().Billion();
            }
        }

        /// <summary>
        /// Return the number times one quadrillions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static ulong Quadrillion(this ulong val)
        {
            checked
            {
                return val * 1UL.Thousand().Trillion();
            }
        }

        /// <summary>
        /// Return the number times one quintillions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static ulong Quintillion(this ulong val)
        {
            checked
            {
                return val * 1UL.Thousand().Quadrillion();
            }
        }

        /// <summary>
        /// Multiple the <paramref name="val"/> by 10 to the <paramref name="exp"/>.
        /// </summary>
        /// <param name="val">The value to multiply</param>
        /// <param name="exp">The exponent to which 10 will be raised.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static ulong Pow10(this ulong val, int exp)
        {
            checked
            {
                var returnExp = 1UL;
                for (var i = 0; i < exp; i++)
                {
                    returnExp = returnExp * 10;
                }
                return val * returnExp;
            }
        }

        /// <summary>
        /// Perform the <see cref="Action"/> this many times.
        /// </summary>
        /// <param name="val">The number of times to execute the <see cref="Action"/></param>
        /// <param name="action">The <see cref="Action"/> to be executed</param>
        public static void Times(this ulong val, Action action)
        {
            for (var i = 0UL; i < val; i++)
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
        public static IEnumerable<ulong> To(this ulong val, ulong end)
        {
            for (var i = val; i < end; i++)
            {
                yield return i;
            }
        }
    }
}
