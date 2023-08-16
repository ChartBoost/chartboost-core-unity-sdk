using System;
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

        protected override IConsentManagementPlatform _consent { get; } = new ConsentManagementPlatform();

        protected override IPublisherMetadata _publisherMetadata { get; } = new PublisherMetadata();

        protected override IAdvertisingEnvironment _advertisingEnvironment { get; } = new AdvertisingEnvironment();

        protected override IAnalyticsEnvironment _analyticsEnvironment { get; } = new AnalyticsEnvironment();

        protected override IAttributionEnvironment _attributionEnvironment { get; } = new AttributionEnvironment();
        protected override bool _debug { get; set; }
        protected override string _version => _getChartboostCoreVersion();

        protected override void _initialize(SDKConfiguration sdkConfiguration, InitializableModule[] modules)
        {
            foreach (var module in modules)
            {
                PendingModuleCache.TrackInitializableModule(module);
                if (module.NativeModule)
                    module.AddNativeInstance();
                else
                   _chartboostCoreAddUnityModule(module.ModuleId, module.ModuleVersion, OnModuleInitializeCallback);
            }

            _chartboostCoreInitialize(sdkConfiguration.ChartboostApplicationIdentifier, OnModuleInitializationResultCallback);
        }

        [MonoPInvokeCallback(typeof(ChartboostCoreOnModuleInitializationResultCallback))]
        private static void OnModuleInitializationResultCallback(string moduleIdentifier, long start, long end, long duration, string jsonError)
        {
            var module = PendingModuleCache.GetInitializableModule(moduleIdentifier);
            ChartboostCoreError? coreError = null;
            if (!string.IsNullOrEmpty(jsonError))
                coreError = JsonConvert.DeserializeObject<ChartboostCoreError>(jsonError);
            var result = new ModuleInitializationResult(start, end, duration, coreError, module!);
            OnModuleInitializationCompleted(result);
        }
        
        [MonoPInvokeCallback(typeof(ChartboostCoreOnModuleInitializeDelegate))]
        private static void OnModuleInitializeCallback(string moduleIdentifier)
        {
            Task.Run(async () =>
            {
                var module = PendingModuleCache.GetInitializableModule(moduleIdentifier);
                if (module == null)
                {
                    ChartboostCoreLogger.Log($"Unable to find module! Cannot initialize: {moduleIdentifier}");
                    return;
                }
                
                var result = await await MainThreadDispatcher.MainThreadTask(module.OnInitialize);
                var json = result.HasValue? JsonConvert.SerializeObject(result.Value) : null;
                _completeModuleInitialization(moduleIdentifier, json);
            });
        }

        private delegate void ChartboostCoreOnModuleInitializationResultCallback(string moduleIdentifier, long start, long end, long duration, string exception);
        private delegate void ChartboostCoreOnModuleInitializeDelegate(string moduleIdentifier);

        #nullable enable
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreInitialize(string chartboostAppIdentifier, ChartboostCoreOnModuleInitializationResultCallback moduleInitializationCallback);
        [DllImport(IOSConstants.DLLImport)] private static extern void _completeModuleInitialization(string moduleIdentifier, string? jsonError);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreAddUnityModule(string moduleIdentifier, string moduleVersion, ChartboostCoreOnModuleInitializeDelegate moduleInitializer);
        [DllImport(IOSConstants.DLLImport)] private static extern string _getChartboostCoreVersion();
        #nullable disable
    }
}
