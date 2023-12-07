using Chartboost.Core.Android.Consent;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Consent;
using Chartboost.Core.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    #nullable enable
    /// <summary>
    /// An observer that gets notified whenever any change happens in the CMP consent status.
    /// </summary>
    internal class ConsentObserver : AndroidJavaProxy
    {
        internal static ConsentObserver? Instance { get; private set; }

        private readonly ConsentManagementPlatform _environment;
        
        public ConsentObserver(ConsentManagementPlatform environment) : base(AndroidConstants.ConsentObserver)
        {
            _environment = environment;
            Instance = this;
        }

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeForStandard"/>
        /// <param name="standard">The <see cref="ConsentStandard"/> obtained from the observer.</param>
        /// <param name="value">The <see cref="ConsentValue"/> obtained from the observer.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentChangeForStandard(string standard, string value) =>
            MainThreadDispatcher.Post(o => _environment.OnConsentChangeForStandard(standard, value));

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeForStandard"/>
        /// <param name="standard">The <see cref="ConsentStandard"/> obtained from the observer.</param>s
        /// <param name="value">he <see cref="ConsentValue"/> nullable value obtained from the observer.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentChangeForStandard(string standard, AndroidJavaObject? value) =>
            MainThreadDispatcher.Post(o =>
            {
                ConsentValue? consentValue = value?.Call<string>(AndroidConstants.FunctionToString);
                _environment.OnConsentChangeForStandard(standard, consentValue);
            });

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatusChange"/>
        /// <param name="nativeStatus">The <see cref="ConsentStatus"/> native value.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentStatusChange(AndroidJavaObject nativeStatus) 
            => MainThreadDispatcher.Post(o => _environment.OnConsentStatusChange(nativeStatus.ToCSharpString().ConsentStatus()));

        /// <inheritdoc cref="IConsentManagementPlatform.PartnerConsentStatusChange"/>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onPartnerConsentStatusChange(string partnerIdentifier, AndroidJavaObject consentStatus)
            => MainThreadDispatcher.Post(o => _environment.OnPartnerConsentStatusChange(partnerIdentifier, consentStatus.ToCSharpString().ConsentStatus()));
        
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReady"/>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentModuleReady() 
            => MainThreadDispatcher.Post(o => _environment.OnConsentModuleReady());
    }
    #nullable disable
}
