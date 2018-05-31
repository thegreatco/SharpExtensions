﻿using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SharpExtensions
{
    /// <summary>
    /// A collection of extension methods to use with <see cref="Task"/>.
    /// </summary>
    public static partial class Tasktensions
    {
        /// <summary>
        /// The <see cref="EventHandler"/> that will return any exceptions thrown during any task execution.
        /// </summary>
        public static EventHandler<TaskErrorEventArgs> TaskErrorEventHandler;

        private static readonly TaskCompletionSource<object> NeverCompleteSource = new TaskCompletionSource<object>();

        /// <summary>
        /// A <see cref="Task"/> that never completes.
        /// </summary>
        public static Task NeverComplete => NeverCompleteSource.Task;

        /// <summary>
        /// Run a task with a timeout.
        /// </summary>
        /// <param name="task">The task to run.</param>
        /// <param name="timeout">The maximum amount of time the task is allowed to run.</param>
        /// <exception cref="TimeoutException">A timeout exception if the task takes too long to run.</exception>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task WithTimeout(this Task task, TimeSpan timeout)
        {
            if (task != await Task.WhenAny(task, Task.Delay(timeout)))
                throw new TimeoutException();

            await task;
        }

        /// <summary>
        /// Allows non-cancelable tasks to be awaited, and throws an exception after a specified timeout if the task has not yet completed.
        /// </summary>
        /// <typeparam name="T">The type of the task's return value.</typeparam>
        /// <param name="task">The <see cref="Task{T}"/> to run.</param>
        /// <param name="timeout">The timeout after which to throw.</param>
        /// <returns>A task that will throw a <see cref="TimeoutException"/> depending on the provided timeout.</returns>
        public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout)
        {
            if (task != await Task.WhenAny(task, Task.Delay(timeout)))
                throw new TimeoutException();

            return await task;
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
        /// Run a task with a timeout.
        /// </summary>
        /// <param name="task">The <see cref="Task{T}"/> to run.</param>
        /// <param name="timeout">The maximum amount of time in milliseconds the task is allowed to run.</param>
        /// <exception cref="TimeoutException">A timeout exception if the task takes too long to run. </exception>
        /// <returns>A <see cref="Task"/>.</returns>
        public static async Task<T> WithTimeout<T>(this Task<T> task, int timeout)
        {
            return await WithTimeout(task, TimeSpan.FromMilliseconds(timeout));
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
            if (task != await Task.WhenAny(task, Task.Delay(timeout)))
                action.Invoke();

            await task;
        }

        /// <summary>
        /// Allows non-cancelable tasks to be awaited, and throws an exception after a specified timeout if the task has not yet completed.
        /// </summary>
        /// <typeparam name="T">The type of the task's return value.</typeparam>
        /// <param name="task">The <see cref="Task{T}"/>.</param>
        /// <param name="timeout">The timeout after which to throw.</param>
        /// <param name="action">The action to execute if the task times out.</param>
        /// <returns>A task that will throw a <see cref="TimeoutException"/> depending on the provided timeout.</returns>
        public static async Task<T> WithTimeout<T>(this Task<T> task, TimeSpan timeout, Action action)
        {
            if (task != await Task.WhenAny(task, Task.Delay(timeout)))
                action.Invoke();

            return await task;
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

            await Task.WhenAny(task, cancelTask);
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

            await Task.WhenAny(task, cancelTask);
        }
        
        /// <summary>
        /// Run a task as void, allowing control to return immediately to the application.
        /// </summary>
        /// <param name="caller">The caller of the method.</param>
        /// <param name="task">The task to run.</param>
        public static void ToVoid(this Task task, string caller = null)
        {
            try
            {
#pragma warning disable 4014
                Task.Run(async () =>
#pragma warning restore 4014
                {
                    try
                    {
                        await task;
                    }
                    catch (Exception ex)
                    {
                        TaskErrorEventHandler?.Invoke(null, new TaskErrorEventArgs(ex, caller));
                    }
                });
            }
            catch (AggregateException aggex)
            {
                TaskErrorEventHandler?.Invoke(null, new TaskErrorEventArgs(aggex, caller));
            }
        }

        /// <summary>
        /// Executes the <see cref="Task"/> ignoring all thrown exceptions during execution.
        /// </summary>
        /// <param name="task">The <see cref="Task"/> to run.</param>
        /// <param name="caller">The caller of the method.</param>
        /// <returns>A <see cref="Task"/> that ignores all thrown exceptions during execution.</returns>
        public static async Task IgnoreExceptions(this Task task, string caller = null)
        {
            await task.ContinueWith(t =>
            {
                t.Exception?.Handle(ex =>
                {
                    if (TaskErrorEventHandler != null) TaskErrorEventHandler(null, new TaskErrorEventArgs(ex, caller));
                    else Debug.WriteLine(ex);
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
        /// <returns>A <see cref="Task"/> that ignores all thrown exceptions during execution.</returns>
        public static async Task<T> IgnoreExceptions<T>(this Task<T> task, string caller = null)
        {
            return await task.ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    t.Exception.Handle(ex =>
                    {
                        if (TaskErrorEventHandler != null)
                            TaskErrorEventHandler(null, new TaskErrorEventArgs(ex, caller));
                        else Debug.WriteLine(ex);
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
// ReSharper disable once UnusedVariable
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
// ReSharper disable once UnusedVariable
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
        /// <returns>A <see cref="Task"/> with the <paramref name="e"/> set as the <see cref="Exception"/>.</returns>
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
        /// <returns>A <see cref="Task"/> with the <paramref name="e"/> set as the <see cref="Exception"/>.</returns>
        public static Task AsTask(this Exception e)
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.SetException(e);
            return tcs.Task;
        }
    }
}
