#import "CBCUnityUtilities.h"

@interface CBCUnityObserver : NSObject<CBCInitializableModuleObserver, CBCConsentObserver, CBCPublisherMetadataObserver>

+ (instancetype) sharedObserver;
- (NSMutableDictionary*) initializableModules;
- (NSMutableDictionary*) nativeModuleStore;
- (void) addModule:(id<CBCInitializableModule>)initializableModule;
- (void)removeModule:(NSString*)moduleId;
- (void) clearInitializableModules;
- (void) storeModule:(id<CBCInitializableModule>)nativeModule;

#pragma mark CBCInitializableModuleObserver
@property ChartboostCoreOnModuleInitializationResult onModuleInitializationCompleted;

#pragma mark CBCConsentObserver
@property ChartboostCoreOnEnumStatusChange onConsentStatusChange;
@property ChartboostCoreOnConsentChangeForStandard onConsentChangeForStandard;
@property ChartboostCoreAction onConsentReady;

#pragma mark CBCPublisherMetadataObserver

@property ChartboostCoreOnEnumStatusChange onPublisherMetadataPropertyChange;

@end
