using System.Threading.Tasks;
using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class TaskExtensionsTests
    {
        [Test]
        public void ToVoid()
        {
            Assert.DoesNotThrow(() => Task.Delay(5000).ToVoid());
        }
    }
}
