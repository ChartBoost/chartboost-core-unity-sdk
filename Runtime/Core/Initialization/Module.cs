using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Logging;

namespace Chartboost.Core.Initialization
{
    /// <summary>
    /// The class to which all Chartboost Core modules must conform to.
    /// </summary>
    public abstract class Module
    {
        /// <summary>
        /// Invoked when this Module instanced has successfully initialized.
        /// </summary>
        public event Action<Module> ModuleReady;
        
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
        /// <param name="configuration"></param>
        /// <returns> An error should be passed if the initialization failed, whereas null should be passed if it succeeded.</returns>
        protected abstract Task<ChartboostCoreError?> Initialize(ModuleConfiguration configuration);

        /// <summary>
        /// Updates the <see cref="Module"/> with JSON data from the server. A publisher is recommended to
        /// initialize via the constructor with module-specific parameters rather than using this function.
        /// When creating a module, please make sure it's possible to send a <see cref="IReadOnlyDictionary{TKey,TValue}"/> credentials
        /// object to set up the parameters of this module.
        /// <br/>
        /// <br/>
        /// Note: Modules should not perform costly operations on this initializer.
        /// <see cref="ChartboostCore"/> SDK may instantiate and discard several instances of the same module.
        /// <see cref="ChartboostCore"/> SDK keeps strong references to modules that are successfully initialized.
        /// </summary>
        /// <param name="credentials">A <see cref="IReadOnlyDictionary{TKey,TValue}"/> containing all the information required to initialize
        /// this module, as defined on the Chartboost Core's dashboard.</param>
        protected virtual void UpdateCredentials(IReadOnlyDictionary<string, object> credentials) 
            => LogController.Log($"Updating Credentials for Module: {ModuleId} with Credentials", LogLevel.Debug);

        /// <summary>
        /// Event invocation for <see cref="Module.Initialize"/>, called internally.
        /// </summary>
        internal async Task<ChartboostCoreError?> OnInitialize(object configuration) => await Initialize((ModuleConfiguration)configuration);
        
        /// <summary>
        /// Event invocation for <see cref="Module.UpdateCredentials"/>
        /// </summary>
        /// <param name="credentials">A <see cref="IReadOnlyDictionary{TKey,TValue}"/> containing all the information required to initialize
        /// this module, as defined on the Chartboost Core's dashboard.</param>
        internal void OnUpdateCredentials(IReadOnlyDictionary<string, object> credentials) 
            => MainThreadDispatcher.Post(_ => UpdateCredentials(credentials));

        /// <summary>
        /// Event invocation for <see cref="Module.ModuleReady"/>, called internally.
        /// </summary>
        internal void OnModuleReady() 
            => MainThreadDispatcher.Post(_ => ModuleReady?.Invoke(this));

        /// <summary>
        /// Specifies if module is integrated natively or in Unity. Module instancing and handling is done based off this flag.
        /// </summary>
        internal virtual bool NativeModule => false;

        /// <summary>
        /// Utilized to add the native module instance to the corresponding native module cache.
        /// </summary>
        internal virtual void AddNativeInstance() { }
    }

    public static class ModuleExtensions
    {
        public static string GetModuleAssemblyName<T>() where T: Module => typeof(T).AssemblyQualifiedName;
        public static string GetModuleAssemblyName<T>(this T module) where T: Module => typeof(T).AssemblyQualifiedName;
    }
}
