@file:Suppress("PackageDirectoryMismatch")
package com.chartboost.core.unity.bridge

import com.chartboost.core.ChartboostCore
import com.chartboost.core.consent.ConsentDialogType
import com.chartboost.core.consent.ConsentSource
import com.chartboost.core.unity.result.ResultBoolean
import com.chartboost.mediation.unity.logging.LogLevel
import com.chartboost.mediation.unity.logging.UnityLoggingBridge
import com.unity3d.player.UnityPlayer
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Suppress("unused")
class BridgeCMP {

    companion object
    {
        private val TAG = BridgeCMP::class.simpleName

        @JvmStatic
        fun grantConsentStatus(statusSource: ConsentSource, result: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val grantConsentResult = ChartboostCore.consent.grantConsent(it, statusSource)
                    val success = grantConsentResult.isSuccess
                    UnityLoggingBridge.log(TAG, "Granted Consent Status: $success", LogLevel.VERBOSE)
                    result.onResult(success)
                }
            }
        }

        @JvmStatic
        fun denyConsentStatus(statusSource: ConsentSource, result: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val denyConsentResult = ChartboostCore.consent.denyConsent(it, statusSource)
                    val success = denyConsentResult.isSuccess
                    UnityLoggingBridge.log(TAG, "Denied Consent Status: $success", LogLevel.VERBOSE)
                    result.onResult(success)
                }
            }
        }

        @JvmStatic
        fun resetConsentStatus(result: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val resetConsentResult = ChartboostCore.consent.resetConsent(it)
                    val success = resetConsentResult.isSuccess
                    UnityLoggingBridge.log(TAG, "Reset Consent Status: $success", LogLevel.VERBOSE)
                    result.onResult(success)
                }
            }
        }

        @JvmStatic
        fun showConsentDialog(dialogType: ConsentDialogType, resultBoolean: ResultBoolean)
        {
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val showConsentDialogResult = ChartboostCore.consent.showConsentDialog(it, dialogType)
                    val success = showConsentDialogResult.isSuccess
                    UnityLoggingBridge.log(TAG, "Showed Consent Dialog: $success", LogLevel.VERBOSE)
                    resultBoolean.onResult(success)
                }
            }
        }
    }
}
