using System.Threading.Tasks;

namespace Chartboost.Core.Environment
{
    #nullable enable
    /// <summary>
    /// An environment that contains information intended solely for analytics purposes.
    /// </summary>
    public interface IAnalyticsEnvironment : IAttributionEnvironment, IAdvertisingEnvironment
    {
        /// <summary>
        /// The current network connection type, e.g. wifi.
        /// </summary>
        NetworkConnectionType NetworkConnectionType { get; }
        
        /// <summary>
        /// The device volume level.
        /// </summary>
        double? Volume { get; }
        
        /// <summary>
        /// The system identifier for vendor (IFV).
        /// </summary>
        Task<string?> VendorIdentifier { get; }
        
        /// <summary>
        /// The scope of the identifier for vendor.
        /// </summary>
        Task<VendorIdentifierScope> VendorIdentifierScope { get; }
        
        /// <summary>
        /// The tracking authorization status, as determined by the system’s AppTrackingTransparency framework.
        /// Requires iOS 14.0+.
        /// Only supported in iOS devices, other platforms default to Unsupported.
        /// </summary>
        AuthorizationStatus AppTrackingTransparencyStatus { get; }
        
        /// <summary>
        /// The version of the app.
        /// </summary>
        string? AppVersion { get; }
        
        /// <summary>
        /// The session duration, or 0 if the <see cref="ChartboostCore.Initialize"/> method has not been called yet.
        /// A session starts the moment <see cref="ChartboostCore.Initialize"/> is called for the first time, or when the user consent status changes from
        /// <see cref="Chartboost.Core.Consent.ConsentStatus.Granted"/> to any other status.
        /// </summary>
        double AppSessionDuration { get; }
        
        /// <summary>
        /// The session identifier, or null if the <see cref="ChartboostCore.Initialize"/> method has not been called yet.
        /// A session starts the moment <see cref="ChartboostCore.Initialize"/> is called for the first time, or when the user consent status changes from
        /// <see cref="Chartboost.Core.Consent.ConsentStatus.Granted"/> to any other status.
        /// </summary>
        string? AppSessionIdentifier { get; }
        
        /// <summary>
        /// Indicates whether the user is underage.
        /// This is determined by the latest value set by the publisher through a call to <see cref="IPublisherMetadata.SetIsUserUnderage"/>, as well as by the “child-directed” option defined on the Chartboost Core dashboard.
        /// The more restrictive option is used.
        /// </summary>
        bool? IsUserUnderage { get; }

        /// <summary>
        /// The publisher-defined session identifier set by the publisher through a call to <see cref="IPublisherMetadata.SetPublisherSessionIdentifier"/>.
        /// </summary>
        string? PublisherSessionIdentifier { get; }
        
        /// <summary>
        /// The publisher-defined application identifier set by the publisher through a call to <see cref="IPublisherMetadata.SetPublisherAppIdentifier"/>.
        /// </summary>
        string? PublisherAppIdentifier { get; }
        
        /// <summary>
        /// The framework name set by the publisher through a call to <see cref="IPublisherMetadata.SetFrameworkName"/>.
        /// </summary>
        string? FrameworkName { get; }
        
        /// <summary>
        /// The framework version set by the publisher through a call to <see cref="IPublisherMetadata.SetFrameworkVersion"/>.
        /// </summary>
        string? FrameworkVersion { get; }
        
        /// <summary>
        /// The player identifier set by the publisher through a call to <see cref="IPublisherMetadata.SetPlayerIdentifier"/>.
        /// </summary>
        string? PlayerIdentifier { get; }
        
        /// <summary>
        /// The system advertising identifier (IFA).
        /// </summary>
        new Task<string?> AdvertisingIdentifier { get; }
    }
    #nullable disable
}
