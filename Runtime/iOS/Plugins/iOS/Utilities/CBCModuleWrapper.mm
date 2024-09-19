#import "CBCModuleWrapper.h"
#import "CBCUnityObserver.h"

NSString* const CBCModuleWrapperTAG = @"CBCModuleWrapper";

@implementation CBCModuleWrapper

- (instancetype)initWithModuleID:(NSString * _Nonnull)moduleID
                            moduleVersion:(NSString * _Nonnull)moduleVersion
                              initializeCallback:(_Nullable ChartboostCoreOnModuleInitializeDelegate)initializeCallback {
    self = [super init];
    if (self) {
        _moduleID = moduleID;
        _moduleVersion = moduleVersion;
        _initializeCallback = initializeCallback;
    }
    return self;
}

- (instancetype)initWithCredentials:(NSDictionary<NSString *,id> *)credentials {
    return [self init];
}

- (void)initializeWithConfiguration:(CBCModuleConfiguration * _Nonnull)configuration completion:(void (^ _Nonnull)(NSError * _Nullable))completion {
    _completer = ^(NSError * error){
        [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCModuleWrapperTAG log:@"Wrapped module initialization result obtained" logLevel:CBLLogLevelVerbose];
        completion(error);
    };

    [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCModuleWrapperTAG log:[NSString stringWithFormat:@"Wrapped module: %@ attempting initialization", _moduleID] logLevel:CBLLogLevelVerbose];
    _initializeCallback(toCStringOrNull(_moduleID), toCStringOrNull([configuration  chartboostAppID]));
}

- (void)completeInitializationWithError:(NSError* _Nullable)error; {
    if (_completer != nil)
        _completer(error);
}
@end

extern "C" {
    const void * _CBCWrapUnityModule(const char* moduleIdentifier, const char* moduleVersion, ChartboostCoreOnModuleInitializeDelegate initializeCallback){

        id<CBCModule> chartboostCoreModule  = [[CBCModuleWrapper alloc] initWithModuleID:toNSStringOrEmpty(moduleIdentifier) moduleVersion:toNSStringOrEmpty(moduleVersion) initializeCallback:initializeCallback];
        [[CBCUnityObserver sharedObserver] storeModule:chartboostCoreModule];
        return (__bridge void*)chartboostCoreModule;
    }

    void _CBCAddModule(const void* uniqueId){
        id<CBCModule> chartboostCoreModule = (__bridge id<CBCModule>)uniqueId;
        [[CBCUnityObserver sharedObserver] addModule:chartboostCoreModule];
        [[CBCUnityObserver sharedObserver] storeModule:chartboostCoreModule];
    }

    const char* _CBCGetModuleId(const void* uniqueId) {
        id<CBCModule> chartboostCoreModule = (__bridge id<CBCModule>)uniqueId;
        return toCStringOrNull([chartboostCoreModule moduleID]);
    }

    const char* _CBCGetModuleVersion(const void* uniqueId) {
        id<CBCModule> chartboostCoreModule = (__bridge id<CBCModule>)uniqueId;
        return toCStringOrNull([chartboostCoreModule moduleVersion]);
    }

    void _CBCCompleteModuleInitialization(const char* moduleIdentifier, const char* _Nullable jsonError)
    {
        CBCModuleWrapper* targetModule = [[[CBCUnityObserver sharedObserver] moduleStore] valueForKey:toNSStringOrEmpty(moduleIdentifier)];

        if (targetModule == nil)
            return;
        
        if (jsonError != NULL)
        {
            NSError* error = nil;
            NSDictionary* errorAsDictionary= toNSDictionary(jsonError);
            NSString* domain = [errorAsDictionary objectForKey:@"domain"];
            int code = [errorAsDictionary objectForKey:@"code"] ? [[errorAsDictionary objectForKey:@"code"] intValue] : -1;
            NSString* message = [errorAsDictionary valueForKey:@"message"];
            NSString* cause = [errorAsDictionary valueForKey:@"cause"];
            NSString* resolution = [errorAsDictionary valueForKey:@"resolution"];
            error = [NSError errorWithDomain:domain code:code userInfo:@{
                NSLocalizedDescriptionKey: message,
                NSLocalizedFailureReasonErrorKey: cause,
                NSLocalizedRecoverySuggestionErrorKey: resolution
            }];
            [targetModule completeInitializationWithError:error];
            return;
        }
        [targetModule completeInitializationWithError:nil];
    }

}
