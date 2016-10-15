﻿using System;
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
            Assert.IsTrue(innerException1 is NativeTimeoutException);
            
            var innerException2 = Assert.Throws<AggregateException>(() => TaskEx.Delay(500).WithTimeout(TimeSpan.FromMilliseconds(100)).Wait()).InnerException;
            Assert.IsTrue(innerException2 is NativeTimeoutException);

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
        public async void IgnoreExceptions()
        {
            await AssertEx.Throws<NativeTimeoutException>(async () => await TaskEx.Run(() => { throw new NativeTimeoutException(); }));

            // Make sure errors go to trace if there isn't a handler attached.
            var stream = new MemoryStream();
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new TextWriterTraceListener(stream));

            await AssertEx.DoesNotThrow(async () => await TaskEx.Run(() => { throw new NativeTimeoutException(); }).IgnoreExceptions());
            
            stream.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            var output = new StreamReader(stream).ReadToEnd();

            Assert.IsTrue(output.StartsWith("SharpExtensions.NativeTimeoutException: The operation has timed out."));

            // Attach a handler to capture errors.
            var exceptionHandled = false;
            TaskExtensions.TaskErrorEventHandler += (sender, args) => { exceptionHandled = true; };

            await AssertEx.DoesNotThrow(async () => await TaskEx.Run(() => { throw new NativeTimeoutException(); }).IgnoreExceptions());
            Assert.IsTrue(exceptionHandled);
            exceptionHandled = false;

            await AssertEx.Throws<NativeTimeoutException>(async () => await TaskEx.Run(new Func<bool>(() => { throw new NativeTimeoutException(); })));
            Assert.IsFalse(exceptionHandled);

            await AssertEx.DoesNotThrow(async () => await TaskEx.Run(new Func<bool>(() => { throw new NativeTimeoutException(); })).IgnoreExceptions());
            Assert.IsTrue(exceptionHandled);
        }

        [Test]
        public void ObserveExceptions()
        {
            AssertEx.Throws<NativeTimeoutException>(async () => await TaskEx.Run(() => { throw new NativeTimeoutException(); })).Wait();

            var mre = new ManualResetEvent(false);
            EventHandler<UnobservedTaskExceptionEventArgs> subscription = (s, args) => mre.Set();
            TaskScheduler.UnobservedTaskException += subscription;
            try
            {
                var res = TaskEx.Run(() => { throw new NativeTimeoutException(); });
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
