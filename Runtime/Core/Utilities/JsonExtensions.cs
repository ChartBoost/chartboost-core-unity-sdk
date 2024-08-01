using System.Collections.Generic;
using Chartboost.Core.Consent;
using Chartboost.Core.Error;
using Chartboost.Json;

namespace Chartboost.Core.Utilities
{
    /// <summary>
    /// <see cref="JsonTools"/> extensions for Chartboost Mediation SDK operations with JSON objects.
    /// </summary>
    internal static class JsonExtensions 
    {
        public static Dictionary<ConsentKey, ConsentValue> ToConsentDictionary(this string consentsJson)
            => JsonTools.DeserializeObject<Dictionary<ConsentKey, ConsentValue>>(consentsJson);
        
        public static ConsentKey[] ToConsentKeys(this string consentKeysJson)
            => JsonTools.DeserializeObject<ConsentKey[]>(consentKeysJson);
        
        public static ChartboostCoreError ToChartboostCoreError(this string coreErrorJson)
            => JsonTools.DeserializeObject<ChartboostCoreError>(coreErrorJson);

        public static Dictionary<string, object> ToCredentials(this string credentialsJson)
        {
            if (string.IsNullOrEmpty(credentialsJson) || string.IsNullOrWhiteSpace(credentialsJson))
                return new Dictionary<string, object>();
            return JsonTools.DeserializeObject<Dictionary<string, object>>(credentialsJson);
        }
    }
}
