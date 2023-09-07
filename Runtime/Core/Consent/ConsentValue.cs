using System.ComponentModel;
using Chartboost.Core.Utilities;

namespace Chartboost.Core.Consent
{
    /// <summary>
    /// A model representing a consent value for a specific standard, like IAB’s TCF or USP strings. It is used by <see cref="IConsentManagementPlatform"/>> to provide detailed consent status.
    /// </summary>
    [TypeConverter(typeof(StringTypeConverter<ConsentValue>))]
    public class ConsentValue : StronglyTyped<string>
    {
        /// <summary>
        /// Indicates the user granted consent.
        /// </summary>
        public static readonly ConsentValue Granted = "granted";
        
        /// <summary>
        /// Indicates the user denied consent.
        /// </summary>
        public static readonly ConsentValue Denied = "denied";
        
        /// <summary>
        /// Indicates the standard does not apply.
        /// </summary>
        public static readonly ConsentValue DoesNotApply = "does_not_apply";

        /// <summary>
        /// Creates a custom value with an arbitrary string value.
        /// </summary>
        public ConsentValue(string value) : base(value) { }
        
        public static implicit operator ConsentValue(string obj)
        {
            return new ConsentValue(obj);
        }
    }
}
