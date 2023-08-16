using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.iOS.Utilities;

namespace Chartboost.Core.iOS.Modules
{
    public abstract class NativeModule : InitializableModule
    {
        private IntPtr _nativeInstance;

        public NativeModule(IntPtr instance)
        {
            _nativeInstance = instance;
        }

        public override string ModuleId => _chartboostCoreGetNativeModuleId(_nativeInstance);
        public override string ModuleVersion => _chartboostCoreGetNativeModuleVersion(_nativeInstance);

        protected override Task<ChartboostCoreError?> Initialize()
        {
            throw new NotImplementedException();
        }

        internal override void AddNativeInstance()
        {
            _chartboostCoreAddNativeModule(_nativeInstance);
        }

        [DllImport(IOSConstants.DLLImport)] private static extern void _chartboostCoreAddNativeModule(IntPtr uniqueId);
        [DllImport(IOSConstants.DLLImport)] private static extern string _chartboostCoreGetNativeModuleId(IntPtr uniqueId);
        [DllImport(IOSConstants.DLLImport)] private static extern string _chartboostCoreGetNativeModuleVersion(IntPtr uniqueId);
    }
}
