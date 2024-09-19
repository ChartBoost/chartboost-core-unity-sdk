#import "CBCDelegates.h"
#import "UnityAppController.h"
#import "CBCUnityObserver.h"

NSString* const CBCCMPBridgeTAG = @"CBCCMPBridge";

extern "C" {
    const char* _CBCGetConsents(){
        NSDictionary *consents = [[ChartboostCore consent] consents];
        return toJSON(consents);
    }

    bool _CBCShouldCollectConsent(){
        return [[ChartboostCore consent] shouldCollectConsent];
    }

    void _CBCGrantConsent(int source, int hashCode, ChartboostCoreResultBoolean callback){
        [[ChartboostCore consent] grantConsentWithSource:(CBCConsentSource)source completion:^(BOOL result) {
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCCMPBridgeTAG log:[NSString stringWithFormat:@"Granted Consent Status: %d", result] logLevel:CBLLogLevelVerbose];
            callback(hashCode, result);
        }];
    }

    void _CBCDenyConsent(int source, int hashCode, ChartboostCoreResultBoolean callback){
        [[ChartboostCore consent] denyConsentWithSource:(CBCConsentSource)source completion:^(BOOL result) {
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCCMPBridgeTAG log:[NSString stringWithFormat:@"Denied Consent Status: %d", result] logLevel:CBLLogLevelVerbose];
            callback(hashCode, result);
        }];
    }

    void _CBCResetConsent(int hashCode, ChartboostCoreResultBoolean callback){
        [[ChartboostCore consent] resetConsentWithCompletion:^(BOOL result) {
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCCMPBridgeTAG log:[NSString stringWithFormat:@"Reset Consent Status: %d", result] logLevel:CBLLogLevelVerbose];
            callback(hashCode, result);
        }];
    }

    void _CBCShowConsentDialog(int dialogType, int hashCode, ChartboostCoreResultBoolean callback){
        CBCConsentDialogType consentDialogType = (CBCConsentDialogType)dialogType;
        [[ChartboostCore consent] showConsentDialog:consentDialogType from:UnityGetGLViewController() completion:^(BOOL result) {
            [[CBLUnityLoggingBridge sharedLogger] logWithTag:CBCCMPBridgeTAG log:[NSString stringWithFormat:@"Showed Consent Dialog: %d", result] logLevel:CBLLogLevelVerbose];
            callback(hashCode, result);
        }];
    }

    void _CBCSetConsentCallbacks(ChartboostCoreOnConsentChangeWithFullConsents onConsentChange, ChartboostCoreOnConsentReadyWithInitialConsents onConsentModuleReady){
        [[CBCUnityObserver sharedObserver] setOnConsentChange:onConsentChange];
        [[CBCUnityObserver sharedObserver] setOnConsentReady:onConsentModuleReady];
        [[ChartboostCore consent] addObserver:[CBCUnityObserver sharedObserver]];
    }
}
