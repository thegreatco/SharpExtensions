using System;
using System.Globalization;

namespace SharpExtensions
{
    public partial class StringExtensions
    {
        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="short"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="short"/>.</returns>
        public static short? ToShort(this string @string, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return short.Parse(@string);
            short val;
            var result = short.TryParse(@string, out val);
            return result ? new short?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="short"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <returns>A <see cref="short"/>.</returns>
        public static short ToShort(this string @string, NumberStyles styles)
        {
            return short.Parse(@string, styles);
        }

        /// <summary>
        /// Convert the string to a <see cref="short"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <returns>A <see cref="short"/>.</returns>
        public static short ToShort(this string @string, IFormatProvider provider)
        {
            return short.Parse(@string, provider);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="short"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="short"/>.</returns>-
        /// 
        public static short? ToShort(this string @string, NumberStyles styles, IFormatProvider provider, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return short.Parse(@string, styles, provider);
            short val;
            var result = short.TryParse(@string, styles, provider, out val);
            return result ? new short?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="ushort"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="ushort"/>.</returns>
        public static ushort? ToUShort(this string @string, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return ushort.Parse(@string);
            ushort val;
            var result = ushort.TryParse(@string, out val);
            return result ? new ushort?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="ushort"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <returns>A <see cref="ushort"/>.</returns>
        public static ushort ToUShort(this string @string, NumberStyles styles)
        {
            return ushort.Parse(@string, styles);
        }

        /// <summary>
        /// Convert the string to a <see cref="ushort"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <returns>A <see cref="ushort"/>.</returns>
        public static ushort ToUShort(this string @string, IFormatProvider provider)
        {
            return ushort.Parse(@string, provider);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="ushort"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="ushort"/>.</returns>
        public static ushort? ToUShort(this string @string, NumberStyles styles, IFormatProvider provider, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return ushort.Parse(@string, styles, provider);
            ushort val;
            var result = ushort.TryParse(@string, styles, provider, out val);
            return result ? new ushort?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="int"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="int"/>.</returns>
        public static int? ToInt(this string @string, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return int.Parse(@string);
            int val;
            var result = int.TryParse(@string, out val);
            return result ? new int?(val) : null;
        }

        /// <summary>
        /// Convert the string to an <see cref="int"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <returns>An <see cref="int"/>.</returns>
        public static int ToInt(this string @string, NumberStyles styles)
        {
            return int.Parse(@string, styles);
        }

        /// <summary>
        /// Convert the string to an <see cref="int"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <returns>An <see cref="int"/>.</returns>
        public static int ToInt(this string @string, IFormatProvider provider)
        {
            return int.Parse(@string, provider);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="int"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="int"/>.</returns>
        public static int? ToInt(this string @string, NumberStyles styles, IFormatProvider provider, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return int.Parse(@string, styles, provider);
            int val;
            var result = int.TryParse(@string, styles, provider, out val);
            return result ? new int?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="uint"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="uint"/>.</returns>
        public static uint? ToUInt(this string @string, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return uint.Parse(@string);
            uint val;
            var result = uint.TryParse(@string, out val);
            return result ? new uint?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="uint"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <returns>An <see cref="int"/>.</returns>
        public static uint ToUInt(this string @string, NumberStyles styles)
        {
            return uint.Parse(@string, styles);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="uint"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <returns>An <see cref="int"/>.</returns>
        public static uint ToUInt(this string @string, IFormatProvider provider)
        {
            return uint.Parse(@string, provider);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="uint"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="uint"/>.</returns>
        public static uint? ToUInt(this string @string, NumberStyles styles, IFormatProvider provider, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return uint.Parse(@string, styles, provider);
            uint val;
            var result = uint.TryParse(@string, styles, provider, out val);
            return result ? new uint?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="long"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="long"/>.</returns>
        public static long? ToLong(this string @string, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return long.Parse(@string);
            long val;
            var result = long.TryParse(@string, out val);
            return result ? new long?(val) : null;
        }

        /// <summary>
        /// Convert the string to an <see cref="long"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <returns>An <see cref="long"/>.</returns>
        public static long ToLong(this string @string, NumberStyles styles)
        {
            return long.Parse(@string, styles);
        }

        /// <summary>
        /// Convert the string to an <see cref="long"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <returns>An <see cref="long"/>.</returns>
        public static long ToLong(this string @string, IFormatProvider provider)
        {
            return long.Parse(@string, provider);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="long"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="long"/>.</returns>
        public static long? ToLong(this string @string, NumberStyles styles, IFormatProvider provider, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return long.Parse(@string, styles, provider);
            long val;
            var result = long.TryParse(@string, styles, provider, out val);
            return result ? new long?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="ulong"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="ulong"/>.</returns>
        public static ulong? ToULong(this string @string, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return ulong.Parse(@string);
            ulong val;
            var result = ulong.TryParse(@string, out val);
            return result ? new ulong?(val) : null;
        }

        /// <summary>
        /// Convert the string to an <see cref="ulong"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <returns>An <see cref="ulong"/>.</returns>
        public static ulong ToULong(this string @string, NumberStyles styles)
        {
            return ulong.Parse(@string, styles);
        }

        /// <summary>
        /// Convert the string to an <see cref="ulong"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <returns>An <see cref="ulong"/>.</returns>
        public static ulong ToULong(this string @string, IFormatProvider provider)
        {
            return ulong.Parse(@string, provider);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="ulong"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="ulong"/>.</returns>
        public static ulong? ToULong(this string @string, NumberStyles styles, IFormatProvider provider, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return ulong.Parse(@string, styles, provider);
            ulong val;
            var result = ulong.TryParse(@string, styles, provider, out val);
            return result ? new ulong?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="float"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="float"/>.</returns>
        public static float? ToFloat(this string @string, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return float.Parse(@string);
            float val;
            var result = float.TryParse(@string, out val);
            return result ? new float?(val) : null;
        }

        /// <summary>
        /// Convert the string to an <see cref="float"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <returns>An <see cref="float"/>.</returns>
        public static float ToFloat(this string @string, NumberStyles styles)
        {
            return float.Parse(@string, styles);
        }

        /// <summary>
        /// Convert the string to an <see cref="float"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <returns>An <see cref="float"/>.</returns>
        public static float ToFloat(this string @string, IFormatProvider provider)
        {
            return float.Parse(@string, provider);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="float"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="float"/>.</returns>
        public static float? ToFloat(this string @string, NumberStyles styles, IFormatProvider provider, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return float.Parse(@string, styles, provider);
            float val;
            var result = float.TryParse(@string, styles, provider, out val);
            return result ? new float?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="double"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="double"/>.</returns>
        public static double? ToDouble(this string @string, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return double.Parse(@string);
            double val;
            var result = double.TryParse(@string, out val);
            return result ? new double?(val) : null;
        }

        /// <summary>
        /// Convert the string to an <see cref="double"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <returns>An <see cref="double"/>.</returns>
        public static double ToDouble(this string @string, NumberStyles styles)
        {
            return double.Parse(@string, styles);
        }

        /// <summary>
        /// Convert the string to an <see cref="double"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <returns>An <see cref="double"/>.</returns>
        public static double ToDouble(this string @string, IFormatProvider provider)
        {
            return double.Parse(@string, provider);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="double"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="double"/>.</returns>
        public static double? ToDouble(this string @string, NumberStyles styles, IFormatProvider provider, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return double.Parse(@string, styles, provider);
            double val;
            var result = double.TryParse(@string, styles, provider, out val);
            return result ? new double?(val) : null;
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="decimal"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="decimal"/>.</returns>
        public static decimal? ToDecimal(this string @string, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return decimal.Parse(@string);
            decimal val;
            var result = decimal.TryParse(@string, out val);
            return result ? new decimal?(val) : null;
        }

        /// <summary>
        /// Convert the string to an <see cref="decimal"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <returns>An <see cref="decimal"/>.</returns>
        public static decimal ToDecimal(this string @string, NumberStyles styles)
        {
            return decimal.Parse(@string, styles);
        }

        /// <summary>
        /// Convert the string to an <see cref="decimal"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <returns>An <see cref="decimal"/>.</returns>
        public static decimal ToDecimal(this string @string, IFormatProvider provider)
        {
            return decimal.Parse(@string, provider);
        }

        /// <summary>
        /// Convert the string to a <see cref="Nullable"/> <see cref="decimal"/>.
        /// </summary>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="styles">The <see cref="NumberStyles"/>.</param>
        /// <param name="provider">The <see cref="IFormatProvider"/>.</param>
        /// <param name="throwOnParseFailure">A <see cref="bool"/> indicating if the method should throw when a parse fails, default: false.</param>
        /// <returns>A <see cref="Nullable"/> <see cref="decimal"/>.</returns>
        public static decimal? ToDecimal(this string @string, NumberStyles styles, IFormatProvider provider, bool throwOnParseFailure = false)
        {
            if (throwOnParseFailure) return decimal.Parse(@string, styles, provider);
            decimal val;
            var result = decimal.TryParse(@string, styles, provider, out val);
            return result ? new decimal?(val) : null;
        }
    }
}
