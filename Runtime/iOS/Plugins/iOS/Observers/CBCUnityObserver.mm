#import "CBCUnityObserver.h"
#import "CBCModuleWrapper.h"

NSString* const CBCUnityObserverTAG = @"CBCUnityObserver";

@implementation CBCUnityObserver

static NSMutableDictionary* _modulesToInit;
static NSMutableDictionary* _moduleStore;

+ (instancetype) sharedObserver {
    static dispatch_once_t pred = 0;
    static id _sharedObject = nil;
    dispatch_once(&pred, ^{
        _sharedObject = [[self alloc] init];
    });
    
    return _sharedObject;
}

- (NSMutableDictionary*) initializableModules {
    if (_modulesToInit == nil)
        _modulesToInit = [NSMutableDictionary new];
    return _modulesToInit;
}

- (void) clearInitializableModules {
    [[self initializableModules] removeAllObjects];
    [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUnityObserverTAG log:@"Cleared Initializable Modules" logLevel:CBLLogLevelVerbose];
}

- (NSMutableDictionary*) moduleStore {
    if (_moduleStore == nil)
        _moduleStore = [NSMutableDictionary new];
    return _moduleStore;
}

- (void)addModule:(id<CBCModule>)initializableModule {
    NSString* moduleId = [initializableModule moduleID];
    [[self initializableModules] setObject:initializableModule forKey:moduleId];
    [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUnityObserverTAG log:[NSString stringWithFormat:@"Added Module:%@ to Initializable Modules", moduleId] logLevel:CBLLogLevelVerbose];
}

- (void)removeModule:(NSString*)moduleId{
    [[self initializableModules] removeObjectForKey:moduleId];
    [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUnityObserverTAG log:[NSString stringWithFormat:@"Removed Module:%@ from Initializable Modules", moduleId] logLevel:CBLLogLevelVerbose];
}

- (void)storeModule:(id<CBCModule>)nativeModule {
    NSString* moduleId = [nativeModule moduleID];
    [[self moduleStore] setObject:nativeModule forKey:moduleId];
    [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUnityObserverTAG log:[NSString stringWithFormat:@"Added Module:%@ to Module Store", moduleId] logLevel:CBLLogLevelVerbose];
}

#pragma mark CBCInitializableModuleObserver
- (void)onModuleInitializationCompleted:(CBCModuleInitializationResult * _Nonnull)result {
    if (_onModuleInitializationCompleted == nil)
        return;

    const char * moduleId = toCStringOrNull([result moduleID]);
    const char * moduleVersion = toCStringOrNull([result moduleVersion]);
    long start = ([[result startDate] timeIntervalSince1970] * 1000);
    long end = ([[result endDate] timeIntervalSince1970] * 1000);
    long duration = [result duration] * 1000;
    const char* exception = NULL;
    
    NSError* error = [result error];
    
    if (error != nil)
    {
        NSUInteger code = [error code];
        NSString* domain = [error domain];
        NSString* message = [error localizedDescription];
        NSString* cause = [error localizedFailureReason];
        NSString* resolution = [error localizedRecoverySuggestion];
        
        NSDictionary *dict = @{ @"domain" : domain ?: [NSNull null],
                                @"code" : [NSNumber numberWithInt:(int)code],
                                @"message" : message ?: [NSNull null],
                                @"cause" : cause ?: [NSNull null],
                                @"resolution" : resolution ?: [NSNull null] };
        exception = toJSON(dict);
    }
    
    if (_onModuleInitializationCompleted != nil)
        _onModuleInitializationCompleted(moduleId, start, end, duration, moduleId, moduleVersion, exception);
    [self removeModule:[result moduleID]];
}

#pragma mark CBCConsentObserver
- (void)onConsentModuleReadyWithInitialConsents:(NSDictionary<NSString *,NSString *> * _Nonnull)initialConsents {
    if (_onConsentReady != nil)
        _onConsentReady(toJSON(initialConsents));
}

- (void)onConsentChangeWithFullConsents:(NSDictionary<NSString *,NSString *> * _Nonnull)fullConsents modifiedKeys:(NSSet<NSString *> * _Nonnull)modifiedKeys {
    if (_onConsentChange != nil)
        _onConsentChange(toJSON(fullConsents), toJSON([modifiedKeys allObjects]));
}

#pragma mark CBCEnvironmentObserver
- (void)onChange:(enum CBCObservableEnvironmentProperty)property {
    if (_onEnvironmentPropertyChanged != nil)
        _onEnvironmentPropertyChanged((int)property);
}
@end
