#import "CBCDelegates.h"
#import "CBCUnityObserver.h"

extern "C" {
    void _CBCSetIsUserUnderage(bool isUnderage){
        [[ChartboostCore publisherMetadata] setIsUserUnderage:isUnderage];
    }

    void _CBCSetPublisherSessionIdentifier(const char* publisherSessionIdentifier)
    {
        [[ChartboostCore publisherMetadata] setPublisherSessionID:toNSStringOrNull(publisherSessionIdentifier)];
    }

    void _CBCSetPublisherAppIdentifier(const char* publisherAppIdentifier)
    {
        [[ChartboostCore publisherMetadata] setPublisherAppID:toNSStringOrNull(publisherAppIdentifier)];
    }

    void _CBCSetFramework(const char* frameworkName, const char* frameworkVersion)
    {
        [[ChartboostCore publisherMetadata] setFrameworkWithName:toNSStringOrNull(frameworkName) version:toNSStringOrNull(frameworkVersion)];
    }

    void _CBCSetPlayerIdentifier(const char* playerIdentifier)
    {
        [[ChartboostCore publisherMetadata] setPlayerID:toNSStringOrNull(playerIdentifier)];
    }
}
