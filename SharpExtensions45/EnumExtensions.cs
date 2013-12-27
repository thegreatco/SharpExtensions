using System;

namespace SharpExtensions
{
    public static partial class EnumExtensions
    {
        /// <summary>
        /// Convert an enum to a pretty formatted string.
        /// </summary>
        /// <param name="val"> The enum to convert to a <see cref="string"/>. </param>
        /// <param name="case"> A <see cref="StringCase"/> indicating which case to return.  Valid enumerations are StringCase.Lower and StringCase.Upper. </param>
        /// <exception cref="ArgumentNullException"> If the enum is null. </exception>
        /// <returns></returns>
        public static string EnumToString(this Enum val, StringCase @case = StringCase.Lower)
        {
            if (val == null) throw new ArgumentNullException("val");

            switch (@case)
            {
                case StringCase.Lower:
                    return val.ToString().ToLower();
                case StringCase.Upper:
                    return val.ToString().ToUpper();
                default:
                    return val.ToString();
            }
        }
    }
}
