using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chartboost.Core.Consent
{
    #nullable enable
    /// <summary>
    /// Chartboost Core’s CMP in charge of handling user consent management.
    /// Chartboost Core defines a unified API for publishers to request and query user consent, and relies on a 3rd-party CMP SDK to provide the CMP functionality.
    /// </summary>
    public interface IConsentManagementPlatform
    {
        /// <summary>
        /// The current consent status determined by the CMP. Returns <see cref="Chartboost.Core.Consent.ConsentStatus.Unknown"/> if no consent adapter module is available.
        /// </summary>
        abstract ConsentStatus ConsentStatus { get; }

        /// <summary>
        /// Detailed consent status for each consent standard, as determined by the CMP.
        /// Returns an empty dictionary if no consent adapter module is available.
        /// </summary>
        abstract Dictionary<ConsentStandard, ConsentValue?> Consents { get; }

        /// <summary>
        /// Indicates whether the CMP has determined that consent should be collected from the user.
        /// Returns false if no consent adapter module is available.
        /// </summary>
        abstract bool ShouldCollectConsent { get; }

        /// <summary>
        /// Informs the CMP that the user has granted consent.
        /// This method should be used only when a custom consent dialog is presented to the user, thereby making the publisher responsible for the UI-side of collecting consent
        /// In most cases <see cref="IConsentManagementPlatform.ShowConsentDialog"/> should be used instead.
        /// </summary>
        /// <param name="source">The source of the new consent. See the <see cref="Chartboost.Core.Consent.ConsentStatusSource"/> documentation for more info</param>
        /// <returns>Indicates if the operation went through successfully or not</returns>
        abstract Task<bool> GrantConsent(ConsentStatusSource source);

        /// <summary>
        /// Informs the CMP that the user has denied consent.
        /// This method should be used only when a custom consent dialog is presented to the user, thereby making the publisher responsible for the UI-side of collecting consent
        /// In most cases <see cref="IConsentManagementPlatform.ShowConsentDialog"/> should be used instead.
        /// </summary>
        /// <param name="source">The source of the new consent. See the <see cref="Chartboost.Core.Consent.ConsentStatusSource"/> documentation for more info</param>
        /// <returns>Indicates if the operation went through successfully or not</returns>
        abstract Task<bool> DenyConsent(ConsentStatusSource source);

        /// <summary>
        /// Informs the CMP that the given consent should be reset.
        /// Informs the CMP that the given consent should be reset. If the CMP does not support the ResetConsent() function or the operation fails for any other reason, the Task is executed with a false parameter.
        /// </summary>
        /// <returns>Indicates if the operation went through successfully or not</returns>
        abstract Task<bool> ResetConsent();

        /// <summary>
        /// Instructs the CMP to present a consent dialog to the user for the purpose of collecting consent.
        /// </summary>
        /// <param name="dialogType">The type of consent dialog to present. See the <see cref="Chartboost.Core.Consent.ConsentDialogType"/> documentation for more info.
        /// If the CMP does not support a given type, it should default to whatever type it does support.</param>
        /// <returns>Indicates if the operation went through successfully or not</returns>
        abstract Task<bool> ShowConsentDialog(ConsentDialogType dialogType);

        /// <summary>
        /// Called whenever the <see cref="IConsentManagementPlatform.Consents"/> value changed.
        /// </summary>
        abstract event ChartboostConsentChangeForStandard ConsentChangeForStandard;
        
        /// <summary>
        /// Called whenever the <see cref="IConsentManagementPlatform.ConsentStatus"/> value changed.
        /// </summary>
        abstract event ChartboostConsentStatusChange ConsentStatusChange;
        
        /// <summary>
        /// Called when the initial values for <see cref="IConsentManagementPlatform.ConsentStatus"/> and <see cref="IConsentManagementPlatform.Consents"/> first become available for the current session.
        /// </summary>
        abstract event Action ConsentModuleReady;
    }
    #nullable disable
}
