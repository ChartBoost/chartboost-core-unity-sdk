#import <Foundation/Foundation.h>
#import <objc/runtime.h>
#import <AppTrackingTransparency/AppTrackingTransparency.h>
#import <ChartboostcoreSDK/ChartboostCoreSDK-Swift.h>

typedef void (*ChartboostCoreOnModuleInitializationResult)(const char* moduleIdentifier, long start, long end, long duration, const char* exception);
typedef void (*ChartboostCoreOnModuleInitializeDelegate)(const char* moduleIdentifier, const char* chartboostAppIdentifier);
typedef void (*ChartboostCoreOnConsentChangeForStandard)(const char* standard, const char* value);
typedef void (*ChartboostCoreOnEnumStatusChange)(int value);
typedef void (*ChartbosotCoreResultString)(int hashCode, const char* result);
typedef void (*ChartboostCoreResultBoolean)(int hashCode, bool completion);
typedef void (*ChartboostCoreAction)();

typedef void (^block)(void);

/// Allocates a C string from a NSString.
///
/// Use to allocate the C String reference when converting from NSString to C string. This reference should be marshalled to Unity so it can be managed and disposed there as needed.
///
/// - Parameter nsString: NSString to use as a base for the C string.
/// - Returns: Allocated C string or Null.
const char* getCStringOrNull(NSString* nsString);

/// Allocates a NSString from a C string.
///
/// Use to create a NSString reference from a C string. This is generally needed when sending information from Unity to iOS native code "`Objective-C`". Use when it is possible to pass a null value.
///
/// - Parameter cString: C string to use as a base for the NSString.
/// - Returns: Allocated NSString or nil.
NSString* getNSStringOrNil(const char* cString);


/// Allocates a NSString from a C string.
///
/// Use to create a NSString reference from a C string. This is generally needed when sending information from Unity to iOS native code "`Objective-C`". Use when it is not possible to pass a null value.
/// - Parameter cString: C string to use as a base for the NSString.
/// - Returns: Allocated NSString.
NSString * getNSStringOrEmpty(const char* cString);

const char* dictToJson(NSDictionary *data);

NSDictionary* stringToNSDictionary(const char* cString);

void sendToMain(block block);
