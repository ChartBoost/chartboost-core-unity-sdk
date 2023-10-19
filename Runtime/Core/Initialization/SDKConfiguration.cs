using System;
using Newtonsoft.Json;

namespace Chartboost.Core.Initialization
{
    /// <summary>
    /// Configuration with parameters needed by the Chartboost Core SDK on initialization.
    /// </summary>
    [Serializable]
    public struct SDKConfiguration
    {
        /// <summary>
        /// The Chartboost App ID which should match the value on the Chartboost dashboard.
        /// </summary>
        [JsonProperty("chartboostApplicationIdentifier")]
        public readonly string ChartboostApplicationIdentifier;

        public SDKConfiguration(string chartboostApplicationIdentifier)
        {
            ChartboostApplicationIdentifier = chartboostApplicationIdentifier;
        }
    }
}
