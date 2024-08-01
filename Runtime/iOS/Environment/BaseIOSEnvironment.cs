using System;
using System.Threading.Tasks;
using AOT;
using Chartboost.Core.Utilities;
using UnityEngine.Scripting;

namespace Chartboost.Core.iOS.Environment
{
    /// <summary>
    /// Base iOS class for all Chartboost Core environments. 
    /// </summary>
    internal class BaseIOSEnvironment
    {
        /// <summary>
        /// Gets an awaitable nullable <see cref="string"/> from a native environment bridge.
        /// </summary>
        /// <param name="trigger">Trigger function for awaitable <see cref="string"/>.</param>
        /// <returns><see cref="string"/> value.</returns>
        [Preserve]
        private protected static async Task<string> AwaitableString(Action<int, ExternChartboostCoreOnResultString> trigger)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<string>();
            trigger(hashCode, OnAwaitableStringCompletion);
            return await proxy;
        }
        
        /// <summary>
        /// Gets an awaitable nullable <see cref="bool"/> from a native environment bridge.
        /// </summary>
        /// <param name="trigger">Trigger function for awaitable <see cref="string"/>.</param>
        /// <returns><see cref="bool"/> value.</returns>
        [Preserve]
        private protected static async Task<string> AwaitableBoolean(Action<int, ExternChartboostCoreOnResultBoolean> trigger)
        {
            var (proxy, hashCode) = AwaitableProxies.SetupProxy<string>();
            trigger(hashCode, OnAwaitableBooleanCompletion);
            return await proxy;
        }
        
        [MonoPInvokeCallback(typeof(ExternChartboostCoreOnResultString))]
        protected static void OnAwaitableStringCompletion(int hashCode, string result)
            => MainThreadDispatcher.Post(o => AwaitableProxies.ResolveCallbackProxy(hashCode, result));

        [MonoPInvokeCallback(typeof(ExternChartboostCoreOnResultBoolean))]
        protected static void OnAwaitableBooleanCompletion(int hashCode, bool completion)
            => MainThreadDispatcher.Post(o => AwaitableProxies.ResolveCallbackProxy(hashCode, completion));
    }
}
