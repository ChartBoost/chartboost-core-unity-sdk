#import "CBCUnityUtilities.h"

extern "C" {
    void _publisherMetadataSetIsUserUnderage(bool isUnderage){
        [[ChartboostCore publisherMetadata] setIsUserUnderage:isUnderage];
    }

    void _publisherMetadataSetPublisherSessionIdentifier(const char* publisherSessionIdentifier)
    {
        [[ChartboostCore publisherMetadata] setPublisherSessionID:getNSStringOrNil(publisherSessionIdentifier)];
    }

    void _publisherMetadataSetPublisherAppIdentifier(const char* publisherAppIdentifier)
    {
        [[ChartboostCore publisherMetadata] setPublisherAppID:getNSStringOrNil(publisherAppIdentifier)];
    }

    void _publisherMetadataSetFrameworkName(const char* frameworkName)
    {
        [[ChartboostCore publisherMetadata] setFrameworkName:getNSStringOrNil(frameworkName)];
    }

    void _publisherMetadataSetFrameworkVersion(const char* frameworkVersion)
    {
        [[ChartboostCore publisherMetadata] setFrameworkVersion:getNSStringOrNil(frameworkVersion)];
    }

    void _publisherMetadataPlayerIdentifier(const char* playerIdentifier)
    {
        [[ChartboostCore publisherMetadata] setPlayerID:getNSStringOrNil(playerIdentifier)];
    }
}
