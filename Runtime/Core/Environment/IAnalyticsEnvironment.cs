namespace Chartboost.Core.Environment
{
    #nullable enable
    public interface IAnalyticsEnvironment : IAttributionEnvironment, IAdvertisingEnvironment
    {
        NetworkConnectionType NetworkConnectionType { get; }
        double? Volume { get; }
        string? VendorIdentifier { get; }
        VendorIdentifierScope VendorIdentifierScope { get; }
        AuthorizationStatus AppTrackingTransparencyStatus { get; }
        string? AppVersion { get; }
        double AppSessionDuration { get; }
        string? AppSessionIdentifier { get; }
        bool? IsUserUnderage { get; }
        string? PublisherSessionIdentifier { get; }
        string? PublisherAppIdentifier { get; }
        string? FrameworkName { get; }
        string? FrameworkVersion { get; }
        string? PlayerIdentifier { get; }
        new string? AdvertisingIdentifier { get; }
    }
    #nullable disable
}
