using System;
using System.Threading.Tasks;
using Chartboost.Core.Error;
using Chartboost.Logging;

namespace Chartboost.Core.Initialization
{
    /// <summary>
    /// Generic representation of a native <see cref="Module"/>, used to create the C# base class of a native module.
    /// </summary>
    /// <typeparam name="T">Module type.</typeparam>
    public abstract class NativeModuleWrapper<T> : Module where T : class
    {
        // ReSharper disable once StaticMemberInGenericType
        public static Type InstanceType { get; set; }
        protected readonly Module Instance;

        private const string LogNativeModuleInstanceTypeNull = "Unable to create an instance of NativeModule, InstanceType is null.";
        private static string LogNativeModuleFailed => $"Unable to create an instance of {InstanceType.Name}.";
        private static string LogNativeModuleCreated => $"Created instance of {InstanceType.Name}";

        protected NativeModuleWrapper(params object[] parameters)
        {
            if (InstanceType == null)
            {
                LogController.Log(LogNativeModuleInstanceTypeNull, LogLevel.Warning);
                return;
            }

            var instance = (Module)Activator.CreateInstance(InstanceType, parameters);
            if (instance == null)
            {
                LogController.Log(LogNativeModuleFailed, LogLevel.Error);
                return;
            }
            
            LogController.Log(LogNativeModuleCreated, LogLevel.Verbose);
            Instance = instance;
        }

        protected abstract string DefaultModuleId { get; }
        protected abstract string DefaultModuleVersion { get; }

        public override string ModuleId
        {
            get
            {
                if (Instance != null && !string.IsNullOrEmpty(Instance?.ModuleId))
                    return Instance?.ModuleId;
                
                return DefaultModuleId;
            }
        }

        public override string ModuleVersion
        {
            get
            {
                if (Instance != null && !string.IsNullOrEmpty(Instance?.ModuleVersion))
                    return Instance?.ModuleVersion;
                return DefaultModuleVersion;
            }
        }

        protected override Task<ChartboostCoreError?> Initialize(ModuleConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        internal override bool NativeModule => true;

        internal override void AddNativeInstance() => Instance?.AddNativeInstance();
    }
}
