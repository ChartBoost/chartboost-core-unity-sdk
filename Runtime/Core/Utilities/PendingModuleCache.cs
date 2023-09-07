using System.Collections.Generic;
using Chartboost.Core.Initialization;

namespace Chartboost.Core.Utilities
{
    /// <summary>
    /// Utility class to keep track of <see cref="InitializableModule"/> instances as they are initialized and sent back to developers.
    /// </summary>
    internal static class PendingModuleCache
    {
        private static readonly Dictionary<string, InitializableModule> ModulesPendingToInitialize = new Dictionary<string, InitializableModule>();

        /// <summary>
        /// Tracks a <see cref="InitializableModule"/> while it is being initialized. 
        /// </summary>
        /// <param name="module">The <see cref="InitializableModule"/> to be tracked.</param>
        internal static void TrackInitializableModule(InitializableModule module)
        {
            var moduleName = module.ModuleId;
            
            // TODO-  Handle duplicate module scenario
            if (ModulesPendingToInitialize.ContainsKey(moduleName))
                return;
            
            ModulesPendingToInitialize.Add(moduleName, module);
        }
        
        #nullable enable
        /// <summary>
        /// Returns a <see cref="InitializableModule"/> based on a moduleId or null if not found.
        /// </summary>
        /// <param name="moduleId">String identifier for the module.</param>
        /// <returns>The <see cref="InitializableModule"/> instance or null.</returns>
        internal static InitializableModule? GetInitializableModule(string moduleId)
        {
            // TODO - Handle scenario where requested module cannot be found 
            return ModulesPendingToInitialize.TryGetValue(moduleId, out var module) ? module : null;
        }

        /// <summary>
        /// Releases a <see cref="InitializableModule"/> based on a moduleId.
        /// </summary>
        /// <param name="moduleId">String identifier for the module.</param>
        /// <returns>The <see cref="InitializableModule"/> instance or null.</returns>
        internal static void ReleaseInitializableModule(string moduleId) 
            => ModulesPendingToInitialize[moduleId] = null;
        #nullable disable
    }
}
