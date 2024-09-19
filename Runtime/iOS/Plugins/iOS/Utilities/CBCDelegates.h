#import "CBLUnityLoggingBridge.h"
#import "ChartboostUnityUtilities.h"
#import <objc/runtime.h>
#import <AppTrackingTransparency/AppTrackingTransparency.h>
#import <ChartboostcoreSDK/ChartboostCoreSDK-Swift.h>

typedef void (*ChartboostCoreOnModuleInitializationResult)(const char* moduleIdentifier, long start, long end, long duration, const char* moduleId, const char* moduleVersion, const char* exception);
typedef void (*ChartboostCoreOnModuleInitializeDelegate)(const char* moduleIdentifier, const char* chartboostAppIdentifier);
typedef void (*ChartboostCoreOnMakeModule)(const char* className, const char* credentialsJson);
typedef void (*ChartboostCoreOnConsentChangeWithFullConsents)(const char* modifiedKeys, const char* fullConsents);
typedef void (*ChartboostCoreOnConsentReadyWithInitialConsents)(const char* initialConsents);
typedef void (*ChartboostCoreOnEnumStatusChange)(int value);
typedef void (*ChartbosotCoreResultString)(int hashCode, const char* result);
typedef void (*ChartboostCoreResultBoolean)(int hashCode, bool completion);
