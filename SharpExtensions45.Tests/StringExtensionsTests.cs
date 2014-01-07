using System;
using System.Linq;
using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void With()
        {
            Assert.IsTrue("{0}".With("abc") == "abc");

            Assert.IsTrue("{0}".With("abc", "def") == "abc");

            Assert.IsTrue("{0} {1}".With("abc", "def") == "abc def");
        }

        [Test]
        public void Join()
        {
            var arr = new[] { "abc", "def" };
            Assert.IsTrue(arr.Join(",") == "abc,def");
        }

        [Test]
        public void IsNumeric()
        {
            Assert.IsTrue("123".IsNumeric());

            Assert.IsTrue("12.3".IsNumeric());

            Assert.IsFalse("123d".IsNumeric());
        }

        [Test]
        public void CleanForUrl()
        {
            var arr = new[] { "!", "*", "'", "(", ")", ";", ":", "@", "&", "=", "+", "$", ",", "/", "?", "%", "#", "[", "]" };
            var str = new string(Enumerable.Range(0, 128).Select(x => (char) x).ToArray());

            Assert.IsFalse(str.CleanForUrl().ContainsAny(arr));
        }

        [Test]
        public void ValidatedJoin()
        {
            var arr = new[] { "abc", string.Empty, "", " ", "stuff" };

            Assert.IsTrue(arr.ValidatedJoin(",") == "abc,stuff");
        }

        [Test]
        public void IsLower()
        {
            Assert.IsTrue("abc".IsLower());
            
            Assert.IsFalse("Abc".IsLower());

            Assert.IsFalse("aBc".IsLower());

            Assert.IsFalse("abC".IsLower());
        }

        [Test]
        public void IsUpper()
        {
            Assert.IsTrue("ABC".IsUpper());

            Assert.IsFalse("Abc".IsUpper());

            Assert.IsFalse("aBc".IsUpper());

            Assert.IsFalse("abC".IsUpper());
        }

        [Test]
        public void Left()
        {
            Assert.Throws<ArgumentNullException>(() => "".Left(2));
            Assert.Throws<IndexOutOfRangeException>(() => "abc".Left(4));
            
            Assert.IsTrue("abcdef".Left(2) == "ab");
        }

        [Test]
        public void Right()
        {
            Assert.Throws<ArgumentNullException>(() => "".Right(2));
            Assert.Throws<IndexOutOfRangeException>(() => "abc".Right(4));

            Assert.IsTrue("abcdef".Right(2) == "ef");
        }

        [Test]
        public void Mid()
        {
            Assert.Throws<ArgumentNullException>(() => "".Mid(5, 7));
            Assert.Throws<IndexOutOfRangeException>(() => "abcdefghij".Mid(11, 5));
            Assert.Throws<IndexOutOfRangeException>(() => "abcdefghij".Mid(5, 11));
            Assert.Throws<IndexOutOfRangeException>(() => "abcdefghij".Mid(7, 5));

            Assert.IsTrue("abcdefghij".Mid(4, 5) == "ef");
        }

        [Test]
        public void ContainsAny()
        {
            Assert.IsTrue("abcdefghijk".ContainsAny("def", "xyz"));

            Assert.IsFalse("abcdefghijk".ContainsAny("qrs", "xyz"));
        }

        [Test]
        public void ContainsAll()
        {
            Assert.IsTrue("abcdefghijk".ContainsAll("def", "ghi"));

            Assert.IsFalse("abcdefghijk".ContainsAll("def", "xyz"));
        }
    }
}
