using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Utilities;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    /// <summary>
    /// <see cref="AwaitableAndroidJavaProxy{TResult}"/> for <see cref="string"/>.
    /// </summary>
    #nullable enable
    internal class ResultString : AwaitableAndroidJavaProxy<string?>
    {
        public ResultString() : base(AndroidConstants.ResultString) { }

        /// <summary>
        /// Posts a string result from the native layer.
        /// </summary>
        /// <param name="result">String value.</param>
        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onResult(string result)
        {
            MainThreadDispatcher.Post(o => _complete(result));
        }
    }
    #nullable disable
}
