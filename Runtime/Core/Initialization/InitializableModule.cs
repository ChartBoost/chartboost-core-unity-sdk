using System.Threading.Tasks;
using Chartboost.Core.Error;

namespace Chartboost.Core.Initialization
{
    public abstract class InitializableModule
    {
        public abstract string ModuleId { get; }
        public abstract string ModuleVersion { get; }

        protected abstract Task<ChartboostCoreError?> Initialize();

        internal async Task<ChartboostCoreError?> OnInitialize() => await Initialize();
        
        internal virtual bool NativeModule { get; } = false;

        internal virtual void AddNativeInstance() { }
    }
}
