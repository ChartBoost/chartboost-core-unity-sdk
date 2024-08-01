using System.Runtime.InteropServices;
using AOT;
using Chartboost.Constants;
using Chartboost.Core.Consent;
using Chartboost.Core.Environment;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.iOS.Consent;
using Chartboost.Core.iOS.Environment;
using Chartboost.Core.iOS.Modules;
using Chartboost.Core.Utilities;
using Chartboost.Logging;
using UnityEngine;

namespace Chartboost.Core.iOS
{
    internal sealed class ChartboostCore : ChartboostCoreBase
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] 
        private static void SetInstance()
        {
            if (Application.isEditor)
                return;
            Core.ChartboostCore.Instance = new ChartboostCore();
            _CBCSetModuleInitializationResultCallback(OnModuleInitializationResult);
            ModuleFactoryUnity.Initialize();
        }
        
        /// <inheritdoc cref="ChartboostCoreBase.NativeVersion"/>
        public override string NativeVersion => _CBCVersion();

        /// <inheritdoc cref="ChartboostCoreBase.LogLevel"/>
        public override LogLevel LogLevel {
            get => (LogLevel)_CBCGetLogLevel();
            set => _CBCSetLogLevel((int)value);
        }

        /// <inheritdoc cref="ChartboostCoreBase.Consent"/>
        public override IConsentManagementPlatform Consent { get; } = new ConsentManagementPlatform();

        /// <inheritdoc cref="ChartboostCoreBase.PublisherMetadata"/>
        public override IPublisherMetadata PublisherMetadata { get; } = new PublisherMetadata();

        /// <inheritdoc cref="ChartboostCoreBase.AdvertisingEnvironment"/>
        public override IAdvertisingEnvironment AdvertisingEnvironment { get; } = new AdvertisingEnvironment();

        /// <inheritdoc cref="ChartboostCoreBase.AnalyticsEnvironment"/>
        public override IAnalyticsEnvironment AnalyticsEnvironment { get; } = new AnalyticsEnvironment();

        /// <inheritdoc cref="ChartboostCoreBase.AttributionEnvironment"/>
        public override IAttributionEnvironment AttributionEnvironment { get; } = new AttributionEnvironment();

        /// <inheritdoc cref="ChartboostCoreBase.Initialize"/>
        public override void Initialize(SDKConfiguration sdkConfiguration)
        {
            foreach (var module in sdkConfiguration.Modules)
            {
                PendingModuleCache.TrackModule(module);
                if (module.NativeModule)
                    module.AddNativeInstance();
                else
                {
                    var wrappedModule = ModuleWrapper.WrapUnityModule(module);
                    ModuleWrapper.AddModule(wrappedModule);
                }
            }

            _CBCInitialize(sdkConfiguration.ChartboostApplicationIdentifier);
        }
        
        [MonoPInvokeCallback(typeof(ExternChartboostCoreOnModuleInitializationResult))]
        private static void OnModuleInitializationResult(string moduleIdentifier, long start, long end, long duration, string moduleId, string moduleVersion, string jsonError)
        {
            MainThreadDispatcher.Post(_ =>
            {
                ChartboostCoreError? coreError = null;
                if (!string.IsNullOrEmpty(jsonError))
                    coreError = jsonError.ToChartboostCoreError();
                var result = new ModuleInitializationResult(start, end, duration, moduleId, moduleVersion, coreError);
                Core.ChartboostCore.OnModuleInitializationCompleted(result);
            });
        }
        
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCVersion();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern int _CBCGetLogLevel();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetLogLevel(int logLevel);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCInitialize(string chartboostAppIdentifier);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetModuleInitializationResultCallback(ExternChartboostCoreOnModuleInitializationResult onModuleInitializationResult);
    }
}
