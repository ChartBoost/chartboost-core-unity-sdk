package com.chartboost.core.unity.bridge

import com.chartboost.core.ChartboostCore
import com.chartboost.core.consent.ConsentDialogType
import com.chartboost.core.consent.ConsentSource
import com.chartboost.core.unity.result.ResultBoolean
import com.unity3d.player.UnityPlayer
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Suppress("unused")
class BridgeCMP {

    companion object
    {
        @JvmStatic
        fun grantConsentStatus(statusSource: ConsentSource, result: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val grantConsentResult = ChartboostCore.consent.grantConsent(it, statusSource)
                    result.onResult(grantConsentResult.isSuccess)
                }
            }
        }

        @JvmStatic
        fun denyConsentStatus(statusSource: ConsentSource, result: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val denyConsentResult = ChartboostCore.consent.denyConsent(it, statusSource)
                    result.onResult(denyConsentResult.isSuccess)
                }
            }
        }

        @JvmStatic
        fun resetConsentStatus(resultBoolean: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val result = ChartboostCore.consent.resetConsent(it)
                    resultBoolean.onResult(result.isSuccess)
                }
            }
        }

        @JvmStatic
        fun showConsentDialog(dialogType: ConsentDialogType, resultBoolean: ResultBoolean)
        {
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val result = ChartboostCore.consent.showConsentDialog(it, dialogType)
                    resultBoolean.onResult(result.isSuccess)
                }
            }
        }

    }
}
