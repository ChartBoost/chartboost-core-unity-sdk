using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chartboost.Core.Consent;
using Chartboost.Core.Environment;
using Chartboost.Core.Error;
using Chartboost.Core.Initialization;
using Chartboost.Core.Utilities;
using UnityEngine;

namespace Chartboost.Core.Default
{
    /// <summary>
    /// <inheritdoc cref="ChartboostCore"/>
    /// <br/>
    /// <para> Default class implementation for unsupported platforms.</para>
    /// </summary>
    public class ChartboostCoreDefault : ChartboostCore
    {
        protected override IConsentManagementPlatform _consent { get; } = new ConsentManagementPlatform();
        protected override IPublisherMetadata _publisherMetadata { get; } = new PublisherMetadata();
        protected override IAdvertisingEnvironment _advertisingEnvironment { get; } = new AdvertisingEnvironment();
        protected override IAnalyticsEnvironment _analyticsEnvironment { get; } = new AnalyticsEnvironment();
        protected override IAttributionEnvironment _attributionEnvironment { get; } = new AttributionEnvironment();
        protected override bool _debug { get; set; }
        protected override string _version => "0.3.0";
        protected override void _initialize(SDKConfiguration sdkConfiguration, IEnumerable<InitializableModule> modules)
        {
            foreach (var module in modules)
            {
                var start = DateTime.Now;
                if (module.NativeModule)
                {
                    const string message = "Default environment is unable to initialize native modules.";
                    ChartboostCoreLogger.LogWarning(message);
                    OnModuleInitializationCompleted(new ModuleInitializationResult(start, DateTime.Now, new ChartboostCoreError(-1, message), module));
                    continue;
                }

                MainThreadDispatcher.MainThreadTask(async () =>
                {
                    ChartboostCoreError? error = null;
                    try
                    {
                        error = await module.OnInitialize(new ModuleInitializationConfiguration(Application.identifier));
                    }
                    catch (Exception e)
                    {
                        ChartboostCoreLogger.LogException(e);
                        error = new ChartboostCoreError(-1, e.Message);
                    }
                    finally
                    {
                        OnModuleInitializationCompleted(new ModuleInitializationResult(start, DateTime.Now, error, module));
                    }
                });
            }
        }
    }
}
