using System;
using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void EnumToString()
        {
            Assert.Throws<ArgumentNullException>(() => EnumExtensions.GetName(null));
            Assert.IsTrue(TestEnum.Default.GetName() == "Default");
            Assert.IsTrue(TestEnum.A.GetName() == "A");
            Assert.IsTrue(TestEnum.B.GetName() == "B");
            Assert.IsTrue(TestEnum.C.GetName() == "C");

            Assert.IsTrue(TestEnum.Default.GetName(StringCase.Lower) == "default");
            Assert.IsTrue(TestEnum.Default.GetName(StringCase.Upper) == "DEFAULT");
        }

        private enum TestEnum
        {
            Default,
            A,
            B,
            C
        }
    }
}
