using System;
using AOT;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Core.Consent;
using Chartboost.Core.iOS.Utilities;
using Chartboost.Core.Utilities;
using Newtonsoft.Json;

namespace Chartboost.Core.iOS.Consent
{
    public class ConsentManagementPlatform : IConsentManagementPlatform
    {
        private static ConsentManagementPlatform _instance;

        static ConsentManagementPlatform()
            => _chartboostCoreSetConsentCallbacks(OnConsentStatusChange, OnConsentChangeForStandard, OnConsentModuleReady);

        internal ConsentManagementPlatform() 
            => _instance ??= this;

        public ConsentStatus ConsentStatus 
            => (ConsentStatus)_chartboostCoreGetConsentStatus();

        public Dictionary<ConsentStandard, ConsentValue> Consents
        {
            get
            {
                var json = _chartboostCoreGetConsents();
                return JsonConvert.DeserializeObject<Dictionary<ConsentStandard, ConsentValue>>(json);
            }
        }

        public bool ShouldCollectConsent 
            => _chartboostCoreShouldCollectConsent();

        public async Task<bool> GrantConsent(ConsentStatusSource source)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _chartboostCoreGrantConsent((int)source, hashCode, OnConsentActionCompletion);
            return await proxy;
        }

        public async Task<bool> DenyConsent(ConsentStatusSource source)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _chartboostCoreDenyConsent((int)source, hashCode, OnConsentActionCompletion);
            return await proxy;
        }

        public async Task<bool> ResetConsent()
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _chartboostCoreResetConsent(hashCode, OnConsentActionCompletion);
            return await proxy;
        }

        public async Task<bool> ShowConsentDialog(ConsentDialogType dialogType)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _chartboostCoreShowConsentDialog((int)dialogType, hashCode, OnConsentActionCompletion);
            return await proxy;
        }
        
        public event ChartboostConsentChangeForStandard ConsentChangeForStandard;
        public event ChartboostConsentStatusChange ConsentStatusChange;
        public event Action ConsentModuleReady;

        [MonoPInvokeCallback(typeof(ChartboostCoreOnConsentChangeForStandard))]
        private static void OnConsentChangeForStandard(string standard, string value)
            => MainThreadDispatcher.Post(o => _instance?.ConsentChangeForStandard?.Invoke(standard, value));

        [MonoPInvokeCallback(typeof(ChartboostCoreOnConsentStatusChange))]
        private static void OnConsentStatusChange(int status) 
            => MainThreadDispatcher.Post(o => _instance?.ConsentStatusChange?.Invoke((ConsentStatus)status));

        [MonoPInvokeCallback(typeof(Action))]
        private static void OnConsentModuleReady()
            => MainThreadDispatcher.Post(o => _instance?.ConsentModuleReady?.Invoke());

        [MonoPInvokeCallback(typeof(ChartboostCoreOnConsentActionCompletion))]
        private static void OnConsentActionCompletion(int hashCode, bool completion)
            => MainThreadDispatcher.Post(o => AwaitableProxies.ResolveCallbackProxy(hashCode, completion));

        private delegate void ChartboostCoreOnConsentChangeForStandard(string standard, string value);
        private delegate void ChartboostCoreOnConsentStatusChange(int statusChange);
        private delegate void ChartboostCoreOnConsentActionCompletion(int hashCode, bool completion);
        
        [DllImport(IOSConstants.DLLImport)] private static extern int _chartboostCoreGetConsentStatus();
        [DllImport(IOSConstants.DLLImport)] private static extern string _chartboostCoreGetConsents();
        [DllImport(IOSConstants.DLLImport)] private static extern bool _chartboostCoreShouldCollectConsent();
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreGrantConsent(int statusSource, int hashCode, ChartboostCoreOnConsentActionCompletion callback);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreDenyConsent(int statusSource, int hashCode, ChartboostCoreOnConsentActionCompletion callback);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreResetConsent(int hashCode, ChartboostCoreOnConsentActionCompletion callback);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreShowConsentDialog(int dialogType, int hashCode, ChartboostCoreOnConsentActionCompletion callback);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreSetConsentCallbacks(ChartboostCoreOnConsentStatusChange onConsentStatusChange, ChartboostCoreOnConsentChangeForStandard onConsentChangeForStandard, Action onInitialConsentInfoAvailable);
    }
}
