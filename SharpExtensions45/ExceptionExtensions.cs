using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpExtensions
{
    public static partial class ExceptionExtensions
    {
        /// <summary>
        /// Get a simple description of an <see cref="Exception"/> based on the <see cref="Exception.Message"/>.
        /// Each line represents an exception, with the lowest line being the innermost exception.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/></param>
        /// <returns>A <see cref="string"/>.</returns>
        public static string SimpleDescription(this Exception ex)
        {
            var str = new StringBuilder();
            while (ex != null)
            {
                str.AppendLine(ex.Message);
                ex = ex.InnerException;
            }

            return str.ToString();
        }
    }
}
