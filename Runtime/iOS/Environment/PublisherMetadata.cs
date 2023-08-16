using System.Runtime.InteropServices;
using Chartboost.Core.Environment;
using Chartboost.Core.iOS.Utilities;

namespace Chartboost.Core.iOS.Environment
{
    public class PublisherMetadata : IPublisherMetadata
    {
        public void SetIsUserUnderage(bool isUserUnderage) 
            => _publisherMetadataSetIsUserUnderage(isUserUnderage);

        public void SetPublisherSessionIdentifier(string publisherSessionIdentifier) 
            => _publisherMetadataSetPublisherSessionIdentifier(publisherSessionIdentifier);

        public void SetPublisherAppIdentifier(string publisherAppIdentifier) 
            => _publisherMetadataSetPublisherAppIdentifier(publisherAppIdentifier);

        public void SetFrameworkName(string frameworkName) 
            => _publisherMetadataSetFrameworkName(frameworkName);

        public void SetFrameworkVersion(string frameworkVersion) 
            => _publisherMetadataSetFrameworkVersion(frameworkVersion);

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
