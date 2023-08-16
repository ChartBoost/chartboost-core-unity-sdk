using Chartboost.Core.Consent;
using Chartboost.Core.Environment;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;

namespace Chartboost.Core
{
    public abstract partial class ChartboostCore
    {
        internal static ChartboostCore Instance
        {
            get => GetInstance();
            set => _instance = value;
        }

        public static event ChartboostCoreModuleInitializationDelegate ModuleInitializationCompleted;

        public static IConsentManagementPlatform Consent => Instance._consent;
        
        public static IPublisherMetadata PublisherMetadata => Instance._publisherMetadata;

        public static IAdvertisingEnvironment AdvertisingEnvironment => Instance._advertisingEnvironment;

        public static IAnalyticsEnvironment AnalyticsEnvironment => Instance._analyticsEnvironment;

        public static IAttributionEnvironment AttributionEnvironment => Instance._attributionEnvironment;

        public static bool Debug
        {
            get => Instance is { _debug: true };
            set => Instance._debug = value;
        }

        public static string NativeSDKVersion => Instance._version;

        public static string UnitySDKVersion => "0.0.0";

        public static void Initialize(SDKConfiguration sdkConfiguration, InitializableModule[] modules) => Instance._initialize(sdkConfiguration, modules);
        
        protected abstract IConsentManagementPlatform _consent { get; }
        protected abstract IPublisherMetadata _publisherMetadata { get; }
        protected abstract IAdvertisingEnvironment _advertisingEnvironment { get; }
        protected abstract IAnalyticsEnvironment _analyticsEnvironment { get; }
        protected abstract IAttributionEnvironment _attributionEnvironment { get; }
        protected abstract bool _debug { get; set; }
        protected abstract string _version { get; }
        protected abstract void _initialize(SDKConfiguration sdkConfiguration, InitializableModule[] modules);

        protected static void OnModuleInitializationCompleted(ModuleInitializationResult result)
        {
            MainThreadDispatcher.Post(o => { ModuleInitializationCompleted?.Invoke(result); });
        }
    }
}
