using System;
using System.Threading.Tasks;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;
using Chartboost.Core.Utilities;
using UnityEngine;

namespace Chartboost.Core.Android.Environment
{
    #nullable enable
    /// <summary>
    /// <para>Android Implementation of <see cref="IAnalyticsEnvironment"/>.</para>
    /// <inheritdoc cref="IAnalyticsEnvironment"/>
    /// </summary>
    internal class AnalyticsEnvironment : BaseAndroidEnvironment, IAnalyticsEnvironment
    {
        /// <inheritdoc cref="BaseAndroidEnvironment.EnvironmentProperty"/>
        protected override string EnvironmentProperty => AndroidConstants.EnvironmentAnalytics;
        
        /// <inheritdoc cref="BaseAndroidEnvironment.EnvironmentBridge"/>
        protected override Func<AndroidJavaClass> EnvironmentBridge => AndroidUtils.AnalyticsBridge;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.OSName"/>
        public string OSName => Property<string>(AndroidConstants.GetPropertyOSName);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.OSVersion"/>
        public string OSVersion => Property<string>(AndroidConstants.GetPropertyOSVersion);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceMake"/>
        public string DeviceMake => Property<string>(AndroidConstants.GetPropertyDeviceMake);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceModel"/>
        public string DeviceModel => Property<string>(AndroidConstants.GetPropertyDeviceModel);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.DeviceLocale"/>
        public string? DeviceLocale => Property<string?>(AndroidConstants.GetPropertyDeviceLocale);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenHeight"/>
        public double? ScreenHeight => DoubleProperty(AndroidConstants.GetPropertyScreenHeight);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenScale"/>
        public double? ScreenScale => DoubleProperty(AndroidConstants.GetPropertyScreenScale);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.ScreenWidth"/>
        public double? ScreenWidth => DoubleProperty(AndroidConstants.GetPropertyScreenWidth);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.BundleIdentifier"/>
        public string? BundleIdentifier => Property<string?>(AndroidConstants.GetPropertyBundleIdentifier);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.LimitAdTrackingEnabled"/>
        public Task<bool?> LimitAdTrackingEnabled => AwaitableBoolean(AndroidConstants.GetPropertyLimitAdTrackingEnabled);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => AwaitableString(AndroidConstants.GetPropertyAdvertisingIdentifier);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.UserAgent"/>
        public Task<string?> UserAgent => AwaitableString(AndroidConstants.GetPropertyUserAgent);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.NetworkConnectionType"/>
        public NetworkConnectionType NetworkConnectionType => EnumPropertyAsString(AndroidConstants.GetPropertyNetworkConnectionType).NetworkConnectionType();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.Volume"/>
        public double? Volume => DoubleProperty(AndroidConstants.GetPropertyVolume);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.VendorIdentifier"/>
        public Task<string?> VendorIdentifier => AwaitableString(AndroidConstants.GetPropertyVendorIdentifier);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.VendorIdentifierScope"/>
        public Task<VendorIdentifierScope> VendorIdentifierScope => AwaitableVendorIdentifierScope();
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppTrackingTransparencyStatus"/>
        public AuthorizationStatus AppTrackingTransparencyStatus => AuthorizationStatus.Unsupported;
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppVersion"/>
        public string? AppVersion => Property<string?>(AndroidConstants.GetPropertyAppVersion);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppSessionDuration"/>
        public double AppSessionDuration => Property<double>(AndroidConstants.GetPropertyAppSessionDuration);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.AppSessionIdentifier"/>
        public string? AppSessionIdentifier => Property<string?>(AndroidConstants.GetPropertyAppSessionIdentifier);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.IsUserUnderage"/>
        public bool? IsUserUnderage => BoolProperty(AndroidConstants.GetPropertyIsUserUnderAge);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PublisherSessionIdentifier"/>
        public string? PublisherSessionIdentifier => Property<string?>(AndroidConstants.GetPropertyPublisherSessionIdentifier);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PublisherAppIdentifier"/>
        public string? PublisherAppIdentifier => Property<string?>(AndroidConstants.GetPropertyPublisherAppIdentifier);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.FrameworkName"/>
        public string? FrameworkName => Property<string?>(AndroidConstants.GetPropertyFrameworkName);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.FrameworkVersion"/>
        public string? FrameworkVersion => Property<string?>(AndroidConstants.GetPropertyFrameworkVersion);
        
        /// <inheritdoc cref="IAnalyticsEnvironment.PlayerIdentifier"/>
        public string? PlayerIdentifier => Property<string?>(AndroidConstants.GetPropertyPlayerIdentifier);
    }
    #nullable disable
}
