using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Json;
using Chartboost.Logging;
using UnityEngine.Scripting;

namespace Chartboost.Core.Modules
{
    [Preserve]
    public class RemoteOnlyModule : Module
    {
        public override string ModuleId => "unity_remote_module";
        public override string ModuleVersion => "1.0.0";

        private bool _updatedCredentials;
        protected override async Task<ChartboostCoreError?> Initialize(ModuleConfiguration configuration)
        {
            if (!_updatedCredentials)
            {
                var message = $"Module: {nameof(RemoteOnlyModule)} failed to receive updated credentials, failing initialization.";
                LogController.Log(message, LogLevel.Error);
                var error = new ChartboostCoreError(-1, message);
                return await Task.FromResult<ChartboostCoreError?>(error);
            }
            
            // Perform init operation
            await Task.Delay(5000);
            
            // success
            return null;
        }

        protected override void UpdateCredentials(IReadOnlyDictionary<string, object> credentials)
        {
            base.UpdateCredentials(credentials);
            
            if (credentials.TryGetValue("config_key", out var key)){
                
            }

            LogController.Log($"Module: {nameof(RemoteOnlyModule)} updated credentials: {JsonTools.SerializeObject(credentials)}", LogLevel.Info);
            _updatedCredentials = true;
        }
    }
}
