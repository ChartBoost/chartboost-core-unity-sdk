#import "CBCDelegates.h"
#import "UnityAppController.h"
#import "CBCUnityObserver.h"

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
            callback(hashCode, result);
        }];
    }

    void _CBCDenyConsent(int source, int hashCode, ChartboostCoreResultBoolean callback){
        [[ChartboostCore consent] denyConsentWithSource:(CBCConsentSource)source completion:^(BOOL result) {
            callback(hashCode, result);
        }];
    }

    void _CBCResetConsent(int hashCode, ChartboostCoreResultBoolean callback){
        [[ChartboostCore consent] resetConsentWithCompletion:^(BOOL result) {
            callback(hashCode, result);
        }];
    }

    void _CBCShowConsentDialog(int dialogType, int hashCode, ChartboostCoreResultBoolean callback){
        CBCConsentDialogType consentDialogType = (CBCConsentDialogType)dialogType;
        [[ChartboostCore consent] showConsentDialog:consentDialogType from:UnityGetGLViewController() completion:^(BOOL result) {
            callback(hashCode, result);
        }];
    }

    void _CBCSetConsentCallbacks(ChartboostCoreOnConsentChangeWithFullConsents onConsentChange, ChartboostCoreOnConsentReadyWithInitialConsents onConsentModuleReady){
        [[CBCUnityObserver sharedObserver] setOnConsentChange:onConsentChange];
        [[CBCUnityObserver sharedObserver] setOnConsentReady:onConsentModuleReady];
        [[ChartboostCore consent] addObserver:[CBCUnityObserver sharedObserver]];
    }
}
