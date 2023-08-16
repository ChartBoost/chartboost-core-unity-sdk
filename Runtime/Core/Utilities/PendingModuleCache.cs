using System.Collections.Generic;
using Chartboost.Core.Initialization;

namespace Chartboost.Core.Utilities
{
    internal static class PendingModuleCache
    {
        private static readonly Dictionary<string, InitializableModule> ModulesPendingToInitialize = new Dictionary<string, InitializableModule>();

        internal static void TrackInitializableModule(InitializableModule module)
        {
            var moduleName = module.ModuleId;
            
            // TODO-  Handle duplicate module scenario
            if (ModulesPendingToInitialize.ContainsKey(moduleName))
                return;
            
            ModulesPendingToInitialize.Add(moduleName, module);
        }

        #nullable enable
        internal static InitializableModule? GetInitializableModule(string moduleName)
        {
            // TODO - Handle scenario where requested module cannot be found 
            return ModulesPendingToInitialize.TryGetValue(moduleName, out var module) ? module : null;
        }

        internal static InitializableModule? ReleaseInitializableModule(string moduleName)
        {
            var module = ModulesPendingToInitialize[moduleName];
            ModulesPendingToInitialize[moduleName] = null;
            return module;
        }
        #nullable disable
        
    }
}
