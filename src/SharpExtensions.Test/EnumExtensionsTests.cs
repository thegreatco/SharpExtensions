using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpExtensions.Test
{
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void EnumToString()
        {
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
