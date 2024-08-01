using System;
using System.Runtime.InteropServices;
using AOT;
using Chartboost.Constants;
using Chartboost.Core.Environment;

namespace Chartboost.Core.iOS.Environment
{
    internal partial class AnalyticsEnvironment 
    {
        private static AnalyticsEnvironment _instance;
        internal AnalyticsEnvironment()
        {
            _instance = this;
            _CBCSetEnvironmentCallbacks(OnEnvironmentPropertyChange);
        }
        
        public event Action IsUserUnderageChanged;
        public event Action PublisherSessionIdentifierChanged;
        public event Action PublisherAppIdentifierChanged;
        public event Action FrameworkNameChanged;
        public event Action FrameworkVersionChanged;
        public event Action PlayerIdentifierChanged;

        private void OnIsUserUnderageChanged()
            => IsUserUnderageChanged?.Invoke();

        private void OnPublisherSessionIdentifierChanged()
            => PublisherSessionIdentifierChanged?.Invoke();

        private void OnPublisherAppIdentifierChanged() 
            => PublisherAppIdentifierChanged?.Invoke();

        private void OnFrameworkNameChanged() 
            => FrameworkNameChanged?.Invoke();

        private void OnFrameworkVersionChanged() 
            => FrameworkVersionChanged?.Invoke();

        private void OnPlayerIdentifierChanged() 
            => PlayerIdentifierChanged?.Invoke();
        
        [MonoPInvokeCallback(typeof(ExternChartboostCoreOnEnumStatusChange))]
        private static void OnEnvironmentPropertyChange(int value)
        {
            MainThreadDispatcher.Post(o =>
            {
                var property = (ObservableEnvironmentProperty)value;
                switch (property)
                {
                    case ObservableEnvironmentProperty.FrameworkName:
                        _instance.OnFrameworkNameChanged();
                        break;
                    case ObservableEnvironmentProperty.FrameworkVersion:
                        _instance.OnFrameworkVersionChanged();
                        break;
                    case ObservableEnvironmentProperty.IsUserUnderage:
                        _instance.OnIsUserUnderageChanged();
                        break;
                    case ObservableEnvironmentProperty.PlayerIdentifier:
                        _instance.OnPlayerIdentifierChanged();
                        break;
                    case ObservableEnvironmentProperty.PublisherAppIdentifier:
                        _instance.OnPublisherAppIdentifierChanged();
                        break;
                    case ObservableEnvironmentProperty.PublisherSessionIdentifier:
                        _instance.OnPublisherSessionIdentifierChanged();
                        break;
                }
            });
        }
        
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCSetEnvironmentCallbacks(ExternChartboostCoreOnEnumStatusChange onPublisherMetadataPropertyChange);
    }
}
