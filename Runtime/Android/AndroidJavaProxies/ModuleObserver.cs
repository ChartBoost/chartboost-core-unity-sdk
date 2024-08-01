using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    /// <summary>
    /// An observer to be notified whenever a Core module finishes initialization.
    /// </summary>
    internal class ModuleObserver : AndroidJavaProxy
    {
        private readonly ChartboostCoreModuleInitializationDelegate _callback;
        
        public ModuleObserver(ChartboostCoreModuleInitializationDelegate callback) : base(AndroidConstants.ModuleObserver)
        {
            _callback = callback;
        }

        /// <inheritdoc cref="Core.ChartboostCore.ModuleInitializationCompleted"/>
        /// <param name="result">Native <see cref="ModuleInitializationResult"/> object.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
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
