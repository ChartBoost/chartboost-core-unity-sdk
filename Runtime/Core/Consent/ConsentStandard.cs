using System.ComponentModel;
using Chartboost.Core.Utilities;

namespace Chartboost.Core.Consent
{
    /// <summary>
    /// A model representing a user consent standard, like IAB’s TCF or USP strings. It is used by <see cref="IConsentManagementPlatform"/>> to provide detailed consent status.
    /// </summary>
    [TypeConverter(typeof(StringTypeConverter<ConsentStandard>))]
    public class ConsentStandard : StronglyTyped<string>
    {
        /// <summary>
        /// GDPR opt-in standard. Possible values are:
        /// <see cref="Chartboost.Core.Consent.ConsentValue.Denied"/>,
        /// <see cref="Chartboost.Core.Consent.ConsentValue.Granted"/>, 
        /// <see cref="Chartboost.Core.Consent.ConsentValue.DoesNotApply"/>.
        ///</summary>
        public static readonly ConsentStandard GDPRConsentGiven = "gdpr_consent_given";
        
        /// <summary>
        /// CCPA opt-in standard. Possible values are:
        /// <see cref="Chartboost.Core.Consent.ConsentValue.Denied"/>,
        /// <see cref="Chartboost.Core.Consent.ConsentValue.Granted"/>, 
        /// <see cref="Chartboost.Core.Consent.ConsentValue.DoesNotApply"/>.
        ///</summary>
        public static readonly ConsentStandard CCPAOptIn =  "ccpa_opt_in";
        
        /// <summary>
        /// USP standard. Possible values are IAB’s USP strings, as defined in <a href="https://github.com/InteractiveAdvertisingBureau/USPrivacy/blob/master/CCPA/US%20Privacy%20String.md">InteractiveAdvertisingBureau/USPrivacy</a>
        /// </summary>
        public static readonly ConsentStandard USP = "usp";
        
        /// <summary>
        /// TCF standard. Possible values are IAB’s TCF strings, as defined in <a href="https://github.com/InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework/blob/master/TCFv2/IAB%20Tech%20Lab%20-%20Consent%20string%20and%20vendor%20list%20formats%20v2.md">InteractiveAdvertisingBureau/GDPR-Transparency-and-Consent-Framework</a>
        /// </summary>
        public static readonly ConsentStandard TCF = "tcf";
        
        /// <summary>
        /// GPP standard. Possible values are IAB’s GPP strings, as defined in <a href="https://github.com/InteractiveAdvertisingBureau/Global-Privacy-Platform/blob/main/Core/Consent%20String%20Specification.md">InteractiveAdvertisingBureau/Global-Privacy-Platform</a>
        /// </summary>
        public static readonly ConsentStandard GPP = "gpp";

        /// <summary>
        /// Creates a custom standard with an arbitrary string value.
        /// </summary>
        public ConsentStandard(string value) : base(value) { }
        
        public static implicit operator ConsentStandard(string obj)
        {
            return new ConsentStandard(obj);
        }
    }
}
