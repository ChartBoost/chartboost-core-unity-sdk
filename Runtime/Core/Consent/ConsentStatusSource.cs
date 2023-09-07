namespace Chartboost.Core.Consent
{
    /// <summary>
    /// The source of a consent status.
    /// </summary>
    public enum ConsentStatusSource
    {
        /// <summary>
        /// The consent was collected from the user as a result of an explicit user action.
        /// </summary>
        User = 0,
        
        /// <summary>
        /// The consent was set by the developer without an explicit user action.
        /// </summary>
        Developer = 1
    }
}
