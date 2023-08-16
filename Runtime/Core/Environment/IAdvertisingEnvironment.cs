namespace Chartboost.Core.Environment
{
    #nullable enable
    public interface IAdvertisingEnvironment
    {
        string OSName { get; }
        string OSVersion { get; }
        string DeviceMake { get; }
        string DeviceModel { get; }
        string? DeviceLocale { get; }
        double? ScreenHeight { get; }
        double? ScreenScale { get; }
        double? ScreenWidth { get; }
        string? BundleIdentifier { get; }
        bool? LimitAdTrackingEnabled { get; }
        string? AdvertisingIdentifier { get; }
    }
    #nullable disable
}
