using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class LinqExtensionsTests
    {
        [Test]
        public void WhereNotNull()
        {
            var arr = new[] { "1", "2", "3", "4", null };

            Assert.IsTrue(arr.WhereNotNull().ContainsAll(new[] { "1", "2", "3", "4" }));
            Assert.IsFalse(arr.WhereNotNull().Contains(null));

            var nullableArr = new int?[] { null, 1, 2, 3 };

            Assert.IsTrue(nullableArr.WhereNotNull().ContainsAll(new[] { 1, 2, 3 }));
        }

        [Test]
        public void WhereNotNullOrEmpty()
        {
            var arr = new[] { "abc", string.Empty, "def", null, " " };

            Assert.IsTrue(arr.WhereNotNullOrEmpty().ContainsAll(new[] { "abc", "def", " " }));
            Assert.IsFalse(arr.WhereNotNullOrEmpty().ContainsAll(new[] { string.Empty, null }));
        }

        [Test]
        public void WhereNotNullOrWhitespace()
        {
            var arr = new[] { "abc", string.Empty, "def", null, " " };

            Assert.IsTrue(arr.WhereNotNullOrWhitespace().ContainsAll(new[] { "abc", "def" }));
            Assert.IsFalse(arr.WhereNotNullOrWhitespace().ContainsAll(new[] { string.Empty, null, " " }));
        }

        [Test]
        public void SelectMany()
        {
            var arr = new[] { new[] { "str1" }, new[] { "str2" }};

            Assert.IsTrue(arr.SelectMany().ContainsAll(new[] { "str1", "str2" }));
        }

        [Test]
        public void ToHashSet()
        {
            var arr = new[] { "abc", string.Empty, "def", null, "abc", "def" };
            var res = arr.ToHashSet();

            Assert.IsTrue(res is HashSet<string>);
            Assert.IsTrue(res.ContainsOnly(new[] { "abc", "def", string.Empty, null }));
            Assert.IsFalse(res.Contains(" "));
        }

        [Test]
        public void AddRange()
        {
            var arr1 = new List<string> { "abc" };
            var arr2 = new List<string> { "def", "ghi" };
            arr1.AddRange(arr2);

            Assert.IsTrue(arr1.ContainsAll(arr2));
            Assert.IsFalse(arr1.ContainsOnly(arr2));
        }

        [Test]
        public void ContainsAny()
        {
            var arr1 = new[] { "abc" };
            var arr2 = new[] { "abc", "def", "ghi" };

            Assert.IsTrue(arr1.ContainsAny(arr2));

            arr2 = new [] { "def" };

            Assert.IsFalse(arr1.ContainsAny(arr2));
        }

        [Test]
        public void ContainsAll()
        {
            var arr1 = new[] { "abc", "def", "ghi" };
            var arr2 = new[] { "abc", "def", "ghi" };

            Assert.IsTrue(arr1.ContainsAll(arr2));

            arr2 = new[] { "def" };

            Assert.IsTrue(arr1.ContainsAll(arr2));

            arr2 = new[] { "jkl" };

            Assert.IsFalse(arr1.ContainsAll(arr2));
        }

        [Test]
        public void ContainsOnly()
        {
            var arr1 = new[] { "abc", "def", "ghi" };
            var arr2 = new[] { "abc", "def", "ghi" };

            Assert.IsTrue(arr1.ContainsOnly(arr2));

            arr2 = new[] { "def" };

            Assert.IsFalse(arr1.ContainsOnly(arr2));

            arr2 = new[] { "def", "ghi" };

            Assert.IsFalse(arr1.ContainsOnly(arr2));
        }

        [Test]
        public void IncludeIf()
        {
            var stuff = new[] { "abc", "def", "ghi" };
            var args = new[] { true, false, true };
            Assert.IsTrue(stuff.IncludeIf(args).ContainsOnly(new[] { "abc", "ghi" }));
            
            var args2 = new[] { "foo", "foo", "bar" };
            Assert.IsTrue(stuff.IncludeIf(args2, "foo").ContainsOnly(new[] { "abc", "def" }));
        }

        [Test]
        public void RemoveIf()
        {
            var stuff = new[] { "abc", "def", "ghi" };
            Assert.IsTrue(stuff.RemoveIf(x => !x.EndsWith("c")).ContainsOnly(new[] { "def", "ghi" }));
        }

        [Test]
        public void FastAnySpeedTest()
        {
            const int n = 10000;
            var times = new Tuple<long, long>[1000];
            for (var i = 0; i < 1000; i++)
            {
                var r = new Random();
                var en = Enumerable.Range(0, 10*n).ToList();
                var sw1 = Stopwatch.StartNew();
                var result = en.Any();
                sw1.Stop();
                var sw2 = Stopwatch.StartNew();
                result = en.FastAny();
                sw2.Stop();
                times[i] = new Tuple<long, long>(sw1.ElapsedTicks, sw2.ElapsedTicks);
            }

            Console.WriteLine("Any vs FastAny");
            Console.WriteLine("{0} vs {1}", times.Average(x => x.Item1), times.Average(x => x.Item2));
        }

        [Test]
        public void FastSingleSpeedTest()
        {
            const int n = 10000;
            var times = new Tuple<long, long>[1000];
            for (var i = 0; i < 1000; i++)
            {
                var r = new Random();
                var en = Enumerable.Range(0, 10 * n).ToList();
                var sw1 = Stopwatch.StartNew();
                var result = en.Single(x => x == 10000);
                sw1.Stop();
                var sw2 = Stopwatch.StartNew();
                result = en.FastSingle(x => x == 10000);
                sw2.Stop();
                times[i] = new Tuple<long, long>(sw1.ElapsedTicks, sw2.ElapsedTicks);
            }

            Console.WriteLine("Single vs FastSingle");
            Console.WriteLine("{0} vs {1}", times.Average(x => x.Item1), times.Average(x => x.Item2));
        }
    }
}
