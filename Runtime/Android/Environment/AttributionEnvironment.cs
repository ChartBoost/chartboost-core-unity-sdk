using Chartboost.Core.Android.Utilities;
using Chartboost.Core.Environment;

namespace Chartboost.Core.Android.Environment
{
    #nullable enable
    internal sealed class AttributionEnvironment : BaseAndroidEnvironment, IAttributionEnvironment
    {
        protected override string EnvironmentProperty => AndroidConstants.EnvironmentAttribution;
        public string? AdvertisingIdentifier => GetProperty<string?>(AndroidConstants.GetPropertyAdvertisingIdentifier);
        public string? UserAgent => GetProperty<string?>(AndroidConstants.GetPropertyUserAgent);
    }
    #nullable disable
}
