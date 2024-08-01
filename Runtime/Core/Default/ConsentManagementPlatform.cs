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
        /// <inheritdoc cref="IConsentManagementPlatform.Consents"/>
        public IReadOnlyDictionary<ConsentKey, ConsentValue> Consents { get; } = new Dictionary<ConsentKey, ConsentValue>();

        /// <inheritdoc cref="IConsentManagementPlatform.ShouldCollectConsent"/>
        public bool ShouldCollectConsent => false;
        
        /// <inheritdoc cref="IConsentManagementPlatform.GrantConsent"/>
        public Task<bool> GrantConsent(ConsentSource source) 
            => Task.FromResult(false);

        /// <inheritdoc cref="IConsentManagementPlatform.DenyConsent"/>
        public Task<bool> DenyConsent(ConsentSource source) 
            => Task.FromResult(false);

        /// <inheritdoc cref="IConsentManagementPlatform.ResetConsent"/>
        public Task<bool> ResetConsent() 
            => Task.FromResult(false);

        /// <inheritdoc cref="IConsentManagementPlatform.ShowConsentDialog"/>
        public Task<bool> ShowConsentDialog(ConsentDialogType dialogType) 
            => Task.FromResult(false);

        #nullable enable
        #pragma warning disable 0067
        /// <inheritdoc cref="IConsentManagementPlatform.ConsentChangeWithFullConsents"/>
        public event ChartboostCoreConsentChangeWithFullConsents? ConsentChangeWithFullConsents;

        /// <inheritdoc cref="IConsentManagementPlatform.ConsentModuleReadyWithInitialConsents"/>
        public event ChartboostCoreConsentModuleReadyWithInitialConsents? ConsentModuleReadyWithInitialConsents;
        #pragma warning restore 0067
        #nullable disable
    }
}
