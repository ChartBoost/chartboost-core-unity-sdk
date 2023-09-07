using System;
using Chartboost.Core.Error;
using Newtonsoft.Json;

namespace Chartboost.Core.Initialization
{
    #nullable enable
    /// <summary>
    /// A result object with the information regarding a module initialization operation.
    /// </summary>
    [Serializable]
    public class ModuleInitializationResult : ChartboostCoreResult
    {
        [JsonProperty("moduleName")]
        private readonly string _moduleName;
        [JsonProperty("moduleVersion")]
        private readonly string _moduleVersion;
        
        /// <summary>
        /// The module that was initialized. Note that the initialization operation may have failed.
        /// Use the <see cref="ChartboostCoreResult.Error"/> property to determine this.
        /// </summary>
        [NonSerialized]
        public readonly InitializableModule Module;

        public ModuleInitializationResult(DateTime start, DateTime end, ChartboostCoreError? error, InitializableModule module) : base(start, end, error)
        {
            Module = module;
            _moduleName = module.ModuleId;
            _moduleVersion = module.ModuleVersion;
        }

        public ModuleInitializationResult(long start, long end, long duration, ChartboostCoreError? error, InitializableModule module) : base(start, end, duration, error)
        {
            Module = module;
            _moduleName = module.ModuleId;
            _moduleVersion = module.ModuleVersion;
        }
    }
    #nullable disable
}
