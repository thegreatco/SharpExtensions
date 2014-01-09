using System;
using System.Diagnostics;
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
            Assert.Throws<NaiveTimeoutException>(async () => await TaskEx.Delay(500).WithTimeout(100));
            Assert.Throws<NaiveTimeoutException>(async () => await TaskEx.Delay(500).WithTimeout(TimeSpan.FromMilliseconds(100)));

            Assert.DoesNotThrow(async () => await TaskEx.Delay(100).WithTimeout(500));
            Assert.DoesNotThrow(async () => await TaskEx.Delay(100).WithTimeout(TimeSpan.FromMilliseconds(500)));
        }

        [Test]
        public void ToVoid()
        {
            var sw = Stopwatch.StartNew();
            Assert.DoesNotThrow(() => TaskEx.Delay(5000).ToVoid());
            sw.Stop();
            Assert.IsTrue(sw.ElapsedMilliseconds < 5000);
        }

        [Test]
        public void WithCancellation()
        {
            // TODO: actually write this test.
        }

        [Test]
        public void IgnoreExceptions()
        {
            Assert.Throws<Exception>(async () => await TaskEx.Run(() =>
                                                                  {
                                                                      throw new Exception();
                                                                  }));
            Assert.DoesNotThrow(async () => await TaskEx.Run(() =>
                                                             {
                                                                 throw new Exception();
                                                             }).IgnoreExceptions());
        }

        [Test]
        public void ToTask()
        {
            // TODO: actually write this test.
        }

        [Test]
        public void AsTask()
        {
            // TODO: actually write this test.
        }
    }
}
