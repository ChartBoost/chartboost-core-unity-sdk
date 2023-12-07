using Chartboost.Core.Consent;
using Chartboost.Core.Initialization;

namespace Chartboost.Core
{
    #nullable enable
    public delegate void ChartboostCoreModuleInitializationDelegate(ModuleInitializationResult result);
    public delegate void ChartboostConsentChangeForStandard(ConsentStandard standard, ConsentValue? value);
    public delegate void ChartboostPartnerConsentStatusChange(string partnerIdentifier, ConsentStatus status);
    public delegate void ChartboostConsentStatusChange(ConsentStatus status);
    #nullable disable
}
