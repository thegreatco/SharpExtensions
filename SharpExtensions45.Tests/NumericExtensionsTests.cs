using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class NumericExtensionsTests
    {
        [Test]
        public void ToShort()
        {
            Assert.IsTrue("123".ToShort() == 123);
            Assert.IsTrue("+123".ToShort() == 123);
            Assert.IsTrue("-123".ToShort() == -123);
            Assert.IsTrue("123.4".ToShort() == null);
            Assert.Throws<FormatException>(() => "".ToShort(true));
            Assert.Throws<FormatException>(() => "123.4".ToShort(true));
            Assert.Throws<OverflowException>(() => "32768".ToShort(true));
            Assert.Throws<ArgumentNullException>(() => NumericExtensions.ToShort(null, true));
            Assert.IsTrue("123".ToShort().GetType() == typeof(short));
        }

        [Test]
        public void ToUShort()
        {
            Assert.IsTrue("123".ToUShort() == 123);
            Assert.IsTrue("+123".ToUShort() == 123);
            Assert.IsTrue("-123".ToUShort() == null);
            Assert.IsTrue("123.4".ToUShort() == null);
            Assert.Throws<FormatException>(() => "".ToUShort(true));
            Assert.Throws<FormatException>(() => "123.4".ToUShort(true));
            Assert.Throws<OverflowException>(() => "65536".ToUShort(true));
            Assert.Throws<ArgumentNullException>(() => NumericExtensions.ToUShort(null, true));
            Assert.IsTrue("123".ToUShort().GetType() == typeof(ushort));
        }

        [Test]
        public void ToInt()
        {
            Assert.IsTrue("123".ToInt() == 123);
            Assert.IsTrue("+123".ToInt() == 123);
            Assert.IsTrue("-123".ToInt() == -123);
            Assert.IsTrue("123.4".ToInt() == null);
            Assert.Throws<FormatException>(() => "".ToInt(true));
            Assert.Throws<FormatException>(() => "123.4".ToInt(true));
            Assert.Throws<OverflowException>(() => "2147483648".ToInt(true));
            Assert.Throws<ArgumentNullException>(() => NumericExtensions.ToInt(null, true));
            Assert.IsTrue("123".ToInt().GetType() == typeof(int));
        }

        [Test]
        public void ToUInt()
        {
            Assert.IsTrue("123".ToUInt() == 123);
            Assert.IsTrue("+123".ToUInt() == 123);
            Assert.IsTrue("-123".ToUInt() == null);
            Assert.IsTrue("123.4".ToUInt() == null);
            Assert.Throws<FormatException>(() => "".ToUInt(true));
            Assert.Throws<FormatException>(() => "123.4".ToUInt(true));
            Assert.Throws<OverflowException>(() => "4294967296".ToUInt(true));
            Assert.Throws<ArgumentNullException>(() => NumericExtensions.ToUInt(null, true));
            Assert.IsTrue("123".ToUInt().GetType() == typeof(uint));
        }

        [Test]
        public void ToLong()
        {
            Assert.IsTrue("123".ToLong() == 123);
            Assert.IsTrue("+123".ToLong() == 123);
            Assert.IsTrue("-123".ToLong() == -123);
            Assert.IsTrue("123.4".ToLong() == null);
            Assert.Throws<FormatException>(() => "".ToLong(true));
            Assert.Throws<FormatException>(() => "123.4".ToLong(true));
            Assert.Throws<OverflowException>(() => "9223372036854775808".ToLong(true));
            Assert.Throws<ArgumentNullException>(() => NumericExtensions.ToLong(null, true));
            Assert.IsTrue("123".ToLong().GetType() == typeof(long));
        }

        [Test]
        public void ToULong()
        {
            Assert.IsTrue("123".ToULong() == 123);
            Assert.IsTrue("+123".ToULong() == 123);
            Assert.IsTrue("-123".ToULong() == null);
            Assert.IsTrue("123.4".ToULong() == null);
            Assert.Throws<FormatException>(() => "".ToULong(true));
            Assert.Throws<FormatException>(() => "123.4".ToULong(true));
            Assert.Throws<OverflowException>(() => "18446744073709551616".ToULong(true));
            Assert.Throws<ArgumentNullException>(() => NumericExtensions.ToULong(null, true));
            Assert.IsTrue("123".ToULong().GetType() == typeof(ulong));
        }

        [Test]
        public void ToFloat()
        {
            Assert.IsTrue("123".ToFloat() == 123f);
            Assert.IsTrue("+123".ToFloat() == 123f);
            Assert.IsTrue("-123".ToFloat() == -123f);
            Assert.IsTrue("123.4".ToFloat() == 123.4f);
            Assert.Throws<FormatException>(() => "".ToFloat(true));
            Assert.Throws<OverflowException>(() => "3.402824E+38".ToFloat(true));
            Assert.Throws<ArgumentNullException>(() => NumericExtensions.ToFloat(null, true));
            Assert.IsTrue("123".ToFloat().GetType() == typeof(float));
        }

        [Test]
        public void ToDouble()
        {
            Assert.IsTrue("123".ToDouble() == 123d);
            Assert.IsTrue("+123".ToDouble() == 123d);
            Assert.IsTrue("-123".ToDouble() == -123d);
            Assert.IsTrue("123.4".ToDouble() == 123.4d);
            Assert.Throws<FormatException>(() => "".ToDouble(true));
            Assert.Throws<OverflowException>(() => "1.79769313486233E+308".ToDouble(true));
            Assert.Throws<ArgumentNullException>(() => NumericExtensions.ToDouble(null, true));
            Assert.IsTrue("123".ToDouble().GetType() == typeof(double));
        }
    }
}
