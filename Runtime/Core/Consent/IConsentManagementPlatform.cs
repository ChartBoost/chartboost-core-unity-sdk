using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chartboost.Core.Consent
{
    #nullable enable
    public interface IConsentManagementPlatform
    {
        abstract ConsentStatus ConsentStatus { get; }

        abstract Dictionary<ConsentStandard, ConsentValue?> Consents { get; }

        abstract bool ShouldCollectConsent { get; }

        abstract Task<bool> GrantConsent(ConsentStatusSource source);

        abstract Task<bool> DenyConsent(ConsentStatusSource source);

        abstract Task<bool> ResetConsent();

        abstract Task<bool> ShowConsentDialog(ConsentDialogType dialogType);

        abstract event ChartboostConsentChangeForStandard ConsentChangeForStandard;
        
        abstract event ChartboostConsentStatusChange ConsentStatusChange;

        abstract event Action ConsentModuleReady;
    }
    #nullable disable
}
