using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Android.AndroidJavaProxies;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Consent;
using Chartboost.Core.Utilities;
using UnityEngine;

namespace Chartboost.Core.Android.Consent
{
    public class ConsentManagementPlatform : IConsentManagementPlatform
    {
        internal ConsentManagementPlatform()
        {
            using var consentManagementPlatform = AndroidUtils.GetConsentManagementPlatform();
            consentManagementPlatform.Call(AndroidConstants.ConsentAddObserver,
                new ConsentObserver(OnConsentChangeForStandard, OnConsentStatusChange, OnInitialConsentInfoAvailable));
        }

        public ConsentStatus ConsentStatus =>
            AndroidUtils.GetEnumProperty(AndroidConstants.Consent, AndroidConstants.GetConsentStatus)
                .GetConsentStatus();

        public Dictionary<ConsentStandard, ConsentValue> Consents
        {
            get
            {
                using var consentManagementPlatform = AndroidUtils.GetConsentManagementPlatform();
                using var consents = consentManagementPlatform.Call<AndroidJavaObject>(AndroidConstants.GetConsents);

                var ret = new Dictionary<ConsentStandard, ConsentValue>();

                // ensure we don't perform actions on a failure
                if (consents == null)
                {
                    ChartboostCoreLogger.LogWarning(
                        "Failed to get consents from CMP, returning empty Dictionary<string,string>.");
                    return ret;
                }

                var entries = consents.Call<AndroidJavaObject>(AndroidConstants.FunctionEntrySet);
                var iter = entries.Call<AndroidJavaObject>(AndroidConstants.FunctionIterator);

                while (iter.Call<bool>(AndroidConstants.FunctionHasNext))
                {
                    var kvpEntry = iter.Call<AndroidJavaObject>(AndroidConstants.FunctionNext);
                    var key = kvpEntry.Call<string>(AndroidConstants.FunctionGetKey);
                    var valueJo = kvpEntry.Call<AndroidJavaObject>(AndroidConstants.FunctionGetValue);
                    var value = valueJo.Call<string>(AndroidConstants.FunctionToString);
                    ret[key] = string.IsNullOrEmpty(value) ? null : new ConsentValue(value);
                }

                return ret;
            }
        }

        public bool ShouldCollectConsent
        {
            get
            {
                using var consentManagementPlatform = AndroidUtils.GetConsentManagementPlatform();
                return consentManagementPlatform.Call<bool>(AndroidConstants.GetConsentShouldCollect);
            }
        }

        public async Task<bool> GrantConsent(ConsentStatusSource source)
            => await ChangeConsentStatus(AndroidConstants.GrantConsentStatus, source);

        public async Task<bool> DenyConsent(ConsentStatusSource source)
            => await ChangeConsentStatus(AndroidConstants.DenyConsentStatus, source);

        public async Task<bool> ResetConsent()
        {
            using var nativeCmpBridge = AndroidUtils.GetConsentManagementPlatformBridge();
            var setConsentStatusAwaiter = new AwaiterBooleanResult();
            nativeCmpBridge.CallStatic(AndroidConstants.ResetConsentStatus, setConsentStatusAwaiter);
            return await setConsentStatusAwaiter;
        }

        private static AwaiterBooleanResult ChangeConsentStatus(string method, ConsentStatusSource source)
        {
            using var nativeCmpBridge = AndroidUtils.GetConsentManagementPlatformBridge();
            using var nativeStatusSource = source.GetConsentSource();
            var setConsentStatusAwaiter = new AwaiterBooleanResult();
            nativeCmpBridge.CallStatic(method, nativeStatusSource, setConsentStatusAwaiter);
            return setConsentStatusAwaiter;
        }

        public async Task<bool> ShowConsentDialog(ConsentDialogType dialogType)
        {
            using var nativeCmpBridge = AndroidUtils.GetConsentManagementPlatformBridge();
            using var enumClass = new AndroidJavaClass(AndroidConstants.ConsentDialogTypeEnum);
            using var value = dialogType switch
            {
                ConsentDialogType.Concise => enumClass.GetStatic<AndroidJavaObject>(AndroidConstants
                    .ConsentDialogTypeEnumConcise),
                ConsentDialogType.Detailed => enumClass.GetStatic<AndroidJavaObject>(AndroidConstants
                    .ConsentDialogTypeEnumDetailed),
                _ => enumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentDialogTypeEnumDetailed)
            };

            var showConsentDialogAwaiter = new AwaiterBooleanResult();
            nativeCmpBridge.CallStatic(AndroidConstants.ConsentShowConsentDialog, value, showConsentDialogAwaiter);
            return await showConsentDialogAwaiter;
        }

        public event ChartboostConsentChangeForStandard ConsentChangeForStandard;
        public event ChartboostConsentStatusChange ConsentStatusChange;
        public event Action ConsentModuleReady;

        private void OnConsentChangeForStandard(ConsentStandard standard, ConsentValue value)
            => ConsentChangeForStandard?.Invoke(standard, value);

        private void OnConsentStatusChange(ConsentStatus status)
            => ConsentStatusChange?.Invoke(status);

        private void OnInitialConsentInfoAvailable()
            => ConsentModuleReady?.Invoke();

        ~ConsentManagementPlatform()
        {
            AndroidJNI.AttachCurrentThread();
            using var consentManagementPlatform = AndroidUtils.GetConsentManagementPlatform();
            consentManagementPlatform.Call(AndroidConstants.ConsentRemoveObserver, ConsentObserver.Instance);
            AndroidJNI.DetachCurrentThread();
        }
    }
}
