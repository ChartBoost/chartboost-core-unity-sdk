using System.Collections.Generic;
using Chartboost.Core.Android.Consent;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Consent;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    /// <summary>
    /// An observer that gets notified whenever any change happens in the CMP consent status.
    /// </summary>
    internal class ConsentObserver : AndroidJavaProxy
    {
        private readonly ConsentManagementPlatform _environment;
        
        public ConsentObserver(ConsentManagementPlatform environment) : base(AndroidConstants.ConsentObserver) 
            => _environment = environment;

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeWithFullConsents"/>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentChange(AndroidJavaObject context, AndroidJavaObject fullConsents, AndroidJavaObject modifiedKeys) =>
            MainThreadDispatcher.Post(_ =>
            {
                var consents = fullConsents.MapToConsentDictionary();
                var keys = modifiedKeys.SetToHashSet();
                _environment.OnConsentChangeWithFullConsents(consents, keys);
            });

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReadyWithInitialConsents"/>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentModuleReady(AndroidJavaObject context, AndroidJavaObject initialConsents) 
            => MainThreadDispatcher.Post(_ =>
            {
                var consents = initialConsents.MapToConsentDictionary();
                _environment.OnConsentModuleReadyWithInitialConsents(consents);
            });
    }
}
