#import "CBCUnityUtilities.h"

extern "C" {
    const char* _attributionEnvironmentGetAdvertisingIdentifier(){
        return getCStringOrNull([[ChartboostCore attributionEnvironment] advertisingID]);
    }

    const char* _attributionEnvironmentGetUserAgent(){
        return getCStringOrNull([[ChartboostCore attributionEnvironment] userAgent]);
    }
}
