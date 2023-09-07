package com.chartboost.core.unity

import com.chartboost.core.ChartboostCore
import com.chartboost.core.consent.ConsentDialogType
import com.chartboost.core.consent.ConsentStatusSource
import com.unity3d.player.UnityPlayer
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Suppress("unused")
class BridgeCMP {

    companion object
    {
        @JvmStatic
        fun grantConsentStatus(statusSource: ConsentStatusSource, booleanResultAwaiter: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val result = ChartboostCore.consent.grantConsent(it, statusSource)
                    booleanResultAwaiter.onResult(result.isSuccess)
                }
            }
        }

        @JvmStatic
        fun denyConsentStatus(statusSource: ConsentStatusSource, booleanResultAwaiter: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val result = ChartboostCore.consent.denyConsent(it, statusSource)
                    booleanResultAwaiter.onResult(result.isSuccess)
                }
            }
        }

        @JvmStatic
        fun resetConsentStatus(booleanResultAwaiter: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val result = ChartboostCore.consent.resetConsent(it)
                    booleanResultAwaiter.onResult(result.isSuccess)
                }
            }
        }

        @JvmStatic
        fun showConsentDialog(dialogType: ConsentDialogType, booleanResultAwaiter: ResultBoolean)
        {
            CoroutineScope(Dispatchers.Main).launch {
                UnityPlayer.currentActivity.let {
                    val result = ChartboostCore.consent.showConsentDialog(it, dialogType)
                    booleanResultAwaiter.onResult(result.isSuccess)
                }
            }
        }

    }
}
