using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpExtensions.Test
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void With()
        {
            Assert.IsTrue("{0}".With("abc") == "abc");

            Assert.IsTrue("{0}".With("abc", "def") == "abc");

            Assert.IsTrue("{0} {1}".With("abc", "def") == "abc def");
        }

        [TestMethod]
        public void Join()
        {
            var arr = new[] { "abc", "def" };
            Assert.IsTrue(arr.Join(",") == "abc,def");
        }

        [TestMethod]
        public void Split()
        {
            const string str1 = "abc,def,,ghi";
            Assert.IsTrue(str1.Split(",").ContainsAll("abc","def","ghi",""));
            Assert.IsTrue(str1.Split(StringSplitOptions.RemoveEmptyEntries, ",").ContainsOnly("abc", "def", "ghi"));

            const string str2 = "abc,def;ghi,jkl;";
            Assert.IsTrue(str2.Split(",", ";").ContainsAll("abc", "def", "ghi", "jkl", ""));
            Assert.IsTrue(str2.Split(StringSplitOptions.RemoveEmptyEntries, ",", ";").ContainsOnly("abc", "def", "ghi", "jkl"));

            Assert.IsTrue(str2.Split(',', ';').ContainsAll("abc", "def", "ghi", "jkl", ""));
            Assert.IsTrue(str2.Split(StringSplitOptions.RemoveEmptyEntries, ',', ';').ContainsOnly("abc", "def", "ghi", "jkl"));

            const string str3 = "abc,def,,ghi,jkl";
            Assert.IsTrue(str3.Split(',', StringSplitOptions.RemoveEmptyEntries).ContainsOnly("abc", "def", "ghi", "jkl"));
            Assert.IsTrue(str3.Split(',', StringSplitOptions.None).ContainsOnly("abc", "def", string.Empty, "ghi", "jkl"));

            Assert.IsTrue(str3.Split(",", StringSplitOptions.RemoveEmptyEntries).ContainsOnly("abc", "def", "ghi", "jkl"));
            Assert.IsTrue(str3.Split(",", StringSplitOptions.None).ContainsOnly("abc", "def", string.Empty, "ghi", "jkl"));
        }

        [TestMethod]
        public void IsNumeric()
        {
            Assert.ThrowsException<ArgumentNullException>(() => { var res = string.Empty.IsNumeric(); });
            Assert.IsTrue("123".IsNumeric());

            Assert.IsTrue("12.3".IsNumeric());

            Assert.IsFalse("123d".IsNumeric());
        }

        [TestMethod]
        public void CleanForUrl()
        {
            var arr = new[] { "!", "*", "'", "(", ")", ";", ":", "@", "&", "=", "+", "$", ",", "/", "?", "%", "#", "[", "]" };
            var str = new string(Enumerable.Range(0, 128).Select(x => (char) x).ToArray());

            Assert.IsFalse(str.CleanForUrl().ContainsAny(arr));
        }

        [TestMethod]
        public void ValidatedJoin()
        {
            var arr = new[] { "abc", string.Empty, "", " ", "stuff" };

            Assert.IsTrue(arr.ValidatedJoin(",") == "abc,stuff");
        }

        [TestMethod]
        public void IsLower()
        {
            Assert.IsTrue("abc".IsLower());
            
            Assert.IsFalse("Abc".IsLower());

            Assert.IsFalse("aBc".IsLower());

            Assert.IsFalse("abC".IsLower());
        }

        [TestMethod]
        public void IsUpper()
        {
            Assert.IsTrue("ABC".IsUpper());

            Assert.IsFalse("Abc".IsUpper());

            Assert.IsFalse("aBc".IsUpper());

            Assert.IsFalse("abC".IsUpper());
        }

        [TestMethod]
        public void Left()
        {
            Assert.ThrowsException<ArgumentNullException>(() => "".Left(2, true));
            Assert.ThrowsException<IndexOutOfRangeException>(() => "abc".Left(4, true));
            
            Assert.IsTrue("abc".Left(4) == "abc");
            Assert.IsTrue("abcdef".Left(2) == "ab");
        }

        [TestMethod]
        public void Right()
        {
            Assert.ThrowsException<ArgumentNullException>(() => "".Right(2, true));
            Assert.ThrowsException<IndexOutOfRangeException>(() => "abc".Right(4, true));

            Assert.IsTrue("abc".Right(4) == "abc");
            Assert.IsTrue("abcdef".Right(2) == "ef");
        }

        [TestMethod]
        public void Mid()
        {
            Assert.ThrowsException<ArgumentNullException>(() => "".Mid(5, 7));
            Assert.ThrowsException<IndexOutOfRangeException>(() => "abcdefghij".Mid(11, 5));
            Assert.ThrowsException<IndexOutOfRangeException>(() => "abcdefghij".Mid(5, 11));
            Assert.ThrowsException<IndexOutOfRangeException>(() => "abcdefghij".Mid(7, 5));

            Assert.IsTrue("abcdefghij".Mid(4, 5) == "ef");
        }

        [TestMethod]
        public void ContainsAny()
        {
            Assert.IsTrue("abcdefghijk".ContainsAny("def", "xyz"));

            Assert.IsFalse("abcdefghijk".ContainsAny("qrs", "xyz"));
        }

        [TestMethod]
        public void ContainsAll()
        {
            Assert.IsTrue("abcdefghijk".ContainsAll("def", "ghi"));

            Assert.IsFalse("abcdefghijk".ContainsAll("def", "xyz"));
        }
    }
}
