using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Android.AndroidJavaProxies;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Consent;
using UnityEngine;

namespace Chartboost.Core.Android.Consent
{
    /// <summary>
    /// <inheritdoc cref="IConsentManagementPlatform"/>
    /// <br/>
    /// <para>Android implementation.</para>
    /// </summary>
    internal class ConsentManagementPlatform : IConsentManagementPlatform
    {
        private static ConsentObserver _consentObserverInstance;
        internal ConsentManagementPlatform()
        {
            _consentObserverInstance = new ConsentObserver(this);
            using var consentManagementPlatform = Utilities.AndroidExtensions.ConsentManagementPlatform();
            consentManagementPlatform.Call(AndroidConstants.FunctionAddObserver, _consentObserverInstance);
            ConsentChangeWithFullConsents = null!;
            ConsentModuleReadyWithInitialConsents = null!;
        }
        
        /// <inheritdoc cref="IConsentManagementPlatform.Consents"/>
        public IReadOnlyDictionary<ConsentKey, ConsentValue> Consents
        {
            get
            {
                using var consentManagementPlatform = Utilities.AndroidExtensions.ConsentManagementPlatform();
                using var consents = consentManagementPlatform.Call<AndroidJavaObject>(AndroidConstants.GetConsents);
                return consents.MapToConsentDictionary();
            }
        }

        /// <inheritdoc cref="IConsentManagementPlatform.ShouldCollectConsent"/>
        public bool ShouldCollectConsent
        {
            get
            {
                using var consentManagementPlatform = Utilities.AndroidExtensions.ConsentManagementPlatform();
                return consentManagementPlatform.Call<bool>(AndroidConstants.GetConsentShouldCollect);
            }
        }

        /// <inheritdoc cref="IConsentManagementPlatform.GrantConsent"/>
        public async Task<bool> GrantConsent(ConsentSource source)
            => await ChangeConsentStatus(AndroidConstants.GrantConsentStatus, source);

        /// <inheritdoc cref="IConsentManagementPlatform.DenyConsent"/>
        public async Task<bool> DenyConsent(ConsentSource source)
            => await ChangeConsentStatus(AndroidConstants.DenyConsentStatus, source);

        /// <inheritdoc cref="IConsentManagementPlatform.ResetConsent"/>
        public async Task<bool> ResetConsent()
        {
            using var nativeCmpBridge = Utilities.AndroidExtensions.ConsentManagementPlatformBridge();
            var setConsentStatusAwaiter = new ResultBoolean();
            nativeCmpBridge.CallStatic(AndroidConstants.ResetConsentStatus, setConsentStatusAwaiter);
            return await setConsentStatusAwaiter;
        }
        
        /// <summary>
        /// Requests a change for consent status.
        /// </summary>
        /// <param name="method">CMP Bridge method to be called.</param>
        /// <param name="source"><see cref="ConsentSource"/> requesting the change.</param>
        /// <returns>Awaitable <see cref="ResultBoolean"/>.</returns>
        private static ResultBoolean ChangeConsentStatus(string method, ConsentSource source)
        {
            using var nativeCmpBridge = Utilities.AndroidExtensions.ConsentManagementPlatformBridge();
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
            using var nativeCmpBridge = Utilities.AndroidExtensions.ConsentManagementPlatformBridge();
            using var enumClass = new AndroidJavaClass(AndroidConstants.ConsentDialogTypeEnum);
            using var value = dialogType switch
            {
                ConsentDialogType.Concise => enumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentDialogTypeEnumConcise),
                ConsentDialogType.Detailed => enumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentDialogTypeEnumDetailed),
                _ => enumClass.GetStatic<AndroidJavaObject>(AndroidConstants.ConsentDialogTypeEnumDetailed)
            };

            var showConsentDialogAwaiter = new ResultBoolean();
            nativeCmpBridge.CallStatic(AndroidConstants.ConsentShowConsentDialog, value, showConsentDialogAwaiter);
            return await showConsentDialogAwaiter;
        }

       #nullable enable
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeWithFullConsents"/>
        public event ChartboostCoreConsentChangeWithFullConsents? ConsentChangeWithFullConsents;

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReadyWithInitialConsents"/>
        public event ChartboostCoreConsentModuleReadyWithInitialConsents? ConsentModuleReadyWithInitialConsents;

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeWithFullConsents"/>
        internal void OnConsentChangeWithFullConsents(IReadOnlyDictionary<ConsentKey, ConsentValue> fullConsents, IReadOnlyCollection<ConsentKey> modifiedKeys)
            => ConsentChangeWithFullConsents?.Invoke(fullConsents, modifiedKeys);

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReadyWithInitialConsents"/>
        internal void OnConsentModuleReadyWithInitialConsents(IReadOnlyDictionary<ConsentKey, ConsentValue> initialConsents)
            => ConsentModuleReadyWithInitialConsents?.Invoke(initialConsents);
        #nullable disable

        ~ConsentManagementPlatform()
        {
            AndroidJNI.AttachCurrentThread();
            using var consentManagementPlatform = Utilities.AndroidExtensions.ConsentManagementPlatform();
            consentManagementPlatform.Call(AndroidConstants.FunctionRemoveObserver, _consentObserverInstance);
            AndroidJNI.DetachCurrentThread();
        }
    }
}
