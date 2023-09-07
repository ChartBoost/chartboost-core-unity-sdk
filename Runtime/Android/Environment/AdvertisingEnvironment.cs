using System;
using System.Threading.Tasks;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;
using UnityEngine;

namespace Chartboost.Core.Android.Environment
{
    #nullable enable
    /// <summary>
    /// <para>Android Implementation of <see cref="IAdvertisingEnvironment"/>.</para>
    /// <inheritdoc cref="IAdvertisingEnvironment"/>
    /// </summary>
    internal class AdvertisingEnvironment : BaseAndroidEnvironment, IAdvertisingEnvironment
    {
        /// <inheritdoc cref="BaseAndroidEnvironment.EnvironmentProperty"/>
        protected override string EnvironmentProperty => AndroidConstants.EnvironmentAdvertising;
        
        /// <inheritdoc cref="BaseAndroidEnvironment.EnvironmentBridge"/>
        protected override Func<AndroidJavaClass> EnvironmentBridge => AndroidUtils.AdvertisingBridge;
        
        /// <inheritdoc cref="IAdvertisingEnvironment.OSName"/>
        public string OSName => Property<string>(AndroidConstants.GetPropertyOSName);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.OSVersion"/>
        public string OSVersion => Property<string>(AndroidConstants.GetPropertyOSVersion);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceMake"/>
        public string DeviceMake => Property<string>(AndroidConstants.GetPropertyDeviceMake);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceModel"/>
        public string DeviceModel => Property<string>(AndroidConstants.GetPropertyDeviceModel);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceLocale"/>
        public string? DeviceLocale => Property<string>(AndroidConstants.GetPropertyDeviceLocale);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenHeight"/>
        public double? ScreenHeight => DoubleProperty(AndroidConstants.GetPropertyScreenHeight);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenScale"/>
        public double? ScreenScale => DoubleProperty(AndroidConstants.GetPropertyScreenScale);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenWidth"/>
        public double? ScreenWidth => DoubleProperty(AndroidConstants.GetPropertyScreenWidth);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.BundleIdentifier"/>
        public string? BundleIdentifier => Property<string?>(AndroidConstants.GetPropertyBundleIdentifier);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.LimitAdTrackingEnabled"/>
        public Task<bool?> LimitAdTrackingEnabled => AwaitableBoolean(AndroidConstants.GetPropertyLimitAdTrackingEnabled);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => AwaitableString(AndroidConstants.GetPropertyAdvertisingIdentifier);
    }
    #nullable disable
}
