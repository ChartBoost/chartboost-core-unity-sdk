using System.Runtime.InteropServices;
using Chartboost.Constants;
using Chartboost.Core.Environment;

namespace Chartboost.Core.iOS.Environment
{
    /// <summary>
    /// <para>iOS Implementation of <see cref="IPublisherMetadata"/>.</para>
    /// <inheritdoc cref="IPublisherMetadata"/>
    /// </summary>
    internal class PublisherMetadata : IPublisherMetadata
    {
        /// <inheritdoc cref="IPublisherMetadata.SetIsUserUnderage"/>
        public void SetIsUserUnderage(bool isUserUnderage) 
            => _CBCSetIsUserUnderage(isUserUnderage);

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherSessionIdentifier"/>
        public void SetPublisherSessionIdentifier(string publisherSessionIdentifier) 
            => _CBCSetPublisherSessionIdentifier(publisherSessionIdentifier);

        /// <inheritdoc cref="IPublisherMetadata.SetPublisherAppIdentifier"/>
        public void SetPublisherAppIdentifier(string publisherAppIdentifier) 
            => _CBCSetPublisherAppIdentifier(publisherAppIdentifier);

        /// <inheritdoc cref="IPublisherMetadata.SetFramework"/>
        public void SetFramework(string frameworkName, string frameworkVersion)
            => _CBCSetFramework(frameworkName, frameworkVersion);

        /// <inheritdoc cref="IPublisherMetadata.SetPlayerIdentifier"/>
        public void SetPlayerIdentifier(string playerIdentifier) 
            => _CBCSetPlayerIdentifier(playerIdentifier);
        
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetIsUserUnderage(bool isUnderage);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetPublisherSessionIdentifier(string publisherSessionIdentifier);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetPublisherAppIdentifier(string publisherAppIdentifier);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetFramework(string frameworkName, string frameworkVersion);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetPlayerIdentifier(string playerIdentifier);
        
    }
}
