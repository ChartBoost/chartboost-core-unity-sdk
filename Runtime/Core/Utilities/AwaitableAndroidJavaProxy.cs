using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

namespace Chartboost.Core.Utilities
{
    /// <summary>
    /// <para>This class can be used to implement any java interface.Any java vm method invocation matching the interface on the proxy object will automatically be passed to the c# implementation.</para>
    /// Utilized to resolve awaitable APIs from Java/Kotlin integrations.
    /// </summary>
    /// <typeparam name="TResult">Result type</typeparam>
    public class AwaitableAndroidJavaProxy<TResult> : AndroidJavaProxy
    {
        /// <summary>
        /// Returns the <see cref="TaskAwaiter"/> of the passed <see cref="TResult"/> type.
        /// </summary>
        /// <returns></returns>
        public TaskAwaiter<TResult> GetAwaiter() 
        {
            if (_taskCompletionSource != null)
                return _taskCompletionSource.Task.GetAwaiter();
                
            _taskCompletionSource = new TaskCompletionSource<TResult>();
            
            if (_isComplete)
                _setResult();
            else
                DidComplete += result => _setResult();

            return _taskCompletionSource.Task.GetAwaiter();
        }
        
        /// <summary>
        /// Constructs an <see cref="AwaitableAndroidJavaProxy{TResult}"/>
        /// </summary>
        /// <param name="nativeInterface">Java interface implemented by the proxy.</param>
        protected AwaitableAndroidJavaProxy(string nativeInterface) : base(nativeInterface) { }

        protected void _complete(TResult result)
        {
            if (_isComplete)
                return;

            _result = result;
            var toComplete = DidComplete;
            DidComplete = null;
            _isComplete = true;
            toComplete?.Invoke(_result);
        }
        
        private void _setResult()
        {
            try
            {
                _taskCompletionSource.TrySetResult(_result);
            }
            catch (ObjectDisposedException e)
            {
               ChartboostCoreLogger.LogException(e);
            }
        }

        private TaskCompletionSource<TResult> _taskCompletionSource;
        private event Action<TResult> DidComplete;
        private TResult _result;
        private bool _isComplete;
    }
}
