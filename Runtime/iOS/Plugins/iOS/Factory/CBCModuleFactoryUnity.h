#import "CBCDelegates.h"

@interface CBCModuleFactoryUnity : NSObject<CBCModuleFactory>

+ (instancetype _Nonnull) sharedFactory;

@property (nonnull, nonatomic) void (^completer)(id<CBCModule>_Nullable coreModule);

- (void) completeMakeModuleWithModule:(id<CBCModule>_Nullable)coreModule;

#pragma mark CBCModuleFactory
@property ChartboostCoreOnMakeModule _Nullable onMakeModule;
@end
