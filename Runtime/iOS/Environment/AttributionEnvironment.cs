using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Chartboost.Constants;
using Chartboost.Core.Environment;

namespace Chartboost.Core.iOS.Environment
{
    #nullable enable
    /// <summary>
    /// <para>iOS Implementation of <see cref="IAttributionEnvironment"/>.</para>
    /// <inheritdoc cref="IAttributionEnvironment"/>
    /// </summary>
    internal class AttributionEnvironment : BaseIOSEnvironment, IAttributionEnvironment
    {
        /// <inheritdoc cref="IAttributionEnvironment.AdvertisingIdentifier"/>
        public Task<string?> AdvertisingIdentifier => Task.FromResult(_CBCAttributionGetAdvertisingIdentifier());
        
        /// <inheritdoc cref="IAttributionEnvironment.UserAgent"/>
        public Task<string?> UserAgent => AwaitableString(_CBCAttributionGetUserAgent);
        
        [DllImport(SharedIOSConstants.DLLImport)] private static extern string? _CBCAttributionGetAdvertisingIdentifier();
        [DllImport(SharedIOSConstants.DLLImport)] private static extern void _CBCAttributionGetUserAgent(int hashCode, ExternChartboostCoreOnResultString callback);
    }
    #nullable disable
}
