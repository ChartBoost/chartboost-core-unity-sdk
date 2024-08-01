# Chartboost Core Unity SDK

## Summary 

ChartboostCore SDK is a modular Unity SDK designed as an entry point to manage and facilitate different modules for your Android/iOS application. Each module can be individually initialized and has its metrics collected and reported, offering detailed insights into module performance and potential issues.

The main functionalities provided by the SDK are:

* Initialization of individual or a set of modules.
* Performance metrics collection during the module initialization process.
* Detailed error tracking and reporting with categorized error codes.
* Centralized logging system with multiple log levels and output options.

## Minimum Requirements

| Plugin | Version |
| ------ | ------ |
| Cocoapods | 1.11.3+ |
| iOS | 11.0+ |
| Xcode | 14.1+ |
| Android API | 21+ |
| Unity | 2022.3.+ |

# Integration

## Using the public [npm registry](https://www.npmjs.com/search?q=com.chartboost.core)

In order to add the Chartboost Core Unity SDK to your project using the npm package, add the following to your Unity Project's ***manifest.json*** file. The scoped registry section is required in order to fetch packages from the NpmJS registry.

```json
"dependencies": {
    "com.chartboost.core": "1.0.0",
    ...
},
"scopedRegistries": [
{
    "name": "NpmJS",
    "url": "https://registry.npmjs.org",
    "scopes": [
    "com.chartboost"
    ]
}
]
```
## Using the public [NuGet package](https://www.nuget.org/packages/Chartboost.CSharp.Core.Unity)

To add the Chartboost Core Unity SDK to your project using the NuGet package, you will first need to add the [NugetForUnity](https://github.com/GlitchEnzo/NuGetForUnity) package into your Unity Project.

This can be done by adding the following to your Unity Project's ***manifest.json***

```json
  "dependencies": {
    "com.github-glitchenzo.nugetforunity": "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity",
    ...
  },
```

Once <code>NugetForUnity</code> is installed, search for `Chartboost.CSharp.Core.Unity` in the search bar of Nuget Explorer window(Nuget -> Manage Nuget Packages).
You should be able to see the `Chartboost.CSharp.Core.Unity` package. Choose the appropriate version and install.

# Usage

## Creating Modules

Chartboost provides modules to be integrated with Chartboost Core, such as: [Chartboost Mediation](https://github.com/ChartBoost/chartboost-mediation-unity-sdk), [Usercentrics](https://github.com/ChartBoost/chartboost-core-unity-consent-adapter-usercentrics), etc. However, it also provides the option for developer created modules. To create a module, create a class that inherits the `Module` class, as seen in the following example:

```csharp
public class TestModule : Module
{
    public override string ModuleId => "test_module";
    public override string ModuleVersion => "1.0.0";

    private string _configKey = "default_config_key";
    
    // The designated initializer for the module. Sets up the module to make it ready to be used.
    protected override async Task<ChartboostCoreError?> Initialize(ModuleConfiguration configuration)
    {
        // Perform init operation. Async initialization supported. (Sample code, replace as needed)
        var result = await SomeSDK.Initialize(_configKey);

        if (result.Error.HasValue)
        {
          // assemble error in a Chartboost Core acceptable format.
          var error = new ChartboostCoreError(result.Error?.Core, result.Error?.Message);

          // failed initialization.
          return await Task.FromResult<ChartboostCoreError?>(error)
        }
        
        // success
        return null;
    }

    /// <summary>
    /// Updates the Module with JSON data from the server. A publisher is recommended to
    /// initialize via the constructor with module-specific parameters rather than using this function.
    /// When creating a module, please make sure it's possible to send a credentials
    /// object to set up the parameters of this module.
    ///
    /// Note: Modules should not perform costly operations on this initializer.
    /// ChartboostCore SDK may instantiate and discard several instances of the same module.
    /// ChartboostCore SDK keeps strong references to modules that are successfully initialized.
    /// </summary>
    /// <param name="credentials">A credentials object containing all the information required to initialize
    /// this module, as defined on the Chartboost Core's dashboard.</param>
    protected override void UpdateCredentials(IReadOnlyDictionary<string, object> credentials)
    {
        base.UpdateCredentials(credentials);
        
       if (credentials.TryGetValue("config_key", out var key))
       {
          // update values as neeeded utilizing the config field. This allows for remote module initialization.
          _configKey = key;
       }
    }
}
```

## Initializing Chartboost Core & Chartboost Core Modules

In order to initialize Chartboost Core and Chartboost Code modules, you must call `ChartboostCore.Initialize`. Initialization can be configured as needed by utilizing the `SDKConfiguration` class, see example below:

```csharp
const string chartboostApplicationIdentifier = "CHARTBOOST_APPLICATION_IDENTIFIER";

// List of all client-modules we wish to initialize. Note that any remote modules will always be attempted to be initialized.
List<Modules> modulesToInitialize = new List<Modules>();

var testModule = new TestModule();

// When having a explicit reference to a module, we can get notified of its readiness by using the C# specific ModuleReady event.
testMOdule.ModuleReady += module => Debug.Log($"Module: {module.ModuleId}/{module.ModuleVersion} is ready!");

modulesToInitialize.Add(testModule);

// HashSet of modules we wish to skip initialization. This could be modules that are already initialize, or that we do not wish to init for specific clients.
var modulesToSkipInitialization = new HashSet<String>();

// Using Chartboost Mediation as a remote module example. There isn't an explicit Module object for Chartboost Mediation.
modulesToSkipInitialization.Add(ChartboostMediation.CoreModuleId);

var sdkConfig = new SDKConfiguration(chartboostApplicationIdentifier, modulesToInitialize, modulesToSkipInitialization);

// Initialize Chartboost Core utilize the SDKConfiguration object.
ChartboostCore.Initialize(sdkConfig);
```

## Publisher Metadata

Chartboost Core allows developer to set Publisher provided metadata, as seen in the following examples.

### IsUserUnderage
Indicates if the user is underage as determined by the publisher.

```csharp
// Is underage
ChartboostCore.PublisherMedata.SetIsUserUnderage(true);

// Is not underage
ChartboostCore.PublisherMedata.SetIsUserUnderage(false);
```

### PublisherSessionIdentifier
Sets a publisher-defined session identifier.

```csharp
ChartboostCore.PublisherMedata.SetPublisherSessionIdentifier("PUBLISHER_DEFINED_SESSION_IDENTIFIER");
```

### PublisherAppIdentifier
Sets a publisher-defined application identifier.

```csharp
ChartboostCore.PublisherMedata.SetPublisherAppIdentifier("PUBLISHER_DEFINED_APP_IDENTIFIER");
```

### Framework
Sets the framework name and version.

```csharp
ChartboostCore.PublisherMetadata.SetFramework("Unity", Application.unityVersion);
```

### PlayerIdentifier
Sets a publisher-defined player identifier.

```csharp
ChartboostCore.PublisherMedata.SetPlayerIdentifier("PLAYER_IDENTIFIER");
```

## Environments

### Attribution
An environment that contains information intended solely for attribution purposes.

```csharp
// The system advertising identifier.
Task<string?> advertisingIdentifier = await ChartboostCore.AttributionEnvironment.AdvertisingIdentifier;

// The device user agent.
Task<string?> userAgent = await ChartboostCore.AttributionEnvironment.AdvertisingIdentifier;
```

### Advertising
An environment that contains information intended solely for advertising purposes.

```csharp

// The OS name, e.g. “iOS”, "Android", etc.
string osName = ChartboostCore.AttributionEnvironment.OSName;

// The OS version, e.g. “17.0”.
string osVersion = ChartboostCore.AttributionEnvironment.OSVersion;

// The device make, e.g. “Apple”.
var deviceMake = ChartboostCore.AttributionEnvironment.DeviceMake;
string
// The device model, e.g. “iPhone11,2”.
string deviceModule = ChartboostCore.AttributionEnvironment.DeviceModel;

// The device locale string, e.g. “en-US”.
string? deviceLocale = ChartboostCore.AttributionEnvironment.DeviceLocale;

// The height of the screen in pixels.
double? screenHeightPixels = ChartboostCore.AttributionEnvironment.ScreenHeightPixels;

// The screen scale.
double? screenScale = ChartboostCore.AttributionEnvironment.ScreenScale;

// The width of the screen in pixels.
double? screenWidthPixels = ChartboostCore.AttributionEnvironment.ScreenWidthPixels;

// The app bundle identifier.
string? bundleIdentifier = ChartboostCore.AttributionEnvironment.BundleIdentifier;

// Indicates whether the user has limited ad tracking enabled.
Task<bool?> limitedAdtrackingEnabled = await ChartboostCore.AttributionEnvironment.LimitAdTrackingEnabled;

// The system advertising identifier.
Task<string?>  advertisingIdentifier = await ChartboostCore.AttributionEnvironment.AdvertisingIdentifier;
```
### Analytics
An environment that contains information intended solely for analytics purposes. The Analytics environment contains the same fields as the `IAttributionEnvironment` and `IAdvertisingEnvironment` plus the following:

```csharp
// The current network connection type, e.g. wifi.
NetworkConnectionType networkConnectionType = ChartboostCore.AnalyticsEnvironment.NetworkConnectionType;

// The device volume level.
double? volume = ChartboostCore.AnalyticsEnvironment.Volume; 

// The system identifier for vendor (IFV).
Task<string?> vendorIdentifier = await ChartboostCore.AnalyticsEnvironment.VendorIdentifier; 

// The scope of the identifier for vendor.
Task<VendorIdentifierScope> vendorIdentifierScope = await ChartboostCore.AnalyticsEnvironment.VendorIdentifierScope; 

// The tracking authorization status, as determined by the system’s AppTrackingTransparency framework. Requires iOS 14.0+. Only supported in iOS devices, other platforms default to Unsupported.
AuthorizationStatus appTrackingTransparencyStatus = ChartboostCore.AnalyticsEnvironment.AppTrackingTransparencyStatus; 

// The version of the app.
string? appTrackingTransparencyStatus = ChartboostCore.AnalyticsEnvironment.AppVersion; 

// The session duration, or 0 if the ChartboostCore.Initialize method has not been called yet. A session starts the moment ChartboostCore.Initialize is called for the first time.
double appSessionDuration = ChartboostCore.AnalyticsEnvironment.AppSessionDuration; 

// The session identifier, or null if the ChartboostCore.Initialize method has not been called yet. A session starts the moment ChartboostCore.Initialize is called for the first time.
string? appSessionIdentifier = ChartboostCore.AnalyticsEnvironment.AppSessionIdentifier; 

// Indicates whether the user is underage. This is determined by the latest value set by the publisher through a call to IPublisherMetadata.SetIsUserUnderage, as well as by the “child-directed” option defined on the Chartboost Core dashboard.
bool? isUserUnderage = ChartboostCore.AnalyticsEnvironment.IsUserUnderage;

// The publisher-defined session identifier set by the publisher through a call to IPublisherMetadata.SetPublisherSessionIdentifier.
string? publisherSessionIdentifier = ChartboostCore.AnalyticsEnvironment.PublisherSessionIdentifier;

// The publisher-defined app identifier set by the publisher through a call to IPublisherMetadata.SetPublisherAppIdentifier.
string? publisherAppIdentifier = ChartboostCore.AnalyticsEnvironment.PublisherAppIdentifier;

// The framework name set by the publisher through a call to IPublisherMetadata.SetFramework.
string? frameworkName = ChartboostCore.AnalyticsEnvironment.FrameworkName;

// The framework version set by the publisher through a call to IPublisherMetadata.SetFramework.
string? frameworkVersion = ChartboostCore.AnalyticsEnvironment.FrameworkVersion;

// The player identifier set by the publisher through a call to <see cref="IPublisherMetadata.SetPlayerIdentifier"/>.
string? frameworkVersion = ChartboostCore.AnalyticsEnvironment.PlayerIdentifier;
```

Additionally, developers can subscribe to `IPublisherMetadata` values change through the `CharboostCore.AnalyticsEnvironment` events, as seen below:

```csharp
// Using FrameworkName as an example
ChartboostCore.AnalyticsEnvironment.FrameworkNameChanged += () => Debug.Log($"FrameworkNameChanged, with value: {ChartboostCore.AnalyticsEnvironment.FrameworkName}");
```

## Consent

Chartboost Core defines a unified API for publishers to request and query user consent, and relies on a 3rd-party CMP SDK to provide the CMP functionality.

In order to utilize Chartboost Core Unified API, make sure a consent module such as: [Usercentrics](https://github.com/ChartBoost/chartboost-core-unity-consent-adapter-usercentrics) is initialized as a module. See example below:

```csharp
string chartboostApplicationIdentifier = "CHARTBOOST_APPLICATION_IDENTIFIER";

List<Module> modulesToInitialize = new List<Module>();

// create usercentrics options configuration object
UsercentricsOptions usercentricsOptions = new UsercentricsOptions("USERCENTICS_SETTINGS_ID");

// template to partner id can be passed as an optional paramter, but a default set is provided.
UsercentricsAdapter usercentricsAdapter = new UsercentricsAdapter(usercentricsOptions);

SDKConfiguration sdkConfig = new SDKConfiguration(chartboostApplicationIdentifier, modulesToInitialize);

// Initialize Chartboost Core and Usercentrics.
ChartboostCore.Initialize(sdkConfig);
```

### Consents
Detailed consent status for each consent standard, as determined by the CMP.

```csharp
foreach (var consent in ChartboostCore.Consent.Consents) 
  Debug.Log($"ConsentKey {consent.Key} with ConsentValue {consent.Value}");
```

### ShouldCollectConsent
Indicates whether the CMP has determined that consent should be collected from the user.

```csharp
//  Returns false if no consent adapter module is available.
var shouldCollectConsent = ChartboostCore.Consent.ShouldCollectConsent;
```

### GrantConsent
Informs the CMP that the user has granted consent. This method should be used only when a custom consent dialog is presented to the user, thereby making the publisher responsible for the UI-side of collecting consent. In most cases `IConsentManagementPlatform.ShowConsentDialog` should be used instead.

```csharp
// The consent was collected from the user as a result of an explicit user action.
Task<bool> grantConsentUser = await ChartboostCore.Consent.GrantConsent(ConsentSource.User);

//  The consent was set by the developer without an explicit user action
Task<bool> grantConsentDeveloper = await ChartboostCore.Consent.GrantConsent(ConsentSource.Developer);
```

### DenyConsent
Informs the CMP that the user has denied consent. This method should be used only when a custom consent dialog is presented to the user, thereby making the publisher responsible for the UI-side of collecting consent. In most cases `IConsentManagementPlatform.ShowConsentDialog` should be used instead.

```csharp
// The consent was collected from the user as a result of an explicit user action.
Task<bool> denyConsentUser = await ChartboostCore.Consent.DenyConsent(ConsentSource.User);

//  The consent was set by the developer without an explicit user action
Task<bool> denyConsentDeveloper = await ChartboostCore.Consent.DenyConsent(ConsentSource.Developer);
```

### ResetConsent
Informs the CMP that the given consent should be reset. If the CMP does not support the ResetConsent() function or the operation fails for any other reason, the Task is executed with a false parameter.

```csharp
Task<bool> denyConsentUser = await ChartboostCore.Consent.ResetConsent();
```

### ShowConsentDialog
Instructs the CMP to present a consent dialog to the user for the purpose of collecting consent.

```csharp
// A non-intrusive dialog used to collect consent, presenting a minimum amount of information.
Task<bool> showConsentConcise = await ChartboostCore.Consent.ShowConsentDialog(ConsentDialogType.Concise);

// A dialog used to collect consent, presenting detailed information and possibly allowing for granular consent choices.
Task<bool> showConsentDetailed = await ChartboostCore.Consent.ShowConsentDialog(ConsentDialogType.Detailed);
```

### ConsentChangeWithFullConsents & ConsentModuleReadyWithInitialConsents

Chartboost Core unified API provided consent specific events to be used as needed:

```csharp

// Called when the initial values for IConsentManagementPlatform.Consents first become available for the current session.
ChartboostCore.Consent.ConsentModuleReadyWithInitialConsents += initialConsents => {
  foreach (var consent in initialConsents)
  {
    Debug.Log($"ConsentKey {consent.Key} with ConsentValue {consent.Value}");
  }
}

// Called whenever the IConsentManagementPlatform.Consents value changed.
ChartboostCore.Consent.ConsentModuleReadyWithInitialConsents += (fullConsents, modifiedKeys) => {
  Debug.Log($"Full Consents: {JsonTools.SerializeObject(fullConsents)}");
  Debug.Log($"Modified Keys: {JsonTools.SerializeObject(modifiedKeys)}");
}
```

## Calling Async Code from Sync Contexts

A lot of APIs provided in the Chartboost Core Unity SDK utilize the async/await C# implementation. It is possible for developers to try to call the following code from a sync context where async/await might not be supported:

```csharp
Task<bool> showConsentConcise = await ChartboostCore.Consent.ShowConsentDialog(ConsentDialogType.Concise);
```

We have provided tools to allow for such async calls to be called form sync contexts. Utilize `ContinueWithOnMainThread` task extension for this purpose.

```csharp
ChartboostCore.Consent.ShowConsentDialog(ConsentDialogType.Concise).ContinueWithOnMainThread(continuation =>
{
    Debug.Log($"ShowConsentDialog finished with status: {continuation.Result}");
});
```

# Contributions

We are committed to a fully transparent development process and highly appreciate any contributions. Our team regularly monitors and investigates all submissions for the inclusion in our official releases.

# License

Refer to our [LICENSE](LICENSE.md) file for more information.