#import "CBCDelegates.h"

extern "C" {
    const char* _CBCAdvertisingGetOsName(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] osName]);
    }

    const char* _CBCAdvertisingGetOsVersion(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] osVersion]);
    }

    const char* _CBCAdvertisingGetDeviceMake(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] deviceMake]);
    }

    const char* _CBCAdvertisingGetDeviceModel(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] deviceModel]);
    }

    const char* _CBCAdvertisingGetDeviceLocale(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] deviceLocale]);
    }

    double _CBCAdvertisingGetScreenHeight(){
        return [[ChartboostCore advertisingEnvironment] screenHeightPixels];
    }

    double _CBCAdvertisingGetScreenScale(){
        return [[ChartboostCore advertisingEnvironment] screenScale];
    }

    double _CBCAdvertisingGetScreenWidth(){
        return [[ChartboostCore advertisingEnvironment] screenWidthPixels];
    }

    const char* _CBCAdvertisingGetBundleIdentifier(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] bundleID]);
    }

    bool _CBCAdvertisingGetLimitAdTrackingEnabled(){
        return [[ChartboostCore advertisingEnvironment] isLimitAdTrackingEnabled];
    }

    const char* _CBCAdvertisingGetAdvertisingIdentifier(){
        return toCStringOrNull([[ChartboostCore advertisingEnvironment] advertisingID]);
    }
}
