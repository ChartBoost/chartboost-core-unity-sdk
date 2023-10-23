using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AOT;
using Chartboost.Core.Consent;
using Chartboost.Core.Environment;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.iOS.Consent;
using Chartboost.Core.iOS.Environment;
using Chartboost.Core.iOS.Utilities;
using Chartboost.Core.Utilities;
using Newtonsoft.Json;
using UnityEngine;

namespace Chartboost.Core.iOS
{
    internal class ChartboostCoreIOS : ChartboostCore
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void SetInstance()
        {
            if (Instance != null)
                return;
            Instance = new ChartboostCoreIOS();
        }

        public ChartboostCoreIOS() 
            => _chartboostCoreSetModuleInitializationCallback(OnModuleInitializationResult);

        protected override IConsentManagementPlatform _consent { get; } = new ConsentManagementPlatform();

        protected override IPublisherMetadata _publisherMetadata { get; } = new PublisherMetadata();

        protected override IAdvertisingEnvironment _advertisingEnvironment { get; } = new AdvertisingEnvironment();

        protected override IAnalyticsEnvironment _analyticsEnvironment { get; } = new AnalyticsEnvironment();

        protected override IAttributionEnvironment _attributionEnvironment { get; } = new AttributionEnvironment();
        protected override bool _debug { get; set; }
        protected override string _version => _getChartboostCoreVersion();

        protected override void _initialize(SDKConfiguration sdkConfiguration, IEnumerable<InitializableModule> modules)
        {
            foreach (var module in modules)
            {
                PendingModuleCache.TrackInitializableModule(module);
                if (module.NativeModule)
                    module.AddNativeInstance();
                else
                   _chartboostCoreAddUnityModule(module.ModuleId, module.ModuleVersion, OnModuleInitialize);
            }

            _chartboostCoreInitialize(sdkConfiguration.ChartboostApplicationIdentifier, OnModuleInitializationResult);
        }

        [MonoPInvokeCallback(typeof(ChartboostCoreOnModuleInitializationResult))]
        private static void OnModuleInitializationResult(string moduleIdentifier, long start, long end, long duration, string jsonError)
        {
            MainThreadDispatcher.Post(o =>
            {
                var module = PendingModuleCache.GetInitializableModule(moduleIdentifier);
                ChartboostCoreError? coreError = null;
                if (!string.IsNullOrEmpty(jsonError))
                    coreError = JsonConvert.DeserializeObject<ChartboostCoreError>(jsonError);
                var result = new ModuleInitializationResult(start, end, duration, coreError, module!);
                OnModuleInitializationCompleted(result);
                if (coreError == null)
                    module.OnModuleReady();
            });
        }
        
        [MonoPInvokeCallback(typeof(ChartboostCoreOnModuleInitializeDelegate))]
        private static void OnModuleInitialize(string moduleIdentifier, string chartboostAppIdentifier)
        {
            MainThreadDispatcher.MainThreadTask(async () =>
            {
                ChartboostCoreError? error = null;
                try
                {
                    var module = PendingModuleCache.GetInitializableModule(moduleIdentifier);
                    var moduleConfiguration = new ModuleInitializationConfiguration(chartboostAppIdentifier);
                    error = await module!.OnInitialize(moduleConfiguration);
                }
                catch (Exception e)
                {
                    ChartboostCoreLogger.LogException(e);
                    error = new ChartboostCoreError(-1, e.Message);
                }
                finally
                {
                    try
                    {
                        var json = error.HasValue ? JsonConvert.SerializeObject(error.Value) : null;
                        _completeModuleInitialization(moduleIdentifier, json);
                    }
                    catch (Exception e)
                    {
                        ChartboostCoreLogger.LogException(e);
                        var json = error.HasValue ? JsonConvert.SerializeObject(new ChartboostCoreError(-1, e.Message)) : null;
                        _completeModuleInitialization(moduleIdentifier, json);
                    }
                }
            });
        }

        #nullable enable
        [DllImport(IOSConstants.DLLImport)] private static extern string _getChartboostCoreVersion();
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreInitialize(string chartboostAppIdentifier, ChartboostCoreOnModuleInitializationResult moduleInitialization);
        [DllImport(IOSConstants.DLLImport)] private static extern void _completeModuleInitialization(string moduleIdentifier, string? jsonError);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreAddUnityModule(string moduleIdentifier, string moduleVersion, ChartboostCoreOnModuleInitializeDelegate moduleInitializer);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreSetModuleInitializationCallback(ChartboostCoreOnModuleInitializationResult onModuleInitializationResult);
        #nullable disable
    }
}
