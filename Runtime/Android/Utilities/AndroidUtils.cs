using System.Collections.Generic;
using Chartboost.Core.Consent;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using UnityEngine;

namespace Chartboost.Core.Android.Utilities
{
    internal static partial class AndroidUtils
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
        public static AndroidJavaClass AndroidBridge() => new AndroidJavaClass(AndroidConstants.BridgeCBC);

        /// <summary>
        /// Gets a class ref to the Advertising bridge. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaClass AdvertisingBridge() => new AndroidJavaClass(AndroidConstants.BridgeEnvAdvertising);
        
        /// <summary>
        /// Gets a class ref to the Analytics bridge. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaClass AnalyticsBridge() => new AndroidJavaClass(AndroidConstants.BridgeEnvAnalytics);
                
        /// <summary>
        /// Gets a class ref to the Attribution bridge. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns>The <see cref="AndroidJavaClass"/> instance.</returns>
        public static AndroidJavaClass AttributionBridge() => new AndroidJavaClass(AndroidConstants.BridgeEnvAttribution);
        
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
        public static AndroidJavaObject ConsentManagementPlatformBridge() => new AndroidJavaClass(AndroidConstants.BridgeCMP);
        
        /// <summary>
        /// Creates a <see cref="ModuleInitializationResult"/> from a native instance.
        /// </summary>
        /// <param name="nativeResult">A <see cref="AndroidJavaObject"/> instance of <see cref="ModuleInitializationResult"/>.</param>
        /// <returns>A Unity C# instance of <see cref="ModuleInitializationResult"/>.</returns>
        public static ModuleInitializationResult ToUnityModuleInitializationResult(this AndroidJavaObject nativeResult)
        {
            var start = nativeResult.Get<long>(AndroidConstants.ModuleInitializationResultStart);
            var end = nativeResult.Get<long>(AndroidConstants.ModuleInitializationResultEnd);
            var duration = nativeResult.Get<long>(AndroidConstants.ModuleInitializationResultDuration);
            using var nativeException = nativeResult.Get<AndroidJavaObject>(AndroidConstants.ModuleInitializationResultException);
            ChartboostCoreError? chartboostError = null;
            if (nativeException != null)
            {
                var error = nativeException.Call<AndroidJavaObject>(AndroidConstants.GetError);
                var code = error.Call<int>(AndroidConstants.GetCode);
                var message = error.Call<string>(AndroidConstants.GetMessage);
                var cause = error.Call<string>(AndroidConstants.GetCause);
                var resolution = error.Call<string>(AndroidConstants.GetResolution);
                chartboostError = new ChartboostCoreError(code, message, cause, resolution);
            }

            using var nativeModule = nativeResult.Get<AndroidJavaObject>(AndroidConstants.Module);
            var moduleName = nativeModule.Get<string>(AndroidConstants.InitializableModuleModuleId);
            var unityModule = PendingModuleCache.GetInitializableModule(moduleName);
            return new ModuleInitializationResult(start, end, duration, chartboostError, unityModule);
        }

        /// <summary>
        /// Extension method for <see cref="ChartboostCoreError"/>, creates a <see cref="AndroidJavaObject"/> instance with the same properties.
        /// </summary>
        /// <param name="error">Unity C# instance of <see cref="ChartboostCoreError"/>.</param>
        /// <returns>A <see cref="AndroidJavaObject"/> instance of <see cref="ChartboostCoreError"/>.</returns>
        public static AndroidJavaObject ToNativeCoreError(this ChartboostCoreError error)
        {
            return new AndroidJavaObject(AndroidConstants.CoreError, error.Code, error.Message, error.Cause, error.Resolution);
        }
        
        /// <summary>
        /// Extension method for <see cref="ConsentStatusSource"/>, creates a <see cref="AndroidJavaObject"/> instance with the same properties.
        /// </summary>
        /// <param name="source">An instance of <see cref="ConsentStatusSource"/>.</param>
        /// <returns>A <see cref="AndroidJavaObject"/> instance of <see cref="ConsentStatusSource"/>.</returns>
        public static AndroidJavaObject ConsentSource(this ConsentStatusSource source)
        {
            using var statusEnumClass = new AndroidJavaClass(AndroidConstants.ConsentStatusEnum);
            using var statusSourceEnumClass = new AndroidJavaClass(AndroidConstants.ConsentStatusSourceEnum);
            return source switch
            {
                ConsentStatusSource.User => statusSourceEnumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentStatusSourceEnumUser),
                ConsentStatusSource.Developer => statusSourceEnumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentStatusSourceEnumDeveloper),
                _ => statusSourceEnumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentStatusSourceEnumDeveloper)
            };
        }

        public static AndroidJavaObject DictionaryToMap(this IDictionary<string, string> source)
            => DictionaryToMap(source, AndroidConstants.ClassString);
        
        public static AndroidJavaObject DictionaryToMap(this IDictionary<string, bool> source)
            => DictionaryToMap(source, AndroidConstants.ClassBoolean);

        private static AndroidJavaObject DictionaryToMap<TValue>(IDictionary<string, TValue> source, string valueFunc)
        {
            var map = new AndroidJavaObject(AndroidConstants.ClassHashMap);
            
            if (source == null || source.Count == 0)
                return map;
            
            foreach (var kv in source)
            {
                var partnerId = kv.Key;
                if (string.IsNullOrEmpty(partnerId))
                    continue;
                using var key = new AndroidJavaObject(AndroidConstants.ClassString, partnerId);
                using var value = new AndroidJavaObject(valueFunc, kv.Value);
                map.Call<AndroidJavaClass>(AndroidConstants.FunctionPut, partnerId, value);
            }
            return map;
        }
    }
}
