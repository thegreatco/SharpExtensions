using System;

namespace SharpExtensions
{
    /// <summary>
    /// A class for passing errors without the need for a common interface.
    /// </summary>
    public class TaskErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new instance of <see cref="TaskErrorEventArgs"/>.
        /// </summary>
        /// <param name="aggex">The <see cref="Exception"/>.</param>
        public TaskErrorEventArgs(Exception aggex)
        {
            Exception = aggex;
        }

        /// <summary>
        /// Creates a new instance of <see cref="TaskErrorEventArgs"/>.
        /// </summary>
        /// <param name="aggex">The <see cref="Exception"/>.</param>
        /// <param name="caller">The caller of method that threw an <see cref="Exception"/>.</param>
        public TaskErrorEventArgs(Exception aggex, string caller) : this(aggex)
        {
            Caller = caller;
        }

        /// <summary>
        /// The <see cref="Exception"/> that was raised.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// The caller of method that threw an <see cref="Exception"/>.
        /// </summary>
        public string Caller { get; set; }
    }
}
