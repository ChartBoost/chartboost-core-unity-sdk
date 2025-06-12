using Chartboost.Core.Consent;
using Chartboost.Core.Default;
using Chartboost.Core.Environment;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using Chartboost.Logging;

namespace Chartboost.Core
{
    /// <summary>
    /// The main interface to the Chartboost Core SDK. Provides users with access to all of Core’s functionalities.
    /// </summary>
    public sealed class ChartboostCore
    {
        static ChartboostCore() => ModuleInitializationCompleted += OnModuleInitializationReceived;

        ~ChartboostCore() => ModuleInitializationCompleted -= OnModuleInitializationCompleted;

        private static void OnModuleInitializationReceived(ModuleInitializationResult result)
        {
            var module = PendingModuleCache.GetModule(result.ModuleId);

            if (module != null)
            {
                module.OnModuleReady();
                PendingModuleCache.ReleaseModule(result.ModuleId);
            }
            else
                LogController.Log($"Unable to Trigger OnModuleReady, for {result.ModuleId}, possibly initialized through backend or already called.", LogLevel.Verbose);
        }

        internal static ChartboostCoreBase Instance = new ChartboostCoreDefault();

        /// <summary>
        /// The version of the Unity Core SDK.
        /// </summary>
        public const string Version = "1.0.3";

        /// <summary>
        /// The native Chartboost Core SDK version.
        /// The value is a semantic versioning compliant string.
        /// </summary>
        public static string NativeVersion => Instance.NativeVersion;

        /// <summary>
        /// A <see cref="bool"/> flag for setting test mode.
        /// <para/>
        /// warning: Do not enable test mode in production builds.
        /// </summary>
        public static LogLevel LogLevel
        {
            get => Instance.LogLevel;
            set => Instance.LogLevel = value;
        }

        /// <summary>
        /// Called whenever a module is initialized by the Chartboost Core SDK.
        /// </summary>
        public static event ChartboostCoreModuleInitializationDelegate ModuleInitializationCompleted;

        /// <summary>
        /// The CMP in charge of handling user consent.
        /// </summary>
        public static IConsentManagementPlatform Consent => Instance.Consent;

        /// <summary>
        /// Publisher-provided metadata.
        /// </summary>
        public static IPublisherMetadata PublisherMetadata => Instance.PublisherMetadata;

        /// <summary>
        /// The environment that contains information intended solely for advertising purposes.
        /// </summary>
        public static IAdvertisingEnvironment AdvertisingEnvironment => Instance.AdvertisingEnvironment;

        /// <summary>
        /// The environment that contains information intended solely for analytics purposes.
        /// </summary>
        public static IAnalyticsEnvironment AnalyticsEnvironment => Instance.AnalyticsEnvironment;

        /// <summary>
        /// The environment that contains information intended solely for attribution purposes.
        /// </summary>
        public static IAttributionEnvironment AttributionEnvironment => Instance.AttributionEnvironment;

        /// <summary>
        /// Initializes the Chartboost Core SDK and its modules.
        ///
        /// As part of initialization Core will:
        /// <br/>
        /// - Fetch an app config from the Chartboost Core dashboard with all the info needed for Core and its
        /// modules to function.
        /// <br/>
        /// - Instantiate modules defined on the Charboost Core dashboard.
        /// <br/>
        /// - Initialize all the provided modules (both the ones explicitly passed in `configuration` and then
        /// those defined on the dashboard, skipping duplicates).
        /// <br/>
        /// - Keep all modules successfully initialized alive by creating strong references to them.
        /// <br/>
        /// - Set a module that conforms to ``ConsentAdapter`` as the backing CMP for <see cref="ChartboostCore.Consent"/>,
        /// and thus enabling CMP functionalities.
        /// <br/>
        /// </summary>
        /// <param name="sdkConfiguration">Initialization configuration parameters.</param>
        public static void Initialize(SDKConfiguration sdkConfiguration) => Instance.Initialize(sdkConfiguration);

        internal static void OnModuleInitializationCompleted(ModuleInitializationResult result) 
            => MainThreadDispatcher.Post(_ => ModuleInitializationCompleted?.Invoke(result));
    }
}
