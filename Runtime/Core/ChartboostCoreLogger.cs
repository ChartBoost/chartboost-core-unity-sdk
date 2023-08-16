using System;
using UnityEngine;

namespace Chartboost.Core
{
    public static class ChartboostCoreLogger
    {
        public static bool UnityLogger { get; set; } = true;
        
        private const string Tag = "[ChartboostCoreUnity]";

        public static void Log(string message)
        {
            CoreLog(message, LogType.Log);
        }

        public static void LogWarning(string message)
        {
            CoreLog(message, LogType.Warning);
        }

        public static void LogError(string message)
        {
            CoreLog(message, LogType.Error);
        }

        public static void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }

        private static void CoreLog(string message, LogType type)
        {
            if (!ChartboostCore.Debug)
                return;
            
            var logMessage = $"{Tag} {message}";
            if (!UnityLogger) {
                Console.WriteLine(logMessage);
                return;
            }

            switch (type)
            {
                case LogType.Log:
                    Debug.Log(logMessage);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(logMessage);
                    break;
                case LogType.Error:
                    Debug.LogError(logMessage);
                    break;
                default:
                    Console.WriteLine(logMessage);
                    break;
            }
        }
    }
}
