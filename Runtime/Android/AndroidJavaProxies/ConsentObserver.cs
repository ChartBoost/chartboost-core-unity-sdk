using System;
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

        private readonly ChartboostConsentChangeForStandard _consentStatusChangeForStandard;
        private readonly ChartboostConsentStatusChange _consentStatusChange;
        private readonly Action _onConsentModuleReady;
        
        public ConsentObserver(ChartboostConsentChangeForStandard changeForStandard, ChartboostConsentStatusChange consentStatusChange, Action onConsentModuleReady) : base(AndroidConstants.ConsentObserver)
        {
            _consentStatusChangeForStandard = changeForStandard;
            _consentStatusChange = consentStatusChange;
            _onConsentModuleReady = onConsentModuleReady;
            Instance = this;
        }
        
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeForStandard"/>
        /// <param name="standard">The <see cref="ConsentStandard"/> obtained from the observer.</param>
        /// <param name="value">The <see cref="ConsentValue"/> obtained from the observer.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentChangeForStandard(string standard, string value) =>
            MainThreadDispatcher.Post(o => _consentStatusChangeForStandard?.Invoke(standard, value));

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeForStandard"/>
        /// <param name="standard">The <see cref="ConsentStandard"/> obtained from the observer.</param>s
        /// <param name="value">he <see cref="ConsentValue"/> nullable value obtained from the observer.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentChangeForStandard(string standard, AndroidJavaObject? value) =>
            MainThreadDispatcher.Post(o =>
            {
                ConsentValue? consentValue = value?.Call<string>(AndroidConstants.FunctionToString);
                _consentStatusChangeForStandard(standard, consentValue);
            });

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatusChange"/>
        /// <param name="nativeStatus">The <see cref="ConsentStatus"/> native value.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentStatusChange(AndroidJavaObject nativeStatus) 
            => MainThreadDispatcher.Post(o => _consentStatusChange(AndroidUtils.ToString(nativeStatus).ConsentStatus()));

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReady"/>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onConsentModuleReady() 
            => MainThreadDispatcher.Post(o => _onConsentModuleReady());
    }
    #nullable disable
}
