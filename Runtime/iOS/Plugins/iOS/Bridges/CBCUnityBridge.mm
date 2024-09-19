#import "CBCDelegates.h"
#import "CBCUnityObserver.h"
#import "CBCModuleWrapper.h"

NSString* const CBCUnityBridgeTAG = @"CBCUnityBridge";

extern "C" {
    const char* _CBCVersion(){
        return toCStringOrNull([ChartboostCore sdkVersion]);
    }

    int _CBCGetLogLevel(){
        int logLevel = (int)[ChartboostCore logLevel];
        [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUnityBridgeTAG log:[NSString stringWithFormat:@"Get Log Level: %d", logLevel] logLevel:CBLLogLevelVerbose];
        return logLevel;
    }

    void _CBCSetLogLevel(int logLevel){
        [ChartboostCore setLogLevel:(CBCLogLevel)logLevel];
        [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUnityBridgeTAG log:[NSString stringWithFormat:@"Set Log Level: %d", logLevel] logLevel:CBLLogLevelVerbose];
    }

    void _CBCInitialize(const char* chartboostAppIdentifier, const char** skippedModuleIds, int skippedModuleIdsSize){
        NSMutableArray* skippedModules = [NSMutableArray new];
        if (skippedModuleIdsSize > 0)
            skippedModules = toNSMutableArray(skippedModuleIds, skippedModuleIdsSize);
        NSArray* modules = [[[CBCUnityObserver sharedObserver] initializableModules] allValues];
        CBCSDKConfiguration* configuration = [[CBCSDKConfiguration alloc] initWithChartboostAppID:toNSStringOrEmpty(chartboostAppIdentifier) modules:modules skippedModuleIDs:skippedModules];
        [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCUnityBridgeTAG log:[NSString stringWithFormat:@"Attempting to Initialize Chartboost Core with %lu modules", (unsigned long)[modules count]] logLevel:CBLLogLevelVerbose];
        [ChartboostCore initializeSDKWithConfiguration:configuration moduleObserver:[CBCUnityObserver sharedObserver]];
    }

    void _CBCSetModuleInitializationResultCallback(ChartboostCoreOnModuleInitializationResult onModuleInitializationResult){
        [[CBCUnityObserver sharedObserver] setOnModuleInitializationCompleted:onModuleInitializationResult];
    }
}
