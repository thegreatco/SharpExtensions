using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SharpExtensions
{
    /// <summary>
    /// Implementation of TaskEx for .NET45, simply a wrapper around the built in Task methods to allow sharing of source files between .NET40 and .NET45
    /// These methods shouldn't be called from outside SharpExtensions.
    /// </summary>
    public static partial class TaskEx
    {
        /// <summary>
        /// Creates a Task that will complete when any of the tasks in the provided collection completes.
        /// </summary>
        /// <param name="tasks">The Tasks to be monitored.</param>
        /// <returns>A Task that represents the completion of any of the provided Tasks.</returns>
        public static Task<Task> WhenAny(IEnumerable<Task> tasks)
        {
            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// Creates a Task that will complete when any of the tasks in the provided collection completes.
        /// </summary>
        /// <param name="tasks">The Tasks to be monitored.</param>
        /// <returns>A Task that represents the completion of any of the provided Tasks.</returns>
        public static Task<Task> WhenAny(params Task[] tasks)
        {
            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// Creates a Task that will complete when any of the tasks in the provided collection completes.
        /// </summary>
        /// <param name="tasks">The Tasks to be monitored.</param>
        /// <returns>A Task that represents the completion of any of the provided Tasks. The completed Task is this Task's result.</returns>
        public static Task<Task<T>> WhenAny<T>(IEnumerable<Task<T>> tasks)
        {
            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// Creates a Task that will complete when any of the tasks in the provided collection completes.
        /// </summary>
        /// <param name="tasks">The Tasks to be monitored.</param>
        /// <returns>A Task that represents the completion of any of the provided Tasks. The completed Task is this Task's result.</returns>
        public static Task<Task<T>> WhenAny<T>(params Task<T>[] tasks)
        {
            return Task.WhenAny(tasks);
        }

        /// <summary>
        /// Starts a Task that will complete after the specified due time.
        /// </summary>
        /// <param name="timeout">The delay in milliseconds before the returned task completes.</param>
        /// <returns>The timed Task.</returns>
        public static async Task Delay(TimeSpan timeout)
        {
            await Task.Delay(timeout);
        }

        /// <summary>
        /// Starts a Task that will complete after the specified due time.
        /// </summary>
        /// <param name="timeout">The delay in milliseconds before the returned task completes.</param>
        /// <returns>The timed Task.</returns>
        public static async Task Delay(int timeout)
        {
            await Task.Delay(timeout);
        }

        /// <summary>
        /// Starts a Task that will complete after the specified due time.
        /// </summary>
        /// <param name="timeout">The delay in milliseconds before the returned task completes.</param>
        /// <param name="cancellationToken">The cancellation token for the delay task.</param>
        /// <returns>The timed Task.</returns>
        public static async Task Delay(TimeSpan timeout, CancellationToken cancellationToken)
        {
            await Task.Delay(timeout, cancellationToken);
        }

        /// <summary>
        /// Starts a Task that will complete after the specified due time.
        /// </summary>
        /// <param name="timeout">The delay in milliseconds before the returned task completes.</param>
        /// <param name="cancellationToken">The cancellation token for the delay task.</param>
        /// <returns>The timed Task.</returns>
        public static async Task Delay(int timeout, CancellationToken cancellationToken)
        {
            await Task.Delay(timeout, cancellationToken);
        }

        /// <summary>
        /// Creates a task that runs the specified action.
        /// </summary>
        /// <param name="action">The action to execute asynchronously.</param>
        /// <returns>A task that represents the completion of the action.</returns>
        public static async Task Run(Action action)
        {
            await Run(action, CancellationToken.None);
        }

        /// <summary>
        /// Creates a task that runs the specified action.
        /// </summary>
        /// <param name="action">The action to execute asynchronously.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> for the <see cref="Task"/>.</param>
        /// <returns>A task that represents the completion of the action.</returns>
        public static async Task Run(Action action, CancellationToken cancellationToken)
        {
            await Task.Run(action, cancellationToken);
        }
    }
}
