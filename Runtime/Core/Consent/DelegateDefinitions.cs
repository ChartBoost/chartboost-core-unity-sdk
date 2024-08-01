using System.Collections.Generic;

namespace Chartboost.Core.Consent
{
    #nullable enable
    public delegate void ChartboostCoreConsentChangeWithFullConsents(IReadOnlyDictionary<ConsentKey, ConsentValue> fullConsents, IReadOnlyCollection<ConsentKey> modifiedKeys);

    public delegate void ChartboostCoreConsentModuleReadyWithInitialConsents(IReadOnlyDictionary<ConsentKey, ConsentValue> initialConsents);
}
