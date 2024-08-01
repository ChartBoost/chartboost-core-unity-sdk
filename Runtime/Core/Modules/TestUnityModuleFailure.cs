using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Json;
using Chartboost.Logging;

namespace Chartboost.Core.Modules
{
    public class TestUnityModuleFailure : Module
    {
        public override string ModuleId => "Unity Test Module Failure";
        public override string ModuleVersion => "0.0.1";
        
        public static readonly ChartboostCoreError Error = new ChartboostCoreError(500, "Module failed to initialize", "This module always fails to initialize", "nothing...");

        protected override Task<ChartboostCoreError?> Initialize(ModuleConfiguration configuration)
        {
            LogController.Log($"[{ModuleId}/{ModuleVersion}] Attempting Initialization. Always fail and Config: {JsonTools.SerializeObject(configuration)}", LogLevel.Info);
            return Task.FromResult<ChartboostCoreError?>(Error);
        }
    }
}
