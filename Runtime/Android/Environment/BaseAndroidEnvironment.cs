using System;
using Chartboost.Core.Android.Utilities;

namespace Chartboost.Core.Android.Environment
{
    internal abstract class BaseAndroidEnvironment
    {
        protected abstract string EnvironmentProperty { get; }

        protected string GetEnumProperty(string field) 
            => AndroidUtils.GetEnumProperty(EnvironmentProperty, field);

        #nullable enable
        protected bool? GetBoolProperty(string field) 
            => AndroidUtils.GetBoolProperty(EnvironmentProperty, field);

        protected double? GetDoubleProperty(string field) 
            => AndroidUtils.GetDoubleProperty(EnvironmentProperty, field);

        protected float? GetFloatProperty(string field) 
            => AndroidUtils.GetFloatProperty(EnvironmentProperty, field);

        protected int? GetIntegerProperty(string field) 
            => AndroidUtils.GetIntegerProperty(EnvironmentProperty, field);

        protected T GetProperty<T>(string field) 
            => AndroidUtils.GetProperty<T>(EnvironmentProperty, field);

        protected void SetProperty<T>(string field, T value) 
            => AndroidUtils.SetProperty(EnvironmentProperty, field, value);
        #nullable disable
    }
}
