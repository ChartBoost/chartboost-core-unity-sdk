using System.Threading.Tasks;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.Modules
{
    /// <summary>
    /// <see cref="InitializableModule"/> implementation for Android Native Modules.
    /// </summary>
    [Preserve]
    public abstract class NativeModule : InitializableModule
    {
        private readonly AndroidJavaObject _nativeInstance;

        /// <summary>
        /// Create a Unity representation of <see cref="InitializableModule"/>.
        /// </summary>
        /// <param name="instance">Native <see cref="InitializableModule"/> reference.</param>
        public NativeModule(AndroidJavaObject instance) => _nativeInstance = instance;

        /// <inheritdoc cref="InitializableModule.ModuleId"/>
        public override string ModuleId => _nativeInstance.Call<string>(AndroidConstants.GetModuleId);

        /// <inheritdoc cref="InitializableModule.ModuleVersion"/>
        public override string ModuleVersion => _nativeInstance.Call<string>(AndroidConstants.GetModuleVersion);

        /// <summary>
        /// Native modules do not initialized by Unity C#, they are handled natively.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Thrown when attempting to initialize in Unity C#</exception>
        protected override Task<ChartboostCoreError?> Initialize()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Adds <see cref="AndroidJavaObject"/> native module reference to our native modules cache.
        /// </summary>
        internal override void AddNativeInstance()
        {
            using var bridge = AndroidUtils.AndroidBridge();
            bridge.CallStatic(AndroidConstants.FunctionAddModule, _nativeInstance);
        }
    }
}
