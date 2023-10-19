#import "CBCUnityUtilities.h"

@interface CBCModuleWrapper : NSObject <CBCInitializableModule>
@property (nonnull, nonatomic, copy) NSString *moduleID;
@property (nonnull, nonatomic, copy) NSString *moduleVersion;
@property _Nullable ChartboostCoreOnModuleInitializeDelegate initializeCallback;
@property (nonnull, nonatomic) void (^completer)(NSError * _Nonnull error);

- (_Nonnull instancetype)initWithModuleID:(NSString * _Nonnull)moduleID
                   moduleVersion:(NSString * _Nonnull)moduleVersion
                     initializeCallback:(_Nullable ChartboostCoreOnModuleInitializeDelegate)initializeCallback;

- (void)completeInitializationWithError:(NSError* _Nullable)error;

@end
