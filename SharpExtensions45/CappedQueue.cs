using System.Collections;
using System.Collections.Generic;

namespace SharpExtensions
{
    /// <summary>
    /// A collection that has a defined size that will drop the oldest item when the size is exceeded.
    /// </summary>
    /// <typeparam name="T">The type of the object being stored.</typeparam>
    public class CappedQueue<T> : IEnumerable<T>
    {
        private readonly Queue<T> _queue;
 
        /// <summary>
        /// The capacity of the CappedQueue.
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// Gets the number of items in the CappedQueue.
        /// </summary>
        public int Count { get { return _queue.Count; } }

        /// <summary>
        /// Create a new instance of a CappedQueue.
        /// </summary>
        /// <param name="capacity">The size of the CappedQueue.</param>
        public CappedQueue(int capacity)
        {
            Capacity = capacity;
            _queue = new Queue<T>(capacity);
        }

        /// <summary>
        /// Enqueue an item into the collection.
        /// </summary>
        /// <param name="item">The item to enqueue.</param>
        public void Enqueue(T item)
        {
            if (_queue.Count == Capacity) _queue.Dequeue();
            _queue.Enqueue(item);
        }

        /// <summary>
        /// Dequeue an item into the collection.
        /// </summary>
        public T Dequeue()
        {
            return _queue.Dequeue();
        }

        /// <summary>
        /// Clear the CappedQueue.
        /// </summary>
        public void Clear()
        {
            _queue.Clear();
        }

        /// <summary>
        /// Determine if the specified item exists in the collection.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns>If the item exists.</returns>
        public bool Contains(T item)
        {
            return _queue.Contains(item);
        }

        /// <summary>
        /// Copy the CappedQueue to the specified array.
        /// </summary>
        /// <param name="array">The array to which to copy the CappedQueue.</param>
        /// <param name="arrayIndex">The starting index to which to copy the CappedQueue.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _queue.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _queue).GetEnumerator();
        }
    }
}
