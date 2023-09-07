using System;
using System.Threading.Tasks;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;
using Chartboost.Core.Utilities;
using UnityEngine;

namespace Chartboost.Core.Android.Environment
{
    /// <summary>
    /// Base Android class for all Chartboost Core environments. 
    /// </summary>
    internal abstract class BaseAndroidEnvironment
    {
        /// <summary>
        /// Environment as defined in a Java method call definition.
        /// </summary>
        protected abstract string EnvironmentProperty { get; }
        
        /// <summary>
        /// Associated function that returns a <see cref="AndroidJavaClass"/> associated Environment bridge.
        /// </summary>
        protected abstract Func<AndroidJavaClass> EnvironmentBridge { get; }

        protected async Task<VendorIdentifierScope> AwaitableVendorIdentifierScope()
        {
            var result = await AwaitableString(AndroidConstants.GetPropertyVendorIdentifierScope);
            return await Task.FromResult(result.VendorIdentifierScope());
        }

        protected string EnumPropertyAsString(string field) 
            => AndroidUtils.EnumProperty(EnvironmentProperty, field);

        #nullable enable
        protected bool? BoolProperty(string field) 
            => AndroidUtils.BoolProperty(EnvironmentProperty, field);

        protected double? DoubleProperty(string field) 
            => AndroidUtils.DoubleProperty(EnvironmentProperty, field);

        protected float? FloatProperty(string field) 
            => AndroidUtils.FloatProperty(EnvironmentProperty, field);

        protected int? IntegerProperty(string field) 
            => AndroidUtils.IntegerProperty(EnvironmentProperty, field);

        protected Task<string?> AwaitableString(string field) =>
            AndroidUtils.AwaitableString(EnvironmentBridge, field);
        
        protected Task<bool?> AwaitableBoolean(string field) =>
            AndroidUtils.AwaitableBoolean(EnvironmentBridge, field);

        protected T Property<T>(string field) 
            => AndroidUtils.Property<T>(EnvironmentProperty, field);

        protected void SetProperty<T>(string field, T value) 
            => AndroidUtils.SetProperty(EnvironmentProperty, field, value);
        #nullable disable
    }
}
