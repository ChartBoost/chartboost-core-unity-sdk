using System;
using Newtonsoft.Json;

namespace Chartboost.Core.Error
{
    [Serializable]
    #nullable enable
    public struct ChartboostCoreError
    {
        #pragma warning disable 0414
        /// <summary>
        /// A string containing the error domain.
        /// </summary>
        [JsonProperty("domain")]
        private string _domain;
        #pragma warning restore 0414
        
        /// <summary>
        /// The error code.
        /// </summary>
        [JsonProperty("code")]
        public readonly int Code;
        
        /// <summary>
        /// The error message.
        /// </summary>
        [JsonProperty("message")]
        public readonly string Message;

        /// <summary>
        /// The error cause.
        /// </summary>
        [JsonProperty("cause")]
        public readonly string? Cause;

        /// <summary>
        /// The error resolution.
        /// </summary>
        [JsonProperty("resolution")]
        public readonly string Resolution;

        public ChartboostCoreError(int code, string message, string? cause = null, string resolution = "")
        {
            _domain = "com.chartboost.core.unity";
            Code = code;
            Message = message;
            Cause = cause;
            Resolution = resolution;
        }
    }
    #nullable disable
}
