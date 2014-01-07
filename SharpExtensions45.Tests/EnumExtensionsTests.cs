using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void EnumToString()
        {
            Assert.IsTrue(TestEnum.Default.EnumToString() == "Default");
            Assert.IsTrue(TestEnum.A.EnumToString() == "A");
            Assert.IsTrue(TestEnum.B.EnumToString() == "B");
            Assert.IsTrue(TestEnum.C.EnumToString() == "C");

            Assert.IsTrue(TestEnum.Default.EnumToString(StringCase.Lower) == "default");
            Assert.IsTrue(TestEnum.Default.EnumToString(StringCase.Upper) == "DEFAULT");
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
