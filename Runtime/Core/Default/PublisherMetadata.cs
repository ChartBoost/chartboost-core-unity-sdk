using Chartboost.Core.Environment;

namespace Chartboost.Core.Default
{
    /// <summary>
    /// <inheritdoc cref="IPublisherMetadata"/>
    /// <br/>
    /// <para> Default class implementation for unsupported platforms.</para>
    /// </summary>
    internal class PublisherMetadata : IPublisherMetadata
    {
        internal static bool IsUserUnderAge;
        internal static string PublisherSessionIdentifier;
        internal static string PublisherAppIdentifier;
        internal static string FrameworkName;
        internal static string FrameworkVersion;
        internal static string PlayerIdentifier;

        /// <inheritdoc cref="IPublisherMetadata.SetIsUserUnderage"/>
        public void SetIsUserUnderage(bool isUserUnderage) 
            => IsUserUnderAge = isUserUnderage;

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherSessionIdentifier"/>
        public void SetPublisherSessionIdentifier(string publisherSessionIdentifier)
            => PublisherSessionIdentifier = publisherSessionIdentifier;

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherAppIdentifier"/>
        public void SetPublisherAppIdentifier(string publisherAppIdentifier) 
            => PublisherAppIdentifier = publisherAppIdentifier;

        /// <inheritdoc cref="IPublisherMetadata.SetFrameworkName"/>
        public void SetFrameworkName(string frameworkName)
            => FrameworkName = frameworkName;

        /// <inheritdoc cref="IPublisherMetadata.SetFrameworkVersion"/>
        public void SetFrameworkVersion(string frameworkVersion) 
            => FrameworkVersion = frameworkVersion;

        /// <inheritdoc cref="IPublisherMetadata.SetPlayerIdentifier"/>
        public void SetPlayerIdentifier(string playerIdentifier) 
            => PlayerIdentifier = playerIdentifier;
    }
}
