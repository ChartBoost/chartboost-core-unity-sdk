using System.ComponentModel;
using Chartboost.Core.Utilities;

namespace Chartboost.Core.Consent
{
    [TypeConverter(typeof(StringTypeConverter<ConsentStandard>))]
    public class ConsentStandard : StronglyTyped<string>
    {
        public static readonly ConsentStandard GDPRConsentGiven = "gdpr_consent_given";
        public static readonly ConsentStandard CCPAOptIn =  "ccpa_opt_in";
        public static readonly ConsentStandard USP = "usp";
        public static readonly ConsentStandard TCF = "tcf";
        public static readonly ConsentStandard GPP = "gpp";

        public ConsentStandard(string value) : base(value) { }
        
        public static implicit operator ConsentStandard(string obj)
        {
            return new ConsentStandard(obj);
        }
    }
}
