#import "CBCUnityUtilities.h"

extern "C" {
    const char* _analyticsEnvironmentGetOsName(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] osName]);
    }

    const char* _analyticsEnvironmentGetOsVersion(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] osVersion]);
    }

    const char* _analyticsEnvironmentGetDeviceMake(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] deviceMake]);
    }

    const char* _analyticsEnvironmentGetDeviceModel(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] deviceModel]);
    }

    const char* _analyticsEnvironmentGetDeviceLocale(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] deviceLocale]);
    }

    double _analyticsEnvironmentGetScreenHeight(){
        return [[ChartboostCore analyticsEnvironment] screenHeight];
    }

    double _analyticsEnvironmentGetScreenScale(){
        return [[ChartboostCore analyticsEnvironment] screenScale];
    }

    double _analyticsEnvironmentGetScreenWidth(){
        return [[ChartboostCore analyticsEnvironment] screenWidth];
    }

    const char * _analyticsEnvironmentGetBundleIdentifier(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] bundleID]);
    }

    bool _analyticsEnvironmentGetLimitAdTrackingEnabled(){
        return [[ChartboostCore analyticsEnvironment] isLimitAdTrackingEnabled];
    }

    int _analyticsEnvironmentGetNetworkConnectionType() {
        return (int)[[ChartboostCore analyticsEnvironment] networkConnectionType];
    }

    double _analyticsEnvironmentGetVolume(){
        return [[ChartboostCore analyticsEnvironment] volume];
    }

    const char* _analyticsEnvironmentGetVendorIdentifier(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] vendorID]);
    }

    int _analyticsEnvironmentGetVendorIdentifierScope(){
        return (int)[[ChartboostCore analyticsEnvironment] vendorIDScope];
    }

    int _analyticsEnvironmentGetAuthorizationStatus(){
        if (@available(iOS 14.0, *))
            return (int)[[ChartboostCore analyticsEnvironment] appTrackingTransparencyStatus];
        return 3; // 3 == ATTrackingManagerAuthorizationStatusAuthorized
    }

    const char* _analyticsEnvironmentGetAppVersion(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] appVersion]);
    }

    double _analyticsEnvironmentGetAppSessionDuration(){
        return (double)[[ChartboostCore analyticsEnvironment] appSessionDuration];
    }

    const char* _analyticsEnvironmentGetAppSessionIdentifier(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] appSessionID]);
    }

    bool _analyticsEnvironmentGetIsUserUnderage(){
        return [[ChartboostCore analyticsEnvironment] isUserUnderage];
    }

    const char* _analyticsEnvironmentGetPublisherSessionIdentifier(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] publisherSessionID]);
    }

    const char* _analyticsEnvironmentGetPublisherAppIdentifier(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] publisherAppID]);
    }

    const char* _analyticsEnvironmentGetFrameworkName(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] frameworkName]);
    }

    const char* _analyticsEnvironmentGetFrameworkVersion(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] frameworkVersion]);
    }

    const char* _analyticsEnvironmentGetPlayerIdentifier(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] playerID]);
    }

    const char* _analyticsEnvironmentGetUserAgent(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] userAgent]);
    }

    const char* _analyticsEnvironmentGetAdvertisingIdentifier(){
        return getCStringOrNull([[ChartboostCore analyticsEnvironment] advertisingID]);
    }
}
