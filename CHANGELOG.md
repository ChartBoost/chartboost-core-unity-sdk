# Changelog
All notable changes to this project will be documented in this file using the standards as defined at [Keep a Changelog](https://keepachangelog.com/en/1.0.0/). This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0).

### Version 1.0.3 *(2025-06-11)*
Bug Fixes:
- Fix support for consent adapters in Unity Editor.
- Fix `StronglyTyped` definitions to conform to `IComparable` interface.
- Fix unit tests to properly validate Unity Core version.

### Version 1.0.2 *(2024-09-19)*
Improvements:
- Modified `NativeModule` internal class to expand API flexibility of wrapped native modules.

Bug Fixes:
- `ConsentKey` predefined values are in sync with native values.

### Version 1.0.1 *(2024-08-29)*
Fixed:
- Skipped module identifiers not forwarded to iOS Core SDK.

### Version 1.0.0 *(2024-08-01)*

Added:
- Support for `Unity` specific remote module initialization.
- `event ChartboostCoreConsentChangeWithFullConsents ConsentChangeWithFullConsents` to `IConsentManagementPlatform`.
- `event ChartboostCoreConsentModuleReadyWithInitialConsents ConsentModuleReadyWithInitialConsents` to `IConsentManagementPlatform`.
- `IReadOnlyList<Module> Modules` to `SDKConfiguration`.
- `IReadOnlyCollection<string> SkippedModuleIdentifiers` to `SDKConfiguration`

Improvements:
- Combined `FrameworkName` and `FrameworkVersion` setter into a single call `SetFramework`.
- `PublisherMetadata` events moved to `AnalyticsEnvironment`.

Removed:
- `ConsentStatus ConsentStatus` from `IConsentManagementPlatform`.
- `Dictionary<string, ConsentStatus> PartnerConsentStatus` from `IConsentManagementPlatform`.
- `event ChartboostConsentStatusChange ConsentStatusChange` from `IConsentManagementPlatform`.
- `event ChartboostPartnerConsentStatusChange PartnerConsentStatusChange` from `IConsentManagementPlatform`.
- `event Action ConsentModuleReady` from `IConsentManagementPlatform`.

### Version 0.4.0 *(2023-12-7)*
Improvements:
- Added `PartnerConsentStatus: Dictionary<String, ConsentStatus>` to `IConsentManagementPlatform`. This is to facilitate per-partner consent for Mediation.
- Added `delegate void ChartboostPartnerConsentStatusChange(string partnerIdentifier, ConsentStatus status) PartnerConsentStatusChange` to `IConsentManagementPlatform`.
- Added a `IPostGenerateGradleAndroidProject` build processor to automate Gradle modifications.
- Added `ChartboostCore.androidlib` to unify Chartboost Core Gradle modifications and automations.

## Version 0.3.1 *(2023-10-19)*
Bug Fixes:
- Fix Android dependencies for Chartboost Core Unity SDK 0.3.1.

## Version 0.3.0 *(2023-10-19)*
Improvements:
- Added support for observing changes to `IPublisherMetadata` properties via events.
- Propagated Chartboost app ID to modules on initialization via `ModuleInitializationConfiguration.ChartboostApplicationIdentifier`.
- Optimized `InitializableModule.Initialize` calls for Android and iOS platforms.

Bug Fixes:
- Fixed an error where Exceptions would be raised when calling nullable boolean values before initializing the SDK.

### Version 0.2.0 *(2023-09-07)*
Improvements:
- Added Unity Editor API coverage.
- Added `ModuleReady` event for `InitializableModules`.
- Fixed `Exception` catching for `InitializableModules`.
- Fixed code stripping issues by adding `[Preserve]` attribute to `AndroidJavaProxy` classes.
- Modified Unity C# API to match Android/iOS API changes. 
- Added code documentation for all platforms.
 
### Version 0.1.0
- First public alpha.

Added:
- Package with Core, Android & iOS APIs.
- Support for Unity & Native modules.
- Support for Unity modules async initialization.
- Unit tests for API
