using System;
using Chartboost.Core.Error;
using Newtonsoft.Json;

namespace Chartboost.Core.Initialization
{
    #nullable enable
    /// <summary>
    /// A base result class for Chartboost Core operations.
    /// </summary>
    [Serializable]
    public abstract class ChartboostCoreResult
    {
        private readonly DateTime _baseDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        
        /// <summary>
        /// The start date of the operation.
        /// </summary>
        [JsonProperty("start")]
        public readonly DateTime Start;
        
        /// <summary>
        /// The end date of the operation.
        /// </summary>
        [JsonProperty("end")]
        public readonly DateTime End;
        
        /// <summary>
        /// The duration of the operation.
        /// </summary>
        [JsonProperty("duration")]
        public readonly long Duration;
        
        /// <summary>
        /// The error in case the operation failed, or null if the operation succeeded.
        /// </summary>
        [JsonProperty("exception")]
        public readonly ChartboostCoreError? Error;
        
        public ChartboostCoreResult(long start, long end, long duration, ChartboostCoreError? error)
        {
            Start = _baseDate.AddMilliseconds(start);
            End = _baseDate.AddMilliseconds(end);
            Duration = duration;
            Error = error;
        }

        public ChartboostCoreResult(DateTime start, DateTime end, ChartboostCoreError? error)
        {
            Start = start;
            End = end;
            Error = error;
            var duration = end - start;
            Duration = (long)duration.TotalMilliseconds;
        }

        public string ToJson() => JsonConvert.SerializeObject(this);
    }
    #nullable disable
}
