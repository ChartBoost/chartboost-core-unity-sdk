using System;
using Chartboost.Core.Consent;
using Chartboost.Core.Environment;

namespace Chartboost.Core.Utilities
{
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

        public static ConsentDialogType GetDialogType(this string source)
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

        public static ConsentStatus GetConsentStatus(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return ConsentStatus.Unknown;

            return source.ToLower() switch
            {
                Granted => ConsentStatus.Granted,
                Denied => ConsentStatus.Denied,
                Unknown => ConsentStatus.Unknown,
                _ => ConsentStatus.Unknown
            };
        }

        public static ConsentStatusSource GetConsentStatusSource(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return ConsentStatusSource.Developer;

            return source.ToLower() switch
            {
                User => ConsentStatusSource.User,
                Developer => ConsentStatusSource.Developer,
                _ => ConsentStatusSource.Developer
            };
        }

        public static NetworkConnectionType GetNetworkConnectionType(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return NetworkConnectionType.Unknown;
            
            return source.ToLower() switch
            {
                Unknown => NetworkConnectionType.Unknown,
                Wired => NetworkConnectionType.Wired,
                Wifi => NetworkConnectionType.Wifi,
                CellularUnknown => NetworkConnectionType.CellularUnknown,
                Cellular_Unknown => NetworkConnectionType.CellularUnknown,
                Cellular2G => NetworkConnectionType.Cellular2G,
                Cellular_2G => NetworkConnectionType.Cellular2G,
                Cellular3G => NetworkConnectionType.Cellular3G,
                Cellular_3G => NetworkConnectionType.Cellular3G,
                Cellular4G => NetworkConnectionType.Cellular4G,
                Cellular_4G => NetworkConnectionType.Cellular4G,
                Cellular5G => NetworkConnectionType.Cellular5G,
                Cellular_5G => NetworkConnectionType.Cellular5G,
                _ => NetworkConnectionType.Unknown
            };
        }

        public static VendorIdentifierScope GetVendorIdentifierScope(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return VendorIdentifierScope.Unknown;

            return source.ToLower() switch
            {
                Unknown => VendorIdentifierScope.Unknown,
                Application => VendorIdentifierScope.Application,
                Developer => VendorIdentifierScope.Developer,
                _ => VendorIdentifierScope.Unknown
            };
        }
    }
}
