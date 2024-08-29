#import "CBCDelegates.h"
#import "CBCUnityObserver.h"
#import "CBCModuleWrapper.h"

extern "C" {
    const char* _CBCVersion(){
        return toCStringOrNull([ChartboostCore sdkVersion]);
    }

    int _CBCGetLogLevel(){
        return (int)[ChartboostCore logLevel];
    }

    void _CBCSetLogLevel(int logLevel){
        [ChartboostCore setLogLevel:(CBCLogLevel)logLevel];
    }

    void _CBCInitialize(const char* chartboostAppIdentifier, const char** skippedModuleIds, int skippedModuleIdsSize){

        NSMutableArray* skippedModules = [NSMutableArray new];

        if (skippedModuleIdsSize > 0)
            skippedModules = toNSMutableArray(skippedModuleIds, skippedModuleIdsSize);

        NSArray* modules = [[[CBCUnityObserver sharedObserver] initializableModules] allValues];
        CBCSDKConfiguration* configuration = [[CBCSDKConfiguration alloc] initWithChartboostAppID:toNSStringOrEmpty(chartboostAppIdentifier) modules:modules skippedModuleIDs:skippedModules];
        [ChartboostCore initializeSDKWithConfiguration:configuration moduleObserver:[CBCUnityObserver sharedObserver]];
    }

    void _CBCSetModuleInitializationResultCallback(ChartboostCoreOnModuleInitializationResult onModuleInitializationResult){
        [[CBCUnityObserver sharedObserver] setOnModuleInitializationCompleted:onModuleInitializationResult];
    }
}
