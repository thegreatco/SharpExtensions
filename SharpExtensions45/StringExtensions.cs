using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;

namespace SharpExtensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Extension method for <see cref="string"/>.Format().
        /// </summary>
        /// <param name="string"><see cref="string"/> properly formatted for string.Format().</param>
        /// <param name="args"><see cref="Array"/> of objects to replace the placeholders in string.Format()</param>
        /// <returns>The formatted <see cref="string"/>.</returns>
        [StringFormatMethod("format")]
        public static string With(this string @string, params object[] args)
        {
            return string.Format(@string, args);
        }

        /// <summary>
        /// Extensions method for <see cref="string"/>.Join().
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> strings, string delimeter)
        {
            return string.Join(delimeter, strings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string @string)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");

            double val;
            return double.TryParse(@string, out val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static string CleanForUrl(this string @string)
        {
            var chars = Enumerable.Range(0, 31)
                .Union(Enumerable.Range(33, 15))
                .Union(Enumerable.Range(58, 7))
                .Union(Enumerable.Range(91, 6))
                .Union(Enumerable.Range(123, 5))
                .Select(x => ((char) x).ToString(CultureInfo.InvariantCulture));
            @string = chars.Aggregate(@string, (current, x) => current.Replace(x, string.Empty));
            return @string.Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strings"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public static string ValidatedJoin(this string[] strings, string delimeter)
        {
            return string.Join(delimeter, strings.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static bool IsLower(this string @string)
        {
            return @string.Select(x => (int) x).All(x => x >= 97 && x <= 122);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public static bool IsUpper(this string @string)
        {
            return @string.Select(x => (int)x).All(x => x >= 65 && x <= 90);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="string"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string @string, int length)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");
            if (length > @string.Length) throw new IndexOutOfRangeException("length");

            return @string.Substring(0, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="string"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string @string, int length)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");
            if (length > @string.Length) throw new IndexOutOfRangeException("length");

            return @string.Substring(@string.Length - length, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="string"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string Mid(this string @string, int start, int end)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");
            if (start > @string.Length) throw new IndexOutOfRangeException("start");
            if (end > @string.Length) throw new IndexOutOfRangeException("end");
            if (start > end) throw new IndexOutOfRangeException("start must be <= end.");

            return @string.Substring(start, end - start + 1);
        }

        /// <summary>
        /// Determines if a <see cref="string"/> contains any item in <see cref="IEnumerable{T}"/> args.
        /// </summary>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether any item in <param name="args"/> appear in <param name="source"/>.</returns>
        public static bool ContainsAny(this string source, params string[] args)
        {
            return args.Any(source.Contains);
        }

        /// <summary>
        /// Determines if a <see cref="string"/> contains all items in <see cref="IEnumerable{T}"/> args.
        /// </summary>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether all items in <param name="args"/> appear in <param name="source"/>.</returns>
        public static bool ContainsAll(this string source, params string[] args)
        {
            return args.All(source.Contains);
        }
    }
}
