using System;
using Newtonsoft.Json;

namespace Chartboost.Core.Error
{
    [Serializable]
    #nullable enable
    public struct ChartboostCoreError
    {
        #pragma warning disable 0414
        [JsonProperty("domain")] 
        private string _domain;
        #pragma warning restore 0414
        
        [JsonProperty("code")]
        public readonly int Code;
        
        [JsonProperty("message")]
        public readonly string Message;

        [JsonProperty("cause")]
        public readonly string? Cause;

        [JsonProperty("resolution")]
        public readonly string? Resolution;

        public ChartboostCoreError(int code, string message, string? cause = null, string? resolution = null)
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
