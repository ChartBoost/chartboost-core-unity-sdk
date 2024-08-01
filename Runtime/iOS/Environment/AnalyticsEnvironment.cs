using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Constants;
using Chartboost.Core.Environment;

namespace Chartboost.Core.iOS.Environment
{
    #nullable enable
    /// <summary>
    /// <para>iOS Implementation of <see cref="IAnalyticsEnvironment"/>.</para>
    /// <inheritdoc cref="IAnalyticsEnvironment"/>
    /// </summary>
    internal partial class AnalyticsEnvironment : BaseIOSEnvironment, IAnalyticsEnvironment
    {
        /// <inheritdoc cref="IAnalyticsEnvironment.OSName"/>
        public string OSName => _CBCAnalyticsGetOsName();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.OSVersion"/>
        public string OSVersion => _CBCAnalyticsGetOsVersion();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceMake"/>
        public string DeviceMake => _CBCAnalyticsGetDeviceMake();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceModel"/>
        public string DeviceModel => _CBCAnalyticsGetDeviceModel();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceLocale"/>
        public string? DeviceLocale => _CBCAnalyticsGetDeviceLocale();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenHeightPixels"/>
        public double? ScreenHeightPixels => _CBCAnalyticsGetScreenHeight();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenScale"/>
        public double? ScreenScale => _CBCAnalyticsGetScreenScale();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenWidthPixels"/>
        public double? ScreenWidthPixels => _CBCAnalyticsGetScreenWidth();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.BundleIdentifier"/>
        public string? BundleIdentifier => _CBCAnalyticsGetBundleIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.LimitAdTrackingEnabled"/>
        public Task<bool?> LimitAdTrackingEnabled => Task.FromResult<bool?>(_CBCAnalyticsGetLimitAdTrackingEnabled());
       
        /// <inheritdoc cref="IAnalyticsEnvironment.NetworkConnectionType"/>
        public NetworkConnectionType NetworkConnectionType => (NetworkConnectionType)_CBCAnalyticsGetNetworkConnectionType();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.Volume"/>
        public double? Volume => _CBCAnalyticsGetVolume();
                
        /// <inheritdoc cref="IAnalyticsEnvironment.VendorIdentifier"/>
        public Task<string?> VendorIdentifier => Task.FromResult(_CBCAnalyticsGetVendorIdentifier());
        
        /// <inheritdoc cref="IAnalyticsEnvironment.VendorIdentifierScope"/>
        public Task<VendorIdentifierScope> VendorIdentifierScope => Task.FromResult((VendorIdentifierScope)_CBCAnalyticsGetVendorIdentifierScope());
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppTrackingTransparencyStatus"/>
        public AuthorizationStatus AppTrackingTransparencyStatus => (AuthorizationStatus)_CBCAnalyticsGetAuthorizationStatus();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppVersion"/>
        public string? AppVersion => _CBCAnalyticsGetAppVersion();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppSessionDuration"/>
        public double AppSessionDuration => _CBCAnalyticsGetAppSessionDuration();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppSessionIdentifier"/>
        public string? AppSessionIdentifier => _CBCAnalyticsGetAppSessionIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.IsUserUnderage"/>
        public bool? IsUserUnderage => _CBCAnalyticsGetIsUserUnderage();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PublisherSessionIdentifier"/>
        public string? PublisherSessionIdentifier => _CBCAnalyticsGetPublisherSessionIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PublisherAppIdentifier"/>
        public string? PublisherAppIdentifier => _CBCAnalyticsGetPublisherAppIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.FrameworkName"/>
        public string? FrameworkName => _CBCAnalyticsGetFrameworkName();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.FrameworkVersion"/>
        public string? FrameworkVersion => _CBCAnalyticsGetFrameworkVersion();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PlayerIdentifier"/>
        public string? PlayerIdentifier => _CBCAnalyticsGetPlayerIdentifier();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => Task.FromResult(_CBCAnalyticsGetAdvertisingIdentifier());

        /// <inheritdoc cref="IAnalyticsEnvironment.UserAgent"/>
        public Task<string?> UserAgent => AwaitableString(_CBCAnalyticsGetUserAgent);
        
        
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCAnalyticsGetOsName();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCAnalyticsGetOsVersion();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCAnalyticsGetDeviceMake();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCAnalyticsGetDeviceModel();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetDeviceLocale();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern double _CBCAnalyticsGetScreenHeight();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern double _CBCAnalyticsGetScreenScale();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern double _CBCAnalyticsGetScreenWidth();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetBundleIdentifier();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern bool _CBCAnalyticsGetLimitAdTrackingEnabled();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern int _CBCAnalyticsGetNetworkConnectionType();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern double _CBCAnalyticsGetVolume();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetVendorIdentifier();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern int _CBCAnalyticsGetVendorIdentifierScope();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern int _CBCAnalyticsGetAuthorizationStatus();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetAppVersion();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern double _CBCAnalyticsGetAppSessionDuration();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetAppSessionIdentifier();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern bool _CBCAnalyticsGetIsUserUnderage();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetPublisherSessionIdentifier();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetPublisherAppIdentifier();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetFrameworkName();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetFrameworkVersion();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetPlayerIdentifier();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCAnalyticsGetUserAgent(int hashCode, ExternChartboostCoreOnResultString callback);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAnalyticsGetAdvertisingIdentifier();
    }
    #nullable disable
}
