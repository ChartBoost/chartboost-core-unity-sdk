using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Core.Environment;
using Chartboost.Core.iOS.Utilities;

namespace Chartboost.Core.iOS.Environment
{
    #nullable enable
    /// <summary>
    /// <para>iOS Implementation of <see cref="IAnalyticsEnvironment"/>.</para>
    /// <inheritdoc cref="IAnalyticsEnvironment"/>
    /// </summary>
    public class AnalyticsEnvironment : BaseIOSEnvironment, IAnalyticsEnvironment
    {
        /// <inheritdoc cref="IAnalyticsEnvironment.OSName"/>
        public string OSName => _analyticsEnvironmentGetOsName();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.OSVersion"/>
        public string OSVersion => _analyticsEnvironmentGetOsVersion();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceMake"/>
        public string DeviceMake => _analyticsEnvironmentGetDeviceMake();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceModel"/>
        public string DeviceModel => _analyticsEnvironmentGetDeviceModel();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceLocale"/>
        public string? DeviceLocale => _analyticsEnvironmentGetDeviceLocale();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenHeight"/>
        public double? ScreenHeight => _analyticsEnvironmentGetScreenHeight();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenScale"/>
        public double? ScreenScale => _analyticsEnvironmentGetScreenScale();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenWidth"/>
        public double? ScreenWidth => _analyticsEnvironmentGetScreenWidth();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.BundleIdentifier"/>
        public string? BundleIdentifier => _analyticsEnvironmentGetBundleIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.LimitAdTrackingEnabled"/>
        public Task<bool?> LimitAdTrackingEnabled => Task.FromResult<bool?>(_analyticsEnvironmentGetLimitAdTrackingEnabled());
       
        /// <inheritdoc cref="IAnalyticsEnvironment.NetworkConnectionType"/>
        public NetworkConnectionType NetworkConnectionType => (NetworkConnectionType)_analyticsEnvironmentGetNetworkConnectionType();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.Volume"/>
        public double? Volume => _analyticsEnvironmentGetVolume();
                
        /// <inheritdoc cref="IAnalyticsEnvironment.VendorIdentifier"/>
        public Task<string?> VendorIdentifier => Task.FromResult(_analyticsEnvironmentGetVendorIdentifier());
        
        /// <inheritdoc cref="IAnalyticsEnvironment.VendorIdentifierScope"/>
        public Task<VendorIdentifierScope> VendorIdentifierScope => Task.FromResult((VendorIdentifierScope)_analyticsEnvironmentGetVendorIdentifierScope());
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppTrackingTransparencyStatus"/>
        public AuthorizationStatus AppTrackingTransparencyStatus => (AuthorizationStatus)_analyticsEnvironmentGetAuthorizationStatus();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppVersion"/>
        public string? AppVersion => _analyticsEnvironmentGetAppVersion();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppSessionDuration"/>
        public double AppSessionDuration => _analyticsEnvironmentGetAppSessionDuration();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppSessionIdentifier"/>
        public string? AppSessionIdentifier => _analyticsEnvironmentGetAppSessionIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.IsUserUnderage"/>
        public bool? IsUserUnderage => _analyticsEnvironmentGetIsUserUnderage();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PublisherSessionIdentifier"/>
        public string? PublisherSessionIdentifier => _analyticsEnvironmentGetPublisherSessionIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PublisherAppIdentifier"/>
        public string? PublisherAppIdentifier => _analyticsEnvironmentGetPublisherAppIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.FrameworkName"/>
        public string? FrameworkName => _analyticsEnvironmentGetFrameworkName();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.FrameworkVersion"/>
        public string? FrameworkVersion => _analyticsEnvironmentGetFrameworkVersion();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PlayerIdentifier"/>
        public string? PlayerIdentifier => _analyticsEnvironmentGetPlayerIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => Task.FromResult(_analyticsEnvironmentGetAdvertisingIdentifier());
        
        /// <inheritdoc cref="IAnalyticsEnvironment.UserAgent"/>
        public Task<string?> UserAgent => AwaitableString(_analyticsEnvironmentGetUserAgent);
        
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
        [DllImport(IOSConstants.DLLImport)] private static extern void _analyticsEnvironmentGetUserAgent(int hashCode, ChartboostCoreOnResultString callback);
        [DllImport(IOSConstants.DLLImport)] private static extern string? _analyticsEnvironmentGetAdvertisingIdentifier();
    }
    #nullable disable
}
