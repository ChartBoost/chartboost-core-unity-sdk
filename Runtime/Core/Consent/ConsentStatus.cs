namespace Chartboost.Core.Consent
{
    /// <summary>
    /// User consent status.
    /// </summary>
    public enum ConsentStatus
    {
        /// <summary>
        /// Indicates that the consent is unknown, possibly because the user was never asked for consent.
        /// </summary>
        Unknown = -1,
        
        /// <summary>
        /// Indicates the user denied consent.
        /// </summary>
        Denied = 0,
        
        /// <summary>
        /// Indicates the user granted consent.
        /// </summary>
        Granted = 1
    }
}
