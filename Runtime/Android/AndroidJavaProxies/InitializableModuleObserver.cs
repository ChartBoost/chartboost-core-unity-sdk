using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Utilities;
using UnityEngine;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    internal class InitializableModuleObserver : AndroidJavaProxy
    {
        private readonly ChartboostCoreModuleInitializationDelegate _callback;
        
        public InitializableModuleObserver(ChartboostCoreModuleInitializationDelegate callback) : base(AndroidConstants.InitializableModuleObserver)
        {
            _callback = callback;
        }

        private void onModuleInitializationCompleted(AndroidJavaObject result)
        {
            MainThreadDispatcher.Post(o =>
            {
                var moduleInitResult = result.ToUnityModuleInitializationResult();
                _callback?.Invoke(moduleInitResult);
            });
        }
    }
}
