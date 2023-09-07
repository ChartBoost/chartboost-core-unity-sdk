using System;
using System.Threading.Tasks;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Error;
using Chartboost.Core.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    /// <summary>
    /// <see cref="AndroidJavaProxy"/> in charge of handling module Initialization.
    /// </summary>
    internal class ModuleInitializerConsumer : AndroidJavaProxy
    {
        private readonly Func<Task<ChartboostCoreError?>> _initCallback;
        public ModuleInitializerConsumer(Func<Task<ChartboostCoreError?>> initCallback) : base(AndroidConstants.ModuleInitializerConsumer) 
            => _initCallback = initCallback;

        /// <summary>
        /// Initializes the previously retained module init.
        /// </summary>
        /// <param name="completion">Completion object to notify the native layer of Unity modules initialization completion.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void initialize(AndroidJavaObject completion)
        {
            Task.Run(async () =>
            {
                ChartboostCoreError? error = null; 
                try
                {
                    error = await await MainThreadDispatcher.MainThreadTask(_initCallback);
                }
                catch (Exception e)
                {
                    error = new ChartboostCoreError(-1, e.Message);
                }
                finally
                {
                    try
                    {
                        AndroidJNI.AttachCurrentThread();
                        completion.Call(AndroidConstants.FunctionCompleted, error?.ToNativeCoreError());
                        AndroidJNI.DetachCurrentThread();
                    }
                    catch (Exception e)
                    {
                        ChartboostCoreLogger.LogException(e);
                        completion.Call(AndroidConstants.FunctionCompleted, new ChartboostCoreError(-1, e.Message).ToNativeCoreError());
                    }
                }
            });
        }
    }
}
