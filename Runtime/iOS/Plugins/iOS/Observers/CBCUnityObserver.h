#import "CBCUnityUtilities.h"

@interface CBCUnityObserver : NSObject<CBCInitializableModuleObserver, CBCConsentObserver>

+ (instancetype) sharedObserver;
- (NSMutableDictionary*) initializableModules;
- (NSMutableDictionary*) nativeModuleStore;
- (void) addModule:(id<CBCInitializableModule>)initializableModule;
- (void)removeModule:(NSString*)moduleId;
- (void) clearInitializableModules;
- (void) storeModule:(id<CBCInitializableModule>)nativeModule;

@property ChartboostCoreOnModuleInitializationResultCallback onModuleInitializationCompleted;
@property ChartboostCoreOnConsentStatusChangeCallback onConsentStatusChange;
@property ChartboostCoreOnConsentChangeCallback onConsentChangeForStandard;
@property ChartboostCoreAction onConsentReady;

@end
