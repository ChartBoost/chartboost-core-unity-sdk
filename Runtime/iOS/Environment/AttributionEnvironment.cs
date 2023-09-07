using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Core.Environment;
using Chartboost.Core.iOS.Utilities;

namespace Chartboost.Core.iOS.Environment
{
    #nullable enable
    /// <summary>
    /// <para>iOS Implementation of <see cref="IAttributionEnvironment"/>.</para>
    /// <inheritdoc cref="IAttributionEnvironment"/>
    /// </summary>
    public class AttributionEnvironment : BaseIOSEnvironment, IAttributionEnvironment
    {
        /// <inheritdoc cref="IAttributionEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => Task.FromResult(_attributionEnvironmentGetAdvertisingIdentifier());
        
        /// <inheritdoc cref="IAttributionEnvironment.UserAgent"/>
        public Task<string?> UserAgent => AwaitableString(_attributionEnvironmentGetUserAgent);
        
        [DllImport(IOSConstants.DLLImport)] private static extern string? _attributionEnvironmentGetAdvertisingIdentifier();
        [DllImport(IOSConstants.DLLImport)] private static extern void _attributionEnvironmentGetUserAgent(int hashCode, ChartboostCoreOnResultString callback);
    }
    #nullable disable
}
