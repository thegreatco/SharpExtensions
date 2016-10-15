﻿namespace SharpExtensions
{
    /// <summary>
    /// An interface to enable the use of <see cref="UriExtensions"/> on classes.
    /// </summary>
    public partial interface IUrlFormatable
    {
        /// <summary>
        /// Gets the value to use when splitting multiple words.
        /// </summary>
        /// <returns> A <see cref="string"/> to use as the delimeter. </returns>
        string MultiWordDelimeter();

        /// <summary>
        /// Gets a value indicating whether to split words using their case.
        /// </summary>
        /// <returns> A <see cref="bool"/>. </returns>
        bool SplitOnCamelCase();
    }
}
