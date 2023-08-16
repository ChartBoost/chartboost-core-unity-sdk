using System.Runtime.InteropServices;
using Chartboost.Core.Environment;
using Chartboost.Core.iOS.Utilities;
using JetBrains.Annotations;

namespace Chartboost.Core.iOS.Environment
{
    #nullable enable
    public class AnalyticsEnvironment : IAnalyticsEnvironment
    {
        public string OSName => _analyticsEnvironmentGetOsName();
        public string OSVersion => _analyticsEnvironmentGetOsVersion();
        public string DeviceMake => _analyticsEnvironmentGetDeviceMake();
        public string DeviceModel => _analyticsEnvironmentGetDeviceModel();
        public string? DeviceLocale => _analyticsEnvironmentGetDeviceLocale();
        public double? ScreenHeight => _analyticsEnvironmentGetScreenHeight();
        public double? ScreenScale => _analyticsEnvironmentGetScreenScale();
        public double? ScreenWidth => _analyticsEnvironmentGetScreenWidth();
        public string? BundleIdentifier => _analyticsEnvironmentGetBundleIdentifier();
        public bool? LimitAdTrackingEnabled => _analyticsEnvironmentGetLimitAdTrackingEnabled();
        public NetworkConnectionType NetworkConnectionType => (NetworkConnectionType)_analyticsEnvironmentGetNetworkConnectionType();
        public double? Volume => _analyticsEnvironmentGetVolume();
        public string? VendorIdentifier => _analyticsEnvironmentGetVendorIdentifier();

        public VendorIdentifierScope VendorIdentifierScope => (VendorIdentifierScope)_analyticsEnvironmentGetVendorIdentifierScope();
        public AuthorizationStatus AppTrackingTransparencyStatus => (AuthorizationStatus)_analyticsEnvironmentGetAuthorizationStatus();
        public string? AppVersion => _analyticsEnvironmentGetAppVersion();
        public double AppSessionDuration => _analyticsEnvironmentGetAppSessionDuration();
        public string? AppSessionIdentifier => _analyticsEnvironmentGetAppSessionIdentifier();
        public bool? IsUserUnderage => _analyticsEnvironmentGetIsUserUnderage();
        public string? PublisherSessionIdentifier => _analyticsEnvironmentGetPublisherSessionIdentifier();
        public string? PublisherAppIdentifier => _analyticsEnvironmentGetPublisherAppIdentifier();
        public string? FrameworkName => _analyticsEnvironmentGetFrameworkName();
        public string? FrameworkVersion => _analyticsEnvironmentGetFrameworkVersion();
        public string? PlayerIdentifier => _analyticsEnvironmentGetPlayerIdentifier();
        public string? AdvertisingIdentifier => _analyticsEnvironmentGetAdvertisingIdentifier();
        public string? UserAgent => _analyticsEnvironmentGetUserAgent();
        
        [DllImport(IOSConstants.DLLImport)] private static extern string _analyticsEnvironmentGetOsName();
        [DllImport(IOSConstants.DLLImport)] private static extern string _analyticsEnvironmentGetOsVersion();
        [DllImport(IOSConstants.DLLImport)] private static extern string _analyticsEnvironmentGetDeviceMake();
        [DllImport(IOSConstants.DLLImport)] private static extern string _analyticsEnvironmentGetDeviceModel();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetDeviceLocale();
        [DllImport(IOSConstants.DLLImport)] private static extern double _analyticsEnvironmentGetScreenHeight();
        [DllImport(IOSConstants.DLLImport)] private static extern double _analyticsEnvironmentGetScreenScale();
        [DllImport(IOSConstants.DLLImport)] private static extern double _analyticsEnvironmentGetScreenWidth();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetBundleIdentifier();
        [DllImport(IOSConstants.DLLImport)] private static extern bool _analyticsEnvironmentGetLimitAdTrackingEnabled();
        [DllImport(IOSConstants.DLLImport)] private static extern int _analyticsEnvironmentGetNetworkConnectionType();
        [DllImport(IOSConstants.DLLImport)] private static extern double _analyticsEnvironmentGetVolume();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetVendorIdentifier();
        [DllImport(IOSConstants.DLLImport)] private static extern int _analyticsEnvironmentGetVendorIdentifierScope();
        [DllImport(IOSConstants.DLLImport)] private static extern int _analyticsEnvironmentGetAuthorizationStatus();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetAppVersion();
        [DllImport(IOSConstants.DLLImport)] private static extern double _analyticsEnvironmentGetAppSessionDuration();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetAppSessionIdentifier();
        [DllImport(IOSConstants.DLLImport)] private static extern bool _analyticsEnvironmentGetIsUserUnderage();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetPublisherSessionIdentifier();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetPublisherAppIdentifier();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetFrameworkName();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetFrameworkVersion();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetPlayerIdentifier();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetUserAgent();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetAdvertisingIdentifier();
    }
    #nullable disable
}
