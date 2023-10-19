using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.iOS.Utilities;

namespace Chartboost.Core.iOS.Modules
{
    /// <summary>
    /// <see cref="InitializableModule"/> implementation for iOS Native Modules.
    /// </summary>
    public abstract class NativeModule : InitializableModule
    {
        private readonly IntPtr _nativeInstance;

        /// <summary>
        /// Create a Unity representation of <see cref="InitializableModule"/>.
        /// </summary>
        /// <param name="instance">Native <see cref="InitializableModule"/> reference.</param>
        public NativeModule(IntPtr instance)
        {
            _nativeInstance = instance;
        }

        /// <inheritdoc cref="InitializableModule.ModuleId"/>
        public override string ModuleId => _chartboostCoreGetNativeModuleId(_nativeInstance);
       
        /// <inheritdoc cref="InitializableModule.ModuleVersion"/>
        public override string ModuleVersion => _chartboostCoreGetNativeModuleVersion(_nativeInstance);

        /// <summary>
        /// Native modules do not initialized by Unity C#, they are handled natively.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Thrown when attempting to initialize in Unity C#</exception>
        protected override Task<ChartboostCoreError?> Initialize(ModuleInitializationConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds <see cref="IntPtr"/> native module reference to our native modules cache.
        /// </summary>
        internal override void AddNativeInstance()
        {
            _chartboostCoreAddNativeModule(_nativeInstance);
        }

        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreAddNativeModule(IntPtr uniqueId);
        [DllImport(IOSConstants.DLLImport)] private static extern string _chartboostCoreGetNativeModuleId(IntPtr uniqueId);
        [DllImport(IOSConstants.DLLImport)] private static extern string _chartboostCoreGetNativeModuleVersion(IntPtr uniqueId);
    }
}
