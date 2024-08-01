using System;
using System.Threading.Tasks;
using Chartboost.Constants;
using Chartboost.Core.Android.AndroidJavaProxies;
using Chartboost.Logging;
using UnityEngine;

namespace Chartboost.Core.Android.Utilities
{
    internal static partial class AndroidExtensions
    {
        /// <summary>
        /// Calls native method toString() on a <see cref="AndroidJavaObject"/>.
        /// </summary>
        /// <param name="nativeEnum">A <see cref="AndroidJavaObject"/> instance.</param>
        /// <returns>String representing object.</returns>
        public static string ToCSharpString(this AndroidJavaObject nativeEnum) 
            => nativeEnum.Call<string>(SharedAndroidConstants.FunctionToString);

        /// <summary>
        /// Gets an <see cref="Enum"/> property as <see cref="string"/> from a Chartboost Core Environment. 
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field.</param>
        /// <returns><see cref="string"/> value for <see cref="Enum"/> in Native Code.</returns>
        public static string EnumProperty(string environment, string field) 
        {
            using var property = Property(environment, field);
            return ToCSharpString(property);
        }

        #nullable enable
        /// <summary>
        /// Gets an <see cref="bool"/> property from a Chartboost Core Environment.
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field.</param>
        /// <returns><see cref="bool"/> value for target property.</returns>
        public static bool? BoolProperty(string environment, string field)
        {
            using var property = Property(environment, field);
            return property == null ? (bool?)null : property.Call<bool>(SharedAndroidConstants.FunctionBooleanValue);
        }

        /// <summary>
        /// Gets an <see cref="double"/> property from a Chartboost Core Environment.
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field.</param>
        /// <returns><see cref="double"/> value for target property.</returns>
        public static double? DoubleProperty(string environment, string field)
            => NullableProperty<double>(environment, field, SharedAndroidConstants.FunctionDoubleValue);

        /// <summary>
        /// Gets an <see cref="float"/> property from a Chartboost Core Environment.
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field.</param>
        /// <returns><see cref="float"/> value for target property.</returns>
        public static float? FloatProperty(string environment, string field)
            => NullableProperty<float>(environment, field, SharedAndroidConstants.FunctionFloatValue);
        
        /// <summary>
        /// Gets an <see cref="int"/> property from a Chartboost Core Environment.
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field.</param>
        /// <returns><see cref="int"/> value for target property.</returns>
        public static int? IntegerProperty(string environment, string field) 
            => NullableProperty<int>(environment, field, SharedAndroidConstants.FunctionIntValue);

        /// <summary>
        /// Gets a generic nullable property from a Chartboost Core Environment.
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field.</param>
        /// <param name="function">Java function to get value from field.</param>
        /// <returns><see cref="T"/> or null value for target property.</returns>
        public static T NullableProperty<T>(string environment, string field, string function)
        {
            using var property = Property(environment, field);
            return property == null ? default! : property.Call<T>(function);
        }

        /// <summary>
        /// Gets a generic property from a Chartboost Core Environment.
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field.</param>
        /// <returns><see cref="T"/> value for target property.</returns>
        public static T Property<T>(string environment, string field)
        {
            using var sdk = NativeSDK();
            using var property = sdk.CallStatic<AndroidJavaObject>(environment);
            return property.Call<T>(field);
        }

        /// <summary>
        /// Gets an awaitable nullable <see cref="string"/> from a native environment bridge.
        /// </summary>
        /// <param name="environment">Target environment bridge.</param>
        /// <param name="getMethod">Awaitable method in bridge.</param>
        /// <returns><see cref="string"/> value or null.</returns>
        public static async Task<string?> AwaitableString(Func<AndroidJavaObject> environment, string getMethod)
        {
            var awaiter = new ResultString();
            try
            {
                using var env = environment();
                env.CallStatic(getMethod, awaiter);
            }
            catch (Exception exception)
            {
                LogController.LogException(exception);
                return await Task.FromResult<string?>(null);
            }
            return await awaiter;
        }
        
        /// <summary>
        /// Gets an awaitable nullable <see cref="bool"/> from a native environment bridge.
        /// </summary>
        /// <param name="environment">Target environment bridge.</param>
        /// <param name="getMethod">Awaitable method in bridge.</param>
        /// <returns><see cref="bool"/> value or null.</returns>
        public static async Task<bool?> AwaitableBoolean(Func<AndroidJavaObject> environment, string getMethod)
        {
            var awaiter = new ResultNullableBoolean();
            try
            {
                using var env = environment();
                env.CallStatic(getMethod, awaiter);
            }
            catch (Exception exception)
            {
                LogController.LogException(exception);
                return await Task.FromResult<bool?>(null);
            }
            return await awaiter;
        }
        #nullable disable

        /// <summary>
        /// Get property defined as a <see cref="AndroidJavaObject"/>, used for complex data structures.
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field.</param>
        /// <returns><see cref="AndroidJavaObject"/> property.</returns>
        private static AndroidJavaObject Property(string environment, string field)
        {
            using var sdk = NativeSDK();
            using var property = sdk.CallStatic<AndroidJavaObject>(environment);
            return property.Call<AndroidJavaObject>(field);
        }

        /// <summary>
        /// Sets a value for a property on a target environment.
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field in environment.</param>
        /// <param name="value">Target value for field.</param>
        /// <typeparam name="T">Generic value to set, must be known to be supported.</typeparam>
        public static void SetProperty<T>(string environment, string field, T value)
        {
            using var sdk = NativeSDK();
            using var property = sdk.CallStatic<AndroidJavaObject>(environment);
            property.Call(field, value);
        }

        /// <summary>
        /// Sets a value for a kvp property on a target environment
        /// </summary>
        /// <param name="environment">Target environment.</param>
        /// <param name="field">Target field in environment.</param>
        /// <param name="key">Target key for field.</param>
        /// <param name="value">Target value for field.</param>
        public static void SetProperty<T>(string environment, string field, T key, T value)
        {
            using var sdk = NativeSDK();
            using var property = sdk.CallStatic<AndroidJavaObject>(environment);
            property.Call(field, key, value);
        }
    }
}
