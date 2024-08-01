using Chartboost.Constants;
using Chartboost.Core.Android.Environment;
using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;
using Chartboost.Core.Utilities;
using UnityEngine;
using UnityEngine.Scripting;

namespace Chartboost.Core.Android.AndroidJavaProxies
{
#nullable enable
    internal class EnvironmentObserver : AndroidJavaProxy
    {
        internal static EnvironmentObserver? Instance { get; private set; }
        private readonly AnalyticsEnvironment _environment;
        
        public EnvironmentObserver(AnalyticsEnvironment environment) : base(AndroidConstants.EnvironmentObserver)
        {
            _environment = environment;
            Instance = this;
        }

        [Preserve]
        // ReSharper disable once InconsistentNaming
        private void onChanged(AndroidJavaObject property)
        {
            MainThreadDispatcher.Post(o =>
            {
                var environmentProperty = property.Call<string>(SharedAndroidConstants.FunctionToString).ToObservableProperty();
                switch (environmentProperty)
                {
                    case ObservableEnvironmentProperty.FrameworkName:
                        _environment.OnFrameworkNameChanged();
                        break;
                    case ObservableEnvironmentProperty.FrameworkVersion:
                        _environment.OnFrameworkVersionChanged();
                        break;
                    case ObservableEnvironmentProperty.IsUserUnderage:
                        _environment.OnIsUserUnderageChanged();
                        break;
                    case ObservableEnvironmentProperty.PlayerIdentifier:
                        _environment.OnPlayerIdentifierChanged();
                        break;
                    case ObservableEnvironmentProperty.PublisherAppIdentifier:
                        _environment.OnPublisherAppIdentifierChanged();
                        break;
                    case ObservableEnvironmentProperty.PublisherSessionIdentifier:
                        _environment.OnPublisherSessionIdentifierChanged();
                        break;
                }
            });
        }
    }
#nullable disable
}
