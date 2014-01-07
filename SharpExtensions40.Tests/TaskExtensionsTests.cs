using System;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpExtensions;

namespace SharpExtensions40.Tests
{
    [TestFixture]
    public class TaskExtensionsTests
    {
        [Test]
        public void ToVoid()
        {
            Assert.DoesNotThrow(() => Task.Delay(5000).ToVoid());
        }
    }
}
