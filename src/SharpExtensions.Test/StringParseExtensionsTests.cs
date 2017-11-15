using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpExtensions.Test
{
    [TestClass]
    public class StringParseExtensionsTests
    {
        [TestMethod]
        public void ToShort()
        {
            Assert.IsTrue("123".ToShort() == 123);
            Assert.IsTrue("+123".ToShort() == 123);
            Assert.IsTrue("-123".ToShort() == -123);
            Assert.IsTrue("123.4".ToShort() == null);
            Assert.ThrowsException<FormatException>(() => "".ToShort(true));
            Assert.ThrowsException<FormatException>(() => "123.4".ToShort(true));
            Assert.ThrowsException<OverflowException>(() => "32768".ToShort(true));
            Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.ToShort(null, true));
            Assert.IsTrue("123".ToShort().GetType() == typeof(short));
        }

        [TestMethod]
        public void ToUShort()
        {
            Assert.IsTrue("123".ToUShort() == 123);
            Assert.IsTrue("+123".ToUShort() == 123);
            Assert.IsTrue("-123".ToUShort() == null);
            Assert.IsTrue("123.4".ToUShort() == null);
            Assert.ThrowsException<FormatException>(() => "".ToUShort(true));
            Assert.ThrowsException<FormatException>(() => "123.4".ToUShort(true));
            Assert.ThrowsException<OverflowException>(() => "65536".ToUShort(true));
            Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.ToUShort(null, true));
            Assert.IsTrue("123".ToUShort().GetType() == typeof(ushort));
        }

        [TestMethod]
        public void ToInt()
        {
            Assert.IsTrue("123".ToInt() == 123);
            Assert.IsTrue("+123".ToInt() == 123);
            Assert.IsTrue("-123".ToInt() == -123);
            Assert.IsTrue("123.4".ToInt() == null);
            Assert.ThrowsException<FormatException>(() => "".ToInt(true));
            Assert.ThrowsException<FormatException>(() => "123.4".ToInt(true));
            Assert.ThrowsException<OverflowException>(() => "2147483648".ToInt(true));
            Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.ToInt(null, true));
            Assert.IsTrue("123".ToInt().GetType() == typeof(int));
        }

        [TestMethod]
        public void ToUInt()
        {
            Assert.IsTrue("123".ToUInt() == 123);
            Assert.IsTrue("+123".ToUInt() == 123);
            Assert.IsTrue("-123".ToUInt() == null);
            Assert.IsTrue("123.4".ToUInt() == null);
            Assert.ThrowsException<FormatException>(() => "".ToUInt(true));
            Assert.ThrowsException<FormatException>(() => "123.4".ToUInt(true));
            Assert.ThrowsException<OverflowException>(() => "4294967296".ToUInt(true));
            Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.ToUInt(null, true));
            Assert.IsTrue("123".ToUInt().GetType() == typeof(uint));
        }

        [TestMethod]
        public void ToLong()
        {
            Assert.IsTrue("123".ToLong() == 123);
            Assert.IsTrue("+123".ToLong() == 123);
            Assert.IsTrue("-123".ToLong() == -123);
            Assert.IsTrue("123.4".ToLong() == null);
            Assert.ThrowsException<FormatException>(() => "".ToLong(true));
            Assert.ThrowsException<FormatException>(() => "123.4".ToLong(true));
            Assert.ThrowsException<OverflowException>(() => "9223372036854775808".ToLong(true));
            Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.ToLong(null, true));
            Assert.IsTrue("123".ToLong().GetType() == typeof(long));
        }

        [TestMethod]
        public void ToULong()
        {
            Assert.IsTrue("123".ToULong() == 123);
            Assert.IsTrue("+123".ToULong() == 123);
            Assert.IsTrue("-123".ToULong() == null);
            Assert.IsTrue("123.4".ToULong() == null);
            Assert.ThrowsException<FormatException>(() => "".ToULong(true));
            Assert.ThrowsException<FormatException>(() => "123.4".ToULong(true));
            Assert.ThrowsException<OverflowException>(() => "18446744073709551616".ToULong(true));
            Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.ToULong(null, true));
            Assert.IsTrue("123".ToULong().GetType() == typeof(ulong));
        }

        [TestMethod]
        public void ToFloat()
        {
            Assert.IsTrue("123".ToFloat() == 123f);
            Assert.IsTrue("+123".ToFloat() == 123f);
            Assert.IsTrue("-123".ToFloat() == -123f);
            Assert.IsTrue("123.4".ToFloat() == 123.4f);
            Assert.ThrowsException<FormatException>(() => "".ToFloat(true));
            Assert.ThrowsException<OverflowException>(() => "3.402824E+38".ToFloat(true));
            Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.ToFloat(null, true));
            Assert.IsTrue("123".ToFloat().GetType() == typeof(float));
        }

        [TestMethod]
        public void ToDouble()
        {
            Assert.IsTrue("123".ToDouble() == 123d);
            Assert.IsTrue("+123".ToDouble() == 123d);
            Assert.IsTrue("-123".ToDouble() == -123d);
            Assert.IsTrue("123.4".ToDouble() == 123.4d);
            Assert.ThrowsException<FormatException>(() => "".ToDouble(true));
            Assert.ThrowsException<OverflowException>(() => "1.79769313486233E+308".ToDouble(true));
            Assert.ThrowsException<ArgumentNullException>(() => StringExtensions.ToDouble(null, true));
            Assert.IsTrue("123".ToDouble().GetType() == typeof(double));
        }
    }
}
