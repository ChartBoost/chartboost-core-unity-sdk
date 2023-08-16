using System;
using System.Threading.Tasks;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Error;
using Chartboost.Core.Utilities;
using UnityEngine;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    internal class ModuleInitializerConsumer : AndroidJavaProxy
    {
        private readonly Func<Task<ChartboostCoreError?>> _initCallback;
        public ModuleInitializerConsumer(Func<Task<ChartboostCoreError?>> initCallback) : base(AndroidConstants.ModuleInitializerConsumer) 
            => _initCallback = initCallback;

        private void initialize(AndroidJavaObject completion)
        {
            Task.Run(async () =>
            {
                var result = await await MainThreadDispatcher.MainThreadTask(_initCallback);
                AndroidJNI.AttachCurrentThread();
                completion.Call(AndroidConstants.FunctionCompleted, result?.ToNativeChartboostError());
                AndroidJNI.DetachCurrentThread();
            });
        }
    }
}
