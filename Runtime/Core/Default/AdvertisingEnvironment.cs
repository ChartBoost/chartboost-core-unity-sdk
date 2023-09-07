using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chartboost.Core.Environment;
using UnityEngine;

namespace Chartboost.Core.Default
{
    #nullable enable
    /// <summary>
    /// <inheritdoc cref="IAdvertisingEnvironment"/>
    /// <br/>
    /// <para> Default class implementation for unsupported platforms.</para>
    /// </summary>
    internal class AdvertisingEnvironment : IAdvertisingEnvironment
    {
        public AdvertisingEnvironment()
        {
            var (osName, osVersion) = FetchOSInfo();
            OSName = osName;
            OSVersion = osVersion;
            DeviceMake = FetchDeviceMake();
        }

        /// <inheritdoc cref="IAdvertisingEnvironment.OSName"/>
        public string OSName { get; }
        
        /// <inheritdoc cref="IAdvertisingEnvironment.OSVersion"/>
        public string OSVersion { get; }
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceMake"/>
        public string DeviceMake { get; }
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceModel"/>
        public string DeviceModel => SystemInfo.deviceModel;
        
        /// <inheritdoc cref="IAdvertisingEnvironment.DeviceLocale"/>
        public string DeviceLocale => System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenHeight"/>
        public double? ScreenHeight => Screen.height; 
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenScale"/>
        public double? ScreenScale => Screen.dpi;
        
        /// <inheritdoc cref="IAdvertisingEnvironment.ScreenWidth"/>
        public double? ScreenWidth => Screen.width;
        
        /// <inheritdoc cref="IAdvertisingEnvironment.BundleIdentifier"/>
        public string BundleIdentifier => Application.identifier;
        
        /// <inheritdoc cref="IAdvertisingEnvironment.LimitAdTrackingEnabled"/>
        public Task<bool?> LimitAdTrackingEnabled => Task.FromResult<bool?>(null);
        
        /// <inheritdoc cref="IAdvertisingEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => Task.FromResult<string?>(null);

        /// <summary>
        /// Tries to fetch OSInfo from Unity. Data obtained is not guaranteed to match with official platforms.
        /// </summary>
        /// <returns>OSName and OSVersion</returns>
        internal static (string, string) FetchOSInfo()
        {
            var osInfo = SystemInfo.operatingSystem;
            var osName = "Unknown OS";
            var osVersion = "Unknown Version";

            const string osPattern = @"^([^\d]+)([\d\.]+)";
            var matchOS = Regex.Match(osInfo, osPattern);

            if (!matchOS.Success) 
                return (osName, osVersion);
            
            osName = matchOS.Groups[1].Value.Trim();
            osVersion = matchOS.Groups[2].Value.Trim();
            return (osName, osVersion);
        }

        /// <summary>
        /// Tries to fetch DeviceMake from Unity. Data obtained is not guaranteed to match with official platforms.
        /// </summary>
        /// <returns>Unity's DeviceMake</returns>
        internal static string FetchDeviceMake()
        {
            const string makePattern = @"^[\w-]+";
            var matchMake = Regex.Match(SystemInfo.deviceModel, makePattern);

            return matchMake.Success ? matchMake.Value : "Unknown";
        }
    }
    #nullable disable
}
