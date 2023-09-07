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
    /// <summary>
    /// <inheritdoc cref="IConsentManagementPlatform"/>
    /// <br/>
    /// <para>iOS implementation.</para>
    /// </summary>
    public class ConsentManagementPlatform : IConsentManagementPlatform
    {
        private static ConsentManagementPlatform _instance;

        static ConsentManagementPlatform()
            => _chartboostCoreSetConsentCallbacks(OnConsentStatusChange, OnConsentChangeForStandard, OnConsentModuleReady);

        internal ConsentManagementPlatform() 
            => _instance ??= this;

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatus"/>
        public ConsentStatus ConsentStatus 
            => (ConsentStatus)_chartboostCoreGetConsentStatus();

        /// <inheritdoc cref="IConsentManagementPlatform.Consents"/>
        public Dictionary<ConsentStandard, ConsentValue> Consents
        {
            get
            {
                var json = _chartboostCoreGetConsents();
                return JsonConvert.DeserializeObject<Dictionary<ConsentStandard, ConsentValue>>(json);
            }
        }

        /// <inheritdoc cref="IConsentManagementPlatform.ShouldCollectConsent"/>
        public bool ShouldCollectConsent 
            => _chartboostCoreShouldCollectConsent();

        /// <inheritdoc cref="IConsentManagementPlatform.GrantConsent"/>
        public async Task<bool> GrantConsent(ConsentStatusSource source)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _chartboostCoreGrantConsent((int)source, hashCode, OnConsentActionCompletion);
            return await proxy;
        }

        /// <inheritdoc cref="IConsentManagementPlatform.DenyConsent"/>
        public async Task<bool> DenyConsent(ConsentStatusSource source)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _chartboostCoreDenyConsent((int)source, hashCode, OnConsentActionCompletion);
            return await proxy;
        }

        /// <inheritdoc cref="IConsentManagementPlatform.ResetConsent"/>
        public async Task<bool> ResetConsent()
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _chartboostCoreResetConsent(hashCode, OnConsentActionCompletion);
            return await proxy;
        }
        
        /// <inheritdoc cref="IConsentManagementPlatform.ShowConsentDialog"/>
        public async Task<bool> ShowConsentDialog(ConsentDialogType dialogType)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _chartboostCoreShowConsentDialog((int)dialogType, hashCode, OnConsentActionCompletion);
            return await proxy;
        }
        
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeForStandard"/>
        public event ChartboostConsentChangeForStandard ConsentChangeForStandard;
        
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatusChange"/>
        public event ChartboostConsentStatusChange ConsentStatusChange;
        
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReady"/>
        public event Action ConsentModuleReady;

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeForStandard"/>
        /// <param name="standard">The <see cref="ConsentStandard"/> obtained from the observer.</param>
        /// <param name="value">The <see cref="ConsentValue"/> obtained from the observer.</param>
        [MonoPInvokeCallback(typeof(ChartboostCoreOnConsentChangeForStandard))]
        private static void OnConsentChangeForStandard(string standard, string value)
            => MainThreadDispatcher.Post(o => _instance?.ConsentChangeForStandard?.Invoke(standard, value));

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatusChange"/>
        /// <param name="status">The <see cref="ConsentStatus"/> native value.</param>
        [MonoPInvokeCallback(typeof(ChartboostCoreOnConsentStatusChange))]
        private static void OnConsentStatusChange(int status) 
            => MainThreadDispatcher.Post(o => _instance?.ConsentStatusChange?.Invoke((ConsentStatus)status));

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReady"/>
        [MonoPInvokeCallback(typeof(Action))]
        private static void OnConsentModuleReady()
            => MainThreadDispatcher.Post(o => _instance?.ConsentModuleReady?.Invoke());

        /// <summary>
        /// Utilized to await on consent actions
        /// </summary>
        /// <param name="hashCode">Hashcode associated to Awaitable Proxy.</param>
        /// <param name="completion">Result of the operation.</param>
        [MonoPInvokeCallback(typeof(ChartboostCoreOnResultBoolean))]
        private static void OnConsentActionCompletion(int hashCode, bool completion)
            => MainThreadDispatcher.Post(o => AwaitableProxies.ResolveCallbackProxy(hashCode, completion));

        private delegate void ChartboostCoreOnConsentChangeForStandard(string standard, string value);
        private delegate void ChartboostCoreOnConsentStatusChange(int statusChange);
        
        [DllImport(IOSConstants.DLLImport)] private static extern int _chartboostCoreGetConsentStatus();
        [DllImport(IOSConstants.DLLImport)] private static extern string _chartboostCoreGetConsents();
        [DllImport(IOSConstants.DLLImport)] private static extern bool _chartboostCoreShouldCollectConsent();
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreGrantConsent(int statusSource, int hashCode, ChartboostCoreOnResultBoolean callback);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreDenyConsent(int statusSource, int hashCode, ChartboostCoreOnResultBoolean callback);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreResetConsent(int hashCode, ChartboostCoreOnResultBoolean callback);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreShowConsentDialog(int dialogType, int hashCode, ChartboostCoreOnResultBoolean callback);
        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreSetConsentCallbacks(ChartboostCoreOnConsentStatusChange onConsentStatusChange, ChartboostCoreOnConsentChangeForStandard onConsentChangeForStandard, Action onInitialConsentInfoAvailable);
    }
}
