using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Json;
using Chartboost.Logging;

namespace Chartboost.Core.Modules
{
    public class TestUnityModuleSuccess : Module
    {
        public override string ModuleId => "Unity Test Module Success";
        public override string ModuleVersion => "0.0.1";
        
        protected override Task<ChartboostCoreError?> Initialize(ModuleConfiguration configuration)
        {
            LogController.Log($"[{ModuleId}/{ModuleVersion}] Attempting Initialization. Always succeed, and Config: {JsonTools.SerializeObject(configuration)}", LogLevel.Info);
            return Task.FromResult<ChartboostCoreError?>(null);
        }
    }
}
