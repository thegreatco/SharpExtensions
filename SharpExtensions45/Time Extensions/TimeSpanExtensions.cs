using System;

namespace SharpExtensions
{
    public static partial class TimeExtensions
    {
        /// <summary>
        /// Return a <see cref="DateTime"/> representing the <see cref="TimeSpan"/> from now.
        /// </summary>
        /// <param name="timeSpan">The <see cref="TimeSpan"/> to add</param>
        /// <returns>The <see cref="DateTime"/></returns>
        public static DateTime FromNow(this TimeSpan timeSpan)
        {
            return DateTime.Now.Add(timeSpan);
        }

        /// <summary>
        /// Return a <see cref="DateTime"/> representing the <see cref="TimeSpan"/> from Utc now.
        /// </summary>
        /// <param name="timeSpan">The <see cref="TimeSpan"/> to add</param>
        /// <returns>The <see cref="DateTime"/></returns>
        public static DateTime FromNowUtc(this TimeSpan timeSpan)
        {
            return DateTime.UtcNow.Add(timeSpan);
        }

        /// <summary>
        /// Return a <see cref="DateTime"/> representing the <see cref="TimeSpan"/> before now.
        /// </summary>
        /// <param name="timeSpan">The <see cref="TimeSpan"/> to subtract</param>
        /// <returns>The <see cref="DateTime"/></returns>
        public static DateTime BeforeNow(this TimeSpan timeSpan)
        {
            return DateTime.Now.Subtract(timeSpan);
        }

        /// <summary>
        /// Return a <see cref="DateTime"/> representing the <see cref="TimeSpan"/> before Utc now.
        /// </summary>
        /// <param name="timeSpan">The <see cref="TimeSpan"/> to subtract</param>
        /// <returns>The <see cref="DateTime"/></returns>
        public static DateTime BeforeNowUtc(this TimeSpan timeSpan)
        {
            return DateTime.UtcNow.Subtract(timeSpan);
        }
    }
}
