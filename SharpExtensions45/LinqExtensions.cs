using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpExtensions
{
    public static partial class LinqExtensions
    {
        /// <summary>
        /// Return only non-null items from the source sequence.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>A collection with only non-null values.</returns>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source) where T : class
        {
            return source == null ? Enumerable.Empty<T>() : source.Where(x => x != null);
        }

        /// <summary>
        /// Return only non-null items from the source sequence.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>A collection with only non-null values.</returns>
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : struct
        {
            return source == null ? Enumerable.Empty<T>() : source.Where(x => x.HasValue).Select(x => x.Value);
        }

        /// <summary>
        /// Return only non-null, non-empty items from the source sequence.
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <returns>A collection with only non-null, non-empty values.</returns>
        public static IEnumerable<string> WhereNotNullOrEmpty(this IEnumerable<string> source)
        {
            return source == null ? Enumerable.Empty<string>() : source.Where(x => !string.IsNullOrEmpty(x));
        }

        /// <summary>
        /// Return only non-null, non-empty items from the source sequence.
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <returns>A collection with only non-null, non-empty values.</returns>
        public static IEnumerable<string> WhereNotNullOrWhitespace(this IEnumerable<string> source)
        {
            return source == null ? Enumerable.Empty<string>() : source.Where(x => !string.IsNullOrWhiteSpace(x));
        }

        /// <summary>
        /// Flatten the source sequence of enumerables into a single enumerable.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A flattened version of the source sequence.</returns>
        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source.SelectMany(item => item);
        }

        /// <summary>
        /// Convert an <see cref="IEnumerable{T}"/> to a hashset, to eliminate all duplicates.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>The <see cref="ISet{T}"/></returns>
        public static ISet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        /// <summary>
        /// Adds an <see cref="IEnumerable{T}"/> range to an <see cref="ICollection{T}"/> collection.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="collection">The collection to which to add the range.</param>
        /// <param name="range">The range to add to the collection.</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            foreach (var item in range) collection.Add(item);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{T}"/> contains any item in <see cref="IEnumerable{T}"/> args.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether any item in <param name="args"/> appears in <param name="source"/>.</returns>
        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> args)
        {
            return args.Any(source.Contains);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{T}"/> contains any item in args.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether any item in <param name="args"/> appears in <param name="source"/>.</returns>
        public static bool ContainsAny<T>(this IEnumerable<T> source, params T[] args)
        {
            return args.Any(source.Contains);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{T}"/> contains all items in <see cref="IEnumerable{T}"/> args.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether all items in <param name="args"/> appears in <param name="source"/>.</returns>
        public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> args)
        {
            return args.All(source.Contains);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{T}"/> contains all items in args.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether all items in <param name="args"/> appears in <param name="source"/>.</returns>
        public static bool ContainsAll<T>(this IEnumerable<T> source, params T[] args)
        {
            return args.All(source.Contains);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{T}"/> contains only the items in <see cref="IEnumerable{T}"/> args.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether only items in <param name="args"/> appear in <param name="source"/>.</returns>
        public static bool ContainsOnly<T>(this IEnumerable<T> source, IEnumerable<T> args)
        {
            var sourceEnumerable = source as T[] ?? source.ToArray();
            var argsEnumerable = args as T[] ?? args.ToArray();

            if (argsEnumerable.Count() != sourceEnumerable.Length) return false;

            return !sourceEnumerable.Except(argsEnumerable).Any();
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{T}"/> contains only the items in args.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether only items in <param name="args"/> appear in <param name="source"/>.</returns>
        public static bool ContainsOnly<T>(this IEnumerable<T> source, params T[] args)
        {
            // TODO: check this logic against http://msmvps.com/blogs/jon_skeet/archive/2010/12/30/reimplementing-linq-to-objects-part-17-except.aspx
            var sourceEnumerable = source as T[] ?? source.ToArray();

            if (args.Count() != sourceEnumerable.Length) return false;

            return !sourceEnumerable.Except(args).Any();
        }

        /// <summary>
        /// Include only the values in source as long as the corresponding position in args is true.
        /// </summary>
        /// <typeparam name="T">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The <see cref="Enumerable"/> of <see cref="bool"/> values.</param>
        /// <returns>The filtered list.</returns>
        public static IEnumerable<T> IncludeIf<T>(this IEnumerable<T> source, IEnumerable<bool> args)
        {
            var sourceArr = source as T[] ?? source.ToArray();
            var argsArr = args as bool[] ?? args.ToArray();
            if (sourceArr.Length != argsArr.Length) throw new ArgumentOutOfRangeException("args", "Length of includes does not match length of source.");

            return sourceArr.Zip(argsArr, (x, include) => new { include, x }).Where(x => x.include).Select(x => x.x);
        }

        /// <summary>
        /// Include only the values in source as long as the corresponding position in args is true.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the source collection.</typeparam>
        /// <typeparam name="TArgs">The type of data in the args collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The <see cref="Enumerable"/> of <see cref="bool"/> values.</param>
        /// <param name="trueValues">The values that represent a true state for T.</param>
        /// <returns>The filtered list.</returns>
        public static IEnumerable<TSource> IncludeIf<TSource, TArgs>(this IEnumerable<TSource> source, IEnumerable<TArgs> args, params TArgs[] trueValues)
        {
            var sourceArr = source as TSource[] ?? source.ToArray();
            var argsArr = args as TArgs[] ?? args.ToArray();
            if (sourceArr.Length != argsArr.Length) throw new ArgumentOutOfRangeException("args", "Length of includes does not match length of source.");

            return sourceArr.Zip(argsArr, (x, include) => new { include, x }).Where(x => trueValues.Contains(x.include)).Select(x => x.x);
        }
    }
}
