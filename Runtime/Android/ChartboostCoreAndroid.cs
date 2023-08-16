using Chartboost.Core.Android.AndroidJavaProxies;
using Chartboost.Core.Android.Consent;
using Chartboost.Core.Android.Environment;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Consent;
using Chartboost.Core.Environment;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using UnityEngine;

namespace Chartboost.Core.Android
{
    internal class ChartboostCoreAndroid : ChartboostCore
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)] 
        private static void SetInstance()
        {
            if (Instance != null)
                return;
            Instance = new ChartboostCoreAndroid();
        }
        
        public ChartboostCoreAndroid()
        {
            using var bridge = AndroidUtils.GetAndroidBridge();
            bridge.CallStatic(AndroidConstants.InitializePreferences);
           _observer =new InitializableModuleObserver(OnModuleInitializationCompleted);
        }
        
        private static InitializableModuleObserver _observer;

        protected override IConsentManagementPlatform _consent { get; } = new ConsentManagementPlatform();
        protected override IPublisherMetadata _publisherMetadata { get; } = new PublisherMetadata();
        protected override IAdvertisingEnvironment _advertisingEnvironment { get; } = new AdvertisingEnvironment();
        protected override IAnalyticsEnvironment _analyticsEnvironment { get; } = new AnalyticsEnvironment();
        protected override IAttributionEnvironment _attributionEnvironment { get; } = new AttributionEnvironment();
        protected override bool _debug
        {
            get
            {
                using var sdk = AndroidUtils.GetNativeSDK();
                return sdk.CallStatic<bool>(AndroidConstants.GetDebug);
            }
            set
            {
                using var sdk = AndroidUtils.GetNativeSDK();
                sdk.CallStatic(AndroidConstants.SetDebug, value);
            }
        }

        protected override string _version
        {
            get
            {
                using var sdk = AndroidUtils.GetNativeSDK();
                return sdk.CallStatic<string>(AndroidConstants.GetSDKVersion);
            }
        }

        protected override void _initialize(SDKConfiguration sdkConfiguration, InitializableModule[] modules)
        {
            if (string.IsNullOrEmpty(sdkConfiguration.ChartboostApplicationIdentifier))
                return;
            
            using var nativeChartboostCoreSdkConfiguration = new AndroidJavaObject(AndroidConstants.SdkConfiguration, sdkConfiguration.ChartboostApplicationIdentifier);
            
            foreach (var module in modules)
            {
                PendingModuleCache.TrackInitializableModule(module);
                if (module.NativeModule)
                {
                    module.AddNativeInstance();
                    continue;
                }

                var initCallback = new ModuleInitializerConsumer(module.OnInitialize);
                using var moduleFactory = new AndroidJavaClass(AndroidConstants.ModuleFactory);
                moduleFactory.CallStatic(AndroidConstants.FuncMakeUnityModule, module.ModuleId, module.ModuleVersion, initCallback);
            }
            using var androidBridge = AndroidUtils.GetAndroidBridge();
            androidBridge.CallStatic(AndroidConstants.InitializeSDK, nativeChartboostCoreSdkConfiguration, _observer);
        }
    }
}
