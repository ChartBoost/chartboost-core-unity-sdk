using System.Threading.Tasks;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.Modules
{
    [Preserve]
    public abstract class NativeModule : InitializableModule
    {
        private readonly AndroidJavaObject _nativeInstance;

        public NativeModule(AndroidJavaObject instance) => _nativeInstance = instance;

        public override string ModuleId => _nativeInstance.Call<string>(AndroidConstants.GetModuleId);

        public override string ModuleVersion => _nativeInstance.Call<string>(AndroidConstants.GetModuleVersion);

        protected override Task<ChartboostCoreError?> Initialize()
        {
            throw new System.NotImplementedException();
        }

        internal override void AddNativeInstance()
        {
            using var bridge = AndroidUtils.GetAndroidBridge();
            bridge.CallStatic(AndroidConstants.FunctionAddModule, _nativeInstance);
        }
    }
}
