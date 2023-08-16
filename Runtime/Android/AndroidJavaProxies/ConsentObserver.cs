using System;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Consent;
using Chartboost.Core.Utilities;
using UnityEngine;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    #nullable enable
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
        
        private void onConsentChangeForStandard(string standard, string value) =>
            MainThreadDispatcher.Post(o => _consentStatusChangeForStandard?.Invoke(standard, value));

        private void onConsentChangeForStandard(string standard, AndroidJavaObject? value) =>
            MainThreadDispatcher.Post(o =>
            {
                ConsentValue? consentValue = value?.Call<string>(AndroidConstants.FunctionToString);
                _consentStatusChangeForStandard(standard, consentValue);
            });

        private void onConsentStatusChange(AndroidJavaObject nativeStatus) 
            => MainThreadDispatcher.Post(o => _consentStatusChange(AndroidUtils.ToString(nativeStatus).GetConsentStatus()));

        private void onConsentModuleReady() 
            => MainThreadDispatcher.Post(o => _onConsentModuleReady());
    }
    #nullable disable
}
