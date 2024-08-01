using System.Collections.Generic;
using Chartboost.Constants;
using Chartboost.Core.Android.Modules;
using Chartboost.Core.Consent;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using Chartboost.Logging;
using UnityEngine;

namespace Chartboost.Core.Android.Utilities
{
    internal static partial class AndroidExtensions
    {
        /// <summary>
        /// Gets a class ref to the Native SDK. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaClass NativeSDK() => new AndroidJavaClass(AndroidConstants.ChartboostCore);
        
        /// <summary>
        /// Gets a class ref to the Android bridge. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaClass AndroidBridge() => new AndroidJavaClass(AndroidConstants.ClassBridgeChartboostCore);

        /// <summary>
        /// Gets a class ref to the Advertising bridge. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaClass AdvertisingBridge() => new AndroidJavaClass(AndroidConstants.ClassBridgeEnvAdvertising);
        
        /// <summary>
        /// Gets a class ref to the Analytics bridge. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaClass AnalyticsBridge() => new AndroidJavaClass(AndroidConstants.ClassBridgeEnvAnalytics);
                
        /// <summary>
        /// Gets a class ref to the Attribution bridge. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaClass AttributionBridge() => new AndroidJavaClass(AndroidConstants.ClassBridgeEnvAttribution);
        
        /// <summary>
        /// Gets a class ref to the Native SDK <see cref="IConsentManagementPlatform"/>. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaObject ConsentManagementPlatform()
        {
            using var sdk = NativeSDK();
            return sdk.CallStatic<AndroidJavaObject>(AndroidConstants.Consent);
        }

        /// <summary>
        /// Gets a class ref to the CMP Android bridge. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaObject ConsentManagementPlatformBridge() => new AndroidJavaClass(AndroidConstants.ClassBridgeConsentManagementPlatform);
        
        /// <summary>
        /// Creates a <see cref="ModuleInitializationResult"/> from a native instance.
        /// </summary>
        /// <param name="nativeResult">A <see cref="AndroidJavaObject"/> instance of <see cref="ModuleInitializationResult"/>.</param>
        /// <returns>A Unity C# instance of <see cref="ModuleInitializationResult"/>.</returns>
        public static ModuleInitializationResult ToUnityModuleInitializationResult(this AndroidJavaObject nativeResult)
        {
            var start = nativeResult.Call<long>(AndroidConstants.FunctionGetStart);
            var end = nativeResult.Call<long>(AndroidConstants.FunctionGetEnd);
            var duration = nativeResult.Call<long>(AndroidConstants.FunctionGetDuration);
            using var nativeException = nativeResult.Call<AndroidJavaObject>(AndroidConstants.FunctionGetException);
            ChartboostCoreError? chartboostError = null;
            if (nativeException != null)
            {
                var error = nativeException.Call<AndroidJavaObject>(AndroidConstants.FunctionGetError);
                var code = error.Call<int>(AndroidConstants.FunctionGetCode);
                var message = error.Call<string>(AndroidConstants.FunctionGetMessage);
                var cause = error.Call<string>(AndroidConstants.FunctionGetCause);
                var resolution = error.Call<string>(AndroidConstants.FunctionGetResolution);
                chartboostError = new ChartboostCoreError(code, message, cause, resolution);
            }
            
            var moduleId = nativeResult.Call<string>(AndroidConstants.FunctionGetModuleId);
            var moduleVersion = nativeResult.Call<string>(AndroidConstants.FunctionGetModuleVersion);
            return new ModuleInitializationResult(start, end, duration, moduleId, moduleVersion, chartboostError);
        }
        
        public static Dictionary<ConsentKey, ConsentValue> MapToConsentDictionary(this AndroidJavaObject source)
        {
            var ret = new Dictionary<ConsentKey, ConsentValue>();

            // ensure we don't perform actions on a failure
            if (source == null)
            {
                const string failedToGetConsentsWarning = "Failed to get consents from CMP, returning empty Dictionary<string,string>.";
                LogController.Log(failedToGetConsentsWarning, LogLevel.Warning);
                return ret;
            }
            
            var size = source.Call<int>(SharedAndroidConstants.FunctionSize);
            if (size == 0)
                return ret;
            
            var entries = source.Call<AndroidJavaObject>(SharedAndroidConstants.FunctionEntrySet);
            var iter = entries.Call<AndroidJavaObject>(SharedAndroidConstants.FunctionIterator);
            
            do
            {
                var entry = iter.Call<AndroidJavaObject>(SharedAndroidConstants.FunctionNext);
                var key = entry.Call<string>(SharedAndroidConstants.FunctionGetKey);
                var value = entry.Call<string>(SharedAndroidConstants.FunctionGetValue);
                if (!string.IsNullOrEmpty(value))
                    ret[key] = new ConsentValue(value);
            } while (iter.Call<bool>(SharedAndroidConstants.FunctionHasNext));

            return ret;
        }
        
        public static HashSet<ConsentKey> SetToHashSet(this AndroidJavaObject source)
        {
            var ret = new HashSet<ConsentKey>();

            if (source == null)
                return ret;
            
            using var iterator = source.Call<AndroidJavaObject>(SharedAndroidConstants.FunctionIterator);
            
            while (iterator.Call<bool>(SharedAndroidConstants.FunctionHasNext))
            {
                var nativeValue = iterator.Call<string>(SharedAndroidConstants.FunctionNext);
                ret.Add(nativeValue);
            }
            return ret;
        }

        /// <summary>
        /// Extension method for <see cref="ChartboostCoreError"/>, creates a <see cref="AndroidJavaObject"/> instance with the same properties.
        /// </summary>
        /// <param name="error">Unity C# instance of <see cref="ChartboostCoreError"/>.</param>
        /// <returns>A <see cref="AndroidJavaObject"/> instance of <see cref="ChartboostCoreError"/>.</returns>
        public static AndroidJavaObject ToNativeCoreError(this ChartboostCoreError error) 
            => new(AndroidConstants.CoreError, error.Code, error.Message, error.Cause, error.Resolution);

        /// <summary>
        /// Extension method for <see cref="Core.Consent.ConsentSource"/>, creates a <see cref="AndroidJavaObject"/> instance with the same properties.
        /// </summary>
        /// <param name="source">An instance of <see cref="Core.Consent.ConsentSource"/>.</param>
        /// <returns>A <see cref="AndroidJavaObject"/> instance of <see cref="Core.Consent.ConsentSource"/>.</returns>
        public static AndroidJavaObject ConsentSource(this ConsentSource source)
        {
            using var statusSourceEnumClass = new AndroidJavaClass(AndroidConstants.ConsentSourceEnum);
            return source switch
            {
                Core.Consent.ConsentSource.User => statusSourceEnumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentSourceEnumUser),
                Core.Consent.ConsentSource.Developer => statusSourceEnumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentSourceEnumDeveloper),
                _ => statusSourceEnumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentSourceEnumDeveloper)
            };
        }
    }
}
