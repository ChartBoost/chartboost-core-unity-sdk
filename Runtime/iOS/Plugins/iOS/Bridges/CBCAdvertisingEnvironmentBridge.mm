#import "CBCUnityUtilities.h"

extern "C" {
    const char* _advertisingEnvironmentGetOsName(){
        return getCStringOrNull([[ChartboostCore advertisingEnvironment] osName]);
    }

    const char* _advertisingEnvironmentGetOsVersion(){
        return getCStringOrNull([[ChartboostCore advertisingEnvironment] osVersion]);
    }

    const char* _advertisingEnvironmentGetDeviceMake(){
        return getCStringOrNull([[ChartboostCore advertisingEnvironment] deviceMake]);
    }

    const char* _advertisingEnvironmentGetDeviceModel(){
        return getCStringOrNull([[ChartboostCore advertisingEnvironment] deviceModel]);
    }

    const char* _advertisingEnvironmentGetDeviceLocale(){
        return getCStringOrNull([[ChartboostCore advertisingEnvironment] deviceLocale]);
    }

    double _advertisingEnvironmentGetScreenHeight(){
        return [[ChartboostCore advertisingEnvironment] screenHeight];
    }

    double _advertisingEnvironmentGetScreenScale(){
        return [[ChartboostCore advertisingEnvironment] screenScale];
    }

    double _advertisingEnvironmentGetScreenWidth(){
        return [[ChartboostCore advertisingEnvironment] screenWidth];
    }

    const char* _advertisingEnvironmentGetBundleIdentifier(){
        return getCStringOrNull([[ChartboostCore advertisingEnvironment] bundleID]);
    }

    bool _advertisingEnvironmentGetLimitAdTrackingEnabled(){
        return [[ChartboostCore advertisingEnvironment] isLimitAdTrackingEnabled];
    }

    const char* _advertisingEnvironmentGetAdvertisingIdentifier(){
        return getCStringOrNull([[ChartboostCore advertisingEnvironment] advertisingID]);
    }
}
