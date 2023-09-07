using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Core.Environment;
using Chartboost.Core.iOS.Utilities;

namespace Chartboost.Core.iOS.Environment
{
    #nullable enable
    /// <summary>
    /// <para>iOS Implementation of <see cref="IAdvertisingEnvironment"/>.</para>
    /// <inheritdoc cref="IAdvertisingEnvironment"/>
    /// </summary>
    public class AdvertisingEnvironment : BaseIOSEnvironment, IAdvertisingEnvironment
    {
        /// <inheritdoc cref="IAdvertisingEnvironment.OSName"/>
        public string OSName => _advertisingEnvironmentGetOsName();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.OSVersion"/>
        public string OSVersion => _advertisingEnvironmentGetOsVersion();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceMake"/>
        public string DeviceMake => _advertisingEnvironmentGetDeviceMake();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceModel"/>
        public string DeviceModel => _advertisingEnvironmentGetDeviceModel();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceLocale"/>
        public string? DeviceLocale => _advertisingEnvironmentGetDeviceLocale();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenHeight"/>
        public double? ScreenHeight => _advertisingEnvironmentGetScreenHeight();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenScale"/>
        public double? ScreenScale => _advertisingEnvironmentGetScreenScale();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenWidth"/>
        public double? ScreenWidth => _advertisingEnvironmentGetScreenWidth();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.BundleIdentifier"/>
        public string? BundleIdentifier => _advertisingEnvironmentGetBundleIdentifier();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.LimitAdTrackingEnabled"/>
        public Task<bool?> LimitAdTrackingEnabled => Task.FromResult<bool?>(_advertisingEnvironmentGetLimitAdTrackingEnabled());
        
        /// <inheritdoc cref="IAdvertisingEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => Task.FromResult(_advertisingEnvironmentGetAdvertisingIdentifier());
        
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
