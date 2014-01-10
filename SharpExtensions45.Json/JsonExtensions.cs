using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SharpExtensions.Json
{
    public static partial class JsonExtensions
    {
        public static string ToJson(this object obj, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(obj, formatting);
        }

        public static string ToJson(this object obj, Formatting formatting, JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(obj, formatting, settings);
        }

        public static async Task<string> ToJsonAsync(this object obj, Formatting formatting = Formatting.Indented)
        {
            return await JsonConvert.SerializeObjectAsync(obj, formatting);
        }

        public static async Task<string> ToJsonAsync(this object obj, Formatting formatting, JsonSerializerSettings settings)
        {
            return await JsonConvert.SerializeObjectAsync(obj, formatting, settings);
        }

        public static T FromJson<T>(this string @string)
        {
            return JsonConvert.DeserializeObject<T>(@string);
        }

        public static T FromJson<T>(this string @string, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(@string, settings);
        }

        public static T FromJson<T>(this string @string, params JsonConverter[] converters)
        {
            return JsonConvert.DeserializeObject<T>(@string, converters);
        }

        public static async Task<T> FromJsonAsync<T>(this string @string)
        {
            return await JsonConvert.DeserializeObjectAsync<T>(@string);
        }

        public static async Task<T> FromJsonAsync<T>(this string @string, JsonSerializerSettings settings)
        {
            return await JsonConvert.DeserializeObjectAsync<T>(@string, settings);
        }
    }
}
