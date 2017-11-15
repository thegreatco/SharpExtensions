using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpExtensions.Test
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void FromUnixTime()
        {
            var dateTime = 100L.FromUnixTime();
            Assert.IsTrue(dateTime == new DateTime(1970, 1, 1, 0, 1, 40, DateTimeKind.Utc));

            dateTime = 100.FromUnixTime();
            Assert.IsTrue(dateTime == new DateTime(1970, 1, 1, 0, 1, 40, DateTimeKind.Utc));
        }
    }
}
