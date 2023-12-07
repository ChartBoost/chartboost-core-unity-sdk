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
    /// <summary>
    /// <inheritdoc cref="IConsentManagementPlatform"/>
    /// <br/>
    /// <para>Android implementation.</para>
    /// </summary>
    public class ConsentManagementPlatform : IConsentManagementPlatform
    {
        internal ConsentManagementPlatform()
        {
            using var consentManagementPlatform = AndroidUtils.ConsentManagementPlatform();
            consentManagementPlatform.Call(AndroidConstants.AddObserver,
                new ConsentObserver(this));
        }

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatus"/>
        public ConsentStatus ConsentStatus =>
            AndroidUtils.EnumProperty(AndroidConstants.Consent, AndroidConstants.GetConsentStatus).ConsentStatus();

        /// <inheritdoc cref="IConsentManagementPlatform.Consents"/>
        public Dictionary<ConsentStandard, ConsentValue> Consents
        {
            get
            {
                using var consentManagementPlatform = AndroidUtils.ConsentManagementPlatform();
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

        /// <inheritdoc cref="IConsentManagementPlatform.PartnerConsentStatus"/>
        public Dictionary<string, ConsentStatus> PartnerConsentStatus {
            get
            {
                var ret = new Dictionary<string, ConsentStatus>();
                using var consentManagementPlatform = AndroidUtils.ConsentManagementPlatform();
                using var partnerConsentStatus = consentManagementPlatform.Call<AndroidJavaObject>(AndroidConstants.GetPartnerConsentStatus);
               
                var size = partnerConsentStatus.Call<int>(AndroidConstants.FunctionSize);
                if (size == 0)
                    return ret;

                using var entrySet = partnerConsentStatus.Call<AndroidJavaObject>(AndroidConstants.FunctionEntrySet);
                using var iterator = entrySet.Call<AndroidJavaObject>(AndroidConstants.FunctionIterator);
                do
                {
                    using var entry = iterator.Call<AndroidJavaObject>(AndroidConstants.FunctionNext);
                    var key = entry.Call<string>(AndroidConstants.FunctionGetKey);
                    var value = entry.Call<AndroidJavaObject>(AndroidConstants.FunctionGetValue).ToCSharpString().ConsentStatus();
                    ret[key] = value;
                } while (iterator.Call<bool>(AndroidConstants.FunctionHasNext));
                return ret;
            }
        }

        /// <inheritdoc cref="IConsentManagementPlatform.ShouldCollectConsent"/>
        public bool ShouldCollectConsent
        {
            get
            {
                using var consentManagementPlatform = AndroidUtils.ConsentManagementPlatform();
                return consentManagementPlatform.Call<bool>(AndroidConstants.GetConsentShouldCollect);
            }
        }

        /// <inheritdoc cref="IConsentManagementPlatform.GrantConsent"/>
        public async Task<bool> GrantConsent(ConsentStatusSource source)
            => await ChangeConsentStatus(AndroidConstants.GrantConsentStatus, source);

        /// <inheritdoc cref="IConsentManagementPlatform.DenyConsent"/>
        public async Task<bool> DenyConsent(ConsentStatusSource source)
            => await ChangeConsentStatus(AndroidConstants.DenyConsentStatus, source);

        /// <inheritdoc cref="IConsentManagementPlatform.ResetConsent"/>
        public async Task<bool> ResetConsent()
        {
            using var nativeCmpBridge = AndroidUtils.ConsentManagementPlatformBridge();
            var setConsentStatusAwaiter = new ResultBoolean();
            nativeCmpBridge.CallStatic(AndroidConstants.ResetConsentStatus, setConsentStatusAwaiter);
            return await setConsentStatusAwaiter;
        }
        
        /// <summary>
        /// Requests a change for consent status.
        /// </summary>
        /// <param name="method">CMP Bridge method to be called.</param>
        /// <param name="source"><see cref="ConsentStatusSource"/> requesting the change.</param>
        /// <returns>Awaitable <see cref="ResultBoolean"/>.</returns>
        private static ResultBoolean ChangeConsentStatus(string method, ConsentStatusSource source)
        {
            using var nativeCmpBridge = AndroidUtils.ConsentManagementPlatformBridge();
            using var nativeStatusSource = source.ConsentSource();
            var setConsentStatusAwaiter = new ResultBoolean();
            nativeCmpBridge.CallStatic(method, nativeStatusSource, setConsentStatusAwaiter);
            return setConsentStatusAwaiter;
        }
        
       /// <summary>
       /// <inheritdoc cref="IConsentManagementPlatform.ShowConsentDialog"/>
       /// </summary>
       /// <param name="dialogType"><inheritdoc cref="IConsentManagementPlatform.ShowConsentDialog"/></param>
       /// <returns><inheritdoc cref="IConsentManagementPlatform.ShowConsentDialog"/></returns>
        public async Task<bool> ShowConsentDialog(ConsentDialogType dialogType)
        {
            using var nativeCmpBridge = AndroidUtils.ConsentManagementPlatformBridge();
            using var enumClass = new AndroidJavaClass(AndroidConstants.ConsentDialogTypeEnum);
            using var value = dialogType switch
            {
                ConsentDialogType.Concise => enumClass.GetStatic<AndroidJavaObject>(AndroidConstants
                    .ConsentDialogTypeEnumConcise),
                ConsentDialogType.Detailed => enumClass.GetStatic<AndroidJavaObject>(AndroidConstants
                    .ConsentDialogTypeEnumDetailed),
                _ => enumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentDialogTypeEnumDetailed)
            };

            var showConsentDialogAwaiter = new ResultBoolean();
            nativeCmpBridge.CallStatic(AndroidConstants.ConsentShowConsentDialog, value, showConsentDialogAwaiter);
            return await showConsentDialogAwaiter;
        }

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeForStandard"/>
        public event ChartboostConsentChangeForStandard ConsentChangeForStandard;

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatusChange"/>
        public event ChartboostConsentStatusChange ConsentStatusChange;
        
        /// <inheritdoc cref="IConsentManagementPlatform.PartnerConsentStatusChange"/>
        public event ChartboostPartnerConsentStatusChange PartnerConsentStatusChange;
        
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReady"/>
        public event Action ConsentModuleReady;

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeForStandard"/>
        /// <param name="standard">The <see cref="ConsentStandard"/> obtained from the observer.</param>
        /// <param name="value">The <see cref="ConsentValue"/> obtained from the observer.</param>
        internal void OnConsentChangeForStandard(ConsentStandard standard, ConsentValue value)
            => ConsentChangeForStandard?.Invoke(standard, value);

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatusChange"/>
        /// <param name="status">The <see cref="ConsentStatus"/> value.</param>
        internal void OnConsentStatusChange(ConsentStatus status)
            => ConsentStatusChange?.Invoke(status);

        /// <inheritdoc cref="IConsentManagementPlatform.PartnerConsentStatusChange"/>
        internal void OnPartnerConsentStatusChange(string partnerIdentifier, ConsentStatus consentStatus)
            => PartnerConsentStatusChange?.Invoke(partnerIdentifier, consentStatus);

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReady"/>
        internal void OnConsentModuleReady()
            => ConsentModuleReady?.Invoke();

        ~ConsentManagementPlatform()
        {
            AndroidJNI.AttachCurrentThread();
            using var consentManagementPlatform = AndroidUtils.ConsentManagementPlatform();
            consentManagementPlatform.Call(AndroidConstants.RemoveObserver, ConsentObserver.Instance);
            AndroidJNI.DetachCurrentThread();
        }
    }
}
