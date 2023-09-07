using Chartboost.Core.Consent;
using Chartboost.Core.Environment;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;

namespace Chartboost.Core
{
    /// <summary>
    /// The main interface to the Chartboost Core SDK. Provides users with access to all of Core’s functionalities.
    /// </summary>
    public abstract partial class ChartboostCore
    {
        internal static ChartboostCore Instance
        {
            get => FindInstance();
            set => _instance = value;
        }

        /// <summary>
        /// Called whenever a module is initialized by the Chartboost Core SDK.
        /// </summary>
        public static event ChartboostCoreModuleInitializationDelegate ModuleInitializationCompleted;

        /// <summary>
        /// The CMP in charge of handling user consent.
        /// </summary>
        public static IConsentManagementPlatform Consent => Instance._consent;
        
        public static IPublisherMetadata PublisherMetadata => Instance._publisherMetadata;

        /// <summary>
        /// The environment that contains information intended solely for advertising purposes.
        /// </summary>
        public static IAdvertisingEnvironment AdvertisingEnvironment => Instance._advertisingEnvironment;

        /// <summary>
        /// The environment that contains information intended solely for analytics purposes.
        /// </summary>
        public static IAnalyticsEnvironment AnalyticsEnvironment => Instance._analyticsEnvironment;

        /// <summary>
        /// The environment that contains information intended solely for attribution purposes.
        /// </summary>
        public static IAttributionEnvironment AttributionEnvironment => Instance._attributionEnvironment;

        public static bool Debug
        {
            get => Instance is { _debug: true };
            set => Instance._debug = value;
        }

        /// <summary>
        /// The version of the Native Core SDK.
        /// </summary>
        public static string NativeSDKVersion => Instance._version ?? UnitySDKVersion;

        /// <summary>
        /// The version of the Unity Core SDK.
        /// </summary>
        public static string UnitySDKVersion => "0.2.0";

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
