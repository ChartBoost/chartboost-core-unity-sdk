using System;
using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;

namespace Chartboost.Core.iOS.Modules
{
    /// <summary>
    /// <see cref="Module"/> implementation for iOS Native Modules.
    /// </summary>
    public abstract class NativeModule : Module
    {
        protected readonly IntPtr NativeInstance;

        /// <summary>
        /// Create a Unity representation of <see cref="Module"/>.
        /// </summary>
        /// <param name="instance">Native <see cref="Module"/> reference.</param>
        public NativeModule(IntPtr instance)
        {
            NativeInstance = instance;
        }

        /// <inheritdoc cref="Module.ModuleId"/>
        public override string ModuleId => ModuleWrapper.GetModuleId(NativeInstance);
       
        /// <inheritdoc cref="Module.ModuleVersion"/>
        public override string ModuleVersion => ModuleWrapper.GetModuleVersion(NativeInstance);

        /// <summary>
        /// Native modules do not initialized by Unity C#, they are handled natively.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Thrown when attempting to initialize in Unity C#</exception>
        protected override Task<ChartboostCoreError?> Initialize(ModuleConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds <see cref="IntPtr"/> native module reference to our native modules cache.
        /// </summary>
        internal override void AddNativeInstance()
        {
            ModuleWrapper.AddModule(NativeInstance);
        }
    }
}
