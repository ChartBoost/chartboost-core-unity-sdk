#import "CBCDelegates.h"
#import "CBCUnityObserver.h"

extern "C" {
    void _CBCSetEnvironmentCallbacks(ChartboostCoreOnEnumStatusChange onEnvironmentPropertyChanged){
        [[CBCUnityObserver sharedObserver] setOnEnvironmentPropertyChanged:onEnvironmentPropertyChanged];

        [[ChartboostCore analyticsEnvironment] addObserver:[CBCUnityObserver sharedObserver]];
    }

    const char* _CBCAnalyticsGetOsName(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] osName]);
    }

    const char* _CBCAnalyticsGetOsVersion(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] osVersion]);
    }

    const char* _CBCAnalyticsGetDeviceMake(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] deviceMake]);
    }

    const char* _CBCAnalyticsGetDeviceModel(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] deviceModel]);
    }

    const char* _CBCAnalyticsGetDeviceLocale(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] deviceLocale]);
    }

    double _CBCAnalyticsGetScreenHeight(){
        return [[ChartboostCore analyticsEnvironment] screenHeightPixels];
    }

    double _CBCAnalyticsGetScreenScale(){
        return [[ChartboostCore analyticsEnvironment] screenScale];
    }

    double _CBCAnalyticsGetScreenWidth(){
        return [[ChartboostCore analyticsEnvironment] screenWidthPixels];
    }

    const char * _CBCAnalyticsGetBundleIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] bundleID]);
    }

    bool _CBCAnalyticsGetLimitAdTrackingEnabled(){
        return [[ChartboostCore analyticsEnvironment] isLimitAdTrackingEnabled];
    }

    int _CBCAnalyticsGetNetworkConnectionType() {
        return (int)[[ChartboostCore analyticsEnvironment] networkConnectionType];
    }

    double _CBCAnalyticsGetVolume(){
        return [[ChartboostCore analyticsEnvironment] volume];
    }

    const char* _CBCAnalyticsGetVendorIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] vendorID]);
    }

    int _CBCAnalyticsGetVendorIdentifierScope(){
        return (int)[[ChartboostCore analyticsEnvironment] vendorIDScope];
    }

    int _CBCAnalyticsGetAuthorizationStatus(){
        if (@available(iOS 14.0, *))
            return (int)[[ChartboostCore analyticsEnvironment] appTrackingTransparencyStatus];
        return 3; // 3 == ATTrackingManagerAuthorizationStatusAuthorized
    }

    const char* _CBCAnalyticsGetAppVersion(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] appVersion]);
    }

    double _CBCAnalyticsGetAppSessionDuration(){
        return (double)[[ChartboostCore analyticsEnvironment] appSessionDuration];
    }

    const char* _CBCAnalyticsGetAppSessionIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] appSessionID]);
    }

    bool _CBCAnalyticsGetIsUserUnderage(){
        return [[ChartboostCore analyticsEnvironment] isUserUnderage];
    }

    const char* _CBCAnalyticsGetPublisherSessionIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] publisherSessionID]);
    }

    const char* _CBCAnalyticsGetPublisherAppIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] publisherAppID]);
    }

    const char* _CBCAnalyticsGetFrameworkName(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] frameworkName]);
    }

    const char* _CBCAnalyticsGetFrameworkVersion(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] frameworkVersion]);
    }

    const char* _CBCAnalyticsGetPlayerIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] playerID]);
    }

    void _CBCAnalyticsGetUserAgent(int hashCode, ChartbosotCoreResultString onResult){
        [[ChartboostCore analyticsEnvironment] userAgentWithCompletion:^(NSString * _Nonnull userAgent) {
            onResult(hashCode, toCStringOrNull(userAgent));
        }];
    }

    const char* _CBCAnalyticsGetAdvertisingIdentifier(){
        return toCStringOrNull([[ChartboostCore analyticsEnvironment] advertisingID]);
    }
}
