using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SharpExtensions
{
    public static partial class TaskExtensions
    {
        public static EventHandler<TaskErrorEventArgs> TaskErrorEventHandler;

        private static readonly TaskCompletionSource<object> NeverCompleteSource = new TaskCompletionSource<object>();

        public static Task NeverComplete { get { return NeverCompleteSource.Task; } }

        /// <summary>
        /// Run a task with a timeout.
        /// </summary>
        /// <param name="task">The task to run.</param>
        /// <param name="timeout">The maximum amount of time the task is allowed to run.</param>
        /// <exception cref="TimeoutException">A timeout exception if the task takes too long to run.</exception>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task WithTimeout(this Task task, TimeSpan timeout)
        {
            if (task != await TaskEx.WhenAny(task, TaskEx.Delay(timeout)))
                throw new NaiveTimeoutException();

            await task;
        }

        /// <summary>
        /// Run a task with a timeout.
        /// </summary>
        /// <param name="task">The task to run.</param>
        /// <param name="timeout">The maximum amount of time in milliseconds the task is allowed to run.</param>
        /// <exception cref="TimeoutException">A timeout exception if the task takes too long to run. </exception>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task WithTimeout(this Task task, int timeout)
        {
            await WithTimeout(task, TimeSpan.FromMilliseconds(timeout));
        }

        /// <summary>
        /// Run a task with a timeout, execute the supplied action if a timeout occurs.
        /// </summary>
        /// <param name="task">The task to run.</param>
        /// <param name="timeout">The maximum amount of time the task is allowed to run.</param>
        /// <param name="action">The action to execute if the task times out.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task WithTimeout(this Task task, TimeSpan timeout, Action action)
        {
            if (task != await TaskEx.WhenAny(task, TaskEx.Delay(timeout)))
                action.Invoke();

            await task;
        }

        /// <summary>
        /// Run a task with a timeout, execute the supplied action if a timeout occurs.
        /// </summary>
        /// <param name="task">The task to run.</param>
        /// <param name="timeout">The maximum amount of time in milliseconds the task is allowed to run.</param>
        /// <param name="action">The action to execute if the task times out.</param>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task WithTimeout(this Task task, int timeout, Action action)
        {
            await WithTimeout(task, TimeSpan.FromMilliseconds(timeout), action);
        }

        /// <summary>
        /// A naive implementation of timeout and cancellation over an uncancelable <see cref="Task"/>.
        /// </summary>
        /// <typeparam name="T">The result type of the task</typeparam>
        /// <param name="task">the uncancelable task</param>
        /// <param name="token">token to monitor for cancellation</param>
        /// <returns>The <see cref="Task"/> with a <see cref="CancellationToken"/>.</returns>
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken token)
        {
#pragma warning disable 4014
            task.IgnoreExceptions();

            var cancelTask = token.ToTask();

            cancelTask.IgnoreExceptions();
#pragma warning restore 4014

            await TaskEx.WhenAny(task, cancelTask);
            return task.Result;
        }

        /// <summary>
        /// A naive implementation of timeout and cancellation over an uncancelable <see cref="Task"/>.
        /// </summary>
        /// <param name="task">the uncancelable task</param>
        /// <param name="token">token to monitor for cancellation</param>
        /// <returns>The <see cref="Task"/> with a <see cref="CancellationToken"/>.</returns>
        public static async Task WithCancellation(this Task task, CancellationToken token)
        {
#pragma warning disable 4014
            task.IgnoreExceptions();

            var cancelTask = token.ToTask();

            cancelTask.IgnoreExceptions();
#pragma warning restore 4014

            await TaskEx.WhenAny(task, cancelTask);
        }
        
        /// <summary>
        /// Run a task as void, allowing control to return immediately to the application.
        /// </summary>
        /// <param name="caller">The caller of the method.</param>
        /// <param name="task">The task to run.</param>
        public static async void ToVoid(this Task task, string caller = null)
        {
            try
            {
                await task;
            }
            catch (AggregateException aggex)
            {
                if (TaskErrorEventHandler != null)
                    TaskErrorEventHandler(null, new TaskErrorEventArgs(aggex, caller));
            }
        }

        /// <summary>
        /// Executes the <see cref="Task"/> ignoring all thrown exceptions during execution.
        /// </summary>
        /// <param name="task">The <see cref="Task"/> to run.</param>
        /// <param name="caller">The caller of the method.</param>
        /// <returns>A <see cref="Task"/> that ignores all thrown execeptions during execution.</returns>
        public static async Task IgnoreExceptions(this Task task, string caller = null)
        {
            await task.ContinueWith(t =>
                                    {
                                        if (t.Exception != null)
                                            t.Exception.Handle(ex =>
                                                               {
                                                                   if (TaskErrorEventHandler != null) TaskErrorEventHandler(null, new TaskErrorEventArgs(ex, caller));
                                                                   else Trace.WriteLine(ex);
                                                                   return true;
                                                               });

                                    });
        }

        /// <summary>
        /// Executes the <see cref="Task"/> ignoring all thrown exceptions during execution.
        /// </summary>
        /// <typeparam name="T">The return type of the task.</typeparam>
        /// <param name="task">The <see cref="Task"/> to run.</param>
        /// <param name="caller">The caller of the method.</param>
        /// <returns>A <see cref="Task"/> that ignores all thrown execeptions during execution.</returns>
        public static async Task<T> IgnoreExceptions<T>(this Task<T> task, string caller = null)
        {
            return await task.ContinueWith(t =>
                                              {
                                                  if (t.Exception != null)
                                                  {
                                                      t.Exception.Handle(ex =>
                                                                         {
                                                                             if (TaskErrorEventHandler != null) TaskErrorEventHandler(null, new TaskErrorEventArgs(ex, caller));
                                                                             else Trace.WriteLine(ex);
                                                                             return true;
                                                                         });
                                                      return default(T);
                                                  }
                                                  return t.Result;
                                              });
        }

        /// <summary>
        /// Observe any unobserved exceptions to prevent crashing of entire runtime.
        /// </summary>
        /// <param name="task">The task from which to observe the exceptions.</param>
        /// <returns>The original task with the exception observation enabled.</returns>
        public static Task ObserveExceptions(this Task task)
        {
            task.ContinueWith(t => { var ignored = t.Exception; }, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);
            return task;
        }

        /// <summary>
        /// Observe any unobserved exceptions to prevent crashing of entire runtime.
        /// </summary>
        /// <typeparam name="T">The return type of the Task.</typeparam>
        /// <param name="task">The task from which to observe the exceptions.</param>
        /// <returns>The original task with the exception observation enabled.</returns>
        public static Task<T> ObserveExceptions<T>(this Task<T> task)
        {
            task.ContinueWith(t => { var ignored = t.Exception; }, TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);
            return task;
        }

        /// <summary>
        /// Creates a task from the <see cref="CancellationToken"/>.
        /// </summary>
        /// <param name="token">The <see cref="CancellationToken"/> to use.</param>
        /// <returns>A <see cref="Task"/></returns>
        public static Task ToTask(this CancellationToken token)
        {
            if (!token.CanBeCanceled) return NeverComplete;

            var tcs = new TaskCompletionSource<object>();
            token.Register(tcs.SetCanceled);
            return tcs.Task;
        }

        /// <summary>
        /// Converts an exception to a task for throwing an exception from a non-async func.
        /// </summary>
        /// <typeparam name="T">The return type of the <see cref="Task"/>.</typeparam>
        /// <param name="e">The <see cref="Exception"/> to throw.</param>
        /// <returns>A <see cref="Task"/> with the <see cref="e"/> set as the <see cref="Exception"/>.</returns>
        public static Task<T> AsTask<T>(this Exception e)
        {
            var tcs = new TaskCompletionSource<T>();
            tcs.SetException(e);
            return tcs.Task;
        }

        /// <summary>
        /// Converts an exception to a task for throwing an exception from a non-async func.
        /// </summary>
        /// <param name="e">The <see cref="Exception"/> to throw.</param>
        /// <returns>A <see cref="Task"/> with the <see cref="e"/> set as the <see cref="Exception"/>.</returns>
        public static Task AsTask(this Exception e)
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetException(e);
            return tcs.Task;
        }
    }
}
