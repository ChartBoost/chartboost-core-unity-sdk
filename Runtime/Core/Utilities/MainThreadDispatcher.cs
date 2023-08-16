using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Chartboost.Core.Utilities
{
    public static class MainThreadDispatcher {
        /// <summary>
        /// Synchronous; blocks until the callback completes
        /// </summary>
        public static void Send(SendOrPostCallback callback) => _context.Send(callback, null);

        /// <summary>
        /// Asynchronous; send and forget
        /// </summary>
        public static void Post(SendOrPostCallback callback) => _context.Post(callback, null);

        public static Task<T> MainThreadTask<T>(Func<T> task)
        {
            var ret = Task.Factory.StartNew(task, CancellationToken.None, TaskCreationOptions.None, _unityScheduler);
            ret.ContinueWith(faultedTask => ChartboostCoreLogger.LogException(faultedTask.Exception), TaskContinuationOptions.OnlyOnFaulted);
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
