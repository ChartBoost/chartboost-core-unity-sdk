using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chartboost.Core.Default
{
    internal partial class AnalyticsEnvironment
    {
        public event Action IsUserUnderageChanged;
        public event Action PublisherSessionIdentifierChanged;
        public event Action PublisherAppIdentifierChanged;
        public event Action FrameworkNameChanged;
        public event Action FrameworkVersionChanged;
        public event Action PlayerIdentifierChanged;
        
        public static void OnIsUserUnderageChanged()
        {
            _instance?.IsUserUnderageChanged?.Invoke();
        }

        public static void OnPublisherSessionIdentifierChanged()
        {
            _instance?.PublisherSessionIdentifierChanged?.Invoke();
        }

        public static void OnPublisherAppIdentifierChanged()
        {
            _instance?.PublisherAppIdentifierChanged?.Invoke();
        }

        public static void OnFrameworkNameChanged()
        {
            _instance?.FrameworkNameChanged?.Invoke();
        }

        public static void OnFrameworkVersionChanged()
        {
            _instance?.FrameworkVersionChanged?.Invoke();
        }

        public static void OnPlayerIdentifierChanged()
        {
            _instance?.PlayerIdentifierChanged?.Invoke();
        }
    }
}
