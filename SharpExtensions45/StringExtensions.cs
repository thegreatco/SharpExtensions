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
        /// Extension method for <see cref="string"/>.Join().
        /// </summary>
        /// <param name="strings">The strings to join.</param>
        /// <param name="delimeter">The delimiter to insert between the strings.</param>
        /// <returns>The joined string.</returns>
        public static string Join(this IEnumerable<string> strings, string delimeter)
        {
            return string.Join(delimeter, strings);
        }

        /// <summary>
        /// Extension method for <see cref="string"/>.Split()
        /// </summary>
        /// <param name="string">The string to split.</param>
        /// <param name="separators">The separators with which to split the <param name="string"/>.</param>
        /// <returns>The split strings.</returns>
        public static string[] Split(this string @string, params string[] separators)
        {
            return @string == string.Empty ? null : @string.Split(StringSplitOptions.None, separators);
        }

        /// <summary>
        /// Extension method for <see cref="string"/>.Split()
        /// </summary>
        /// <param name="string">The string to split.</param>
        /// <param name="options">The <see cref="StringSplitOptions"/>.</param>
        /// <param name="separators">The separators with which to split the <param name="string"/>.</param>
        /// <returns>The split strings.</returns>
        public static string[] Split(this string @string, StringSplitOptions options, params char[] separators)
        {
            return @string == string.Empty ? null : @string.Split(separators, options);
        }

        /// <summary>
        /// Extension method for <see cref="string"/>.Split()
        /// </summary>
        /// <param name="string">The string to split.</param>
        /// <param name="options">The <see cref="StringSplitOptions"/>.</param>
        /// <param name="separators">The separators with which to split the <param name="string"/>.</param>
        /// <returns>The split strings.</returns>
        public static string[] Split(this string @string, StringSplitOptions options, params string[] separators)
        {
            return @string == string.Empty ? null : @string.Split(separators, options);
        }

        /// <summary>
        /// Determines if a string is Numeric or not.
        /// </summary>
        /// <param name="string">The string to test.</param>
        /// <returns>A <see cref="bool"/> indicating if the string is numeric.</returns>
        public static bool IsNumeric(this string @string)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");

            double val;
            return double.TryParse(@string, out val);
        }

        /// <summary>
        /// Clean a string of all invalid Url characters.
        /// </summary>
        /// <param name="string">The string to clean.</param>
        /// <returns>The cleaned string.</returns>
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
        /// Extension method for <see cref="string"/>.Join(), ignoring null, whitespace, and empty entries.
        /// </summary>
        /// <param name="strings">The strings to join.</param>
        /// <param name="delimeter">The delimiter to insert between the strings.</param>
        /// <returns>The joined string.</returns>
        public static string ValidatedJoin(this string[] strings, string delimeter)
        {
            return string.Join(delimeter, strings.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        /// <summary>
        /// Tests if the supplied string is lowercase.
        /// </summary>
        /// <param name="string">The string to test.</param>
        /// <returns>A <see cref="bool"/> indicating if the string is lowercase.</returns>
        public static bool IsLower(this string @string)
        {
            return @string.Select(x => (int) x).All(x => x >= 97 && x <= 122);
        }

        /// <summary>
        /// Tests if the supplied string is uppercase.
        /// </summary>
        /// <param name="string">The string to test.</param>
        /// <returns>A <see cref="bool"/> indicating if the string is uppercase.</returns>
        public static bool IsUpper(this string @string)
        {
            return @string.Select(x => (int)x).All(x => x >= 65 && x <= 90);
        }

        /// <summary>
        /// Extension method to return only the <param name="length"/> characters from the string.
        /// </summary>
        /// <param name="string">The string from which to get the characters.</param>
        /// <param name="length">The number of characters to return.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Left(this string @string, int length)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");
            if (length > @string.Length) throw new IndexOutOfRangeException("length");

            return @string.Substring(0, length);
        }

        /// <summary>
        /// Extension method to return only the left <param name="length"/> characters from the string.
        /// </summary>
        /// <param name="string">The string from which to get the characters.</param>
        /// <param name="length">The number of characters to return.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Right(this string @string, int length)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");
            if (length > @string.Length) throw new IndexOutOfRangeException("length");

            return @string.Substring(@string.Length - length, length);
        }

        /// <summary>
        /// Extension method for <see cref="string"/>.Substring().
        /// </summary>
        /// <param name="string">The string from which to get the characters.</param>
        /// <param name="start">The index of the first character.</param>
        /// <param name="end">The index of the last character.</param>
        /// <returns>The <see cref="string"/>.</returns>
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
