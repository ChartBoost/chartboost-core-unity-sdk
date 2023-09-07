namespace Chartboost.Core.Environment
{
    /// <summary>
    /// The status values for app tracking authorization.
    /// As defined in <a href="https://developer.apple.com/documentation/apptrackingtransparency/attrackingmanager/authorizationstatus">Apple's Documentation</a>
    /// </summary>
    public enum AuthorizationStatus
    {
        /// <summary>
        /// The value that returns when Core is not running in an iOS environment.
        /// </summary>
        Unsupported = -1,
        
        /// <summary>
        /// The value that returns when the app can’t determine the user’s authorization status for access to app-related data for tracking the user or the device.
        /// </summary>
        NotDetermined = 0,
        
        /// <summary>
        /// The value that returns if authorization to access app-related data for tracking the user or the device has a restricted status.
        /// </summary>
        Restricted = 1,
        
        /// <summary>
        /// The value that returns if the user denies authorization to access app-related data for tracking the user or the device.
        /// </summary>
        Denied = 2,
        
        /// <summary>
        /// The value that returns if the user authorizes access to app-related data for tracking the user or the device.
        /// </summary>
        Authorized = 3,
    }
}
