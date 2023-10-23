#import "CBCUnityUtilities.h"
#import "CBCUnityObserver.h"
#import "CBCModuleWrapper.h"

extern "C" {
    const char* _getChartboostCoreVersion(){
        return getCStringOrNull([ChartboostCore sdkVersion]);
    }

    void _chartboostCoreInitialize(const char* chartboostAppIdentifier)
    {
        CBCSDKConfiguration* configuration = [[CBCSDKConfiguration alloc] initWithChartboostAppID:getNSStringOrEmpty(chartboostAppIdentifier)];
        [ChartboostCore initializeSDKWithConfiguration:configuration modules:[[[CBCUnityObserver sharedObserver] initializableModules] allValues] moduleObserver:[CBCUnityObserver sharedObserver]];
    }

    void _chartboostCoreAddUnityModule(const char* moduleIdentifier, const char* moduleVersion, ChartboostCoreOnModuleInitializeDelegate initializeCallback){

        id<CBCInitializableModule> chartboostCoreModule  = [[CBCModuleWrapper alloc] initWithModuleID:getNSStringOrEmpty(moduleIdentifier) moduleVersion:getNSStringOrEmpty(moduleVersion) initializeCallback:initializeCallback];
        [[CBCUnityObserver sharedObserver] addModule:chartboostCoreModule];
    }

    void _chartboostCoreAddNativeModule(const void* uniqueId){
        id<CBCInitializableModule> chartboostCoreModule = (__bridge id<CBCInitializableModule>)uniqueId;
        [[CBCUnityObserver sharedObserver] addModule:chartboostCoreModule];
    }

    const char* _chartboostCoreGetNativeModuleId(const void* uniqueId) {
        id<CBCInitializableModule> chartboostCoreModule = (__bridge id<CBCInitializableModule>)uniqueId;
        return getCStringOrNull([chartboostCoreModule moduleID]);
    }

    const char* _chartboostCoreGetNativeModuleVersion(const void* uniqueId) {
        id<CBCInitializableModule> chartboostCoreModule = (__bridge id<CBCInitializableModule>)uniqueId;
        return getCStringOrNull([chartboostCoreModule moduleVersion]);
    }
}
