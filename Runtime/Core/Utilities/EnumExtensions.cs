using Chartboost.Core.Environment;

namespace Chartboost.Core.Utilities
{
    /// <summary>
    /// Enum extensions class to wrap native string values to Enums. Not all platforms support this values as enum.
    /// </summary>
    internal static class EnumExtensions
    {
        private const string Unknown = "unknown";

        private const string Developer = "developer";
        
        private const string Wired = "wired";
        private const string Wifi = "wifi";
        private const string CellularUnknown = "cellularunknown";
        private const string Cellular_Unknown = "cellular_unknown";
        private const string Cellular2G = "cellular2g";
        private const string Cellular_2G = "cellular_2g";
        private const string Cellular3G = "cellular3g";
        private const string Cellular_3G = "cellular_3g";
        private const string Cellular4G = "cellular4g";
        private const string Cellular_4G = "cellular_4g";
        private const string Cellular5G = "cellular5g";
        private const string Cellular_5G = "cellular_5g";

        private const string Application = "application";

        private const string IsUserUnderage = "IS_USER_UNDERAGE"; 
        private const string PublisherAppIdentifier = "PUBLISHER_APP_IDENTIFIER"; 
        private const string PublisherSessionIdentifier = "PUBLISHER_SESSION_IDENTIFIER"; 
        private const string FrameworkName = "FRAMEWORK_NAME"; 
        private const string FrameworkVersion = "FRAMEWORK_VERSION"; 
        private const string PlayerIdentifier = "PLAYER_IDENTIFIER";

        /// <summary>
        /// Converts a string to a <see cref="NetworkConnectionType"/> enum value.
        /// </summary>
        /// <param name="source">The string value to convert.</param>
        /// <returns>The <see cref="NetworkConnectionType"/> matching value or <see cref="NetworkConnectionType.Unknown"/> if null or empty.</returns>
        public static NetworkConnectionType NetworkConnectionType(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Environment.NetworkConnectionType.Unknown;
            
            return source.ToLower() switch
            {
                Unknown => Environment.NetworkConnectionType.Unknown,
                Wired => Environment.NetworkConnectionType.Wired,
                Wifi => Environment.NetworkConnectionType.Wifi,
                CellularUnknown => Environment.NetworkConnectionType.CellularUnknown,
                Cellular_Unknown => Environment.NetworkConnectionType.CellularUnknown,
                Cellular2G => Environment.NetworkConnectionType.Cellular2G,
                Cellular_2G => Environment.NetworkConnectionType.Cellular2G,
                Cellular3G => Environment.NetworkConnectionType.Cellular3G,
                Cellular_3G => Environment.NetworkConnectionType.Cellular3G,
                Cellular4G => Environment.NetworkConnectionType.Cellular4G,
                Cellular_4G => Environment.NetworkConnectionType.Cellular4G,
                Cellular5G => Environment.NetworkConnectionType.Cellular5G,
                Cellular_5G => Environment.NetworkConnectionType.Cellular5G,
                _ => Environment.NetworkConnectionType.Unknown
            };
        }

        /// <summary>
        /// Converts a string to a <see cref="VendorIdentifierScope"/> enum value.
        /// </summary>
        /// <param name="source">The string value to convert.</param>
        /// <returns>The <see cref="VendorIdentifierScope"/> matching value or <see cref="VendorIdentifierScope.Unknown"/> if null or empty.</returns>
        public static VendorIdentifierScope VendorIdentifierScope(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Environment.VendorIdentifierScope.Unknown;

            return source.ToLower() switch
            {
                Unknown => Environment.VendorIdentifierScope.Unknown,
                Application => Environment.VendorIdentifierScope.Application,
                Developer => Environment.VendorIdentifierScope.Developer,
                _ => Environment.VendorIdentifierScope.Unknown
            };
        }

        internal static ObservableEnvironmentProperty? ToObservableProperty(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return null;

            return source switch
            {
                FrameworkName => ObservableEnvironmentProperty.FrameworkName,
                FrameworkVersion => ObservableEnvironmentProperty.FrameworkVersion,
                IsUserUnderage => ObservableEnvironmentProperty.IsUserUnderage,
                PlayerIdentifier => ObservableEnvironmentProperty.PlayerIdentifier,
                PublisherAppIdentifier => ObservableEnvironmentProperty.PublisherAppIdentifier,
                PublisherSessionIdentifier => ObservableEnvironmentProperty.PublisherSessionIdentifier,
                _ => null
            };
        }
    }
}
