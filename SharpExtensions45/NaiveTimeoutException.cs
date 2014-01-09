using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
