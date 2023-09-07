using System.Threading.Tasks;
using Chartboost.Core.Environment;

namespace Chartboost.Core.Default
{
    #nullable enable
    /// <summary>
    /// <inheritdoc cref="IAttributionEnvironment"/>
    /// <br/>
    /// <para> Default class implementation for unsupported platforms.</para>
    /// </summary>
    public class AttributionEnvironment : IAttributionEnvironment
    {
        /// <inheritdoc cref="IAttributionEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => Task.FromResult<string?>(null);
        
        /// <inheritdoc cref="IAttributionEnvironment.UserAgent"/>
        public Task<string?> UserAgent => Task.FromResult<string?>(null);
    }
    #nullable enable
}
