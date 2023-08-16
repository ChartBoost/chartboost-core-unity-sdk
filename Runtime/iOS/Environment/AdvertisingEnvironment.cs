using System.Runtime.InteropServices;
using Chartboost.Core.Environment;
using Chartboost.Core.iOS.Utilities;

namespace Chartboost.Core.iOS.Environment
{
    #nullable enable
    public class AdvertisingEnvironment : IAdvertisingEnvironment
    {
        public string OSName => _advertisingEnvironmentGetOsName();
        public string OSVersion => _advertisingEnvironmentGetOsVersion();
        public string DeviceMake => _advertisingEnvironmentGetDeviceMake();
        public string DeviceModel => _advertisingEnvironmentGetDeviceModel();
        public string? DeviceLocale => _advertisingEnvironmentGetDeviceLocale();
        public double? ScreenHeight => _advertisingEnvironmentGetScreenHeight();
        public double? ScreenScale => _advertisingEnvironmentGetScreenScale();
        public double? ScreenWidth => _advertisingEnvironmentGetScreenWidth();
        public string? BundleIdentifier => _advertisingEnvironmentGetBundleIdentifier();
        public bool? LimitAdTrackingEnabled => _advertisingEnvironmentGetLimitAdTrackingEnabled();
        public string? AdvertisingIdentifier => _advertisingEnvironmentGetAdvertisingIdentifier();
        
        [DllImport(IOSConstants.DLLImport)] private static extern string _advertisingEnvironmentGetOsName();
        [DllImport(IOSConstants.DLLImport)] private static extern string _advertisingEnvironmentGetOsVersion();
        [DllImport(IOSConstants.DLLImport)] private static extern string _advertisingEnvironmentGetDeviceMake();
        [DllImport(IOSConstants.DLLImport)] private static extern string _advertisingEnvironmentGetDeviceModel();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _advertisingEnvironmentGetDeviceLocale();
        [DllImport(IOSConstants.DLLImport)] private static extern double _advertisingEnvironmentGetScreenHeight();
        [DllImport(IOSConstants.DLLImport)] private static extern double _advertisingEnvironmentGetScreenScale();
        [DllImport(IOSConstants.DLLImport)] private static extern double _advertisingEnvironmentGetScreenWidth();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _advertisingEnvironmentGetBundleIdentifier();
        [DllImport(IOSConstants.DLLImport)] private static extern bool _advertisingEnvironmentGetLimitAdTrackingEnabled();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _advertisingEnvironmentGetAdvertisingIdentifier();
    }
    #nullable enable
}
