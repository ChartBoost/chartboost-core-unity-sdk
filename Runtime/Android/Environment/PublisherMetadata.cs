using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;

namespace Chartboost.Core.Android.Environment
{
    #nullable enable
    internal sealed class PublisherMetadata : BaseAndroidEnvironment, IPublisherMetadata
    {
        protected override string EnvironmentProperty => AndroidConstants.PublisherMetadata;

        public void SetIsUserUnderage(bool isUserUnderage) => SetProperty(AndroidConstants.SetPropertyIsUserUnderAge, isUserUnderage);
        public void SetPublisherSessionIdentifier(string? publisherSessionIdentifier) => SetProperty(AndroidConstants.SetPropertyPublisherSessionIdentifier, publisherSessionIdentifier);
        public void SetPublisherAppIdentifier(string? publisherAppIdentifier) => SetProperty(AndroidConstants.SetPropertyPublisherAppIdentifier, publisherAppIdentifier);
        public void SetFrameworkName(string? frameworkName) => SetProperty(AndroidConstants.SetPropertyFrameworkName, frameworkName);
        public void SetFrameworkVersion(string? frameworkVersion) => SetProperty(AndroidConstants.SetPropertyFrameworkVersion, frameworkVersion);
        public void SetPlayerIdentifier(string? playerIdentifier) => SetProperty(AndroidConstants.SetPropertyPlayerIdentifier, playerIdentifier);
    }
    #nullable disable
}
