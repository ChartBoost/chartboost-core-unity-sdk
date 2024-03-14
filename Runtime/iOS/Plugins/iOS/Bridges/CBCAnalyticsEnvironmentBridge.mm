#import "CBCDelegates.h"

extern "C" {
    const char* _analyticsEnvironmentGetOsName(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] osName]);
    }

    const char* _analyticsEnvironmentGetOsVersion(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] osVersion]);
    }

    const char* _analyticsEnvironmentGetDeviceMake(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] deviceMake]);
    }

    const char* _analyticsEnvironmentGetDeviceModel(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] deviceModel]);
    }

    const char* _analyticsEnvironmentGetDeviceLocale(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] deviceLocale]);
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
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] bundleID]);
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
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] vendorID]);
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
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] appVersion]);
    }

    double _analyticsEnvironmentGetAppSessionDuration(){
        return (double)[[ChartboostCore analyticsEnvironment] appSessionDuration];
    }

    const char* _analyticsEnvironmentGetAppSessionIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] appSessionID]);
    }

    bool _analyticsEnvironmentGetIsUserUnderage(){
        return [[ChartboostCore analyticsEnvironment] isUserUnderage];
    }

    const char* _analyticsEnvironmentGetPublisherSessionIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] publisherSessionID]);
    }

    const char* _analyticsEnvironmentGetPublisherAppIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] publisherAppID]);
    }

    const char* _analyticsEnvironmentGetFrameworkName(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] frameworkName]);
    }

    const char* _analyticsEnvironmentGetFrameworkVersion(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] frameworkVersion]);
    }

    const char* _analyticsEnvironmentGetPlayerIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] playerID]);
    }

    void _analyticsEnvironmentGetUserAgent(int hashCode, ChartbosotCoreResultString onResult){
        [[ChartboostCore analyticsEnvironment] userAgentWithCompletion:^(NSString * _Nonnull userAgent) {
            onResult(hashCode, toCStringOrNull(userAgent));
        }];
    }

    const char* _analyticsEnvironmentGetAdvertisingIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] advertisingID]);
    }
}
