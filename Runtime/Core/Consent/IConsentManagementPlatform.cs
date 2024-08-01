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
        /// Detailed consent status for each consent standard, as determined by the CMP.
        /// Returns an empty dictionary if no consent adapter module is available.
        /// </summary>
        IReadOnlyDictionary<ConsentKey, ConsentValue?> Consents { get; }

        /// <summary>
        /// Indicates whether the CMP has determined that consent should be collected from the user.
        /// Returns false if no consent adapter module is available.
        /// </summary>
        bool ShouldCollectConsent { get; }

        /// <summary>
        /// Informs the CMP that the user has granted consent.
        /// This method should be used only when a custom consent dialog is presented to the user, thereby making the publisher responsible for the UI-side of collecting consent
        /// In most cases <see cref="IConsentManagementPlatform.ShowConsentDialog"/> should be used instead.
        /// </summary>
        /// <param name="source">The source of the new consent. See the <see cref="ConsentSource"/> documentation for more info</param>
        /// <returns>Indicates if the operation went through successfully or not</returns>
        Task<bool> GrantConsent(ConsentSource source);

        /// <summary>
        /// Informs the CMP that the user has denied consent.
        /// This method should be used only when a custom consent dialog is presented to the user, thereby making the publisher responsible for the UI-side of collecting consent
        /// In most cases <see cref="IConsentManagementPlatform.ShowConsentDialog"/> should be used instead.
        /// </summary>
        /// <param name="source">The source of the new consent. See the <see cref="ConsentSource"/> documentation for more info</param>
        /// <returns>Indicates if the operation went through successfully or not</returns>
        Task<bool> DenyConsent(ConsentSource source);

        /// <summary>
        /// Informs the CMP that the given consent should be reset. If the CMP does not support the ResetConsent() function or the operation fails for any other reason, the Task is executed with a false parameter.
        /// </summary>
        /// <returns>Indicates if the operation went through successfully or not</returns>
        Task<bool> ResetConsent();

        /// <summary>
        /// Instructs the CMP to present a consent dialog to the user for the purpose of collecting consent.
        /// </summary>
        /// <param name="dialogType">The type of consent dialog to present. See the <see cref="Chartboost.Core.Consent.ConsentDialogType"/> documentation for more info.
        /// If the CMP does not support a given type, it should default to whatever type it does support.</param>
        /// <returns>Indicates if the operation went through successfully or not</returns>
        Task<bool> ShowConsentDialog(ConsentDialogType dialogType);

        /// <summary>
        /// Called whenever the <see cref="IConsentManagementPlatform.Consents"/> value changed.
        /// </summary>
        event ChartboostCoreConsentChangeWithFullConsents ConsentChangeWithFullConsents;
        
        /// <summary>
        /// Called when the initial values for <see cref="IConsentManagementPlatform.Consents"/> first become available for the current session.
        /// </summary>
        event ChartboostCoreConsentModuleReadyWithInitialConsents ConsentModuleReadyWithInitialConsents;
    }
}
