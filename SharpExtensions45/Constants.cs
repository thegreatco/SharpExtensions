namespace SharpExtensions
{
    /// <summary>
    /// A collection of commonly used Constants.
    /// </summary>
    public static partial class Constants
    {
        /// <summary>
        /// The number of bytes in a Petabyte.
        /// </summary>
        public const long PB = TB * 1000;
        
        /// <summary>
        /// The number of bytes in a Terabyte.
        /// </summary>
        public const long TB = GB * 1000;

        /// <summary>
        /// The number of bytes in a Gigabyte.
        /// </summary>
        public const long GB = MB * 1000;

        /// <summary>
        /// The number of bytes in a Megabyte.
        /// </summary>
        public const long MB = KB * 1000;

        /// <summary>
        /// The number of bytes in a Kilobyte.
        /// </summary>
        public const long KB = 1000;

        /// <summary>
        /// The number of bytes in a Pebibyte <see href="http://en.wikipedia.org/wiki/IEC_80000-13#Prefixes_for_binary_multiples"/>.
        /// </summary>
        public const long PiB = TiB * 1024;

        /// <summary>
        /// The number of bytes in a Tebibyte <see href="http://en.wikipedia.org/wiki/IEC_80000-13#Prefixes_for_binary_multiples"/>.
        /// </summary>
        public const long TiB = GiB * 1024;

        /// <summary>
        /// The number of bytes in a Gibibyte <see href="http://en.wikipedia.org/wiki/IEC_80000-13#Prefixes_for_binary_multiples"/>.
        /// </summary>
        public const long GiB = MiB * 1024;

        /// <summary>
        /// The number of bytes in a Mebibyte <see href="http://en.wikipedia.org/wiki/IEC_80000-13#Prefixes_for_binary_multiples"/>.
        /// </summary>
        public const long MiB = KiB * 1024;

        /// <summary>
        /// The number of bytes in a Kibibyte <see href="http://en.wikipedia.org/wiki/IEC_80000-13#Prefixes_for_binary_multiples"/>.
        /// </summary>
        public const long KiB = 1024;
    }
}
