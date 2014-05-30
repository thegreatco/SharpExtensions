using System;
using System.Linq;
using NUnit.Framework;

namespace SharpExtensions.Tests
{
    [TestFixture]
    public class CappedQueueTests
    {
        [Test]
        public void Test()
        {
            var collection = new CappedQueue<int>(10);
            for (var i = 0; i < 5; i++)
            {
                collection.Enqueue(i);
            }

            Assert.IsTrue(collection.Capacity == 10);
            Assert.IsTrue(collection.Count == 5);

            for (var i = 5; i < 10; i++)
            {
                collection.Enqueue(i);
            }

            Assert.IsTrue(collection.ContainsAll(Enumerable.Range(0, 10)));

            Console.WriteLine(collection.Join(","));

            collection.Enqueue(10);
            Assert.IsTrue(collection.ContainsAll(Enumerable.Range(1, 10)));
            
            Console.WriteLine(collection.Join(","));

            Assert.IsTrue(collection.Dequeue() == 1);
            Assert.IsTrue(collection.Count == 9);
            Assert.IsTrue(collection.ContainsAll(Enumerable.Range(2, 9)));

            collection.Clear();
            Assert.IsTrue(collection.Capacity == 10);
            Assert.IsTrue(collection.Count == 0);

            Console.WriteLine(collection.Join(","));
        }
    }
}
