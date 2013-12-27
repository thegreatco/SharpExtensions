using System;
using System.Threading.Tasks;

namespace SharpExtensions
{
    public static partial class TaskExtensions
    {
        /// <summary>
        /// Run a task with a timeout.
        /// </summary>
        /// <param name="task"> The task to run. </param>
        /// <param name="timeout"> The maximum amount of time in milliseconds the task is allowed to run. </param>
        /// <exception cref="TimeoutException"> A timeout exception if the task takes too long to run. </exception>
        /// <returns> A <see cref="Task"/>. </returns>
        public static async Task WithTimeout(this Task task, long timeout)
        {
            await WithTimeout(task, TimeSpan.FromMilliseconds(timeout));
        }

        /// <summary>
        /// Run a task with a timeout.
        /// </summary>
        /// <param name="task"> The task to run. </param>
        /// <param name="timeout"> The maximum amount of time the task is allowed to run. </param>
        /// <exception cref="TimeoutException"> A timeout exception if the task takes too long to run. </exception>
        /// <returns> A <see cref="Task"/>. </returns>
        public static async Task WithTimeout(this Task task, TimeSpan timeout)
        {
            if (task != await Task.WhenAny(task, Task.Delay(timeout)))
                throw new TimeoutException();

            await task;
        }

        /// <summary>
        /// Run a task as void, allowing control to return immediately to the application.
        /// </summary>
        /// <param name="task"> The task to run. </param>
        public static async void ToVoid(this Task task)
        {
            await task;
        }
    }
}
