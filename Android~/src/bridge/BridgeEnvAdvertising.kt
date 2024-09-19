@file:Suppress("PackageDirectoryMismatch")
package com.chartboost.core.unity.bridge

import com.chartboost.core.ChartboostCore
import com.chartboost.core.unity.result.ResultBoolean
import com.chartboost.core.unity.result.ResultString
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Suppress("unused")
class BridgeEnvAdvertising {
    companion object {
        @JvmStatic
        fun getAdvertisingIdentifier(stringResult: ResultString){
            CoroutineScope(Dispatchers.Main).launch {
                val advertisingIdentifier = ChartboostCore.advertisingEnvironment.getAdvertisingIdentifier()
                stringResult.onResult(advertisingIdentifier)
            }
        }

        @JvmStatic
        fun getLimitAdTrackingEnabled(booleanResult: ResultBoolean){
            CoroutineScope(Dispatchers.Main).launch {
                val limitAdTrackingEnabled = ChartboostCore.advertisingEnvironment.getLimitAdTrackingEnabled()
                booleanResult.onResult(limitAdTrackingEnabled)
            }
        }
    }
}
