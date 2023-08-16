#import "CBCUnityObserver.h"
#import "CBCModuleWrapper.h"

@implementation CBCUnityObserver

static NSMutableDictionary* _modulesToInit;
static NSMutableDictionary* _nativeModuleStore;

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
}

- (NSMutableDictionary*) nativeModuleStore {
    if (_nativeModuleStore == nil)
        _nativeModuleStore = [NSMutableDictionary new];
    return _nativeModuleStore;
}

- (void)addModule:(id<CBCInitializableModule>)initializableModule {
    [[self initializableModules] setObject:initializableModule forKey:[initializableModule moduleID]];
}

- (void)removeModule:(NSString*)moduleId{
    [[self initializableModules] removeObjectForKey:moduleId];
}

- (void)storeModule:(id<CBCInitializableModule>)nativeModule {
    [[self nativeModuleStore] setObject:nativeModule forKey:[nativeModule moduleID]];
}

- (void)onModuleInitializationCompleted:(CBCModuleInitializationResult * _Nonnull)result {
    if (_onModuleInitializationCompleted == nil)
        return;
    
    const char* moduleIdentifier = [[[result module] moduleID] UTF8String];
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
        NSDictionary *dict = @{ @"domain" : domain,
                                @"code" : [NSNumber numberWithInt:(int)code],
                                @"message" : message,
                                @"cause" : cause,
                                @"resolution" : resolution };
        exception = dictToJson(dict);
    }
    
    if (_onModuleInitializationCompleted != nil)
        _onModuleInitializationCompleted(moduleIdentifier, start, end, duration, exception);
    [self removeModule:[[result module] moduleID]];
}

- (void)onConsentChangeWithStandard:(CBCConsentStandard * _Nonnull)standard value:(CBCConsentValue * _Nullable)value {
    if (_onConsentChangeForStandard != nil)
        _onConsentChangeForStandard([[standard value] UTF8String], [[value value] UTF8String]);
}

- (void)onConsentStatusChange:(enum CBCConsentStatus)status {
    if (_onConsentStatusChange != nil)
        _onConsentStatusChange((int)status);
}

- (void)onConsentModuleReady {
    if (_onConsentReady != nil)
        _onConsentReady();
}
@end

extern "C" {
    void _chartboostCoreAddUnityModule(const char* moduleIdentifier, const char* moduleVersion, ChartboostCoreOnModuleInitializeCallback initializeCallback){
                
        id<CBCInitializableModule> chartboostCoreModule  = [[CBCModuleWrapper alloc] initWithModuleID:getNSStringOrEmpty(moduleIdentifier) moduleVersion:getNSStringOrEmpty(moduleVersion) initializeCallback:initializeCallback];
        [[CBCUnityObserver sharedObserver] addModule:chartboostCoreModule];
    }

    void _chartboostCoreAddNativeModule(const void* uniqueId){
        id<CBCInitializableModule> chartboostCoreModule = (__bridge id<CBCInitializableModule>)uniqueId;
        [[CBCUnityObserver sharedObserver] addModule:chartboostCoreModule];
    }

    const char* _chartboostCoreGetNativeModuleId(const void* uniqueId) {
        id<CBCInitializableModule> chartboostCoreModule = (__bridge id<CBCInitializableModule>)uniqueId;
        return getCStringOrNull([chartboostCoreModule moduleID]);
    }

    const char* _chartboostCoreGetNativeModuleVersion(const void* uniqueId) {
        id<CBCInitializableModule> chartboostCoreModule = (__bridge id<CBCInitializableModule>)uniqueId;
        return getCStringOrNull([chartboostCoreModule moduleVersion]);
    }
}