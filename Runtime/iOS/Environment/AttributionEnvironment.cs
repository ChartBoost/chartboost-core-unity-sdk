using System.Runtime.InteropServices;
using Chartboost.Core.Environment;
using Chartboost.Core.iOS.Utilities;

namespace Chartboost.Core.iOS.Environment
{
    #nullable enable
    public class AttributionEnvironment : IAttributionEnvironment
    {
        public string? AdvertisingIdentifier => _attributionEnvironmentGetAdvertisingIdentifier();
        public string? UserAgent => _attributionEnvironmentGetUserAgent();
        
        [DllImport(IOSConstants.DLLImport)] private static extern string? _attributionEnvironmentGetAdvertisingIdentifier();
        [DllImport(IOSConstants.DLLImport)] private static extern string? _attributionEnvironmentGetUserAgent();
    }
    #nullable disable
}
