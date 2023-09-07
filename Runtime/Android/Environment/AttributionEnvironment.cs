using System;
using System.Threading.Tasks;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;
using UnityEngine;

namespace Chartboost.Core.Android.Environment
{
    #nullable enable
    /// <summary>
    /// <para>Android Implementation of <see cref="IAttributionEnvironment"/>.</para>
    /// <inheritdoc cref="IAttributionEnvironment"/>
    /// </summary>
    internal sealed class AttributionEnvironment : BaseAndroidEnvironment, IAttributionEnvironment
    {
        /// <inheritdoc cref="BaseAndroidEnvironment.EnvironmentProperty"/>
        protected override string EnvironmentProperty => AndroidConstants.EnvironmentAttribution;
        
        /// <inheritdoc cref="BaseAndroidEnvironment.EnvironmentBridge"/>
        protected override Func<AndroidJavaClass> EnvironmentBridge => AndroidUtils.AttributionBridge;
        
        /// <inheritdoc cref="IAttributionEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => AwaitableString(AndroidConstants.GetPropertyAdvertisingIdentifier);
        
        /// <inheritdoc cref="IAttributionEnvironment.UserAgent"/>
        public Task<string?> UserAgent => AwaitableString(AndroidConstants.GetPropertyUserAgent);
    }
    #nullable disable
}
