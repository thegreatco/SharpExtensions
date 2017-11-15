using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpExtensions
{
    /// <summary>
    /// A collection of extension methods for <see cref="Uri"/>.
    /// </summary>
    public static partial class UriExtensions
    {
        /// <summary>
        /// Generates a Uri from the supplied string and any string format parameters.
        /// </summary>
        /// <param name="formatString"> The string to convert to a Uri. </param>
        /// <param name="args"> The parameters to format into the string using string.format. </param>
        /// <returns> A <see cref="Uri"/>. </returns>
        public static Uri ToUri(this string formatString, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(formatString)) throw new ArgumentNullException(nameof(formatString));

            return new UriBuilder(formatString.With(args)).Uri;
        }

        /// <summary>
        /// Generates a Uri from the supplied string, any string format parameters, and the supplied <see cref="IUrlFormatable"/> object as query parameters.
        /// </summary>
        /// <param name="formatString"> The string to convert to a Uri. </param>
        /// <param name="obj"> The <see cref="IUrlFormatable"/> object to be added as Url parameters. </param>
        /// <param name="args"> The parameters to format into the string using string.format. </param>
        /// <returns>A <see cref="Uri"/> formatted with the supplied args and <see cref="IUrlFormatable"/> object as query parameters.</returns>
        public static Uri ToUri(this string formatString, IUrlFormatable obj, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(formatString)) throw new ArgumentNullException(nameof(formatString));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            return obj.FormatForUri(formatString.With(args));
        }

        /// <summary>
        /// Add the supplied Key and Value to the <see cref="Uri"/> Query string.
        /// </summary>
        /// <param name="uri"> The Uri to which the query parameters are to be added. </param>
        /// <param name="key"> The key to be added to the query. </param>
        /// <param name="val"> The of the key to be added to the query. </param>
        /// <returns> The modified <see cref="Uri"/>. </returns>
        public static Uri With(this Uri uri, string key, object val)
        {
            return uri.With(new[] { new KeyValuePair<string, object>(key, val) });
        }

        /// <summary>
        /// Add the supplied KeyValuePairs to the <see cref="Uri"/> Query string.
        /// </summary>
        /// <param name="uri"> The Uri to which the Query string will be added. </param>
        /// <param name="arg"> The KeyValuePair to be added to the Query string. </param>
        /// <returns> The modified <see cref="Uri"/>. </returns>
        public static Uri With(this Uri uri, KeyValuePair<string, object> arg)
        {
            return uri.With(new[] { arg });
        }

        /// <summary>
        /// Add the supplied KeyValuePairs to the <see cref="Uri"/> Query string.
        /// </summary>
        /// <param name="uri"> The Uri to which the Query string will be added. </param>
        /// <param name="args"> The KeyValuePairs to be added to the Query string. </param>
        /// <returns> The modified <see cref="Uri"/>. </returns>
        public static Uri With(this Uri uri, IEnumerable<KeyValuePair<string, object>> args)
        {
            var builder = new UriBuilder(uri);
            var query = GetQueryParameters(builder);
            query.AddRange(args.Select(arg => new KeyValuePair<string, object>(arg.Key, arg.Value)));
            builder.Query = string.Join("&", query.Select(x => "{0}={1}".With(x.Key, x.Value)));
            return builder.Uri;
        }

        /// <summary>
        /// Remove the provided KeyValuePair from the Uri Query string.
        /// </summary>
        /// <param name="uri"> The Uri from which to remove the KeyValuePair. </param>
        /// <param name="key"> The key of the parameter to remove. </param>
        /// <param name="val"> The value of the parameter to remove. </param>
        /// <returns> The modified <see cref="Uri"/>. </returns>
        public static Uri Remove(this Uri uri, string key, object val)
        {
            return uri.Remove(new[] { new KeyValuePair<string, object>(key, val) });
        }

        /// <summary>
        /// Remove the provided KeyValuePair from the Uri Query string.
        /// </summary>
        /// <param name="uri"> The Uri from which to remove the KeyValuePair. </param>
        /// <param name="arg"> The KeyValuePair to remove from the Query string. </param>
        /// <returns> The modified <see cref="Uri"/>. </returns>
        public static Uri Remove(this Uri uri, KeyValuePair<string, object> arg)
        {
            return uri.Remove(new[] { arg });
        }

        /// <summary>
        /// Remove the provided KeyValuePairs from the Uri Query string.
        /// </summary>
        /// <param name="uri"> The Uri from which to remove the KeyValuePairs. </param>
        /// <param name="args"> The KeyValuePairs to remove from the Query string. </param>
        /// <returns> The modified <see cref="Uri"/>. </returns>
        public static Uri Remove(this Uri uri, IEnumerable<KeyValuePair<string, object>> args)
        {
            var builder = new UriBuilder(uri);
            var query = GetQueryParameters(builder);
            foreach (var obj in args)
            {
                bool result;
                do result = query.Remove(obj); while (result);
            }

            builder.Query = string.Join("&", query.Select(x => "{0}={1}".With(x.Key, x.Value)));
            return builder.Uri;
        }

        /// <summary>
        /// Remove all of the provided keys from the Uri Query string.
        /// </summary>
        /// <param name="uri"> The Uri from which to remove the keys. </param>
        /// <param name="keys"> The keys to removed from the Query string. </param>
        /// <returns> The modified <see cref="Uri"/>. </returns>
        public static Uri Remove(this Uri uri, params string[] keys)
        {
            var builder = new UriBuilder(uri);
            var query = GetQueryParameters(builder);
            foreach (var obj in keys)
            {
                bool result;
                do result = query.Remove(query.FirstOrDefault(x => x.Key == obj)); while (result);
            }

            builder.Query = string.Join("&", query.Select(x => "{0}={1}".With(x.Key, x.Value)));
            return builder.Uri;
        }

        private static Uri FormatForUri<T>(this T obj, string @string) where T : IUrlFormatable
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException(nameof(@string));

            var allNull = true;
            var props = obj.GetType().GetRuntimeProperties();
            var kvps = new List<KeyValuePair<string, object>>();
            foreach (var prop in props)
            {
                var val = prop.GetValue(obj, null);
                if (val == null) continue;

                var propName = obj.SplitOnCamelCase()
                    ? prop.Name.FormatUriParameter(obj.MultiWordDelimeter())
                    : prop.Name;

                allNull = false;
                switch (val)
                {
                case string[] stringArray:
                    kvps.Add(new KeyValuePair<string, object>(propName, stringArray.ValidatedJoin(",")));
                    break;
                case Enum _:
                    kvps.Add(new KeyValuePair<string, object>(propName, val.ToString().ToLower()));
                    break;
                case bool _:
                    kvps.Add(new KeyValuePair<string, object>(propName, val.ToString().ToLower()));
                    break;
                default:
                    kvps.Add(new KeyValuePair<string, object>(propName, val));
                    break;
                }
            }

            if (allNull) throw new ArgumentException("All the properties of the object are null.  At least one property must have a value. ");

            return @string.ToUri().With(kvps);
        }

        private static string FormatUriParameter(this string @string, string delimeter)
        {
            if (string.IsNullOrWhiteSpace(@string)) throw new ArgumentNullException(nameof(@string));
            if (string.IsNullOrWhiteSpace(delimeter)) throw new ArgumentNullException(nameof(delimeter));

            var builder = new StringBuilder();
            for (var i = 0; i < @string.Length; i++)
            {
                builder.Append(@string[i]);
                if (i < @string.Length - 1 && char.IsLower(@string[i]) && char.IsUpper(@string[i + 1])) builder.Append(delimeter);
            }

            return builder.ToString().ToLower();
        }

        private static List<KeyValuePair<string, object>> GetQueryParameters(UriBuilder builder)
        {
            return builder.Query.Replace("?", string.Empty).Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries)).Select(x => new KeyValuePair<string, object>(x[0], x[1])).ToList();
        }
    }
}
