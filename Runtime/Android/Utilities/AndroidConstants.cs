namespace Chartboost.Core.Android.Utilities
{
    internal class AndroidConstants
    {
        #region Android Chartboost Core SDK

        private const string NamespaceChartboostCore = "com.chartboost.core";
        private static readonly string NamespaceUnity = $"{NamespaceChartboostCore}.unity";
        
        internal static readonly string CoreError = GetErrorType("CoreErrorUnity");
        
        private static string GetErrorType(string className) => $"{NamespaceUnity}.error.{className}";
        
        internal const string FunctionGetError = "getError";
        internal const string FunctionGetCode = "getCode";
        internal const string FunctionGetMessage = "getMessage";
        internal const string FunctionGetCause = "getCause";
        internal const string FunctionGetResolution = "getResolution";

        private static string GetNativeType(string className) => $"{NamespaceChartboostCore}.{className}";
        
        internal static readonly string ChartboostCore = GetNativeType("ChartboostCore");
        internal static readonly string ModuleObserver = GetNativeType("initialization.ModuleObserver");
        internal static readonly string EnvironmentObserver = GetNativeType("environment.EnvironmentObserver");
        
        internal const string FunctionInitializeSDK = "initializeSdk";
        internal const string FunctionGetNativeSDKConfiguration = "getNativeSDKConfiguration";
        internal const string FunctionGetSDKVersion = "getSdkVersion";
        internal const string FunctionGetModuleId= "getModuleId";
        internal const string FunctionGetModuleVersion = "getModuleVersion";
        internal const string FunctionGetChartboostApplicationIdentifier = "getChartboostApplicationIdentifier";
        
        internal const string FunctionGetStart = "getStart";
        internal const string FunctionGetEnd = "getEnd";
        internal const string FunctionGetDuration = "getDuration";
        internal const string FunctionGetException = "getException";
        internal const string FunctionAddObserver = "addObserver";
        internal const string FunctionRemoveObserver = "removeObserver";
        
        #region Consent
        internal static readonly string ConsentObserver = GetNativeConsentType("ConsentObserver");
        internal const string Consent = "getConsent";
        internal const string GetConsentShouldCollect = "getShouldCollectConsent";
        internal const string GetConsents = "getConsents";
        internal const string GrantConsentStatus = "grantConsentStatus";
        internal const string DenyConsentStatus = "denyConsentStatus";
        internal const string ResetConsentStatus = "resetConsentStatus";
        internal const string ConsentShowConsentDialog = "showConsentDialog";
        
        internal static readonly string ConsentSourceEnum = GetNativeConsentType("ConsentSource");
        internal const string ConsentSourceEnumUser = "USER";
        internal const string ConsentSourceEnumDeveloper = "DEVELOPER";
        
        internal static readonly string ConsentDialogTypeEnum = GetNativeConsentType("ConsentDialogType");
        internal const string ConsentDialogTypeEnumConcise = "CONCISE";
        internal const string ConsentDialogTypeEnumDetailed = "DETAILED";
        
        private static string GetNativeConsentType(string className) => $"{NamespaceChartboostCore}.consent.{className}";
        #endregion

        #region Publisher Metadata
        internal const string PublisherMetadata = "getPublisherMetadata";
        internal const string SetPropertyIsUserUnderAge = "setIsUserUnderage";
        internal const string SetPropertyPublisherSessionIdentifier = "setPublisherSessionIdentifier";
        internal const string SetPropertyPublisherAppIdentifier = "setPublisherAppIdentifier";
        internal const string SetPropertyFramework = "setFramework";
        internal const string SetPropertyPlayerIdentifier = "setPlayerIdentifier";
        #endregion

        #region Attribution Environment
        internal const string EnvironmentAttribution = "getAttributionEnvironment";
        internal const string GetPropertyAdvertisingIdentifier = "getAdvertisingIdentifier";
        internal const string GetPropertyUserAgent = "getUserAgent";
        #endregion

        #region Advertising Environment
        internal const string EnvironmentAdvertising = "getAdvertisingEnvironment";
        internal const string GetPropertyOSName = "getOsName";
        internal const string GetPropertyOSVersion = "getOsVersion";
        internal const string GetPropertyDeviceMake = "getDeviceMake";
        internal const string GetPropertyDeviceModel = "getDeviceModel";
        internal const string GetPropertyDeviceLocale = "getDeviceLocale";
        internal const string GetPropertyScreenHeightPixels = "getScreenHeightPixels";
        internal const string GetPropertyScreenScale = "getScreenScale";
        internal const string GetPropertyScreenWidthPixels = "getScreenWidthPixels";
        internal const string GetPropertyBundleIdentifier = "getBundleIdentifier";
        internal const string GetPropertyLimitAdTrackingEnabled = "getLimitAdTrackingEnabled";
        #endregion

        #region Analytics Environment
        internal const string EnvironmentAnalytics = "getAnalyticsEnvironment";
        internal const string GetPropertyIsUserUnderAge = "isUserUnderage";
        internal const string GetPropertyPublisherSessionIdentifier = "getPublisherSessionIdentifier";
        internal const string GetPropertyPublisherAppIdentifier = "getPublisherAppIdentifier";
        internal const string GetPropertyFrameworkName = "getFrameworkName";
        internal const string GetPropertyFrameworkVersion = "getFrameworkVersion";
        internal const string GetPropertyPlayerIdentifier = "getPlayerIdentifier";
        internal const string GetPropertyNetworkConnectionType = "getNetworkConnectionType";
        internal const string GetPropertyVolume = "getVolume";
        internal const string GetPropertyVendorIdentifier = "getVendorIdentifier";
        internal const string GetPropertyVendorIdentifierScope = "getVendorIdentifierScope";
        internal const string GetPropertyAppVersion = "getAppVersion";
        internal const string GetPropertyAppSessionDuration = "getAppSessionDurationSeconds";
        internal const string GetPropertyAppSessionIdentifier = "getAppSessionIdentifier";
        #endregion

        #endregion

        #region Chartboost Core Android Bridge
        internal static readonly string ClassBridgeChartboostCore = GetBridgeType("BridgeCBC");
        internal static readonly string ClassBridgeConsentManagementPlatform = GetBridgeType("BridgeCMP");
        internal static readonly string ClassBridgeEnvAdvertising = GetBridgeType("BridgeEnvAdvertising");
        internal static readonly string ClassBridgeEnvAnalytics = GetBridgeType("BridgeEnvAnalytics");
        internal static readonly string ClassBridgeEnvAttribution = GetBridgeType("BridgeEnvAttribution");
        private static string GetBridgeType(string className) => $"{NamespaceUnity}.bridge.{className}";
        
        internal static readonly string InterfaceResultBoolean = GetResultType("ResultBoolean");
        internal static readonly string InterfaceResultString = GetResultType("ResultString");
        private static string GetResultType(string interfaceName) => $"{NamespaceUnity}.result.{interfaceName}";
        
        internal static readonly string InterfaceModuleInitializerConsumer = GetModuleType("ModuleEventConsumer");
        internal static readonly string ClassModuleWrapper = GetModuleType("ModuleWrapper");
        private static string GetModuleType(string className) => $"{NamespaceUnity}.initialization.{className}";
        
        internal const string FunctionWrapUnityModule = "wrapUnityModule";
        internal const string FunctionAddModule = "addModule";
        internal const string FunctionClearModules = "clearModules";
        
        internal static readonly string ClassModuleFactoryUnity = GetFactoryType("ModuleFactoryUnity");
        internal static readonly string ClassModuleFactoryEventConsumer = GetFactoryType("ModuleFactoryEventConsumer");
        private static string GetFactoryType(string className) => $"{NamespaceUnity}.factory.{className}";

        internal const string FunctionSetModuleFactoryEventConsumer = "setModuleFactoryEventConsumer";
        internal const string FunctionSetModuleFactoryUnity = "setModuleFactoryUnity";
        #endregion
    }
}
