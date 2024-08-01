using System;
using System.Collections.Generic;
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

        /// <summary>
        /// The list of publisher-specified modules to initialize.
        /// The modules will be initialized simultaneously in the order specified.
        /// Only the first ConsentAdapter is initialized. If other ConsentAdapters are attempted to initialize, they will fail.
        /// </summary>
        public readonly IReadOnlyList<Module> Modules;

        /// <summary>
        /// Use this to skip modules.
        /// This will skip initialization for both the modules passed in to the modules list and server-side modules with the same module identifier.
        /// </summary>
        public readonly IReadOnlyCollection<string> SkippedModuleIdentifiers;

        public SDKConfiguration(string chartboostApplicationIdentifier, List<Module> modules, HashSet<string> skippedModuleIdentifiers = null)
        {
            ChartboostApplicationIdentifier = chartboostApplicationIdentifier;
            Modules = modules ?? new List<Module>();
            SkippedModuleIdentifiers = skippedModuleIdentifiers ?? new HashSet<string>();
        }
    }
}
