using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Chartboost.Core.Environment;
using Chartboost.Core.Initialization;
using Chartboost.Core.Modules;
using Chartboost.Json;
using Chartboost.Logging;
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
        private static readonly Module SuccessfulModule = new TestUnityModuleSuccess();
        private static readonly Module FailureModule = new TestUnityModuleFailure();
        private const float ConstDelayAfterInit = 10f;
        
        private readonly List<Module> _modules = new()
        {
            SuccessfulModule,
            FailureModule
        };

        [SetUp]
        public void Setup()
        {
            LogController.LoggingLevel = LogLevel.Debug;
        }

        [Test, Order(0)]
        public void Version()
        {
            var nativeSDKVersion = ChartboostCore.NativeVersion;
            Assert.IsNotNull(nativeSDKVersion);
            Assert.IsNotEmpty(nativeSDKVersion);

            var unitySDKVersion = ChartboostCore.Version;
            
            LogController.Log($"NativeSDKVersion: {nativeSDKVersion}", LogLevel.Debug);
            LogController.Log($"UnitySDKVersion: {unitySDKVersion}", LogLevel.Debug);
            
            Assert.IsNotNull(unitySDKVersion);
            Assert.IsNotEmpty(unitySDKVersion);

            if (unitySDKVersion == nativeSDKVersion) 
                return;

            LogController.Log($"NativeSDKVersion does not match UnitySDKVersion, this is expected but not ideal.", LogLevel.Debug);
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
                LogController.Log($"--------", LogLevel.Debug);
                LogController.Log($"Start: {result.Start.ToString(dateFormat)}", LogLevel.Debug);
                LogController.Log($"End: {result.End.ToString(dateFormat)}", LogLevel.Debug);
                LogController.Log($"Duration: {result.Duration}", LogLevel.Debug);
                LogController.Log($"Module Id: {result.ModuleId}", LogLevel.Debug);
                LogController.Log($"Module Version: {result.ModuleVersion}", LogLevel.Debug);
                
                if (result.ModuleId == SuccessfulModule.ModuleId)
                {
                    Assert.AreEqual(result.ModuleId, SuccessfulModule.ModuleId);
                    Assert.AreEqual(result.ModuleVersion, SuccessfulModule.ModuleVersion);
                    Assert.IsNull(result.Error);
                }

                else if (result.ModuleId == FailureModule.ModuleId)
                {
                    Assert.AreEqual(result.ModuleId, FailureModule.ModuleId);
                    Assert.AreEqual(result.ModuleVersion, FailureModule.ModuleVersion);
                    Assert.IsNotNull(result.Error);
                    LogController.Log($"Exception: {JsonTools.SerializeObject(result.Error)}\n Default: {JsonTools.SerializeObject(TestUnityModuleFailure.Error)}", LogLevel.Debug);
                    Assert.AreEqual(result.Error, TestUnityModuleFailure.Error);
                }
                LogController.Log($"--------", LogLevel.Debug);
            }

            var sdkConfig = new SDKConfiguration(Application.identifier, _modules, new HashSet<string> { "chartboost_mediation"});
            ChartboostCore.Initialize(sdkConfig);
            yield return new WaitForSeconds(ConstDelayAfterInit);
            ChartboostCore.ModuleInitializationCompleted -= AssertModules;
        }

        [Test, Order(1)]
        public void LoggingLevel()
        {
            var initial = ChartboostCore.LogLevel;
            
            ChartboostCore.LogLevel = LogLevel.Disabled;
            Assert.AreEqual(LogLevel.Disabled, ChartboostCore.LogLevel);
            
            ChartboostCore.LogLevel = LogLevel.Error;
            Assert.AreEqual(LogLevel.Error, ChartboostCore.LogLevel);
            
            ChartboostCore.LogLevel = LogLevel.Warning;
            Assert.AreEqual(LogLevel.Warning, ChartboostCore.LogLevel);
            
            ChartboostCore.LogLevel = LogLevel.Info;
            Assert.AreEqual(LogLevel.Info, ChartboostCore.LogLevel);
    
            ChartboostCore.LogLevel = LogLevel.Info;
            Assert.AreEqual(LogLevel.Info, ChartboostCore.LogLevel);
            
            ChartboostCore.LogLevel = LogLevel.Debug;
            Assert.AreEqual(LogLevel.Debug, ChartboostCore.LogLevel);
            
            ChartboostCore.LogLevel = LogLevel.Verbose;
            Assert.AreEqual(LogLevel.Verbose, ChartboostCore.LogLevel);
            
            ChartboostCore.LogLevel = initial;
            Assert.AreEqual(initial, ChartboostCore.LogLevel);
        }

        [UnityTest, Order(1)]
        public IEnumerator AdvertisingIdentifier()
        {
            var attributionTask = ChartboostCore.AttributionEnvironment.AdvertisingIdentifier;
            yield return new WaitUntil(() => attributionTask.IsCompleted);
            var advertisingTask =  ChartboostCore.AdvertisingEnvironment.AdvertisingIdentifier;
            yield return new WaitUntil(() => advertisingTask.IsCompleted);
            var analyticsTask = ChartboostCore.AnalyticsEnvironment.AdvertisingIdentifier;
            yield return new WaitUntil(() => analyticsTask.IsCompleted);

            var advertisingResult = advertisingTask.Result;
            var analyticsResult = analyticsTask.Result;
            var attributionResult = attributionTask.Result;
            
            LogController.Log($"AdvertisingIdentifier Advertising: {advertisingResult},", LogLevel.Debug);
            LogController.Log($"AdvertisingIdentifier Analytics: {analyticsResult}", LogLevel.Debug);
            LogController.Log($"AdvertisingIdentifier Attribution: {attributionResult}", LogLevel.Debug);
            
            Assert.AreEqual(attributionResult, advertisingResult);
            Assert.AreEqual(advertisingResult, analyticsResult);

            if (string.IsNullOrEmpty(analyticsResult)) 
                Assert.Pass("AdvertisingIdentifier can be null");
        }

        [UnityTest, Order(1)]
        public IEnumerator UserAgent()
        {
            var attributionTask = ChartboostCore.AttributionEnvironment.UserAgent;
            yield return new WaitUntil(() => attributionTask.IsCompleted);
            var analyticsTask = ChartboostCore.AnalyticsEnvironment.UserAgent;
            yield return new WaitUntil(() => analyticsTask.IsCompleted);

            var attributionResult = attributionTask.Result;
            var analyticsResult = analyticsTask.Result;
            
            LogController.Log($"UserAgent Attribution: {attributionResult}", LogLevel.Debug);
            LogController.Log($"UserAgent Analytics: {analyticsResult}", LogLevel.Debug);
            
            Assert.AreEqual(attributionResult, analyticsResult);
            
            if (string.IsNullOrEmpty(analyticsResult))
                Assert.Pass("UserAgent can be null");
        }
        
        [Test, Order(1)]
        public void OSName()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.OSName;
            var analytics = ChartboostCore.AnalyticsEnvironment.OSName;
            LogController.Log($"OSName Advertising: {advertising}", LogLevel.Debug);
            LogController.Log($"OSName Analytics: {analytics}", LogLevel.Debug);
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
            LogController.Log($"OSVersion Advertising: {advertising}", LogLevel.Debug);
            LogController.Log($"OSVersion Analytics: {analytics}", LogLevel.Debug);
            Assert.AreEqual(advertising, analytics);
            Assert.IsNotNull(analytics);
            Assert.IsNotEmpty(analytics);
        }
        
        [Test, Order(1)]
        public void DeviceMake()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.DeviceMake;
            var analytics = ChartboostCore.AnalyticsEnvironment.DeviceMake;
            LogController.Log($"DeviceMake Advertising: {advertising}", LogLevel.Debug);
            LogController.Log($"DeviceMake Analytics: {analytics}", LogLevel.Debug);
            Assert.AreEqual(advertising, analytics);
            Assert.IsNotNull(analytics);
            Assert.IsNotEmpty(analytics);
        }
        
        [Test, Order(1)]
        public void DeviceModel()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.DeviceModel;
            var analytics = ChartboostCore.AnalyticsEnvironment.DeviceModel;
            LogController.Log($"DeviceModel Advertising: {advertising}", LogLevel.Debug);
            LogController.Log($"DeviceModel Analytics: {analytics}", LogLevel.Debug);
            Assert.AreEqual(advertising, analytics);
            Assert.IsNotNull(analytics);
            Assert.IsNotEmpty(analytics);
        }
        
        [Test, Order(1)]
        public void DeviceLocale()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.DeviceLocale;
            var analytics = ChartboostCore.AnalyticsEnvironment.DeviceLocale;
            LogController.Log($"DeviceLocale Advertising: {advertising}", LogLevel.Debug);
            LogController.Log($"DeviceLocale Analytics: {analytics}", LogLevel.Debug);
            Assert.AreEqual(advertising, analytics);
            Assert.IsNotNull(analytics);
            Assert.IsNotEmpty(analytics);
        }
        
        [Test, Order(1)]
        public void ScreenHeight()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.ScreenHeightPixels;
            var analytics = ChartboostCore.AnalyticsEnvironment.ScreenHeightPixels;
            LogController.Log($"ScreenWidth Advertising: {advertising}", LogLevel.Debug);
            LogController.Log($"ScreenWidth Analytics: {analytics}", LogLevel.Debug);
            LogController.Log($"ScreenWidth Unity: {Screen.height}", LogLevel.Debug);
            Assert.AreEqual(advertising, analytics);
            Assert.GreaterOrEqual(Screen.height, analytics);
            Assert.IsNotNull(analytics);
        }

        [Test, Order(1)]
        public void ScreenWidth()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.ScreenWidthPixels;
            var analytics = ChartboostCore.AnalyticsEnvironment.ScreenWidthPixels;
            LogController.Log($"ScreenWidth Advertising: {advertising}", LogLevel.Debug);
            LogController.Log($"ScreenWidth Analytics: {analytics}", LogLevel.Debug);
            Assert.AreEqual(advertising, analytics);
            Assert.AreEqual(analytics, Screen.width);
            Assert.IsNotNull(analytics);
        }
        
        [Test, Order(1)]
        public void ScreenScale()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.ScreenScale;
            var analytics = ChartboostCore.AnalyticsEnvironment.ScreenScale;
            LogController.Log($"ScreenScale Advertising: {advertising}", LogLevel.Debug);
            LogController.Log($"ScreenScale Analytics: {analytics}", LogLevel.Debug);
            LogController.Log($"ScreenScale Unity: {Screen.dpi}", LogLevel.Debug);
            Assert.AreEqual(advertising, analytics);
            Assert.Greater(ChartboostCore.AdvertisingEnvironment.ScreenScale, 0);
            Assert.IsNotNull(analytics);
        }
        
        [Test, Order(1)]
        public void BundleIdentifier()
        {
            var advertising = ChartboostCore.AdvertisingEnvironment.BundleIdentifier;
            var analytics = ChartboostCore.AnalyticsEnvironment.BundleIdentifier;
            LogController.Log($"BundleIdentifier Advertising: {advertising}", LogLevel.Debug);
            LogController.Log($"BundleIdentifier Analytics: {analytics}", LogLevel.Debug);
            LogController.Log($"BundleIdentifier Unity: {Application.identifier}", LogLevel.Debug);
            Assert.AreEqual(advertising, analytics);
            Assert.AreEqual(analytics, Application.identifier);
            Assert.IsNotNull(analytics);
        }

        [UnityTest, Order(1)]
        public IEnumerator LimitAdTrackingEnabled()
        {
            var advertisingTask = ChartboostCore.AdvertisingEnvironment.LimitAdTrackingEnabled;
            yield return new WaitUntil(() => advertisingTask.IsCompleted);
            var analyticsTask = ChartboostCore.AnalyticsEnvironment.LimitAdTrackingEnabled;
            yield return new WaitUntil(() => analyticsTask.IsCompleted);

            var advertisingResult = advertisingTask.Result;
            var analyticsResult = analyticsTask.Result;
            
            LogController.Log($"LimitAdTrackingEnabled Advertising: {advertisingResult}", LogLevel.Debug);
            LogController.Log($"LimitAdTrackingEnabled Analytics: {analyticsResult}", LogLevel.Debug);
            Assert.AreEqual(advertisingResult, analyticsResult);
        }
        
        [Test, Order(1)]
        public void NetworkConnectionType()
        {
            var networkConnectionType = ChartboostCore.AnalyticsEnvironment.NetworkConnectionType;
            LogController.Log($"NetworkConnectionType Analytics: {networkConnectionType}", LogLevel.Debug);
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
                Assert.AreEqual(networkConnectionType, Environment.NetworkConnectionType.Unknown);
            Assert.IsNotNull(networkConnectionType);
        }

        [UnityTest, Order(1)]
        public IEnumerator VendorIdentifierScope()
        {
            var vendorIdentifierScopeTask = ChartboostCore.AnalyticsEnvironment.VendorIdentifierScope;
            yield return new WaitUntil(() => vendorIdentifierScopeTask.IsCompleted);
            var vendorIdentifierScope = vendorIdentifierScopeTask.Result;
            LogController.Log($"VendorIdentifierScope Analytics: {vendorIdentifierScope}", LogLevel.Debug);
            Assert.IsNotNull(vendorIdentifierScope);
        }

        [Test, Order(1)]
        public void Volume()
        {
            var volume = ChartboostCore.AnalyticsEnvironment.Volume;
            LogController.Log($"Volume Analytics: {volume}", LogLevel.Debug);
            if (volume == null)
                Assert.Pass("Volume can be null");
            Assert.GreaterOrEqual(volume, 0);
            Assert.IsNotNull(volume);
        }
        
        [UnityTest, Order(1)]
        public IEnumerator VendorIdentifier()
        {
            var vendorIdentifierTask =  ChartboostCore.AnalyticsEnvironment.VendorIdentifier;
            yield return new WaitUntil(() => vendorIdentifierTask.IsCompleted);

            var vendorIdentifier = vendorIdentifierTask.Result;
            LogController.Log($"VendorIdentifier Analytics: {vendorIdentifier}", LogLevel.Debug);

            if (string.IsNullOrEmpty(vendorIdentifier)) 
                Assert.Pass("VendorIdentifier can be null");
        }
        
        [Test, Order(1)]
        public void AppTrackingTransparencyStatus()
        {
            var appTrackingTransparencyStatus = ChartboostCore.AnalyticsEnvironment.AppTrackingTransparencyStatus;
            LogController.Log($"AppTrackingTransparencyStatus Analytics: {appTrackingTransparencyStatus}", LogLevel.Debug);
            
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
            LogController.Log($"AppVersion Analytics: {appVersion}", LogLevel.Debug);
            LogController.Log($"AppVersion Unity: {Application.version}", LogLevel.Debug);
            Assert.AreEqual(appVersion, Application.version);
            Assert.IsNotNull(appVersion);
            Assert.IsNotEmpty(appVersion);
        }
        
        [Test, Order(1)]
        public void AppSessionDuration()
        {
            var appSessionDuration = ChartboostCore.AnalyticsEnvironment.AppSessionDuration;
            LogController.Log($"AppSessionDuration Analytics: {appSessionDuration}", LogLevel.Debug);
            Assert.IsNotNull(appSessionDuration);
            
            if (appSessionDuration <= 0)
                Assert.Pass("AppSessionDuration can be 0, it only increases after initialization");
        }
        
        [Test, Order(1)]
        public void AppSessionIdentifier()
        {
            var appSessionIdentifier = ChartboostCore.AnalyticsEnvironment.AppSessionIdentifier;
            LogController.Log($"AppSessionIdentifier Analytics: {appSessionIdentifier}", LogLevel.Debug);
            
            if (string.IsNullOrEmpty(appSessionIdentifier))
                Assert.Pass("AppSessionIdentifier can be null");
        }
        
        [UnityTest, Order(1)]
        public IEnumerator FrameworkName()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.FrameworkName);
            var fired = false;
            ChartboostCore.AnalyticsEnvironment.FrameworkNameChanged += WaitForChange;
            void WaitForChange()
            {
                ChartboostCore.AnalyticsEnvironment.FrameworkNameChanged -= WaitForChange;
                fired = true;
                LogController.Log("FrameworkName Changed", LogLevel.Debug);
            }
            
            ChartboostCore.PublisherMetadata.SetFramework(ConstFrameworkName, null);
            yield return new WaitUntil(() => fired);

            var frameworkName = ChartboostCore.AnalyticsEnvironment.FrameworkName;
            LogController.Log($"FrameworkName Analytics: {frameworkName}, Expected: {ConstFrameworkName}", LogLevel.Debug);
            Assert.IsNotNull(frameworkName);
            Assert.AreEqual(frameworkName, ConstFrameworkName);
            
            ChartboostCore.PublisherMetadata.SetFramework(null, null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.FrameworkName);
        }

        [UnityTest, Order(1)]
        public IEnumerator FrameworkVersion()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.FrameworkVersion);
            var fired = false;
            ChartboostCore.AnalyticsEnvironment.FrameworkVersionChanged += WaitForChange;
            void WaitForChange()
            {
                ChartboostCore.AnalyticsEnvironment.FrameworkVersionChanged -= WaitForChange;
                fired = true;
                LogController.Log("FrameworkVersion Changed", LogLevel.Debug);
            }
            
            ChartboostCore.PublisherMetadata.SetFramework(null, ConstFrameworkVersion);
            yield return new WaitUntil(() => fired);
            
            var frameworkVersion = ChartboostCore.AnalyticsEnvironment.FrameworkVersion;
            LogController.Log($"FrameworkVersion Analytics: {frameworkVersion}, Expected: {ConstFrameworkVersion}", LogLevel.Debug);
            Assert.IsNotNull(frameworkVersion);
            Assert.AreEqual(frameworkVersion, ConstFrameworkVersion);
            
            ChartboostCore.PublisherMetadata.SetFramework(null, null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.FrameworkVersion);
        }

        [UnityTest, Order(1)]
        public IEnumerator IsUserUnderage()
        {
            Assert.IsFalse(ChartboostCore.AnalyticsEnvironment.IsUserUnderage);
            var fired = false;
            ChartboostCore.AnalyticsEnvironment.IsUserUnderageChanged += WaitForChange;
            void WaitForChange()
            {
                ChartboostCore.AnalyticsEnvironment.IsUserUnderageChanged -= WaitForChange;
                fired = true;
                LogController.Log("IsUserUnderage Changed", LogLevel.Debug);
            }
            
            ChartboostCore.PublisherMetadata.SetIsUserUnderage(ConstIsUserUnderage);
            yield return new WaitUntil(() => fired);
            
            var isUserUnderage = ChartboostCore.AnalyticsEnvironment.IsUserUnderage;
            LogController.Log($"IsUserUnderage Analytics: {isUserUnderage}, Expected: {ConstIsUserUnderage}", LogLevel.Debug);
            Assert.IsNotNull(isUserUnderage);
            Assert.AreEqual(isUserUnderage, ConstIsUserUnderage);
            
            ChartboostCore.PublisherMetadata.SetIsUserUnderage(false);
            Assert.IsFalse(ChartboostCore.AnalyticsEnvironment.IsUserUnderage);
        }

        [UnityTest, Order(1)]
        public IEnumerator PlayerIdentifier()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PlayerIdentifier);
            var fired = false;
            ChartboostCore.AnalyticsEnvironment.PlayerIdentifierChanged += WaitForChange;
            void WaitForChange()
            {
                ChartboostCore.AnalyticsEnvironment.PlayerIdentifierChanged -= WaitForChange;
                fired = true;
                LogController.Log("PlayerIdentifier Changed", LogLevel.Debug);
            }
            
            ChartboostCore.PublisherMetadata.SetPlayerIdentifier(ConstPlayerIdentifier);
            yield return new WaitUntil(() => fired);
            
            var playerIdentifier = ChartboostCore.AnalyticsEnvironment.PlayerIdentifier;
            LogController.Log($"PlayerIdentifier Analytics: {playerIdentifier}, Expected: {ConstPlayerIdentifier}", LogLevel.Debug);
            Assert.IsNotNull(playerIdentifier);
            Assert.AreEqual(playerIdentifier, ConstPlayerIdentifier);
            
            ChartboostCore.PublisherMetadata.SetPlayerIdentifier(null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PlayerIdentifier);
        }

        [UnityTest, Order(1)]
        public IEnumerator PublisherApplicationIdentifier()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PublisherAppIdentifier);
            var fired = false;
            ChartboostCore.AnalyticsEnvironment.PublisherAppIdentifierChanged += WaitForChange;
            void WaitForChange()
            {
                ChartboostCore.AnalyticsEnvironment.PublisherAppIdentifierChanged -= WaitForChange;
                fired = true;
                LogController.Log("PublisherAppIdentifier Changed", LogLevel.Debug);
            }
            
            ChartboostCore.PublisherMetadata.SetPublisherAppIdentifier(Application.identifier);
            yield return new WaitUntil(() => fired);
            
            var publisherAppIdentifier = ChartboostCore.AnalyticsEnvironment.PublisherAppIdentifier;
            LogController.Log($"PublisherAppIdentifier Analytics: {publisherAppIdentifier}, Expected: {Application.identifier}", LogLevel.Debug);
            Assert.IsNotNull(publisherAppIdentifier);
            Assert.AreEqual(publisherAppIdentifier, Application.identifier);
            
            ChartboostCore.PublisherMetadata.SetPublisherAppIdentifier(null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PublisherAppIdentifier);
        }

        [UnityTest, Order(1)]
        public IEnumerator PublisherSessionIdentifier()
        {
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PublisherSessionIdentifier);
            var fired = false;
            ChartboostCore.AnalyticsEnvironment.PublisherSessionIdentifierChanged += WaitForChange;
            void WaitForChange()
            {
                ChartboostCore.AnalyticsEnvironment.PublisherSessionIdentifierChanged -= WaitForChange;
                fired = true;
                LogController.Log("PublisherSessionIdentifier Changed", LogLevel.Debug);
            }
            
            ChartboostCore.PublisherMetadata.SetPublisherSessionIdentifier(ConstPublisherSessionIdentifier);
            yield return new WaitUntil(() => fired);
            
            var publisherSessionIdentifier = ChartboostCore.AnalyticsEnvironment.PublisherSessionIdentifier;
            LogController.Log($"PublisherSessionIdentifier Analytics: {publisherSessionIdentifier}, Expected: {ConstPublisherSessionIdentifier}", LogLevel.Debug);
            Assert.IsNotNull(publisherSessionIdentifier);
            Assert.AreEqual(publisherSessionIdentifier, ConstPublisherSessionIdentifier);
            
            ChartboostCore.PublisherMetadata.SetPublisherSessionIdentifier(null);
            Assert.IsNull(ChartboostCore.AnalyticsEnvironment.PublisherSessionIdentifier);
        }
    }
}
