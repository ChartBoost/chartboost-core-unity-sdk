namespace Chartboost.Core.Environment
{
    #nullable enable
    public interface IAttributionEnvironment
    {
        string? AdvertisingIdentifier { get; }
        string? UserAgent { get; }
    }
    #nullable disable
}
