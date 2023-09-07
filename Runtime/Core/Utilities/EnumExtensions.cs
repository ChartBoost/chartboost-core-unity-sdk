using System;
using Chartboost.Core.Consent;
using Chartboost.Core.Environment;

namespace Chartboost.Core.Utilities
{
    /// <summary>
    /// Enum extensions class to wrap native string values to Enums. Not all platforms support this values as enum.
    /// </summary>
    internal static class EnumExtensions
    {
        private const string DialogTypeConcise = "concise";
        private const string DialogTypeDetailed = "detailed";

        private const string GPP = "gpp";
        private const string GDPRConsentGiven = "gdpr_consent_given";
        private const string CCPAOptIn = "ccpa_opt_in";
        private const string TCF = "tcf";
        private const string USP = "usp";

        private const string Granted = "granted";
        private const string Denied = "denied";
        private const string Unknown = "unknown";

        private const string User = "user";
        private const string Developer = "developer";

        private const string DoesNotApply = "does_not_apply";
        
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

        /// <summary>
        /// Converts a string to a <see cref="ConsentDialogType"/> enum value.
        /// </summary>
        /// <param name="source">The string value to convert.</param>
        /// <returns>The <see cref="ConsentDialogType"/> matching value or <see cref="ConsentDialogType.Detailed"/> if null or empty.</returns>
        public static ConsentDialogType DialogType(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return ConsentDialogType.Detailed;

            return source.ToLower() switch
            {
                DialogTypeConcise => ConsentDialogType.Concise,
                DialogTypeDetailed => ConsentDialogType.Detailed,
                _ => ConsentDialogType.Detailed
            };
        }

        /// <summary>
        /// Converts a string to a <see cref="ConsentStatus"/> enum value.
        /// </summary>
        /// <param name="source">The string value to convert.</param>
        /// <returns>The <see cref="ConsentStatus"/> matching value or <see cref="ConsentStatus.Unknown"/> if null or empty.</returns>
        public static ConsentStatus ConsentStatus(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Consent.ConsentStatus.Unknown;

            return source.ToLower() switch
            {
                Granted => Consent.ConsentStatus.Granted,
                Denied => Consent.ConsentStatus.Denied,
                Unknown => Consent.ConsentStatus.Unknown,
                _ => Consent.ConsentStatus.Unknown
            };
        }

        /// <summary>
        /// Converts a string to a <see cref="ConsentStatusSource"/> enum value.
        /// </summary>
        /// <param name="source">The string value to convert.</param>
        /// <returns>The <see cref="ConsentStatusSource"/> matching value or <see cref="ConsentStatusSource.Developer"/> if null or empty.</returns>
        public static ConsentStatusSource ConsentStatusSource(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return Consent.ConsentStatusSource.Developer;

            return source.ToLower() switch
            {
                User => Consent.ConsentStatusSource.User,
                Developer => Consent.ConsentStatusSource.Developer,
                _ => Consent.ConsentStatusSource.Developer
            };
        }

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
    }
}
