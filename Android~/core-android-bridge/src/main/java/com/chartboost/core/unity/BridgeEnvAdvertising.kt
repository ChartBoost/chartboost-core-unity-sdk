package com.chartboost.core.unity

import com.chartboost.core.ChartboostCore
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
