using System;
using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Core.Utilities;

namespace Chartboost.Core.Initialization
{
    /// <summary>
    /// The class to which all Chartboost Core modules must conform to.
    /// </summary>
    public abstract class InitializableModule
    {
        /// <summary>
        /// Invoked when this Module instanced has successfully initialized.
        /// </summary>
        public event Action ModuleReady;
        
        /// <summary>
        /// The module identifier.
        /// </summary>
        public abstract string ModuleId { get; }
        
        /// <summary>
        /// The version of the module.
        /// </summary>
        public abstract string ModuleVersion { get; }

        /// <summary>
        /// The designated initializer for the module. Sets up the module to make it ready to be used.
        /// </summary>
        /// <returns> An error should be passed if the initialization failed, whereas null should be passed if it succeeded.</returns>
        protected abstract Task<ChartboostCoreError?> Initialize();

        /// <summary>
        /// Event invocation for <see cref="InitializableModule.Initialize"/>, called internally.
        /// </summary>
        internal async Task<ChartboostCoreError?> OnInitialize() => await Initialize();

        /// <summary>
        /// Event invocation for <see cref="InitializableModule.ModuleReady"/>, called internally.
        /// </summary>
        internal void OnModuleReady() { MainThreadDispatcher.Post(o => ModuleReady?.Invoke()); }

        /// <summary>
        /// Specifies if module is integrated natively or in Unity. Module instancing and handling is done based off this flag.
        /// </summary>
        internal virtual bool NativeModule { get; } = false;

        /// <summary>
        /// Utilized to add the native module instance to the corresponding native module cache.
        /// </summary>
        internal virtual void AddNativeInstance() { }
    }
}
