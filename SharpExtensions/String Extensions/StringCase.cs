using System;
using System.ComponentModel;

namespace SharpExtensions
{
    /// <summary>
    /// An <see cref="Enum"/> of possible capitalization cases of strings.
    /// </summary>
    public enum StringCase
    {
        /// <summary>
        /// The default capitalization
        /// </summary>
        Default,
        /// <summary>
        /// Lower Case, ex. i like widgets.
        /// </summary>
        [Description("Lower Case")]
        Lower,
        /// <summary>
        /// Upper Case, ex. I LIKE WIDGETS.
        /// </summary>
        [Description("Upper Case")]
        Upper,
        /// <summary>
        /// Lower Camelcase, ex: iLikeWidgets.
        /// </summary>
        [Description("Lower Camelcase")]
        LowerCamel,
        /// <summary>
        /// Upper Camelcase, ex: ILikeWidgets.
        /// </summary>
        [Description("Upper Camelcase")]
        UpperCamel
    }
}
