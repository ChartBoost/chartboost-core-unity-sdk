#import "ChartboostUnityUtilities.h"
#import <objc/runtime.h>
#import <AppTrackingTransparency/AppTrackingTransparency.h>
#import <ChartboostcoreSDK/ChartboostCoreSDK-Swift.h>

typedef void (*ChartboostCoreOnModuleInitializationResult)(const char* moduleIdentifier, long start, long end, long duration, const char* exception);
typedef void (*ChartboostCoreOnModuleInitializeDelegate)(const char* moduleIdentifier, const char* chartboostAppIdentifier);
typedef void (*ChartboostCoreOnConsentChangeForStandard)(const char* standard, const char* value);
typedef void (*ChartboostCoreOnPartnerConsentChange)(const char* partnerIdentifier, int consentStatus);
typedef void (*ChartboostCoreOnEnumStatusChange)(int value);
typedef void (*ChartbosotCoreResultString)(int hashCode, const char* result);
typedef void (*ChartboostCoreResultBoolean)(int hashCode, bool completion);
typedef void (*ChartboostCoreAction)();
