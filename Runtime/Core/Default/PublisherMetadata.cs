using System;
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
        internal static bool? IsUserUnderAge = false;
        internal static string PublisherSessionIdentifier;
        internal static string PublisherAppIdentifier;
        internal static string FrameworkName;
        internal static string FrameworkVersion;
        internal static string PlayerIdentifier;
        
        /// <inheritdoc cref="IPublisherMetadata.SetIsUserUnderage"/>
        public void SetIsUserUnderage(bool isUserUnderage)
        {
            if (isUserUnderage == IsUserUnderAge)
                return;
            IsUserUnderAge = isUserUnderage;
            AnalyticsEnvironment.OnIsUserUnderageChanged();
        }

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherSessionIdentifier"/>
        public void SetPublisherSessionIdentifier(string publisherSessionIdentifier) 
            => SetProperty(ref PublisherSessionIdentifier, publisherSessionIdentifier, AnalyticsEnvironment.OnPublisherSessionIdentifierChanged);

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherAppIdentifier"/>
        public void SetPublisherAppIdentifier(string publisherAppIdentifier) 
            => SetProperty(ref PublisherAppIdentifier, publisherAppIdentifier, AnalyticsEnvironment.OnPublisherAppIdentifierChanged);

        /// <inheritdoc cref="IPublisherMetadata.SetFramework"/>
        public void SetFramework(string frameworkName, string frameworkVersion)
        {
            SetProperty(ref FrameworkName, frameworkName, AnalyticsEnvironment.OnFrameworkNameChanged);
            SetProperty(ref FrameworkVersion, frameworkVersion, AnalyticsEnvironment.OnFrameworkVersionChanged);
        }

        /// <inheritdoc cref="IPublisherMetadata.SetPlayerIdentifier"/>
        public void SetPlayerIdentifier(string playerIdentifier) 
            => SetProperty(ref PlayerIdentifier, playerIdentifier, AnalyticsEnvironment.OnPlayerIdentifierChanged);

        private static void SetProperty(ref string property, string value, Action onChanged)
        {
            if (property == value)
                return;
            property = value;
            onChanged?.Invoke();
        }
    }
}
