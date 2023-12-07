namespace Chartboost.Core.iOS.Utilities
{
    internal delegate void ChartboostCoreOnModuleInitializationResult(string moduleIdentifier, long start, long end, long duration, string exception);
    internal delegate void ChartboostCoreOnModuleInitializeDelegate(string moduleIdentifier, string chartboostAppIdentifier);
    internal delegate void ChartboostCoreOnConsentChangeForStandard(string standard, string value);
    internal delegate void ChartboostCoreOnPartnerConsentChange(string partnerIdentifier, int consentStatus);
    internal delegate void ChartboostCoreOnEnumStatusChange(int value);
    internal delegate void ChartboostCoreOnResultBoolean(int hashCode, bool completion);
    internal delegate void ChartboostCoreOnResultString(int hashCode, string result);

    public static class IOSConstants
    {
        public const string DLLImport = "__Internal";
    }
}
