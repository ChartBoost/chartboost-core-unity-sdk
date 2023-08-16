using System;
using Chartboost.Core.Error;
using Newtonsoft.Json;

namespace Chartboost.Core.Initialization
{
    #nullable enable
    [Serializable]
    public abstract class ChartboostCoreResult
    {
        private readonly DateTime _baseDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        
        [JsonProperty("start")]
        public readonly DateTime Start;
        [JsonProperty("end")]
        public readonly DateTime End;
        [JsonProperty("duration")]
        public readonly long Duration;
        [JsonProperty("exception")]
        public readonly ChartboostCoreError? Error;
        
        public ChartboostCoreResult(long start, long end, long duration, ChartboostCoreError? error)
        {
            Start = _baseDate.AddMilliseconds(start);
            End = _baseDate.AddMilliseconds(end);
            Duration = duration;
            Error = error;
        }

        public string ToJson() => JsonConvert.SerializeObject(this);
    }
    #nullable disable
}
