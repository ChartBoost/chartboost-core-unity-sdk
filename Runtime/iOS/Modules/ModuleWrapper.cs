using System;
using System.Runtime.InteropServices;
using AOT;
using Chartboost.Constants;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using Chartboost.Json;
using Chartboost.Logging;

namespace Chartboost.Core.iOS.Modules
{
    internal static class ModuleWrapper
    {
        public static IntPtr WrapUnityModule(Module module)
            => _CBCWrapUnityModule(module.ModuleId, module.ModuleVersion, OnModuleInitialize);

        public static void AddModule(IntPtr uniqueId) 
            => _CBCAddModule(uniqueId);

        public static string GetModuleId(IntPtr uniqueId) 
            => _CBCGetModuleId(uniqueId);
        
        public static string GetModuleVersion(IntPtr uniqueId) 
            => _CBCGetModuleVersion(uniqueId);
        
        [MonoPInvokeCallback(typeof(ExternChartboostCoreOnModuleInitializeDelegate))]
        private static void OnModuleInitialize(string moduleIdentifier, string chartboostAppIdentifier)
        {
            MainThreadDispatcher.MainThreadTask(async () =>
            {
                ChartboostCoreError? error = null;
                try
                {
                    var module = PendingModuleCache.GetModule(moduleIdentifier);

                    if (module == null)
                    {
                        var errorMessage = $"Attempting to initialize module: {moduleIdentifier} but no reference was found, returning.";
                        error = new ChartboostCoreError(-1, errorMessage);
                        LogController.Log(errorMessage, LogLevel.Error);
                        _CBCCompleteModuleInitialization(moduleIdentifier, JsonTools.SerializeObject(error.Value));
                        return;
                    }
                    
                    var moduleConfiguration = new ModuleConfiguration(chartboostAppIdentifier);
                    error = await module.OnInitialize(moduleConfiguration);
                }
                catch (Exception exception)
                {
                    LogController.LogException(exception);
                    error = new ChartboostCoreError(-1, exception.Message);
                }
                finally
                {
                    try
                    {
                        var json = error.HasValue ? JsonTools.SerializeObject(error.Value) : null;
                        _CBCCompleteModuleInitialization(moduleIdentifier, json);
                    }
                    catch (Exception e)
                    {
                        LogController.LogException(e);
                        var json = error.HasValue ? JsonTools.SerializeObject(new ChartboostCoreError(-1, e.Message)) : null;
                        _CBCCompleteModuleInitialization(moduleIdentifier, json);
                    }
                }
            });
        }
        
        [DllImport(SharedIOSConstants.DLLImport)] private static extern IntPtr _CBCWrapUnityModule(string moduleIdentifier, string moduleVersion, ExternChartboostCoreOnModuleInitializeDelegate moduleInitializer);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCAddModule(IntPtr uniqueId);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCGetModuleId(IntPtr uniqueId);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCGetModuleVersion(IntPtr uniqueId);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCCompleteModuleInitialization(string moduleIdentifier, string jsonError);
    }
}
