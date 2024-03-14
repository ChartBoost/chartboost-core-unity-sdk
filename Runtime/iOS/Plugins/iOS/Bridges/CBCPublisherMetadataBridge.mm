#import "CBCDelegates.h"

extern "C" {
    void _publisherMetadataSetIsUserUnderage(bool isUnderage){
        [[ChartboostCore publisherMetadata] setIsUserUnderage:isUnderage];
    }

    void _publisherMetadataSetPublisherSessionIdentifier(const char* publisherSessionIdentifier)
    {
        [[ChartboostCore publisherMetadata] setPublisherSessionID:toNSStringOrNull(publisherSessionIdentifier)];
    }

    void _publisherMetadataSetPublisherAppIdentifier(const char* publisherAppIdentifier)
    {
        [[ChartboostCore publisherMetadata] setPublisherAppID:toNSStringOrNull(publisherAppIdentifier)];
    }

    void _publisherMetadataSetFrameworkName(const char* frameworkName)
    {
        [[ChartboostCore publisherMetadata] setFrameworkName:toNSStringOrNull(frameworkName)];
    }

    void _publisherMetadataSetFrameworkVersion(const char* frameworkVersion)
    {
        [[ChartboostCore publisherMetadata] setFrameworkVersion:toNSStringOrNull(frameworkVersion)];
    }

    void _publisherMetadataPlayerIdentifier(const char* playerIdentifier)
    {
        [[ChartboostCore publisherMetadata] setPlayerID:toNSStringOrNull(playerIdentifier)];
    }
}
