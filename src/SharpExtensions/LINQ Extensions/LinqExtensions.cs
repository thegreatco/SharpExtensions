using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SharpExtensions
{
    /// <summary>
    /// A collection of extension methods for LINQ.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static partial class LinqExtensions
    {
        /// <summary>
        /// Return only non-null items from the source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>A collection with only non-null values.</returns>
        public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source) where TSource : class
        {
            return source == null ? Enumerable.Empty<TSource>() : source.Where(x => x != null);
        }

        /// <summary>
        /// Return only non-null items from the source sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>A collection with only non-null values.</returns>
        public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource?> source) where TSource : struct
        {
            return source == null ? Enumerable.Empty<TSource>() : source.Where(x => x.HasValue).Select(x => x.Value);
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
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>A flattened version of the source sequence.</returns>
        public static IEnumerable<TSource> SelectMany<TSource>(this IEnumerable<IEnumerable<TSource>> source)
        {
            return source.SelectMany(item => item);
        }

        /// <summary>
        /// Convert an <see cref="IEnumerable{TSource}"/> to a hashset, to eliminate all duplicates.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <returns>The <see cref="ISet{TSource}"/></returns>
        public static ISet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source)
        {
            return new HashSet<TSource>(source);
        }

        /// <summary>
        /// Adds an <see cref="IEnumerable{TSource}"/> range to an <see cref="ICollection{TSource}"/> collection.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="collection">The collection to which to add the range.</param>
        /// <param name="range">The range to add to the collection.</param>
        public static void AddRange<TSource>(this ICollection<TSource> collection, IEnumerable<TSource> range)
        {
            foreach (var item in range) collection.Add(item);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{TSource}"/> contains any item in <see cref="IEnumerable{TSource}"/> args.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether any item in <paramref name="args"/> appears in <paramref name="source"/>.</returns>
        public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> args)
        {
            return args.Any(source.Contains);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{TSource}"/> contains any item in args.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether any item in <paramref name="args"/> appears in <paramref name="source"/>.</returns>
        public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, params TSource[] args)
        {
            return args.Any(source.Contains);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{TSource}"/> contains all items in <see cref="IEnumerable{TSource}"/> args.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether all items in <paramref name="args"/> appears in <paramref name="source"/>.</returns>
        public static bool ContainsAll<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> args)
        {
            return args.All(source.Contains);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{TSource}"/> contains all items in args.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether all items in <paramref name="args"/> appears in <paramref name="source"/>.</returns>
        public static bool ContainsAll<TSource>(this IEnumerable<TSource> source, params TSource[] args)
        {
            return args.All(source.Contains);
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{TSource}"/> contains only the items in <see cref="IEnumerable{TSource}"/> args.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether only items in <paramref name="args"/> appear in <paramref name="source"/>.</returns>
        public static bool ContainsOnly<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> args)
        {
            var sourceEnumerable = source as TSource[] ?? source.ToArray();
            var argsEnumerable = args as TSource[] ?? args.ToArray();

            if (argsEnumerable.Count() != sourceEnumerable.Length) return false;

            return !sourceEnumerable.Except(argsEnumerable).Any();
        }

        /// <summary>
        /// Determines if a <see cref="IEnumerable{T}"/> contains only the items in args.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The args to check against the source.</param>
        /// <returns>A <see cref="bool"/> indicating whether only items in <paramref name="args"/> appear in <paramref name="source"/>.</returns>
        public static bool ContainsOnly<TSource>(this IEnumerable<TSource> source, params TSource[] args)
        {
            // TODO: check this logic against http://msmvps.com/blogs/jon_skeet/archive/2010/12/30/reimplementing-linq-to-objects-part-17-except.aspx
            var sourceEnumerable = source as TSource[] ?? source.ToArray();

            if (args.Count() != sourceEnumerable.Length) return false;

            return !sourceEnumerable.Except(args).Any();
        }

        /// <summary>
        /// Include only the values in source as long as the corresponding position in args is true.
        /// </summary>
        /// <typeparam name="TSource">The type of data in the collection.</typeparam>
        /// <param name="source">The source against which to check the args.</param>
        /// <param name="args">The <see cref="Enumerable"/> of <see cref="bool"/> values.</param>
        /// <returns>The filtered list.</returns>
        public static IEnumerable<TSource> IncludeIf<TSource>(this IEnumerable<TSource> source, IEnumerable<bool> args)
        {
            var sourceArr = source as TSource[] ?? source.ToArray();
            var argsArr = args as bool[] ?? args.ToArray();
            if (sourceArr.Length != argsArr.Length) throw new ArgumentOutOfRangeException(nameof(args), "Length of includes does not match length of source.");

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
            if (sourceArr.Length != argsArr.Length) throw new ArgumentOutOfRangeException(nameof(args), "Length of includes does not match length of source.");

            return sourceArr.Zip(argsArr, (x, include) => new { include, x }).Where(x => trueValues.Contains(x.include)).Select(x => x.x);
        }

        /// <summary>
        /// Remove values from the source that match the supplied predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source from which to remove items.</param>
        /// <param name="matchPredicate">The predicate to use to match items.</param>
        /// <returns>The filtered list.</returns>
        public static IEnumerable<TSource> RemoveIf<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> matchPredicate)
        {
            return source.Where(matchPredicate);
        }

        /// <summary>
        /// An Optimized implementation of <see cref="IEnumerable{T}"/>.Any()
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source to test.</param>
        /// <returns>A <see cref="bool"/> indicating if any items exist in the collection.</returns>
        public static bool FastAny<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source is ICollection<TSource> collection)
                return collection.Count > 0;
            using (var enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                    return true;
            }
            return false;
        }

        /// <summary>
        /// An Optimized implementation of <see cref="IEnumerable{T}"/>.Single(predicate).
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source to test.</param>
        /// <param name="predicate">The predicate on which to filter.</param>
        /// <returns>The first item matching <paramref name="predicate"/> in the <paramref name="source"/> collection.</returns>
        public static TSource FastSingle<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Single();
        }

        /// <summary>
        /// Like FirstOrDefault() but returns the <see cref="IEnumerable{T}"/> as an array or null.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source to to return.</param>
        /// <returns>The <paramref name="source"/> as an array or null.</returns>
        public static TSource[] ToArrayOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.FastAny() ? source.ToArray() : null;
        }

        /// <summary>
        /// Like FirstOrDefault() but returns the <see cref="IEnumerable{T}"/> as a List or null.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source to to return.</param>
        /// <returns>The <paramref name="source"/> as an List or null.</returns>
        public static IList<TSource> ToListOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.FastAny() ? source.ToList() : null;
        }

        /// <summary>
        /// Like FirstOrDefault() but returns the <see cref="IEnumerable{T}"/> as a HashSet or null.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source to to return.</param>
        /// <returns>The <paramref name="source"/> as a HashSet or null.</returns>
        public static ISet<TSource> ToHashSetOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            return source.FastAny() ? source.ToHashSet() : null;
        }

        /// <summary>
        /// Takes an Array of Arrays and merges them into a Set of Sets where overlapping groups are merged.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source to to return.</param>
        /// <returns>The <paramref name="source"/> as a HashSet or null.</returns>
        public static ISet<HashSet<TSource>> Crossect<TSource>(this IEnumerable<IEnumerable<TSource>> source)
        {
            var hashSet = new HashSet<HashSet<TSource>>();
            var sourceArr = source as IEnumerable<TSource>[] ?? source.ToArray();
            foreach (var list in sourceArr)
            {
                var set = new HashSet<TSource>();
                var listArr = list as TSource[] ?? list.ToArray();
                foreach (var item in listArr)
                {
                    var items = sourceArr.Where(x => x.ContainsAny(item)).SelectMany();
                    set.AddRange(items);
                }

                var hashSetItem = hashSet.SingleOrDefault(x => x.Contains(set.First()));
                if (hashSetItem != null) hashSetItem.AddRange(set);
                else hashSet.Add(set);
            }

            return hashSet;
        }

        /// <summary>
        /// Partitions an enumerable into multiple enumerables of specified size.
        /// </summary>
        /// <param name="enumerable"> The input enumerable. </param>
        /// <param name="size"> The size of each partition. </param>
        /// <typeparam name="T"> The type of element. </typeparam>
        /// <returns> A enumerable of partitions. </returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> enumerable, int size)
        {
            if (enumerable == null) throw new ArgumentNullException(nameof(enumerable));
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size must be > 0");

            var partition = new List<T>();
            foreach (var item in enumerable)
            {
                partition.Add(item);
                if (partition.Count != size) continue;

                yield return partition;
                partition = new List<T>();
            }

            if (partition.Count > 0)
                yield return partition;
        }

        /// <summary>
        /// Returns the first (or default) object that is wrapped in a Task. <seealso cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IEnumerable</param>
        /// <typeparam name="TSource">The type wrapped by the IEnumerable</typeparam>
        /// <returns>The first or default object returned by the Task.</returns>
        public static async Task<TSource> FirstOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> task)
        {
            return (await task.ConfigureAwait(false)).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first (or default) object that is wrapped in a Task. <seealso cref="List{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IList</param>
        /// <typeparam name="TSource">The type wrapped by the IList</typeparam>
        /// <returns>The first or default object returned by the Task.</returns>
        public static async Task<TSource> FirstOrDefaultAsync<TSource>(this Task<List<TSource>> task)
        {
            return (await task.ConfigureAwait(false)).FirstOrDefault();
        }

        /// <summary>
        /// Returns the first (or default) object that is wrapped in a Task. <seealso cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IEnumerable</param>
        /// <typeparam name="TSource">The type wrapped by the IEnumerable</typeparam>
        /// <param name="predicate">The predicate used to filter the results.</param>
        /// <returns>The first or default object returned by the Task.</returns>
        /// <returns>The first (or default) object matching the predicate</returns>
        public static async Task<TSource> FirstOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> task, Func<TSource, bool> predicate)
        {
            return (await task.ConfigureAwait(false)).FirstOrDefault(predicate);
        }

        /// <summary>
        /// Returns the first (or default) object that is wrapped in a Task. <seealso cref="List{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IList</param>
        /// <typeparam name="TSource">The type wrapped by the IList</typeparam>
        /// <param name="predicate">The predicate used to filter the results.</param>
        /// <returns>The first or default object returned by the Task.</returns>
        /// <returns>The first (or default) object matching the predicate</returns>
        public static async Task<TSource> FirstOrDefaultAsync<TSource>(this Task<List<TSource>> task, Func<TSource, bool> predicate)
        {
            return (await task.ConfigureAwait(false)).FirstOrDefault(predicate);
        }

        /// <summary>
        /// Returns the first object that is wrapped in a Task. <seealso cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IEnumerable</param>
        /// <typeparam name="TSource">The type wrapped by the IEnumerable</typeparam>
        /// <returns>The first or default object returned by the Task.</returns>
        public static async Task<TSource> FirstAsync<TSource>(this Task<IEnumerable<TSource>> task)
        {
            return (await task.ConfigureAwait(false)).First();
        }

        /// <summary>
        /// Returns the first object that is wrapped in a Task. <seealso cref="List{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IList</param>
        /// <typeparam name="TSource">The type wrapped by the IList</typeparam>
        /// <returns>The first or default object returned by the Task.</returns>
        public static async Task<TSource> FirstAsync<TSource>(this Task<List<TSource>> task)
        {
            return (await task.ConfigureAwait(false)).First();
        }

        /// <summary>
        /// Returns the first (or default) object that is wrapped in a Task. <seealso cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IEnumerable</param>
        /// <typeparam name="TSource">The type wrapped by the IEnumerable</typeparam>
        /// <param name="predicate">The predicate used to filter the results.</param>
        /// <returns>The first or default object returned by the Task.</returns>
        /// <returns>The first object matching the predicate.</returns>
        public static async Task<TSource> FirstAsync<TSource>(this Task<IEnumerable<TSource>> task, Func<TSource, bool> predicate)
        {
            return (await task.ConfigureAwait(false)).First();
        }

        /// <summary>
        /// Returns the first (or default) object that is wrapped in a Task. <seealso cref="List{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IList</param>
        /// <typeparam name="TSource">The type wrapped by the IList</typeparam>
        /// <param name="predicate">The predicate used to filter the results.</param>
        /// <returns>The first or default object returned by the Task.</returns>
        /// <returns>The first object matching the predicate.</returns>
        public static async Task<TSource> FirstAsync<TSource>(this Task<List<TSource>> task, Func<TSource, bool> predicate)
        {
            return (await task.ConfigureAwait(false)).First();
        }

        /// <summary>
        /// Returns a single (or default) object that is wrapped in a Task. <seealso cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IEnumerable</param>
        /// <typeparam name="TSource">The type wrapped by the IEnumerable</typeparam>
        /// <returns>The single (or default) object returned by the Task.</returns>
        public static async Task<TSource> SingleOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> task)
        {
            return (await task.ConfigureAwait(false)).Single();
        }

        /// <summary>
        /// Returns a single (or default) object that is wrapped in a Task. <seealso cref="List{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IList</param>
        /// <typeparam name="TSource">The type wrapped by the IList</typeparam>
        /// <returns>The single (or default) object returned by the Task.</returns>
        public static async Task<TSource> SingleOrDefaultAsync<TSource>(this Task<List<TSource>> task)
        {
            return (await task.ConfigureAwait(false)).Single();
        }

        /// <summary>
        /// Returns a single (or default) object that is wrapped in a Task. <seealso cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IEnumerable</param>
        /// <param name="predicate">The predicate used to filter the results.</param>
        /// <typeparam name="TSource">The type wrapped by the IEnumerable</typeparam>
        /// <returns>The single (or default) object matching the predicate.</returns>
        public static async Task<TSource> SingleOrDefaultAsync<TSource>(this Task<IEnumerable<TSource>> task, Func<TSource, bool> predicate)
        {
            return (await task.ConfigureAwait(false)).Single(predicate);
        }

        /// <summary>
        /// Returns a single (or default) object that is wrapped in a Task. <seealso cref="List{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IList</param>
        /// <param name="predicate">The predicate used to filter the results.</param>
        /// <typeparam name="TSource">The type wrapped by the IList</typeparam>
        /// <returns>The single (or default) object matching the predicate.</returns>
        public static async Task<TSource> SingleOrDefaultAsync<TSource>(this Task<List<TSource>> task, Func<TSource, bool> predicate)
        {
            return (await task.ConfigureAwait(false)).Single(predicate);
        }

        /// <summary>
        /// Returns a single object that is wrapped in a Task. <seealso cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IEnumerable</param>
        /// <typeparam name="TSource">The type wrapped by the IEnumerable</typeparam>
        /// <returns>A single object returned by the Task.</returns>
        public static async Task<TSource> SingleAsync<TSource>(this Task<IEnumerable<TSource>> task)
        {
            return (await task.ConfigureAwait(false)).Single();
        }

        /// <summary>
        /// Returns a single object that is wrapped in a Task. <seealso cref="List{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IList</param>
        /// <typeparam name="TSource">The type wrapped by the IList</typeparam>
        /// <returns>A single object returned by the Task.</returns>
        public static async Task<TSource> SingleAsync<TSource>(this Task<List<TSource>> task)
        {
            return (await task.ConfigureAwait(false)).Single();
        }

        /// <summary>
        /// Returns a single object that is wrapped in a Task. <seealso cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IEnumerable</param>
        /// <param name="predicate">The predicate used to filter the results.</param>
        /// <typeparam name="TSource">The type wrapped by the IEnumerable</typeparam>
        /// <returns>A single object matching the predicate.</returns>
        public static async Task<TSource> SingleAsync<TSource>(this Task<IEnumerable<TSource>> task, Func<TSource, bool> predicate)
        {
            return (await task.ConfigureAwait(false)).Single();
        }

        /// <summary>
        /// Returns a single object that is wrapped in a Task. <seealso cref="List{T}"/>
        /// </summary>
        /// <param name="task">The task that wraps the IList</param>
        /// <param name="predicate">The predicate used to filter the results.</param>
        /// <typeparam name="TSource">The type wrapped by the IList</typeparam>
        /// <returns>A single object matching the predicate.</returns>
        public static async Task<TSource> SingleAsync<TSource>(this Task<List<TSource>> task, Func<TSource, bool> predicate)
        {
            return (await task.ConfigureAwait(false)).Single();
        }

        /// <summary>
        /// Partitions an enumerable into multiple enumerables of specified size.
        /// </summary>
        /// <param name="memBlock"> The input Memory{T}. </param>
        /// <param name="size"> The size of each partition. </param>
        /// <typeparam name="T"> The type of element. </typeparam>
        /// <returns> A enumerable of partitions. </returns>
        public static IEnumerable<Memory<T>> Partition<T>(this Memory<T> memBlock, int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size must be > 0");

            var currentIndex = 0;
            while (currentIndex < memBlock.Length)
            {
                yield return memBlock.Slice(currentIndex, size);
                currentIndex += size;
            }
        }

        public static TSource FirstOrDefault<TSource>(this Span<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var t in source)
            {
                if (predicate(t)) return t;
            }

            return default;
        }

        public static TSource FirstOrDefault<TSource>(this Memory<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Span.FirstOrDefault(predicate);
        }

        public static TSource FirstOrDefault<TSource>(this Span<TSource> source)
        {
            return source.Length == 0 ? default : source[0];
        }

        public static TSource FirstOrDefault<TSource>(this Memory<TSource> source)
        {
            return source.Span.FirstOrDefault();
        }

        public static TSource First<TSource>(this Span<TSource> source, Func<TSource, bool> predicate)
        {
            if (source.Length == 0) throw new InvalidOperationException("Sequence contains no elements.");
            
            foreach (var t in source)
            {
                if (predicate(t)) return t;
            }

            throw new InvalidOperationException("Sequence contains no matching element.");
        }

        public static TSource First<TSource>(this Memory<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Span.First(predicate);
        }

        public static TSource First<TSource>(this Span<TSource> source)
        {
            if (source.Length == 0) throw new InvalidOperationException("Sequence contains no elements.");

            return source[0];
        }

        public static TSource First<TSource>(this Memory<TSource> source)
        {
            return source.Span.First();
        }

        public static TSource Single<TSource>(this Span<TSource> source, Func<TSource, bool> predicate)
        {
            if (source.Length == 0) throw new InvalidOperationException("Sequence contains no elements.");
            object match = null;
            foreach (var t in source)
            {
                if (!predicate(t)) continue;
                if (match == null)
                    match = t;
                else
                    throw new InvalidOperationException("Sequence contains more than one element");
            }

            return (TSource) match;
        }

        public static TSource Single<TSource>(this Memory<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Span.Single(predicate);
        }

        public static TSource Single<TSource>(this Span<TSource> source)
        {
            if (source.Length == 0) throw new InvalidOperationException("Sequence contains no elements.");
            if (source.Length > 1) throw new InvalidOperationException("Sequence contains more than one element");
            return source[0];
        }

        public static TSource Single<TSource>(this Memory<TSource> source)
        {
            return source.Span.Single();
        }

        public static bool Any<TSource>(this Span<TSource> source, Func<TSource, bool> predicate)
        {
            return source.FirstOrDefault(predicate) == null;
        }

        public static bool Any<TSource>(this Memory<TSource> source, Func<TSource, bool> predicate)
        {
            return source.FirstOrDefault(predicate) == null;
        }

        public static bool Any<TSource>(this Span<TSource> source)
        {
            return source.FirstOrDefault() == null;
        }

        public static bool Any<TSource>(this Memory<TSource> source)
        {
            return source.FirstOrDefault() == null;
        }
    }
}
