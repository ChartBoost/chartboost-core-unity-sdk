using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Environment;
using Chartboost.Json;
using Newtonsoft.Json;

namespace Chartboost.Core.Utilities
{
    #nullable enable
    /// <summary>
    /// Extension class to simplify serialization of Core environments.
    /// </summary>
    internal static class EnvironmentSerialization
    {
        #region Advertising
        /// <summary>
        /// Creates a <see cref="Dictionary{TKey,TValue}"/> based off <see cref="IAdvertisingEnvironment"/> values.
        /// </summary>
        /// <param name="env"><see cref="IAdvertisingEnvironment"/> instance.</param>
        public static async Task<Dictionary<string, object?>>? ToDictionary(this IAdvertisingEnvironment env)
        {
            return await Task.FromResult(new Dictionary<string, object?>
            {
                {nameof(env.OSName), env.OSName},
                {nameof(env.OSVersion), env.OSVersion},
                {nameof(env.DeviceMake), env.DeviceMake},
                {nameof(env.DeviceModel), env.DeviceModel},
                {nameof(env.DeviceLocale), env.DeviceLocale},
                {nameof(env.ScreenHeightPixels), env.ScreenHeightPixels},
                {nameof(env.ScreenScale), env.ScreenScale},
                {nameof(env.ScreenWidthPixels), env.ScreenWidthPixels},
                {nameof(env.BundleIdentifier), env.BundleIdentifier},
                {nameof(env.LimitAdTrackingEnabled), env.LimitAdTrackingEnabled},
                {nameof(env.AdvertisingIdentifier), await env.AdvertisingIdentifier},
            });
        }
        
        /// <summary>
        /// Creates a JSON <see cref="string"/> based off <see cref="IAdvertisingEnvironment"/> values.
        /// </summary>
        public static Task<string?> ToJson(this IAdvertisingEnvironment env) => ToJson(env.ToDictionary()!);
        #endregion

        #region Analytics
        /// <summary>
        /// Creates a <see cref="Dictionary{TKey,TValue}"/> based off <see cref="IAnalyticsEnvironment"/> values.
        /// </summary>
        /// <param name="env"><see cref="IAnalyticsEnvironment"/> instance.</param>
        public static async Task<Dictionary<string, object?>>? ToDictionary(this IAnalyticsEnvironment env )
        {
            return await Task.FromResult(new Dictionary<string, object?>
            {
                {nameof(env.OSName), env.OSName},
                {nameof(env.OSVersion), env.OSVersion},
                {nameof(env.DeviceMake), env.DeviceMake},
                {nameof(env.DeviceModel), env.DeviceModel},
                {nameof(env.DeviceLocale), env.DeviceLocale},
                {nameof(env.ScreenHeightPixels), env.ScreenHeightPixels},
                {nameof(env.ScreenScale), env.ScreenScale},
                {nameof(env.ScreenWidthPixels), env.ScreenWidthPixels},
                {nameof(env.BundleIdentifier), env.BundleIdentifier},
                {nameof(env.LimitAdTrackingEnabled), env.LimitAdTrackingEnabled},
                {nameof(env.AdvertisingIdentifier), await env.AdvertisingIdentifier},
                {nameof(env.UserAgent), await env.UserAgent},
                {nameof(env.NetworkConnectionType), env.NetworkConnectionType},
                {nameof(env.Volume), env.Volume},
                {nameof(env.VendorIdentifier), env.VendorIdentifier},
                {nameof(env.VendorIdentifierScope), await env.VendorIdentifierScope},
                {nameof(env.AppTrackingTransparencyStatus), env.AppTrackingTransparencyStatus},
                {nameof(env.AppVersion), env.AppVersion},
                {nameof(env.AppSessionDuration), env.AppSessionDuration},
                {nameof(env.AppSessionIdentifier), env.AppSessionIdentifier},
                {nameof(env.IsUserUnderage), env.IsUserUnderage},
                {nameof(env.PublisherSessionIdentifier), env.PublisherSessionIdentifier},
                {nameof(env.PublisherAppIdentifier), env.PublisherAppIdentifier},
                {nameof(env.FrameworkName), env.FrameworkName},
                {nameof(env.FrameworkVersion), env.FrameworkVersion},
                {nameof(env.PlayerIdentifier), env.PlayerIdentifier}
            });
        }

        /// <summary>
        /// Creates a JSON <see cref="string"/> based off <see cref="IAnalyticsEnvironment"/> values.
        /// </summary>
        public static Task<string?> ToJson(this IAnalyticsEnvironment env) => ToJson(env.ToDictionary()!);
        #endregion

        #region Attribution
        /// <summary>
        /// Creates a <see cref="Dictionary{TKey,TValue}"/> based off <see cref="IAttributionEnvironment"/> values.
        /// </summary>
        /// <param name="env"><see cref="IAttributionEnvironment"/> instance.</param>
        public static async Task<Dictionary<string, object?>>? ToDictionary(this IAttributionEnvironment env)
        {
            return await Task.FromResult(new Dictionary<string, object?>
            {
                {nameof(env.AdvertisingIdentifier), await env.AdvertisingIdentifier},
                {nameof(env.UserAgent), await env.UserAgent},
            });
        }
        
        /// <summary>
        /// Creates a JSON <see cref="string"/> based off <see cref="IAttributionEnvironment"/> values.
        /// </summary>
        public static Task<string?> ToJson(this IAttributionEnvironment env) => ToJson(env.ToDictionary()!);
        #endregion

        /// <summary>
        /// Serializes a <see cref="Dictionary{TKey,TValue}"/> into a JSON <see cref="string"/>.
        /// </summary>
        /// <param name="objects"><see cref="Task"/> providing the <see cref="Dictionary{TKey,TValue}"/>.</param>
        /// <returns>JSON representation of <see cref="Dictionary{TKey,TValue}"/>.</returns>
        private static async Task<string?> ToJson(Task<Dictionary<string, object?>?> objects)
        {
            var contents= await objects;
            var contentsAsJson = JsonTools.SerializeObject(contents);
            return contentsAsJson;
        }
    }
}
