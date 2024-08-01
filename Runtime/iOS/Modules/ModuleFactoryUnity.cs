using System;
using System.Runtime.InteropServices;
using AOT;
using Chartboost.Constants;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using Chartboost.Logging;

namespace Chartboost.Core.iOS.Modules
{
    public static class ModuleFactoryUnity
    {
        private static bool _initialized;
        
        public static void Initialize()
        {
            if (_initialized)
            {
                LogController.Log($"{nameof(ModuleFactoryUnity)} is already initialized", LogLevel.Warning);
                return;
            }

            _CBCSetModuleFactoryUnityCallbacks(OnMakeModule);
            _initialized = true;
        }

        [MonoPInvokeCallback(typeof(ExternChartboostCoreOnMakeModule))]
        private static void OnMakeModule(string className, string credentialsJson)
        {
            MainThreadDispatcher.Post(_ =>
            {
                var type = Type.GetType(className);
            
                // Check if something went wrong
                if (type == null)
                {
                    LogController.Log($"Requested {className} for Remote Initialization but Class was not found!", LogLevel.Error);
                    _CBCCompleteModuleMake(IntPtr.Zero);
                    return;
                }
            
                // // Create Instance
                var module = (Module)Activator.CreateInstance(type);
                PendingModuleCache.TrackModule(module);
                module.OnUpdateCredentials(credentialsJson.ToCredentials());
                _CBCCompleteModuleMake(ModuleWrapper.WrapUnityModule(module));
            });
        }
        
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetModuleFactoryUnityCallbacks(ExternChartboostCoreOnMakeModule onMakeModule);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCCompleteModuleMake(IntPtr uniqueId);
    }
}
