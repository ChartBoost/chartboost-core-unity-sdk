#import "CBCUnityUtilities.h"

extern "C" {
    const char* _attributionEnvironmentGetAdvertisingIdentifier(){
        return getCStringOrNull([[ChartboostCore attributionEnvironment] advertisingID]);
    }

    void _attributionEnvironmentGetUserAgent(int hashCode, ChartbosotCoreResultString onResult){
        [[ChartboostCore attributionEnvironment] userAgentWithCompletion:^(NSString * _Nonnull userAgent) {
            onResult(hashCode, getCStringOrNull(userAgent));
        }];
    }
}
