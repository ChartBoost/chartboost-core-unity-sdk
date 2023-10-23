using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Error;

namespace Chartboost.Core.Initialization
{
    /// <summary>
    /// Generic representation of a native <see cref="InitializableModule"/>, used to create the C# base class of a native module.
    /// </summary>
    /// <typeparam name="T">Module type.</typeparam>
    public abstract class NativeInitializableModule<T> : InitializableModule where T : class
    {
        // ReSharper disable once StaticMemberInGenericType
        public static Type InstanceType { get; set; }
        private readonly InitializableModule _instance;
        private readonly Dictionary<string, object> _jsonConfig;

        public NativeInitializableModule(params object[] parameters)
        {
            if (InstanceType == null)
            {
                ChartboostCoreLogger.Log($"could not create an instance of NativeModule.");
                return;
            }

            var instance = (InitializableModule)Activator.CreateInstance(InstanceType, parameters);
            if (instance == null)
            {
                ChartboostCoreLogger.LogError($"could not create an instance of {InstanceType.Name}");
                return;
            }
            ChartboostCoreLogger.Log($"Created instance of {InstanceType.Name}");
            _instance = instance;
        }

        public NativeInitializableModule(Dictionary<string, object> config)
        {
            var instance = (InitializableModule)Activator.CreateInstance(InstanceType, config);
            if (instance == null)
            {
                ChartboostCoreLogger.LogError($"could not create an instance of {InstanceType.Name}");
                return;
            }
            ChartboostCoreLogger.Log($"Created instance of {InstanceType.Name}");
            _jsonConfig = config;
        }

        protected abstract string DefaultModuleId { get; }
        protected abstract string DefaultModuleVersion { get; }

        public override string ModuleId 
            => _instance?.ModuleId ?? DefaultModuleId;
        public override string ModuleVersion 
            => _instance?.ModuleVersion ?? DefaultModuleVersion;

        protected override Task<ChartboostCoreError?> Initialize(ModuleInitializationConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        internal override bool NativeModule => true;

        internal override void AddNativeInstance() => _instance?.AddNativeInstance();
    }
}
