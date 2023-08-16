using System.ComponentModel;
using Chartboost.Core.Utilities;

namespace Chartboost.Core.Consent
{
    [TypeConverter(typeof(StringTypeConverter<ConsentValue>))]
    public class ConsentValue : StronglyTyped<string>
    {
        public static readonly ConsentValue Granted = "granted";
        public static readonly ConsentValue Denied = "denied";
        public static readonly ConsentValue DoesNotApply = "does_not_apply";

        public ConsentValue(string value) : base(value) { }
        
        public static implicit operator ConsentValue(string obj)
        {
            return new ConsentValue(obj);
        }
    }
}
