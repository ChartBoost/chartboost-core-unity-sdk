// ReSharper disable InconsistentNaming

namespace Chartboost.Core.Android.Utilities
{
    internal class AndroidConstants
    {
        #region Native Java/Kotlin Classes
        internal const string FunctionToString = "toString";
        internal const string FunctionGetValue = "getValue";
        internal const string FunctionNext = "next";
        internal const string FunctionHasNext = "hasNext";
        internal const string FunctionIterator = "iterator";
        internal const string FunctionEntrySet = "entrySet";
        internal const string FunctionGetKey = "getKey";
        internal const string FunctionBooleanValue = "booleanValue";
        internal const string FunctionDoubleValue = "doubleValue";
        internal const string FunctionFloatValue = "floatValue";
        internal const string FunctionIntValue = "intValue";
        internal const string FunctionAddModule = "addModule";
        internal const string FunctionClearModules = "clearModules";
        internal const string FunctionCompleted = "completed";
        #endregion

        #region Android Chartboost Core SDK

        internal const string CoreError = "com.chartboost.core.unity.CoreErrorUnity";
        internal const string GetError = "getError";
        internal const string GetCode = "getCode";
        internal const string GetMessage = "getMessage";
        internal const string GetCause = "getCause";
        internal const string GetResolution = "getResolution";
        
        internal const string ChartboostCore = "com.chartboost.core.ChartboostCore";
        internal const string InitializeSDK = "initializeSdk";
        internal const string GetSDKVersion = "getSdkVersion";
        internal const string GetDebug = "getDebug";
        internal const string SetDebug = "setDebug";
        internal const string GetModuleId= "getModuleId";
        internal const string GetModuleVersion = "getModuleVersion";

        internal const string SdkConfiguration = "com.chartboost.core.initialization.SdkConfiguration";
        internal const string ConsentObserver = "com.chartboost.core.consent.ConsentObserver";
        internal const string InitializableModuleObserver = "com.chartboost.core.initialization.InitializableModuleObserver";
        internal const string PublisherMetadataObserver = "com.chartboost.core.environment.PublisherMetadataObserver";

        internal const string ModuleConfigurationJson = "json";
        internal const string Module = "module";
        internal const string ModuleInitializationResultStart = "start";
        internal const string ModuleInitializationResultEnd = "end";
        internal const string ModuleInitializationResultDuration = "duration";
        internal const string ModuleInitializationResultException = "exception";
        internal const string InitializableModuleModuleId = "moduleId";
        internal const string AddObserver = "addObserver";
        internal const string RemoveObserver = "removeObserver";
        
        #region Consent
        internal const string Consent = "getConsent";
        internal const string GetConsentShouldCollect = "getShouldCollectConsent";
        internal const string GetConsents = "getConsents";
        internal const string GetConsentStatus = "getConsentStatus";
        internal const string GrantConsentStatus = "grantConsentStatus";
        internal const string DenyConsentStatus = "denyConsentStatus";
        internal const string ResetConsentStatus = "resetConsentStatus";
        internal const string ConsentStatusEnum = "com.chartboost.core.consent.ConsentStatus";
        internal const string ConsentStatusSourceEnum = "com.chartboost.core.consent.ConsentStatusSource";
        internal const string ConsentStatusSourceEnumUser = "USER";
        internal const string ConsentStatusSourceEnumDeveloper = "DEVELOPER";
        
        internal const string ConsentShowConsentDialog = "showConsentDialog";
        internal const string ConsentDialogTypeEnum = "com.chartboost.core.consent.ConsentDialogType";
        internal const string ConsentDialogTypeEnumConcise = "CONCISE";
        internal const string ConsentDialogTypeEnumDetailed = "DETAILED";
        #endregion

        #region Publisher Metadata
        internal const string PublisherMetadata = "getPublisherMetadata";
        internal const string SetPropertyIsUserUnderAge = "setIsUserUnderage";
        internal const string SetPropertyPublisherSessionIdentifier = "setPublisherSessionIdentifier";
        internal const string SetPropertyPublisherAppIdentifier = "setPublisherAppIdentifier";
        internal const string SetPropertyFrameworkName = "setFrameworkName";
        internal const string SetPropertyFrameworkVersion = "setFrameworkVersion";
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
        internal const string GetPropertyScreenHeight = "getScreenHeight";
        internal const string GetPropertyScreenScale = "getScreenScale";
        internal const string GetPropertyScreenWidth = "getScreenWidth";
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
        internal const string BridgeCBC = "com.chartboost.core.unity.BridgeCBC";
        internal const string BridgeCMP = "com.chartboost.core.unity.BridgeCMP";
        internal const string BridgeEnvAdvertising = "com.chartboost.core.unity.BridgeEnvAdvertising";
        internal const string BridgeEnvAnalytics = "com.chartboost.core.unity.BridgeEnvAnalytics";
        internal const string BridgeEnvAttribution = "com.chartboost.core.unity.BridgeEnvAttribution";
        internal const string ResultBoolean = "com.chartboost.core.unity.ResultBoolean";
        internal const string ResultString = "com.chartboost.core.unity.ResultString";
        internal const string ModuleInitializerConsumer = "com.chartboost.core.unity.ModuleInitializerConsumer";
        internal const string ModuleFactory = "com.chartboost.core.unity.ModuleFactory";
        internal const string FuncMakeUnityModule = "makeUnityModule";
        #endregion
    }
}
