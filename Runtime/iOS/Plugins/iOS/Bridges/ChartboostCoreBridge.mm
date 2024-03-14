#import "CBCDelegates.h"
#import "CBCUnityObserver.h"
#import "CBCModuleWrapper.h"

extern "C" {
    const char* _getChartboostCoreVersion(){
        return toCStringOrNull([ChartboostCore sdkVersion]);
    }

    void _chartboostCoreInitialize(const char* chartboostAppIdentifier)
    {
        CBCSDKConfiguration* configuration = [[CBCSDKConfiguration alloc] initWithChartboostAppID:toNSStringOrEmpty(chartboostAppIdentifier)];
        [ChartboostCore initializeSDKWithConfiguration:configuration modules:[[[CBCUnityObserver sharedObserver] initializableModules] allValues] moduleObserver:[CBCUnityObserver sharedObserver]];
    }

    void _chartboostCoreAddUnityModule(const char* moduleIdentifier, const char* moduleVersion, ChartboostCoreOnModuleInitializeDelegate initializeCallback){

        id<CBCInitializableModule> chartboostCoreModule  = [[CBCModuleWrapper alloc] initWithModuleID:toNSStringOrEmpty(moduleIdentifier) moduleVersion:toNSStringOrEmpty(moduleVersion) initializeCallback:initializeCallback];
        [[CBCUnityObserver sharedObserver] addModule:chartboostCoreModule];
    }

    void _chartboostCoreAddNativeModule(const void* uniqueId){
        id<CBCInitializableModule> chartboostCoreModule = (__bridge id<CBCInitializableModule>)uniqueId;
        [[CBCUnityObserver sharedObserver] addModule:chartboostCoreModule];
    }

    const char* _chartboostCoreGetNativeModuleId(const void* uniqueId) {
        id<CBCInitializableModule> chartboostCoreModule = (__bridge id<CBCInitializableModule>)uniqueId;
        return toCStringOrNull([chartboostCoreModule moduleID]);
    }

    const char* _chartboostCoreGetNativeModuleVersion(const void* uniqueId) {
        id<CBCInitializableModule> chartboostCoreModule = (__bridge id<CBCInitializableModule>)uniqueId;
        return toCStringOrNull([chartboostCoreModule moduleVersion]);
    }
}
