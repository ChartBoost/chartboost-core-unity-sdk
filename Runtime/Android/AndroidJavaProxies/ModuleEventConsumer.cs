using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Constants;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using Chartboost.Logging;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    /// <summary>
    /// <see cref="AndroidJavaProxy"/> in charge of handling module Initialization.
    /// </summary>
    internal class ModuleEventConsumer : AndroidJavaProxy
    {
        private readonly Func<object, Task<ChartboostCoreError?>> _onInitialize;
        private readonly Action<IReadOnlyDictionary<string, object>> _onUpdateCredentials;

        
        public ModuleEventConsumer(Func<object, Task<ChartboostCoreError?>> onInitialize, Action<IReadOnlyDictionary<string, object>> credentialsEvent) : base(AndroidConstants.InterfaceModuleInitializerConsumer)
        {
            _onInitialize = onInitialize;
            _onUpdateCredentials = credentialsEvent;
        }

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
                    var identifier = moduleConfiguration.Call<string>(AndroidConstants.FunctionGetChartboostApplicationIdentifier);
                    ModuleConfiguration? configuration = new ModuleConfiguration(identifier);
                    error = await _onInitialize(configuration);
                }
                catch (Exception exception)
                {
                    error = new ChartboostCoreError(-1, exception.Message);
                    LogController.LogException(exception);
                }
                finally
                {
                    try
                    {
                        completion.Call(SharedAndroidConstants.FunctionCompleted, error?.ToNativeCoreError());
                    }
                    catch (Exception exception)
                    {
                        LogController.LogException(exception);
                        completion.Call(SharedAndroidConstants.FunctionCompleted, new ChartboostCoreError(-1, exception.Message).ToNativeCoreError());
                    }
                }
            });
        }
        
        [Preserve]
        // ReSharper disable once InconsistentNaming
        public void updateCredentials(string credentialsJson) 
            => MainThreadDispatcher.Post(_ => _onUpdateCredentials?.Invoke(credentialsJson.ToCredentials()));
    }
}
