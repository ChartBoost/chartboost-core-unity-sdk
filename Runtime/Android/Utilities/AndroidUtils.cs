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
        /// <returns></returns>
        public static AndroidJavaObject GetNativeSDK() => new AndroidJavaClass(AndroidConstants.ChartboostCore);
        
        /// <summary>
        /// Gets a class ref to the Android bridge. Should be utilized with the see <a href="https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/using">using statement</a>.
        /// </summary>
        /// <returns></returns>
        public static AndroidJavaObject GetAndroidBridge() => new AndroidJavaClass(AndroidConstants.BridgeCBC);
        
        public static AndroidJavaObject GetConsentManagementPlatform()
        {
            using var sdk = GetNativeSDK();
            return sdk.CallStatic<AndroidJavaObject>(AndroidConstants.Consent);
        }

        public static AndroidJavaObject GetConsentManagementPlatformBridge() => new AndroidJavaClass(AndroidConstants.BridgeCMP);
        
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

        public static AndroidJavaObject ToNativeChartboostError(this ChartboostCoreError error)
        {
            return new AndroidJavaObject(AndroidConstants.ChartboostCoreError, error.Code, error.Message, error.Cause, error.Resolution);
        }
        
        public static AndroidJavaObject GetConsentSource(this ConsentStatusSource source)
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
    }
}
