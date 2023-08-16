using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;

namespace Chartboost.Core.Android.Environment
{
    #nullable enable
    internal class AdvertisingEnvironment : BaseAndroidEnvironment, IAdvertisingEnvironment
    {
        protected override string EnvironmentProperty => AndroidConstants.EnvironmentAdvertising;
        public string OSName => GetProperty<string>(AndroidConstants.GetPropertyOSName);
        public string OSVersion => GetProperty<string>(AndroidConstants.GetPropertyOSVersion);
        public string DeviceMake => GetProperty<string>(AndroidConstants.GetPropertyDeviceMake);
        public string DeviceModel => GetProperty<string>(AndroidConstants.GetPropertyDeviceModel);
        public string? DeviceLocale => GetProperty<string>(AndroidConstants.GetPropertyDeviceLocale);
        public double? ScreenHeight => GetDoubleProperty(AndroidConstants.GetPropertyScreenHeight);
        public double? ScreenScale => GetDoubleProperty(AndroidConstants.GetPropertyScreenScale);
        public double? ScreenWidth => GetDoubleProperty(AndroidConstants.GetPropertyScreenWidth);
        public string? BundleIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyBundleIdentifier);
        public bool? LimitAdTrackingEnabled => GetBoolProperty(AndroidConstants.GetPropertyLimitAdTrackingEnabled);
        public string? AdvertisingIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyAdvertisingIdentifier);
    }
    #nullable disable
}
