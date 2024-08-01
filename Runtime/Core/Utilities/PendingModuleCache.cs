using System.Collections.Generic;
using Chartboost.Core.Initialization;

namespace Chartboost.Core.Utilities
{
    /// <summary>
    /// Utility class to keep track of <see cref="Module"/> instances as they are initialized and sent back to developers.
    /// </summary>
    internal static class PendingModuleCache
    {
        private static readonly Dictionary<string, Module> ModulesPendingToInitialize = new();

        /// <summary>
        /// Tracks a <see cref="Module"/> while it is being initialized. 
        /// </summary>
        /// <param name="module">The <see cref="Module"/> to be tracked.</param>
        internal static void TrackModule(Module module)
        {
            var moduleName = module.ModuleId;
            
            // TODO-  Handle duplicate module scenario
            if (ModulesPendingToInitialize.ContainsKey(moduleName))
                return;
            
            ModulesPendingToInitialize.Add(moduleName, module);
        }
        
        #nullable enable
        /// <summary>
        /// Returns a <see cref="Module"/> based on a moduleId or null if not found.
        /// </summary>
        /// <param name="moduleId">String identifier for the module.</param>
        /// <returns>The <see cref="Module"/> instance or null.</returns>
        internal static Module? GetModule(string moduleId)
        {
            // TODO - Handle scenario where requested module cannot be found 
            if (ModulesPendingToInitialize.TryGetValue(moduleId, out var module))
            {
                return module;
            }
            return null;
        }

        /// <summary>
        /// Releases a <see cref="Module"/> based on a moduleId.
        /// </summary>
        /// <param name="moduleId">String identifier for the module.</param>
        /// <returns>The <see cref="Module"/> instance or null.</returns>
        internal static void ReleaseModule(string moduleId) 
            => ModulesPendingToInitialize[moduleId] = null;
    }
}
