using System;
using NUnit.Framework;
using SharpExtensions;

namespace SharpExtensions45.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void FromUnixTime()
        {
            var dateTime = 100L.FromUnixTime();
            Assert.IsTrue(dateTime == new DateTime(1970, 1, 1, 0, 1, 40, DateTimeKind.Utc));

            dateTime = 100.FromUnixTime();
            Assert.IsTrue(dateTime == new DateTime(1970, 1, 1, 0, 1, 40, DateTimeKind.Utc));
        }
    }
}
