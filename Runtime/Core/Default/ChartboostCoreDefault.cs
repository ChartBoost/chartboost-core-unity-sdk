using System;
using System.Linq;
using Chartboost.Core.Consent;
using Chartboost.Core.Environment;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Logging;
using UnityEngine;

namespace Chartboost.Core.Default
{
    /// <summary>
    /// <inheritdoc cref="ChartboostCore"/>
    /// <br/>
    /// <para> Default class implementation for unsupported platforms.</para>
    /// </summary>
    internal class ChartboostCoreDefault : ChartboostCoreBase
    {
        /// <inheritdoc cref="ChartboostCore.NativeVersion"/>
        public override string NativeVersion => ChartboostCore.Version;
        
        /// <inheritdoc cref="ChartboostCore.LogLevel"/>
        public override LogLevel LogLevel { get; set; }
        
        /// <inheritdoc cref="ChartboostCore.Consent"/>
        public override IConsentManagementPlatform Consent { get; } = new ConsentManagementPlatform();
        
        /// <inheritdoc cref="ChartboostCore.PublisherMetadata"/>
        public override IPublisherMetadata PublisherMetadata { get; } = new PublisherMetadata();
        
        /// <inheritdoc cref="ChartboostCore.AdvertisingEnvironment"/>
        public override IAdvertisingEnvironment AdvertisingEnvironment { get; } = new AdvertisingEnvironment();
        
        /// <inheritdoc cref="ChartboostCore.AnalyticsEnvironment"/>
        public override IAnalyticsEnvironment AnalyticsEnvironment { get; } = new AnalyticsEnvironment();
        
        /// <inheritdoc cref="ChartboostCore.AttributionEnvironment"/>
        public override IAttributionEnvironment AttributionEnvironment { get; } = new AttributionEnvironment();
        
        /// <inheritdoc cref="ChartboostCore.Initialize"/>
        public override void Initialize(SDKConfiguration sdkConfiguration)
        {
            foreach (var module in sdkConfiguration.Modules)
            {
                if(sdkConfiguration.SkippedModuleIdentifiers.Contains(module.ModuleId))
                {
                    LogController.Log($"Skipping {module.ModuleId} from Initialization as it was added to Skipped Module Identifiers", LogLevel.Debug);
                    continue;
                }
                
                var start = DateTime.Now;
                if (module.NativeModule)
                {
                    const string message = "Default environment is unable to initialize native modules.";
                    LogController.Log(message, LogLevel.Warning);
                    ChartboostCore.OnModuleInitializationCompleted(new ModuleInitializationResult(start, DateTime.Now, module.ModuleId, module.ModuleVersion, new ChartboostCoreError(-1, message)));
                    continue;
                }

                MainThreadDispatcher.MainThreadTask(async () =>
                {
                    ChartboostCoreError? error = null;
                    try
                    {
                        error = await module.OnInitialize(new ModuleConfiguration(Application.identifier));
                    }
                    catch (Exception exception)
                    {
                        LogController.LogException(exception);
                        error = new ChartboostCoreError(-1, exception.Message);
                    }
                    finally
                    {
                        ChartboostCore.OnModuleInitializationCompleted(new ModuleInitializationResult(start, DateTime.Now, module.ModuleId, module.ModuleVersion, error));
                    }
                });
            }
        }
    }
}
