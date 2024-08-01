#import "CBCDelegates.h"

extern "C" {
    const char* _CBCAttributionGetAdvertisingIdentifier(){
        return toCStringOrNull([[ChartboostCore attributionEnvironment] advertisingID]);
    }

    void _CBCAttributionGetUserAgent(int hashCode, ChartbosotCoreResultString onResult){
        [[ChartboostCore attributionEnvironment] userAgentWithCompletion:^(NSString * _Nonnull userAgent) {
            onResult(hashCode, toCStringOrNull(userAgent));
        }];
    }
}
