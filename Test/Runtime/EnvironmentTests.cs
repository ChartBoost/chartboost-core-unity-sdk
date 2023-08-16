using System.Collections;
using Chartboost.Core.Environment;
using Chartboost.Core.Initialization;
using Chartboost.Core.Modules;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Chartboost.Core.Tests
{
    public class EnvironmentTests
    {
        private const string ConstFrameworkName = "CUSTOM_CORE_ANALYTICS";
        private const string ConstFrameworkVersion = "0.0.1";
        private const bool ConstIsUserUnderage = true;
        private const string ConstPlayerIdentifier = "PLAYER_IDENTIFIER_ANALYTICS";
        private const string ConstPublisherSessionIdentifier = "SESSION_IDENTIFIER_ANALYTICS";
        private static readonly InitializableModule SuccessfulModule = new TestUnityModuleSuccess();
        private static readonly InitializableModule FailureModule = new TestUnityModuleFailure();
        private const float ConstDelayAfterInit = 10f;
        
        private readonly InitializableModule[] _modules = new[]
        {
            SuccessfulModule,
            FailureModule
        };

        [SetUp]
        public void Setup()
        {
            ChartboostCore.Debug = true;
        }

        [Test, Order(0)]
        public void GetVersion()
        {
            var nativeSDKVersion = ChartboostCore.NativeSDKVersion;
            Assert.IsNotNull(nativeSDKVersion);
            Assert.IsNotEmpty(nativeSDKVersion);

            var unitySDKVersion = ChartboostCore.UnitySDKVersion;
            
            ChartboostCoreLogger.Log($"NativeSDKVersion: {nativeSDKVersion}");
            ChartboostCoreLogger.Log($"UnitySDKVersion: {unitySDKVersion}");
            
            Assert.IsNotNull(unitySDKVersion);
            Assert.IsNotEmpty(unitySDKVersion);

            if (unitySDKVersion == nativeSDKVersion) 
                return;

            ChartboostCoreLogger.Log($"NativeSDKVersion does not match UnitySDKVersion, this is expected but not ideal.");
            Assert.Pass();
        }

        [UnityTest, Order(0)]
        public IEnumerator ModuleInitialization()
        {
            const string dateFormat = "yyyy/MM/dd HH:mm:ss.fff";

            ChartboostCore.ModuleInitializationCompleted += AssertModules;

            void AssertModules(ModuleInitializationResult result)
            {
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ToJson());
                Assert.IsNotEmpty(result.ToJson());
                
                Assert.IsNotNull(result.Start);
                Assert.IsNotNull(result.End);
                Assert.GreaterOrEqual(result.Duration, 0);
                ChartboostCoreLogger.Log($"--------");
                ChartboostCoreLogger.Log($"Start: {result.Start.ToString(dateFormat)}");
                ChartboostCoreLogger.Log($"End: {result.End.ToString(dateFormat)}");
                ChartboostCoreLogger.Log($"Duration: {result.Duration}");
                ChartboostCoreLogger.Log($"Module Id: {result.Module.ModuleId}");
                ChartboostCoreLogger.Log($"Module Version: {result.Module.ModuleVersion}");
                
                if (result.Module == SuccessfulModule)
                {
                    Assert.AreEqual(result.Module.ModuleId, SuccessfulModule.ModuleId);
                    Assert.AreEqual(result.Module.ModuleVersion, SuccessfulModule.ModuleVersion);
                    Assert.IsNull(result.Error);
                }

                else if (result.Module == FailureModule)
                {
                    Assert.AreEqual(result.Module.ModuleId, FailureModule.ModuleId);
                    Assert.AreEqual(result.Module.ModuleVersion, FailureModule.ModuleVersion);
                    Assert.IsNotNull(result.Error);
                    ChartboostCoreLogger.Log($"Exception: {JsonConvert.SerializeObject(result.Error)}\n Default: {JsonConvert.SerializeObject(TestUnityModuleFailure.Error)}");
                    Assert.AreEqual(result.Error, TestUnityModuleFailure.Error);
                }
                ChartboostCoreLogger.Log($"--------");
            }

            var sdkConfig = new SDKConfiguration(Application.identifier);
            ChartboostCore.Initialize(sdkConfig, _modules);
            yield return new WaitForSeconds(ConstDelayAfterInit);
            ChartboostCore.ModuleInitializationCompleted -= AssertModules;
        }

        [Test, Order(1)]
        public void Debug()
        {
            ChartboostCore.Debug = false;
            Assert.IsFalse(ChartboostCore.Debug);
            ChartboostCore.Debug = true;
            var debugging = ChartboostCore.Debug;
            Assert.IsTrue(debugging);
            ChartboostCoreLogger.Log($"Debug: {debugging}");
        }

        [Test, Order(1)]
        public void AdvertisingIdentifier()
        {
            var attribution = ChartboostCore.AttributionEnvironment.AdvertisingIdentifier;
            var advertising = ChartboostCore.AdvertisingEnvironment.AdvertisingIdentifier;
            var analytics = ChartboostCore.AnalyticsEnvironment.AdvertisingIdentifier;
            ChartboostCoreLogger.Log($"AdvertisingIdentifier Advertising: {advertising},");
            ChartboostCoreLogger.Log($"AdvertisingIdentifier Analytics: {analytics}");
            ChartboostCoreLogger.Log($"AdvertisingIdentifier Attribution: {attribution}");
            Assert.AreEqual(attribution, advertising);
            Assert.AreEqual(advertising, analytics);
            
            Assert.IsNotNull(analytics);
            Assert.IsNotEmpty(analytics);
        }

        [Test, Order(1)]
        public void UserAgent()
        {
            var attribution = ChartboostCore.AttributionEnvironment.UserAgent;
            var analytics = ChartboostCore.AnalyticsEnvironment.UserAgent;
            ChartboostCoreLogger.Log($"UserAgent Attribution: {attribution}");
            ChartboostCoreLogger.Log($"UserAgent Analytics: {analytics}");
            
            Assert.AreEqual(attribution, analytics);
            
            if (string.IsNullOrEmpty(attribution))
                Assert.Inconclusive("UserAgent can be null");
            else
                Assert.Pass();
        }
        
        [Test, Order(1)]
        public void OSName()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.OSName;
            var analytics = ChartboostCore.AnalyticsEnvironment.OSName;
            ChartboostCoreLogger.Log($"OSName Advertising: {advertising}");
            ChartboostCoreLogger.Log($"OSName Analytics: {analytics}");
            Assert.AreEqual(advertising, analytics);

            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    Assert.AreEqual(analytics, "Android");
                    break;
                case RuntimePlatform.IPhonePlayer:
                    break;
                default:
                    return;
            }
        }
        
        [Test, Order(1)]
        public void OSVersion()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.OSVersion;
            var analytics = ChartboostCore.AnalyticsEnvironment.OSVersion;
            ChartboostCoreLogger.Log($"OSVersion Advertising: {advertising}");
            ChartboostCoreLogger.Log($"OSVersion Analytics: {analytics}");
            Assert.AreEqual(advertising, analytics);
            Assert.IsNotNull(analytics);
            Assert.IsNotEmpty(analytics);
        }
        
        [Test, Order(1)]
        public void DeviceMake()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.DeviceMake;
            var analytics = ChartboostCore.AnalyticsEnvironment.DeviceMake;
            ChartboostCoreLogger.Log($"DeviceMake Advertising: {advertising}");
            ChartboostCoreLogger.Log($"DeviceMake Analytics: {analytics}");
            Assert.AreEqual(advertising, analytics);
            Assert.IsNotNull(analytics);
            Assert.IsNotEmpty(analytics);
        }
        
        [Test, Order(1)]
        public void DeviceModel()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.DeviceModel;
            var analytics = ChartboostCore.AnalyticsEnvironment.DeviceModel;
            ChartboostCoreLogger.Log($"DeviceModel Advertising: {advertising}");
            ChartboostCoreLogger.Log($"DeviceModel Analytics: {analytics}");
            Assert.AreEqual(advertising, analytics);
            Assert.IsNotNull(analytics);
            Assert.IsNotEmpty(analytics);
        }
        
        [Test, Order(1)]
        public void DeviceLocale()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.DeviceLocale;
            var analytics = ChartboostCore.AnalyticsEnvironment.DeviceLocale;
            ChartboostCoreLogger.Log($"DeviceLocale Advertising: {advertising}");
            ChartboostCoreLogger.Log($"DeviceLocale Analytics: {analytics}");
            Assert.AreEqual(advertising, analytics);
            Assert.IsNotNull(analytics);
            Assert.IsNotEmpty(analytics);
        }
        
        [Test, Order(1)]
        public void ScreenHeight()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.ScreenHeight;
            var analytics = ChartboostCore.AnalyticsEnvironment.ScreenHeight;
            ChartboostCoreLogger.Log($"ScreenWidth Advertising: {advertising}");
            ChartboostCoreLogger.Log($"ScreenWidth Analytics: {analytics}");
            ChartboostCoreLogger.Log($"ScreenWidth Unity: {Screen.height}");
            Assert.AreEqual(advertising, analytics);
            Assert.GreaterOrEqual(Screen.height, analytics);
            Assert.IsNotNull(analytics);
        }

        [Test, Order(1)]
        public void ScreenWidth()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.ScreenWidth;
            var analytics = ChartboostCore.AnalyticsEnvironment.ScreenWidth;
            ChartboostCoreLogger.Log($"ScreenWidth Advertising: {advertising}");
            ChartboostCoreLogger.Log($"ScreenWidth Analytics: {analytics}");
            Assert.AreEqual(advertising, analytics);
            Assert.AreEqual(analytics, Screen.width);
            Assert.IsNotNull(analytics);
        }
        
        [Test, Order(1)]
        public void ScreenScale()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.ScreenScale;
            var analytics = ChartboostCore.AnalyticsEnvironment.ScreenScale;
            ChartboostCoreLogger.Log($"ScreenScale Advertising: {advertising}");
            ChartboostCoreLogger.Log($"ScreenScale Analytics: {analytics}");
            ChartboostCoreLogger.Log($"ScreenScale Unity: {Screen.dpi}");
            Assert.AreEqual(advertising, analytics);
            Assert.Greater(ChartboostCore.AdvertisingEnvironment.ScreenScale, 0);
            Assert.IsNotNull(analytics);
        }
        
        [Test, Order(1)]
        public void BundleIdentifier()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.BundleIdentifier;
            var analytics = ChartboostCore.AnalyticsEnvironment.BundleIdentifier;
            ChartboostCoreLogger.Log($"BundleIdentifier Advertising: {advertising}");
            ChartboostCoreLogger.Log($"BundleIdentifier Analytics: {analytics}");
            ChartboostCoreLogger.Log($"BundleIdentifier Unity: {Application.identifier}");
            Assert.AreEqual(advertising, analytics);
            Assert.AreEqual(analytics, Application.identifier);
            Assert.IsNotNull(analytics);
        }

        [Test, Order(1)]
        public void LimitAdTrackingEnabled()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.LimitAdTrackingEnabled;
            var analytics = ChartboostCore.AnalyticsEnvironment.LimitAdTrackingEnabled;
            ChartboostCoreLogger.Log($"LimitAdTrackingEnabled Advertising: {advertising}");
            ChartboostCoreLogger.Log($"LimitAdTrackingEnabled Analytics: {analytics}");
            Assert.AreEqual(advertising, analytics);
        }
        
        [Test, Order(1)]
        public void NetworkConnectionType()
        {
            var networkConnectionType = ChartboostCore.AnalyticsEnvironment.NetworkConnectionType;
            ChartboostCoreLogger.Log($"NetworkConnectionType Analytics: {networkConnectionType}");
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
                Assert.AreEqual(networkConnectionType, Environment.NetworkConnectionType.Unknown);
            Assert.IsNotNull(networkConnectionType);
        }

        [Test, Order(1)]
        public void VendorIdentifierScope()
        {
            var vendorIdentifierScope = ChartboostCore.AnalyticsEnvironment.VendorIdentifierScope;
            ChartboostCoreLogger.Log($"VendorIdentifierScope Analytics: {vendorIdentifierScope}");
            Assert.IsNotNull(vendorIdentifierScope);
        }

        [Test, Order(1)]
        public void Volume()
        {
            var volume = ChartboostCore.AnalyticsEnvironment.Volume;
            ChartboostCoreLogger.Log($"Volume Analytics: {volume}");
            Assert.GreaterOrEqual(volume, 0);
            Assert.IsNotNull(volume);
        }
        
        [Test, Order(1)]
        public void VendorIdentifier()
        {
            var vendorIdentifier = ChartboostCore.AnalyticsEnvironment.VendorIdentifier;
            ChartboostCoreLogger.Log($"VendorIdentifier Analytics: {vendorIdentifier}");
            Assert.IsNotNull(vendorIdentifier);
            Assert.IsNotEmpty(vendorIdentifier);
        }
        
        [Test, Order(1)]
        public void AppTrackingTransparencyStatus()
        {
            var appTrackingTransparencyStatus = ChartboostCore.AnalyticsEnvironment.AppTrackingTransparencyStatus;
            ChartboostCoreLogger.Log($"AppTrackingTransparencyStatus Analytics: {appTrackingTransparencyStatus}");
            
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    Assert.AreEqual(appTrackingTransparencyStatus, AuthorizationStatus.Unsupported);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    Assert.AreNotEqual(appTrackingTransparencyStatus, AuthorizationStatus.Unsupported);
                    break;
                default:
                    return;
            }
        }
        
        [Test, Order(1)]
        public void AppVersion()
        {
            var appVersion = ChartboostCore.AnalyticsEnvironment.AppVersion;
            ChartboostCoreLogger.Log($"AppVersion Analytics: {appVersion}");
            ChartboostCoreLogger.Log($"AppVersion Unity: {Application.version}");
            Assert.AreEqual(appVersion, Application.version);
            Assert.IsNotNull(appVersion);
            Assert.IsNotEmpty(appVersion);
        }
        
        [Test, Order(1)]
        public void AppSessionDuration()
        {
            var appSessionDuration = ChartboostCore.AnalyticsEnvironment.AppSessionDuration;
            ChartboostCoreLogger.Log($"AppSessionDuration Analytics: {appSessionDuration}");
            Assert.IsNotNull(appSessionDuration);
            
            if (appSessionDuration <= 0)
                Assert.Inconclusive("AppSessionDuration can be 0, it only increases after initialization");
            else
                Assert.Pass();
        }
        
        [Test, Order(1)]
        public void AppSessionIdentifier()
        {
            var appSessionIdentifier = ChartboostCore.AnalyticsEnvironment.AppSessionIdentifier;
            ChartboostCoreLogger.Log($"AppSessionIdentifier Analytics: {appSessionIdentifier}");
            if (string.IsNullOrEmpty(appSessionIdentifier))
                Assert.Inconclusive("AppSessionIdentifier can be null");
            else
                Assert.Pass();
        }
        
        [Test, Order(1)]
        public void FrameworkName()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.FrameworkName);
            ChartboostCore.PublisherMetadata.SetFrameworkName(ConstFrameworkName);
            var frameworkName = ChartboostCore.AnalyticsEnvironment.FrameworkName;
            ChartboostCoreLogger.Log($"FrameworkName Analytics: {frameworkName}, Expected: {ConstFrameworkName}");
            Assert.IsNotNull(frameworkName);
            Assert.AreEqual(frameworkName, ConstFrameworkName);
            
            ChartboostCore.PublisherMetadata.SetFrameworkName(null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.FrameworkName);
        }

        [Test, Order(1)]
        public void FrameworkVersion()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.FrameworkVersion);
            ChartboostCore.PublisherMetadata.SetFrameworkVersion(ConstFrameworkVersion);
            var frameworkVersion = ChartboostCore.AnalyticsEnvironment.FrameworkVersion;
            ChartboostCoreLogger.Log($"FrameworkVersion Analytics: {frameworkVersion}, Expected: {ConstFrameworkVersion}");
            Assert.IsNotNull(frameworkVersion);
            Assert.AreEqual(frameworkVersion, ConstFrameworkVersion);
            
            ChartboostCore.PublisherMetadata.SetFrameworkVersion(null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.FrameworkVersion);
        }

        [Test, Order(1)]
        public void IsUserUnderage()
        {
            Assert.IsFalse(ChartboostCore.AnalyticsEnvironment.IsUserUnderage);
            ChartboostCore.PublisherMetadata.SetIsUserUnderage(ConstIsUserUnderage);
            var isUserUnderage = ChartboostCore.AnalyticsEnvironment.IsUserUnderage;
            ChartboostCoreLogger.Log($"IsUserUnderage Analytics: {isUserUnderage}, Expected: {ConstIsUserUnderage}");
            Assert.IsNotNull(isUserUnderage);
            Assert.AreEqual(isUserUnderage, ConstIsUserUnderage);
            
            ChartboostCore.PublisherMetadata.SetIsUserUnderage(false);
            Assert.IsFalse(ChartboostCore.AnalyticsEnvironment.IsUserUnderage);
        }

        [Test, Order(1)]
        public void PlayerIdentifier()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PlayerIdentifier);
            ChartboostCore.PublisherMetadata.SetPlayerIdentifier(ConstPlayerIdentifier);
            var playerIdentifier = ChartboostCore.AnalyticsEnvironment.PlayerIdentifier;
            ChartboostCoreLogger.Log($"PlayerIdentifier Analytics: {playerIdentifier}, Expected: {ConstPlayerIdentifier}");
            Assert.IsNotNull(playerIdentifier);
            Assert.AreEqual(playerIdentifier, ConstPlayerIdentifier);
            
            ChartboostCore.PublisherMetadata.SetPlayerIdentifier(null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PlayerIdentifier);
        }

        [Test, Order(1)]
        public void PublisherApplicationIdentifier()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PublisherAppIdentifier);
            ChartboostCore.PublisherMetadata.SetPublisherAppIdentifier(Application.identifier);
            var publisherAppIdentifier = ChartboostCore.AnalyticsEnvironment.PublisherAppIdentifier;
            ChartboostCoreLogger.Log($"PublisherAppIdentifier Analytics: {publisherAppIdentifier}, Expected: {Application.identifier}");
            Assert.IsNotNull(publisherAppIdentifier);
            Assert.AreEqual(publisherAppIdentifier, Application.identifier);
            
            ChartboostCore.PublisherMetadata.SetPublisherAppIdentifier(null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PublisherAppIdentifier);
        }

        [Test, Order(1)]
        public void PublisherSessionIdentifier()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PublisherSessionIdentifier);
            ChartboostCore.PublisherMetadata.SetPublisherSessionIdentifier(ConstPublisherSessionIdentifier);
            var publisherSessionIdentifier = ChartboostCore.AnalyticsEnvironment.PublisherSessionIdentifier;
            ChartboostCoreLogger.Log($"PublisherSessionIdentifier Analytics: {publisherSessionIdentifier}, Expected: {ConstPublisherSessionIdentifier}");
            Assert.IsNotNull(publisherSessionIdentifier);
            Assert.AreEqual(publisherSessionIdentifier, ConstPublisherSessionIdentifier);
            
            ChartboostCore.PublisherMetadata.SetPublisherSessionIdentifier(null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PublisherSessionIdentifier);
        }
    }
}
