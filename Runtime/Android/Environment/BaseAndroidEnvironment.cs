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
            => Utilities.AndroidExtensions.EnumProperty(EnvironmentProperty, field);

        #nullable enable
        protected bool? BoolProperty(string field) 
            => Utilities.AndroidExtensions.BoolProperty(EnvironmentProperty, field);

        protected double? DoubleProperty(string field) 
            => Utilities.AndroidExtensions.DoubleProperty(EnvironmentProperty, field);

        protected float? FloatProperty(string field) 
            => Utilities.AndroidExtensions.FloatProperty(EnvironmentProperty, field);

        protected int? IntegerProperty(string field) 
            => Utilities.AndroidExtensions.IntegerProperty(EnvironmentProperty, field);

        protected Task<string?> AwaitableString(string field) =>
            Utilities.AndroidExtensions.AwaitableString(EnvironmentBridge, field);
        
        protected Task<bool?> AwaitableBoolean(string field) =>
            Utilities.AndroidExtensions.AwaitableBoolean(EnvironmentBridge, field);

        protected T Property<T>(string field) 
            => Utilities.AndroidExtensions.Property<T>(EnvironmentProperty, field);

        protected void SetProperty<T>(string field, T value) 
            => Utilities.AndroidExtensions.SetProperty(EnvironmentProperty, field, value);
        
        protected void SetProperty<T>(string field, T key, T value) 
            => Utilities.AndroidExtensions.SetProperty(EnvironmentProperty, field, key, value);
        #nullable disable
    }
}
