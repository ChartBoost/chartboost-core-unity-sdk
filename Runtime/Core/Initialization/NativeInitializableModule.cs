using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Error;

namespace Chartboost.Core.Initialization
{
    public class NativeInitializableModule<T> : InitializableModule where T : class
    {
        public static Type InstanceType;
        private readonly InitializableModule _instance;
        private readonly Dictionary<string, object> _jsonConfig;

        public NativeInitializableModule(params object[] parameters)
        {
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

        public override string ModuleId 
            => _instance?.ModuleId;
        public override string ModuleVersion 
            => _instance?.ModuleVersion;

        protected override Task<ChartboostCoreError?> Initialize()
        {
            throw new NotImplementedException();
        }

        internal override bool NativeModule => true;

        internal override void AddNativeInstance() => _instance?.AddNativeInstance();
    }
}
