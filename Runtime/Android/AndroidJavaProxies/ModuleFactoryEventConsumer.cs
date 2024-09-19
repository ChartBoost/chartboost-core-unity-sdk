#nullable enable
using System;
using Chartboost.Constants;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using Chartboost.Logging;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    public class ModuleFactoryEventConsumer : AndroidJavaProxy
    {
        public ModuleFactoryEventConsumer() : base(AndroidConstants.ClassModuleFactoryEventConsumer) { }

        [Preserve]
        // ReSharper disable once InconsistentNaming
        public void makeModule(string className, AndroidJavaObject completion)
        {
            MainThreadDispatcher.Post(_ =>
            {
                var type = Type.GetType(className);

                AndroidJavaObject? wrappedModule = null;
            
                // Check if something went wrong
                if (type == null)
                {
                    LogController.Log($"Requested {className} for Remote Initialization but Class was not found!", LogLevel.Error);
                    completion.Call(SharedAndroidConstants.FunctionCompleted, wrappedModule);
                    return;
                }
            
                // Create Instance
                var module = (Module)Activator.CreateInstance(type);
                PendingModuleCache.TrackModule(module);
                var moduleEventConsumer = new ModuleEventConsumer(module.OnInitialize, module.OnUpdateCredentials);
                using var moduleWrapper = new AndroidJavaClass(AndroidConstants.ClassModuleWrapper);
                
                wrappedModule = moduleWrapper.CallStatic<AndroidJavaObject>(AndroidConstants.FunctionWrapUnityModule, module.ModuleId, module.ModuleVersion, moduleEventConsumer);
                completion.Call(SharedAndroidConstants.FunctionCompleted, wrappedModule);
                wrappedModule.Dispose();
            });
        }
    }
}
