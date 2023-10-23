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

        public event Action IsUserUnderageChanged;
        public event Action PublisherSessionIdentifierChanged;
        public event Action PublisherAppIdentifierChanged;
        public event Action FrameworkNameChanged;
        public event Action FrameworkVersionChanged;
        public event Action PlayerIdentifierChanged;

        /// <inheritdoc cref="IPublisherMetadata.SetIsUserUnderage"/>
        public void SetIsUserUnderage(bool isUserUnderage)
        {
            if (isUserUnderage == IsUserUnderAge)
                return;
            IsUserUnderAge = isUserUnderage;
            IsUserUnderageChanged?.Invoke();
        }

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherSessionIdentifier"/>
        public void SetPublisherSessionIdentifier(string publisherSessionIdentifier) 
            => SetProperty(ref PublisherSessionIdentifier, publisherSessionIdentifier, PublisherSessionIdentifierChanged);

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherAppIdentifier"/>
        public void SetPublisherAppIdentifier(string publisherAppIdentifier) 
            => SetProperty(ref PublisherAppIdentifier, publisherAppIdentifier, PublisherAppIdentifierChanged);

        /// <inheritdoc cref="IPublisherMetadata.SetFrameworkName"/>
        public void SetFrameworkName(string frameworkName)
            => SetProperty(ref FrameworkName, frameworkName, FrameworkNameChanged);

        /// <inheritdoc cref="IPublisherMetadata.SetFrameworkVersion"/>
        public void SetFrameworkVersion(string frameworkVersion) 
            => SetProperty(ref FrameworkVersion, frameworkVersion, FrameworkVersionChanged);

        /// <inheritdoc cref="IPublisherMetadata.SetPlayerIdentifier"/>
        public void SetPlayerIdentifier(string playerIdentifier) 
            => SetProperty(ref PlayerIdentifier, playerIdentifier, PlayerIdentifierChanged);

        private static void SetProperty(ref string property, string value, Action onChanged)
        {
            if (property == value)
                return;
            property = value;
            onChanged?.Invoke();
        }
    }
}
