#import "CBCDelegates.h"

@interface CBCUnityObserver : NSObject<CBCModuleObserver, CBCConsentObserver, CBCEnvironmentObserver>

+ (instancetype) sharedObserver;
- (NSMutableDictionary*) initializableModules;
- (NSMutableDictionary*) moduleStore;
- (void) addModule:(id<CBCModule>)initializableModule;
- (void) removeModule:(NSString*)moduleId;
- (void) clearInitializableModules;
- (void) storeModule:(id<CBCModule>)nativeModule;

#pragma mark CBCInitializableModuleObserver
@property ChartboostCoreOnModuleInitializationResult onModuleInitializationCompleted;

#pragma mark CBCConsentObserver
@property ChartboostCoreOnConsentChangeWithFullConsents onConsentChange;
@property ChartboostCoreOnConsentReadyWithInitialConsents onConsentReady;

#pragma mark CBCPublisherMetadataObserver

@property ChartboostCoreOnEnumStatusChange onEnvironmentPropertyChanged;

@end
