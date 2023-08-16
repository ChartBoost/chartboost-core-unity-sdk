namespace Chartboost.Core.Environment
{
    public enum AuthorizationStatus
    {
        Unsupported = -1,
        NotDetermined = 0,
        Restricted = 1,
        Denied = 2,
        Authorized = 3,
    }
}
