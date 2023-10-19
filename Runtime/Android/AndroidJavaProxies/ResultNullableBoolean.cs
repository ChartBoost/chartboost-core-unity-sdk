using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    /// <summary>
    /// <see cref="AwaitableAndroidJavaProxy{TResult}"/> for Nullable booleans.
    /// </summary>
    #nullable enable
    internal class ResultNullableBoolean : AwaitableAndroidJavaProxy<bool?>
    {
        public ResultNullableBoolean() : base(AndroidConstants.ResultBoolean) { }

        /// <summary>
        /// Posts a boolean result from the native layer.
        /// </summary>
        /// <param name="result">Boolean value.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onResult(bool result) => MainThreadDispatcher.Post(o => _complete(result));

        /// <summary>
        /// Posts a null result from the native layer.
        /// </summary>
        /// <param name="result">Null value.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onResult(AndroidJavaObject result) => MainThreadDispatcher.Post(o => _complete(null));
    }

    /// <summary>
    /// <see cref="AwaitableAndroidJavaProxy{TResult}"/> for booleans.
    /// </summary>
    internal class ResultBoolean : AwaitableAndroidJavaProxy<bool>
    {
        public ResultBoolean() : base(AndroidConstants.ResultBoolean) { }
        
        /// <summary>
        /// Posts a boolean result from the native layer.
        /// </summary>
        /// <param name="result">Boolean value</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onResult(bool result) => MainThreadDispatcher.Post(o => _complete(result));
    }
#nullable disable
}
