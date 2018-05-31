using System;
using System.Threading.Tasks;

namespace SharpExtensions
{
    /// <summary>
    /// An exception specificially for when a task is executed using <see cref="TaskExtensions"/>
    /// </summary>
    public class NativeTimeoutException : TimeoutException
    {
        /// <summary>
        /// Creates a new instance of <see cref="NativeTimeoutException"/>
        /// </summary>
        public NativeTimeoutException()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="NativeTimeoutException"/>
        /// </summary>
        /// <param name="message">A custom, informative message about the exception</param>
        public NativeTimeoutException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="NativeTimeoutException"/>
        /// </summary>
        /// <param name="message">A custom, informative message about the exception</param>
        /// <param name="innerException">The inner exception that cause the throwing of <see cref="NativeTimeoutException"/></param>
        public NativeTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
