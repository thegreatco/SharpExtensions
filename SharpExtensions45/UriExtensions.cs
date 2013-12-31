using System;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace SharpExtensions
{
    internal static partial class UriExtensions
    {
        /// <summary>
        /// Generates a Uri from the supplied string and any string format parameters.
        /// </summary>
        /// <param name="string"> The string to convert to a Uri. </param>
        /// <param name="args"> The parameters to format into the string using string.format. </param>
        /// <returns> A <see cref="Uri"/>. </returns>
        [StringFormatMethod("format")]
        public static Uri ToUri(this string @string, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");

            return new UriBuilder(@string.With(args)).Uri;
        }

        /// <summary>
        /// Generates a Uri from the supplied string, any string format parameters, and the supplied <see cref="IUrlFormatable"/> object.
        /// </summary>
        /// <param name="string"> The string to convert to a Uri. </param>
        /// <param name="obj"> The <see cref="IUrlFormatable"/> object to be added as Url parameters. </param>
        /// <param name="args"> The parameters to format into the string using string.format. </param>
        /// <returns></returns>
        [StringFormatMethod("format")]
        public static Uri ToUri(this string @string, IUrlFormatable obj, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");
            if (obj == null) throw new ArgumentNullException("obj");

            return obj.FormatForUri(@string.With(args));
        }

        private static Uri FormatForUri<T>(this T obj, string @string) where T : IUrlFormatable
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");

            var allNull = true;
            var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                var val = prop.GetValue(obj, null);
                if (val == null) continue;

                var propName = obj.SplitOnCamelCase()
                    ? prop.Name.FormatUriParameter(obj.MultiWordDelimeter())
                    : prop.Name;

                allNull = false;
                var stringArray = val as string[];
                if (stringArray != null)
                {
                    if (@string.Contains("?")) @string += "&{0}={1}".With(propName, stringArray.ValidatedJoin(","));
                    else @string += "?{0}={1}".With(propName, stringArray.ValidatedJoin(","));
                }
                else if (val is Enum)
                {
                    if (@string.Contains("?")) @string += "&{0}={1}".With(propName, val.ToString().ToLower());
                    else @string += "?{0}={1}".With(propName, val.ToString().ToLower());
                }
                else if (val is bool)
                {
                    if (@string.Contains("?")) @string += "&{0}={1}".With(propName, val.ToString().ToLower());
                    else @string += "?{0}={1}".With(propName, val.ToString().ToLower());
                }
                else
                {
                    if (@string.Contains("?")) @string += "&{0}={1}".With(propName, val);
                    else @string += "?{0}={1}".With(propName, val);
                }
            }

            if (allNull) throw new ArgumentException("All the properties of the object are null.  At least one property must have a value. ");

            return @string.ToUri();
        }

        private static string FormatUriParameter(this string @string, string delimeter)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException("string");
            if (string.IsNullOrWhiteSpace(delimeter)) throw new ArgumentNullException("delimeter");

            var builder = new StringBuilder();
            for (var i = 0; i < @string.Length; i++)
            {
                builder.Append(@string[i]);
                if (i < @string.Length - 1 && char.IsLower(@string[i]) && char.IsUpper(@string[i + 1])) builder.Append(delimeter);
            }

            return builder.ToString().ToLower();
        }
    }
}
