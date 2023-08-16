using System;
using Chartboost.Core.Error;
using Newtonsoft.Json;

namespace Chartboost.Core.Initialization
{
    #nullable enable
    [Serializable]
    public class ModuleInitializationResult : ChartboostCoreResult
    {
        [JsonProperty("moduleName")]
        private readonly string _moduleName;
        [JsonProperty("moduleVersion")]
        private readonly string _moduleVersion;
        
        [NonSerialized]
        public readonly InitializableModule Module;
        
        public ModuleInitializationResult(long start, long end, long duration, ChartboostCoreError? error, InitializableModule module) : base(start, end, duration, error)
        {
            Module = module;
            _moduleName = module.ModuleId;
            _moduleVersion = module.ModuleVersion;
        }
    }
    #nullable disable
}
