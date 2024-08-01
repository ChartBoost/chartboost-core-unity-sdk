using Chartboost.Core.Consent;
using Chartboost.Core.Environment;
using Chartboost.Core.Initialization;
using Chartboost.Logging;

namespace Chartboost.Core
{
    internal abstract class ChartboostCoreBase
    {
        /// <inheritdoc cref="ChartboostCore.NativeVersion"/>
        public abstract string NativeVersion { get; }
        
        /// <inheritdoc cref="ChartboostCore.LogLevel"/>
        public abstract LogLevel LogLevel { get; set; }
        
        /// <inheritdoc cref="ChartboostCore.Consent"/>
        public abstract IConsentManagementPlatform Consent { get; }
        
        /// <inheritdoc cref="ChartboostCore.PublisherMetadata"/>
        public abstract IPublisherMetadata PublisherMetadata { get; }
        
        /// <inheritdoc cref="ChartboostCore.AdvertisingEnvironment"/>
        public abstract IAdvertisingEnvironment AdvertisingEnvironment { get; }
        
        /// <inheritdoc cref="ChartboostCore.AnalyticsEnvironment"/>
        public abstract IAnalyticsEnvironment AnalyticsEnvironment { get; }
        
        /// <inheritdoc cref="ChartboostCore.AttributionEnvironment"/>
        public abstract IAttributionEnvironment AttributionEnvironment { get; }
        
        /// <inheritdoc cref="ChartboostCore.Initialize"/>
        public virtual void Initialize(SDKConfiguration sdkConfiguration)
        {
            // TODO - add centralized logging
        }
    }
}
