using System.Threading.Tasks;

namespace Chartboost.Core.Environment
{
    #nullable enable
    /// <summary>
    /// An environment that contains information intended solely for attribution purposes.
    /// </summary>
    public interface IAttributionEnvironment
    { 
        /// <summary>
        /// The system advertising identifier (IFA).
        /// </summary>
        Task<string?> AdvertisingIdentifier { get; }
        
        /// <summary>
        /// The device user agent.
        /// </summary>
        Task<string?> UserAgent { get; }
    }
    #nullable disable
}
