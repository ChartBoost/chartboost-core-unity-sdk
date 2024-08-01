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

    void _CBCInitialize(const char* chartboostAppIdentifier){
        NSArray* modules = [[[CBCUnityObserver sharedObserver] initializableModules] allValues];

        CBCSDKConfiguration* configuration = [[CBCSDKConfiguration alloc] initWithChartboostAppID:toNSStringOrEmpty(chartboostAppIdentifier) modules:modules skippedModuleIDs:[NSArray new]];

        id<CBCModuleObserver> observer = [CBCUnityObserver sharedObserver];

        [ChartboostCore initializeSDKWithConfiguration:configuration moduleObserver:observer];
    }

    void _CBCSetModuleInitializationResultCallback(ChartboostCoreOnModuleInitializationResult onModuleInitializationResult){
        [[CBCUnityObserver sharedObserver] setOnModuleInitializationCompleted:onModuleInitializationResult];
    }
}
