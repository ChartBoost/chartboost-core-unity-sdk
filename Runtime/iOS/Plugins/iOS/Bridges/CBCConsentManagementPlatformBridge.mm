#import "CBCUnityUtilities.h"
#import "UnityAppController.h"
#import "CBCUnityObserver.h"

extern "C" {
    int _chartboostCoreGetConsentStatus(){
        return (int)[[ChartboostCore consent] consentStatus];
    }

    const char* _chartboostCoreGetConsents(){
        NSDictionary<CBCConsentStandard *, CBCConsentValue*>* consents = [[ChartboostCore consent] consents];
        NSMutableDictionary *retConsents = [NSMutableDictionary dictionary];
        for(CBCConsentStandard* standard in consents)
        {
            CBCConsentValue* consentValue = consents[standard];
            retConsents[[standard value]] = [consentValue value];
        }
        return dictToJson(retConsents);
    }

    bool _chartboostCoreShouldCollectConsent(){
        return [[ChartboostCore consent] shouldCollectConsent];
    }

    void _chartboostCoreGrantConsent(int statusSource, int hashCode, ChartboostCoreResultBoolean callback){
        [[ChartboostCore consent] grantConsentWithSource:(CBCConsentStatusSource)statusSource completion:^(BOOL result) {
            callback(hashCode, result);
        }];
    }

    void _chartboostCoreDenyConsent(int statusSource, int hashCode, ChartboostCoreResultBoolean callback){
        [[ChartboostCore consent] denyConsentWithSource:(CBCConsentStatusSource)statusSource completion:^(BOOL result) {
            callback(hashCode, result);
        }];
    }

    void _chartboostCoreResetConsent(int hashCode, ChartboostCoreResultBoolean callback){
        [[ChartboostCore consent] resetConsentWithCompletion:^(BOOL result) {
            callback(hashCode, result);
        }];
    }

    void _chartboostCoreShowConsentDialog(int dialogType, int hashCode, ChartboostCoreResultBoolean callback){
        CBCConsentDialogType consentDialogType = (CBCConsentDialogType)dialogType;
        [[ChartboostCore consent] showConsentDialog:consentDialogType from:UnityGetGLViewController() completion:^(BOOL result) {
            callback(hashCode, result);
        }];
    }

    void _chartboostCoreSetConsentCallbacks(ChartboostCoreOnConsentStatusChangeCallback onConsentStatusChange, ChartboostCoreOnConsentChangeCallback onConsentChangeForStandard, ChartboostCoreAction onConsentModuleReady){
        [[CBCUnityObserver sharedObserver] setOnConsentStatusChange:onConsentStatusChange];
        [[CBCUnityObserver sharedObserver] setOnConsentChangeForStandard:onConsentChangeForStandard];
        [[CBCUnityObserver sharedObserver] setOnConsentReady:onConsentModuleReady];
        [[ChartboostCore consent] addObserver:[CBCUnityObserver sharedObserver]];
    }
}
