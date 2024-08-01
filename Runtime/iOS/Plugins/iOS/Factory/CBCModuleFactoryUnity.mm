#import "CBCModuleFactoryUnity.h"

@implementation CBCModuleFactoryUnity

+ (instancetype) sharedFactory {
    static dispatch_once_t pred = 0;
    static id _sharedObject = nil;
    dispatch_once(&pred, ^{
        _sharedObject = [[self alloc] init];
    });

    return _sharedObject;
}

- (void)makeModuleWithClassName:(NSString * _Nonnull)className credentials:(NSDictionary<NSString *,id> * _Nullable)credentials completion:(void (^ _Nonnull)(id<CBCModule> _Nullable))completion { 
    _completer = ^(id<CBCModule> coreModule){
        completion(coreModule);
    };

    _onMakeModule(toCStringOrNull(className), toJSON(credentials));
}

- (void)completeMakeModuleWithModule:(id<CBCModule> _Nullable)coreModule {
    if (_completer != nil)
        _completer(coreModule);
}
@end

extern "C" {
    void _CBCSetModuleFactoryUnityCallbacks(ChartboostCoreOnMakeModule onMakeModule) {
        [[CBCModuleFactoryUnity sharedFactory] setOnMakeModule:onMakeModule];
        [ChartboostCore performSelector:@selector(setNonNativeModuleFactory:) withObject:[CBCModuleFactoryUnity sharedFactory]];
    }

    void _CBCCompleteModuleMake(const void * uniqueId){
        id<CBCModule> moduleInstance = nil;

        if (uniqueId != nil)
            moduleInstance = (__bridge id<CBCModule>)uniqueId;

        [[CBCModuleFactoryUnity sharedFactory] completeMakeModuleWithModule:moduleInstance];
    }
}
