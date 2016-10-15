using System;

namespace SharpExtensions
{
    /// <summary>
    /// A collection of extension methods for numeric datatypes.
    /// </summary>
    public static partial class NumericExtensions
    {
        #region TimeSpan

        /// <summary>
        /// Converts the <see cref="double"/> to years using the <see cref="Constants.SecondsInAJulianYear"/>. A Julian Year is defined at 365.25 days.
        /// </summary>
        /// <param name="val">The number of years</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Years(this double val)
        {
            return TimeSpan.FromSeconds(val * Constants.SecondsInAJulianYear);
        }

        /// <summary>
        /// Convert the <see cref="double"/> to Months. This is not implemented since each month varies in length.
        /// </summary>
        /// <param name="val">The number of months</param>
        /// <returns>A <see cref="TimeSpan"/></returns>
        /// <exception cref="NotImplementedException">This throws because the method can never be implemented</exception>
        public static TimeSpan Months(this double val)
        {
            throw new NotImplementedException("This TimeSpan is inaccurate as each month is of varying length.");
        }

        /// <summary>
        /// Converts the <see cref="double"/> to days.
        /// </summary>
        /// <param name="val">The number of days</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Days(this double val)
        {
            return TimeSpan.FromDays(val);
        }

        /// <summary>
        /// Converts the <see cref="double"/> to hours.
        /// </summary>
        /// <param name="val">The number of hours</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Hours(this double val)
        {
            return TimeSpan.FromHours(val);
        }

        /// <summary>
        /// Converts the <see cref="double"/> to minutes.
        /// </summary>
        /// <param name="val">The number of minutes</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Minutes(this double val)
        {
            return TimeSpan.FromMinutes(val);
        }

        /// <summary>
        /// Converts the <see cref="double"/> to seconds.
        /// </summary>
        /// <param name="val">The number of seconds</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Seconds(this double val)
        {
            return TimeSpan.FromSeconds(val);
        }

        /// <summary>
        /// Converts the <see cref="double"/> to milliseconds.
        /// </summary>
        /// <param name="val">The number of milliseconds</param>
        /// <returns>A <see cref="TimeSpan"/>.</returns>
        public static TimeSpan Milliseconds(this double val)
        {
            return TimeSpan.FromMilliseconds(val);
        }

        #endregion

        #region Multipliers

        /// <summary>
        /// Return the number times one thousand.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static double Thousand(this double val)
        {
            checked
            {
                return val * 1000d;
            }
        }

        /// <summary>
        /// Return the number times one millions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static double Million(this double val)
        {
            checked
            {
                return val * 1d.Thousand().Thousand();
            }
        }

        /// <summary>
        /// Return the number times one billions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static double Billion(this double val)
        {
            checked
            {
                return val * 1d.Thousand().Million();
            }
        }

        /// <summary>
        /// Return the number times one billions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static double Trillion(this double val)
        {
            checked
            {
                return val * 1d.Thousand().Billion();
            }
        }

        /// <summary>
        /// Return the number times one quadrillions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static double Quadrillion(this double val)
        {
            checked
            {
                return val * 1d.Thousand().Trillion();
            }
        }

        /// <summary>
        /// Return the number times one quintillions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static double Quintillion(this double val)
        {
            checked
            {
                return val * 1d.Thousand().Quadrillion();
            }
        }

        /// <summary>
        /// Return the number times one sextillions.
        /// </summary>
        /// <param name="val">The number to multiply</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static double Sextillion(this double val)
        {
            checked
            {
                return val * 1d.Thousand().Quintillion();
            }
        }

        /// <summary>
        /// Multiple the <paramref name="val"/> by 10 to the <paramref name="exp"/>.
        /// </summary>
        /// <param name="val">The value to multiply</param>
        /// <param name="exp">The exponent to which 10 will be raised.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double Pow10(this double val, int exp)
        {
            var returnExp = 1d;
            for (var i = 0; i < exp; i++)
            {
                returnExp = returnExp * 10;
            }
            return val * returnExp;
        }

        #endregion

        /// <summary>
        /// Round the provided <see cref="double"/> value to the nearest <paramref name="numberOfDigits"/> using the specified <paramref name="rounding"/>.
        /// </summary>
        /// <param name="double">The value to round.</param>
        /// <param name="numberOfDigits">The number of digits to which to round.</param>
        /// <param name="rounding">The type of rounding to perform, <see cref="MidpointRounding"/>. The default is <see cref="MidpointRounding.ToEven"/></param>
        /// <returns></returns>
        public static double Round(this double @double, int numberOfDigits = 0, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return Math.Round(@double, numberOfDigits, rounding);
        }

        /// <summary>
        /// Round the provided <see cref="decimal"/> value to the nearest <paramref name="numberOfDigits"/> using the specified <paramref name="rounding"/>.
        /// </summary>
        /// <param name="decimal">The value to round.</param>
        /// <param name="numberOfDigits">The number of digits to which to round.</param>
        /// <param name="rounding">The type of rounding to perform, <see cref="MidpointRounding"/>. The default is <see cref="MidpointRounding.ToEven"/></param>
        /// <returns></returns>
        public static decimal Round(this decimal @decimal, int numberOfDigits = 0, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return Math.Round(@decimal, numberOfDigits);
        }
    }
}
