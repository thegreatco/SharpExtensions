using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class ExceptionExtensionsTests
    {
        [Test]
        public void Descrption()
        {
            var str = new StringBuilder()
                .AppendLine("Application failed.")
                .AppendLine("Call of Method0 failed")
                .AppendLine("Arg0 cannot be blank.");

            var argEx = new ArgumentException("Arg0 cannot be blank.");
            var ex = new Exception("Call of Method0 failed", argEx);
            var appEx = new ApplicationException("Application failed.", ex);

            Assert.IsTrue(appEx.SimpleDescription() == str.ToString());
        }
    }
}
