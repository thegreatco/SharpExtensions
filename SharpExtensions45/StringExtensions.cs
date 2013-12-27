using System;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;

namespace DotNetExtensions
{
    public static class StringExtensions
    {
        [StringFormatMethod("format")]
        public static string With(this string @string, params object[] args)
        {
            return string.Format(@string, args);
        }

        public static string Join(this string[] strings, string delimeter)
        {
            return string.Join(delimeter, strings);
        }

        public static bool IsNumeric(this string @string)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");

            double val;
            return double.TryParse(@string, out val);
        }

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

        public static string ValidatedJoin(this string[] strings, string delimeter)
        {
            return string.Join(delimeter, strings.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public static bool IsLower(this string @string)
        {
            return @string.Select(x => (int) x).All(x => x >= 97 && x <= 122);
        }

        public static bool IsUpper(this string @string)
        {
            return @string.Select(x => (int)x).All(x => x >= 65 && x <= 90);
        }

        public static string Left(this string @string, int length)
        {
            return @string.Substring(0, length);
        }

        public static string Right(this string @string, int length)
        {
            return @string.Substring(@string.Length - length - 1, length);
        }

        public static string Mid(this string @string, int start, int end)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");
            if (start > end) throw new IndexOutOfRangeException("start must be <= end.");

            return @string.Substring(start, end - start);
        }
    }
}
