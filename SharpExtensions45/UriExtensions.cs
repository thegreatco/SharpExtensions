using System;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace DotNetExtensions
{
    public static class UriExtensions
    {
        [StringFormatMethod("format")]
        public static Uri ToUri(this string @string, params object[] args)
        {
            return new UriBuilder(@string.With(args)).Uri;
        }

        [StringFormatMethod("format")]
        public static Uri ToUri(this string @string, IUrlFormatable obj, params object[] args)
        {
            return obj.FormatForUri(@string.With(args));
        }

        private static Uri FormatForUri<T>(this T obj, string baseString) where T : IUrlFormatable
        {
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
                    if (baseString.Contains("?")) baseString += "&{0}={1}".With(propName, stringArray.ValidatedJoin(","));
                    else baseString += "?{0}={1}".With(propName, stringArray.ValidatedJoin(","));
                }
                else if (val is Enum)
                {
                    if (baseString.Contains("?")) baseString += "&{0}={1}".With(propName, val.ToString().ToLower());
                    else baseString += "?{0}={1}".With(propName, val.ToString().ToLower());
                }
                else if (val is bool)
                {
                    if (baseString.Contains("?")) baseString += "&{0}={1}".With(propName, val.ToString().ToLower());
                    else baseString += "?{0}={1}".With(propName, val.ToString().ToLower());
                }
                else
                {
                    if (baseString.Contains("?")) baseString += "&{0}={1}".With(propName, val);
                    else baseString += "?{0}={1}".With(propName, val);
                }
            }

            if (allNull) throw new ArgumentException("All the properties of the object are null.  At least one property must have a value. ");

            return baseString.ToUri();
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
