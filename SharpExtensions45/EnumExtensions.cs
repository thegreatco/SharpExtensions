using System;
using System.ComponentModel;
using System.Linq;

namespace SharpExtensions
{
    public static partial class EnumExtensions
    {
        /// <summary>
        /// Get the value of an enum as a string.
        /// </summary>
        /// <param name="val"> The enum to convert to a <see cref="string"/>. </param>
        /// <param name="case"> A <see cref="StringCase"/> indicating which case to return.  Valid enumerations are StringCase.Lower and StringCase.Upper. </param>
        /// <exception cref="ArgumentNullException"> If the enum is null. </exception>
        /// <returns></returns>
        public static string GetName(this Enum val, StringCase @case = StringCase.Default)
        {
            if (val == null) throw new ArgumentNullException("val");

            var name = Enum.GetName(val.GetType(), val);
            if (name == null) return null;

            switch (@case)
            {
                case StringCase.Lower:
                    return name.ToLower();
                case StringCase.Upper:
                    return name.ToUpper();
                default:
                    return name;
            }
        }

        /// <summary>
        /// Gets the Description Attribute of the <see cref="Enum"/> value.
        /// </summary>
        /// <param name="val">The value for which to get the description.</param>
        /// <returns>The description of the <see cref="Enum"/>.</returns>
        public static string GetDescription(this Enum val)
        {
            var fields = val.GetType().GetField(val.GetName());
            var attribute = fields.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute == null ? val.GetName() : attribute.Description;
        }
    }
}
