#import "CBCDelegates.h"
#import "CBCUnityObserver.h"

NSString* const CBCPublisherMetadataTAG = @"CBCPublisherMetadata";

extern "C" {
    void _CBCSetIsUserUnderage(bool isUnderage)
    {
        [[ChartboostCore publisherMetadata] setIsUserUnderage:isUnderage];
        [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCPublisherMetadataTAG log:[NSString stringWithFormat:@"Set IsUserUnderage: %d", isUnderage] logLevel:CBLLogLevelVerbose];
    }

    void _CBCSetPublisherSessionIdentifier(const char* publisherSessionIdentifier)
    {
        NSString* nsPublisherSessionIdentifier = toNSStringOrNull(publisherSessionIdentifier);
        [[ChartboostCore publisherMetadata] setPublisherSessionID:nsPublisherSessionIdentifier];
        [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCPublisherMetadataTAG log:[NSString stringWithFormat:@"Set Publisher Session Identifier: %@", nsPublisherSessionIdentifier] logLevel:CBLLogLevelVerbose];
    }

    void _CBCSetPublisherAppIdentifier(const char* publisherAppIdentifier)
    {
        NSString* nsPublisherAppIdentifier = toNSStringOrNull(publisherAppIdentifier);
        [[ChartboostCore publisherMetadata] setPublisherAppID:nsPublisherAppIdentifier];
        [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCPublisherMetadataTAG log:[NSString stringWithFormat:@"Set Publisher App Identifier: %@", nsPublisherAppIdentifier] logLevel:CBLLogLevelVerbose];
    }

    void _CBCSetFramework(const char* frameworkName, const char* frameworkVersion)
    {
        NSString* nsFrameworkName = toNSStringOrNull(frameworkName);
        NSString* nsFrameworkVersion = toNSStringOrNull(frameworkVersion);
        [[ChartboostCore publisherMetadata] setFrameworkWithName:nsFrameworkName version:nsFrameworkVersion];
        [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCPublisherMetadataTAG log:[NSString stringWithFormat:@"Set Framework Name: %@ and Version: %@", nsFrameworkName, nsFrameworkVersion] logLevel:CBLLogLevelVerbose];
    }

    void _CBCSetPlayerIdentifier(const char* playerIdentifier)
    {
        NSString* nsPlayerIdentifier = toNSStringOrNull(playerIdentifier);
        [[ChartboostCore publisherMetadata] setPlayerID:nsPlayerIdentifier];
        [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCPublisherMetadataTAG log:[NSString stringWithFormat:@"Set Player Identifier: %@", nsPlayerIdentifier] logLevel:CBLLogLevelVerbose];
    }
}
