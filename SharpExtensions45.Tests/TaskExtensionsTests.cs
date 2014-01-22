using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
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
            var innerException1 = Assert.Throws<AggregateException>(() => TaskEx.Delay(500).WithTimeout(100).Wait()).InnerException;
            Assert.IsTrue(innerException1 is NaiveTimeoutException);
            
            var innerException2 = Assert.Throws<AggregateException>(() => TaskEx.Delay(500).WithTimeout(TimeSpan.FromMilliseconds(100)).Wait()).InnerException;
            Assert.IsTrue(innerException2 is NaiveTimeoutException);

            Assert.DoesNotThrow(() => TaskEx.Delay(100).WithTimeout(500).Wait());
            Assert.DoesNotThrow(() => TaskEx.Delay(100).WithTimeout(TimeSpan.FromMilliseconds(500)).Wait());
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
            var innerException1 = Assert.Throws<AggregateException>(() => TaskEx.Run(() => { throw new NaiveTimeoutException(); }).Wait()).InnerException;
            Assert.IsTrue(innerException1 is NaiveTimeoutException);

            // Make sure errors go to trace if there isn't a handler attached.
            var stream = new MemoryStream();
            Trace.Listeners.Add(new TextWriterTraceListener(stream));

            Assert.DoesNotThrow(() => TaskEx.Run(() => { throw new NaiveTimeoutException(); }).IgnoreExceptions().Wait());
            
            stream.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            var output = new StreamReader(stream).ReadToEnd();
            
            Assert.IsTrue(output.StartsWith("SharpExtensions.NaiveTimeoutException: The operation has timed out."));

            // Attach a handler to capture errors.
            var exceptionHandled = false;
            TaskExtensions.TaskErrorEventHandler += (sender, args) => { exceptionHandled = true; };

            Assert.DoesNotThrow(() => TaskEx.Run(() => { throw new NaiveTimeoutException(); }).IgnoreExceptions().Wait());
            Assert.IsTrue(exceptionHandled);
            exceptionHandled = false;

            var innerException2 = Assert.Throws<AggregateException>(() => TaskEx.Run(new Func<bool>(() => { throw new NaiveTimeoutException(); })).Wait()).InnerException;
            Assert.IsTrue(innerException2 is NaiveTimeoutException);
            Assert.IsFalse(exceptionHandled);

            Assert.DoesNotThrow(() => TaskEx.Run(new Func<bool>(() => { throw new NaiveTimeoutException(); })).IgnoreExceptions().Wait());
            Assert.IsTrue(exceptionHandled);
        }

        [Test]
        public void ObserveExceptions()
        {
            var innerException1 = Assert.Throws<AggregateException>(() => TaskEx.Run(() => { throw new NaiveTimeoutException(); }).Wait()).InnerException;
            Assert.IsTrue(innerException1 is NaiveTimeoutException);

            var mre = new ManualResetEvent(false);
            EventHandler<UnobservedTaskExceptionEventArgs> subscription = (s, args) => mre.Set();
            TaskScheduler.UnobservedTaskException += subscription;
            try
            {
                var res = TaskEx.Run(() =>
                {
                    throw new NaiveTimeoutException();
                });
                ((IAsyncResult)res).AsyncWaitHandle.WaitOne(); // Wait for the task to complete
                res = null; // Allow the task to be GC'ed
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (!mre.WaitOne(10000))
                    Assert.Fail();
            }
            finally
            {
                TaskScheduler.UnobservedTaskException -= subscription;
            }
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
