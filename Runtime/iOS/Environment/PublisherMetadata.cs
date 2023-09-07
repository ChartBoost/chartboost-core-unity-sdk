using System.Runtime.InteropServices;
using Chartboost.Core.Environment;
using Chartboost.Core.iOS.Utilities;

namespace Chartboost.Core.iOS.Environment
{
    /// <summary>
    /// <para>iOS Implementation of <see cref="IPublisherMetadata"/>.</para>
    /// <inheritdoc cref="IPublisherMetadata"/>
    /// </summary>
    public class PublisherMetadata : IPublisherMetadata
    {
        /// <inheritdoc cref="IPublisherMetadata.SetIsUserUnderage"/>
        public void SetIsUserUnderage(bool isUserUnderage) 
            => _publisherMetadataSetIsUserUnderage(isUserUnderage);

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherSessionIdentifier"/>
        public void SetPublisherSessionIdentifier(string publisherSessionIdentifier) 
            => _publisherMetadataSetPublisherSessionIdentifier(publisherSessionIdentifier);

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherAppIdentifier"/>
        public void SetPublisherAppIdentifier(string publisherAppIdentifier) 
            => _publisherMetadataSetPublisherAppIdentifier(publisherAppIdentifier);

        /// <inheritdoc cref="IPublisherMetadata.SetFrameworkName"/>
        public void SetFrameworkName(string frameworkName) 
            => _publisherMetadataSetFrameworkName(frameworkName);

        /// <inheritdoc cref="IPublisherMetadata.SetFrameworkVersion"/>
        public void SetFrameworkVersion(string frameworkVersion) 
            => _publisherMetadataSetFrameworkVersion(frameworkVersion);

        /// <inheritdoc cref="IPublisherMetadata.SetPlayerIdentifier"/>
        public void SetPlayerIdentifier(string playerIdentifier) 
            => _publisherMetadataPlayerIdentifier(playerIdentifier);

        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetIsUserUnderage(bool isUnderage);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetPublisherSessionIdentifier(string publisherSessionIdentifier);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetPublisherAppIdentifier(string publisherAppIdentifier);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetFrameworkName(string frameworkName);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetFrameworkVersion(string frameworkVersion);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataPlayerIdentifier(string playerIdentifier);
    }
}
