using System;
using Chartboost.Core.Error;
using Chartboost.Json;
using Newtonsoft.Json;

namespace Chartboost.Core.Initialization
{
    #nullable enable
    /// <summary>
    /// A result object with the information regarding a module initialization operation.
    /// </summary>
    [Serializable]
    public struct ModuleInitializationResult : IChartboostCoreResult
    {
        /// <inheritdoc/>
        public DateTime Start => _start;
        
        /// <inheritdoc/>
        public DateTime End => _end;
        
        /// <inheritdoc/>
        public long Duration => _duration;
        
        /// <inheritdoc/>
        public ChartboostCoreError? Error => _error;


        /// <summary>
        /// 
        /// </summary>
        public string ModuleId => _moduleId;
        
        /// <summary>
        /// 
        /// </summary>
        public string ModuleVersion => _moduleVersion;
        

        private static readonly DateTime BaseDate = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        [JsonProperty("start")]
        private readonly DateTime _start;
        
        [JsonProperty("end")]
        private readonly DateTime _end;
        
        [JsonProperty("duration")]
        private readonly long _duration;

        [JsonProperty("exception")]
        private readonly ChartboostCoreError? _error;
        
        [JsonProperty("moduleId")]
        private readonly string _moduleId;
        
        [JsonProperty("moduleVersion")]
        private readonly string _moduleVersion;

        public ModuleInitializationResult(DateTime start, DateTime end, string moduleId, string moduleVersion, ChartboostCoreError? error) 
        {
            _start = start;
            _end = end;
            var duration = end - start;
            _duration = (long)duration.TotalMilliseconds;
            _moduleId = moduleId;
            _moduleVersion = moduleVersion;
            _error = error;
        }

        public ModuleInitializationResult(long start, long end, long duration, string moduleId, string moduleVersion, ChartboostCoreError? error)
        {
            _start = BaseDate.AddMilliseconds(start);
            _end = BaseDate.AddMilliseconds(end);
            _duration = duration;
            _moduleId = moduleId;
            _moduleVersion = moduleVersion;
            _error = error;
        }
        
        public string ToJson()
            => JsonTools.SerializeObject(this);
    }
}
