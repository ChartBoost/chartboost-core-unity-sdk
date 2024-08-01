using System.Linq;
using Chartboost.Constants;
using Chartboost.Core.Android.AndroidJavaProxies;
using Chartboost.Core.Android.Consent;
using Chartboost.Core.Android.Environment;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Consent;
using Chartboost.Core.Environment;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using Chartboost.Logging;
using UnityEngine;

namespace Chartboost.Core.Android
{
    internal class ChartboostCore : ChartboostCoreBase
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] 
        private static void SetInstance()
        {
            if (Application.isEditor)
                return;
            Core.ChartboostCore.Instance = new ChartboostCore();
            using var moduleFactory = new AndroidJavaObject(AndroidConstants.ClassModuleFactoryUnity);
            moduleFactory.CallStatic(AndroidConstants.FunctionSetModuleFactoryEventConsumer, ModuleFactoryEventConsumer);
            moduleFactory.Call(AndroidConstants.FunctionSetModuleFactoryUnity);
        }

        private static readonly ModuleObserver ModuleObserverInstance = new(Core.ChartboostCore.OnModuleInitializationCompleted);
        private static readonly ModuleFactoryEventConsumer ModuleFactoryEventConsumer = new();

        /// <inheritdoc cref="ChartboostCoreBase.NativeVersion"/>
        public override string NativeVersion
        {
            get
            {
                using var sdk = Utilities.AndroidExtensions.NativeSDK();
                return sdk.CallStatic<string>(AndroidConstants.FunctionGetSDKVersion);
            }
        }
        
        /// <inheritdoc cref="ChartboostCoreBase.LogLevel"/>
        public override LogLevel LogLevel
        {
            get
            {
                using var bridge = Utilities.AndroidExtensions.AndroidBridge();
                return (LogLevel)bridge.CallStatic<int>(SharedAndroidConstants.FunctionGetLogLevel);;
            }
            set
            {
                using var bridge = Utilities.AndroidExtensions.AndroidBridge();
                bridge.CallStatic(SharedAndroidConstants.FunctionSetLogLevel, (int)value);
            }
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
            if (string.IsNullOrEmpty(sdkConfiguration.ChartboostApplicationIdentifier))
            {
                LogController.Log("Cannot Initialize Core. `ChartboostApplicationIdentifier` is not provided", LogLevel.Error);
                return;
            }

            using var bridge = Utilities.AndroidExtensions.AndroidBridge();
            
            // clear any cached modules before every init
            bridge.CallStatic(AndroidConstants.FunctionClearModules);
            
            foreach (var module in sdkConfiguration.Modules)
            {
                PendingModuleCache.TrackModule(module);
                if (module.NativeModule)
                {
                    module.AddNativeInstance();
                    continue;
                }

                var moduleEventConsumer = new ModuleEventConsumer(module.OnInitialize, module.OnUpdateCredentials);
                var moduleWrapper = new AndroidJavaClass(AndroidConstants.ClassModuleWrapper);
                var nativeModule = moduleWrapper.CallStatic<AndroidJavaObject>(AndroidConstants.FunctionWrapUnityModule, module.ModuleId, module.ModuleVersion, moduleEventConsumer);
                bridge.CallStatic(AndroidConstants.FunctionAddModule, nativeModule);
            }

            using var nativeSDKConfiguration = bridge.CallStatic<AndroidJavaObject>(AndroidConstants.FunctionGetNativeSDKConfiguration, sdkConfiguration.ChartboostApplicationIdentifier, sdkConfiguration.SkippedModuleIdentifiers.ToArray());
            bridge.CallStatic(AndroidConstants.FunctionInitializeSDK, nativeSDKConfiguration, ModuleObserverInstance);
        }
    }
}
