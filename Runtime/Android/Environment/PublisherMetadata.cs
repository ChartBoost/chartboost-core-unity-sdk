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
        
        /// <inheritdoc cref="IPublisherMetadata.SetIsUserUnderage"/>
        public void SetIsUserUnderage(bool isUserUnderage) => SetProperty(AndroidConstants.SetPropertyIsUserUnderAge, isUserUnderage);
        
        /// <inheritdoc cref="IPublisherMetadata.SetPublisherSessionIdentifier"/>
        public void SetPublisherSessionIdentifier(string? publisherSessionIdentifier) => SetProperty(AndroidConstants.SetPropertyPublisherSessionIdentifier, publisherSessionIdentifier);
        
        /// <inheritdoc cref="IPublisherMetadata.SetPublisherAppIdentifier"/>
        public void SetPublisherAppIdentifier(string? publisherAppIdentifier) => SetProperty(AndroidConstants.SetPropertyPublisherAppIdentifier, publisherAppIdentifier);

        /// <inheritdoc cref="IPublisherMetadata.SetFramework"/>
        public void SetFramework(string? frameworkName, string? frameworkVersion) => SetProperty(AndroidConstants.SetPropertyFramework, frameworkName, frameworkVersion);

        /// <inheritdoc cref="IPublisherMetadata.SetPlayerIdentifier"/>
        public void SetPlayerIdentifier(string? playerIdentifier) => SetProperty(AndroidConstants.SetPropertyPlayerIdentifier, playerIdentifier);
    }
}
