using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpExtensions.Test
{
    /// <summary>
    /// The assert ex.
    /// </summary>
    public static partial class AssertEx
    {
        public static async Task Throws<TException>(Func<Task> func) where TException : Exception
        {
            await Throws<TException>(func, exception => { });
        }

        public static async Task Throws<TException>(Func<Task> func, Action<TException> action) where TException : Exception
        {
            var exception = default(TException);
            var expected = typeof(TException);
            Type actual = null;
            try
            {
                await func();
            }
            catch (Exception e)
            {
                exception = e as TException;
                actual = e.GetType();
            }

            Assert.AreEqual(expected, actual);
            action(exception);
        }

        public static async Task DoesNotThrow(Func<Task> func)
        {
            Exception exception = null;
            try
            {
                await func();
            }
            catch (Exception e)
            {
                exception = e;
            }

            Assert.IsTrue(exception == null);
        }

        public static async Task DoesNotThrow<TException>(Func<Task> func) where TException : Exception
        {
            await DoesNotThrow<TException>(func, exception => { });
        }

        public static async Task DoesNotThrow<TException>(Func<Task> func, Action<TException> action) where TException : Exception
        {
            var exception = default(TException);
            var expected = typeof(TException);
            Type actual = null;
            try
            {
                await func();
            }
            catch (Exception e)
            {
                exception = e as TException;
                actual = e.GetType();
            }

            Assert.AreEqual(expected, actual);
            action(exception);
        }
    }
}
