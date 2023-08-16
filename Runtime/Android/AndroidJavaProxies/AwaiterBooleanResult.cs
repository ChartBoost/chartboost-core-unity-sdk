using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Utilities;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
    internal class AwaiterBooleanResult : AwaitableAndroidJavaProxy<bool>
    {
        public AwaiterBooleanResult() : base(AndroidConstants.AwaiterBooleanResult) { }

        private void onResult(bool result)
        {
            MainThreadDispatcher.Post(o => _complete(result));
        }
    }
}
