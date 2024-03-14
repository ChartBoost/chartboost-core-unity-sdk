#import "CBCDelegates.h"

extern "C" {
    const char* _advertisingEnvironmentGetOsName(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] osName]);
    }

    const char* _advertisingEnvironmentGetOsVersion(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] osVersion]);
    }

    const char* _advertisingEnvironmentGetDeviceMake(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] deviceMake]);
    }

    const char* _advertisingEnvironmentGetDeviceModel(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] deviceModel]);
    }

    const char* _advertisingEnvironmentGetDeviceLocale(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] deviceLocale]);
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
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] bundleID]);
    }

    bool _advertisingEnvironmentGetLimitAdTrackingEnabled(){
        return [[ChartboostCore advertisingEnvironment] isLimitAdTrackingEnabled];
    }

    const char* _advertisingEnvironmentGetAdvertisingIdentifier(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] advertisingID]);
    }
}
