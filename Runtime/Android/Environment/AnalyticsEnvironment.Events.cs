using System;
using Chartboost.Core.Android.AndroidJavaProxies;
using Chartboost.Core.Android.Utilities;
using UnityEngine;

namespace Chartboost.Core.Android.Environment
{
    internal partial class AnalyticsEnvironment
    {
        internal AnalyticsEnvironment()
        {
            using var sdk = Utilities.AndroidExtensions.NativeSDK();
            using var analyticsEnvironment = sdk.CallStatic<AndroidJavaObject>(EnvironmentProperty);
            analyticsEnvironment.Call(AndroidConstants.FunctionAddObserver, new EnvironmentObserver(this));
        }

        public event Action IsUserUnderageChanged;
        public event Action PublisherSessionIdentifierChanged;
        public event Action PublisherAppIdentifierChanged;
        public event Action FrameworkNameChanged;
        public event Action FrameworkVersionChanged;
        public event Action PlayerIdentifierChanged;
        
        internal void OnIsUserUnderageChanged()
            => IsUserUnderageChanged?.Invoke();

        internal void OnPublisherSessionIdentifierChanged()
            => PublisherSessionIdentifierChanged?.Invoke();

        internal void OnPublisherAppIdentifierChanged() 
            => PublisherAppIdentifierChanged?.Invoke();

        internal void OnFrameworkNameChanged() 
            => FrameworkNameChanged?.Invoke();

        internal void OnFrameworkVersionChanged() 
            => FrameworkVersionChanged?.Invoke();

        internal void OnPlayerIdentifierChanged() 
            => PlayerIdentifierChanged?.Invoke();
        
        ~AnalyticsEnvironment()
        {
            AndroidJNI.AttachCurrentThread();
            using var sdk = Utilities.AndroidExtensions.NativeSDK();
            using var analyticsEnvironment = sdk.CallStatic<AndroidJavaObject>(EnvironmentProperty);
            analyticsEnvironment.Call(AndroidConstants.FunctionRemoveObserver, EnvironmentObserver.Instance);
            AndroidJNI.DetachCurrentThread();
        }
    }
}
