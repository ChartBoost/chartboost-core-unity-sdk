using System;
using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Newtonsoft.Json;

namespace Chartboost.Core.Modules
{
    public class TestUnityModuleSuccess : InitializableModule
    {
        public override string ModuleId => "Unity Test Module Success";
        public override string ModuleVersion => "0.0.1";

        protected override Task<ChartboostCoreError?> Initialize(ModuleInitializationConfiguration configuration)
        {
            ChartboostCoreLogger.Log($"[{ModuleId}/{ModuleVersion}] Attempting Initialization. Always succeed, and Config {JsonConvert.SerializeObject(configuration)}");
            return Task.FromResult<ChartboostCoreError?>(null);
        }
    }
}
