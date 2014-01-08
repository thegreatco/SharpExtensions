using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class TaskExtensionsTests
    {
        [Test]
        public void WithTimeout()
        {
            var task = TaskEx.Delay(500);
            Assert.Throws<TimeoutException>(async () => await task.WithTimeout(100));
            Assert.Throws<TimeoutException>(async () => await task.WithTimeout(TimeSpan.FromMilliseconds(100)));

            Assert.DoesNotThrow(async () => await TaskEx.Delay(100).WithTimeout(500));
            Assert.DoesNotThrow(async () => await TaskEx.Delay(100).WithTimeout(TimeSpan.FromMilliseconds(500)));
        }

        [Test]
        public void ToVoid()
        {
            Assert.DoesNotThrow(() => TaskEx.Delay(5000).ToVoid());
        }
    }
}
