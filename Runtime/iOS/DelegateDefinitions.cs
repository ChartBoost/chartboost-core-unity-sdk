namespace Chartboost.Core.iOS
{
    internal delegate void ExternChartboostCoreOnModuleInitializationResult(string moduleIdentifier, long start, long end, long duration, string moduleId, string moduleVersion, string exception);
    internal delegate void ExternChartboostCoreOnModuleInitializeDelegate(string moduleIdentifier, string chartboostAppIdentifier);
    internal delegate void ExternChartboostCoreOnMakeModule(string className, string credentialsJson);
    internal delegate void ExternChartboostCoreOnConsentChangeWithFullConsents(string modifiedKeys, string allConsents);
    internal delegate void ExternChartboostCoreOnConsentReadyWithInitialConsents(string initialConsents);
    internal delegate void ExternChartboostCoreOnEnumStatusChange(int value);
    internal delegate void ExternChartboostCoreOnResultBoolean(int hashCode, bool completion);
    internal delegate void ExternChartboostCoreOnResultString(int hashCode, string result);
}
