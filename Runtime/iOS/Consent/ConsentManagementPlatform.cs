using AOT;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Constants;
using Chartboost.Core.Consent;
using Chartboost.Core.Utilities;

namespace Chartboost.Core.iOS.Consent
{
    /// <summary>
    /// <inheritdoc cref="IConsentManagementPlatform"/>
    /// <br/>
    /// <para>iOS implementation.</para>
    /// </summary>
    internal class ConsentManagementPlatform : IConsentManagementPlatform
    {
        private static ConsentManagementPlatform _instance;

        static ConsentManagementPlatform()
            => _CBCSetConsentCallbacks(OnConsentChangeWithFullConsents, OnConsentModuleReadyWithInitialConsents);

        internal ConsentManagementPlatform()
        {
            _instance ??= this;
            ConsentChangeWithFullConsents = null!;
            ConsentModuleReadyWithInitialConsents = null!;
        }

        /// <inheritdoc cref="IConsentManagementPlatform.Consents"/>
        public IReadOnlyDictionary<ConsentKey, ConsentValue> Consents 
            => _CBCGetConsents().ToConsentDictionary();

        /// <inheritdoc cref="IConsentManagementPlatform.ShouldCollectConsent"/>
        public bool ShouldCollectConsent 
            => _CBCShouldCollectConsent();

        /// <inheritdoc cref="IConsentManagementPlatform.GrantConsent"/>
        public async Task<bool> GrantConsent(ConsentSource source)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _CBCGrantConsent((int)source, hashCode, OnConsentActionCompletion);
            return await proxy;
        }

        /// <inheritdoc cref="IConsentManagementPlatform.DenyConsent"/>
        public async Task<bool> DenyConsent(ConsentSource source)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _CBCDenyConsent((int)source, hashCode, OnConsentActionCompletion);
            return await proxy;
        }

        /// <inheritdoc cref="IConsentManagementPlatform.ResetConsent"/>
        public async Task<bool> ResetConsent()
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _CBCResetConsent(hashCode, OnConsentActionCompletion);
            return await proxy;
        }
        
        /// <inheritdoc cref="IConsentManagementPlatform.ShowConsentDialog"/>
        public async Task<bool> ShowConsentDialog(ConsentDialogType dialogType)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<bool>();
            _CBCShowConsentDialog((int)dialogType, hashCode, OnConsentActionCompletion);
            return await proxy;
        }
        
        #nullable enable
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeWithFullConsents"/>
        public event ChartboostCoreConsentChangeWithFullConsents? ConsentChangeWithFullConsents;
        
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReadyWithInitialConsents"/>
        public event ChartboostCoreConsentModuleReadyWithInitialConsents? ConsentModuleReadyWithInitialConsents;
        #nullable disable

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeWithFullConsents"/>
        [MonoPInvokeCallback(typeof(ExternChartboostCoreOnConsentChangeWithFullConsents))]
        private static void OnConsentChangeWithFullConsents(string consentsJson, string modifiedKeysJson)
            => MainThreadDispatcher.Post(_ => _instance?.ConsentChangeWithFullConsents?.Invoke(consentsJson.ToConsentDictionary(), modifiedKeysJson.ToConsentKeys()));

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReadyWithInitialConsents"/>
        [MonoPInvokeCallback(typeof(ExternChartboostCoreOnConsentReadyWithInitialConsents))]
        private static void OnConsentModuleReadyWithInitialConsents(string consentsJson) 
            => MainThreadDispatcher.Post(_ => _instance?.ConsentModuleReadyWithInitialConsents?.Invoke(consentsJson.ToConsentDictionary()));

        /// <summary>
        /// Utilized to await on consent actions
        /// </summary>
        /// <param name="hashCode">Hashcode associated to Awaitable Proxy.</param>
        /// <param name="completion">Result of the operation.</param>
        [MonoPInvokeCallback(typeof(ExternChartboostCoreOnResultBoolean))]
        private static void OnConsentActionCompletion(int hashCode, bool completion)
            => MainThreadDispatcher.Post(_ => AwaitableProxies.ResolveCallbackProxy(hashCode, completion));

        [DllImport(SharedIOSConstants.DLLImport)] private static extern string _CBCGetConsents();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern bool _CBCShouldCollectConsent();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCGrantConsent(int statusSource, int hashCode, ExternChartboostCoreOnResultBoolean callback);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCDenyConsent(int statusSource, int hashCode, ExternChartboostCoreOnResultBoolean callback);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCResetConsent(int hashCode, ExternChartboostCoreOnResultBoolean callback);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCShowConsentDialog(int dialogType, int hashCode, ExternChartboostCoreOnResultBoolean callback);
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetConsentCallbacks(ExternChartboostCoreOnConsentChangeWithFullConsents onConsentChangeWithFullConsents, ExternChartboostCoreOnConsentReadyWithInitialConsents onInitialConsentInfoAvailable);
    }
}
