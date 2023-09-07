# Changelog
All notable changes to this project will be documented in this file using the standards as defined at [Keep a Changelog](https://keepachangelog.com/en/1.0.0/). This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0).

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
