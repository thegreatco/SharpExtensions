using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SharpExtensions;

namespace SharpExtensions45.Tests
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

            var nullableArr = new int?[4] { null, 1, 2, 3 };

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
    }
}
