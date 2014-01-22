using System;

namespace SharpExtensions
{
    public class NaiveTimeoutException : TimeoutException
    {
        public NaiveTimeoutException()
        {
        }

        public NaiveTimeoutException(string message)
            : base(message)
        {
        }

        public NaiveTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
