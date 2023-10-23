using System;
using Chartboost.Core.Android.AndroidJavaProxies;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;
using UnityEngine;

namespace Chartboost.Core.Android.Environment
{
    #nullable enable
    /// <summary>
    /// <para>Android Implementation of <see cref="IPublisherMetadata"/>.</para>
    /// <inheritdoc cref="IPublisherMetadata"/>
    /// </summary>
    internal sealed class PublisherMetadata : BaseAndroidEnvironment, IPublisherMetadata
    {
        /// <inheritdoc cref="BaseAndroidEnvironment.EnvironmentProperty"/>
        protected override string EnvironmentProperty => AndroidConstants.PublisherMetadata;
        
        /// <inheritdoc cref="BaseAndroidEnvironment.EnvironmentBridge"/>
        protected override Func<AndroidJavaClass> EnvironmentBridge => null!;

        public PublisherMetadata()
        {
            using var sdk = AndroidUtils.NativeSDK();
            using var publisherMetadata = sdk.CallStatic<AndroidJavaObject>(EnvironmentProperty);
            publisherMetadata.Call(AndroidConstants.AddObserver, new PublisherMetadataObserver(this));
        }

        public event Action? IsUserUnderageChanged;
        public event Action? PublisherSessionIdentifierChanged;
        public event Action? PublisherAppIdentifierChanged;
        public event Action? FrameworkNameChanged;
        public event Action? FrameworkVersionChanged;
        public event Action? PlayerIdentifierChanged;

        /// <inheritdoc cref="IPublisherMetadata.SetIsUserUnderage"/>
        public void SetIsUserUnderage(bool isUserUnderage) => SetProperty(AndroidConstants.SetPropertyIsUserUnderAge, isUserUnderage);
        
        /// <inheritdoc cref="IPublisherMetadata.SetPublisherSessionIdentifier"/>
        public void SetPublisherSessionIdentifier(string? publisherSessionIdentifier) => SetProperty(AndroidConstants.SetPropertyPublisherSessionIdentifier, publisherSessionIdentifier);
        
        /// <inheritdoc cref="IPublisherMetadata.SetPublisherAppIdentifier"/>
        public void SetPublisherAppIdentifier(string? publisherAppIdentifier) => SetProperty(AndroidConstants.SetPropertyPublisherAppIdentifier, publisherAppIdentifier);
      
        /// <inheritdoc cref="IPublisherMetadata.SetFrameworkName"/>
        public void SetFrameworkName(string? frameworkName) => SetProperty(AndroidConstants.SetPropertyFrameworkName, frameworkName);
        
        /// <inheritdoc cref="IPublisherMetadata.SetFrameworkVersion"/>
        public void SetFrameworkVersion(string? frameworkVersion) => SetProperty(AndroidConstants.SetPropertyFrameworkVersion, frameworkVersion);
        
        /// <inheritdoc cref="IPublisherMetadata.SetPlayerIdentifier"/>
        public void SetPlayerIdentifier(string? playerIdentifier) => SetProperty(AndroidConstants.SetPropertyPlayerIdentifier, playerIdentifier);

        ~PublisherMetadata()
        {
            AndroidJNI.AttachCurrentThread();
            using var consentManagementPlatform = AndroidUtils.ConsentManagementPlatform();
            consentManagementPlatform.Call(AndroidConstants.RemoveObserver, PublisherMetadataObserver.Instance);
            AndroidJNI.DetachCurrentThread();
        }

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
    }
    #nullable disable
}
