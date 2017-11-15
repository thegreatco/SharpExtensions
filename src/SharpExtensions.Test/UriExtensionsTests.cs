﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpExtensions.Test
{
    [TestClass]
    public class UriExtensionsTests
    {
        [TestMethod]
        public void ToUri()
        {
            const string testUriString = "http://www.test.com/{0}/{1}";
            Assert.ThrowsException<ArgumentNullException>(() => "".ToUri("abc"));
            Assert.IsTrue(testUriString.ToUri("abc", "def") == new UriBuilder(testUriString.With("abc", "def")).Uri);

            var testObj = new TestObject();
            var expectedUri = new UriBuilder(testUriString.With("abc", "def")) { Query = "stuff=foobar" };
            Assert.ThrowsException<ArgumentNullException>(() => "".ToUri(testObj, null));
            Assert.ThrowsException<ArgumentNullException>(() => testUriString.ToUri((IUrlFormatable)null));
            Assert.IsTrue(testUriString.ToUri(testObj, "abc", "def") == expectedUri.Uri);
        }

        [TestMethod]
        public void With()
        {
            const string testUriString = "http://www.test.com/";
            var testUri = new Uri(testUriString).With("abc", "def");

            Assert.IsTrue(testUri == (new UriBuilder(testUriString) { Query = "abc=def" }).Uri);

            testUri = new Uri(testUriString).With(new KeyValuePair<string, object>("abc", "def"));
            Assert.IsTrue(testUri == (new UriBuilder(testUriString) { Query = "abc=def" }).Uri);

            testUri = new Uri(testUriString).With(new[] { new KeyValuePair<string, object>("abc", "def"), new KeyValuePair<string, object>("ghi", "jkl") });
            Assert.IsTrue(testUri == (new UriBuilder(testUriString) { Query = "abc=def&ghi=jkl" }).Uri);
        }

        [TestMethod]
        public void Remove()
        {
            const string testUriString = "http://www.test.com/?abc=def&ghi=jkl";
            var testUri = new Uri(testUriString);

            Assert.IsTrue(testUri.Remove("abc", "def") == new Uri("http://www.test.com/?ghi=jkl"));

            Assert.IsTrue(testUri.Remove("abc", (object)"def") == new Uri("http://www.test.com/?ghi=jkl"));

            Assert.IsTrue(testUri.Remove(new KeyValuePair<string, object>("abc", "def")) == new Uri("http://www.test.com/?ghi=jkl"));

            Assert.IsTrue(testUri.Remove(new[] { new KeyValuePair<string, object>("abc", "def"), new KeyValuePair<string, object>("ghi", "jkl") }) == new Uri("http://www.test.com/"));
        }

        private class TestObject : IUrlFormatable
        {
            public string MultiWordDelimeter()
            {
                return "_";
            }

            public bool SplitOnCamelCase()
            {
                return true;
            }

            public string Stuff { get { return "foobar";  } }
        }
    }
}
