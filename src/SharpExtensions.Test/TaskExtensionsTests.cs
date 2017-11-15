using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpExtensions.Test
{
    [TestClass]
    public class TaskExtensionsTests
    {
        [TestMethod]
        public void WithTimeout()
        {
            var innerException1 = Assert.ThrowsException<AggregateException>(() => Task.Delay(500).WithTimeout(100).Wait()).InnerException;
            Assert.IsTrue(innerException1 is TimeoutException, $"innerException1 is NativeTimeoutException failed, got {innerException1.GetType()}");
            
            var innerException2 = Assert.ThrowsException<AggregateException>(() => Task.Delay(500).WithTimeout(TimeSpan.FromMilliseconds(100)).Wait()).InnerException;
            Assert.IsTrue(innerException2 is TimeoutException);

            Task.Delay(100).WithTimeout(500).Wait();
            Task.Delay(100).WithTimeout(TimeSpan.FromMilliseconds(500)).Wait();
        }

        [TestMethod]
        public void ToVoid()
        {
            var sw = Stopwatch.StartNew();
            Task.Delay(5000).ToVoid();
            sw.Stop();
            Assert.IsTrue(sw.ElapsedMilliseconds < 5000);
        }

        [TestMethod]
        public void WithCancellation()
        {
            // TODO: actually write this test.
        }

        [TestMethod]
        public async Task IgnoreExceptions()
        {
            await AssertEx.Throws<NativeTimeoutException>(async () => await Task.Run(() => throw new NativeTimeoutException()));

            // Make sure errors go to trace if there isn't a handler attached.
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new TextWriterTraceListener(writer));
            
            await AssertEx.DoesNotThrow(async () => await Task.Run(() => throw new NativeTimeoutException()).IgnoreExceptions());
            
            writer.Flush();
            stream.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            var output = new StreamReader(stream).ReadToEnd();

            Assert.IsTrue(output.StartsWith("SharpExtensions.NativeTimeoutException: The operation has timed out."), $"Expected a NativeTimeoutException, got: {output}");

            // Attach a handler to capture errors.

            await AssertEx.DoesNotThrow(async () => await Task.Run(() => throw new NativeTimeoutException()).IgnoreExceptions());

            await AssertEx.Throws<NativeTimeoutException>(async () => await Task.Run(() => throw new NativeTimeoutException()));

            await AssertEx.DoesNotThrow(async () => await Task.Run(() => throw new NativeTimeoutException()).IgnoreExceptions());
        }
        [TestMethod]
        public void ObserveExceptions()
        {
            void Handler(object sender, UnhandledExceptionEventArgs args)
            {
                Assert.Fail("An exception was unhandled.");
            }

            AppDomain.CurrentDomain.UnhandledException += Handler;

            AssertEx.Throws<NativeTimeoutException>(async () => await Task.Run(() => throw new NativeTimeoutException())).Wait();
            
            try
            {
                //var res = Task.FromException(new NativeTimeoutException());
                var res = Task.Run(() => throw new NativeTimeoutException());
                ((IAsyncResult)res).AsyncWaitHandle.WaitOne(); // Wait for the task to complete
                res = null; // Allow the task to be GC'ed
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            finally
            {
                AppDomain.CurrentDomain.UnhandledException -= Handler;
            }
        }

        [TestMethod]
        public void ToTask()
        {
            // TODO: actually write this test.
        }

        [TestMethod]
        public void AsTask()
        {
            // TODO: actually write this test.
        }
    }
}
