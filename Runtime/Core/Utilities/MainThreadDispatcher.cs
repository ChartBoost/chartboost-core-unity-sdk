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
        /// <param name="task"><see cref="Task{T}"/> to be dispatched.</param>
        /// <typeparam name="T">Result type.</typeparam>
        /// <returns>The result form the Unity task.</returns>
        public static Task<T> MainThreadTask<T>(Func<T> task)
        {
            var ret = Task.Factory.StartNew(task, CancellationToken.None, TaskCreationOptions.None, _unityScheduler);
            ret.ContinueWith(faultedTask => ChartboostCoreLogger.LogException(faultedTask.Exception), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
           return ret;
        }

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
