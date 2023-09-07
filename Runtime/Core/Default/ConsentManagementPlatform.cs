using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Consent;

namespace Chartboost.Core.Default
{
    /// <summary>
    /// <inheritdoc cref="IConsentManagementPlatform"/>
    /// <br/>
    /// <para> Default class implementation for unsupported platforms.</para>
    /// </summary>
    internal class ConsentManagementPlatform : IConsentManagementPlatform
    {
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatus"/>
        public ConsentStatus ConsentStatus => ConsentStatus.Unknown;
        
        /// <inheritdoc cref="IConsentManagementPlatform.Consents"/>
        public Dictionary<ConsentStandard, ConsentValue> Consents { get; } = new Dictionary<ConsentStandard, ConsentValue>();
        
        /// <inheritdoc cref="IConsentManagementPlatform.ShouldCollectConsent"/>
        public bool ShouldCollectConsent => false;
        
        /// <inheritdoc cref="IConsentManagementPlatform.GrantConsent"/>
        public Task<bool> GrantConsent(ConsentStatusSource source) 
            => Task.FromResult(false);

        /// <inheritdoc cref="IConsentManagementPlatform.DenyConsent"/>
        public Task<bool> DenyConsent(ConsentStatusSource source) 
            => Task.FromResult(false);

        /// <inheritdoc cref="IConsentManagementPlatform.ResetConsent"/>
        public Task<bool> ResetConsent() 
            => Task.FromResult(false);

        /// <inheritdoc cref="IConsentManagementPlatform.ShowConsentDialog"/>
        public Task<bool> ShowConsentDialog(ConsentDialogType dialogType) 
            => Task.FromResult(false);

        #pragma warning disable 0067
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeForStandard"/>
        public event ChartboostConsentChangeForStandard ConsentChangeForStandard;
        
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentStatusChange"/>
        public event ChartboostConsentStatusChange ConsentStatusChange;
        
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReady"/>
        public event Action ConsentModuleReady;
        #pragma warning restore 0067
    }
}
