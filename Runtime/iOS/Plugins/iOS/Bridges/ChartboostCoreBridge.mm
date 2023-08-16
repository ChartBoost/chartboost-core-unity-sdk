#import "CBCUnityUtilities.h"
#import "CBCUnityObserver.h"
#import "CBCModuleWrapper.h"

extern "C" {
    const char* _getChartboostCoreVersion(){
        return getCStringOrNull([ChartboostCore sdkVersion]);
    }

    void _chartboostCoreInitialize(const char* chartboostAppIdentifier, ChartboostCoreOnModuleInitializationResultCallback moduleInitializationCallback)
    {
        CBCSDKConfiguration* configuration = [[CBCSDKConfiguration alloc] initWithChartboostAppID:getNSStringOrEmpty(chartboostAppIdentifier)];
        
        [[CBCUnityObserver sharedObserver] setOnModuleInitializationCompleted:moduleInitializationCallback];
                
        [ChartboostCore initializeSDKWithConfiguration:configuration modules:[[[CBCUnityObserver sharedObserver] initializableModules] allValues] moduleObserver:[CBCUnityObserver sharedObserver]];
    }
}
