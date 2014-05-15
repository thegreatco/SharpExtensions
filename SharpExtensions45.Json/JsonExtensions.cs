using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SharpExtensions.Json
{
    /// <summary>
    /// A set of Json Extension Methods
    /// </summary>
    public static partial class JsonExtensions
    {
        /// <summary>
        /// Convert the object to a Json string.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <param name="formatting">The <see cref="Formatting"/> options for the Json.</param>
        /// <returns>The object in Json format.</returns>
        public static string ToJson(this object obj, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        /// <summary>
        /// Conver the object to a Json string.
        /// </summary>
        /// <param name="obj">The objec to convert.</param>
        /// <param name="formatting">Formatting options for the Json.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> to use.</param>
        /// <returns>The object in Json format.</returns>
        public static string ToJson(this object obj, Formatting formatting, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(obj, formatting, settings);
        }

        /// <summary>
        /// Convert the object to a Json string asynchronously.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <param name="formatting">The <see cref="Formatting"/> options for the Json.</param>
        /// <returns>The object in Json format.</returns>
        public static async Task<string> ToJsonAsync(this object obj, Formatting formatting = Formatting.Indented)
        {
            return await JsonConvert.SerializeObjectAsync(obj, formatting);
        }

        /// <summary>
        /// Conver the object to a Json string asynchronously.
        /// </summary>
        /// <param name="obj">The objec to convert.</param>
        /// <param name="formatting">Formatting options for the Json.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> to use.</param>
        /// <returns>The object in Json format.</returns>
        public static async Task<string> ToJsonAsync(this object obj, Formatting formatting, JsonSerializerSettings settings)
        {
            return await JsonConvert.SerializeObjectAsync(obj, formatting, settings);
        }

        /// <summary>
        /// Convert from a Json string to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="string">The Json string to deserialize.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The new <typeparamref name="T"/></returns>
        public static T FromJson<T>(this string @string)
        {
            return JsonConvert.DeserializeObject<T>(@string);
        }

        /// <summary>
        /// Convert from a Json string to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="string">The Json string to deserialize.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> to use.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The new <typeparamref name="T"/></returns>
        public static T FromJson<T>(this string @string, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(@string, settings);
        }

        /// <summary>
        /// Convert from a Json string to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="string">The Json string to deserialize.</param>
        /// <param name="converters">The <see cref="JsonConverter"/>s to use.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The new <typeparamref name="T"/></returns>
        public static T FromJson<T>(this string @string, params JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<T>(@string, converters);
        }

        /// <summary>
        /// Convert from a Json string to <typeparamref name="T"/> asynchronously.
        /// </summary>
        /// <param name="string">The Json string to deserialize.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The new <typeparamref name="T"/></returns>
        public static async Task<T> FromJsonAsync<T>(this string @string)
        {
            return await JsonConvert.DeserializeObjectAsync<T>(@string);
        }

        /// <summary>
        /// Convert from a Json string to <typeparamref name="T"/> asynchronously.
        /// </summary>
        /// <param name="string">The Json string to deserialize.</param>
        /// <param name="settings">The <see cref="JsonSerializerSettings"/> to use.</param>
        /// <typeparam name="T">The type to deserialize to.</typeparam>
        /// <returns>The new <typeparamref name="T"/></returns>
        public static async Task<T> FromJsonAsync<T>(this string @string, JsonSerializerSettings settings)
        {
            return await JsonConvert.DeserializeObjectAsync<T>(@string, settings);
        }
    }
}
