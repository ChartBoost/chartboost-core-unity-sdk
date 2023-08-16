namespace Chartboost.Core.Environment
{
    #nullable enable
    public interface IPublisherMetadata
    {
        void SetIsUserUnderage(bool isUserUnderage);
        void SetPublisherSessionIdentifier(string? publisherSessionIdentifier);
        void SetPublisherAppIdentifier(string? publisherAppIdentifier);
        void SetFrameworkName(string? frameworkName);
        void SetFrameworkVersion(string? frameworkVersion);
        void SetPlayerIdentifier(string? playerIdentifier);
    }
    #nullable disable
}
