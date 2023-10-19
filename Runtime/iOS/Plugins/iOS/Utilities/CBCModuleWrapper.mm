#import "CBCModuleWrapper.h"
#import "CBCUnityObserver.h"

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

- (void)initializeWithConfiguration:(CBCModuleInitializationConfiguration * _Nonnull)configuration completion:(void (^ _Nonnull)(NSError * _Nullable))completion {
    _completer = ^(NSError * error){
        completion(error);
    };
    _initializeCallback(getCStringOrNull(_moduleID), getCStringOrNull([configuration  chartboostAppID]));
}

- (void)completeInitializationWithError:(NSError* _Nullable)error; {
    if (_completer != nil)
        _completer(error);
}
@end

extern "C" {
    void _completeModuleInitialization(const char* moduleIdentifier, const char* _Nullable jsonError)
    {
        CBCModuleWrapper* targetModule = [[[CBCUnityObserver sharedObserver] initializableModules] valueForKey:getNSStringOrEmpty(moduleIdentifier)];
        
        if (targetModule == nil)
            return;
        
        if (jsonError != NULL)
        {
            NSError* error = nil;
            NSDictionary* errorAsDictionary= stringToNSDictionary(jsonError);
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
