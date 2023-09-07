using System.Threading.Tasks;

namespace Chartboost.Core.Environment
{
    #nullable enable
    /// <summary>
    /// An environment that contains information intended solely for advertising purposes.
    /// </summary>
    public interface IAdvertisingEnvironment
    {
        /// <summary>
        /// The OS name, e.g. “iOS” or “iPadOS”.
        /// </summary>
        string OSName { get; }
        
        /// <summary>
        /// The OS version, e.g. “17.0”.
        /// </summary>
        string OSVersion { get; }
        
        /// <summary>
        /// The device make, e.g. “Apple”.
        /// </summary>
        string DeviceMake { get; }
        
        /// <summary>
        /// The device model, e.g. “iPhone11,2”.
        /// </summary>
        string DeviceModel { get; }
        
        /// <summary>
        /// The device locale string, e.g. “en-US”.
        /// </summary>
        string? DeviceLocale { get; }
        
        /// <summary>
        /// The height of the screen in pixels.
        /// </summary>
        double? ScreenHeight { get; }
        
        /// <summary>
        /// The screen scale.
        /// </summary>
        double? ScreenScale { get; }
        
        /// <summary>
        /// The width of the screen in pixels.
        /// </summary>
        double? ScreenWidth { get; }
        
        /// <summary>
        /// The app bundle identifier.
        /// </summary>
        string? BundleIdentifier { get; }
        
        /// <summary>
        /// Indicates whether the user has limited ad tracking enabled.
        /// </summary>
        Task<bool?> LimitAdTrackingEnabled { get; }
        
        /// <summary>
        /// The system advertising identifier (IFA).
        /// </summary>
        Task<string?> AdvertisingIdentifier { get; }
    }
    #nullable disable
}
