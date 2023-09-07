using System;
using System.Threading.Tasks;
using Chartboost.Core.Environment;
using UnityEngine;

namespace Chartboost.Core.Default
{
    #nullable enable
    /// <summary>
    /// <inheritdoc cref="IAnalyticsEnvironment"/>
    /// <br/>
    /// <para> Default class implementation for unsupported platforms.</para>
    /// </summary>
    internal class AnalyticsEnvironment : IAnalyticsEnvironment
    {
        public AnalyticsEnvironment()
        {
            var (osName, osVersion) = AdvertisingEnvironment.FetchOSInfo();
            OSName = osName;
            OSVersion = osVersion;
            DeviceMake = AdvertisingEnvironment.FetchDeviceMake();
        }

        /// <inheritdoc cref="IAnalyticsEnvironment.OSName"/>
        public string OSName { get; }
        
        /// <inheritdoc cref="IAnalyticsEnvironment.OSVersion"/>
        public string OSVersion { get; }
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceMake"/>
        public string DeviceMake { get; }
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceModel"/>
        public string DeviceModel => SystemInfo.deviceModel;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceLocale"/>
        public string DeviceLocale => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenHeight"/>
        public double? ScreenHeight => Screen.height; 
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenScale"/>
        public double? ScreenScale => Screen.dpi;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenWidth"/>
        public double? ScreenWidth => Screen.width;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.BundleIdentifier"/>
        public string BundleIdentifier => Application.identifier;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.LimitAdTrackingEnabled"/>
        public Task<bool?> LimitAdTrackingEnabled => Task.FromResult<bool?>(null);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.NetworkConnectionType"/>
        public NetworkConnectionType NetworkConnectionType => NetworkConnectionType.Unknown;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.Volume"/>
        public double? Volume => null;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.VendorIdentifier"/>
        public Task<string?> VendorIdentifier => Task.FromResult<string?>(null);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.VendorIdentifierScope"/>
        public Task<VendorIdentifierScope> VendorIdentifierScope =>  Task.FromResult(Environment.VendorIdentifierScope.Unknown);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppTrackingTransparencyStatus"/>
        public AuthorizationStatus AppTrackingTransparencyStatus => AuthorizationStatus.Unsupported;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppVersion"/>
        public string AppVersion => Application.version;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppSessionDuration"/>
        public double AppSessionDuration => 0;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppSessionIdentifier"/>
        public string AppSessionIdentifier { get; } = Guid.NewGuid().ToString();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.IsUserUnderage"/>
        public bool? IsUserUnderage => PublisherMetadata.IsUserUnderAge;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PublisherSessionIdentifier"/>
        public string PublisherSessionIdentifier => PublisherMetadata.PublisherSessionIdentifier;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PublisherAppIdentifier"/>
        public string PublisherAppIdentifier => PublisherMetadata.PublisherAppIdentifier;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.FrameworkName"/>
        public string FrameworkName => PublisherMetadata.FrameworkName;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.FrameworkVersion"/>
        public string FrameworkVersion => PublisherMetadata.FrameworkVersion;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PlayerIdentifier"/>
        public string PlayerIdentifier => PublisherMetadata.PlayerIdentifier;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => Task.FromResult<string?>(null);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.UserAgent"/>
        public Task<string?> UserAgent => Task.FromResult<string?>(null);
    }
    #nullable disable
}
