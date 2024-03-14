#import "CBCDelegates.h"

extern "C" {
    const char* _attributionEnvironmentGetAdvertisingIdentifier(){
        return toCStringOrNull([[ChartboostCore attributionEnvironment] advertisingID]);
    }

    void _attributionEnvironmentGetUserAgent(int hashCode, ChartbosotCoreResultString onResult){
        [[ChartboostCore attributionEnvironment] userAgentWithCompletion:^(NSString * _Nonnull userAgent) {
            onResult(hashCode, toCStringOrNull(userAgent));
        }];
    }
}
