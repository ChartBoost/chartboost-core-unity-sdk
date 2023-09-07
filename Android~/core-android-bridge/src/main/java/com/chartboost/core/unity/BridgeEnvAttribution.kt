package com.chartboost.core.unity

import com.chartboost.core.ChartboostCore
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

@Suppress("unused")
class BridgeEnvAttribution {
    companion object {
        @JvmStatic
        fun getAdvertisingIdentifier(stringResult: ResultString){
            CoroutineScope(Dispatchers.Main).launch {
                val advertisingIdentifier = ChartboostCore.attributionEnvironment.getAdvertisingIdentifier()
                stringResult.onResult(advertisingIdentifier)
            }
        }

        @JvmStatic
        fun getUserAgent(stringResult: ResultString){
            CoroutineScope(Dispatchers.Main).launch {
                val userAgent = ChartboostCore.attributionEnvironment.getUserAgent()
                stringResult.onResult(userAgent)
            }
        }
    }
}
