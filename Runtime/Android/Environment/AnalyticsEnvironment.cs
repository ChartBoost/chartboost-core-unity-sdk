using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;
using Chartboost.Core.Utilities;

namespace Chartboost.Core.Android.Environment
{
    #nullable enable
    internal class AnalyticsEnvironment : BaseAndroidEnvironment, IAnalyticsEnvironment
    {
        protected override string EnvironmentProperty => AndroidConstants.EnvironmentAnalytics;
        public string OSName => GetProperty<string>(AndroidConstants.GetPropertyOSName);
        public string OSVersion => GetProperty<string>(AndroidConstants.GetPropertyOSVersion);
        public string DeviceMake => GetProperty<string>(AndroidConstants.GetPropertyDeviceMake);
        public string DeviceModel => GetProperty<string>(AndroidConstants.GetPropertyDeviceModel);
        public string? DeviceLocale => GetProperty<string?>(AndroidConstants.GetPropertyDeviceLocale);
        public double? ScreenHeight => GetDoubleProperty(AndroidConstants.GetPropertyScreenHeight);
        public double? ScreenScale => GetDoubleProperty(AndroidConstants.GetPropertyScreenScale);
        public double? ScreenWidth => GetDoubleProperty(AndroidConstants.GetPropertyScreenWidth);
        public string? BundleIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyBundleIdentifier);
        public bool? LimitAdTrackingEnabled => GetBoolProperty(AndroidConstants.GetPropertyLimitAdTrackingEnabled);
        public string? AdvertisingIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyAdvertisingIdentifier);
        public string? UserAgent => GetProperty<string?>(AndroidConstants.GetPropertyUserAgent);
        public NetworkConnectionType NetworkConnectionType => GetEnumProperty(AndroidConstants.GetPropertyNetworkConnectionType).GetNetworkConnectionType();
        public double? Volume => GetDoubleProperty(AndroidConstants.GetPropertyVolume);
        public string? VendorIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyVendorIdentifier);
        public VendorIdentifierScope VendorIdentifierScope => GetEnumProperty(AndroidConstants.GetPropertyVendorIdentifierScope).GetVendorIdentifierScope();
        public AuthorizationStatus AppTrackingTransparencyStatus => AuthorizationStatus.Unsupported;
        public string? AppVersion => GetProperty<string?>(AndroidConstants.GetPropertyAppVersion);
        public double AppSessionDuration => GetProperty<double>(AndroidConstants.GetPropertyAppSessionDuration);
        public string? AppSessionIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyAppSessionIdentifier);
        public bool? IsUserUnderage => GetBoolProperty(AndroidConstants.GetPropertyIsUserUnderAge);
        public string? PublisherSessionIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyPublisherSessionIdentifier);
        public string? PublisherAppIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyPublisherAppIdentifier);
        public string? FrameworkName => GetProperty<string?>(AndroidConstants.GetPropertyFrameworkName);
        public string? FrameworkVersion => GetProperty<string?>(AndroidConstants.GetPropertyFrameworkVersion);
        public string? PlayerIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyPlayerIdentifier);
    }
    #nullable disable
}
