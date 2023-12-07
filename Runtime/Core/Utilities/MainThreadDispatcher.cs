using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Chartboost.Core.Utilities
{
    /// <summary>
    /// <para>Utility class to dispatch functionality into Unity's main thread.</para>
    /// If your code contains references to any Unity Code and not just raw C#, you will have to dispatch it to the Unity main thread.
    /// </summary>
    public static class MainThreadDispatcher {
        
        /// <summary>
        /// Synchronous; blocks until the callback completes
        /// </summary>
        public static void Send(SendOrPostCallback callback) => _context.Send(callback, null);

        /// <summary>
        /// Asynchronous; send and forget
        /// </summary>
        public static void Post(SendOrPostCallback callback) => _context.Post(callback, null);

        /// <summary>
        /// Creates a <see cref="Task"/> that will be dispatched on the Unity Scheduler.
        /// </summary>
        /// <param name="task"><see cref="Task"/> to be dispatched.</param>
        /// <typeparam name="T">Result type.</typeparam>
        /// <returns>The result form the Unity task.</returns>
        public static void MainThreadTask<T>(Func<T> task)
        {
            var ret = Task.Factory.StartNew(task, CancellationToken.None, TaskCreationOptions.None, _unityScheduler);
            ret.AppendExceptionLogging();
        }

        /// <summary>
        /// Creates a <see cref="Task{TResult}"/> that will be dispatched on the Unity Scheduler.
        /// </summary>
        /// <param name="task"><see cref="Task{TResult}"/> to be dispatched.</param>
        /// <param name="parameter">Parameter to pass into <see cref="Task{TResult}"/> </param>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <typeparam name="TResult">Result type.</typeparam>
        /// <returns></returns>
        public static Task<TResult> MainThreadTask<T, TResult>(Func<object, TResult> task, T parameter)
        {
            var ret = Task.Factory.StartNew(task, parameter, CancellationToken.None, TaskCreationOptions.None, _unityScheduler);
            ret.AppendExceptionLogging();
            return ret;
        }

        /// <summary>
        /// Creates a continuation that executes asynchronously, on the Unity main thread, when the target <see cref="Task{T}"/> completes.
        /// </summary>
        /// <param name="task">Target <see cref="Task"/>.</param>
        /// <param name="continuation">An action to run when the <see cref="Task"/> completes. </param>
        /// <typeparam name="T">The type of the result produced by the <see cref="Task"/>.</typeparam>
        /// <returns>A new continuation <see cref="Task"/>.</returns>
        public static Task ContinueWithOnMainThread<T>(this Task<T> task, Action<Task<T>> continuation)
        {
           var ret = task.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.None,
                _unityScheduler);
            ret.AppendExceptionLogging();
            return ret;
        }

        /// <summary>
        /// Creates a continuation that executes asynchronously, on the Unity main thread, when the target <see cref="Task"/> completes.
        /// </summary>
        /// <param name="task">Target <see cref="Task"/>.</param>
        /// <param name="continuation">An action to run when the <see cref="Task"/> completes. </param>
        /// <typeparam name="T">The type of the result produced by the <see cref="Task"/>.</typeparam>
        /// <returns>A new continuation <see cref="Task"/>.</returns>
        public static Task ContinueWithOnMainThread(this Task task, Action<Task> continuation)
        {
            var ret = task.ContinueWith(continuation, CancellationToken.None, TaskContinuationOptions.None, _unityScheduler); 
            ret.AppendExceptionLogging();
            return ret;
        }

        private static void AppendExceptionLogging(this Task inputTask) 
            => inputTask.ContinueWith(faultedTask => ChartboostCoreLogger.LogException(faultedTask.Exception), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

        private static SynchronizationContext _context;
        private static TaskScheduler _unityScheduler;

        #if UNITY_EDITOR
        [InitializeOnLoadMethod]
        #endif
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void _initialize()
        {
            _context ??= SynchronizationContext.Current;
            _unityScheduler ??= TaskScheduler.FromCurrentSynchronizationContext();
        }
    }
}
