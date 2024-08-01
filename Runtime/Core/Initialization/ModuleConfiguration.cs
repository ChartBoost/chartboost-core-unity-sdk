using System;
using Newtonsoft.Json;

namespace Chartboost.Core.Initialization
{
    [Serializable]
    public struct ModuleConfiguration
    {
        [JsonProperty("chartboostApplicationIdentifier")]
        public readonly string ChartboostApplicationIdentifier;

        internal ModuleConfiguration(SDKConfiguration configuration)
        {
            ChartboostApplicationIdentifier = configuration.ChartboostApplicationIdentifier;
        }

        internal ModuleConfiguration(string chartboostApplicationIdentifier)
        {
            ChartboostApplicationIdentifier = chartboostApplicationIdentifier;
        }
    }
}
