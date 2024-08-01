using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Constants;
using Chartboost.Core.Environment;

namespace Chartboost.Core.iOS.Environment
{
    #nullable enable
    /// <summary>
    /// <para>iOS Implementation of <see cref="IAdvertisingEnvironment"/>.</para>
    /// <inheritdoc cref="IAdvertisingEnvironment"/>
    /// </summary>
    internal class AdvertisingEnvironment : BaseIOSEnvironment, IAdvertisingEnvironment
    {
        /// <inheritdoc cref="IAdvertisingEnvironment.OSName"/>
        public string OSName => _CBCAdvertisingGetOsName();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.OSVersion"/>
        public string OSVersion => _CBCAdvertisingGetOsVersion();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceMake"/>
        public string DeviceMake => _CBCAdvertisingGetDeviceMake();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceModel"/>
        public string DeviceModel => _CBCAdvertisingGetDeviceModel();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceLocale"/>
        public string? DeviceLocale => _CBCAdvertisingGetDeviceLocale();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenHeightPixels"/>
        public double? ScreenHeightPixels => _CBCAdvertisingGetScreenHeight();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenScale"/>
        public double? ScreenScale => _CBCAdvertisingGetScreenScale();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenWidthPixels"/>
        public double? ScreenWidthPixels => _CBCAdvertisingGetScreenWidth();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.BundleIdentifier"/>
        public string? BundleIdentifier => _CBCAdvertisingGetBundleIdentifier();
        
        /// <inheritdoc cref="IAdvertisingEnvironment.LimitAdTrackingEnabled"/>
        public Task<bool?> LimitAdTrackingEnabled => Task.FromResult<bool?>(_CBCAdvertisingGetLimitAdTrackingEnabled());
        
        /// <inheritdoc cref="IAdvertisingEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => Task.FromResult(_CBCAdvertisingGetAdvertisingIdentifier());
        
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCAdvertisingGetOsName();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCAdvertisingGetOsVersion();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCAdvertisingGetDeviceMake();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCAdvertisingGetDeviceModel();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAdvertisingGetDeviceLocale();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern double _CBCAdvertisingGetScreenHeight();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern double _CBCAdvertisingGetScreenScale();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern double _CBCAdvertisingGetScreenWidth();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAdvertisingGetBundleIdentifier();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern bool _CBCAdvertisingGetLimitAdTrackingEnabled();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAdvertisingGetAdvertisingIdentifier();
    }
    #nullable enable
}
