using System;
using Newtonsoft.Json;

namespace Chartboost.Core.Initialization
{
    [Serializable]
    public struct ModuleInitializationConfiguration
    {
        [JsonProperty("chartboostApplicationIdentifier")]
        public readonly string ChartboostApplicationIdentifier;

        internal ModuleInitializationConfiguration(SDKConfiguration configuration)
        {
            ChartboostApplicationIdentifier = configuration.ChartboostApplicationIdentifier;
        }

        internal ModuleInitializationConfiguration(string chartboostApplicationIdentifier)
        {
            ChartboostApplicationIdentifier = chartboostApplicationIdentifier;
        }
    }
}
