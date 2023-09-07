using System;
using UnityEngine;

namespace Chartboost.Core
{
    /// <summary>
    /// Core SDK common logger. Messages by this logger are controlled by <see cref="ChartboostCore.Debug"/> flag.
    /// <br/>
    /// <para>Exceptions are always logged.</para>
    /// </summary>
    public static class ChartboostCoreLogger
    {
        public static bool UnityLogger { get; set; } = true;
        
        private const string Tag = "[ChartboostCoreUnity]";

        /// <summary>
        /// Logs a message to the Unity Console.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void Log(object message)
        {
            CoreLog(message, LogType.Log);
        }

        /// <summary>
        /// A variant of ChartboostCoreLogger.Log that logs a warning message to the console.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void LogWarning(string message)
        {
            CoreLog(message, LogType.Warning);
        }

        /// <summary>
        /// A variant of ChartboostCoreLogger.Log that logs an error message to the console.
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void LogError(string message)
        {
            CoreLog(message, LogType.Error);
        }

        /// <summary>
        /// A variant of Debug.Log that logs an error message to the console.
        /// </summary>
        /// <param name="exception">Runtime Exception.</param>
        public static void LogException(Exception exception)
        {
            Debug.LogException(exception);
        }

        /// <summary>
        /// Private handler for Core Logs
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="type">The type of the log message.</param>
        private static void CoreLog(object message, LogType type)
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
