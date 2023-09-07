namespace Chartboost.Core.Consent
{
    /// <summary>
    /// The type of consent dialog to be presented to the user.
    /// </summary>
    public enum ConsentDialogType
    {
        /// <summary>
        /// A non-intrusive dialog used to collect consent, presenting a minimum amount of information.
        /// </summary>
        Concise = 0,
        
        /// <summary>
        /// A dialog used to collect consent, presenting detailed information and possibly allowing for granular consent choices.
        /// </summary>
        Detailed = 1
    }
}
