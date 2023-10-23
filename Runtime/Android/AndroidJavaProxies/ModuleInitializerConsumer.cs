using System;
using System.Threading.Tasks;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    /// <summary>
    /// <see cref="AndroidJavaProxy"/> in charge of handling module Initialization.
    /// </summary>
    internal class ModuleInitializerConsumer : AndroidJavaProxy
    {
        private readonly Func<object, Task<ChartboostCoreError?>> _initCallback;
        public ModuleInitializerConsumer(Func<object, Task<ChartboostCoreError?>> initCallback) : base(AndroidConstants.ModuleInitializerConsumer) 
            => _initCallback = initCallback;

        /// <summary>
        /// Initializes the previously retained module init.
        /// </summary>
        /// <param name="moduleConfiguration"></param>
        /// <param name="completion">Completion object to notify the native layer of Unity modules initialization completion.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void initialize(AndroidJavaObject moduleConfiguration, AndroidJavaObject completion)
        {
            MainThreadDispatcher.MainThreadTask(async () =>
            {
                ChartboostCoreError? error = null; 
                try
                {
                    var identifier = moduleConfiguration.Get<string>("chartboostApplicationIdentifier");
                    ModuleInitializationConfiguration? configuration = new ModuleInitializationConfiguration(identifier);
                    error = await _initCallback(configuration);
                }
                catch (Exception e)
                {
                    error = new ChartboostCoreError(-1, e.Message);
                }
                finally
                {
                    try
                    {
                        completion.Call(AndroidConstants.FunctionCompleted, error?.ToNativeCoreError());
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
