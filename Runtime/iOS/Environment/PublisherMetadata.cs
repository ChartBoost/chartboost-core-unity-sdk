using System;
using System.Runtime.InteropServices;
using AOT;
using Chartboost.Core.Environment;
using Chartboost.Core.iOS.Utilities;
using Chartboost.Core.Utilities;

namespace Chartboost.Core.iOS.Environment
{
    /// <summary>
    /// <para>iOS Implementation of <see cref="IPublisherMetadata"/>.</para>
    /// <inheritdoc cref="IPublisherMetadata"/>
    /// </summary>
    public class PublisherMetadata : IPublisherMetadata
    {
        private static PublisherMetadata _instance;
        internal PublisherMetadata()
        {
            _instance = this;
            _chartboostCoreSetPublisherMetadataCallbacks(OnPublisherMetadataPropertyChange);
        }

        public event Action IsUserUnderageChanged;
        public event Action PublisherSessionIdentifierChanged;
        public event Action PublisherAppIdentifierChanged;
        public event Action FrameworkNameChanged;
        public event Action FrameworkVersionChanged;
        public event Action PlayerIdentifierChanged;

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
        
        internal void OnIsUserUnderageChanged()
            => IsUserUnderageChanged?.Invoke();

        internal void OnPublisherSessionIdentifierChanged()
            => PublisherSessionIdentifierChanged?.Invoke();

        internal void OnPublisherAppIdentifierChanged() 
            => PublisherAppIdentifierChanged?.Invoke();

        internal void OnFrameworkNameChanged() 
            => FrameworkNameChanged?.Invoke();

        internal void OnFrameworkVersionChanged() 
            => FrameworkVersionChanged?.Invoke();

        internal void OnPlayerIdentifierChanged() 
            => PlayerIdentifierChanged?.Invoke();
        
        [MonoPInvokeCallback(typeof(ChartboostCoreOnEnumStatusChange))]
        private static void OnPublisherMetadataPropertyChange(int value)
        {
            MainThreadDispatcher.Post(o =>
            {
                var property = (PublisherMetadataProperty)value;
                switch (property)
                {
                    case PublisherMetadataProperty.FrameworkName:
                        _instance.OnFrameworkNameChanged();
                        break;
                    case PublisherMetadataProperty.FrameworkVersion:
                        _instance.OnFrameworkVersionChanged();
                        break;
                    case PublisherMetadataProperty.IsUserUnderage:
                        _instance.OnIsUserUnderageChanged();
                        break;
                    case PublisherMetadataProperty.PlayerIdentifier:
                        _instance.OnPlayerIdentifierChanged();
                        break;
                    case PublisherMetadataProperty.PublisherAppIdentifier:
                        _instance.OnPublisherAppIdentifierChanged();
                        break;
                    case PublisherMetadataProperty.PublisherSessionIdentifier:
                        _instance.OnPublisherSessionIdentifierChanged();
                        break;
                }
            });
        }

        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetIsUserUnderage(bool isUnderage);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetPublisherSessionIdentifier(string publisherSessionIdentifier);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetPublisherAppIdentifier(string publisherAppIdentifier);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetFrameworkName(string frameworkName);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataSetFrameworkVersion(string frameworkVersion);
        [DllImport(IOSConstants.DLLImport)] private static extern void _publisherMetadataPlayerIdentifier(string playerIdentifier);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreSetPublisherMetadataCallbacks(ChartboostCoreOnEnumStatusChange onPublisherMetadataPropertyChange);
    }
}
