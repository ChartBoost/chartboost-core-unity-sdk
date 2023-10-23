using Chartboost.Core.Android.Environment;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;
using Chartboost.Core.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
#nullable enable
    internal class PublisherMetadataObserver : AndroidJavaProxy
    {
        internal static PublisherMetadataObserver? Instance { get; private set; }
        private readonly PublisherMetadata _environment;
        
        public PublisherMetadataObserver(PublisherMetadata environment) : base(AndroidConstants.PublisherMetadataObserver)
        {
            _environment = environment;
            Instance = this;
        }

        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onChanged(AndroidJavaObject property)
        {
            MainThreadDispatcher.Post(o =>
            {
                var publisherMetadataProperty = property.Call<string>(AndroidConstants.FunctionToString).PublisherMetadataProperty();
                switch (publisherMetadataProperty)
                {
                    case PublisherMetadataProperty.FrameworkName:
                        _environment.OnFrameworkNameChanged();
                        break;
                    case PublisherMetadataProperty.FrameworkVersion:
                        _environment.OnFrameworkVersionChanged();
                        break;
                    case PublisherMetadataProperty.IsUserUnderage:
                        _environment.OnIsUserUnderageChanged();
                        break;
                    case PublisherMetadataProperty.PlayerIdentifier:
                        _environment.OnPlayerIdentifierChanged();
                        break;
                    case PublisherMetadataProperty.PublisherAppIdentifier:
                        _environment.OnPublisherAppIdentifierChanged();
                        break;
                    case PublisherMetadataProperty.PublisherSessionIdentifier:
                        _environment.OnPublisherSessionIdentifierChanged();
                        break;
                }
            });
        }
    }
#nullable disable
}
