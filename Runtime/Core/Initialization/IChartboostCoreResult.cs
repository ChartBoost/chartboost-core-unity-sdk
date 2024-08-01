using System;
using Chartboost.Core.Error;

namespace Chartboost.Core.Initialization
{
    /// <summary>
    /// A base result class for Chartboost Core operations.
    /// </summary>
    public interface IChartboostCoreResult
    {
        /// <summary>
        /// The start date of the operation.
        /// </summary>
        public DateTime Start { get; }

        /// <summary>
        /// The end date of the operation.
        /// </summary>
        
        public DateTime End { get; }

        /// <summary>
        /// The duration of the operation.
        /// </summary>
        
        public long Duration { get; }

        /// <summary>
        /// The error in case the operation failed, or null if the operation succeeded.
        /// </summary>
        public ChartboostCoreError? Error { get; }
        
        public string ToJson();
    }
}
