﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SharpExtensions
{
    /// <summary>
    /// A collection of extension methods for <see cref="Enum"/>.
    /// </summary>
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
        /// Gets the description for the supplied Enum Value.
        /// </summary>
        /// <param name="val">The value for which to get the description attribute.</param>
        /// <returns>The <see cref="string"/> description.</returns>
        public static string GetDescription(this Enum val)
        {
            var fields = val.GetType().GetField(val.GetName());
            var attribute = fields.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute == null ? val.GetName() : attribute.Description;
        }

        /// <summary>
        /// Gets the description for the supplied Enum Value.
        /// </summary>
        /// <typeparam name="T">The type of Enum to get.</typeparam>
        /// <param name="val">The value for which to get the description attribute.</param>
        /// <returns>The <see cref="string"/> description.</returns>
        public static string GetDescription<T>(this T val)
        {
            var type = typeof (T);
            if (!type.IsEnum) throw new InvalidEnumArgumentException();
            var castVal = val as Enum;

            var fields = type.GetField(castVal.GetName());
            var attribute = fields.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute == null ? castVal.GetName() : attribute.Description;
        }

        /// <summary>
        /// Gets all the description attributes in the Enum.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Enum"/>.</typeparam>
        /// <param name="val">The value of the Enum.</param>
        /// <returns>A Dictionary keyed on the <see cref="Enum"/> values and their descriptions.</returns>
        public static Dictionary<T, string> GetDescriptions<T>(this Enum val)
        {
            return GetDescriptions<T>();
        }

        /// <summary>
        /// Gets all the description attributes in the Enum.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Enum"/>.</typeparam>
        /// <returns>A Dictionary keyed on the <see cref="Enum"/> values and their descriptions.</returns>
        public static Dictionary<T, string> GetDescriptions<T>()
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidEnumArgumentException();

            var values = Enum.GetValues(type).OfType<T>();

            return values.ToDictionary(value => value, value => value.GetDescription());
        }

        /// <summary>
        /// Get the value of an <see cref="Enum"/> based on its description attribute.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="Enum"/>.</typeparam>
        /// <param name="description">The Description attribute of the <see cref="Enum"/>.</param>
        /// <returns>The value of T or default(T) if the description is not found.</returns>
        public static T GetValueFromDescription<T>(this string description) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidEnumArgumentException();

            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.Name == description) return (T) field.GetValue(null);
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null && attribute.Description == description) return (T) field.GetValue(null);    
            }

            return default(T);
        }

        /// <summary>
        /// Convert the string to an <see cref="Enum"/> of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="string">The <see cref="string"/> to parse.</param>
        /// <param name="ignoreCase">A <see cref="bool"/> indicating if the parser should ignore case.</param>
        /// <returns>A <see cref="Nullable"/> of <typeparamref name="T"/>.</returns>
        public static T? ToEnum<T>(this string @string, bool ignoreCase = false) where T : struct
        {
            T val;
            var result = Enum.TryParse(@string, ignoreCase, out val);
            return result ? new T?(val) : null;
        }
    }
}
